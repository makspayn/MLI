using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using NLog;
using KnowledgeBase = MLI.Machine.KnowledgeBase;

namespace MLI.Method
{
	public class MainProcess : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private KnowledgeBase knowledgeBase;

		public MainProcess(Process parentProcess, int index, KnowledgeBase knowledgeBase) : base(parentProcess, index)
		{
			reentry = false;
			name = "Main";
			this.index = "";
			this.knowledgeBase = knowledgeBase;
			logger.Debug($"[{GetName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			logger.Info($"[{GetName()}]: процесс запущен");
			foreach (Sequence rule in knowledgeBase.rules)
			{
				childProcesses.Add(new ProcessM(this, childProcessCount++, rule, knowledgeBase.conclusions, false));
			}
			status = Status.Progress;
			reentry = true;
			logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			logger.Info($"[{GetName()}]: процесс повторно запущен");
			List<Process> newChildProcesses = new List<Process>();
			foreach (ProcessM childProcess in childProcesses.Cast<ProcessM>())
			{
				if (childProcess.GetProcessMStatus() == ProcessM.ProcessMStatus.RestsExist)
				{
					foreach (Sequence rest in childProcess.GetRests())
					{
						newChildProcesses.Add(new ProcessM(this, childProcessCount++, rest, knowledgeBase.facts, true));
					}
				}
			}
			childProcesses.Clear();
			childProcesses.AddRange(newChildProcesses);
			if (childProcesses.Count == 0)
			{
				status = Status.Complete;
				logger.Info($"[{GetName()}]: процесс завершен");
			}
			else
			{
				status = Status.Progress;
				logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
			}
		}
	}
}