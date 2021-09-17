using System;

namespace ZumasRevenge
{
	public class Boss_Param_Range
	{
		public void Init()
		{
			this.mMin = 0f;
			this.mMax = 0f;
			this.mRatingMin = -1f;
			this.mRatingMax = -1f;
		}

		public bool InRange(float amt)
		{
			return this.mRatingMin < 0f || this.mRatingMax < 0f || (amt >= this.mRatingMin && amt < this.mRatingMax);
		}

		public float mMin;

		public float mMax;

		public float mRatingMin;

		public float mRatingMax;
	}
}
