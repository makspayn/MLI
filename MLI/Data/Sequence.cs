﻿using System;
using System.Collections.Generic;
using System.Linq;
using MLI.Services;

namespace MLI.Data
{
	public class Sequence
	{
		public enum SequenceState
		{
			Sequence, One, Zero
		}
		
		private static string start = "1 -> ";
		private static string separator = " v ";
		private List<Disjunct> disjuncts;

		public Sequence(string sequence)
		{
			try
			{
				if (sequence.StartsWith(start))
				{
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
				LogService.Error($"Некорректная секвенция: {sequence}");
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

		public void ValidateFact()
		{
			if (GetPredicates().Count > 1)
			{
				LogService.Error($"Некорректная секвенция-факт: {this}");
				throw new Exception($"Некорректная секвенция-факт: {this}");
			}
		}

		public static SequenceState GetSequenceState(Sequence sequence)
		{
			return sequence == null ? SequenceState.One : 
				sequence.GetDisjuncts().Count == 0 ? 
				SequenceState.Zero : 
				SequenceState.Sequence;
		}

		public static Sequence Multiply(List<Sequence> sequences)
		{
			Sequence sequence = new Sequence(sequences[0].ToString());
			sequences.RemoveAt(0);
			return sequences.Aggregate(sequence, Multiply);
		}

		private static Sequence Multiply(Sequence sequence1, Sequence sequence2)
		{
			List<Disjunct> disjuncts = (from disj1 in sequence1.GetDisjuncts() from disj2 in sequence2.GetDisjuncts()
										select Disjunct.Multiply(new List<Disjunct> {disj1, disj2})).ToList();
			return new Sequence(start + string.Join(separator, disjuncts));
		}
	}
}