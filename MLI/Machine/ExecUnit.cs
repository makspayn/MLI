using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Method;

namespace MLI.Machine
{
	public class ExecUnit
	{
		private int unifUnitCount;
		private WorkSupervisor workSupervisor;
		private List<UnifUnit> unifUnits;
		private bool busy;

		public ExecUnit(int unifUnitCount)
		{
			this.unifUnitCount = unifUnitCount;
			busy = false;
			BuildUnifUnits();
		}

		private void BuildUnifUnits()
		{
			unifUnits = new List<UnifUnit>();
			for (int i = 0; i < unifUnitCount; i++)
			{
				unifUnits.Add(new UnifUnit());
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

		public void RunProcess(Process process)
		{
			process.Run();
			List<Message> messages;
			if (process.GetStatus() == Process.Status.Progress)
			{
				messages = process.GetChildProcesses()
					.Select(childProcess =>
						Message.GetCreateMessage(childProcess, process)).ToList();
			}
			else
			{
				messages = new List<Message> { Message.GetEndMessage(process)};
			}
			workSupervisor.AddMessages(messages, this);
		}
	}
}