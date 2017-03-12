using System.Collections.Generic;
using System.Linq;
using System.Text;
using MLI.Data;
using NLog;

namespace MLI.Method
{
	public class ProcessM : Process
	{
		public enum ProcessMStatus
		{
			ZeroRest, RestsExist, OnesMatrix
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();
		private Sequence ruleSequence;
		private List<Predicate> rulePredicates = new List<Predicate>();
		private List<Predicate> predicates = new List<Predicate>();
		private List<Sequence> rests = new List<Sequence>();
		private ProcessMStatus processMStatus;

		public ProcessM(Process parentProcess, int index, Sequence ruleSequence, List<Sequence> sequences, bool invers) : base(parentProcess, index)
		{
			reentry = false;
			name = "M";
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
			logger.Debug($"[{GetName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			logger.Info($"[{GetName()}]: процесс запущен");
			logger.Info($"[{GetName()}]: правило {ruleSequence}");
			foreach (Predicate rulePredicate in rulePredicates)
			{
				foreach (Predicate predicate in predicates)
				{
					childProcesses.Add(new ProcessU(this, ++childProcessCount, rulePredicate, predicate));
				}
			}
			status = Status.Progress;
			reentry = true;
			logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			logger.Info($"[{GetName()}]: процесс повторно запущен");
			int restCount = childProcesses.Cast<ProcessU>().Count(
				childProcess => childProcess.GetProcessUStatus() == ProcessU.ProcessUStatus.Complete);
			if (restCount > 0)
			{
				processMStatus = ProcessMStatus.RestsExist;
				foreach (ProcessU childProcess in childProcesses.Cast<ProcessU>().Where(
					childProcess => childProcess.GetProcessUStatus() == ProcessU.ProcessUStatus.Complete))
				{
					rests.Add(FormRest(childProcess.GetSubstitution(), childProcesses.IndexOf(childProcess) % rulePredicates.Count));
				}
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
			childProcesses.Clear();
			switch (processMStatus)
			{
				case ProcessMStatus.ZeroRest:
					logger.Info($"[{GetName()}]: получен нулевой остаток");
					break;
				case ProcessMStatus.RestsExist:
					logger.Info($"[{GetName()}]: получены остатки:\n{GetFormatRests()}");
					break;
				case ProcessMStatus.OnesMatrix:
					rests.Add(new Sequence(ruleSequence.ToString()));
					logger.Info($"[{GetName()}]: получена единичная матрица. Остаток: {rests[0].GetContent()}");
					break;
			}
			status = Status.Complete;
			logger.Info($"[{GetName()}]: процесс завершен");
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
			StringBuilder sb = new StringBuilder();
			int i = 1;
			foreach (Sequence rest in rests)
			{
				sb.Append($"{i++}) {rest.GetContent()}\n");
			}
			return sb.ToString();
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