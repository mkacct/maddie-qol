using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
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

	internal static readonly SoundStyle UniversalBucketSwapSound = SoundID.Splash;

	static LiquidManipulationUniversalBucketSystem() {
		UniversalBucketItemIDs = new HashSet<int> {UniversalBucketDummyItemID};
		UniversalBucketItemIDs.UnionWith(UniversalBucketItemIDSequence);
	}

	public override void Load() {
		SwappableItemUtil.RegisterItemResearchOverrideHook(UniversalBucketItemIDSequence, UniversalBucketDummyItemID);
		SwappableItemUtil.RegisterItemSwapHook(UniversalBucketItemIDs, UniversalBucketNextItemID, UniversalBucketSwapSound);
	}

	internal static int UniversalBucketNextItemID(int itemId) {
		return SwappableItemUtil.NextID(UniversalBucketItemIDSequence, itemId);
	}
}
