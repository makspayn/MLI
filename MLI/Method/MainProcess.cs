using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using NLog;

namespace MLI.Method
{
	public class MainProcess : Process
	{
		public enum MainProcessStatus
		{
			Success, Failure
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();
		private List<Sequence> facts;
		private List<Sequence> rules;
		private Sequence conclusionSequence;
		private MainProcessStatus mainProcessStatus;

		public MainProcess(Process parentProcess, int index, List<Sequence> facts, List<Sequence> rules, Sequence conclusionSequence) : base(parentProcess, index)
		{
			reentry = false;
			name = "Main";
			this.index = "";
			this.facts = facts;
			this.rules = rules;
			this.conclusionSequence = conclusionSequence;
			logger.Debug($"[{GetName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			logger.Info($"[{GetName()}]: процесс запущен");
			logger.Info($"[{GetName()}]: выводимое правило {conclusionSequence}");
			childProcesses.Add(new ProcessV(this, ++childProcessCount, facts, rules, conclusionSequence));
			status = Status.Progress;
			reentry = true;
			logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			logger.Info($"[{GetName()}]: процесс повторно запущен");
			List<Process> newChildProcesses = new List<Process>();
			if (childProcesses.Cast<ProcessV>()
				.All(childProcess => childProcess.GetProcessVStatus() != ProcessV.ProcessVStatus.Success))
			{
				mainProcessStatus = MainProcessStatus.Failure;
				newChildProcesses.AddRange(from childProcess in childProcesses.Cast<ProcessV>()
										   where childProcess.GetProcessVStatus() == ProcessV.ProcessVStatus.Progress
										   from disjunct in childProcess.GetRest().GetDisjuncts()
										   select new ProcessV(this, ++childProcessCount, facts, rules, GetNewConclusion(disjunct)));
			}
			else
			{
				mainProcessStatus = MainProcessStatus.Success;
			}
			childProcesses.Clear();
			childProcesses.AddRange(newChildProcesses);
			if (childProcesses.Count != 0)
			{
				status = Status.Progress;
				logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
			}
			else
			{
				PrintStatus();
				status = Status.Complete;
				logger.Info($"[{GetName()}]: процесс завершен");
			}
		}

		private void PrintStatus()
		{
			switch (mainProcessStatus)
			{
				case MainProcessStatus.Success:
					logger.Info($"[{GetName()}]: логический вывод завершен успешно");
					break;
				case MainProcessStatus.Failure:
					logger.Info($"[{GetName()}]: логический вывод завершен неудачно");
					break;
			}
		}

		private Sequence GetNewConclusion(Disjunct disjunct)
		{
			return new Sequence($"1 -> {string.Join(" v ", disjunct.GetPredicates().Select(Predicate.GetInversPredicate).ToList())}");
		}
	}
}