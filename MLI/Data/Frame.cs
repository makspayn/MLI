using MLI.Method;

namespace MLI.Data
{
	public class Frame
	{
		public Process process { get; }
		public bool state { get; set; }
		public bool reentry { get; set; }
		public int childProcessCount { get; set; }

		public Frame(Process process)
		{
			this.process = process;
			state = false;
			reentry = false;
			childProcessCount = 0;
		}
	}
}