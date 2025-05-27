using Terraria;

namespace MaddieQoL.Content.Mirrors;

static class Util {
	public static void DisplayEverything(Player player) {
		player.accWatch = 3;
		player.accDepthMeter = 1;
		player.accCompass = 1;
		player.accFishFinder = true;
		player.accWeatherRadio = true;
		player.accCalendar = true;
		player.accThirdEye = true;
		player.accJarOfSouls = true;
		player.accCritterGuide = true;
		player.accStopwatch = true;
		player.accOreFinder = true;
		player.accDreamCatcher = true;
	}
}
