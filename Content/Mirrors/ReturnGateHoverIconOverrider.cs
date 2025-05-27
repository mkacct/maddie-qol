using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors;

public sealed class MirrorReturnGateHoverIconOverrider : ModSystem {
	int gateIcon = -1;

	internal static void SetGateIcon(int newGateIconId) {
		ModContent.GetInstance<MirrorReturnGateHoverIconOverrider>().gateIcon = newGateIconId;
	}

	internal static void ResetGateIcon() {
		ModContent.GetInstance<MirrorReturnGateHoverIconOverrider>().gateIcon = -1;
	}

	public override void Load() {
		On_PotionOfReturnGateInteractionChecker.DoHoverEffect += (On_PotionOfReturnGateInteractionChecker.orig_DoHoverEffect orig, PotionOfReturnGateInteractionChecker self, Player player, Rectangle hitbox) => {
			bool overridden = this.OverrideGateHoverIcon(player);
			if (!overridden) {orig(self, player, hitbox);}
		};
	}

	bool OverrideGateHoverIcon(Player player) {
		if (this.gateIcon >= 0) {
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = this.gateIcon;
			return true;
		}
		return false;
	}
}
