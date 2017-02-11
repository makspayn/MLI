using MLI.Data;
using MLI.Method;
using NLog;

namespace MLI.Machine
{
	public class ControlUnit : Processor
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private WorkSupervisor workSupervisor;
		private FrameList frameList;
		Message message;

		public void SetWorkSupervisor(WorkSupervisor workSupervisor)
		{
			this.workSupervisor = workSupervisor;
			frameList = workSupervisor.GetFrameList();
		}

		public void ProcessMessage(Message message)
		{
			this.message = message;
			ReRun();
		}

		protected override void Run()
		{
			Process process = message.Process;
			Process parentProcess = message.ParentProcess;
			if (message.Type == Message.MessageType.Create)
			{
				frameList.AddFrame(new Frame(message));
				if (parentProcess != null)
				{
					frameList.AddChild(parentProcess);
				}
				workSupervisor.AddProcess(process, this);
			}
			else
			{
				frameList.DeleteFrame(process);
				if (parentProcess != null)
				{
					if (frameList.DeleteChild(parentProcess))
					{
						workSupervisor.AddProcess(parentProcess, this);
					}
					else
					{
						SetBusyFlag(false);
						workSupervisor.TryGiveTasks();
					}
				}
				else
				{
					//стоп машина
				}
			}
		}
	}
}