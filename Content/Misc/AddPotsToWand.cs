using static MaddieQoL.Common.Shorthands;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace MaddieQoL.Content.Misc;

public class AddPotsToWand : ModSystem {
	public override void Load() {
		if (!ModuleConf.enableAddPotsToWand) {return;}
		int echoPileStyle = 0;
		ushort tileType = 0;
		FlexibleTileWand.ForModders_AddPotsToWand(FlexibleTileWand.RubblePlacementMedium, ref echoPileStyle, ref tileType);
	}
}
