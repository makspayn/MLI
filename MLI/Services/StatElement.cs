using System.Collections.Generic;
using MLI.Machine;
using MLI.Method;

namespace MLI.Services
{
	public class Execution
	{
		public string ProcessUnitName { get; set; }

		public int ProcessUnitNumber { get; set; }

		public int readyTime { get; set; }

		public int runTime { get; set; }

		public int waitTime { get; set; }
	}

	public class StatElement
	{
		public string ProcessKind { get; set; }

		public int ProcessVIndex { get; set; }

		public int ProcessMIndex { get; set; }

		public int ProcessNIndex { get; set; }

		public int ProcessUIndex { get; set; }

		public string InputData { get; set; }

		public string StatusData { get; set; }

		public string ResultData { get; set; }

		private List<Execution> executions = new List<Execution>();

		public List<Execution> GetExecutions()
		{
			return executions;
		}

		public bool IndexEquals(Process process)
		{
			return false;
		}

		public StatElement Build(Process process, ProcessUnit processUnit)
		{
			ProcessKind = process.GetName();
			ProcessVIndex = GetProcessIndex(process, 0);
			ProcessMIndex = GetProcessIndex(process, 1);
			ProcessNIndex = GetProcessIndex(process, 2);
			ProcessUIndex = GetProcessIndex(process, 3);
			InputData = process.GetInputData();
			StatusData = process.GetStatusData();
			ResultData = process.GetResultData();
			executions.Add(new Execution
			{
				ProcessUnitName = processUnit.GetId()
			});
			return this;
		}

		private int GetProcessIndex(Process process, int index)
		{
			if (string.IsNullOrEmpty(process.GetIndex()))
			{
				return 0;
			}
			string[] indexes = process.GetIndex().Split('.');
			return indexes.Length > index ? int.Parse(indexes[index]) : 0;
		}
	}
}