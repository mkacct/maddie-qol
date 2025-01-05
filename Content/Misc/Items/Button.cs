using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc.Items;

public class Button : ModItem {
	public override LocalizedText Tooltip => LocalizedText.Empty;

	public override void SetStaticDefaults() {
		this.Item.ResearchUnlockCount = 5;
		ItemID.Sets.SortingPriorityWiring[this.Type] = 91;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.Switch);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Button>());
	}
}
