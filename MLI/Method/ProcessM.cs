using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Services;

namespace MLI.Method
{
	public class ProcessM : Process
	{
		public enum ProcessMStatus
		{
			ZeroRest, RestsExist, OnesMatrix
		}
		
		private Sequence ruleSequence;
		private List<Predicate> rulePredicates = new List<Predicate>();
		private List<Predicate> predicates = new List<Predicate>();
		private List<Sequence> rests = new List<Sequence>();
		private ProcessMStatus processMStatus;

		public ProcessM(Process parentProcess, int index, Sequence ruleSequence, List<Sequence> sequences, bool invers) : base(parentProcess, index)
		{
			infoLevel = LogService.InfoLevel.ProcessM;
			reentry = false;
			name = "M";
			inputData = $"правило {ruleSequence}";
			this.ruleSequence = ruleSequence;
			foreach (Predicate predicate in ruleSequence.GetPredicates())
			{
				rulePredicates.Add(new Predicate(predicate.ToString()));
			}
			foreach (Predicate predicate in sequences.SelectMany(seq => seq.GetPredicates()))
			{
				predicates.Add(invers ? 
					Predicate.GetInversPredicate(predicate) : 
					new Predicate(predicate.ToString()));
			}
			LogService.Debug(LogService.InfoLevel.ProcessM, $"[{GetFullName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			runTime += (predicates.Count + rulePredicates.Count) * processUnit.RunCommand(CommandId.ReadMemory);
			Log("процесс запущен");
			Log(inputData);
			runTime += processUnit.RunCommand(CommandId.FormPredicatePair, predicates.Count, rulePredicates.Count);
			foreach (Predicate predicate in predicates)
			{
				foreach (Predicate rulePredicate in rulePredicates)
				{
					runTime += processUnit.RunCommand(CommandId.CreateMessage);
					runTime += processUnit.RunCommand(CommandId.AddMessageToQueue);
					childProcesses.Add(new ProcessU(this, ++childProcessCount, rulePredicate, predicate));
				}
			}
			status = Status.Progress;
			reentry = true;
			Log("ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			Log("процесс повторно запущен");
			int restCount = childProcesses.Cast<ProcessU>().Count(
				childProcess => childProcess.GetProcessUStatus() == ProcessU.ProcessUStatus.Complete);
			if (restCount > 0)
			{
				processMStatus = ProcessMStatus.RestsExist;
				runTime += processUnit.RunCommand(CommandId.CreateRestsMatrix, restCount);
				for (int i = 0; i < childProcesses.Count; i++)
				{
					if (((ProcessU) childProcesses[i]).GetProcessUStatus() == ProcessU.ProcessUStatus.Complete)
					{
						rests.Add(FormRest(((ProcessU)childProcesses[i]).GetSubstitution(), i % rulePredicates.Count));
					}
				}
				runTime += processUnit.RunCommand(CommandId.FormRests, rests.Count);
				if (rests.Any(rest => rest.GetDisjuncts().Count == 0))
				{
					rests.Clear();
					processMStatus = ProcessMStatus.ZeroRest;
				}
			}
			else
			{
				processMStatus = childProcesses.Cast<ProcessU>().Any(
					childProcess => childProcess.GetProcessUStatus() == ProcessU.ProcessUStatus.Absolute) ?
					ProcessMStatus.ZeroRest : ProcessMStatus.OnesMatrix;
			}
			runTime += processUnit.RunCommand(CommandId.CreateMessage);
			runTime += processUnit.RunCommand(CommandId.AddMessageToQueue);
			runTime += rests.Count * processUnit.RunCommand(CommandId.WriteMemory);
			childProcesses.Clear();
			PrintStatus();
			status = Status.Complete;
			Log("процесс завершен");
		}

		private Sequence FormRest(ProcessU.Substitution substitution, int number)
		{
			string rest = "1 -> ";
			int i = 0;
			foreach (Predicate rulePredicate in rulePredicates)
			{
				ProcessU.Unify(rulePredicate.GetArguments(), substitution);
				if (i++ != number)
				{
					if (rest != "1 -> ")
					{
						rest += " v ";
					}
					rest += rulePredicate.ToString();
				}
			}
			return new Sequence(rest);
		}

		private string GetFormatRests()
		{
			return string.Join("; ", rests.Select(rest => rest.GetContent()).ToList());
		}

		private void PrintStatus()
		{
			switch (processMStatus)
			{
				case ProcessMStatus.ZeroRest:
					statusData = "получен нулевой остаток";
					Log(statusData);
					break;
				case ProcessMStatus.RestsExist:
					statusData = "получены остатки";
					resultData = $"Остатки: {GetFormatRests()}";
					Log($"{statusData}: {GetFormatRests()}");
					break;
				case ProcessMStatus.OnesMatrix:
					rests.Add(new Sequence(ruleSequence.ToString()));
					statusData = "получена единичная матрица";
					resultData = $"Остаток: {rests[0].GetContent()}";
					Log($"{statusData}. {resultData}");
					break;
			}
		}

		public ProcessMStatus GetProcessMStatus()
		{
			return processMStatus;
		}

		public List<Sequence> GetRests()
		{
			return rests;
		}
	}
}