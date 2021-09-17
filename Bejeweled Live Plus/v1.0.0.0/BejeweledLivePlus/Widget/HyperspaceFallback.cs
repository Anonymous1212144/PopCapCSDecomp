using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Widget
{
	internal class HyperspaceFallback : Hyperspace
	{
		public HyperspaceFallback(Board theBoard)
		{
			this.mBoard = theBoard;
			this.mFromBall = new CrystalBall("", "", "", 0, null, Bej3Widget.COLOR_CRYSTALBALL_FONT);
			this.mToBall = new CrystalBall("", "", "", 0, null, Bej3Widget.COLOR_CRYSTALBALL_FONT);
			this.mFromBall.mWidth = 0;
			this.mFromBall.mHeight = 0;
			this.mToBall.mWidth = 0;
			this.mToBall.mHeight = 0;
			this.mDelay = GlobalMembers.M(0);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eHYPERSPACE_FROM_CENTER_PCT, this.mFromCenterPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eHYPERSPACE_TO_CENTER_PCT, this.mToCenterPct, this.mFromCenterPct);
			this.mFromBall.mFullPct.SetCurve(GlobalMembers.MP("b+0,1,0.003333,1,~###x~###   ]####     J####"), this.mFromCenterPct);
			this.mFromBall.mScale.SetCurve(GlobalMembers.MP("b+0,1,0,1,~###|~###  @/k=] 3####     R####"), this.mFromCenterPct);
			this.mToBall.mFullPct.SetCurve(GlobalMembers.MP("b+0,1,0.003333,1,####    i####     8~###"), this.mFromCenterPct);
			this.mToBall.mScale.SetCurve(GlobalMembers.MP("b+0,1,0.003333,1,####   d####      =~###"), this.mFromCenterPct);
		}

		public override void Dispose()
		{
		}

		public override void Update()
		{
			base.Update();
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			if (!this.mWidgetsAdded)
			{
				if (this.mWidgetManager != null)
				{
					this.mWidgetManager.AddWidget(this.mFromBall);
					this.mWidgetManager.AddWidget(this.mToBall);
				}
				this.mWidgetsAdded = true;
			}
			if (!this.mFromCenterPct.IncInVal())
			{
				this.mBoard.HyperspaceEvent(HYPERSPACEEVENT.HYPERSPACEEVENT_Finish);
			}
			if (this.mUpdateCnt == GlobalMembers.M(200))
			{
				this.mBoard.HyperspaceEvent(HYPERSPACEEVENT.HYPERSPACEEVENT_OldLevelClear);
				this.mToBall.mImage = this.mBoard.mBackground.GetBackgroundImage();
			}
			if (this.mUpdateCnt == GlobalMembers.M(250))
			{
				this.mBoard.RandomizeBoard();
			}
			this.mFromBall.Update();
			this.mToBall.Update();
		}

		public override float GetPieceAlpha()
		{
			return this.mBoard.GetPieceAlpha();
		}

		public override void Draw(Graphics g)
		{
			FPoint[] array = new FPoint[]
			{
				new FPoint((float)(this.mWidth / 2), (float)(this.mHeight / 2)),
				new FPoint((float)(this.mWidth / 2), (float)(this.mHeight / 2))
			};
			CrystalBall[] array2 = new CrystalBall[] { this.mFromBall, this.mToBall };
			float[] array3 = new float[]
			{
				(float)this.mFromCenterPct,
				(float)this.mToCenterPct
			};
			for (int i = GlobalMembers.M(1); i < 2; i++)
			{
				g.PushState();
				FPoint impliedObject = new FPoint((float)(this.mWidth / 2), (float)(this.mHeight / 2)) * array3[i] + array[i] * (1f - array3[i]);
				g.Translate((int)impliedObject.mX, (int)impliedObject.mY);
				array2[i].mOffset = impliedObject - new FPoint((float)((int)impliedObject.mX), (float)((int)impliedObject.mY));
				array2[i].Draw(g);
				g.PopState();
			}
		}

		public override void DrawBackground(Graphics g)
		{
		}

		public override bool IsUsing3DTransition()
		{
			return false;
		}

		public override void SetBGImage(SharedImageRef inImage)
		{
			this.mFromBall.mImage = new SharedImageRef(inImage);
		}

		public Board mBoard;

		public CrystalBall mFromBall;

		public CrystalBall mToBall;

		public CurvedVal mFromCenterPct = new CurvedVal();

		public CurvedVal mToCenterPct = new CurvedVal();

		public int mDelay;

		private bool mWidgetsAdded;
	}
}
