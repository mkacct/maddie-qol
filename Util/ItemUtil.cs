using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace MaddieQoL.Util;

public static class ItemUtil {
	public static void DrawItemInWorld(
		int drawItemId, Item thisItem,
		SpriteBatch spriteBatch, Color alphaColor, float rotation, float scale
	) {
		Main.GetItemDrawFrame(drawItemId, out Texture2D itemTexture, out Rectangle itemFrame);
		Vector2 drawOrigin = itemFrame.Size() / 2f;
		Vector2 drawPosition = thisItem.Bottom - Main.screenPosition - new Vector2(0, drawOrigin.Y);
		spriteBatch.Draw(itemTexture, drawPosition, itemFrame, alphaColor, rotation, drawOrigin, scale, SpriteEffects.None, 0);
	}

	public static void DrawHoldItemIcon(Player player, Item thisItem) {
		if (player != Main.LocalPlayer) {return;}
		if (!player.IsTargetTileInItemRange(thisItem)) {return;}
		player.cursorItemIconEnabled = true;
		Main.ItemIconCacheUpdate(thisItem.type);
	}

	public static void RegisterContainerItemLockHook(int containerItemId, Predicate<Player> tryUnlock) {
		On_ItemSlot.TryOpenContainer += (On_ItemSlot.orig_TryOpenContainer orig, Item item, Player player) => {
			if (item.type == containerItemId) {
				if (!tryUnlock(player)) {return;}
			}
			orig(item, player);
		};
	}
}
