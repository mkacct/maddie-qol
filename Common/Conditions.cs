using static MaddieQoL.Common.Shorthands;
using Terraria;

namespace MaddieQoL.Common;

public static class Conditions {
	public static Condition InTown => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(InTown)}"),
		() => {return Main.LocalPlayer.townNPCs >= 3f;}
	);

	public static Condition BiomeTorchSwapEnabled => new(
		ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(BiomeTorchSwapEnabled)}"),
		() => {return Main.LocalPlayer.UsingBiomeTorches;}
	);

	public static Condition AtLeastXTownNPCsPresent(int x) {
		return new Condition(
			ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(AtLeastXTownNPCsPresent)}").WithFormatArgs(x),
			() => {
				int count = 0;
				foreach (NPC npc in Main.npc) {
					if ((npc != null) && npc.active && npc.townNPC) {count++;}
				}
				return count >= x;
			}
		);
	}

	public static Condition PlayerHasPickaxePower(int pick) {
		return new Condition(
			ThisMod.GetLocalization($"{nameof(Conditions)}.{nameof(PlayerHasPickaxePower)}").WithFormatArgs(pick),
			() => {return Main.LocalPlayer.GetBestPickaxe().pick >= pick;}
		);
	}
}
