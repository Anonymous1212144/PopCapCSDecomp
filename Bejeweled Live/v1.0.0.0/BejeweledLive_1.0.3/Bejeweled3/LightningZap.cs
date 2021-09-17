using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class LightningZap
	{
		private Image anImage
		{
			get
			{
				return AtlasResources.IMAGE_LIGHTNING;
			}
		}

		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 10; i++)
			{
				new LightningZap().PrepareForReuse();
			}
		}

		private LightningZap()
		{
		}

		public static LightningZap GetNewLightningZap(BoardBejLive theBoard, int theStartX, int theStartY, int theEndX, int theEndY, SexyColor theColor, float theTime, bool isFlamimg)
		{
			if (LightningZap.unusedObjects.Count > 0)
			{
				LightningZap lightningZap = LightningZap.unusedObjects.Pop();
				lightningZap.Reset(theBoard, theStartX, theStartY, theEndX, theEndY, theColor, theTime, isFlamimg);
				return lightningZap;
			}
			return new LightningZap(theBoard, theStartX, theStartY, theEndX, theEndY, theColor, theTime, isFlamimg);
		}

		public void PrepareForReuse()
		{
			LightningZap.unusedObjects.Push(this);
		}

		public void Reset(BoardBejLive theBoard, int theStartX, int theStartY, int theEndX, int theEndY, SexyColor theColor, float theTime, bool isFlamimg)
		{
			for (int i = 0; i < this.mPoints.Length; i++)
			{
				if (this.mPoints[i] == null)
				{
					this.mPoints[i] = new List<TPointFloat>();
				}
				else
				{
					this.mPoints[i].Clear();
				}
			}
			this.mBoard = theBoard;
			this.mDeleteMe = false;
			this.mRainbowSize.SetCurve(CurvedValDefinition.mRainbowSizeCurve);
			this.mRainbowAlpha.SetCurve(CurvedValDefinition.mRainbowAlphaCurve);
			this.mFlaming = isFlamimg;
			this.mPercentDone = 0f;
			this.mDoneTime = theTime;
			this.mTimer = 0f;
			this.mColor = theColor;
			this.mStartPoint = new TPointFloat((float)theStartX, (float)theStartY);
			this.mEndPoint = new TPointFloat((float)theEndX, (float)theEndY);
			float num = this.mEndPoint.mY - this.mStartPoint.mY;
			float num2 = this.mEndPoint.mX - this.mStartPoint.mX;
			this.mAngle = (float)Math.Atan2((double)num, (double)num2);
			this.mLength = (float)Math.Sqrt((double)(num2 * num2 + num * num));
			this.mFrame = 0;
			this.mUpdates = 0;
			this.Update();
		}

		private LightningZap(BoardBejLive theBoard, int theStartX, int theStartY, int theEndX, int theEndY, SexyColor theColor, float theTime, bool isFlamimg)
		{
			this.Reset(theBoard, theStartX, theStartY, theEndX, theEndY, theColor, theTime, isFlamimg);
		}

		public void Update()
		{
			this.mUpdates++;
			this.mTimer += Constants.mConstants.M(0.1f);
			float num = this.mEndPoint.mX - this.mStartPoint.mX;
			float num2 = this.mEndPoint.mY - this.mStartPoint.mY;
			float num3 = Math.Max(Constants.mConstants.M(1f), (float)Math.Sqrt((double)(num * num + num2 * num2)));
			float num4 = (this.mFlaming ? Constants.mConstants.S(80f) : Constants.mConstants.S(80f));
			float num5 = num4 * num2 / num3;
			float num6 = num4 * num / num3;
			if ((float)this.mUpdates % Constants.mConstants.M(5f) == 0f)
			{
				this.mPoints[0].Clear();
				this.mPoints[1].Clear();
				this.mFrame = Board.Rand() % this.anImage.GetCelCount();
				int num7 = (int)Math.Max(1f, Constants.mConstants.M(0.5f) * this.mLength / (float)this.anImage.GetCelHeight()) + 1;
				for (int i = 0; i < num7; i++)
				{
					float num8 = (float)i / (float)(num7 - 1);
					int num9 = 1;
					if (i != 0 && i < num7 - 1)
					{
						num9 = (int)Math.Max(Constants.mConstants.S(80f), (float)((int)(Constants.mConstants.S(160f) * this.mTimer / this.mDoneTime)));
					}
					this.aPoint[0].mX = (this.aPoint[1].mX = this.mStartPoint.mX + num8 * num + (float)(num9 / 2) - (float)(Board.Rand() % num9));
					this.aPoint[0].mY = (this.aPoint[1].mY = this.mStartPoint.mY + num8 * num2 + (float)(num9 / 2) - (float)(Board.Rand() % num9));
					TPointFloat[] array = this.aPoint;
					int num10 = 0;
					array[num10].mX = array[num10].mX - num5;
					TPointFloat[] array2 = this.aPoint;
					int num11 = 1;
					array2[num11].mX = array2[num11].mX + num5;
					TPointFloat[] array3 = this.aPoint;
					int num12 = 0;
					array3[num12].mY = array3[num12].mY + num6;
					TPointFloat[] array4 = this.aPoint;
					int num13 = 1;
					array4[num13].mY = array4[num13].mY - num6;
					this.mPoints[0].Add(this.aPoint[0]);
					this.mPoints[1].Add(this.aPoint[1]);
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
			float num2;
			float num = (num2 = 1f / (float)this.anImage.GetCelCount());
			num2 *= (float)this.mFrame;
			num += num2;
			num2 += Constants.mConstants.M(0.02f);
			num += Constants.mConstants.M(-0.02f);
			float num3 = Math.Max(0f, Math.Min((1f - this.mPercentDone) * 8f, 1f));
			int count = this.mPoints[0].Count;
			int theDrawMode = 1;
			g.BeginDrawTrianglesTexBatch(Graphics.WrapSamplerState, theDrawMode, AtlasResources.IMAGE_LIGHTNING_GLOW);
			for (int i = 0; i < count - 1; i++)
			{
				float num4 = 0f;
				float num5 = 1f;
				float num6 = Constants.mConstants.M(0f);
				float num7 = this.mPoints[0][i].mX - this.mPoints[0][i + 1].mX + this.mPoints[1][i].mX - this.mPoints[1][i + 1].mX;
				float num8 = this.mPoints[0][i].mY - this.mPoints[0][i + 1].mY + this.mPoints[1][i].mY - this.mPoints[1][i + 1].mY;
				float num9 = (float)Math.Max((double)Constants.mConstants.M(1f), Math.Sqrt((double)(num7 * num7 + num8 * num8)));
				float num10 = num7;
				num7 = num6 * num8 / num9;
				num8 = num6 * num10 / num9;
				int num11 = 0;
				LightningZap.aTriVertices[num11].x = (float)g.mTransX + (this.mPoints[0][i].mX - num7);
				LightningZap.aTriVertices[num11].y = (float)g.mTransY + (this.mPoints[0][i].mY - num8);
				LightningZap.aTriVertices[num11].u = 0f;
				LightningZap.aTriVertices[num11].v = num4;
				num11++;
				LightningZap.aTriVertices[num11].x = (float)g.mTransX + (this.mPoints[1][i].mX + num7);
				LightningZap.aTriVertices[num11].y = (float)g.mTransY + (this.mPoints[1][i].mY + num8);
				LightningZap.aTriVertices[num11].u = 1f;
				LightningZap.aTriVertices[num11].v = num4;
				num11++;
				LightningZap.aTriVertices[num11].x = (float)g.mTransX + (this.mPoints[1][i + 1].mX + num7);
				LightningZap.aTriVertices[num11].y = (float)g.mTransY + (this.mPoints[1][i + 1].mY + num8);
				LightningZap.aTriVertices[num11].u = 1f;
				LightningZap.aTriVertices[num11].v = num5;
				num11++;
				LightningZap.aTriVertices[num11].x = (float)g.mTransX + (this.mPoints[0][i + 1].mX - num7);
				LightningZap.aTriVertices[num11].y = (float)g.mTransY + (this.mPoints[0][i + 1].mY - num8);
				LightningZap.aTriVertices[num11].u = 0f;
				LightningZap.aTriVertices[num11].v = num5;
				SexyColor aColor = this.mColor;
				aColor.mAlpha = Math.Min(255, (int)(800f * num3));
				LightningZap.aTri[0, 0] = LightningZap.aTriVertices[0];
				LightningZap.aTri[0, 1] = LightningZap.aTriVertices[1];
				LightningZap.aTri[0, 2] = LightningZap.aTriVertices[2];
				LightningZap.aTri[1, 0] = LightningZap.aTriVertices[2];
				LightningZap.aTri[1, 1] = LightningZap.aTriVertices[3];
				LightningZap.aTri[1, 2] = LightningZap.aTriVertices[0];
				g.DrawTrianglesTexBatch(LightningZap.aTri, 2, new Color?(aColor));
				num4 = num5;
				if (num4 >= 1f)
				{
					num4 -= 1f;
				}
			}
			g.EndDrawTrianglesTexBatch();
			g.BeginDrawTrianglesTexBatch(Graphics.WrapSamplerState, theDrawMode, this.anImage);
			for (int j = 0; j < count - 1; j++)
			{
				float num12 = 0f;
				float num13 = 1f;
				float num14 = Constants.mConstants.M(0f);
				float num15 = this.mPoints[0][j].mX - this.mPoints[0][j + 1].mX + this.mPoints[1][j].mX - this.mPoints[1][j + 1].mX;
				float num16 = this.mPoints[0][j].mY - this.mPoints[0][j + 1].mY + this.mPoints[1][j].mY - this.mPoints[1][j + 1].mY;
				float num17 = (float)Math.Max((double)Constants.mConstants.M(1f), Math.Sqrt((double)(num15 * num15 + num16 * num16)));
				float num18 = num15;
				num15 = num14 * num16 / num17;
				num16 = num14 * num18 / num17;
				int num19 = 0;
				LightningZap.aTriVertices[num19].x = (float)g.mTransX + this.mPoints[0][j].mX - num15;
				LightningZap.aTriVertices[num19].y = (float)g.mTransY + this.mPoints[0][j].mY - num16;
				LightningZap.aTriVertices[num19].u = num2;
				LightningZap.aTriVertices[num19].v = num12;
				num19++;
				LightningZap.aTriVertices[num19].x = (float)g.mTransX + this.mPoints[1][j].mX + num15;
				LightningZap.aTriVertices[num19].y = (float)g.mTransY + this.mPoints[1][j].mY + num16;
				LightningZap.aTriVertices[num19].u = num;
				LightningZap.aTriVertices[num19].v = num12;
				num19++;
				LightningZap.aTriVertices[num19].x = (float)g.mTransX + this.mPoints[1][j + 1].mX + num15;
				LightningZap.aTriVertices[num19].y = (float)g.mTransY + this.mPoints[1][j + 1].mY + num16;
				LightningZap.aTriVertices[num19].u = num;
				LightningZap.aTriVertices[num19].v = num13;
				num19++;
				LightningZap.aTriVertices[num19].x = (float)g.mTransX + this.mPoints[0][j + 1].mX - num15;
				LightningZap.aTriVertices[num19].y = (float)g.mTransY + this.mPoints[0][j + 1].mY - num16;
				LightningZap.aTriVertices[num19].u = num2;
				LightningZap.aTriVertices[num19].v = num13;
				SexyColor white = SexyColor.White;
				white.mAlpha = Math.Min(255, (int)(800.0 * (double)num3));
				LightningZap.aTri[0, 0] = LightningZap.aTriVertices[0];
				LightningZap.aTri[0, 1] = LightningZap.aTriVertices[1];
				LightningZap.aTri[0, 2] = LightningZap.aTriVertices[2];
				LightningZap.aTri[1, 0] = LightningZap.aTriVertices[2];
				LightningZap.aTri[1, 1] = LightningZap.aTriVertices[3];
				LightningZap.aTri[1, 2] = LightningZap.aTriVertices[0];
				g.DrawTrianglesTexBatch(LightningZap.aTri, 2, new Color?(white));
				num12 = num13;
				if (num12 >= 1f)
				{
					num12 -= 1f;
				}
			}
			g.EndDrawTrianglesTexBatch();
		}

		public static readonly SexyColor[] gElectColors = new SexyColor[]
		{
			new SexyColor(255, 255, 0),
			new SexyColor(255, 255, 255),
			new SexyColor(0, 128, 255),
			new SexyColor(255, 32, 32),
			new SexyColor(255, 0, 255),
			new SexyColor(255, 128, 0),
			new SexyColor(0, 255, 0),
			new SexyColor(0, 0, 0)
		};

		public BoardBejLive mBoard;

		public TPointFloat mStartPoint = default(TPointFloat);

		public TPointFloat mEndPoint = default(TPointFloat);

		public PieceBejLive mStartPiece;

		public CurvedVal mRainbowSize = CurvedVal.GetNewCurvedVal();

		public CurvedVal mRainbowAlpha = CurvedVal.GetNewCurvedVal();

		public List<TPointFloat>[] mPoints = new List<TPointFloat>[2];

		public float mPercentDone;

		public float mTimer;

		public float mDoneTime;

		public float mAngle;

		public float mLength;

		public SexyColor mColor = default(SexyColor);

		public int mUpdates;

		public int mFrame;

		public bool mDeleteMe;

		public bool mFlaming;

		private static Stack<LightningZap> unusedObjects = new Stack<LightningZap>();

		private TPointFloat[] aPoint = new TPointFloat[2];

		private static TriVertex[] aTriVertices = new TriVertex[4];

		private static TriVertex[,] aTri = new TriVertex[2, 3];
	}
}
