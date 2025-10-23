using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Tiles.SourcesAndDrains;

public sealed class LavaDrain : AbstractDrainTile {

	protected override bool CanDrain(int liquidId) => liquidId == LiquidID.Lava;

}
