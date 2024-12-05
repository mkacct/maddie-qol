using static MaddieQoL.Util.RecipeUtil;
using MaddieQoL.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors.Items;

public class CellPhonePlus : ModItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModContent.GetInstance<ModuleConfig>().enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults(){
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.CellPhone);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		bool enable = ModContent.GetInstance<ModuleConfig>().enableReturnTools;
		MirrorUtil.UseMirrorStyle(player, this.Item, enable ? MirrorUtil.Return : MirrorUtil.Recall);
	}

	public override void UpdateInfoAccessory(Player player) {
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
		player.accFishFinder = true;
		player.accWeatherRadio = true;
		player.accCalendar = true;
		player.accThirdEye = true;
		player.accJarOfSouls = true;
		player.accCritterGuide = true;
		player.accStopwatch = true;
		player.accOreFinder = true;
		player.accDreamCatcher = true;
	}

	public override void AddRecipes() {
		if (!ModContent.GetInstance<ModuleConfig>().enableReturnTools) {return;}
		RecipeOrderedRegisterer registerer = OrderedRegistererStartingAfter(ItemID.CellPhone);
		int[] devices = [
			ItemID.PDA,
			ItemID.CellPhone
		];
		foreach (int device in devices) {
			Recipe recipe = Recipe.Create(ModContent.ItemType<CellPhonePlus>());
			recipe.AddIngredient(device);
			recipe.AddIngredient<ReturnMirror>();
			recipe.AddTile(TileID.TinkerersWorkbench);
			registerer.Register(recipe);
		}
	}
}
