using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public sealed class ShimmerSource : AbstractSourceItem {

	static LocalizedText TooltipWhenEnabled {get; set;}

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
