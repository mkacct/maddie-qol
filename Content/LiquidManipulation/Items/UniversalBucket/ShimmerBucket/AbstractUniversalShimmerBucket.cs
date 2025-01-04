using static MaddieQoL.Content.LiquidManipulation.LiquidManipulationUniversalBucketSystem;
using Terraria.ID;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket.ShimmerBucket;

public abstract class AbstractUniversalShimmerBucket : AbstractUniversalBucket {
	protected override int DummyItemID => UniversalShimmerBucketDummyItemID;

	public override void SetDefaults() {
		base.SetDefaults();
		this.Item.rare = ItemRarityID.Purple;
	}

	protected override int NextItemID(int itemId) {
		return UniversalShimmerBucketNextItemID(itemId);
	}
}
