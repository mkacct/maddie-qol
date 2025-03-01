using static MaddieQoL.Common.Shorthands;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace MaddieQoL.Content.Misc;

public sealed class BuffFurnitureAutoActivator : ModSystem {
	private const int BuffTimeInfinite = 108000;

	private readonly record struct TileBuffData(
		ushort TileID,
		int BuffID,
		SoundStyle SoundStyle,
		Func<BuffFurnitureAutoActivationPrefs, bool> CheckEnabled
	);

	private static readonly ISet<TileBuffData> FurnitureInfiniteBuffs = new HashSet<TileBuffData> {
		new(TileID.CrystalBall, BuffID.Clairvoyance, SoundID.Item4, (prefs) => prefs.enableForCrystalBall),
		new(TileID.AmmoBox, BuffID.AmmoBox, SoundID.Item149, (prefs) => prefs.enableForAmmoBox),
		new(TileID.BewitchingTable, BuffID.Bewitched, SoundID.Item4, (prefs) => prefs.enableForBewitchingTable),
		new(TileID.SharpeningStation, BuffID.Sharpened, SoundID.Item37, (prefs) => prefs.enableForSharpeningStation),
		new(TileID.WarTable, BuffID.WarTable, SoundID.Item4, (prefs) => prefs.enableForWarTable),
	}; // cake is limited duration, so it is omitted

	public override void PostUpdatePlayers() {
		if (Main.netMode == NetmodeID.Server) {return;}
		if (!ModuleConf.enableBuffFurnitureAutoActivation) {return;}
		if (Main.LocalPlayer.DeadOrGhost) {return;}
		ClientCheckBuffFurniture(Main.LocalPlayer);
	}

	private static void ClientCheckBuffFurniture(Player player) {
		foreach (TileBuffData data in FurnitureInfiniteBuffs) {
			if (!data.CheckEnabled(UserConf.buffFurnitureAutoActivationPrefs)) {continue;}
			if (player.HasBuff(data.BuffID)) {continue;}
			if (player.IsTileTypeInInteractionRange(data.TileID, TileReachCheckSettings.Simple)) {
				player.AddBuff(data.BuffID, BuffTimeInfinite, false);
				SoundEngine.PlaySound(data.SoundStyle, player.Center);
			}
		}
	}
}
