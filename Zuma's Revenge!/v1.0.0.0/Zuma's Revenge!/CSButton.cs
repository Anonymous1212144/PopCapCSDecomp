using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class CSButton : ButtonWidget
	{
		public CSButton(int id, ChallengeMenu theChallengeMenu, ButtonListener listener)
			: base(id, listener)
		{
			this.mChallengeMenu = theChallengeMenu;
		}

		public override void Dispose()
		{
			if (this.mUnlockSparkles != null)
			{
				this.mUnlockSparkles.Dispose();
				this.mUnlockSparkles = null;
			}
		}

		public override void Draw(Graphics g)
		{
			if (g.mClipRect.mWidth <= 0 || g.mClipRect.mHeight <= 0)
			{
				return;
			}
			CSButton.last_uc = this.mUpdateCnt;
			bool flag = this.mIsDown && this.mIsOver && !this.mDisabled;
			flag ^= this.mInverted;
			bool flag2 = this.mId - 3 + 1 == GameApp.gLastLevel && this.mChallengeMenu.mCrownZoomType >= 0;
			int num = (flag ? Common._DS(Common._M(0)) : 0);
			int num2 = (flag ? Common._DS(Common._M(0)) : 0);
			Image image = null;
			if (this.mLevel != -1)
			{
				image = GameApp.gApp.GetLevelThumbnail(this.mLevel);
			}
			if (image != null)
			{
				g.DrawImage(image, GlobalChallenge.gScreenShake + num, GlobalChallenge.gScreenShake + num2, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
				if (this.mMouseOver)
				{
					g.PushState();
					g.SetColor(new Color(255, 255, 255, Common._M(100)));
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.DrawImage(image, GlobalChallenge.gScreenShake + num, GlobalChallenge.gScreenShake + num2, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
					g.PopState();
				}
				if (flag)
				{
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_CH_THUMBNAILOVERLAY), GlobalChallenge.gScreenShake, GlobalChallenge.gScreenShake, Common._DS(GlobalChallenge.CS_BTN_WIDTH + Common._M(0)), Common._DS(GlobalChallenge.CS_BTN_HEIGHT + Common._M1(0)));
				}
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_CS_LOCK_ANIMATION);
			if (this.mOpaque)
			{
				g.SetColor(new Color(0, 0, 0, Common._M(191)));
				g.FillRect(0, 0, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
			}
			else if (this.mMedal == imageByID)
			{
				g.SetColor(new Color(0, 0, 0, 120));
				g.FillRect(0, 0, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
			}
			Common.DrawCommonDialogBorder(g, GlobalChallenge.gScreenShake - Common._DS(15), GlobalChallenge.gScreenShake - Common._DS(15), this.mWidth + Common._DS(30), this.mHeight + Common._DS(30));
			if (this.mUnlockAlpha > 0)
			{
				Image image2 = imageByID;
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, this.mUnlockAlpha));
				g.DrawImageCel(image2, (this.mWidth - image2.GetCelWidth()) / 2 + GlobalChallenge.gScreenShake, (this.mHeight - image2.GetCelHeight()) / 2 + GlobalChallenge.gScreenShake, this.mLockCel);
				g.SetColorizeImages(false);
			}
			if (this.mMedal != null)
			{
				if (!flag2 || !g.Is3D())
				{
					if (this.mMedal == imageByID)
					{
						g.DrawImageCel(this.mMedal, (this.mWidth - this.mMedal.GetCelWidth()) / 2 + GlobalChallenge.gScreenShake + Common._DS(10), (this.mHeight - this.mMedal.GetCelHeight()) / 2 + GlobalChallenge.gScreenShake, 0);
					}
					else
					{
						g.DrawImageCel(this.mMedal, (this.mWidth - this.mMedal.GetCelWidth()) / 2 + GlobalChallenge.gScreenShake, (this.mHeight - this.mMedal.GetCelHeight()) / 2 + GlobalChallenge.gScreenShake, 0);
					}
				}
				else if (this.mMedal != null)
				{
					g.PushState();
					g.ClearClipRect();
					g.Translate(-this.mX, -this.mY);
					Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_CROWN);
					if (this.mChallengeMenu.mCrownZoomType == 1)
					{
						g.DrawImage(imageByID2, this.mX + (this.mWidth - imageByID2.mWidth) / 2, this.mY + (this.mHeight - imageByID2.mHeight) / 2);
						imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_ACECROWN);
					}
					g.SetColor(new Color(255, 255, 255, (int)this.mChallengeMenu.mCrownAlpha));
					g.SetColorizeImages(true);
					SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
					sexyTransform2D.Scale(this.mChallengeMenu.mCrownSize, this.mChallengeMenu.mCrownSize);
					sexyTransform2D.Translate((float)this.mX + ((float)this.mWidth - (float)imageByID2.mWidth * this.mChallengeMenu.mCrownSize) / 2f, (float)this.mY + ((float)this.mHeight - (float)imageByID2.mHeight * this.mChallengeMenu.mCrownSize) / 2f);
					g.DrawImageMatrix(imageByID2, sexyTransform2D, (float)imageByID2.mWidth * this.mChallengeMenu.mCrownSize / 2f, (float)imageByID2.mHeight * this.mChallengeMenu.mCrownSize / 2f);
					g.PopState();
				}
			}
			if (!flag2 && this.mUnlockSparkles != null)
			{
				g.Is3D();
			}
		}

		public override void Update()
		{
			this.mUpdateCnt++;
			bool flag = this.mChallengeMenu.mCrownZoomType >= 0;
			if (this.mUnlockSparkles != null && !flag)
			{
				this.mUnlockSparkles.Update();
				this.MarkDirty();
				if (this.mUnlockSparkles.mCurNumParticles == 0 && this.mUnlockSparkles.mFrameNum > 10f)
				{
					this.mUnlockSparkles.Dispose();
					this.mUnlockSparkles = null;
				}
			}
			if (!flag)
			{
				if (this.mLockCel < Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_CS_LOCK_ANIMATION).mNumCols - 1 && this.mUpdateCnt % Common._M(8) == 0)
				{
					this.mLockCel++;
					return;
				}
				if (this.mUnlockAlpha > 0)
				{
					this.mUnlockAlpha -= Common._M(2);
				}
			}
		}

		public override void MouseEnter()
		{
		}

		public override void MouseLeave()
		{
		}

		public void PreLoadImage()
		{
			if (this.mLevel != -1)
			{
				GameApp.gApp.GetLevelThumbnail(this.mLevel);
			}
		}

		private static int last_uc;

		public PIEffect mUnlockSparkles;

		public int mUnlockAlpha;

		public int mLockCel;

		public Image mMedal;

		public string mScoreStr = "";

		public string mLevelStr = "";

		public string mAceStr = "";

		public string mLevelId = "";

		public bool mMouseOver;

		public bool mOpaque = true;

		public int mLevel = -1;

		public ChallengeMenu mChallengeMenu;

		public enum BtnType
		{
			Btn_CS_Back,
			Btn_CS_PrevSet,
			Btn_CS_NextSet,
			Btn_First_Challenge
		}
	}
}
