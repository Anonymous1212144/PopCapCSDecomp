using System;
using System.Text;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	internal class OptionsDialog : ZumaDialog, SliderListener
	{
		public OptionsDialog(bool inGame)
			: base(2, true, "", "", "", 0)
		{
			this.mLanguageButton = null;
			this.mInGame = inGame;
			this.mMusicEnabled = false;
			this.mMusicSliderOn = false;
			this.mHeightPad = ZumasRevenge.Common._S(ZumasRevenge.Common._M(272));
			this.mState = OptionsDialog.OptionState.OptionState_None;
			this.mAllowDrag = false;
			this.mClip = false;
			this.LoadResources();
			this.InitMusicSlider();
			this.InitSfxSlider();
			this.InitColorblindSlider();
			this.InitButtons();
			this.InitSize();
		}

		~OptionsDialog()
		{
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			if (this.mInGame)
			{
				this.LayoutAdventureDialog();
				return;
			}
			this.LayoutMainMenuDialog();
		}

		public override void Update()
		{
			base.Update();
			if (this.mMusicVolumeSlider.mDisabled && !GameApp.gApp.mMusicInterface.m_isUserMusicOn)
			{
				this.mMusicVolumeSlider.mDisabled = false;
				this.mMusicEnabled = true;
				double musicVolume = GameApp.gApp.GetMusicVolume();
				this.mOriginMusicVolume = musicVolume;
				this.mMusicVolumeSlider.SetValue(musicVolume);
			}
			else if (!this.mMusicVolumeSlider.mDisabled && GameApp.gApp.mMusicInterface.m_isUserMusicOn)
			{
				this.mMusicVolumeSlider.mDisabled = true;
				this.mMusicEnabled = false;
				this.mMusicVolumeSlider.SetValue(0.0);
			}
			if (this.mState == OptionsDialog.OptionState.OptionState_OptionToMainMenuPrompt)
			{
				this.SetVisible(false);
				return;
			}
			this.SetVisible(true);
		}

		public override void Draw(Graphics g)
		{
			if (GameApp.gApp.mCredits != null)
			{
				return;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_STROKE);
			Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
			Font fontByID3 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_CROWN);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_ACECROWN);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CROWN_HOLE);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_ADVENTURE);
			g.PushState();
			g.Translate(-this.mX, -this.mY);
			g.SetColor(0, 0, 0, 130);
			g.FillRect(ZumasRevenge.Common._S(-80), 0, GameApp.gApp.mWidth + ZumasRevenge.Common._S(160), GameApp.gApp.mHeight);
			g.PopState();
			base.Draw(g);
			if (this.mInGame)
			{
				g.SetFont(fontByID);
				g.SetColor(255, 255, 255);
				Board board = GameApp.gApp.GetBoard();
				if (GameApp.gApp.GetBoard().GauntletMode())
				{
					g.SetFont(fontByID2);
					g.SetColorizeImages(true);
					g.SetColor(Color.White);
					int num = ZumasRevenge.Common._S(100);
					int theY = ZumasRevenge.Common._S(120);
					if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_SP || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_SPC)
					{
						num -= 35;
					}
					string @string = TextManager.getInstance().getString(669);
					float num2 = (float)fontByID2.StringWidth(@string);
					g.DrawString(@string, num, theY);
					string theString = SexyFramework.Common.CommaSeperate(board.mScore);
					fontByID2.StringWidth(theString);
					float num3 = (float)num + num2 + (float)ZumasRevenge.Common._DS(20);
					g.DrawString(theString, (int)num3, theY);
					float num4 = (float)ZumasRevenge.Common._S(190);
					float num5 = (float)ZumasRevenge.Common._S(150);
					float num6 = (float)ZumasRevenge.Common._DS(15);
					float num7 = (float)ZumasRevenge.Common._DS(10);
					float num8 = 0.5f;
					float num9 = (float)imageByID.GetWidth() * num8;
					float num10 = (float)imageByID.GetHeight() * num8;
					float num11 = (float)imageByID.GetWidth() * num8;
					imageByID.GetHeight();
					g.DrawImage(imageByID2, ZumasRevenge.Common._S(40), ZumasRevenge.Common._S(129));
					g.DrawImage(imageByID, (int)num4, (int)num5, (int)num9, (int)num10);
					string theString2 = SexyFramework.Common.UCommaSeparate((uint)board.mLevel.mChallengePoints);
					g.DrawString(theString2, (int)(num4 + num9 + num7), (int)(num5 + (float)fontByID2.mAscent));
					g.DrawImage(imageByID3, (int)num4, (int)(num5 + num10 + num6), (int)num11, (int)num10);
					string theString3 = SexyFramework.Common.UCommaSeparate((uint)board.mLevel.mChallengeAcePoints);
					g.DrawString(theString3, (int)(num4 + num11 + num7), (int)(num5 + num10 + num6 + (float)fontByID2.mAscent));
					string text = JeffLib.Common.UpdateToTimeStr(board.mLevel.mGauntletCurTime);
					string text2 = JeffLib.Common.UpdateToTimeStr(((GameApp)GlobalMembers.gSexyApp).GetLevelMgr().mGauntletSessionLength);
					string text3 = string.Format(" {0} / {1}", text, text2);
					g.DrawString(TextManager.getInstance().getString(679) + text3, ZumasRevenge.Common._S(45), ZumasRevenge.Common._S(310));
					if (GameApp.gApp.mUserProfile != null && GameApp.gApp.mBoard != null && GameApp.gApp.mBoard.mLevel != null)
					{
						float num12 = (float)ZumasRevenge.Common._S(60);
						float num13 = (float)ZumasRevenge.Common._S(132);
						string text4 = "";
						Image image;
						if (board.mScore < board.mLevel.mChallengePoints)
						{
							image = imageByID4;
							text4 = TextManager.getInstance().getString(681);
						}
						else if (board.mScore < board.mLevel.mChallengeAcePoints)
						{
							image = imageByID;
						}
						else
						{
							image = imageByID3;
						}
						if (image != null)
						{
							if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_RU)
							{
								g.DrawImage(image, (int)num12 - 24, (int)num13 - 28, (int)((double)imageByID4.GetWidth() * 1.35), (int)((double)imageByID4.GetHeight() * 1.35));
							}
							else if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_SP || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_SPC || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_PL)
							{
								g.DrawImage(image, (int)num12 - 13, (int)num13 - 15, (int)((double)imageByID4.GetWidth() * 1.2), (int)((double)imageByID4.GetHeight() * 1.2));
							}
							else
							{
								g.DrawImage(image, (int)num12, (int)num13, imageByID4.GetWidth(), imageByID4.GetHeight());
							}
							g.SetColor(136, 156, 43, 255);
							g.SetFont(fontByID3);
							g.GetFont().StringWidth(text4);
							float num14 = num12 + (float)ZumasRevenge.Common._S(7);
							float num15 = num13 + (float)ZumasRevenge.Common._S(38);
							g.PushState();
							g.SetScale(0.7f, 0.7f, num14, num15);
							g.WriteWordWrapped(new Rect((int)num14 + ZumasRevenge.Common._S(15), (int)num15, imageByID4.GetWidth(), imageByID4.GetHeight()), text4, -1, 0);
							g.PopState();
						}
					}
					g.SetColorizeImages(false);
					return;
				}
				g.SetFont(fontByID2);
				g.SetColorizeImages(true);
				g.SetColor(Color.White);
				g.DrawString(TextManager.getInstance().getString(670), ZumasRevenge.Common._S(120), ZumasRevenge.Common._S(120));
				int num16 = ZumasRevenge.Common._S(80);
				int num17 = ZumasRevenge.Common._S(130);
				g.DrawImage(imageByID5, num16, num17);
				int num18 = board.GetNumLives() - 1;
				if (num18 < 0)
				{
					num18 = 0;
				}
				else if (num18 > 99)
				{
					num18 = 99;
				}
				string theString4 = string.Format("x {0}", num18);
				fontByID2.StringWidth(theString4);
				float num19 = (float)(num16 + imageByID5.GetWidth() + ZumasRevenge.Common._S(10));
				float num20 = (float)(num17 + imageByID5.GetHeight() / 2);
				g.DrawString(theString4, (int)num19, (int)num20);
				if (GameApp.gApp.mBoard.mGameState != GameState.GameState_Losing)
				{
					string string2 = TextManager.getInstance().getString(679);
					float num21 = (float)fontByID2.StringWidth(string2);
					Level mLevel = board.mLevel;
					int num22 = 65;
					if (mLevel != null && mLevel.mBoss == null && mLevel.mIndex != num22)
					{
						StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(671));
						stringBuilder.Replace("$1", JeffLib.Common.UpdateToTimeStr(mLevel.mParTime));
						string theString5 = stringBuilder.ToString();
						float num23 = (float)fontByID2.StringWidth(theString5);
						g.DrawString(theString5, ZumasRevenge.Common._S(120) + (int)(num21 - num23) / 2, (int)num20 + ZumasRevenge.Common._S(160));
					}
					string theString6;
					if (board.mGameState != GameState.GameState_Playing)
					{
						theString6 = JeffLib.Common.UpdateToTimeStr(board.mEndLevelStats.mTimePlayed);
					}
					else
					{
						theString6 = JeffLib.Common.UpdateToTimeStr(board.mStateCount - board.mIgnoreCount);
					}
					float num24 = (float)fontByID2.StringWidth(theString6);
					g.DrawString(string2, ZumasRevenge.Common._S(120), (int)num20 + ZumasRevenge.Common._S(80));
					g.DrawString(theString6, ZumasRevenge.Common._S(120) + (int)(num21 - num24) / 2, (int)num20 + ZumasRevenge.Common._S(120));
				}
			}
		}

		public virtual void DrawAll(ref ModalFlags theFlags, Graphics g)
		{
			g.PushState();
			g.Translate(-this.mX, -this.mY);
			g.SetColor(0, 0, 0, 130);
			g.FillRect(ZumasRevenge.Common._S(-80), 0, GameApp.gApp.mWidth + ZumasRevenge.Common._S(160), GameApp.gApp.mHeight);
			g.PopState();
			base.DrawAll(theFlags, g);
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mMusicVolumeSlider);
			this.AddWidget(this.mSfxVolumeSlider);
			this.AddWidget(this.mHelpButton);
			this.AddWidget(this.mMainMenuButton);
			this.AddWidget(this.mBackToGame);
			this.AddWidget(this.mCreditsButton);
			this.AddWidget(this.mColorBlindSlider);
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mMusicVolumeSlider);
			this.RemoveWidget(this.mSfxVolumeSlider);
			this.RemoveWidget(this.mHelpButton);
			this.RemoveWidget(this.mMainMenuButton);
			this.RemoveWidget(this.mBackToGame);
			this.RemoveWidget(this.mCreditsButton);
			this.RemoveWidget(this.mColorBlindSlider);
		}

		public void SliderVal(int theId, double theVal)
		{
			switch (theId)
			{
			case 0:
				if (GameApp.gApp.mMusicInterface.isPlayingUserMusic() && theVal > 0.0)
				{
					GameApp.gApp.mMusicInterface.stopUserMusic();
				}
				this.SetMusicSlider(theVal);
				return;
			case 1:
				this.SetSfxSlider(theVal);
				return;
			default:
				return;
			}
		}

		public void ProcessYesNo(int theId)
		{
			GameApp gameApp = (GameApp)GlobalMembers.gSexyApp;
			if (theId == 1000)
			{
				gameApp.KillDialog(this);
				gameApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.DoDeferredEndGame);
				gameApp.ToggleBambooTransition();
				gameApp.mMusic.StopAll();
			}
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			GameApp gameApp = (GameApp)GlobalMembers.gSexyApp;
			if (theId == 3)
			{
				this.mState = OptionsDialog.OptionState.OptionState_OptionToMainMenuPrompt;
				int width_pad = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(20));
				string @string;
				if (((GameApp)GlobalMembers.gSexyApp).GetBoard().GauntletMode())
				{
					@string = TextManager.getInstance().getString(449);
				}
				else if (GameApp.gApp.GetBoard().IronFrogMode())
				{
					@string = TextManager.getInstance().getString(450);
				}
				else
				{
					@string = TextManager.getInstance().getString(451);
				}
				this.SetVisible(false);
				gameApp.DoYesNoDialog(TextManager.getInstance().getString(448), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, ZumasRevenge.Common._S(ZumasRevenge.Common._M(50)), 1, width_pad);
				gameApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessYesNo);
				this.SetVisible(true);
				return;
			}
			if (theId == 8)
			{
				this.mState = OptionsDialog.OptionState.OptionState_None;
				GameApp.gApp.FinishOptionsDialog(true);
				return;
			}
			if (theId == 2)
			{
				this.mState = OptionsDialog.OptionState.OptionState_Help;
				Board board = GameApp.gApp.GetBoard();
				GameApp.gApp.mColorblind = this.mColorBlindSlider.IsOn();
				if (board != null && board.GauntletMode())
				{
					board.ShowChallengeHelpScreen();
					return;
				}
				GameApp.gApp.mGenericHelp = new GenericHelp();
				GameApp.gApp.AddDialog(GameApp.gApp.mGenericHelp);
				return;
			}
			else
			{
				if (theId == 5)
				{
					this.mState = OptionsDialog.OptionState.OptionState_Credits;
					GameApp.gApp.DoCredits(true);
					return;
				}
				if (theId == 7)
				{
					this.mState = OptionsDialog.OptionState.OptionState_Legal;
					GameApp.gApp.ShowLegal();
				}
				return;
			}
		}

		public void DetectMusicSettings()
		{
			this.mMusicEnabled = GameApp.gApp.MusicEnabled();
			double num = (this.mMusicEnabled ? GameApp.gApp.GetMusicVolume() : 0.0);
			this.mOriginMusicVolume = num;
			this.mMusicVolumeSlider.SetValue(num);
			this.SetMusicSlider(num);
		}

		private void LoadResources()
		{
			if (GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame"))
			{
				return;
			}
			if (!GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.Shutdown();
			}
		}

		private void InitMusicSlider()
		{
			this.mMusicVolumeSlider = new ZumaSlider(0, this, TextManager.getInstance().getString(672));
			this.mMusicVolumeSlider.mFeedbackSoundID = Res.GetSoundByID(ResID.SOUND_BALLCLICK1);
			this.DetectMusicSettings();
		}

		private void InitSfxSlider()
		{
			this.mSfxVolumeSlider = new ZumaSlider(1, this, TextManager.getInstance().getString(673));
			this.mSfxVolumeSlider.mFeedbackSoundID = Res.GetSoundByID(ResID.SOUND_BALLCLICK1);
			this.mOriginSfxVolume = GlobalMembers.gSexyApp.GetSfxVolume();
			this.mSfxVolumeSlider.SetValue(this.mOriginSfxVolume);
		}

		private void InitColorblindSlider()
		{
			this.mColorBlindSlider = new ZumaSlideBox(this, 4, TextManager.getInstance().getString(680));
			this.mOriginColorBlind = GameApp.gApp.mColorblind;
			this.mColorBlindSlider.SetOnOff(this.mOriginColorBlind);
		}

		private void InitButtons()
		{
			this.mMainMenuButton = this.InitButton(3, TextManager.getInstance().getString(676));
			this.mHelpButton = this.InitButton(2, TextManager.getInstance().getString(674));
			this.mBackToGame = this.InitButton(8, TextManager.getInstance().getString(675));
			this.mCreditsButton = this.InitButton(5, TextManager.getInstance().getString(677));
			this.HideButton(this.mMainMenuButton, !this.mInGame);
			this.HideButton(this.mCreditsButton, this.mInGame);
		}

		private void InitSize()
		{
			if (this.mInGame)
			{
				this.Resize(0, 0, ZumasRevenge.Common._S(ZumasRevenge.Common._M(690)), ZumasRevenge.Common._S(ZumasRevenge.Common._M1(230)) + this.mHeightPad);
				return;
			}
			this.Resize(0, 0, ZumasRevenge.Common._S(ZumasRevenge.Common._M(600)), ZumasRevenge.Common._S(ZumasRevenge.Common._M1(230)) + this.mHeightPad - 80);
		}

		private ButtonWidget InitButton(int inButtonID, string inButtonName)
		{
			ButtonWidget buttonWidget = ZumasRevenge.Common.MakeButton(inButtonID, this, inButtonName);
			buttonWidget.mDoFinger = true;
			return buttonWidget;
		}

		private void HideButton(ButtonWidget inButton, bool inHide)
		{
			inButton.SetVisible(!inHide);
			inButton.mDisabled = inHide;
		}

		private void LayoutMainMenuDialog()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			int num = base.GetLeft() - this.mX;
			int num2 = base.GetTop() - this.mY;
			int width = base.GetWidth();
			int num3 = width / 2;
			int num4 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(8));
			int theY = num2 - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(70));
			this.mMusicVolumeSlider.Resize(num + num4 / 2 + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(10)), theY, num3 - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(24)), ZumasRevenge.Common._DS(ZumasRevenge.Common._M2(94)));
			this.mSfxVolumeSlider.Layout(17411, this.mMusicVolumeSlider, ZumasRevenge.Common._DS(ZumasRevenge.Common._M(25)), 0, 0, 0);
			this.mColorBlindSlider.Resize((this.mMusicVolumeSlider.mX + this.mSfxVolumeSlider.mX + this.mSfxVolumeSlider.mWidth) / 2 - imageByID.GetWidth() / 2, this.mMusicVolumeSlider.mY + ZumasRevenge.Common._S(45), imageByID.GetWidth(), imageByID.GetHeight());
			int num5 = ZumasRevenge.Common._DS(10);
			int theX = (this.mWidth - (OptionsDialog.OPTIONS_BUTTON_WIDTH * 3 + num5)) / 2;
			this.mCreditsButton.Resize(theX, this.mColorBlindSlider.mY + ZumasRevenge.Common._S(90), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mHelpButton.Resize(this.mCreditsButton.mX + this.mCreditsButton.mWidth + num5, this.mCreditsButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.HideButton(this.mHelpButton, true);
			int num6 = 200;
			this.mBackToGame.Resize(this.mHelpButton.mX + num6, this.mHelpButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mMainMenuButton.Layout(16387, this.mBackToGame, 0, 0, 0, 0);
		}

		private void LayoutAdventureDialog()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			int num = base.GetLeft() - this.mX;
			int num2 = base.GetTop() - this.mY;
			int width = base.GetWidth();
			int num3 = width / 2;
			ZumasRevenge.Common._S(ZumasRevenge.Common._M(8));
			int num4 = num2 - ZumasRevenge.Common._S(ZumasRevenge.Common._M(40));
			this.mMusicVolumeSlider.Resize(num + ZumasRevenge.Common._S(ZumasRevenge.Common._M(340)), num4 - 7, num3 - ZumasRevenge.Common._S(ZumasRevenge.Common._M1(24)), ZumasRevenge.Common._S(ZumasRevenge.Common._M2(44)));
			this.mSfxVolumeSlider.Layout(4611, this.mMusicVolumeSlider, ZumasRevenge.Common._S(ZumasRevenge.Common._M(0)), ZumasRevenge.Common._S(37), 0, 0);
			this.mColorBlindSlider.Resize(this.mSfxVolumeSlider.mX - ZumasRevenge.Common._S(80), this.mSfxVolumeSlider.mY + ZumasRevenge.Common._S(45), imageByID.GetWidth(), imageByID.GetHeight());
			this.mHelpButton.Resize(10 + this.mSfxVolumeSlider.mX + ZumasRevenge.Common._S(115), this.mColorBlindSlider.mY + ZumasRevenge.Common._S(100), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mBackToGame.Resize(10 + this.mHelpButton.mX - this.mHelpButton.mWidth - ZumasRevenge.Common._S(50), this.mHelpButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mMainMenuButton.Resize(10 + this.mBackToGame.mX - this.mBackToGame.mWidth + ZumasRevenge.Common._S(-50), this.mBackToGame.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
		}

		private void SetMusicSlider(double inVolume)
		{
			if (this.mMusicEnabled)
			{
				GameApp.gApp.SetMusicVolume(inVolume);
			}
			if (this.mMusicVolumeSlider.mDragging)
			{
				return;
			}
			this.mMusicSliderOn = this.mMusicEnabled && inVolume > 0.0;
			this.mMusicVolumeSlider.Label = (this.mMusicSliderOn ? TextManager.getInstance().getString(672) : TextManager.getInstance().getString(682));
			this.mMusicVolumeSlider.mDisabled = !this.mMusicEnabled;
			GameApp.gApp.mMusic.Enable(this.mMusicSliderOn);
		}

		private void SetSfxSlider(double inVolume)
		{
			GameApp.gApp.SetSfxVolume(inVolume);
		}

		public void SliderReleased(int theId, double theVal)
		{
		}

		public void OnLegalInfoHided()
		{
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		public void OnCreditsHided()
		{
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		public void OnHelpHided()
		{
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		public void ProcessHardwareBackButton()
		{
			switch (this.mState)
			{
			case OptionsDialog.OptionState.OptionState_OptionToMainMenuPrompt:
			{
				this.mState = OptionsDialog.OptionState.OptionState_None;
				Dialog dialog = GameApp.gApp.GetDialog(1);
				if (dialog != null)
				{
					dialog.ButtonDepress(1001);
				}
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			case OptionsDialog.OptionState.OptionState_Credits:
				this.mState = OptionsDialog.OptionState.OptionState_None;
				GameApp.gApp.ReturnFromCredits();
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			case OptionsDialog.OptionState.OptionState_Help:
			{
				this.mState = OptionsDialog.OptionState.OptionState_None;
				Board board = GameApp.gApp.GetBoard();
				if (board != null && board.GauntletMode())
				{
					board.ChallengeHelpClosed();
				}
				else
				{
					GameApp.gApp.mGenericHelp.ButtonDepress(0);
				}
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			case OptionsDialog.OptionState.OptionState_Legal:
				GameApp.gApp.mLegalInfo.ProcessHardwareBackButton();
				if (GameApp.gApp.mLegalInfo == null)
				{
					this.mState = OptionsDialog.OptionState.OptionState_None;
					return;
				}
				break;
			default:
				this.mState = OptionsDialog.OptionState.OptionState_None;
				this.SetMusicSlider(this.mOriginMusicVolume);
				this.SetSfxSlider(this.mOriginSfxVolume);
				GameApp.gApp.FinishOptionsDialog(false);
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				break;
			}
		}

		private const double MUSIC_SLIDER_THRESHOLD = 0.01;

		private static int OPTIONS_BUTTON_WIDTH = ZumasRevenge.Common._DS(372);

		private static int OPTIONS_BUTTON_HEIGHT = ZumasRevenge.Common._DS(157);

		private static int INCLUDE_LANGUAGE_BUTTON = 0;

		public ZumaSlider mMusicVolumeSlider;

		public ZumaSlider mSfxVolumeSlider;

		public ZumaSlideBox mColorBlindSlider;

		public double mOriginMusicVolume;

		public double mOriginSfxVolume;

		public bool mOriginColorBlind;

		public ButtonWidget mHelpButton;

		public ButtonWidget mMainMenuButton;

		public ButtonWidget mBackToGame;

		public ButtonWidget mCreditsButton;

		public ButtonWidget mLanguageButton;

		public bool mInGame;

		public bool mMusicEnabled;

		public bool mMusicSliderOn;

		public int mHeightPad;

		protected OptionsDialog.OptionState mState;

		public enum ControlId
		{
			OptionsDialog_MusicVolume,
			OptionsDialog_SfxVolume,
			OptionsDialog_Help,
			OptionsDialog_ToMainMenu,
			OptionsDialog_Colorblind,
			OptionsDialog_Credits,
			OptionsDialog_Language,
			OptionsDialog_Legal,
			OptionsDialog_BackToGame
		}

		protected enum OptionState
		{
			OptionState_BackToMainMenuPrompt,
			OptionState_OptionToMainMenuPrompt,
			OptionState_Credits,
			OptionState_Help,
			OptionState_Legal,
			OptionState_None
		}
	}
}
