using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class ChallengeMenu : Widget, ButtonListener, PopAnimListener
	{
		public ChallengeMenu(GameApp theApp, MainMenu theMainMenu, bool fromMainMenu)
		{
			this.mApp = theApp;
			this.mMainMenu = theMainMenu;
			this.mCurrentChallengeZone = 0;
			this.mCrownSize = 1f;
			this.mCrownAlpha = 255f;
			this.mCrownZoomType = -1;
			this.mCrownZoomDelay = 0;
			this.mTrophy = null;
			this.mTrophyY = 0f;
			this.mDoBounceTrophy = false;
			this.mTrophyVY = 0f;
			this.mTrophyBounceCount = 0;
			this.mAceFX = null;
			this.mRegularTrophy = null;
			this.mIsAceTrophy = false;
			this.mCrossFadeTrophies = false;
			this.mAceTrophyAlpha = 0f;
			this.mFadeInAceTrophy = false;
			this.mTrophyBounceDelay = 0;
			this.mTrophyFlare = null;
			this.mShowFullAceFX = false;
			this.mCSVisFrame = 0;
			this.mLoopTrophyFlare = false;
			this.mSlideDir = 0;
			this.mXFadeAlpha = 255f;
			this.mTimer = 0;
			this.mSelectedLevel = -1;
			this.mButtons = new List<ButtonWidget>();
			this.mDefaultStringContainer = new ChallengeMenu.DefaultStringContainer();
			this.mHomeButton = null;
			this.mChallengeLevelInfoWidget = null;
			this.mChallengeScrollWidget = null;
			if (GameApp.mGameRes == 768)
			{
				this.mTitleXOffset = 30f;
			}
			else
			{
				this.mTitleXOffset = 0f;
			}
			this.mFromMainMenu = fromMainMenu;
			this.mCueMainSong = false;
			this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE);
			this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR);
			this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);
			this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END);
			this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP);
			this.IMAGE_UI_CHALLENGESCREEN_WOOD = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD);
			this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE);
			this.IMAGE_GUI_TIKITEMPLE_PEDESTAL = Res.GetImageByID(ResID.IMAGE_GUI_TIKITEMPLE_PEDESTAL);
			this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1);
			this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2);
			this.IMAGE_UI_CHALLENGESCREEN_DRUMS = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS);
			this.IMAGE_UI_CHALLENGESCREEN_FRUIT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT);
			this.IMAGE_UI_LEADERBOARDS_LEAVES2 = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2);
			this.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING);
			this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT);
			this.IMAGE_UI_CHALLENGESCREEN_HOME = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			this.mChallengePages = null;
			this.mChallengeScrollWidget = null;
			this.mHomeButton = null;
			this.mChallengeLevelInfoWidget = null;
			this.mChallengeScrollPageControl = null;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				GameApp.gApp.DeleteZoneThumbnails(i);
			}
		}

		public override void Draw(Graphics g)
		{
			g.Translate(-GameApp.gApp.GetScreenRect().mX / 2, 0);
			int gScreenShake = GlobalChallenge.gScreenShake;
			int num = 0;
			if (GameApp.gLastZone != -1)
			{
				num = ((GameApp.gLastZone == 7 && this.mApp.mUserProfile.mChallengeUnlockState[GameApp.gLastZone - 1, 0] == 0) ? Common._DS(Common._M(635)) : Common._DS(Common._M1(608))) + gScreenShake;
			}
			int num2 = Common._DS(Common._M(500));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE, -GameApp.gApp.mWideScreenXOffset, 0);
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE, -GameApp.gApp.mWideScreenXOffset + this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, 0, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR, 0, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR)), GameApp.gApp.GetScreenWidth(), this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)));
			int num3 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END));
			int num4 = GameApp.gApp.GetScreenRect().mWidth - GameApp.gApp.GetScreenRect().mX - num3;
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP, num3 + this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP)), num4 - (num3 + this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth()), this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END, num3, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END, num4, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_WOOD, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)));
			if (this.mTrophy != null)
			{
				int num5 = (this.mApp.mHiRes ? 2 : 0);
				int[] array = new int[]
				{
					Common._M(194),
					Common._M1(184) + num5,
					Common._M2(188),
					Common._M3(194),
					Common._M4(188),
					Common._M5(194),
					Common._M6(194)
				};
				Point[] array2 = new Point[]
				{
					new Point(Common._M(-250), Common._M1(-830)),
					new Point(Common._M2(-246), Common._M3(-850)),
					new Point(Common._M4(-246), Common._M5(-850)),
					new Point(Common._M6(-252), Common._M7(-876)),
					new Point(Common._M(-246), Common._M1(-850)),
					new Point(Common._M2(-252), Common._M3(-860)),
					new Point(Common._M4(-248), Common._M5(-838))
				};
				g.DrawImage(this.mTrophy, num + Common._DS(array[GameApp.gLastZone - 1]) - this.mTrophy.mWidth / 2, (int)(this.mTrophyY + (float)GlobalChallenge.gScreenShake));
				if (g.Is3D() && this.mTrophyFlare != null && !this.mDoBounceTrophy)
				{
					Transform transform = new Transform();
					transform.Translate((float)(num + Common._DS(array[GameApp.gLastZone - 1] + array2[GameApp.gLastZone - 1].mX)), (float)(num2 + Common._DS(array2[GameApp.gLastZone - 1].mY)));
					this.mTrophyFlare.SetTransform(transform.GetMatrix());
					this.mTrophyFlare.Draw(g);
				}
				if (g.Is3D() && this.mAceFX != null)
				{
					g.PushState();
					g.ClipRect(Common._DS(Common._M(540)) + gScreenShake, Common._DS(Common._M1(40)), Common._DS(Common._M2(530)), Common._DS(Common._M3(1200)));
					if (this.mShowFullAceFX)
					{
						this.mAceFX.Draw(g);
					}
					else
					{
						this.mAceFX.DrawLayer(g, this.mAceFX.GetLayer("mask"));
					}
					g.PopState();
				}
			}
			base.DeferOverlay(9);
			g.Translate(GameApp.gApp.GetScreenRect().mX / 2, 0);
		}

		public override void DrawOverlay(Graphics g)
		{
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, -GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, GameApp.gApp.GetScreenWidth() + GameApp.gApp.mWideScreenXOffset - this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE.GetWidth() + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_GUI_TIKITEMPLE_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset - Common._DS(30), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1.GetHeight() + Common._DS(15));
			g.DrawImage(this.IMAGE_GUI_TIKITEMPLE_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset - Common._DS(20) + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2.GetHeight() - Common._DS(15));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + Common._DS(120));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + Common._DS(120));
			g.SetColor(new Color(255, 255, 255, 255));
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_GAUNTLET));
			string @string = TextManager.getInstance().getString(782);
			int num = g.GetFont().StringWidth(@string);
			g.DrawString(@string, (int)(this.mTitleXOffset + (float)Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - (float)GameApp.gApp.mWideScreenXOffset + (float)((this.IMAGE_UI_CHALLENGESCREEN_WOOD.GetWidth() - num) / 2)), Common._DS(135));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DRUMS, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_FRUIT, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)));
			g.DrawImage(this.IMAGE_UI_LEADERBOARDS_LEAVES2, 42 + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)));
			if (this.mHomeButton != null)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING, 0, 0);
				if (this.mHomeButton.IsButtonDown())
				{
					float num2 = (float)((this.IMAGE_UI_CHALLENGESCREEN_HOME.GetWidth() - this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT.GetWidth()) / 2);
					float num3 = (float)((this.IMAGE_UI_CHALLENGESCREEN_HOME.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT.GetHeight()) / 2);
					g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT, (int)((float)this.mHomeButton.mX + num2 + (float)GameApp.gApp.GetScreenRect().mX), (int)((float)this.mHomeButton.mY + num3));
					return;
				}
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_HOME, this.mHomeButton.mX + GameApp.gApp.GetScreenRect().mX, this.mHomeButton.mY);
			}
		}

		public override void Update()
		{
			Common._M(0);
			if (this.mTrophyFlare != null && GameApp.gApp.Is3DAccelerated() && !this.mDoBounceTrophy)
			{
				this.MarkDirty();
				this.mTrophyFlare.Update();
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mChallengeScrollWidget.SetVisible(false);
			}
			else
			{
				this.mChallengeScrollWidget.SetVisible(true);
			}
			if (this.mFromMainMenu && !GameApp.gApp.mBambooTransition.IsInProgress())
			{
				if (this.mChallengeScrollWidget == null)
				{
					this.Init();
				}
				if (this.mChallengeScrollWidget != null)
				{
					this.mChallengeScrollWidget.SetPageHorizontal(1, false);
					this.mChallengeScrollWidget.SetPageHorizontal(0, true);
					this.mChallengePages.PreloadButtonImage(0);
				}
				this.mFromMainMenu = false;
			}
			if (this.mCueMainSong && GameApp.gApp.mBambooTransition != null && !GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mApp.PlaySong(1);
				this.mCueMainSong = false;
			}
			if (GlobalChallenge.gScreenShakeTimer > 0)
			{
				this.MarkDirty();
				GlobalChallenge.gScreenShakeTimer--;
				GlobalChallenge.gScreenShake = Common.Rand(Common._M(10));
				if (GlobalChallenge.gScreenShakeTimer == 0)
				{
					GlobalChallenge.gScreenShake = 0;
				}
			}
			if (this.mCrownZoomType >= 0 && --this.mCrownZoomDelay <= 0)
			{
				this.MarkDirty();
				this.mTimer++;
				int num = Common._M(75) - this.mTimer;
				float num2 = 255f / (float)num;
				this.mCrownAlpha += (float)((int)num2);
				if (this.mCrownAlpha > 255f)
				{
					this.mCrownAlpha = 255f;
				}
				num2 = Common._M(15f) / (float)num;
				this.mCrownSize -= num2;
				if (this.mCrownSize <= 1f)
				{
					if (this.mCrownZoomType == 0)
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_MINI_CROWN_IMPACT));
					}
					else
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_ACE_MINI_CROWN_IMPACT));
					}
					GlobalChallenge.gScreenShakeTimer = Common._M(15);
					this.mCrownSize = 1f;
					this.mCrownAlpha = 255f;
					if (this.mApp.mUserProfile.mDoChallengeAceTrophyZoom)
					{
						if (this.mApp.mUserProfile.mDoChallengeTrophyZoom)
						{
							this.mCrownZoomType = 1;
							this.mCrownSize = Common._M(16f);
							this.mCrownAlpha = 0f;
							this.mCrownZoomDelay = Common._M(20);
							this.mTimer = 0;
						}
						else
						{
							this.mCrownZoomType = -1;
						}
						this.mApp.mUserProfile.mDoChallengeTrophyZoom = (this.mApp.mUserProfile.mDoChallengeAceTrophyZoom = false);
						return;
					}
					this.mApp.mUserProfile.mDoChallengeTrophyZoom = false;
					this.mCrownZoomType = -1;
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_LEVELS_UNLOCKED));
					return;
				}
			}
			else if (this.mCrownZoomType == -1 && --this.mCrownZoomDelay <= 0 && this.mDoBounceTrophy)
			{
				this.MarkDirty();
			}
		}

		public void ShowChallengeLevelInfo(int theZoneNum, int theLevelNum, string theLevelName)
		{
			if (this.mChallengeLevelInfoWidget != null)
			{
				this.mChallengeState = ChallengeMenu.EChallengeState.State_LevelInfo;
				this.mChallengeLevelInfoWidget.SetLevel(theZoneNum, theLevelNum, theLevelName);
				this.mChallengeLevelInfoWidget.SetVisible(true);
				this.mChallengeLevelInfoWidget.SetDisabled(false);
				this.SetFocus(this.mChallengeLevelInfoWidget);
				int theNewX;
				if (GameApp.gApp.IsWideScreen())
				{
					theNewX = (int)((float)(GameApp.gApp.GetScreenRect().mWidth - GameApp.gApp.GetScreenRect().mX - this.mChallengeLevelInfoWidget.GetWidth()) * 0.5f);
				}
				else
				{
					theNewX = (int)((float)(GameApp.gApp.GetScreenRect().mWidth - this.mChallengeLevelInfoWidget.mWidth) * 0.5f);
				}
				int mY = this.mChallengeLevelInfoWidget.mY;
				this.mChallengeLevelInfoWidget.Move(theNewX, mY);
			}
		}

		public void HideChallengeLevelInfo()
		{
			this.mChallengeState = ChallengeMenu.EChallengeState.State_Challenge;
			if (this.mChallengeLevelInfoWidget != null)
			{
				this.mChallengeLevelInfoWidget.SetLevel(-1, -1, "");
				this.mChallengeLevelInfoWidget.SetVisible(false);
				this.mChallengeLevelInfoWidget.SetDisabled(true);
				this.SetFocus(this);
			}
		}

		public override void MouseUp(int x, int y)
		{
		}

		public void Init()
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame") && !GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.ShowResourceError(true);
				GameApp.gApp.Shutdown();
			}
			Common._M(0);
			this.mChallengeState = ChallengeMenu.EChallengeState.State_Challenge;
			this.mChallengePages = new ChallengeMenuScrollContainer(this);
			this.mChallengeScrollWidget = new ScrollWidget();
			this.mChallengeScrollWidget.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)) - GameApp.gApp.mWideScreenXOffset - GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth(), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
			this.mChallengeScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mChallengeScrollWidget.EnableBounce(true);
			this.mChallengeScrollWidget.EnablePaging(true);
			this.mChallengeScrollWidget.AddWidget(this.mChallengePages);
			this.mChallengeScrollPageControl = new PageControl(this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR.GetCelWidth();
			this.mChallengePages.NumPages();
			this.mChallengeScrollPageControl.SetNumberOfPages(this.mChallengePages.NumPages());
			this.mChallengeScrollPageControl.Move((int)(this.mTitleXOffset + (float)((this.mWidth - this.mChallengeScrollPageControl.mWidth) / 2) - (float)GameApp.gApp.GetScreenRect().mX), Common._DS(145));
			this.mChallengeScrollPageControl.SetCurrentPage(0);
			this.AddWidget(this.mChallengeScrollPageControl);
			this.mChallengeScrollWidget.SetPageControl(this.mChallengeScrollPageControl);
			this.AddWidget(this.mChallengeScrollWidget);
			if (this.mFromMainMenu)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(this.mChallengePages.NumPages(), false);
			}
			else
			{
				this.mChallengeScrollWidget.SetPageHorizontal(0, false);
			}
			this.mChallengeLevelInfoWidget = new ChallengeLevelInfo(this);
			int theWidth = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + Common._DS(100);
			this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight();
			Common._DS(150);
			this.mChallengeLevelInfoWidget.Resize(0, 0, theWidth, GameApp.gApp.GetScreenRect().mHeight);
			Common.SetupDialog(this.mChallengeLevelInfoWidget);
			this.mChallengeLevelInfoWidget.mPriority = 2147483645;
			this.mChallengeLevelInfoWidget.SetVisible(false);
			this.mChallengeLevelInfoWidget.SetDisabled(true);
			this.AddWidget(this.mChallengeLevelInfoWidget);
		}

		public void RehupChallengeButtons()
		{
			if (this.mChallengePages != null)
			{
				this.mChallengePages.RehupChallengeButtons();
			}
		}

		public void InitCS()
		{
			if (this.mApp.mUserProfile.mAdvModeVars.mHighestLevelBeat < 10)
			{
				return;
			}
			this.mChallengePages.RehupChallengeButtons();
			this.mMainMenu.RehupButtons();
			this.mCrownZoomType = -1;
			if ((this.mApp.mUserProfile.mDoChallengeTrophyZoom || this.mApp.mUserProfile.mDoChallengeAceTrophyZoom) && GameApp.gApp.Is3DAccelerated())
			{
				this.mCrownZoomType = (this.mApp.mUserProfile.mDoChallengeTrophyZoom ? 0 : 1);
				this.mCrownSize = Common._M(16f);
				this.mCrownAlpha = 0f;
				this.mTimer = 0;
				this.mCrownZoomDelay = 0;
			}
			bool flag = GameApp.gApp.mUserProfile.mDoChallengeCupComplete || GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete;
			bool flag2 = GameApp.gApp.mUserProfile.mUnlockSparklesIdx1 != -1 || GameApp.gApp.mUserProfile.mUnlockSparklesIdx2 != -1;
			if (flag && !flag2)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(0, false);
				this.mChallengeScrollPageControl.SetCurrentPage(0);
				this.mChallengePages.AwardMedal(GameApp.gLastZone, GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete);
			}
			else
			{
				this.SetupChallengeZone(GameApp.gLastZone);
			}
			if (this.mFromMainMenu && this.mChallengeScrollWidget != null)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(this.mChallengePages.NumPages(), false);
			}
		}

		public void StartChallengeGame()
		{
			this.mApp.StartGauntletMode(this.mChallengeLevelInfoWidget.GetChallengeLevelName(), this.mCSOverRect);
		}

		public virtual void ButtonPress(int id)
		{
			this.ButtonPress(id, 1);
		}

		public virtual void ButtonPress(int id, int cc)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
		}

		public virtual void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (this.mSlideDir != 0)
			{
				return;
			}
			if (id == 0)
			{
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.HideChallengeMenu);
				GameApp.gApp.ToggleBambooTransition();
			}
		}

		public void SetupChallengeZone(int zone)
		{
			if (this.mAceFX != null)
			{
				this.mAceFX.mEmitAfterTimeline = false;
			}
			if (this.mChallengeScrollWidget != null)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(zone + 1, false);
				this.mChallengeScrollPageControl.SetCurrentPage(zone + 1);
			}
		}

		public bool ProcessHardwareBackButton()
		{
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
			if (GameApp.gApp.GetDialog(0) != null)
			{
				GameApp.gApp.DialogButtonDepress(0, 0);
				return false;
			}
			if (this.mChallengeLevelInfoWidget != null && this.mChallengeLevelInfoWidget.mVisible && !this.mChallengeLevelInfoWidget.mDisabled)
			{
				this.HideChallengeLevelInfo();
				return false;
			}
			if (this.mChallengeScrollWidget.GetPageHorizontal() > 0)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(0, true);
				return false;
			}
			GameApp.gApp.ToggleBambooTransition();
			GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideChallengeMenu);
			return true;
		}

		public bool IsReceivingAward()
		{
			bool flag = GameApp.gApp.mUserProfile.mUnlockSparklesIdx1 != -1 || GameApp.gApp.mUserProfile.mUnlockSparklesIdx2 != -1;
			bool flag2 = GameApp.gApp.mUserProfile.mDoChallengeCupComplete || GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete;
			return flag || flag2;
		}

		public bool HasAcedZone(int theZoneNum)
		{
			bool flag = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, 0] == 0;
			if (flag)
			{
				return false;
			}
			bool result = true;
			if (GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete && GameApp.gLastZone == theZoneNum)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < 10; i++)
				{
					int num = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, i];
					if (num != 5)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		public bool HasBeatZone(int theZoneNum)
		{
			bool flag = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, 0] == 0;
			if (flag)
			{
				return false;
			}
			bool result = true;
			if ((GameApp.gApp.mUserProfile.mDoChallengeCupComplete || GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete) && GameApp.gLastZone == theZoneNum)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < 10; i++)
				{
					int num = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, i];
					if (num != 4 && num != 5)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		public virtual void ButtonDownTick(int x)
		{
		}

		public virtual void ButtonMouseEnter(int x)
		{
		}

		public virtual void ButtonMouseLeave(int x)
		{
		}

		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		public void PopAnimStopped(int id)
		{
			if (this.mTrophyFlare != null && id == this.mTrophyFlare.mId)
			{
				if (this.mLoopTrophyFlare)
				{
					this.mTrophyFlare.Play("Main");
					return;
				}
				this.mTrophyFlare = null;
			}
		}

		public void PopAnimPlaySample(string theSampleName, int thePan, double theVolume, double theNumSteps)
		{
		}

		public PIEffect PopAnimLoadParticleEffect(string theEffectName)
		{
			return null;
		}

		public bool PopAnimObjectPredraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		public bool PopAnimObjectPostdraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		public ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, Graphics g, int theDrawCount)
		{
			return ImagePredrawResult.ImagePredraw_Normal;
		}

		public void PopAnimCommand(int theId, string theCommand, string theParam)
		{
		}

		public bool PopAnimCommand(int theId, PASpriteInst theSpriteInst, string theCommand, string theParam)
		{
			this.PopAnimCommand(theId, theCommand, theParam);
			return true;
		}

		protected ChallengeMenu.EChallengeState mChallengeState;

		public GameApp mApp;

		public PIEffect mAceFX;

		public PopAnim mTrophyFlare;

		public Rect mCSOverRect;

		public Image mTrophy;

		public Image mRegularTrophy;

		public float mTrophyY;

		public bool mDoBounceTrophy;

		public float mTrophyVY;

		public int mTrophyBounceCount;

		public int mTrophyBounceDelay;

		public bool mCrossFadeTrophies;

		public bool mIsAceTrophy;

		public bool mShowFullAceFX;

		public int mCurrentChallengeZone;

		public float mCrownSize;

		public float mCrownAlpha;

		public int mCrownZoomType;

		public int mCrownZoomDelay;

		public int mCSVisFrame;

		public bool mLoopTrophyFlare;

		public float mXFadeAlpha;

		public int mTimer;

		public int mSelectedLevel;

		public List<ButtonWidget> mButtons;

		public MainMenu mMainMenu;

		public float mAceTrophyAlpha;

		public bool mFadeInAceTrophy;

		public int mSlideDir;

		public ChallengeMenuScrollContainer mChallengePages;

		public PageControl mChallengeScrollPageControl;

		public ScrollWidget mChallengeScrollWidget;

		public ButtonWidget mHomeButton;

		public ChallengeLevelInfo mChallengeLevelInfoWidget;

		public float mTitleXOffset;

		public bool mFromMainMenu;

		public bool mCueMainSong;

		public ChallengeMenu.DefaultStringContainer mDefaultStringContainer;

		private Image IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE;

		private Image IMAGE_UI_CHALLENGESCREEN_BG_FLOOR;

		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES;

		private Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END;

		private Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP;

		private Image IMAGE_UI_CHALLENGESCREEN_WOOD;

		private Image IMAGE_UI_CHALLENGESCREEN_BG_SIDE;

		private Image IMAGE_GUI_TIKITEMPLE_PEDESTAL;

		private Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1;

		private Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2;

		private Image IMAGE_UI_CHALLENGESCREEN_DRUMS;

		private Image IMAGE_UI_CHALLENGESCREEN_FRUIT;

		private Image IMAGE_UI_LEADERBOARDS_LEAVES2;

		private Image IMAGE_UI_CHALLENGESCREEN_HOME_BACKING;

		private Image IMAGE_UI_CHALLENGESCREEN_HOME_SELECT;

		private Image IMAGE_UI_CHALLENGESCREEN_HOME;

		private Image IMAGE_UI_CHALLENGE_PAGE_INDICATOR;

		public enum Zoom
		{
			Zooming_Crown,
			Zooming_AceCrown
		}

		protected enum EChallengeState
		{
			State_Challenge,
			State_LevelInfo
		}

		public class DefaultStringContainer
		{
			public DefaultStringContainer()
			{
				this.mDefaultStr = this.NonIfLocked();
			}

			public string NonIfLocked()
			{
				return TextManager.getInstance().getString(427);
			}

			public string IfLocked()
			{
				return TextManager.getInstance().getString(428);
			}

			public string CanPlayZone()
			{
				return TextManager.getInstance().getString(429);
			}

			public string ZoneUnlocked()
			{
				return TextManager.getInstance().getString(430);
			}

			public string NothingSelected()
			{
				return TextManager.getInstance().getString(431);
			}

			public string mDefaultStr;
		}
	}
}
