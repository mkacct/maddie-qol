using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;

namespace MaddieQoL.Content.LiquidManipulation.Tiles;

public abstract class AbstractDrainTile : AbstractSourceOrDrainTile {
	public override void SetStaticDefaults() {
		base.SetStaticDefaults();

		TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.InletPump, 0));
		TileObjectData.addTile(this.Type);

		this.AddMapEntry(new Color(144, 148, 144), this.Mod.GetLocalization($"{LangMisc}.LiquidDrainName"));
	}

	protected override void OperateOnTile(int tileX, int tileY) {
		Tile tile = Framing.GetTileSafely(tileX, tileY);
		if ((tile.LiquidAmount > 0) && this.CanDrain(tile.LiquidType)) {
			tile.Get<LiquidData>().Amount = 0;
			WorldGen.SquareTileFrame(tileX, tileY);
		}
	}

	protected abstract bool CanDrain(int liquidId);
}
