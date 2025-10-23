using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using MaddieQoL.Util;
using static MaddieQoL.Content.Mirrors.MirrorShellphonePlusSystem;

namespace MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

public abstract class AbstractShellphonePlus : AbstractSwappableItem {

	protected override SoundStyle AltFunctionSwapSound => ShellphonePlusSwapSound;

	public override void SetStaticDefaults() {
		if (this.Type != ShellphonePlusDummyItemID) {
			ItemID.Sets.ShimmerCountsAsItem[this.Type] = ShellphonePlusDummyItemID;
		}
		this.Item.ResearchUnlockCount = (this.Type == ShellphonePlusDummyItemID) ? 1 : 0;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 30;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.ShellphoneDummy);
		this.Item.rare = ItemRarityID.Cyan;
	}

	public override void UpdateInfoAccessory(Player player) {
		Util.DisplayEverything(player);
	}

	protected override int NextItemID(int itemId) => ShellphonePlusNextItemID(itemId);

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
		ItemUtil.DrawItemInWorld(ShellphonePlusDummyItemID, this.Item, spriteBatch, alphaColor, rotation, scale);
		return false;
	}

}
