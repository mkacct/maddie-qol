using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Content.LiquidManipulation.Items.SourcesAndDrains;

namespace MaddieQoL.Content.LiquidManipulation;

public class LiquidManipulationRecipes : ModSystem {
	public override void AddRecipes() {
		AddSourceAndDrainRecipes();
	}

	private static void AddSourceAndDrainRecipes() {
		if (!ModuleConf.enableLiquidSourcesAndDrains) {return;}
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingAfter(ItemID.OutletPump);
		{ // Endless Water Source
			Recipe recipe = Recipe.Create(ModContent.ItemType<WaterSource>());
			recipe.AddIngredient(ItemID.OutletPump);
			recipe.AddIngredient(ItemID.BottomlessBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Endless Lava Source
			Recipe recipe = Recipe.Create(ModContent.ItemType<LavaSource>());
			recipe.AddIngredient(ItemID.OutletPump);
			recipe.AddIngredient(ItemID.BottomlessLavaBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Endless Honey Source
			Recipe recipe = Recipe.Create(ModContent.ItemType<HoneySource>());
			recipe.AddIngredient(ItemID.OutletPump);
			recipe.AddIngredient(ItemID.BottomlessHoneyBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Endless Shimmer Source
			Recipe recipe = Recipe.Create(ModContent.ItemType<ShimmerSource>());
			recipe.AddIngredient(ItemID.OutletPump);
			recipe.AddIngredient(ItemID.BottomlessShimmerBucket);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Endless Water Drain
			Recipe recipe = Recipe.Create(ModContent.ItemType<WaterDrain>());
			recipe.AddIngredient(ItemID.InletPump);
			recipe.AddIngredient(ItemID.SuperAbsorbantSponge);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Endless Lava Drain
			Recipe recipe = Recipe.Create(ModContent.ItemType<LavaDrain>());
			recipe.AddIngredient(ItemID.InletPump);
			recipe.AddIngredient(ItemID.LavaAbsorbantSponge);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Endless Honey Drain
			Recipe recipe = Recipe.Create(ModContent.ItemType<HoneyDrain>());
			recipe.AddIngredient(ItemID.InletPump);
			recipe.AddIngredient(ItemID.HoneyAbsorbantSponge);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Universal Endless Drain
			Recipe recipe = Recipe.Create(ModContent.ItemType<UniversalDrain>());
			recipe.AddIngredient(ItemID.InletPump);
			recipe.AddIngredient(ItemID.UltraAbsorbantSponge);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{ // Universal Endless Drain
			Recipe recipe = Recipe.Create(ModContent.ItemType<UniversalDrain>());
			recipe.AddIngredient<WaterDrain>();
			recipe.AddIngredient<LavaDrain>();
			recipe.AddIngredient<HoneyDrain>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.DisableDecraft().RegisterUsing(registerer);
		}
	}
}
