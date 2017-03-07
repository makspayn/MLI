﻿using MLI.Data;
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
			int i = 1;
			foreach (Sequence rule in knowledgeBase.rules)
			{
				foreach (Sequence conclusion in knowledgeBase.conclusions)
				{
					foreach (Predicate rulePredicate in rule.GetPredicates())
					{
						foreach (Predicate conclusionPredicate in conclusion.GetPredicates())
						{
							childProcesses.Add(new ProcessU(this, i++, rulePredicate, conclusionPredicate));
						}
					}
				}
			}
			status = Status.Progress;
			reentry = true;
			logger.Info($"[{GetName()}]: ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			logger.Info($"[{GetName()}]: процесс повторно запущен");
			childProcesses.Clear();
			status = Status.Success;
			logger.Info($"[{GetName()}]: процесс завершен");
		}
	}
}