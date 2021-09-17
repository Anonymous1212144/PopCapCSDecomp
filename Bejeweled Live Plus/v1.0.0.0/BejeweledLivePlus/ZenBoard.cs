using System;
using System.Collections.Generic;
using System.Text;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Sound;

namespace BejeweledLivePlus
{
	public class ZenBoard : Board
	{
		public ZenBoard()
		{
			if (GlobalMembers.gApp.mSBAFiles.size<string>() > 0 && GlobalMembers.gApp.mProfile.mSBAFileName.Length == 0)
			{
				GlobalMembers.gApp.mProfile.mSBAFileName = GlobalMembers.gApp.mSBAFiles.front<string>();
			}
			this.mZenUIBoardAlpha = 1f;
			this.mDynamicSpeed.SetConstant(1.0);
			this.mLastNoiseIdx = -1;
			this.mAffirmationIndex = -1;
			this.mDebugMantras = false;
			this.mHasZenOptionsDialog = false;
			if (GlobalMembers.gApp.mAffirmationFiles.size<string>() > 0)
			{
				if (GlobalMembers.gApp.mProfile.mAffirmationFileName.Length == 0)
				{
					GlobalMembers.gApp.mProfile.mAffirmationFileName = GlobalMembers.gApp.mAffirmationFiles.front<string>();
				}
				if (GlobalMembers.gApp.mProfile.mAffirmationOn)
				{
					this.LoadAffirmations();
				}
			}
			this.mAffirmationPct = 0f;
			this.mUsingAmbientMusicVolume = false;
			if (GlobalMembers.gApp.mProfile.mBeatOn)
			{
				this.StartupSBA();
			}
			this.mNoiseSoundInstance = null;
			this.mNoiseSoundId = -1;
			this.mBreathSoundInstance = null;
			this.mParams["Title"] = "Zen";
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eZEN_BOARD_NOISE_VOLUME_INIT, this.mNoiseVolume);
			this.mZenOptionsButton = null;
		}

		public override void Dispose()
		{
			GlobalMembers.gApp.mBinauralManager.Load("");
			this.StopZenNoise();
			if (GlobalMembers.gApp.mProfile.mNoiseOn)
			{
				GlobalMembers.gApp.mZenAmbientMusicVolume = GlobalMembers.gApp.mMusicVolume;
				GlobalMembers.gApp.SetMusicVolume(this.mMasterMusicVolume);
			}
			if (this.mNoiseSoundInstance != null)
			{
				this.mNoiseSoundInstance.Release();
			}
			this.mNoiseSoundInstance = null;
			if (this.mNoiseSoundId != -1)
			{
				GlobalMembers.gApp.mSoundManager.ReleaseSound(this.mNoiseSoundId);
			}
			this.mNoiseSoundId = -1;
		}

		public override void UnloadContent()
		{
			BejeweledLivePlusApp.UnloadContent("GamePlay_UI_Normal");
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
			base.LoadContent(threaded);
		}

		public new void NewGame(bool restartingGame)
		{
			base.NewGame(restartingGame);
		}

		public override void LoadGameExtra(Serialiser theBuffer)
		{
			base.LoadGameExtra(theBuffer);
		}

		public override void DrawScore(Graphics g)
		{
			base.DrawScore(g);
		}

		public override void DrawScoreWidget(Graphics g)
		{
		}

		public override int GetLevelPoints()
		{
			return GlobalMembers.M(2500) + Math.Min(this.mLevel, GlobalMembers.M(30)) * GlobalMembers.M(750);
		}

		public void StartupSBA()
		{
			if (GlobalMembers.gApp.mProfile.mBeatOn)
			{
				GlobalMembers.gApp.mBinauralManager.Load("binaural\\" + GlobalMembers.gApp.mProfile.mSBAFileName);
				return;
			}
			GlobalMembers.gApp.mBinauralManager.Load("");
		}

