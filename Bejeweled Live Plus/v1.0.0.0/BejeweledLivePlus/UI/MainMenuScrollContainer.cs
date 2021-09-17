using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class MainMenuScrollContainer : Bej3Widget, Bej3ButtonListener, ButtonListener, ScrollWidgetListener
	{
		public MainMenuScrollContainer(MainMenu parent)
			: base(Menu_Type.MENU_MAINMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mButtons = new List<CrystalBall>();
			this.mCrystalBallCountdown = 150;
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			this.mZenButton = new CrystalBall(GlobalMembers._ID("ZEN", 3366), GlobalMembers._ID("", 3367), GlobalMembers._ID("", 3368), 1, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_ZEN_SCALE + 0.1f);
			this.mButtons.Add(this.mZenButton);
			this.AddWidget(this.mZenButton);
			this.mDiamondMineButton = new CrystalBall(GlobalMembers._ID("DIAMOND", 3369), GlobalMembers._ID("MINE", 3370), GlobalMembers._ID("", 3371), 2, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_DIAMONDMINE_SCALE);
			this.mButtons.Add(this.mDiamondMineButton);
			this.AddWidget(this.mDiamondMineButton);
			this.mClassicButton = new CrystalBall(GlobalMembers._ID("CLASSIC", 3372), GlobalMembers._ID("", 3373), GlobalMembers._ID("", 3374), 0, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_SCALE);
			this.mButtons.Add(this.mClassicButton);
			this.AddWidget(this.mClassicButton);
			this.mLightningButton = new CrystalBall(GlobalMembers._ID("LIGHTNING", 3375), GlobalMembers._ID("", 3376), GlobalMembers._ID("", 3377), 3, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_LIGHTNING_SCALE);
			this.mButtons.Add(this.mLightningButton);
			this.AddWidget(this.mLightningButton);
			this.mButterflyButton = new CrystalBall(GlobalMembers._ID("BUTTERFLIES", 3378), GlobalMembers._ID("", 3379), GlobalMembers._ID("", 3380), 5, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_SCALE + 0.1f);
			this.mButtons.Add(this.mButterflyButton);
			this.AddWidget(this.mButterflyButton);
			this.mLeaderBoardButton = new CrystalBall(GlobalMembers._ID("LeaderBoards", 3381), GlobalMembers._ID("", 3382), GlobalMembers._ID("", 3383), 7, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_SCALE - 0.3f);
			this.mButtons.Add(this.mLeaderBoardButton);
			this.AddWidget(this.mLeaderBoardButton);
			this.mAchievementButton = new CrystalBall(GlobalMembers._ID("Achievements", 3384), GlobalMembers._ID("", 3385), GlobalMembers._ID("", 3386), 8, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_SCALE - 0.3f);
			this.mButtons.Add(this.mAchievementButton);
			this.AddWidget(this.mAchievementButton);
			this.mBuyFullGameButton = new CrystalBall(GlobalMembers._ID("Buy", 3387), GlobalMembers._ID("FullGame", 3388), GlobalMembers._ID("", 3389), 9, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_SCALE);
			this.mBuyFullGameButton.mFontScale = 0.9f;
			this.mButtons.Add(this.mBuyFullGameButton);
			this.AddWidget(this.mBuyFullGameButton);
			this.Tst1Button = new ArrowButton(GlobalMembers._ID("Bonus", 3393), GlobalMembers._ID("Games", 3394), GlobalMembers._ID("", 3392), 10, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_SCALE - 0.3f);
			this.Tst1Button.Resize(0, 0, 180, 180);
			this.AddWidget(this.Tst1Button);
			this.Tst2Button = new ArrowButton(GlobalMembers._ID("Help &", 3390), GlobalMembers._ID("About", 3391), GlobalMembers._ID("", 7669), 11, this, Bej3Widget.COLOR_CRYSTALBALL_FONT, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_SCALE - 0.3f);
			this.Tst2Button.Resize(0, 0, 180, 180);
			this.Tst2Button.IsLeft = true;
			this.AddWidget(this.Tst2Button);
			this.initButtonPosition();
			this.mCurrentPage = MainMenuScrollContainer.PAGE_NUM.MAIN_PAGE;
			this.mFlag = true;
			this.mSlide_Left = false;
			this.mSlide_Right = false;
			this.mSlide_Count = 0;
			for (int i = 0; i < this.mButtons.size<CrystalBall>(); i++)
			{
				this.mButtons[i].SetVisible(false);
				this.mButtons[i].mAlpha = this.mAlphaCurve;
			}
			this.mScrollWidget = new ScrollWidget(this);
			this.mScrollWidget.Resize(ConstantsWP.BADGEMENU_CONTAINER_PADDING_X, ConstantsWP.BADGEMENU_CONTAINER_TOP, this.mWidth - ConstantsWP.BADGEMENU_CONTAINER_PADDING_X * 2, ConstantsWP.BADGEMENU_CONTAINER_HEIGHT);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.EnablePaging(true);
			this.mScrollWidget.SetScrollInsets(new Insets(0, 0, 0, 0));
			this.mScrollWidget.SetPageHorizontal(0, false);
		}

		public void initButtonPosition()
		{
			int num = 40;
			int num2 = 40;
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_BLITZ_X - 75, ConstantsWP.MAIN_MENU_BUTTON_BLITZ_Y + 470 + num, this.Tst2Button);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_DIAMONDMINE_X + 75, ConstantsWP.MAIN_MENU_BUTTON_BLITZ_Y + 470 + num, this.Tst1Button);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X, ConstantsWP.MAIN_MENU_BUTTON_BLITZ_Y + 410 + num, this.mBuyFullGameButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X, ConstantsWP.MAIN_MENU_BUTTON_BLITZ_Y + 250 + num, this.mAchievementButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X, ConstantsWP.MAIN_MENU_BUTTON_BLITZ_Y + 120 + num, this.mLeaderBoardButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_X + 2 * ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X, ConstantsWP.MAIN_MENU_BUTTON_BUTTERFLIES_Y + num - num2, this.mButterflyButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_BLITZ_X, ConstantsWP.MAIN_MENU_BUTTON_BLITZ_Y + num, this.mLightningButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X, ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_Y + num, this.mClassicButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_DIAMONDMINE_X, ConstantsWP.MAIN_MENU_BUTTON_DIAMONDMINE_Y + num, this.mDiamondMineButton);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAIN_MENU_BUTTON_ZEN_X + 2 * ConstantsWP.MAIN_MENU_BUTTON_CLASSIC_X, ConstantsWP.MAIN_MENU_BUTTON_ZEN_Y + num + num2, this.mZenButton);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Update()
		{
			base.Update();
			if (this.mSlide_Left)
			{
				if ((float)this.mSlide_Count < ConstantsWP.DEVICE_WIDTH_F / 20f)
				{
					for (int i = 0; i < this.mButtons.size<CrystalBall>(); i++)
					{
						this.mButtons[i].mX -= 20;
					}
					this.mSlide_Count++;
				}
				else
				{
					this.mSlide_Count = 0;
					this.mSlide_Left = false;
				}
			}
			if (this.mSlide_Right)
			{
				if ((float)this.mSlide_Count < ConstantsWP.DEVICE_WIDTH_F / 20f)
				{
					for (int j = 0; j < this.mButtons.size<CrystalBall>(); j++)
					{
						this.mButtons[j].mX += 20;
					}
					this.mSlide_Count++;
				}
				else
				{
					this.mSlide_Count = 0;
					this.mSlide_Right = false;
				}
			}
			if (this.mFlag)
			{
				if (!MainMenuScrollContainer.hasValue)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_SCROLL_CONTAINER_UPDATE_ALPHA, MainMenuScrollContainer.alpha);
					MainMenuScrollContainer.hasValue = true;
				}
				int count = this.mButtons.Count;
				for (int k = 0; k < this.mButtons.size<CrystalBall>(); k++)
				{
					this.mButtons[k].SetVisible(true);
					this.mButtons[k].mAlpha = MainMenuScrollContainer.alpha;
				}
				((MainMenu)GlobalMembers.gApp.mMenus[2]).ShowLogo();
				((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).mCanSlideIn = true;
				GlobalMembers.gApp.ClearUpdateBacklog(false);
				this.mFlag = false;
			}
			this.mBuyFullGameButton.SetVisible(!this.mIsFullGame);
			this.mLeaderBoardButton.SetDisabled(!this.mIsFullGame);
			this.mLeaderBoardButton.mFontColor.SetColor(this.mIsFullGame ? Bej3Widget.COLOR_CRYSTALBALL_FONT.mRed : 127, this.mIsFullGame ? Bej3Widget.COLOR_CRYSTALBALL_FONT.mGreen : 127, this.mIsFullGame ? Bej3Widget.COLOR_CRYSTALBALL_FONT.mBlue : 127, 255);
			this.mAchievementButton.SetDisabled(!this.mIsFullGame);
			this.mAchievementButton.mFontColor.SetColor(this.mIsFullGame ? Bej3Widget.COLOR_CRYSTALBALL_FONT.mRed : 127, this.mIsFullGame ? Bej3Widget.COLOR_CRYSTALBALL_FONT.mGreen : 127, this.mIsFullGame ? Bej3Widget.COLOR_CRYSTALBALL_FONT.mBlue : 127, 255);
		}

		public override void Draw(Graphics g)
		{
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public void EnterMain()
		{
			this.mFlag = true;
			this.mCurrentPage = MainMenuScrollContainer.PAGE_NUM.MAIN_PAGE;
			this.initButtonPosition();
		}

		public override void Show()
		{
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_LOADING)
			{
				return;
			}
			this.Tst1Button.mLabel = GlobalMembers._ID("Bonus", 3393);
			this.Tst1Button.mLabel2 = GlobalMembers._ID("Games", 3394);
			this.Tst2Button.mLabel = GlobalMembers._ID("Help &", 3390);
			this.Tst2Button.mLabel2 = GlobalMembers._ID("About", 3391);
			this.Tst2Button.mLabel3 = GlobalMembers._ID("", 7669);
			this.EnterMain();
			base.Show();
			this.mY = 0;
		}

		public void EnableButtons(bool enabled)
		{
			for (int i = 0; i < this.mButtons.size<CrystalBall>(); i++)
			{
				this.mButtons[i].mDisabled = !enabled;
			}
		}

		public override void ButtonDepress(int theId)
		{
			if (GlobalMembers.gApp.mTooltipManager.mTooltips.size<Tooltip>() > 0)
			{
				GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			}
			if (GlobalMembers.gApp.mGameInProgress || GlobalMembers.gApp.mMenus[5].IsTransitioning())
			{
				return;
			}
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.DoNewGame(GameMode.MODE_CLASSIC);
				return;
			case 1:
				if (this.mIsFullGame)
				{
					GlobalMembers.gApp.DoNewGame(GameMode.MODE_ZEN);
					return;
				}
				GlobalMembers.gApp.DoTrialDialog(theId);
				return;
			case 2:
				GlobalMembers.gApp.DoNewGame(GameMode.MODE_DIAMOND_MINE);
				return;
			case 3:
				if (this.mIsFullGame)
				{
					GlobalMembers.gApp.DoNewGame(GameMode.MODE_LIGHTNING);
					return;
				}
				GlobalMembers.gApp.DoTrialDialog(theId);
				return;
			case 4:
			{
				GlobalMembers.gApp.GoToBlitz();
				Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.GetDialog(48);
				return;
			}
			case 5:
				GlobalMembers.gApp.DoNewGame(GameMode.MODE_BUTTERFLY);
				return;
			case 6:
			{
				Bej3Dialog bej3Dialog2 = (Bej3Dialog)GlobalMembers.gApp.DoDialog(0, true, GlobalMembers._ID("Coming Soon", 3399), GlobalMembers._ID("We're working hard to bring you more game modes in future updates. Stay tuned!", 3400), GlobalMembers._ID("BACK", 3606), 3, 3, 3);
				GlobalMembers.gApp.mMenus[5].Transition_SlideOut();
				return;
			}
			case 7:
				if (this.mIsFullGame)
				{
					GlobalMembers.gApp.DoHighScoresMenu();
					return;
				}
				GlobalMembers.gApp.DoTrialDialog(theId);
				return;
			case 8:
				if (this.mIsFullGame)
				{
					GlobalMembers.gApp.DoBadgeMenu(2, GlobalMembers.gApp.mProfile.mDeferredBadgeVector);
					return;
				}
				GlobalMembers.gApp.DoTrialDialog(theId);
				return;
			case 9:
				if (Guide.IsTrialMode)
				{
					GlobalMembers.gApp.BuyGame();
				}
				this.mIsFullGame = !Guide.IsTrialMode;
				return;
			case 10:
				if (this.mCurrentPage == MainMenuScrollContainer.PAGE_NUM.MAIN_PAGE)
				{
					if (this.mSlide_Right)
					{
						return;
					}
					this.Tst1Button.mLabel = GlobalMembers._ID("More", 3395);
					this.Tst1Button.mLabel2 = GlobalMembers._ID("Games", 3396);
					this.Tst2Button.mLabel = GlobalMembers._ID("Back", 3640);
					this.Tst2Button.mLabel2 = "";
					this.Tst2Button.mLabel3 = "";
					this.mCurrentPage = MainMenuScrollContainer.PAGE_NUM.EXPEND_PAGE;
					this.mFlag = true;
					this.mSlide_Left = true;
					return;
				}
				else
				{
					if (this.mSlide_Left)
					{
						return;
					}
					GlobalMembers.gApp.OpenURLWithWarning(GlobalMembers._ID("http://mg.eamobile.com/?rId=1560", 8888));
					return;
				}
				break;
			case 11:
				if (this.mCurrentPage == MainMenuScrollContainer.PAGE_NUM.EXPEND_PAGE)
				{
					if (this.mSlide_Left)
					{
						return;
					}
					this.Tst1Button.mLabel = GlobalMembers._ID("Bonus", 3393);
					this.Tst1Button.mLabel2 = GlobalMembers._ID("Games", 3394);
					this.Tst2Button.mLabel = GlobalMembers._ID("Help &", 3390);
					this.Tst2Button.mLabel2 = GlobalMembers._ID("About", 3391);
					this.Tst2Button.mLabel3 = "";
					this.mCurrentPage = MainMenuScrollContainer.PAGE_NUM.MAIN_PAGE;
					this.mFlag = true;
					this.mSlide_Right = true;
					return;
				}
				else
				{
					if (this.mSlide_Right)
					{
						return;
					}
					GlobalMembers.gApp.DoOptionsMenu();
					return;
				}
				break;
			default:
				return;
			}
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
			scrollWidget.mIsDown = false;
			scrollWidget.ScrollToPoint(new Point(ConstantsWP.MAIN_MENU_TAB_WIDTH * MainMenu.mScrollwidgetPage, 0), true);
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
			base.TouchBegan(touch);
			GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mClassicButton.LinkUpAssets();
			this.mZenButton.LinkUpAssets();
			this.mDiamondMineButton.LinkUpAssets();
		}

		public override int GetShowCurve()
		{
			return 25;
		}

		public void MakeButtonsFullyVisible()
		{
			for (int i = 0; i < this.mButtons.size<CrystalBall>(); i++)
			{
				this.mButtons[i].mAlpha.SetConstant(1.0);
			}
		}

		public int CurrentPage
		{
			get
			{
				return (int)this.mCurrentPage;
			}
		}

		private bool mSlide_Left;

		private bool mSlide_Right;

		private int mSlide_Count;

		private List<CrystalBall> mButtons;

		public CrystalBall mClassicButton;

		private CrystalBall mLeaderBoardButton;

		private CrystalBall mAchievementButton;

		private CrystalBall mBuyFullGameButton;

		public bool mIsFullGame = !Guide.IsTrialMode;

		private CrystalBall mButterflyButton;

		private CrystalBall mZenButton;

		private CrystalBall mDiamondMineButton;

		private CrystalBall mLightningButton;

		private CrystalBall mComingSoonButton;

		private ArrowButton Tst1Button;

		private ArrowButton Tst2Button;

		private int mCrystalBallCountdown;

		private MainMenuScrollContainer.PAGE_NUM mCurrentPage;

		public ScrollWidget mScrollWidget;

		public bool mFlag;

		public static CurvedVal alpha = new CurvedVal();

		public static bool hasValue = false;

		public enum MAINMENU_BUTTON_IDS
		{
			BTN_CLASSIC_ID,
			BTN_ZEN_ID,
			BTN_DIAMOND_MINE_ID,
			BTN_LIGHTNING_ID,
			BTN_BLITZ_ID,
			BTN_BUTTERFLY_ID,
			BTN_COMING_SOON_ID,
			BTN_MORE_1_ID,
			BTN_MORE_2_ID,
			BTN_MORE_3_ID,
			BTN_MORE_4_ID,
			BTN_MORE_5_ID
		}

		public enum PAGE_NUM
		{
			MAIN_PAGE,
			EXPEND_PAGE
		}
	}
}
