using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors;

public class MirrorRecipes : ModSystem {
	public override void AddRecipes() {
		if (!ModuleConfig().enableReturnTools) {return;}
		{
			Recipe recipe = Recipe.Create(ItemID.ShellphoneDummy);
			recipe.AddIngredient<Items.CellPhonePlus>();
			recipe.AddIngredient(ItemID.MagicConch);
			recipe.AddIngredient(ItemID.DemonConch);
			recipe.AddTile(TileID.TinkerersWorkbench);
			RegisterAfterLastRecipe(recipe, ItemID.ShellphoneDummy);
		}
	}

	public override void PostAddRecipes() {
		if (!ModuleConfig().enableReturnTools) {return;}
		bool hasDisabledVanillaShellphoneRecipe = false;
		foreach (Recipe recipe in Main.recipe) {
			if (
				!hasDisabledVanillaShellphoneRecipe
				&& recipe.HasResult(ItemID.ShellphoneDummy)
				&& recipe.HasIngredient(ItemID.CellPhone)
				&& recipe.HasIngredient(ItemID.MagicConch)
				&& recipe.HasIngredient(ItemID.DemonConch)
				&& recipe.HasTile(TileID.TinkerersWorkbench)
			) {
				recipe.DisableRecipe();
				hasDisabledVanillaShellphoneRecipe = true;
			}
		}
	}
}
