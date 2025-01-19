using static MaddieQoL.Common.Shorthands;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc;

public sealed class BuffFurnitureAutoActivator : ModSystem {
	private const int BuffTimeInfinite = 108000;

	private readonly record struct TileBuffData(ushort TileID, int BuffID, SoundStyle SoundStyle);

	private static readonly ISet<TileBuffData> FurnitureInfiniteBuffs = new HashSet<TileBuffData> {
		new(TileID.CrystalBall, BuffID.Clairvoyance, SoundID.Item4),
		new(TileID.AmmoBox, BuffID.AmmoBox, SoundID.Item149),
		new(TileID.BewitchingTable, BuffID.Bewitched, SoundID.Item4),
		new(TileID.SharpeningStation, BuffID.Sharpened, SoundID.Item37),
		new(TileID.WarTable, BuffID.WarTable, SoundID.Item4),
	}; // cake is limited duration, so it is omitted

	public override void PostUpdatePlayers() {
		if (Main.netMode == NetmodeID.Server) {return;}
		if (!ModuleConf.enableBuffFurnitureAutoActivation) {return;}
		if (Main.LocalPlayer.DeadOrGhost) {return;}
		ClientCheckBuffFurniture(Main.LocalPlayer);
	}

	private static void ClientCheckBuffFurniture(Player player) {
		foreach (TileBuffData data in FurnitureInfiniteBuffs) {
			if (player.HasBuff(data.BuffID)) {continue;}
			if (player.IsTileTypeInInteractionRange(data.TileID, TileReachCheckSettings.Simple)) {
				player.AddBuff(data.BuffID, BuffTimeInfinite, false);
				SoundEngine.PlaySound(data.SoundStyle, player.Center);
			}
		}
	}
}
