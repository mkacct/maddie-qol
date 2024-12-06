using System.Collections.Generic;
using Terraria.ID;

namespace MaddieQoL.Common;

public sealed class IDSets {
	private IDSets() {} // prevent instantiation

	public static readonly ISet<int> ShellphoneItemIDs = new HashSet<int> {
		ItemID.ShellphoneDummy,
		ItemID.Shellphone,
		ItemID.ShellphoneSpawn,
		ItemID.ShellphoneOcean,
		ItemID.ShellphoneHell
	};
}
