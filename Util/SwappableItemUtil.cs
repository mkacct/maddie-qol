using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace MaddieQoL.Util;

public abstract class AbstractSwappableItem : ModItem {
	protected virtual bool CanSwapByAltFunction => true;
	protected virtual SoundStyle AltFunctionSwapSound => SoundID.Grab;

	public override bool AltFunctionUse(Player player) {
		if (CanSwapByAltFunction) {return true;}
		return base.AltFunctionUse(player);
	}

	public override bool CanUseItem(Player player) {
		if (CanSwapByAltFunction && (player.altFunctionUse == 2)) {
			HandleAltFunction(player);
			return false;
		}
		return base.CanUseItem(player);
	}

	void HandleAltFunction(Player player) {
		player.releaseUseTile = false;
		Main.mouseRightRelease = false;
		SoundEngine.PlaySound(this.AltFunctionSwapSound with {
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		});
		this.Item.ChangeItemType(this.NextItemID(this.Type));
		Recipe.FindRecipes();
	}

	protected virtual int NextItemID(int itemId) {
		throw new NotImplementedException();
	}

	protected void HandleDummyItemCreation(ItemCreationContext context) {
		if (context is InitializationItemCreationContext) {return;}
		this.Item.ChangeItemType(this.NextItemID(-1));
	}
}

public static class SwappableItemUtil {
	public static void RegisterItemResearchOverrideHook(int[] itemIdSequence, int? dummyItemId = null) {
		int defaultItemId = dummyItemId ?? itemIdSequence[0];
		int[] otherItemIds = dummyItemId.HasValue ? itemIdSequence : itemIdSequence[1..];
		On_ContentSamples.FillResearchItemOverrides += (On_ContentSamples.orig_FillResearchItemOverrides orig) => {
			orig();
			ContentSamples.AddItemResearchOverride(defaultItemId, otherItemIds);
		};
	}

	public static void RegisterItemSwapHook(ISet<int> itemIds, Func<int, int> nextItemIdFunc, SoundStyle? swapSound = null) {
		SoundStyle swapSoundToUse = swapSound ?? SoundID.Grab;
		On_ItemSlot.TryItemSwap += (On_ItemSlot.orig_TryItemSwap orig, Item item) => {
			orig(item);
			TryItemSwap(item, itemIds, nextItemIdFunc, swapSoundToUse);
		};
	}

	static void TryItemSwap(Item item, ISet<int> itemIds, Func<int, int> nextItemIdFunc, SoundStyle swapSound) {
		if (itemIds.Contains(item.type)) {
			item.ChangeItemType(nextItemIdFunc(item.type));
			AfterItemSwap(swapSound);
		}
	}

	// This method replicates the behavior of ItemSlot.AfterItemSwap()
	// The behavior should be identical (besides the specific sound effect)
	static void AfterItemSwap(SoundStyle swapSound) {
		SoundEngine.PlaySound(swapSound with {
			SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		});
		Main.stackSplit = 30;
		Main.mouseRightRelease = false;
		Recipe.FindRecipes();
	}

	public static int NextID(int[] idSequence, int id) {
		int index = Array.IndexOf(idSequence, id);
		index = (index + 1) % idSequence.Length;
		return idSequence[index];
	}
}
