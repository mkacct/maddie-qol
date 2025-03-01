using Terraria.ModLoader;

namespace MaddieQoL.Common;

public static class Shorthands {
	public const string LangMisc = "Misc";

	public static Mod ThisMod => ModContent.GetInstance<MaddieQoL>();
	public static ModuleConfig ModuleConf => ModContent.GetInstance<ModuleConfig>();
	public static UserPrefsConfig UserConf => ModContent.GetInstance<UserPrefsConfig>();
}
