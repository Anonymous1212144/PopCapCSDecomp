using System;
using Sexy;

namespace BejeweledLIVE
{
	public abstract class Piece : IReusable
	{
		public virtual void Reset()
		{
			this.mShrinkSize = 0;
			this.mShrinking = false;
			this.mShineFactor = 0.0;
			this.mShineAnim = false;
			this.mShineAnimFrame = 0f;
			this.mSparkleLife = 0;
			this.mSparkleFrame = 0;
		}

		public virtual void PrepareForReuse()
		{
		}

		public Piece()
		{
			this.Reset();
		}

		protected const int FLAME_GEM_FRAME_COUNT = 20;

		protected const int HYPER_CUBE_FRAME_COUNT = 30;

		protected const int HYPER_CUBE_PER_ROW = 5;

		public const int LASER_GLOW_FRAME_COUNT = 11;

		public const int LASER_GLOW_PER_ROW = 11;

		protected const int MULTIPLIER_FONT_WIDTH = 25;

		protected const int MULTIPLIER_FONT_HEIGHT = 30;

		protected const int MULTIPLIER_FONT_OFFSET = 7;

		public float mX;

		public float mY;

		public int mCol;

		public int mRow;

		public float mZ;

		public int mOfsX;

		public int mOfsY;

		public bool mShrinking;

		public int mShrinkSize;

		public float[] mLighting = new float[9];

		public bool mShineAnim;

		public double mShineFactor;

		public float mShineAnimFrame;

		public int mSparkleLife;

		public int mSparkleFrame;

		public int mSparklePairOfsCol;

		public int mSparklePairOfsRow;

		public float mFallVelocity;

		public float mXVelocity;
	}
}
