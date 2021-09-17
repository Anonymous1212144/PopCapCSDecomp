using System;
using Microsoft.Xna.Framework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class ParticleEffect : Effect
	{
		public ParticleEffect()
			: base(Effect.Type.TYPE_PI)
		{
		}

		public void initWithPIEffect(PIEffect thePIEffect)
		{
			base.init(Effect.Type.TYPE_PI);
			this.mPIEffect = thePIEffect.Duplicate();
			this.mDoDrawTransform = false;
			this.mDAlpha = 0f;
			this.mIsFirstUpdate = true;
		}

		public override void Dispose()
		{
			this.mPIEffect.Dispose();
			base.Dispose();
		}

		public override void Update()
		{
			base.Update();
			this.PE_Update_trans.Reset();
			this.PE_Update_trans.Scale(this.mScale, this.mScale);
			this.PE_Update_trans.RotateRad(this.mAngle);
			Piece mPieceRel = this.mPieceRel;
			if (this.mPieceRel != null)
			{
				this.mStopWhenPieceRelMissing = true;
			}
			if (this.mStopWhenPieceRelMissing && mPieceRel == null)
			{
				this.Stop();
			}
			if (mPieceRel != null)
			{
				this.mX = mPieceRel.CX();
				this.mY = mPieceRel.CY();
				if (this.mFXManager.mBoard != null)
				{
					this.mX += 1260f * (float)this.mFXManager.mBoard.mSlideUIPct;
				}
				if (this.mFXManager.mBoard != null && this.mFXManager.mBoard.mPostFXManager == this.mFXManager)
				{
					this.mX += (float)this.mFXManager.mBoard.mSideXOff * this.mFXManager.mBoard.mSlideXScale;
				}
				if (mPieceRel.mHidePct > 0f)
				{
					this.mPIEffect.mColor.mAlpha = (int)(255f - mPieceRel.mHidePct * 255f);
				}
			}
			this.PE_Update_trans.Translate(this.mX, this.mY);
			if (this.mDoDrawTransform)
			{
				this.PE_Update_trans.Scale(GlobalMembers.S(1f), GlobalMembers.S(1f));
				this.mPIEffect.mDrawTransform = this.PE_Update_trans.GetMatrix();
			}
			else
			{
				this.mPIEffect.mDrawTransform.LoadIdentity();
				this.mPIEffect.mDrawTransform.Scale(GlobalMembers.S(1f), GlobalMembers.S(1f));
				this.mPIEffect.mEmitterTransform = this.PE_Update_trans.GetMatrix();
			}
			if (this.mIsFirstUpdate)
			{
				this.mPIEffect.ResetAnim();
				this.mIsFirstUpdate = false;
			}
			else
			{
				this.mPIEffect.Update();
			}
			if (!this.mPIEffect.IsActive())
			{
				this.mDeleteMe = true;
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mIsFirstUpdate)
			{
				return;
			}
			this.mPIEffect.mColor = new Color(255, 255, 255, (int)(255f * this.mFXManager.mAlpha * this.mAlpha));
			if (GlobalMembers.gApp.mBoard != null)
			{
				this.mPIEffect.mColor.mAlpha = (int)((float)this.mPIEffect.mColor.mAlpha * GlobalMembers.gApp.mBoard.GetAlpha());
			}
			this.mPIEffect.Draw(g);
		}

		public void SetEmitAfterTimeline(bool emitAfterTimeline)
		{
			this.mPIEffect.mEmitAfterTimeline = emitAfterTimeline;
		}

		public bool SetLineEmitterPoint(int theLayerIdx, int theEmitterIdx, int thePointIdx, int theKeyFrame, FPoint thePoint)
		{
			PILayer layer = this.mPIEffect.GetLayer(theLayerIdx);
			if (layer == null)
			{
				return false;
			}
			PIEmitterInstance emitter = layer.GetEmitter(theEmitterIdx);
			if (emitter == null)
			{
				return false;
			}
			if (emitter.mEmitterInstanceDef.mEmitterGeom != 1)
			{
				return false;
			}
			if (thePointIdx >= emitter.mEmitterInstanceDef.mPoints.Count)
			{
				return false;
			}
			if (theKeyFrame >= emitter.mEmitterInstanceDef.mPoints[thePointIdx].mValuePoint2DVector.Count)
			{
				return false;
			}
			emitter.mEmitterInstanceDef.mPoints[thePointIdx].mValuePoint2DVector[theKeyFrame].mValue = new Vector2(thePoint.mX, thePoint.mY);
			return true;
		}

		public bool SetEmitterTint(int theLayerIdx, int theEmitterIdx, Color theColor)
		{
			PILayer layer = this.mPIEffect.GetLayer(theLayerIdx);
			if (layer == null)
			{
				return false;
			}
			PIEmitterInstance emitter = layer.GetEmitter(theEmitterIdx);
			if (emitter == null)
			{
				return false;
			}
			emitter.mTintColor = theColor;
			return true;
		}

		public void Stop()
		{
			this.SetEmitAfterTimeline(false);
			if (this.mPIEffect.mFrameNum < (float)(this.mPIEffect.mLastFrameNum - 1))
			{
				this.mPIEffect.mFrameNum = (float)(this.mPIEffect.mLastFrameNum - 1);
			}
		}

		public PILayer GetLayer(int theIdx)
		{
			return this.mPIEffect.GetLayer(theIdx);
		}

		public PILayer GetLayer(string theName)
		{
			return this.mPIEffect.GetLayer(theName);
		}

		public new static void initPool()
		{
			ParticleEffect.thePool_ = new SimpleObjectPool(4096, typeof(ParticleEffect));
		}

		public static ParticleEffect fromPIEffect(PIEffect thePIEffect)
		{
			ParticleEffect particleEffect = (ParticleEffect)ParticleEffect.thePool_.alloc();
			particleEffect.initWithPIEffect(thePIEffect);
			return particleEffect;
		}

		public override void release()
		{
			this.Dispose();
			ParticleEffect.thePool_.release(this);
		}

		public PIEffect mPIEffect;

		public bool mIsFirstUpdate;

		public bool mDoDrawTransform;

		private Transform PE_Update_trans = new Transform();

		private static SimpleObjectPool thePool_;
	}
}
