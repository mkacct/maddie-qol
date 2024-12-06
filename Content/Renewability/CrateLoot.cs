using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityCrateLoot : GlobalItem {
	public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
		switch (item.type) {
			case ItemID.DungeonFishingCrateHard:
				ModifyStockadeCrateLoot(itemLoot);
				break;
		}
	}

	private static void ModifyStockadeCrateLoot(ItemLoot itemLoot) {
		if (!ModuleConfig().enableDungeonItemRenewability) {return;}
		IItemDropRule[] dungeonBricks = [
			ItemDropRule.Common(ItemID.BlueBrick, 1, 20, 50),
			ItemDropRule.Common(ItemID.GreenBrick, 1, 20, 50),
			ItemDropRule.Common(ItemID.PinkBrick, 1, 20, 50),
		];
		itemLoot.Add(new OneFromRulesRule(2, dungeonBricks));
	}
}
