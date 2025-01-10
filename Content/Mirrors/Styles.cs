using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace MaddieQoL.Content.Mirrors;

internal static class Styles {
	internal static void UseRecallStyle(Player player, Item item) {
		UseMirrorStyle(player, item, () => {
			player.RemoveAllGrapplingHooks();
			player.Spawn(PlayerSpawnContext.RecallFromItem);
		});
	}

	internal static void UseReturnStyle(Player player, Item item) {
		UseMirrorStyle(player, item, () => {
			player.RemoveAllGrapplingHooks();
			if (player == Main.LocalPlayer) {
				MirrorReturnGateHoverIconOverrider.SetGateIcon(item.type);
			}
			player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
		});
	}

	internal static void UseOceanTpStyle(Player player, Item item) {
		UseTpStyle(player, item, () => {
			Vector2 vector = Vector2.UnitY.RotatedBy(player.itemAnimation * ((float)Math.PI * 2f) / 30f) * new Vector2(15f, 0f);
			for (int n = 0; n < 2; n++) {
				if (Main.rand.NextBool(3)) {
					Dust dust = Dust.NewDustPerfect(player.Bottom + vector, Dust.dustWater());
					dust.velocity.Y *= 0f;
					dust.velocity.Y -= 4.5f;
					dust.velocity.X *= 1.5f;
					dust.scale = 0.8f;
					dust.alpha = 130;
					dust.noGravity = true;
					dust.fadeIn = 1.1f;
				}
			}
		}, () => {
			if (Main.netMode == NetmodeID.SinglePlayer) {
				player.MagicConch();
			} else if ((Main.netMode == NetmodeID.MultiplayerClient) && (player == Main.LocalPlayer)) {
				NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 1);
			}
		});
	}

	internal static void UseHellTpStyle(Player player, Item item) {
		UseTpStyle(player, item, () => {
			Vector2 vector2 = Vector2.UnitY.RotatedBy(player.itemAnimation * ((float)Math.PI * 2f) / 30f) * new Vector2(15f, 0f);
			for (int num = 0; num < 2; num++) {
				if (Main.rand.NextBool(3)) {
					Dust dust2 = Dust.NewDustPerfect(player.Bottom + vector2, DustID.Lava);
					dust2.velocity.Y *= 0f;
					dust2.velocity.Y -= 4.5f;
					dust2.velocity.X *= 1.5f;
					dust2.scale = 0.8f;
					dust2.alpha = 130;
					dust2.noGravity = true;
					dust2.fadeIn = 1.1f;
				}
			}
		}, () => {
			if (Main.netMode == NetmodeID.SinglePlayer) {
				player.DemonConch();
			} else if ((Main.netMode == NetmodeID.MultiplayerClient) && (player == Main.LocalPlayer)) {
				NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 2);
			}
		});
	}

	internal static void UseSpawnTpStyle(Player player, Item item) {
		UseTpStyle(player, item, () => {
			if (Main.rand.NextBool(2)) {
				int num2 = Main.rand.Next(4);
				Color color = Color.Green;
				switch (num2) {
					case 0:
					case 1:
						color = new Color(100, 255, 100);
						break;
					case 2:
						color = Color.Yellow;
						break;
					case 3:
						color = Color.White;
						break;
				}
				Dust dust3 = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(player.Hitbox), DustID.RainbowMk2);
				dust3.noGravity = true;
				dust3.color = color;
				dust3.velocity *= 2f;
				dust3.scale = 0.8f + Main.rand.NextFloat() * 0.6f;
			}
		}, () => {
			if (Main.netMode == NetmodeID.SinglePlayer) {
				player.Shellphone_Spawn();
			} else if ((Main.netMode == NetmodeID.MultiplayerClient) && (player == Main.LocalPlayer)) {
				NetMessage.SendData(MessageID.RequestTeleportationByServer, -1, -1, null, 3);
			}
		});
	}

	private static void UseMirrorStyle(Player player, Item item, Action activate) {
		UseTpStyle(player, item, () => {
			if (Main.rand.NextBool(2)) {
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.1f);
			}
		}, () => {
			for (int i = 0; i < 70; i++) {
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default, 1.5f);
			}
			activate();
			for (int i = 0; i < 70; i++) {
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
			}
		});
	}

	private static void UseTpStyle(Player player, Item item, Action makeDust, Action activate) {
		if (player.itemTime == 0) {player.ApplyItemTime(item);}
		makeDust();
		if (player.itemTime == player.itemTimeMax / 2) {
			activate();
		}
	}
}
