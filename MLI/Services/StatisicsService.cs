using System.Collections.Generic;
using MLI.Method;

namespace MLI.Services
{
	public static class StatisticsService
	{
		private static List<StatElement> statistics = new List<StatElement>();
		private static List<StatElement> sortStatistics = new List<StatElement>();
		public static int ProcessVCount { get; set; }
		public static int ProcessNCount { get; set; }
		public static int ProcessMCount { get; set; }
		public static int ProcessUCount { get; set; }

		public static void Clear()
		{
			lock (statistics)
			{
				statistics.Clear();
				sortStatistics.Clear();
				ProcessVCount = 0;
				ProcessNCount = 0;
				ProcessMCount = 0;
				ProcessUCount = 0;
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
				//int index = statistics.IndexOf(element);
				element = element.Build(process);
				process.SetStatElement(element);
				//statistics[index] = element;
			}
		}

		public static List<StatElement> GetStatistics()
		{
			return statistics;
		}

		public static void SortStatistics()
		{
			lock (statistics)
			{
				sortStatistics.AddRange(statistics);
			}
			sortStatistics.Sort();
		}

		public static List<StatElement> GetSortStatistics()
		{
			return sortStatistics;
		}
	}
}