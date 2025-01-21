using static MaddieQoL.Common.Shorthands;
using Terraria;

namespace MaddieQoL.Common;

public static class Conditions {
	public static Condition InTown => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(InTown)}"),
		() => Main.LocalPlayer.townNPCs >= 3f
	);

	public static Condition BiomeTorchSwapEnabled => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(BiomeTorchSwapEnabled)}"),
		() => Main.LocalPlayer.UsingBiomeTorches
	);

	public static Condition PlayerHasPickaxePower(int pick) {
		return new Condition(
			ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(PlayerHasPickaxePower)}").WithFormatArgs(pick),
			() => Main.LocalPlayer.GetBestPickaxe().pick >= pick
		);
	}
}
