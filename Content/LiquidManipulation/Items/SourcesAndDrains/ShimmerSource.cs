using static MaddieQoL.Common.Shorthands;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public class ShimmerSource : AbstractSourceItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableLiquidSourcesAndDrains ? TooltipWhenEnabled : base.Tooltip;

	protected override int TileIDToPlace => ModContent.TileType<Tiles.SourcesAndDrains.ShimmerSource>();

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void SetDefaults() {
		base.SetDefaults();
		this.Item.rare = ItemRarityID.Red;
	}
}
