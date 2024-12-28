using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.GameContent;
using Terraria;
using MaddieQoL.Util;

namespace MaddieQoL.Content.Pylon.Tiles;

public class CavernPylon : StandardPylonTile {
	protected override ModItem PylonItemInstance => ModContent.GetInstance<Items.CavernPylon>();
	protected override TEModdedPylon PylonTileEntityInstance => ModContent.GetInstance<TileEntities.CavernPylon>();

    protected override Condition ShopEntryBiomeCondition => Condition.InRockLayerHeight; // TODO: also allow hell

    protected override Color DustColor => new(0.4f, 0.4f, 0.6f);
	protected override Color LightColor => new(0.6f, 0.6f, 0.8f);

    public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData) {
		return pylonInfo.PositionInTiles.Y >= Main.rockLayer;
	}
}
