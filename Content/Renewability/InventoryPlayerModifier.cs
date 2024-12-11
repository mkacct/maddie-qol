using static MaddieQoL.Common.Shorthands;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityInventoryPlayerModifier : ModPlayer {
	public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath) {
		if (!ModuleConfig().enableDefaultFamiliarSet) {yield break;}
		yield return new Item(ItemID.FamiliarWig);
		yield return new Item(ItemID.FamiliarShirt);
		yield return new Item(ItemID.FamiliarPants);
	}
}