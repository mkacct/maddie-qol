using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.MusicBoxes;

public sealed class MusicBoxRecipes : ModSystem {

	public override void AddRecipes() {
		if (!ModuleConf.enableEasierTitleMusicBoxRecipes) {return;}
		{ // Music Box (Title)
			Recipe recipe = Recipe.Create(ItemID.MusicBoxTitle);
			recipe.AddIngredient(ItemID.MusicBox);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofFright);
			recipe.AddIngredient(ItemID.SoulofMight);
			recipe.AddIngredient(ItemID.SoulofSight);
			recipe.AddTile(TileID.DemonAltar);
			recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.MusicBoxTitle);
		}
		{ // Music Box (Alt Title)
			Recipe recipe = Recipe.Create(ItemID.MusicBoxConsoleTitle);
			recipe.AddIngredient(ItemID.MusicBoxTitle);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddTile(TileID.WorkBenches).AddCondition(Condition.InGraveyard);
			recipe.DisableDecraft().RegisterAfterLastRecipeOf(ItemID.MusicBoxConsoleTitle);
		}
	}

}