		public void LoadAffirmations()
		{
			this.mAffirmationIndex = -1;
			this.mAffirmations.Clear();
			this.mSubliminalAffirmations.Clear();
			bool flag = false;
			EncodingParser encodingParser = new EncodingParser();
			if (encodingParser.OpenFile(GlobalMembers.gApp.mResourceManager.GetLocaleFolder(true) + "affirmations\\" + GlobalMembers.gApp.mProfile.mAffirmationFileName))
			{
				StringBuilder stringBuilder = new StringBuilder();
				char c = '\0';
				while (encodingParser.GetChar(ref c) == EncodingParser.GetCharReturnType.SUCCESSFUL)
				{
					if (c == '\n' || c == '\r')
					{
						string text = stringBuilder.ToString().Trim();
						if (stringBuilder.Length > 0)
						{
							if (stringBuilder.get_Chars(0) == '#')
							{
								if (string.Compare(text.Substring(0, 6), "#DESC:", 5) == 0)
								{
									this.mAffirmationDesc = text.Substring(6).Trim();
								}
								else if (string.Compare(text.Substring(0, 5), "#DESC", 5) == 0)
								{
									this.mAffirmationDesc = text.Substring(5).Trim();
								}
								else
								{
									flag = true;
								}
							}
							else if (flag)
							{
								this.mSubliminalAffirmations.Add(text);
							}
							else
							{
								this.mAffirmations.Add(text);
							}
						}
						stringBuilder.Clear();
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string text2 = stringBuilder.ToString().Trim();
				if (text2 != string.Empty)
				{
					if (flag)
					{
						this.mSubliminalAffirmations.Add(text2);
						return;
					}
					this.mAffirmations.Add(text2);
				}
			}
		}

		public void LoadAmbientSound()
		{
			if (this.mNoiseSoundInstance != null)
			{
				this.mNoiseSoundInstance.Release();
				this.mNoiseSoundInstance = null;
			}
			if (this.mNoiseSoundId != -1)
			{
				GlobalMembers.gApp.mSoundManager.ReleaseSound(this.mNoiseSoundId);
				this.mNoiseSoundId = -1;
			}
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_INGAME && this.mInterfaceState != InterfaceState.INTERFACE_STATE_PAUSEMENU && this.mInterfaceState != InterfaceState.INTERFACE_STATE_ZENOPTIONSMENU)
			{
				return;
			}
			if (GlobalMembers.gApp.mProfile.mNoiseFileName == "*")
			{
				int num = (int)((ulong)this.mRand.Next() % (ulong)((long)GlobalMembers.gApp.mAmbientFiles.size<string>()));
				if (num == this.mLastNoiseIdx)
				{
					this.mLastNoiseIdx = (this.mLastNoiseIdx + 1) % GlobalMembers.gApp.mAmbientFiles.size<string>();
				}
				else
				{
					this.mLastNoiseIdx = num;
				}
			}
			string mNoiseFileName = GlobalMembers.gApp.mProfile.mNoiseFileName;
			if (string.IsNullOrEmpty(mNoiseFileName))
			{
				this.mNoiseSoundId = -1;
				this.mNoiseSoundInstance = null;
			}
			else if (mNoiseFileName == "*")
			{
				this.mNoiseSoundId = GlobalMembers.gApp.mSoundManager.LoadSound("ambient\\" + SexyFramework.Common.GetFileName(GlobalMembers.gApp.mAmbientFiles[this.mLastNoiseIdx], true));
			}
			else
			{
				this.mNoiseSoundId = GlobalMembers.gApp.mSoundManager.LoadSound("ambient\\" + SexyFramework.Common.GetFileName(GlobalMembers.gApp.mProfile.mNoiseFileName, true));
			}
			if (this.mNoiseSoundId != -1)
			{
				this.mNoiseSoundInstance = GlobalMembers.gApp.mSoundManager.GetSoundInstance(this.mNoiseSoundId);
				if (this.mNoiseSoundInstance != null)
				{
					this.mNoiseSoundInstance.retain();
					if (!GlobalMembers.gApp.IsMuted())
					{
						GlobalMembers.gApp.mSoundManager.SetVolume(2, GlobalMembers.gApp.mZenAmbientVolume);
					}
					this.mNoiseSoundInstance.SetMasterVolumeIdx(2);
					if (GlobalMembers.gApp.mProfile.mNoiseOn)
					{
						this.mNoiseSoundInstance.SetVolume(this.mNoiseVolume);
					}
					else
					{
						this.mNoiseSoundInstance.SetVolume(0.0);
					}
				}
			}
			this.RehupMusicVolume();
		}

		public override void GameOver(bool visible)
		{
			Piece pieceAtRowCol = base.GetPieceAtRowCol((int)(this.mRand.Next() % 8U), (int)(this.mRand.Next() % 8U));
			if (pieceAtRowCol != null)
			{
				this.Hypercubeify(pieceAtRowCol);
			}
		}

		public override void HyperspaceEvent(HYPERSPACEEVENT inEvent)
		{
			base.HyperspaceEvent(inEvent);
			switch (inEvent)
			{
			case HYPERSPACEEVENT.HYPERSPACEEVENT_Start:
			case HYPERSPACEEVENT.HYPERSPACEEVENT_HideAll:
				break;
			case HYPERSPACEEVENT.HYPERSPACEEVENT_OldLevelClear:
				this.CalcBadges();
				this.mBadgeManager.SyncBadges();
				if (GlobalMembers.gApp.mProfile.mDeferredBadgeVector.size<int>() > 0)
				{
					this.mZenDoBadgeAward = true;
					return;
				}
				break;
			default:
			{
				if (inEvent != HYPERSPACEEVENT.HYPERSPACEEVENT_Finish)
				{
					return;
				}
				if (this.mLevel + 1 >= 5 && !GlobalMembers.gApp.mProfile.mEndlessModeUnlocked[1])
				{
					GlobalMembers.gApp.UnlockEndlessMode(EEndlessMode.ENDLESS_BUTTERFLY);
				}
				long thePoints = GlobalMembers.gApp.mProfile.mOfflineRankPoints + (long)GlobalMembers.gApp.mProfile.GetRankPointsBracket((int)((float)this.mGameStats[1] / this.GetRankPointMultiplier()));
				int num = (int)GlobalMembers.gApp.mProfile.GetRankAtPoints(thePoints);
				if (num != GlobalMembers.gApp.mProfile.mOfflineRank)
				{
					this.mDoRankUp = true;
				}
				if (GlobalMembers.gApp.mProfile.mNoiseFileName == "*")
				{
					this.LoadAmbientSound();
					this.PlayZenNoise();
				}
				break;
			}
			}
		}

		public void RehupMusicVolume()
		{
			if (this.mNoiseSoundInstance != null && GlobalMembers.gApp.mProfile.mNoiseOn)
			{
				if (!this.mUsingAmbientMusicVolume)
				{
					this.mMasterMusicVolume = GlobalMembers.gApp.mMusicVolume;
					GlobalMembers.gApp.SetMusicVolume(GlobalMembers.gApp.mZenAmbientMusicVolume);
					this.mUsingAmbientMusicVolume = true;
					return;
				}
			}
			else if (this.mUsingAmbientMusicVolume)
			{
				GlobalMembers.gApp.mZenAmbientMusicVolume = GlobalMembers.gApp.mMusicVolume;
				GlobalMembers.gApp.SetMusicVolume(this.mMasterMusicVolume);
				this.mUsingAmbientMusicVolume = false;
			}
		}

		public override bool AllowPowerups()
		{
			return true;
		}

		public override bool WantsCalmEffects()
		{
			return true;
		}

		public override int GetMinComplementLevel()
		{
			return 1;
		}

		public override float GetGravityFactor()
		{
			return (float)((double)GlobalMembers.M(0.9f) * this.mDynamicSpeed);
		}

		public override float GetSwapSpeed()
		{
			return (float)((double)GlobalMembers.M(0.9f) * this.mDynamicSpeed);
		}

		public override float GetMatchSpeed()
		{
			return (float)((double)GlobalMembers.M(0.75f) * this.mDynamicSpeed);
		}

		public override float GetBoardAlpha()
		{
			return (float)((double)this.mZenUIBoardAlpha * this.mAlpha);
		}

		public override bool WantsLevelBasedBackground()
		{
			return true;
		}

		public override bool WantAnnihilatorReplacement()
		{
			return true;
		}

		public override void ExplodeAt(int theCol, int theRow)
		{
			base.ExplodeAt(theCol, theRow);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eZEN_BOARD_DYNAMIC_SPEED, this.mDynamicSpeed);
		}

