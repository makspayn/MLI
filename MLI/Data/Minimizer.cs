using System.Collections.Generic;

namespace MLI.Data
{
	public class Minimizer
	{
		private static string disjunctSeparator = " & ";
		private static string sequenceStart = "1 -> ";
		private static string sequenceSeparator = " v ";

		public static Sequence Minimize(Sequence sequence)
		{
			List<Disjunct> newDisjuncts = new List<Disjunct>();
			foreach (Disjunct disjunct in sequence.GetDisjuncts())
			{
				Disjunct disj = Minimize(disjunct);
				if (Disjunct.GetDisjunctState(disj).Equals(Disjunct.DisjunctState.Disjunct) && !Disjunct.Contains(disj, newDisjuncts))
				{
					newDisjuncts.Add(disj);
				}
			}
			foreach (Disjunct newDisjunct in newDisjuncts)
			{
				if (Disjunct.ContainsInverse(newDisjunct, newDisjuncts))
				{
					return null;
				}
			}
			return new Sequence(sequenceStart + string.Join(sequenceSeparator, newDisjuncts));
		}

		private static Disjunct Minimize(Disjunct disjunct)
		{
			List<Predicate> predicates = RemoveInversePredicates(RemoveSamePredicates(disjunct.GetPredicates()));
			return new Disjunct(string.Join(disjunctSeparator, predicates));
		}

		private static List<Predicate> RemoveSamePredicates(List<Predicate> predicates)
		{
			List<Predicate> newPredicates = new List<Predicate>();
			foreach (Predicate predicate in predicates)
			{
				if (!Predicate.Contains(predicate, newPredicates))
				{
					newPredicates.Add(predicate);
				}
			}
			return newPredicates;
		}

		private static List<Predicate> RemoveInversePredicates(List<Predicate> predicates)
		{
			List<Predicate> newPredicates = new List<Predicate>();
			foreach (Predicate predicate in predicates)
			{
				if (!Predicate.Contains(Predicate.GetInversPredicate(predicate), newPredicates))
				{
					newPredicates.Add(predicate);
				}
				else
				{
					newPredicates.Clear();
					break;
				}
			}
			return newPredicates;
		}
	}
}