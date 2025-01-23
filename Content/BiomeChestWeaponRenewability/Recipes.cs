using static MaddieQoL.Common.Shorthands;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Content.BiomeChestWeaponRenewability.Items;

namespace MaddieQoL.Content.BiomeChestWeaponRenewability;

public sealed class BiomeChestWeaponRenewabilityRecipes : ModSystem {
	public override void AddRecipes() {
		if (!ModuleConf.enableBiomeLockBoxes) {return;}
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingAfter(ItemID.CelestialSigil);
		AddBiomeLockBoxRecipe(registerer, ModContent.ItemType<LockBoxJungle>(), (recipe) => {
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
		});
		AddBiomeLockBoxRecipe(registerer, ModContent.ItemType<LockBoxCorruption>(), (recipe) => {
			recipe.AddIngredient(ItemID.CursedFlame, 10);
		});
		AddBiomeLockBoxRecipe(registerer, ModContent.ItemType<LockBoxCrimson>(), (recipe) => {
			recipe.AddIngredient(ItemID.Ichor, 10);
		});
		AddBiomeLockBoxRecipe(registerer, ModContent.ItemType<LockBoxHallowed>(), (recipe) => {
			recipe.AddIngredient(ItemID.CrystalShard, 10);
		});
		AddBiomeLockBoxRecipe(registerer, ModContent.ItemType<LockBoxIce>(), (recipe) => {
			recipe.AddIngredient(ItemID.FrostCore);
		});
		AddBiomeLockBoxRecipe(registerer, ModContent.ItemType<LockBoxDesert>(), (recipe) => {
			recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
		});
	}

	private static void AddBiomeLockBoxRecipe(RecipeOrderedRegisterer registerer, int box, Action<Recipe> addSpecialIngredients) {
		Recipe recipe = Recipe.Create(box);
		recipe.AddIngredient(ItemID.LockBox);
		addSpecialIngredients(recipe);
		recipe.AddIngredient(ItemID.Ectoplasm, 5);
		recipe.AddTile(TileID.CrystalBall).AddCondition(Condition.InGraveyard);
		recipe.RegisterUsing(registerer);
	}
}
