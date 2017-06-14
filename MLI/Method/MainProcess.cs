using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Services;

namespace MLI.Method
{
	public class MainProcess : Process
	{
		public enum MainProcessStatus
		{
			Success, Failure
		}
		
		private List<Sequence> facts;
		private List<Sequence> rules;
		private Sequence conclusionSequence;
		private MainProcessStatus mainProcessStatus;

		public MainProcess(Process parentProcess, int index, List<Sequence> facts, List<Sequence> rules, Sequence conclusionSequence) : base(parentProcess, index)
		{
			infoLevel = LogService.InfoLevel.All;
			reentry = false;
			name = "Main";
			inputData = $"выводимое правило {conclusionSequence}";
			this.index = "";
			this.facts = facts;
			this.rules = rules;
			this.conclusionSequence = conclusionSequence;
			LogService.Debug($"[{GetFullName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			Log("процесс запущен");
			LogService.Info($"[{GetFullName()}]: {inputData}");
			runTime += processUnit.RunCommand(Command.CreateMessage);
			runTime += processUnit.RunCommand(Command.AddMessageToQueue);
			childProcesses.Add(new ProcessV(this, ++childProcessCount, facts, rules, conclusionSequence));
			status = Status.Progress;
			reentry = true;
			Log("ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			Log("процесс повторно запущен");
			Log(inputData);
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
				runTime += childProcesses.Count * processUnit.RunCommand(Command.CreateMessage);
				runTime += childProcesses.Count * processUnit.RunCommand(Command.AddMessageToQueue);
				status = Status.Progress;
				Log("ожидание завершения дочерних процессов");
			}
			else
			{
				runTime += processUnit.RunCommand(Command.CreateMessage);
				runTime += processUnit.RunCommand(Command.AddMessageToQueue);
				PrintStatus();
				status = Status.Complete;
				Log("процесс завершен");
			}
		}

		private void PrintStatus()
		{
			switch (mainProcessStatus)
			{
				case MainProcessStatus.Success:
					statusData = "логический вывод завершен успешно";
					break;
				case MainProcessStatus.Failure:
					statusData = "логический вывод завершен неудачно";
					break;
			}
			Log(statusData);
		}

		

		private Sequence GetNewConclusion(Disjunct disjunct)
		{
			runTime += processUnit.RunCommand(Command.FormConclusion, 1);
			Sequence sequence =
				new Sequence($"1 -> {string.Join(" v ", disjunct.GetPredicates().Select(Predicate.GetInversPredicate).ToList())}");
			runTime += processUnit.RunCommand(Command.MinimizeSequence, sequence.GetDisjuncts().Count);
			return Minimizer.Minimize(sequence);
		}
	}
}