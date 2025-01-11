using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Content.MusicBoxes.Items;

public sealed class SilenceBox : ModItem {
	public override LocalizedText Tooltip => LocalizedText.Empty;

	public override void SetStaticDefaults() {
		ItemID.Sets.CanGetPrefixes[this.Type] = false;
		ItemID.Sets.ShimmerTransformToItem[this.Type] = ItemID.MusicBox;
		MusicLoader.AddMusicBox(
			this.Mod,
			MusicLoader.GetMusicSlot(this.Mod, "Assets/Music/Silence"),
			this.Type,
			ModContent.TileType<Tiles.SilenceBox>()
		);
	}

	public override void SetDefaults() {
		Item.DefaultToMusicBox(ModContent.TileType<Tiles.SilenceBox>(), 0);
	}
}
