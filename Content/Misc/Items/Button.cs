using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc.Items;

public class Button : ModItem {
	public override void SetStaticDefaults() {
		this.Item.ResearchUnlockCount = 5;
		ItemID.Sets.SortingPriorityWiring[this.Item.type] = 91;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.Switch);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Button>());
	}
}
