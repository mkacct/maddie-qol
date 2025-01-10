using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Common;

public class IDSets : ModSystem {
	public static readonly ISet<int> ShellphoneItemIDs = new HashSet<int> {
		ItemID.ShellphoneDummy,
		ItemID.Shellphone,
		ItemID.ShellphoneSpawn,
		ItemID.ShellphoneOcean,
		ItemID.ShellphoneHell
	};

	public static readonly ISet<int> CampfireItemIDs = new HashSet<int> {
		ItemID.Campfire,
		ItemID.CursedCampfire,
		ItemID.DemonCampfire,
		ItemID.FrozenCampfire,
		ItemID.IchorCampfire,
		ItemID.RainbowCampfire,
		ItemID.UltraBrightCampfire,
		ItemID.BoneCampfire,
		ItemID.DesertCampfire,
		ItemID.CoralCampfire,
		ItemID.CorruptCampfire,
		ItemID.CrimsonCampfire,
		ItemID.HallowedCampfire,
		ItemID.JungleCampfire,
		ItemID.MushroomCampfire,
		ItemID.ShimmerCampfire
	};

	public override void PostSetupRecipes() {
		SuppressCampfireMaterialTooltips();
	}

	private static void SuppressCampfireMaterialTooltips() {
		foreach (int campfire in CampfireItemIDs) {
			ItemID.Sets.IsAMaterial[campfire] = false;
		}
	}
}
