using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Misc
{
	public class CrystalBall : ButtonWidget
	{
		public bool IsInVisibleRange()
		{
			int num = this.GetAbsPos().mX + this.mWidth / 2;
			return num + ConstantsWP.CRYSTALBALL_STOP_DRAW_OFFSET >= 0 && num - ConstantsWP.CRYSTALBALL_STOP_DRAW_OFFSET <= GlobalMembers.gApp.mWidth;
		}

		public CrystalBall(string theLabel1, string theLabel2, string theLabel3, int theId, ButtonListener theListener, Color fontColour)
			: this(theLabel1, theLabel2, theLabel3, theId, theListener, fontColour, 1f)
		{
		}

		public CrystalBall(string theLabel1, string theLabel2, string theLabel3, int theId, ButtonListener theListener, Color fontColour, float scale)
			: base(theId, theListener)
		{
			this.mPressed = false;
			this.mOriginalCrystalScale = 1f;
			this.mRestartGame = true;
			this.mAlpha.SetConstant(1.0);
			Point point = new Point((int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL.GetCelWidth() * scale), (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL.GetCelHeight() * scale));
			this.mWidth = point.mX;
			this.mHeight = point.mY;
			this.mFlushPriority = -1;
			this.mScale.SetConstant((double)(scale * ConstantsWP.CRYSTALBALL_BASE_SCALE));
			this.mOriginalCrystalScale = (float)this.mScale;
			this.mOriginalX = this.mX;
			this.mOriginalY = this.mY;
			this.mZ = 0f;
			this.mClip = false;
			this.mLabel = theLabel1;
			this.mLabel2 = theLabel2;
			this.mLabel3 = theLabel3;
			this.mDoFinger = theLabel1.Length != 0;
			this.mTextAlpha = 1f;
			this.mMouseOverPct = 0f;
			this.mBaseAlpha = 0f;
			this.mColor = Color.White;
			this.mLocked = false;
			this.mDoBob = true;
			this.mUpdateCnt = 0;
			this.mTextIsQuestionMark = false;
			this.mHasCrest = false;
			this.mExtraFontScaling = 0f;
			this.mGlowEffect = GlobalMembersResourcesWP.PIEFFECT_CRYSTALBALL.Duplicate();
			this.mGlowEffect.mEmitAfterTimeline = true;
			this.mRayEffect = GlobalMembersResourcesWP.PIEFFECT_CRYSTALRAYS.Duplicate();
			this.mRayEffect.mEmitAfterTimeline = true;
			this.mFontColor = fontColour;
			if (GlobalMembers.gIs3D)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eCRYSTAL_BALL_X_BOB, this.mXBob);
				this.mXBob.SetMode(1);
				this.mXBob.mInitAppUpdateCount = Common.Rand() % 100;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eCRYSTAL_BALL_Y_BOB, this.mYBob);
				this.mYBob.SetMode(1);
				this.mYBob.mInitAppUpdateCount = Common.Rand() % 100;
			}
			this.mEffectStartCountdown = Common.Rand(200);
			this.mWidgetFlagsMod.mRemoveFlags = this.mWidgetFlagsMod.mRemoveFlags | 8;
			this.mAnimationFrameOffset = Common.Rand(200);
		}

		public override void Dispose()
		{
		}

		public void StartInGameTransition(bool restartGame)
		{
			this.mPressed = true;
			this.mRestartGame = restartGame;
			this.mOriginalX = this.mX;
			this.mOriginalY = this.mY;
		}

		public void ManageTransitionEffect()
		{
			if (this.mPressed)
			{
				if ((float)this.mScale < 1.5f)
				{
					this.mScale.SetConstant((double)((float)this.mScale + 0.02f));
					int num = ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X - this.mWidth / 2;
					int num2 = ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_Y - this.mHeight / 2;
					int num3 = (num - this.mX) / 50;
					int num4 = (num2 - this.mY) / 50;
					if ((float)this.mAlpha >= 0.02f)
					{
						this.mAlpha.SetConstant((double)((float)this.mAlpha - 0.01f));
					}
					this.Resize(this.mX + num3, this.mY + num4, this.mWidth, this.mHeight);
					return;
				}
				this.mAlpha.SetConstant(0.0);
				GlobalMembers.gApp.StartSetupGame(this.mRestartGame);
				this.mPressed = false;
				return;
			}
			else
			{
				if ((float)this.mScale > this.mOriginalCrystalScale)
				{
					this.mScale.SetConstant((double)((float)this.mScale - 0.01f));
					int num5 = this.mOriginalX;
					int num6 = this.mOriginalY;
					int num7 = (num5 - this.mX) / 3;
					int num8 = (num6 - this.mY) / 3;
					if ((float)this.mAlpha < 1f)
					{
						this.mAlpha.SetConstant((double)((float)this.mAlpha + 0.005f));
					}
					this.Resize(this.mX + num7, this.mY + num8, this.mWidth, this.mHeight);
					return;
				}
				if ((float)this.mScale < this.mOriginalCrystalScale)
				{
					this.mScale.SetConstant((double)this.mOriginalCrystalScale);
					this.Resize(this.mOriginalX, this.mOriginalY, this.mWidth, this.mHeight);
					this.mAlpha.SetConstant(1.0);
				}
				return;
			}
		}

		public override void Resize(int x, int y, int width, int height)
		{
			base.Resize(x, y, width, height);
			SexyTransform2D mDrawTransform = new SexyTransform2D(true);
			mDrawTransform.LoadIdentity();
			float num = (float)this.mScale;
			if (this.mZ > 0f)
			{
				num *= ModVal.M(0.00195f) / this.mZ;
			}
			mDrawTransform.Scale(num * GlobalMembers.MAGIC_SCALE * (1f + (float)this.mFullPct * ModVal.M(0.5f)), num * GlobalMembers.MAGIC_SCALE * (1f + (float)this.mFullPct * ModVal.M(0.2f)));
			this.mGlowEffect.mDrawTransform = mDrawTransform;
			this.mRayEffect.mDrawTransform = mDrawTransform;
		}

		public void DrawCrystal(Graphics g)
		{
			float num = (float)this.mAlpha;
			int num2 = (int)(num * 255f);
			this.mColor.mAlpha = num2;
			this.mTextAlpha = num;
			g.SetColor(new Color(255, 255, 255, num2));
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D != null && (this.mOffset.mX != 0f || this.mOffset.mY != 0f))
			{
				SexyTransform2D theTransform = new SexyTransform2D(true);
				theTransform.Translate(this.mOffset.mX, this.mOffset.mY);
				graphics3D.PushTransform(theTransform);
			}
			g.PushState();
			g.TranslateF((float)this.mWidth / 2f, (float)this.mHeight / 2f);
			if (!this.mLocked && this.mDoBob)
			{
				g.TranslateF((float)(GlobalMembers.S(this.mXBob) * (1.0 - this.mFullPct)), (float)(GlobalMembers.S(this.mYBob) * (1.0 - this.mFullPct)));
			}
			float num3 = (float)this.mScale;
			float num4 = num3;
			num4 += this.mMouseOverPct * ConstantsWP.CRYSTALBALL_HIGHLIGHT_SCALE_1;
			if (this.mZ > 0f)
			{
				num4 *= ModVal.M(0.00255f) / this.mZ;
			}
			float theNum = num4 * GlobalMembers.MAGIC_SCALE * (1f + (float)this.mFullPct * ModVal.M(0.5f));
			float theNum2 = num4 * GlobalMembers.MAGIC_SCALE * (1f + (float)this.mFullPct * ModVal.M(0.2f));
			float num5 = 1f - (float)this.mFullPct;
			int num6 = (int)Math.Max(0f, num5 * 255f);
			num6 = (int)((float)num6 * num);
			int theAlpha = (int)((float)(num2 * g.GetFinalColor().mAlpha) / 255f);
			this.mRayEffect.mColor = new Color(num6, num6, num6, theAlpha);
			this.mGlowEffect.mColor = new Color(num6, num6, num6, theAlpha);
			if (!this.mLocked)
			{
				this.mRayEffect.mDrawTransform.LoadIdentity();
				this.mRayEffect.mDrawTransform.Scale(GlobalMembers.S(theNum), GlobalMembers.S(theNum2));
				this.mRayEffect.Draw(g);
			}
			if (this.mLocked)
			{
				g.SetColor(new Color(this.mColor.mRed, this.mColor.mGreen, this.mColor.mBlue, (int)((float)this.mColor.mAlpha * ModVal.M(0.5f))));
			}
			else
			{
				g.SetColor(this.mColor);
			}
			g.SetColorizeImages(true);
			g.SetDrawMode(0);
			int theCel = (int)(this.mLocked ? ModVal.M(0f) : ((float)((this.mUpdateCnt + this.mAnimationFrameOffset) / 4 % 20)));
			num3 /= ConstantsWP.CRYSTALBALL_BASE_SCALE;
			num3 += this.mMouseOverPct * ConstantsWP.CRYSTALBALL_HIGHLIGHT_SCALE_2;
			g.SetDrawMode(1);
			int num7 = (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_SHADOW.GetCelWidth() * num3);
			int num8 = (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_SHADOW.GetCelHeight() * num3);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_SHADOW, -num7 / 2, -num8 / 2, num7, num8);
			g.SetDrawMode(0);
			num7 = (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL.GetCelWidth() * num3);
			num8 = (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL.GetCelHeight() * num3);
			g.DrawImageCel(GlobalMembersResourcesWP.IMAGE_CRYSTALBALL, new Rect(-num7 / 2, -num8 / 2, num7, num8), theCel);
			if (this.mIsDown)
			{
				g.SetDrawMode(1);
				num7 = (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_GLOW.GetCelWidth() * num3);
				num8 = (int)((float)GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_GLOW.GetCelHeight() * num3);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_GLOW, -num7 / 2, -num8 / 2, num7, num8);
				g.SetDrawMode(0);
			}
			if (!this.mLocked)
			{
				this.mGlowEffect.mDrawTransform.LoadIdentity();
				this.mGlowEffect.mDrawTransform.Scale(GlobalMembers.S(theNum), GlobalMembers.S(theNum2));
				this.mGlowEffect.Draw(g);
			}
			if (graphics3D != null && (this.mOffset.mX != 0f || this.mOffset.mY != 0f))
			{
				graphics3D.PopTransform();
			}
			g.PopState();
			if (this.mTextIsQuestionMark)
			{
				return;
			}
			if (this.mLabel.Length > 0)
			{
				g.SetFont(GlobalMembersResources.FONT_CRYSTALBALL);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_CRYSTALBALL, 0, this.mFontColor);
				g.SetColor(new Color(255, 255, 255, (int)(255f * this.mTextAlpha)));
				num4 = ConstantsWP.CRYSTALBALL_FONT_SCALE * this.mFontScale;
				Utils.PushScale(g, num4, num4, (float)(this.mWidth / 2), (float)(this.mHeight / 2));
				g.PushState();
				g.mClipRect.mX = g.mClipRect.mX - 1000;
				g.mClipRect.mWidth = g.mClipRect.mWidth + 2000;
				g.mClipRect.mY = g.mClipRect.mY - 1000;
				g.mClipRect.mHeight = g.mClipRect.mHeight + 2000;
				if ((float)this.mScale == this.mOriginalCrystalScale)
				{
					if (this.mLabel3.Length != 0)
					{
						g.DrawString(this.mLabel, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel) / 2, this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_3_1_Y);
						g.DrawString(this.mLabel2, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel2) / 2, this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_3_2_Y);
						g.DrawString(this.mLabel3, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel3) / 2, this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_3_3_Y);
					}
					else if (this.mLabel2.Length != 0)
					{
						g.DrawString(this.mLabel, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel) / 2, this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_2_1_Y);
						g.DrawString(this.mLabel2, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel2) / 2, this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_2_2_Y);
					}
					else
					{
						g.DrawString(this.mLabel, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel) / 2, this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_1_1_Y);
					}
				}
				g.PopState();
				Utils.PopScale(g);
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mParent != null && this.mWidth == 0)
			{
				return;
			}
			if (!this.IsInVisibleRange())
			{
				return;
			}
			if ((float)this.mScale > this.mOriginalCrystalScale)
			{
				base.DeferOverlay(2);
				return;
			}
			this.DrawCrystal(g);
		}

		public override void DrawOverlay(Graphics g)
		{
			this.DrawCrystal(g);
		}

		public override void Update()
		{
			base.WidgetUpdate();
			if (!this.mVisible)
			{
				return;
			}
			if (!this.IsInVisibleRange())
			{
				return;
			}
			this.mUpdateCnt++;
			if (this.mGlowEffect != null)
			{
				this.mGlowEffect.Update();
			}
			if (this.mEffectStartCountdown > 0)
			{
				this.mEffectStartCountdown--;
			}
			if (this.mEffectStartCountdown <= 0)
			{
				this.mRayEffect.Update();
			}
			if (this.mIsDown)
			{
				this.mMouseOverPct = Math.Min(1f, this.mMouseOverPct + 0.05f);
			}
			else
			{
				this.mMouseOverPct = Math.Max(0f, this.mMouseOverPct - 0.05f);
			}
			this.mAlpha.IncInVal();
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
		}

		public override void MouseUp(int theX, int theY, int theBtnNum, int theClickCount)
		{
			GlobalMembers.gApp.mLatestClickedBall = this;
			base.MouseUp(theX, theY, theBtnNum, theClickCount);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_MOUSEOVER);
		}

		public override void MouseDown(int theX, int theY, int theBtnNum, int theClickCount)
		{
			base.MouseDown(theX, theY, theBtnNum, theClickCount);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_PRESS);
		}

		public SexyVertex2D GetVertex(int theAngIdx, int theDistIdx)
		{
			float num = this.mDists[theDistIdx] * this.mDistMults[theAngIdx];
			float num2 = (float)GlobalMembers.S(1200) / 2f + (float)(GlobalMembers.S(1920) - GlobalMembers.S(1200)) / 2f * (float)this.mFullPct;
			float num3 = (float)GlobalMembers.S(1200) / 2f;
			float num4 = num2 / num3 / 1.6f;
			float num5 = this.mTexDists[theDistIdx] * this.mDistMults[theAngIdx];
			float num6 = this.mAlphas[theDistIdx];
			SexyVertex2D result = new SexyVertex2D(0f + this.mCoss[theAngIdx] * num2 * num, 0f + this.mSins[theAngIdx] * num3 * num, 0.5f + this.mCoss[theAngIdx] * 0.5f * num5 * num4, 0.5f + this.mSins[theAngIdx] * 0.5f * num5, (uint)new Color(255, 255, 255, (int)(Math.Max(0f, num6) * 255f)).ToInt());
			return result;
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
		}

		public void LinkUpAssets()
		{
		}

		private int mEffectStartCountdown;

		public CurvedVal mAlpha = new CurvedVal();

		public SharedImageRef mImage = new SharedImageRef();

		public Rect mImageSrcRect = default(Rect);

		public PIEffect mGlowEffect;

		public PIEffect mRayEffect;

		public CurvedVal mFullPct = new CurvedVal();

		public CurvedVal mScale = new CurvedVal();

		public FPoint mOffset = default(FPoint);

		public float mZ;

		public CurvedVal mXBob = new CurvedVal();

		public CurvedVal mYBob = new CurvedVal();

		public float mTextAlpha;

		public CurvedVal mLeftArrowPct = new CurvedVal();

		public CurvedVal mRightArrowPct = new CurvedVal();

		public float mMouseOverPct;

		public float mBaseAlpha;

		public Color mColor = default(Color);

		public Color mFontColor = default(Color);

		public float mExtraFontScaling;

		public new int mUpdateCnt;

		public int mFlushPriority;

		public bool mLocked;

		public bool mDoBob;

		public bool mTextIsQuestionMark;

		public bool mHasCrest;

		public float[] mDists = new float[GlobalMembers.NUM_DIST_POINTS];

		public float[] mTexDists = new float[GlobalMembers.NUM_DIST_POINTS];

		public float[] mAlphas = new float[GlobalMembers.NUM_DIST_POINTS];

		public float[] mDistMults = new float[GlobalMembers.NUM_RADIAL_POINTS + 1];

		public float[] mSins = new float[GlobalMembers.NUM_RADIAL_POINTS + 1];

		public float[] mCoss = new float[GlobalMembers.NUM_RADIAL_POINTS + 1];

		public string mLabel2 = string.Empty;

		public string mLabel3 = string.Empty;

		public int mAnimationFrameOffset;

		public bool mPressed;

		public float mOriginalCrystalScale;

		public int mOriginalX;

		public int mOriginalY;

		public bool mRestartGame;

		public float mFontScale = 1f;
	}
}
