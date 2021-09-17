using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class EyeAnim
	{
		public EyeAnim()
		{
		}

		public EyeAnim(EyeAnim rhs)
		{
			if (rhs == null || rhs == this)
			{
				return;
			}
			this.mEyeFlame = rhs.mEyeFlame;
			this.mFiring = rhs.mFiring;
			this.mUpdateCount = rhs.mUpdateCount;
		}

		public void Update(int x, int y, int alpha)
		{
			if (!this.mFiring && this.mEyeFlame.mCurNumParticles == 0)
			{
				return;
			}
			this.mUpdateCount++;
			this.mEyeFlame.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mEyeFlame.mDrawTransform.Scale(num, num);
			this.mEyeFlame.mDrawTransform.Translate((float)Common._S(x), (float)Common._S(y));
			this.mEyeFlame.mColor.mAlpha = alpha;
			this.mEyeFlame.Update();
		}

		public void Draw(Graphics g)
		{
			if (!this.mFiring && this.mEyeFlame.mCurNumParticles == 0)
			{
				return;
			}
			this.mEyeFlame.Draw(g);
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncBoolean(ref this.mFiring);
			if (sync.isWrite())
			{
				Common.SerializePIEffect(this.mEyeFlame, sync);
				return;
			}
			Common.DeserializePIEffect(this.mEyeFlame, sync);
		}

		public PIEffect mEyeFlame;

		public bool mFiring;

		public int mUpdateCount;
	}
}
