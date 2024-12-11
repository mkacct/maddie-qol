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
	internal static readonly int ShellphonePlusDummyItemID = ModContent.ItemType<ShellphonePlusDummy>();

	private static readonly int[] ShellphonePlusItemIDSequence = [
		ModContent.ItemType<ShellphonePlusReturn>(),
		ModContent.ItemType<ShellphonePlusHome>(),
		ModContent.ItemType<ShellphonePlusOcean>(),
		ModContent.ItemType<ShellphonePlusHell>(),
		ModContent.ItemType<ShellphonePlusSpawn>()
	];

	internal static readonly ISet<int> ShellphonePlusItemIDs;

	static MirrorShellphonePlusSystem() {
		ShellphonePlusItemIDs = new HashSet<int> {ShellphonePlusDummyItemID};
		ShellphonePlusItemIDs.UnionWith(ShellphonePlusItemIDSequence);
	}

	internal static int ShellphonePlusNextItemID(int itemId) {
		int index = Array.IndexOf(ShellphonePlusItemIDSequence, itemId);
		index = (index + 1) % ShellphonePlusItemIDSequence.Length;
		if (!ModuleConfig().enableReturnTools && (ShellphonePlusItemIDSequence[index] == ModContent.ItemType<ShellphonePlusReturn>())) {
			index = (index + 1) % ShellphonePlusItemIDSequence.Length;
		}
		return ShellphonePlusItemIDSequence[index];
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
		AddItemResearchOverride.Invoke(null, [ShellphonePlusDummyItemID, ShellphonePlusItemIDSequence]);
	}

	private static void TryItemSwap(Item item) {
		if (ShellphonePlusItemIDs.Contains(item.type)) {
			item.ChangeItemType(ShellphonePlusNextItemID(item.type));
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
