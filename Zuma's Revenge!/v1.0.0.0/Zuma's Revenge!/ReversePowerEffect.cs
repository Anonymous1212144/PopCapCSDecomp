using System;
using System.Linq;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class ReversePowerEffect : PowerEffect
	{
		public ReversePowerEffect()
		{
		}

		public ReversePowerEffect(float x, float y, Ball b)
			: base(x, y)
		{
			this.mScale = 1f;
			this.mCurve = GameApp.gApp.GetBoard().GetCurve(b);
			this.mStartWaypoint = (this.mWaypoint = b.GetWayPoint());
			SexyVector2 pointPos = this.mCurve.mWayPointMgr.GetPointPos(this.mWaypoint);
			this.mX = pointPos.x;
			this.mY = pointPos.y;
			this.mRotation = this.mCurve.mWayPointMgr.GetRotationForPoint((int)this.mWaypoint);
		}

		public override void Update()
		{
			if (this.IsDone())
			{
				return;
			}
			base.Update();
			if (!this.mDone)
			{
				return;
			}
			this.mWaypoint -= (float)Common._M(20);
			SexyVector2 pointPos = this.mCurve.mWayPointMgr.GetPointPos(this.mWaypoint);
			this.mX = pointPos.x;
			this.mY = pointPos.y;
			this.mRotation = this.mCurve.mWayPointMgr.GetRotationForPoint((int)this.mWaypoint);
			this.mScale = this.mWaypoint / this.mStartWaypoint;
			if (this.mScale < 0f)
			{
				this.mDone = true;
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.IsDone())
			{
				return;
			}
			g.PushState();
			g.SetColorizeImages(true);
			g.SetDrawMode(1);
			int num = (this.mDrawReverse ? (Enumerable.Count<EffectItem>(this.mItems) - 1) : 0);
			int num2 = (this.mDrawReverse ? 0 : Enumerable.Count<EffectItem>(this.mItems));
			int num3 = num;
			while (this.mDrawReverse ? (num3 >= num2) : (num3 < num2))
			{
				EffectItem effectItem = this.mItems[num3];
				Color mColor = effectItem.mColor;
				mColor.mAlpha = (int)Component.GetComponentValue(effectItem.mOpacity, 255f, this.mUpdateCount);
				if (mColor.mAlpha != 0)
				{
					float num4 = (this.mDone ? this.mScale : Component.GetComponentValue(effectItem.mScale, 1f, this.mUpdateCount));
					g.SetColor(mColor);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.RotateRad(this.mRotation);
					this.mGlobalTranform.Scale(num4, num4);
					Rect celRect = effectItem.mImage.GetCelRect(effectItem.mCel);
					if (g.Is3D())
					{
						g.DrawImageTransformF(effectItem.mImage, this.mGlobalTranform, celRect, Common._S(this.mX), Common._S(this.mY));
					}
					else
					{
						g.DrawImageTransform(effectItem.mImage, this.mGlobalTranform, celRect, Common._S(this.mX), Common._S(this.mY));
					}
				}
				num3 += (this.mDrawReverse ? (-1) : 1);
			}
			g.PopState();
		}

		public override bool IsDone()
		{
			return this.mDone && this.mWaypoint < 0f;
		}

		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncFloat(ref this.mWaypoint);
			sync.SyncFloat(ref this.mStartWaypoint);
			sync.SyncFloat(ref this.mRotation);
			sync.SyncFloat(ref this.mScale);
			sync.SyncPointer(this);
		}

		protected float mWaypoint;

		protected float mStartWaypoint;

		protected float mRotation;

		protected float mScale;

		public CurveMgr mCurve;
	}
}
