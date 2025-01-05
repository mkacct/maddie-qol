using static MaddieQoL.Util.RecipeUtil;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using MaddieQoL.Common;
using MaddieQoL.Util;
using Terraria.ModLoader;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket.ShimmerBucket;

public class UniversalShimmerBucketDummy : AbstractUniversalShimmerBucket {
	public override void AddRecipes() {
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.BottomlessShimmerBucket);
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(ItemID.BottomlessBucket);
			recipe.AddIngredient(ItemID.BottomlessLavaBucket);
			recipe.AddIngredient(ItemID.BottomlessHoneyBucket);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			registerer.Register(recipe);
		}
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroups.UniversalBucketRecipeGroup);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	public override void OnCreated(ItemCreationContext context) {
		this.HandleDummyItemCreation(context);
	}
}
