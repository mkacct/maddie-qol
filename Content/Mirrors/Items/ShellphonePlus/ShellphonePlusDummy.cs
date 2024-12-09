using static MaddieQoL.Util.RecipeUtil;
using static MaddieQoL.Common.Shorthands;
using static MaddieQoL.Content.Mirrors.MirrorShellphonePlusSystem;
using MaddieQoL.Util;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using MaddieQoL.Common;

namespace MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

public class ShellphonePlusDummy : AbstractShellphonePlus {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConfig().enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void AddRecipes() {
		if (!ModuleConfig().enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.ShellphoneDummy);
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient<CellPhonePlus>();
			recipe.AddIngredient(ItemID.MagicConch);
			recipe.AddIngredient(ItemID.DemonConch);
			recipe.AddTile(TileID.TinkerersWorkbench);
			registerer.Register(recipe);
		}
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroups.ShellphoneRecipeGroup);
			recipe.AddIngredient<ReturnMirror>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			registerer.Register(recipe.DisableDecraft());
		}
	}

	public override void OnCreated(ItemCreationContext context) {
		if (context is InitializationItemCreationContext) {return;}
		this.Item.ChangeItemType(ShellphonePlusTypeSequence[0]);
	}
}
