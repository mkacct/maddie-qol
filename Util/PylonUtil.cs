using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

namespace MaddieQoL.Util;

public abstract class StandardPylonItem : ModItem {
    public override LocalizedText Tooltip => Language.GetText("CommonItemTooltip.TeleportationPylon");

	protected abstract int PylonTileID {get;}

	public override void SetDefaults() {
		this.Item.DefaultToPlaceableTile(this.PylonTileID);
		this.Item.rare = ItemRarityID.Blue;
		this.Item.value = Item.buyPrice(0, 10, 0, 0);
	}
}

public abstract class StandardPylonTile : ModPylon {
	private const int CrystalVerticalFrameCount = 8;

	protected abstract ModItem PylonItemInstance {get;}
	protected abstract TEModdedPylon PylonTileEntityInstance {get;}

	protected virtual Condition ShopEntryBiomeCondition => null;

	protected abstract Color DustColor {get;}
	protected abstract Color LightColor {get;}
	protected virtual float LightMultiplier => 0.75f;

	private Asset<Texture2D> CrystalTexture {get; set;}
	private Asset<Texture2D> CrystalHighlightTexture {get; set;}
	private Asset<Texture2D> MapIcon {get; set;}

	// make sure to override ValidTeleportCheck_BiomeRequirements

	public override void Load() {
		this.CrystalTexture = ModContent.Request<Texture2D>(this.Texture + "_Crystal");
		this.CrystalHighlightTexture = ModContent.Request<Texture2D>(this.Texture + "_CrystalHighlight");
		this.MapIcon = ModContent.Request<Texture2D>(this.Texture + "_MapIcon");
	}

	public override void SetStaticDefaults() {
		Main.tileLighted[this.Type] = true;
		Main.tileFrameImportant[this.Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TEModdedPylon moddedPylon = this.PylonTileEntityInstance;
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(moddedPylon.PlacementPreviewHook_CheckIfCanPlace, 1, 0, true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(moddedPylon.Hook_AfterPlacement, -1, 0, false);
		TileObjectData.addTile(this.Type);

		TileID.Sets.InteractibleByNPCs[this.Type] = true;
		TileID.Sets.PreventsSandfall[this.Type] = true;
		TileID.Sets.AvoidedByMeteorLanding[this.Type] = true;

		this.AddToArray(ref TileID.Sets.CountsAsPylon);

		this.AddMapEntry(Color.White, this.CreateMapEntryName());
	}

	public override NPCShop.Entry GetNPCShopEntry() {
		NPCShop.Entry shopEntry = base.GetNPCShopEntry();
		if (this.ShopEntryBiomeCondition != null) {
			shopEntry.AddCondition(this.ShopEntryBiomeCondition);
		}
		return shopEntry;
	}

	public override void MouseOver(int i, int j) {
		Main.LocalPlayer.cursorItemIconEnabled = true;
		Main.LocalPlayer.cursorItemIconID = this.PylonItemInstance.Item.type;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY) {
		this.PylonTileEntityInstance.Kill(i, j);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
		r = this.LightColor.R / 255f * this.LightMultiplier;
		g = this.LightColor.G / 255f * this.LightMultiplier;
		b = this.LightColor.B / 255f * this.LightMultiplier;
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch) {
		this.DefaultDrawPylonCrystal(
			spriteBatch, i, j,
			this.CrystalTexture, this.CrystalHighlightTexture,
			new Vector2(0f, -12f), // crystalOffset
			Color.White * 0.1f, // pylonShadowColor
			this.DustColor, // dustColor
			4, // dustChanceDenominator
			CrystalVerticalFrameCount
		);
	}

	public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale) {
		bool mouseOver = DefaultDrawMapIcon(ref context, this.MapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
		this.DefaultMapClickHandle(mouseOver, pylonInfo, this.PylonItemInstance.DisplayName.Key, ref mouseOverText);
	}

	public override void NumDust(int i, int j, bool fail, ref int num) {
		num = 0;
	}
}
