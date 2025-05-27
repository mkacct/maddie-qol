using static MaddieQoL.Common.Shorthands;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Chat;
using Terraria.Audio;
using MaddieQoL.Util;
using MaddieQoL.Common;

namespace MaddieQoL.Content.Misc.Items;

public sealed class CurfewBell : ModItem {
	public static readonly PacketHandler<CurfewPacketData> CurfewPacketHandler = new(
		clientHandler: ClientHandleCurfewPacket
	);

	static readonly SoundStyle ActivateSound = Sounds.DeepBell;

	static LocalizedText TooltipWhenEnabled {get; set;}
	static LocalizedText ActivationText {get; set;}
	static LocalizedText TimeoutErrorText {get; set;}

	public override LocalizedText Tooltip => ModuleConf.enableCurfewBell ? TooltipWhenEnabled : base.Tooltip;

	public override void SetStaticDefaults() {
		TooltipWhenEnabled = this.GetLocalization(nameof(TooltipWhenEnabled));
		ActivationText = this.GetLocalization(nameof(ActivationText));
		TimeoutErrorText = this.GetLocalization(nameof(TimeoutErrorText));

		ItemID.Sets.DuplicationMenuToolsFilter[this.Type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[this.Type] = int.MaxValue;
	}

	public override void SetDefaults() {
		this.Item.width = 30;
		this.Item.height = 30;
		this.Item.useStyle = ItemUseStyleID.Swing;
		this.Item.useAnimation = 20;
		this.Item.useTime = 20;
		this.Item.rare = ItemRarityID.Blue;
		this.Item.value = Item.buyPrice(0, 10, 0, 0);
	}

	public override bool CanUseItem(Player player) {
		return ModuleConf.enableCurfewBell;
	}

	public override bool? UseItem(Player player) {
		if (Main.netMode == NetmodeID.MultiplayerClient) {return null;}
		ServerUse(player);
		return true;
	}

	static void ServerUse(Player player) {
		CurfewTimeoutSystem timeoutSystem = ModContent.GetInstance<CurfewTimeoutSystem>();
		if (timeoutSystem.IsTimeoutActive) {
			ChatHelper.SendChatMessageToClient(TimeoutErrorText.ToNetworkText(), TextColors.Status, player.whoAmI);
			return;
		}
		ChatHelper.BroadcastChatMessage(ActivationText.ToNetworkText(), TextColors.Event);
		TeleportAllNPCsHome(out ISet<DustCoords> srcCoords, out ISet<DustCoords> destCoords);
		if (Main.netMode == NetmodeID.Server) {
			CurfewPacketHandler.Send(new(player.whoAmI, srcCoords, destCoords));
		} else { // single player
			ClientReactToUse(player, srcCoords, destCoords);
		}
		timeoutSystem.IsTimeoutActive = true;
	}

	static void TeleportAllNPCsHome(out ISet<DustCoords> srcCoords, out ISet<DustCoords> destCoords) {
		srcCoords = new HashSet<DustCoords>();
		destCoords = new HashSet<DustCoords>();
		MethodInfo method_AI_007_TownEntities_TeleportToHome = typeof(NPC).GetMethod("AI_007_TownEntities_TeleportToHome", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(int), typeof(int)]);
		foreach (NPC npc in Main.npc) {
			if ((npc != null) && npc.active && npc.townNPC && !npc.homeless) {
				srcCoords.Add(new(npc.position, npc.width, npc.height));
				method_AI_007_TownEntities_TeleportToHome.Invoke(npc, [npc.homeTileX, npc.homeTileY]);
				destCoords.Add(new(npc.position, npc.width, npc.height));
			}
		}
	}

	public class CurfewPacketData(int itemUserPlayerId, ISet<DustCoords> srcCoords, ISet<DustCoords> destCoords) : IPacketData {
		public int ItemUserPlayerID {get; set;} = itemUserPlayerId;
		public ISet<DustCoords> SrcCoords {get; set;} = srcCoords;
		public ISet<DustCoords> DestCoords {get; set;} = destCoords;

		public CurfewPacketData() : this(-1, new HashSet<DustCoords>(), new HashSet<DustCoords>()) {}

		public void ReadFrom(BinaryReader reader) {
			this.ItemUserPlayerID = reader.ReadInt32();
			this.SrcCoords = ReadSetOfDustCoords(reader);
			this.DestCoords = ReadSetOfDustCoords(reader);
		}

		static ISet<DustCoords> ReadSetOfDustCoords(BinaryReader reader) {
			int count = reader.ReadInt32();
			HashSet<DustCoords> coords = [];
			for (int i = 0; i < count; i++) {
				coords.Add(new(new(reader.ReadSingle(), reader.ReadSingle()), reader.ReadInt32(), reader.ReadInt32()));
			}
			return coords;
		}

		public void WriteTo(BinaryWriter writer) {
			writer.Write(this.ItemUserPlayerID);
			WriteSetOfDustCoords(writer, this.SrcCoords);
			WriteSetOfDustCoords(writer, this.DestCoords);
		}

		static void WriteSetOfDustCoords(BinaryWriter writer, ISet<DustCoords> coords) {
			writer.Write(coords.Count);
			foreach (DustCoords item in coords) {
				writer.Write(item.Position.X);
				writer.Write(item.Position.Y);
				writer.Write(item.Width);
				writer.Write(item.Height);
			}
		}
	}

	static void ClientHandleCurfewPacket(CurfewPacketData data) {
		ClientReactToUse(Main.player[data.ItemUserPlayerID], data.SrcCoords, data.DestCoords);
	}

	static void ClientReactToUse(Player itemUser, ISet<DustCoords> srcCoords, ISet<DustCoords> destCoords) {
		SoundEngine.PlaySound(ActivateSound, itemUser.Center);
		MakeParticles(srcCoords, destCoords);
	}

	static void MakeParticles(ISet<DustCoords> srcCoords, ISet<DustCoords> destCoords) {
		foreach (DustCoords src in srcCoords) {
			MakeDustCloud(src);
		}
		foreach (DustCoords dest in destCoords) {
			MakeDustCloud(dest);
		}
	}

	static void MakeDustCloud(DustCoords coords) {
		int numDust = coords.Width * coords.Height / 25;
		for (int i = 0; i < numDust; i++) {
			Dust.NewDust(coords.Position, coords.Width, coords.Height, DustID.Cloud);
		}
	}
}
