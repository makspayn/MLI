using System.Collections.Generic;
using MLI.Machine;
using MLI.Services;

namespace MLI.Method
{
	public abstract class Process
	{
		public enum Status
		{
			Progress, Complete
		}

		protected LogService.InfoLevel infoLevel;
		protected Process parentProcess;
		protected List<Process> childProcesses = new List<Process>();
		protected Status status;
		protected bool reentry;
		protected string name;
		protected string index;
		protected int childProcessCount;
		protected string inputData = "";
		protected string statusData = "";
		protected string resultData = "";
		protected int runTime;
		protected ProcessUnit processUnit;
		protected StatElement statElement;
		protected List<Execution> executions = new List<Execution>();

		protected Process(Process parentProcess, int index)
		{
			this.parentProcess = parentProcess;
			this.index = index.ToString();
			if (!string.IsNullOrEmpty(parentProcess?.GetIndex()))
			{
				this.index = $"{parentProcess.GetIndex()}.{index}";
			}
		}

		public void Run(ProcessUnit processUnit)
		{
			this.processUnit = processUnit;
			if (!reentry)
			{
				FirstRun();
			}
			else
			{
				ReRun();
			}
			Execution execution = new Execution();
			execution.ProcessUnitName = processUnit.GetId();
			execution.ProcessExecUnitNumber = int.Parse(processUnit.GetId().Substring(processUnit.GetId().IndexOf('№') + 1).Split(' ')[0]);
			if (processUnit.GetId().Contains("("))
			{
				execution.ProcessUnifUnitNumber = execution.ProcessExecUnitNumber;
				string processUnitName = processUnit.GetId().Substring(processUnit.GetId().IndexOf('(') + 1).Split(')')[0];
				execution.ProcessExecUnitNumber = int.Parse(processUnitName.Substring(processUnit.GetId().IndexOf('№') + 1).Split(' ')[0]);
			}
			execution.RunTime = runTime;
			executions.Add(execution);
		}

		protected abstract void FirstRun();

		protected abstract void ReRun();

		public LogService.InfoLevel GetInfoLevel()
		{
			return infoLevel;
		}

		public Process GetParentProcess()
		{
			return parentProcess;
		}

		public List<Process> GetChildProcesses()
		{
			return childProcesses;
		}

		public List<Execution> GetExecutions()
		{
			return executions;
		}

		public void SetStatElement(StatElement statElement)
		{
			this.statElement = statElement;
		}

		public StatElement GetStatElement()
		{
			return statElement;
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

		protected void Log(string message)
		{
			LogService.Info(infoLevel, $"[{GetFullName()}]: {message}");
		}
	}
}