using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Mirrors;

public class MirrorShellphoneHomeOverrider : GlobalItem {
	private static LocalizedText ReturnPortalTooltipLine {get; set;}

	public override void SetStaticDefaults(){
		ReturnPortalTooltipLine = Mod.GetLocalization($"Misc.{nameof(ReturnPortalTooltipLine)}");
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		if (!ModContent.GetInstance<ModuleConfig>().enableReturnTools) {return;}
		if (item.type == ItemID.Shellphone) {
			tooltips.Add(new TooltipLine(Mod, "ReturnPortalInfo", ReturnPortalTooltipLine.Value));
		}
	}

	public override void Load() {
		if (!ModContent.GetInstance<ModuleConfig>().enableReturnTools) {return;}
		On_Player.Spawn += (On_Player.orig_Spawn orig, Player self, PlayerSpawnContext context) => {
			Item selectedItem = self.inventory[self.selectedItem];
			if ((context == PlayerSpawnContext.RecallFromItem) && (selectedItem.type == ItemID.Shellphone)) {
				if (self == Main.LocalPlayer) {
					MirrorReturnGateHoverIconOverrider.SetGateIcon(ItemID.ShellphoneDummy);
				}
				DoPotionOfReturnTeleportationAndSetTheComebackPointInPlaceOfSpawnMethod(orig, self);
				return;
			}
			orig(self, context);
		};
	}

	// this method must exactly replicate the behavior of Player.DoPotionOfReturnTeleportationAndSetTheComebackPoint()
	// but using orig(), and not removing grappling hooks (that is done before the call)
	private static void DoPotionOfReturnTeleportationAndSetTheComebackPointInPlaceOfSpawnMethod(On_Player.orig_Spawn orig, Player self) {
		self.PotionOfReturnOriginalUsePosition = self.Bottom;
		bool flag = self.immune;
		int num = self.immuneTime;
		self.StopVanityActions(multiplayerBroadcast: false);
		orig(self, PlayerSpawnContext.RecallFromItem);
		self.PotionOfReturnHomePosition = self.Bottom;
		NetMessage.SendData(MessageID.PlayerControls, -1, self.whoAmI, null, self.whoAmI);
		self.immune = flag;
		self.immuneTime = num;
	}
}
