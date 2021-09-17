using System;
using SexyFramework;

namespace BejeweledLivePlus
{
	public class QuasiRandom
	{
		public QuasiRandom()
		{
			this.mSteps = 0f;
			this.mLastRoll = 0f;
		}

		public void Init(string theCurve)
		{
			this.mSteps = 0f;
			this.mLastRoll = 0f;
			this.mChance.SetCurve(theCurve);
		}

		public void Step()
		{
			this.Step(1f);
		}

		public void Step(float theCount)
		{
			this.mSteps += theCount;
			if (this.mChance <= 0.0)
			{
				this.mLastRoll = 0f;
				return;
			}
			this.mLastRoll = (float)(this.mChance * (double)Math.Min(GlobalMembers.M(2.5f), Math.Max(GlobalMembers.M(0.2f), (float)Math.Pow((double)(GlobalMembers.M(1.5f) * this.mSteps) * this.mChance, GlobalMembers.M(1.2)))));
		}

		public bool Check(float theRand)
		{
			if (theRand < this.mLastRoll)
			{
				this.mSteps = 0f;
				return true;
			}
			return false;
		}

		public CurvedVal mChance = new CurvedVal();

		public float mSteps;

		public float mLastRoll;
	}
}
