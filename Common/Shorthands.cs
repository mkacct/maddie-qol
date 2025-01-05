using Terraria.ModLoader;

namespace MaddieQoL.Common;

public sealed class Shorthands {
	private Shorthands() {} // prevent instantiation

	public const string LangMisc = "Misc";

	public static Mod ThisMod => ModContent.GetInstance<MaddieQoL>();
	public static ModuleConfig ModuleConf => ModContent.GetInstance<ModuleConfig>();
}
