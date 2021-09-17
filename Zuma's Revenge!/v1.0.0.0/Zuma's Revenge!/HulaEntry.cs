using System;

namespace ZumasRevenge
{
	public class HulaEntry
	{
		public HulaEntry()
		{
		}

		public HulaEntry(HulaEntry rhs)
		{
			this.mBerserkAmt = rhs.mBerserkAmt;
			this.mAmnesty = rhs.mAmnesty;
			this.mVX = rhs.mVX;
			this.mProjVY = rhs.mProjVY;
			this.mSpawnY = rhs.mSpawnY;
			this.mSpawnRate = rhs.mSpawnRate;
			this.mProjChance = rhs.mProjChance;
			this.mAttackType = rhs.mAttackType;
			this.mAttackTime = rhs.mAttackTime;
			this.mProjRange = rhs.mProjRange;
		}

		public int mBerserkAmt;

		public int mAmnesty;

		public float mVX;

		public float mProjVY;

		public int mSpawnY;

		public int mSpawnRate;

		public int mProjChance;

		public int mAttackType;

		public int mAttackTime;

		public int mProjRange;

		public enum AttackType
		{
			Attack_None,
			Attack_Stun,
			Attack_Poison,
			Attack_Hallucinate,
			Attack_Slow
		}
	}
}
