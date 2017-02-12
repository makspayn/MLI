using System;
using System.Collections.Generic;
using System.IO;

namespace MLI.Services
{
	public static class LogService
	{
		private static DateTime startLogDate;

		public static void StartLog()
		{
			startLogDate = DateTime.Now;
		}

		public static List<string> GetLog()
		{
			List<string> log = new List<string>();
			string logPath = $"logs//info//mli.{startLogDate.Date.ToString("yyyy-MM-dd")}.log";
			try
			{
				StreamReader logReader = File.OpenText(logPath);
				bool canAdd = false;
				while (!logReader.EndOfStream)
				{
					string str = logReader.ReadLine();
					if (!canAdd && (str != null && str.StartsWith(startLogDate.Date.ToString("yyyy-MM-dd"))))
					{
						DateTime dt = DateTime.Parse(str.Substring(0, 24));
						canAdd = dt >= startLogDate;
					}
					if (canAdd)
					{
						log.Add(str);
					}
				}
			}
			catch
			{
				// ignored
			}
			return log;
		}
	}
}