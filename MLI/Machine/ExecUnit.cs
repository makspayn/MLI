using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MLI.Data;
using MLI.Method;
using NLog;

namespace MLI.Machine
{
	public class ExecUnit : Processor
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private int number;
		private int unifUnitCount;
		private WorkSupervisor workSupervisor;
		private List<UnifUnit> unifUnits;
		private Process process;

		public ExecUnit(int number, int unifUnitCount)
		{
			this.number = number;
			this.unifUnitCount = unifUnitCount;
			BuildUnifUnits();
		}

		private void BuildUnifUnits()
		{
			unifUnits = new List<UnifUnit>();
			for (int i = 0; i < unifUnitCount; i++)
			{
				unifUnits.Add(new UnifUnit(i + 1));
			}
		}

		public void SetWorkSupervisor(WorkSupervisor workSupervisor)
		{
			this.workSupervisor = workSupervisor;
		}

		public int GetNumber() => number;

		public void RunProcess(Process process)
		{
			this.process = process;
			ReRun();
		}

		protected override void Run()
		{
			logger.Debug($"На {number} ИБ поступил процесс {process.GetName()}");
			process.Run();
			FormMessages(process);
		}

		private void FormMessages(Process process)
		{
			logger.Debug($"{number} ИБ формирует сообщения");
			if (process.GetStatus() == Process.Status.Progress)
			{
				List<Message> messages = process.GetChildProcesses().
					Select(childProcess =>
						new Message(childProcess, Message.MessageType.Create)).ToList();
				workSupervisor.AddMessages(messages, this);
			}
			else
			{
				workSupervisor.AddMessage(
					new Message(process, Message.MessageType.End), this);
			}
		}
	}
}