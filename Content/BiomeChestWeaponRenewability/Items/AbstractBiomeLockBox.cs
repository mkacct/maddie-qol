using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.BiomeChestWeaponRenewability.Items;

public abstract class AbstractBiomeLockBox : ModItem {
	protected abstract int ChestKeyItemID {get;}
	protected abstract int ContainedWeaponItemID {get;}

	public override void SetStaticDefaults() {
		this.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults() {
		this.Item.width = 12;
		this.Item.height = 12;
		this.Item.maxStack = Item.CommonMaxStack;
		this.Item.rare = ItemRarityID.Yellow;
		this.Item.value = Item.buyPrice(0, 2, 0, 0);
	}

    public override void Load() {
        ItemUtil.RegisterContainerItemLockHook(this.Item.type, (player) => {
			if (!ModuleConf.enableBiomeLockBoxes) {return false;}
			if (!NPC.downedPlantBoss) {return false;}
			return player.ConsumeItem(this.ChestKeyItemID, false, true);
		});
    }

    public override bool CanRightClick() {
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot) {
		itemLoot.Add(ItemDropRule.NotScalingWithLuck(this.ContainedWeaponItemID));
	}
}
