using Terraria.ID;
using Terraria.ModLoader;
using static MaddieQoL.Common.Shorthands;
using MaddieQoL.Content.ClentaminatorSolution.Items;

namespace MaddieQoL.Content.ClentaminatorSolution;

public sealed class ClentaminatorSolutionShops : GlobalNPC {

	public override void ModifyShop(NPCShop shop) {
		switch (shop.NpcType) {
			case NPCID.Steampunker:
				ModifySteampunkerShop(shop);
				break;
		}
	}

	static void ModifySteampunkerShop(NPCShop shop) {
		if (!ModuleConf.enablePurificationOnlySolution) {return;}
		shop.InsertAfter(
			ItemID.GreenSolution,
			ModContent.ItemType<LightGreenSolution>(),
			[.. shop.GetEntry(ItemID.GreenSolution).Conditions]
		);
	}

}
