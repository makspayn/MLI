﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NLog;

namespace MLI.Data
{
	public enum ArgumentType
	{
		Constant, Variable, Functor
	}

	public class Argument
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static Regex rgxConstant = new Regex(@"^[A-Z0-9][a-zA-Z0-9]*$");
		private static Regex rgxVariable = new Regex(@"^[a-z][a-zA-Z0-9]*$");
		private static Regex rgxFunctor = new Regex(@"^[a-z][a-zA-Z0-9]*\([^\s]*\)$");
		private static string separator = ",";
		private ArgumentType type;
		private string name;
		private List<Argument> arguments;

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
				logger.Error($"Некорректный аргумент: {argument}");
				throw new Exception($"Некорректный аргумент: {argument}");
			}
		}

		public override string ToString()
		{
			return type == ArgumentType.Functor ? $"{name}({string.Join(separator, arguments)})" : name;
		}

		public static bool Equals(Argument argument1, Argument argument2)
		{
			return string.Equals(argument1.ToString(), argument2.ToString());
		}

		public ArgumentType GetArgumentType()
		{
			return type;
		}

		public List<Argument> GetArguments()
		{
			return arguments ?? new List<Argument>();
		}

		public static bool Contains(Argument argument, List<Argument> arguments)
		{
			return arguments.Any(arg => Equals(arg, argument) || Contains(arg, arg.GetArguments()));
		}

		public static bool CanUnify(Argument argument1, Argument argument2)
		{
			return argument1.type == argument2.type && argument1.name == argument2.name &&
				   argument1.GetArguments().Count == argument2.GetArguments().Count;
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