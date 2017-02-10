using System;
using System.Collections.Generic;
using NLog;

namespace MLI.Method
{
	public class ProcessU : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private string index;

		public ProcessU()
		{
			index = new Random().Next(1000000).ToString();
			logger.Debug($"Создан U-процесс ({index})");
		}

		protected override void FirstRun()
		{
			logger.Debug($"Запущен U-процесс ({index})");
			status = Status.Success;
		}

		protected override void ReRun()
		{
			
		}
	}
}