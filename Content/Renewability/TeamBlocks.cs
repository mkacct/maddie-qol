using System.Collections.Generic;
using MaddieQoL.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public sealed class RenewabilityTeamBlocks : GlobalItem {
	static readonly IList<KeyValuePair<int, int>> BlocksToPlatforms = [
		new(ItemID.TeamBlockRed, ItemID.TeamBlockRedPlatform),
		new(ItemID.TeamBlockGreen, ItemID.TeamBlockGreenPlatform),
		new(ItemID.TeamBlockBlue, ItemID.TeamBlockBluePlatform),
		new(ItemID.TeamBlockYellow, ItemID.TeamBlockYellowPlatform),
		new(ItemID.TeamBlockPink, ItemID.TeamBlockPinkPlatform)
	];

	static readonly ISet<int> Platforms;

	static RenewabilityTeamBlocks() {
		Platforms = new HashSet<int>();
		foreach (KeyValuePair<int, int> pair in BlocksToPlatforms) {
			Platforms.Add(pair.Value);
		}
	}

	public override void SetStaticDefaults() {
		AddShimmers();
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

	static void AddShimmers() {
		ItemID.Sets.ShimmerTransformToItem[ItemID.TeamBlockWhite] = ItemID.TeamBlockRed;
		ItemID.Sets.ShimmerTransformToItem[ItemID.TeamBlockRed] = ItemID.TeamBlockGreen;
		ItemID.Sets.ShimmerTransformToItem[ItemID.TeamBlockGreen] = ItemID.TeamBlockBlue;
		ItemID.Sets.ShimmerTransformToItem[ItemID.TeamBlockBlue] = ItemID.TeamBlockYellow;
		ItemID.Sets.ShimmerTransformToItem[ItemID.TeamBlockYellow] = ItemID.TeamBlockPink;
		ItemID.Sets.ShimmerTransformToItem[ItemID.TeamBlockPink] = ItemID.TeamBlockWhite;
	}
}
