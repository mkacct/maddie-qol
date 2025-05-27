using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Misc;

public sealed class MiscRecipes : ModSystem {
	public override void AddRecipes() {
		AddDirtBlockRecipe();
		AddPwnhammerRecipe();
		AddConveyerBeltRecipes();
	}

	public override void PostAddRecipes() {
		AddCopperArmorShimmerExceptions();
	}

	static void AddDirtBlockRecipe() {
		if (!ModuleConf.enableDirtFromMud) {return;}
		Recipe recipe = Recipe.Create(ItemID.DirtBlock);
		recipe.AddIngredient(ItemID.MudBlock);
		recipe.AddTile(TileID.Furnaces);
		recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.MudBlock);
	}

	static void AddPwnhammerRecipe() {
		if (!ModuleConf.enablePwnhammerRecipe) {return;}
		Recipe recipe = Recipe.Create(ItemID.Pwnhammer);
		recipe.AddIngredient(ItemID.HallowedBar, 18);
		recipe.AddTile(TileID.MythrilAnvil);
		recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.PickaxeAxe);
	}

	static void AddConveyerBeltRecipes() {
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingBefore(ItemID.LogicSensor_Water);
		{
			Recipe recipe = Recipe.Create(ItemID.ConveyorBeltRight);
			recipe.AddIngredient(ItemID.ConveyorBeltLeft);
			recipe.DisableDecraft().RegisterUsing(registerer);
		}
		{
			Recipe recipe = Recipe.Create(ItemID.ConveyorBeltLeft);
			recipe.AddIngredient(ItemID.ConveyorBeltRight);
			recipe.DisableDecraft().RegisterUsing(registerer);
		}
	}

	static void AddCopperArmorShimmerExceptions() {
		if (!ModuleConf.enableMerchantShopPerDialogue) {return;}
		int[] copperArmors = [ItemID.CopperHelmet, ItemID.CopperChainmail, ItemID.CopperGreaves];
		foreach (Recipe recipe in Main.recipe) {
			if (!recipe.HasIngredient(ItemID.CopperBar)) {continue;}
			if (recipe.HasCustomShimmerResults()) {continue;}
			foreach (int copperArmor in copperArmors) {
				if (recipe.HasResult(copperArmor)) {
					recipe.AddCustomShimmerResult(ItemID.CopperOre);
				}
			}
		}
	}
}
