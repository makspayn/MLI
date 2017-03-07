using System.Collections.Generic;
using System.Text;
using MLI.Data;
using NLog;

namespace MLI.Method
{
	public class ProcessU : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private Predicate predicate1;
		private Predicate predicate2;
		private Substitution substitution = new Substitution();

		public ProcessU(Process parentProcess, int index, Predicate predicate1, Predicate predicate2) : base(parentProcess, index)
		{
			name = "U";
			this.predicate1 = new Predicate(predicate1.ToString());
			this.predicate2 = new Predicate(predicate2.ToString());
			logger.Debug($"[{GetName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			logger.Info($"[{GetName()}]: процесс запущен");
			logger.Info($"[{GetName()}]: унификация {predicate1} и {predicate2}");
			status = Predicate.CanUnify(predicate1, predicate2) ? 
				(Predicate.Equals(predicate1, predicate2) ? Status.Success : 
				GetUnificator(predicate1.GetArguments(), predicate2.GetArguments())) : 
				Status.Failure;
			switch (status)
			{
				case Status.Success:
					logger.Info($"[{GetName()}]: унификация выполнена полностью");
					break;
				case Status.Progress:
					logger.Info($"[{GetName()}]: унификация выполнена. Унификатор: {{ {substitution.GetSubstitutions()}}}");
					break;
				case Status.Failure:
					logger.Info($"[{GetName()}]: унификация невозможна");
					break;
			}
			logger.Info($"[{GetName()}]: процесс завершен");
		}

		protected override void ReRun()
		{
		}

		private Status GetUnificator(List<Argument> arguments1, List<Argument> arguments2)
		{
			Status status = Status.Progress;
			for (int i = 0; i < arguments1.Count; i++)
			{
				switch (arguments1[i].GetArgumentType())
				{
					case ArgumentType.Constant:
						switch (arguments2[i].GetArgumentType())
						{
							case ArgumentType.Constant:
								status = GetUnificatorConstantAndConstant(arguments1[i], arguments2[i]);
								break;
							case ArgumentType.Variable:
								status = GetUnificatorConstantAndVariable(arguments1[i], arguments2[i]);
								break;
							case ArgumentType.Functor:
								status = Status.Failure;
								break;
						}
						break;
					case ArgumentType.Variable:
						switch (arguments2[i].GetArgumentType())
						{
							case ArgumentType.Constant:
								status = GetUnificatorConstantAndVariable(arguments2[i], arguments1[i]);
								break;
							case ArgumentType.Variable:
								status = GetUnificatorVariableAndVariable(arguments1[i], arguments2[i]);
								break;
							case ArgumentType.Functor:
								status = GetUnificatorVariableAndFunctor(arguments1[i], arguments2[i]);
								break;
						}
						break;
					case ArgumentType.Functor:
						switch (arguments2[i].GetArgumentType())
						{
							case ArgumentType.Constant:
								status = Status.Failure;
								break;
							case ArgumentType.Variable:
								status = GetUnificatorVariableAndFunctor(arguments2[i], arguments1[i]);
								break;
							case ArgumentType.Functor:
								status = GetUnificatorFunctorAndFunctor(arguments1[i], arguments2[i]);
								break;
						}
						break;
				}
				if (status == Status.Failure)
				{
					break;
				}
			}
			return status;
		}

		private Status GetUnificatorConstantAndConstant(Argument constant1, Argument constant2)
		{
			return Argument.Equals(constant1, constant2) ? Status.Progress : Status.Failure;
		}

		private Status GetUnificatorConstantAndVariable(Argument constant, Argument variable)
		{
			substitution.AddSubstitution(variable, constant);
			DoUnification();
			return Status.Progress;
		}

		private Status GetUnificatorVariableAndVariable(Argument variable1, Argument variable2)
		{
			if (!Argument.Equals(variable1, variable2))
			{
				if (Argument.Contains(variable2, substitution.GetToArguments()))
				{
					substitution.AddSubstitution(variable2, variable1);
				}
				else
				{
					substitution.AddSubstitution(variable1, variable2);
				}
				DoUnification();
			}
			return Status.Progress;
		}

		private Status GetUnificatorVariableAndFunctor(Argument variable, Argument functor)
		{
			if (Argument.Contains(variable, functor.GetArguments()))
			{
				return Status.Failure;
			}
			substitution.AddSubstitution(variable, functor);
			DoUnification();
			return Status.Progress;
		}

		private Status GetUnificatorFunctorAndFunctor(Argument functor1, Argument functor2)
		{
			return Argument.CanUnify(functor1, functor2) ? 
				(Argument.Equals(functor1, functor2) ? 
				Status.Progress : GetUnificator(functor1.GetArguments(), functor2.GetArguments())) : 
				Status.Failure;
		}

		private void DoUnification()
		{
			Unify(substitution.GetToArguments(), substitution);
			Unify(predicate1.GetArguments(), substitution);
			Unify(predicate2.GetArguments(), substitution);
		}

		public static void Unify(List<Argument> arguments, Substitution substitution)
		{
			for (int i = 0; i < arguments.Count; i++)
			{
				for (int j = 0; j < substitution.GetFromArguments().Count; j++)
				{
					if (arguments[i].ToString() == substitution.GetFromArguments()[j].ToString())
					{
						arguments[i] = new Argument(substitution.GetToArguments()[j].ToString());
					}
				}
				Unify(arguments[i].GetArguments(), substitution);
			}
		} 

		public class Substitution
		{
			private List<Argument> fromArguments = new List<Argument>();
			private List<Argument> toArguments = new List<Argument>();

			public void AddSubstitution(Argument fromArgument, Argument toArgument)
			{
				fromArguments.Add(new Argument(fromArgument.ToString()));
				toArguments.Add(new Argument(toArgument.ToString()));
			}

			public List<Argument> GetFromArguments()
			{
				return fromArguments;
			}

			public List<Argument> GetToArguments()
			{
				return toArguments;
			}

			public string GetSubstitutions()
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < fromArguments.Count; i++)
				{
					sb.Append($"{fromArguments[i]}/{toArguments[i]}; ");
				}
				return sb.ToString();
			}
		}
	}
}