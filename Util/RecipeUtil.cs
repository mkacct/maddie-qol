using System;
using System.Linq;
using Terraria;

namespace MaddieQoL.Util;

public static class RecipeExtensions {
	public static Recipe RegisterBeforeFirstRecipeOf(this Recipe recipe, int itemId) {
		recipe.SortBeforeFirstRecipesOf(itemId);
		recipe.Register();
		return recipe;
	}

	public static Recipe RegisterAfterLastRecipeOf(this Recipe recipe, int itemId) {
		recipe.SortAfter(Main.recipe.Last((recipe) => recipe.createItem.type == itemId));
		recipe.Register();
		return recipe;
	}

	public static Recipe RegisterUsing(this Recipe recipe, RecipeOrderedRegistrar registrar) {
		registrar.InternalRegisterRecipe(recipe);
		return recipe;
	}

	public static bool HasCustomShimmerResults(this Recipe recipe) {
		return (recipe.customShimmerResults != null) && (recipe.customShimmerResults.Count > 0);
	}
}

public class RecipeOrderedRegistrar {
	int lastItemId = -1;
	bool isBefore = false;
	Recipe lastRecipe = null;

	public static RecipeOrderedRegistrar StartingBefore(int itemId) {
		RecipeOrderedRegistrar registrar = new();
		registrar.SortBeforeFirstRecipeOf(itemId);
		return registrar;
	}

	public static RecipeOrderedRegistrar StartingAfter(int itemId) {
		RecipeOrderedRegistrar registrar = new();
		registrar.SortAfterLastRecipeOf(itemId);
		return registrar;
	}

	public void SortBeforeFirstRecipeOf(int itemId) {
		this.lastItemId = itemId;
		this.isBefore = true;
		this.lastRecipe = null;
	}

	public void SortAfterLastRecipeOf(int itemId) {
		this.lastItemId = itemId;
		this.isBefore = false;
		this.lastRecipe = null;
	}

	/// <remarks>Prefer extension method Recipe.RegisterUsing() over calling this directly.</remarks>
	/// <param name="recipe">Should not yet be sorted</param>
	/// <exception cref="InvalidOperationException">If there is no basis for sorting yet</exception>
	internal void InternalRegisterRecipe(Recipe recipe) {
		if (this.lastRecipe != null) {
			recipe.SortAfter(lastRecipe);
			recipe.Register();
		} else if (this.lastItemId >= 0) {
			if (this.isBefore) {
				recipe.RegisterBeforeFirstRecipeOf(this.lastItemId);
			} else {
				recipe.RegisterAfterLastRecipeOf(this.lastItemId);
			}
		} else {
			throw new InvalidOperationException("No sorting criteria set");
		}
		this.lastRecipe = recipe;
		this.lastItemId = -1;
		this.isBefore = false;
	}
}
