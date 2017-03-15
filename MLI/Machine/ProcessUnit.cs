using MLI.Method;
using MLI.Services;

namespace MLI.Machine
{
	public abstract class ProcessUnit : Processor
	{
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
			LogService.Debug($"На {id} поступил процесс {process.GetFullName()}");
			process.Run();
			LogService.Debug($"{id} выполнил процесс {process.GetFullName()}");
			StatisticsService.Add(process, this);
			FormMessages(process);
		}

		protected abstract void FormMessages(Process process);
	}
}