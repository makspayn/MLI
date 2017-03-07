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

		protected Process(Process parentProcess, int index)
		{
			this.parentProcess = parentProcess;
			this.index = index.ToString();
			if (!string.IsNullOrEmpty(parentProcess?.GetIndex()))
			{
				this.index = $"{parentProcess.GetIndex()}.{index}";
			}
		}

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

		public string GetIndex()
		{
			return index;
		}

		public string GetName()
		{
			return name + index;
		}
	}
}