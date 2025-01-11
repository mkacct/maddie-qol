using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public sealed class WaterDrain : AbstractDrainTile {
	protected override bool CanDrain(int liquidId) {
		return (liquidId == LiquidID.Water) || (liquidId == LiquidID.Shimmer);
	}
}
