using System.Collections;
using System.Collections.Generic;
using MLI.Data;
using MLI.Method;
using Message = MLI.Data.Message;

namespace MLI.Machine
{
	public class Supervisor
	{
		private Queue messageQueue;
		private Queue processQueue;
		private FrameList frameList;
		private RunProcessUnitsBlock runProcessUnitsBlock;
		private RunControlUnitBlock runControlUnitBlock;

		public Supervisor()
		{
			messageQueue = Queue.Synchronized(new Queue());
			processQueue = Queue.Synchronized(new Queue());
			frameList = new FrameList();
			runProcessUnitsBlock = new RunProcessUnitsBlock("RPUB", 1, processQueue);
			runControlUnitBlock = new RunControlUnitBlock("RCUB", 1, messageQueue);
		}

		public FrameList GetFrameList()
		{
			return frameList;
		}

		public void SetProcessUnits(List<ProcessUnit> processUnits)
		{
			runProcessUnitsBlock.SetProcessUnits(processUnits);
		}

		public void SetControlUnit(ControlUnit controlUnit)
		{
			runControlUnitBlock.SetControlUnit(controlUnit);
		}

		public void AddMessage(Message message, ProcessUnit processUnit)
		{
			AddMessages(new List<Message> { message }, processUnit);
		}

		public void AddMessages(List<Message> messages, ProcessUnit processUnit)
		{
			processUnit.SetBusyFlag(false);
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
			runProcessUnitsBlock.RunProcessUnits();
			runControlUnitBlock.RunControlUnit();
		}

		public void CompleteWork()
		{
			runProcessUnitsBlock.CompleteWork();
			runControlUnitBlock.CompleteWork();
			runProcessUnitsBlock.CompleteProcessUnitsWork();
			runControlUnitBlock.CompleteControlUnitWork();
		}

		private class RunProcessUnitsBlock : Processor
		{
			private List<ProcessUnit> processUnits;
			private Queue processQueue;

			public RunProcessUnitsBlock(string name, int number, Queue processQueue) : base(name, number)
			{
				this.processQueue = processQueue;
			}

			public void SetProcessUnits(List<ProcessUnit> processUnits)
			{
				this.processUnits = processUnits;
			}

			public void RunProcessUnits()
			{
				ReRun();
			}

			public void CompleteProcessUnitsWork()
			{
				foreach (ProcessUnit processUnit in processUnits)
				{
					processUnit.CompleteWork();
				}
			}

			protected override void Run()
			{
				bool flag = true;
				foreach (ProcessUnit processUnit in processUnits)
				{
					if (processQueue.Count <= 0) break;
					if (processUnit.IsBusy()) continue;
					flag = false;
					processUnit.SetBusyFlag(true);
					processUnit.RunProcess((Process)processQueue.Dequeue());
				}
				if (flag && processQueue.Count > 0)
				{
					((Process)processQueue.Peek()).SetReadyTime();
				}
			}
		}

		private class RunControlUnitBlock : Processor
		{
			private ControlUnit controlUnit;
			private Queue messageQueue;

			public RunControlUnitBlock(string name, int number, Queue messageQueue) : base(name, number)
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

			public void CompleteControlUnitWork()
			{
				controlUnit.CompleteWork();
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