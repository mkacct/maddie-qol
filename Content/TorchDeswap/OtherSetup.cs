using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;

namespace MaddieQoL.Content.TorchDeswap;

public sealed class TorchDeswapOtherSetup : ModSystem {

	public override void PostSetupRecipes() {
		SuppressCampfireMaterialTooltips();
	}

	static void SuppressCampfireMaterialTooltips() {
		int campfireRecipeGroupId = RecipeGroup.recipeGroupIDs[RecipeGroups.BiomeCampfireRecipeGroup];
		ISet<int> campfireItemIds = RecipeGroup.recipeGroups[campfireRecipeGroupId].ValidItems;
		foreach (int campfire in campfireItemIds) {
			ItemID.Sets.IsAMaterial[campfire] = false;
		}
	}

}
