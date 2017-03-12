using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using NLog;

namespace MLI.Method
{
	public class ProcessN : Process
	{
		public enum ProcessNStatus
		{
			ZeroRest, RestExists, NoRest
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();
		private List<Sequence> facts;
		private Sequence ruleSequence;
		private Sequence conclusionSequence;
		private Sequence rest;
		private ProcessNStatus processNStatus;

		public ProcessN(Process parentProcess, int index, List<Sequence> facts, Sequence ruleSequence, Sequence conclusionSequence) : base(parentProcess, index)
		{
			reentry = false;
			name = "N";
			this.facts = facts;
			this.ruleSequence = ruleSequence;
			this.conclusionSequence = conclusionSequence;
			logger.Debug($"[{GetName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			logger.Info($"[{GetName()}]: процесс запущен");
			logger.Info($"[{GetName()}]: правило {ruleSequence}");
			childProcesses.Add(new ProcessM(this, ++childProcessCount, ruleSequence, new List<Sequence>() { conclusionSequence }, false));
			status = Status.Progress;
			reentry = true;
			logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			logger.Info($"[{GetName()}]: процесс повторно запущен");
			List<Process> newChildProcesses = new List<Process>();
			if (childProcesses.Cast<ProcessM>()
				.All(childProcess => childProcess.GetProcessMStatus() != ProcessM.ProcessMStatus.ZeroRest))
			{
				foreach (ProcessM childProcess in childProcesses.Cast<ProcessM>())
				{
					if (childProcess.GetProcessMStatus() == ProcessM.ProcessMStatus.OnesMatrix)
					{
						if (childProcessCount == 1)
						{
							processNStatus = ProcessNStatus.NoRest;
							break;
						}
						processNStatus = ProcessNStatus.RestExists;
						if (FormRest(childProcess.GetRests()))
						{
							newChildProcesses.Clear();
							processNStatus = ProcessNStatus.ZeroRest;
							break;
						}
					}
					else
					{
						newChildProcesses.AddRange(childProcess.GetRests().Select(rest => new ProcessM(this, ++childProcessCount, rest, facts, true)));
					}
				}
			}
			else
			{
				rest = null;
				processNStatus = ProcessNStatus.ZeroRest;
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
				switch (processNStatus)
				{
					case ProcessNStatus.ZeroRest:
						logger.Info($"[{GetName()}]: получен нулевой остаток");
						break;
					case ProcessNStatus.RestExists:
						logger.Info($"[{GetName()}]: получен конченый остаток:\n{rest?.GetContent()}");
						break;
					case ProcessNStatus.NoRest:
						logger.Info($"[{GetName()}]: конечного остатка не получено");
						break;
				}
				status = Status.Complete;
				logger.Info($"[{GetName()}]: процесс завершен");
			}
		}

		private bool FormRest(List<Sequence> rests)
		{
			List<Sequence> fullRests = new List<Sequence>(rests);
			if (rest != null)
			{
				rests.Add(rest);
			}
			rest = Sequence.Minimize(Sequence.Multiply(fullRests));
			return rest == null;
		}

		public ProcessNStatus GetProcessNStatus()
		{
			return processNStatus;
		}
	}
}