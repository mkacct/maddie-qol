using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Common;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.Renewability;

public sealed class RenewabilityCrateLoot : GlobalItem {

	const int MaxBanners = 2;

	public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
		switch (item.type) {
			case ItemID.OasisCrate:
			case ItemID.OasisCrateHard:
				ModifyDesertCratesLoot(itemLoot);
				break;
			case ItemID.JungleFishingCrate:
			case ItemID.JungleFishingCrateHard:
				ModifyJungleCratesLoot(itemLoot);
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

	static void ModifyDesertCratesLoot(ItemLoot itemLoot) {
		DesertCratesAddFlyingCarpet(itemLoot);
		AddPyramidBanners(itemLoot);
		AddDesertMinecart(itemLoot);
	}

	static void ModifyJungleCratesLoot(ItemLoot itemLoot) {
		AddBeeMinecart(itemLoot);
	}

	static void ModifySkyCratesLoot(ItemLoot itemLoot) {
		AddFloatingIslandBanners(itemLoot);
	}

	static void ModifyDungeonCratesLoot(ItemLoot itemLoot) {
		DungeonCratesAddWaterBolt(itemLoot);
		AddFactionFlags(itemLoot);
	}

	static void ModifyLavaCratesLoot(ItemLoot itemLoot) {
		AddHellBanners(itemLoot);
	}

	static void DungeonCratesAddWaterBolt(ItemLoot itemLoot) {
		if (!ModuleConf.enableDungeonItemRenewability) {return;}
		IItemDropRule bookRule = DungeonCratesFindBookRule(itemLoot);
		if (bookRule == null) {return;}
		if (bookRule.HasMatchingChainedRule((rule) => {
			return (rule is CommonDropNotScalingWithLuck commonNswlRule) && (commonNswlRule.itemId == ItemID.WaterBolt);
		})) {return;}
		bookRule.OnSuccess(ItemDropRule.NotScalingWithLuckWithNumerator(ItemID.WaterBolt, 10000, 1829));
		// overall chance of water bolt is 9.15%
	}

	static CommonDropNotScalingWithLuck DungeonCratesFindBookRule(ItemLoot itemLoot) {
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

	static void DesertCratesAddFlyingCarpet(ItemLoot itemLoot) {
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

	static CommonDropNotScalingWithLuck DesertCratesFindSandstormBottleRule(ItemLoot itemLoot) {
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

	static void AddFactionFlags(ItemLoot itemLoot) {
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

	static void AddFloatingIslandBanners(ItemLoot itemLoot) {
		if (!ModuleConf.enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(4, 1, MaxBanners, [
			ItemID.WorldBanner,
			ItemID.SunBanner,
			ItemID.GravityBanner
		]));
	}

	static void AddHellBanners(ItemLoot itemLoot) {
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

	static void AddPyramidBanners(ItemLoot itemLoot) {
		if (!ModuleConf.enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptionsWithNumerator(35, 4, 1, MaxBanners, [
			ItemID.AnkhBanner,
			ItemID.SnakeBanner,
			ItemID.OmegaBanner
		]));
	}

	static void AddDesertMinecart(ItemLoot itemLoot) {
		if (!ModuleConf.enableMinecartRenewability) {return;}
		itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.DesertMinecart, 30));
	}

	static void AddBeeMinecart(ItemLoot itemLoot) {
		if (!ModuleConf.enableMinecartRenewability) {return;}
		itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.BeeMinecart, 20));
	}

}
