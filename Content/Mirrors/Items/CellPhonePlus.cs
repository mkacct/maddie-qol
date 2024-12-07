using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using MaddieQoL.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors.Items;

public class CellPhonePlus : ModItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConfig().enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults(){
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));

		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Item.type] = 25;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.CellPhone);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		if (ModuleConfig().enableReturnTools) {
			Styles.UseReturnStyle(player, this.Item);
		} else {
			Styles.UseRecallStyle(player, this.Item);
		}
	}

	public override void UpdateInfoAccessory(Player player) {
		Util.DisplayEverything(player);
	}

	public override void AddRecipes() {
		if (!ModuleConfig().enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.CellPhone);
		int[] devices = [
			ItemID.PDA,
			ItemID.CellPhone
		];
		foreach (int device in devices) {
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(device);
			recipe.AddIngredient<ReturnMirror>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			registerer.Register(recipe);
		}
	}
}
