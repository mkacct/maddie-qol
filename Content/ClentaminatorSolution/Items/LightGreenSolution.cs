using static MaddieQoL.Common.Shorthands;
using static MaddieQoL.Util.BiomeConversionUtil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using Terraria.Localization;

namespace MaddieQoL.Content.ClentaminatorSolution.Items;

public sealed class LightGreenSolution : AbstractSolutionItem {
	static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enablePurificationOnlySolution ? TooltipWhenEnabled : base.Tooltip;

	protected override int SprayProjectileID => ModContent.ProjectileType<LightGreenSolutionProjectile>();

	protected override bool IsItemTypeUsableAsAmmo => ModuleConf.enablePurificationOnlySolution;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));

		ItemID.Sets.SortingPriorityTerraforming[this.Type] = 98;
		this.AddShimmers();
	}

	void AddShimmers() {
		if (ModuleConf.enablePurificationOnlySolution) {
			ItemID.Sets.ShimmerTransformToItem[ItemID.GreenSolution] = this.Type;
		}
		ItemID.Sets.ShimmerTransformToItem[this.Type] = ItemID.GreenSolution;
	}
}

public sealed class LightGreenSolutionProjectile : AbstractSolutionProjectile {
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.PureSpray}";

	protected override ModBiomeConversion Conversion => ModContent.GetInstance<LightGreenSolutionConversion>();
	protected override int SprayDustID => DustID.PureSpray;
}

// this should replicate the behavior of the vanilla green solution, just without the mushroom grass conversion
public sealed class LightGreenSolutionConversion : ModBiomeConversion {
	public override void PostSetupContent() {
		for (int tileId = 0; tileId < TileLoader.TileCount; tileId++) {
			this.TryRegisterTileConversion(tileId);
		}
		for (int wallId = 0; wallId < WallLoader.WallCount; wallId++) {
			this.TryRegisterWallConversion(wallId);
		}
	}

	void TryRegisterTileConversion(int tileId) {
		if (tileId == TileID.GolfGrassHallowed) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.GolfGrass));
		} else if (TileID.Sets.Conversion.JungleGrass[tileId]) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.JungleGrass));
		} else if (TileID.Sets.Conversion.Grass[tileId] && (tileId != TileID.GolfGrass)) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.Grass));
		} else if (TileID.Sets.Conversion.Stone[tileId]) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.Stone));
		} else if (TileID.Sets.Conversion.Sand[tileId]) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.Sand));
		} else if (TileID.Sets.Conversion.HardenedSand[tileId]) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.HardenedSand));
		} else if (TileID.Sets.Conversion.Sandstone[tileId]) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.Sandstone));
		} else if (TileID.Sets.Conversion.Ice[tileId]) {
			this.RegisterTileConversion(tileId, IndiscriminateTileConversion(TileID.IceBlock));
		} else if (TileID.Sets.Conversion.MushroomGrass[tileId]) {
			// no change to mushrooms!
		} else if ((tileId == TileID.CorruptThorns) || (tileId == TileID.CrimsonThorns)) {
			this.RegisterTileConversion(tileId, KillTileConversion);
		}
	}

	void TryRegisterWallConversion(int wallId) {
		switch (wallId) {
			case WallID.CorruptGrassUnsafe:
			case WallID.HallowedGrassUnsafe:
			case WallID.CrimsonGrassUnsafe:
				this.RegisterWallConversion(wallId, (i, j, type, conversionType) => {
					WorldGen.ConvertWall(i, j, Main.rand.NextBool(10) ? WallID.FlowerUnsafe : WallID.GrassUnsafe);
					return false;
				});
				return;
		}
		if (WallID.Sets.Conversion.Stone[wallId]) {
			switch (wallId) {
				case WallID.Cave7Echo:
					this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.Cave7Unsafe));
					return;
				case WallID.Cave8Echo:
					this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.Cave8Unsafe));
					return;
				case WallID.Cave7Unsafe:
				case WallID.Cave8Unsafe:
					break;
				default:
					this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.Stone));
					return;
			}
		}
		if (WallID.Sets.Conversion.NewWall1[wallId]) {
			this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.RocksUnsafe1));
		} else if (WallID.Sets.Conversion.NewWall2[wallId]) {
			this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.RocksUnsafe2));
		} else if (WallID.Sets.Conversion.NewWall3[wallId]) {
			this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.RocksUnsafe3));
		} else if (WallID.Sets.Conversion.NewWall4[wallId]) {
			this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.RocksUnsafe4));
		} else if (wallId == WallID.MushroomUnsafe) {
			// no change to mushrooms!
		} else if (WallID.Sets.Conversion.HardenedSand[wallId]) {
			this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.HardenedSand));
		} else if (WallID.Sets.Conversion.Sandstone[wallId]) {
			this.RegisterWallConversion(wallId, IndiscriminateWallConversion(WallID.Sandstone));
		}
	}
}
