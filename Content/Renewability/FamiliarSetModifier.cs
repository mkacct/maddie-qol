using static MaddieQoL.Common.Shorthands;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityFamiliarSetModifier : GlobalItem {
	private static readonly ISet<int> FamiliarSet = new HashSet<int> {
		ItemID.FamiliarWig,
		ItemID.FamiliarShirt,
		ItemID.FamiliarPants
	};

	public override bool AppliesToEntity(Item item, bool lateInstantiation) {
		return FamiliarSet.Contains(item.type);
	}

	public override void SetDefaults(Item item) {
		if (!ModuleConf.enableDefaultFamiliarSet) {return;}
		item.value = 0;
	}
}
