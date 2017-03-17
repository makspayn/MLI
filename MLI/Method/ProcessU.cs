using System.Collections.Generic;
using System.Linq;
using System.Text;
using MLI.Data;
using MLI.Services;

namespace MLI.Method
{
	public class ProcessU : Process
	{
		public enum ProcessUStatus
		{
			Absolute, Complete, Failure
		}
		
		private Predicate predicate1;
		private Predicate predicate2;
		private Substitution substitution = new Substitution();
		private ProcessUStatus processUStatus;

		public ProcessU(Process parentProcess, int index, Predicate predicate1, Predicate predicate2) : base(parentProcess, index)
		{
			infoLevel = LogService.InfoLevel.ProcessU;
			name = "U";
			inputData = $"унификация {predicate1} и {predicate2}";
			this.predicate1 = new Predicate(predicate1.ToString());
			this.predicate2 = new Predicate(predicate2.ToString());
			LogService.Debug(LogService.InfoLevel.ProcessU, $"[{GetFullName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			Log("процесс запущен");
			Log(inputData);
			processUStatus = Predicate.CanUnify(predicate1, predicate2) ? 
				(Predicate.Equals(predicate1, predicate2) ? ProcessUStatus.Absolute : 
				GetUnificator(predicate1.GetArguments(), predicate2.GetArguments())) :
				ProcessUStatus.Failure;
			PrintStatus();
			status = Status.Complete;
			Log("процесс завершен");
		}

		protected override void ReRun()
		{
		}

		private void PrintStatus()
		{
			switch (processUStatus)
			{
				case ProcessUStatus.Absolute:
					statusData = "унификация выполнена полностью";
					Log(statusData);
					break;
				case ProcessUStatus.Complete:
					statusData = "унификация выполнена";
					resultData = $"Унификатор: {{ {substitution.GetSubstitutions()} }}";
					Log($"{statusData}. {resultData}");
					break;
				case ProcessUStatus.Failure:
					statusData = "унификация невозможна";
					Log(statusData);
					break;
			}
		}

		private ProcessUStatus GetUnificator(List<Argument> arguments1, List<Argument> arguments2)
		{
			ProcessUStatus status = ProcessUStatus.Complete;
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
								status = ProcessUStatus.Failure;
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
								status = ProcessUStatus.Failure;
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
				if (status == ProcessUStatus.Failure)
				{
					break;
				}
			}
			return status;
		}

		private ProcessUStatus GetUnificatorConstantAndConstant(Argument constant1, Argument constant2)
		{
			return Argument.Equals(constant1, constant2) ? ProcessUStatus.Complete : ProcessUStatus.Failure;
		}

		private ProcessUStatus GetUnificatorConstantAndVariable(Argument constant, Argument variable)
		{
			substitution.AddSubstitution(variable, constant);
			DoUnification();
			return ProcessUStatus.Complete;
		}

		private ProcessUStatus GetUnificatorVariableAndVariable(Argument variable1, Argument variable2)
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
			return ProcessUStatus.Complete;
		}

		private ProcessUStatus GetUnificatorVariableAndFunctor(Argument variable, Argument functor)
		{
			if (Argument.Contains(variable, functor.GetArguments()))
			{
				return ProcessUStatus.Failure;
			}
			substitution.AddSubstitution(variable, functor);
			DoUnification();
			return ProcessUStatus.Complete;
		}

		private ProcessUStatus GetUnificatorFunctorAndFunctor(Argument functor1, Argument functor2)
		{
			return Argument.CanUnify(functor1, functor2) ? 
				(Argument.Equals(functor1, functor2) ?
				ProcessUStatus.Complete : GetUnificator(functor1.GetArguments(), functor2.GetArguments())) :
				ProcessUStatus.Failure;
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

		public Substitution GetSubstitution()
		{
			return substitution;
		}

		public ProcessUStatus GetProcessUStatus()
		{
			return processUStatus;
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
				return string.Join("; ", fromArguments.Select(
					(t, i) => string.Join("/", t.ToString(), toArguments[i].ToString())).ToList());
			}
		}
	}
}