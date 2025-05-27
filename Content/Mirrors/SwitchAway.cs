using static MaddieQoL.Common.Shorthands;
using static MaddieQoL.Content.Mirrors.MirrorShellphonePlusSystem;
using System.Collections.Generic;
using MaddieQoL.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors;

public sealed class MirrorSwitchAway : GlobalItem {
	static readonly ISet<int> AllTeleportOnUseItems;

	static MirrorSwitchAway() {
		AllTeleportOnUseItems = new HashSet<int> {
			ItemID.RecallPotion,
			ItemID.TeleportationPotion,
			ItemID.PotionOfReturn,
			ItemID.MagicMirror,
			ItemID.IceMirror,
			ModContent.ItemType<Items.ReturnMirror>(),
			ItemID.CellPhone,
			ModContent.ItemType<Items.CellPhonePlus>(),
			ItemID.MagicConch,
			ItemID.DemonConch
		};
		AllTeleportOnUseItems.UnionWith(IDSets.ShellphoneItemIDs);
		AllTeleportOnUseItems.UnionWith(ShellphonePlusItemIDs);
	}

	public override void UseStyle(Item item, Player player, Rectangle heldItemFrame) {
		if (!UserConf.enableRecallItemSwitchAway) {return;}
		if (AllTeleportOnUseItems.Contains(item.type) && (player.itemTime == player.itemTimeMax / 2)) {
			if ((player.selectedItem > 0) && (player.selectedItem <= 9)) {
				player.HotbarOffset = -player.selectedItem;
			}
		}
	}
}
