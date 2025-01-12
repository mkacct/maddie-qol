using Terraria.Audio;

namespace MaddieQoL.Common;

public static class Sounds {
	private const string SoundsDir = $"{nameof(MaddieQoL)}/Assets/Sounds";

	public static readonly SoundStyle DeepBell = new($"{SoundsDir}/DeepBell");
}
