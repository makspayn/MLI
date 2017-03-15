using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace MLI.Services
{
	public static class LogService
	{
		public enum LogLevel
		{
			Error, Info, Debug
		}

		public enum InfoLevel
		{
			All, ProcessV, ProcessN, ProcessM, ProcessU
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static DateTime startLogDate;

		public static void StartLog()
		{
			startLogDate = DateTime.Now.AddSeconds(-1);
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
					if (!canAdd && str != null && str.StartsWith(startLogDate.Date.ToString("yyyy-MM-dd")))
					{
						DateTime dt = DateTime.Parse(str.Substring(0, 24));
						canAdd = dt >= startLogDate;
					}
					if (canAdd)
					{
						log.Add(str);
					}
				}
				logReader.Close();
			}
			catch
			{
				// ignored
			}
			return log;
		}

		public static void Debug(string message)
		{
			Log(LogLevel.Debug, InfoLevel.All, message);
		}

		public static void Info(string message)
		{
			Log(LogLevel.Info, InfoLevel.All, message);
		}

		public static void Error(string message)
		{
			Log(LogLevel.Error, InfoLevel.All, message);
		}

		public static void Debug(InfoLevel infoLevel, string message)
		{
			Log(LogLevel.Debug, infoLevel, message);
		}

		public static void Info(InfoLevel infoLevel, string message)
		{
			Log(LogLevel.Info, infoLevel, message);
		}

		public static void Error(InfoLevel infoLevel, string message)
		{
			Log(LogLevel.Error, infoLevel, message);
		}

		private static void Log(LogLevel logLevel, InfoLevel infoLevel, string message)
		{
			if (logLevel > SettingsService.LogLevel)
			{
				return;
			}
			if (infoLevel > SettingsService.InfoLevel)
			{
				return;
			}
			switch (logLevel)
			{
				case LogLevel.Error:
					logger.Error(message);
					break;
				case LogLevel.Info:
					logger.Info(message);
					break;
				case LogLevel.Debug:
					logger.Debug(message);
					break;
			}
		}
	}
}