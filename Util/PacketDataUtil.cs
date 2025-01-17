using System.IO;
using Microsoft.Xna.Framework;

namespace MaddieQoL.Util;

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
