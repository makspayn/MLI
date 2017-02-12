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
			logger.Info("Main-процесс запущен");
			for (int i = 0; i < 10; i++)
			{
				childProcesses.Add(new ProcessU(this));
			}
			status = Status.Progress;
			reentry = true;
			logger.Info("Main-процесс ожидает завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			logger.Info("Main-процесс повторно запущен");
			childProcesses.Clear();
			status = Status.Success;
			logger.Info("Main-процесс завершен");
		}
	}
}