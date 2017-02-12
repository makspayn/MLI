using System.Threading;

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
		}

		public void SetBusyFlag(bool busy)
		{
			this.busy = busy;
		}

		public bool IsBusy()
		{
			return busy;
		}

		public void CompleteWork()
		{
			complete = true;
			ReRun();
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
				Run();
			}
		}
	}
}