using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace MaddieQoL.Util;

public sealed class ItemUtil {
	private ItemUtil() {} // prevent instantiation

	public static void DrawItemInWorld(
		int drawItemId, Item thisItem,
		SpriteBatch spriteBatch, Color lightColor, float rotation, float scale
	) {
		Main.GetItemDrawFrame(drawItemId, out var itemTexture, out var itemFrame);
		Vector2 drawOrigin = itemFrame.Size() / 2f;
		Vector2 drawPosition = thisItem.Bottom - Main.screenPosition - new Vector2(0, drawOrigin.Y);
		spriteBatch.Draw(itemTexture, drawPosition, itemFrame, lightColor, rotation, drawOrigin, scale, SpriteEffects.None, 0);
	}

	public static void DrawHoldItemIcon(Player player, Item thisItem) {
		if (player.whoAmI != Main.myPlayer) {return;}
		if (!player.IsTargetTileInItemRange(thisItem)) {return;}
		player.cursorItemIconEnabled = true;
		Main.ItemIconCacheUpdate(thisItem.type);
	}
}