using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

public abstract class AbstractSourceOrDrainItem : ModItem {

	protected abstract int TileIDToPlace {get;}

	protected abstract int ItemIDToClone {get;}

	public override void SetStaticDefaults() {
		ItemID.Sets.SortingPriorityWiring[this.Type] = 84;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(this.ItemIDToClone);
		this.Item.DefaultToPlaceableTile(this.TileIDToPlace);
		this.Item.rare = ItemRarityID.Lime;
		this.Item.value = Item.sellPrice(0, 10, 0, 0);
	}

}
