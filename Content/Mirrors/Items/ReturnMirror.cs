using static MaddieQoL.Util.RecipeUtil;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Mirrors.Items;

public class ReturnMirror : ModItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

    public override LocalizedText Tooltip => ModContent.GetInstance<ModuleConfig>().enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults(){
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.MagicMirror);
		this.Item.rare = ItemRarityID.LightRed;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		bool enable = ModContent.GetInstance<ModuleConfig>().enableReturnTools;
		MirrorUtil.UseMirrorStyle(player, this.Item, enable ? MirrorUtil.Return : MirrorUtil.Recall);
	}

	public override void AddRecipes() {
		if (!ModContent.GetInstance<ModuleConfig>().enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.MagicMirror);
		int[] mirrors = [
			ItemID.MagicMirror,
			ItemID.IceMirror
		];
		foreach (int mirror in mirrors) {
			Recipe recipe = Recipe.Create(ModContent.ItemType<ReturnMirror>());
			recipe.AddIngredient(mirror);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddIngredient(ItemID.CrystalShard, 30);
			recipe.AddIngredient(ItemID.SoulofNight, 15);
			recipe.AddTile(TileID.AdamantiteForge);
			registerer.Register(recipe);
		}
	}
}
