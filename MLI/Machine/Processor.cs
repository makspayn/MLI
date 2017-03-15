using System.Threading;
using MLI.Services;

namespace MLI.Machine
{
	public abstract class Processor
	{
		protected string id;
		private bool busy;
		private bool complete;
		private Semaphore semaphore;

		protected Processor(string id)
		{
			this.id = id;
			busy = false;
			complete = false;
			semaphore = new Semaphore(0, 1);
			Thread thread = new Thread(RunInThread) { IsBackground = true };
			thread.Start();
			LogService.Debug($"{id} создан и запущен");
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