using System.Collections.Generic;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket;

namespace MaddieQoL.Content.LiquidManipulation;

public class LiquidManipulationUniversalBucketSystem : ModSystem {
	internal static readonly int UniversalBucketDummyItemID = ModContent.ItemType<UniversalBucketDummy>();

	private static readonly int[] UniversalBucketItemIDSequence = [
		ModContent.ItemType<UniversalBucketWater>(),
		ModContent.ItemType<UniversalBucketLava>(),
		ModContent.ItemType<UniversalBucketHoney>()
	];

	internal static readonly ISet<int> UniversalBucketItemIDs;

	static LiquidManipulationUniversalBucketSystem() {
		UniversalBucketItemIDs = new HashSet<int> {UniversalBucketDummyItemID};
		UniversalBucketItemIDs.UnionWith(UniversalBucketItemIDSequence);
	}

	public override void Load() {
		SwappableItemUtil.RegisterItemResearchOverrideHook(UniversalBucketItemIDSequence, UniversalBucketDummyItemID);
		SwappableItemUtil.RegisterItemSwapHook(UniversalBucketItemIDs, UniversalBucketNextItemID);
	}

	internal static int UniversalBucketNextItemID(int itemId) {
		return SwappableItemUtil.NextID(UniversalBucketItemIDSequence, itemId);
	}
}
