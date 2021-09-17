using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Misc
{
	public class PointsManager : Widget
	{
		public PointsManager()
		{
			this.mHasAlpha = true;
			this.mMouseVisible = false;
			this.mClip = false;
			this.mOverlayLevel = 1;
		}

		public override void Dispose()
		{
			this.KillPoints();
		}

		public override void Update()
		{
			int i = 0;
			while (i < this.mPointsList.Count)
			{
				if (this.mPointsList[i].mState == 2)
				{
					this.mPointsList[i].mRotation = (float)this.Update_vertShifterRotation.GetOutVal((double)((float)this.mPointsList[i].mUpdateCnt / 100f));
					this.mPointsList[i].mY += (float)this.Update_vertShifterDY.GetOutVal((double)((float)this.mPointsList[i].mUpdateCnt / 100f));
					float num = (float)this.mPointsList[i].mUpdateCnt / 100f;
					if ((double)num >= this.Update_vertShifterRotation.mInMax && (double)num >= this.Update_vertShifterDY.mInMax)
					{
						this.mPointsList[i].StartFading();
					}
				}
				this.mPointsList[i].Update();
				if (this.mPointsList[i].mDeleteMe)
				{
					if (this.mPointsList[i] != null)
					{
						this.mPointsList[i].Dispose();
						this.mPointsList[i] = null;
					}
					this.mPointsList.RemoveAt(i);
					this.MarkDirty();
				}
				else
				{
					i++;
				}
			}
		}

		public Points Add(int theX, int theY, int thePoints, Color theColor, uint theId, bool usePointMultiplier, int theMoveCreditId)
		{
			return this.Add(theX, theY, thePoints, theColor, theId, usePointMultiplier, theMoveCreditId, false);
		}

		public Points Add(int theX, int theY, int thePoints, Color theColor, uint theId, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd)
		{
			if (thePoints <= 0 && !theForceAdd)
			{
				return null;
			}
			float num = (float)GlobalMembers.gApp.mBoard.mPointMultiplier * GlobalMembers.gApp.mBoard.GetModePointMultiplier();
			if (!usePointMultiplier)
			{
				num = 1f;
			}
			Points points = null;
			int num2 = thePoints;
			thePoints *= (int)num;
			if (theX >= 0 && theY >= 0)
			{
				float num3 = ModVal.M(50f);
				float num4 = ModVal.M(1000f);
				float num5 = ModVal.M(0.6f);
				float num6 = ModVal.M(1f);
				float num7 = ModVal.M(1f);
				int num8 = thePoints;
				int num9 = num2;
				int num10 = (int)((double)num9 * Math.Pow((double)GlobalMembers.gApp.mBoard.mPointMultiplier, 0.44999998807907104));
				if (theId != 4294967295U)
				{
					for (int i = 0; i < this.mPointsList.Count; i++)
					{
						Points points2 = this.mPointsList[i];
						if (points2.mId == theId)
						{
							points2.mState = 0;
							points2.mAlpha = 1f;
							points2.mValue += (uint)thePoints;
							points2.mDestScale = Math.Min(num6, points2.mDestScale + 0.05f);
							points2.mTimer = num7;
							points = points2;
							num8 = (int)points2.mValue;
							num10 += points2.mCorrectedPoints;
							num9 += points2.mScalePoints;
							break;
						}
					}
				}
				string text = num8.ToString();
				float num11 = num6 - num5;
				float num12 = Math.Max(0f, Math.Min(1f, ((float)num9 - num3) / num4));
				float num13 = Math.Max(0f, Math.Min(1f, ((float)num10 - num3) / num4));
				float mDestScale = num5 + Math.Min(1f, num12 * 2f) * num11;
				theY = Math.Max(theY, 120);
				if (points == null && GlobalMembersResources.FONT_FLOATERS != null)
				{
					int theAnim = -1;
					points = new Points(GlobalMembers.gApp, GlobalMembersResources.FONT_FLOATERS, text, theX, theY, num7, 0, theColor, theAnim);
					points.mMoveCreditId = theMoveCreditId;
					points.mId = theId;
					points.mDestScale = mDestScale;
					points.mScaleDifMult = ModVal.M(0.15f);
					points.mScaleDampening = ModVal.M(0.46f) + (float)num9 * ModVal.M(0.0015f);
					if (points.mScaleDampening > ModVal.M(0.962f))
					{
						points.mScaleDampening = ModVal.M(0.962f);
					}
					points.mValue = (uint)thePoints;
					this.mPointsList.Add(points);
				}
				else if (points != null)
				{
					points.mString = text;
					points.RestartWobble();
					if (!points.mDrawn)
					{
						points.mX = ((float)theX + points.mX) / 2f;
						points.mY = ((float)theY + points.mY) / 2f;
					}
				}
				points.mColor = theColor;
				points.mColorCycle[0].mCycleColors.Clear();
				points.mColorCycle[1].mCycleColors.Clear();
				for (int j = 0; j < 6; j++)
				{
					for (int k = 0; k < 3; k++)
					{
						float num14 = 0f;
						float num15 = 0f;
						float num16 = 0f;
						if (k == 0)
						{
							num16 = Math.Min(1f, Math.Max(0f, (num13 - 0.3f) * 2f));
							float num17 = Math.Max(0f, num13 - 0.5f);
							num14 = ((j % 2 == 0) ? 0.5f : (0.5f + num17 * ModVal.M(1f)));
							num15 = 1f;
						}
						else if (k == 1)
						{
							num16 = Math.Min(1f, Math.Max(0f, (num13 - 0.3f) * 2f));
							float num18 = Math.Max(0f, num13 - 0.1f);
							num14 = ((j % 2 == 0) ? 0.5f : (0.5f + num18 * ModVal.M(1f)));
							num15 = Math.Max(0f, (num13 - 0.5f) * 3f);
						}
						else if (k == 2)
						{
							num16 = Math.Min(1f, Math.Max(0f, (num13 - 1f) * 1f));
							num14 = 1f;
							num15 = 0.7f;
						}
						Color color = default(Color);
						color.mRed = (int)((float)this.gCycleColors[j].mRed * num16 + Math.Min(255f, (float)theColor.mRed * num14 * (1f - num16)));
						color.mGreen = (int)((float)this.gCycleColors[j].mGreen * num16 + Math.Min(255f, (float)theColor.mGreen * num14 * (1f - num16)));
						color.mBlue = (int)((float)this.gCycleColors[j].mBlue * num16 + Math.Min(255f, (float)theColor.mBlue * num14 * (1f - num16)));
						color.mAlpha = (int)num15 * 255;
						points.mColorCycle[k].mCycleColors.Add(color);
					}
				}
				points.mColorCycle[0].SetPosition(0.25f);
				points.mColorCycle[1].SetPosition(0.75f);
				points.mColorCycling = true;
				points.mCorrectedPoints = num10;
				points.mScalePoints = num9;
				points.mWobbleScale = num12 * ModVal.M(0.7f);
				points.mColor.mAlpha = (int)(255f * Math.Min(1f, ModVal.M(0.75f) + num13 * ModVal.M(0f)));
				points.mTimer = 0.6f + num12 * 1.6f;
				if (points != null)
				{
					points.mScale = points.mDestScale * ModVal.M(0.1f);
				}
			}
			return points;
		}

		public Points Find(uint theId)
		{
			for (int i = 0; i < this.mPointsList.Count; i++)
			{
				if (this.mPointsList[i].mId == theId)
				{
					return this.mPointsList[i];
				}
			}
			return null;
		}

		public override void Draw(Graphics g)
		{
			base.DeferOverlay(this.mOverlayLevel);
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			g.SetDrawMode(0);
			g.SetColor(Color.White);
			foreach (Points points in this.mPointsList)
			{
				g.mColor.mAlpha = (int)(((Board)this.mParent).GetPieceAlpha() * 255f);
				g.PushColorMult();
				points.Draw(g);
				g.PopColorMult();
			}
		}

		public void KillPoints()
		{
			while (this.mPointsList.Count > 0)
			{
				Points points = this.mPointsList.back<Points>();
				if (points != null)
				{
					points.Dispose();
				}
				this.mPointsList.RemoveAt(this.mPointsList.Count - 1);
			}
		}

		private CurvedVal Update_vertShifterRotation = new CurvedVal("b+0,1.57,0.01,1,~###   3####      n####");

		private CurvedVal Update_vertShifterDY = new CurvedVal("b+-12,8,0.01,0.4,####   M####   W~###  z]###");

		private Color[] gCycleColors = new Color[]
		{
			new Color(255, 0, 0),
			new Color(255, 255, 0),
			new Color(0, 255, 0),
			new Color(0, 255, 255),
			new Color(0, 0, 255),
			new Color(255, 0, 255)
		};

		public List<Points> mPointsList = new List<Points>();

		public int mOverlayLevel;
	}
}
