using static MaddieQoL.Common.Shorthands;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public sealed class RenewabilityInventoryPlayerModifier : ModPlayer {
	public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath) {
		if (!UserConf.enableDefaultFamiliarSet) {yield break;}
		if (mediumCoreDeath && !ModuleConf.enableDefaultFamiliarSet) {yield break;}
		yield return new Item(ItemID.FamiliarWig);
		yield return new Item(ItemID.FamiliarShirt);
		yield return new Item(ItemID.FamiliarPants);
	}
}
