using static MaddieQoL.Common.Shorthands;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public class LavaDrain : AbstractDrainItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConfig().enableLiquidSourcesAndDrains ? TooltipWhenEnabled : base.Tooltip;

	protected override int TileIDToPlace => ModContent.TileType<Tiles.SourcesAndDrains.LavaDrain>();

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}
}
