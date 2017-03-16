using System;
using System.Collections.Generic;
using MLI.Method;

namespace MLI.Services
{
	public class Execution
	{
		public string ProcessUnitName { get; set; }

		public int ProcessUnitNumber { get; set; }

		public int ReadyTime { get; set; }

		public int RunTime { get; set; }

		public int WaitTime { get; set; }
	}

	public class StatElement : IComparable<StatElement>
	{
		public string ProcessFullName => ProcessKind + ProcessIndex;

		public string ProcessKind { get; set; }

		public string ProcessIndex { get; set; }

		public int ProcessVIndex { get; set; }

		public int ProcessMIndex { get; set; }

		public int ProcessNIndex { get; set; }

		public int ProcessUIndex { get; set; }

		public string InputData { get; set; }

		public string StatusData { get; set; }

		public string ResultData { get; set; }

		private List<Execution> executions;

		public List<Execution> GetExecutions()
		{
			return executions;
		}

		public StatElement Build(Process process)
		{
			ProcessKind = process.GetName();
			ProcessIndex = process.GetIndex();
			ProcessVIndex = GetProcessIndex(process, 0);
			ProcessNIndex = GetProcessIndex(process, 1);
			ProcessMIndex = GetProcessIndex(process, 2);
			ProcessUIndex = GetProcessIndex(process, 3);
			InputData = process.GetInputData();
			StatusData = process.GetStatusData();
			ResultData = process.GetResultData();
			executions = process.GetExecutions();
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

		public int CompareTo(StatElement other)
		{
			if (ProcessVIndex == other.ProcessVIndex)
			{
				if (ProcessNIndex == other.ProcessNIndex)
				{
					return ProcessMIndex == other.ProcessMIndex ?
						ProcessUIndex.CompareTo(other.ProcessUIndex) :
						ProcessMIndex.CompareTo(other.ProcessMIndex);
				}
				return ProcessNIndex.CompareTo(other.ProcessNIndex);
			}
			return ProcessVIndex.CompareTo(other.ProcessVIndex);
		}
	}
}