using System.Collections.Generic;
using MLI.Method;

namespace MLI.Services
{
	public static class StatisticsService
	{
		private static List<StatElement> statistics = new List<StatElement>();
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
			if (process.GetInfoLevel() > SettingsService.InfoLevel)
			{
				return;
			}
			StatElement element = process.GetStatElement();
			if (element == null)
			{
				element = new StatElement().Build(process);
				switch (element.ProcessKind)
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
	}
}