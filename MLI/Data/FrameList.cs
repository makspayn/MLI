using System.Collections.Generic;
using System.Linq;
using MLI.Method;

namespace MLI.Data
{
	public class FrameList
	{
		private List<Frame> frameList;

		public FrameList()
		{
			frameList = new List<Frame>();
		}

		public void AddFrame(Frame frame)
		{
			lock (frameList)
			{
				frameList.Add(frame);
			}
		}

		public void AddChild(Process process)
		{
			Frame frame = FindFrame(process);
			lock (frameList)
			{
				frame.ChildProcessCount++;
				frame.Reentry = true;
				frame.State = false;
			}
		}

		public void DeleteFrame(Process process)
		{
			Frame frame = FindFrame(process);
			lock (frameList)
			{
				frameList.Remove(frame);
			}
		}

		public bool DeleteChild(Process process)
		{
			bool needRun;
			Frame frame = FindFrame(process);
			lock (frameList)
			{
				needRun = --frame.ChildProcessCount == 0;
				frame.Reentry = !needRun;
				frame.State = needRun;
			}
			return needRun;
		}

		private Frame FindFrame(Process process)
		{
			lock (frameList)
			{
				return frameList.FirstOrDefault(frame => frame.Process == process);
			}
		}
	}
}