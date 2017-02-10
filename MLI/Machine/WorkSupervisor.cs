using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Method;

namespace MLI.Machine
{
	public class WorkSupervisor
	{
		private List<ExecUnit> execUnits;
		private ControlUnit controlUnit;
		private Queue messageQueue;
		private Queue processQueue;
		private List<Frame> frameList; 

		public WorkSupervisor()
		{
			messageQueue = Queue.Synchronized(new Queue());
			processQueue = Queue.Synchronized(new Queue());
			frameList = new List<Frame>();
		}

		public List<Frame> GetFrameList()
		{
			return frameList;
		}

		public void SetExecUnits(List<ExecUnit> execUnits)
		{
			this.execUnits = execUnits;
		}

		public void SetControlUnit(ControlUnit controlUnit)
		{
			this.controlUnit = controlUnit;
		}

		public void AddMessages(List<Message> messages, ExecUnit execUnit)
		{
			execUnit?.SetBusyFlag(false);
			foreach (Message message in messages)
			{
				messageQueue.Enqueue(message);
			}
			TryRunControlUnit();
		}

		public void AddProcess(Process process, ControlUnit controlUnit)
		{
			controlUnit?.SetBusyFlag(false);
			processQueue.Enqueue(process);
			TryRunExecUnit();
		}

		private void TryRunExecUnit()
		{
			lock (execUnits)
			{
				foreach (ExecUnit execUnit in execUnits.
					Where(execUnit => !execUnit.IsBusy()).
					TakeWhile(execUnit => processQueue.Count > 0))
				{
					execUnit.SetBusyFlag(true);
					execUnit.RunProcess((Process) processQueue.Dequeue());
				}
			}
		}

		private void TryRunControlUnit()
		{
			lock (controlUnit)
			{
				if (controlUnit.IsBusy() || messageQueue.Count <= 0) return;
				controlUnit.SetBusyFlag(true);
				controlUnit.ProcessMessage((Message) messageQueue.Dequeue());
			}
		}
	}
}