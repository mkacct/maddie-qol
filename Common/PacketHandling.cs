using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using MaddieQoL.Util;

namespace MaddieQoL.Common;

public sealed class PacketHandling : ModSystem {
	// All packet handlers must be registered here!
	private static readonly IList<IPacketHandler> PacketHandlers = [
		Content.Misc.Items.ActivationRod.ActivationPacketHandler,
		Content.Misc.Items.CurfewBell.CurfewPacketHandler
	];

	public static PacketDispatcher PacketDispatcher {get; private set;}

	public override void Load() {
		PacketDispatcher = new(this.Mod, PacketHandlers);
	}
}

// Common packet data classes follow:

/// <summary>
/// Packet data containing one XNA integer Point.
/// </summary>
public class PointPacketData(Point point) : IPacketData {
	public Point Point {get; set;} = point;

	public PointPacketData() : this(Point.Zero) {}

	public void ReadFrom(BinaryReader reader) {
		this.Point = new(reader.ReadInt32(), reader.ReadInt32());
	}

	public void WriteTo(BinaryWriter writer) {
		writer.Write(this.Point.X);
		writer.Write(this.Point.Y);
	}
}
