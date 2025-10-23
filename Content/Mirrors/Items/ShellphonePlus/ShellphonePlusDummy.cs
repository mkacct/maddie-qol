using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using MaddieQoL.Util;
using MaddieQoL.Common;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

public sealed class ShellphonePlusDummy : AbstractShellphonePlus {

	static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void AddRecipes() {
		if (!ModuleConf.enableReturnTools) {return;}
		RecipeOrderedRegistrar registrar = RecipeOrderedRegistrar.StartingAfter(ItemID.ShellphoneDummy);
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient<CellPhonePlus>();
			recipe.AddIngredient(ItemID.MagicConch);
			recipe.AddIngredient(ItemID.DemonConch);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registrar);
		}
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroups.ShellphoneRecipeGroup);
			recipe.AddIngredient<ReturnMirror>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.DisableDecraft().RegisterUsing(registrar);
		}
	}

	public override void OnCreated(ItemCreationContext context) {
		this.HandleDummyItemCreation(context);
	}

}
