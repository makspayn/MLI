using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MLI.Data
{
	public class Predicate
	{
		private static string separator = ",";
		private bool type;
		private string name;
		private List<Argument> arguments;
		private static Regex rgxPredicate = new Regex(@"^[-+][A-Z][a-zA-Z0-9]*\([^\s]*\)$");

		public Predicate(string predicate)
		{
			if (rgxPredicate.IsMatch(predicate))
			{
				type = predicate[0] == '+';
				name = predicate.Substring(1, predicate.IndexOf("(", StringComparison.Ordinal) - 1);
				int prefixLength = name.Length + 2;
				arguments = Argument.ParseArguments(predicate.Substring(prefixLength, predicate.Length - prefixLength - 1));
			}
			else
			{
				throw new Exception($"Некорректный предикат: {predicate}");
			}
		}

		public override string ToString()
		{
			return (type ? "+" : "-") + $"{name}({string.Join(separator, arguments)})";
		}

		public static bool Equals(Predicate pred1, Predicate pred2)
		{
			return string.Equals(pred1.ToString(), pred2.ToString());
		}
	}
}