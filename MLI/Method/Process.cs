using System.Collections.Generic;

namespace MLI.Method
{
	public abstract class Process
	{
		public enum Status
		{
			Progress, Complete
		}

		protected Process parentProcess;
		protected List<Process> childProcesses = new List<Process>();
		protected Status status;
		protected bool reentry;
		protected string name;
		protected string index;
		protected int childProcessCount;
		protected string inputData;
		protected string statusData;
		protected string resultData;

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

		public string GetName()
		{
			return name;
		}

		public string GetIndex()
		{
			return index;
		}

		public string GetFullName()
		{
			return name + index;
		}

		public string GetInputData()
		{
			return inputData;
		}

		public string GetStatusData()
		{
			return statusData;
		}

		public string GetResultData()
		{
			return resultData;
		}
	}
}