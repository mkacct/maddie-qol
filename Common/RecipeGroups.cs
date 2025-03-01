using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Common;

public sealed class RecipeGroups : ModSystem {
	public const string ShellphoneRecipeGroup = nameof(ItemID.Shellphone);
	public const string UniversalBucketRecipeGroup = $"{nameof(MaddieQoL)}:{nameof(Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketDummy)}";
	public const string BiomeTorchRecipeGroup = $"{nameof(MaddieQoL)}:BiomeTorch";
	public const string BiomeCampfireRecipeGroup = $"{nameof(MaddieQoL)}:BiomeCampfire";
	public const string DungeonBrickRecipeGroup = nameof(ItemID.BlueBrick);

	private static LocalizedText ShellphoneRecipeGroupName {get; set;}
	private static LocalizedText UniversalBucketRecipeGroupName {get; set;}
	private static LocalizedText BiomeTorchRecipeGroupName {get; set;}
	private static LocalizedText BiomeCampfireRecipeGroupName {get; set;}
	private static LocalizedText DungeonBrickRecipeGroupName {get; set;}

	public override void SetStaticDefaults() {
		ShellphoneRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(ShellphoneRecipeGroupName)}");
		UniversalBucketRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(UniversalBucketRecipeGroupName)}");
		BiomeTorchRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(BiomeTorchRecipeGroupName)}");
		BiomeCampfireRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(BiomeCampfireRecipeGroupName)}");
		DungeonBrickRecipeGroupName = this.Mod.GetLocalization($"{nameof(RecipeGroups)}.{nameof(DungeonBrickRecipeGroupName)}");
	}

	public override void AddRecipeGroups() {
		RegisterRecipeGroup(ShellphoneRecipeGroup, ShellphoneRecipeGroupName, GetShellphoneRecipeGroupItems());
		RegisterRecipeGroup(UniversalBucketRecipeGroup, UniversalBucketRecipeGroupName, GetUniversalBucketRecipeGroupItems());
		RegisterRecipeGroup(BiomeTorchRecipeGroup, BiomeTorchRecipeGroupName, GetBiomeTorchRecipeGroupItems());
		RegisterRecipeGroup(BiomeCampfireRecipeGroup, BiomeCampfireRecipeGroupName, GetBiomeCampfireRecipeGroupItems());
		RegisterRecipeGroup(DungeonBrickRecipeGroup, DungeonBrickRecipeGroupName, GetDungeonBrickRecipeGroupItems());
	}

	private static void RegisterRecipeGroup(string name, LocalizedText displayName, IList<int> validItems) {
		RecipeGroup group = new(() => displayName.Value, [..validItems]);
		RecipeGroup.RegisterGroup(name, group);
	}

	private static IList<int> GetShellphoneRecipeGroupItems() {
		return [
			ItemID.ShellphoneDummy,
			ItemID.Shellphone,
			ItemID.ShellphoneOcean,
			ItemID.ShellphoneHell,
			ItemID.ShellphoneSpawn
		];
	}

	private static IList<int> GetUniversalBucketRecipeGroupItems() {
		return [
			ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketDummy>(),
			ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketWater>(),
			ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketLava>(),
			ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketHoney>()
		];
	}

	private static IList<int> GetBiomeTorchRecipeGroupItems() {
		List<int> items = [
			ItemID.ShimmerTorch,
			ItemID.DemonTorch,
			ItemID.BoneTorch,
			ItemID.HallowedTorch,
			ItemID.CorruptTorch,
			ItemID.CrimsonTorch,
			ItemID.IceTorch,
			ItemID.MushroomTorch,
			ItemID.JungleTorch,
			ItemID.DesertTorch
		];
		if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod)) {
			TryAddModItemToList(items, thoriumMod, "DeeplightTorch");
		}
		return items;
	}

	private static IList<int> GetBiomeCampfireRecipeGroupItems() {
		List<int> items = [
			ItemID.ShimmerCampfire,
			ItemID.DemonCampfire,
			ItemID.BoneCampfire,
			ItemID.HallowedCampfire,
			ItemID.CorruptCampfire,
			ItemID.CrimsonCampfire,
			ItemID.FrozenCampfire,
			ItemID.MushroomCampfire,
			ItemID.JungleCampfire,
			ItemID.DesertCampfire
		];
		if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod)) {
			TryAddModItemToList(items, thoriumMod, "DeeplightCampfire");
		}
		return items;
	}

	private static IList<int> GetDungeonBrickRecipeGroupItems() {
		return [
			ItemID.BlueBrick,
			ItemID.GreenBrick,
			ItemID.PinkBrick
		];
	}

	private static void TryAddModItemToList(IList<int> list, Mod mod, string itemName) {
		if (mod.TryFind(itemName, out ModItem item)) {
			list.Add(item.Type);
		}
	}
}
