using static MaddieQoL.Util.RecipeUtil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc.Items;

public class PinkBanner : ModItem {
	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.RedBanner);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PinkBanner>());
	}

    public override void AddRecipes() {
        Recipe recipe = this.CreateRecipe();
		recipe.AddIngredient(ItemID.Silk, 3);
		recipe.AddTile(TileID.Loom);
		RegisterAfterLastRecipe(recipe, ItemID.YellowBanner);
    }
}
