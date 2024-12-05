using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MaddieQoL.Content.Misc.Tiles;

public class Button : ModTile {
	private static LocalizedText MapEntryName {get; set;}

	public override void SetStaticDefaults() {
		MapEntryName = this.GetLocalization(nameof(MapEntryName));

		Main.tileFrameImportant[this.Type] = true;
		Main.tileSolid[this.Type] = false;
		Main.tileObsidianKill[this.Type] = true;
		TileID.Sets.FramesOnKillWall[this.Type] = true;
		TileID.Sets.HasOutlines[this.Type] = true;
		TileID.Sets.IsATrigger[this.Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
		TileObjectData.newTile.AnchorWall = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.FlattenAnchors = true;
		TileObjectData.addTile(this.Type);

		this.DustType = DustID.Silver;

		AddMapEntry(new Color(213, 203, 204), MapEntryName);
	}

	public override void MouseOver(int i, int j) {
		Player player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		player.cursorItemIconID = ModContent.ItemType<Items.Button>();
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
		return true;
	}

    public override bool RightClick(int i, int j) {
        SoundEngine.PlaySound(SoundID.Mech, new Vector2(i*16, j*16));
		Wiring.TripWire(i, j, 1, 1);
		return true;
    }
}
