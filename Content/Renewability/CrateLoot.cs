using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;
using Terraria.GameContent.ItemDropRules;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Renewability;

public sealed class RenewabilityCrateLoot : GlobalItem {
	private const int MaxBanners = 2;

	public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
		switch (item.type) {
			case ItemID.OasisCrate:
			case ItemID.OasisCrateHard:
				ModifyDesertCratesLoot(itemLoot);
				break;
			case ItemID.FloatingIslandFishingCrate:
			case ItemID.FloatingIslandFishingCrateHard:
				ModifySkyCratesLoot(itemLoot);
				break;
			case ItemID.DungeonFishingCrate:
			case ItemID.DungeonFishingCrateHard:
				ModifyDungeonCratesLoot(itemLoot);
				break;
			case ItemID.LavaCrate:
			case ItemID.LavaCrateHard:
				ModifyLavaCratesLoot(itemLoot);
				break;
		}
	}

	private static void ModifyDesertCratesLoot(ItemLoot itemLoot) {
		DesertCratesAddFlyingCarpet(itemLoot);
		AddPyramidBanners(itemLoot);
	}

	private static void ModifySkyCratesLoot(ItemLoot itemLoot) {
		AddFloatingIslandBanners(itemLoot);
	}

	private static void ModifyDungeonCratesLoot(ItemLoot itemLoot) {
		DungeonCratesAddWaterBolt(itemLoot);
		AddFactionFlags(itemLoot);
	}

	private static void ModifyLavaCratesLoot(ItemLoot itemLoot) {
		AddHellBanners(itemLoot);
	}

	private static void DungeonCratesAddWaterBolt(ItemLoot itemLoot) {
		if (!ModuleConf.enableDungeonItemRenewability) {return;}
		IItemDropRule bookRule = DungeonCratesFindBookRule(itemLoot);
		if (bookRule == null) {return;}
		if (bookRule.HasMatchingChainedRule((rule) => {
			return (rule is CommonDropNotScalingWithLuck commonNswlRule) && (commonNswlRule.itemId == ItemID.WaterBolt);
		})) {return;}
		bookRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.WaterBolt, 6));
	}

	private static CommonDropNotScalingWithLuck DungeonCratesFindBookRule(ItemLoot itemLoot) {
		foreach (IItemDropRule rule in itemLoot.Get(false)) {
			if (rule is AlwaysAtleastOneSuccessDropRule aalosRule) {
				foreach (IItemDropRule subRule in aalosRule.rules) {
					if (subRule is CommonDropNotScalingWithLuck commonNswlRule) {
						if (commonNswlRule.itemId == ItemID.Book) {
							return commonNswlRule;
						}
					}
				}
			}
		}
		return null;
	}

	private static void DesertCratesAddFlyingCarpet(ItemLoot itemLoot) {
		if (!ModuleConf.enableFlyingCarpetRenewability) {return;}
		CommonDropNotScalingWithLuck sandstormBottleRule = DesertCratesFindSandstormBottleRule(itemLoot);
		if (sandstormBottleRule == null) {return;}
		if (sandstormBottleRule.HasMatchingChainedRule((rule) => {
			return (rule is CommonDropNotScalingWithLuck commonNswlRule) && (commonNswlRule.itemId == ItemID.FlyingCarpet);
		})) {return;}
		int num = sandstormBottleRule.chanceNumerator;
		int den = sandstormBottleRule.chanceDenominator - num;
		sandstormBottleRule.OnFailedRoll(ItemDropRule.NotScalingWithLuckWithNumerator(ItemID.FlyingCarpet, den, num));
		// due to probability shenanigans, the flying carpet should now have the same chance as the sandstorm bottle
	}

	private static CommonDropNotScalingWithLuck DesertCratesFindSandstormBottleRule(ItemLoot itemLoot) {
		foreach (IItemDropRule rule in itemLoot.Get(false)) {
			if (rule is AlwaysAtleastOneSuccessDropRule aalosRule) {
				foreach (IItemDropRule subRule in aalosRule.rules) {
					if (subRule is CommonDropNotScalingWithLuck commonNswlRule) {
						if (commonNswlRule.itemId == ItemID.SandstorminaBottle) {
							return commonNswlRule;
						}
					}
				}
			}
		}
		return null;
	}

	private static void AddFactionFlags(ItemLoot itemLoot) {
		if (!ModuleConf.enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(2, 1, MaxBanners, [
			ItemID.MarchingBonesBanner,
			ItemID.NecromanticSign,
			ItemID.RustedCompanyStandard,
			ItemID.RaggedBrotherhoodSigil,
			ItemID.MoltenLegionFlag,
			ItemID.DiabolicSigil
		]));
	}

	private static void AddFloatingIslandBanners(ItemLoot itemLoot) {
		if (!ModuleConf.enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(4, 1, MaxBanners, [
			ItemID.WorldBanner,
			ItemID.SunBanner,
			ItemID.GravityBanner
		]));
	}

	private static void AddHellBanners(ItemLoot itemLoot) {
		if (!ModuleConf.enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(2, 1, MaxBanners, [
			ItemID.HellboundBanner,
			ItemID.HellHammerBanner,
			ItemID.HelltowerBanner,
			ItemID.LostHopesofManBanner,
			ItemID.ObsidianWatcherBanner,
			ItemID.LavaEruptsBanner
		]));
	}

	private static void AddPyramidBanners(ItemLoot itemLoot) {
		if (!ModuleConf.enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptionsWithNumerator(35, 4, 1, MaxBanners, [
			ItemID.AnkhBanner,
			ItemID.SnakeBanner,
			ItemID.OmegaBanner
		]));
	}
}
