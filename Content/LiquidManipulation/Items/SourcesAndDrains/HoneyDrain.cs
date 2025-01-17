using static MaddieQoL.Common.Shorthands;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public sealed class HoneyDrain : AbstractDrainItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableLiquidSourcesAndDrains ? TooltipWhenEnabled : base.Tooltip;

	protected override int TileIDToPlace => ModContent.TileType<Tiles.SourcesAndDrains.HoneyDrain>();

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}
}
