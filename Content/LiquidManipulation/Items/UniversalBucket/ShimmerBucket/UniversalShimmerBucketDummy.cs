using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using MaddieQoL.Util;
using MaddieQoL.Common;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket.ShimmerBucket;

public sealed class UniversalShimmerBucketDummy : AbstractUniversalShimmerBucket {

	public override void AddRecipes() {
		RecipeOrderedRegistrar registrar = RecipeOrderedRegistrar.StartingAfter(ItemID.BottomlessShimmerBucket);
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(ItemID.BottomlessBucket);
			recipe.AddIngredient(ItemID.BottomlessLavaBucket);
			recipe.AddIngredient(ItemID.BottomlessHoneyBucket);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registrar);
		}
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroups.UniversalBucketRecipeGroup);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.DisableDecraft().RegisterUsing(registrar);
		}
	}

	public override void OnCreated(ItemCreationContext context) {
		this.HandleDummyItemCreation(context);
	}

}
