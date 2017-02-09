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

		public WorkSupervisor(List<ExecUnit> execUnits, ControlUnit controlUnit)
		{
			this.execUnits = execUnits;
			this.controlUnit = controlUnit;
			messageQueue = Queue.Synchronized(new Queue());
			processQueue = Queue.Synchronized(new Queue());
			frameList = new List<Frame>();
		}

		public List<Frame> GetFrameList()
		{
			return frameList;
		}

		public void AddMessages(List<Message> messages, ExecUnit execUnit)
		{
			execUnit.SetBusyFlag(false);
			foreach (Message message in messages)
			{
				messageQueue.Enqueue(message);
			}
			TryRunControlUnit();
		}

		public void AddProcess(IProcess process)
		{
			processQueue.Enqueue(process);
			TryRunExecUnit();
		}

		private void TryRunExecUnit()
		{
			if (processQueue.Count <= 0) return;
			lock (execUnits)
			{
				foreach (ExecUnit execUnit in execUnits.Where(execUnit => !execUnit.IsBusy()))
				{
					execUnit.SetBusyFlag(true);
					execUnit.RunProcess((IProcess) processQueue.Dequeue());
					return;
				}
			}
		}

		private void TryRunControlUnit()
		{
			if (messageQueue.Count <= 0) return;
			lock (controlUnit)
			{
				if (controlUnit.IsBusy()) return;
				controlUnit.SetBusyFlag(true);
				controlUnit.ProcessMessage((Message) messageQueue.Dequeue());
			}
		}
	}
}