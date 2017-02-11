using MLI.Method;

namespace MLI.Data
{
	public class Frame
	{
		public Process Process { get; }
		public Process ParentProcess { get; }
		public bool State { get; set; }
		public bool Reentry { get; set; }
		public int ChildProcessCount { get; set; }

		public Frame(Message message)
		{
			Process = message.Process;
			ParentProcess = message.ParentProcess;
			State = true;
			Reentry = false;
			ChildProcessCount = 0;
		}
	}
}