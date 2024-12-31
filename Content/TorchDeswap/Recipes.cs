using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;

namespace MaddieQoL.Content.TorchDeswap;

public class TorchDeswapRecipes : ModSystem {
	private bool _wasUsingBiomeTorches = false;

	public override void AddRecipes() {
		if (!ModuleConfig().enableTorchDeswap) {return;}
		{
			Recipe recipe = Recipe.Create(ItemID.Torch);
			recipe.AddRecipeGroup(RecipeGroups.BiomeTorchRecipeGroup);
			recipe.AddCondition(Conditions.BiomeTorchSwapEnabled);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.Torch);
		}
		{
			Recipe recipe = Recipe.Create(ItemID.Campfire);
			recipe.AddRecipeGroup(RecipeGroups.BiomeCampfireRecipeGroup);
			recipe.AddCondition(Conditions.BiomeTorchSwapEnabled);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.Campfire);
		}
	}

	public override void UpdateUI(GameTime gameTime) {
		if (!ModuleConfig().enableTorchDeswap) {return;}
		if (Main.LocalPlayer.UsingBiomeTorches != this._wasUsingBiomeTorches) {
			this._wasUsingBiomeTorches = Main.LocalPlayer.UsingBiomeTorches;
			Recipe.FindRecipes();
		}
	}
}
