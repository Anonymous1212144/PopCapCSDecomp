using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class LightningZap : IDisposable
	{
		public LightningZap(Board theBoard, int theStartX, int theStartY, int theEndX, int theEndY, Color theColor, float theTime, bool isFlamimg)
		{
			this.mBoard = theBoard;
			this.mDeleteMe = false;
			this.mFlaming = isFlamimg;
			this.mPercentDone = 0f;
			this.mDoneTime = theTime;
			this.mTimer = 0f;
			this.mColor = theColor;
			this.mStartPoint = new FPoint((float)theStartX, (float)theStartY);
			this.mEndPoint = new FPoint((float)theEndX, (float)theEndY);
			float num = this.mEndPoint.mY - this.mStartPoint.mY;
			float num2 = this.mEndPoint.mX - this.mStartPoint.mX;
			this.mAngle = (float)Math.Atan2((double)num, (double)num2);
			this.mLength = (float)Math.Sqrt((double)(num2 * num2 + num * num));
			this.mFrame = 0;
			this.mUpdates = 0;
			this.Update();
		}

		public void Dispose()
		{
		}

		public void Update()
		{
			Image image_LIGHTNING = GlobalMembersResourcesWP.IMAGE_LIGHTNING;
			bool flag = this.mBoard.WantsCalmEffects();
			this.mUpdates++;
			if (flag)
			{
				this.mTimer += GlobalMembers.M(0.05f);
			}
			else
			{
				this.mTimer += GlobalMembers.M(0.1f);
			}
			float num = this.mEndPoint.mX - this.mStartPoint.mX;
			float num2 = this.mEndPoint.mY - this.mStartPoint.mY;
			float num3 = Math.Max(GlobalMembers.M(1f), (float)Math.Sqrt((double)(num * num + num2 * num2)));
			float num4 = ConstantsWP.LIGHTNING_THICKNESS;
			float num5 = num4 * num2 / num3;
			float num6 = num4 * num / num3;
			if ((flag && this.mUpdates % GlobalMembers.M(10) != 0) || (!flag && this.mUpdates % GlobalMembers.M(5) != 0))
			{
				this.mPoints[0].Clear();
				this.mPoints[1].Clear();
				this.mFrame = BejeweledLivePlus.Misc.Common.Rand() % 5;
				int num7 = (int)Math.Max(1f, GlobalMembers.M(0.5f) * GlobalMembers.S(this.mLength) / (float)image_LIGHTNING.GetCelHeight()) + 1;
				for (int i = 0; i < num7; i++)
				{
					FPoint[] array = new FPoint[]
					{
						default(FPoint),
						default(FPoint)
					};
					float num8 = (float)i / (float)(num7 - 1);
					int num9 = 1;
					if (i != 0 && i < num7 - 1)
					{
						num9 = Math.Max(GlobalMembers.M(80), (int)(GlobalMembers.M(160f) * this.mTimer / this.mDoneTime));
					}
					if (flag)
					{
						num9 = Math.Max(1, (int)((float)num9 * GlobalMembers.M(0.5f)));
					}
					array[0].mX = (array[1].mX = this.mStartPoint.mX + num8 * num + (float)(num9 / 2) - (float)(BejeweledLivePlus.Misc.Common.Rand() % num9));
					array[0].mY = (array[1].mY = this.mStartPoint.mY + num8 * num2 + (float)(num9 / 2) - (float)(BejeweledLivePlus.Misc.Common.Rand() % num9));
					FPoint[] array2 = array;
					int num10 = 0;
					array2[num10].mX = array2[num10].mX - num5;
					FPoint[] array3 = array;
					int num11 = 1;
					array3[num11].mX = array3[num11].mX + num5;
					FPoint[] array4 = array;
					int num12 = 0;
					array4[num12].mY = array4[num12].mY + num6;
					FPoint[] array5 = array;
					int num13 = 1;
					array5[num13].mY = array5[num13].mY - num6;
					this.mPoints[0].Add(array[0]);
					this.mPoints[1].Add(array[1]);
				}
			}
			this.mPercentDone = this.mTimer / this.mDoneTime;
			if (this.mPercentDone >= 1f)
			{
				this.mDeleteMe = true;
			}
		}

		public void Draw(Graphics g)
		{
			Image image_LIGHTNING = GlobalMembersResourcesWP.IMAGE_LIGHTNING;
			Graphics3D graphics3D = g.Get3D();
			float num2;
			float num = (num2 = 0.15625f);
			num2 *= (float)this.mFrame;
			num += num2;
			num2 += GlobalMembers.M(0.02f);
			num += GlobalMembers.M(-0.02f);
			float num3 = Math.Max(0f, Math.Min((1f - this.mPercentDone) * 8f, 1f)) * this.mBoard.GetPieceAlpha();
			bool flag = GlobalMembers.gApp.Is3DAccelerated();
			int num4 = this.mPoints[0].size<FPoint>();
			g.PushState();
			g.Translate(GlobalMembers.S(this.mBoard.GetBoardX()), GlobalMembers.S(this.mBoard.GetBoardY()));
			g.SetColorizeImages(true);
			if (graphics3D != null)
			{
				graphics3D.SetTextureWrap(0, true);
			}
			int num5 = 0;
			int mAlpha = Math.Min(255, (int)(GlobalMembers.M(800f) * num3));
			Color color = this.mColor;
			color.mAlpha = mAlpha;
			Color theColor;
			theColor.mRed = (GlobalMembers.M(255) + this.mColor.mRed) / 2;
			theColor.mGreen = (GlobalMembers.M(255) + this.mColor.mGreen) / 2;
			theColor.mBlue = (GlobalMembers.M(255) + this.mColor.mBlue) / 2;
			theColor.mAlpha = mAlpha;
			float num6 = 0f;
			float num7 = 0f;
			if (flag)
			{
				num6 = g.mTransX;
				num7 = g.mTransY;
			}
			for (int i = 0; i < num4 - 1; i++)
			{
				float v = 0f;
				float v2 = 1f;
				float num8 = GlobalMembers.M(0f);
				SexyVertex2D[] lz_Draw_aTriVertices = LightningZap.LZ_Draw_aTriVertices;
				float num9 = this.mPoints[0][i].mX - this.mPoints[0][i + 1].mX + this.mPoints[1][i].mX - this.mPoints[1][i + 1].mX;
				float num10 = this.mPoints[0][i].mY - this.mPoints[0][i + 1].mY + this.mPoints[1][i].mY - this.mPoints[1][i + 1].mY;
				float num11 = 1f / Math.Max(GlobalMembers.M(1f), (float)Math.Sqrt((double)(num9 * num9 + num10 * num10)));
				float num12 = num9;
				num9 = num8 * (num10 * num11);
				num10 = num8 * (num12 * num11);
				float x = num6 + GlobalMembers.S(this.mPoints[0][i].mX - num9);
				float y = num7 + GlobalMembers.S(this.mPoints[0][i].mY - num10);
				float x2 = num6 + GlobalMembers.S(this.mPoints[1][i].mX + num9);
				float y2 = num7 + GlobalMembers.S(this.mPoints[1][i].mY + num10);
				float x3 = num6 + GlobalMembers.S(this.mPoints[1][i + 1].mX + num9);
				float y3 = num7 + GlobalMembers.S(this.mPoints[1][i + 1].mY + num10);
				float x4 = num6 + GlobalMembers.S(this.mPoints[0][i + 1].mX - num9);
				float y4 = num7 + GlobalMembers.S(this.mPoints[0][i + 1].mY - num10);
				lz_Draw_aTriVertices[0].x = x;
				lz_Draw_aTriVertices[0].y = y;
				lz_Draw_aTriVertices[0].u = 0f;
				lz_Draw_aTriVertices[0].v = v;
				lz_Draw_aTriVertices[1].x = x2;
				lz_Draw_aTriVertices[1].y = y2;
				lz_Draw_aTriVertices[1].u = 1f;
				lz_Draw_aTriVertices[1].v = v;
				lz_Draw_aTriVertices[2].x = x3;
				lz_Draw_aTriVertices[2].y = y3;
				lz_Draw_aTriVertices[2].u = 1f;
				lz_Draw_aTriVertices[2].v = v2;
				lz_Draw_aTriVertices[3].x = x4;
				lz_Draw_aTriVertices[3].y = y4;
				lz_Draw_aTriVertices[3].u = 0f;
				lz_Draw_aTriVertices[3].v = v2;
				LightningZap.LZ_Draw_aTriList_1[num5, 0] = LightningZap.LZ_Draw_aTriVertices[0];
				LightningZap.LZ_Draw_aTriList_1[num5, 1] = LightningZap.LZ_Draw_aTriVertices[1];
				LightningZap.LZ_Draw_aTriList_1[num5, 2] = LightningZap.LZ_Draw_aTriVertices[2];
				LightningZap.LZ_Draw_aTriList_1[num5 + 1, 0] = LightningZap.LZ_Draw_aTriVertices[2];
				LightningZap.LZ_Draw_aTriList_1[num5 + 1, 1] = LightningZap.LZ_Draw_aTriVertices[3];
				LightningZap.LZ_Draw_aTriList_1[num5 + 1, 2] = LightningZap.LZ_Draw_aTriVertices[0];
				lz_Draw_aTriVertices = LightningZap.LZ_Draw_aTriVertices;
				lz_Draw_aTriVertices[0].u = num2;
				lz_Draw_aTriVertices[1].u = num;
				lz_Draw_aTriVertices[2].u = num;
				lz_Draw_aTriVertices[3].u = num2;
				LightningZap.LZ_Draw_aTriList_2[num5, 0] = LightningZap.LZ_Draw_aTriVertices[0];
				LightningZap.LZ_Draw_aTriList_2[num5, 1] = LightningZap.LZ_Draw_aTriVertices[1];
				LightningZap.LZ_Draw_aTriList_2[num5, 2] = LightningZap.LZ_Draw_aTriVertices[2];
				LightningZap.LZ_Draw_aTriList_2[num5 + 1, 0] = LightningZap.LZ_Draw_aTriVertices[2];
				LightningZap.LZ_Draw_aTriList_2[num5 + 1, 1] = LightningZap.LZ_Draw_aTriVertices[3];
				LightningZap.LZ_Draw_aTriList_2[num5 + 1, 2] = LightningZap.LZ_Draw_aTriVertices[0];
				num5 += 2;
				if (num5 >= Bej3Com.MAX_TRIS)
				{
					break;
				}
			}
			if (flag)
			{
				g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_GRITTYBLURRY, LightningZap.LZ_Draw_aTriList_1, num5, color, 1, 0f, 0f, true, Rect.INVALIDATE_RECT);
				g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_GRITTYBLURRY, LightningZap.LZ_Draw_aTriList_1, num5, color, 1, 0f, 0f, true, Rect.INVALIDATE_RECT);
				g.DrawTrianglesTex(image_LIGHTNING, LightningZap.LZ_Draw_aTriList_2, num5, theColor, 1, 0f, 0f, true, Rect.INVALIDATE_RECT);
				g.DrawTrianglesTex(image_LIGHTNING, LightningZap.LZ_Draw_aTriList_2, num5, theColor, 1, 0f, 0f, true, Rect.INVALIDATE_RECT);
			}
			else
			{
				g.SetColor(color);
				g.SetDrawMode(1);
				g.DrawTrianglesTex(image_LIGHTNING, LightningZap.LZ_Draw_aTriList_1, num5);
				g.DrawTrianglesTex(image_LIGHTNING, LightningZap.LZ_Draw_aTriList_1, num5);
				g.DrawTrianglesTex(image_LIGHTNING, LightningZap.LZ_Draw_aTriList_2, num5);
				g.DrawTrianglesTex(image_LIGHTNING, LightningZap.LZ_Draw_aTriList_2, num5);
			}
			g.PopState();
		}

		public Board mBoard;

		public FPoint mStartPoint;

		public FPoint mEndPoint;

		public List<FPoint>[] mPoints = new List<FPoint>[]
		{
			new List<FPoint>(),
			new List<FPoint>()
		};

		public float mPercentDone;

		public float mTimer;

		public float mDoneTime;

		public float mAngle;

		public float mLength;

		public Color mColor;

		public int mUpdates;

		public int mFrame;

		public bool mDeleteMe;

		public bool mFlaming;

		private static SexyVertex2D[,] LZ_Draw_aTriList_1 = new SexyVertex2D[Bej3Com.MAX_TRIS, 3];

		private static SexyVertex2D[,] LZ_Draw_aTriList_2 = new SexyVertex2D[Bej3Com.MAX_TRIS, 3];

		private static SexyVertex2D[] LZ_Draw_aTriVertices = new SexyVertex2D[4];
	}
}
