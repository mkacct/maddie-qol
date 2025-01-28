using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.Misc.Items;

public sealed class Diode : ModItem {
    public override LocalizedText Tooltip => LocalizedText.Empty;

    public override void SetDefaults() {
		this.Item.CloneDefaults(ItemID.LogicGate_AND);
		this.Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Diode>());
		this.Item.value = Item.buyPrice(0, 2, 10, 0);
	}
}
