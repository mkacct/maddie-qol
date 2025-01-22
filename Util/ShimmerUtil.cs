using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace MaddieQoL.Util;

public static class ShimmerUtil {
	public static void RegisterCustomShimmer(ShimmerAction shimmerAction, Predicate<int> predicate) {
		On_Item.CanShimmer += (On_Item.orig_CanShimmer orig, Item self) => {
			if (CheckShimmerPredicate(self, predicate)) {return true;}
			return orig(self);
		};
		On_Item.GetShimmered += (On_Item.orig_GetShimmered orig, Item self) => {
			if (CheckShimmerPredicate(self, predicate)) {
				shimmerAction.Invoke(self);
				self.PostShimmer();
			} else {
				orig(self);
			}
		};
	}

	public static void RegisterCustomShimmer(ShimmerAction shimmerAction, params int[] itemId) {
		RegisterCustomShimmer(shimmerAction, (int shimmerEquivalentType) => {
			foreach (int id in itemId) {
				if (shimmerEquivalentType == id) {return true;}
			}
			return false;
		});
	}

	private static bool CheckShimmerPredicate(Item item, Predicate<int> predicate) {
		int shimmerEquivalentType = item.GetShimmerEquivalentType();
		return predicate(shimmerEquivalentType);
	}

	private static int GetShimmerEquivalentType(this Item item) {
		MethodInfo method_GetShimmerEquivalentType = typeof(Item).GetMethod("GetShimmerEquivalentType", BindingFlags.NonPublic | BindingFlags.Instance, []);
		return (int)method_GetShimmerEquivalentType.Invoke(item, null);
	}

	// This method replicates the item-independent behavior of Item.GetShimmered()
	// The behavior should be identical to the vanilla code after the if/else chain
	private static void PostShimmer(this Item item) {
		if (item.stack > 0) {
			item.shimmerTime = 1f;
		} else {
			item.shimmerTime = 0f;
		}

		item.shimmerWet = true;
		item.wet = true;
		item.velocity *= 0.1f;
		if (Main.netMode == NetmodeID.SinglePlayer) {
			Item.ShimmerEffect(item.Center);
		} else {
			NetMessage.SendData(MessageID.ShimmerActions, -1, -1, null, 0, (int)item.Center.X, (int)item.Center.Y);
			NetMessage.SendData(MessageID.SyncItemsWithShimmer, -1, -1, null, item.whoAmI, 1f);
		}

		AchievementsHelper.NotifyProgressionEvent(27);
		if (item.stack == 0) {
			item.makeNPC = -1;
			item.active = false;
		}
	}

	public sealed class ShimmerAction {
		private readonly Action<Item, int> action;

		public ShimmerAction(Action<Item, int> action) {
			this.action = action;
		}

		public ShimmerAction(Action<Item> action) {
			this.action = (Item item, int shimmerEquivalentType) => {action(item);};
		}

		public void Invoke(Item item) {
			int shimmerEquivalentType = item.GetShimmerEquivalentType();
			this.action(item, shimmerEquivalentType);
		}

		public static ShimmerAction Transform(int toItemId) {
			return new((Item item, int shimmerEquivalentType) => {
				int temp = item.stack;
				item.SetDefaults(toItemId);
				item.stack = temp;
				item.shimmered = true;
			});
		}
	}
}
