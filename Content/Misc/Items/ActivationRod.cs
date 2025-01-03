using static MaddieQoL.Util.RecipeUtil;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Misc.Items;

public class ActivationRod : ModItem {
	private static readonly SoundStyle SignalSound = SoundID.Item49;

	private Point? _pendingNetActivationCoords = null;

	public override void SetStaticDefaults() {
		ItemID.Sets.AlsoABuildingItem[this.Item.type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
	}

	public override void SetDefaults() {
		this.Item.width = 32;
		this.Item.height = 32;
		this.Item.useStyle = ItemUseStyleID.Swing;
		this.Item.useAnimation = 15;
		this.Item.useTime = 15;
		this.Item.useTurn = true;
		this.Item.tileBoost = 20;
		this.Item.mech = true;
		this.Item.rare = ItemRarityID.Blue;
		this.Item.value = Item.sellPrice(0, 0, 40, 0);
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) {
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Wiring;
	}

	public override bool CanUseItem(Player player) {
		if (player.whoAmI != Main.myPlayer) {return true;}
		this.ClientUse(player);
		return true;
	}

	private void ClientUse(Player player) {
		if (!player.IsTargetTileInItemRange(this.Item)) {return;}
		if (!player.CanDoWireStuffHere(Player.tileTargetX, Player.tileTargetY)) {return;}
		Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);;
		if (!TileHasWire(tile)) {return;}

		if (Main.netMode == NetmodeID.SinglePlayer) {
			ActivateWire(Player.tileTargetX, Player.tileTargetY);
		} else {
			this.NetActivateWire(Player.tileTargetX, Player.tileTargetY);
		}
	}

	private static bool TileHasWire(Tile tile) {
		return tile.RedWire || tile.GreenWire || tile.BlueWire || tile.YellowWire;
	}

	private void NetActivateWire(int tileTargetX, int tileTargetY) {
		if (Main.netMode == NetmodeID.MultiplayerClient) {
			this._pendingNetActivationCoords = new Point(tileTargetX, tileTargetY);
			this.Item.NetStateChanged();
		}
	}

	public override void NetSend(BinaryWriter writer) {
		writer.Write(this._pendingNetActivationCoords.HasValue);
		if (this._pendingNetActivationCoords.HasValue) {
			writer.Write(this._pendingNetActivationCoords.Value.X);
			writer.Write(this._pendingNetActivationCoords.Value.Y);
			this._pendingNetActivationCoords = null;
		}
	}

	public override void NetReceive(BinaryReader reader) {
		if (reader.ReadBoolean()) {
			Point coords = new(reader.ReadInt32(), reader.ReadInt32());
			ActivateWire(coords.X, coords.Y);
			if (Main.netMode == NetmodeID.Server) {
				this._pendingNetActivationCoords = coords;
				this.Item.NetStateChanged();
			}
		}
	}

	private static void ActivateWire(int tileTargetX, int tileTargetY) {
		SoundEngine.PlaySound(SignalSound, new Vector2(tileTargetX * 16 + 8, tileTargetY * 16 + 8));
		Wiring.TripWire(tileTargetX, tileTargetY, 1, 1);
	}

	public override void HoldItem(Player player) {
		ItemUtil.DrawHoldItemIcon(player, this.Item);
	}

	public override void AddRecipes() {
		Recipe recipe = this.CreateRecipe();
		recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
		recipe.AddIngredient(ItemID.Wire, 60);
		recipe.AddTile(TileID.Anvils);
		RegisterBeforeFirstRecipe(recipe, ItemID.ActuationRod);
	}
}
