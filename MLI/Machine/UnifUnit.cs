using NLog;

namespace MLI.Machine
{
	public class UnifUnit
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private int number;

		public UnifUnit(int number)
		{
			this.number = number;
		}

		public int GetNumber() => number;
	}
}