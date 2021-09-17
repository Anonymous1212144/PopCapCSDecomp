using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class PopAnimEffect : Effect
	{
		public PopAnimEffect()
			: base(Effect.Type.TYPE_POPANIM)
		{
		}

		public void initWithPopAnim(PopAnim thePopAnim)
		{
			base.init(Effect.Type.TYPE_POPANIM);
			this.mPopAnim = thePopAnim.Duplicate();
			this.mDAlpha = 0f;
			this.mDoubleSpeed = true;
			this.mUpdatedOnce = false;
		}

		public override void Dispose()
		{
			this.mPopAnim.Dispose();
			this.mPopAnim = null;
			base.Dispose();
		}

		public virtual void Play()
		{
			this.mPopAnim.Play();
		}

		public virtual void Play(string theComposition)
		{
			this.mPopAnim.Play(theComposition);
		}

		public override void Update()
		{
			this.mUpdatedOnce = true;
			base.Update();
			int num = (int)this.mScale;
			int num2 = (int)(0.625 * (double)this.mPopAnim.mAnimRect.mWidth * (double)num);
			int num3 = (int)(0.625 * (double)this.mPopAnim.mAnimRect.mHeight * (double)num);
			if (GlobalMembers.gApp.mResourceManager.mCurArtRes == 480)
			{
				num2 = (int)((float)num2 * 0.5f);
				num3 = (int)((float)num3 * 0.5f);
			}
			Transform transform = new Transform();
			transform.Reset();
			transform.Scale((float)num, (float)num);
			if (this.mAngle != 0f)
			{
				transform.Translate((float)(-(float)num2 / 2), (float)(-(float)num3 / 2));
				transform.RotateRad(this.mAngle);
				transform.Translate((float)(num2 / 2), (float)(num3 / 2));
			}
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
			}
			transform.Translate(GlobalMembers.S(this.mX) - (float)(num2 / 2), GlobalMembers.S(this.mY) - (float)(num3 / 2));
			this.mPopAnim.SetTransform(transform.GetMatrix());
			this.mPopAnim.Update();
			if (this.mDoubleSpeed)
			{
				this.mPopAnim.Update();
			}
			if (!this.mPopAnim.IsActive())
			{
				this.mDeleteMe = true;
			}
		}

		public override void Draw(Graphics g)
		{
			if (!this.mUpdatedOnce)
			{
				this.Update();
			}
			this.mPopAnim.mColor = new Color(255, 255, 255, (int)(255f * this.mFXManager.mAlpha));
			if (GlobalMembers.gApp.mBoard != null)
			{
				this.mPopAnim.mColor.mAlpha = (int)((float)this.mPopAnim.mColor.mAlpha * GlobalMembers.gApp.mBoard.GetAlpha());
			}
			this.mPopAnim.Draw(g);
		}

		public void Stop()
		{
		}

		public new static void initPool()
		{
			PopAnimEffect.thePool_ = new SimpleObjectPool(512, typeof(PopAnimEffect));
		}

		public static PopAnimEffect fromPopAnim(PopAnim thePopAnim)
		{
			PopAnimEffect popAnimEffect = (PopAnimEffect)PopAnimEffect.thePool_.alloc();
			popAnimEffect.initWithPopAnim(thePopAnim);
			return popAnimEffect;
		}

		public override void release()
		{
			this.Dispose();
			PopAnimEffect.thePool_.release(this);
		}

		private bool mUpdatedOnce;

		public PopAnim mPopAnim;

		public new bool mDoubleSpeed;

		private static SimpleObjectPool thePool_;
	}
}
