using static MaddieQoL.Util.RecipeUtil;
using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityRecipes : ModSystem {
	private const string DungeonBrickRecipeGroup = nameof(ItemID.BlueBrick);

	private static LocalizedText DungeonBrickRecipeGroupName {get; set;}

	public override void SetStaticDefaults() {
		DungeonBrickRecipeGroupName = Mod.GetLocalization($"Recipes.{nameof(DungeonBrickRecipeGroupName)}");
		AddGoldChestShimmer();
	}

	public override void AddRecipeGroups() {
		{
			RecipeGroup group = new(
				() => {return DungeonBrickRecipeGroupName.Value;},
				ItemID.BlueBrick, ItemID.GreenBrick, ItemID.PinkBrick
			);
			RecipeGroup.RegisterGroup(DungeonBrickRecipeGroup, group);
		}
	}

	public override void AddRecipes() {
		AddChestRecipes();
		AddTrapRecipes();
		AddEncumberingStoneRecipe();
		AddObsidianFurnitureRecipes();
		AddHellforgeRecipe();
		AddDungeonFurnitureRecipes();
		AddLihzahrdFurnitureRecipes();
	}

	private static void AddChestRecipes() {
		if (!ModContent.GetInstance<ModuleConfig>().enableChestRecipes) {return;}
		{ // Gold Chest
			Recipe recipe = Recipe.Create(ItemID.GoldChest);
			recipe.AddIngredient(ItemID.GoldBar, 8);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.AddTile(TileID.Anvils);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.Candelabra);
		}
		{ // Ivy Chest
			Recipe recipe = Recipe.Create(ItemID.IvyChest);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 6);
			recipe.AddIngredient(ItemID.Vine, 2);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.AddTile(TileID.WorkBenches).AddCondition(Condition.InGraveyard);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.JunglePants);
		}
		{ // Water Chest
			Recipe recipe = Recipe.Create(ItemID.WaterChest);
			recipe.AddIngredient(ItemID.CoralstoneBlock, 8);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.AddTile(TileID.WorkBenches).AddCondition(Condition.InGraveyard);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.CoralstoneBlock);
		}
		{ // Web Covered Chest
			Recipe recipe = Recipe.Create(ItemID.WebCoveredChest);
			recipe.AddIngredient(ItemID.Chest, 1);
			recipe.AddIngredient(ItemID.Cobweb, 8);
			recipe.AddTile(TileID.WorkBenches).AddCondition(Condition.InGraveyard);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.ToiletSpider);
		}
		{ // Shadow Chest
			Recipe recipe = Recipe.Create(ItemID.ShadowChest);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.AddTile(TileID.WorkBenches);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.ToiletObsidian);
		}
	}

	private static void AddGoldChestShimmer() {
		if (!ModContent.GetInstance<ModuleConfig>().enableTrapRecipes) {return;}
		ItemID.Sets.ShimmerTransformToItem[ItemID.GoldChest] = ItemID.DeadMansChest;
	}

	private static void AddTrapRecipes() {
		if (!ModContent.GetInstance<ModuleConfig>().enableTrapRecipes) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.PressureTrack);
		IList<KeyValuePair<short, int>> poisonItemsAndQtys = [
			new(ItemID.VilePowder, 30),
			new(ItemID.ViciousPowder, 30),
			new(ItemID.Stinger, 15),
		];
		foreach (KeyValuePair<short, int> entry in poisonItemsAndQtys) { // Dart Trap
			Recipe recipe = Recipe.Create(ItemID.DartTrap);
			recipe.AddIngredient(ItemID.StoneBlock, 8);
			recipe.AddIngredient(entry.Key, entry.Value);
			recipe.AddIngredient(ItemID.Wire);
			recipe.AddTile(TileID.HeavyWorkBench).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Geyser
			Recipe recipe = Recipe.Create(ItemID.GeyserTrap);
			recipe.AddIngredient(ItemID.StoneBlock, 8);
			recipe.AddIngredient(ItemID.LavaBucket);
			recipe.AddIngredient(ItemID.Gel, 30);
			recipe.AddTile(TileID.HeavyWorkBench).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		AddDungeonTrapRecipes(registerer);
		AddLihzahrdTrapRecipes(registerer);
	}

	private static void AddDungeonTrapRecipes(RecipeOrderedRegisterer registerer) {
		if (!ModContent.GetInstance<ModuleConfig>().enableDungeonItemRenewability) {return;}
		{ // Spike
			Recipe recipe = Recipe.Create(ItemID.Spike);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	private static void AddLihzahrdTrapRecipes(RecipeOrderedRegisterer registerer) {
		if (!ModContent.GetInstance<ModuleConfig>().enableLihzahrdItemRenewability) {return;}
		{ // Wooden Spike
			Recipe recipe = Recipe.Create(ItemID.WoodenSpike);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
			recipe.AddTile(TileID.LihzahrdFurnace).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Super Dart Trap
			Recipe recipe = Recipe.Create(ItemID.SuperDartTrap);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 8);
			recipe.AddIngredient(ItemID.Stinger, 15);
			recipe.AddIngredient(ItemID.Wire);
			recipe.AddTile(TileID.LihzahrdFurnace).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Spiky Ball Trap
			Recipe recipe = Recipe.Create(ItemID.SpikyBallTrap);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 8);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 30);
			recipe.AddIngredient(ItemID.Wire);
			recipe.AddTile(TileID.LihzahrdFurnace).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Spear Trap
			Recipe recipe = Recipe.Create(ItemID.SpearTrap);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 8);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
			recipe.AddIngredient(ItemID.StoneBlock, 4);
			recipe.AddIngredient(ItemID.Wire);
			recipe.AddTile(TileID.LihzahrdFurnace).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Flame Trap
			Recipe recipe = Recipe.Create(ItemID.FlameTrap);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 8);
			recipe.AddIngredient(ItemID.Gel, 30);
			recipe.AddIngredient(ItemID.Wire);
			recipe.AddTile(TileID.LihzahrdFurnace).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	private static void AddEncumberingStoneRecipe() {
		if (!ModContent.GetInstance<ModuleConfig>().enableEncumberingStoneRecipe) {return;}
		Recipe recipe = Recipe.Create(ItemID.EncumberingStone);
		recipe.AddIngredient(ItemID.StoneBlock, 50);
		recipe.AddIngredient(ItemID.IronBar, 2);
		recipe.AddTile(TileID.HeavyWorkBench).AddCondition(Condition.InGraveyard);
		RegisterBeforeFirstRecipe(recipe.DisableDecraft(), ItemID.PanicNecklace);
	}

	private static void AddObsidianFurnitureRecipes() {
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingBefore(ItemID.ObsidianChest);
		{ // Obsidian Bathtub
			Recipe recipe = Recipe.Create(ItemID.ObsidianBathtub);
			recipe.AddIngredient(ItemID.Obsidian, 12);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Obsidian Bed
			Recipe recipe = Recipe.Create(ItemID.ObsidianBed);
			recipe.AddIngredient(ItemID.Obsidian, 13);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Obsidian Bookcase
			Recipe recipe = Recipe.Create(ItemID.ObsidianBookcase);
			recipe.AddIngredient(ItemID.Obsidian, 18);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddIngredient(ItemID.Book, 10);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Obsidian Dresser
			Recipe recipe = Recipe.Create(ItemID.ObsidianDresser);
			recipe.AddIngredient(ItemID.Obsidian, 14);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Obsidian Candelabra
			Recipe recipe = Recipe.Create(ItemID.ObsidianCandelabra);
			recipe.AddIngredient(ItemID.Obsidian, 5);
			recipe.AddIngredient(ItemID.DemonTorch, 3);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Obsidian Candle
			Recipe recipe = Recipe.Create(ItemID.ObsidianCandle);
			recipe.AddIngredient(ItemID.Obsidian, 4);
			recipe.AddIngredient(ItemID.DemonTorch);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Obsidian Chair
			Recipe recipe = Recipe.Create(ItemID.ObsidianChair);
			recipe.AddIngredient(ItemID.Obsidian, 2);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Obsidian Chanedlier
			Recipe recipe = Recipe.Create(ItemID.ObsidianChandelier);
			recipe.AddIngredient(ItemID.Obsidian, 4);
			recipe.AddIngredient(ItemID.DemonTorch, 4);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils);
			registerer.Register(recipe);
		}
		registerer.SortAfterLastRecipeOf(ItemID.ObsidianChest);
		{ // Obsidian Clock
			Recipe recipe = Recipe.Create(ItemID.ObsidianClock);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
			recipe.AddIngredient(ItemID.Glass, 6);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Obsidian Door
			Recipe recipe = Recipe.Create(ItemID.ObsidianDoor);
			recipe.AddIngredient(ItemID.Obsidian, 4);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Obsidian Lamp
			Recipe recipe = Recipe.Create(ItemID.ObsidianLamp);
			recipe.AddIngredient(ItemID.DemonTorch);
			recipe.AddIngredient(ItemID.Obsidian, 4);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Obsidian Lantern
			Recipe recipe = Recipe.Create(ItemID.ObsidianLantern);
			recipe.AddIngredient(ItemID.Obsidian, 6);
			recipe.AddIngredient(ItemID.DemonTorch);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Obsidian Piano
			Recipe recipe = Recipe.Create(ItemID.ObsidianPiano);
			recipe.AddIngredient(ItemID.Bone, 4);
			recipe.AddIngredient(ItemID.Obsidian, 13);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		registerer.SortAfterLastRecipeOf(ItemID.ObsidianSink);
		{ // Obsidian Sofa
			Recipe recipe = Recipe.Create(ItemID.ObsidianSofa);
			recipe.AddIngredient(ItemID.Obsidian, 3);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Obsidian Table
			Recipe recipe = Recipe.Create(ItemID.ObsidianTable);
			recipe.AddIngredient(ItemID.Obsidian, 6);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		registerer.SortAfterLastRecipeOf(ItemID.ToiletObsidian);
		{ // Obsidian Work Bench
			Recipe recipe = Recipe.Create(ItemID.ObsidianWorkBench);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			registerer.Register(recipe);
		}
		if (ModContent.GetInstance<ModuleConfig>().enableVaseRecipes) { // Obsidian Vase
			Recipe recipe = Recipe.Create(ItemID.ObsidianVase);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
	}

	private static void AddHellforgeRecipe() {
		if (!ModContent.GetInstance<ModuleConfig>().enableHellforgeRecipe) {return;}
		Recipe recipe = Recipe.Create(ItemID.Hellforge);
		recipe.AddIngredient(ItemID.Furnace);
		recipe.AddIngredient(ItemID.HellstoneBar, 10);
		recipe.AddTile(TileID.Anvils);
		RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.FireproofBugNet);
	}

	private static void AddDungeonFurnitureRecipes() {
		AddDungeonFurnitureSetRecipes(ItemID.BlueBrick, ItemID.BlueDungeonChest, [
			ItemID.BlueDungeonBathtub,
			ItemID.BlueDungeonBed,
			ItemID.BlueDungeonBookcase,
			ItemID.BlueDungeonCandelabra,
			ItemID.BlueDungeonCandle,
			ItemID.BlueDungeonChair,
			ItemID.BlueDungeonChandelier,
			ItemID.BlueDungeonChest,
			ItemID.DungeonClockBlue,
			ItemID.BlueDungeonDoor,
			ItemID.BlueDungeonDresser,
			ItemID.BlueDungeonLamp,
			ItemID.BlueDungeonPiano,
			ItemID.BlueDungeonSink,
			ItemID.BlueDungeonSofa,
			ItemID.BlueDungeonTable,
			ItemID.ToiletDungeonBlue,
			ItemID.BlueDungeonVase,
			ItemID.BlueDungeonWorkBench
		]);
		AddDungeonFurnitureSetRecipes(ItemID.GreenBrick, ItemID.GreenDungeonChest, [
			ItemID.GreenDungeonBathtub,
			ItemID.GreenDungeonBed,
			ItemID.GreenDungeonBookcase,
			ItemID.GreenDungeonCandelabra,
			ItemID.GreenDungeonCandle,
			ItemID.GreenDungeonChair,
			ItemID.GreenDungeonChandelier,
			ItemID.GreenDungeonChest,
			ItemID.DungeonClockGreen,
			ItemID.GreenDungeonDoor,
			ItemID.GreenDungeonDresser,
			ItemID.GreenDungeonLamp,
			ItemID.GreenDungeonPiano,
			ItemID.GreenDungeonSink,
			ItemID.GreenDungeonSofa,
			ItemID.GreenDungeonTable,
			ItemID.ToiletDungeonGreen,
			ItemID.GreenDungeonVase,
			ItemID.GreenDungeonWorkBench
		]);
		AddDungeonFurnitureSetRecipes(ItemID.PinkBrick, ItemID.PinkDungeonChest, [
			ItemID.PinkDungeonBathtub,
			ItemID.PinkDungeonBed,
			ItemID.PinkDungeonBookcase,
			ItemID.PinkDungeonCandelabra,
			ItemID.PinkDungeonCandle,
			ItemID.PinkDungeonChair,
			ItemID.PinkDungeonChandelier,
			ItemID.PinkDungeonChest,
			ItemID.DungeonClockPink,
			ItemID.PinkDungeonDoor,
			ItemID.PinkDungeonDresser,
			ItemID.PinkDungeonLamp,
			ItemID.PinkDungeonPiano,
			ItemID.PinkDungeonSink,
			ItemID.PinkDungeonSofa,
			ItemID.PinkDungeonTable,
			ItemID.ToiletDungeonPink,
			ItemID.PinkDungeonVase,
			ItemID.PinkDungeonWorkBench
		]);
		AddGothicFurnitureRecipes();
	}

	private static void AddDungeonFurnitureSetRecipes(short brick, short sortBeforeItemId, short[] furnitureItems) {
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingBefore(sortBeforeItemId);
		int i = 0;
		{ // Bathtub
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 14);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Bed
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 15);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Bookcase
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 20);
			recipe.AddIngredient(ItemID.Book, 10);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Candelabra
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 5);
			recipe.AddIngredient(ItemID.BoneTorch, 3);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Candle
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 4);
			recipe.AddIngredient(ItemID.BoneTorch);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Chair
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 4);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Chandelier
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 4);
			recipe.AddIngredient(ItemID.BoneTorch, 4);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils);
			registerer.Register(recipe);
		}
		// Skip: Chest
		registerer.SortAfterLastRecipeOf(furnitureItems[i++]);
		{ // Clock
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
			recipe.AddIngredient(ItemID.Glass, 6);
			recipe.AddIngredient(brick, 10);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Door
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 6);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Dresser
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 16);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Lamp
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(ItemID.BoneTorch);
			recipe.AddIngredient(brick, 3);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		{ // Piano
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(ItemID.Bone, 4);
			recipe.AddIngredient(brick, 15);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		// Skip: Sink
		registerer.SortAfterLastRecipeOf(furnitureItems[i++]);
		{ // Sofa
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 5);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddTile(TileID.Sawmill);
			registerer.Register(recipe);
		}
		{ // Table
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 8);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
		// Skip: Toilet
		registerer.SortAfterLastRecipeOf(furnitureItems[i++]);
		if (ModContent.GetInstance<ModuleConfig>().enableVaseRecipes) { // Vase
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 12);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		} else {i++;}
		{ // Work Bench
			Recipe recipe = Recipe.Create(furnitureItems[i++]);
			recipe.AddIngredient(brick, 10);
			registerer.Register(recipe);
		}
	}

	private static void AddGothicFurnitureRecipes() {
		if (!ModContent.GetInstance<ModuleConfig>().enableDungeonItemRenewability) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.GreenDungeonWorkBench);
		{ // Gothic Bookcase
			Recipe recipe = Recipe.Create(ItemID.GothicBookcase);
			recipe.AddRecipeGroup(DungeonBrickRecipeGroup, 20);
			recipe.AddIngredient(ItemID.Book, 10);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Gothic Chair
			Recipe recipe = Recipe.Create(ItemID.GothicChair);
			recipe.AddRecipeGroup(DungeonBrickRecipeGroup, 4);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Gothic Table
			Recipe recipe = Recipe.Create(ItemID.GothicTable);
			recipe.AddRecipeGroup(DungeonBrickRecipeGroup, 8);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Gothic Work Bench
			Recipe recipe = Recipe.Create(ItemID.GothicWorkBench);
			recipe.AddRecipeGroup(DungeonBrickRecipeGroup, 10);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	private static void AddLihzahrdFurnitureRecipes() {
		{ // Lihzahrd Work Bench
			Recipe recipe = Recipe.Create(ItemID.LihzahrdWorkBench);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
			recipe.AddTile(TileID.LihzahrdFurnace);
			RegisterAfterLastRecipe(recipe, ItemID.ToiletLihzhard);
		}
	}
}
