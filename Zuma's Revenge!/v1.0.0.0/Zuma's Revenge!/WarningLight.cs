using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class WarningLight
	{
		public WarningLight(float x, float y)
		{
			this.mX = x;
			this.mY = y;
			this.mAlpha = 0f;
			this.mUpdateCount = 0;
			this.mAngle = 0f;
			this.mState = 0;
			this.mWaypoint = -1f;
			this.mPulseAlpha = 0f;
			this.mPulseRate = 0f;
			this.mPriority = 0;
		}

		public bool Update()
		{
			this.mUpdateCount++;
			float num = Common._M(5f);
			if (this.mState == 1)
			{
				this.mAlpha = Math.Min(255f, this.mAlpha + num);
				if (this.mAlpha >= 255f)
				{
					this.mState = 0;
				}
			}
			else if (this.mState == -1)
			{
				this.mAlpha = Math.Max(0f, this.mAlpha - num);
				if (this.mAlpha <= 0f)
				{
					this.mState = 0;
				}
			}
			else if (this.mPulseRate != 0f)
			{
				this.mPulseAlpha += ((this.mPulseRate > 0f) ? (this.mPulseRate * 2f) : this.mPulseRate);
				if (this.mPulseRate < 0f && this.mPulseAlpha <= 0f)
				{
					this.mPulseRate = 0f;
					this.mPulseAlpha = 0f;
				}
				else if (this.mPulseAlpha >= 255f && this.mPulseRate > 0f)
				{
					this.mPulseRate *= -1f;
					this.mPulseAlpha = 255f;
					return true;
				}
			}
			return false;
		}

		public void Draw(Graphics g)
		{
			if (this.mAlpha == 0f)
			{
				return;
			}
			g.PushState();
			if (this.mAlpha != 0f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mAlpha);
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_SKULL_PATH);
			g.DrawImageRotated(imageByID, (int)(Common._S(this.mX) - (float)(imageByID.mWidth / 2)), (int)(Common._S(this.mY) - (float)(imageByID.mHeight / 2)), (double)(this.mAngle + 1.570795f));
			if (this.mPulseAlpha != 0f)
			{
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_SKULL_PATH_LIT);
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mPulseAlpha);
				g.DrawImageRotated(imageByID2, (int)(Common._S(this.mX) - (float)(imageByID2.mWidth / 2)), (int)(Common._S(this.mY) - (float)(imageByID2.mHeight / 2)), (double)(this.mAngle + 1.570795f));
			}
			g.SetColorizeImages(false);
			g.PopState();
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mPulseAlpha);
			sync.SyncFloat(ref this.mPulseRate);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mUpdateCount);
		}

		public float mX;

		public float mY;

		public float mAlpha;

		public float mAngle;

		public float mPulseAlpha;

		public float mPulseRate;

		public float mWaypoint;

		public int mState;

		public int mUpdateCount;

		public int mPriority;
	}
}
