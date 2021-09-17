using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class Effect : IDisposable
	{
		public static Effect alloc()
		{
			return Effect.alloc(Effect.Type.TYPE_NONE);
		}

		public static void initPool()
		{
			Effect.thePool_ = new SimpleObjectPool(8192, typeof(Effect));
		}

		public static Effect alloc(Effect.Type theType)
		{
			Effect effect = (Effect)Effect.thePool_.alloc();
			effect.init(theType);
			return effect;
		}

		public virtual void release()
		{
			this.Dispose();
			Effect.thePool_.release(this);
		}

		public Effect()
			: this(Effect.Type.TYPE_NONE)
		{
		}

		public void init(Effect.Type theType)
		{
			this.mDoubleSpeed = false;
			this.mOverlay = false;
			this.mX = 0f;
			this.mY = 0f;
			this.mZ = 0f;
			this.mPieceRel = null;
			this.mStopWhenPieceRelMissing = false;
			this.mDX = (this.mDY = (this.mDZ = 0f));
			this.mDXScalar = (this.mDYScalar = (this.mDZScalar = 1f));
			this.mGravity = 0f;
			this.mDelay = 0f;
			this.mLightSize = 0f;
			this.mLightIntensity = 0f;
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
			this.mDeleteImage = false;
			this.mDeleteMe = false;
			this.mRefCount = 0;
			this.SetType(theType);
		}

		public Effect(Effect.Type theType)
		{
			this.init(theType);
		}

		public virtual void Dispose()
		{
			if (this.mDeleteImage)
			{
				this.mImage.Dispose();
				this.mImage = null;
			}
		}

		public virtual void Update()
		{
			if (this.mDeleteMe)
			{
				return;
			}
			this.mCurvedAlpha.IncInVal();
			this.mCurvedScale.IncInVal();
		}

		public virtual void Draw(Graphics g)
		{
		}

		public virtual void SetType(Effect.Type theType)
		{
			this.mType = theType;
			switch (this.mType)
			{
			case Effect.Type.TYPE_EMBER_BOTTOM:
			case Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM:
			case Effect.Type.TYPE_EMBER:
			case Effect.Type.TYPE_EMBER_FADEINOUT:
				this.mImage = GlobalMembersResourcesWP.IMAGE_FIREPARTICLE;
				this.mColor = new Color(255, BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(64) + GlobalMembers.M(64), BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(32) + GlobalMembers.M(30));
				this.mGravity = GlobalMembers.M(-0f);
				this.mScale = GlobalMembers.M(0.75f);
				this.mDScale = GlobalMembers.M(0.005f);
				this.mAngle = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
				this.mDAngle = GlobalMembers.M(0.01f);
				if (this.mType == Effect.Type.TYPE_EMBER_FADEINOUT || this.mType == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
				{
					this.mAlpha = 0.01f;
					this.mDAlpha = GlobalMembers.M(0.02f);
					this.mStage = 0;
					return;
				}
				break;
			case Effect.Type.TYPE_SMOKE_PUFF:
				this.mImage = GlobalMembersResourcesWP.IMAGE_SMOKE;
				this.mGravity = GlobalMembers.MS(-0.005f);
				this.mAlpha = GlobalMembers.M(0.5f);
				this.mDAlpha = GlobalMembers.M(-0.005f);
				return;
			case Effect.Type.TYPE_DROPLET:
				this.mImage = GlobalMembersResourcesWP.IMAGE_DRIP;
				this.mDAlpha = 0f;
				this.mGravity = GlobalMembers.MS(0.05f);
				return;
			case Effect.Type.TYPE_STEAM_COMET:
				this.mImage = GlobalMembersResourcesWP.IMAGE_FX_STEAM;
				this.mGravity = GlobalMembers.MS(-0.005f);
				this.mAngle = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
				this.mDAngle = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.04f);
				this.mAlpha = GlobalMembers.M(0.85f);
				this.mDAlpha = 0f;
				break;
			case Effect.Type.TYPE_FRUIT_SPARK:
				this.mGravity = GlobalMembers.MS(0.005f);
				this.mDX = GlobalMembersUtils.GetRandFloat() * GlobalMembers.MS(1f);
				this.mDY = GlobalMembersUtils.GetRandFloat() * GlobalMembers.MS(1f);
				this.mScale = 0.2f;
				this.mAlpha = 1f;
				this.mDAlpha = -0.005f;
				this.mAngle = (GlobalMembers.gApp.Is3DAccelerated() ? (GlobalMembersUtils.GetRandFloat() * 3.14159274f) : 0f);
				return;
			case Effect.Type.TYPE_GEM_SHARD:
				this.mFrame = BejeweledLivePlus.Misc.Common.Rand() % 40;
				this.mUpdateDiv = 0;
				this.mAlpha = 1f;
				this.mDAlpha = GlobalMembers.M(-0.005f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(-0.01f);
				this.mDecel = 1f;
				return;
			case Effect.Type.TYPE_COUNTDOWN_SHARD:
				break;
			case Effect.Type.TYPE_SPARKLE_SHARD:
				this.mUpdateDiv = BejeweledLivePlus.Misc.Common.Rand() % 4 + 3;
				this.mDX = GlobalMembers.S(-1f + (float)(BejeweledLivePlus.Misc.Common.Rand() % 20) * 0.1f);
				this.mDY = GlobalMembers.S((float)(BejeweledLivePlus.Misc.Common.Rand() % 50) * 0.1f);
				this.mColor = new Color(255, 255, 255);
				return;
			case Effect.Type.TYPE_STEAM:
				this.mImage = GlobalMembersResourcesWP.IMAGE_FX_STEAM;
				this.mGravity = GlobalMembers.MS(-0.005f);
				this.mAngle = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
				this.mDAngle = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.04f);
				this.mAlpha = GlobalMembers.M(0.85f);
				this.mDAlpha = 0f;
				this.mValue[0] = GlobalMembers.M(0.5f);
				this.mValue[1] = GlobalMembers.M(-0.02f);
				this.mValue[2] = GlobalMembers.M(0.95f);
				return;
			case Effect.Type.TYPE_GLITTER_SPARK:
				this.mImage = GlobalMembersResourcesWP.IMAGE_GEM_FRUIT_SPARK;
				this.mIsAdditive = true;
				this.mGravity = GlobalMembers.MS(0.01f);
				this.mAlpha = GlobalMembers.M(1f);
				this.mDAlpha = GlobalMembers.M(0f);
				this.mScale = GlobalMembers.M(0.5f);
				this.mDScale = GlobalMembers.M(-0.005f);
				return;
			default:
				return;
			}
		}

		private static SimpleObjectPool thePool_;

		public Effect.Type mType;

		public bool mOverlay;

		public float mX;

		public float mY;

		public float mZ;

		public float mDX;

		public float mDY;

		public float mDZ;

		public Piece mPieceRel;

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

		public float mLightSize;

		public float mLightIntensity;

		public float mMinScale;

		public float mMaxScale;

		public Color mColor = default(Color);

		public Color mColor2 = default(Color);

		public float mAngle;

		public float mDAngle;

		public int mStage;

		public float mTimer;

		public CurvedVal mCurvedAlpha = new CurvedVal();

		public CurvedVal mCurvedScale = new CurvedVal();

		public Image mImage;

		public bool mDoubleSpeed;

		public int mUpdateDiv;

		public bool mIsCyclingColor;

		public int mCurHue;

		public bool mIsAdditive;

		public float[] mValue = new float[4];

		public uint mPieceId;

		public int mFlags;

		public bool mDeleteMe;

		public int mRefCount;

		public EffectsManager mFXManager;

		public bool mDeleteImage;

		public bool mStopWhenPieceRelMissing;

		public enum Type
		{
			TYPE_NONE,
			TYPE_CUSTOMCLASS,
			TYPE_BLAST_RING,
			TYPE_EMBER_BOTTOM,
			TYPE_EMBER_FADEINOUT_BOTTOM,
			TYPE_EMBER,
			TYPE_EMBER_FADEINOUT,
			TYPE_SMOKE_PUFF,
			TYPE_DROPLET,
			TYPE_STEAM_COMET,
			TYPE_FRUIT_SPARK,
			TYPE_GEM_SHARD,
			TYPE_COUNTDOWN_SHARD,
			TYPE_SPARKLE_SHARD,
			TYPE_STEAM,
			TYPE_GLITTER_SPARK,
			TYPE_CURSOR_RING,
			TYPE_LIGHT,
			TYPE_WALL_ROCK,
			TYPE_QUAKE_DUST,
			TYPE_HYPERCUBE_ENERGIZE,
			TYPE_TIME_BONUS,
			TYPE_PI,
			TYPE_POPANIM,
			NUM_TYPES
		}

		public enum FLAG
		{
			FLAG_SCALE_FADEINOUT = 1,
			FLAG_ALPHA_FADEINOUT,
			FLAG_ALPHA_FADEINDELAY = 4,
			FLAG_HYPERSPACE_ONLY = 8
		}
	}
}
