using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using MaddieQoL.Common;
using MaddieQoL.Util;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket.ShimmerBucket;

public sealed class UniversalShimmerBucketDummy : AbstractUniversalShimmerBucket {
	public override void AddRecipes() {
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingAfter(ItemID.BottomlessShimmerBucket);
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(ItemID.BottomlessBucket);
			recipe.AddIngredient(ItemID.BottomlessLavaBucket);
			recipe.AddIngredient(ItemID.BottomlessHoneyBucket);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroups.UniversalBucketRecipeGroup);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.DisableDecraft().RegisterUsing(registerer);
		}
	}

	public override void OnCreated(ItemCreationContext context) {
		this.HandleDummyItemCreation(context);
	}
}
