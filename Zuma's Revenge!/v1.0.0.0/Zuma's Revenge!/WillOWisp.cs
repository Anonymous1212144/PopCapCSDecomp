using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class WillOWisp : FlyingBug
	{
		protected static float GetVel()
		{
			return ZumasRevenge.Common._M(0.05f) + (float)(SexyFramework.Common.SafeRand() % ZumasRevenge.Common._M1(10)) / ZumasRevenge.Common._M2(100f);
		}

		protected static FColor GetTargetColor(FColor curr_color)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < WillOWisp.NUM_WISP_COLORS; i++)
			{
				if (WillOWisp.WISP_COLORS[i] != curr_color)
				{
					list.Add(i);
				}
			}
			return WillOWisp.WISP_COLORS[list[SexyFramework.Common.SafeRand() % list.Count]];
		}

		protected override void SetupBug(Critter d)
		{
			d.mImage = null;
			base.SetupBug(d);
			if (++WillOWisp.mSpawnCounter < ZumasRevenge.Common._M(2) && this.mBugs.Count > 1)
			{
				Critter critter = this.mBugs[this.mBugs.Count - 2];
				d.mX = critter.mX;
				d.mY = critter.mY;
				d.mAngle = critter.mAngle;
				WillOWisp.mSpawnCounter = 0;
			}
			d.mInitVel = WillOWisp.GetVel();
			d.mVX = (float)Math.Cos((double)d.mAngle) * d.mInitVel;
			d.mVY = -(float)Math.Sin((double)d.mAngle) * d.mInitVel;
			d.mFader.mColor = (d.mFader.mMinColor = WillOWisp.GetTargetColor(new FColor(0f, 0f, 0f)));
			d.mFader.mMaxColor = WillOWisp.GetTargetColor(d.mFader.mColor);
			d.mFader.FadeOverTime(ZumasRevenge.Common._M(200) + SexyFramework.Common.SafeRand() % ZumasRevenge.Common._M1(300));
		}

		public WillOWisp()
		{
			this.mMinBugs = ZumasRevenge.Common._M(10);
			this.mMaxBugs = ZumasRevenge.Common._M(15);
			this.mNoEnterRect = new Rect(ZumasRevenge.Common._S(ZumasRevenge.Common._M(100)), ZumasRevenge.Common._S(ZumasRevenge.Common._M1(100)), ZumasRevenge.Common._S(ZumasRevenge.Common._M2(600)), ZumasRevenge.Common._S(ZumasRevenge.Common._M3(400)));
			this.mReverseTimer = ZumasRevenge.Common._M(100);
			this.mReverseRotateDelay = ZumasRevenge.Common._M(100);
			this.mRotateMinTimer = ZumasRevenge.Common._M(300);
			this.mRotateMaxTimer = ZumasRevenge.Common._M(400);
			this.mFlyingMinTimer = ZumasRevenge.Common._M(10);
			this.mFlyingMaxTimer = ZumasRevenge.Common._M(25);
			this.mDefaultAnimRate = ZumasRevenge.Common._M(12);
			this.mRestingMinTimer = ZumasRevenge.Common._M(100);
			this.mRestingMaxTimer = ZumasRevenge.Common._M(500);
			this.mMaxRotateDegrees = ZumasRevenge.Common._M(360);
			this.mAllowRest = false;
			this.mNoRotateUntilOnScreen = true;
		}

		public override void Update()
		{
			base.Update();
			for (int i = 0; i < this.mBugs.Count; i++)
			{
				Critter critter = this.mBugs[i];
				if (critter.mFader.Update())
				{
					if (critter.mFader.mForward)
					{
						critter.mFader.mMaxColor = WillOWisp.GetTargetColor(critter.mFader.mColor);
					}
					else
					{
						critter.mFader.mMinColor = WillOWisp.GetTargetColor(critter.mFader.mColor);
					}
					critter.mFader.FadeOverTime(ZumasRevenge.Common._M(200) + SexyFramework.Common.SafeRand() % ZumasRevenge.Common._M1(300));
				}
			}
		}

		public override void DrawBug(Graphics g, Critter d, Transform t)
		{
		}

		public override string GetName()
		{
			return "WillOWisp";
		}

		protected static int mSpawnCounter = 0;

		protected static int NUM_WISP_COLORS = 4;

		protected static FColor[] WISP_COLORS = new FColor[]
		{
			new FColor(255f, 255f, 0f),
			new FColor(141f, 141f, 255f),
			new FColor(0f, 0f, 255f),
			new FColor(255f, 179f, 179f)
		};
	}
}
