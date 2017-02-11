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
			//logger.Debug($"Запущен U-процесс ({index})");
			status = Status.Success;
			//logger.Debug($"U-процесс ({index}) окончательно завершен");
		}

		protected override void ReRun()
		{
			
		}
	}
}