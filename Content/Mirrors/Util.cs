using System;
using Terraria;
using Terraria.ID;

namespace MaddieQoL.Content.Mirrors;

internal sealed class MirrorUtil {
	private MirrorUtil() {} // prevent instantiation

	internal static void Recall(Player player, Item item) {
		player.RemoveAllGrapplingHooks();
		player.Spawn(PlayerSpawnContext.RecallFromItem);
	}

	internal static void Return(Player player, Item item) {
		player.RemoveAllGrapplingHooks();
		if (player == Main.LocalPlayer) {
			MirrorReturnGateHoverIconOverrider.SetGateIcon(item.type);
		}
		player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
	}

	internal static void UseMirrorStyle(Player player, Item item, Action<Player, Item> activate) {
		if (player.itemTime == 0) {player.ApplyItemTime(item);}
		MakeContinuousDust(player);
		if (player.itemTime == player.itemTimeMax / 2) {
			MakeSourceDust(player);
			activate(player, item);
			MakeDestinationDust(player);
		}
	}

	private static void MakeContinuousDust(Player player) {
		if (Main.rand.NextBool(2)) {
			Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
		}
	}

	private static void MakeSourceDust(Player player) {
		for (int i = 0; i < 70; i++) {
			Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default, 1.5f);
		}
	}

	private static void MakeDestinationDust(Player player) {
		for (int i = 0; i < 70; i++) {
			Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
		}
	}
}
