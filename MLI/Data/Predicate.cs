using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MLI.Services;

namespace MLI.Data
{
	public class Predicate
	{
		private static Regex rgxPredicate = new Regex(@"^[-+][A-Z][a-zA-Z0-9]*\([^\s]*\)$");
		private static string separator = ",";
		private bool type;
		private string name;
		private List<Argument> arguments;

		public Predicate(string predicate)
		{
			try {
				if (rgxPredicate.IsMatch(predicate))
				{
					type = predicate[0] == '+';
					name = predicate.Substring(1, predicate.IndexOf("(", StringComparison.Ordinal) - 1);
					int prefixLength = name.Length + 2;
					arguments = Argument.ParseArguments(predicate.Substring(prefixLength, predicate.Length - prefixLength - 1));

				}
				else
				{
					throw new Exception("Неверный формат предиката");
				}
			}
			catch (Exception exception)
			{
				LogService.Error($"Некорректный предикат: {predicate}");
				throw new Exception($"Некорректный предикат: {predicate}\n" + exception.Message);
			}
		}

		public override string ToString()
		{
			return (type ? "+" : "-") + $"{name}({string.Join(separator, arguments)})";
		}

		public static bool Equals(Predicate predicate1, Predicate predicate2)
		{
			return string.Equals(predicate1.ToString(), predicate2.ToString());
		}

		public List<Argument> GetArguments()
		{
			return arguments;
		}

		public static bool CanUnify(Predicate predicate1, Predicate predicate2)
		{
			return predicate1.type == predicate2.type && predicate1.name == predicate2.name &&
			       predicate1.GetArguments().Count == predicate2.GetArguments().Count;
		}

		public static Predicate GetInversPredicate(Predicate predicate)
		{
			return new Predicate(predicate.ToString()) {type = !predicate.type};
		}

		public static bool Contains(Predicate predicate, List<Predicate> predicates)
		{
			foreach (Predicate pred in predicates)
			{
				if (Equals(pred, predicate))
				{
					return true;
				}
			}
			return false;
		}
	}
}