using MLI.Data;
using MLI.Method;
using MLI.Services;

namespace MLI.Machine
{
	public class ControlUnit : Processor
	{
		protected Supervisor supervisor;
		protected FrameList frameList;
		protected Message message;

		public ControlUnit(string name, int number) : base(name, number)
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
				RunCommand(CommandId.CreateFrame);
				RunCommand(CommandId.AddFrameToList);
				frameList.AddFrame(new Frame(message));
				if (parentProcess != null)
				{
					frameList.AddChild(parentProcess);
				}
				RunCommand(CommandId.DeleteMessageFromQueue);
				RunCommand(CommandId.CreateProcess);
				supervisor.AddProcess(process, this);
			}
			else
			{
				frameList.DeleteFrame(process);
				RunCommand(CommandId.DeleteFrameFromList);
				if (parentProcess != null)
				{
					RunCommand(CommandId.DeleteMessageFromQueue);
					if (frameList.DeleteChild(parentProcess))
					{
						RunCommand(CommandId.CreateProcess);
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
			StatisticsService.SetTotalTimeControlUnit(runTime);
		}
	}
}