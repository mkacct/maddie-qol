using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public abstract class AbstractSourceItem : AbstractSourceOrDrainItem {
	protected override int ItemIDToClone => ItemID.OutletPump;
}
