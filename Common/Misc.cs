using Terraria.ModLoader;

namespace MaddieQoL.Common;

public sealed class Misc {
	private Misc() {} // prevent instantiation

	public const string LangMisc = "Misc";

	public static Mod ThisMod() {return ModContent.GetInstance<MaddieQoL>();}
}
