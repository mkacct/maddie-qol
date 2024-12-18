using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc;

public class MiscRecipes : ModSystem {
	public override void AddRecipes() {
		AddDirtBlockRecipe();
		AddPwnhammerRecipe();
	}

	private static void AddDirtBlockRecipe() {
		if (!ModuleConfig().enableDirtFromMud) {return;}
		Recipe recipe = Recipe.Create(ItemID.DirtBlock);
		recipe.AddIngredient(ItemID.MudBlock);
		recipe.AddTile(TileID.Furnaces);
		RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.MudBlock);
	}

	private static void AddPwnhammerRecipe() {
		if (!ModuleConfig().enablePwnhammerRecipe) {return;}
		Recipe recipe = Recipe.Create(ItemID.Pwnhammer);
		recipe.AddIngredient(ItemID.HallowedBar, 18);
		recipe.AddTile(TileID.MythrilAnvil);
		RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.PickaxeAxe);
	}
}
