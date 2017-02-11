using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MLI.Data;
using NLog;

namespace MLI.Machine
{
	public class ControlUnit
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private WorkSupervisor workSupervisor;
		private List<Frame> frameList; 
		private bool busy;

		public ControlUnit()
		{
			busy = false;
		}

		public void SetWorkSupervisor(WorkSupervisor workSupervisor)
		{
			this.workSupervisor = workSupervisor;
			frameList = workSupervisor.GetFrameList();
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
			new Thread(() =>
			{
				if (message.GetMessageTypeType() == Message.MessageType.Create)
				{
					Frame frame = new Frame(message.GetProcess());
					frameList.Add(frame);
					workSupervisor.AddProcess(message.GetProcess(), this);
				}
				else
				{

				}
			}).Start();
		}
	}
}