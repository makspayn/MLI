using System.Collections.Generic;
using System.Linq;
using MLI.Machine;
using MLI.Method;

namespace MLI.Services
{
	public static class StatisticsService
	{
		private static List<StatElement> statistics = new List<StatElement>();

		public static void Clear()
		{
			statistics.Clear();
		}

		public static void Add(Process process, ProcessUnit processUnit)
		{
			FindStatElement(process).Build(process, processUnit);
		}

		private static StatElement FindStatElement(Process process)
		{
			foreach (StatElement statElement in statistics.Where(statElement => statElement.IndexEquals(process)))
			{
				return statElement;
			}
			StatElement element = new StatElement();
			statistics.Add(element);
			return element;
		}
	}
}