using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
		private FrameList frameList;
		private RunExecUnitsBlock runExecUnitsBlock;
		private RunControlUnitBlock runControlUnitBlock;

		public WorkSupervisor()
		{
			messageQueue = Queue.Synchronized(new Queue());
			processQueue = Queue.Synchronized(new Queue());
			frameList = new FrameList();
			runExecUnitsBlock = new RunExecUnitsBlock(processQueue);
			runControlUnitBlock = new RunControlUnitBlock(messageQueue);
		}

		public FrameList GetFrameList()
		{
			return frameList;
		}

		public void SetExecUnits(List<ExecUnit> execUnits)
		{
			this.execUnits = execUnits;
			runExecUnitsBlock.SetExecUnits(execUnits);
		}

		public void SetControlUnit(ControlUnit controlUnit)
		{
			this.controlUnit = controlUnit;
			runControlUnitBlock.SetControlUnit(controlUnit);
		}

		public void AddMessage(Message message, ExecUnit execUnit)
		{
			AddMessages(new List<Message> { message }, execUnit);
		}

		public void AddMessages(List<Message> messages, ExecUnit execUnit)
		{
			execUnit.SetBusyFlag(false);
			foreach (Message message in messages)
			{
				messageQueue.Enqueue(message);
			}
			TryGiveTasks();
		}

		public void AddProcess(Process process, ControlUnit controlUnit)
		{
			controlUnit.SetBusyFlag(false);
			processQueue.Enqueue(process);
			TryGiveTasks();
		}

		public void TryGiveTasks()
		{
			runExecUnitsBlock.RunExecUnits();
			runControlUnitBlock.RunControlUnit();
		}

		private class RunExecUnitsBlock : Processor
		{
			private List<ExecUnit> execUnits;
			private Queue processQueue;

			public RunExecUnitsBlock(Queue processQueue)
			{
				this.processQueue = processQueue;
			}

			public void SetExecUnits(List<ExecUnit> execUnits)
			{
				this.execUnits = execUnits;
			}

			public void RunExecUnits()
			{
				ReRun();
			}

			protected override void Run()
			{
				foreach (ExecUnit execUnit in execUnits)
				{
					if (processQueue.Count <= 0) break;
					if (execUnit.IsBusy()) continue;
					execUnit.SetBusyFlag(true);
					execUnit.RunProcess((Process)processQueue.Dequeue());
				}
			}
		}

		private class RunControlUnitBlock : Processor
		{
			private ControlUnit controlUnit;
			private Queue messageQueue;

			public RunControlUnitBlock(Queue messageQueue)
			{
				this.messageQueue = messageQueue;
			}

			public void SetControlUnit(ControlUnit controlUnit)
			{
				this.controlUnit = controlUnit;
			}

			public void RunControlUnit()
			{
				ReRun();
			}

			protected override void Run()
			{
				if (controlUnit.IsBusy() || messageQueue.Count <= 0) return;
				controlUnit.SetBusyFlag(true);
				controlUnit.ProcessMessage((Message)messageQueue.Dequeue());
			}
		}
	}
}