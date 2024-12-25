namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public class UniversalDrain : AbstractDrainTile {
	protected override bool CanDrain(int liquidId) {
		return true;
	}
}
