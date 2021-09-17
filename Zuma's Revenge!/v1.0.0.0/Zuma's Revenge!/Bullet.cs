using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class Bullet : Ball
	{
		public Bullet()
		{
			this.mVelX = 0f;
			this.mVelY = 0f;
			this.mHitBall = null;
			this.mHitPercent = 0f;
			this.mMergeSpeed = Common._M(0.025f);
			this.mJustFired = false;
			this.mDoNewMerge = false;
			this.mUpdateCount = 0;
			this.mHitDX = 0f;
			this.mHitDY = 0f;
			this.mAngleFired = 0f;
			this.mSkip = false;
		}

		public Bullet(Bullet other)
		{
			base.CopyFrom(other);
			this.mHitBall = other.mHitBall;
			this.mVelX = other.mVelX;
			this.mVelY = other.mVelY;
			this.mHitX = other.mHitX;
			this.mHitY = other.mHitY;
			this.mHitDX = other.mHitDX;
			this.mHitDY = other.mHitDY;
			this.mDestX = other.mDestX;
			this.mDestY = other.mDestY;
			this.mHitPercent = other.mHitPercent;
			this.mMergeSpeed = other.mMergeSpeed;
			this.mAngleFired = other.mAngleFired;
			this.mUpdateCount = other.mUpdateCount;
			this.mHitInFront = other.mHitInFront;
			this.mHaveSetPrevBall = other.mHaveSetPrevBall;
			this.mJustFired = other.mJustFired;
			this.mDoNewMerge = other.mDoNewMerge;
			this.mSkip = other.mSkip;
			this.mGapInfo.AddRange(other.mGapInfo.ToArray());
			Array.Copy(other.mCurCurvePoint, this.mCurCurvePoint, this.mCurCurvePoint.Length);
		}

		public override void Dispose()
		{
			this.SetBallInfo(null);
		}

		public void SetBallInfo(Bullet theBullet)
		{
			if (this.mHitBall != null)
			{
				this.mHitBall.SetBullet(theBullet);
			}
		}

		public void SetVelocity(float vx, float vy)
		{
			this.mVelX = vx;
			this.mVelY = vy;
		}

		public void SetHitBall(Ball theBall, bool hitInFront)
		{
			this.SetBallInfo(null);
			this.mHaveSetPrevBall = false;
			this.mHitBall = theBall;
			this.mHitX = this.mX;
			this.mHitY = this.mY;
			this.mHitDX = this.mX - theBall.GetX();
			this.mHitDY = this.mY - theBall.GetY();
			this.mHitPercent = 0f;
			this.mHitInFront = hitInFront;
			this.SetBallInfo(this);
		}

		public void CheckSetHitBallToPrevBall()
		{
			if (this.mHaveSetPrevBall || this.mHitBall == null)
			{
				return;
			}
			Ball prevBall = this.mHitBall.GetPrevBall();
			if (prevBall == null)
			{
				return;
			}
			if (prevBall.CollidesWithPhysically(this) && !prevBall.GetIsExploding())
			{
				this.mHaveSetPrevBall = true;
				this.SetBallInfo(null);
				this.mHitBall = prevBall;
				this.mHitInFront = true;
				this.mHitX = this.mX;
				this.mHitY = this.mY;
				this.mHitDX = this.mX - prevBall.GetX();
				this.mHitDY = this.mY - prevBall.GetY();
				this.mHitPercent = 0f;
				this.SetBallInfo(this);
			}
		}

		public void SetDestPos(float x, float y)
		{
			this.mDestX = x;
			this.mDestY = y;
		}

		public void SetDXPos()
		{
			float num = 1f - this.mHitPercent;
			this.mX += this.mHitDX * num;
			this.mY += this.mHitDY * num;
		}

		public void Update(float theAmount)
		{
			this.mUpdateCount++;
			this.mDisplayType = this.mColorType;
			if (this.mHitBall == null)
			{
				float num = this.mVelX * theAmount;
				float num2 = this.mVelY * theAmount;
				this.mX += num;
				this.mY += num2;
			}
			else if (!this.mExploding)
			{
				this.mHitPercent += this.mMergeSpeed;
				if (this.mHitPercent > 1f)
				{
					this.mHitPercent = 1f;
				}
				if (!this.mDoNewMerge)
				{
					this.mX = this.mHitX + this.mHitPercent * (this.mDestX - this.mHitX);
					this.mY = this.mHitY + this.mHitPercent * (this.mDestY - this.mHitY);
				}
			}
			base.UpdateRotation();
		}

		public new void Update()
		{
			this.Update(1f);
		}

		public void MergeFully()
		{
			this.mHitPercent = 1f;
			this.Update();
		}

		public Ball GetPushBall()
		{
			if (this.mHitBall == null)
			{
				return null;
			}
			Ball ball = (this.mHitInFront ? this.mHitBall.GetNextBall() : this.mHitBall);
			if (ball != null && (this.mDoNewMerge || ball.CollidesWithPhysically(this)))
			{
				return ball;
			}
			return null;
		}

		public void UpdateHitPos()
		{
			this.mHitX = this.mX;
			this.mHitY = this.mY;
		}

		public void SetCurCurvePoint(int theCurveNum, int thePoint)
		{
			this.mCurCurvePoint[theCurveNum] = thePoint;
		}

		public int GetCurCurvePoint(int theCurveNum)
		{
			return this.mCurCurvePoint[theCurveNum];
		}

		public bool AddGapInfo(int theCurve, int theDist, int theBallId)
		{
			foreach (GapInfo gapInfo in this.mGapInfo)
			{
				if (gapInfo.mBallId == theBallId)
				{
					return false;
				}
			}
			GapInfo gapInfo2 = new GapInfo();
			gapInfo2.mBallId = theBallId;
			gapInfo2.mDist = theDist;
			gapInfo2.mCurve = theCurve;
			this.mGapInfo.Add(gapInfo2);
			return true;
		}

		public int GetCurGapBall(int theCurveNum)
		{
			int result = 0;
			foreach (GapInfo gapInfo in this.mGapInfo)
			{
				if (gapInfo.mCurve == theCurveNum)
				{
					result = gapInfo.mBallId;
				}
			}
			return result;
		}

		public int GetMinGapDist()
		{
			int num = 0;
			foreach (GapInfo gapInfo in this.mGapInfo)
			{
				if (num == 0 || gapInfo.mDist < num)
				{
					num = gapInfo.mDist;
				}
			}
			return num;
		}

		public void RemoveGapInfoForBall(int theBallId)
		{
			int num = 0;
			while (num != Enumerable.Count<GapInfo>(this.mGapInfo))
			{
				if (this.mGapInfo[num].mBallId == theBallId)
				{
					this.mGapInfo.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}

		public override void SyncState(DataSync theSync)
		{
			base.SyncState(theSync);
			theSync.SyncFloat(ref this.mVelX);
			theSync.SyncFloat(ref this.mVelY);
			theSync.SyncBoolean(ref this.mHitInFront);
			theSync.SyncBoolean(ref this.mHaveSetPrevBall);
			theSync.SyncFloat(ref this.mHitX);
			theSync.SyncFloat(ref this.mHitY);
			theSync.SyncFloat(ref this.mDestX);
			theSync.SyncFloat(ref this.mDestY);
			theSync.SyncFloat(ref this.mHitDX);
			theSync.SyncFloat(ref this.mHitDY);
			theSync.SyncLong(ref this.mUpdateCount);
			theSync.SyncBoolean(ref this.mHitInFront);
			theSync.SyncBoolean(ref this.mHaveSetPrevBall);
			theSync.SyncBoolean(ref this.mJustFired);
			theSync.SyncBoolean(ref this.mDoNewMerge);
			theSync.SyncFloat(ref this.mHitPercent);
			theSync.SyncFloat(ref this.mMergeSpeed);
			theSync.SyncFloat(ref this.mAngleFired);
			theSync.SyncBoolean(ref this.mSkip);
			for (int i = 0; i < 4; i++)
			{
				theSync.SyncLong(ref this.mCurCurvePoint[i]);
			}
			theSync.SyncPointer(this);
			this.SyncListGapInfos(theSync, true);
		}

		private void SyncListGapInfos(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mGapInfo.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					GapInfo gapInfo = new GapInfo();
					gapInfo.SyncState(sync);
					this.mGapInfo.Add(gapInfo);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mGapInfo.Count);
			foreach (GapInfo gapInfo2 in this.mGapInfo)
			{
				gapInfo2.SyncState(sync);
			}
		}

		public override void Draw(Graphics g, int xoff, int yoff)
		{
			if (!this.mIsCannon)
			{
				float mWayPoint = this.mWayPoint;
				this.mWayPoint = 0f;
				base.Draw(g, xoff, yoff);
				this.mWayPoint = mWayPoint;
				return;
			}
			if (this.mFrog.mBoard.LevelIsSkeletonBoss())
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_GLOWBALL);
				float num = Common._S(this.mX) - (float)(imageByID.mWidth / 2);
				float num2 = Common._S(this.mY) - (float)(imageByID.mHeight / 2);
				g.DrawImage(imageByID, (int)num, (int)num2);
				g.PushState();
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				int alphaFromUpdateCount = Common.GetAlphaFromUpdateCount(this.mUpdateCount, Common._M(64));
				g.SetColor(255, 255, 255, alphaFromUpdateCount);
				g.DrawImage(imageByID, (int)num, (int)num2);
				g.PopState();
				return;
			}
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_CANNON_BALL);
			float num3 = Common._S(this.mX) - (float)(imageByID2.mWidth / 2);
			float num4 = Common._S(this.mY) - (float)(imageByID2.mHeight / 2);
			if (g.Is3D())
			{
				g.DrawImageRotatedF(imageByID2, num3, num4, (double)(this.mRotation + 3.14159f));
				return;
			}
			g.DrawImageRotated(imageByID2, (int)num3, (int)num4, (double)(this.mRotation + 3.14159f));
		}

		public new void Draw(Graphics g)
		{
			this.Draw(g, 0, 0);
		}

		public Ball GetHitBall()
		{
			return this.mHitBall;
		}

		public float GetHitPercent()
		{
			return this.mHitPercent;
		}

		public float GetVelX()
		{
			return this.mVelX;
		}

		public float GetVelY()
		{
			return this.mVelY;
		}

		public bool GetHitInFront()
		{
			return this.mHitInFront;
		}

		public bool GetJustFired()
		{
			return this.mJustFired;
		}

		public new int GetNumGaps()
		{
			return Enumerable.Count<GapInfo>(this.mGapInfo);
		}

		public int GetUpdateCount()
		{
			return this.mUpdateCount;
		}

		public void SetJustFired(bool fired)
		{
			this.mJustFired = fired;
		}

		public void SetMergeSpeed(float theSpeed)
		{
			this.mMergeSpeed = theSpeed;
		}

		public Ball mHitBall;

		public float mVelX;

		public float mVelY;

		public float mHitX;

		public float mHitY;

		public float mHitDX;

		public float mHitDY;

		public float mDestX;

		public float mDestY;

		public float mHitPercent;

		public float mMergeSpeed;

		public float mAngleFired;

		public new int mUpdateCount;

		public bool mHitInFront;

		public bool mHaveSetPrevBall;

		public bool mJustFired;

		public bool mDoNewMerge;

		public bool mSkip;

		public List<GapInfo> mGapInfo = new List<GapInfo>();

		public int[] mCurCurvePoint = new int[4];
	}
}
