using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.MusicBoxes;

public class MusicBoxRecipes : ModSystem {
	public override void AddRecipes() {
		if (!ModuleConfig().enableEasierTitleMusicBoxRecipes) {return;}
		{ // Music Box (Title)
			Recipe recipe = Recipe.Create(ItemID.MusicBoxTitle);
			recipe.AddIngredient(ItemID.MusicBox);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofFright);
			recipe.AddIngredient(ItemID.SoulofMight);
			recipe.AddIngredient(ItemID.SoulofSight);
			recipe.AddTile(TileID.DemonAltar);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.MusicBoxTitle);
		}
		{ // Music Box (Alt Title)
			Recipe recipe = Recipe.Create(ItemID.MusicBoxConsoleTitle);
			recipe.AddIngredient(ItemID.MusicBoxTitle);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddTile(TileID.WorkBenches).AddCondition(Condition.InGraveyard);
			RegisterAfterLastRecipe(recipe.DisableDecraft(), ItemID.MusicBoxConsoleTitle);
		}
	}
}
