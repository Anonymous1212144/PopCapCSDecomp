using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class CannonPowerEffect : PowerEffect
	{
		public CannonPowerEffect(Ball b)
		{
			int radius = b.GetRadius();
			int num = (int)b.GetX() - radius;
			int num2 = (int)b.GetY() - radius;
			this.mRings[0].mX = (float)(num + 18);
			this.mRings[0].mY = (float)(num2 + 11);
			this.mRings[1].mX = (float)(num + 11);
			this.mRings[1].mY = (float)(num2 + 23);
			this.mRings[2].mX = (float)(num + 24);
			this.mRings[2].mY = (float)(num2 + 22);
			this.mBallRotation = b.GetRotation();
			for (int i = 0; i < 3; i++)
			{
				Common.RotatePoint(this.mBallRotation - 1.57076454f, ref this.mRings[i].mX, ref this.mRings[i].mY, (float)(num + radius), (float)(num2 + radius));
			}
		}

		public CannonPowerEffect()
		{
		}

		public override void Update()
		{
			if (this.IsDone())
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				if (this.mRings[i].mSizePct < 1f)
				{
					this.mRings[i].mSizePct += 0.06666667f;
					if (this.mRings[i].mSizePct >= 1f)
					{
						this.mRings[i].mSizePct = 1f;
						float num2 = MathUtils.DegreesToRadians(this.mBallRotation + (float)(120 * i));
						this.mRings[i].mVX = (float)Math.Cos((double)num2) * 1.2f;
						this.mRings[i].mVY = -(float)Math.Sin((double)num2) * 1.2f;
						this.mRings[i].mTX = this.mRings[i].mX + this.mRings[i].mVX * 15f;
						this.mRings[i].mTY = this.mRings[i].mY + this.mRings[i].mVY * 15f;
					}
				}
				else if (this.mRings[i].mVX != 0f || this.mRings[i].mVY != 0f)
				{
					this.mRings[i].mX += this.mRings[i].mVX;
					this.mRings[i].mY += this.mRings[i].mVY;
					if (Common.DoneMoving(this.mRings[i].mX, this.mRings[i].mVX, this.mRings[i].mTX))
					{
						this.mRings[i].mX = this.mRings[i].mTX;
						this.mRings[i].mVX = 0f;
					}
					if (Common.DoneMoving(this.mRings[i].mY, this.mRings[i].mVY, this.mRings[i].mTY))
					{
						this.mRings[i].mY = this.mRings[i].mTY;
						this.mRings[i].mVY = 0f;
					}
				}
				else if (this.mRings[i].mAlpha != 0f)
				{
					this.mRings[i].mSizePct += 1f / (float)Common._M(25);
					this.mRings[i].mAlpha -= Common._M(6f);
					if (this.mRings[i].mAlpha < 0f)
					{
						num++;
						this.mRings[i].mAlpha = 0f;
					}
				}
				else
				{
					num++;
				}
			}
			if (num == 3)
			{
				this.mDone = true;
			}
		}

		public override void Draw(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_CANNON_RING_BLUE + this.mColorType);
			g.SetDrawMode(1);
			for (int i = 0; i < 3; i++)
			{
				int num = (int)(this.mRings[i].mSizePct * (float)imageByID.mWidth);
				int num2 = (int)(this.mRings[i].mSizePct * (float)imageByID.mHeight);
				if (this.mRings[i].mAlpha != 255f)
				{
					g.SetColor(255, 255, 255, (int)this.mRings[i].mAlpha);
					g.SetColorizeImages(true);
				}
				g.DrawImage(imageByID, (int)(Common._S(this.mRings[i].mX) - (float)(num / 2)), (int)(Common._S(this.mRings[i].mY) - (float)(num2 / 2)), num, num2);
				g.SetColorizeImages(false);
			}
			g.SetDrawMode(0);
		}

		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncFloat(ref this.mBallRotation);
			for (int i = 0; i < 3; i++)
			{
				sync.SyncFloat(ref this.mRings[i].mX);
				sync.SyncFloat(ref this.mRings[i].mY);
				sync.SyncFloat(ref this.mRings[i].mVX);
				sync.SyncFloat(ref this.mRings[i].mVY);
				sync.SyncFloat(ref this.mRings[i].mTX);
				sync.SyncFloat(ref this.mRings[i].mTY);
				sync.SyncFloat(ref this.mRings[i].mSizePct);
				sync.SyncFloat(ref this.mRings[i].mAlpha);
			}
		}

		protected CannonPowerEffect.CannonRing[] mRings = new CannonPowerEffect.CannonRing[]
		{
			new CannonPowerEffect.CannonRing(),
			new CannonPowerEffect.CannonRing(),
			new CannonPowerEffect.CannonRing()
		};

		protected float mBallRotation;

		protected class CannonRing
		{
			public float mX;

			public float mY;

			public float mVX;

			public float mVY;

			public float mTX;

			public float mTY;

			public float mSizePct;

			public float mAlpha = 255f;
		}
	}
}
