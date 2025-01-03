using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Common;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityRecipes : ModSystem {
	public override void SetStaticDefaults() {
		AddGoldChestShimmer();
		AddReverseShimmers();
		AddExtraShimmers();
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
		if (!ModuleConfig().enableChestRecipes) {return;}
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
		if (!ModuleConfig().enableTrapRecipes) {return;}
		ItemID.Sets.ShimmerTransformToItem[ItemID.GoldChest] = ItemID.DeadMansChest;
	}

	private static void AddTrapRecipes() {
		if (!ModuleConfig().enableTrapRecipes) {return;}
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
			recipe.AddIngredient(ItemID.Gel, 30);
			recipe.AddTile(TileID.HeavyWorkBench).AddCondition(Condition.NearLava, Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		AddDungeonTrapRecipes(registerer);
		AddLihzahrdTrapRecipes(registerer);
	}

	private static void AddDungeonTrapRecipes(RecipeOrderedRegisterer registerer) {
		if (!ModuleConfig().enableDungeonItemRenewability) {return;}
		{ // Spike
			Recipe recipe = Recipe.Create(ItemID.Spike);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	private static void AddLihzahrdTrapRecipes(RecipeOrderedRegisterer registerer) {
		if (!ModuleConfig().enableLihzahrdItemRenewability) {return;}
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
		if (!ModuleConfig().enableEncumberingStoneRecipe) {return;}
		Recipe recipe = Recipe.Create(ItemID.EncumberingStone);
		recipe.AddIngredient(ItemID.StoneBlock, 100);
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
		if (ModuleConfig().enableVaseRecipes) { // Obsidian Vase
			Recipe recipe = Recipe.Create(ItemID.ObsidianVase);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddIngredient(ItemID.Hellstone, 2);
			recipe.AddTile(TileID.WorkBenches);
			registerer.Register(recipe);
		}
	}

	private static void AddHellforgeRecipe() {
		if (!ModuleConfig().enableHellforgeRecipe) {return;}
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
		AddDungeonMiscFurnitureRecipes();
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
		if (ModuleConfig().enableVaseRecipes) { // Vase
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

	private static void AddDungeonMiscFurnitureRecipes() {
		if (!ModuleConfig().enableDungeonItemRenewability) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingBefore(ItemID.PinkDungeonBathtub);
		{ // Dungeon Door
			Recipe recipe = Recipe.Create(ItemID.DungeonDoor);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 6);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Chain Lantern
			Recipe recipe = Recipe.Create(ItemID.ChainLantern);
			recipe.AddIngredient(ItemID.Chain, 6);
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Brass Lantern
			Recipe recipe = Recipe.Create(ItemID.BrassLantern);
			recipe.AddIngredient(ItemID.CopperBrick, 6);
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Caged Lantern
			Recipe recipe = Recipe.Create(ItemID.CagedLantern);
			recipe.AddIngredient(ItemID.TinBrick, 6);
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Carriage Lantern
			Recipe recipe = Recipe.Create(ItemID.CarriageLantern);
			recipe.AddIngredient(ItemID.LeadBrick, 6);
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Alchemy Lantern
			Recipe recipe = Recipe.Create(ItemID.AlchemyLantern);
			recipe.AddIngredient(ItemID.Glass, 6);
			recipe.AddIngredient(ItemID.ShinePotion);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Diabolist Lamp
			Recipe recipe = Recipe.Create(ItemID.DiablostLamp);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Oil Rag Sconce
			Recipe recipe = Recipe.Create(ItemID.OilRagSconse);
			recipe.AddIngredient(ItemID.IronBrick, 4);
			recipe.AddIngredient(ItemID.Silk);
			recipe.AddIngredient(ItemID.Torch);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	private static void AddGothicFurnitureRecipes() {
		if (!ModuleConfig().enableDungeonItemRenewability) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.GreenDungeonWorkBench);
		{ // Gothic Bookcase
			Recipe recipe = Recipe.Create(ItemID.GothicBookcase);
			recipe.AddRecipeGroup(RecipeGroups.DungeonBrickRecipeGroup, 20);
			recipe.AddIngredient(ItemID.Book, 10);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Gothic Chair
			Recipe recipe = Recipe.Create(ItemID.GothicChair);
			recipe.AddRecipeGroup(RecipeGroups.DungeonBrickRecipeGroup, 4);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Gothic Table
			Recipe recipe = Recipe.Create(ItemID.GothicTable);
			recipe.AddRecipeGroup(RecipeGroups.DungeonBrickRecipeGroup, 8);
			recipe.AddTile(TileID.BoneWelder).AddCondition(Condition.InGraveyard);
			registerer.Register(recipe.DisableDecraft());
		}
		{ // Gothic Work Bench
			Recipe recipe = Recipe.Create(ItemID.GothicWorkBench);
			recipe.AddRecipeGroup(RecipeGroups.DungeonBrickRecipeGroup, 10);
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

	private static void AddReverseShimmers() {
		ItemID.Sets.ShimmerTransformToItem[ItemID.Moondial] = ItemID.Sundial;

		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientCopperBrick] = ItemID.CopperBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientSilverBrick] = ItemID.SilverBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientGoldBrick] = ItemID.GoldBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientBlueDungeonBrick] = ItemID.BlueBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientGreenDungeonBrick] = ItemID.GreenBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientPinkDungeonBrick] = ItemID.PinkBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientObsidianBrick] = ItemID.ObsidianBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientHellstoneBrick] = ItemID.HellstoneBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientCobaltBrick] = ItemID.CobaltBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AncientMythrilBrick] = ItemID.MythrilBrick;

		ItemID.Sets.ShimmerTransformToItem[ItemID.HeavenforgeBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.LunarRustBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.AstraBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.DarkCelestialBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.MercuryBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.StarRoyaleBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.CryocoreBrick] = ItemID.LunarBrick;
		ItemID.Sets.ShimmerTransformToItem[ItemID.CosmicEmberBrick] = ItemID.LunarBrick;
	}

	private static void AddExtraShimmers() {
		ItemID.Sets.ShimmerTransformToItem[ItemID.HelFire] = ItemID.Cascade;
		ItemID.Sets.ShimmerTransformToItem[ItemID.ZapinatorOrange] = ItemID.ZapinatorGray;

		ItemID.Sets.ShimmerTransformToItem[ItemID.SunflowerMinecart] = ItemID.LadybugMinecart;
		ItemID.Sets.ShimmerTransformToItem[ItemID.LadybugMinecart] = ItemID.SunflowerMinecart;
	}
}
