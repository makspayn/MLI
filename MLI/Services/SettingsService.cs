namespace MLI.Services
{
	public static class SettingsService
	{
		public static int ExecUnitCount { get; set; } = 1;

		public static int UnifUnitCount { get; set; } = 1;

		public static int TickLength { get; set; } = 10;

		public static LogService.InfoLevel InfoLevel { get; set; } = LogService.InfoLevel.ProcessU;

		public static LogService.LogLevel LogLevel { get; set; } = LogService.LogLevel.Debug;
	}
}