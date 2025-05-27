using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.MusicBoxes;

public sealed class MusicBoxShops : GlobalNPC {
	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Wizard:
				ModifyWizardShop(shop);
				break;
			case NPCID.Princess:
				ModifyPrincessShop(shop);
				break;
		}
	}

	static void ModifyWizardShop(NPCShop shop) {
		shop.InsertAfter(ItemID.MusicBox, ModContent.ItemType<Items.SilenceBox>(), Condition.InGraveyard);
	}

	static void ModifyPrincessShop(NPCShop shop) {
		if (!ModuleConf.enableEasierTitleMusicBoxRecipes) {return;}
		// MusicBoxTitleAlt is actually Music Box (Journey's Beginning)
		shop.InsertBefore(ItemID.MusicBoxCredits, ItemID.MusicBoxTitleAlt, Condition.Hardmode);
	}
}
