using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public abstract class AbstractSourceTile : AbstractSourceOrDrainTile {
	protected abstract int LiquidType {get;}

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.OutletPump, 0));
		TileObjectData.addTile(this.Type);

		this.AddMapEntry(new Color(144, 148, 144), this.Mod.GetLocalization($"{LangMisc}.LiquidSourceName"));
	}

	protected override void OperateOnTile(int tileX, int tileY) {
		Tile tile = Framing.GetTileSafely(tileX, tileY);
		if ((tile.LiquidAmount == 0) || (tile.LiquidType == this.LiquidType)) {
			tile.LiquidType = this.LiquidType;
			tile.Get<LiquidData>().Amount = 255;
			WorldGen.SquareTileFrame(tileX, tileY);
		}
	}
}
