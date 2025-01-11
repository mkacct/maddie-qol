using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using MaddieQoL.Util;
using MaddieQoL.Common;

namespace MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

public class ShellphonePlusDummy : AbstractShellphonePlus {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void AddRecipes() {
		if (!ModuleConf.enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = RecipeOrderedRegisterer.StartingAfter(ItemID.ShellphoneDummy);
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient<CellPhonePlus>();
			recipe.AddIngredient(ItemID.MagicConch);
			recipe.AddIngredient(ItemID.DemonConch);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.RegisterUsing(registerer);
		}
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroups.ShellphoneRecipeGroup);
			recipe.AddIngredient<ReturnMirror>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.DisableDecraft().RegisterUsing(registerer);
		}
	}

	public override void OnCreated(ItemCreationContext context) {
		this.HandleDummyItemCreation(context);
	}
}
