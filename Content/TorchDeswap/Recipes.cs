using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Common;

namespace MaddieQoL.Content.TorchDeswap;

public class TorchDeswapRecipes : ModSystem {
	private bool _wasUsingBiomeTorches = false;

	public override void AddRecipes() {
		if (!ModuleConf.enableTorchDeswap) {return;}
		{ // Torch
			Recipe recipe = Recipe.Create(ItemID.Torch);
			recipe.AddRecipeGroup(RecipeGroups.BiomeTorchRecipeGroup);
			recipe.AddCondition(Conditions.BiomeTorchSwapEnabled);
			recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.Torch);
		}
		{ // Campfire
			Recipe recipe = Recipe.Create(ItemID.Campfire);
			recipe.AddRecipeGroup(RecipeGroups.BiomeCampfireRecipeGroup);
			recipe.AddCondition(Conditions.BiomeTorchSwapEnabled);
			recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.Campfire);
		}
	}

	public override void UpdateUI(GameTime gameTime) {
		if (!ModuleConf.enableTorchDeswap) {return;}
		if (Main.LocalPlayer.UsingBiomeTorches != this._wasUsingBiomeTorches) {
			this._wasUsingBiomeTorches = Main.LocalPlayer.UsingBiomeTorches;
			Recipe.FindRecipes();
		}
	}
}
