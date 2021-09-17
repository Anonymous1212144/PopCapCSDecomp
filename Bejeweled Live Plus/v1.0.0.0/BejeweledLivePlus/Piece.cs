using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus
{
	public class Piece : IDisposable
	{
		public static Piece alloc(Board board, int id)
		{
			Piece piece = (Piece)Piece.pool_.alloc();
			piece.mBoard = board;
			if (id < 0)
			{
				id = board.mNextPieceId++;
			}
			piece.Init(id);
			return piece;
		}

		public static Piece alloc(Board board)
		{
			return Piece.alloc(board, -1);
		}

		public void release()
		{
			this.ClearBoundEffects();
			if (this.mBoard != null)
			{
				this.mBoard.RemoveFromPieceMap(this.mId);
			}
			if (this.mOverlay != null)
			{
				this.mOverlay.Dispose();
			}
			if (this.mOverlayGlow != null)
			{
				this.mOverlayGlow.Dispose();
			}
			Piece.pool_.release(this);
		}

		public Piece()
		{
		}

		public Piece(Board theBoard)
		{
			this.mBoard = theBoard;
			this.Init(this.mBoard.mNextPieceId++);
		}

		public Piece(Board theBoard, int theId)
		{
			this.mBoard = theBoard;
			this.Init(theId);
		}

		public void Dispose()
		{
			this.ClearBoundEffects();
			if (this.mBoard != null)
			{
				this.mBoard.RemoveFromPieceMap(this.mId);
			}
			if (this.mOverlay != null)
			{
				this.mOverlay.Dispose();
			}
			if (this.mOverlayGlow != null)
			{
				this.mOverlayGlow.Dispose();
			}
		}

		public void Init(int theId)
		{
			this.mCreatedTick = 0;
			this.mFlags = 0;
			this.mDisallowFlags = 0;
			this.mColor = -1;
			this.mDestColor = -1;
			this.mExplodeDelay = 0;
			this.mExplodeSourceId = -1;
			this.mExplodeSourceFlags = 0;
			this.mLastActiveTick = 0;
			this.mChangedTick = 0;
			this.mLastColor = -1;
			this.mSwapTick = -1;
			this.mIsElectrocuting = false;
			this.mElectrocutePercent = 0f;
			this.mDestructing = false;
			this.mIsExploding = false;
			this.mCanMatch = true;
			this.mCanSwap = true;
			this.mCanScramble = true;
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
			this.mDestCol = 0;
			this.mDestRow = 0;
			this.mAlpha.SetConstant(1.0);
			this.mRotPct = 0f;
			this.mSelected = false;
			this.mHidePct = 0f;
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
			this.mSpinFrame = 0f;
			this.mSpinSpeed = 0f;
			this.mDestSpinSpeed = 0f;
			this.mSpinSpeedHoldTime = 0f;
			this.mOverlay = (this.mOverlayGlow = null);
			this.mFlyVX = 0f;
			this.mFlyVY = 0f;
			this.mFlyAY = 0f;
			this.mForceScreenY = false;
			this.mForcedY = 0f;
			this.mSelectorAlpha = new CurvedVal();
			this.mDestPct = new CurvedVal();
			this.mId = theId;
			this.mBoard.AddToPieceMap(this.mId, this);
		}

		public Piece CopyForm(Piece obj)
		{
			if (obj != null)
			{
				this.mBoard = obj.mBoard;
				this.mId = obj.mId;
				this.mCol = obj.mCol;
				this.mRow = obj.mRow;
				this.mX = obj.mX;
				this.mY = obj.mY;
				this.mCreatedTick = obj.mCreatedTick;
				this.mFallVelocity = obj.mFallVelocity;
				this.mRotPct = obj.mRotPct;
				this.mIsElectrocuting = obj.mIsElectrocuting;
				this.mElectrocutePercent = obj.mElectrocutePercent;
				this.mDestructing = obj.mDestructing;
				this.mIsExploding = obj.mIsExploding;
				this.mCanMatch = obj.mCanMatch;
				this.mCanSwap = obj.mCanMatch;
				this.mCanScramble = obj.mCanScramble;
				this.mTallied = obj.mTallied;
				this.mCanDestroy = obj.mCanDestroy;
				this.mIsBulging = obj.mIsBulging;
				this.mImmunityCount = obj.mImmunityCount;
				this.mMoveCreditId = obj.mMoveCreditId;
				this.mLastMoveCreditId = obj.mLastMoveCreditId;
				this.mMatchId = obj.mMatchId;
				this.mDestCol = obj.mDestCol;
				this.mDestRow = obj.mDestRow;
				this.mChangedTick = obj.mChangedTick;
				this.mSwapTick = obj.mSwapTick;
				this.mLastActiveTick = obj.mLastActiveTick;
				this.mLastColor = obj.mLastColor;
				this.mColor = obj.mColor;
				this.mDestColor = obj.mDestColor;
				this.mFlags = obj.mFlags;
				this.mDisallowFlags = obj.mDisallowFlags;
				this.mExplodeDelay = obj.mExplodeDelay;
				this.mExplodeSourceId = obj.mExplodeSourceId;
				this.mExplodeSourceFlags = obj.mExplodeSourceFlags;
				this.mSelected = obj.mSelected;
				this.mHidePct = obj.mHidePct;
				this.mCounter = obj.mCounter;
				this.mCounterDelay = obj.mCounterDelay;
				this.mTimer = obj.mTimer;
				this.mTimerThreshold = obj.mTimerThreshold;
				this.mMoveCountdownPerLevel = obj.mMoveCountdownPerLevel;
				this.mShakeOfsX = obj.mShakeOfsX;
				this.mShakeOfsY = obj.mShakeOfsY;
				this.mShakeTime = obj.mShakeTime;
				this.mShakeScale = obj.mShakeScale;
				this.mShakeAngle = obj.mShakeAngle;
				this.mSpinFrame = obj.mSpinFrame;
				this.mSpinSpeed = obj.mSpinSpeed;
				this.mDestSpinSpeed = obj.mDestSpinSpeed;
				this.mSpinSpeedHoldTime = obj.mSpinSpeedHoldTime;
				this.mFlyVX = obj.mFlyAY;
				this.mFlyVY = obj.mFlyVX;
				this.mFlyAY = obj.mFlyAY;
				this.mForceScreenY = obj.mForceScreenY;
				this.mForcedY = obj.mForcedY;
			}
			return this;
		}

		public bool IsShrinking()
		{
			return this.mScale.mRamp == 6;
		}

		public bool IsButton()
		{
			return this.IsFlagSet(6144U);
		}

		public void BindEffect(Effect theEffect)
		{
			theEffect.mPieceRel = this;
			theEffect.mRefCount++;
			this.mBoundEffects.Add(theEffect);
		}

		public void ClearBoundEffects()
		{
			while (this.mBoundEffects.Count > 0)
			{
				this.mBoundEffects[this.mBoundEffects.Count - 1].mPieceRel = null;
				this.mBoundEffects[this.mBoundEffects.Count - 1].mRefCount--;
				this.mBoundEffects.RemoveAt(this.mBoundEffects.Count - 1);
			}
		}

		public void ClearHyperspaceEffects()
		{
			for (int i = 0; i < this.mBoundEffects.Count; i++)
			{
				Effect effect = this.mBoundEffects[i];
				if ((effect.mFlags & 8) != 0)
				{
					effect.mPieceRel = null;
					effect.mRefCount--;
					this.mBoundEffects.RemoveAt(i);
					i--;
				}
			}
		}

		public void CancelMatch()
		{
			this.mScale.SetConstant(1.0);
		}

		public float CX()
		{
			return this.GetScreenX() + 50f;
		}

		public float CY()
		{
			return this.GetScreenY() + 50f;
		}

		public float GetScreenX()
		{
			return this.mX + (float)this.mBoard.GetBoardX();
		}

		public float GetScreenY()
		{
			if (!this.mForceScreenY)
			{
				return this.mY + (float)this.mBoard.GetBoardY();
			}
			return this.mForcedY + (float)this.mBoard.GetBoardY();
		}

		public int FindRowFromY()
		{
			return this.mBoard.GetRowAt((int)this.mY);
		}

		public int FindColFromX()
		{
			return this.mBoard.GetColAt((int)this.mX);
		}

		public static float GetAngleRadius(float theAngle, int theColor, int theFrame)
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
			int num2 = (int)num;
			if (theColor < 0)
			{
				theColor = 0;
			}
			if (num2 < 0)
			{
				return GlobalMembers.GEM_OUTLINE_RADIUS_POINTS[theFrame, theColor, 0] * ConstantsWP.GEM_PRE_SCALE_FACTOR;
			}
			if (num2 >= 255)
			{
				return GlobalMembers.GEM_OUTLINE_RADIUS_POINTS[theFrame, theColor, 255] * ConstantsWP.GEM_PRE_SCALE_FACTOR;
			}
			float num3 = num - (float)num2;
			if (theColor < 0)
			{
				theColor = 0;
			}
			return GlobalMembers.GEM_OUTLINE_RADIUS_POINTS[theFrame, theColor, num2] * ConstantsWP.GEM_PRE_SCALE_FACTOR * (1f - num3) + GlobalMembers.GEM_OUTLINE_RADIUS_POINTS[theFrame, theColor, num2 + 1] * ConstantsWP.GEM_PRE_SCALE_FACTOR * num3;
		}

		public float GetAngleRadius(float theAngle)
		{
			int theFrame = (int)Math.Min(19f, 20f * this.mRotPct);
			return Piece.GetAngleRadius(theAngle, this.mColor, theFrame);
		}

		public void Save(Serialiser theBuffer)
		{
			theBuffer.WriteFloat(this.mX);
			theBuffer.WriteFloat(this.mY);
			theBuffer.WriteInt32(this.mCreatedTick);
			theBuffer.WriteFloat(this.mFallVelocity);
			theBuffer.WriteCurvedVal(this.mScale);
			theBuffer.WriteCurvedVal(this.mAlpha);
			theBuffer.WriteCurvedVal(this.mSelectorAlpha);
			theBuffer.WriteFloat(this.mRotPct);
			theBuffer.WriteBoolean(this.mDestructing);
			theBuffer.WriteBoolean(this.mIsExploding);
			theBuffer.WriteBoolean(this.mCanMatch);
			theBuffer.WriteBoolean(this.mCanScramble);
			theBuffer.WriteBoolean(this.mTallied);
			theBuffer.WriteBoolean(this.mCanDestroy);
			theBuffer.WriteBoolean(this.mIsBulging);
			theBuffer.WriteBoolean(this.mCanSwap);
			theBuffer.WriteInt32(this.mImmunityCount);
			theBuffer.WriteInt32(this.mMoveCreditId);
			theBuffer.WriteInt32(this.mLastMoveCreditId);
			theBuffer.WriteInt32(this.mMatchId);
			theBuffer.WriteCurvedVal(this.mDestPct);
			theBuffer.WriteInt32(this.mDestCol);
			theBuffer.WriteInt32(this.mDestRow);
			theBuffer.WriteInt32(this.mChangedTick);
			theBuffer.WriteInt32(this.mSwapTick);
			theBuffer.WriteInt32(this.mLastActiveTick);
			theBuffer.WriteInt32(this.mLastColor);
			theBuffer.WriteInt32(this.mColor);
			theBuffer.WriteInt32(this.mFlags);
			theBuffer.WriteInt32(this.mExplodeDelay);
			theBuffer.WriteInt32(this.mExplodeSourceId);
			theBuffer.WriteInt32(this.mExplodeSourceFlags);
			theBuffer.WriteBoolean(this.mSelected);
			theBuffer.WriteInt32(this.mCounter);
			theBuffer.WriteInt32(this.mCounterDelay);
			theBuffer.WriteFloat(this.mMoveCountdownPerLevel);
			theBuffer.WriteFloat(this.mShakeOfsX);
			theBuffer.WriteFloat(this.mShakeOfsY);
			theBuffer.WriteFloat(this.mShakeTime);
			theBuffer.WriteFloat(this.mShakeScale);
			theBuffer.WriteFloat(this.mShakeAngle);
			theBuffer.WriteFloat(this.mSpinFrame);
			theBuffer.WriteFloat(this.mSpinSpeed);
			theBuffer.WriteFloat(this.mDestSpinSpeed);
			theBuffer.WriteFloat(this.mSpinSpeedHoldTime);
		}

		public void Load(Serialiser theBuffer, int Version)
		{
			this.mX = theBuffer.ReadFloat();
			this.mY = theBuffer.ReadFloat();
			this.mCreatedTick = theBuffer.ReadInt32();
			this.mFallVelocity = theBuffer.ReadFloat();
			this.mScale = theBuffer.ReadCurvedVal();
			this.mAlpha = theBuffer.ReadCurvedVal();
			this.mSelectorAlpha = theBuffer.ReadCurvedVal();
			this.mRotPct = theBuffer.ReadFloat();
			this.mDestructing = theBuffer.ReadBoolean();
			this.mIsExploding = theBuffer.ReadBoolean();
			this.mCanMatch = theBuffer.ReadBoolean();
			this.mCanScramble = theBuffer.ReadBoolean();
			this.mTallied = theBuffer.ReadBoolean();
			this.mCanDestroy = theBuffer.ReadBoolean();
			this.mIsBulging = theBuffer.ReadBoolean();
			this.mCanSwap = theBuffer.ReadBoolean();
			this.mImmunityCount = theBuffer.ReadInt32();
			this.mMoveCreditId = theBuffer.ReadInt32();
			this.mLastMoveCreditId = theBuffer.ReadInt32();
			this.mMatchId = theBuffer.ReadInt32();
			this.mDestPct = theBuffer.ReadCurvedVal();
			this.mDestCol = theBuffer.ReadInt32();
			this.mDestRow = theBuffer.ReadInt32();
			this.mChangedTick = theBuffer.ReadInt32();
			this.mSwapTick = theBuffer.ReadInt32();
			this.mLastActiveTick = theBuffer.ReadInt32();
			this.mLastColor = theBuffer.ReadInt32();
			this.mColor = theBuffer.ReadInt32();
			this.mFlags = theBuffer.ReadInt32();
			this.mExplodeDelay = theBuffer.ReadInt32();
			this.mExplodeSourceId = theBuffer.ReadInt32();
			this.mExplodeSourceFlags = theBuffer.ReadInt32();
			this.mSelected = theBuffer.ReadBoolean();
			this.mCounter = theBuffer.ReadInt32();
			this.mCounterDelay = theBuffer.ReadInt32();
			if (Version > 101)
			{
				this.mMoveCountdownPerLevel = theBuffer.ReadFloat();
			}
			this.mShakeOfsX = theBuffer.ReadFloat();
			this.mShakeOfsY = theBuffer.ReadFloat();
			this.mShakeTime = theBuffer.ReadFloat();
			this.mShakeScale = theBuffer.ReadFloat();
			this.mShakeAngle = theBuffer.ReadFloat();
			this.mSpinFrame = theBuffer.ReadFloat();
			this.mSpinSpeed = theBuffer.ReadFloat();
			this.mDestSpinSpeed = theBuffer.ReadFloat();
			this.mSpinSpeedHoldTime = theBuffer.ReadFloat();
		}

		public void Update()
		{
			this.mOverlayBulge.IncInVal();
			this.mOverlayCurve.IncInVal();
			if (this.mShakeScale > 0f)
			{
				this.mShakeAngle = 3.14159274f * GlobalMembersUtils.GetRandFloat();
				this.mShakeOfsX = (float)Math.Cos((double)this.mShakeAngle) * this.mShakeScale * 100f / 20f;
				this.mShakeOfsY = (float)Math.Sin((double)this.mShakeAngle) * this.mShakeScale * 100f / 20f;
			}
			else
			{
				this.mShakeOfsX = (this.mShakeOfsY = 0f);
			}
			float num = this.mElectrocutePercent;
			if (this.IsFlagSet(128U) && !this.mBoard.IsPieceSwapping(this) && (this.mId * 10 + this.mBoard.mUpdateCnt) % 400 == 0)
			{
				switch (BejeweledLivePlus.Misc.Common.Rand(3))
				{
				case 0:
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ANIM_CURVE_C, this.mAnimCurve);
					break;
				case 1:
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ANIM_CURVE_D, this.mAnimCurve);
					break;
				case 2:
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ANIM_CURVE_E, this.mAnimCurve);
					break;
				}
			}
			int val = 40;
			if (this.IsFlagSet(1U))
			{
				bool flag = this.mBoard.WantsCalmEffects();
				int num2 = (((GlobalMembers.gApp.mUpdateCount + this.mId) % GlobalMembers.M(3) == 0) ? 1 : 0);
				for (int i = 0; i < num2; i++)
				{
					bool flag2 = BejeweledLivePlus.Misc.Common.Rand() % 2 == 0;
					EffectsManager effectsManager;
					Effect effect;
					if (flag2)
					{
						effectsManager = ((BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(32) == 0) ? this.mBoard.mPostFXManager : this.mBoard.mPreFXManager);
						effect = effectsManager.AllocEffect(Effect.Type.TYPE_EMBER_FADEINOUT);
						effect.mAngle = 0f;
						effect.mDAngle = 0f;
						effect.mAlpha = 0f;
						effect.mDAlpha = GlobalMembers.M(0.0075f) + GlobalMembers.M(0.0015f) * GlobalMembersUtils.GetRandFloat();
						if (GlobalMembers.gIs3D)
						{
							effect.mScale = GlobalMembers.M(0.12f) + GlobalMembers.M(0.035f) * GlobalMembersUtils.GetRandFloat();
							effect.mDScale = GlobalMembers.M(0.01f) + GlobalMembers.M(0.005f) * GlobalMembersUtils.GetRandFloat();
						}
						effect.mDY = GlobalMembers.M(-0.12f) + GlobalMembers.M(-0.05f) * GlobalMembersUtils.GetRandFloat();
						if (flag)
						{
							effect.mDScale *= GlobalMembers.M(0.67f);
						}
						bool flag3 = this.mBoard.mWantsReddishFlamegems && BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(4) <= GlobalMembers.M(0) && effectsManager == this.mBoard.mPreFXManager;
						if (this.mColor == 3 || flag3)
						{
							effect.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(128), GlobalMembers.M(128));
							if (flag3)
							{
								effect.mType = Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM;
							}
						}
						else
						{
							effect.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255));
						}
					}
					else
					{
						effectsManager = ((BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(2) == 0) ? this.mBoard.mPostFXManager : this.mBoard.mPreFXManager);
						effect = effectsManager.AllocEffect(Effect.Type.TYPE_EMBER);
						effect.mAlpha = 1f;
						if (GlobalMembers.gIs3D)
						{
							effect.mScale = GlobalMembers.M(2f);
						}
						effect.mDScale = GlobalMembers.M(-0.01f);
						effect.mFrame = 0;
						if (GlobalMembers.gIs3D)
						{
							effect.mImage = GlobalMembersResourcesWP.IMAGE_SPARKLET;
						}
						else
						{
							effect.mImage = GlobalMembersResourcesWP.IMAGE_SPARKLET_FAT;
						}
						effect.mDY = GlobalMembers.M(-0.4f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.15f);
						effect.mColor = new Color(GlobalMembers.M(128), BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(32) + GlobalMembers.M(48), BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(24) + GlobalMembers.M(24));
						bool flag4 = this.mBoard.mWantsReddishFlamegems && BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(3) <= GlobalMembers.M(0) && effectsManager == this.mBoard.mPreFXManager;
						if (this.mColor == 3 || flag4)
						{
							effect.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(0), GlobalMembers.M(0));
							if (flag4)
							{
								effect.mType = Effect.Type.TYPE_EMBER_BOTTOM;
							}
						}
						else if (this.mColor == 0)
						{
							effect.mColor = new Color(GlobalMembers.M(240), GlobalMembers.M(128), GlobalMembers.M(64));
						}
					}
					if (flag)
					{
						effect.mDY *= GlobalMembers.M(0.67f);
						effect.mDAlpha *= GlobalMembers.M(0.67f);
					}
					float num3 = 3.14159274f + Math.Abs(GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f);
					float angleRadius = this.GetAngleRadius(num3);
					if (flag2 && BejeweledLivePlus.Misc.Common.Rand() % 2 != 0)
					{
						float num4 = (float)Math.Sin((double)num3);
						if (num4 > 0f)
						{
							float num5 = (float)Math.Cos((double)num3);
							float num6;
							if (num5 < 0f)
							{
								num6 = GlobalMembers.M(0.001f);
							}
							else
							{
								num6 = GlobalMembers.M(-0.001f);
							}
							float num7 = ((float)Math.Cos((double)(num3 + num6)) + (float)Math.Cos((double)(num3 + num6 * 2f))) / 2f;
							float num8 = ((float)Math.Sin((double)(num3 + num6)) + (float)Math.Sin((double)(num3 + num6 * 2f))) / 2f;
							float num9 = (float)Math.Atan2((double)(num8 - num4), (double)(num7 - num5));
							float num10 = GlobalMembers.M(0.12f) + GlobalMembers.M(0.05f) * GlobalMembersUtils.GetRandFloat();
							float num11 = (float)Math.Cos((double)num9) * num10;
							float num12 = (float)Math.Sin((double)num9) * num10;
							effect.mDX = (effect.mDX + num11) / 2f;
							effect.mDY = (effect.mDY + num12) / 2f;
						}
					}
					float num13 = this.GetScreenX();
					if (effectsManager == this.mBoard.mPostFXManager)
					{
						num13 += (float)this.mBoard.mSideXOff;
					}
					effect.mX = num13 + 50f + (float)Math.Cos((double)num3) * angleRadius;
					effect.mY = this.GetScreenY() + 50f + (float)Math.Sin((double)num3) * angleRadius + GlobalMembers.M(2f);
					if (BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(6) != 0 || this.mBoard.mHyperspace != null)
					{
						effect.mX -= num13;
						effect.mY -= this.GetScreenY();
						effect.mPieceRel = this;
					}
					effectsManager.AddEffect(effect);
				}
				if ((GlobalMembers.gApp.mUpdateCount + this.mId) % GlobalMembers.M(val) == 0 || BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(40) == 0)
				{
					Effect effect2 = this.mBoard.mPostFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
					effect2.mFlags = 2;
					effect2.mX = this.CX() + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(20f);
					effect2.mY = this.CY() + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(20f) + GlobalMembers.M(-5f);
					effect2.mZ = GlobalMembers.M(0.08f);
					effect2.mValue[0] = GlobalMembers.M(30f);
					effect2.mValue[1] = GlobalMembers.M(-0.1f);
					effect2.mAlpha = GlobalMembers.M(0f);
					effect2.mDAlpha = GlobalMembers.M(0.1f);
					effect2.mScale = GlobalMembers.M(140f);
					effect2.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(140), GlobalMembers.M(48));
					if (BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(2) != 0 || this.mBoard.mHyperspace != null)
					{
						effect2.mPieceId = (uint)this.mId;
					}
					this.mBoard.mPostFXManager.AddEffect(effect2);
				}
			}
			if (this.IsFlagSet(2U) && ((GlobalMembers.gApp.mUpdateCount + this.mId) % GlobalMembers.M(val) == 0 || BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(40) == 0))
			{
				Effect effect3 = this.mBoard.mPostFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
				effect3.mFlags = 2;
				effect3.mX = this.CX() + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(30f);
				effect3.mY = this.CY() + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(30f) + GlobalMembers.M(0f);
				effect3.mZ = GlobalMembers.M(0.08f);
				effect3.mValue[0] = GlobalMembers.M(30f);
				effect3.mValue[1] = GlobalMembers.M(-0.3f);
				effect3.mAlpha = GlobalMembers.M(0f);
				effect3.mDAlpha = GlobalMembers.M(0.07f);
				effect3.mScale = GlobalMembers.M(140f);
				effect3.mColor = new Color((int)GlobalMembers.gApp.HSLToRGB(BejeweledLivePlus.Misc.Common.Rand() % 255, GlobalMembers.M(255), GlobalMembers.M(128)));
				if (BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(2) != 0)
				{
					effect3.mPieceId = (uint)this.mId;
				}
				this.mBoard.mPostFXManager.AddEffect(effect3);
			}
			if (this.IsFlagSet(512U))
			{
				if (this.mOverlay == null)
				{
					this.StampOverlay();
				}
				if (this.mCounter <= 8)
				{
					float num14 = (float)Math.Max(1, 8 - this.mCounter);
					int num15 = BejeweledLivePlus.Misc.Common.Rand() % 4;
					int num16 = Math.Max(GlobalMembers.M(2), this.mCounter * GlobalMembers.M(1));
					int num17 = 0;
					if (num16 < GlobalMembers.M(20) && this.mBoard.mUpdateCnt % num16 == 0)
					{
						num17 = GlobalMembers.M(1);
					}
					while (num17-- != 0)
					{
						Effect effect4 = this.mBoard.mPostFXManager.AllocEffect(Effect.Type.TYPE_STEAM);
						float num18 = (GlobalMembers.M(0.25f) + GlobalMembers.M(0.05f) * Math.Abs(GlobalMembersUtils.GetRandFloat())) * num14;
						float num19 = 0.7853982f + (float)num15 * 3.14159274f / 2f;
						effect4.mX = this.CX() + this.mShakeOfsX + (float)Math.Cos((double)num19) * 100f * (GlobalMembers.M(0.6f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.1f));
						effect4.mY = this.CY() + this.mShakeOfsY + (float)Math.Sin((double)num19) * 100f * (GlobalMembers.M(0.6f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.1f));
						effect4.mIsAdditive = false;
						effect4.mScale = GlobalMembers.M(0.1f);
						effect4.mDScale = GlobalMembers.M(0.03f);
						effect4.mMaxScale = GlobalMembers.M(2f);
						effect4.mDX = num18 * (float)Math.Cos((double)num19);
						effect4.mDY = num18 * (float)Math.Sin((double)num19);
						this.mBoard.mPostFXManager.AddEffect(effect4);
					}
				}
			}
			for (int j = 0; j < this.mBoundEffects.Count; j++)
			{
				Effect effect5 = this.mBoundEffects[j];
				if (effect5.mDeleteMe)
				{
					effect5.mRefCount--;
					this.mBoundEffects.RemoveAt(j);
					j--;
				}
			}
		}

		public bool SetFlag(uint theFlag)
		{
			if ((theFlag & (uint)this.mDisallowFlags) != 0U)
			{
				return false;
			}
			this.mFlags |= (int)theFlag;
			return true;
		}

		public bool CanSetFlag(uint theFlag)
		{
			return ((ulong)theFlag & (ulong)((long)this.mDisallowFlags)) == 0UL;
		}

		public bool IsFlagSet(uint theFlag)
		{
			return ((long)this.mFlags & (long)((ulong)theFlag)) != 0L;
		}

		public void SetDisallowFlag(uint theFlag)
		{
			this.SetDisallowFlag(theFlag, true);
		}

		public void SetDisallowFlag(uint theFlag, bool theValue)
		{
			if (theValue)
			{
				this.mDisallowFlags |= (int)theFlag;
			}
			else
			{
				this.mDisallowFlags &= (int)(~(int)theFlag);
			}
			this.mFlags &= ~this.mDisallowFlags;
		}

		public void ClearDisallowFlag(uint theFlag)
		{
			this.mDisallowFlags &= (int)(~(int)theFlag);
			this.mFlags &= ~this.mDisallowFlags;
		}

		public void ClearDisallowFlags()
		{
			this.mDisallowFlags = 0;
		}

		public void ClearFlag(uint theFlag)
		{
			this.mFlags &= (int)(~(int)theFlag);
		}

		public void ClearFlags()
		{
			this.mFlags = 0;
		}

		public void StampOverlay()
		{
			string theString = string.Format("{0}", this.mCounter);
			ImageFont imageFont = (ImageFont)GlobalMembersResources.FONT_SCORE;
			Utils.SetFontLayerColor(imageFont, "Layer_3", new Color(0, 0, 0, 128));
			int num = (int)((float)imageFont.StringWidth(theString) * GlobalMembers.M(1.5f));
			int num2 = Math.Max(GlobalMembers.S(100), imageFont.mHeight - imageFont.mAscent) + GlobalMembers.S(18);
			if (this.mOverlay == null)
			{
				this.mOverlay = new MemoryImage();
				this.mOverlay.SetImageMode(true, true);
				this.mOverlay.ReplaceImageFlags(128U);
			}
			if (this.mOverlayGlow == null)
			{
				this.mOverlayGlow = new MemoryImage();
				this.mOverlayGlow.SetImageMode(true, true);
				this.mOverlayGlow.ReplaceImageFlags(128U);
			}
			if (num > this.mOverlay.mWidth || num2 > this.mOverlay.mHeight)
			{
				this.mOverlay.Create(num, num2);
				this.mOverlayGlow.Create(num, num2);
			}
			Graphics graphics = new Graphics(this.mOverlay);
			graphics.SetColorizeImages(true);
			Color theColor = default(Color);
			theColor = GlobalMembers.gGemColors[this.mColor];
			theColor.mRed /= 2;
			theColor.mGreen /= 2;
			theColor.mBlue /= 2;
			graphics.SetFont(imageFont);
			imageFont.PushLayerColor(0, Color.Black);
			imageFont.PushLayerColor(1, theColor);
			imageFont.PushLayerColor(2, Color.White);
			imageFont.PushLayerColor(3, Color.White);
			int theX = (this.mOverlay.mWidth - graphics.StringWidth(theString)) / 2 + GlobalMembers.MS(0);
			int theY = this.mOverlay.mHeight / 2 + graphics.GetFont().mHeight - graphics.GetFont().mAscent + GlobalMembers.MS(28);
			graphics.SetColor(new Color(255, 255, 255));
			graphics.DrawString(theString, theX, theY);
			imageFont.PopLayerColor(0);
			imageFont.PopLayerColor(1);
			imageFont.PopLayerColor(2);
			imageFont.PopLayerColor(3);
			imageFont.PushLayerColor(0, Color.Black);
			imageFont.PushLayerColor(1, Color.White);
			imageFont.PushLayerColor(2, new Color(255, 0, 0));
			imageFont.PushLayerColor(3, new Color(255, 0, 0));
			Graphics graphics2 = new Graphics(this.mOverlayGlow);
			graphics2.SetColorizeImages(true);
			graphics2.SetFont(imageFont);
			graphics2.SetColor(new Color(255, 255, 255));
			graphics2.DrawString(theString, theX, theY);
			imageFont.PopLayerColor(0);
			imageFont.PopLayerColor(1);
			imageFont.PopLayerColor(2);
			imageFont.PopLayerColor(3);
			if (this.mCounter <= 8)
			{
				float num3 = (float)Math.Max(1, 8 - this.mCounter);
				this.mShakeScale = num3 * GlobalMembers.M(0.15f);
				if (this.mOverlayCurve.mIncRate == 0.0)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_OVERLAY_CURVE, this.mOverlayCurve);
					this.mOverlayCurve.mMode = 1;
					return;
				}
			}
			else
			{
				this.mOverlayCurve.mInVal = 0.0;
				this.mOverlayCurve.mIncRate = 0.0;
			}
		}

		public Board mBoard;

		public int mId;

		public int mCol;

		public int mRow;

		public float mX;

		public float mY;

		public int mCreatedTick;

		public float mFallVelocity;

		public CurvedVal mScale = new CurvedVal();

		public CurvedVal mAlpha = new CurvedVal();

		public CurvedVal mSelectorAlpha = new CurvedVal();

		public float mRotPct;

		public bool mIsElectrocuting;

		public float mElectrocutePercent;

		public bool mDestructing;

		public bool mIsExploding;

		public bool mCanMatch;

		public bool mCanSwap;

		public bool mCanScramble;

		public bool mTallied;

		public bool mCanDestroy;

		public bool mIsBulging;

		public int mImmunityCount;

		public int mMoveCreditId;

		public int mLastMoveCreditId;

		public int mMatchId;

		public CurvedVal mDestPct = new CurvedVal();

		public int mDestCol;

		public int mDestRow;

		public int mChangedTick;

		public int mSwapTick;

		public int mLastActiveTick;

		public int mLastColor;

		public int mColor;

		public int mDestColor;

		public int mFlags;

		public int mDisallowFlags;

		public int mExplodeDelay;

		public int mExplodeSourceId;

		public int mExplodeSourceFlags;

		public bool mSelected;

		public float mHidePct;

		public int mCounter;

		public int mCounterDelay;

		public int mTimer;

		public int mTimerThreshold;

		public float mMoveCountdownPerLevel;

		public float mShakeOfsX;

		public float mShakeOfsY;

		public float mShakeTime;

		public float mShakeScale;

		public float mShakeAngle;

		public float mSpinFrame;

		public float mSpinSpeed;

		public float mDestSpinSpeed;

		public float mSpinSpeedHoldTime;

		public CurvedVal mHintAlpha = new CurvedVal();

		public CurvedVal mHintArrowPos = new CurvedVal();

		public MemoryImage mOverlay;

		public MemoryImage mOverlayGlow;

		public CurvedVal mOverlayCurve = new CurvedVal();

		public CurvedVal mOverlayBulge = new CurvedVal();

		public CurvedVal mAnimCurve = new CurvedVal();

		public float mFlyVX;

		public float mFlyVY;

		public float mFlyAY;

		public List<Effect> mBoundEffects = new List<Effect>();

		public bool mForceScreenY;

		public float mForcedY;

		private static SimpleObjectPool pool_ = new SimpleObjectPool(128, typeof(Piece));
	}
}
