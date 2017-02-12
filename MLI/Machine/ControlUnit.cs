using MLI.Data;
using MLI.Method;

namespace MLI.Machine
{
	public class ControlUnit : Processor
	{
		protected Supervisor supervisor;
		protected FrameList frameList;
		protected Message message;

		public ControlUnit(string id) : base(id)
		{

		}

		public void SetSupervisor(Supervisor supervisor)
		{
			this.supervisor = supervisor;
			frameList = supervisor.GetFrameList();
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
				supervisor.AddProcess(process, this);
			}
			else
			{
				frameList.DeleteFrame(process);
				if (parentProcess != null)
				{
					if (frameList.DeleteChild(parentProcess))
					{
						supervisor.AddProcess(parentProcess, this);
					}
					else
					{
						SetBusyFlag(false);
						supervisor.TryGiveTasks();
					}
				}
				else
				{
					supervisor.CompleteWork();
					Machine.GetInstance().CompleteWork("Логический вывод завершен успешно!");
				}
			}
		}
	}
}