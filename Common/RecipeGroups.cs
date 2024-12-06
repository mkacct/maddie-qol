using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Common;

public class RecipeGroups : ModSystem {
	public const string DungeonBrickRecipeGroup = nameof(ItemID.BlueBrick);

	private static LocalizedText DungeonBrickRecipeGroupName {get; set;}

    public override void SetStaticDefaults() {
    	DungeonBrickRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(DungeonBrickRecipeGroupName)}");
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
}
