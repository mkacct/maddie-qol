using System.Collections.Generic;
using MaddieQoL.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public sealed class TeamBlockRenewability : GlobalItem {
	private static readonly IList<KeyValuePair<int, int>> BlocksToPlatforms = [
		new(ItemID.TeamBlockRed, ItemID.TeamBlockRedPlatform),
		new(ItemID.TeamBlockGreen, ItemID.TeamBlockGreenPlatform),
		new(ItemID.TeamBlockBlue, ItemID.TeamBlockBluePlatform),
		new(ItemID.TeamBlockYellow, ItemID.TeamBlockYellowPlatform),
		new(ItemID.TeamBlockPink, ItemID.TeamBlockPinkPlatform)
	];

	private static readonly ISet<int> Platforms;

	static TeamBlockRenewability() {
		Platforms = new HashSet<int>();
		foreach (KeyValuePair<int, int> pair in BlocksToPlatforms) {
			Platforms.Add(pair.Value);
		}
	}

	public override void AddRecipes() {
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingBefore(ItemID.WoodShelf);
		foreach (KeyValuePair<int, int> pair in BlocksToPlatforms) {
			int block = pair.Key, platform = pair.Value;
			{
				Recipe recipe = Recipe.Create(platform, 2);
				recipe.AddIngredient(block);
				recipe.RegisterUsing(registerer);
			}
			{
				Recipe recipe = Recipe.Create(block);
				recipe.AddIngredient(platform, 2);
				recipe.DisableDecraft().RegisterUsing(registerer);
			}
		}
	}

    public override void SetDefaults(Item item) {
		if (Platforms.Contains(item.type)) {
			item.value /= 2;
		}
    }
}
