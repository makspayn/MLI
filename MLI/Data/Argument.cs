using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MLI.Data
{
	public enum ArgumentType
	{
		Constant, Variable, Functor
	}

	public class Argument
	{
		private static string separator = ",";
		private ArgumentType type;
		private string name;
		private List<Argument> arguments;
		private static Regex rgxConstant = new Regex(@"^[A-Z0-9][a-zA-Z0-9]*$");
		private static Regex rgxVariable = new Regex(@"^[a-z][a-zA-Z0-9]*$");
		private static Regex rgxFunctor = new Regex(@"^[a-z][a-zA-Z0-9]*\([^\s]*\)$");

		public Argument(string argument)
		{
			if (rgxConstant.IsMatch(argument))
			{
				type = ArgumentType.Constant;
				name = argument;
			}
			else if (rgxVariable.IsMatch(argument))
			{
				type = ArgumentType.Variable;
				name = argument;
			}
			else if (rgxFunctor.IsMatch(argument))
			{
				type = ArgumentType.Functor;
				name = argument.Substring(0, argument.IndexOf("(", StringComparison.Ordinal));
				int prefixLength = name.Length + 1;
				arguments = ParseArguments(argument.Substring(prefixLength, argument.Length - prefixLength - 1));
			}
			else
			{
				throw new Exception($"Некорректный аргумент: {argument}");
			}
		}

		public override string ToString()
		{
			return type == ArgumentType.Functor ? $"{name}({string.Join(separator, arguments)})" : name;
		}

		public static bool Equals(Argument arg1, Argument arg2)
		{
			return string.Equals(arg1.ToString(), arg2.ToString());
		}

		public static List<Argument> ParseArguments(string arguments)
		{
			List<Argument> args = new List<Argument>();
			int len = 0;
			int pos = 0;
			int c = 0;
			while (pos < arguments.Length)
			{
				for (int j = pos; j < arguments.Length; j++)
				{
					if (arguments[j] == '(')
					{
						c++;
					}
					if ((arguments[j].ToString() == separator) && (c == 0))
					{
						break;
					}
					if (arguments[j] == ')')
					{
						c--;
					}
					len++;
					pos++;
				}
				args.Add(new Argument(arguments.Substring(pos - len, len)));
				len = 0;
				pos++;
			}
			return args;
		}
	}
}