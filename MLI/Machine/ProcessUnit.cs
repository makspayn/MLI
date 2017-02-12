using MLI.Method;

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
	}
}