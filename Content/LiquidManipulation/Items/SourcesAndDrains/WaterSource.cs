using static MaddieQoL.Common.Shorthands;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public class WaterSource : AbstractSourceOrDrainItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConfig().enableLiquidSourcesAndDrains ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.OutletPump);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.SourcesAndDrains.WaterSource>());
		base.SetDefaults();
	}
}
