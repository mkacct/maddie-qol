using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Misc.Items;

public sealed class ActivationRod : ModItem {
	public static readonly PacketHandler<PointPacketData> ActivationPacketHandler = new(
		ServerHandleActivationPacket,
		ClientHandleActivationPacket
	);

	static readonly SoundStyle SignalSound = SoundID.Item49;

	public override void SetStaticDefaults() {
		ItemID.Sets.AlsoABuildingItem[this.Type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Type] = true;
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

	public override bool? UseItem(Player player) {
		if (player != Main.LocalPlayer) {return null;}
		this.ClientUse(player);
		return true;
	}

	void ClientUse(Player player) {
		if (!player.IsTargetTileInItemRange(this.Item)) {return;}
		if (!player.CanDoWireStuffHere(Player.tileTargetX, Player.tileTargetY)) {return;}
		Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
		if (!TileHasWire(tile)) {return;}

		if (Main.netMode == NetmodeID.MultiplayerClient) {
			Point tileTarget = new(Player.tileTargetX, Player.tileTargetY);
			ActivationPacketHandler.Send(new(tileTarget));
		} else { // single player
			Wiring.TripWire(Player.tileTargetX, Player.tileTargetY, 1, 1);
			EmitSound(Player.tileTargetX, Player.tileTargetY);
		}
	}

	static bool TileHasWire(Tile tile) {
		return tile.RedWire || tile.GreenWire || tile.BlueWire || tile.YellowWire;
	}

	static void EmitSound(int tileTargetX, int tileTargetY) {
		SoundEngine.PlaySound(SignalSound, new Vector2(tileTargetX * 16 + 8, tileTargetY * 16 + 8));
	}

	static void ServerHandleActivationPacket(PointPacketData data, int srcPlayerId) {
		Wiring.TripWire(data.Point.X, data.Point.Y, 1, 1);
		ActivationPacketHandler.Send(new(data.Point));
	}

	static void ClientHandleActivationPacket(PointPacketData data) {
		EmitSound(data.Point.X, data.Point.Y);
	}

	public override void HoldItem(Player player) {
		ItemUtil.DrawHoldItemIcon(player, this.Item);
	}

	public override void AddRecipes() {
		Recipe recipe = this.CreateRecipe();
		recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
		recipe.AddIngredient(ItemID.Wire, 60);
		recipe.AddTile(TileID.Anvils);
		recipe.RegisterBeforeFirstRecipeOf(ItemID.ActuationRod);
	}
}
