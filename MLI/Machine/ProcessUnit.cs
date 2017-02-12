using MLI.Method;
using NLog;

namespace MLI.Machine
{
	public abstract class ProcessUnit : Processor
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		protected Supervisor supervisor;
		protected Process process;

		protected ProcessUnit(string id) : base(id)
		{

		}

		public void SetSupervisor(Supervisor supervisor)
		{
			this.supervisor = supervisor;
		}

		public void RunProcess(Process process)
		{
			this.process = process;
			ReRun();
		}

		protected override void Run()
		{
			logger.Debug($"На {id} поступил процесс {process.GetName()}");
			process.Run();
			logger.Debug($"{id} выполнил процесс {process.GetName()}");
			FormMessages(process);
		}

		protected abstract void FormMessages(Process process);
	}
}