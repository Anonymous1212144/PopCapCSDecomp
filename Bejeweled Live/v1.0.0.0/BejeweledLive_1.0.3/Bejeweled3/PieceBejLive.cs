using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class PieceBejLive : Piece, IEquatable<PieceBejLive>
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 100; i++)
			{
				new PieceBejLive().PrepareForReuse();
			}
		}

		public static PieceBejLive GetNewCPieceBejLive(BoardBejLive theBoard)
		{
			if (PieceBejLive.unusedObjects.Count > 0)
			{
				PieceBejLive pieceBejLive = PieceBejLive.unusedObjects.Pop();
				pieceBejLive.Reset(theBoard);
				return pieceBejLive;
			}
			return new PieceBejLive(theBoard);
		}

		public static PieceBejLive GetNewCPieceBejLive()
		{
			if (PieceBejLive.unusedObjects.Count > 0)
			{
				PieceBejLive pieceBejLive = PieceBejLive.unusedObjects.Pop();
				pieceBejLive.Reset();
				return pieceBejLive;
			}
			return new PieceBejLive();
		}

		private PieceBejLive(BoardBejLive theBoard)
		{
			this.Reset(theBoard);
		}

		private PieceBejLive()
		{
			this.Reset();
		}

		public void Reset(BoardBejLive theBoard)
		{
			this.Reset();
			this.mBoard = theBoard;
			this.Init(this.mBoard.mNextPieceId++);
		}

		private new void Reset()
		{
			base.Reset();
			this.mScale.Reset();
		}

		public override void PrepareForReuse()
		{
			if (this.mBoard != null)
			{
				this.mBoard.RemoveFromPieceMap(this.mId);
				this.mBoard.RemoveGemsFromMultiFontBatch(this);
				this.mBoard.RemoveGemsFromBlitzBatch(this);
			}
			if (this.mOverlay != null)
			{
				this.mOverlay.Dispose();
			}
			if (this.mOverlayGlow != null)
			{
				this.mOverlayGlow.Dispose();
			}
			PieceBejLive.unusedObjects.Push(this);
		}

		public void Init(int theId)
		{
			this.mCanDestroy = false;
			this.mCreatedTick = 0;
			this.mFlags = 0;
			this.mColor = -1;
			this.mDestColor = -1;
			this.mExplodeDelay = 0;
			this.mLastActiveTick = 0;
			this.mChangedTick = 0;
			this.mLastColor = -1;
			this.mSwapTick = -1;
			this.mIsElectrocuting = false;
			this.mElectrocutePercent = 0f;
			this.mDestructing = false;
			this.mIsExploding = false;
			this.mCanMatch = true;
			this.mTallied = false;
			this.mCanDestroy = true;
			this.mIsBulging = false;
			this.mImmunityCount = 0;
			this.mMoveCreditId = -1;
			this.mLastMoveCreditId = -1;
			this.mMatchId = -1;
			this.mX = 0f;
			this.mY = 0f;
			this.mCol = 0;
			this.mRow = 0;
			this.mFallVelocity = 0f;
			this.mXVelocity = 0f;
			this.mDestCol = 0;
			this.mDestRow = 0;
			this.mAlpha.SetConstant(1.0);
			this.mRotPct = 0f;
			this.mSelected = false;
			this.mScale.SetConstant(1.0);
			this.mCounter = 0;
			this.mCounterDelay = 0;
			this.mTimer = 0;
			this.mTimerThreshold = 100;
			this.mShakeOfsX = 0f;
			this.mShakeOfsY = 0f;
			this.mShakeScale = 0f;
			this.mShakeAngle = 0f;
			this.mShakeTime = 0f;
			this.mSpinFrame = 0;
			this.mSpinSpeed = 0f;
			this.mDestSpinSpeed = 0f;
			this.mSpinSpeedHoldTime = 0f;
			this.mOverlay = (this.mOverlayGlow = null);
			this.mFlyVX = 0f;
			this.mFlyVY = 0f;
			this.mIsFlying = false;
			this.mOfsY = 0;
			this.mOfsX = 0;
			this.mbDrawSelector = false;
			this.mId = theId;
			this.mBoard.AddToPieceMap(this.mId, this);
			this.miFlameGemFrame = 0;
			this.miHyperCubeFrame = 0;
			this.miLaserGemFrame = 0;
			this.miLaserGlowFrame = 0;
			this.miPreviousTick = 0;
			this.mfStarRotate = 0.0;
			this.mfStarRotate2 = 0.0;
			this.mfStarFade = 0.0;
		}

		public bool IsShrinking()
		{
			return this.mShrinking;
		}

		public bool IsButton()
		{
			return this.IsFlagSet(17) || this.IsFlagSet(18);
		}

		public float CX()
		{
			return this.mX + (float)(GameConstants.GEM_WIDTH / 2);
		}

		public float CY()
		{
			return this.mY + (float)(GameConstants.GEM_HEIGHT / 2);
		}

		public int GetRow()
		{
			return this.mBoard.GetRowAt((int)this.mY);
		}

		public int GetCol()
		{
			return this.mBoard.GetColAt((int)this.mX);
		}

		public void SetFlag(PIECEFLAG theFlag)
		{
			this.SetFlag((int)theFlag);
		}

		public void SetFlag(int theFlag)
		{
			this.SetFlag(theFlag, true);
		}

		public void SetFlag(PIECEFLAG theFlag, bool theValue)
		{
			this.SetFlag((int)theFlag, theValue);
		}

		public void SetFlag(int theFlag, bool theValue)
		{
			uint flagBit = GlobalMembersPieceBej3.GetFlagBit(theFlag);
			if (theValue)
			{
				this.mFlags |= (int)flagBit;
				return;
			}
			this.mFlags &= (int)(~(int)flagBit);
		}

		public void ClearFlag(PIECEFLAG theFlag)
		{
			this.ClearFlag((int)theFlag);
		}

		public void ClearFlag(int theFlag)
		{
			this.SetFlag(theFlag, false);
		}

		public void ClearFlags()
		{
			this.mFlags = 0;
		}

		public float GetAngleRadius(float theAngle)
		{
			while (theAngle >= 6.28318548f)
			{
				theAngle -= 6.28318548f;
			}
			while (theAngle < 0f)
			{
				theAngle += 6.28318548f;
			}
			float num = 256f * theAngle / 6.28318548f;
			int num2 = (int)Math.Min(19f, 20f * this.mRotPct);
			int num3 = (int)num;
			if (num3 < 0)
			{
				return GemOutlines.GEM_OUTLINE_RADIUS_POINTS[num2, this.mColor, 0];
			}
			if (num3 >= 255)
			{
				return GemOutlines.GEM_OUTLINE_RADIUS_POINTS[num2, this.mColor, 255];
			}
			float num4 = num - (float)num3;
			return GemOutlines.GEM_OUTLINE_RADIUS_POINTS[num2, this.mColor, num3] * (1f - num4) + GemOutlines.GEM_OUTLINE_RADIUS_POINTS[num2, this.mColor, num3 + 1] * num4;
		}

		public bool IsFlagSet(PIECEFLAG theFlag)
		{
			return this.IsFlagSet((int)theFlag);
		}

		public bool IsFlagSet(int theFlag)
		{
			uint flagBit = GlobalMembersPieceBej3.GetFlagBit(theFlag);
			return ((long)this.mFlags & (long)((ulong)flagBit)) != 0L;
		}

		public void Update()
		{
			this.mOverlayBulge.IncInVal();
			this.mOverlayCurve.IncInVal();
			if (this.mShakeScale > 0f)
			{
				this.mShakeAngle = 3.14159274f * Board.GetRandFloat();
				this.mShakeOfsX = (float)(Math.Cos((double)this.mShakeAngle) * (double)this.mShakeScale * (double)GameConstants.GEM_WIDTH / 20.0);
				this.mShakeOfsY = (float)(Math.Sin((double)this.mShakeAngle) * (double)this.mShakeScale * (double)GameConstants.GEM_HEIGHT / 20.0);
			}
			else
			{
				this.mShakeOfsX = (this.mShakeOfsY = 0f);
			}
			float num = this.mElectrocutePercent;
			if (this.mSparkleLife > 0)
			{
				this.mSparkleLife--;
				if (this.mSparkleLife % 5 == 0)
				{
					this.mSparkleFrame++;
				}
			}
			if (this.IsFlagSet(PIECEFLAG.PIECEFLAG_COUNTER) && this.mOverlay == null)
			{
				this.StampOverlay();
			}
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, Colour: {2}", this.mCol, this.mRow, this.mColor);
		}

		public void StampOverlay()
		{
		}

		public void DrawFlameGemOverlay(Graphics g, int iUpdateCnt, double fShowPct)
		{
		}

		public void DrawFlameGem(Graphics g, int iUpdateCnt, double fShowPct)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * fShowPct)));
			g.DrawImage(AtlasResources.GetImageInAtlasById(10038 + this.mColor), (int)(this.mX + (float)this.mOfsX - (float)Constants.mConstants.Piece_FlameGem_Offset + this.mFlyVX), (int)(this.mY + (float)this.mOfsY - (float)Constants.mConstants.Piece_FlameGem_Offset + this.mFlyVY), new TRect(Constants.mConstants.Piece_FlameGem_Size * (this.miFlameGemFrame % 20), 0, Constants.mConstants.Piece_FlameGem_Size, Constants.mConstants.Piece_FlameGem_Size));
			if (iUpdateCnt - this.miPreviousTick >= 5)
			{
				this.miFlameGemFrame = (this.miFlameGemFrame + 1) % 20;
				this.miPreviousTick = iUpdateCnt;
			}
			g.SetColorizeImages(false);
		}

		public void DrawHyperGem(Graphics g, int iUpdateCnt, double fShowPct)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * fShowPct)));
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawImage(AtlasResources.IMAGE_HYPERCUBE_LOWER, (int)(this.mX + (float)this.mOfsX - (float)Constants.mConstants.Piece_HyperCube_Offset + this.mFlyVX), (int)(this.mY + (float)this.mOfsY - (float)Constants.mConstants.Piece_HyperCube_Offset + this.mFlyVY), new TRect(Constants.mConstants.Piece_HyperCube_Size * (this.miHyperCubeFrame % 5), Constants.mConstants.Piece_HyperCube_Size * (this.miHyperCubeFrame / 5), Constants.mConstants.Piece_HyperCube_Size, Constants.mConstants.Piece_HyperCube_Size));
			if (iUpdateCnt - this.miPreviousTick >= 5)
			{
				this.miHyperCubeFrame = ((this.miHyperCubeFrame + 1 == 30) ? 0 : (this.miHyperCubeFrame + 1));
				this.miPreviousTick = iUpdateCnt;
			}
			g.SetColorizeImages(false);
		}

		public void DrawHyperGemOverlay(Graphics g, int iUpdateCnt, double fShowPct)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * fShowPct)));
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			g.DrawImage(AtlasResources.IMAGE_HYPERCUBE_ADD, (int)(this.mX + (float)this.mOfsX - (float)Constants.mConstants.Piece_HyperCube_Offset + this.mFlyVX), (int)(this.mY + (float)this.mOfsY - (float)Constants.mConstants.Piece_HyperCube_Offset + this.mFlyVY), new TRect(Constants.mConstants.Piece_HyperCube_Size * (this.miHyperCubeFrame % 5), Constants.mConstants.Piece_HyperCube_Size * (this.miHyperCubeFrame / 5), Constants.mConstants.Piece_HyperCube_Size, Constants.mConstants.Piece_HyperCube_Size));
			g.SetColorizeImages(false);
		}

		public void DrawLaserGemLowerLayer(Graphics g, int iUpdateCnt, double fShowPct)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * fShowPct)));
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawImage(AtlasResources.GetImageInAtlasById(10047 + this.mColor), (int)(this.mX + (float)this.mOfsX - (float)Constants.mConstants.Piece_LASER_GEM_OFFSET + this.mFlyVX), (int)(this.mY + (float)this.mOfsY - (float)Constants.mConstants.Piece_LASER_GEM_OFFSET + this.mFlyVY), new TRect(Constants.mConstants.LASER_GEM_LENGTH * this.miLaserGemFrame, 0, Constants.mConstants.LASER_GEM_LENGTH, Constants.mConstants.LASER_GEM_LENGTH));
			if (iUpdateCnt - this.miPreviousTick >= 10)
			{
				this.miLaserGemFrame = ((this.miLaserGemFrame + 1 == 20) ? 0 : (this.miLaserGemFrame + 1));
				this.miLaserGlowFrame = ((this.miLaserGlowFrame + 1 == 11) ? 0 : (this.miLaserGlowFrame + 1));
				this.miPreviousTick = iUpdateCnt;
			}
			g.SetColorizeImages(false);
		}

		public void DrawLaserGemOverlay(Graphics g, int iUpdateCnt, double fShowPct)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * fShowPct)));
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			g.DrawImage(AtlasResources.GetImageInAtlasById(10054), (int)(this.mX + (float)this.mOfsX - (float)Constants.mConstants.LASER_GLOW_OFFSET + this.mFlyVX), (int)(this.mY + (float)this.mOfsY - (float)Constants.mConstants.LASER_GLOW_OFFSET + this.mFlyVY), new TRect(Constants.mConstants.LASER_GLOW_LENGTH * (this.miLaserGlowFrame % 11), Constants.mConstants.LASER_GLOW_LENGTH * (this.miLaserGlowFrame / 11), Constants.mConstants.LASER_GLOW_LENGTH, Constants.mConstants.LASER_GLOW_LENGTH));
			g.SetColorizeImages(false);
		}

		public void DrawMultiplierGem(Graphics g, int iUpdateCnt, int iMultiplier, double fShowPct)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * fShowPct)));
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawImage(AtlasResources.GetImageInAtlasById(10056 + this.mColor), (int)this.mX - 2, (int)this.mY - 2, new TRect(0, 0, Constants.mConstants.BoardBej3_MultiplierGem_Size, Constants.mConstants.BoardBej3_MultiplierGem_Size));
			g.DrawImage(AtlasResources.GetImageInAtlasById(10064 + (iMultiplier - 1)), (int)this.mX + Constants.mConstants.BoardBej3_MultiplierGem_Text_Offset, (int)this.mY + Constants.mConstants.BoardBej3_MultiplierGem_Text_Offset, new TRect(0, 0, Constants.mConstants.BoardBej3_MultiplierGem_Text_Size, Constants.mConstants.BoardBej3_MultiplierGem_Text_Size));
			g.SetColorizeImages(false);
		}

		public void Shake()
		{
			this.mOfsX = (int)((float)(GameConstants.GEM_WIDTH >> 3) * Board.GetRandFloat());
			this.mOfsY = (int)((float)(GameConstants.GEM_WIDTH >> 3) * Board.GetRandFloat());
		}

		public void GameOverFall()
		{
			this.mFlyVX += this.mXVelocity;
			this.mFlyVY += this.mFallVelocity;
			this.mFallVelocity += 0.046875f;
			this.mFallVelocity += 0.01171875f;
		}

		bool IEquatable<PieceBejLive>.Equals(PieceBejLive other)
		{
			return this == other;
		}

		public override int GetHashCode()
		{
			return this.mId;
		}

		public override bool Equals(object obj)
		{
			PieceBejLive pieceBejLive = obj as PieceBejLive;
			return pieceBejLive != null && pieceBejLive.mId == this.mId;
		}

		public PieceParticleEmitter AssociatedParticleEmitter;

		private static Stack<PieceBejLive> unusedObjects = new Stack<PieceBejLive>(100);

		public BoardBejLive mBoard;

		public int mId;

		public int mCreatedTick;

		public CurvedVal mScale = CurvedVal.GetNewCurvedVal();

		public CurvedVal mAlpha = CurvedVal.GetNewCurvedVal();

		public CurvedVal mSelectorAlpha = CurvedVal.GetNewCurvedVal();

		public float mRotPct;

		public bool mIsElectrocuting;

		public float mElectrocutePercent;

		public bool mDestructing;

		public bool mIsExploding;

		public bool mCanMatch;

		public bool mTallied;

		public bool mCanDestroy;

		public bool mIsBulging;

		public int mImmunityCount;

		public int mMoveCreditId;

		public int mLastMoveCreditId;

		public int mMatchId;

		public CurvedVal mDestPct = CurvedVal.GetNewCurvedVal();

		public int mDestCol;

		public int mDestRow;

		public int mChangedTick;

		public int mSwapTick;

		public int mLastActiveTick;

		public int mLastColor;

		public int mColor;

		public int mDestColor;

		public int mFlags;

		public int mExplodeDelay;

		public bool mSelected;

		public int mCounter;

		public int mCounterDelay;

		public int mTimer;

		public int mTimerThreshold;

		public float mShakeOfsX;

		public float mShakeOfsY;

		public float mShakeTime;

		public float mShakeScale;

		public float mShakeAngle;

		public int mSpinFrame;

		public float mSpinSpeed;

		public float mDestSpinSpeed;

		public float mSpinSpeedHoldTime;

		public CurvedVal mHintAlpha = CurvedVal.GetNewCurvedVal();

		public CurvedVal mHintArrowPos = CurvedVal.GetNewCurvedVal();

		public CurvedVal mHintFlashScale = CurvedVal.GetNewCurvedVal();

		public CurvedVal mHintFlashAlpha = CurvedVal.GetNewCurvedVal();

		public MemoryImage mOverlay;

		public MemoryImage mOverlayGlow;

		public CurvedVal mOverlayCurve = CurvedVal.GetNewCurvedVal();

		public CurvedVal mOverlayBulge = CurvedVal.GetNewCurvedVal();

		public CurvedVal mAnimCurve = CurvedVal.GetNewCurvedVal();

		public float mFlyVX;

		public float mFlyVY;

		public bool mIsFlying;

		public bool mbDrawSelector;

		private int miFlameGemFrame;

		private double mfStarRotate;

		private double mfStarRotate2;

		private double mfStarFade;

		private int miHyperCubeFrame;

		private int miLaserGemFrame;

		private int miLaserGlowFrame;

		private int miPreviousTick;
	}
}
