using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Services;

namespace MLI.Method
{
	public class ProcessV : Process
	{
		public enum ProcessVStatus
		{
			Success, Failure, Progress
		}
		
		private List<Sequence> facts;
		private List<Sequence> rules;
		private Sequence conclusionSequence;
		private Sequence rest;
		private ProcessVStatus processVStatus;

		public ProcessV(Process parentProcess, int index, List<Sequence> facts, List<Sequence> rules, Sequence conclusionSequence) : base(parentProcess, index)
		{
			infoLevel = LogService.InfoLevel.ProcessV;
			reentry = false;
			name = "V";
			inputData = $"выводимое правило {conclusionSequence}";
			this.facts = facts;
			this.rules = rules;
			this.conclusionSequence = conclusionSequence;
			LogService.Debug(LogService.InfoLevel.ProcessV, $"[{GetFullName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			Log("процесс запущен");
			Log(inputData);
			if (CanInference())
			{
				Log("вывод может быть осуществим");
				foreach (Sequence rule in rules)
				{
					childProcesses.Add(new ProcessN(this, ++childProcessCount, facts, rule, conclusionSequence));
				}
				status = Status.Progress;
				reentry = true;
				Log("ожидание завершения дочерних процессов");
			}
			else
			{
				Log("вывод не может быть осуществим");
				processVStatus = ProcessVStatus.Failure;
				PrintStatus();
				status = Status.Complete;
				Log("процесс завершен");
			}
		}

		protected override void ReRun()
		{
			Log("процесс повторно запущен");
			if (childProcesses.Cast<ProcessN>()
				.All(childProcess => childProcess.GetProcessNStatus() != ProcessN.ProcessNStatus.ZeroRest))
			{
				if (childProcesses.Cast<ProcessN>()
					.Any(childProcess => childProcess.GetProcessNStatus() == ProcessN.ProcessNStatus.RestExists))
				{
					foreach (ProcessN childProcess in childProcesses.Cast<ProcessN>())
					{
						switch (FormRest(childProcess.GetRest()))
						{
							case Sequence.SequenceState.Sequence:
								processVStatus = ProcessVStatus.Progress;
								break;
							case Sequence.SequenceState.One:
								processVStatus = ProcessVStatus.Failure;
								break;
							case Sequence.SequenceState.Zero:
								processVStatus = ProcessVStatus.Success;
								break;
						}
						if (processVStatus == ProcessVStatus.Success || processVStatus == ProcessVStatus.Failure)
						{
							rest = null;
							break;
						}
					}
				}
				else
				{
					processVStatus = ProcessVStatus.Failure;
				}
			}
			else
			{
				processVStatus = ProcessVStatus.Success;
			}
			PrintStatus();
			status = Status.Complete;
			Log("процесс завершен");
		}

		private bool CanInference()
		{
			bool canInference = false;
			foreach (Predicate conclusionPredicate in 
				from conclusionPredicate in conclusionSequence.GetPredicates()
				from fact in facts
				from factPredicate in fact.GetPredicates()
				where Predicate.CanUnify(conclusionPredicate, factPredicate)
				select conclusionPredicate)
			{
				canInference = true;
			}
			foreach (Predicate conclusionPredicate in
				from conclusionPredicate in conclusionSequence.GetPredicates()
				from rule in rules
				from rulePredicate in rule.GetPredicates()
				where Predicate.CanUnify(conclusionPredicate, rulePredicate)
				select conclusionPredicate)
			{
				canInference = true;
			}
			return canInference;
		}

		private Sequence.SequenceState FormRest(Sequence newRest)
		{
			List<Sequence> fullRests = new List<Sequence> { newRest };
			if (rest != null)
			{
				fullRests.Add(rest);
			}
			rest = Sequence.Minimize(Sequence.Multiply(fullRests));
			return Sequence.GetSequenceState(rest);
		}

		private void PrintStatus()
		{
			switch (processVStatus)
			{
				case ProcessVStatus.Success:
					statusData = "вывод завершен успешно";
					Log(statusData);
					break;
				case ProcessVStatus.Progress:
					statusData = "требуется продолжение вывода";
					resultData = $"Остаток: {rest.GetContent()}";
					Log($"{statusData}. {resultData}");
					break;
				case ProcessVStatus.Failure:
					statusData = "вывод завершен неудачно";
					Log(statusData);
					break;
			}
		}

		public ProcessVStatus GetProcessVStatus()
		{
			return processVStatus;
		}

		public Sequence GetRest()
		{
			return rest;
		}
	}
}