using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Util;

public abstract class AbstractSolutionItem : ModItem {
	protected abstract int SprayProjectileID {get;}

	protected virtual bool IsItemTypeUsableAsAmmo => true;

	public override void SetStaticDefaults() {
		this.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults() {
		this.Item.DefaultToSolution(this.SprayProjectileID);
		if (!this.IsItemTypeUsableAsAmmo) {
			this.Item.ammo = int.MaxValue; // i sure hope that's never a real ammo type id
			this.Item.shoot = ProjectileID.None;
		}
		this.Item.value = Item.buyPrice(0, 0, 15, 0);
		this.Item.rare = ItemRarityID.Orange;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) {
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Solutions;
	}
}

public abstract class AbstractSolutionProjectile : ModProjectile {
	protected abstract int SprayDustID {get;}

	private ref float Progress => ref Projectile.ai[0];

	public override void SetDefaults() {
		this.Projectile.DefaultToSpray();
		this.Projectile.aiStyle = 0;
	}

	public override void AI() {
		if (this.Projectile.owner == Main.myPlayer) {
			this.Convert((int)(this.Projectile.position.X + (this.Projectile.width * 0.5f)) / 16, (int)(this.Projectile.position.Y + (this.Projectile.height * 0.5f)) / 16, 2);
		}

		if (this.Projectile.timeLeft > 133) {
			this.Projectile.timeLeft = 133;
		}

		if (this.Progress > 7f) {
			float dustScale = 1f;

			if (this.Progress == 8f) {
				dustScale = 0.2f;
			} else if (this.Progress == 9f) {
				dustScale = 0.4f;
			} else if (this.Progress == 10f) {
				dustScale = 0.6f;
			} else if (this.Progress == 11f) {
				dustScale = 0.8f;
			}

			this.Progress += 1f;

			Dust dust = Dust.NewDustDirect(new Vector2(this.Projectile.position.X, this.Projectile.position.Y), this.Projectile.width, this.Projectile.height, this.SprayDustID, this.Projectile.velocity.X * 0.2f, this.Projectile.velocity.Y * 0.2f, 100);

			dust.noGravity = true;
			dust.scale *= 1.75f;
			dust.velocity.X *= 2f;
			dust.velocity.Y *= 2f;
			dust.scale *= dustScale;
		} else {
			this.Progress += 1f;
		}

		this.Projectile.rotation += 0.3f * this.Projectile.direction;
	}

	private void Convert(int i, int j, int size = 4) {
		for (int k = i - size; k <= i + size; k++) {
			for (int l = j - size; l <= j + size; l++) {
				if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt((size * size) + (size * size))) {
					this.ConvertTile(k, l);
				}
			}
		}
	}

	/// <summary>
	/// Override if you have custom conversion logic.
	/// Otherwise, converts tiles and walls based on GetResultTile() and GetResultWall().
	/// </summary>
	protected virtual void ConvertTile(int x, int y) {
		this.TryConvertWallLayer(x, y);
		this.TryConvertTileLayer(x, y);
	}

	private void TryConvertTileLayer(int x, int y) {
		Tile tile = Framing.GetTileSafely(x, y);
		if (!tile.HasTile) {return;}
		TileConversion? tileConversion = this.GetResultTile(tile.TileType, y);
		if (!tileConversion.HasValue) {return;}
		if (tileConversion.Value.NewTileID.HasValue) {
			if (tile.TileType == tileConversion.Value.NewTileID.Value) {return;}
			WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(x, y, tileConversion.Value.NewTileID.Value);
			tile.TileType = tileConversion.Value.NewTileID.Value;
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
		} else { // kill tile
			WorldGen.KillTile(x, y);
			if (Main.netMode == NetmodeID.MultiplayerClient) {
				NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
			}
		}
	}

	private void TryConvertWallLayer(int x, int y) {
		Tile tile = Framing.GetTileSafely(x, y);
		if (tile.WallType == WallID.None) {return;}
		WallConversion? wallConversion = this.GetResultWall(tile.WallType, y);
		if (!wallConversion.HasValue) {return;}
		if (tile.WallType == wallConversion.Value.NewWallID) {return;}
		tile.WallType = wallConversion.Value.NewWallID;
		WorldGen.SquareWallFrame(x, y);
		NetMessage.SendTileSquare(-1, x, y, 1);
	}

	protected virtual TileConversion? GetResultTile(ushort origTileId, int y) {return null;}
	protected virtual WallConversion? GetResultWall(ushort origWallId, int y) {return null;}

	protected readonly record struct TileConversion(ushort? NewTileID) {
		public static TileConversion Kill => new(NewTileID: null);
	}

	protected readonly record struct WallConversion(ushort NewWallID) {
		public static WallConversion Kill => new(WallID.None);
	}
}
