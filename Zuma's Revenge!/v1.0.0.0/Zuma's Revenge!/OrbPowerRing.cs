using System;
using System.Collections.Generic;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class OrbPowerRing : IDisposable
	{
		public OrbPowerRing()
		{
		}

		public OrbPowerRing(float angle, float max_radius, float alpha_fade, float size_fade, float angle_inc)
		{
			this.mAlphaFade = alpha_fade;
			this.mSizeFade = size_fade;
			this.mAngle = angle;
			this.mRadius = 0f;
			this.mMaxRadius = max_radius;
			this.mExpanding = true;
			this.mUpdateCount = 0;
			this.mDone = false;
			this.mAngleInc = angle_inc;
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < this.mParticles.Count; i++)
			{
				this.mParticles[i] = null;
			}
			this.mParticles.Clear();
		}

		public void Update()
		{
			if (this.mDone)
			{
				return;
			}
			this.mAngle += this.mAngleInc;
			this.mUpdateCount++;
			if (this.mUpdateCount > Common._M(50))
			{
				this.mExpanding = false;
			}
			if (this.mExpanding && this.mRadius < this.mMaxRadius)
			{
				this.mRadius += this.mMaxRadius / Common._M(30f);
			}
			else if (!this.mExpanding && this.mRadius > 0f)
			{
				this.mRadius -= this.mMaxRadius / Common._M(15f);
				if (this.mRadius < 0f)
				{
					this.mRadius = 0f;
				}
			}
			if ((this.mExpanding || this.mRadius > 0f) && this.mUpdateCount % Common._M(1) == 0)
			{
				this.mParticles.Add(new OrbParticle(this.mAngle, this.mRadius, this.mAlphaFade, this.mSizeFade));
			}
			bool flag = true;
			for (int i = 0; i < this.mParticles.Count; i++)
			{
				OrbParticle orbParticle = this.mParticles[i];
				orbParticle.Update();
				if (!orbParticle.IsDone())
				{
					flag = false;
				}
				else
				{
					this.mParticles.RemoveAt(i);
					i--;
				}
			}
			if (!this.mExpanding && flag)
			{
				this.mDone = true;
			}
		}

		public void Draw(Graphics g, float x, float y)
		{
			if (this.mDone)
			{
				return;
			}
			for (int i = 0; i < this.mParticles.Count; i++)
			{
				this.mParticles[i].Draw(g, x, y);
			}
		}

		public bool IsDone()
		{
			return this.mDone;
		}

		public bool IsExpanding()
		{
			return this.mExpanding;
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mRadius);
			sync.SyncFloat(ref this.mMaxRadius);
			sync.SyncFloat(ref this.mAlphaFade);
			sync.SyncFloat(ref this.mSizeFade);
			sync.SyncFloat(ref this.mAngleInc);
			sync.SyncBoolean(ref this.mExpanding);
			sync.SyncBoolean(ref this.mDone);
			sync.SyncLong(ref this.mUpdateCount);
			this.SyncListOrbParticles(sync, true);
		}

		private void SyncListOrbParticles(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mParticles.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					OrbParticle orbParticle = new OrbParticle();
					orbParticle.SyncState(sync);
					this.mParticles.Add(orbParticle);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mParticles.Count);
			foreach (OrbParticle orbParticle2 in this.mParticles)
			{
				orbParticle2.SyncState(sync);
			}
		}

		protected List<OrbParticle> mParticles = new List<OrbParticle>();

		protected float mAngle;

		protected float mRadius;

		protected float mMaxRadius;

		protected float mAlphaFade;

		protected float mSizeFade;

		protected float mAngleInc;

		protected bool mExpanding;

		protected bool mDone;

		protected int mUpdateCount;
	}
}
