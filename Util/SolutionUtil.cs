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
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) {
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Solutions;
	}
}

public abstract class AbstractSolutionProjectile : ModProjectile {
	protected abstract ModBiomeConversion Conversion {get;}
	protected abstract int SprayDustID {get;}

	int _conversionType;

	ref float Progress => ref Projectile.ai[0];
	bool ShotFromTerraformer => Projectile.ai[1] == 1f;

	public override void SetDefaults() {
		this._conversionType = this.Conversion.Type;

		this.Projectile.DefaultToSpray();
		this.Projectile.aiStyle = 0;
	}

	public override bool? CanDamage() {return false;}

	public override void AI() {
		if (this.Projectile.timeLeft > 133) {
			this.Projectile.timeLeft = 133;
		}

		if (this.Projectile.owner == Main.myPlayer) {
			int size = this.ShotFromTerraformer ? 3 : 2;
			Point tileCenter = this.Projectile.Center.ToTileCoordinates();
			WorldGen.Convert(tileCenter.X, tileCenter.Y, this._conversionType, size);
		}

		int spawnDustThreshold = this.ShotFromTerraformer ? 3 : 7;

		if (this.Progress > spawnDustThreshold) {
			float dustScale = 1f;

			if (this.Progress == spawnDustThreshold + 1) {
				dustScale = 0.2f;
			} else if (this.Progress == spawnDustThreshold + 2) {
				dustScale = 0.4f;
			} else if (this.Progress == spawnDustThreshold + 3) {
				dustScale = 0.6f;
			} else if (this.Progress == spawnDustThreshold + 4) {
				dustScale = 0.8f;
			}

			int dustArea = 0;
			if (this.ShotFromTerraformer) {
				dustScale *= 1.2f;
				dustArea = (int)(12f * dustScale);
			}

			Dust dust = Dust.NewDustDirect(
				new Vector2(this.Projectile.position.X - dustArea, this.Projectile.position.Y - dustArea),
				this.Projectile.width + (dustArea * 2),
				this.Projectile.height + (dustArea * 2),
				this.SprayDustID,
				this.Projectile.velocity.X * 0.4f,
				this.Projectile.velocity.Y * 0.4f,
				100
			);
			dust.noGravity = true;
			dust.scale *= 1.75f * dustScale;
		}

		this.Progress++;
		this.Projectile.rotation += 0.3f * this.Projectile.direction;
	}
}

public static class BiomeConversionExtensions {
	public static void RegisterTileConversion(this ModBiomeConversion modBiomeConversion, int tileId, TileLoader.ConvertTile conversion) {
		TileLoader.RegisterConversion(tileId, modBiomeConversion.Type, conversion);
	}

	public static void RegisterWallConversion(this ModBiomeConversion modBiomeConversion, int wallId, WallLoader.ConvertWall conversion) {
		WallLoader.RegisterConversion(wallId, modBiomeConversion.Type, conversion);
	}
}

public static class BiomeConversionUtil {
	public static readonly TileLoader.ConvertTile KillTileConversion = (i, j, _type, _conversionType) => {
		WorldGen.KillTile(i, j);
		if (Main.netMode == NetmodeID.MultiplayerClient) {
			NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
		}
		return false;
	};

	public static readonly WallLoader.ConvertWall KillWallConversion = IndiscriminateWallConversion(WallID.None);

	public static TileLoader.ConvertTile IndiscriminateTileConversion(int tileId) {
		return (i, j, _type, _conversionType) => {
			WorldGen.ConvertTile(i, j, tileId, true);
			return false;
		};
	}

	public static WallLoader.ConvertWall IndiscriminateWallConversion(int wallId) {
		return (i, j, _type, _conversionType) => {
			WorldGen.ConvertWall(i, j, wallId);
			return false;
		};
	}
}
