using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.Renewability;

public sealed class RenewabilityShops : GlobalNPC {

	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Merchant:
				ModifyMerchantShop(shop);
				break;
			case NPCID.Demolitionist:
				ModifyDemolitionistShop(shop);
				break;
			case NPCID.BestiaryGirl:
				ModifyZoologistShop(shop);
				break;
			case NPCID.Dryad:
				ModifyDryadShop(shop);
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
			case NPCID.Truffle:
				ModifyTruffleShop(shop);
				break;
			case NPCID.SkeletonMerchant:
				ModifySkeletonMerchantShop(shop);
				break;
		}
	}

	static void ModifyMerchantShop(NPCShop shop) {
		if (!ModuleConf.enableStatueRenewability) {return;}
		shop.Add(new Item(ItemID.AngelStatue) {
			shopCustomPrice = Item.buyPrice(0, 5, 0, 0)
		}, Condition.InGraveyard);
	}

	static void ModifyDemolitionistShop(NPCShop shop) {
		if (!ModuleConf.enableTrapRecipes) {return;}
		shop.InsertAfter(ItemID.Dynamite, ItemID.Detonator, Condition.NpcIsPresent(NPCID.Mechanic));
	}

	static void ModifyZoologistShop(NPCShop shop) {
		if (!ModuleConf.enableStatueRenewability) {return;}
		shop.Add(new Item(ItemID.KingStatue) {
			shopCustomPrice = Item.buyPrice(0, 5, 0, 0)
		}, Condition.InGraveyard);
		shop.Add(new Item(ItemID.QueenStatue) {
			shopCustomPrice = Item.buyPrice(0, 5, 0, 0)
		}, Condition.InGraveyard);
	}

	static void ModifyDryadShop(NPCShop shop) {
		if (!ModuleConf.enableMinecartRenewability) {return;}
		shop.Add(new Item(ItemID.SunflowerMinecart) {
			shopCustomPrice = Item.buyPrice(0, 10, 0, 0)
		}, Condition.MoonPhasesEvenQuarters);
		shop.Add(new Item(ItemID.LadybugMinecart) {
			shopCustomPrice = Item.buyPrice(0, 10, 0, 0)
		}, Condition.MoonPhasesOddQuarters);
	}

	static void ModifyWitchDoctorShop(NPCShop shop) {
		// TODO: reassess after 1.4.5
		// if (!ModuleConf.enableLihzahrdItemRenewability) {return;}
		// shop.Add(new Item(ItemID.LihzahrdBrick) {
		// 	shopCustomPrice = Item.buyPrice(0, 0, 10, 0)
		// }, Condition.InJungle, Condition.DownedGolem, Conditions.PlayerHasPickaxePower(210));
	}

	static void ModifyMechanicShop(NPCShop shop) {
		if (!ModuleConf.enableLihzahrdItemRenewability) {return;}
		shop.InsertAfter(
			ItemID.BluePressurePlate, ItemID.LihzahrdPressurePlate, Condition.InJungle, Condition.DownedGolem
		);
	}

	static void ModifySteampunkerShop(NPCShop shop) {
		if (!ModuleConf.enableLihzahrdItemRenewability) {return;}
		shop.InsertBefore(ItemID.SteampunkBoiler, ItemID.LihzahrdFurnace, Condition.InJungle, Condition.DownedGolem);
	}

	static void ModifyTruffleShop(NPCShop shop) {
		if (!ModuleConf.enableMinecartRenewability) {return;}
		shop.InsertBefore(ItemID.Autohammer, new Item(ItemID.ShroomMinecart) {
			shopCustomPrice = Item.buyPrice(0, 10, 0, 0)
		});
	}

	static void ModifySkeletonMerchantShop(NPCShop shop) {
		if (!ModuleConf.enableGoldChestItemRenewability) {return;}
		shop.InsertBefore(ItemID.StrangeBrew, ItemID.FlareGun, Condition.MoonPhaseFull);
	}

}
