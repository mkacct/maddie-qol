using Terraria;

namespace MaddieQoL.Util;

public static class ShopUtil {

	public static bool TryAddItemToActiveShop(Item[] items, Item newItem) {
		for (int i = 0; i < items.Length; i++) {
			if (items[i] == null) {
				items[i] = newItem;
				return true;
			}
		}
		return false;
	}

}
