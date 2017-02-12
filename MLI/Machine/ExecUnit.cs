using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Method;
using NLog;

namespace MLI.Machine
{
	public class ExecUnit : ProcessUnit
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private int unifUnitCount;
		private Supervisor workExecUnitSupervisor;
		private List<ProcessUnit> unifUnits;
		private ControlUnifUnit controlUnifUnit;

		public ExecUnit(string id, int unifUnitCount) : base(id)
		{
			this.unifUnitCount = unifUnitCount;
			BuildWorkExecUnitSupervisor();
			BuildUnifUnits();
			BuildControlUnifUnit();
			ResolveDependencies();
		}

		private void BuildWorkExecUnitSupervisor()
		{
			workExecUnitSupervisor = new Supervisor();
		}

		private void BuildUnifUnits()
		{
			unifUnits = new List<ProcessUnit>();
			for (int i = 0; i < unifUnitCount; i++)
			{
				unifUnits.Add(new UnifUnit($"UU №{i + 1} ({id})"));
			}
		}

		private void BuildControlUnifUnit()
		{
			controlUnifUnit = new ControlUnifUnit($"CCU №1 ({id})", this);
		}

		private void ResolveDependencies()
		{
			workExecUnitSupervisor.SetProcessUnits(unifUnits);
			workExecUnitSupervisor.SetControlUnit(controlUnifUnit);
			foreach (ProcessUnit unifUnit in unifUnits)
			{
				unifUnit.SetSupervisor(workExecUnitSupervisor);
			}
			controlUnifUnit.SetSupervisor(workExecUnitSupervisor);
		}

		public new void CompleteWork()
		{
			workExecUnitSupervisor.CompleteWork();
			base.CompleteWork();
		}

		protected override void Run()
		{
			logger.Debug($"На {id} поступил процесс {process.GetName()}");
			process.Run();
			FormMessages(process);
		}

		private void FormMessages(Process process)
		{
			logger.Debug($"{id} выполнил процесс {process.GetName()}");
			if (process.GetStatus() == Process.Status.Progress)
			{
				List<Process> childProcesses = process.GetChildProcesses();
				List<Message> messages = childProcesses.Select(childProcess =>
						new Message(childProcess, Message.MessageType.Create)).ToList();
				if (childProcesses[0] is ProcessU)
				{
					controlUnifUnit.Init(process);
					workExecUnitSupervisor.AddMessages(messages, unifUnits[0]);
				}
				else
				{
					supervisor.AddMessages(messages, this);
				}
			}
			else
			{
				supervisor.AddMessage(
					new Message(process, Message.MessageType.End), this);
			}
		}

		private class UnifUnit : ProcessUnit
		{
			private static Logger logger = LogManager.GetCurrentClassLogger();

			public UnifUnit(string id) : base(id)
			{

			}

			protected override void Run()
			{
				logger.Debug($"На {id} поступил процесс {process.GetName()}");
				process.Run();
				FormMessages(process);
			}

			private void FormMessages(Process process)
			{
				logger.Debug($"{id} выполнил процесс {process.GetName()}");
				supervisor.AddMessage(
						new Message(process, Message.MessageType.End), this);
			}
		}

		private class ControlUnifUnit : ControlUnit
		{
			private ExecUnit execUnit;

			public ControlUnifUnit(string id, ExecUnit execUnit) : base(id)
			{
				this.execUnit = execUnit;
			}

			public void Init(Process process)
			{
				frameList.AddFrame(new Frame(new Message(process, Message.MessageType.Create)));
			}

			protected override void Run()
			{
				Process process = message.Process;
				Process parentProcess = message.ParentProcess;
				if (message.Type == Message.MessageType.Create)
				{
					frameList.AddFrame(new Frame(message));
					frameList.AddChild(parentProcess);
					supervisor.AddProcess(process, this);
				}
				else
				{
					frameList.DeleteFrame(process);
					if (frameList.DeleteChild(parentProcess))
					{
						frameList.DeleteFrame(parentProcess);
						execUnit.RunProcess(parentProcess);
					}
					else
					{
						SetBusyFlag(false);
						supervisor.TryGiveTasks();
					}
				}
			}
		}
	}
}