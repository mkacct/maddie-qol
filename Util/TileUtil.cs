using Microsoft.Xna.Framework;
using Terraria;

namespace MaddieQoL.Util;

public static class TileUtil {
	public static Vector2 TileDrawPosition(float x, float y) {
		Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
		return new Vector2(x, y) + zero;
	}
}
