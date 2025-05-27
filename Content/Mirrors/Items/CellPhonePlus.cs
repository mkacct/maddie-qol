using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Mirrors.Items;

public sealed class CellPhonePlus : ModItem {
	static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));

		ItemID.Sets.DuplicationMenuToolsFilter[this.Type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 25;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.CellPhone);
		this.Item.rare = ItemRarityID.Yellow;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		if (ModuleConf.enableReturnTools) {
			Styles.UseReturnStyle(player, this.Item);
		} else {
			Styles.UseRecallStyle(player, this.Item);
		}
	}

	public override void UpdateInfoAccessory(Player player) {
		Util.DisplayEverything(player);
	}

	public override bool OnPickup(Player player) {
		AchievementsHelper.NotifyItemPickup(player, new Item(ItemID.CellPhone));
		return true;
	}

	public override void AddRecipes() {
		if (!ModuleConf.enableReturnTools) {return;}
		RecipeOrderedRegistrar registrar = RecipeOrderedRegistrar.StartingAfter(ItemID.CellPhone);
		int[] devices = [
			ItemID.PDA,
			ItemID.CellPhone
		];
		foreach (int device in devices) {
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(device);
			recipe.AddIngredient<ReturnMirror>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registrar);
		}
	}
}
