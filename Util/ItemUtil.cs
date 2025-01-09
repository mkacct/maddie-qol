using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace MaddieQoL.Util;

public sealed class ItemUtil {
	private ItemUtil() {} // prevent instantiation

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
}
