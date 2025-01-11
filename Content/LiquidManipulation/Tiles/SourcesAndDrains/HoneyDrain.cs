using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public sealed class HoneyDrain : AbstractDrainTile {
	protected override bool CanDrain(int liquidId) {
		return liquidId == LiquidID.Honey;
	}
}
