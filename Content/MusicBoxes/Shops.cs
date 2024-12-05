using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.MusicBoxes;

public class MusicBoxShops : GlobalNPC {
	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Princess:
				ModifyPrincessShop(shop);
				break;
		}
	}

	private static void ModifyPrincessShop(NPCShop shop) {
		if (!ModContent.GetInstance<ModuleConfig>().enableEasierTitleMusicBoxRecipes) {return;}
		// MusicBoxTitleAlt is actually Music Box (Journey's Beginning)
		shop.InsertBefore(ItemID.MusicBoxCredits, ItemID.MusicBoxTitleAlt, Condition.Hardmode);
	}
}
