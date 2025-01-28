using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MaddieQoL.Content.Misc.Tiles;

public sealed class Diode : ModTile {
	public override void SetStaticDefaults() {
		Main.tileFrameImportant[this.Type] = true;
		Main.tileSolid[this.Type] = false;
		TileID.Sets.IsATrigger[this.Type] = true;
		TileID.Sets.IsAMechanism[this.Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(this.Type);

		this.AddMapEntry(new Color(255, 255, 42), this.CreateMapEntryName());
	}

	public override void HitWire(int i, int j) {
		Tile tile = Framing.GetTileSafely(i, j);
		if (tile.TileFrameY == 0) {
			// TODO: somehow trip wire of below tile
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num) {
		num = 0;
	}
}
