using static MaddieQoL.Common.Shorthands;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MaddieQoL.Content.Mirrors.Items.ShellphonePlus;

public sealed class ShellphonePlusReturn : AbstractShellphonePlus {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableReturnTools ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame) {
		if (ModuleConf.enableReturnTools) {
			Styles.UseReturnStyle(player, this.Item);
		} else {
			Styles.UseRecallStyle(player, this.Item);
		}
	}
}
