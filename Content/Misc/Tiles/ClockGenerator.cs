using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Audio;

namespace MaddieQoL.Content.Misc.Tiles;

public sealed class ClockGenerator : ModTile {
	public override void SetStaticDefaults() {
		Main.tileFrameImportant[this.Type] = true;
		Main.tileSolid[this.Type] = false;
		TileID.Sets.HasOutlines[this.Type] = true;
		TileID.Sets.IsATrigger[this.Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleLineSkip = 6;
		TileObjectData.newTile.StyleWrapLimit = 6;
		TileObjectData.addTile(this.Type);

		this.DustType = DustID.TintableDust;

		this.AddMapEntry(new Color(62, 28, 87), this.CreateMapEntryName());
	}

	public override void MouseOver(int i, int j) {
		Player player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.ClockGenerator>();
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
		return true;
	}

	public override bool RightClick(int i, int j) {
		SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16 + 8, j * 16 + 8));
		ToggleTile(i, j);
		return true;
	}

	public override void HitWire(int i, int j) {
		ToggleTile(i, j);
	}

	private static bool IsOn(Tile tile) {
		return tile.TileFrameY >= 18;
	}

	private static void ToggleTile(int i, int j) {
		Tile tile = Framing.GetTileSafely(i, j);
		Main.tile[i, j].TileFrameY += (short)(IsOn(tile) ? -18 : 18);
		if (Main.netMode != NetmodeID.SinglePlayer) {
			NetMessage.SendTileSquare(-1, i, j, 1, 1);
		}
	}

	public override void AnimateTile(ref int frame, ref int frameCounter) {
		if (++frameCounter >= 4) {
			frameCounter = 0;
			frame = ++frame % 6;
		}
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) {
		Tile tile = Framing.GetTileSafely(i, j);
		if (IsOn(tile)) {
			frameXOffset = Main.tileFrame[type] * 18;
		}
	}
}
