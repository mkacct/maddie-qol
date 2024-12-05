using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors;

public class MirrorReturnPotionUseDetector : GlobalItem {
	public override bool? UseItem(Item item, Player player) {
		if ((player == Main.LocalPlayer) && (item.type == ItemID.PotionOfReturn)) {
			MirrorReturnGateHoverIconOverrider.ResetGateIcon();
		}
		return null;
	}
}
