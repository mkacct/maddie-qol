using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public abstract class AbstractSourceOrDrainItem : ModItem {
	public override void SetStaticDefaults() {
		ItemID.Sets.SortingPriorityWiring[this.Item.type] = 84;
	}

	public override void SetDefaults() {
		this.Item.rare = ItemRarityID.Lime;
		this.Item.value = Item.sellPrice(0, 10, 0, 0);
	}
}
