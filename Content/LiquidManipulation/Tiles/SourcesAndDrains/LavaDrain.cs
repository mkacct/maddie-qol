using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public class LavaDrain : AbstractDrainTile {
	protected override bool CanDrain(int liquidId) {
		return liquidId == LiquidID.Lava;
	}
}
