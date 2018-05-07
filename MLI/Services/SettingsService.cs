namespace MLI.Services
{
	public static class SettingsService
	{
		public static int UnitResourceCount { get; set; } = 9;
		
		public static int ExecUnitWeight { get; set; } = 8;

		public static int TickLength { get; set; } = 10;

		public static LogService.InfoLevel InfoLevel { get; set; } = LogService.InfoLevel.ProcessU;

		public static LogService.LogLevel LogLevel { get; set; } = LogService.LogLevel.Debug;
	}
}