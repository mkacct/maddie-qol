using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc;

public sealed class MiscShops : GlobalNPC {
	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Mechanic:
				ModifyMechanicShop(shop);
				break;
		}
	}

	private static void ModifyMechanicShop(NPCShop shop) {
		shop.InsertAfter(ItemID.Switch, ModContent.ItemType<Items.Button>());
	}
}
