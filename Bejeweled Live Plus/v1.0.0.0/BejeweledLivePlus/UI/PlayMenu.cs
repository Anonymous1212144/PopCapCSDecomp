using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class PlayMenu : Widget, Bej3ButtonListener, ButtonListener
	{
		public PlayMenu(MainMenu theMainMenu)
		{
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, false);
			if (this.mClassicBackground != null)
			{
				this.mClassicBackground.Dispose();
			}
		}

		public override void Update()
		{
			base.Update();
			if (!this.mMainMenu.mLoaded)
			{
				return;
			}
			this.mVisible = this.mShowPct > 0.0 && this.mSelectCenterPct != 1.0;
			bool mMouseVisible = true;
			for (int i = 0; i < 8; i++)
			{
				if (this.mModeButtons[i].mScale.IsDoingCurve())
				{
					mMouseVisible = false;
				}
			}
			this.mBackButton.mMouseVisible = mMouseVisible;
			this.mQuitButton.mMouseVisible = mMouseVisible;
			for (int j = 0; j < 8; j++)
			{
				this.mModeButtons[j].mMouseVisible = mMouseVisible;
			}
			if (!this.mVisible)
			{
				for (int k = 0; k < 8; k++)
				{
					this.mModeButtons[k].mImage.Release();
					this.mModeButtons[k].mVisible = false;
				}
			}
			else
			{
				for (int l = 0; l < 8; l++)
				{
					this.mModeButtons[l].mVisible = true;
				}
				if (!GlobalMembersResourcesWP.POPANIM_ANIMS_100CREST.mAnimRunning)
				{
					GlobalMembersResourcesWP.POPANIM_ANIMS_100CREST.Play("idle");
				}
				GlobalMembersResourcesWP.POPANIM_ANIMS_100CREST.Update();
			}
			if (this.mShowPct == 0.0)
			{
				return;
			}
			if (this.mShowPct.GetOutFinalVal() == 1.0 && this.mShowPct.HasBeenTriggered())
			{
				this.mWidgetManager.BringToFront(this);
			}
			if (this.IsClosing())
			{
				this.StopHoverText();
			}
			int num = GlobalMembers.MS(180);
			int num2 = GlobalMembers.MS(180);
			if (GlobalMembers.gApp.mBoard != null && this.mSelectCenterPct == 1.0 && GlobalMembers.gApp.mBoard.mBackground != null && !GlobalMembers.gApp.mBoard.mBackground.mVisible && GlobalMembers.gApp.mBoard.mHyperspace == null)
			{
				GlobalMembers.gApp.mBoard.mBackground.mVisible = true;
			}
			if (GlobalMembers.gApp.mQuestMenu != null && this.mSelectCenterPct == 1.0 && !GlobalMembers.gApp.mQuestMenu.mBackground.mVisible && GlobalMembers.gApp.mBoard == null)
			{
				GlobalMembers.gApp.mQuestMenu.mBackground.mVisible = true;
			}
			for (int m = 0; m < 8; m++)
			{
				FPoint impliedObject = default(FPoint);
				if (m < this.MINIGAME_START_ID)
				{
					impliedObject = new FPoint((float)GlobalMembers.MS(460), (float)GlobalMembers.MS(250));
					if (m % 2 == 1)
					{
						impliedObject.mX = (float)this.mWidth - impliedObject.mX;
					}
					if (m >= 2)
					{
						impliedObject.mY += (float)GlobalMembers.MS(650);
					}
					impliedObject += (impliedObject - new FPoint((float)(this.mWidth / 2), (float)(this.mHeight / 2)) * GlobalMembers.M(1.25)) * (1.0 - this.mShowPct);
				}
				else
				{
					this.mModeButtons[m].mColor = new Color(GlobalMembers.M(16742399));
					this.mModeButtons[m].mFontColor = new Color(GlobalMembers.M(2236962), GlobalMembers.M(160));
					int num3 = m - this.MINIGAME_START_ID;
					impliedObject = new FPoint((float)GlobalMembers.MS(711), (float)GlobalMembers.MS(420));
					if (num3 % 2 == 1)
					{
						impliedObject.mX = (float)this.mWidth - impliedObject.mX + (float)GlobalMembers.MS(4);
					}
					int num4;
					int num5;
					if (num3 >= 2)
					{
						impliedObject.mY += (float)GlobalMembers.MS(295);
						num4 = GlobalMembers.M(3);
						num5 = GlobalMembers.M(4);
					}
					else
					{
						num4 = GlobalMembers.M(3);
						num5 = GlobalMembers.M(3);
					}
					impliedObject.mX += (float)((double)num4 * (1.0 - this.mShowPct) * (double)(impliedObject.mX - (float)(this.mWidth / 2)) * GlobalMembers.M(1.25));
					impliedObject.mY += (float)((double)num5 * (1.0 - this.mShowPct) * (double)(impliedObject.mY - (float)(this.mHeight / 2)) * GlobalMembers.M(1.25));
					int num6 = 0;
					switch (m)
					{
					case 4:
						num6 = 0;
						break;
					case 5:
						num6 = 1;
						break;
					case 6:
						num6 = 2;
						break;
					case 7:
						num6 = 3;
						break;
					default:
						GlobalMembers.DBG_ASSERT(false);
						break;
					}
					bool flag = !GlobalMembers.gApp.mProfile.mEndlessModeUnlocked[num6];
					this.mModeButtons[m].mTextIsQuestionMark = flag;
					this.mModeButtons[m].mLocked = flag;
					this.mModeButtons[m].mDoBob = false;
					this.mModeButtons[m].mColor = new Color(GlobalMembers.M(11184810));
				}
				if (m == this.mSelectedMode)
				{
					FPoint impliedObject2 = new FPoint((float)(this.mWidth / 2), (float)(this.mHeight / 2));
					impliedObject = impliedObject2 * this.mSelectCenterPct + impliedObject * (1.0 - this.mSelectCenterPct);
				}
				this.mModeButtons[m].Resize((int)impliedObject.mX - num / 2, (int)impliedObject.mY - num2 / 2, num, num2);
				this.mModeButtons[m].mOffset = impliedObject - new FPoint((float)((int)impliedObject.mX), (float)((int)impliedObject.mY));
			}
			this.mBackButton.mAlpha = (float)this.mBackBtnAlpha;
			this.mQuitButton.mAlpha = (float)this.mBackBtnAlpha;
			if (this.mShowPct == 1.0 && this.mVisible)
			{
				if (this.mHoverTextAlpha == 0.0)
				{
					if (GlobalMembers.gApp.mProfile.mStats[0] == 0 && !this.mIntroTextAlpha.IsDoingCurve() && this.mIntroTextAlpha == 0.0)
					{
						this.mIntroTextAlpha.SetCurve("b;0,1,0.01,1,####         ~~###");
					}
				}
				else if (!this.mIntroTextAlpha.IsDoingCurve())
				{
					this.mIntroTextAlpha.SetCurveMult("b;0,1,0.05,1,~###         ~####");
				}
			}
			else
			{
				this.mIntroTextAlpha.SetConstant(0.0);
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			if (this.mShowPct == 0.0)
			{
				return;
			}
			base.DeferOverlay(0);
			for (int i = 0; i < 4; i++)
			{
				this.mModeButtons[i].mFontColor = new Color(BlendColorsForPlayMenu.BlendColors(this.mBaseModeFontColor, GlobalMembers.M(16711765), (float)this.mHoverTextConnectorAlpha[i], false));
				if (this.mIntroTextAlpha != null && i == 0)
				{
					this.mModeButtons[i].mFontColor = new Color(BlendColorsForPlayMenu.BlendColors(this.mModeButtons[i].mFontColor.ToInt(), GlobalMembers.M(16736400), (0.5f + (float)Math.Cos((double)((float)this.mUpdateCnt * GlobalMembers.M(0.075f))) * 0.5f) * (float)this.mIntroTextAlpha, false));
				}
				this.mModeButtons[i].mExtraFontScaling = (float)this.mHoverTextConnectorAlpha[i] * GlobalMembers.M(1f);
			}
			if ((double)((float)this.mHoverTextAlpha) > 0.0 || this.mIntroTextAlpha != null)
			{
				g.SetColor(new Color(GlobalMembers.M(16777215), (int)(GlobalMembers.M(255f) * (float)this.mHoverTextAlpha)));
				g.SetColorizeImages(true);
				int num = GlobalMembers.M(0);
				if (this.mHoverTextLocked)
				{
					g.SetFont(GlobalMembersResources.FONT_DIALOG);
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("MAIN", new Color(GlobalMembers.M(16737894)));
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("GLOW", new Color(GlobalMembers.M(0)));
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("OUTLINE", new Color(GlobalMembers.M(0)));
					g.WriteString(GlobalMembers._ID("SECRET GAME", 352), this.mWidth / 2, this.mHeight / 2 + GlobalMembers.MS(-40), GlobalMembers.M(0), GlobalMembers.M(0));
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("MAIN");
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("GLOW");
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("OUTLINE");
					num = GlobalMembers.M(12);
				}
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("GLOW", new Color(GlobalMembers.M(0)));
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("OUTLINE", new Color(GlobalMembers.M(0)));
				g.WriteString(this.mHoverText, this.mWidth / 2, this.mHeight / 2 + GlobalMembers.S(GlobalMembers.M(10) + num), GlobalMembers.M(0), GlobalMembers.M(0));
				if (this.mIntroTextAlpha != null)
				{
					g.SetColor(this.mIntroTextAlpha);
					g.WriteString(GlobalMembers._ID("New to Bejeweled? Try Classic mode", 566), this.mWidth / 2, this.mHeight / 2 + GlobalMembers.S(GlobalMembers.M(10) + num), GlobalMembers.M(0), GlobalMembers.M(0));
				}
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("GLOW");
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("OUTLINE");
				g.SetColorizeImages(false);
				g.SetColor(new Color(-1));
			}
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			int num = 0;
			for (int i = 0; i < BejeweledLivePlusAppConstants.NUM_QUEST_SETS; i++)
			{
				for (int j = 0; j < BejeweledLivePlusAppConstants.QUESTS_PER_SET; j++)
				{
					if (!GlobalMembers.gApp.mProfile.mQuestsCompleted[i, j])
					{
						num++;
					}
				}
			}
			if (num == 0)
			{
				float num2 = 0f;
				for (int k = 0; k < 8; k++)
				{
					num2 = Math.Max(num2, (float)this.mModeButtons[k].mScale / 0.2f);
				}
				this.mModeButtons[3].mHasCrest = true;
				GlobalMembersResourcesWP.POPANIM_ANIMS_100CREST.mColor = new Color(255, 255, 255, (int)(Math.Max(0.0, 1.0 - ((double)num2 - 1.0) * GlobalMembers.M(0.5) - (1.0 - this.mShowPct) * GlobalMembers.M(3.0)) * 255.0));
				return;
			}
			this.mModeButtons[3].mHasCrest = false;
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			base.DrawAll(theFlags, g);
		}

		public override void KeyChar(char theChar)
		{
		}

		public bool ButtonsEnabled()
		{
			return new Bej3ButtonListenerImpl().ButtonsEnabled();
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonDepress(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
			string text = string.Empty;
			bool flag = false;
			int num = -1;
			string text2 = "ff93c0";
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER1, 0, GlobalMembers.M(0.5), GlobalMembers.M(-4.0));
				text = GlobalMembers._ID("Play the original untimed game", 365);
				break;
			case 1:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER2, 0, GlobalMembers.M(0.5), GlobalMembers.M(-4.0));
				text = GlobalMembers._ID("Relax body and mind in this endless game", 366);
				break;
			case 2:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER3, 0, GlobalMembers.M(0.5), GlobalMembers.M(-4.0));
				text = GlobalMembers._ID("Collect Time Gems to charge up bonus rounds", 367);
				break;
			case 3:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER4, 0, GlobalMembers.M(0.5), GlobalMembers.M(-4.0));
				text = GlobalMembers._ID("Solve 40 different puzzling minigames", 368);
				break;
			case 4:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER1, 0, GlobalMembers.M(0.5));
				if (GlobalMembers.gApp.mProfile.mEndlessModeUnlocked[0])
				{
					text = GlobalMembers._ID("Match gems and make your best hand", 369);
				}
				else
				{
					text = string.Format(GlobalMembers._ID("Reach level {0} of ^{1}^Classic Mode^oldclr^ to unlock", 370), 5, text2);
					flag = true;
					num = 0;
				}
				break;
			case 5:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER2, 0, GlobalMembers.M(0.5));
				if (GlobalMembers.gApp.mProfile.mEndlessModeUnlocked[1])
				{
					text = GlobalMembers._ID("Collect colorful jeweled butterflies", 371);
				}
				else
				{
					text = string.Format(GlobalMembers._ID("Reach level {0} of ^{1}^Zen Mode^oldclr^ to unlock", 372), 5, text2);
					flag = true;
					num = 1;
				}
				break;
			case 6:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER3, 0, GlobalMembers.M(0.5));
				if (GlobalMembers.gApp.mProfile.mEndlessModeUnlocked[2])
				{
					text = GlobalMembers._ID("Hold back the rising ice columns", 373);
				}
				else
				{
					text = string.Format(GlobalMembers._ID("Score {0} points in ^{1}^Lightning Mode^oldclr^ to unlock", 374), SexyFramework.Common.CommaSeperate(100000), text2);
					flag = true;
					num = 2;
				}
				break;
			case 7:
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER4, 0, GlobalMembers.M(0.5));
				if (GlobalMembers.gApp.mProfile.mEndlessModeUnlocked[3])
				{
					text = GlobalMembers._ID("Dig deep to unearth buried treasure", 375);
				}
				else
				{
					text = string.Format(GlobalMembers._ID("Reveal the first Relic in ^{0}^Quest Mode^oldclr^ to unlock", 376), text2);
					flag = true;
					num = 3;
				}
				break;
			default:
				switch (theId)
				{
				case 100:
				case 101:
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_MOUSEOVER);
					break;
				}
				break;
			}
			if (!this.IsClosing() && text.Length != 0)
			{
				this.mHoverText = text;
				this.mHoverTextLocked = flag;
				if (this.mHoverText.Length != 0)
				{
					if (this.mHoverTextAlpha == 1.0)
					{
						this.mHoverTextAlpha.SetConstant(1.0);
					}
					else if (this.mHoverTextAlpha.GetOutVal(this.mHoverTextAlpha.mInMax) == 0.0)
					{
						this.mHoverTextAlpha.SetCurve("b;0,1,0.01,0.25,####         ~~###");
					}
					for (uint num2 = 0U; num2 < 4U; num2 += 1U)
					{
						if ((ulong)num2 == (ulong)((long)num))
						{
							if (this.mHoverTextConnectorAlpha[(int)((UIntPtr)num2)].GetOutFinalVal() != 1.0)
							{
								this.mHoverTextConnectorAlpha[(int)((UIntPtr)num2)].Intercept("b;0,1,0.01,0.25,####         ~~###");
							}
						}
						else if (this.mHoverTextConnectorAlpha[(int)((UIntPtr)num2)].GetOutFinalVal() != 0.0)
						{
							this.mHoverTextConnectorAlpha[(int)((UIntPtr)num2)].Intercept("b;0,1,0.01,0.25,~###  v~###       +#G8)");
						}
					}
				}
			}
			else
			{
				this.StopHoverText();
			}
			this.mIntroTextAlpha.SetConstant(this.mIntroTextAlpha);
		}

		public void ButtonMouseLeave(int theId)
		{
			this.StopHoverText();
		}

		public void PlayLeaveAnim()
		{
			this.StopHoverText();
			this.mModeButtons[this.mSelectedMode].mFullPct.SetCurve("b;0,1,0.006667,1,~###         ~####");
			if (this.mSelectedMode >= this.MINIGAME_START_ID)
			{
				this.mModeButtons[this.mSelectedMode].mScale.SetCurve("b;0.1,1,0.006667,1,~###         ~#Q(j");
			}
			else
			{
				this.mModeButtons[this.mSelectedMode].mScale.SetCurve("b;0.2,1,0.006667,1,~###         ~#Q(j");
			}
			this.mSelectCenterPct.SetCurve("b;0,1,0.006667,1,~###         ~####");
		}

		public bool IsShowing()
		{
			return this.mShowPct != 0.0 || this.mShowPct.GetOutFinalVal() == 1.0;
		}

		public bool IsClosing()
		{
			return this.mShowPct.GetOutVal(this.mShowPct.mInMax) == 0.0;
		}

		public void StopHoverText()
		{
			for (uint num = 0U; num < 4U; num += 1U)
			{
				if (this.mHoverTextConnectorAlpha[(int)((UIntPtr)num)].GetOutVal(this.mHoverTextConnectorAlpha[(int)((UIntPtr)num)].mInMax) != 0.0)
				{
					this.mHoverTextConnectorAlpha[(int)((UIntPtr)num)].SetCurve("b;0,1,0.01,0.25,~###  v~###       +#G8)");
				}
			}
			if (this.mHoverTextAlpha.GetOutFinalVal() == 1.0)
			{
				this.mHoverTextAlpha.SetCurve("b;0,1,0.01,0.55,~###  v~###       +#G8*");
			}
		}

		public void StartLaunch()
		{
			if (this.mWantsLaunchId == -1)
			{
				return;
			}
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_CLICKFLYIN);
			if (GlobalMembers.gApp.mNeedMusicStart)
			{
				GlobalMembers.gApp.mNeedMusicStart = false;
			}
			CrystalBall crystalBall = this.mModeButtons[this.mWantsLaunchId];
			if (GlobalMembers.gApp.mBoard != null)
			{
				GlobalMembers.gApp.mBoard.mBackground.mVisible = false;
				GlobalMembers.gApp.mBoard.mAlpha.SetCurve("b;0,1,0.01,1,####  Z####       F~###");
				GlobalMembers.gApp.mBoard.mScale.SetCurve("b;1,5,0.01,1,~pF[         ~####");
				crystalBall.mImage = GlobalMembers.gApp.mBoard.mBackground.GetBackgroundImage(true, false);
				crystalBall.mImageSrcRect = new Rect(0, 0, 0, 0);
			}
			else if (GlobalMembers.gIs3D)
			{
				GlobalMembers.gApp.mQuestMenu.mBackground.mVisible = false;
				crystalBall.mImage = GlobalMembers.gApp.mQuestMenu.mBackground.GetBackgroundImage(true, false);
				crystalBall.mImageSrcRect = new Rect((int)(GlobalMembers.gApp.mQuestMenu.mQuestSetNumDisp * GlobalMembers.S(205.5)) + GlobalMembers.MS(0), 0, GlobalMembers.MS(480), crystalBall.mImage.mHeight);
			}
			else
			{
				crystalBall.mImage = null;
			}
			this.mSelectedMode = this.mWantsLaunchId;
			if (this.mWantsLaunchId >= this.MINIGAME_START_ID)
			{
				crystalBall.mScale.SetCurve("b;0.1,1,0.01,1,####         ~~Q(j");
			}
			else
			{
				crystalBall.mScale.SetCurve("b;0.2,1,0.01,1,####         ~~Q(j");
			}
			if (crystalBall != null)
			{
				this.BringToFront(crystalBall);
				crystalBall.mFullPct.SetCurve("b;0,1,0.01,1,####         ~~###");
				this.mSelectCenterPct.SetCurve("b;0,1,0.01,1,####         ~~###");
			}
			this.mWantsLaunchId = -1;
		}

		public const int CONNECTOR_COUNT = 4;

		public int MINIGAME_START_ID = 4;

		public Background mClassicBackground;

		public MainMenu mMainMenu;

		public CrystalBall[] mModeButtons = new CrystalBall[8];

		public CurvedVal mShowPct = new CurvedVal();

		public CurvedVal mBackBtnAlpha = new CurvedVal();

		public string mHoverText;

		public bool mHoverTextLocked;

		public CurvedVal mHoverTextAlpha = new CurvedVal();

		public CurvedVal[] mHoverTextConnectorAlpha = new CurvedVal[4];

		public CurvedVal mIntroTextAlpha = new CurvedVal();

		public int mSelectedMode;

		public CurvedVal mSelectCenterPct = new CurvedVal();

		public int mWantsLaunchId;

		public Bej3Button mBackButton;

		public Bej3Button mQuitButton;

		public int mBaseModeFontColor;

		public bool mPlaying100Anim;

		public enum EWidgetId
		{
			eId_Back = 100,
			eId_Quit
		}

		public enum EMinigamesId
		{
			eMode_Classic,
			eMode_Zen,
			eMode_Speed,
			eMode_Quest,
			eMode_Poker,
			eMode_Butterfly,
			eMode_IceStorm,
			eMode_GoldRush,
			NUM_MODE_BUTTONS
		}
	}
}
