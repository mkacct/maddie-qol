using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using MaddieQoL.Util;
using static MaddieQoL.Content.LiquidManipulation.LiquidManipulationUniversalBucketSystem;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket;

public abstract class AbstractUniversalBucket : AbstractSwappableItem {

	protected override SoundStyle AltFunctionSwapSound => UniversalBucketSwapSound;

	protected virtual int DummyItemID => UniversalBucketDummyItemID;

	protected virtual int? LiquidType => null;

	public override void SetStaticDefaults() {
		if (this.Type != DummyItemID) {
			ItemID.Sets.ShimmerCountsAsItem[this.Type] = DummyItemID;
		}
		this.Item.ResearchUnlockCount = (this.Type == DummyItemID) ? 1 : 0;
		ItemID.Sets.AlsoABuildingItem[this.Type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Type] = true;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.BottomlessBucket);
		this.Item.tileBoost = 3;
		this.Item.useAnimation = 7;
		this.Item.useTime = 3;
		this.Item.rare = ItemRarityID.Yellow;
		this.Item.value = Item.sellPrice(0, 30, 0, 0);
	}

	public override bool? UseItem(Player player) {
		if (!this.LiquidType.HasValue) {return null;}
		if (player != Main.LocalPlayer) {return null;}
		if (this.ClientPourLiquid(player)) {return true;}
		return null;
	}

	bool ClientPourLiquid(Player player) {
		if (player.noBuilding) {return false;}
		if (!player.IsTargetTileInItemRange(this.Item)) {return false;}
		Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
		if (tile.LiquidAmount >= 200) {return false;}
		if (
			tile.HasUnactuatedTile
			&& Main.tileSolid[tile.TileType]
			&& !Main.tileSolidTop[tile.TileType]
			&& (tile.TileType != TileID.Grate)
		) {return false;}
		if ((tile.LiquidAmount == 0) || (tile.LiquidType == this.LiquidType.Value)) {
			SoundEngine.PlaySound(SoundID.SplashWeak, player.Center);
			tile.LiquidType = this.LiquidType.Value;
			tile.LiquidAmount = byte.MaxValue;
			WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
			if (Main.netMode == NetmodeID.MultiplayerClient) {
				NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
			}
			return true;
		}
		return false;
	}

	public override void HoldItem(Player player) {
		if (player.noBuilding) {return;}
		ItemUtil.DrawHoldItemIcon(player, this.Item);
	}

	protected override int NextItemID(int itemId) => UniversalBucketNextItemID(itemId);

	public override bool PreDrawInWorld(
		SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI
	) {
		ItemUtil.DrawItemInWorld(DummyItemID, this.Item, spriteBatch, alphaColor, rotation, scale);
		return false;
	}

}
