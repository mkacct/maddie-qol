using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityCrateLoot : GlobalItem {
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
			case ItemID.DungeonFishingCrateHard:
				ModifyStockadeCrateLoot(itemLoot);
				break;
			case ItemID.LavaCrate:
			case ItemID.LavaCrateHard:
				ModifyLavaCratesLoot(itemLoot);
				break;
		}
	}

	private static void ModifyDesertCratesLoot(ItemLoot itemLoot) {
		if (!ModuleConfig().enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(7, 1, MaxBanners, [
			ItemID.AnkhBanner,
			ItemID.SnakeBanner,
			ItemID.OmegaBanner
		]));
	}

	private static void ModifySkyCratesLoot(ItemLoot itemLoot) {
		if (!ModuleConfig().enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(4, 1, MaxBanners, [
			ItemID.WorldBanner,
			ItemID.SunBanner,
			ItemID.GravityBanner
		]));
	}

	private static void ModifyStockadeCrateLoot(ItemLoot itemLoot) {
		if (!ModuleConfig().enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(2, 1, MaxBanners, [
			ItemID.MarchingBonesBanner,
			ItemID.NecromanticSign,
			ItemID.RustedCompanyStandard,
			ItemID.RaggedBrotherhoodSigil,
			ItemID.MoltenLegionFlag,
			ItemID.DiabolicSigil
		]));
	}

	private static void ModifyLavaCratesLoot(ItemLoot itemLoot) {
		if (!ModuleConfig().enableDecorativeBannerRenewability) {return;}
		itemLoot.Add(ItemDropRules.OneStackFromOptions(2, 1, MaxBanners, [
			ItemID.HellboundBanner,
			ItemID.HellHammerBanner,
			ItemID.HelltowerBanner,
			ItemID.LostHopesofManBanner,
			ItemID.ObsidianWatcherBanner,
			ItemID.LavaEruptsBanner
		]));
	}
}
