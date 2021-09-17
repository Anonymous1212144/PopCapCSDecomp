using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public class QuestBoard : Board
	{
		public bool CallBoardCheckWin()
		{
			return base.CheckWin();
		}

		public void CallBoardFillInBlanks(bool allowCascades)
		{
			base.FillInBlanks(allowCascades);
		}

		public void CallBoardDrawHypercube(Graphics g, Piece thePiece)
		{
			base.DrawHypercube(g, thePiece);
		}

		public bool CallBoardIsHypermixerCancelledBy(Piece thePiece)
		{
			return base.IsHypermixerCancelledBy(thePiece);
		}

		public bool CallBoardIsGameSuspended()
		{
			return base.IsGameSuspended();
		}

		public Rect CallBoardGetCountdownBarRect()
		{
			return base.GetCountdownBarRect();
		}

		public bool CallBoardWantsBackground()
		{
			return base.WantsBackground();
		}

		public int CallBoardGetUICenterX()
		{
			return base.GetUICenterX();
		}

		public bool CallBoardWantBottomFrame()
		{
			return base.WantBottomFrame();
		}

		public bool CallBoardWantTopFrame()
		{
			return base.WantTopFrame();
		}

		public bool CallBoardWantTopLevelBar()
		{
			return base.WantTopLevelBar();
		}

		public bool CallBoardWantWarningGlow()
		{
			return this.CallBoardWantWarningGlow(false);
		}

		public bool CallBoardWantWarningGlow(bool forSound)
		{
			return base.WantWarningGlow(forSound);
		}

		public void CallBoardSetupBackground(int idx)
		{
			base.SetupBackground(idx);
		}

		public int CallBoardGetTimeDrawX()
		{
			return base.GetTimeDrawX();
		}

		public uint CallBoardGetRandSeedOverride()
		{
			return base.GetRandSeedOverride();
		}

		public bool CallBoardIsGameIdle()
		{
			return base.IsGameIdle();
		}

		public int CallBoardGetBoardX()
		{
			return base.GetBoardX();
		}

		public int CallBoardGetBoardY()
		{
			return base.GetBoardY();
		}

		public void CallBoardDrawUI(Graphics g)
		{
			base.DrawUI(g);
		}

		public void CallBoardDrawBottomFrame(Graphics g)
		{
			base.DrawBottomFrame(g);
		}

		public void CallBoardDeletePiece(Piece thePiece)
		{
			base.DeletePiece(thePiece);
		}

		public QuestBoard()
		{
			this.mQuestGoal = null;
			this.mDoDrawGameElements = true;
			this.mLevelCompleteTicksStart = 200;
			this.mLevelCompleteTicksAnnounce = 200;
			this.mQuestId = -1;
			this.mUiConfig = Board.EUIConfig.eUIConfig_Quest;
			this.mHelpButton = null;
		}

		public override void Dispose()
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.Dispose();
			}
			if (!this.mIsPerpetual && GlobalMembers.gApp.mQuestMenu != null)
			{
				GlobalMembers.gApp.mQuestMenu.mBackground.SetVisible(true);
			}
			base.Dispose();
		}

		public override void LoadContent(bool threaded)
		{
			base.LoadContent(threaded);
			if (threaded)
			{
				BejeweledLivePlusApp.LoadContentInBackground("GamePlayQuest");
				return;
			}
			BejeweledLivePlusApp.LoadContent("GamePlayQuest");
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			BejeweledLivePlusApp.UnloadContent("GamePlayQuest");
		}

		public override void Init()
		{
			base.Init();
			this.mWantShowPoints = false;
			this.mWantHyperMixers = false;
			this.mWantPointComplements = false;
			this.mColorCountStart = 7;
			this.mColorCount = this.mColorCountStart;
			this.mNeverAllowCascades = false;
			this.mUpdateCnt = 0;
			this.mHighScoreIsLevelPoints = true;
			this.mShowLevelPoints = true;
			this.mTicksInPlay = 0;
			this.mAllowLevelUp = true;
			this.mDefaultBaseScore = GlobalMembers.M(10);
			this.mDefaultBaseScoreIncr = GlobalMembers.M(15);
			this.mBoardType = EBoardType.eBoardType_Quest;
			this.mEndLevelDialog = null;
			this.mRecordHighScores = this.mIsPerpetual;
			if (this.mParams.ContainsKey("PowerGemThreshold"))
			{
				this.mPowerGemThreshold = GlobalMembers.sexyatoi(this.mParams["PowerGemThreshold"]);
			}
			bool flag = false;
			if (!this.mIsPerpetual && GlobalMembers.gApp.mProfile.WantQuestHelp(this.mQuestId))
			{
				flag = true;
			}
			else if (this.mIsPerpetual && this.mParams.ContainsKey("HelpText") && !string.IsNullOrEmpty(this.mParams["HelpText"]))
			{
				int num = 0;
				switch (GlobalMembers.gApp.mLastDataParserId)
				{
				case 0:
					num = 0;
					break;
				case 1:
					num = 1;
					break;
				case 2:
					num = 2;
					break;
				case 3:
					num = 3;
					break;
				}
				if ((GlobalMembers.gApp.mProfile.mFlags & (1 << num)) == 0)
				{
					flag = true;
					GlobalMembers.gApp.mProfile.mFlags |= 1 << num;
				}
			}
			if (flag)
			{
				this.mTimerAlpha.SetConstant(0.0);
			}
		}

		public override void InitUI()
		{
			if (this.mUiConfig != Board.EUIConfig.eUIConfig_Quest)
			{
				base.InitUI();
				return;
			}
			if (this.mHintButton == null)
			{
				this.mHintButton = new Bej3Button(0, this);
				this.AddWidget(this.mHintButton);
			}
			if (this.mHelpButton == null)
			{
				this.mHelpButton = new ButtonWidget(4, this);
				this.AddWidget(this.mHelpButton);
			}
			if (this.mResetButton == null)
			{
				this.mResetButton = new ButtonWidget(2, this);
				this.AddWidget(this.mResetButton);
			}
			this.mReplayButton = null;
		}

		public override void RefreshUI()
		{
			if (this.mHelpButton != null)
			{
				this.mHelpButton.Resize(GlobalMembers.S(this.GetUICenterX() - 125), GlobalMembers.MS(755), GlobalMembers.MS(250), GlobalMembers.S(GlobalMembers.M(120) + this.GetBottomWidgetOffset()));
				this.mHelpButton.mBtnNoDraw = true;
				this.mHelpButton.mDoFinger = true;
				this.mHelpButton.mHasAlpha = true;
				this.mHelpButton.mDoFinger = true;
				this.mHelpButton.mOverAlphaSpeed = 0.1;
				this.mHelpButton.mOverAlphaFadeInSpeed = 0.2;
			}
		}

		public override void BoardSettled()
		{
			if (this.mTimerAlpha > 0.0 || (!this.mIsPerpetual && !GlobalMembers.gApp.mProfile.WantQuestHelp(this.mQuestId)))
			{
				this.mHelpHasBeenClosed = true;
				this.ReadyToPlay();
			}
			else
			{
				this.OpenHelpDialog();
			}
			base.BoardSettled();
		}

		public override void DialogClosed(int theId)
		{
			if (theId == 40 && !this.mHelpHasBeenClosed)
			{
				this.mHelpHasBeenClosed = true;
				this.ReadyToPlay();
				this.StartTimer();
			}
			base.DialogClosed(theId);
		}

		public virtual void ReadyToPlay()
		{
		}

		public override bool AllowUI()
		{
			return this.mLevelCompleteCount <= GlobalMembers.M(0) && this.mGameOverCount <= GlobalMembers.M(0);
		}

		public override bool AllowSpeedBonus()
		{
			return false;
		}

		public override bool AllowNoMoreMoves()
		{
			return false;
		}

		public override bool AllowHints()
		{
			return true;
		}

		public override bool WantColorCombos()
		{
			return false;
		}

		public override bool WantHyperMixers()
		{
			return this.mWantHyperMixers;
		}

		public override void NewHyperMixer()
		{
			QuestBoard.GameTypeData questStats = this.GetQuestStats();
			questStats.mHypermixerCount++;
		}

		public override int GetSidebarTextY()
		{
			if (this.mQuestGoal != null)
			{
				int sidebarTextY = this.mQuestGoal.GetSidebarTextY();
				if (sidebarTextY > 0)
				{
					return sidebarTextY;
				}
			}
			return base.GetSidebarTextY();
		}

		public override bool WantsTutorial(int theTutorialFlag)
		{
			return base.WantsTutorial(theTutorialFlag);
		}

		public override bool WantWarningGlow()
		{
			return this.WantWarningGlow(false);
		}

		public override bool WantWarningGlow(bool forSound)
		{
			if (this.mLevelCompleteCount != 0)
			{
				return false;
			}
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.WantWarningGlow();
			}
			return base.WantWarningGlow();
		}

		public virtual void DoEndLevelDialog()
		{
			if (this.mQuestGoal == null || !this.mQuestGoal.DoEndLevelDialog())
			{
				this.mEndLevelDialog = new EndLevelDialog(this);
				this.mEndLevelDialog.SetQuestName(this.GetQuestName());
				GlobalMembers.gApp.AddDialog(38, this.mEndLevelDialog);
				this.BringToFront(this.mEndLevelDialog);
			}
		}

		public override bool WantBottomFrame()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.WantBottomFrame();
			}
			return base.WantBottomFrame();
		}

		public override bool WantTopLevelBar()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.WantTopLevelBar();
			}
			return base.WantTopLevelBar();
		}

		public override bool WantTopFrame()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.WantTopFrame();
			}
			return base.WantTopFrame();
		}

		public override bool WantPointComplements()
		{
			return this.mWantPointComplements;
		}

		public override bool WantsTutorialReplays()
		{
			return false;
		}

		public override void CelDestroyedBySpecial(int theCol, int theRow)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.CelDestroyedBySpecial(theCol, theRow);
			}
		}

		public override bool WantDrawTimer()
		{
			return this.mQuestGoal == null || this.mQuestGoal.WantDrawTimer();
		}

		public override bool WantsBackground()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.WantsBackground();
			}
			return base.WantsBackground();
		}

		public override void GameOverAnnounce()
		{
			base.GameOverAnnounce();
		}

		public virtual int GetTitleY()
		{
			return GlobalMembers.M(90);
		}

		public override bool CheckWin()
		{
			if (this.mIsPerpetual)
			{
				return false;
			}
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.CheckWin();
			}
			return base.CheckWin();
		}

		public override void SetupBackground()
		{
			this.SetupBackground(0);
		}

		public override void SetupBackground(int theDeltaIdx)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.SetupBackground(theDeltaIdx);
				return;
			}
			base.SetupBackground(theDeltaIdx);
		}

		public override int GetUICenterX()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.GetUICenterX();
			}
			return base.GetUICenterX();
		}

		public QuestBoard.GameTypeData GetQuestStats()
		{
			string questName = this.GetQuestName();
			QuestBoard.GameTypeData gameTypeData;
			if (QuestBoard.mGameData.ContainsKey(questName))
			{
				gameTypeData = QuestBoard.mGameData[questName];
			}
			else
			{
				gameTypeData = new QuestBoard.GameTypeData();
				gameTypeData.mPlays = 0;
				gameTypeData.mScoreTotal = 0;
				gameTypeData.mTimeTotal = 0;
				gameTypeData.mHighScore = -1;
				gameTypeData.mLowScore = -1;
				gameTypeData.mHypermixerCount = 0;
				QuestBoard.mGameData.Add(questName, gameTypeData);
			}
			return gameTypeData;
		}

		public void RecordQuestStats()
		{
		}

		public void StartTimer()
		{
			if (this.mTimerAlpha == 0.0)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_TIMER_INFLATE, this.mTimerInflate);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_TIMER_ALPHA, this.mTimerAlpha);
			}
		}

		public void DoQuestPortal(Announcement theAnnouncement, bool theWon)
		{
			GlobalMembers.gApp.KillDialog(40);
			base.Flatten();
			GlobalMembers.gApp.DoQuestMenu(false);
			GlobalMembers.gApp.mQuestMenu.mDrawGemsAsOverlay = false;
			if (theAnnouncement != null)
			{
				GlobalMembers.gApp.mQuestMenu.SetPortalAnnouncement(theAnnouncement, theWon);
			}
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_QUEST_PORTAL_PCT_OPEN, this.mQuestPortalPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_QUEST_PORTAL_CENTER_PCT_OPEN, this.mQuestPortalPct, GlobalMembers.gApp.mBoard.mQuestPortalPct);
		}

		public override bool WantAutoload()
		{
			return true;
		}

		public override string GetMusicName()
		{
			if (this.GetTimeLimit() == 0)
			{
				return "QuestTurnBased";
			}
			return "QuestTimeBased";
		}

		public override bool SaveGameExtra(Serialiser theBuffer)
		{
			if (this.mQuestGoal != null && this.mQuestGoal.ExtraSaveGameInfo() && !this.mQuestGoal.SaveGameExtra(theBuffer))
			{
				return false;
			}
			if (this.ExtraSaveGameInfo())
			{
				int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkQuestBoard);
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						theBuffer.WriteInt32((int)this.mGridState[i, j]);
					}
				}
				theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			}
			return true;
		}

		public override void LoadGameExtra(Serialiser theBuffer)
		{
			if (this.mQuestGoal != null && this.mQuestGoal.ExtraSaveGameInfo())
			{
				this.mQuestGoal.LoadGameExtra(theBuffer);
			}
			if (this.ExtraSaveGameInfo())
			{
				int num = 0;
				GameChunkHeader header = new GameChunkHeader();
				if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkQuestBoard, header, out num))
				{
					return;
				}
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						this.mGridState[i, j] = (uint)theBuffer.ReadInt32();
					}
				}
			}
		}

		public virtual bool ExtraSaveGameInfo()
		{
			return this.mQuestGoal != null && this.mQuestGoal.ExtraSaveGameInfo();
		}

		public override int GetTimeDrawX()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.GetTimeDrawX();
			}
			return base.GetTimeDrawX();
		}

		public override string GetSavedGameName()
		{
			if (this.mIsPerpetual)
			{
				switch (GlobalMembers.gApp.mLastDataParserId)
				{
				case 0:
					return "poker.sav";
				case 1:
					return "butterfly.sav";
				case 2:
					return "ice_storm.sav";
				case 3:
					return "diamond_mine.sav";
				}
			}
			return string.Empty;
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, true, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, true, true, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, uint.MaxValue, true, true, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd, int thePointType)
		{
			if (this.mWantShowPoints || (this.mQuestGoal != null && this.mQuestGoal.mWantShowPoints))
			{
				return base.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, thePointType);
			}
			return null;
		}

		public override string GetLoggingGameName()
		{
			if (!this.mParams.ContainsKey("Title"))
			{
				this.mParams.Add("Title", "");
			}
			if (this.mIsPerpetual)
			{
				return "Perpetual " + this.mParams["Title"];
			}
			return this.mParams["Title"];
		}

		public override void LevelUp()
		{
			if (this.mGameOverPiece != null)
			{
				return;
			}
			if (!this.mAllowLevelUp)
			{
				return;
			}
			if (this.mLevelCompleteCount == 0)
			{
				if (this.mIsPerpetual)
				{
					this.DoLevelUp();
					return;
				}
				this.LogGameOver("QuestCompleted " + this.GetExtraGameOverLogParams());
				this.mGameFinished = true;
				this.mLevelCompleteCount = this.mLevelCompleteTicksStart;
				if (this.mRecordHighScores)
				{
					GlobalMembers.gApp.mHighScoreMgr.Submit(this.GetQuestName(), GlobalMembers.gApp.mProfile.mProfileName, this.mLevelPointsTotal, GlobalMembers.gApp.mProfile.GetProfilePictureId());
				}
			}
		}

		public virtual void DoLevelUp()
		{
			base.LevelUp();
		}

		public override void GameOver(bool visible)
		{
			if ((this.mQuestGoal == null || this.mQuestGoal.AllowGameOver()) && !this.CheckWin())
			{
				this.SubmitHighscore();
				bool flag = this.mGameOverCount > 0;
				base.GameOver(true);
				if (!this.mIsPerpetual)
				{
				}
			}
		}

		public override void GameOverExit()
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.GameOverExit();
			}
			this.RecordQuestStats();
			if (this.mRecordHighScores)
			{
				GlobalMembers.gApp.DoGameDetailMenu(GlobalMembers.gApp.mCurrentGameMode, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME);
				return;
			}
			if (this.mIsPerpetual)
			{
				GlobalMembers.gApp.DoSecretMenu();
				return;
			}
			this.BackToQuestMenu();
		}

		public override void BackToMenu()
		{
			base.BackToMenu();
		}

		public void BackToQuestMenu()
		{
			if (GlobalMembers.gApp.mQuestMenu != null && GlobalMembers.gApp.mQuestMenu.mVisible)
			{
				GlobalMembers.KILL_WIDGET(GlobalMembers.gApp.mBoard);
				GlobalMembers.gApp.mBoard = null;
				return;
			}
			GlobalMembers.gApp.DoQuestMenu();
		}

		public override void DrawMenuWidget(Graphics g)
		{
			if (this.mUiConfig != Board.EUIConfig.eUIConfig_Quest)
			{
				base.DrawMenuWidget(g);
				return;
			}
			if (this.mSidebarText != string.Empty)
			{
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("MAIN", Color.White);
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("GLOW", Color.Black);
				string text = this.mSidebarText;
				if (this.mHelpButton.mIsOver)
				{
					text += GlobalMembers._ID("\n^FFD0FF^- more -", 415);
				}
				else
				{
					text += GlobalMembers._ID("\n^FF80FF^- more -", 416);
				}
				g.SetColor(new Color(255, 255, 255, 255));
				g.SetColor(Color.White);
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("MAIN");
				((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("GLOW");
			}
		}

		public override uint GetRandSeedOverride()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.GetRandSeedOverride();
			}
			return base.GetRandSeedOverride();
		}

		public virtual void TryGenerateDefaultScores()
		{
		}

		public string GetQuestName()
		{
			return GlobalMembers.gApp.GetModeHeading(GlobalMembers.gApp.mCurrentGameMode);
		}

		public override void ButtonDepress(int theId)
		{
			if (!this.AllowUI())
			{
				return;
			}
			if (theId == 4)
			{
				this.OpenHelpDialog();
			}
			base.ButtonDepress(theId);
		}

		public void OpenHelpDialog()
		{
			GlobalMembers.gApp.mProfile.SetQuestHelpShown(this.mQuestId);
		}

		public override bool IsHypermixerCancelledBy(Piece thePiece)
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.IsHypermixerCancelledBy(thePiece);
			}
			return base.IsHypermixerCancelledBy(thePiece);
		}

		public override void DrawHypercube(Graphics g, Piece thePiece)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawHypercube(g, thePiece);
				return;
			}
			base.DrawHypercube(g, thePiece);
		}

		public virtual void SetColorCount(int theColorCount)
		{
			if (this.mColorCount != theColorCount)
			{
				this.mColorCount = theColorCount;
				GlobalMembers.M(1);
				this.mNewGemColors.Clear();
				for (int i = 0; i < theColorCount; i++)
				{
					if (theColorCount <= 6 && i == 5)
					{
						i = 6;
					}
					this.mNewGemColors.Add(i);
					this.mNewGemColors.Add(i);
				}
			}
		}

		public override void DrawCountPopups(Graphics g)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawCountPopups(g);
			}
		}

		public override void ProcessMatches(List<MatchSet> theMatches, Dictionary<Piece, int> theTallySet, bool fromUpdateSwapping)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.ProcessMatches(theMatches, theTallySet, fromUpdateSwapping);
			}
			base.ProcessMatches(theMatches, theTallySet, fromUpdateSwapping);
		}

		public override void DeletePiece(Piece thePiece)
		{
			if (this.mQuestGoal != null && !this.mQuestGoal.DeletePiece(thePiece))
			{
				return;
			}
			base.DeletePiece(thePiece);
		}

		public override bool CreateMatchPowerup(int theMatchCount, Piece thePiece, Dictionary<Piece, int> thePieceSet)
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.CreateMatchPowerup(theMatchCount, thePiece, thePieceSet);
			}
			return base.CreateMatchPowerup(theMatchCount, thePiece, thePieceSet);
		}

		public override bool PiecesDropped(List<Piece> thePieceVector)
		{
			return (this.mQuestGoal == null || this.mQuestGoal.PiecesDropped(thePieceVector)) && base.PiecesDropped(thePieceVector);
		}

		public virtual void DoQuestBonus()
		{
			this.DoQuestBonus(1f);
		}

		public virtual void DoQuestBonus(float iOpt_multiplier)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DoQuestBonus(iOpt_multiplier);
			}
		}

		public virtual void DoQuestPenalty()
		{
			this.DoQuestPenalty(1f);
		}

		public virtual void DoQuestPenalty(float iOpt_multiplier)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DoQuestPenalty();
			}
		}

		public override int GetLevelPoints()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.GetLevelPoints();
			}
			return 0;
		}

		public override void DoHypercube(Piece thePiece, int theColor)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DoHypergem(thePiece, theColor);
			}
			base.DoHypercube(thePiece, theColor);
		}

		public override void SwapSucceeded(SwapData theSwapData)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.SwapSucceeded(theSwapData);
			}
			base.SwapSucceeded(theSwapData);
		}

		public override void FillInBlanks()
		{
			this.FillInBlanks(true);
		}

		public override void FillInBlanks(bool allowCascades)
		{
			if (this.mNeverAllowCascades)
			{
				allowCascades = false;
			}
			if (this.mGameTicks > 0)
			{
				this.CheckWin();
			}
			if (this.mGameOverCount != 0 || this.mLevelCompleteCount != 0)
			{
				allowCascades = false;
			}
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.FillInBlanks(allowCascades);
				return;
			}
			base.FillInBlanks(allowCascades);
		}

		public override void DrawQuestPieces(Graphics g, Piece[] pPieces, int numPieces, bool thePostFX)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawPieces(g, pPieces, numPieces, thePostFX);
			}
		}

		public override void DrawPieces(Graphics g, bool thePostFX)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawPiecesPre(g, thePostFX);
			}
			base.DrawPieces(g, thePostFX);
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawPiecesPost(g, thePostFX);
			}
		}

		public override void DrawPieceShadow(Graphics g, Piece thePiece)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawPieceShadow(g, thePiece);
			}
			base.DrawPieceShadow(g, thePiece);
		}

		public override Rect GetCountdownBarRect()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.GetCountdownBarRect();
			}
			return base.GetCountdownBarRect();
		}

		public override bool WantSpecialPiece(List<Piece> thePieceVector)
		{
			return this.mQuestGoal != null && this.mQuestGoal.WantSpecialPiece(thePieceVector);
		}

		public override bool DropSpecialPiece(List<Piece> thePieceVector)
		{
			return this.mQuestGoal != null && this.mQuestGoal.DropSpecialPiece(thePieceVector);
		}

		public override void BlanksFilled(bool specialDropped)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.BlanksFilled(specialDropped);
			}
		}

		public override void SwapFailed(SwapData theSwapData)
		{
			base.SwapFailed(theSwapData);
		}

		public override void PieceDestroyedInSwap(Piece thePiece)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.PieceDestroyedInSwap(thePiece);
			}
		}

		public override int GetBoardX()
		{
			if (this.mQuestGoal != null)
			{
				int boardX = this.mQuestGoal.GetBoardX();
				if (boardX > 0)
				{
					return boardX;
				}
			}
			return base.GetBoardX();
		}

		public override int GetBoardY()
		{
			if (this.mQuestGoal != null)
			{
				int boardY = this.mQuestGoal.GetBoardY();
				if (boardY > 0)
				{
					return boardY;
				}
			}
			return base.GetBoardY();
		}

		public override bool IsGameSuspended()
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.IsGameSuspended();
			}
			return base.IsGameSuspended();
		}

		public override bool TriggerSpecial(Piece aPiece)
		{
			return this.TriggerSpecial(aPiece, null);
		}

		public override bool TriggerSpecial(Piece aPiece, Piece aSpecialPiece)
		{
			return (this.mQuestGoal != null && this.mQuestGoal.TriggerSpecial(aPiece, aSpecialPiece)) || base.TriggerSpecial(aPiece, aSpecialPiece);
		}

		public virtual TextNotifyEffect ShowQuestText(string i_text)
		{
			TextNotifyEffect textNotifyEffect = TextNotifyEffect.alloc();
			textNotifyEffect.mDuration = GlobalMembers.M(200);
			textNotifyEffect.mText = i_text;
			textNotifyEffect.mX = (float)GlobalMembers.M(1000);
			textNotifyEffect.mY = (float)GlobalMembers.M(620);
			this.mPostFXManager.AddEffect(textNotifyEffect);
			return textNotifyEffect;
		}

		public override void KeyChar(char theChar)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.KeyChar(theChar);
			}
			base.KeyChar(theChar);
		}

		public override bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, false);
		}

		public override bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, true, false);
		}

		public override bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, false, true, false);
		}

		public override bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped, bool destroyTarget)
		{
			if (this.mQuestGoal != null)
			{
				return this.mQuestGoal.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, destroyTarget);
			}
			return base.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, destroyTarget);
		}

		public virtual bool CallBaseTrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			return this.CallBaseTrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, false);
		}

		public virtual bool CallBaseTrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.CallBaseTrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, true, false);
		}

		public virtual bool CallBaseTrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.CallBaseTrySwap(theSelected, theSwappedRow, theSwappedCol, false, true, false);
		}

		public virtual bool CallBaseTrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped, bool destroyTarget)
		{
			return base.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, destroyTarget);
		}

		public override void TallyPiece(Piece thePiece, bool thePieceDestroyed)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.TallyPiece(thePiece, thePieceDestroyed);
			}
			base.TallyPiece(thePiece, thePieceDestroyed);
		}

		public override void PieceTallied(Piece thePiece)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.PieceTallied(thePiece);
			}
			base.PieceTallied(thePiece);
		}

		public override void DoHypercube(Piece thePiece, Piece theSwappedPiece)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DoHypercube(thePiece, theSwappedPiece);
			}
			base.DoHypercube(thePiece, theSwappedPiece);
		}

		public override void ExamineBoard()
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.ExamineBoard();
			}
			base.ExamineBoard();
		}

		public override void NewGame()
		{
			this.NewGame(false);
		}

		public override void NewGame(bool restartingGame)
		{
			this.mUpdateCnt = 0;
			this.mWantShowPoints = this.mIsPerpetual;
			this.mRecordHighScores = this.mIsPerpetual;
			this.mHasBoardSettled = false;
			this.mHelpHasBeenClosed = false;
			this.mColorCountStart = 0;
			if (this.mParams.ContainsKey("ColorCount"))
			{
				int.TryParse(this.mParams["ColorCount"], ref this.mColorCountStart);
				this.SetColorCount(this.mColorCountStart);
			}
			if (this.mParams.ContainsKey("RecordHighScores"))
			{
				bool.TryParse(this.mParams["RecordHighScores"], ref this.mRecordHighScores);
			}
			if (this.mParams.ContainsKey("HighScoreBase"))
			{
				int.TryParse(this.mParams["HighScoreBase"], ref this.mDefaultBaseScore);
			}
			if (this.mParams.ContainsKey("HighScoreIncr"))
			{
				int.TryParse(this.mParams["HighScoreIncr"], ref this.mDefaultBaseScoreIncr);
			}
			if (this.mParams.ContainsKey("WantPointComplements"))
			{
				bool.TryParse(this.mParams["WantPointComplements"], ref this.mWantPointComplements);
			}
			if (this.mParams.ContainsKey("NeverAllowCascades"))
			{
				bool.TryParse(this.mParams["NeverAllowCascades"], ref this.mNeverAllowCascades);
			}
			if (!this.mParams.ContainsKey("ShowAutohints") || !bool.TryParse(this.mParams["ShowAutohints"], ref this.mShowAutohints))
			{
				this.mShowAutohints = true;
			}
			string text = string.Empty;
			if (this.mParams.ContainsKey("Goal"))
			{
				text = this.mParams["Goal"].ToUpper();
			}
			if (this.mQuestGoal != null)
			{
				if (this.mQuestGoal != null)
				{
					this.mQuestGoal.Dispose();
				}
				this.mQuestGoal = null;
			}
			if (text == "DIG")
			{
				this.mQuestGoal = new DigGoal(this);
			}
			else if (text == "CATCH BUTTERFLIES")
			{
				this.mQuestGoal = new ButterflyGoal(this);
			}
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.NewGamePreSetup();
			}
			base.NewGame(restartingGame);
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.NewGame();
			}
			this.mSidebarText = this.mParams["Description"];
			if (this.mParams.ContainsKey("HyperMixers"))
			{
				bool.TryParse("HyperMixers", ref this.mWantHyperMixers);
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mRewinding || this.mUserPaused || GlobalMembers.gApp.GetDialog(23) != null)
			{
				return;
			}
			if (!this.mIsPerpetual && this.mGameOverCount == GlobalMembers.M(300))
			{
				this.DoQuestPortal(null, false);
				this.mQuestPortalPct.SetInVal(GlobalMembers.M(0.5));
			}
			this.mTicksInPlay++;
			if (this.mLevelCompleteCount > 0)
			{
				if (this.mLevelCompleteCount > this.mLevelCompleteTicksAnnounce - 1)
				{
					if (base.IsBoardStill())
					{
						this.mLevelCompleteCount--;
					}
				}
				else if (this.mLevelCompleteCount == this.mLevelCompleteTicksAnnounce - 2)
				{
					if (base.IsBoardStill() && this.mLevelBarPct == this.GetLevelPct())
					{
						GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_CHALLENGECOMPLETE);
						this.mLevelCompleteCount--;
						if (GlobalMembers.gApp.mQuestMenu != null)
						{
							Announcement announcement = new Announcement(null, GlobalMembers._ID("QUEST\nCOMPLETED", 417));
							this.DoQuestPortal(announcement, true);
							if (GlobalMembers.gApp.mQuestMenu != null && GlobalMembers.gApp.mQuestMenu.IsLastQuestCompleted())
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_ALPHA_QUEST_1, announcement.mAlpha);
							}
							else
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_ALPHA_QUEST_2, announcement.mAlpha);
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_SCALE_QUEST, announcement.mScale);
							}
						}
					}
				}
				else
				{
					if ((this.mLevelCompleteCount > 50 || this.mDeferredTutorialVector.Count == 0) && GlobalMembers.gApp.GetDialog(41) == null)
					{
						this.mLevelCompleteCount--;
					}
					if (this.mLevelCompleteCount == GlobalMembers.M(100))
					{
						bool flag = GlobalMembers.gApp.mProfile.mQuestsCompleted[GlobalMembers.gApp.mQuestMenu.mQuestSetNum, GlobalMembers.gApp.mQuestMenu.mGemOver];
						GlobalMembers.gApp.mProfile.mQuestsCompleted[GlobalMembers.gApp.mQuestMenu.mQuestSetNum, GlobalMembers.gApp.mQuestMenu.mGemOver] = true;
						this.CalcBadges();
						GlobalMembers.gApp.mProfile.mQuestsCompleted[GlobalMembers.gApp.mQuestMenu.mQuestSetNum, GlobalMembers.gApp.mQuestMenu.mGemOver] = flag;
					}
					if (this.mLevelCompleteCount == 0)
					{
						if (!this.mIsPerpetual)
						{
							QuestMenu mQuestMenu = GlobalMembers.gApp.mQuestMenu;
						}
						this.GameOverExit();
					}
				}
			}
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.Update();
			}
			if (this.mHelpButton != null)
			{
				this.mHelpButton.mMouseVisible = (double)this.GetAlpha() * this.mSideAlpha == 1.0;
				this.mHelpButton.mDisabled = false;
			}
			this.mUpdateCnt++;
		}

		public override void DoUpdate()
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DoUpdate();
			}
			base.DoUpdate();
		}

		public override void Draw(Graphics g)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.Draw(g);
			}
			base.Draw(g);
			if (!this.mIsPerpetual)
			{
				g.SetFont(GlobalMembersResources.FONT_HEADER);
				((ImageFont)g.mFont).PushLayerColor("OUTLINE", Color.Black);
				((ImageFont)g.mFont).PushLayerColor("GLOW", new Color(0, 0, 0, 128));
				g.WriteString(this.mParams["TitleText"], GlobalMembers.S(this.GetUICenterX()), GlobalMembers.S(this.GetTitleY()));
				((ImageFont)g.mFont).PopLayerColor("OUTLINE");
				((ImageFont)g.mFont).PopLayerColor("GLOW");
			}
			if (this.mDoDrawGameElements)
			{
				if (this.mQuestGoal != null)
				{
					this.mQuestGoal.DrawGameElements(g);
				}
				this.DrawGameElements(g);
				base.DrawGameElements(g);
				if (this.mQuestGoal != null)
				{
					this.mQuestGoal.DrawGameElementsPost(g);
				}
			}
		}

		public override void DrawPiece(Graphics g, Piece thePiece)
		{
			this.DrawPiece(g, thePiece, 1f);
		}

		public override void DrawPiece(Graphics g, Piece thePiece, float theScale)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawPiecePre(g, thePiece, theScale);
			}
			base.DrawPiece(g, thePiece, theScale);
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawPiecePost(g, thePiece, theScale);
			}
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			base.DrawOverlay(g, thePriority);
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawOverlay(g);
			}
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawOverlayPost(g);
			}
		}

		public override void DrawOverlayPreAnnounce(Graphics g, int thePriority)
		{
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawOverlay(g);
			}
			base.DrawOverlayPreAnnounce(g, thePriority);
		}

		public override void DrawOverlayPostAnnounce(Graphics g, int thePriority)
		{
			base.DrawOverlayPostAnnounce(g, thePriority);
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawOverlayPost(g);
			}
		}

		public override void DrawGameElements(Graphics g)
		{
		}

		public override void DrawScoreWidget(Graphics g)
		{
			if (this.mQuestGoal != null && this.mQuestGoal.DrawScoreWidget(g))
			{
				return;
			}
			this.DrawSpeedBonus(g);
			this.DrawLevelBar(g);
			g.SetColor(Color.FAlpha(this.GetAlpha()));
		}

		public override void DrawScore(Graphics g)
		{
			if (this.mQuestGoal != null && this.mQuestGoal.DrawScore(g))
			{
				return;
			}
			base.DrawScore(g);
		}

		public override void DrawUI(Graphics g)
		{
			base.DrawUI(g);
		}

		public override void DrawFrame(Graphics g)
		{
			base.DrawFrame(g);
		}

		public override bool CanPlay()
		{
			return (this.mQuestGoal == null || this.mQuestGoal.CanPlay()) && base.CanPlay();
		}

		public override int GetPowerGemThreshold()
		{
			if (this.mQuestGoal != null && this.mQuestGoal.GetPowerGemThreshold() > 0)
			{
				return this.mQuestGoal.GetPowerGemThreshold();
			}
			if (this.mPowerGemThreshold > 0)
			{
				return this.mPowerGemThreshold;
			}
			return base.GetPowerGemThreshold();
		}

		public override bool GetTooltipText(Piece thePiece, ref string theHeader, ref string theBody)
		{
			string text = theHeader;
			string text2 = theBody;
			if (this.mQuestGoal != null && this.mQuestGoal.GetTooltipText(thePiece, ref text, ref text2))
			{
				theHeader = text;
				theBody = text2;
				return true;
			}
			return base.GetTooltipText(thePiece, ref theHeader, ref theBody);
		}

		public override void SubmitHighscore()
		{
			if ((this.mQuestGoal == null || this.mQuestGoal.AllowGameOver()) && !this.CheckWin() && this.mRecordHighScores && this.mGameOverCount == 0)
			{
				this.TryGenerateDefaultScores();
				string questName = this.GetQuestName();
				HighScoreTable orCreateTable = GlobalMembers.gApp.mHighScoreMgr.GetOrCreateTable(questName);
				if (orCreateTable.Submit(GlobalMembers.gApp.mProfile.mProfileName, this.mHighScoreIsLevelPoints ? this.mLevelPointsTotal : this.mPoints, GlobalMembers.gApp.mProfile.GetProfilePictureId()))
				{
					GlobalMembers.gApp.SaveHighscores();
				}
			}
		}

		public static Dictionary<string, QuestBoard.GameTypeData> mGameData = new Dictionary<string, QuestBoard.GameTypeData>();

		public uint[,] mGridState = new uint[8, 8];

		public QuestGoal mQuestGoal;

		public bool mIsPerpetual;

		public bool mAllowLevelUp;

		public bool mWantHyperMixers;

		public bool mWantShowPoints;

		public int mPowerGemThreshold;

		public int mLevelCompleteTicksStart;

		public int mLevelCompleteTicksAnnounce;

		public int mColorCountStart;

		public new int mUpdateCnt;

		public int mColorCount;

		public int mTicksInPlay;

		public bool mDoDrawGameElements;

		public bool mHelpHasBeenClosed;

		public EndLevelDialog mEndLevelDialog;

		public bool mRecordHighScores;

		public bool mHighScoreIsLevelPoints;

		public ButtonWidget mHelpButton;

		public CurvedVal mMenuButtonFlash = new CurvedVal();

		public bool mNeverAllowCascades;

		public int mDefaultBaseScore;

		public int mDefaultBaseScoreIncr;

		public int mQuestId;

		public bool mWantPointComplements;

		public Announcement mGameOverAnnouncement;

		public class GameTypeData
		{
			public int mPlays;

			public int mScoreTotal;

			public int mTimeTotal;

			public int mHighScore;

			public int mLowScore;

			public int mHypermixerCount;
		}
	}
}
