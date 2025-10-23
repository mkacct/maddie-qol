using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public abstract class AbstractSourceTile : AbstractSourceOrDrainTile {

	protected abstract int LiquidType {get;}

	protected override TileObjectData CopyFromTileObjectData => TileObjectData.GetTileData(TileID.OutletPump, 0);
	protected override LocalizedText MapEntryName => this.Mod.GetLocalization($"{LangMisc}.LiquidSourceName");

	protected override void OperateOnTile(int tileX, int tileY) {
		Tile tile = Framing.GetTileSafely(tileX, tileY);
		if ((tile.LiquidAmount == 0) || (tile.LiquidType == this.LiquidType)) {
			tile.LiquidType = this.LiquidType;
			tile.LiquidAmount = byte.MaxValue;
			WorldGen.SquareTileFrame(tileX, tileY);
		}
	}

}
