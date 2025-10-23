using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using MaddieQoL.Util;
using static MaddieQoL.Common.Shorthands;
using MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

namespace MaddieQoL.Content.Mirrors;

public sealed class MirrorShellphonePlusSystem : ModSystem {

	internal static readonly int ShellphonePlusDummyItemID = ModContent.ItemType<ShellphonePlusDummy>();

	static readonly int[] ShellphonePlusItemIDSequence = [
		ModContent.ItemType<ShellphonePlusReturn>(),
		ModContent.ItemType<ShellphonePlusHome>(),
		ModContent.ItemType<ShellphonePlusOcean>(),
		ModContent.ItemType<ShellphonePlusHell>(),
		ModContent.ItemType<ShellphonePlusSpawn>()
	];

	internal static readonly ISet<int> ShellphonePlusItemIDs;

	internal static readonly SoundStyle ShellphonePlusSwapSound = SoundID.Unlock;

	static MirrorShellphonePlusSystem() {
		ShellphonePlusItemIDs = new HashSet<int> {ShellphonePlusDummyItemID};
		ShellphonePlusItemIDs.UnionWith(ShellphonePlusItemIDSequence);
	}

	public override void Load() {
		SwappableItemUtil.RegisterItemResearchOverrideHook(ShellphonePlusItemIDSequence, ShellphonePlusDummyItemID);
		SwappableItemUtil.RegisterItemSwapHook(
			ShellphonePlusItemIDs, ShellphonePlusNextItemID, ShellphonePlusSwapSound
		);
	}

	internal static int ShellphonePlusNextItemID(int itemId) {
		do {
			itemId = SwappableItemUtil.NextID(ShellphonePlusItemIDSequence, itemId);
		} while (!ModuleConf.enableReturnTools && (itemId == ModContent.ItemType<ShellphonePlusReturn>()));
		return itemId;
	}

}
