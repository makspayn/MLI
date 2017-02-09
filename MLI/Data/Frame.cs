using MLI.Method;

namespace MLI.Data
{
	public class Frame
	{
		public IProcess process { get; }
		public bool state { get; set; }
		public bool reentry { get; set; }
		public int childProcessCount { get; set; }

		public Frame(IProcess process)
		{
			this.process = process;
			state = false;
			reentry = false;
			childProcessCount = 0;
		}
	}
}