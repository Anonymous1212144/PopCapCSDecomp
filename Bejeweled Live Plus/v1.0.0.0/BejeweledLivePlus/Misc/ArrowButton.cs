using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Misc
{
	internal class ArrowButton : CrystalBall
	{
		public ArrowButton(string theLabel1, string theLabel2, string theLabel3, int theId, ButtonListener theListener, Color fontColour, float scale)
			: base(theLabel1, theLabel2, theLabel3, theId, theListener, fontColour, scale)
		{
			this.IsLeft = false;
		}

		public override void Draw(Graphics g)
		{
			if (this.mParent != null && this.mWidth == 0)
			{
				return;
			}
			if (!base.IsInVisibleRange())
			{
				return;
			}
			if ((float)this.mScale > this.mOriginalCrystalScale)
			{
				base.DeferOverlay(2);
				return;
			}
			this.DrawArrow(g);
		}

		public override void DrawOverlay(Graphics g)
		{
			this.DrawArrow(g);
		}

		public void DrawArrow(Graphics g)
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
			int num7 = (int)(this.mLocked ? ModVal.M(0f) : ((float)((this.mUpdateCnt + this.mAnimationFrameOffset) / 10 % 25)));
			num3 /= ConstantsWP.CRYSTALBALL_BASE_SCALE;
			num3 += this.mMouseOverPct * ConstantsWP.CRYSTALBALL_HIGHLIGHT_SCALE_2;
			g.SetDrawMode(0);
			SexyVertex2D[] array = new SexyVertex2D[4];
			if (!this.mIsDown)
			{
				if (!this.IsLeft)
				{
					array[0].x = (float)this.mX;
					array[0].y = (float)(this.mY + this.mHeight);
					array[0].u = 0f;
					array[0].v = 1f;
					array[1].x = (float)this.mX;
					array[1].y = (float)this.mY;
					array[1].u = 0f;
					array[1].v = 0f;
					array[2].x = (float)(this.mX + this.mWidth);
					array[2].y = (float)(this.mY + this.mHeight);
					array[2].u = 1f;
					array[2].v = 1f;
					array[3].x = (float)(this.mX + this.mWidth);
					array[3].y = (float)this.mY;
					array[3].u = 1f;
					array[3].v = 0f;
					g.DrawTrianglesTexStrip(this.Imag[(num7 >= 10) ? 0 : num7], array, 2);
				}
				else
				{
					array[0].x = (float)this.mX;
					array[0].y = (float)(this.mY + this.mHeight);
					array[0].u = 1f;
					array[0].v = 0f;
					array[1].x = (float)this.mX;
					array[1].y = (float)this.mY;
					array[1].u = 1f;
					array[1].v = 1f;
					array[2].x = (float)(this.mX + this.mWidth);
					array[2].y = (float)(this.mY + this.mHeight);
					array[2].u = 0f;
					array[2].v = 0f;
					array[3].x = (float)(this.mX + this.mWidth);
					array[3].y = (float)this.mY;
					array[3].u = 0f;
					array[3].v = 1f;
					g.DrawTrianglesTexStrip(this.Imag[(num7 >= 10) ? 0 : (10 - num7)], array, 2);
				}
			}
			else if (!this.IsLeft)
			{
				array[0].x = (float)(this.mX + 5);
				array[0].y = (float)(this.mY + this.mHeight + 5);
				array[0].u = 0f;
				array[0].v = 1f;
				array[1].x = (float)(this.mX + 5);
				array[1].y = (float)(this.mY + 5);
				array[1].u = 0f;
				array[1].v = 0f;
				array[2].x = (float)(this.mX + this.mWidth + 5);
				array[2].y = (float)(this.mY + this.mHeight + 5);
				array[2].u = 1f;
				array[2].v = 1f;
				array[3].x = (float)(this.mX + this.mWidth + 5);
				array[3].y = (float)(this.mY + 5);
				array[3].u = 1f;
				array[3].v = 0f;
				g.DrawTrianglesTexStrip(this.Imag[(num7 >= 10) ? 0 : num7], array, 2);
			}
			else
			{
				array[0].x = (float)(this.mX + 5);
				array[0].y = (float)(this.mY + this.mHeight + 5);
				array[0].u = 1f;
				array[0].v = 0f;
				array[1].x = (float)(this.mX + 5);
				array[1].y = (float)(this.mY + 5);
				array[1].u = 1f;
				array[1].v = 1f;
				array[2].x = (float)(this.mX + this.mWidth + 5);
				array[2].y = (float)(this.mY + this.mHeight + 5);
				array[2].u = 0f;
				array[2].v = 0f;
				array[3].x = (float)(this.mX + this.mWidth + 5);
				array[3].y = (float)(this.mY + 5);
				array[3].u = 0f;
				array[3].v = 1f;
				g.DrawTrianglesTexStrip(this.Imag[(num7 >= 10) ? 0 : (10 - num7)], array, 2);
			}
			g.SetDrawMode(Graphics.DrawMode.Additive);
			if (this.mIsDown)
			{
				if (!this.IsLeft)
				{
					array[0].x = (float)(this.mX + 5);
					array[0].y = (float)(this.mY + this.mHeight + 5);
					array[0].u = 0f;
					array[0].v = 1f;
					array[1].x = (float)(this.mX + 5);
					array[1].y = (float)(this.mY + 5);
					array[1].u = 0f;
					array[1].v = 0f;
					array[2].x = (float)(this.mX + this.mWidth + 5);
					array[2].y = (float)(this.mY + this.mHeight + 5);
					array[2].u = 1f;
					array[2].v = 1f;
					array[3].x = (float)(this.mX + this.mWidth + 5);
					array[3].y = (float)(this.mY + 5);
					array[3].u = 1f;
					array[3].v = 0f;
					g.DrawTrianglesTexStrip(GlobalMembersResourcesWP.IMAGE_ARROW_GLOW, array, 2);
				}
				else
				{
					array[0].x = (float)(this.mX + 5);
					array[0].y = (float)(this.mY + this.mHeight + 5);
					array[0].u = 1f;
					array[0].v = 0f;
					array[1].x = (float)(this.mX + 5);
					array[1].y = (float)(this.mY + 5);
					array[1].u = 1f;
					array[1].v = 1f;
					array[2].x = (float)(this.mX + this.mWidth + 5);
					array[2].y = (float)(this.mY + this.mHeight + 5);
					array[2].u = 0f;
					array[2].v = 0f;
					array[3].x = (float)(this.mX + this.mWidth + 5);
					array[3].y = (float)(this.mY + 5);
					array[3].u = 0f;
					array[3].v = 1f;
					g.DrawTrianglesTexStrip(GlobalMembersResourcesWP.IMAGE_ARROW_GLOW, array, 2);
				}
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
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
				num4 = ConstantsWP.CRYSTALBALL_FONT_SCALE;
				Utils.PushScale(g, num4, num4, (float)(this.mWidth / 2), (float)(this.mHeight / 2));
				g.PushState();
				g.mClipRect.mX = g.mClipRect.mX - 1000;
				g.mClipRect.mWidth = g.mClipRect.mWidth + 2000;
				g.mClipRect.mY = g.mClipRect.mY - 1000;
				g.mClipRect.mHeight = g.mClipRect.mHeight + 2000;
				if ((float)this.mScale == this.mOriginalCrystalScale)
				{
					int num8 = (this.IsLeft ? 20 : 0);
					if (this.mLabel3.Length != 0)
					{
						g.DrawString(this.mLabel, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel) / 2 + num8 + (this.mIsDown ? 5 : 0), this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_3_1_Y + (this.mIsDown ? 5 : 0));
						g.DrawString(this.mLabel2, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel2) / 2 + num8 + (this.mIsDown ? 5 : 0), this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_3_2_Y + (this.mIsDown ? 5 : 0));
						g.DrawString(this.mLabel3, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel3) / 2 + num8 + (this.mIsDown ? 5 : 0), this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_3_3_Y + (this.mIsDown ? 5 : 0));
					}
					else if (this.mLabel2.Length != 0)
					{
						g.DrawString(this.mLabel, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel) / 2 + num8 + (this.mIsDown ? 5 : 0), this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_2_1_Y + (this.mIsDown ? 5 : 0));
						g.DrawString(this.mLabel2, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel2) / 2 + num8 + (this.mIsDown ? 5 : 0), this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_2_2_Y + (this.mIsDown ? 5 : 0));
					}
					else
					{
						g.DrawString(this.mLabel, this.mWidth / 2 - GlobalMembersResources.FONT_CRYSTALBALL.StringWidth(this.mLabel) / 2 + num8 + (this.mIsDown ? 5 : 0), this.mHeight / 2 + ConstantsWP.CRYSTALBALL_TEXT_1_1_Y + (this.mIsDown ? 5 : 0));
					}
				}
				g.PopState();
				Utils.PopScale(g);
			}
		}

		public bool IsLeft;

		private Image[] Imag = new Image[]
		{
			GlobalMembersResourcesWP.IMAGE_ARROW_01,
			GlobalMembersResourcesWP.IMAGE_ARROW_02,
			GlobalMembersResourcesWP.IMAGE_ARROW_03,
			GlobalMembersResourcesWP.IMAGE_ARROW_04,
			GlobalMembersResourcesWP.IMAGE_ARROW_05,
			GlobalMembersResourcesWP.IMAGE_ARROW_06,
			GlobalMembersResourcesWP.IMAGE_ARROW_07,
			GlobalMembersResourcesWP.IMAGE_ARROW_08,
			GlobalMembersResourcesWP.IMAGE_ARROW_09,
			GlobalMembersResourcesWP.IMAGE_ARROW_10,
			GlobalMembersResourcesWP.IMAGE_ARROW_01
		};
	}
}
