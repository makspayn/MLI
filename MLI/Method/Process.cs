using System.Collections.Generic;

namespace MLI.Method
{
	public abstract class Process
	{
		public enum Status
		{
			Success, Progress, Failure
		}

		protected Process parentProcess;
		protected List<Process> childProcesses = new List<Process>();
		protected Status status;
		protected bool reentry;
		protected string name;
		protected string index;

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

		public Process GetParentProcess()
		{
			return parentProcess;
		}

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

		public string GetName()
		{
			return name + index;
		}
	}
}