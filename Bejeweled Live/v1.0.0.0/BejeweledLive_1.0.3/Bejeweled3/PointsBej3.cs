using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class PointsBej3
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 40; i++)
			{
				new PointsBej3().PrepareForReuse();
			}
		}

		private PointsBej3()
		{
		}

		public static PointsBej3 GetNewPointsBej3(GameApp theApp, Font theFont, string theString, int theX, int theY, float theLife, int theJustification, SexyColor theColor, int theAnim)
		{
			if (PointsBej3.unusedObjects.Count > 0)
			{
				PointsBej3 pointsBej = PointsBej3.unusedObjects.Pop();
				pointsBej.Reset(theApp, theFont, theString, theX, theY, theLife, theJustification, theColor, theAnim);
				return pointsBej;
			}
			return new PointsBej3(theApp, theFont, theString, theX, theY, theLife, theJustification, theColor, theAnim);
		}

		public void PrepareForReuse()
		{
			PointsBej3.unusedObjects.Push(this);
		}

		private void Reset(GameApp theApp, Font theFont, string theString, int theX, int theY, float theLife, int theJustification, SexyColor theColor, int theAnim)
		{
			for (int i = 0; i < this.mColorCycle.Length; i++)
			{
				if (this.mColorCycle[i] != null)
				{
					this.mColorCycle[i].PrepareForReuse();
				}
			}
			for (int j = 0; j < this.mColorCycle.Length; j++)
			{
				this.mColorCycle[j] = ColorCycle.GetNewColorCycle();
			}
			this.mLimitY = true;
			this.mDelay = 0;
			this.mColorCycling = false;
			this.mApp = theApp;
			this.mFont = theFont;
			this.mTimer = theLife;
			this.mColor = theColor;
			this.mWobbleSin = 0f;
			for (int k = 0; k < 5; k++)
			{
				this.mImage[k] = null;
			}
			if (theAnim < 3)
			{
				for (int l = 0; l < 5; l++)
				{
					this.mColorCycle[l].mCyclePos = Math.Abs(Board.GetRandFloat());
					this.mColorCycle[l].mCycleColors.Clear();
					this.mColorCycle[l].mColor = new SexyColor(0, 0, 0, 0);
				}
			}
			for (int m = 0; m < 5; m++)
			{
				this.mColorCycle[m].SetSpeed(Constants.mConstants.M(1.8f));
			}
			SexyColor sexyColor = this.mColor;
			sexyColor.mRed = (int)Math.Min(255f, (float)sexyColor.mRed * 1.5f);
			sexyColor.mGreen = (int)Math.Min(255f, (float)sexyColor.mGreen * 1.5f);
			sexyColor.mBlue = (int)Math.Min(255f, (float)sexyColor.mBlue * 1.5f);
			SexyColor sexyColor2 = this.mColor;
			sexyColor2.mRed = (int)Math.Min(255f, (float)sexyColor2.mRed * 0.5f);
			sexyColor2.mGreen = (int)Math.Min(255f, (float)sexyColor2.mGreen * 0.5f);
			sexyColor2.mBlue = (int)Math.Min(255f, (float)sexyColor2.mBlue * 0.5f);
			switch (theAnim)
			{
			case 0:
				this.mColorCycle[1].mCycleColors.Add(sexyColor2);
				this.mColorCycle[1].mCycleColors.Add(sexyColor2);
				goto IL_2FE;
			case 1:
				this.mColorCycle[1].mCycleColors.Add(sexyColor2);
				this.mColorCycle[1].mCycleColors.Add(sexyColor);
				goto IL_2FE;
			case 2:
				this.mColorCycle[1].mCycleColors.Add(sexyColor2);
				this.mColorCycle[1].mCycleColors.Add(sexyColor);
				this.mColorCycle[1].SetPosition(0.25f);
				this.mColorCycle[2].mCycleColors.Add(sexyColor2);
				this.mColorCycle[2].mCycleColors.Add(sexyColor);
				this.mColorCycle[2].SetPosition(0.75f);
				goto IL_2FE;
			}
			this.mColorCycle[1].SetPosition(0.25f);
			this.mColorCycle[2].SetPosition(0.5f);
			IL_2FE:
			this.mString = theString;
			this.RestartWobble();
			float num = 1f;
			int num2 = this.mFont.StringWidth(this.mString);
			int num3 = theX;
			if (theJustification == 0)
			{
				num3 -= (int)((float)num2 / num / 2f);
			}
			else if (theJustification == 1)
			{
				num3 -= (int)((float)num2 / num);
			}
			this.mHue = 0;
			if (num3 + this.mWidth > GlobalStaticVars.gSexyAppBase.mWidth)
			{
				num3 = GlobalStaticVars.gSexyAppBase.mWidth - this.mWidth;
			}
			this.mX = (float)theX;
			this.mY = (float)theY;
			this.mScale = 0f;
			this.mScaleAdd = 0f;
			this.mDY = Constants.mConstants.MS(1.2f);
			this.mUpdateCnt = 0;
			this.mId = uint.MaxValue;
			this.mMoveCreditId = -1;
			this.mState = 0;
			this.mAlpha = 1f;
			this.mLayerCount = 3;
			this.mDrawn = false;
			this.mDeleteMe = false;
			this.mfFade = 1f;
		}

		private PointsBej3(GameApp theApp, Font theFont, string theString, int theX, int theY, float theLife, int theJustification, SexyColor theColor, int theAnim)
		{
			this.Reset(theApp, theFont, theString, theX, theY, theLife, theJustification, theColor, theAnim);
		}

		public virtual void Update()
		{
			this.mUpdateCnt++;
			int num = this.mFont.StringWidth(this.mString);
			if (this.mX + (float)(num / 2) * Constants.mConstants.M(1.5f) > 1920f)
			{
				this.mX = 1920f - (float)(num / 2) * Constants.mConstants.M(1.5f);
			}
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			this.mWobbleSin += Constants.mConstants.M(0.03f) * 6.28318548f;
			if (this.mWobbleSin > 6.28318548f)
			{
				this.mWobbleSin -= 6.28318548f;
			}
			this.mWobbleScale -= Constants.mConstants.M(0.005f);
			if (this.mWobbleScale < 0f)
			{
				this.mWobbleScale = 0f;
			}
			for (int i = 0; i < 3; i++)
			{
				this.mColorCycle[i].Update();
			}
			float num2 = this.mDestScale - this.mScale;
			if (this.mState == 0)
			{
				this.mScaleAdd += num2 * this.mScaleDifMult;
				this.mScaleAdd *= this.mScaleDampening;
				this.mScale += this.mScaleAdd;
				if (this.mScale < this.mDestScale)
				{
					this.mScale = this.mDestScale;
				}
			}
			else
			{
				this.mAlpha -= Constants.mConstants.M(0.05f);
				this.mScale -= Constants.mConstants.M(0.03f);
				if (this.mScale <= 0f || this.mAlpha <= 0f)
				{
					this.mDeleteMe = true;
				}
			}
			float num3 = (float)Math.Pow((double)Math.Min(this.mY / Constants.mConstants.M(50f), 1f), (double)Constants.mConstants.M(0.015f));
			this.mDY *= num3;
			this.mY -= this.mDY;
			if (this.mLimitY)
			{
				this.mY = Math.Max((float)Constants.mConstants.PointsBej3_Min_Y, this.mY);
			}
			this.mTimer -= 0.01f;
			if (this.mTimer <= 0f && this.mState == 0)
			{
				this.StartFading();
			}
		}

		public virtual void Draw(Graphics g)
		{
			if (this.mDelay > 0)
			{
				return;
			}
			bool flag = this.mApp.Is3DAccelerated();
			g.SetColorizeImages(true);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetFont(this.mFont);
			int num = 0;
			SexyColor aColor = this.mColor;
			aColor.mRed = Math.Max(0, Math.Min(255, (int)((float)aColor.mRed * 0.25f)));
			aColor.mGreen = Math.Max(0, Math.Min(255, (int)((float)aColor.mGreen * 0.25f)));
			aColor.mBlue = Math.Max(0, Math.Min(255, (int)((float)aColor.mBlue * 0.25f)));
			aColor.mAlpha = (int)((float)Math.Min(255, (int)(this.mAlpha * 255f)) * this.mfFade);
			g.SetColor(aColor);
			if (!flag)
			{
				g.SetFastStretch(true);
			}
			aColor = this.mColor;
			aColor.mRed = Math.Max(0, Math.Min(255, aColor.mRed + num));
			aColor.mGreen = Math.Max(0, Math.Min(255, aColor.mGreen + num));
			aColor.mBlue = Math.Max(0, Math.Min(255, aColor.mBlue + num));
			aColor.mAlpha = (int)((float)Math.Min(255, (int)(this.mAlpha * 255f)) * this.mfFade);
			g.SetColor(new SexyColor(255, 255, 255, (int)(this.mAlpha * this.mfFade * 255f)));
			g.SetScale(this.mScale);
			int num2 = this.mFont.StringWidth(this.mString);
			int ascent = this.mFont.GetAscent();
			g.SetColor(new Color(this.mColor.mRed, this.mColor.mGreen, this.mColor.mBlue, (int)((float)this.mColor.mAlpha * this.mfFade * this.mAlpha)));
			g.DrawString(this.mString, (int)(this.mX - (float)(num2 / 2)), (int)(this.mY + (float)ascent * Constants.mConstants.S(0.2f)));
			if (!flag)
			{
				g.SetFastStretch(false);
			}
			g.SetColorizeImages(false);
			this.mDrawn = true;
			g.SetScale(1f);
		}

		public void RestartWobble()
		{
			this.mWobbleScale = Constants.mConstants.M(0.3f);
		}

		public void StartFading()
		{
			this.mState = 1;
		}

		public const int MAX_LAYERS = 5;

		public float mX;

		public float mY;

		public float mDY;

		public bool mLimitY;

		public int mState;

		public int mScalePoints;

		public int mCorrectedPoints;

		public int mWidth;

		public int mHeight;

		public GameApp mApp;

		public float mTimer;

		public string mString = string.Empty;

		public Font mFont;

		public int mHue;

		public MemoryImage[] mImage = new MemoryImage[5];

		public int mLayerCount;

		public int mDelay;

		public float mScale;

		public float mDestScale;

		public float mScaleAdd;

		public float mScaleDampening;

		public float mScaleDifMult;

		public ColorCycle[] mColorCycle = new ColorCycle[5];

		public bool mColorCycling;

		public int mUpdateCnt;

		public SexyColor mColor = default(SexyColor);

		public float mWobbleSin;

		public float mWobbleScale;

		public float mAlpha;

		public uint mId;

		public int mMoveCreditId;

		public uint mValue;

		public bool mDrawn;

		public bool mDeleteMe;

		public float mfFade;

		private static Stack<PointsBej3> unusedObjects = new Stack<PointsBej3>();

		public enum PointState
		{
			STATE_RISING,
			STATE_FADING
		}
	}
}
