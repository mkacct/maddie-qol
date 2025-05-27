using static MaddieQoL.Common.Shorthands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Mirrors.Items;

public sealed class ReturnMirror : ModItem {
	static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));

		ItemID.Sets.DuplicationMenuToolsFilter[this.Type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 24;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.MagicMirror);
		this.Item.rare = ItemRarityID.LightRed;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		if (ModuleConf.enableReturnTools) {
			Styles.UseReturnStyle(player, this.Item);
		} else {
			Styles.UseRecallStyle(player, this.Item);
		}
	}

	public override void AddRecipes() {
		if (!ModuleConf.enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingAfter(ItemID.MagicMirror);
		int[] mirrors = [
			ItemID.MagicMirror,
			ItemID.IceMirror
		];
		foreach (int mirror in mirrors) {
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient(mirror);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 8);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.RegisterUsing(registerer);
		}
	}
}
