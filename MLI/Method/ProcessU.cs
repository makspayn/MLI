using System;
using NLog;

namespace MLI.Method
{
	public class ProcessU : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public ProcessU(Process parentProcess)
		{
			this.parentProcess = parentProcess;
			name = "U";
			index = new Random(GetHashCode() + DateTime.Now.Millisecond).Next(1000000).ToString();
			logger.Debug($"Создан U{index} процесс");
		}

		protected override void FirstRun()
		{
			logger.Info($"U-процесс ({index}) запущен");
			status = Status.Success;
			logger.Info($"U-процесс ({index}) завершен");
		}

		protected override void ReRun()
		{
			
		}
	}
}