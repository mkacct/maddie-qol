using static MaddieQoL.Content.Mirrors.MirrorShellphonePlusSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

public abstract class AbstractShellphonePlus : ModItem {
	public override void SetStaticDefaults() {
		if (this.Item.type != ShellphonePlusDummyType) {
			ItemID.Sets.ShimmerCountsAsItem[this.Item.type] = ShellphonePlusDummyType;
		}
		this.Item.ResearchUnlockCount = (this.Item.type == ShellphonePlusDummyType) ? 1 : 0;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Item.type] = 30;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.ShellphoneDummy);
		this.Item.rare = ItemRarityID.Cyan;
	}

	public override void UpdateInfoAccessory(Player player) {
		Util.DisplayEverything(player);
	}

	public override bool AltFunctionUse(Player player) {return true;}

	public override bool CanUseItem(Player player) {
		if (player.altFunctionUse == 2) {
			HandleAltFunction(player);
			return false;
		}
		return true;
	}

	private void HandleAltFunction(Player player) {
		player.releaseUseTile = false;
		Main.mouseRightRelease = false;
		SoundEngine.PlaySound(SoundID.Unlock);
		this.Item.ChangeItemType(ShellphonePlusNextType(this.Item.type));
		Recipe.FindRecipes();
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
		Main.GetItemDrawFrame(ShellphonePlusDummyType, out var itemTexture, out var itemFrame);
		Vector2 drawOrigin = itemFrame.Size() / 2f;
		Vector2 drawPosition = this.Item.Bottom - Main.screenPosition - new Vector2(0, drawOrigin.Y);
		spriteBatch.Draw(itemTexture, drawPosition, itemFrame, lightColor, rotation, drawOrigin, scale, SpriteEffects.None, 0);
		return false;
	}
}
