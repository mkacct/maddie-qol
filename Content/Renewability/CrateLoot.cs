using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;
using Terraria.GameContent.ItemDropRules;

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
