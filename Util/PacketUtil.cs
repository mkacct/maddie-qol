using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MaddieQoL.Util;

/// <summary>
/// Creates and dispatches packets for a mod.
/// Create only one of these, and put it in a static property.
/// </summary>
public sealed class PacketDispatcher {
	private readonly Mod _mod;

	private readonly IDictionary<int, IPacketHandler> _packetIndicesToHandlers = new Dictionary<int, IPacketHandler>();
	private readonly IDictionary<IPacketHandler, int> _packetHandlersToIndices = new Dictionary<IPacketHandler, int>();

	/// <summary>
	/// Construct a new packet dispatcher.
	/// Call this in a ModSystem.Load() method, to ensure it actually runs when the mod is loaded.
	/// </summary>
	/// <param name="mod">The mod, for creating new packets</param>
	/// <param name="packetHandlers">All known packet handlers in the mod</param>
	/// <remarks>packetHandlers is an ordered list since the indices are used to identify the handlers.</remarks>
	public PacketDispatcher(Mod mod, IList<IPacketHandler> packetHandlers) {
		this._mod = mod;
		for (int i = 0; i < packetHandlers.Count; i++) {
			IPacketHandler handler = packetHandlers[i];
			handler.AssignDispatcher(this);
			this._packetIndicesToHandlers[i] = handler;
			this._packetHandlersToIndices[handler] = i;
		}
	}

	/// <summary>
	/// Handle a packet received from the network. (Call this from Mod.HandlePacket().)</summary>
	/// </summary>
	/// <param name="reader">BinaryReader reader as passed to Mod.HandlePacket()</param>
	/// <param name="srcPlayerId">int whoAmI as passed to Mod.HandlePacket()</param>
	/// <exception cref="InvalidDataException">
	/// If the packet handler index is out of range
	/// (You probably forgot to register the packet handler with the dispatcher...)
	/// </exception>
	public void HandlePacket(BinaryReader reader, int srcPlayerId) {
		int packetHandlerIndex = reader.ReadInt32();
		bool gotHandler = this._packetIndicesToHandlers.TryGetValue(packetHandlerIndex, out IPacketHandler handler);
		if (!gotHandler) {throw new InvalidDataException($"Unknown packet handler index: {packetHandlerIndex}");}
		handler.HandlePacket(reader, srcPlayerId);
	}

	internal ModPacket GetPacket(IPacketHandler handler) {
		ModPacket packet = this._mod.GetPacket();
		bool gotIndex = this._packetHandlersToIndices.TryGetValue(handler, out int index);
		if (!gotIndex) {throw new ArgumentException("Packet handler not in list");}
		packet.Write(index);
		return packet;
	}
}

/// <summary>
/// Implemented by PacketHandler. Exists so PacketDispatcher can call methods without needing to know the packet data type.
/// You shouldn't need to use this interface outside this file.
/// </summary>
public interface IPacketHandler {
	void AssignDispatcher(PacketDispatcher dispatcher);
	void HandlePacket(BinaryReader reader, int srcPlayerId);
}

/// <summary>
/// A packet handler for sending and receiving packets with packet data type D.
/// Put these in static readonly fields in classes that need to work with packets.
/// </summary>
/// <typeparam name="D">The packet data class (must implement IPacketData and have a no-argument constructor)</typeparam>
/// <param name="serverHandler">Called when a packet is received on the server (from the client)</param>
/// <param name="clientHandler">Called when a packet is received on the client (from the server)</param>
public sealed class PacketHandler<D>(
	Action<D, int> serverHandler = null,
	Action<D> clientHandler = null
) : IPacketHandler where D : IPacketData, new() {
	private PacketDispatcher _dispatcher = null;

	/// <summary>
	/// Assign the dispatcher for this packet handler.
	/// Called by PacketDispatcher; you don't need to call this anywhere else.
	/// </summary>
	/// <param name="dispatcher">The PacketDispatcher</param>
	/// <exception cref="InvalidOperationException">
	/// If the dispatcher is already assigned
	/// (I said you don't need to call this anywhere else...)
	/// </exception>
	public void AssignDispatcher(PacketDispatcher dispatcher) {
		if (this._dispatcher != null) {throw new InvalidOperationException("Dispatcher already assigned");}
		this._dispatcher = dispatcher;
	}

	/// <summary>
	/// Send a packet containing the given packet data.
	/// If you're on the client, sends to the server.
	/// If you're on the server, sends to all clients, unless this behavior is overridden by setting either toClient or ignoreClient.
	/// </summary>
	/// <param name="data">The data to send</param>
	/// <param name="toClient">One client to send to (passed directly to ModPacket.Send())</param>
	/// <param name="ignoreClient">One client to NOT send to (passed directly to ModPacket.Send())</param>
	public void Send(D data, int toClient = -1, int ignoreClient = -1) {
		ModPacket packet = this._dispatcher.GetPacket(this);
		data.WriteTo(packet);
		packet.Send(toClient, ignoreClient);
	}

	/// <summary>
	/// Handle a packet received from the network.
	/// Called by PacketDispatcher; you don't need to call this anywhere else.
	/// </summary>
	/// <param name="reader">The binary reader, after reading the packet handler index</param>
	/// <param name="srcPlayerId">ID of the client sending the request (only used on server)</param>
	/// <exception cref="NotSupportedException">
	/// If no handler is implemented for the receiving side
	/// </exception>
	public void HandlePacket(BinaryReader reader, int srcPlayerId) {
		D data = new();
		data.ReadFrom(reader);
		if (Main.netMode == NetmodeID.Server) {
			if (serverHandler == null) {throw new NotSupportedException("Server handler not implemented");}
			serverHandler(data, srcPlayerId);
		} else {
			if (clientHandler == null) {throw new NotSupportedException("Client handler not implemented");}
			clientHandler(data);
		}
	}
}

/// <summary>
/// Packet data, with methods to read from and write to a binary stream.
/// All packet data classes (for use with PacketHandler) must implement this interface.
/// When implementing this interface, you must also include a no-argument constructor.
/// </summary>
public interface IPacketData {
	void ReadFrom(BinaryReader reader);
	void WriteTo(BinaryWriter writer);
}
