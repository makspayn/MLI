using NLog;

namespace MLI.Method
{
	public class MainProcess : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public MainProcess()
		{
			logger.Debug("Создан Main-процесс");
			reentry = false;
		}

		protected override void FirstRun()
		{
			logger.Debug("Запущен Main-процесс");
			for (int i = 0; i < 10; i++)
			{
				childProcesses.Add(new ProcessU());
			}
			status = Status.Progress;
			reentry = true;
		}

		protected override void ReRun()
		{
			logger.Debug("Повторно запущен Main-процесс");
			childProcesses.Clear();
			status = Status.Success;
		}
	}
}