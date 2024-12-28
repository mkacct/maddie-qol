using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Pylon.Items;

public class CavernPylon : StandardPylonItem {
	protected override int PylonTileID => ModContent.TileType<Tiles.CavernPylon>();
}
