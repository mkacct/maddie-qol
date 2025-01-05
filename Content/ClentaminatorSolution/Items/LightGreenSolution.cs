using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using Terraria.Localization;

namespace MaddieQoL.Content.ClentaminatorSolution.Items;

public class LightGreenSolution : AbstractSolutionItem {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enablePurificationOnlySolution ? TooltipWhenEnabled : base.Tooltip;

	protected override int SprayProjectileID => ModContent.ProjectileType<LightGreenSolutionProjectile>();

	protected override bool IsItemTypeUsableAsAmmo => ModuleConf.enablePurificationOnlySolution;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));

		this.AddShimmers();
	}

	private void AddShimmers() {
		if (ModuleConf.enablePurificationOnlySolution) {
			ItemID.Sets.ShimmerTransformToItem[ItemID.GreenSolution] = this.Type;
		}
		ItemID.Sets.ShimmerTransformToItem[this.Type] = ItemID.GreenSolution;
	}
}

public class LightGreenSolutionProjectile : AbstractSolutionProjectile {
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.PureSpray}";

	protected override int SprayDustID => DustID.PureSpray;

	// this should replicate the behavior of the vanilla green solution, just without the mushroom grass conversion

	protected override TileConversion? GetResultTile(ushort tileId, int y) {
		TileConversion? res = null;
		if (tileId == TileID.GolfGrassHallowed) {
			res = new(TileID.GolfGrass);
		} else if (TileID.Sets.Conversion.JungleGrass[tileId]) {
			res = new(TileID.JungleGrass);
		} else if (TileID.Sets.Conversion.Grass[tileId] && (tileId != TileID.GolfGrass)) {
			res = new(TileID.Grass);
		} else if (TileID.Sets.Conversion.Stone[tileId]) {
			res = new(TileID.Stone);
		} else if (TileID.Sets.Conversion.Sand[tileId]) {
			res = new(TileID.Sand);
		} else if (TileID.Sets.Conversion.HardenedSand[tileId]) {
			res = new(TileID.HardenedSand);
		} else if (TileID.Sets.Conversion.Sandstone[tileId]) {
			res = new(TileID.Sandstone);
		} else if (TileID.Sets.Conversion.Ice[tileId]) {
			res = new(TileID.IceBlock);
		} else if (TileID.Sets.Conversion.MushroomGrass[tileId]) {
			// no change to mushrooms!
		} else if ((tileId == TileID.CorruptThorns) || (tileId == TileID.CrimsonThorns)) {
			res = TileConversion.Kill;
		}
		return res;
	}

	protected override WallConversion? GetResultWall(ushort wallId, int y) {
		WallConversion? res = null;
		switch (wallId) {
			case WallID.CorruptGrassUnsafe:
			case WallID.HallowedGrassUnsafe:
			case WallID.CrimsonGrassUnsafe:
				res = new(Main.rand.NextBool(10) ? WallID.FlowerUnsafe : WallID.GrassUnsafe);
				break;
		}
		if (WallID.Sets.Conversion.Stone[wallId]) {
			switch (wallId) {
				case WallID.Cave7Echo:
					res = new(WallID.Cave7Unsafe);
					break;
				case WallID.Cave8Echo:
					res = new(WallID.Cave8Unsafe);
					break;
				case WallID.Cave7Unsafe:
				case WallID.Cave8Unsafe:
					break;
				default:
					res = new(WallID.Stone);
					break;
			};
		}
		if (WallID.Sets.Conversion.NewWall1[wallId]) {
			res = new(WallID.RocksUnsafe1);
		} else if (WallID.Sets.Conversion.NewWall2[wallId]) {
			res = new(WallID.RocksUnsafe2);
		} else if (WallID.Sets.Conversion.NewWall3[wallId]) {
			res = new(WallID.RocksUnsafe3);
		} else if (WallID.Sets.Conversion.NewWall4[wallId]) {
			res = new(WallID.RocksUnsafe4);
		} else if (wallId == WallID.MushroomUnsafe) {
			// no change to mushrooms!
		} else if (WallID.Sets.Conversion.HardenedSand[wallId]) {
			res = new(WallID.HardenedSand);
		} else if (WallID.Sets.Conversion.Sandstone[wallId]) {
			res = new(WallID.Sandstone);
		}
		return res;
	}
}
