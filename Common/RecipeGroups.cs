using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Common;

public class RecipeGroups : ModSystem {
	public const string ShellphoneRecipeGroup = nameof(ItemID.Shellphone);
	public const string DungeonBrickRecipeGroup = nameof(ItemID.BlueBrick);

	private static LocalizedText ShellphoneRecipeGroupName {get; set;}
	private static LocalizedText DungeonBrickRecipeGroupName {get; set;}

	public override void SetStaticDefaults() {
		ShellphoneRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(ShellphoneRecipeGroupName)}");
		DungeonBrickRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(DungeonBrickRecipeGroupName)}");
	}

	public override void AddRecipeGroups() {
		{
			RecipeGroup group = new(
				() => {return ShellphoneRecipeGroupName.Value;},
				ItemID.ShellphoneDummy, ItemID.Shellphone, ItemID.ShellphoneOcean, ItemID.ShellphoneHell, ItemID.ShellphoneSpawn
			);
			RecipeGroup.RegisterGroup(ShellphoneRecipeGroup, group);
		}
		{
			RecipeGroup group = new(
				() => {return DungeonBrickRecipeGroupName.Value;},
				ItemID.BlueBrick, ItemID.GreenBrick, ItemID.PinkBrick
			);
			RecipeGroup.RegisterGroup(DungeonBrickRecipeGroup, group);
		}
	}
}
