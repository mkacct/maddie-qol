using System.IO;
using Terraria.ModLoader;
using MaddieQoL.Common;

namespace MaddieQoL;

public sealed class MaddieQoL : Mod {
	public override void HandlePacket(BinaryReader reader, int whoAmI) {
		PacketHandling.PacketDispatcher.HandlePacket(reader, whoAmI);
	}
}
