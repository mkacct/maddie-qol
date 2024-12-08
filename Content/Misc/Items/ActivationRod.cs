using static MaddieQoL.Util.RecipeUtil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace MaddieQoL.Content.Misc.Items;

public class ActivationRod : ModItem {
	private static readonly SoundStyle SignalSound = SoundID.Item49;

	public override void SetStaticDefaults() {
		ItemID.Sets.AlsoABuildingItem[this.Item.type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
	}

	public override void SetDefaults() {
		this.Item.width = 36;
		this.Item.height = 36;
		this.Item.useStyle = ItemUseStyleID.HoldUp;
		this.Item.useAnimation = 15;
		this.Item.useTime = 15;
		this.Item.useTurn = true;
		this.Item.tileBoost = 20;
		this.Item.mech = true;
		this.Item.rare = ItemRarityID.Blue;
		this.Item.value = Item.sellPrice(0, 0, 40, 0);
	}

	public override bool CanUseItem(Player player) {
		if (player.whoAmI != Main.myPlayer) {return true;}
		Use(player, Main.MouseWorld);
		return true;
	}

	private void Use(Player player, Vector2 mouseWorld) {
		if (!player.IsTargetTileInItemRange(this.Item)) {return;}
		Point tilePos = mouseWorld.ToTileCoordinates();
		if (!player.CanDoWireStuffHere(tilePos.X, tilePos.Y)) {return;}
		Tile tile = Main.tile[tilePos];
		if (!TileHasWire(tile)) {return;}
		SoundEngine.PlaySound(SignalSound, new Vector2(mouseWorld.X, mouseWorld.Y));
		Wiring.TripWire(tilePos.X, tilePos.Y, 1, 1);
	}

	private static bool TileHasWire(Tile tile) {
		return tile.RedWire || tile.GreenWire || tile.BlueWire || tile.YellowWire;
	}

	public override void HoldItem(Player player) {
		if (player.whoAmI != Main.myPlayer) {return;}
		if (!player.IsTargetTileInItemRange(this.Item)) {return;}
		player.cursorItemIconEnabled = true;
		Main.ItemIconCacheUpdate(this.Item.type);
	}

	public override void AddRecipes() {
		Recipe recipe = this.CreateRecipe();
		recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
		recipe.AddIngredient(ItemID.Wire, 60);
		recipe.AddTile(TileID.Anvils);
		RegisterBeforeFirstRecipe(recipe, ItemID.ActuationRod);
	}
}
