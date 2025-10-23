using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static MaddieQoL.Common.Shorthands;

namespace MaddieQoL.Content.Renewability;

public sealed class RenewabilityChestLockOverrider : GlobalItem {

	const int DoorHeightTiles = 3, DoorTilesheetCellSize = 18;
	const int LihzahrdDoorClosedLockedSubId = 11, LihzahrdDoorClosedUnlockedSubId = 12;

	static int LihzahrdDoorClosedLockedFrameY => LihzahrdDoorClosedLockedSubId * DoorHeightTiles * DoorTilesheetCellSize;
	static int LihzahrdDoorClosedUnlockedFrameY => LihzahrdDoorClosedUnlockedSubId * DoorHeightTiles * DoorTilesheetCellSize;

	static LocalizedText ChestLockAddlTooltip {get; set;}

	public override void SetStaticDefaults() {
		ChestLockAddlTooltip = this.Mod.GetLocalization($"{LangMisc}.{nameof(ChestLockAddlTooltip)}");
	}

	public override void Load() {
		On_Player.PlaceThing_LockChest += (On_Player.orig_PlaceThing_LockChest orig, Player self) => {
			orig(self);
			if (ModuleConf.enableLihzahrdDoorLock) {
				TryUseLockOnDoor(self);
			}
		};
	}

	static void TryUseLockOnDoor(Player player) {
		Item item = player.inventory[player.selectedItem];
		if (!((item.type == ItemID.ChestLock) && (item.stack > 0))) {return;}
		if (!(
			player.IsTargetTileInItemRange(item)
			&& player.ItemTimeIsZero
			&& player.ItemAnimationActive
			&& player.controlUseItem
		)) {return;}
		if (LockDoor(Player.tileTargetX, Player.tileTargetY)) {
			ConsumeOneOfItem(player);
			if (Main.netMode == NetmodeID.MultiplayerClient) {
				NetMessage.SendData(
					MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 2f, Player.tileTargetX, Player.tileTargetY
				);
			}
		}
	}

	static bool LockDoor(int x, int y) {
		Tile targetTile = Framing.GetTileSafely(x, y);
		if (!TileIsClosedDoor(targetTile)) {return false;}
		if (TileObjectData.GetTileStyle(targetTile) != LihzahrdDoorClosedUnlockedSubId) {return false;}
		if (!NPC.downedPlantBoss) {return false;}
		while (targetTile.TileFrameY != LihzahrdDoorClosedUnlockedFrameY) {
			y--;
			targetTile = Framing.GetTileSafely(x, y);
			if (!TileIsClosedDoor(targetTile)) {return false;}
			if (TileObjectData.GetTileStyle(targetTile) != LihzahrdDoorClosedUnlockedSubId) {return false;}
		} // targetTile is now the top tile of the closed unlocked door
		SoundEngine.PlaySound(SoundID.Unlock, new Vector2(16*x, 16*y + 16));
		for (int i = 0; i < DoorHeightTiles; i++) {
			Tile tile = Framing.GetTileSafely(x, y + i);
			tile.TileFrameY = (short)(LihzahrdDoorClosedLockedFrameY + (DoorTilesheetCellSize * i));
		}
		return true;
	}

	static bool TileIsClosedDoor(Tile tile) => tile.HasTile && (tile.TileType == TileID.ClosedDoor);

	static void ConsumeOneOfItem(Player player) {
		player.inventory[player.selectedItem].stack--;
		if (player.inventory[player.selectedItem].stack <= 0) {
			player.inventory[player.selectedItem] = new Item();
		}
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		if (!ModuleConf.enableLihzahrdDoorLock) {return;}
		if (item.type != ItemID.ChestLock) {return;}
		tooltips.Add(new TooltipLine(this.Mod, "CanLockDoor", ChestLockAddlTooltip.Value));
	}

}
