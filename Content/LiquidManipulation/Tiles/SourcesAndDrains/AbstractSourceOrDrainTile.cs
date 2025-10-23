using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public abstract class AbstractSourceOrDrainTile : ModTile {

	protected abstract TileObjectData CopyFromTileObjectData {get;}
	protected abstract LocalizedText MapEntryName {get;}

	public override void SetStaticDefaults() {
		Main.tileFrameImportant[this.Type] = true;
		Main.tileLavaDeath[this.Type] = false;
		TileID.Sets.IsAMechanism[this.Type] = true;

		this.DustType = DustID.Iron;

		TileObjectData.newTile.CopyFrom(this.CopyFromTileObjectData);
		TileObjectData.addTile(this.Type);

		this.AddMapEntry(new Color(144, 148, 144), this.MapEntryName);
	}

	public override void HitWire(int i, int j) {
		if (!ModuleConf.enableLiquidSourcesAndDrains) {return;}
		Tile targetTile = Framing.GetTileSafely(i, j);
		int x = i - targetTile.TileFrameX / 18;
		int y = j - targetTile.TileFrameY / 18;
		if (targetTile.TileFrameY / 18 > 1) {y -= 2;}
		for (int tileX = x; tileX < x + 2; tileX++) {
			for (int tileY = y; tileY < y + 2; tileY++) {
				Wiring.SkipWire(tileX, tileY);
			}
		}
		for (int tileX = x; tileX < x + 2; tileX++) {
			for (int tileY = y; tileY < y + 2; tileY++) {
				this.OperateOnTile(tileX, tileY);
			}
		}
	}

	protected abstract void OperateOnTile(int tileX, int tileY);

}
