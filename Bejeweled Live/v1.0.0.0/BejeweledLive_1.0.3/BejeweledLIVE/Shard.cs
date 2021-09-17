using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BejeweledLIVE
{
	public class Shard
	{
		public static Shard GetNewShard()
		{
			if (Shard.unusedObjects.Count > 0)
			{
				return Shard.unusedObjects.Pop();
			}
			return new Shard();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			Shard.unusedObjects.Push(this);
		}

		private void Reset()
		{
			this.mX = 0f;
			this.mY = 0f;
			this.mYDelta = 0f;
			this.mType = Shard.ShardType.TYPE_GEM_SHARD;
			this.mFrame = 0;
			this.mVelX = 0f;
			this.mVelY = 0f;
			this.mSparkleChance = 0f;
			this.mSparkleChanceAdd = 0f;
			this.mAccelScale = 1f;
			this.mAccY = 0f;
			this.mColor = new Color(255, 255, 255);
			this.mIsCyclingColor = false;
			this.mCurHue = 0;
			this.mLifeAcc = 0;
			this.mSparkNum = 0;
			this.mSparkDead = false;
			this.mSparkAlpha = 0f;
			this.mUpdateDiv = 1;
		}

		private Shard()
		{
			this.Reset();
		}

		public Shard.ShardType mType;

		public Color mColor = default(Color);

		public float mX;

		public float mY;

		public float mYDelta;

		public int mFrame;

		public int mUpdateDiv;

		public float mVelX;

		public float mVelY;

		public float mAccelScale;

		public float mSparkleChance;

		public float mSparkleChanceAdd;

		public float mDrawX;

		public float mDrawY;

		public float mCenterX;

		public float mCenterY;

		public float mRot;

		public bool mIsCyclingColor;

		public int mCurHue;

		public float mOrigX;

		public int mLifeAcc;

		public int mSparkNum;

		public float mAccY;

		public bool mSparkDead;

		public float mSparkAlpha;

		public float mSparkAngleMult;

		public float mSparkWaviness;

		public float mSparkDist;

		private static Stack<Shard> unusedObjects = new Stack<Shard>();

		public enum ShardType
		{
			TYPE_GEM_SHARD,
			TYPE_ROCK_CHUNK,
			TYPE_SPARKLE,
			TYPE_SPARK,
			TYPE_PLASMA,
			TYPE_SNOW
		}
	}
}
