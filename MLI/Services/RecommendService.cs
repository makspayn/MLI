using System;
using System.Collections.Generic;
using System.Linq;
using MLI.Data;

namespace MLI.Services
{
	public static class RecommendService
	{
		public static Tuple<int, int> GetRecommendation(List<Sequence> facts, List<Sequence> rules, List<Sequence> conclusions)
		{
			return new Tuple<int, int>(RecommendedExecUnitCount(rules, conclusions), RecommendedUnifUnitCount(facts, rules));
		}

		private static int RecommendedExecUnitCount(List<Sequence> rules, List<Sequence> conclusions)
		{
			int recommendedExecUnitCount = conclusions[0].GetPredicates().Sum(predicate => 
				rules.Sum(rule => 
					rule.GetPredicates().Count(pred => 
						Predicate.CanUnify(pred, predicate))));
			recommendedExecUnitCount += rules.Count;
			return recommendedExecUnitCount;
		}

		private static int RecommendedUnifUnitCount(List<Sequence> facts, List<Sequence> rules)
		{
			return rules.Select(rule => rule.GetPredicates().Count * facts.Count).Concat(new[] { 0 }).Max();
		}
	}
}