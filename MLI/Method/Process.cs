using System.Collections.Generic;

namespace MLI.Method
{
	public abstract class Process
	{
		public enum Status
		{
			Success, Progress, Failure
		}

		protected List<Process> childProcesses = new List<Process>();
		protected Status status;
		protected bool reentry;

		public void Run()
		{
			if (!reentry)
			{
				FirstRun();
			}
			else
			{
				ReRun();
			}
		}

		protected abstract void FirstRun();

		protected abstract void ReRun();

		public List<Process> GetChildProcesses()
		{
			return childProcesses;
		}

		public Status GetStatus()
		{
			return status;
		}

		public bool GetReentry()
		{
			return reentry;
		}
	}
}