		public void FireAffirmation()
		{
			this.mAffirmationPct = GlobalMembers.M(0.7601f);
			if (GlobalMembers.gApp.mProfile.mAffirmationSubliminal)
			{
				int count = this.mAffirmations.Count;
				int count2 = this.mSubliminalAffirmations.Count;
				if (count > 0 || count2 > 0)
				{
					if (count2 > 0)
					{
						this.mAffirmationIndex++;
						if (this.mAffirmationIndex >= count2)
						{
							this.mAffirmationIndex = 0;
						}
					}
					else if (count > 0)
					{
						this.mAffirmationIndex++;
						if (this.mAffirmationIndex >= count)
						{
							this.mAffirmationIndex = 0;
						}
					}
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eZEN_BOARD_AFFIRMATION_CENTER_PCT, this.mAffirmationCenterPct);
					this.mAffirmationCenterPct.mIncRate *= GlobalMembers.M(1.25) + GlobalMembers.M(-1.0) * (double)GlobalMembers.gApp.mProfile.mAffirmationSubliminality;
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eZEN_BOARD_AFFIRMATION_ZOOM, this.mAffirmationZoom, this.mAffirmationCenterPct);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eZEN_BOARD_AFFIRMATION_ALPHA, this.mAffirmationAlpha, this.mAffirmationCenterPct);
					this.mAffirmationAlpha.mOutMax *= GlobalMembers.M(2.5) + Math.Max(GlobalMembers.M(-2.0), GlobalMembers.M(-3.0) * (1.0 - (double)GlobalMembers.gApp.mProfile.mAffirmationSubliminality));
				}
			}
		}

		public override void SwapSucceeded(SwapData theSwapData)
		{
			base.SwapSucceeded(theSwapData);
			Point impliedObject = default(Point);
			int num = 0;
			Piece[,] mBoard = this.mBoard;
			int upperBound = mBoard.GetUpperBound(0);
			int upperBound2 = mBoard.GetUpperBound(1);
			for (int i = mBoard.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = mBoard.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = mBoard[i, j];
					if (piece != null && piece.mMoveCreditId == theSwapData.mPiece1.mMoveCreditId)
					{
						impliedObject.mX += (int)piece.CX();
						impliedObject.mY += (int)piece.CY();
						num++;
					}
				}
			}
			if (num > 0)
			{
				this.mAffirmationOrigin = impliedObject / num;
			}
			if (((double)this.mAffirmationPct > GlobalMembers.M(0.75) && (double)this.mAffirmationPct < GlobalMembers.M(0.76)) || this.mDebugMantras)
			{
				this.FireAffirmation();
			}
		}

		public override HYPERSPACETRANS GetHyperspaceTransType()
		{
			return HYPERSPACETRANS.HYPERSPACETRANS_Zen;
		}

		public override string GetSavedGameName()
		{
			return "zen.sav";
		}

		public override bool AllowSpeedBonus()
		{
			return false;
		}

		public override bool AllowNoMoreMoves()
		{
			return false;
		}

		public override bool SupportsReplays()
		{
			return false;
		}

		public override float GetModePointMultiplier()
		{
			return 1f;
		}

		public override float GetRankPointMultiplier()
		{
			return 0.4f;
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			base.DrawOverlay(g, thePriority);
			if (GlobalMembers.gApp.mProfile.mBreathOn && GlobalMembers.gApp.mProfile.mBreathVisual)
			{
				g.SetColorizeImages(true);
			}
		}

		public override void FirstDraw()
		{
			base.FirstDraw();
		}

		public override void Draw(Graphics g)
		{
			if (!this.mContentLoaded)
			{
				return;
			}
			base.Draw(g);
			if (GlobalMembers.gApp.mProfile.mBreathOn && GlobalMembers.gApp.mProfile.mBreathVisual && this.mHyperspace == null)
			{
				int num = (int)GlobalMembers.S(800.0 * this.mBreathPct);
				int theY = GlobalMembers.S(this.GetBoardY() + 800) - num;
				Rect theRect = new Rect(GlobalMembers.S(this.GetBoardX()), theY, GlobalMembers.S(800), num);
				if (this.mSideXOff != 0.0)
				{
					theRect.mX += (int)(this.mSideXOff * (double)this.mSlideXScale);
				}
				g.SetColor(new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(0) + (int)(((double)GlobalMembers.M(40) * this.mBreathPct + (double)GlobalMembers.M(24)) * (double)this.GetBoardAlpha() * (double)this.GetAlpha())));
				g.FillRect(theRect);
				num = (int)GlobalMembers.S(1200.0 * this.mBreathPct);
				theY = GlobalMembers.S(1200) - num;
				Rect theRect2 = new Rect(GlobalMembers.gApp.mScreenBounds.mX, theY, GlobalMembers.gApp.mScreenBounds.mWidth, num);
				g.SetColor(new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(0) + (int)(((double)GlobalMembers.M(40) * this.mBreathPct + (double)GlobalMembers.M(24)) * (1.0 - (double)this.GetBoardAlpha()) * (double)this.GetAlpha())));
				g.FillRect(theRect2);
			}
			if (GlobalMembers.gApp.mProfile.mAffirmationSubliminal && GlobalMembers.gApp.mProfile.mAffirmationOn && this.mAffirmationAlpha != 0.0 && this.mAffirmationIndex > -1)
			{
				string theString = string.Empty;
				if (this.mSubliminalAffirmations.Count > 0)
				{
					theString = this.mSubliminalAffirmations[this.mAffirmationIndex];
				}
				else if (this.mAffirmations.Count > 0)
				{
					theString = this.mAffirmations[this.mAffirmationIndex];
				}
				g.PushState();
				g.SetColor(Color.White);
				g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
				Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, new Color(255, 255, 255, 0));
				Utils.SetFontLayerColor((ImageFont)g.GetFont(), 1, new Color(255, 255, 255, (int)((double)GlobalMembers.M(40) * this.mAffirmationAlpha * (double)this.GetAlpha())));
				Point point = new Point(GlobalMembers.S(base.GetBoardCenterX()), GlobalMembers.S(base.GetBoardCenterY())) * this.mAffirmationCenterPct + GlobalMembers.S(this.mAffirmationOrigin) * (1.0 - this.mAffirmationCenterPct);
				float num2 = (float)(this.mAffirmationZoom * (double)GlobalMembers.MS(300) / (double)g.StringWidth(theString));
				Utils.PushScale(g, num2, num2, (float)point.mX, (float)point.mY);
				g.WriteString(theString, point.mX, point.mY + GlobalMembers.MS(25));
				Utils.PopScale(g);
				g.PopState();
			}
			base.DrawGameElements(g);
			if (GlobalMembers.gApp.mProfile.mAffirmationOn && !GlobalMembers.gApp.mProfile.mAffirmationSubliminal)
			{
				float num3 = (float)((double)(Math.Min(1f, Math.Min(this.mAffirmationPct * 7f, (GlobalMembers.M(0.9f) - this.mAffirmationPct) * 7f)) * this.GetBoardAlpha()) * (1.0 - this.mTransitionBoardCurve));
				if (num3 > 0f && !string.IsNullOrEmpty(this.mAffirmationText))
				{
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("MAIN", new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255)));
					g.SetFont(GlobalMembersResources.FONT_DIALOG);
					Rect levelBarRect = this.GetLevelBarRect();
					int num4 = levelBarRect.mX + levelBarRect.mWidth / 2;
					int num5 = levelBarRect.mY + levelBarRect.mHeight / 2 - GlobalMembersResources.FONT_DIALOG.GetHeight() / 2 + GlobalMembersResources.FONT_DIALOG.mAscent;
					num4 += this.mTransBoardOffsetX;
					num5 -= this.mTransBoardOffsetY;
					g.SetColor(new Color(255, 255, 255, (int)(255f * num3)));
					Utils.PushScale(g, 0.8f, 0.8f, (float)num4, (float)num5);
					g.DrawString(this.mAffirmationText, num4 - GlobalMembersResources.FONT_DIALOG.StringWidth(this.mAffirmationText) / 2, num5 - 2);
					Utils.PopScale(g);
					((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("MAIN");
				}
			}
		}

		public override void DrawMenuWidget(Graphics g)
		{
			base.DrawMenuWidget(g);
		}

		public override void DoUpdate()
		{
			if (this.mHasZenOptionsDialog)
			{
				this.mSideAlpha.IncInVal();
				return;
			}
			base.DoUpdate();
		}

		public override void Update()
		{
			if (!this.mContentLoaded)
			{
				return;
			}
			if (this.mNoiseVolume.IsDoingCurve())
			{
				this.mNoiseVolume.IncInVal();
				if (GlobalMembers.gApp.mProfile.mNoiseOn && this.mNoiseSoundInstance != null)
				{
					this.mNoiseSoundInstance.SetVolume(this.mNoiseVolume);
				}
			}
			if (this.mHasZenOptionsDialog)
			{
				this.mZenUIBoardAlpha = Math.Max(0f, this.mZenUIBoardAlpha - GlobalMembers.M(0.03f));
			}
			else
			{
				this.mZenUIBoardAlpha = Math.Min(1f, this.mZenUIBoardAlpha + GlobalMembers.M(0.05f));
			}
			base.Update();
			if (GlobalMembers.gApp.mProfile.mBreathOn)
			{
				float num = (float)this.mBreathPct;
				if (!this.mBreathPct.IsDoingCurve())
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eZEN_BOARD_BREATH_PCT, this.mBreathPct);
				}
				this.mBreathPct.mIncRate = GlobalMembers.M(0.00057) + (double)GlobalMembers.gApp.mProfile.mBreathSpeed * GlobalMembers.M(0.001);
				this.mBreathPct.IncInVal();
				if (!GlobalMembers.gApp.IsMuted())
				{
					GlobalMembers.M(-6.0);
					float mBreathSpeed = GlobalMembers.gApp.mProfile.mBreathSpeed;
					GlobalMembers.MS(20.0);
					if (num < 0.01f && this.mBreathPct >= 0.0099999997764825821)
					{
						this.mBreathSoundInstance = GlobalMembers.gApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_BREATH_IN);
						if (this.mBreathSoundInstance != null)
						{
							this.mBreathSoundInstance.Play(false, true);
							this.mBreathSoundInstance.SetVolume(GlobalMembers.gApp.mZenBreathVolume);
						}
					}
					else if (num > 0.99f && this.mBreathPct <= 0.99000000953674316)
					{
						this.mBreathSoundInstance = GlobalMembers.gApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_BREATH_OUT);
						if (this.mBreathSoundInstance != null)
						{
							this.mBreathSoundInstance.Play(false, true);
							this.mBreathSoundInstance.SetVolume(GlobalMembers.gApp.mZenBreathVolume);
						}
					}
				}
			}
			if (GlobalMembers.gApp.mProfile.mAffirmationOn && (double)this.GetBoardAlpha() == 1.0)
			{
				if ((double)this.mAffirmationPct < GlobalMembers.M(0.75) || (double)this.mAffirmationPct >= GlobalMembers.M(0.76))
				{
					if (GlobalMembers.gApp.mProfile.mAffirmationSubliminal)
					{
						this.mAffirmationPct += GlobalMembers.MS(0.001f);
					}
					else
					{
						this.mAffirmationPct += GlobalMembers.MS(5E-05f) + GlobalMembers.gApp.mProfile.mAffirmationSpeed * GlobalMembers.MS(0.001f);
					}
				}
				if (this.mAffirmationPct > 1f && this.mAffirmations.Count > 0)
				{
					this.mAffirmationIndex = (this.mAffirmationIndex + 1) % this.mAffirmations.Count;
					this.mAffirmationText = this.mAffirmations[this.mAffirmationIndex];
					this.mAffirmationPct = 0f;
					if (!GlobalMembers.gApp.mProfile.mAffirmationSubliminal && GlobalMembers.gApp.mProfile.mAffirmationOn)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ZEN_MANTRA1, 0, GlobalMembers.M(0.3));
					}
				}
			}
			this.mDynamicSpeed.IncInVal();
			if (base.GetSelectedPiece() != null || this.mMoveCounter > 0)
			{
				base.SetTutorialCleared(11);
			}
			GlobalMembers.gApp.mProfile.mStats[2] = this.mPoints;
		}

		public override void ButtonDepress(int theId)
		{
			if (this.mGameOverCount > GlobalMembers.M(200))
			{
				return;
			}
			base.SetTutorialCleared(11);
			if (theId == 5)
			{
				((ZenOptionsMenu)GlobalMembers.gApp.mMenus[19]).Expand();
				((ZenOptionsMenu)GlobalMembers.gApp.mMenus[19]).Transition_SlideThenFadeIn();
				return;
			}
			base.ButtonDepress(theId);
		}

		public override void KeyChar(char theChar)
		{
			base.KeyChar(theChar);
		}

		public override void PlayMenuMusic()
		{
			GlobalMembers.gApp.mMusic.PlaySongNoDelay(4, true);
		}

		public override void InitUI()
		{
			base.InitUI();
			if (this.mZenOptionsButton == null)
			{
				this.mZenOptionsButton = new Bej3Button(5, this, Bej3ButtonType.BUTTON_TYPE_LONG);
				this.mZenOptionsButton.SetLabel(GlobalMembers._ID("ZEN OPTIONS", 3442));
				this.AddWidget(this.mZenOptionsButton);
			}
		}

		public override void RefreshUI()
		{
			if (this.mUiConfig == Board.EUIConfig.eUIConfig_Standard || this.mUiConfig == Board.EUIConfig.eUIConfig_StandardNoReplay)
			{
				this.mHintButton.Resize(ConstantsWP.BOARD_UI_HINT_BTN_X, ConstantsWP.BOARD_UI_HINT_BTN_Y, ConstantsWP.BOARD_UI_HINT_BTN_WIDTH, 0);
				this.mHintButton.mHasAlpha = true;
				this.mHintButton.mDoFinger = true;
				this.mHintButton.mOverAlphaSpeed = 0.1;
				this.mHintButton.mOverAlphaFadeInSpeed = 0.2;
				this.mHintButton.mWidgetFlagsMod.mRemoveFlags |= 4;
				this.mHintButton.mDisabled = false;
				this.mHintButton.SetOverlayType(Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_NONE);
			}
			if (this.mReplayButton != null)
			{
				this.mReplayButton.Resize(GlobalMembers.IMGRECT_S(GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON, 0f, (float)this.GetBottomWidgetOffset()));
				this.mReplayButton.mButtonImage = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON;
				this.mReplayButton.mNormalRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelRect(0);
				this.mReplayButton.mOverImage = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON;
				this.mReplayButton.mOverRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelRect(1);
				this.mReplayButton.mDownImage = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON;
				this.mReplayButton.mDownRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelRect(1);
				this.mReplayButton.mHasAlpha = true;
				this.mReplayButton.mDoFinger = true;
				this.mReplayButton.mOverAlphaSpeed = 0.1;
				this.mReplayButton.mOverAlphaFadeInSpeed = 0.2;
				this.mReplayButton.mLabel = "";
			}
			this.mZenOptionsButton.Resize(ConstantsWP.ZENBOARD_UI_ZEN_BTN_X, ConstantsWP.ZENBOARD_UI_ZEN_BTN_Y, ConstantsWP.ZENBOARD_UI_ZEN_BTN_WIDTH, 0);
			this.mZenOptionsButton.mHasAlpha = true;
			this.mZenOptionsButton.mDoFinger = true;
			this.mZenOptionsButton.mOverAlphaSpeed = 0.1;
			this.mZenOptionsButton.mOverAlphaFadeInSpeed = 0.2;
			this.mZenOptionsButton.mWidgetFlagsMod.mRemoveFlags |= 4;
			this.mZenOptionsButton.mDisabled = false;
			this.mZenOptionsButton.SetOverlayType(Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_NONE);
		}

		public override void DrawButtons(Graphics g)
		{
			if (!this.mIsWholeGameReplay)
			{
				g.SetDrawMode(Graphics.DrawMode.Normal);
				float mTransX = g.mTransX;
				float mTransY = g.mTransY;
				g.Translate(this.mZenOptionsButton.mX + (int)GlobalMembers.S(this.mSideXOff) + this.mOffsetX, this.mZenOptionsButton.mY + this.mOffsetY);
				this.mZenOptionsButton.Draw(g);
				g.SetColor(Color.White);
				g.mTransX = mTransX;
				g.mTransY = mTransY;
			}
			base.DrawButtons(g);
		}

		public void MuteZenSounds()
		{
			if (GlobalMembers.gApp.mProfile.mNoiseOn)
			{
				GlobalMembers.gApp.mSoundManager.SetVolume(2, 0.0);
				this.StopZenNoise();
			}
			if (GlobalMembers.gApp.mProfile.mBreathOn)
			{
				GlobalMembers.gApp.mSoundManager.SetVolume(4, 0.0);
			}
		}

		public void UnmuteZenSounds()
		{
			if (GlobalMembers.gApp.mProfile.mNoiseOn)
			{
				this.mNoiseSoundInstance.Play(true, false);
				GlobalMembers.gApp.mSoundManager.SetVolume(2, GlobalMembers.gApp.mZenAmbientVolume);
			}
			if (GlobalMembers.gApp.mProfile.mBreathOn)
			{
				GlobalMembers.gApp.mSoundManager.SetVolume(4, GlobalMembers.gApp.mZenBreathVolume);
			}
		}

		public void PlayZenNoise()
		{
			if (GlobalMembers.gApp.mMuteCount <= 0 && this.mNoiseSoundInstance != null && !this.mNoiseSoundInstance.IsPlaying())
			{
				this.mNoiseSoundInstance.Play(true, false);
				this.mNoiseSoundInstance.SetVolume(GlobalMembers.gApp.mZenAmbientVolume);
			}
		}

		public void StopZenNoise()
		{
			if (this.mNoiseSoundInstance != null && this.mNoiseSoundInstance.IsPlaying())
			{
				this.mNoiseSoundInstance.Stop();
			}
		}

		public void MusicVolumeChanged()
		{
			if (GlobalMembers.gApp.mProfile.mNoiseOn)
			{
				GlobalMembers.gApp.mZenAmbientMusicVolume = GlobalMembers.gApp.mMusicVolume;
			}
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
			GlobalMembers.gApp.mMenus[2].SetVisible(false);
		}

		private static int NegDiv(int num, int den)
		{
			if (num >= 0 || num < -den)
			{
				return num / den;
			}
			return -1;
		}

		public Bej3Button mZenOptionsButton;

		public int mLastNoiseIdx;

		public int mNoiseSoundId;

		public SoundInstance mNoiseSoundInstance;

		public SoundInstance mBreathSoundInstance;

		public CurvedVal mNoiseVolume = new CurvedVal();

		public float mZenUIBoardAlpha;

		public CurvedVal mBreathPct = new CurvedVal();

		public float mAffirmationPct;

		public string mAffirmationDesc = string.Empty;

		public string mAffirmationText = string.Empty;

		public int mAffirmationIndex;

		public List<string> mAffirmations = new List<string>();

		public List<string> mSubliminalAffirmations = new List<string>();

		public Point mAffirmationOrigin = default(Point);

		public CurvedVal mAffirmationCenterPct = new CurvedVal();

		public CurvedVal mAffirmationZoom = new CurvedVal();

		public CurvedVal mAffirmationAlpha = new CurvedVal();

		public bool mDebugMantras;

		public CurvedVal mDynamicSpeed = new CurvedVal();

		public bool mHasZenOptionsDialog;

		public bool mUsingAmbientMusicVolume;

		public double mMasterMusicVolume;
	}
}
