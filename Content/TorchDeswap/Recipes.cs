using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Common;

namespace MaddieQoL.Content.TorchDeswap;

public sealed class TorchDeswapRecipes : ModSystem {
	private bool _wasUsingBiomeTorches = false;

	public override void AddRecipes() {
		{ // Torch
			Recipe recipe = Recipe.Create(ItemID.Torch);
			recipe.AddRecipeGroup(RecipeGroups.BiomeTorchRecipeGroup);
			recipe.AddCondition(Conditions.TorchDeswapAllowed);
			recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.Torch);
		}
		{ // Campfire
			Recipe recipe = Recipe.Create(ItemID.Campfire);
			recipe.AddRecipeGroup(RecipeGroups.BiomeCampfireRecipeGroup);
			recipe.AddCondition(Conditions.TorchDeswapAllowed);
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
