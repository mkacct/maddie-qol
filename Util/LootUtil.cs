using System;
using Terraria.GameContent.ItemDropRules;

namespace MaddieQoL.Util;

public static class LootUtil {
	public static bool HasMatchingChainedRule(this IItemDropRule rule, Predicate<IItemDropRule> match) {
		return rule.ChainedRules.Find((chainAttempt) => {
			return (chainAttempt != null) && match(chainAttempt.RuleToChain);
		}) != null;
	}
}
