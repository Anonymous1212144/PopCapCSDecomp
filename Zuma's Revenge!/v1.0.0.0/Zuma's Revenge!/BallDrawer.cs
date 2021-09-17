using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class BallDrawer
	{
		public void Reset()
		{
			this.mMaxBallPriority = 0;
			for (int i = 0; i < 5; i++)
			{
				this.mNumBalls[i] = 0;
				this.mNumShadows[i] = 0;
				this.mNumOverlays[i] = 0;
				this.mNumUnderlays[i] = 0;
			}
		}

		public void AddBall(Ball theBall, int thePriority)
		{
			int num = this.mNumBalls[thePriority]++;
			this.mBalls[thePriority, num] = theBall;
		}

		public void AddShadow(Ball theBall, int thePriority)
		{
			int num = this.mNumShadows[thePriority]++;
			this.mShadows[thePriority, num] = theBall;
		}

		public void AddOverlay(Ball theBall, int thePriority)
		{
			int num = this.mNumOverlays[thePriority]++;
			this.mOverlays[thePriority, num] = theBall;
		}

		public void AddUnderlay(Ball theBall, int thePriority)
		{
			int num = this.mNumUnderlays[thePriority]++;
			this.mUnderlays[thePriority, num] = theBall;
		}

		public void Draw(Graphics g, Board theBoard)
		{
			g.Get3D();
			for (int i = 0; i < 5; i++)
			{
				if (i != 0)
				{
					theBoard.DrawTunnels(g, i, true);
				}
				theBoard.mLevel.DrawPriority(g, i);
				for (int j = 0; j < theBoard.mLevel.mNumCurves; j++)
				{
					theBoard.mLevel.mCurveMgr[j].DrawMisc(g, i);
					theBoard.mLevel.mCurveMgr[j].DrawSkullPathShit(g, i);
				}
				if (!Board.gHideBalls)
				{
					int num = this.mNumShadows[i];
					for (int j = 0; j < num; j++)
					{
						this.mShadows[i, j].DrawShadow(g);
					}
					theBoard.DrawTunnels(g, i, false);
					num = this.mNumUnderlays[i];
					for (int j = 0; j < num; j++)
					{
						this.mUnderlays[i, j].DrawBottomLayer(g);
					}
					num = this.mNumBalls[i];
					for (int j = 0; j < num; j++)
					{
						this.mBalls[i, j].DrawBase(g, 0, 0);
					}
					for (int j = 0; j < num; j++)
					{
						this.mBalls[i, j].DrawAdditive(g, 0, 0);
					}
					if (g.Is3D())
					{
						num = this.mNumOverlays[i];
						for (int j = 0; j < num; j++)
						{
							this.mOverlays[i, j].DrawTopLayer(g);
						}
					}
				}
			}
			for (int j = 0; j < theBoard.mLevel.mNumCurves; j++)
			{
				theBoard.mLevel.mCurveMgr[j].DrawAboveBalls(g);
			}
		}

		public int mMaxBallPriority;

		public int[] mNumBalls = new int[5];

		public int[] mNumShadows = new int[5];

		public int[] mNumOverlays = new int[5];

		public int[] mNumUnderlays = new int[5];

		private Ball[,] mBalls = new Ball[5, 1024];

		private Ball[,] mShadows = new Ball[5, 1024];

		private Ball[,] mOverlays = new Ball[5, 1024];

		private Ball[,] mUnderlays = new Ball[5, 1024];
	}
}
