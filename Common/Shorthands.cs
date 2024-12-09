using Terraria.ModLoader;

namespace MaddieQoL.Common;

public sealed class Shorthands {
	private Shorthands() {} // prevent instantiation

	public static Mod ThisMod() {return ModContent.GetInstance<MaddieQoL>();}
	public static ModuleConfig ModuleConfig() {return ModContent.GetInstance<ModuleConfig>();}
}
