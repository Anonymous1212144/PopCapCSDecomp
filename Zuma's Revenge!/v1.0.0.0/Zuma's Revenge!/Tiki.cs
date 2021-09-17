using System;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class Tiki : IDisposable
	{
		public Tiki()
		{
		}

		public Tiki(Tiki rhs)
			: this()
		{
			this.CopyFrom(rhs);
		}

		public void CopyFrom(Tiki rhs)
		{
			this.mUpdateCount = rhs.mUpdateCount;
			this.mCollRect = new Rect(rhs.mCollRect);
			this.mDoExplosion = rhs.mDoExplosion;
			this.mBoss = rhs.mBoss;
			this.mRailStartX = rhs.mRailStartX;
			this.mRailStartY = rhs.mRailStartY;
			this.mRailEndX = rhs.mRailEndX;
			this.mRailEndY = rhs.mRailEndY;
			this.mTravelTime = rhs.mTravelTime;
			this.mId = rhs.mId;
			this.mAlphaFadeDir = rhs.mAlphaFadeDir;
			this.mAlpha = rhs.mAlpha;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWasHit = rhs.mWasHit;
			this.mIsLeftTiki = rhs.mIsLeftTiki;
			this.mVX = rhs.mVX;
			this.mComp = rhs.mComp;
			this.mExplosion = rhs.mExplosion;
		}

		public virtual void Dispose()
		{
			if (this.mExplosion != null)
			{
				this.mExplosion.Dispose();
				this.mExplosion = null;
			}
		}

		public void Init(Boss b)
		{
			this.mBoss = b;
		}

		public void Update()
		{
			if (this.mDoExplosion)
			{
				this.mExplosion.mDrawTransform.LoadIdentity();
				float num = GameApp.DownScaleNum(1f);
				this.mExplosion.mDrawTransform.Scale(num, num);
				this.mExplosion.mDrawTransform.Translate(Common._S(this.mX) + (float)Common._DS(Common._M(80)), Common._S(this.mY) + (float)Common._DS(Common._M1(150)));
				this.mExplosion.Update();
				if (this.mExplosion.mFrameNum > (float)this.mExplosion.mLastFrameNum)
				{
					this.mDoExplosion = false;
				}
			}
			this.mComp.Update();
			this.mAlpha += this.mAlphaFadeDir * Common._M(12);
			if (this.mAlpha < 0)
			{
				this.mAlpha = 0;
			}
			else if (this.mAlpha > 255)
			{
				this.mAlpha = 255;
			}
			if (!this.mDoExplosion && ((this.mVX > 0f && this.mX + (float)this.mCollRect.mX > (float)this.mRailEndX) || (this.mVX < 0f && this.mX + (float)this.mCollRect.mX < (float)this.mRailStartX)))
			{
				this.mX = (float)((this.mVX > 0f) ? (this.mRailEndX - this.mCollRect.mX) : (this.mRailStartX - this.mCollRect.mX));
				this.mVX *= -1f;
			}
			this.mX += this.mVX;
			this.mUpdateCount++;
		}

		public void Draw(Graphics g)
		{
			if (this.mAlpha > 0)
			{
				CumulativeTransform cumulativeTransform = new CumulativeTransform();
				cumulativeTransform.mOpacity = (float)this.mAlpha / 255f;
				if (this.mBoss != null && this.mBoss.mAlphaOverride <= 254f)
				{
					cumulativeTransform.mOpacity = this.mBoss.mAlphaOverride / 255f;
				}
				cumulativeTransform.mTrans.Translate(Common._S(this.mX - (float)this.mCollRect.mX), Common._S(this.mY - (float)this.mCollRect.mY));
				this.mComp.Draw(g, cumulativeTransform, -1, Common._DS(1f));
			}
			if (g.Is3D() && this.mDoExplosion)
			{
				if (this.mBoss != null && this.mBoss.mAlphaOverride <= 254f)
				{
					this.mExplosion.mColor.mAlpha = (int)(this.mBoss.mAlphaOverride / 255f);
				}
				this.mExplosion.Draw(g);
			}
		}

		public void SetIsLeft(bool l)
		{
			this.mIsLeftTiki = l;
			this.mCollRect = new Rect(Common._M(74), Common._M1(70), Common._M2(75), Common._M4(104));
			this.mExplosion = null;
			this.mExplosion = (this.mIsLeftTiki ? GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_CIRCLEEXPLOSIONTIKI").Duplicate() : GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_TRIANGLEEXPLOSIONTIKI").Duplicate());
		}

		public bool Collides(Bullet b, ref bool should_destroy)
		{
			Rect rect = new Rect(this.mCollRect);
			rect.mX = (int)this.mX;
			rect.mY = (int)this.mY;
			Rect theTRect = new Rect((int)b.GetX() - b.GetRadius(), (int)b.GetY() - b.GetRadius(), b.GetRadius() * 2, b.GetRadius() * 2);
			should_destroy = false;
			if (this.mWasHit || this.mAlphaFadeDir < 0 || !rect.Intersects(theTRect))
			{
				return false;
			}
			should_destroy = true;
			this.mWasHit = true;
			this.mAlphaFadeDir = -1;
			this.mDoExplosion = true;
			this.mExplosion.ResetAnim();
			return true;
		}

		protected int mUpdateCount;

		protected Rect mCollRect = default(Rect);

		protected bool mDoExplosion;

		protected Boss mBoss;

		public int mRailStartX;

		public int mRailStartY;

		public int mRailEndX;

		public int mRailEndY;

		public int mTravelTime;

		public int mId = -1;

		public int mAlphaFadeDir = 1;

		public int mAlpha;

		public float mX;

		public float mY;

		public bool mWasHit;

		public bool mIsLeftTiki;

		public float mVX;

		public Composition mComp;

		public PIEffect mExplosion;
	}
}
