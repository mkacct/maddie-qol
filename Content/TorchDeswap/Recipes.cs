using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;

namespace MaddieQoL.Content.TorchDeswap;

public class TorchDeswapRecipes : ModSystem {
	public override void AddRecipes() {
		if (!ModuleConfig().enableTorchDeswap) {return;}
		{
			Recipe recipe = Recipe.Create(ItemID.Torch);
			recipe.AddRecipeGroup(RecipeGroups.BiomeTorchRecipeGroup);
			recipe.AddCondition(Conditions.PlayerHasTorchGodsFavor);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.Torch);
		}
		{
			Recipe recipe = Recipe.Create(ItemID.Campfire);
			recipe.AddRecipeGroup(RecipeGroups.BiomeCampfireRecipeGroup);
			recipe.AddCondition(Conditions.PlayerHasTorchGodsFavor);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.Campfire);
		}
	}
}
