using System;

namespace BejeweledLIVE
{
	public abstract class AbstractTimeline
	{
		public abstract void FireEvents();

		public abstract void Apply();

		public AbstractTimeline()
		{
			this.mDirection = AbstractTimeline.Direction.STOP;
			this.mLastTick = 0;
			this.mElapsedTicksI = 0;
			this.mElapsedTicksF = 0f;
		}

		public virtual void Dispose()
		{
		}

		public virtual void Start()
		{
			this.mDirection = AbstractTimeline.Direction.FORWARD;
			this.mElapsedTicksI = 0;
			this.mElapsedTicksF = 0f;
		}

		public virtual void Stop()
		{
			this.mDirection = AbstractTimeline.Direction.STOP;
		}

		public virtual void Resume()
		{
			this.mDirection = AbstractTimeline.Direction.FORWARD;
		}

		public virtual void Clear()
		{
			this.Stop();
			this.mLastTick = 0;
			this.mElapsedTicksI = 0;
			this.mElapsedTicksF = 0f;
		}

		public virtual int LastTick()
		{
			return this.mLastTick;
		}

		public virtual bool IsRunning()
		{
			return AbstractTimeline.Direction.FORWARD == this.mDirection;
		}

		public abstract bool AtEnd();

		public void Update()
		{
			if (this.IsRunning())
			{
				this.mElapsedTicksI++;
				this.FireEvents();
				if (!this.IsRunning())
				{
					this.mElapsedTicksF = (float)this.mElapsedTicksI;
					this.Apply();
				}
			}
		}

		public void UpdateF(float f)
		{
			if (this.IsRunning())
			{
				this.mElapsedTicksF += f;
				this.Apply();
			}
		}

		protected int mLastTick;

		protected int mElapsedTicksI;

		protected float mElapsedTicksF;

		protected AbstractTimeline.Direction mDirection;

		public enum Direction
		{
			BACKWARD = -1,
			STOP,
			FORWARD
		}
	}
}
