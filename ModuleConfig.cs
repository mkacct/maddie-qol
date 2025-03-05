using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MaddieQoL;

public sealed class ModuleConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ServerSide;

	// Teleportation
	[Header("Teleportation")]

	// Enable Return Tools
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableReturnTools;

	// Liquid Manipulation
	[Header("LiquidManipulation")]

	// Enable Endless Liquid Sources & Drains
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableLiquidSourcesAndDrains;

	// Wiring
	[Header("Wiring")]

	// Enable Clock Generator
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableClockGenerator;

	// Other Items
	[Header("OtherItems")]

	// Enable Curfew Bell
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableCurfewBell;

	// Enable Purification-Only Solution
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enablePurificationOnlySolution;

	// Miscellanous Enhancements
	[Header("MiscEnhancements")]

	// Enable Buff Furniture Auto-Activation
	[DefaultValue(true)]
	public bool enableBuffFurnitureAutoActivation;

	// Vanilla Item Obtainability
	[Header("VanillaItemObtainability")]

	// Enable Torch Deswapping
	[DefaultValue(true)]
	public bool enableTorchDeswap;

	// Enable Biome Lock Boxes
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableBiomeLockBoxes;

	// Enable Lihzahrd Item Renewability
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableLihzahrdItemRenewability;

	// Enable Dungeon Item Renewability
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableDungeonItemRenewability;

	// Enable Trap Recipes
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableTrapRecipes;

	// Enable Chest Recipes
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableChestRecipes;

	// Enable Vase Recipes
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableVaseRecipes;

	// Enable Easier Title Music Box Obtainability
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableEasierTitleMusicBoxRecipes;

	// Enable Dirt from Mud
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableDirtFromMud;

	// Enable Default Familiar Set
	[DefaultValue(true)]
	public bool enableDefaultFamiliarSet;

	// Enable Decorative Banner Renewability
	[DefaultValue(false)]
	[ReloadRequired]
	public bool enableDecorativeBannerRenewability;

	// Enable Statue Renewability
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableStatueRenewability;

	// Enable Add Pots to Rubblemaker
	[DefaultValue(false)]
	[ReloadRequired]
	public bool enableAddPotsToWand;

	// Enable Lihzahrd Door Lock
	[DefaultValue(true)]
	public bool enableLihzahrdDoorLock;

	// Enable Minecart Obtainability Enhancement
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableMinecartRenewability;

	// Enable Flying Carpet Obtainability Enhancement
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableFlyingCarpetRenewability;

	// Enable Early-Game Treasure Renewability
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableGoldChestItemRenewability;

	// Enable Encumbering Stone Recipe
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableEncumberingStoneRecipe;

	// Enable Pwnhammer Recipe
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enablePwnhammerRecipe;

	// Enable Hellforge Recipe
	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableHellforgeRecipe;

	// Enable No False Advertising
	[DefaultValue(false)]
	[ReloadRequired]
	public bool enableMerchantShopPerDialogue;
}
