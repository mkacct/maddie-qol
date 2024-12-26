using static MaddieQoL.Content.LiquidManipulation.LiquidManipulationUniversalBucketSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using MaddieQoL.Util;
using Terraria.Audio;

namespace MaddieQoL.Content.LiquidManipulation.Items.UniversalBucket;

public abstract class AbstractUniversalBucket : AbstractSwappableItem {
	protected virtual int? LiquidType => null;

	public override void SetStaticDefaults() {
		if (this.Item.type != UniversalBucketDummyItemID) {
			ItemID.Sets.ShimmerCountsAsItem[this.Item.type] = UniversalBucketDummyItemID;
		}
		this.Item.ResearchUnlockCount = (this.Item.type == UniversalBucketDummyItemID) ? 1 : 0;
		ItemID.Sets.AlsoABuildingItem[this.Item.type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[this.Item.type] = true;
	}

	public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.BottomlessBucket);
		this.Item.tileBoost = 3;
		this.Item.useAnimation = 8;
		this.Item.useTime = 3;
		this.Item.rare = ItemRarityID.Yellow;
		this.Item.value = Item.sellPrice(0, 30, 0, 0);
	}

	public override bool? UseItem(Player player) {
		if (!this.LiquidType.HasValue) {return null;}
		if (this.PourLiquid(player)) {return true;}
		return null;
	}

	private bool PourLiquid(Player player) {
		if (player.noBuilding) {return false;}
		if (!player.IsTargetTileInItemRange(this.Item)) {return false;}
		Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
		if (tile.LiquidAmount >= 200) {return false;}
		if (tile.HasUnactuatedTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType] && (tile.TileType != TileID.Grate)) {return false;}
		if ((tile.LiquidAmount == 0) || (tile.LiquidType == this.LiquidType.Value)) {
			SoundEngine.PlaySound(SoundID.SplashWeak, new Vector2(player.Center.X, player.Center.Y));
			tile.LiquidType = this.LiquidType.Value;
			tile.Get<LiquidData>().Amount = byte.MaxValue;
			WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY);
			if (Main.netMode == NetmodeID.MultiplayerClient) {NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);}
			return true;
		}
		return false;
	}

	public override void HoldItem(Player player) {
		if (player.whoAmI != Main.myPlayer) {return;}
		if (player.noBuilding) {return;}
		if (!player.IsTargetTileInItemRange(this.Item)) {return;}
		player.cursorItemIconEnabled = true;
		Main.ItemIconCacheUpdate(this.Item.type);
	}

	protected override int NextItemID(int itemId) {
		return UniversalBucketNextItemID(itemId);
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
		this.DrawItemInWorld(UniversalBucketDummyItemID, spriteBatch, lightColor, rotation, scale);
		return false;
	}
}
