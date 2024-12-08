using static MaddieQoL.Util.RecipeUtil;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace MaddieQoL.Content.Misc.Items;

public class ActivationRod : ModItem {
	private static readonly SoundStyle SignalSound = SoundID.Item49;

	private Vector2? _pendingNetActivateWireCoords = null;

	public override void SetStaticDefaults() {
		ItemID.Sets.AlsoABuildingItem[this.Item.type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
	}

	public override void SetDefaults() {
		this.Item.width = 36;
		this.Item.height = 36;
		this.Item.useStyle = ItemUseStyleID.Swing;
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
		this.ClientUse(player);
		return true;
	}

	private void ClientUse(Player player) {
		if (!player.IsTargetTileInItemRange(this.Item)) {return;}
		Point tilePos = Main.MouseWorld.ToTileCoordinates();
		if (!player.CanDoWireStuffHere(tilePos.X, tilePos.Y)) {return;}
		Tile tile = Main.tile[tilePos];
		if (!TileHasWire(tile)) {return;}

		ActivateWire(Main.MouseWorld);
		this.NetActivateWire(Main.MouseWorld);
	}

	private static bool TileHasWire(Tile tile) {
		return tile.RedWire || tile.GreenWire || tile.BlueWire || tile.YellowWire;
	}

	private void NetActivateWire(Vector2 coords) {
		if (Main.netMode == NetmodeID.MultiplayerClient) {
			this._pendingNetActivateWireCoords = coords;
			this.Item.NetStateChanged();
		}
	}

	private static void ActivateWire(Vector2 coords) {
		SoundEngine.PlaySound(SignalSound, coords);
		Point tilePos = coords.ToTileCoordinates();
		Wiring.TripWire(tilePos.X, tilePos.Y, 1, 1);
	}

	public override void NetSend(BinaryWriter writer) {
		if (this._pendingNetActivateWireCoords.HasValue) {
			writer.Write(true);
			writer.Write(this._pendingNetActivateWireCoords.Value.X);
			writer.Write(this._pendingNetActivateWireCoords.Value.Y);
			this._pendingNetActivateWireCoords = null;
			return;
		}
		writer.Write(false);
	}

	public override void NetReceive(BinaryReader reader) {
		if (reader.ReadBoolean()) {
			Vector2 worldPos = new(reader.ReadSingle(), reader.ReadSingle());
			ActivateWire(worldPos);
		}
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
