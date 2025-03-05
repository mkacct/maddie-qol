using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Common;

namespace MaddieQoL.Content.Misc;

public sealed class MiscShops : GlobalNPC {
	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Merchant:
				ModifyMerchantShop(shop);
				break;
			case NPCID.Mechanic:
				ModifyMechanicShop(shop);
				break;
			case NPCID.Steampunker:
				ModifySteampunkerShop(shop);
				break;
		}
	}

	private static void ModifyMerchantShop(NPCShop shop) {
		if (ModuleConf.enableCurfewBell) {
			shop.Add<Items.CurfewBell>(Conditions.InTown);
		}
	}

	private static void ModifyMechanicShop(NPCShop shop) {
		shop.InsertAfter(ItemID.Switch, ModContent.ItemType<Items.Button>());
		if (ModuleConf.enableClockGenerator) {
			shop.InsertAfter(ItemID.TimerOneFourthSecond, ModContent.ItemType<Items.ClockGenerator>(), Condition.DownedMoonLord);
		}
	}

	private static void ModifySteampunkerShop(NPCShop shop) {
		shop.InsertAfter(ItemID.LogicGateLamp_Faulty, ModContent.ItemType<Items.Diode>());
	}
}
