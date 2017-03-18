using System.Collections.Generic;
using MLI.Method;

namespace MLI.Services
{
	public static class StatisticsService
	{
		private static List<StatElement> statistics = new List<StatElement>();
		private static object TotalTimeSync = new object();
		private static object TotalTimeControlUnitSync = new object();
		public static int TotalTime { get; set; }
		public static int ProcessVCount { get; set; }
		public static int ProcessNCount { get; set; }
		public static int ProcessMCount { get; set; }
		public static int ProcessUCount { get; set; }
		public static int TotalTimeControlUnit { get; set; }
		public static int TotalTimeProcessUnit { get; set; }
		public static int TotalTimeUnifUnit { get; set; }
		public static int LoadProcessExecUnit { get; set; }
		public static int LoadProcessUnifUnit { get; set; }

		public static void Clear()
		{
			lock (statistics)
			{
				statistics.Clear();
				TotalTime = 0;
				ProcessVCount = 0;
				ProcessNCount = 0;
				ProcessMCount = 0;
				ProcessUCount = 0;
				TotalTimeControlUnit = 0;
				TotalTimeProcessUnit = 0;
				TotalTimeUnifUnit = 0;
				LoadProcessExecUnit = 0;
				LoadProcessUnifUnit = 0;
			}
		}

		public static void Add(Process process)
		{
			DefineLoadProcessUnits(process);
			if (process.GetInfoLevel() > SettingsService.InfoLevel)
			{
				return;
			}
			StatElement element = process.GetStatElement();
			if (element == null)
			{
				DefineProcessCount(process);
				element = new StatElement().Build(process);
				process.SetStatElement(element);
				lock (statistics)
				{
					statistics.Add(element);
				}
			}
			else
			{
				element.Build(process);
			}
		}

		public static List<StatElement> GetStatistics()
		{
			return statistics;
		}

		public static void PrepareStatistics()
		{
			lock (statistics)
			{
				statistics.Sort();
			}
		}

		public static void SetTotalTime(int time, bool unification)
		{
			lock (TotalTimeSync)
			{
				if (TotalTime < time)
				{
					if (unification)
					{
						TotalTimeUnifUnit += time - TotalTime;
					}
					else
					{
						TotalTimeProcessUnit += time - TotalTime;
					}
					TotalTime = time;
				}
			}
		}

		public static void SetTotalTimeControlUnit(int time)
		{
			lock (TotalTimeControlUnitSync)
			{
				if (TotalTime < time)
				{
					TotalTimeControlUnit = time;
				}
			}
		}

		private static void DefineProcessCount(Process process)
		{
			switch (process.GetName())
			{
				case "V":
					ProcessVCount++;
					break;
				case "N":
					ProcessNCount++;
					break;
				case "M":
					ProcessMCount++;
					break;
				case "U":
					ProcessUCount++;
					break;
			}
		}

		private static void DefineLoadProcessUnits(Process process)
		{
			int processExecUnitNumber = process.GetExecutions()[process.GetExecutions().Count - 1].ProcessExecUnitNumber;
			if (processExecUnitNumber > LoadProcessExecUnit)
			{
				LoadProcessExecUnit = processExecUnitNumber;
			}
			int processUnifUnitNumber = process.GetExecutions()[process.GetExecutions().Count - 1].ProcessUnifUnitNumber;
			if (processUnifUnitNumber > LoadProcessUnifUnit)
			{
				LoadProcessUnifUnit = processUnifUnitNumber;
			}
		}
	}
}