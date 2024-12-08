using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Mirrors.Items;

public class ReturnMirror : ModItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConfig().enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));

		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Item.type] = 24;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.MagicMirror);
		this.Item.rare = ItemRarityID.LightRed;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		if (ModuleConfig().enableReturnTools) {
			Styles.UseReturnStyle(player, this.Item);
		} else {
			Styles.UseRecallStyle(player, this.Item);
		}
	}

	public override void AddRecipes() {
		if (!ModuleConfig().enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.MagicMirror);
		int[] mirrors = [
			ItemID.MagicMirror,
			ItemID.IceMirror
		];
		foreach (int mirror in mirrors) {
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(mirror);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddIngredient(ItemID.CrystalShard, 30);
			recipe.AddIngredient(ItemID.SoulofNight, 15);
			recipe.AddTile(TileID.AdamantiteForge);
			registerer.Register(recipe);
		}
	}
}
