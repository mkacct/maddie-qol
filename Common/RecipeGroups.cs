using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MaddieQoL.Common;

public class RecipeGroups : ModSystem {
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
		{
			RecipeGroup group = new(
				() => {return ShellphoneRecipeGroupName.Value;},
				[
					ItemID.ShellphoneDummy,
					ItemID.Shellphone,
					ItemID.ShellphoneOcean,
					ItemID.ShellphoneHell,
					ItemID.ShellphoneSpawn
				]
			);
			RecipeGroup.RegisterGroup(ShellphoneRecipeGroup, group);
		}
		{
			RecipeGroup group = new(
				() => {return UniversalBucketRecipeGroupName.Value;},
				[
					ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketDummy>(),
					ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketWater>(),
					ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketLava>(),
					ModContent.ItemType<Content.LiquidManipulation.Items.UniversalBucket.UniversalBucketHoney>()
				]
			);
			RecipeGroup.RegisterGroup(UniversalBucketRecipeGroup, group);
		}
		{
			RecipeGroup group = new(
				() => {return BiomeTorchRecipeGroupName.Value;},
				[
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
				]
			);
			RecipeGroup.RegisterGroup(BiomeTorchRecipeGroup, group);
		}
		{
			RecipeGroup group = new(
				() => {return BiomeCampfireRecipeGroupName.Value;},
				[
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
				]
			);
			RecipeGroup.RegisterGroup(BiomeCampfireRecipeGroup, group);
		}
		{
			RecipeGroup group = new(
				() => {return DungeonBrickRecipeGroupName.Value;},
				[
					ItemID.BlueBrick,
					ItemID.GreenBrick,
					ItemID.PinkBrick
				]
			);
			RecipeGroup.RegisterGroup(DungeonBrickRecipeGroup, group);
		}
	}
}
