using System;
using System.Collections.Generic;
using Sexy;

namespace Bejeweled3
{
	public class PointsManager : Widget
	{
		public PointsManager()
		{
			this.mHasAlpha = true;
			this.mMouseVisible = false;
			this.mClip = false;
			this.mbPause = false;
		}

		public new void Dispose()
		{
			this.KillPoints();
			base.Dispose();
		}

		public void Add(PointsBej3 thePoints)
		{
			this.mPointsList.Add(thePoints);
		}

		public override void Draw(Graphics g)
		{
			base.DeferOverlay(1);
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColor(new SexyColor(SexyColor.White));
			foreach (PointsBej3 pointsBej in this.mPointsList)
			{
				pointsBej.Draw(g);
			}
		}

		public void KillPoints()
		{
			for (int i = 0; i < this.mPointsList.Count; i++)
			{
				this.mPointsList[i].PrepareForReuse();
			}
			this.mPointsList.Clear();
		}

		public void Pause(bool bPause, float fFade)
		{
			this.mbPause = bPause;
			for (int i = 0; i < this.mPointsList.size<PointsBej3>(); i++)
			{
				this.mPointsList[i].mfFade = fFade;
			}
		}

		public override void Update()
		{
			if (this.mbPause)
			{
				return;
			}
			for (int i = 0; i < this.mPointsList.Count; i++)
			{
				this.mPointsList[i].Update();
				if (this.mPointsList[i].mDeleteMe)
				{
					this.mPointsList[i].PrepareForReuse();
					this.mPointsList.RemoveAt(i);
					i--;
					this.MarkDirty();
				}
			}
		}

		public List<PointsBej3> mPointsList = new List<PointsBej3>();

		private bool mbPause;
	}
}
