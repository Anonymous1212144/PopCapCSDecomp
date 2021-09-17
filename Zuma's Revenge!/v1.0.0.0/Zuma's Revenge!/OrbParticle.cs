using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class OrbParticle
	{
		public OrbParticle()
		{
		}

		public OrbParticle(float angle, float radius, float alpha_fade, float size_fade)
		{
			this.mAngle = angle;
			this.mAlphaFade = alpha_fade;
			this.mSizeFade = size_fade;
			this.mAlpha = 255f;
			this.mRadius = radius;
			this.mSize = 1f;
			this.mRotation = 0f;
			this.mRed = 255f;
			this.mGreen = 255f;
			float num = 255f / this.mAlphaFade;
			this.mRedFade = 255f / num;
			this.mGreenFade = Common._M(54f) / num;
		}

		public void Update()
		{
			this.mAlpha -= this.mAlphaFade;
			this.mSize -= this.mSizeFade;
			this.mRed -= this.mRedFade;
			this.mGreen -= this.mGreenFade;
			if (this.mRed < 0f)
			{
				this.mRed = 0f;
			}
			if (this.mGreen < 0f)
			{
				this.mGreen = 0f;
			}
			if (this.mAlpha < 0f)
			{
				this.mAlpha = 0f;
			}
			if (this.mSize < 0f)
			{
				this.mSize = 0f;
			}
			this.mRotation += Common._M(0.1f);
		}

		public void Draw(Graphics g, float x, float y)
		{
			g.SetColorizeImages(true);
			g.SetColor((int)this.mRed, (int)this.mGreen, 255, (int)this.mAlpha);
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.RotateRad(this.mRotation);
			this.mGlobalTranform.Scale(this.mSize, this.mSize);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_PART_FAT);
			g.DrawImageTransform(imageByID, this.mGlobalTranform, x + this.mRadius * (float)Math.Cos((double)this.mAngle), y - this.mRadius * (float)Math.Sin((double)this.mAngle));
			g.SetColorizeImages(false);
		}

		public bool IsDone()
		{
			return this.mAlpha <= 0f;
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mRadius);
			sync.SyncFloat(ref this.mRotation);
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mAlphaFade);
			sync.SyncFloat(ref this.mSizeFade);
			sync.SyncFloat(ref this.mRedFade);
			sync.SyncFloat(ref this.mGreenFade);
			sync.SyncFloat(ref this.mRed);
			sync.SyncFloat(ref this.mGreen);
		}

		protected float mAlpha;

		protected float mAngle;

		protected float mRadius;

		protected float mRotation;

		protected float mSize;

		protected float mAlphaFade;

		protected float mSizeFade;

		protected float mRedFade;

		protected float mGreenFade;

		protected float mRed;

		protected float mGreen;

		protected Transform mGlobalTranform = new Transform();
	}
}
