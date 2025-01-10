using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace MaddieQoL.Common;

public static class ItemDropRules {
	public static IItemDropRule OneStackFromOptions(
		int chanceDenominator, int minimumDropped, int maximumDropped, params int[] options
	) {
		return OneStackFromOptionsWithNumerator(chanceDenominator, 1, minimumDropped, maximumDropped, options);
	}

	public static IItemDropRule OneStackFromOptionsWithNumerator(
		int chanceDenominator, int chanceNumerator, int minimumDropped, int maximumDropped, params int[] options
	) {
		IList<IItemDropRule> ruleOptions = new List<IItemDropRule>(options.Length);
		foreach (int option in options) {
			ruleOptions.Add(ItemDropRule.Common(option, 1, minimumDropped, maximumDropped));
		}
		return new OneFromRulesRule(chanceDenominator, chanceNumerator, [..ruleOptions]);
	}
}
