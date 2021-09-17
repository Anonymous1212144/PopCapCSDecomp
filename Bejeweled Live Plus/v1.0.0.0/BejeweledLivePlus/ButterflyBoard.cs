using System;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public class ButterflyBoard : QuestBoard
	{
		public ButterflyBoard()
		{
			this.mSpiderRandomBehaviour = false;
			this.mSpider = null;
			this.mLastButterflyEffect = null;
			this.mCurrentReleasedButterflies = 0;
			this.mSpotOnSpider = false;
			this.mCountingForGameOver = false;
			this.mGameOverCountdown = 0;
			this.mSpiderYOffset = 0f;
			this.mSpiderDownAfterGrab = true;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override int FindSets(bool fromUpdateSwap, Piece thePiece1, Piece thePiece2)
		{
			this.mForceReleaseButterfly = false;
			this.mForcedReleasedBflyPiece = null;
			int result = base.FindSets(fromUpdateSwap, thePiece1, thePiece2);
			if (this.mForceReleaseButterfly && this.mForcedReleasedBflyPiece != null)
			{
				this.mGameStats[28]++;
				this.mLastButterflyEffect = ButterflyEffect.alloc(this.mForcedReleasedBflyPiece, this);
				if (!this.mIsPerpetual)
				{
					this.mLastButterflyEffect.mTargetY.mOutMin = (double)GlobalMembers.M(590);
				}
				this.mPostFXManager.AddEffect(this.mLastButterflyEffect);
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTERFLYESCAPE);
			}
			return result;
		}

		public void OnButterflyMatched(Piece thePiece)
		{
			int moveStat = base.GetMoveStat(thePiece.mMoveCreditId, 28);
			base.MaxStat(29, moveStat, thePiece.mMoveCreditId);
			this.mAllowNewComboFloaters = true;
			this.AddPoints((int)thePiece.CX(), (int)thePiece.CY(), GlobalMembers.M(150) + GlobalMembers.M(5) * this.mGameStats[28] + (moveStat - 1) * GlobalMembers.M(100), Color.White, (uint)thePiece.mMatchId, true, true, thePiece.mMoveCreditId, false);
			this.mAllowNewComboFloaters = false;
			this.ReleaseButterfly(thePiece);
			if (this.GetLevelPointsTotal() >= this.GetLevelPoints() && !this.mIsPerpetual)
			{
				Piece[,] mBoard = this.mBoard;
				int upperBound = mBoard.GetUpperBound(0);
				int upperBound2 = mBoard.GetUpperBound(1);
				for (int i = mBoard.GetLowerBound(0); i <= upperBound; i++)
				{
					for (int j = mBoard.GetLowerBound(1); j <= upperBound2; j++)
					{
						Piece piece = mBoard[i, j];
						if (piece != null && piece.IsFlagSet(128U))
						{
							piece.mCounter++;
						}
					}
				}
			}
		}

		public void DoSpiderRandomMove()
		{
			if (this.mSpiderRandomState == 0)
			{
				if (this.mSpiderCol < this.mStartSpiderCol + 0.25f)
				{
					this.mSpiderCol += 0.013f;
					return;
				}
				this.mSpiderRandomState = 1;
				return;
			}
			else if (this.mSpiderRandomState == 1)
			{
				if (this.mSpiderCol > this.mStartSpiderCol - 0.25f)
				{
					this.mSpiderCol -= 0.013f;
					return;
				}
				this.mSpiderRandomState = 2;
				return;
			}
			else
			{
				if (this.mSpiderRandomState != 2)
				{
					if (this.mSpiderRandomState == 3 && --this.mGrabCounter == 0)
					{
						this.mSpiderRandomBehaviour = false;
						this.mSpider.Play("IDLE");
					}
					return;
				}
				if (this.mSpiderCol < this.mStartSpiderCol)
				{
					this.mSpiderCol += 0.013f;
					return;
				}
				this.mSpiderRandomBehaviour = false;
				this.mSpider.Play("IDLE");
				return;
			}
		}

		public override int GetGameOverCountTreshold()
		{
			return GlobalMembers.M(250);
		}

		public override void GameOverAnnounce()
		{
			Announcement announcement = new Announcement(this, GlobalMembers._ID("GAME OVER", 95));
			announcement.mDarkenBoard = false;
			GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER);
		}

		public override void UnloadContent()
		{
			BejeweledLivePlusApp.UnloadContent("GamePlayQuest_Butterfly");
			BejeweledLivePlusApp.UnloadContent("GamePlay_UI_Normal");
			if (this.mSpider != null && this.mSpider != null)
			{
				this.mSpider.Dispose();
			}
			if (this.mBiggerSpider != null && this.mBiggerSpider != null)
			{
				this.mBiggerSpider.Dispose();
			}
			base.UnloadContent();
		}

		public override void LoadContent(bool threaded)
		{
			if (threaded)
			{
				BejeweledLivePlusApp.LoadContentInBackground("GamePlay_UI_Normal");
			}
			else
			{
				BejeweledLivePlusApp.LoadContent("GamePlay_UI_Normal");
			}
			BejeweledLivePlusApp.LoadContent("GamePlayQuest_Butterfly");
			this.mLastButterflyEffect = null;
			this.mSpider = GlobalMembersResourcesWP.POPANIM_ANIMS_SPIDER.Duplicate();
			this.mBiggerSpider = GlobalMembersResourcesWP.POPANIM_ANIMS_LARGE_SPIDER.Duplicate();
			base.LoadContent(threaded);
		}

		public override void RefreshUI()
		{
			this.mHintButton.SetBorderGlow(true);
			this.mHintButton.Resize(ConstantsWP.BOARD_UI_HINT_BTN_X, ConstantsWP.BUTTERFLYBOARD_HINT_BTN_Y, ConstantsWP.BOARD_UI_HINT_BTN_WIDTH, 0);
			this.mHintButton.mHasAlpha = true;
			this.mHintButton.mDoFinger = true;
			this.mHintButton.mOverAlphaSpeed = 0.1;
			this.mHintButton.mOverAlphaFadeInSpeed = 0.2;
			this.mHintButton.mWidgetFlagsMod.mRemoveFlags |= 4;
			this.mHintButton.mDisabled = false;
			this.mHintButton.SetOverlayType(Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_NONE);
		}

		public override void DrawUI(Graphics g)
		{
			this.DrawTopFrame(g);
			this.DrawBottomFrame(g);
		}

		public override void DrawWarningHUD(Graphics g)
		{
		}

		public override void DrawHUDText(Graphics g)
		{
		}

		public override void DrawBottomFrame(Graphics g)
		{
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY_ID)));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG_ID)));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME_ID)));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM_ID)));
			if (this.WantWarningGlow())
			{
				g.PushState();
				g.SetColor(this.GetWarningGlowColor());
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY_ID)));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM_ID)));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME_ID)));
				g.PopState();
			}
		}

		public override void DrawTopFrame(Graphics g)
		{
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_WEB, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_WEB_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_WEB_ID)));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP_ID)));
			if (this.WantWarningGlow())
			{
				g.PushState();
				g.SetColor(this.GetWarningGlowColor());
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP_ID)));
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.PopState();
			}
		}

		public override void DrawHUD(Graphics g)
		{
		}

		public override void PlayMenuMusic()
		{
			GlobalMembers.gApp.mMusic.PlaySongNoDelay(5, true);
		}

		public override void SetupBackground(int theDeltaIdx)
		{
			string background = string.Empty;
			background = string.Format("images\\{0}\\backgrounds\\lion_tower_cascade_bfly", GlobalMembers.gApp.mArtRes);
			base.SetBackground(background);
		}

		public override int GetBoardY()
		{
			return GlobalMembers.RS(ConstantsWP.BOARD_Y_BUTTERFLY);
		}

		public override bool WantsTutorialReplays()
		{
			return false;
		}

		public override void DoEndLevelDialog()
		{
			this.mEndLevelDialog = new ButterflyEndLevelDialog(this);
			this.mEndLevelDialog.SetQuestName(base.GetQuestName());
			GlobalMembers.gApp.AddDialog(38, this.mEndLevelDialog);
			this.BringToFront(this.mEndLevelDialog);
		}

		public override string GetMusicName()
		{
			return "Butterflies";
		}

		public override float GetModePointMultiplier()
		{
			if (this.mIsPerpetual)
			{
				return 5f;
			}
			return 1f;
		}

		public override float GetRankPointMultiplier()
		{
			return 4f;
		}

		public override bool WantsCalmEffects()
		{
			return true;
		}

		public override bool CanPlay()
		{
			Piece[,] mBoard = this.mBoard;
			int upperBound = mBoard.GetUpperBound(0);
			int upperBound2 = mBoard.GetUpperBound(1);
			for (int i = mBoard.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = mBoard.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = mBoard[i, j];
					if (piece != null && piece.mDestRow == -1)
					{
						return false;
					}
				}
			}
			return base.CanPlay();
		}

		public override void Init()
		{
			if (this.mIsPerpetual)
			{
				this.mUiConfig = Board.EUIConfig.eUIConfig_StandardNoReplay;
			}
			base.Init();
		}

		public override void NewGame(bool restartingGame)
		{
			this.mDefaultDropCountdown = GlobalMembers.sexyatoi(this.mParams, "DropCountdown");
			this.mDefaultMoveCountdown = GlobalMembers.sexyatoi(this.mParams, "MoveCountdown");
			this.mDropCountdownPerLevel = GlobalMembers.sexyatof(this.mParams, "DropCountdownPerLevel");
			this.mMoveCountdownPerLevel = GlobalMembers.sexyatof(this.mParams, "MoveCountdownPerLevel");
			this.mSideSpawnChance = GlobalMembers.sexyatof(this.mParams, "SideSpawnChance");
			this.mSideSpawnChancePerLevel = GlobalMembers.sexyatof(this.mParams, "SideSpawnChancePerLevel");
			this.mSideSpawnChanceMax = GlobalMembers.sexyatof(this.mParams, "SideSpawnChanceMax");
			this.mSpawnCountStart = GlobalMembers.sexyatoi(this.mParams, "SpawnCountStart");
			this.mSpawnCountMax = GlobalMembers.sexyatof(this.mParams, "SpawnCountMax");
			this.mSpawnCountPerLevel = GlobalMembers.sexyatof(this.mParams, "SpawnCountPerLevel");
			if (this.mSpawnCountMax == 0f)
			{
				this.mSpawnCountMax = 1f;
			}
			if (this.mSpawnCountStart == 0)
			{
				this.mSpawnCountStart = 1;
			}
			this.mSpawnCount = (float)this.mSpawnCountStart;
			this.mSpawnCountAcc = 0f;
			this.mQueueSpawn = false;
			this.mDropCountdown = 0;
			this.mPostFXManager.Clear();
			this.mLastButterflyEffect = null;
			if (this.mIsPerpetual)
			{
				this.mHighScoreIsLevelPoints = false;
				this.mShowLevelPoints = false;
			}
			this.mAllowNewComboFloaters = false;
			this.mGrabbedAt = -1;
			this.mPlayedGrabSound = false;
			this.mSpiderCol = 3f;
			this.mSpider.Play("DROP");
			this.mSpiderWalkPct.SetConstant(0.0);
			this.mBiggerSpider.Play("DROP");
			this.mCurrentReleasedButterflies = 0;
			this.mSpotOnSpider = false;
			this.mCountingForGameOver = false;
			this.mGameOverCountdown = 0;
			this.mSpiderYOffset = 0f;
			this.mSpiderDownAfterGrab = true;
			base.NewGame(restartingGame);
		}

		public override void GameOver(bool visible)
		{
			if (!this.mCountingForGameOver)
			{
				GlobalMembers.gApp.mMusic.PlaySongNoDelay(6, false);
				this.mSpotOnSpider = true;
				this.mCountingForGameOver = true;
				this.mGameOverCountdown = 200;
				return;
			}
			if (--this.mGameOverCountdown < 0)
			{
				base.GameOver(visible);
				GlobalMembers.gApp.DisableOptionsButtons(false);
			}
		}

		public override void PieceTallied(Piece thePiece)
		{
			base.PieceTallied(thePiece);
			if (thePiece.IsFlagSet(128U) && (this.mIsPerpetual || this.mGameOverCount == 0))
			{
				this.OnButterflyMatched(thePiece);
			}
		}

		public override void SwapSucceeded(SwapData theSwapData)
		{
			if (!theSwapData.mForceSwap)
			{
				this.mDropCountdown--;
				if (this.CountButterflies() == 0)
				{
					this.mDropCountdown = 0;
				}
				if (this.mDropCountdown <= 0)
				{
					this.SpawnButterfly();
				}
			}
			base.SwapSucceeded(theSwapData);
		}

		public override void DoHypercube(Piece thePiece, int theColor)
		{
			base.DoHypercube(thePiece, theColor);
			this.QueueSpawnButterfly();
		}

		public new bool QueueSwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped, bool destroyTarget)
		{
			if (playerSwapped)
			{
				for (int i = 0; i < this.mSwapDataVector.size<SwapData>(); i++)
				{
					SwapData swapData = this.mSwapDataVector[i];
					if (!swapData.mForceSwap)
					{
						return false;
					}
				}
			}
			return base.QueueSwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, destroyTarget);
		}

		public override void UpdateGame()
		{
			base.UpdateGame();
			bool flag = this.mGameTicks <= GlobalMembers.M(150);
			Piece[,] mBoard = this.mBoard;
			int upperBound = mBoard.GetUpperBound(0);
			int upperBound2 = mBoard.GetUpperBound(1);
			for (int i = mBoard.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = mBoard.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = mBoard[i, j];
					if (piece != null && piece.mDestRow == -1)
					{
						piece.mFallVelocity = 0.01f;
					}
				}
			}
			if (this.mGrabbedAt >= 0 && this.mGameTicks == this.mGrabbedAt + GlobalMembers.M(30))
			{
				int mLevelCompleteCount = this.mLevelCompleteCount;
			}
			if (!this.mSpider.IsActive() && this.mGrabbedAt == -1)
			{
				this.mSpider.Play("IDLE");
			}
			if (this.mSpiderRandomBehaviour)
			{
				this.DoSpiderRandomMove();
			}
			else if (this.mSpiderWalkPct.IsDoingCurve())
			{
				if (!this.mSpiderWalkPct.IncInVal())
				{
					this.mSpiderCol = this.mSpiderWalkColTo;
					this.mSpider.Play("IDLE");
				}
				else
				{
					this.mSpiderCol = (float)(this.mSpiderWalkPct * (double)this.mSpiderWalkColTo + (1.0 - this.mSpiderWalkPct) * (double)this.mSpiderWalkColFrom);
				}
			}
			else
			{
				Piece piece2 = null;
				for (int k = -1; k < (flag ? GlobalMembers.M(8) : GlobalMembers.M(5)); k++)
				{
					for (int l = 0; l < 8; l++)
					{
						Piece piece3 = this.mBoard[Math.Max(0, k), l];
						if (piece3 != null && piece3.IsFlagSet(128U) && (k > -1 || piece3.mDestRow < 0) && (piece2 == null || Math.Abs((float)piece3.mCol - this.mSpiderCol) < Math.Abs((float)piece2.mCol - this.mSpiderCol)))
						{
							piece2 = piece3;
						}
					}
					if (piece2 != null)
					{
						break;
					}
				}
				if (piece2 != null)
				{
					if (this.mSpiderCol == (float)piece2.mCol)
					{
						if (piece2.mDestRow >= 0 && SexyFramework.Common.Rand() % GlobalMembers.M(500) == 0)
						{
							int num = SexyFramework.Common.Rand(4);
							if (piece2.mRow == 0 && num > 0)
							{
								this.mSpiderRandomBehaviour = true;
								this.mSpider.Play("GRAB");
								this.mGrabCounter = 100;
								this.mSpiderRandomState = 3;
							}
							else
							{
								this.mSpiderRandomBehaviour = true;
								this.mSpider.Play("WALK");
								this.mSpiderRandomState = 0;
								this.mStartSpiderCol = this.mSpiderCol;
							}
						}
						if (piece2.mDestRow == -1)
						{
							if (this.mLevelCompleteCount == 0 && this.mGrabbedAt == -1 && piece2.mDestPct >= GlobalMembers.M(0.3))
							{
								this.mSpider.Play("GRAB");
								this.mGrabbedAt = this.mGameTicks;
							}
							if (!this.mGameFinished && piece2.mDestRow == -1 && !piece2.mDestPct.IsDoingCurve())
							{
								if (!this.mPlayedGrabSound)
								{
									GlobalMembers.gSexyApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTERFLY_DEATH1);
									this.mPlayedGrabSound = true;
									piece2.mForceScreenY = true;
									piece2.mForcedY = piece2.mY;
								}
								bool flag2 = false;
								Piece[,] mBoard2 = this.mBoard;
								int upperBound3 = mBoard2.GetUpperBound(0);
								int upperBound4 = mBoard2.GetUpperBound(1);
								for (int m = mBoard2.GetLowerBound(0); m <= upperBound3; m++)
								{
									for (int n = mBoard2.GetLowerBound(1); n <= upperBound4; n++)
									{
										Piece piece4 = mBoard2[m, n];
										if (piece4 != null && piece4.mMoveCreditId != -1 && piece4.mDestRow >= 0)
										{
											flag2 = true;
										}
									}
								}
								Piece[,] mBoard3 = this.mBoard;
								int upperBound5 = mBoard3.GetUpperBound(0);
								int upperBound6 = mBoard3.GetUpperBound(1);
								for (int num2 = mBoard3.GetLowerBound(0); num2 <= upperBound5; num2++)
								{
									for (int num3 = mBoard3.GetLowerBound(1); num3 <= upperBound6; num3++)
									{
										Piece piece5 = mBoard3[num2, num3];
										if (piece5 != null && piece5.mDestRow < 0 && piece5.mRow != 0)
										{
											flag2 = false;
										}
									}
								}
								if (this.mGameOverCount == 0 && !flag2)
								{
									if (this.mSpiderDownAfterGrab)
									{
										this.mSpiderYOffset += 0.5f;
										piece2.mForcedY += 0.5f;
										if (this.mSpiderYOffset > 30f)
										{
											this.mSpiderDownAfterGrab = false;
										}
									}
									else
									{
										this.mSpiderYOffset -= 6f;
										piece2.mForcedY -= 6f;
									}
									this.GameOver();
								}
							}
						}
						else if (SexyFramework.Common.Rand() % GlobalMembers.M(1500) == 0)
						{
							this.mSpider.Play("IDLE");
						}
					}
					else if (flag)
					{
						this.mSpiderCol = (float)piece2.mCol;
					}
					else
					{
						this.mSpider.Play("WALK");
						this.mSpiderWalkColFrom = this.mSpiderCol;
						this.mSpiderWalkColTo = (float)piece2.mCol;
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBUTTERFLY_SPIDER_WALK_PCT, this.mSpiderWalkPct);
						this.mSpiderWalkPct.mIncRate *= GlobalMembers.M(1.8) + (double)Math.Abs((float)piece2.mCol - this.mSpiderCol) * GlobalMembers.M(-0.1) + (double)piece2.mRow * GlobalMembers.M(-0.1);
					}
				}
			}
			this.mSpider.Resize(GlobalMembers.S(this.GetBoardX() + (int)(this.mSpiderCol * 100f) + ConstantsWP.BUTTERFLY_SPIDER_X_OFFSET), (int)(GlobalMembers.MS(-450f + this.mSpiderYOffset) + (float)ConstantsWP.BUTTERFLY_SPIDER_Y_OFFSET), GlobalMembers.S(this.mSpider.mAnimRect.mWidth), GlobalMembers.S(this.mSpider.mAnimRect.mHeight));
			if (this.mGameTicks >= GlobalMembers.M(150))
			{
				this.mSpider.Update();
			}
			if (this.mSpiderYOffset < -500f)
			{
				this.mBiggerSpider.Resize(GlobalMembers.S(this.GetBoardX() + ConstantsWP.BUTTERFLY_BIGSPIDER_X_OFFSET), GlobalMembers.S(ConstantsWP.BUTTERFLY_BIGSPIDER_Y_OFFSET), GlobalMembers.S(this.mSpider.mAnimRect.mWidth), GlobalMembers.S(this.mSpider.mAnimRect.mHeight));
				if (this.mGameOverCount < 400)
				{
					this.mBiggerSpider.Update();
				}
			}
			if (this.mQueueSpawn && this.mLightningStorms.size<LightningStorm>() == 0)
			{
				this.SpawnButterfly();
				this.mQueueSpawn = false;
			}
			Piece[,] mBoard4 = this.mBoard;
			int upperBound7 = mBoard4.GetUpperBound(0);
			int upperBound8 = mBoard4.GetUpperBound(1);
			for (int num4 = mBoard4.GetLowerBound(0); num4 <= upperBound7; num4++)
			{
				for (int num5 = mBoard4.GetLowerBound(1); num5 <= upperBound8; num5++)
				{
					Piece piece6 = mBoard4[num4, num5];
					if (piece6 != null && piece6.IsFlagSet(128U))
					{
						piece6.mShakeScale = Math.Max(0f, 2f - (float)piece6.mRow) / 2f;
						if (piece6.mDestRow == -1)
						{
							piece6.mShakeScale *= (float)(1.0 - piece6.mDestPct * (double)GlobalMembers.M(1f));
						}
						if (piece6.mCounter <= 0 && this.mDeferredTutorialVector.size<DeferredTutorial>() == 0 && this.mLightningStorms.size<LightningStorm>() == 0)
						{
							int num6 = piece6.mRow - 1;
							if (num6 >= 0)
							{
								if (base.GetPieceAtRowCol(num6, piece6.mCol) != null || num6 == -1)
								{
									this.QueueSwap(piece6, num6, piece6.mCol, true, false);
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ANIM_CURVE_B, piece6.mAnimCurve);
								}
								piece6.mCounter = (int)Math.Max(1.0, (double)this.mDefaultMoveCountdown - Math.Floor((double)((float)this.mLevel * piece6.mMoveCountdownPerLevel)));
							}
							else
							{
								int num7 = 0;
								while (num7 < 8 && this.mBoard[num7, piece6.mCol] != null && !base.IsPieceMatching(this.mBoard[num7, piece6.mCol]))
								{
									num7++;
								}
								if (num7 == 8 && this.mLightningStorms.size<LightningStorm>() == 0)
								{
									if (piece6.mDestRow != -1)
									{
										piece6.mDestCol = piece6.mCol;
										piece6.mDestRow = -1;
										GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_DEST_PCT_D, piece6.mDestPct);
									}
									return;
								}
							}
						}
					}
				}
			}
		}

		public override void Update()
		{
			base.Update();
			this.mSpider.mColor = Color.FAlpha(this.GetAlpha());
		}

		public override void LoadGameExtra(Serialiser theBuffer)
		{
			int num = 0;
			GameChunkHeader header = new GameChunkHeader();
			if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkButterflyBoard, header, out num))
			{
				return;
			}
			theBuffer.ReadValuePair(out this.mDefaultMoveCountdown);
			theBuffer.ReadValuePair(out this.mDefaultDropCountdown);
			theBuffer.ReadValuePair(out this.mGrabbedAt);
			theBuffer.ReadValuePair(out this.mDropCountdown);
			theBuffer.ReadValuePair(out this.mSpawnCountStart);
			theBuffer.ReadValuePair(out this.mDropCountdownPerLevel);
			theBuffer.ReadValuePair(out this.mMoveCountdownPerLevel);
			theBuffer.ReadValuePair(out this.mSpawnCountMax);
			theBuffer.ReadValuePair(out this.mSpawnCountPerLevel);
			theBuffer.ReadValuePair(out this.mSpawnCountAcc);
			theBuffer.ReadValuePair(out this.mSpiderCol);
			theBuffer.ReadValuePair(out this.mSpiderWalkColFrom);
			theBuffer.ReadValuePair(out this.mSpiderWalkColTo);
			theBuffer.ReadValuePair(out this.mSpawnCount);
			theBuffer.ReadValuePair(out this.mSideSpawnChance);
			theBuffer.ReadValuePair(out this.mSideSpawnChancePerLevel);
			theBuffer.ReadValuePair(out this.mSideSpawnChanceMax);
			theBuffer.ReadValuePair(out this.mAllowNewComboFloaters);
			theBuffer.ReadValuePair(out this.mSpiderWalkPct);
			base.LoadGameExtra(theBuffer);
		}

		public override bool SaveGameExtra(Serialiser theBuffer)
		{
			int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkButterflyBoard);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyDefMoveCountdown, this.mDefaultMoveCountdown);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyDefDropCountdown, this.mDefaultDropCountdown);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyGrabbedAt, this.mGrabbedAt);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyDropCountdown, this.mDropCountdown);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpawnCountStart, this.mSpawnCountStart);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyDropCountdownPerLevel, this.mDropCountdownPerLevel);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyMoveCountdownPerLevel, this.mMoveCountdownPerLevel);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpawnCountMax, this.mSpawnCountMax);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpawnCountPerLevel, this.mSpawnCountPerLevel);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpawnCountAcc, this.mSpawnCountAcc);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpiderCol, this.mSpiderCol);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpiderWalkColFrom, this.mSpiderWalkColFrom);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpiderWalkColTo, this.mSpiderWalkColTo);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpawnCount, this.mSpawnCount);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySideSpawnChance, this.mSideSpawnChance);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySideSpawnChancePerLevel, this.mSideSpawnChance);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySideSpawnChanceMax, this.mSideSpawnChanceMax);
			theBuffer.WriteValuePair(Serialiser.PairID.BflyAllowNewComboFloaters, this.mAllowNewComboFloaters);
			theBuffer.WriteValuePair(Serialiser.PairID.BflySpiderWalkPct, this.mSpiderWalkPct);
			theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			return base.SaveGameExtra(theBuffer);
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			GlobalMembers.M(380);
			GlobalMembers.M(150);
			GlobalMembers.M(30);
			if (g.mPushedColorVector.Count > 0)
			{
				g.PopColorMult();
			}
			g.PushState();
			g.Translate(this.mSpider.mX + 15, this.mSpider.mY);
			this.mSpider.Draw(g);
			g.PopState();
			base.DrawOverlayPreAnnounce(g, thePriority);
			if (this.mQuestGoal != null)
			{
				this.mQuestGoal.DrawScore(g);
			}
			if (this.mSpotOnSpider)
			{
				base.DrawIris(g, this.mSpider.mX + GlobalMembers.S(145), (int)GlobalMembers.S(60f + this.mSpiderYOffset));
			}
			base.DrawOverlayPostAnnounce(g, thePriority);
			if (this.mSpiderYOffset < -500f)
			{
				g.PushState();
				g.Translate(this.mBiggerSpider.mX, this.mBiggerSpider.mY);
				this.mBiggerSpider.Draw(g);
				g.PopState();
			}
		}

		public override void LevelUp()
		{
			if (!this.mIsPerpetual)
			{
				base.LevelUp();
			}
		}

		public void ReleaseButterfly(Piece thePiece)
		{
			this.mLastButterflyEffect = ButterflyEffect.alloc(thePiece, this);
			if (!this.mIsPerpetual)
			{
				this.mLastButterflyEffect.mTargetY.mOutMin = (double)GlobalMembers.M(590);
			}
			this.mPostFXManager.AddEffect(this.mLastButterflyEffect);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTERFLYESCAPE);
			thePiece.mCounter++;
			thePiece.mAlpha.SetConstant(0.0);
		}

		public void QueueSpawnButterfly()
		{
			this.mQueueSpawn = true;
		}

		public void SpawnButterfly()
		{
			this.mDropCountdown = (int)Math.Max(1.0, (double)this.mDefaultDropCountdown - Math.Floor((double)((float)this.mLevel * this.mDropCountdownPerLevel)));
			this.mSpawnCount = Math.Min(this.mSpawnCountMax, this.mSpawnCount + this.mSpawnCountPerLevel);
			this.mSideSpawnChance = Math.Min(this.mSideSpawnChanceMax, this.mSideSpawnChance + this.mSideSpawnChancePerLevel);
			this.mSpawnCountAcc += this.mSpawnCount;
			while ((double)this.mSpawnCountAcc >= 1.0)
			{
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < 100; j++)
					{
						int theCol;
						if (this.mRand.Next() % 100000U / 100000.0 < (double)this.mSideSpawnChance)
						{
							theCol = ((this.mRand.Next() % 2U == 0U) ? 0 : 7);
						}
						else
						{
							theCol = (int)(this.mRand.Next() % 6U + 1U);
						}
						Piece pieceAtRowCol = base.GetPieceAtRowCol(7, theCol);
						if (pieceAtRowCol != null && !pieceAtRowCol.IsFlagSet(128U) && (pieceAtRowCol.mFlags == 0 || (i == 1 && pieceAtRowCol.mColor > -1)) && base.IsPieceStill(pieceAtRowCol))
						{
							base.Butterflyify(pieceAtRowCol);
							GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTERFLY_APPEAR);
							pieceAtRowCol.mMoveCountdownPerLevel = this.mMoveCountdownPerLevel;
							pieceAtRowCol.mCounter = (int)Math.Max(1.0, (double)this.mDefaultMoveCountdown - Math.Floor((double)((float)this.mLevel * this.mMoveCountdownPerLevel)));
							pieceAtRowCol.mTallied = false;
							i = 2;
							break;
						}
					}
				}
				this.mSpawnCountAcc -= 1f;
			}
		}

		public override void ReadyToPlay()
		{
			if (this.mSpawnCountAcc == 0f)
			{
				this.SpawnButterfly();
			}
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd, int thePointType)
		{
			if (this.mIsPerpetual)
			{
				Points points = base.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, thePointType);
				if (theId > 0U && (this.mAllowNewComboFloaters || this.mPointsManager.Find((uint)(-2 - theMoveCreditId)) != null))
				{
					int moveStat = base.GetMoveStat(theMoveCreditId, 28);
					if (moveStat >= 2 && points != null)
					{
						Points points2 = this.AddPoints(theX, theY, 0, Color.White, (uint)(-2 - theMoveCreditId), true, true, theMoveCreditId, true);
						points2.mX = points.mX;
						points2.mY = points.mY + points.mScale * (float)GlobalMembers.M(1000);
						points2.mTimer = points.mTimer;
						for (int i = 0; i < GlobalMembers.Max_LAYERS; i++)
						{
							points2.mColorCycle[i] = points.mColorCycle[i];
						}
						points2.mString = string.Format(GlobalMembers._ID("x{0} COMBO", 158), moveStat);
					}
				}
				return points;
			}
			return null;
		}

		public override bool WantTopLevelBar()
		{
			return false;
		}

		public int CountButterflies()
		{
			int num = 0;
			Piece[,] mBoard = this.mBoard;
			int upperBound = mBoard.GetUpperBound(0);
			int upperBound2 = mBoard.GetUpperBound(1);
			for (int i = mBoard.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = mBoard.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = mBoard[i, j];
					if (piece != null && piece.IsFlagSet(128U) && !piece.mTallied)
					{
						num++;
					}
				}
			}
			return num;
		}

		public override bool WantWarningGlow(bool forSound)
		{
			if (!this.mGameFinished && !this.mCountingForGameOver)
			{
				for (int i = 0; i < 8; i++)
				{
					Piece pieceAtRowCol = base.GetPieceAtRowCol(0, i);
					if (pieceAtRowCol != null && pieceAtRowCol.IsFlagSet(128U))
					{
						return true;
					}
				}
			}
			return false;
		}

		public override Color GetWarningGlowColor()
		{
			int ticksLeft = this.GetTicksLeft();
			if (ticksLeft == 0)
			{
				return new Color(255, 0, 0, 127);
			}
			int num = ((this.GetTimeLimit() > 60) ? 1500 : 1000);
			float num2 = (float)(num - ticksLeft) / (float)num;
			int num3 = (int)((Math.Sin((double)((float)this.mUpdateCnt * GlobalMembers.M(0.15f))) * 127.0 + 127.0) * (double)num2 * (double)this.GetPieceAlpha());
			return new Color(255, 0, 0, num3 / 2);
		}

		public const int gGiantSpiderAppearingOffsY = -500;

		public int mDefaultMoveCountdown;

		public int mDefaultDropCountdown;

		public int mGrabbedAt;

		public bool mPlayedGrabSound;

		public int mDropCountdown;

		public int mSpawnCountStart;

		public bool mQueueSpawn;

		public float mDropCountdownPerLevel;

		public float mMoveCountdownPerLevel;

		public float mSpawnCountMax;

		public float mSpawnCountPerLevel;

		public float mSpawnCountAcc;

		public float mSideSpawnChance;

		public float mSideSpawnChancePerLevel;

		public float mSideSpawnChanceMax;

		public float mSpiderCol;

		public float mSpiderWalkColFrom;

		public float mSpiderWalkColTo;

		public float mSpawnCount;

		public bool mAllowNewComboFloaters;

		public CurvedVal mSpiderWalkPct = new CurvedVal();

		public ButterflyEffect mLastButterflyEffect;

		public PopAnim mSpider;

		public PopAnim mBiggerSpider;

		public bool mSpiderRandomBehaviour;

		public int mSpiderRandomState;

		public float mStartSpiderCol;

		public int mGrabCounter;

		public int mCurrentReleasedButterflies;

		public bool mSpotOnSpider;

		public bool mCountingForGameOver;

		public int mGameOverCountdown;

		public float mSpiderYOffset;

		public bool mSpiderDownAfterGrab;
	}
}
