using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MaddieQoL.Content.Misc.Tiles;

public class Button : ModTile {
	public override void SetStaticDefaults() {
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

		this.DustType = DustID.TintableDust;

		this.AddMapEntry(new Color(213, 203, 204), this.CreateMapEntryName());
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
		Wiring.HitSwitch(i, j);
		NetMessage.SendData(MessageID.HitSwitch, -1, -1, null, i, j);
		return true;
	}

	public override void Load() {
		On_Wiring.HitSwitch += (On_Wiring.orig_HitSwitch orig, int i, int j) => {
			bool overridden = OverrideHitSwitch(i, j);
			if (!overridden) {orig(i, j);}
		};
	}

	private static bool OverrideHitSwitch(int i, int j) {
		if (!WorldGen.InWorld(i, j) || Main.tile[i, j] == null) {return false;}
		if (Main.tile[i, j].TileType == ModContent.TileType<Button>()) {
			SoundEngine.PlaySound(SoundID.Mech, new Vector2(i*16, j*16));
			Wiring.TripWire(i, j, 1, 1);
			return true;
		}
		return false;
	}
}
