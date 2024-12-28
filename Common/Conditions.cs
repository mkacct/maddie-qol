using static MaddieQoL.Common.Shorthands;
using Terraria;

namespace MaddieQoL.Common;

public sealed class Conditions {
	private Conditions() {} // prevent instantiation

	public static Condition PlayerHasTorchGodsFavor => new(
		ThisMod().GetLocalization($"{nameof(Conditions)}.{nameof(PlayerHasTorchGodsFavor)}"),
		() => {return Main.LocalPlayer.unlockedBiomeTorches;}
	);

	public static Condition PlayerHasPickaxePower(int pick) {
		return new Condition(
			ThisMod().GetLocalization($"{nameof(Conditions)}.{nameof(PlayerHasPickaxePower)}").WithFormatArgs(pick),
			() => {return Main.LocalPlayer.GetBestPickaxe().pick >= pick;}
		);
	}
}
