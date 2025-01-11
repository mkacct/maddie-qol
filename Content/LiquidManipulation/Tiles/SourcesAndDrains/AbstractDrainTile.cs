using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public abstract class AbstractDrainTile : AbstractSourceOrDrainTile {
	protected override TileObjectData CopyFromTileObjectData => TileObjectData.GetTileData(TileID.InletPump, 0);
	protected override LocalizedText MapEntryName => this.Mod.GetLocalization($"{LangMisc}.LiquidDrainName");

	protected override void OperateOnTile(int tileX, int tileY) {
		Tile tile = Framing.GetTileSafely(tileX, tileY);
		if ((tile.LiquidAmount > 0) && this.CanDrain(tile.LiquidType)) {
			tile.LiquidAmount = 0;
			WorldGen.SquareTileFrame(tileX, tileY);
		}
	}

	protected abstract bool CanDrain(int liquidId);
}
