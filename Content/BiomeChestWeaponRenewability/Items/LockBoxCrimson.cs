using static MaddieQoL.Common.Shorthands;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.BiomeChestWeaponRenewability.Items;

public sealed class LockBoxCrimson : AbstractBiomeLockBox {
	private static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableBiomeLockBoxes ? TooltipWhenEnabled : base.Tooltip;

	protected override int ChestKeyItemID => ItemID.CrimsonKey;
	protected override int ContainedWeaponItemID => ItemID.VampireKnives;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}
}
