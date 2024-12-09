using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc.Items;

public class Button : ModItem {
	public override void SetStaticDefaults() {
		this.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.Switch);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Button>());
	}
}
