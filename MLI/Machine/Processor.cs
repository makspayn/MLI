using System.Threading;

namespace MLI.Machine
{
	public abstract class Processor
	{
		private Semaphore semaphore;
		private bool busy;

		protected Processor()
		{
			busy = false;
			semaphore = new Semaphore(0, 1);
			Thread thread = new Thread(RunInThread);
			thread.IsBackground = true;
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
			while (true)
			{
				semaphore.WaitOne();
				Run();
			}
			
		}
	}
}