using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.Misc;

public sealed class NoFalseAdvertisingShops : GlobalNPC {

	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Merchant:
				ModifyMerchantShop(shop);
				break;
		}
	}

	static void ModifyMerchantShop(NPCShop shop) {
		if (!ModuleConf.enableMerchantShopPerDialogue) {return;}
		shop.InsertAfter(ItemID.BugNet, new Item(ItemID.Umbrella) {
			shopCustomPrice = Item.buyPrice(0, 0, 50, 0)
		}, Condition.InRain);
		shop.InsertBefore(ItemID.CopperPickaxe, ItemID.CopperShortsword);
		shop.InsertAfter(ItemID.CopperAxe, ItemID.CopperHelmet, Condition.TimeDay);
		shop.InsertAfter(ItemID.CopperHelmet, ItemID.CopperChainmail, Condition.TimeDay);
		shop.InsertAfter(ItemID.CopperChainmail, ItemID.CopperGreaves, Condition.TimeDay);
		shop.InsertAfter(ItemID.ThrowingKnife, ItemID.Lens, Condition.BloodMoon);
		shop.Add(new Item(ItemID.DirtBlock) {
			shopCustomPrice = Item.buyPrice(0, 0, 0, 2)
		});
	}

	public override void ModifyActiveShop(NPC npc, string shopName, Item[] items) {
		if (npc.type == NPCID.TravellingMerchant) {
			ModifyTravelingMerchantActiveShop(items);
		}
	}

	static void ModifyTravelingMerchantActiveShop(Item[] items) {
		if (!ModuleConf.enableMerchantShopPerDialogue) {return;}
		ShopUtil.TryAddItemToActiveShop(items, new Item(ItemID.AngelStatue) {
			shopCustomPrice = Item.buyPrice(0, 5, 0, 0)
		});
	}

}
