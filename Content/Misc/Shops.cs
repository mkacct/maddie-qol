using static MaddieQoL.Common.Shorthands;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Misc;

public class MiscShops : GlobalNPC {
	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Merchant:
				ModifyMerchantShop(shop);
				break;
			case NPCID.Mechanic:
				ModifyMechanicShop(shop);
				break;
		}
	}

	private static void ModifyMerchantShop(NPCShop shop) {
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

	private static void ModifyMechanicShop(NPCShop shop) {
		shop.InsertAfter(ItemID.Switch, ModContent.ItemType<Items.Button>());
	}

	public override void ModifyActiveShop(NPC npc, string shopName, Item[] items) {
		if (npc.type == NPCID.TravellingMerchant) {
			ModifyTravelingMerchantActiveShop(items);
		}
	}

	private static void ModifyTravelingMerchantActiveShop(Item[] items) {
		if (!ModuleConf.enableMerchantShopPerDialogue) {return;}
		ShopUtil.TryAddItemToActiveShop(items, new Item(ItemID.AngelStatue) {
			shopCustomPrice = Item.buyPrice(0, 5, 0, 0)
		});
	}
}
