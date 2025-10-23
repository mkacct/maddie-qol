using Terraria;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Common;

public static class Conditions {

	public static Condition InTown => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(InTown)}"),
		() => Main.LocalPlayer.townNPCs >= 3f
	);

	public static Condition TorchDeswapAllowed => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(TorchDeswapAllowed)}"),
		() => {
			if (!ModuleConf.enableTorchDeswap) {return false;}
			if (!Main.LocalPlayer.unlockedBiomeTorches) {return false;}
			return UserConf.showTorchDeswapRecipes switch {
				TorchDeswapDisplayMode.Never => false,
				TorchDeswapDisplayMode.WhenBiomeTorchSwapEnabled => Main.LocalPlayer.UsingBiomeTorches,
				TorchDeswapDisplayMode.Always => true,
				_ => false,// should never happen
			};
		}
	);

	public static Condition PlayerHasPickaxePower(int pick) => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(PlayerHasPickaxePower)}").WithFormatArgs(pick),
		() => Main.LocalPlayer.GetBestPickaxe().pick >= pick
	);

}
