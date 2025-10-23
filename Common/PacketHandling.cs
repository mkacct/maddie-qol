using System.Collections.Generic;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Common;

public sealed class PacketHandling : ModSystem {

	// All packet handlers must be registered here!
	static readonly IList<IPacketHandler> PacketHandlers = [
		Content.Misc.Items.ActivationRod.ActivationPacketHandler,
		Content.Misc.Items.CurfewBell.CurfewPacketHandler
	];

	public static PacketDispatcher PacketDispatcher {get; private set;}

	public override void Load() {
		PacketDispatcher = new(this.Mod, PacketHandlers);
	}

}
