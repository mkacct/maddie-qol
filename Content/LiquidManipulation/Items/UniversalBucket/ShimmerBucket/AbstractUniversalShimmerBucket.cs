using Terraria;
using Terraria.ID;
using static MaddieQoL.Content.LiquidManipulation.LiquidManipulationUniversalBucketSystem;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket.ShimmerBucket;

public abstract class AbstractUniversalShimmerBucket : AbstractUniversalBucket {

	protected override int DummyItemID => UniversalShimmerBucketDummyItemID;

	public override void SetDefaults() {
		base.SetDefaults();
		this.Item.rare = ItemRarityID.Purple;
		this.Item.value = Item.sellPrice(0, 40, 0, 0);
	}

	protected override int NextItemID(int itemId) => UniversalShimmerBucketNextItemID(itemId);

}
