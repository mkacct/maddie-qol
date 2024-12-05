using static MaddieQoL.Util.RecipeUtil;
using System;
using System.Linq;
using Terraria;

namespace MaddieQoL.Util;

public sealed class RecipeUtil {
	private RecipeUtil() {} // prevent instantiation

	public static void RegisterBeforeFirstRecipe(Recipe recipe, short itemId) {
		recipe.SortBeforeFirstRecipesOf(itemId);
		recipe.Register();
	}

	public static void RegisterAfterLastRecipe(Recipe recipe, short itemId) {
		recipe.SortAfter(Main.recipe.Last((recipe) => {return recipe.createItem.type == itemId;}));
		recipe.Register();
	}

	public static RecipeOrderedRegisterer OrderedRegistererStartingBefore(short itemId) {
		RecipeOrderedRegisterer registerer = new();
		registerer.SortBeforeFirstRecipeOf(itemId);
		return registerer;
	}

	public static RecipeOrderedRegisterer OrderedRegistererStartingAfter(short itemId) {
		RecipeOrderedRegisterer registerer = new();
		registerer.SortAfterLastRecipeOf(itemId);
		return registerer;
	}
}

public class RecipeOrderedRegisterer() {
	private short lastItemId = -1;
	private bool isBefore = false;
	private Recipe lastRecipe = null;

	/// <param name="recipe">must not yet be sorted</param>
	public void Register(Recipe recipe) {
		if (this.lastRecipe != null) {
			recipe.SortAfter(lastRecipe);
			recipe.Register();
		} else if (this.lastItemId >= 0) {
			if (this.isBefore) {
				RegisterBeforeFirstRecipe(recipe, this.lastItemId);
			} else {
				RegisterAfterLastRecipe(recipe, this.lastItemId);
			}
		} else {
			throw new Exception("No sorting criteria set");
		}
		this.lastRecipe = recipe;
		this.lastItemId = -1;
		this.isBefore = false;
	}

	public void SortBeforeFirstRecipeOf(short itemId) {
		this.lastItemId = itemId;
		this.isBefore = true;
		this.lastRecipe = null;
	}

	public void SortAfterLastRecipeOf(short itemId) {
		this.lastItemId = itemId;
		this.isBefore = false;
		this.lastRecipe = null;
	}
}
