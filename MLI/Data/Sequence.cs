using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace MLI.Data
{
	public class Sequence
	{
		public enum SequenceType
		{
			Fact, Rule, Conclusion
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static string start = "1 -> ";
		private static string separator = " v ";
		private SequenceType type;
		private List<Disjunct> disjuncts;

		public Sequence(string sequence, SequenceType type)
		{
			try
			{
				if (sequence.StartsWith(start))
				{
					this.type = type;
					string[] disjunctStrings = sequence.Substring(start.Length).Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
					disjuncts = new List<Disjunct>();
					foreach (string disjunctString in disjunctStrings)
					{
						disjuncts.Add(new Disjunct(disjunctString));
					}
				}
				else
				{
					throw new Exception($"Секвенция должна начинаться с {start}");
				}
			}
			catch (Exception exception)
			{
				logger.Error($"Некорректная секвенция: {sequence}");
				throw new Exception($"Некорректная секвенция: {sequence}\n" + exception.Message);
			}
		}

		public override string ToString()
		{
			return start + string.Join(separator, disjuncts);
		}

		public static bool Equals(Sequence sequence1, Sequence sequence2)
		{
			return string.Equals(sequence1.ToString(), sequence2.ToString());
		}

		public List<Disjunct> GetDisjuncts()
		{
			return disjuncts;
		}

		public List<Predicate> GetPredicates()
		{
			return disjuncts.SelectMany(disjunct => disjunct.GetPredicates()).ToList();
		}

		public string GetContent()
		{
			return string.Join(separator, disjuncts);
		}
	}
}