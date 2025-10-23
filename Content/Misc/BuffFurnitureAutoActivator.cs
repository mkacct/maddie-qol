using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.Misc;

public sealed class BuffFurnitureAutoActivator : ModSystem {

	const int BuffTimeInfinite = 108000;

	readonly record struct TileBuffData(
		ushort TileID,
		int BuffID,
		SoundStyle SoundStyle,
		Func<BuffFurnitureAutoActivationPrefs, bool> CheckEnabled
	);

	static readonly ISet<TileBuffData> FurnitureInfiniteBuffs = new HashSet<TileBuffData> {
		new(TileID.CrystalBall, BuffID.Clairvoyance, SoundID.Item4, (prefs) => prefs.enableForCrystalBall),
		new(TileID.AmmoBox, BuffID.AmmoBox, SoundID.Item149, (prefs) => prefs.enableForAmmoBox),
		new(TileID.BewitchingTable, BuffID.Bewitched, SoundID.Item4, (prefs) => prefs.enableForBewitchingTable),
		new(TileID.SharpeningStation, BuffID.Sharpened, SoundID.Item37, (prefs) => prefs.enableForSharpeningStation),
		new(TileID.WarTable, BuffID.WarTable, SoundID.Item4, (prefs) => prefs.enableForWarTable),
	}; // cake is limited duration, so it is omitted

	static void TryAddModFurniture(
		Mod mod,
		string tileName,
		string buffName,
		SoundStyle soundStyle,
		Func<BuffFurnitureAutoActivationPrefs, bool> checkEnabled
	) {
		if (mod.TryFind(tileName, out ModTile tile) && mod.TryFind(buffName, out ModBuff buff)) {
			FurnitureInfiniteBuffs.Add(new TileBuffData(
				tile.Type,
				buff.Type,
				soundStyle,
				checkEnabled
			));
		}
	}

	public override void SetStaticDefaults() {
		TryAddThoriumModContent();
	}

	public override void PostUpdatePlayers() {
		if (Main.netMode == NetmodeID.Server) {return;}
		if (!ModuleConf.enableBuffFurnitureAutoActivation) {return;}
		if (Main.LocalPlayer.DeadOrGhost) {return;}
		ClientCheckBuffFurniture(Main.LocalPlayer);
	}

	static void ClientCheckBuffFurniture(Player player) {
		if (!UserConf.buffFurnitureAutoActivationPrefs.masterEnable) {return;}
		foreach (TileBuffData data in FurnitureInfiniteBuffs) {
			if (!data.CheckEnabled(UserConf.buffFurnitureAutoActivationPrefs)) {continue;}
			if (player.HasBuff(data.BuffID)) {continue;}
			if (player.IsTileTypeInInteractionRange(data.TileID, TileReachCheckSettings.Simple)) {
				player.AddBuff(data.BuffID, BuffTimeInfinite, false);
				SoundEngine.PlaySound(data.SoundStyle, player.Center);
			}
		}
	}

	static void TryAddThoriumModContent() {
		if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod)) {
			TryAddModFurniture(
				thoriumMod, "Altar", "AltarBuff", SoundID.Item29,
				(prefs) => prefs.enableForAltar
			);
			TryAddModFurniture(
				thoriumMod, "ConductorsStand", "ConductorsStandBuff", SoundID.Item29,
				(prefs) => prefs.enableForConductorsStand
			);
			TryAddModFurniture(
				thoriumMod, "NinjaRack", "NinjaBuff", SoundID.Item37,
				(prefs) => prefs.enableForNinjaRack
			);
		}
	}

}
