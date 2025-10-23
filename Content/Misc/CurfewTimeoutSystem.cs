using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MaddieQoL.Content.Misc;

public sealed class CurfewTimeoutSystem : ModSystem {

	public bool IsTimeoutActive {get; set;}

	public override void ClearWorld() {
		this.IsTimeoutActive = false;
	}

	public override void PreUpdateWorld() {
		if (Main.dayTime && (Main.time == 0)) {
			this.IsTimeoutActive = false;
		}
	}

	public override void LoadWorldData(TagCompound tag) {
		this.IsTimeoutActive = tag.GetBool(nameof(this.IsTimeoutActive));
	}

	public override void SaveWorldData(TagCompound tag) {
		tag[nameof(this.IsTimeoutActive)] = this.IsTimeoutActive;
	}

}
