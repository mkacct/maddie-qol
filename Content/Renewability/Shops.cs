using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Renewability;

public class RenewabilityShops : GlobalNPC {
	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Demolitionist:
				ModifyDemolitionistShop(shop);
				break;
			case NPCID.WitchDoctor:
				ModifyWitchDoctorShop(shop);
				break;
			case NPCID.Mechanic:
				ModifyMechanicShop(shop);
				break;
			case NPCID.Steampunker:
				ModifySteampunkerShop(shop);
				break;
		}
	}

	private static void ModifyDemolitionistShop(NPCShop shop) {
		if (!ModContent.GetInstance<ModuleConfig>().enableTrapRecipes) {return;}
		shop.InsertAfter(ItemID.Dynamite, ItemID.Detonator, Condition.NpcIsPresent(NPCID.Mechanic));
	}

	private static void ModifyWitchDoctorShop(NPCShop shop) {
		if (!ModContent.GetInstance<ModuleConfig>().enableLihzahrdItemRenewability) {return;}
		shop.Add(new Item(ItemID.LihzahrdBrick) {
			shopCustomPrice = Item.buyPrice(0, 0, 10, 0)
		}, Condition.InJungle, Condition.DownedGolem);
	}

	private static void ModifyMechanicShop(NPCShop shop) {
		if (!ModContent.GetInstance<ModuleConfig>().enableLihzahrdItemRenewability) {return;}
		shop.InsertAfter(ItemID.BluePressurePlate, ItemID.LihzahrdPressurePlate, Condition.InJungle, Condition.DownedGolem);
	}

	private static void ModifySteampunkerShop(NPCShop shop) {
		if (!ModContent.GetInstance<ModuleConfig>().enableLihzahrdItemRenewability) {return;}
		shop.InsertBefore(ItemID.SteampunkBoiler, ItemID.LihzahrdFurnace, Condition.InJungle, Condition.DownedGolem);
	}
}
