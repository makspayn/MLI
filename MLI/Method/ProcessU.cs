using System;
using NLog;

namespace MLI.Method
{
	public class ProcessU : IProcess
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private int id;

		public ProcessU()
		{
			id = new Random().Next(1000000);
			logger.Debug($"Создан U-процесс ({id})");
		}

		public void Run()
		{
			logger.Debug($"Запущен U-процесс ({id})");
		}
	}
}