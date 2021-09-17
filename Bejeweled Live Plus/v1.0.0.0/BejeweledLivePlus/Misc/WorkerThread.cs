using System;
using System.Threading;

namespace BejeweledLivePlus.Misc
{
	public class WorkerThread
	{
		private void ThreadProc()
		{
			for (;;)
			{
				this.mSignalEvent.WaitOne(1000);
				if (this.mStopped)
				{
					break;
				}
				if (this.mTask != null)
				{
					this.mTask(this.mTaskArg);
					this.mTask = null;
					this.mDoneEvent.Set();
				}
			}
			this.mDoneEvent.Set();
		}

		public WorkerThread()
		{
			this.mTask = null;
			this.mTaskArg = null;
			this.mStopped = false;
			Thread thread = new Thread(new ThreadStart(this.ThreadProc));
			thread.Start();
		}

		public virtual void Dispose()
		{
			this.WaitForTask();
			this.mStopped = true;
			this.mSignalEvent.Set();
			this.mDoneEvent.WaitOne(5000);
		}

		public void DoTask(WorkerThread.ThreadTask theTask, object theTaskArg)
		{
			this.WaitForTask();
			this.mTask = theTask;
			this.mTaskArg = theTaskArg;
			this.mSignalEvent.Set();
		}

		public void WaitForTask()
		{
			while (this.mTask != null)
			{
				if (GlobalMembers.gApp.mResStreamsManager != null && GlobalMembers.gApp.mResStreamsManager.IsInitialized())
				{
					GlobalMembers.gApp.mResStreamsManager.Update();
				}
				this.mDoneEvent.WaitOne(10);
			}
			this.mDoneEvent.Reset();
		}

		public bool IsProcessingTask()
		{
			return this.mTask != null;
		}

		protected WorkerThread.ThreadTask mTask;

		protected object mTaskArg;

		protected bool mStopped;

		private ManualResetEvent mSignalEvent = new ManualResetEvent(true);

		private ManualResetEvent mDoneEvent = new ManualResetEvent(true);

		public delegate void ThreadTask(object obj);
	}
}
