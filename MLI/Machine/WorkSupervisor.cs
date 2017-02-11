using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLI.Data;
using MLI.Method;
using NLog;
using Message = MLI.Data.Message;

namespace MLI.Machine
{
	public class WorkSupervisor
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
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

		public void AddMessage(Message message, ExecUnit execUnit)
		{
			AddMessages(new List<Message> { message }, execUnit);
		}

		public void AddMessages(List<Message> messages, ExecUnit execUnit)
		{
			lock (execUnit)
			{
				execUnit.SetBusyFlag(false);
			}
			foreach (Message message in messages)
			{
				messageQueue.Enqueue(message);
			}
			TryGiveTasks();
		}

		public void AddProcess(Process process, ControlUnit controlUnit)
		{
			lock (controlUnit)
			{
				controlUnit.SetBusyFlag(false);
			}
			processQueue.Enqueue(process);
			TryGiveTasks();
		}

		public void TryGiveTasks()
		{
			new Thread(() =>
			{
				TryRunControlUnit();
			}).Start();
			new Thread(() =>
			{
				TryRunExecUnit();
			}).Start();
		}

		private void TryRunExecUnit()
		{
			foreach (ExecUnit execUnit in execUnits)
			{
				lock (execUnit)
				{
					if (processQueue.Count <= 0) return;
					if (execUnit.IsBusy()) continue;
					execUnit.SetBusyFlag(true);
				}
				execUnit.RunProcess((Process) processQueue.Dequeue());
			}
		}

		private void TryRunControlUnit()
		{
			lock (controlUnit)
			{
				if (controlUnit.IsBusy() || messageQueue.Count <= 0) return;
				controlUnit.SetBusyFlag(true);
			}
			controlUnit.ProcessMessage((Message)messageQueue.Dequeue());
		}
	}
}