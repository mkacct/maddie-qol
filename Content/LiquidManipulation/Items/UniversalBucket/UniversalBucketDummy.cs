using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using MaddieQoL.Util;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket;

public sealed class UniversalBucketDummy : AbstractUniversalBucket {

	public override void AddRecipes() {
		Recipe recipe = this.CreateRecipe();
		recipe.AddIngredient(ItemID.BottomlessBucket);
		recipe.AddIngredient(ItemID.BottomlessLavaBucket);
		recipe.AddIngredient(ItemID.BottomlessHoneyBucket);
		recipe.AddTile(TileID.TinkerersWorkbench);
		recipe.RegisterBeforeFirstRecipeOf(ItemID.UltraAbsorbantSponge);
	}

	public override void OnCreated(ItemCreationContext context) {
		this.HandleDummyItemCreation(context);
	}

}
