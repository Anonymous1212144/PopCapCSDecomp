using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class EffectBej3
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 1000; i++)
			{
				new EffectBej3(EffectBej3.EffectType.TYPE_SPARKLE_SHARD).PrepareForReuse();
			}
		}

		public static EffectBej3 GetNewEffectBej3(EffectBej3.EffectType theType)
		{
			if (EffectBej3.unusedObjects.Count > 0)
			{
				EffectBej3 effectBej = EffectBej3.unusedObjects.Pop();
				effectBej.Reset(theType);
				return effectBej;
			}
			return new EffectBej3(theType);
		}

		public void PrepareForReuse()
		{
			EffectBej3.unusedObjects.Push(this);
		}

		private void Reset(EffectBej3.EffectType theType)
		{
			this.mType = theType;
			this.mOverlay = false;
			this.mX = 0f;
			this.mY = 0f;
			this.mZ = 0f;
			this.mPieceIdRel = -1;
			this.mDX = (this.mDY = (this.mDZ = 0f));
			this.mDXScalar = (this.mDYScalar = (this.mDZScalar = 1f));
			this.mGravity = 0f;
			this.mDelay = 0f;
			this.mScale = 1f;
			this.mDScale = 0f;
			this.mMinScale = 0f;
			this.mMaxScale = 10000f;
			this.mFrame = 0;
			this.mAngle = (this.mDAngle = 0f);
			this.mColor = new Color(255, 255, 255);
			this.mIsCyclingColor = false;
			this.mCurHue = 0;
			this.mUpdateDiv = 1;
			this.mAlpha = 1f;
			this.mDAlpha = -0.01f;
			this.mMaxAlpha = 1f;
			this.mImage = null;
			this.mFlags = 0;
			this.mIsAdditive = false;
			switch (this.mType)
			{
			case EffectBej3.EffectType.TYPE_GEM_SHARD:
				this.mFrame = Board.Rand() % 40;
				this.mUpdateDiv = 0;
				this.mAlpha = 1f;
				this.mDAlpha = Constants.mConstants.M(-0.005f) + stdC.fabsf(Board.GetRandFloat()) * Constants.mConstants.M(-0.01f);
				this.mDecel = 1f;
				break;
			case EffectBej3.EffectType.TYPE_SPARKLE_SHARD:
				this.mUpdateDiv = Board.Rand() % 4 + 3;
				this.mDX = (-1f + (float)Board.Rand() % 20f * 0.1f) * 480f / 1024f;
				this.mDY = (float)Board.Rand() % 50f * 0.1f * 480f / 1024f;
				this.mColor = new Color(255, 255, 255);
				break;
			}
			this.mDeleteImage = false;
			this.mDeleteMe = false;
		}

		private EffectBej3(EffectBej3.EffectType theType)
		{
			this.Reset(theType);
		}

		public void Update()
		{
		}

		public void Draw(Graphics g)
		{
		}

		public Color GetAlphaColor()
		{
			return new Color((float)this.mColor.R, (float)this.mColor.G, (float)this.mColor.B, this.mAlpha);
		}

		public EffectBej3.EffectType mType;

		public bool mOverlay;

		public float mX;

		public float mY;

		public float mZ;

		public float mDX;

		public float mDY;

		public float mDZ;

		public int mPieceIdRel;

		public float mDXScalar;

		public float mDYScalar;

		public float mDZScalar;

		public float mGravity;

		public int mFrame;

		public float mDelay;

		public int mGemType;

		public float mDecel;

		public float mAlpha;

		public float mDAlpha;

		public float mMaxAlpha;

		public float mScale;

		public float mDScale;

		public float mMinScale;

		public float mMaxScale;

		public Color mColor = default(Color);

		public Color mColor2 = default(Color);

		public float mAngle;

		public float mDAngle;

		public int mStage;

		public float mTimer;

		public CurvedVal mCurvedAlpha = CurvedVal.GetNewCurvedVal();

		public CurvedVal mCurvedScale = CurvedVal.GetNewCurvedVal();

		public Image mImage;

		public int mUpdateDiv;

		public bool mIsCyclingColor;

		public int mCurHue;

		public bool mIsAdditive;

		public float[] mValue = new float[4];

		public uint mPieceId;

		public int mFlags;

		public bool mDeleteMe;

		public EffectsManager mFXManager;

		public bool mDeleteImage;

		private static Stack<EffectBej3> unusedObjects = new Stack<EffectBej3>();

		public enum EffectType
		{
			TYPE_EXPLOSION,
			TYPE_CUSTOMCLASS,
			TYPE_GEM_SHARD,
			TYPE_SPARKLE_SHARD,
			TYPE_GLITTER_SPARK
		}

		public enum FadeFlag
		{
			FLAG_SCALE_FADEINOUT = 1,
			FLAG_ALPHA_FADEINOUT,
			FLAG_ALPHA_FADEINDELAY = 4
		}
	}
}
