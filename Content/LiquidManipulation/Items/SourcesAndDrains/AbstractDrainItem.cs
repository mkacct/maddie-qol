using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public abstract class AbstractDrainItem : AbstractSourceOrDrainItem {
	protected override int ItemIDToClone => ItemID.InletPump;
}
