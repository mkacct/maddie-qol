using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc.Items;

public sealed class ClockGenerator : ModItem {
	public override void SetStaticDefaults() {
		ItemID.Sets.SortingPriorityWiring[this.Type] = 96;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.Timer1Second);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ClockGenerator>());
		this.Item.rare = ItemRarityID.Red;
		this.Item.value = Item.buyPrice(0, 30, 0, 0);
	}
}
