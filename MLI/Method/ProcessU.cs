using System;
using System.Threading;
using NLog;

namespace MLI.Method
{
	public class ProcessU : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public ProcessU()
		{
			name = "U";
			index = new Random(GetHashCode() + DateTime.Now.Millisecond).Next(1000000).ToString();
			logger.Debug($"Создан U{index} процесс");
		}

		protected override void FirstRun()
		{
			//logger.Debug($"Запущен U-процесс ({index})");
			Thread.Sleep(100);
			status = Status.Success;
			//logger.Debug($"U-процесс ({index}) окончательно завершен");
		}

		protected override void ReRun()
		{
			
		}
	}
}