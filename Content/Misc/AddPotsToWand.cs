using static MaddieQoL.Common.Shorthands;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace MaddieQoL.Content.Misc;

public sealed class AddPotsToWand : ModSystem {
	public override void Load() {
		if (!ModuleConf.enableAddPotsToWand) {return;}
		if (!UserConf.enableAddPotsToWand) {return;}
		int echoPileStyle = 0;
		ushort tileType = 0;
		FlexibleTileWand.ForModders_AddPotsToWand(FlexibleTileWand.RubblePlacementMedium, ref echoPileStyle, ref tileType);
	}
}
