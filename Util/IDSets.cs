using System.Collections.Generic;
using Terraria.ID;

namespace MaddieQoL.Util;

public sealed class IDSets {
	private IDSets() {} // prevent instantiation

	public static readonly ISet<int> ShellphoneItemIDs = new HashSet<int> {
		ItemID.ShellphoneDummy,
		ItemID.Shellphone,
		ItemID.ShellphoneSpawn,
		ItemID.ShellphoneOcean,
		ItemID.ShellphoneHell
	};

	public static readonly int[] BeamTileIDArr = [
			TileID.WoodenBeam,
			TileID.MarbleColumn,
			TileID.BorealBeam,
			TileID.RichMahoganyBeam,
			TileID.GraniteColumn,
			TileID.SandstoneColumn,
			TileID.MushroomBeam
		];
}
