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

	public override void PostAddRecipes() {
		AddCopperArmorShimmerExceptions();
	}

	private static void AddDirtBlockRecipe() {
		if (!ModuleConf.enableDirtFromMud) {return;}
		Recipe recipe = Recipe.Create(ItemID.DirtBlock);
		recipe.AddIngredient(ItemID.MudBlock);
		recipe.AddTile(TileID.Furnaces);
		RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.MudBlock);
	}

	private static void AddPwnhammerRecipe() {
		if (!ModuleConf.enablePwnhammerRecipe) {return;}
		Recipe recipe = Recipe.Create(ItemID.Pwnhammer);
		recipe.AddIngredient(ItemID.HallowedBar, 18);
		recipe.AddTile(TileID.MythrilAnvil);
		RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.PickaxeAxe);
	}

	private static void AddCopperArmorShimmerExceptions() {
		if (!ModuleConf.enableMerchantShopPerDialogue) {return;}
		int[] copperArmors = [ItemID.CopperHelmet, ItemID.CopperChainmail, ItemID.CopperGreaves];
		foreach (Recipe recipe in Main.recipe) {
			if (!recipe.HasIngredient(ItemID.CopperBar)) {continue;}
			if (RecipeHasCustomShimmerResults(recipe)) {continue;}
			foreach (int copperArmor in copperArmors) {
				if (recipe.HasResult(copperArmor)) {
					recipe.AddCustomShimmerResult(ItemID.CopperOre);
				}
			}
		}
	}
}
