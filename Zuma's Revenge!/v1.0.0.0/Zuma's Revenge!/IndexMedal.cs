using System;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class IndexMedal : ButtonWidget
	{
		public IndexMedal(bool theIsAced, int theId, ButtonListener theButtonListener)
			: base(theId, theButtonListener)
		{
			this.mIsAced = theIsAced;
			SexyFramework.Common.SRand(SexyFramework.Common.SexyTime());
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				this.mSparkles[i].mEffect = null;
				this.mSparkles[i].mOffsetX = -1f;
				this.mSparkles[i].mOffsetY = -1f;
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				PIEffect mEffect = this.mSparkles[i].mEffect;
				if (mEffect != null)
				{
					mEffect.Dispose();
				}
				this.mSparkles[i].mEffect = null;
			}
		}

		public void FindRandomOffsetsInRadius(float theRadius, ref float theOffsetX, ref float theOffsetY)
		{
			float num = (float)SexyFramework.Common.Rand() / (float)QRand.RAND_MAX * (theRadius * 0.9f);
			int num2 = ((SexyFramework.Common.Rand() % 2 == 0) ? (-1) : 1);
			float num3 = (float)SexyFramework.Common.Rand() / (float)QRand.RAND_MAX * 6.28318548f;
			float num4 = num * (float)Math.Cos((double)num3) * (float)num2;
			float num5 = num * (float)Math.Sin((double)num3) * (float)num2;
			theOffsetX = theRadius + num4;
			theOffsetY = theRadius + num5;
		}

		public override void Update()
		{
			base.Update();
			if (!this.mIsAced)
			{
				return;
			}
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				PIEffect mEffect = this.mSparkles[i].mEffect;
				if (mEffect != null)
				{
					mEffect.mDrawTransform.LoadIdentity();
					mEffect.mDrawTransform.Scale(ZumasRevenge.Common._DS(1.4f), ZumasRevenge.Common._DS(1.4f));
					mEffect.mDrawTransform.Translate(this.mSparkles[i].mOffsetX, this.mSparkles[i].mOffsetY);
					mEffect.Update();
					if (SexyFramework.Common.Rand(500) == 0 && mEffect.mCurNumParticles == 0 && MathUtils._geq(mEffect.mFrameNum, (float)mEffect.mLastFrameNum))
					{
						mEffect.ResetAnim();
						mEffect.mRandSeeds.Clear();
						mEffect.mRandSeeds.Add(SexyFramework.Common.Rand(1000));
						this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
					}
				}
				else if (SexyFramework.Common.Rand(500) == 0)
				{
					this.mSparkles[i].mEffect = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_MM_SPARKLE").Duplicate();
					this.mSparkles[i].mEffect.mEmitAfterTimeline = false;
					ZumasRevenge.Common.SetFXNumScale(this.mSparkles[i].mEffect, 3f);
					this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
				}
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (!this.mIsAced)
			{
				return;
			}
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				PIEffect mEffect = this.mSparkles[i].mEffect;
				if (mEffect != null)
				{
					mEffect.Draw(g);
				}
			}
		}

		public void SetAced()
		{
			this.mIsAced = true;
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				this.mSparkles[i].mEffect = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_MM_SPARKLE").Duplicate();
				this.mSparkles[i].mEffect.mEmitAfterTimeline = true;
				ZumasRevenge.Common.SetFXNumScale(this.mSparkles[i].mEffect, 3f);
				this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
			}
		}

		public void Init()
		{
			if (!this.mIsAced)
			{
				return;
			}
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
			}
		}

		private static int MAX_NUM_BUTTON_SPARKLES = 2;

		public IndexMedal.AceSparkle[] mSparkles = new IndexMedal.AceSparkle[IndexMedal.MAX_NUM_BUTTON_SPARKLES];

		public bool mIsAced;

		public float mRadius;

		public struct AceSparkle
		{
			public PIEffect mEffect;

			public float mOffsetX;

			public float mOffsetY;
		}
	}
}
