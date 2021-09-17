using System;
using System.Linq;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class HoleMgr
	{
		public HoleMgr()
		{
		}

		public HoleMgr(HoleMgr rhs)
		{
			if (rhs == null)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				this.mHoles[i] = new HoleInfo(rhs.mHoles[i]);
			}
			this.mNumHoles = rhs.mNumHoles;
		}

		protected void SetupHole(ref int x, ref int y, ref float rot)
		{
			x -= 48;
			y -= 48;
			while (rot < 0f)
			{
				rot += 6.28318f;
			}
			while (rot > 6.28318f)
			{
				rot -= 6.28318f;
			}
			if ((double)Math.Abs(rot) < 0.2)
			{
				rot = 0f;
				return;
			}
			if ((double)Math.Abs(rot - 1.570795f) < 0.2)
			{
				rot = 1.570795f;
				return;
			}
			if ((double)Math.Abs(rot - 3.14159f) < 0.2)
			{
				rot = 3.14159f;
				return;
			}
			if ((double)Math.Abs(rot - 4.712385f) < 0.2)
			{
				rot = 4.712385f;
				return;
			}
			if ((double)Math.Abs(rot - 6.28318f) < 0.2)
			{
				rot = 0f;
			}
		}

		public int PlaceHole(int curve_num, int x, int y, float rot, bool visible)
		{
			this.SetupHole(ref x, ref y, ref rot);
			HoleInfo holeInfo = new HoleInfo();
			holeInfo.mX = x;
			holeInfo.mY = y;
			holeInfo.mFrame = 0;
			holeInfo.mRotation = rot;
			holeInfo.mPercentOpen = 0f;
			holeInfo.mVisible = visible;
			holeInfo.mCurve = null;
			holeInfo.mCurveNum = curve_num;
			this.mHoles[this.mNumHoles++] = holeInfo;
			return this.mNumHoles - 1;
		}

		public int PlaceHole(int curve_num, int x, int y, float rot)
		{
			return this.PlaceHole(curve_num, x, y, rot, true);
		}

		public void UpdateHoleInfo(int hole_index, int x, int y, float rot, bool visible)
		{
			HoleInfo holeInfo = this.mHoles[hole_index];
			this.SetupHole(ref x, ref y, ref rot);
			holeInfo.mX = x;
			holeInfo.mY = y;
			holeInfo.mRotation = rot;
			holeInfo.mVisible = visible;
		}

		public void UpdateHoleInfo(int hole_index, int x, int y, float rot)
		{
			this.UpdateHoleInfo(hole_index, x, y, rot, true);
		}

		public void Update()
		{
			for (int i = 0; i < this.mNumHoles; i++)
			{
				HoleInfo holeInfo = this.mHoles[i];
				for (int j = 0; j < holeInfo.mShared.Count; j++)
				{
					HoleInfo holeInfo2 = this.mHoles[holeInfo.mShared[j]];
					if (holeInfo.GetPctOpen() > holeInfo2.GetPctOpen())
					{
						holeInfo2.SetPctOpen(holeInfo.GetPctOpen());
					}
				}
				holeInfo.Update();
			}
		}

		public void SetPctOpen(int curve_num, float pct_open)
		{
			this.mHoles[curve_num].SetPctOpen(pct_open);
			HoleInfo holeInfo = this.mHoles[curve_num];
			if (!holeInfo.mVisible)
			{
				for (int i = 0; i < Enumerable.Count<int>(holeInfo.mShared); i++)
				{
					HoleInfo holeInfo2 = this.mHoles[holeInfo.mShared[i]];
					if (holeInfo.GetPctOpen() > holeInfo2.GetPctOpen())
					{
						holeInfo2.SetPctOpen(holeInfo.GetPctOpen());
					}
				}
			}
		}

		public void Draw(Graphics g)
		{
			float hilite_override = 0f;
			for (int i = 0; i < this.mNumHoles; i++)
			{
				if (!this.mHoles[i].mVisible && this.mHoles[i].mCurve != null && this.mHoles[i].mCurve.mInitialPathHilite)
				{
					hilite_override = this.mHoles[i].mCurve.mSkullHilite;
				}
			}
			for (int j = 0; j < this.mNumHoles; j++)
			{
				if (this.mHoles[j].mVisible)
				{
					this.mHoles[j].Draw(g, hilite_override);
				}
			}
		}

		public void DrawRings(Graphics g)
		{
			for (int i = 0; i < this.mNumHoles; i++)
			{
				if (this.mHoles[i].mVisible)
				{
					this.mHoles[i].DrawRings(g);
				}
			}
		}

		public int GetNumHoles()
		{
			return this.mNumHoles;
		}

		public HoleInfo GetHole(int idx)
		{
			if (idx < 0 || idx >= this.mNumHoles)
			{
				return null;
			}
			return this.mHoles[idx];
		}

		protected HoleInfo[] mHoles = new HoleInfo[4];

		protected int mNumHoles;
	}
}
