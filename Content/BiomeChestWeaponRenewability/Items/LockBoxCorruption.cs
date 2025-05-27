using static MaddieQoL.Common.Shorthands;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.BiomeChestWeaponRenewability.Items;

public sealed class LockBoxCorruption : AbstractBiomeLockBox {
	static LocalizedText TooltipWhenEnabled {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableBiomeLockBoxes ? TooltipWhenEnabled : base.Tooltip;

	protected override int ChestKeyItemID => ItemID.CorruptionKey;
	protected override int ContainedWeaponItemID => ItemID.ScourgeoftheCorruptor;

	public override void SetStaticDefaults() {
		base.SetStaticDefaults();
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
	}
}
