using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MaddieQoL;

public sealed class UserPrefsConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ClientSide;

	// Teleportation
	[Header("Teleportation")]

	// Enable Teleport Item Scroll-Away
	[DefaultValue(false)]
	public bool enableRecallItemSwitchAway;

	// Miscellanous Enhancements
	[Header("MiscEnhancements")]

	// Buff Furniture Auto-Activation Preferences
	public BuffFurnitureAutoActivationPrefs buffFurnitureAutoActivationPrefs;

	// Vanilla Item Obtainability
	[Header("VanillaItemObtainability")]

	// Show Torch Deswapping Recipes
	[DefaultValue(TorchDeswapDisplayMode.Always)]
	public TorchDeswapDisplayMode showTorchDeswapRecipes;

	// Enable Default Familiar Set
	[DefaultValue(false)]
	[ReloadRequired]
	public bool enableDefaultFamiliarSet;

	// Enable Add Pots to Rubblemaker
	[DefaultValue(false)]
	[ReloadRequired]
	public bool enableAddPotsToWand;

	public UserPrefsConfig() {
		this.buffFurnitureAutoActivationPrefs = new BuffFurnitureAutoActivationPrefs();
	}
}

public sealed class BuffFurnitureAutoActivationPrefs {
	[DefaultValue(true)]
	public bool masterEnable;

	// Vanilla
	[Header("Vanilla")]

	// Enable for Crystal Ball
	[DefaultValue(true)]
	public bool enableForCrystalBall;

	// Enable for Ammo Box
	[DefaultValue(true)]
	public bool enableForAmmoBox;

	// Enable for Bewitching Table
	[DefaultValue(true)]
	public bool enableForBewitchingTable;

	// Enable for Sharpening Station
	[DefaultValue(true)]
	public bool enableForSharpeningStation;

	// Enable for War Table
	[DefaultValue(true)]
	public bool enableForWarTable;

	// Thorium Mod
	[Header("ThoriumMod")]

	// Enable for Altar
	[DefaultValue(true)]
	public bool enableForAltar;

	// Enable for Conductor's Stand
	[DefaultValue(true)]
	public bool enableForConductorsStand;

	// Enable for Ninja Rack
	[DefaultValue(true)]
	public bool enableForNinjaRack;

	public BuffFurnitureAutoActivationPrefs() {
		this.enableForCrystalBall = true;
		this.enableForAmmoBox = true;
		this.enableForBewitchingTable = true;
		this.enableForSharpeningStation = true;
		this.enableForWarTable = true;

		this.enableForAltar = true;
		this.enableForConductorsStand = true;
		this.enableForNinjaRack = true;
	}
}

public enum TorchDeswapDisplayMode {
	[Description("Never")]
	Never,

	[Description("WhenBiomeTorchSwapEnabled")]
	WhenBiomeTorchSwapEnabled,

	[Description("Always")]
	Always
}
