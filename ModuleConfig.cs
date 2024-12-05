using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MaddieQoL;

public class ModuleConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("Teleportation")]

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableReturnTools;

	[DefaultValue(false)]
	public bool enableRecallItemSwitchAway;

	[Header("VanillaItemObtainability")]

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableDungeonItemRenewability;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableLihzahrdItemRenewability;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableTrapRecipes;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableChestRecipes;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableVaseRecipes;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableEasierTitleMusicBoxRecipes;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableEncumberingStoneRecipe;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool enableHellforgeRecipe;
}
