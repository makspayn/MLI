using System;
using System.Collections.Generic;
using MLI.Services;

namespace MLI.Data
{
	public class Disjunct
	{
		public enum DisjunctState
		{
			Disjunct, Zero
		}

		private static string separator = " & ";
		private List<Predicate> predicates;

		public Disjunct(string disjunct)
		{
			try
			{
				string[] predicateStrings = disjunct.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
				predicates = new List<Predicate>();
				foreach (string predicateString in predicateStrings)
				{
					predicates.Add(new Predicate(predicateString));
				}
			}
			catch (Exception exception)
			{
				LogService.Error($"Некорректный дизъюнкт: {disjunct}");
				throw new Exception($"Некорректный дизъюнкт: {disjunct}\n" + exception.Message);
			}
		}

		public override string ToString()
		{
			return string.Join(separator, predicates);
		}

		public static bool Equals(Disjunct disjunct1, Disjunct disjunct2)
		{
			return string.Equals(disjunct1.ToString(), disjunct2.ToString());
		}

		public List<Predicate> GetPredicates()
		{
			return predicates;
		}

		public static DisjunctState GetDisjunctState(Disjunct disjunct)
		{
			return disjunct.predicates.Count == 0 ? DisjunctState.Zero : DisjunctState.Disjunct;
		}

		public static Disjunct Multiply(List<Disjunct> disjuncts)
		{
			return new Disjunct(string.Join(separator, disjuncts));
		}

		public static bool Contains(Disjunct disjunct, List<Disjunct> disjuncts)
		{
			foreach (Disjunct disj in disjuncts)
			{
				if (Equals(disj, disjunct))
				{
					return true;
				}
			}
			return false;
		}

		public static bool ContainsInverse(Disjunct disjunct, List<Disjunct> disjuncts)
		{
			if (disjunct.GetPredicates().Count == 1)
			{
				foreach (Disjunct disj in disjuncts)
				{
					if (disj.GetPredicates().Count == 1 && 
						Predicate.Equals(Predicate.GetInversPredicate(disjunct.GetPredicates()[0]), disj.GetPredicates()[0]))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}