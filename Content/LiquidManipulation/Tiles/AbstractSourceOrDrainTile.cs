using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Tiles;

public abstract class AbstractSourceOrDrainTile : ModTile {
	public override void SetStaticDefaults() {
		Main.tileFrameImportant[this.Type] = true;
		Main.tileLavaDeath[this.Type] = false;
		TileID.Sets.IsAMechanism[this.Type] = true;

		this.DustType = DustID.Iron;
	}

	public override void HitWire(int i, int j) {
		if (!ModuleConfig().enableLiquidSourcesAndDrains) {return;}
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
