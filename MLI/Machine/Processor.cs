using System.Threading;
using MLI.Data;
using MLI.Services;

namespace MLI.Machine
{
	public abstract class Processor
	{
		protected string id;
		protected string name;
		protected int number;
		protected int runTime;
		private bool busy;
		private bool complete;
		private Semaphore semaphore;

		protected Processor(string name, int number)
		{
			this.name = name;
			this.number = number;
			id = $"{name} №{number}";
			busy = false;
			complete = false;
			semaphore = new Semaphore(0, 1);
			Thread thread = new Thread(RunInThread) { IsBackground = true };
			thread.Start();
			LogService.Debug($"{id} создан и запущен");
		}

		public int RunCommand(CommandId commandId, params object[] param)
		{
			int time = CommandService.RunCommand(commandId, param);
			runTime += time;
			return time;
		}

		public void SetBusyFlag(bool busy)
		{
			this.busy = busy;
		}

		public bool IsBusy()
		{
			return busy;
		}

		public virtual void CompleteWork()
		{
			complete = true;
			ReRun();
			LogService.Debug($"{id} завершил работу");
		}

		public string GetId() => id;

		public string GetName() => name;

		public int GetNumber() => number;

		protected abstract void Run();

		protected void ReRun()
		{
			try
			{
				semaphore.Release();
			}
			catch
			{
				// ignored
			}
		}

		private void RunInThread()
		{
			while (!complete)
			{
				semaphore.WaitOne();
				if (complete) return;
				Run();
			}
		}
	}
}