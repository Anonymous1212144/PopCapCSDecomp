using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Tasks;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class MainMenuButtonsWidget : Widget, ButtonListener
	{
		public void ButtonPress(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDepress(int theId)
		{
			GameApp gameApp = this.mApp;
			if (this.mMenu.mFirstTimeAlpha > 0 || this.mMenu.mIFUnlockAnim != null || this.mApp.mGenericHelp != null || this.mMenu.mDelayedIFStartState > 0 || this.mMenu.ShowingTikiTemple() || this.mApp.mMapScreen != null)
			{
				return;
			}
			if (this.mMenu.mState == MainMenu_State.State_Scroll)
			{
				return;
			}
			if (gameApp.mBambooTransition != null && gameApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (this.mMenu.mChallengeMenu != null && (this.mMenu.mUpdateCnt - this.mMenu.mChallengeMenu.mCSVisFrame < 10 || this.mMenu.mChallengeMenu.mCrownZoomType >= 0 || this.mMenu.mChallengeMenu.mDoBounceTrophy || this.mMenu.mChallengeMenu.mCrossFadeTrophies))
			{
				return;
			}
			Dialog dialog = gameApp.GetDialog(2);
			if (dialog != null)
			{
				return;
			}
			this.mMenu.mTip = null;
			this.mApp.mClickedHardMode = false;
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON3));
			if (theId == 0)
			{
				if (!this.mApp.IsRegistered() && this.mApp.mTrialType == 1 && (this.mApp.mUserProfile.mAdvModeVars.mCurrentAdvZone > 2 || this.mApp.mUserProfile.mAdvModeVars.mHighestZoneBeat >= 2))
				{
					this.mApp.DoUpsell(false);
					return;
				}
				if (!this.mApp.mUserProfile.mNeedsFirstTimeIntro)
				{
					this.mApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(this.mApp.ShowAdventureModeMapScreen);
					this.mApp.ToggleBambooTransition();
					return;
				}
				this.mMenu.mFirstTimeAlpha = 1;
				return;
			}
			else
			{
				if (theId == 5)
				{
					return;
				}
				if (theId == 1)
				{
					if (GameApp.USE_TRIAL_VERSION)
					{
						this.mMenu.mState = MainMenu_State.State_QuitPrompt;
						string @string = TextManager.getInstance().getString(836);
						int width_pad = Common._DS(Common._M(20));
						GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(835), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
						GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessUnlock);
						return;
					}
					if (!this.mApp.ChallengeModeUnlocked())
					{
						this.mMenu.mState = MainMenu_State.State_UnlockPrompt;
						this.mApp.DoGenericDialog(TextManager.getInstance().getString(837), TextManager.getInstance().getString(838), true, new GameApp.PreBlockCallback(this.ChangeMainMenuState), Common._DS(100));
						this.mMenu.mSkipEnterSound = true;
						return;
					}
					gameApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(this.mMenu.ShowChallengeMenuFromMainMenu);
					gameApp.ToggleBambooTransition();
					this.mApp.mUserProfile.mDoChallengeAceCupComplete = (this.mApp.mUserProfile.mDoChallengeCupComplete = false);
					this.mApp.mUserProfile.mDoChallengeAceTrophyZoom = (this.mApp.mUserProfile.mDoChallengeTrophyZoom = false);
					this.mApp.mUserProfile.mNewChallengeCupUnlocked = false;
					return;
				}
				else
				{
					if (theId == 9)
					{
						gameApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(gameApp.mMainMenu.ShowTikiTemple);
						gameApp.ToggleBambooTransition();
						return;
					}
					if (theId == 3)
					{
						if (GameApp.USE_TRIAL_VERSION)
						{
							this.ProcessLocked(false);
							return;
						}
						if (GameApp.UN_UPDATE_VERSION)
						{
							GameApp.gApp.HandleGameUpdateRequired(null);
							return;
						}
						GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.ShowAchievements);
						GameApp.gApp.ToggleBambooTransition();
						return;
					}
					else if (theId == 2)
					{
						if (GameApp.USE_TRIAL_VERSION)
						{
							this.ProcessLocked(false);
							return;
						}
						if (GameApp.UN_UPDATE_VERSION)
						{
							GameApp.gApp.HandleGameUpdateRequired(null);
							return;
						}
						GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.ShowLeaderBoards);
						GameApp.gApp.ToggleBambooTransition();
						return;
					}
					else
					{
						if (theId == 4)
						{
							GameApp.gApp.OpenURL("http://mg.eamobile.com/?rId=1560");
							return;
						}
						if (theId == 14 && GameApp.USE_TRIAL_VERSION)
						{
							this.ProcessLocked(true);
						}
						return;
					}
				}
			}
		}

		public void ChangeMainMenuState()
		{
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		public void ProcessLocked(bool unlock)
		{
			this.mMenu.mState = MainMenu_State.State_QuitPrompt;
			string @string;
			if (unlock)
			{
				@string = TextManager.getInstance().getString(834);
			}
			else
			{
				@string = TextManager.getInstance().getString(836);
			}
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(835), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessUnlock);
		}

		public void ProcessUnlock(int theId)
		{
			if (theId == 1000 && GameApp.USE_TRIAL_VERSION)
			{
				GameApp.gApp.ToMarketPlace();
			}
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		public void ProcessUpdateLocked()
		{
			this.mMenu.mState = MainMenu_State.State_QuitPrompt;
			string @string = TextManager.getInstance().getString(62);
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(62), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessUpdateUnlock);
		}

		public void ProcessUpdateUnlock(int theId)
		{
			if (theId == 1000)
			{
				new MarketplaceDetailTask
				{
					ContentType = 1
				}.Show();
			}
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		public void UpdateLeaderboard()
		{
			this.mMenu.mState = MainMenu_State.State_QuitPrompt;
			string @string = TextManager.getInstance().getString(61);
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(61), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessLeaderboard);
		}

		public void ProcessLeaderboard(int theId)
		{
			if (theId == 1000)
			{
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.ShowLeaderBoards);
				GameApp.gApp.ToggleBambooTransition();
			}
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		public void UpdateAchievement()
		{
			this.mMenu.mState = MainMenu_State.State_QuitPrompt;
			string @string = TextManager.getInstance().getString(61);
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(61), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessAchievement);
		}

		public void ProcessAchievement(int theId)
		{
			if (theId == 1000)
			{
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.ShowAchievements);
				GameApp.gApp.ToggleBambooTransition();
			}
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public MainMenuButtonsWidget(MainMenu theMenu, GameApp theApp)
		{
			this.mApp = theApp;
			this.mMenu = theMenu;
			this.mWidth = this.mApp.GetScreenWidth();
			this.IMAGE_UI_MAINMENU_TIKI = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKI);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON);
			this.IMAGE_UI_MAINMENU_ADVENTURE_OFF_STATE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_ADVENTURE_OFF_STATE);
			this.IMAGE_UI_MAINMENU_ADVENTURE_CLICK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_ADVENTURE_CLICK);
			this.IMAGE_UI_MAINMENU_CHALLENGE_CLICK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_CLICK);
			this.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE);
			this.IMAGE_UI_MAINMENU_TIKI_CLICK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKI_CLICK);
			this.IMAGE_UI_MAINMENU_TIKI_OFF_STATE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKI_OFF_STATE);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDERBOT = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDERBOT);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT);
			this.IMAGE_UI_MM_FLOWER = Res.GetImageByID(ResID.IMAGE_UI_MM_FLOWER);
			this.IMAGE_UI_MM_FLOWERBOT = Res.GetImageByID(ResID.IMAGE_UI_MM_FLOWERBOT);
			this.IMAGE_UI_MAINMENU_LEADERBOARD_OFF_STATE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_LEADERBOARD_OFF_STATE);
			this.IMAGE_UI_MAINMENU_LEADERBOARD_CLICK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_LEADERBOARD_CLICK);
			this.IMAGE_UI_MAINMENU_ACHIEVEMENT_CLICK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_ACHIEVEMENT_CLICK);
			this.IMAGE_UI_MAINMENU_ACHIEVEMENT_OFF_STATE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_ACHIEVEMENT_OFF_STATE);
			this.IMAGE_UI_MAINMENU_HELP_CLICK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_HELP_CLICK);
			this.IMAGE_UI_MAINMENU_HELP_OFF_STATE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_HELP_OFF_STATE);
			this.mHeight = this.IMAGE_UI_MAINMENU_TIKI.GetHeight() + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI) - Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI)) + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER) + this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER.GetHeight() - Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI) + this.IMAGE_UI_MAINMENU_TIKI.GetHeight())));
			this.mButtonWidth = this.IMAGE_UI_MAINMENU_TIKI.GetWidth() * 2 + this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON.GetWidth() + this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER.GetWidth() / 2;
			this.mButtonOriginX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE)) - Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI)) - this.IMAGE_UI_MAINMENU_TIKI.GetWidth();
			this.mButtonOriginY = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE)) - Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI));
			this.mPlankHeight = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI)) + this.IMAGE_UI_MAINMENU_TIKI.GetHeight() - Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE));
			this.IMAGE_UI_MAINMENU_CHALLENGE_CLICK.GetWidth();
			this.IMAGE_UI_MAINMENU_CHALLENGE_CLICK.GetHeight();
			int num = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE));
			int num2 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE)) + 8;
			this.AddNewButtonFrame(0, this.IMAGE_UI_MAINMENU_ADVENTURE_OFF_STATE, this.IMAGE_UI_MAINMENU_ADVENTURE_CLICK, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_ADVENTURE_OFF_STATE)) - num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_ADVENTURE_OFF_STATE)) - num2);
			this.AddNewButtonFrame(1, this.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE, this.IMAGE_UI_MAINMENU_CHALLENGE_CLICK, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE)) - num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE)) - num2);
			this.AddNewButtonFrame(9, this.IMAGE_UI_MAINMENU_TIKI_OFF_STATE, this.IMAGE_UI_MAINMENU_TIKI_CLICK, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI_OFF_STATE)) - num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI_OFF_STATE)) - num2);
			this.AddNewButtonFrame(3, this.IMAGE_UI_MAINMENU_ACHIEVEMENT_OFF_STATE, this.IMAGE_UI_MAINMENU_ACHIEVEMENT_CLICK, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_ACHIEVEMENT_OFF_STATE)) - num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_ACHIEVEMENT_OFF_STATE)) - num2);
			this.AddNewButtonFrame(2, this.IMAGE_UI_MAINMENU_LEADERBOARD_OFF_STATE, this.IMAGE_UI_MAINMENU_LEADERBOARD_CLICK, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_LEADERBOARD_OFF_STATE)) - num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_LEADERBOARD_OFF_STATE)) - num2);
			this.AddNewButtonFrame(4, this.IMAGE_UI_MAINMENU_HELP_OFF_STATE, this.IMAGE_UI_MAINMENU_HELP_CLICK, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_HELP_OFF_STATE)) - num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_HELP_OFF_STATE)) - num2);
		}

		public override void Dispose()
		{
			if (this.mButtonFrames.Count > 0)
			{
				this.mButtonFrames.Clear();
			}
		}

		public int GetButtonCount()
		{
			return this.mButtonFrames.Count;
		}

		public void AddNewButtonFrame(int theButtonID, Image theButtonImage, Image theButtonDownImage, int x, int y)
		{
			MainMenuButtonsWidget.MenuButtonFrame menuButtonFrame = default(MainMenuButtonsWidget.MenuButtonFrame);
			if (this.mButtonFrames.Count == 0)
			{
				menuButtonFrame.mX = 0f;
				menuButtonFrame.mY = 0f;
			}
			else
			{
				menuButtonFrame.mX = Enumerable.Last<MainMenuButtonsWidget.MenuButtonFrame>(this.mButtonFrames).mX + (float)this.mButtonWidth;
				menuButtonFrame.mY = 0f;
			}
			menuButtonFrame.mButton = new ButtonWidget(theButtonID, this);
			menuButtonFrame.mButton.mButtonImage = theButtonImage;
			menuButtonFrame.mButton.mDownImage = theButtonDownImage;
			float num = (float)(theButtonDownImage.GetWidth() - theButtonImage.GetWidth()) / 2f;
			float num2 = (float)(theButtonDownImage.GetHeight() - theButtonImage.GetHeight()) / 2f;
			menuButtonFrame.mButton.Resize((int)(menuButtonFrame.mX + (float)this.mButtonOriginX + num) + x, (int)(menuButtonFrame.mY + (float)this.mButtonOriginY + num2) + y, theButtonDownImage.GetWidth(), theButtonDownImage.GetHeight());
			menuButtonFrame.mButton.mNormalRect = new Rect(0, 0, theButtonImage.GetWidth(), theButtonImage.GetHeight());
			menuButtonFrame.mButton.mDownRect = new Rect((int)num, (int)num2, (int)((float)theButtonDownImage.GetWidth() - num), (int)((float)theButtonDownImage.GetHeight() - num2));
			this.AddWidget(menuButtonFrame.mButton);
			this.mButtonFrames.Enqueue(menuButtonFrame);
			this.Resize((int)Enumerable.First<MainMenuButtonsWidget.MenuButtonFrame>(this.mButtonFrames).mX, 0, this.mButtonFrames.Count * this.mButtonWidth, this.mHeight);
		}

		public void DrawButtonFrame(Graphics g, MainMenuButtonsWidget.MenuButtonFrame theFrame)
		{
			int num = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI)) + this.IMAGE_UI_MAINMENU_TIKI.GetWidth() - Common._DS(11);
			int num2 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKI));
			int num3 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE)) - num2;
			g.ClearClipRect();
			g.SetClipRect((int)theFrame.mX, num3, this.mButtonWidth, this.mPlankHeight - Common._DS(15));
			int num4 = (int)theFrame.mX;
			int i = num3;
			bool flag = false;
			while (i <= num3 + this.mPlankHeight)
			{
				while ((float)num4 <= theFrame.mX + (float)this.mButtonWidth)
				{
					if (flag)
					{
						g.DrawImageMirror(this.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE, num4, i);
					}
					else
					{
						g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE, num4, i);
					}
					num4 += this.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE.GetWidth();
				}
				num4 = (int)theFrame.mX;
				i += this.IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE.GetHeight();
				flag = false;
			}
			g.ClearClipRect();
			int num5 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER)) - num2;
			int num6 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION)) - num;
			int num7 = this.mButtonWidth - this.IMAGE_UI_MAINMENU_TIKI.GetWidth() - this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION.GetWidth() + Common._DS(MainMenuButtonsWidget.aTRSideOffsetX);
			int num8 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION)) - num2;
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER, (int)theFrame.mX, (int)(theFrame.mY + (float)num5), this.mButtonWidth, this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER.GetHeight());
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION, (int)(theFrame.mX + (float)num6), (int)(theFrame.mY + (float)num8));
			g.DrawImageMirror(this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION, (int)(theFrame.mX + (float)num7), (int)(theFrame.mY + (float)num8));
			int num9 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDERBOT)) - num2;
			int num10 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT)) - num2;
			int num11 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT)) - num + Common._DS(-10);
			int num12 = this.mButtonWidth - this.IMAGE_UI_MAINMENU_TIKI.GetWidth() - this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION.GetWidth() + Common._DS(27);
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDERBOT, (int)theFrame.mX, (int)(theFrame.mY + (float)num9), this.mButtonWidth, this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER.GetHeight());
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT, (int)(theFrame.mX + (float)num11), (int)(theFrame.mY + (float)num10));
			g.DrawImageMirror(this.IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT, (int)(theFrame.mX + (float)num12), (int)(theFrame.mY + (float)num10));
			int num13 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON)) - num;
			int num14 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON)) - num2;
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON, (int)(theFrame.mX + (float)num13), (int)(theFrame.mY + (float)num14));
			int num15 = 0;
			g.DrawImageMirror(this.IMAGE_UI_MAINMENU_TIKI, (int)theFrame.mX, (int)(theFrame.mY + (float)num15));
			int num16 = this.mButtonWidth - this.IMAGE_UI_MAINMENU_TIKI.GetWidth();
			g.DrawImage(this.IMAGE_UI_MAINMENU_TIKI, (int)(theFrame.mX + (float)num16), (int)(theFrame.mY + (float)num15));
			g.DrawImage(this.IMAGE_UI_MM_FLOWER, (int)(theFrame.mX + (float)num13 - (float)Common._DS(MainMenuButtonsWidget.flowerXOff)), (int)(theFrame.mY + (float)num14 - (float)Common._DS(MainMenuButtonsWidget.flowerYOff)));
			g.DrawImageMirror(this.IMAGE_UI_MM_FLOWER, (int)(theFrame.mX + (float)num13 + (float)this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON.GetWidth() - (float)this.IMAGE_UI_MM_FLOWER.GetWidth() + (float)Common._DS(MainMenuButtonsWidget.flowerXOff)), (int)(theFrame.mY + (float)num14 - (float)Common._DS(MainMenuButtonsWidget.flowerYOff)));
			g.DrawImage(this.IMAGE_UI_MM_FLOWERBOT, (int)(theFrame.mX + (float)num13 - (float)Common._DS(MainMenuButtonsWidget.flowerXOff)), (int)(theFrame.mY + (float)num14 + (float)this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON.GetHeight() - (float)this.IMAGE_UI_MM_FLOWERBOT.GetHeight() + (float)Common._DS(MainMenuButtonsWidget.flowerYOff)));
			g.DrawImageMirror(this.IMAGE_UI_MM_FLOWERBOT, (int)(theFrame.mX + (float)num13 + (float)this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON.GetWidth() - (float)this.IMAGE_UI_MM_FLOWERBOT.GetWidth() + (float)Common._DS(MainMenuButtonsWidget.flowerXOff)), (int)(theFrame.mY + (float)num14 + (float)this.IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON.GetHeight() - (float)this.IMAGE_UI_MM_FLOWERBOT.GetHeight() + (float)Common._DS(MainMenuButtonsWidget.flowerYOff)));
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mApp != null && this.mApp.mMapScreen != null)
			{
				return;
			}
			if (this.mMenu != null && this.mMenu.mChallengeMenu != null)
			{
				return;
			}
			if (this.mButtonFrames.Count > 0)
			{
				foreach (MainMenuButtonsWidget.MenuButtonFrame theFrame in this.mButtonFrames)
				{
					this.DrawButtonFrame(g, theFrame);
				}
			}
		}

		public override void Update()
		{
			base.Update();
			int pageHorizontal = this.mMenu.mMainMenuButtonsScrollWidget.GetPageHorizontal();
			int num = 0;
			Queue<MainMenuButtonsWidget.MenuButtonFrame>.Enumerator enumerator = this.mButtonFrames.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (num == pageHorizontal)
				{
					enumerator.Current.mButton.SetDisabled(false);
				}
				else
				{
					enumerator.Current.mButton.SetDisabled(true);
				}
				num++;
			}
		}

		public void HideScrollButtons()
		{
			foreach (MainMenuButtonsWidget.MenuButtonFrame menuButtonFrame in this.mButtonFrames)
			{
				menuButtonFrame.mButton.SetVisible(false);
			}
		}

		public void ShowScrollButtons()
		{
			foreach (MainMenuButtonsWidget.MenuButtonFrame menuButtonFrame in this.mButtonFrames)
			{
				menuButtonFrame.mButton.SetVisible(true);
			}
		}

		public int GetNumButtons()
		{
			return this.mButtonFrames.Count;
		}

		public GameApp mApp;

		public MainMenu mMenu;

		public int mCurrentlySelectedButton;

		public Queue<MainMenuButtonsWidget.MenuButtonFrame> mButtonFrames = new Queue<MainMenuButtonsWidget.MenuButtonFrame>();

		public int mButtonWidth;

		public int mPlankHeight;

		public int mButtonOriginX;

		public int mButtonOriginY;

		public int mVisibleButtonX;

		private Image IMAGE_UI_MAINMENU_TIKI;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_BORDER;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_BUTTON;

		private Image IMAGE_UI_MAINMENU_ADVENTURE_OFF_STATE;

		private Image IMAGE_UI_MAINMENU_ADVENTURE_CLICK;

		private Image IMAGE_UI_MAINMENU_CHALLENGE_CLICK;

		private Image IMAGE_UI_MAINMENU_CHALLENGE_OFF_STATE;

		private Image IMAGE_UI_MAINMENU_LEADERBOARD_OFF_STATE;

		private Image IMAGE_UI_MAINMENU_LEADERBOARD_CLICK;

		private Image IMAGE_UI_MAINMENU_ACHIEVEMENT_CLICK;

		private Image IMAGE_UI_MAINMENU_ACHIEVEMENT_OFF_STATE;

		private Image IMAGE_UI_MAINMENU_HELP_CLICK;

		private Image IMAGE_UI_MAINMENU_HELP_OFF_STATE;

		private Image IMAGE_UI_MAINMENU_TIKI_CLICK;

		private Image IMAGE_UI_MAINMENU_TIKI_OFF_STATE;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_WOODTEXTURE;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_DECORATION;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_BORDERBOT;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_DECORATIONBOT;

		private Image IMAGE_UI_MM_FLOWER;

		private Image IMAGE_UI_MM_FLOWERBOT;

		private static int aTRSideOffsetX = 20;

		private static int flowerXOff = 20;

		private static int flowerYOff = 20;

		public struct MenuButtonFrame
		{
			public ButtonWidget mButton;

			public ButtonWidget mAttachButton;

			public float mX;

			public float mY;
		}
	}
}
