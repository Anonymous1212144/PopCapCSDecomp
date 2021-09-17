using System;
using Sexy;

namespace BejeweledLIVE
{
	public class CPieceBej2 : Piece
	{
		public static CPieceBej2 GetNewCPieceBej2()
		{
			return CPieceBej2.reuseHelper.GetNew();
		}

		public override void PrepareForReuse()
		{
			CPieceBej2.reuseHelper.PushOnToReuseStack(this);
		}

		public override void Reset()
		{
			base.Reset();
			this.mType = GemTypes.TYPE_GEM_1;
			this.mX = 0f;
			this.mY = 0f;
			this.mZ = 0f;
			this.mOfsX = 0;
			this.mOfsY = 0;
			this.mBombCountdown = 0;
			this.mCol = 0;
			this.mRow = 0;
			this.mFallVelocity = 0f;
			this.mSwapMultX = 0;
			this.mSwapMultY = 0;
			this.mBombified = false;
			this.mBombifyTick = 0;
			this.mXVelocity = 0f;
			this.mZVelocity = 0f;
			this.mZRot = 0f;
			this.mZRotAdd = 0f;
			this.mSpinFrame = 0;
			this.mLastActiveTick = 0;
			this.mExplodeDelay = 0;
			this.mIsElectrocuting = false;
			this.mElectrocutePercent = 0f;
			this.mEffectNum = 0;
			this.mEffectPercent = 0f;
			this.mEffectDelay = 0;
			this.mChangedTick = -1;
		}

		public CPieceBej2()
		{
			this.Reset();
		}

		public bool ContainsPoint(int x, int y)
		{
			return (float)x >= this.mX && (float)y >= this.mY && (float)x < this.mX + (float)GameConstants.GEM_WIDTH && (float)y < this.mY + (float)GameConstants.GEM_HEIGHT;
		}

		public bool ContainsPoint(int x, int y, int theFuzzFactor)
		{
			return this.ContainsPoint(x, y) || this.ContainsPoint(x - theFuzzFactor, y - theFuzzFactor) || this.ContainsPoint(x, y - theFuzzFactor) || this.ContainsPoint(x + theFuzzFactor, y - theFuzzFactor) || this.ContainsPoint(x + theFuzzFactor, y) || this.ContainsPoint(x + theFuzzFactor, y + theFuzzFactor) || this.ContainsPoint(x, y + theFuzzFactor) || this.ContainsPoint(x - theFuzzFactor, y + theFuzzFactor) || this.ContainsPoint(x - theFuzzFactor, y);
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", this.mCol, this.mRow);
		}

		public GemTypes mType;

		public float mZVelocity;

		public float mZRot;

		public float mZRotAdd;

		public bool mIsElectrocuting;

		public float mElectrocutePercent;

		public int mLastActiveTick;

		public int mChangedTick;

		public int mLastGemType;

		public int mBombifyTick;

		public int mExplodeDelay;

		public int mSwapMultX;

		public int mSwapMultY;

		public int mSwapDirX;

		public int mSwapDirY;

		public int mSpinFrame;

		public int mEffectNum;

		public int mEffectDelay;

		public float mEffectPercent;

		public double mRot;

		public double mRotVel;

		public double mRotDist;

		public int mBombCountdown;

		public bool mBombified;

		private static ReusableObjectHelper<CPieceBej2> reuseHelper = new ReusableObjectHelper<CPieceBej2>();
	}
}
