using System.Collections.Generic;
using MLI.Data;

namespace MLI.Machine
{
	public class ControlUnit
	{
		private List<Frame> frameList; 
		private bool busy;

		public ControlUnit(List<Frame> frameList)
		{
			this.frameList = frameList;
			busy = false;
		}

		public void SetBusyFlag(bool busy)
		{
			this.busy = busy;
		}

		public bool IsBusy()
		{
			return busy;
		}

		public void ProcessMessage(Message message)
		{
			
		}
	}
}