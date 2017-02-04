using System;
using System.Collections.Generic;
using NLog;

namespace MLI.Data
{
	public class Disjunct
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
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
				logger.Error($"Некорректный дизъюнкт: {disjunct}");
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
	}
}