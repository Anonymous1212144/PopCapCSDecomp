using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class Points : IDisposable
	{
		public Points(BejeweledLivePlusApp theApp, Font theFont, string theString, int theX, int theY, float theLife, int theJustification, Color theColor, int theAnim)
		{
			this.mLimitY = true;
			this.mDelay = 0;
			this.mColorCycling = false;
			for (int i = 0; i < this.mImage.Length; i++)
			{
				this.mImage[i] = null;
			}
			this.mRotation = 0f;
			this.mApp = theApp;
			this.mFont = (ImageFont)theFont;
			this.mTimer = theLife;
			this.mColor = theColor;
			this.mWobbleSin = 0f;
			this.mDoBounce = false;
			this.mWantCachedImage = false;
			this.mCachedImage = null;
			for (int j = 0; j < GlobalMembers.Max_LAYERS; j++)
			{
				this.mImage[j] = null;
			}
			if (theAnim < 3)
			{
				for (int k = 0; k < GlobalMembers.Max_LAYERS; k++)
				{
					this.mColorCycle[k] = new ColorCycle();
					this.mColorCycle[k].mCyclePos = Math.Abs(GlobalMembersUtils.GetRandFloat());
					this.mColorCycle[k].mCycleColors.Clear();
					this.mColorCycle[k].mColor = new Color(0, 0, 0, 0);
				}
			}
			for (int l = 0; l < GlobalMembers.Max_LAYERS; l++)
			{
				this.mColorCycle[l].SetSpeed(ModVal.M(1.8f));
			}
			Color color = this.mColor;
			color.mRed = (int)Math.Min(255f, (float)color.mRed * 1.5f);
			color.mGreen = (int)Math.Min(255f, (float)color.mGreen * 1.5f);
			color.mBlue = (int)Math.Min(255f, (float)color.mBlue * 1.5f);
			Color color2 = this.mColor;
			color2.mRed = (int)Math.Min(255f, (float)color2.mRed * 0.5f);
			color2.mGreen = (int)Math.Min(255f, (float)color2.mGreen * 0.5f);
			color2.mBlue = (int)Math.Min(255f, (float)color2.mBlue * 0.5f);
			switch (theAnim)
			{
			case 0:
				this.mColorCycle[0].mCycleColors.Add(color2);
				this.mColorCycle[0].mCycleColors.Add(color2);
				goto IL_359;
			case 1:
				this.mColorCycle[0].mCycleColors.Add(color2);
				this.mColorCycle[0].mCycleColors.Add(color);
				goto IL_359;
			case 2:
				this.mColorCycle[0].mCycleColors.Add(color2);
				this.mColorCycle[0].mCycleColors.Add(color);
				this.mColorCycle[0].SetPosition(0.25f);
				this.mColorCycle[2].mCycleColors.Add(color2);
				this.mColorCycle[2].mCycleColors.Add(color);
				this.mColorCycle[2].SetPosition(0.75f);
				goto IL_359;
			}
			this.mColorCycle[0].SetPosition(0.25f);
			this.mColorCycle[2].SetPosition(0.5f);
			IL_359:
			this.mString = theString;
			this.RestartWobble();
			this.mX = (float)theX;
			this.mY = (float)theY;
			this.mScale = 0f;
			this.mScaleAdd = 0f;
			this.mDY = GlobalMembers.MS(1.2f);
			this.mUpdateCnt = 0;
			this.mSubStringShowTick = -1;
			this.mSubFont = null;
			this.mId = uint.MaxValue;
			this.mMoveCreditId = -1;
			this.mState = 0;
			this.mAlpha = 1f;
			this.mLayerCount = this.mFont.GetLayerCount();
			this.mDrawn = false;
			this.mDeleteMe = false;
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.mImage[i] != null)
				{
					this.mImage[i].Dispose();
				}
			}
			if (this.mCachedImage != null)
			{
				this.mCachedImage.Dispose();
			}
		}

		public virtual void Update()
		{
			this.mUpdateCnt++;
			int num = this.mFont.StringWidth(this.mString);
			if (this.mX + (float)(num / 2) * ModVal.M(1.5f) > 1920f)
			{
				this.mX = 1920f - (float)(num / 2) * ModVal.M(1.5f);
			}
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			this.mWobbleSin += ModVal.M(0.03f) * GlobalMembers.M_PI * 2f;
			if (this.mWobbleSin > GlobalMembers.M_PI * 2f)
			{
				this.mWobbleSin -= GlobalMembers.M_PI * 2f;
			}
			this.mWobbleScale -= ModVal.M(0.005f);
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
				if (this.mScale < this.mDestScale && !this.mDoBounce)
				{
					this.mScale = this.mDestScale;
				}
			}
			else if (this.mState != 2)
			{
				this.mAlpha -= ModVal.M(0.05f);
				this.mScale -= ModVal.M(0.03f);
				if (this.mScale <= 0f || this.mAlpha <= 0f)
				{
					this.mDeleteMe = true;
				}
			}
			float num3 = (float)Math.Pow((double)Math.Min(this.mY / ModVal.M(50f), 1f), (double)ModVal.M(0.015f));
			this.mDY *= num3;
			this.mY -= this.mDY;
			if (this.mLimitY)
			{
				this.mY = Math.Max(ModVal.M(75f), this.mY);
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
			float num = (float)((double)this.mScale + Math.Sin((double)this.mWobbleSin) * (double)this.mWobbleScale);
			if (!this.mDoBounce)
			{
				num = this.mScale;
			}
			if (this.mWantCachedImage)
			{
				this.UpdateCachedImage();
			}
			if (this.mWantCachedImage && this.mCachedImage != null)
			{
				Transform transform = new Transform();
				transform.Scale(num, num);
				transform.RotateRad(this.mRotation);
				g.DrawImageTransform(this.mCachedImage, transform, GlobalMembers.S(this.mX), GlobalMembers.S(this.mY));
			}
			else
			{
				if (this.mRotation != 0f && GlobalMembers.gIs3D)
				{
					SexyTransform2D theTransform = new SexyTransform2D(true);
					theTransform.Translate(-GlobalMembers.S(g.mTransX + this.mX), -GlobalMembers.S(g.mTransY + this.mY));
					theTransform.RotateRad(this.mRotation);
					theTransform.Translate(GlobalMembers.S(g.mTransX + this.mX), GlobalMembers.S(g.mTransY + this.mY));
					g.Get3D().PushTransform(theTransform);
				}
				this.SetupForDraw(g);
				int num2 = this.mFont.StringWidth(this.mString);
				int ascent = this.mFont.GetAscent();
				if (GlobalMembers.gIs3D)
				{
					g.mClipRect.mWidth = g.mClipRect.mWidth + 1000;
					g.mClipRect.mX = g.mClipRect.mX - 500;
				}
				float num3 = num;
				Utils.PushScale(g, num3, num3, GlobalMembers.S(this.mX), GlobalMembers.S(this.mY));
				int num4 = 0;
				int num5 = (int)(GlobalMembers.S(this.mY) + (float)ascent * ModVal.M(0.2f));
				if (this.mX != 0f)
				{
					num4 = (int)(GlobalMembers.S(this.mX) - (float)(num2 / 2));
					if ((float)num4 < ConstantsWP.POINTS_LIMIT)
					{
						this.mX = GlobalMembers.RS(ConstantsWP.POINTS_LIMIT + (float)(num2 / 2));
						num4 = (int)ConstantsWP.POINTS_LIMIT;
					}
					if ((float)num4 > (float)GlobalMembers.gApp.mWidth - ConstantsWP.POINTS_LIMIT - (float)num2)
					{
						this.mX = GlobalMembers.RS((float)GlobalMembers.gApp.mWidth - ConstantsWP.POINTS_LIMIT - (float)num2 + (float)(num2 / 2));
						num4 = (int)((float)GlobalMembers.gApp.mWidth - ConstantsWP.POINTS_LIMIT - (float)num2);
					}
				}
				if ((float)num5 < ConstantsWP.POINTS_LIMIT_TOP)
				{
					this.mY = ConstantsWP.POINTS_LIMIT_TOP - (float)ascent * ModVal.M(0.2f);
					num5 = (int)ConstantsWP.POINTS_LIMIT_TOP;
				}
				g.DrawString(this.mString, num4, num5);
				Utils.PopScale(g);
				int fontLayerCount = Utils.GetFontLayerCount(this.mFont);
				for (int i = 0; i < fontLayerCount; i++)
				{
					Utils.SetFontLayerColor(this.mFont, i, Color.White);
				}
				if (this.mRotation != 0f && GlobalMembers.gIs3D)
				{
					g.Get3D().PopTransform();
				}
			}
			if (this.mSubStringShowTick >= 0 && this.mSubStringShowTick <= this.mUpdateCnt && this.mSubString.Length != 0 && this.mSubFont != null)
			{
				int num6 = this.mSubFont.StringWidth(this.mSubString);
				int ascent2 = this.mSubFont.GetAscent();
				g.SetFont(this.mSubFont);
				int theNum = (int)(this.mY + ModVal.M(-80f));
				double num7 = (double)((float)(this.mUpdateCnt - this.mSubStringShowTick) / ModVal.M(20f));
				if (num7 < 1.0)
				{
					num = (float)((double)ModVal.M(1f) + (double)ModVal.M(0.5f) * (1.0 - num7));
				}
				g.SetScale(num, num, GlobalMembers.S(this.mX), (float)GlobalMembers.S(theNum));
				double num8 = Math.Min((double)this.mAlpha, num7);
				int theLayer = ((ModVal.M(1f) < 0f || ModVal.M(1f) > -0f) ? (Utils.GetFontLayerCount(this.mSubFont) - 1) : 0);
				g.mColor.mAlpha = (int)(255.0 * num8);
				g.SetColorizeImages(num8 < 1.0);
				this.mSubFont.PushLayerColor(theLayer, this.mSubColor);
				g.DrawString(this.mSubString, (int)(GlobalMembers.S(this.mX) - (float)(num6 / 2)), (int)((float)GlobalMembers.S(theNum) + (float)ascent2 * ModVal.M(0.2f)));
				this.mSubFont.PopLayerColor(theLayer);
			}
			if (!flag)
			{
				g.SetFastStretch(false);
			}
			g.SetColorizeImages(false);
			this.mDrawn = true;
		}

		public void RestartWobble()
		{
			this.mWobbleScale = ModVal.M(0.3f);
		}

		public void StartFading()
		{
			this.mState = 1;
		}

		public void SetupForDraw(Graphics g)
		{
			bool flag = this.mApp.Is3DAccelerated();
			g.SetColorizeImages(true);
			g.SetDrawMode(0);
			g.SetFont(this.mFont);
			Color color = default(Color);
			int num = 0;
			color = this.mColor;
			color.mRed = Math.Max(0, Math.Min(255, (int)((float)color.mRed * 0.25f)));
			color.mGreen = Math.Max(0, Math.Min(255, (int)((float)color.mGreen * 0.25f)));
			color.mBlue = Math.Max(0, Math.Min(255, (int)((float)color.mBlue * 0.25f)));
			color.mAlpha = Math.Min(255, (int)(this.mAlpha * 255f));
			g.SetColor(color);
			if (!flag)
			{
				g.SetFastStretch(true);
			}
			color = this.mColor;
			color.mRed = Math.Max(0, Math.Min(255, color.mRed + num));
			color.mGreen = Math.Max(0, Math.Min(255, color.mGreen + num));
			color.mBlue = Math.Max(0, Math.Min(255, color.mBlue + num));
			color.mAlpha = Math.Min(255, (int)(this.mAlpha * 255f));
			g.SetColor(new Color(255, 255, 255, (int)(this.mAlpha * 255f)));
			if (this.mFont == GlobalMembersResources.FONT_FLOATERS)
			{
				for (int i = 0; i < Utils.GetFontLayerCount(this.mFont); i++)
				{
					if (i != this.mLayerCount - 1)
					{
						this.mColorCycle[i].GetColor();
					}
					color.mAlpha *= (int)this.mAlpha;
					Utils.SetFontLayerColor(this.mFont, i, color);
				}
			}
			g.SetColor(Color.White);
		}

		public void UpdateCachedImage()
		{
			int num = this.mFont.StringWidth(this.mString);
			int ascent = this.mFont.GetAscent();
			if (this.mCachedImage == null)
			{
				this.mCachedImage = new MemoryImage();
				this.mCachedImage.Create((int)((float)num * ModVal.M(1.1f)), (int)((float)ascent * ModVal.M(1.1f)));
				this.mCachedImage.mIsVolatile = true;
				this.mCachedImage.SetImageMode(true, true);
			}
			Graphics graphics = new Graphics(this.mCachedImage);
			this.SetupForDraw(graphics);
			graphics.WriteString(this.mString, this.mCachedImage.mWidth / 2, this.mCachedImage.mHeight / 2 + GlobalMembers.MS(24));
		}

		public float mX;

		public float mY;

		public float mDY;

		public bool mLimitY;

		public int mState;

		public int mScalePoints;

		public int mCorrectedPoints;

		public int mWidth;

		public int mHeight;

		public BejeweledLivePlusApp mApp;

		public float mTimer;

		public string mString = string.Empty;

		public ImageFont mFont;

		public int mHue;

		public MemoryImage[] mImage = new MemoryImage[GlobalMembers.Max_LAYERS];

		public string mSubString = string.Empty;

		public int mSubStringShowTick;

		public ImageFont mSubFont;

		public Color mSubColor = default(Color);

		public int mLayerCount;

		public int mDelay;

		public float mScale;

		public float mDestScale;

		public float mScaleAdd;

		public float mScaleDampening;

		public float mScaleDifMult;

		public ColorCycle[] mColorCycle = new ColorCycle[GlobalMembers.Max_LAYERS];

		public bool mColorCycling;

		public int mUpdateCnt;

		public Color mColor = default(Color);

		public float mWobbleSin;

		public float mWobbleScale;

		public bool mDoBounce;

		public float mAlpha;

		public bool mWantCachedImage;

		public MemoryImage mCachedImage;

		public float mRotation;

		public uint mId;

		public int mMoveCreditId;

		public uint mValue;

		public bool mDrawn;

		public bool mDeleteMe;

		public enum POINTSSTATE
		{
			STATE_RISING,
			STATE_FADING,
			STATE_VERT_SHIFTING
		}
	}
}
