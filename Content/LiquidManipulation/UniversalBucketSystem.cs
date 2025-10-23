using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket;
using MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket.ShimmerBucket;

namespace MaddieQoL.Content.LiquidManipulation;

public sealed class LiquidManipulationUniversalBucketSystem : ModSystem {

	internal static readonly int UniversalBucketDummyItemID = ModContent.ItemType<UniversalBucketDummy>();
	internal static readonly int UniversalShimmerBucketDummyItemID = ModContent.ItemType<UniversalShimmerBucketDummy>();

	static readonly int[] UniversalBucketItemIDSequence = [
		ModContent.ItemType<UniversalBucketWater>(),
		ModContent.ItemType<UniversalBucketLava>(),
		ModContent.ItemType<UniversalBucketHoney>()
	];
	static readonly int[] UniversalShimmerBucketItemIDSequence = [
		ModContent.ItemType<UniversalShimmerBucketWater>(),
		ModContent.ItemType<UniversalShimmerBucketLava>(),
		ModContent.ItemType<UniversalShimmerBucketHoney>(),
		ModContent.ItemType<UniversalShimmerBucketShimmer>()
	];

	internal static readonly ISet<int> UniversalBucketItemIDs;
	internal static readonly ISet<int> UniversalShimmerBucketItemIDs;

	internal static readonly SoundStyle UniversalBucketSwapSound = SoundID.Splash;

	static LiquidManipulationUniversalBucketSystem() {
		UniversalBucketItemIDs = new HashSet<int> {UniversalBucketDummyItemID};
		UniversalBucketItemIDs.UnionWith(UniversalBucketItemIDSequence);

		UniversalShimmerBucketItemIDs = new HashSet<int> {UniversalShimmerBucketDummyItemID};
		UniversalShimmerBucketItemIDs.UnionWith(UniversalShimmerBucketItemIDSequence);
	}

	public override void Load() {
		SwappableItemUtil.RegisterItemResearchOverrideHook(UniversalBucketItemIDSequence, UniversalBucketDummyItemID);
		SwappableItemUtil.RegisterItemSwapHook(UniversalBucketItemIDs, UniversalBucketNextItemID, UniversalBucketSwapSound);

		SwappableItemUtil.RegisterItemResearchOverrideHook(UniversalShimmerBucketItemIDSequence, UniversalShimmerBucketDummyItemID);
		SwappableItemUtil.RegisterItemSwapHook(UniversalShimmerBucketItemIDs, UniversalShimmerBucketNextItemID, UniversalBucketSwapSound);
	}

	internal static int UniversalBucketNextItemID(int itemId) {
		return SwappableItemUtil.NextID(UniversalBucketItemIDSequence, itemId);
	}

	internal static int UniversalShimmerBucketNextItemID(int itemId) {
		return SwappableItemUtil.NextID(UniversalShimmerBucketItemIDSequence, itemId);
	}

}
