using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using MaddieQoL.Util;

namespace MaddieQoL.Content.MusicBoxes.Tiles;

public sealed class SilenceBox : ModTile {
	private static Asset<Texture2D> GlowTexture {get; set;}

	public override void SetStaticDefaults() {
		GlowTexture = ModContent.Request<Texture2D>($"{this.Texture}_Glow");

		Main.tileFrameImportant[this.Type] = true;
		Main.tileObsidianKill[this.Type] = true;
		TileID.Sets.HasOutlines[this.Type] = true;
		TileID.Sets.DisableSmartCursor[this.Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(this.Type);

		this.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.MusicBox"));
	}

	public override void MouseOver(int i, int j) {
		Player player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.SilenceBox>();
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
		return true;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
		Tile tile = Framing.GetTileSafely(i, j);
		if (!TileDrawing.IsVisible(tile)) {return;}
		int drawYOffset = TileObjectData.GetTileData(tile).DrawYOffset;
		spriteBatch.Draw(
			GlowTexture.Value,
			TileUtil.TileDrawPosition(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + drawYOffset),
			new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16),
			Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f
		);
	}

	public override void NumDust(int i, int j, bool fail, ref int num) {
		num = 0;
	}
}
