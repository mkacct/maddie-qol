using static MaddieQoL.Common.Shorthands;
using System;
using System.Collections.Generic;
using System.Reflection;
using MaddieQoL.Content.Mirrors.Items.ShellphonePlus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace MaddieQoL.Content.Mirrors;

public class MirrorShellphonePlusSystem : ModSystem {
	internal static readonly int ShellphonePlusDummyType = ModContent.ItemType<ShellphonePlusDummy>();

	private static readonly int[] ShellphonePlusTypeSequence = [
		ModContent.ItemType<ShellphonePlusReturn>(),
		ModContent.ItemType<ShellphonePlusHome>(),
		ModContent.ItemType<ShellphonePlusOcean>(),
		ModContent.ItemType<ShellphonePlusHell>(),
		ModContent.ItemType<ShellphonePlusSpawn>()
	];

	internal static readonly ISet<int> ShellphonePlusItemIDs;

	static MirrorShellphonePlusSystem() {
		ShellphonePlusItemIDs = new HashSet<int> {ShellphonePlusDummyType};
		ShellphonePlusItemIDs.UnionWith(ShellphonePlusTypeSequence);
	}

	internal static int ShellphonePlusNextType(int type) {
		int index = Array.IndexOf(ShellphonePlusTypeSequence, type);
		index = (index + 1) % ShellphonePlusTypeSequence.Length;
		if (!ModuleConfig().enableReturnTools && (ShellphonePlusTypeSequence[index] == ModContent.ItemType<ShellphonePlusReturn>())) {
			index = (index + 1) % ShellphonePlusTypeSequence.Length;
		}
		return ShellphonePlusTypeSequence[index];
	}

	public override void Load() {
		On_ContentSamples.FillResearchItemOverrides += (On_ContentSamples.orig_FillResearchItemOverrides orig) => {
			orig();
			AddItemResearchOverride();
		};
		On_ItemSlot.TryItemSwap += (On_ItemSlot.orig_TryItemSwap orig, Item item) => {
			orig(item);
			TryItemSwap(item);
		};
	}

	private static void AddItemResearchOverride() {
		// Using reflection since ContentSamples.AddItemResearchOverride() is private for some reason
		MethodInfo AddItemResearchOverride = typeof(ContentSamples).GetMethod("AddItemResearchOverride", BindingFlags.NonPublic | BindingFlags.Static);
		AddItemResearchOverride.Invoke(null, [ShellphonePlusDummyType, ShellphonePlusTypeSequence]);
	}

	private static void TryItemSwap(Item item) {
		if (ShellphonePlusItemIDs.Contains(item.type)) {
			item.ChangeItemType(ShellphonePlusNextType(item.type));
			AfterItemSwap();
		}
	}

	// This method replicates the behavior of ItemSlot.AfterItemSwap() for the specific case of the ShellphonePlus
	// The behavior should be identical, besides the specific sound effect
	private static void AfterItemSwap() {
		SoundEngine.PlaySound(SoundID.Unlock);

		Main.stackSplit = 30;
		Main.mouseRightRelease = false;
		Recipe.FindRecipes();
	}
}
