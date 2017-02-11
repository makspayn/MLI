using NLog;

namespace MLI.Method
{
	public class MainProcess : Process
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public MainProcess()
		{
			parentProcess = null;
			reentry = false;
			name = "Main";
			index = "";
			logger.Debug("Создан Main-процесс");
		}

		protected override void FirstRun()
		{
			logger.Debug("Запущен Main-процесс");
			for (int i = 0; i < 10; i++)
			{
				childProcesses.Add(new ProcessU(this));
			}
			status = Status.Progress;
			reentry = true;
			logger.Debug("Main-процесс завершен");
		}

		protected override void ReRun()
		{
			logger.Debug("Повторно запущен Main-процесс");
			childProcesses.Clear();
			status = Status.Success;
			logger.Debug("Main-процесс окончательно завершен");
		}
	}
}