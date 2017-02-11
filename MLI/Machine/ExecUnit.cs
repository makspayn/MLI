using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MLI.Data;
using MLI.Method;
using NLog;

namespace MLI.Machine
{
	public class ExecUnit
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private int number;
		private int unifUnitCount;
		private WorkSupervisor workSupervisor;
		private List<UnifUnit> unifUnits;
		private bool busy;
		
		public ExecUnit(int number, int unifUnitCount)
		{
			this.number = number;
			this.unifUnitCount = unifUnitCount;
			busy = false;
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

		public void SetBusyFlag(bool busy)
		{
			this.busy = busy;
		}

		public bool IsBusy()
		{
			return busy;
		}

		public int GetNumber() => number;

		public void RunProcess(Process process)
		{
			logger.Debug($"На {number} ИБ поступил процесс {process.GetName()}");
			new Thread(() =>
			{
				process.Run();
				FormMessages(process);
			}).Start();
		}

		private void FormMessages(Process process)
		{
			//logger.Debug($"{number} ИБ формирует сообщения");
			if (process.GetStatus() == Process.Status.Progress)
			{
				List<Message> messages = process.GetChildProcesses()
					.Select(childProcess =>
						Message.GetCreateMessage(childProcess, process)).ToList();
				workSupervisor.AddMessages(messages, this);
			}
			else
			{
				workSupervisor.AddMessage(Message.GetEndMessage(process), this);
			}
		}
	}
}