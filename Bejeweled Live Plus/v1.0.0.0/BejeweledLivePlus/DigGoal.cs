using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class DigGoal : QuestGoal
	{
		public static void CalcRotatedBounds(int theW, int theH, double theRot, ref int theNewW, ref int theNewH)
		{
			double num = Math.Cos(theRot);
			double num2 = Math.Sin(theRot);
			theNewW = (int)(Math.Abs(num * (double)theW) + Math.Abs(num2 * (double)theH));
			theNewH = (int)(Math.Abs(num2 * (double)theW) + Math.Abs(num * (double)theH));
		}

		public static int ImgCXOfs(int theId)
		{
			return (int)(GlobalMembersResourcesWP.ImgXOfs(theId) + (float)(GlobalMembersResourcesWP.GetImageById(theId).GetWidth() / 2));
		}

		public static int ImgCYOfs(int theId)
		{
			return (int)(GlobalMembersResourcesWP.ImgYOfs(theId) + (float)(GlobalMembersResourcesWP.GetImageById(theId).GetHeight() / 2));
		}

		public DigGoal(QuestBoard theQuestBoard)
			: base(theQuestBoard)
		{
			this.mDefaultNeverAllowCascades = true;
			for (int i = 0; i < 8; i++)
			{
				List<bool> list = new List<bool>();
				list.Capacity = 8;
				for (int j = 0; j < 8; j++)
				{
					list.Add(false);
				}
				this.mVisited.Add(list);
			}
			this.mSkipMoveCreditIdCheck = false;
			this.mWantTopFrame = true;
			this.mGridDepth = 0.0;
			this.mRandRowIdx = 0;
			this.mArtifactMinTiles = 0;
			this.mArtifactMaxTiles = 8;
			this.mAllowDescent = false;
			this.mForceScroll = false;
			this.mQuestBoard.mScrambleUsesLeft = 1;
			this.mTextFlashTicks = 0;
			this.mAllClearedAnimAtTick = -1;
			this.mClearedAnimAtTick = -1;
			this.mArtifactPossRange.SetConstant(12.0);
			this.mNextArtifactTileCount = -1;
			this.mGoldRushTipPieceId = -1;
			this.mUpdateCnt.value = 0;
			this.mGoldValue = 1;
			this.mGemValue = 1;
			this.mArtifactBaseValue = 1;
			this.mFirstChestPt = true;
			this.mQuestBoard.mShowLevelPoints = !this.mQuestBoard.mIsPerpetual;
			this.mBoardOffsetXExtra = 0;
			this.mBoardOffsetYExtra = 0;
			this.mNextBottomHypermixerWait = 0;
			this.mInMegaDig = false;
			this.mDigBarFlashCount = 0;
			this.mClearedAnimPlayed = false;
			this.mAllClearAnimPlayed = false;
			GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.ResetAnim();
			GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.ResetAnim();
			this.mBlackrockTipPieceId = -1;
			for (int k = 0; k < 3; k++)
			{
				this.mTreasureEarnings[k] = 0;
			}
			this.mDigBarFlash.SetConstant(0.0);
			this.mDigBarFlash.SetMode(0);
			this.mQuestBoard.mHypermixerCheckRow = GlobalMembers.M(3);
			this.mInitPushAnimPixels = 0;
			this.mUsingRandomizers = false;
			this.mGoldPointsFXManager = new EffectsManager(this.mQuestBoard);
			this.mDustFXManager = new EffectsManager(this.mQuestBoard);
			this.mMessageFXManager = new EffectsManager(this.mQuestBoard);
			this.mGoldPointsFXManager.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mDustFXManager.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mMessageFXManager.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mCvScrollY.SetConstant(0.0);
			this.mCvScrollY.mAppUpdateCountSrc = this.mUpdateCnt;
			this.mCvShakey.mAppUpdateCountSrc = this.mUpdateCnt;
			for (int l = 0; l < 2; l++)
			{
				for (int m = 0; m < 8; m++)
				{
					this.mOldPieceData[l, m] = new DigGoal.OldPieceData();
					this.mOldPieceData[l, m].mFlags = -1;
					this.mOldPieceData[l, m].mColor = -1;
				}
			}
			this.mLoadingGame = false;
			this.mDigInProgress = false;
		}

		public override void Dispose()
		{
			for (int i = 0; i < this.mGoldFx.Count; i++)
			{
				if (this.mGoldFx[i] != null)
				{
					this.mGoldFx[i].Dispose();
				}
			}
			this.mGoldFx.Clear();
			foreach (DigGoal.TileData tileData in this.mIdToTileData.Values)
			{
				if (tileData.mDiamondFx != null)
				{
					tileData.mDiamondFx.Dispose();
					tileData.mDiamondFx = null;
				}
				if (tileData.mGoldInnerFx != null)
				{
					tileData.mGoldInnerFx.Dispose();
					tileData.mGoldInnerFx = null;
				}
				if (tileData.mBlingPIFx != null && tileData.mBlingPIFx.mRefCount > 0)
				{
					tileData.mBlingPIFx.mRefCount--;
				}
			}
			for (int j = 0; j < this.mMovingPieces.Count; j++)
			{
				if (this.mMovingPieces[j] != null)
				{
					this.mMovingPieces[j].Dispose();
				}
			}
			this.mMovingPieces.Clear();
			this.mGoldPointsFXManager.Dispose();
			this.mDustFXManager.Dispose();
			this.mMessageFXManager.Dispose();
		}

		public override int GetBoardX()
		{
			return this.mQuestBoard.CallBoardGetBoardX() + this.mBoardOffsetXExtra;
		}

		public override int GetBoardY()
		{
			return this.mQuestBoard.CallBoardGetBoardY() + this.GetBoardScrollOffsetY() + this.mBoardOffsetYExtra;
		}

		public void SyncGrid(int theStartRow)
		{
			bool[] array = new bool[8];
			bool flag = theStartRow != 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = flag;
			}
			int num = 8;
			for (int j = theStartRow; j < 8; j++)
			{
				int k = j + this.GetGridDepth();
				Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
				this.mArtifactPoss.Clear();
				for (int l = 0; l < this.mArtifacts.Count; l++)
				{
					if (this.GetGridDepth() >= this.mArtifacts[l].mMinDepth && this.mPlacedArtifacts.IndexOf(l) < 0 && this.mCollectedArtifacts.IndexOf(l) < 0)
					{
						int mMinDepth = this.mArtifacts[l].mMinDepth;
						if (!dictionary.ContainsKey(mMinDepth))
						{
							dictionary.Add(mMinDepth, new List<int>());
						}
						dictionary[this.mArtifacts[l].mMinDepth].Add(l);
					}
				}
				float num2 = 1f;
				float num3 = (float)this.mRandRowIdx * num2;
				int num4 = (int)Utils.Round((float)this.mArtifactPossRange.GetOutVal((double)num3));
				List<int> list = new List<int>();
				list.AddRange(dictionary.Keys);
				int[] array2 = list.ToArray();
				for (int m = 0; m < array2.Length - 1; m++)
				{
					for (int n = m + 1; n < array2.Length; n++)
					{
						if (array2[m] < array2[n])
						{
							int num5 = array2[m];
							array2[m] = array2[n];
							array2[n] = num5;
						}
					}
				}
				foreach (int num7 in array2)
				{
					List<int> list2 = dictionary[num7];
					for (int num8 = 0; num8 < list2.Count; num8++)
					{
						int num9 = BejeweledLivePlus.Misc.Common.Rand() % list2.Count;
						int num10 = list2[num8];
						list2[num8] = list2[num9];
						list2[num9] = num10;
					}
					int num11 = 0;
					while (num11 < list2.Count && this.mArtifactPoss.Count < num4)
					{
						this.mArtifactPoss.Add(list2[num11]);
						num11++;
					}
				}
				this.mArtifactPoss.Reverse();
				List<Piece> list3 = new List<Piece>();
				for (int num12 = 0; num12 < 8; num12++)
				{
					bool flag2 = false;
					Piece pieceAtRowCol = this.mQuestBoard.GetPieceAtRowCol(j, num12);
					if (pieceAtRowCol != null)
					{
						bool flag3 = false;
						bool flag4 = this.mQuestBoard.mIsPerpetual;
						DigGoal.TileData tileData = null;
						if (this.mUsingRandomizers && k >= this.mGridData.GetRowCount())
						{
							tileData = this.GenRandomTile(num3, pieceAtRowCol, ref flag3);
						}
						else
						{
							while (k >= this.mGridData.GetRowCount())
							{
								k -= 8;
							}
							GridData.TileData tileData2 = this.mGridData.At(k, num12);
							char c = char.ToUpper(tileData2.mAttr);
							Piece pieceAtRowCol2 = this.mQuestBoard.GetPieceAtRowCol(j, num12);
							if (c == 'H')
							{
								if (tileData2.mBack > 0U)
								{
									this.mHypercubes.Add(pieceAtRowCol2.mId);
								}
								else
								{
									this.mQuestBoard.Hypercubeify(pieceAtRowCol2);
								}
								flag4 = false;
							}
							else if (c == 'S')
							{
								pieceAtRowCol2.SetFlag(4096U);
								pieceAtRowCol2.mCanScramble = false;
								flag4 = false;
							}
							else if (c == 'R' && this.mUsingRandomizers)
							{
								tileData = this.GenRandomTile(num3, pieceAtRowCol2, ref flag3);
								if (pieceAtRowCol2.IsFlagSet(65536U) && pieceAtRowCol2.mId == this.mBlackrockTipPieceId && this.mQuestBoard.WantsTutorial(16))
								{
									flag4 = false;
								}
							}
							if (tileData2.mBack > 0U)
							{
								if (this.mIdToTileData.ContainsKey(pieceAtRowCol2.mId))
								{
									tileData = this.mIdToTileData[pieceAtRowCol2.mId];
								}
								else
								{
									tileData = new DigGoal.TileData();
									this.mIdToTileData[pieceAtRowCol2.mId] = tileData;
								}
								pieceAtRowCol2.SetFlag(65536U);
								if (c == 'M')
								{
									flag4 = false;
									tileData.SetAs(DigGoal.EDigPieceType.eDigPiece_Goal, (int)tileData2.mBack, pieceAtRowCol2, this);
								}
								else if (c == 'A')
								{
									tileData.SetAs(DigGoal.EDigPieceType.eDigPiece_Artifact, (int)tileData2.mBack, pieceAtRowCol2, this);
									flag4 = false;
								}
								else
								{
									tileData.SetAs(DigGoal.EDigPieceType.eDigPiece_Block, (int)tileData2.mBack, pieceAtRowCol2, this);
									flag4 = false;
								}
								flag3 = true;
							}
						}
						if (tileData != null)
						{
							if (this.mNextArtifactTileCount-- <= 0)
							{
								flag2 = true;
							}
							tileData.mIsTopTile = !array[num12];
							if (tileData.mIsTopTile)
							{
								array[num12] = true;
							}
						}
						if (flag2 && flag4)
						{
							list3.Add(pieceAtRowCol);
						}
						if (flag3)
						{
							pieceAtRowCol.mCanScramble = false;
							pieceAtRowCol.mCanSwap = false;
							pieceAtRowCol.mCanDestroy = true;
							pieceAtRowCol.mCanMatch = false;
							pieceAtRowCol.mColor = -1;
							pieceAtRowCol.mX = (float)this.mQuestBoard.GetColX(pieceAtRowCol.mCol);
							pieceAtRowCol.mY = (float)this.mQuestBoard.GetRowY(pieceAtRowCol.mRow);
							num = Math.Min(pieceAtRowCol.mRow, num);
						}
						if (flag2 && list3.Count > 0 && this.mArtifactPoss.Count > 0)
						{
							Piece piece = list3[BejeweledLivePlus.Misc.Common.Rand() % list3.Count];
							float outVal = this.mArtifactSpread.GetOutVal(GlobalMembersUtils.GetRandFloatU());
							int num13 = (int)Utils.Round((float)(this.mArtifactPoss.Count - 1) * outVal);
							this.mIdToTileData[piece.mId].SetAs(DigGoal.EDigPieceType.eDigPiece_Artifact, this.mArtifactPoss[num13], piece, this);
							this.mPlacedArtifacts.Add(this.mArtifactPoss[num13]);
							this.mArtifactPoss.RemoveAt(num13);
							this.AdvanceArtifact(false);
						}
					}
				}
				if (this.mUsingRandomizers)
				{
					this.mRandRowIdx++;
				}
			}
			this.mInitPushAnimPixels = (8 - num + 1) * 100 + GlobalMembers.M(500);
			this.ResyncInitPushAnim();
		}

		public int GetDarkenedRGBForRow(int theRow, bool theIsShadow)
		{
			float num = (float)this.GetExtraLuminosity(this.GetGridDepth() + theRow);
			int num2 = GlobalMembers.M(255) - (int)((double)num * GlobalMembers.M(200.0));
			int num3 = GlobalMembers.M(255) - (int)((double)num * GlobalMembers.M(170.0));
			int num4 = (num3 << 16) | (num2 << 8) | num2;
			if (theIsShadow)
			{
				return Utils.ColorLerp(new Color(num4), new Color(GlobalMembers.M(4210752)), 0.5f).ToInt();
			}
			return num4;
		}

		public void AdvanceArtifact(bool theFirstOne)
		{
			if (!this.mQuestBoard.mIsPerpetual)
			{
				return;
			}
			this.mNextArtifactTileCount = this.mArtifactMinTiles + BejeweledLivePlus.Misc.Common.Rand(this.mArtifactMaxTiles - this.mArtifactMinTiles);
			if (theFirstOne)
			{
				this.mNextArtifactTileCount += this.mArtifactSkipTileCount;
			}
		}

		public void UpdateShift()
		{
			float num = (float)this.GetDigCountPerScroll();
			float num2 = (float)(this.mCvScrollY.GetOutVal() * (double)num);
			this.mCvScrollY.IncInVal();
			float num3 = (float)(this.mCvScrollY.GetOutVal() * (double)num);
			if (((int)num2 != (int)num3 || (num2 != num3 && (double)num2 == 0.0)) && (int)num3 < this.GetDigCountPerScroll())
			{
				this.ShiftRowsUp();
			}
			if (this.mCvScrollY.HasBeenTriggered())
			{
				((DigBoard)this.mQuestBoard).mRotatingCounter.IncByNum((int)this.mCvScrollY * this.GetDigCountPerScroll() * 10);
				this.mGridDepth += (double)((int)this.mCvScrollY * this.GetDigCountPerScroll());
				this.mCvScrollY.SetConstant(0.0);
			}
		}

		public void ShiftRowsUp()
		{
			for (int i = this.mMovingPieces.Count - 1; i >= 0; i--)
			{
				Piece piece = this.mMovingPieces[i];
				if (piece != null)
				{
					this.mQuestBoard.mPreFXManager.FreePieceEffect(piece);
					this.mQuestBoard.mPostFXManager.FreePieceEffect(piece);
					piece.release();
				}
			}
			this.mMovingPieces.Clear();
			for (int j = 0; j < 8; j++)
			{
				this.mMovingPieces.Add(null);
			}
			List<int> newGemColors = this.mQuestBoard.GetNewGemColors();
			for (int k = 0; k < 8; k++)
			{
				for (int l = 0; l < 8; l++)
				{
					Piece piece2 = this.mQuestBoard.mBoard[k, l];
					if (k == 0)
					{
						this.mOldPieceData[0, l] = this.mOldPieceData[1, l];
						if (piece2 != null)
						{
							this.mOldPieceData[1, l].mFlags = piece2.mFlags;
							this.mOldPieceData[1, l].mColor = piece2.mColor;
						}
						else
						{
							this.mOldPieceData[1, l].mFlags = -1;
							this.mOldPieceData[1, l].mColor = -1;
						}
					}
					if (piece2 != null)
					{
						if (k == 0)
						{
							this.mMovingPieces[l] = piece2;
							piece2.ClearBoundEffects();
							piece2.mX = (float)(100 * l);
							piece2.mY = (float)(100 * (k - 1));
						}
						else
						{
							this.mQuestBoard.mBoard[k - 1, l] = piece2;
							piece2.mX = (float)(100 * l);
							piece2.mY = (float)(100 * (k - 1));
							piece2.mRow = k - 1;
						}
						this.mQuestBoard.mBoard[k, l] = null;
					}
					if (k == 7)
					{
						int num = 25;
						piece2 = this.mQuestBoard.CreateNewPiece(k, l);
						while (num-- > 0 && this.mQuestBoard.FindMove(null, 0, true, true, false, piece2))
						{
							piece2.mColor = newGemColors[BejeweledLivePlus.Misc.Common.Rand() % newGemColors.Count];
						}
					}
				}
			}
			this.SyncGrid(7);
			if (!this.mQuestBoard.FindMove(null, 0, true, true))
			{
				int num2 = 50;
				while (--num2 > 0)
				{
					Piece piece3 = this.mQuestBoard.mBoard[0, BejeweledLivePlus.Misc.Common.Rand() % 8];
					if (piece3 != null)
					{
						this.mQuestBoard.Hypercubeify(piece3);
						return;
					}
				}
			}
		}

		public override int GetSidebarTextY()
		{
			return GlobalMembers.MS(265);
		}

		public override void GameOverExit()
		{
			this.mQuestBoard.mPoints = this.mQuestBoard.mLevelPointsTotal;
		}

		public override bool CanPlay()
		{
			return !this.mCvScrollY.IsDoingCurve() && base.CanPlay();
		}

		public override int GetTimeDrawX()
		{
			if (!this.mQuestBoard.mIsPerpetual)
			{
				return this.mQuestBoard.CallBoardGetTimeDrawX();
			}
			Rect countdownBarRect = this.mQuestBoard.GetCountdownBarRect();
			return countdownBarRect.mX + (int)((float)countdownBarRect.mWidth * this.mQuestBoard.mCountdownBarPct);
		}

		public int GetDigCountPerScroll()
		{
			return this.mDigCountPerScroll * (this.mInMegaDig ? 2 : 1);
		}

		public virtual void SetShadowClipRect(Graphics g, int theXOff, int theYOff)
		{
			g.SetClipRect(new Rect(theXOff, GlobalMembers.S(this.mQuestBoard.CallBoardGetBoardY() + this.mBoardOffsetYExtra + 600 + GlobalMembers.M(6)) + theYOff, GlobalMembers.gApp.mWidth * 2, GlobalMembers.S(200 + GlobalMembers.M(10))));
		}

		public void SetAdditiveClipRect(Graphics g, int theXOff, int theYOff)
		{
			g.SetClipRect(new Rect(theXOff, GlobalMembers.S(this.mQuestBoard.CallBoardGetBoardY() + this.mBoardOffsetYExtra + GlobalMembers.M(6)) + theYOff, GlobalMembers.gApp.mWidth * 2, GlobalMembers.S(600 + GlobalMembers.M(10))));
		}

		public bool CheckNeedScroll(bool theMegaDig)
		{
			if (!this.mQuestBoard.mIsPerpetual)
			{
				return false;
			}
			if (this.mForceScroll)
			{
				return true;
			}
			for (int i = (theMegaDig ? 7 : 5); i >= 0; i--)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mQuestBoard.mBoard[i, j];
					if (piece != null && piece.IsFlagSet(65536U))
					{
						return false;
					}
				}
			}
			return true;
		}

		public bool IsDoubleHypercubeActive()
		{
			for (int i = 0; i < this.mQuestBoard.mLightningStorms.Count; i++)
			{
				LightningStorm lightningStorm = this.mQuestBoard.mLightningStorms[i];
				if (lightningStorm.mStormType == 7 && lightningStorm.mColor == -1)
				{
					return true;
				}
			}
			return false;
		}

		public double GetExtraLuminosity(int theDepth)
		{
			return Math.Max(0.0, Math.Min(1.0, (double)(theDepth - GlobalMembers.M(4)) / GlobalMembers.M(40.0)));
		}

		public void ResyncInitPushAnim()
		{
			if (this.mInitPushAnimCv.IsDoingCurve())
			{
				foreach (int theId in this.mIdToTileData.Keys)
				{
					Piece pieceById = this.mQuestBoard.GetPieceById(theId);
					if (pieceById != null)
					{
						pieceById.mX = (float)this.mQuestBoard.GetColX(pieceById.mCol);
						pieceById.mY = (float)this.mQuestBoard.GetRowY(pieceById.mRow) + (float)GlobalMembers.S((double)this.mInitPushAnimPixels * this.mInitPushAnimCv);
					}
				}
			}
		}

		public override void SetupBackground(int theDeltaIdx)
		{
			if (!this.mQuestBoard.mIsPerpetual)
			{
				this.mQuestBoard.CallBoardSetupBackground(theDeltaIdx);
				return;
			}
			GlobalMembers.gApp.ClearUpdateBacklog(false);
		}

		public override Rect GetCountdownBarRect()
		{
			if (this.mQuestBoard.mIsPerpetual)
			{
				return new Rect((int)(GlobalMembers.IMG_SXOFS(874) + (float)(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER.mWidth / 2)), (int)GlobalMembers.IMG_SYOFS(874), GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK.mWidth - GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER.mWidth, GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK.mHeight);
			}
			return this.mQuestBoard.CallBoardGetCountdownBarRect();
		}

		public override bool WantWarningGlow()
		{
			return (!this.mQuestBoard.mIsPerpetual || !this.CheckNeedScroll(false)) && this.mQuestBoard.CallBoardWantWarningGlow();
		}

		public void DrawScrollingDigLineText(Graphics g, Font theFont, int theY, string theStr, int theTicksElapsed, CurvedVal theAnimX, CurvedVal theAnimScale, CurvedVal theAnimAlpha, CurvedVal theColorCycle, bool theUseHueColorCycle, int theShadowCount, int theShadowTotal, int theLastX)
		{
			this.DrawScrollingDigLineText(g, theFont, theY, theStr, theTicksElapsed, theAnimX, theAnimScale, theAnimAlpha, theColorCycle, theUseHueColorCycle, theShadowCount, theShadowTotal, theLastX, 0);
		}

		public void DrawScrollingDigLineText(Graphics g, Font theFont, int theY, string theStr, int theTicksElapsed, CurvedVal theAnimX, CurvedVal theAnimScale, CurvedVal theAnimAlpha, CurvedVal theColorCycle, bool theUseHueColorCycle, int theShadowCount, int theShadowTotal)
		{
			this.DrawScrollingDigLineText(g, theFont, theY, theStr, theTicksElapsed, theAnimX, theAnimScale, theAnimAlpha, theColorCycle, theUseHueColorCycle, theShadowCount, theShadowTotal, 0, 0);
		}

		public void DrawScrollingDigLineText(Graphics g, Font theFont, int theY, string theStr, int theTicksElapsed, CurvedVal theAnimX, CurvedVal theAnimScale, CurvedVal theAnimAlpha, CurvedVal theColorCycle, bool theUseHueColorCycle, int theShadowCount)
		{
			this.DrawScrollingDigLineText(g, theFont, theY, theStr, theTicksElapsed, theAnimX, theAnimScale, theAnimAlpha, theColorCycle, theUseHueColorCycle, theShadowCount, 0, 0, 0);
		}

		public void DrawScrollingDigLineText(Graphics g, Font theFont, int theY, string theStr, int theTicksElapsed, CurvedVal theAnimX, CurvedVal theAnimScale, CurvedVal theAnimAlpha, CurvedVal theColorCycle, bool theUseHueColorCycle)
		{
			this.DrawScrollingDigLineText(g, theFont, theY, theStr, theTicksElapsed, theAnimX, theAnimScale, theAnimAlpha, theColorCycle, theUseHueColorCycle, 0, 0, 0, 0);
		}

		public void DrawScrollingDigLineText(Graphics g, Font theFont, int theY, string theStr, int theTicksElapsed, CurvedVal theAnimX, CurvedVal theAnimScale, CurvedVal theAnimAlpha, CurvedVal theColorCycle, bool theUseHueColorCycle, int theShadowCount, int theShadowTotal, int theLastX, int theLastY)
		{
			float num = (float)theTicksElapsed / 100f;
			double num2 = theAnimAlpha.GetOutVal((double)num);
			double outVal = theAnimScale.GetOutVal((double)num);
			double outVal2 = theAnimX.GetOutVal((double)num);
			double num3 = (double)this.mQuestBoard.GetBoardX() + 800.0 * outVal2;
			double num4 = (double)ConstantsWP.DIG_BOARD_CLEARED_TEXT_Y;
			if (theShadowCount > 0)
			{
				int theShadowTotal2 = ((theShadowTotal <= 0) ? theShadowCount : theShadowTotal);
				int theShadowCount2 = theShadowCount - 1;
				if (theTicksElapsed >= 0)
				{
					this.DrawScrollingDigLineText(g, theFont, theY, theStr, theTicksElapsed - GlobalMembers.M(4), theAnimX, theAnimScale, theAnimAlpha, theColorCycle, theUseHueColorCycle, theShadowCount2, theShadowTotal2, (int)num3, (int)num4);
				}
			}
			g.PushState();
			g.SetClipRect(GlobalMembers.S(this.GetBoardX() + GlobalMembers.M(0)), GlobalMembers.S(this.GetBoardY()), GlobalMembers.S(800 + GlobalMembers.M(0)), GlobalMembers.S(800));
			g.SetFont(theFont);
			Utils.SetFontLayerColor((ImageFont)theFont, 0, Bej3Widget.COLOR_INGAME_ANNOUNCEMENT);
			Utils.SetFontLayerColor((ImageFont)theFont, 1, Color.White);
			g.Translate(0, GlobalMembers.S(theY));
			Color color = default(Color);
			color = new Color((int)GlobalMembers.gApp.HSLToRGB(GlobalMembers.M(0) + (int)theColorCycle.GetOutVal((double)num % theColorCycle.mInMax), GlobalMembers.M(250), GlobalMembers.M(110)), GlobalMembers.M(200));
			bool flag = false;
			if (theShadowTotal > 0)
			{
				double num5 = (double)theShadowCount / (double)theShadowTotal;
				if (GlobalMembers.M(1) != 0)
				{
					num2 *= Math.Pow(num5 * GlobalMembers.M(0.75), GlobalMembers.M(1.0));
				}
				if (GlobalMembers.M(0) != 0)
				{
					g.SetDrawMode(Graphics.DrawMode.Additive);
				}
				if (GlobalMembers.M(1) != 0)
				{
					color = Utils.ColorLerp(new Color(GlobalMembers.M(0)), new Color(color.ToInt()), (float)(num5 * GlobalMembers.M(0.75)));
				}
				double num6 = (double)theLastX - num3;
				double num7 = (double)theLastY - num4;
				if (GlobalMembers.M(1) != 0 && num6 == 0.0 && num7 == 0.0)
				{
					flag = true;
				}
			}
			num4 -= GlobalMembers.RS((double)g.mFont.GetAscent() / 2.0);
			if (!flag && num2 > (double)GlobalMembers.M(0.01f))
			{
				g.SetColor(Color.FAlpha((float)num2));
				g.SetColorizeImages(num2 < 1.0);
				num4 -= (double)GlobalMembers.RS(g.mFont.GetAscent()) / 2.0 * outVal;
				g.SetScale((float)outVal, (float)outVal, (float)GlobalMembers.S(num3), (float)GlobalMembers.S(num4));
				int num8 = (int)((float)this.GetBoardX() + GlobalMembers.S(ConstantsWP.DIG_BOARD_CLEARED_TEXT_RECT_X));
				int num9 = (int)GlobalMembers.S(num4);
				int num10 = 0;
				int num11 = 0;
				int wordWrappedHeight = g.GetWordWrappedHeight((int)(ConstantsWP.DEVICE_WIDTH_F - (float)(num8 * 2)), theStr, -1, ref num10, ref num11);
				g.WriteWordWrapped(new Rect(num8, num9 - wordWrappedHeight / 2, GlobalMembers.S(800) - (int)(GlobalMembers.S(ConstantsWP.DIG_BOARD_CLEARED_TEXT_RECT_X) * 2f), num9 + wordWrappedHeight), theStr, -1, 0);
			}
			g.PopState();
		}

		public bool IsSpecialPieceUnlocked(Piece thePiece)
		{
			for (int i = 0; i < this.IsSpecialPieceUnlocked_neighbors.GetLength(0); i++)
			{
				int num = thePiece.mCol + this.IsSpecialPieceUnlocked_neighbors[i, 0];
				int num2 = thePiece.mRow + this.IsSpecialPieceUnlocked_neighbors[i, 1];
				if (num2 >= 0 && num2 < 8 && num >= 0 && num < 8)
				{
					Piece piece = this.mQuestBoard.mBoard[num2, num];
					if (piece == null || !this.mIdToTileData.ContainsKey(piece.mId))
					{
						bool flag = true;
						for (int j = num2; j >= 0; j--)
						{
							Piece piece2 = this.mQuestBoard.mBoard[j, num];
							if (piece2 != null && this.mIdToTileData.ContainsKey(piece2.mId))
							{
								flag = false;
							}
						}
						if (flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public override bool IsHypermixerCancelledBy(Piece thePiece)
		{
			return thePiece.IsFlagSet(2U) && !thePiece.IsFlagSet(65536U);
		}

		public override bool ExtraSaveGameInfo()
		{
			return this.mQuestBoard.mIsPerpetual;
		}

		public override bool DoEndLevelDialog()
		{
			if (this.mQuestBoard.mIsPerpetual)
			{
				EndLevelDialog endLevelDialog = new DigGoalEndLevelDialog(this.mQuestBoard);
				endLevelDialog.SetQuestName("Diamond Mine");
				GlobalMembers.gApp.AddDialog(38, endLevelDialog);
				this.mQuestBoard.BringToFront(endLevelDialog);
				return true;
			}
			return false;
		}

		public override bool SaveGameExtra(Serialiser theBuffer)
		{
			if (!this.mQuestBoard.mIsPerpetual)
			{
				return false;
			}
			if (this.mCvScrollY.mInMin != this.mCvScrollY.mInMax && !this.mCvScrollY.HasBeenTriggered())
			{
				return false;
			}
			if (this.mGoldFx.Count != 0)
			{
				return false;
			}
			int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkDigGoal);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.DigIdToTileData, this.mIdToTileData.Count);
			foreach (int num in this.mIdToTileData.Keys)
			{
				DigGoal.TileData tileData = this.mIdToTileData[num];
				theBuffer.WriteLong((long)num);
				theBuffer.WriteShort((short)tileData.mStrength.value);
				theBuffer.WriteShort((short)tileData.mStartStrength);
				theBuffer.WriteShort((short)tileData.mPieceType);
				theBuffer.WriteLong((long)((ulong)tileData.mRandCfg));
				theBuffer.WriteBoolean(tileData.mIsTopTile);
			}
			theBuffer.WriteArrayPair(Serialiser.PairID.DigTreasureEarnings, 3, this.mTreasureEarnings);
			theBuffer.WriteValuePair(Serialiser.PairID.DigTextFlashTicks, this.mTextFlashTicks);
			theBuffer.WriteValuePair(Serialiser.PairID.DigNextBottomHypermixerWait, this.mNextBottomHypermixerWait);
			theBuffer.WriteValuePair(Serialiser.PairID.DigArtifactMinTiles, this.mArtifactMinTiles);
			theBuffer.WriteValuePair(Serialiser.PairID.DigArtifactMaxTiles, this.mArtifactMaxTiles);
			theBuffer.WriteValuePair(Serialiser.PairID.DigNextArtifactTileCount, this.mNextArtifactTileCount);
			theBuffer.WriteVectorPair(Serialiser.PairID.DigStartArtifactRow, this.mStartArtifactRow);
			theBuffer.WriteVectorPair(Serialiser.PairID.DigHypercubes, this.mHypercubes);
			theBuffer.WriteValuePair(Serialiser.PairID.DigGridDepth, this.mGridDepth);
			theBuffer.WriteValuePair(Serialiser.PairID.DigRandRowIdx, this.mRandRowIdx);
			theBuffer.WriteValuePair(Serialiser.PairID.DigFirstChestPt, this.mFirstChestPt);
			theBuffer.WriteVectorPair(Serialiser.PairID.DigArtifactPoss, this.mArtifactPoss);
			theBuffer.WriteVectorPair(Serialiser.PairID.DigPlacedArtifacts, this.mPlacedArtifacts);
			theBuffer.WriteVectorPair(Serialiser.PairID.DigCollectedArtifacts, this.mCollectedArtifacts);
			theBuffer.WriteValuePair(Serialiser.PairID.DigTimeLimit, ((TimeLimitBoard)this.mQuestBoard).mTimeLimit);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.DigOldPieceData, 2, 8);
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					theBuffer.WriteLong((long)this.mOldPieceData[i, j].mFlags);
					theBuffer.WriteByte((byte)this.mOldPieceData[i, j].mColor);
				}
			}
			theBuffer.WriteValuePair(Serialiser.PairID.DigRotatingCounter, ((DigBoard)this.mQuestBoard).mRotatingCounter.mCurNumber);
			theBuffer.WriteBoolean(this.mDigInProgress);
			theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			return true;
		}

		public override void LoadGameExtra(Serialiser theBuffer)
		{
			foreach (GoldCollectEffect goldCollectEffect in this.mGoldFx)
			{
				if (goldCollectEffect != null)
				{
					goldCollectEffect.Dispose();
				}
			}
			this.mGoldFx.Clear();
			this.mGoldPointsFXManager.Clear();
			this.mDustFXManager.Clear();
			this.mMessageFXManager.Clear();
			this.mIdToTileData.Clear();
			this.mInitPushAnimCv.SetConstant(0.0);
			int num = 0;
			GameChunkHeader header = new GameChunkHeader();
			if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkDigGoal, header, out num))
			{
				return;
			}
			int num2;
			theBuffer.ReadSpecialBlock(out num2);
			for (int i = 0; i < num2; i++)
			{
				int num3 = (int)theBuffer.ReadLong();
				DigGoal.TileData tileData = new DigGoal.TileData();
				tileData.mStrength.value = (int)theBuffer.ReadShort();
				tileData.mStartStrength = (int)theBuffer.ReadShort();
				tileData.mPieceType = (DigGoal.EDigPieceType)theBuffer.ReadShort();
				tileData.mRandCfg = (uint)theBuffer.ReadLong();
				tileData.mIsTopTile = theBuffer.ReadBoolean();
				this.mIdToTileData.Add(num3, tileData);
			}
			theBuffer.ReadArrayPair(3, this.mTreasureEarnings);
			theBuffer.ReadValuePair(out this.mTextFlashTicks);
			theBuffer.ReadValuePair(out this.mNextBottomHypermixerWait);
			theBuffer.ReadValuePair(out this.mArtifactMinTiles);
			theBuffer.ReadValuePair(out this.mArtifactMaxTiles);
			theBuffer.ReadValuePair(out this.mNextArtifactTileCount);
			theBuffer.ReadVectorPair(out this.mStartArtifactRow);
			theBuffer.ReadVectorPair(out this.mHypercubes);
			theBuffer.ReadValuePair(out this.mGridDepth);
			theBuffer.ReadValuePair(out this.mRandRowIdx);
			theBuffer.ReadValuePair(out this.mFirstChestPt);
			theBuffer.ReadVectorPair(out this.mArtifactPoss);
			theBuffer.ReadVectorPair(out this.mPlacedArtifacts);
			theBuffer.ReadVectorPair(out this.mCollectedArtifacts);
			foreach (int num4 in this.mIdToTileData.Keys)
			{
				DigGoal.TileData tileData2 = this.mIdToTileData[num4];
				int mStartStrength = tileData2.mStartStrength;
				uint mRandCfg = tileData2.mRandCfg;
				tileData2.SetAs(tileData2.mPieceType, tileData2.mStrength, this.mQuestBoard.GetPieceById(num4), this);
				tileData2.mStartStrength = mStartStrength;
				tileData2.mRandCfg = mRandCfg;
			}
			theBuffer.ReadValuePair(out ((TimeLimitBoard)this.mQuestBoard).mTimeLimit);
			this.mGoldRushTipPieceId = -1;
			int num5;
			int num6;
			theBuffer.ReadSpecialBlock(out num5, out num6);
			for (int j = 0; j < num5; j++)
			{
				for (int k = 0; k < num6; k++)
				{
					this.mOldPieceData[j, k].mFlags = (int)theBuffer.ReadLong();
					this.mOldPieceData[j, k].mColor = (int)theBuffer.ReadByte();
				}
			}
			int theNum;
			theBuffer.ReadValuePair(out theNum);
			((DigBoard)this.mQuestBoard).mRotatingCounter.ResetCounter(theNum);
			this.mLoadingGame = true;
			this.mDigInProgress = theBuffer.ReadBoolean();
		}

		public override int GetUICenterX()
		{
			if (this.mQuestBoard.mIsPerpetual)
			{
				return this.mQuestBoard.CallBoardGetUICenterX() + GlobalMembers.M(-75);
			}
			return this.mQuestBoard.CallBoardGetUICenterX();
		}

		public int GetPieceValue(ref DigGoal.TileData theData, ref ETreasureType outType)
		{
			int result = 0;
			outType = ETreasureType.eTreasure_Gold;
			if (theData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
			{
				result = this.mArtifacts[theData.mStrength].mValue * this.mArtifactBaseValue;
				outType = ETreasureType.eTreasure_Artifact;
			}
			else if (theData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal)
			{
				int num = theData.mStrength;
				int num2 = Math.Min(num - 1, this.mTreasureRangeMax.Count - 1);
				int num3 = this.mTreasureRangeMax[num2] - this.mTreasureRangeMin[num2];
				num = this.mTreasureRangeMin[num2] + num3;
				outType = ((theData.mStrength <= 3) ? ETreasureType.eTreasure_Gold : ETreasureType.eTreasure_Gem);
				if (outType == ETreasureType.eTreasure_Gold)
				{
					result = num * this.mGoldValue;
				}
				else if (outType == ETreasureType.eTreasure_Gem)
				{
					result = num * this.mGemValue;
				}
			}
			return result;
		}

		public override bool AllowGameOver()
		{
			return this.mGoldFx.Count == 0 && !this.CheckNeedScroll(false) && !this.mDigInProgress;
		}

		public override bool WantBottomFrame()
		{
			return true;
		}

		public override bool WantTopFrame()
		{
			return this.mWantTopFrame;
		}

		public int GetGridDepth()
		{
			return (int)this.GetGridDepthAsDouble();
		}

		public double GetGridDepthAsDouble()
		{
			return this.mGridDepth + this.mCvScrollY * (double)this.GetDigCountPerScroll();
		}

		public override uint GetRandSeedOverride()
		{
			if (!GlobalMembers.gApp.mDiamondMineFirstLaunch)
			{
				return this.mQuestBoard.CallBoardGetRandSeedOverride();
			}
			if (!this.mQuestBoard.mIsPerpetual)
			{
				return (uint)GlobalMembers.M(40000000);
			}
			return (uint)GlobalMembers.M(40000000);
		}

		public override bool WantTopLevelBar()
		{
			return false;
		}

		public override void DrawOverlay(Graphics g)
		{
			if (g.mPushedColorVector.Count > 0)
			{
				g.PopColorMult();
			}
			Color mColor = g.mColor;
			if (GlobalMembers.M(0) != 0)
			{
				for (int i = 0; i < this.mMovingPieces.Count; i++)
				{
					g.SetColor(new Color(-1));
					g.DrawRect((int)GlobalMembers.S(this.mMovingPieces[i].GetScreenX()), (int)GlobalMembers.S(this.mMovingPieces[i].GetScreenY()), GlobalMembers.S(100), GlobalMembers.S(100));
				}
			}
			float alpha = this.mQuestBoard.GetAlpha();
			bool flag = (double)alpha < 1.0;
			if (alpha == 0f)
			{
				return;
			}
			if (flag)
			{
				g.SetColor(Color.FAlpha(alpha));
				g.SetColorizeImages(true);
				g.PushColorMult();
			}
			if (this.mQuestBoard.mIsPerpetual)
			{
				if (GlobalMembers.M(0) != 0)
				{
					g.SetColor(new Color(0, (int)((float)GlobalMembers.M(80) * this.mQuestBoard.GetBoardAlpha())));
					g.FillRect(GlobalMembers.S(this.mQuestBoard.GetBoardX()), GlobalMembers.S(this.mQuestBoard.GetBoardY() + 600 - this.GetBoardScrollOffsetY()), GlobalMembers.S(800), GlobalMembers.S(200));
				}
				g.PushState();
				g.PopState();
			}
			g.SetFont(GlobalMembersResources.FONT_HEADER);
			if (this.mQuestBoard.mIsPerpetual)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = ((this.mGoldFx.Count > 10) ? 10 : this.mGoldFx.Count);
					for (int k = 0; k < num; k++)
					{
						if (this.mGoldFx[k].mLayerOnTop)
						{
							this.mGoldFx[k].Draw(g, j);
						}
					}
				}
			}
			this.mMessageFXManager.Draw(g);
			if (flag)
			{
				g.PopColorMult();
				g.SetColorizeImages(false);
			}
		}

		public override void DrawCountPopups(Graphics g)
		{
			float alpha = this.mQuestBoard.GetAlpha();
			bool flag = (double)alpha < 1.0;
			if (alpha == 0f)
			{
				return;
			}
			if (flag)
			{
				g.SetColor(Color.FAlpha(alpha));
				g.SetColorizeImages(true);
				g.PushColorMult();
			}
			this.mGoldPointsFXManager.Draw(g);
			if (flag)
			{
				g.PopColorMult();
				g.SetColorizeImages(false);
			}
		}

		public override bool DrawScoreWidget(Graphics g)
		{
			return this.mQuestBoard.mIsPerpetual;
		}

		public override void DrawPieces(Graphics g, Piece[] pPieces, int numPieces, bool thePostFX)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < numPieces; i++)
			{
				Piece piece;
				if ((piece = pPieces[i]) != null)
				{
					base.DrawPiecePre(g, piece);
					if (this.mIdToTileData.ContainsKey(piece.mId))
					{
						DigGoal.TileData tileData = this.mIdToTileData[piece.mId];
						if (this.mHypercubes.Contains(piece.mId))
						{
							DigGoal.DP_pHypercubes[num].piece = piece;
							DigGoal.DP_pHypercubes[num].tile = tileData;
							num++;
						}
						else if (this.IsSpecialPiece(piece))
						{
							DigGoal.DP_pSpecial[num2].piece = piece;
							DigGoal.DP_pSpecial[num2].tile = tileData;
							num2++;
						}
						else if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal)
						{
							DigGoal.DP_pGoals[num3].piece = piece;
							DigGoal.DP_pGoals[num3].tile = tileData;
							num3++;
						}
						else if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Block)
						{
							DigGoal.DP_pBlocks[num4].piece = piece;
							DigGoal.DP_pBlocks[num4].tile = tileData;
							num4++;
						}
						else if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
						{
							DigGoal.DP_pArtifacts[num5].piece = piece;
							DigGoal.DP_pArtifacts[num5].tile = tileData;
							num5++;
						}
					}
				}
			}
			float alpha = this.mQuestBoard.GetAlpha();
			bool flag = (double)alpha < 1.0;
			if (alpha == 0f)
			{
				return;
			}
			if (flag)
			{
				g.SetColor(Color.FAlpha(alpha));
				g.SetColorizeImages(true);
				g.PushColorMult();
			}
			int num6 = (int)GlobalMembersResourcesWP.ImgXOfs(1176);
			int num7 = (int)GlobalMembersResourcesWP.ImgYOfs(1176);
			g.PushState();
			g.Translate(GlobalMembers.S(-num6), GlobalMembers.S(-num7));
			g.ClearClipRect();
			for (int j = 0; j < num; j++)
			{
				Piece piece = DigGoal.DP_pHypercubes[j].piece;
			}
			for (int j = 0; j < num4; j++)
			{
				Piece piece = DigGoal.DP_pBlocks[j].piece;
				DigGoal.TileData tile = DigGoal.DP_pBlocks[j].tile;
				switch (tile.mStrength.value)
				{
				case 1:
				{
					int theId = (int)(1277U + tile.mRandCfg % 3U);
					g.DrawImage(GlobalMembersResourcesWP.GetImageById(theId), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId) + piece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId) + piece.GetScreenY()));
					break;
				}
				case 2:
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR1, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1280) + piece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1280) + piece.GetScreenY()));
					break;
				case 3:
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR2, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1281) + piece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1281) + piece.GetScreenY()));
					break;
				case 4:
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR4, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1283) + piece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1283) + piece.GetScreenY()));
					break;
				default:
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR3, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1282) + piece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1282) + piece.GetScreenY()));
					break;
				}
			}
			for (int k = 0; k < 2; k++)
			{
				if (k == 1)
				{
					g.SetDrawMode(Graphics.DrawMode.Additive);
					g.SetColorizeImages(true);
				}
				int j = 0;
				while (j < num5)
				{
					Piece piece = DigGoal.DP_pArtifacts[j].piece;
					DigGoal.TileData tile = DigGoal.DP_pArtifacts[j].tile;
					if (k != 1)
					{
						goto IL_47A;
					}
					float num8 = (float)(this.GetExtraLuminosity(this.GetGridDepth() + piece.mRow) * GlobalMembers.M(0.3));
					if ((double)num8 > GlobalMembers.M(0.1))
					{
						g.SetColor(Color.FAlpha(num8));
						this.SetAdditiveClipRect(g, (int)(-g.mTransX + (float)GlobalMembers.MS(0)), (int)(-(int)g.mTransY));
						goto IL_47A;
					}
					IL_5A9:
					j++;
					continue;
					IL_47A:
					DigGoal.ArtifactData artifactData = this.mArtifacts[tile.mArtifactId];
					g.DrawImage(GlobalMembersResourcesWP.GetImageById(artifactData.mUnderlayImgId), (int)GlobalMembers.S(piece.GetScreenX() + (float)num6), (int)GlobalMembers.S(piece.GetScreenY() + (float)num7));
					g.SetScale(ConstantsWP.DIG_BOARD_ARTIFACT_SCALE, ConstantsWP.DIG_BOARD_ARTIFACT_SCALE, GlobalMembers.S(piece.GetScreenX() + 50f + (float)num6), GlobalMembers.S(piece.GetScreenY() + 50f + (float)num7));
					g.DrawImage(GlobalMembersResourcesWP.GetImageById(artifactData.mImageId), (int)GlobalMembers.S(piece.GetScreenX() + (float)num6), (int)GlobalMembers.S(piece.GetScreenY() + (float)num7));
					g.SetScale(1f, 1f, GlobalMembers.S(piece.GetScreenX() + 50f + (float)num6), GlobalMembers.S(piece.GetScreenY() + 50f + (float)num7));
					g.DrawImage(GlobalMembersResourcesWP.GetImageById(artifactData.mOverlayImgId), (int)GlobalMembers.S(piece.GetScreenX() + (float)num6), (int)GlobalMembers.S(piece.GetScreenY() + (float)num7));
					goto IL_5A9;
				}
				for (j = 0; j < num3; j++)
				{
					Piece piece = DigGoal.DP_pGoals[j].piece;
					DigGoal.TileData tile = DigGoal.DP_pGoals[j].tile;
					int num9 = tile.mStrength;
					if (this.mQuestBoard.mIsPerpetual)
					{
						g.Translate((int)GlobalMembers.S(piece.GetScreenX()), (int)GlobalMembers.S(piece.GetScreenY()));
						if (k == 1)
						{
							float num10 = (float)(this.GetExtraLuminosity(this.GetGridDepth() + piece.mRow) * ((tile.mDiamondFx != null) ? GlobalMembers.M(0.6) : GlobalMembers.M(0.3)));
							if ((double)num10 <= GlobalMembers.M(0.1))
							{
								goto IL_950;
							}
							g.SetColor(Color.FAlpha(num10));
							this.SetAdditiveClipRect(g, (int)(-g.mTransX + (float)GlobalMembers.MS(0)), (int)(-(int)g.mTransY));
						}
						if (tile.mDiamondFx != null)
						{
							int theId2 = (int)(1277U + tile.mRandCfg % 3U);
							g.DrawImage(GlobalMembersResourcesWP.GetImageById(theId2), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId2) + 0f), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId2) + 0f));
							int theX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(tile.mDiamondFx.mDirtImg));
							int theY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(tile.mDiamondFx.mDirtImg));
							if (k == 0)
							{
								g.SetColor(new Color(this.GetDarkenedRGBForRow(piece.mRow, false)));
							}
							g.SetColorizeImages(true);
							tile.mDiamondFx.DrawDirt(g, theX, theY);
							if (k == 0)
							{
								g.SetColor(new Color(-1));
							}
							theX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(tile.mDiamondFx.mBaseImg));
							theY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(tile.mDiamondFx.mBaseImg));
							tile.mDiamondFx.Draw(g, theX, theY, k == 0);
						}
						else
						{
							int theId3 = 0;
							switch (num9)
							{
							case 1:
								theId3 = 1224;
								break;
							case 2:
								theId3 = 1225;
								break;
							case 3:
								theId3 = 1226;
								break;
							}
							Graphics3D graphics3D = g.Get3D();
							if (graphics3D != null && tile.mGoldInnerFx != null)
							{
								graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_WRITE_MASKANDCOLOR);
							}
							Image imageById = GlobalMembersResourcesWP.GetImageById(theId3);
							int num11 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId3));
							int num12 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId3));
							g.DrawImage(imageById, num11, num12);
							if (graphics3D != null && tile.mGoldInnerFx != null)
							{
								g.Translate(num11 + imageById.GetWidth() / 2, num12 + imageById.GetHeight() / 2);
								graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_TEST_INSIDE);
								tile.mGoldInnerFx.Draw(g);
								graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_NONE);
								graphics3D.ClearMask();
								g.Translate(-(num11 + imageById.GetWidth() / 2), -(num12 + imageById.GetHeight() / 2));
							}
						}
						g.Translate((int)(-(int)GlobalMembers.S(piece.GetScreenX())), (int)(-(int)GlobalMembers.S(piece.GetScreenY())));
					}
					else
					{
						int num13 = Math.Min(5, num9);
						int[,] array = new int[,]
						{
							{ 1157, 1158, 1159 },
							{ 1160, 1161, 1162 },
							{ 1163, 1164, 1165 },
							{ 1166, 1167, 1168 },
							{ 1169, 1170, 1171 }
						};
						int theId4;
						checked
						{
							theId4 = array[(int)((IntPtr)(unchecked((long)(num13 - 1)))), (int)((IntPtr)(unchecked((ulong)(tile.mRandCfg * 10232U % 3U))))];
						}
						g.DrawImage(GlobalMembersResourcesWP.GetImageById(theId4), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId4) + piece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId4) + piece.GetScreenY()));
					}
					IL_950:;
				}
			}
			g.PopState();
			if (flag)
			{
				g.PopColorMult();
				g.SetColorizeImages(false);
			}
		}

		public override void DrawPiecePre(Graphics g, Piece thePiece, float theScale)
		{
			base.DrawPiecePre(g, thePiece, theScale);
			float alpha = this.mQuestBoard.GetAlpha();
			bool flag = (double)alpha < 1.0;
			if (alpha == 0f)
			{
				return;
			}
			if (flag)
			{
				g.SetColor(Color.FAlpha(alpha));
				g.SetColorizeImages(true);
				g.PushColorMult();
			}
			if (thePiece.IsFlagSet(65536U) && this.mIdToTileData.ContainsKey(thePiece.mId))
			{
				DigGoal.TileData tileData = this.mIdToTileData[thePiece.mId];
				int num = tileData.mStrength;
				int num2 = (int)GlobalMembersResourcesWP.ImgXOfs(1176);
				int num3 = (int)GlobalMembersResourcesWP.ImgYOfs(1176);
				g.PushState();
				g.Translate(GlobalMembers.S(-num2), GlobalMembers.S(-num3));
				int i = 0;
				while (i < 3)
				{
					if (i <= 0)
					{
						g.ClearClipRect();
						goto IL_239;
					}
					if (this.mQuestBoard.mIsPerpetual && GlobalMembers.M(0) == 0)
					{
						if (i == 1)
						{
							if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal || tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
							{
								float num4 = (float)(this.GetExtraLuminosity(this.GetGridDepth() + thePiece.mRow) * GlobalMembers.M(0.3));
								if (tileData.mDiamondFx != null)
								{
									num4 *= GlobalMembers.M(0.2f);
								}
								if ((double)num4 > GlobalMembers.M(0.1))
								{
									g.SetDrawMode(Graphics.DrawMode.Additive);
									g.SetColorizeImages(true);
									g.SetColor(Color.FAlpha(num4));
									this.SetAdditiveClipRect(g, (int)(-g.mTransX + (float)GlobalMembers.MS(0)), (int)(-(int)g.mTransY));
									goto IL_239;
								}
							}
						}
						else
						{
							if (i != 2)
							{
								goto IL_239;
							}
							g.SetDrawMode(Graphics.DrawMode.Normal);
							g.SetColorizeImages(true);
							this.SetShadowClipRect(g, (int)(-g.mTransX + (float)GlobalMembers.MS(0)), (int)(-(int)g.mTransY));
							g.SetDrawMode(Graphics.DrawMode.Normal);
							g.SetColorizeImages(true);
							if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal || tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
							{
								g.SetColor(new Color(GlobalMembers.M(0), (tileData.mDiamondFx != null) ? GlobalMembers.M(120) : GlobalMembers.M(70)));
								goto IL_239;
							}
							g.SetColor(new Color(GlobalMembers.M(0), GlobalMembers.M(120)));
							goto IL_239;
						}
					}
					IL_86A:
					i++;
					continue;
					IL_239:
					if (this.mHypercubes.Contains(thePiece.mId))
					{
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_HYPERCUBE, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1230) + (thePiece.GetScreenX() + (float)GlobalMembers.M(0))), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1230) + (thePiece.GetScreenY() + (float)GlobalMembers.M(0))));
						goto IL_86A;
					}
					if (this.IsSpecialPiece(thePiece))
					{
						goto IL_86A;
					}
					if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal)
					{
						if (this.mQuestBoard.mIsPerpetual)
						{
							g.Translate((int)GlobalMembers.S(thePiece.GetScreenX()), (int)GlobalMembers.S(thePiece.GetScreenY()));
							if (tileData.mDiamondFx != null)
							{
								int theId = (int)(1277U + tileData.mRandCfg % 3U);
								g.DrawImage(GlobalMembersResourcesWP.GetImageById(theId), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId) + 0f), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId) + 0f));
								int theX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(tileData.mDiamondFx.mDirtImg));
								int theY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(tileData.mDiamondFx.mDirtImg));
								if (i == 0)
								{
									g.SetColor(new Color(this.GetDarkenedRGBForRow(thePiece.mRow, false)));
								}
								g.SetColorizeImages(true);
								tileData.mDiamondFx.DrawDirt(g, theX, theY);
								if (i == 0)
								{
									g.SetColor(new Color(-1));
								}
								theX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(tileData.mDiamondFx.mBaseImg));
								theY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(tileData.mDiamondFx.mBaseImg));
								tileData.mDiamondFx.Draw(g, theX, theY, false);
							}
							else
							{
								int theId2 = 0;
								switch (num)
								{
								case 1:
									theId2 = 1224;
									break;
								case 2:
									theId2 = 1225;
									break;
								case 3:
									theId2 = 1226;
									break;
								}
								Graphics3D graphics3D = g.Get3D();
								if (graphics3D != null && tileData.mGoldInnerFx != null)
								{
									graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_WRITE_MASKANDCOLOR);
								}
								Image imageById = GlobalMembersResourcesWP.GetImageById(theId2);
								int num5 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId2));
								int num6 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId2));
								g.DrawImage(imageById, num5, num6);
								if (graphics3D != null && tileData.mGoldInnerFx != null)
								{
									g.Translate(num5 + imageById.GetWidth() / 2, num6 + imageById.GetHeight() / 2);
									graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_TEST_INSIDE);
									tileData.mGoldInnerFx.Draw(g);
									graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_NONE);
									graphics3D.ClearMask();
									g.Translate(-(num5 + imageById.GetWidth() / 2), -(num6 + imageById.GetHeight() / 2));
								}
							}
							g.Translate((int)(-(int)GlobalMembers.S(thePiece.GetScreenX())), (int)(-(int)GlobalMembers.S(thePiece.GetScreenY())));
							goto IL_86A;
						}
						int num7 = Math.Min(5, num);
						int idByStringId = (int)GlobalMembersResourcesWP.GetIdByStringId(string.Format("IMAGE_QUEST_DIG_BOARD_NUGGET{0}_{1}", num7, 1U + tileData.mRandCfg * 10232U % 3U));
						g.DrawImage(GlobalMembersResourcesWP.GetImageById(idByStringId), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(idByStringId) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(idByStringId) + thePiece.GetScreenY()));
						goto IL_86A;
					}
					else if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Block)
					{
						switch (num)
						{
						case 1:
						{
							int theId3 = (int)(1277U + tileData.mRandCfg % 3U);
							g.DrawImage(GlobalMembersResourcesWP.GetImageById(theId3), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId3) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId3) + thePiece.GetScreenY()));
							goto IL_86A;
						}
						case 2:
							g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR1, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1280) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1280) + thePiece.GetScreenY()));
							goto IL_86A;
						case 3:
							g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR2, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1281) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1281) + thePiece.GetScreenY()));
							goto IL_86A;
						case 4:
							g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR4, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1283) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1283) + thePiece.GetScreenY()));
							goto IL_86A;
						case 5:
							g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR3, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1282) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1282) + thePiece.GetScreenY()));
							goto IL_86A;
						default:
							g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR3, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1282) + thePiece.GetScreenX()), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1282) + thePiece.GetScreenY()));
							goto IL_86A;
						}
					}
					else
					{
						if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
						{
							int num8 = (int)GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_CENTER_FULL_ID);
							int num9 = (int)GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_CENTER_FULL_ID);
							DigGoal.ArtifactData artifactData = this.mArtifacts[tileData.mArtifactId];
							g.DrawImage(GlobalMembersResourcesWP.GetImageById(artifactData.mUnderlayImgId), (int)GlobalMembers.S(thePiece.GetScreenX() + (float)num8), (int)GlobalMembers.S(thePiece.GetScreenY() + (float)num9));
							g.SetScale(ConstantsWP.DIG_BOARD_ARTIFACT_SCALE, ConstantsWP.DIG_BOARD_ARTIFACT_SCALE, (float)((int)GlobalMembers.S(thePiece.GetScreenX() + 50f + (float)num8)), (float)((int)GlobalMembers.S(thePiece.GetScreenY() + 50f + (float)num9)));
							g.DrawImage(GlobalMembersResourcesWP.GetImageById(artifactData.mImageId), (int)GlobalMembers.S(thePiece.GetScreenX() + (float)num8), (int)GlobalMembers.S(thePiece.GetScreenY() + (float)num9));
							g.SetScale(1f, 1f, GlobalMembers.S(thePiece.GetScreenX() + 50f + (float)num8), GlobalMembers.S(thePiece.GetScreenY() + 50f + (float)num9));
							g.DrawImage(GlobalMembersResourcesWP.GetImageById(artifactData.mOverlayImgId), (int)GlobalMembers.S(thePiece.GetScreenX() + (float)num8), (int)GlobalMembers.S(thePiece.GetScreenY() + (float)num9));
							goto IL_86A;
						}
						goto IL_86A;
					}
				}
				g.PopState();
			}
			if (flag)
			{
				g.PopColorMult();
				g.SetColorizeImages(false);
			}
		}

		public override void DrawPiecesPost(Graphics g, bool thePostFX)
		{
			base.DrawPiecesPost(g, thePostFX);
			if (this.mCvScrollY.IsDoingCurve())
			{
				for (int i = 0; i < this.mMovingPieces.Count; i++)
				{
					if (this.mMovingPieces[i] != null)
					{
						this.mQuestBoard.DrawPiece(g, this.mMovingPieces[i]);
					}
				}
			}
			g.ClearClipRect();
			if (!this.mQuestBoard.mIsPerpetual)
			{
				this.mWantTopFrame = true;
				this.DrawBottomFrame(g);
				this.mWantTopFrame = false;
			}
			if (!this.mQuestBoard.mIsPerpetual)
			{
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < this.mGoldFx.Count; k++)
					{
						this.mGoldFx[k].Draw(g, j);
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mQuestBoard.mIsPerpetual)
			{
				float alpha = this.mQuestBoard.GetAlpha();
				bool flag = (double)alpha < 1.0;
				if (alpha == 0f)
				{
					return;
				}
				if (flag)
				{
					g.SetColor(Color.FAlpha(alpha));
					g.SetColorizeImages(true);
					g.PushColorMult();
				}
				GlobalMembers.M(1);
				g.SetColor(new Color(-1));
				if (flag)
				{
					g.PopColorMult();
					g.SetColorizeImages(false);
				}
			}
		}

		public override bool DrawScore(Graphics g)
		{
			return this.mQuestBoard.mIsPerpetual;
		}

		public override void DrawGameElements(Graphics g)
		{
			base.DrawGameElements(g);
			float alpha = this.mQuestBoard.GetAlpha();
			bool flag = (double)alpha < 1.0;
			if (alpha == 0f)
			{
				return;
			}
			if (flag)
			{
				g.SetColor(Color.FAlpha(alpha));
				g.SetColorizeImages(true);
				g.PushColorMult();
			}
			List<Piece> list = new List<Piece>();
			new Color(GlobalMembers.M(4210752), GlobalMembers.M(255));
			new Color(GlobalMembers.M(10526880), GlobalMembers.M(255));
			Color color = default(Color);
			int num = (int)GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_CENTER_FULL_ID);
			int num2 = (int)GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_CENTER_FULL_ID);
			g.PushState();
			g.ClipRect(GlobalMembers.S(this.mQuestBoard.CallBoardGetBoardX() + this.mBoardOffsetXExtra), GlobalMembers.S(this.mQuestBoard.CallBoardGetBoardY() + this.mBoardOffsetYExtra), GlobalMembers.S(800), GlobalMembers.S(800));
			int num3 = 0;
			g.Translate(GlobalMembers.S(-num), GlobalMembers.S(num3 - num2));
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 1; j++)
				{
					bool flag2 = false;
					if (i == 1 || i == 4)
					{
						if (list.Count != 0)
						{
							for (int k = 0; k < list.Count; k++)
							{
								Piece piece = list[k];
								DigGoal.TileData tileData = this.mIdToTileData[piece.mId];
								if (i == 1)
								{
									g.DrawImage(GlobalMembersResourcesWP.GetImageById(1227), (int)GlobalMembers.S(piece.GetScreenX() + GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_GRASS_ID)), (int)GlobalMembers.S(piece.GetScreenY() + GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_GRASS_ID)));
								}
								else
								{
									for (int l = 0; l < DigGoal.DrawGameElements_DrawGrassLR_neighbors.GetLength(0); l++)
									{
										int num4 = piece.mCol + DigGoal.DrawGameElements_DrawGrassLR_neighbors[l, 0];
										int num5 = piece.mRow + DigGoal.DrawGameElements_DrawGrassLR_neighbors[l, 1];
										bool flag3 = false;
										if (num4 >= 0 && num4 < 8 && num5 >= 0 && num5 < 8)
										{
											Piece piece2 = this.mQuestBoard.mBoard[num5, num4];
											if (piece2 != null)
											{
												flag3 = !piece2.IsFlagSet(65536U);
											}
										}
										else
										{
											flag3 = true;
										}
										if (flag3)
										{
											g.DrawImage(GlobalMembersResourcesWP.GetImageById(DigGoal.DrawGameElements_DrawGrassLR_images[l]), (int)GlobalMembers.S(piece.GetScreenX() + GlobalMembersResourcesWP.ImgXOfs(DigGoal.DrawGameElements_DrawGrassLR_images[l])), (int)GlobalMembers.S(piece.GetScreenY() + GlobalMembersResourcesWP.ImgYOfs(DigGoal.DrawGameElements_DrawGrassLR_images[l])));
										}
									}
								}
							}
						}
					}
					else
					{
						Piece[,] mBoard = this.mQuestBoard.mBoard;
						int upperBound = mBoard.GetUpperBound(0);
						int upperBound2 = mBoard.GetUpperBound(1);
						for (int m = mBoard.GetLowerBound(0); m <= upperBound; m++)
						{
							for (int n = mBoard.GetLowerBound(1); n <= upperBound2; n++)
							{
								Piece piece3 = mBoard[m, n];
								if (piece3 != null)
								{
									if (GlobalMembers.M(0) != 0 && piece3.mCol == GlobalMembers.M(6))
									{
										int mRow = piece3.mRow;
										GlobalMembers.M(6);
									}
									if (piece3.IsFlagSet(65536U))
									{
										DigGoal.TileData tileData2 = this.mIdToTileData[piece3.mId];
										g.SetColor(new Color(this.GetDarkenedRGBForRow(piece3.mRow, flag2)));
										if (flag2)
										{
											color = g.mColor;
										}
										g.SetColorizeImages(g.mColor != Color.White);
										if (i == 0)
										{
											if (tileData2.mIsTopTile)
											{
												list.Add(piece3);
											}
											g.DrawImage(GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_FULL, (int)GlobalMembers.S(piece3.GetScreenX() + GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_CENTER_FULL_ID)), (int)GlobalMembers.S(piece3.GetScreenY() + GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_QUEST_DIG_BOARD_CENTER_FULL_ID)));
											g.SetColorizeImages(flag);
											g.SetColor(new Color(-1));
										}
										else if (i == 3 || i == 2)
										{
											Color mColor = g.mColor;
											for (int num6 = 0; num6 < DigGoal.DrawGameElements_neighbors.GetLength(0); num6++)
											{
												if (!tileData2.mIsTopTile || num6 != 3)
												{
													int num7 = piece3.mCol + DigGoal.DrawGameElements_neighbors[num6, 0];
													int num8 = piece3.mRow + DigGoal.DrawGameElements_neighbors[num6, 1];
													if (num7 >= 0 && num7 < 8 && num8 >= 0 && num8 < 8)
													{
														Piece piece4 = this.mQuestBoard.mBoard[num8, num7];
														if (piece4 == null || !piece4.IsFlagSet(65536U))
														{
															g.DrawImage(GlobalMembersResourcesWP.GetImageById(DigGoal.DrawGameElements_images[num6]), (int)GlobalMembers.S(piece3.GetScreenX() + (float)(100 * DigGoal.DrawGameElements_neighbors[num6, 0]) + GlobalMembersResourcesWP.ImgXOfs(DigGoal.DrawGameElements_images[num6])), (int)GlobalMembers.S(piece3.GetScreenY() + (float)(100 * DigGoal.DrawGameElements_neighbors[num6, 1]) + GlobalMembersResourcesWP.ImgYOfs(DigGoal.DrawGameElements_images[num6])));
															if (!flag2)
															{
																g.SetColor(new Color(-1));
															}
															else
															{
																g.SetColor(new Color(GlobalMembers.M(16777215)));
															}
															int num9;
															if (i == 2)
															{
																num9 = DigGoal.DrawGameElements_imagesHighlightShadows[num6];
															}
															else
															{
																num9 = DigGoal.DrawGameElements_imagesHighlights[num6];
															}
															if (num9 != -1)
															{
																g.DrawImage(GlobalMembersResourcesWP.GetImageById(num9), (int)GlobalMembers.S(piece3.GetScreenX() + (float)(100 * DigGoal.DrawGameElements_neighbors[num6, 0]) + GlobalMembersResourcesWP.ImgXOfs(num9)), (int)GlobalMembers.S(piece3.GetScreenY() + (float)(100 * DigGoal.DrawGameElements_neighbors[num6, 1]) + GlobalMembersResourcesWP.ImgYOfs(num9)));
															}
															if (!flag2)
															{
																g.SetColor(mColor);
															}
															else
															{
																g.SetColor(color);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						g.SetColorizeImages(flag);
						g.SetColor(new Color(-1));
					}
				}
			}
			g.PopState();
			g.ClearClipRect();
			this.mDustFXManager.Draw(g);
			g.SetDrawMode(Graphics.DrawMode.Normal);
			if (flag)
			{
				g.PopColorMult();
				g.SetColorizeImages(false);
			}
		}

		public override void DrawGameElementsPost(Graphics g)
		{
			this.mQuestBoard.CallBoardDrawUI(g);
		}

		public void DrawDigBarAnim(Graphics g, bool theDrawMega)
		{
			if (!DigGoal.DrawDigBarAnim_made)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_X, DigGoal.DrawDigBarAnim_cvClearAnimX);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_SCALE, DigGoal.DrawDigBarAnim_cvClearAnimScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_ALPHA, DigGoal.DrawDigBarAnim_cvClearAnimAlpha);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_COLOR_CYCLE, DigGoal.DrawDigBarAnim_cvClearAnimColorCycle);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_X, DigGoal.DrawDigBarAnim_cvAllClearAnimX);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_SCALE, DigGoal.DrawDigBarAnim_cvAllClearAnimScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_ALPHA, DigGoal.DrawDigBarAnim_cvAllClearAnimAlpha);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_COLOR_CYCLE, DigGoal.DrawDigBarAnim_cvAllClearAnimColorCycle);
				DigGoal.DrawDigBarAnim_made = true;
			}
			if (!theDrawMega)
			{
				if (GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.IsActive())
				{
					g.PushState();
					g.Translate(GlobalMembers.S(this.GetBoardX() + 400 + GlobalMembers.M(0)), GlobalMembers.S(this.mQuestBoard.CallBoardGetBoardY() + 600 + GlobalMembers.M(0)));
					GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.mDrawTransform.LoadIdentity();
					GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.mDrawTransform.Scale(GlobalMembers.MS(0.9f), GlobalMembers.S(1f));
					GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.Draw(g);
					g.PopState();
				}
				if (this.mClearedAnimAtTick > 0 && (double)(this.mUpdateCnt - this.mClearedAnimAtTick) < DigGoal.DrawDigBarAnim_cvClearAnimAlpha.mInMax * 100.0)
				{
					this.DrawScrollingDigLineText(g, GlobalMembersResources.FONT_HUGE, ConstantsWP.DIG_BOARD_CLEARED_TEXT_EXTRA_Y, GlobalMembers._ID("CLEARED!", 542), this.mUpdateCnt - this.mClearedAnimAtTick, DigGoal.DrawDigBarAnim_cvClearAnimX, DigGoal.DrawDigBarAnim_cvClearAnimScale, DigGoal.DrawDigBarAnim_cvClearAnimAlpha, DigGoal.DrawDigBarAnim_cvClearAnimColorCycle, false, GlobalMembers.M(0));
					return;
				}
			}
			else
			{
				if (GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.IsActive())
				{
					g.PushState();
					g.Translate(GlobalMembers.S(this.GetBoardX() + 400 + GlobalMembers.M(0)), GlobalMembers.S(this.mQuestBoard.CallBoardGetBoardY() + 800 + GlobalMembers.M(0)));
					GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.mDrawTransform.LoadIdentity();
					GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.mDrawTransform.Scale(GlobalMembers.MS(1f), GlobalMembers.S(1f));
					GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.Draw(g);
					g.PopState();
				}
				if (this.mAllClearedAnimAtTick > 0 && (double)(this.mUpdateCnt - this.mAllClearedAnimAtTick) < DigGoal.DrawDigBarAnim_cvAllClearAnimAlpha.mInMax * 100.0)
				{
					this.DrawScrollingDigLineText(g, GlobalMembersResources.FONT_HUGE, ConstantsWP.DIG_BOARD_ALL_CLEAR_TEXT_EXTRA_Y, GlobalMembers._ID("ALL CLEAR", 544), this.mUpdateCnt - this.mAllClearedAnimAtTick, DigGoal.DrawDigBarAnim_cvAllClearAnimX, DigGoal.DrawDigBarAnim_cvAllClearAnimScale, DigGoal.DrawDigBarAnim_cvAllClearAnimAlpha, DigGoal.DrawDigBarAnim_cvAllClearAnimColorCycle, true, g.Is3D() ? GlobalMembers.M(0) : GlobalMembers.M(0));
				}
			}
		}

		public void GoldAnimDoPoints(ETreasureType theType, int theVal)
		{
			if (this.mQuestBoard.mIsPerpetual)
			{
				this.mQuestBoard.mLevelPointsTotal += theVal;
				this.mTextFlashTicks = GlobalMembers.M(120);
				this.mTreasureEarnings[(int)theType] += theVal;
				return;
			}
			this.mQuestBoard.mLevelPointsTotal++;
			this.mQuestBoard.mPoints++;
		}

		public void DrawBottomFrame(Graphics g)
		{
			this.mQuestBoard.CallBoardDrawBottomFrame(g);
		}

		public bool IsSpecialPiece(Piece thePiece)
		{
			return thePiece.IsFlagSet(4103U);
		}

		public override bool DeletePiece(Piece thePiece)
		{
			if (this.mQuestBoard.mInLoadSave)
			{
				return true;
			}
			int num = this.mHypercubes.IndexOf(thePiece.mId);
			if (num >= 0)
			{
				this.mHypercubes.RemoveAt(num);
				this.mQuestBoard.Hypercubeify(thePiece);
				int[] array = new int[7];
				for (int i = 0; i < 7; i++)
				{
					array[i] = 0;
				}
				Piece[,] mBoard = this.mQuestBoard.mBoard;
				int upperBound = mBoard.GetUpperBound(0);
				int upperBound2 = mBoard.GetUpperBound(1);
				for (int j = mBoard.GetLowerBound(0); j <= upperBound; j++)
				{
					for (int k = mBoard.GetLowerBound(1); k <= upperBound2; k++)
					{
						Piece piece = mBoard[j, k];
						if (piece != null && piece.mColor >= 0 && piece.mColor < 7)
						{
							array[piece.mColor]++;
						}
					}
				}
				int num2 = 0;
				for (int l = 0; l < 7; l++)
				{
					if (array[l] > array[num2])
					{
						num2 = l;
					}
				}
				thePiece.ClearFlag(65536U);
				thePiece.mIsElectrocuting = false;
				thePiece.mElectrocutePercent = 0f;
				this.mQuestBoard.DoHypercube(thePiece, num2);
				return false;
			}
			int num3 = this.mMovingPieces.IndexOf(thePiece);
			if (num3 >= 0)
			{
				this.mMovingPieces[num3] = null;
				return true;
			}
			if (this.mIdToTileData.ContainsKey(thePiece.mId))
			{
				DigGoal.TileData tileData = this.mIdToTileData[thePiece.mId];
				if (thePiece.IsFlagSet(65536U) && (tileData.mStrength > 1 || tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact))
				{
					this.mQuestBoard.TallyPiece(thePiece, true);
					if (!this.mIdToTileData.ContainsKey(thePiece.mId) || !tileData.mIsDeleting)
					{
						return false;
					}
				}
				if (tileData.mBlingPIFx != null)
				{
					tileData.mBlingPIFx.Stop();
					if (tileData.mBlingPIFx.mRefCount > 0)
					{
						tileData.mBlingPIFx.mRefCount--;
					}
					tileData.mBlingPIFx = null;
				}
				this.mQuestBoard.CallBoardDeletePiece(thePiece);
				if (tileData.mDiamondFx != null)
				{
					tileData.mDiamondFx.Dispose();
				}
				this.mIdToTileData.Remove(thePiece.mId);
				return false;
			}
			return true;
		}

		public override void PieceTallied(Piece thePiece)
		{
			this.mGoldRushTipPieceId = -1;
			new List<Piece>();
			if (!thePiece.IsFlagSet(65536U))
			{
				this.mVisited[thePiece.mRow][thePiece.mCol] = true;
				if (!thePiece.mIsExploding || thePiece.mColor == -1 || ((long)thePiece.mExplodeSourceFlags & 5L) == 0L)
				{
					DigGoal.CheckPiece checkPiece = new DigGoal.CheckPiece(thePiece.mCol, thePiece.mRow, thePiece.mColor == -1 || ((long)thePiece.mExplodeSourceFlags & 2L) != 0L, thePiece.mMoveCreditId);
					this.mCheckPieces.Add(checkPiece);
				}
			}
			base.PieceTallied(thePiece);
		}

		public override void TallyPiece(Piece thePiece, bool thePieceDestroyed)
		{
			bool flag = false;
			if (thePiece.IsFlagSet(65536U) && !this.IsSpecialPiece(thePiece) && !this.mQuestBoard.mInLoadSave)
			{
				bool flag2 = false;
				bool theIsSpecialGem = false;
				if (thePiece.mIsExploding && ((long)thePiece.mExplodeSourceFlags & 7L) != 0L)
				{
					int mCol = thePiece.mCol;
					int mRow = thePiece.mRow;
					bool flag3 = this.mVisited[mRow][mCol];
					theIsSpecialGem = true;
					flag = this.TriggerDigPiece(null, thePiece, theIsSpecialGem, !thePieceDestroyed);
					this.mVisited[mRow][mCol] = flag3;
					flag2 = true;
				}
				if (!flag)
				{
					this.TriggerDigPiece(null, thePiece, theIsSpecialGem, !thePieceDestroyed, !flag2);
				}
			}
			base.TallyPiece(thePiece, thePieceDestroyed);
		}

		public void TriggerDigCoin(Piece thePiece, int theId)
		{
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_TREASUREFIND);
			DigGoal.TileData theData = this.mIdToTileData[thePiece.mId];
			if (this.mIdToTileData.ContainsKey(thePiece.mId))
			{
				int num = 1147 + BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(9);
				GoldCollectEffect goldCollectEffect = new GoldCollectEffect(this, theData);
				goldCollectEffect.mTreasureType = ETreasureType.eTreasure_Gold;
				goldCollectEffect.mVal = 1;
				goldCollectEffect.mImageId = num;
				goldCollectEffect.mSrcImageId = num;
				goldCollectEffect.mStartPoint = new Point((int)thePiece.CX(), (int)thePiece.CY());
				goldCollectEffect.mTargetPoint = new Point(ConstantsWP.DIG_BOARD_SCORE_CENTER_X, ConstantsWP.DIG_BOARD_SCORE_BTM_Y);
				goldCollectEffect.mTimeMod = GlobalMembersUtils.GetRandFloatU() * GlobalMembers.M(0.5f);
				goldCollectEffect.mIsNugget = true;
				goldCollectEffect.mStopGlowAtPct = GlobalMembers.M(0.9);
				goldCollectEffect.mLayerOnTop = true;
				goldCollectEffect.mGlowRGB = GlobalMembers.M(16118660);
				goldCollectEffect.Init();
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SCALE_CV_DIG_COIN, goldCollectEffect.mScaleCv);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SPLINE_INTERP_DIG_COIN, goldCollectEffect.mSplineInterp);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_PARTICLE_EMIT_OVER_TIME_DIG_COIN, goldCollectEffect.mParticleEmitOverTime);
				this.mGoldFx.Add(goldCollectEffect);
			}
		}

		public override bool IsGameSuspended()
		{
			return this.mCvScrollY.IsDoingCurve() || base.IsGameSuspended();
		}

		public override void Update()
		{
			if (this.mDigInProgress && !this.mClearedAnimPlayed && !this.mAllClearAnimPlayed && !this.mCvScrollY.IsDoingCurve())
			{
				this.mDigInProgress = false;
			}
			if (this.mQuestBoard.mIsPerpetual)
			{
				this.mQuestBoard.mBoardColors[0] = new Color(77, 61, 40);
				this.mQuestBoard.mBoardColors[1] = new Color(88, 72, 51);
			}
			if (this.mClearedAnimPlayed)
			{
				GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.Update();
			}
			if (this.mAllClearAnimPlayed)
			{
				GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.Update();
			}
			int i = 0;
			while (i < this.mGoldFx.Count)
			{
				if (this.mGoldFx[i].mDeleteMe)
				{
					if (this.mGoldFx[i] != null)
					{
						this.mGoldFx[i].Dispose();
					}
					this.mGoldFx.RemoveAt(i);
				}
				else
				{
					this.mGoldFx[i++].Update();
				}
			}
			if (GlobalMembers.gApp.GetDialog(19) == null)
			{
				this.mInitPushAnimCv.IncInVal();
			}
			foreach (int num in this.mIdToTileData.Keys)
			{
				DigGoal.TileData tileData = this.mIdToTileData[num];
				if (tileData.mDiamondFx != null)
				{
					tileData.mDiamondFx.Update();
				}
				if (tileData.mGoldInnerFx != null)
				{
					tileData.mGoldInnerFx.Update();
				}
				if (tileData.mBlingPIFx != null)
				{
					Piece pieceById = this.mQuestBoard.GetPieceById(num);
					if (pieceById != null)
					{
						tileData.mBlingPIFx.SetActive(pieceById.mRow < 6);
					}
				}
			}
			this.mGoldPointsFXManager.Update();
			this.mUpdateCnt.value++;
			if (this.mCvScrollY.IsDoingCurve())
			{
				this.UpdateShift();
			}
			else
			{
				((DigBoard)this.mQuestBoard).SetCogsAnim(false);
			}
			foreach (List<bool> list in this.mVisited)
			{
				for (int j = 0; j < list.Count; j++)
				{
					list[j] = false;
				}
			}
			base.Update();
			if (this.mTextFlashTicks > 0)
			{
				this.mTextFlashTicks--;
			}
			foreach (DigGoal.CheckPiece checkPiece in this.mCheckPieces)
			{
				int mCol = checkPiece.mCol;
				int mRow = checkPiece.mRow;
				int mMoveCreditId = checkPiece.mMoveCreditId;
				bool mIsHyperCube = checkPiece.mIsHyperCube;
				for (int k = 0; k < 4; k++)
				{
					Piece pieceAtRowCol = this.mQuestBoard.GetPieceAtRowCol(mRow + DigGoal.Update_neighbors[k, 1], mCol + DigGoal.Update_neighbors[k, 0]);
					if (pieceAtRowCol != null)
					{
						this.mQuestBoard.SetMoveCredit(pieceAtRowCol, mMoveCreditId);
						bool flag = false;
						bool theIsSpecialGem = false;
						bool flag2 = false;
						if (mIsHyperCube)
						{
							theIsSpecialGem = true;
							flag = this.TriggerDigPiece(this.mQuestBoard.GetPieceAtRowCol(mRow, mCol), pieceAtRowCol, theIsSpecialGem, true);
							flag2 = true;
						}
						if (!flag)
						{
							pieceAtRowCol = this.mQuestBoard.GetPieceAtRowCol(mRow + DigGoal.Update_neighbors[k, 1], mCol + DigGoal.Update_neighbors[k, 0]);
							if (pieceAtRowCol != null)
							{
								this.TriggerDigPiece(this.mQuestBoard.GetPieceAtRowCol(mRow, mCol), pieceAtRowCol, theIsSpecialGem, true, !flag2, true);
							}
						}
					}
				}
			}
			if (this.mQuestBoard.GetTimeLimit() - this.mQuestBoard.GetTicksLeft() / 100 >= GlobalMembers.M(1) && this.mGoldRushTipPieceId != -1)
			{
				int num2 = (this.mQuestBoard.mIsPerpetual ? GlobalMembers.M(3) : GlobalMembers.M(5));
				int num3 = (this.mQuestBoard.mIsPerpetual ? GlobalMembers.M(2) : GlobalMembers.M(4));
				Piece piece = this.mQuestBoard.mBoard[num3, num2];
				if (piece != null && piece.mId == this.mGoldRushTipPieceId)
				{
					if (this.mQuestBoard.mGoAnnouncementDone)
					{
						this.mQuestBoard.ShowHint(piece, true);
						this.mQuestBoard.mGoAnnouncementDone = false;
					}
					if ((double)this.mQuestBoard.mVisPausePct == 0.0 && this.mQuestBoard.AllowTooltips())
					{
						GlobalMembers.gApp.mTooltipManager.RequestTooltip(this.mQuestBoard, this.mTutorialHeader, this.mTutorialText, new Point((int)GlobalMembers.S(piece.CX()), (int)GlobalMembers.S(piece.CY())), GlobalMembers.MS(320), 1, 0, null, null, 0, -1);
					}
				}
			}
			this.mCheckPieces.Clear();
			if (!this.mCvScrollY.IsDoingCurve())
			{
				if (this.mBlackrockTipPieceId != -1 && this.mQuestBoard.WantsTutorial(16))
				{
					Piece pieceById2 = this.mQuestBoard.GetPieceById(this.mBlackrockTipPieceId);
					if (pieceById2 != null)
					{
						this.mQuestBoard.DeferTutorialDialog(16, pieceById2);
					}
					this.mBlackrockTipPieceId = -1;
				}
				if (this.mMovingPieces.Count != 0)
				{
					int count = this.mMovingPieces.Count;
					for (int l = 0; l < count; l++)
					{
						Piece piece2;
						if ((piece2 = this.mMovingPieces[l]) != null)
						{
							this.mQuestBoard.mPreFXManager.FreePieceEffect(piece2);
							this.mQuestBoard.mPostFXManager.FreePieceEffect(piece2);
							piece2.release();
						}
					}
					this.mMovingPieces.Clear();
				}
			}
			this.mDustFXManager.Update();
			this.mMessageFXManager.Update();
			Piece[,] mBoard = this.mQuestBoard.mBoard;
			int upperBound = mBoard.GetUpperBound(0);
			int upperBound2 = mBoard.GetUpperBound(1);
			for (int m = mBoard.GetLowerBound(0); m <= upperBound; m++)
			{
				for (int n = mBoard.GetLowerBound(1); n <= upperBound2; n++)
				{
					Piece piece3 = mBoard[m, n];
					if (piece3 != null && piece3.IsFlagSet(65536U))
					{
						piece3.mShakeScale = 0f;
					}
				}
			}
			bool flag3 = false;
			if (this.mAllowDescent && !this.mCvScrollY.IsDoingCurve())
			{
				flag3 = this.CheckNeedScroll(false);
			}
			if (flag3)
			{
				if (!this.mDigBarFlash.IsDoingCurve())
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DIG_BAR_FLASH, this.mDigBarFlash);
					this.mDigBarFlash.SetMode(1);
				}
				if (this.mQuestBoard.IsBoardStill() && this.mQuestBoard.mQueuedMoveVector.Count == 0 && this.mQuestBoard.mDeferredTutorialVector.Count == 0 && GlobalMembers.gApp.GetDialog(18) == null)
				{
					this.mQuestBoard.mNeverAllowCascades = this.mDefaultNeverAllowCascades;
					this.mForceScroll = false;
					this.mInMegaDig = this.CheckNeedScroll(true);
					if (this.mGridDepth == 2.0 || this.mInMegaDig)
					{
						this.mQuestBoard.mPowerGemThreshold = this.mPowerGemThresholdDepth40;
					}
					else if (this.mGridDepth == 0.0)
					{
						this.mQuestBoard.mPowerGemThreshold = this.mPowerGemThresholdDepth20;
					}
					this.mQuestBoard.SetColorCount(6);
					((DigBoard)this.mQuestBoard).DoTimeBonus(this.mInMegaDig);
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG);
					((DigBoard)this.mQuestBoard).SetCogsAnim(true);
					this.mGridDepth += (double)((int)this.mCvScrollY * this.GetDigCountPerScroll());
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_CV_SCROLL_Y, this.mCvScrollY);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_CV_SHAKEY, this.mCvShakey);
					this.mCvShakey.mLinkedVal = this.mCvScrollY;
				}
				else
				{
					this.mQuestBoard.mNeverAllowCascades = true;
					if (this.mAllClearedAnimAtTick > 0 && this.mUpdateCnt - this.mAllClearedAnimAtTick >= GlobalMembers.M(100))
					{
						this.mAllClearedAnimAtTick++;
					}
				}
				if (!this.IsDoubleHypercubeActive())
				{
					if (this.CheckNeedScroll(true))
					{
						if (this.mAllClearedAnimAtTick <= 0)
						{
							this.mDigInProgress = true;
							this.mAllClearedAnimAtTick = this.mUpdateCnt;
							this.mAllClearAnimPlayed = true;
							GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA.ResetAnim();
							GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG_LINE_HIT_MEGA, 0, GlobalMembers.M(1.0));
						}
					}
					else if (this.mAllClearedAnimAtTick <= 0 && this.mClearedAnimAtTick <= 0)
					{
						this.mDigInProgress = true;
						this.mClearedAnimAtTick = this.mUpdateCnt;
						this.mClearedAnimPlayed = true;
						GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT.ResetAnim();
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG_LINE_HIT, 0, GlobalMembers.M(1.0));
					}
				}
			}
			else
			{
				this.mQuestBoard.mNeverAllowCascades = this.mDefaultNeverAllowCascades;
				if (GlobalMembers.M(1) != 0 && this.mDigBarFlash.IsDoingCurve())
				{
					this.mDigBarFlash.SetMode(0);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_DIG_BAR_FLASH_OFF, this.mDigBarFlash);
					this.mDigBarFlash.Intercept(string.Empty);
				}
				this.mDigBarFlashCount = 0;
				if (this.mClearedAnimAtTick > 0 && this.mUpdateCnt - this.mClearedAnimAtTick > GlobalMembers.M(300))
				{
					this.mClearedAnimAtTick = -1;
					this.mClearedAnimPlayed = false;
				}
				if (this.mAllClearedAnimAtTick > 0 && this.mUpdateCnt - this.mAllClearedAnimAtTick > GlobalMembers.M(600))
				{
					this.mAllClearedAnimAtTick = -1;
					this.mAllClearAnimPlayed = false;
				}
			}
			double num4 = this.mDigBarFlash;
			this.mDigBarFlash.IncInVal();
			if (num4 < this.mDigBarFlash && num4 < GlobalMembers.M(0.1) && this.mDigBarFlash >= GlobalMembers.M(0.1))
			{
				this.mDigBarFlashCount++;
				if (this.mDigBarFlashCount != GlobalMembers.M(1) && this.mDigBarFlashCount >= GlobalMembers.M(3))
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG_NOTIFY, 0, GlobalMembers.M(1.0));
				}
			}
			((DigBoard)this.mQuestBoard).UpdateCogsAnim();
			float num5 = -1f;
			this.mQuestBoard.mBoardUIOffsetY = (int)((float)this.GetBoardScrollOffsetY() * num5);
			if (this.mCvShakey.IsDoingCurve() && (int)this.mCvShakey > 0)
			{
				this.mQuestBoard.mOffsetX = BejeweledLivePlus.Misc.Common.Rand() % (int)this.mCvShakey - (int)this.mCvShakey / 2;
				this.mQuestBoard.mOffsetY = BejeweledLivePlus.Misc.Common.Rand() % (int)this.mCvShakey - (int)this.mCvShakey / 2;
			}
			else
			{
				this.mQuestBoard.mOffsetX = 0;
				this.mQuestBoard.mOffsetY = 0;
			}
			int num6 = this.mHypercubes.Count;
			int num7 = 0;
			while (num7 < num6)
			{
				Piece pieceById3 = this.mQuestBoard.GetPieceById(this.mHypercubes[num7]);
				if (pieceById3 == null)
				{
					this.mHypercubes.RemoveAt(num7);
					num6--;
				}
				else if (pieceById3.mTallied)
				{
					this.mQuestBoard.Hypercubeify(pieceById3);
					this.mHypercubes.RemoveAt(num7);
					num6--;
				}
				else if (this.IsSpecialPieceUnlocked(pieceById3))
				{
					pieceById3.mCanSwap = true;
					this.mQuestBoard.Hypercubeify(pieceById3);
					this.mHypercubes.RemoveAt(num7);
					num6--;
				}
				else
				{
					num7++;
				}
			}
		}

		public bool TriggerDigPiece(Piece theFromPiece, Piece thePiece, bool theIsSpecialGem, bool theAllowDeletion, bool theAllowSound)
		{
			return this.TriggerDigPiece(theFromPiece, thePiece, theIsSpecialGem, theAllowDeletion, theAllowSound, false);
		}

		public bool TriggerDigPiece(Piece theFromPiece, Piece thePiece, bool theIsSpecialGem, bool theAllowDeletion)
		{
			return this.TriggerDigPiece(theFromPiece, thePiece, theIsSpecialGem, theAllowDeletion, true, false);
		}

		public bool TriggerDigPiece(Piece theFromPiece, Piece thePiece, bool theIsSpecialGem, bool theAllowDeletion, bool theAllowSound, bool theForce)
		{
			if (!theForce && this.mVisited[thePiece.mRow][thePiece.mCol])
			{
				return false;
			}
			bool flag = false;
			int mRow = thePiece.mRow;
			int mCol = thePiece.mCol;
			if (thePiece.IsFlagSet(65536U))
			{
				thePiece.mShakeScale = 0f;
				if (this.IsSpecialPiece(thePiece))
				{
					if (thePiece.IsFlagSet(2U))
					{
						if (theFromPiece == null)
						{
							thePiece.mLastColor = this.mQuestBoard.mNewGemColors[BejeweledLivePlus.Misc.Common.Rand() % this.mQuestBoard.mNewGemColors.size<int>()];
						}
						else if (((long)theFromPiece.mExplodeSourceFlags & 2L) != 0L)
						{
							int num = 20;
							while (num-- > 0)
							{
								thePiece.mLastColor = this.mQuestBoard.mNewGemColors[BejeweledLivePlus.Misc.Common.Rand() % this.mQuestBoard.mNewGemColors.size<int>()];
								if (thePiece.mLastColor != theFromPiece.mColor)
								{
									break;
								}
							}
						}
					}
					this.mQuestBoard.TriggerSpecial(thePiece, theFromPiece);
				}
				else if (this.mIdToTileData.ContainsKey(thePiece.mId))
				{
					DigGoal.TileData tileData = this.mIdToTileData[thePiece.mId];
					if (tileData.mIsDeleting)
					{
						return false;
					}
					DigGoal.EDigPieceType mPieceType = tileData.mPieceType;
					if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal || tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
					{
						if (tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
						{
							GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_QUEST_GET);
							this.mQuestBoard.AddToStat(36);
							DigGoal.ArtifactData artifactData = this.mArtifacts[tileData.mArtifactId];
							this.mCollectedArtifacts.Add(tileData.mArtifactId);
							Point point = new Point(ConstantsWP.DIG_BOARD_SCORE_CENTER_X, ConstantsWP.DIG_BOARD_SCORE_BTM_Y);
							int num2 = 0;
							int num3 = GlobalMembers.M(200);
							for (int i = 0; i < this.mGoldFx.Count; i++)
							{
								GoldCollectEffect goldCollectEffect = this.mGoldFx[i];
								if (goldCollectEffect.mCentering)
								{
									num2 = Math.Max(num2, goldCollectEffect.mExtraSplineTime + num3 - (this.mQuestBoard.mGameTicks - goldCollectEffect.mStartedAtTick));
								}
							}
							GoldCollectEffect goldCollectEffect2 = new GoldCollectEffect(this, tileData);
							goldCollectEffect2.mExtraSplineTime = num2;
							goldCollectEffect2.mTreasureType = ETreasureType.eTreasure_Artifact;
							goldCollectEffect2.mVal = artifactData.mValue * this.mArtifactBaseValue;
							goldCollectEffect2.mImageId = artifactData.mImageId;
							goldCollectEffect2.mSrcImageId = artifactData.mImageId;
							goldCollectEffect2.mGlowImageId = 1290;
							goldCollectEffect2.mExtraScaling = GlobalMembers.M(0.75);
							goldCollectEffect2.mStartPoint = new Point((int)thePiece.CX(), (int)thePiece.CY());
							goldCollectEffect2.mTargetPoint = new Point(point.mX, point.mY);
							goldCollectEffect2.mCentering = true;
							goldCollectEffect2.mDisplayVal = artifactData.mValue * this.mArtifactBaseValue;
							goldCollectEffect2.mDisplayName = artifactData.mName;
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_PARTICLE_EMIT_OVER_TIME_ARTIFACT, goldCollectEffect2.mParticleEmitOverTime);
							goldCollectEffect2.mGlowRGB = GlobalMembers.M(10047098);
							goldCollectEffect2.mLayerOnTop = true;
							goldCollectEffect2.Init();
							this.mGoldFx.Insert(0, goldCollectEffect2);
							int theMoveCreditId = ((theFromPiece != null) ? theFromPiece.mMoveCreditId : thePiece.mMoveCreditId);
							this.mQuestBoard.AddToStat(1, goldCollectEffect2.mVal, theMoveCreditId);
							this.mQuestBoard.MaxStat(25, this.mQuestBoard.GetMoveStat(theMoveCreditId, 1));
						}
						else
						{
							if (tileData.mStrength > 3)
							{
								GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_TREASUREFIND_DIAMONDS, this.mQuestBoard.GetPanPosition(thePiece));
							}
							else
							{
								GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_TREASUREFIND, this.mQuestBoard.GetPanPosition(thePiece));
							}
							int num4 = tileData.mStrength;
							int num5 = num4;
							if (num4 == 9 || !this.mQuestBoard.mIsPerpetual)
							{
								num5 = 1;
							}
							PointsEffect pointsEffect = null;
							foreach (Effect effect in this.mGoldPointsFXManager.mEffectList[1])
							{
								pointsEffect = (PointsEffect)effect;
								if (pointsEffect.mPieceId == thePiece.mId)
								{
									break;
								}
								pointsEffect = null;
							}
							ETreasureType etreasureType = ((num4 <= 3) ? ETreasureType.eTreasure_Gold : ETreasureType.eTreasure_Gem);
							int num6 = num5;
							if (this.mQuestBoard.mIsPerpetual)
							{
								int num7 = Math.Min(num5 - 1, this.mTreasureRangeMax.Count - 1);
								int num8 = this.mTreasureRangeMax[num7] - this.mTreasureRangeMin[num7];
								num5 = this.mTreasureRangeMin[num7] + BejeweledLivePlus.Misc.Common.Rand() % (num8 + 1);
								if (etreasureType == ETreasureType.eTreasure_Gold)
								{
									num6 = num5 * this.mGoldValue;
								}
								else if (etreasureType == ETreasureType.eTreasure_Gem)
								{
									num6 = num5 * this.mGemValue;
								}
							}
							if (pointsEffect != null)
							{
								pointsEffect.mCount += num6;
							}
							else
							{
								pointsEffect = PointsEffect.alloc(num6, thePiece.mId, !this.mQuestBoard.mIsPerpetual);
								this.mGoldPointsFXManager.AddEffect(pointsEffect);
							}
							pointsEffect.mX = thePiece.CX();
							pointsEffect.mY = thePiece.CY();
							foreach (Effect effect2 in this.mGoldPointsFXManager.mEffectList[1])
							{
								PointsEffect pointsEffect2 = (PointsEffect)effect2;
								if (pointsEffect2 != pointsEffect && Math.Abs(pointsEffect.mX - pointsEffect2.mX) <= (float)GlobalMembers.M(200) && Math.Abs(pointsEffect.mY - pointsEffect2.mY) <= (float)GlobalMembers.M(40))
								{
									pointsEffect.mY += (float)GlobalMembers.M(50);
									break;
								}
							}
							for (int j = 0; j < num5; j++)
							{
								if (this.mQuestBoard.mIsPerpetual)
								{
									GoldCollectEffect goldCollectEffect2 = new GoldCollectEffect(this, tileData);
									int num9;
									if (tileData.mDiamondFx != null)
									{
										num9 = tileData.mDiamondFx.GetExplodeSubId(BejeweledLivePlus.Misc.Common.Rand());
										switch (num4 - 3)
										{
										case 1:
											goldCollectEffect2.mGlowRGB = GlobalMembers.M(4052442);
											goldCollectEffect2.mGlowRGB2 = GlobalMembers.M(4717055);
											break;
										case 2:
											goldCollectEffect2.mGlowRGB = GlobalMembers.M(6580987);
											goldCollectEffect2.mGlowRGB2 = GlobalMembers.M(3386875);
											break;
										case 3:
											goldCollectEffect2.mGlowRGB = GlobalMembers.M(16347389);
											goldCollectEffect2.mGlowRGB2 = GlobalMembers.M(16467965);
											break;
										case 4:
											goldCollectEffect2.mGlowRGB = GlobalMembers.M(16428975);
											goldCollectEffect2.mGlowRGB2 = GlobalMembers.M(16416091);
											break;
										}
										goldCollectEffect2.mUseBaseSparkles = false;
									}
									else
									{
										num9 = 1147 + BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(9);
										goldCollectEffect2.mGlowRGB = GlobalMembers.M(16118660);
									}
									Point point2 = new Point(GlobalMembers.S(ConstantsWP.DIG_BOARD_SCORE_CENTER_X), GlobalMembers.S(ConstantsWP.DIG_BOARD_SCORE_BTM_Y) - GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK.mHeight / 2);
									goldCollectEffect2.mTreasureType = etreasureType;
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_PARTICLE_EMIT_OVER_TIME, goldCollectEffect2.mParticleEmitOverTime);
									goldCollectEffect2.mVal = ((etreasureType == ETreasureType.eTreasure_Gem) ? this.mGemValue : this.mGoldValue);
									goldCollectEffect2.mImageId = num9;
									goldCollectEffect2.mSrcImageId = num9;
									goldCollectEffect2.mStartPoint = new Point((int)((float)this.mQuestBoard.GetBoardX() + thePiece.mX + (float)DigGoal.TriggerDigPiece_goldCollectRect.mX + (float)(BejeweledLivePlus.Misc.Common.Rand() % DigGoal.TriggerDigPiece_goldCollectRect.mWidth)), (int)((float)this.mQuestBoard.GetBoardY() + thePiece.mY + (float)DigGoal.TriggerDigPiece_goldCollectRect.mY + (float)(BejeweledLivePlus.Misc.Common.Rand() % DigGoal.TriggerDigPiece_goldCollectRect.mHeight)));
									goldCollectEffect2.mTargetPoint = new Point(point2.mX, point2.mY);
									goldCollectEffect2.mTimeMod = GlobalMembersUtils.GetRandFloatU() * GlobalMembers.M(0.5f);
									goldCollectEffect2.mLayerOnTop = false;
									goldCollectEffect2.Init();
									this.mGoldFx.Add(goldCollectEffect2);
									int theMoveCreditId2 = ((theFromPiece != null) ? theFromPiece.mMoveCreditId : thePiece.mMoveCreditId);
									this.mQuestBoard.AddToStat(1, goldCollectEffect2.mVal, theMoveCreditId2);
									this.mQuestBoard.MaxStat(25, this.mQuestBoard.GetMoveStat(theMoveCreditId2, 1));
								}
								else
								{
									this.TriggerDigCoin(thePiece, num4 - 1);
								}
							}
						}
						if ((tileData.mStrength == 1 || this.mQuestBoard.mIsPerpetual) && (tileData.mStrength < 9 || tileData.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact))
						{
							tileData.SetAs(DigGoal.EDigPieceType.eDigPiece_Block, 1, thePiece, this);
						}
					}
					if (theIsSpecialGem && mPieceType == DigGoal.EDigPieceType.eDigPiece_Block && tileData.mStrength == 4)
					{
						tileData.mStrength.value = 1;
					}
					if (tileData.mStrength >= 1 && (tileData.mStrength != 4 || mPieceType != DigGoal.EDigPieceType.eDigPiece_Block))
					{
						if (theAllowSound && mPieceType == DigGoal.EDigPieceType.eDigPiece_Block)
						{
							int num10 = tileData.mStrength;
							if (num10 != 1)
							{
								if (num10 != 4)
								{
									GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_STONE_CRACKED, this.mQuestBoard.GetPanPosition(thePiece));
								}
								else
								{
									GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_BIGSTONE_CRACKED, this.mQuestBoard.GetPanPosition(thePiece));
								}
							}
							else
							{
								GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIRT_CRACKED, this.mQuestBoard.GetPanPosition(thePiece));
							}
						}
						if (tileData.mStrength > 4)
						{
							tileData.mStrength.value--;
						}
						tileData.mStrength.value--;
						if (tileData.mStrength == 0)
						{
							this.CreateRockFragments(thePiece, true);
							tileData.mIsDeleting = true;
							if (theAllowDeletion)
							{
								this.mQuestBoard.DeletePiece(thePiece);
								flag = true;
							}
						}
						else
						{
							this.CreateRockFragments(thePiece, false);
						}
					}
				}
				if (!flag)
				{
					thePiece.mElectrocutePercent = 0f;
				}
			}
			this.mVisited[mRow][mCol] = true;
			return flag;
		}

		public void CreateRockFragments(Piece i_piece, bool i_isDirt)
		{
			this.AddDustEffect(new FPoint(i_piece.CX(), i_piece.CY()));
			int num = (int)GlobalMembers.S(100.0 * GlobalMembers.M(0.75));
			if (i_isDirt)
			{
				int num2 = ConstantsWP.DIG_BOARD_BROWN_ROCK_BASE_COUNT + BejeweledLivePlus.Misc.Common.Rand(ConstantsWP.DIG_BOARD_BROWN_ROCK_RAND_COUNT);
				for (int i = 0; i < num2; i++)
				{
					this.CreateRockFragment((int)(i_piece.CX() - (float)(num / 2) + (float)BejeweledLivePlus.Misc.Common.Rand(num)), (int)(i_piece.CY() - (float)(num / 2) + (float)BejeweledLivePlus.Misc.Common.Rand(num)), GlobalMembersResourcesWP.IMAGE_WALLROCKS_SMALL_BROWN, (float)GlobalMembers.MS(10));
				}
				return;
			}
			this.CreateRockFragment((int)i_piece.CX(), (int)i_piece.CY(), GlobalMembersResourcesWP.IMAGE_WALLROCKS_LARGE, (float)GlobalMembers.MS(2));
			int num3 = ConstantsWP.DIG_BOARD_MEDIUM_ROCK_BASE_COUNT + BejeweledLivePlus.Misc.Common.Rand(ConstantsWP.DIG_BOARD_MEDIUM_ROCK_RAND_COUNT);
			for (int j = 0; j < num3; j++)
			{
				this.CreateRockFragment((int)(i_piece.CX() - (float)(num / 2) + (float)BejeweledLivePlus.Misc.Common.Rand(num)), (int)(i_piece.CY() - (float)(num / 2) + (float)BejeweledLivePlus.Misc.Common.Rand(num)), GlobalMembersResourcesWP.IMAGE_WALLROCKS_MEDIUM, (float)GlobalMembers.MS(6));
			}
			num3 = ConstantsWP.DIG_BOARD_SMALL_ROCK_BASE_COUNT + BejeweledLivePlus.Misc.Common.Rand(ConstantsWP.DIG_BOARD_SMALL_ROCK_RAND_COUNT);
			for (int k = 0; k < num3; k++)
			{
				this.CreateRockFragment((int)(i_piece.CX() - (float)(num / 2) + (float)BejeweledLivePlus.Misc.Common.Rand(num)), (int)(i_piece.CY() - (float)(num / 2) + (float)BejeweledLivePlus.Misc.Common.Rand(num)), GlobalMembersResourcesWP.IMAGE_WALLROCKS_SMALL, (float)GlobalMembers.MS(8));
			}
		}

		public void CreateRockFragment(int theX, int theY, Image theImage, float theSpeed)
		{
			Effect effect = this.mQuestBoard.mPostFXManager.AllocEffect(Effect.Type.TYPE_WALL_ROCK);
			float num = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
			effect.mDX = theSpeed * (float)Math.Cos((double)num);
			effect.mDY = theSpeed * (float)Math.Sin((double)num);
			effect.mDecel = GlobalMembers.M(0.95f);
			effect.mX = (float)theX;
			effect.mY = (float)theY;
			effect.mGravity = GlobalMembers.M(0.33f);
			effect.mImage = theImage;
			effect.mFrame = BejeweledLivePlus.Misc.Common.Rand(4);
			effect.mDAlpha = 0f;
			this.mQuestBoard.mPostFXManager.AddEffect(effect);
		}

		public DigGoal.TileData GenRandomTile(float cvLevel, Piece piece, ref bool o_isImmovable)
		{
			double outVal = this.mCvMinBrickStr.GetOutVal((double)cvLevel);
			double outVal2 = this.mCvMaxBrickStr.GetOutVal((double)cvLevel);
			double outVal3 = this.mCvMinMineStr.GetOutVal((double)cvLevel);
			double outVal4 = this.mCvMaxMineStr.GetOutVal((double)cvLevel);
			double outVal5 = this.mCvMineProb.GetOutVal((double)cvLevel);
			double outVal6 = this.mCvDarkRockFreq.GetOutVal((double)cvLevel);
			if ((double)GlobalMembersUtils.GetRandFloatU() < outVal5)
			{
				double num = outVal3 + (outVal4 - outVal3) * (double)this.mMineStrSpread.GetOutVal(GlobalMembersUtils.GetRandFloatU());
				int theStr = (int)Math.Max(1f, Math.Min(7f, Utils.Round((float)num)));
				if (!this.mIdToTileData.ContainsKey(piece.mId))
				{
					this.mIdToTileData.Add(piece.mId, new DigGoal.TileData());
				}
				this.mIdToTileData[piece.mId].SetAs(DigGoal.EDigPieceType.eDigPiece_Goal, theStr, piece, this);
				piece.SetFlag(65536U);
				o_isImmovable = true;
				return this.mIdToTileData[piece.mId];
			}
			double num2 = outVal + (outVal2 - outVal) * (double)this.mBrickStrSpread.GetOutVal(GlobalMembersUtils.GetRandFloatU());
			if (piece.mCol == 0 || piece.mCol == 7)
			{
				num2 *= this.mCvEdgeBrickStrPerLevel.GetOutVal((double)cvLevel);
			}
			int num3 = Math.Max(1, Math.Min(5, (int)Utils.Round((float)num2)));
			if (num3 == 4)
			{
				num3 = 5;
			}
			if (num3 == 5 && (double)GlobalMembersUtils.GetRandFloatU() <= outVal6)
			{
				num3 = 4;
			}
			if (!this.mIdToTileData.ContainsKey(piece.mId))
			{
				this.mIdToTileData.Add(piece.mId, new DigGoal.TileData());
			}
			this.mIdToTileData[piece.mId].SetAs(DigGoal.EDigPieceType.eDigPiece_Block, num3, piece, this);
			piece.SetFlag(65536U);
			o_isImmovable = true;
			return this.mIdToTileData[piece.mId];
		}

		public int GetBoardScrollOffsetY()
		{
			if (!this.mQuestBoard.mIsPerpetual)
			{
				return 0;
			}
			if (this.mCvScrollY > 0.0 && this.mCvScrollY < 1.0 - (double)GlobalMembers.SEXYMATH_EPSILON)
			{
				return (int)(100.0 - 100.0 * (this.GetGridDepthAsDouble() - (double)this.GetGridDepth()));
			}
			return 0;
		}

		public void DeleteAllPieces()
		{
			this.mIdToTileData.Clear();
			Piece[,] mBoard = this.mQuestBoard.mBoard;
			int upperBound = mBoard.GetUpperBound(0);
			int upperBound2 = mBoard.GetUpperBound(1);
			for (int i = mBoard.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = mBoard.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = mBoard[i, j];
					if (piece != null && piece.IsFlagSet(65536U))
					{
						this.DeletePiece(piece);
					}
				}
			}
		}

		public override void LevelUp()
		{
			if (this.mGoldFx.Count != 0)
			{
				return;
			}
			base.LevelUp();
		}

		public override void NewGame()
		{
			Dictionary<string, string> mParams = this.mQuestBoard.mParams;
			this.mArtifacts.Clear();
			if (!mParams.ContainsKey("TargetCount") || !int.TryParse(mParams["TargetCount"], ref this.mTargetCount))
			{
				this.mTargetCount = 0;
			}
			List<GridData> list = new List<GridData>();
			Board.ParseGridLayout(mParams["Grids"], list, false);
			if (!mParams.ContainsKey("DigCountPerScroll") || !int.TryParse(mParams["DigCountPerScroll"], ref this.mDigCountPerScroll))
			{
				this.mDigCountPerScroll = 0;
			}
			if (!mParams.ContainsKey("GoldValue") || !int.TryParse(mParams["GoldValue"], ref this.mGoldValue))
			{
				this.mGoldValue = 0;
			}
			if (!mParams.ContainsKey("DiamondValue") || !int.TryParse(mParams["DiamondValue"], ref this.mGemValue))
			{
				this.mGemValue = 0;
			}
			if (!mParams.ContainsKey("ArtifactBaseValue") || !int.TryParse(mParams["ArtifactBaseValue"], ref this.mArtifactBaseValue))
			{
				this.mArtifactBaseValue = 0;
			}
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_ARTIFACT_POSS_RANGE, this.mArtifactPossRange);
			if (!mParams.ContainsKey("ArtifactSkipTileCount") || !int.TryParse(mParams["ArtifactSkipTileCount"], ref this.mArtifactSkipTileCount))
			{
				this.mArtifactSkipTileCount = 0;
			}
			if (!mParams.ContainsKey("PowerGemThresholdDepth0") || !int.TryParse(mParams["PowerGemThresholdDepth0"], ref this.mPowerGemThresholdDepth0))
			{
				this.mPowerGemThresholdDepth0 = 0;
			}
			if (!mParams.ContainsKey("PowerGemThresholdDepth20") || !int.TryParse(mParams["PowerGemThresholdDepth20"], ref this.mPowerGemThresholdDepth20))
			{
				this.mPowerGemThresholdDepth20 = 0;
			}
			if (!mParams.ContainsKey("PowerGemThresholdDepth40") || !int.TryParse(mParams["PowerGemThresholdDepth40"], ref this.mPowerGemThresholdDepth40))
			{
				this.mPowerGemThresholdDepth40 = 0;
			}
			if (mParams.ContainsKey("TreasureRange"))
			{
				List<int> list2 = new List<int>();
				Utils.SplitAndConvertStr(mParams["TreasureRange"], list2);
				for (int i = 0; i < list2.Count; i++)
				{
					if (i % 2 == 0)
					{
						this.mTreasureRangeMin.Add(list2[i]);
					}
					else if (i % 2 == 1)
					{
						this.mTreasureRangeMax.Add(list2[i]);
					}
				}
			}
			if (mParams.ContainsKey("ArtifactMinRows"))
			{
				Utils.SplitAndConvertStr(mParams["ArtifactMinRows"], this.mStartArtifactRow);
			}
			if (!mParams.ContainsKey("ArtifactMinTiles") || !int.TryParse(mParams["ArtifactMinTiles"], ref this.mArtifactMinTiles))
			{
				this.mArtifactMinTiles = 0;
			}
			if (!mParams.ContainsKey("ArtifactMaxTiles") || !int.TryParse(mParams["ArtifactMaxTiles"], ref this.mArtifactMaxTiles))
			{
				this.mArtifactMaxTiles = 0;
			}
			int num = 1;
			while (mParams.ContainsKey(string.Format("Artifact{0}", num)))
			{
				List<string> list3 = new List<string>();
				Utils.SplitAndConvertStr(mParams[string.Format("Artifact{0}", num)], list3, ',', true, -1);
				DigGoal.ArtifactData artifactData = new DigGoal.ArtifactData();
				artifactData.mId = list3[0];
				artifactData.mName = list3[3];
				artifactData.mMinDepth = GlobalMembers.sexyatoi(list3[1]);
				artifactData.mValue = GlobalMembers.sexyatoi(list3[2]);
				string theStringId = string.Format("IMAGE_QUEST_DIG_BOARD_ITEM_{0}_BIG", artifactData.mId).ToUpper();
				artifactData.mImageId = (int)GlobalMembersResourcesWP.GetIdByStringId(theStringId);
				artifactData.mUnderlayImgId = this.ARTIFACT_UNDERLAY_IDS[BejeweledLivePlus.Misc.Common.Rand() % this.ARTIFACT_UNDERLAY_IDS.Length];
				artifactData.mOverlayImgId = this.ARTIFACT_OVERLAY_IDS[BejeweledLivePlus.Misc.Common.Rand() % this.ARTIFACT_OVERLAY_IDS.Length];
				if (artifactData.mImageId != -1)
				{
					this.mArtifacts.Add(artifactData);
				}
				num++;
			}
			this.mTutorialHeader = mParams["TutorialHeader"];
			this.mTutorialText = mParams["TutorialText"];
			if (mParams.ContainsKey("MaxBrickStrPerLevel"))
			{
				this.mUsingRandomizers = true;
				CurvedVal curvedVal = new CurvedVal();
				CurvedVal curvedVal2 = new CurvedVal();
				CurvedVal curvedVal3 = new CurvedVal();
				this.mCvMinBrickStr.SetCurve(mParams["MinBrickStrPerLevel"]);
				this.mCvMaxBrickStr.SetCurve(mParams["MaxBrickStrPerLevel"]);
				this.mCvEdgeBrickStrPerLevel.SetCurve(mParams["EdgeBrickStrPerLevel"]);
				this.mCvMinMineStr.SetCurve(mParams["MinMineStrPerLevel"]);
				this.mCvMaxMineStr.SetCurve(mParams["MaxMineStrPerLevel"]);
				this.mCvMineProb.SetCurve(mParams["MineProbPerLevel"]);
				this.mCvDarkRockFreq.SetCurve(mParams["DarkRockFrequency"]);
				curvedVal.SetCurve(mParams["ArtifactSpread"]);
				curvedVal2.SetCurve(mParams["BrickStrSpread"]);
				curvedVal3.SetCurve(mParams["MineStrSpread"]);
				this.mBrickStrSpread.SetToCurve(curvedVal2);
				this.mArtifactSpread.SetToCurve(curvedVal);
				this.mMineStrSpread.SetToCurve(curvedVal3);
				this.mQuestBoard.mIsPerpetual = true;
				this.mQuestBoard.mAllowLevelUp = false;
			}
			new List<int>();
			new List<int>();
			new List<int>();
			base.NewGame();
			this.AdvanceArtifact(true);
			this.mInitPushAnimCv.mAppUpdateCountSrc = this.mUpdateCnt;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_INIT_PUSH_ANIM_CV, this.mInitPushAnimCv);
			if (list.size<GridData>() > 0)
			{
				this.mGridData = list.front<GridData>();
				this.mAllowDescent = this.mUsingRandomizers || this.mGridData.GetRowCount() > 8;
				if (!this.mQuestBoard.mContinuedFromLoad)
				{
					this.SyncGrid(0);
					int num2 = 0;
					while (num2 < 1000 && !this.mQuestBoard.FindMove(null, 0, true, true))
					{
						Piece piece = this.mQuestBoard.mBoard[BejeweledLivePlus.Misc.Common.Rand() % 8, BejeweledLivePlus.Misc.Common.Rand() % 8];
						if (piece.mFlags == 0 && piece.mColor != -1)
						{
							piece.mColor = BejeweledLivePlus.Misc.Common.Rand() % 7;
						}
						num2++;
					}
				}
			}
			this.mQuestBoard.mWantShowPoints = false;
			if (GlobalMembers.gApp.mDiamondMineFirstLaunch)
			{
				int num3 = (this.mQuestBoard.mIsPerpetual ? GlobalMembers.M(3) : GlobalMembers.M(5));
				int num4 = (this.mQuestBoard.mIsPerpetual ? GlobalMembers.M(2) : GlobalMembers.M(4));
				Piece piece2 = this.mQuestBoard.mBoard[num4, num3];
				if (piece2 != null)
				{
					this.mGoldRushTipPieceId = piece2.mId;
				}
				GlobalMembers.gApp.mDiamondMineFirstLaunch = false;
			}
			this.mDefaultNeverAllowCascades = this.mQuestBoard.mNeverAllowCascades;
			if (!this.mLoadingGame)
			{
				((DigBoard)this.mQuestBoard).mRotatingCounter.ResetCounter(0);
			}
			this.mLoadingGame = false;
			this.mDigInProgress = false;
		}

		public override int GetLevelPoints()
		{
			return this.mTargetCount;
		}

		public new bool GetTooltipText(Piece thePiece, ref string theHeader, ref string theBody)
		{
			bool result = false;
			if (thePiece.IsFlagSet(65536U))
			{
				DigGoal.TileData tileData = this.mIdToTileData[thePiece.mId];
				switch (tileData.mPieceType)
				{
				case DigGoal.EDigPieceType.eDigPiece_Artifact:
					theHeader = GlobalMembers._ID("ARTIFACT", 184);
					theBody = GlobalMembers._ID("Uncover this object for bonus points.", 185);
					result = true;
					break;
				case DigGoal.EDigPieceType.eDigPiece_Block:
					if (this.mHypercubes.Contains(thePiece.mId))
					{
						theHeader = GlobalMembers._ID("HYPERCUBE", 190);
						theBody = GlobalMembers._ID("Uncover this buried Hypercube in order to use it.", 191);
						result = true;
					}
					else
					{
						switch (tileData.mStrength)
						{
						case 1:
							theHeader = GlobalMembers._ID("DIRT", 192);
							theBody = GlobalMembers._ID("Match a Gem adjacent to this block to break it up.", 193);
							result = true;
							break;
						case 2:
							theHeader = GlobalMembers._ID("ROCKS", 194);
							theBody = GlobalMembers._ID("Match 2 Gems adjacent to this block to break it up.", 195);
							result = true;
							break;
						case 3:
							theHeader = GlobalMembers._ID("STONES", 196);
							theBody = GlobalMembers._ID("Match 3 Gems adjacent to this block to break it up.", 197);
							result = true;
							break;
						case 4:
							theHeader = GlobalMembers._ID("DARK ROCK", 198);
							theBody = GlobalMembers._ID("This block can be destroyed only by blasting it with Special Gems.", 199);
							result = true;
							break;
						case 5:
							theHeader = GlobalMembers._ID("BOULDER", 200);
							theBody = GlobalMembers._ID("Match 4 Gems adjacent to this block to break it up.", 201);
							result = true;
							break;
						default:
							theHeader = GlobalMembers._ID("BOULDER", 202);
							theBody = GlobalMembers._ID("Match Multiple Gems adjacent to this block to break it up.", 203);
							result = true;
							break;
						}
					}
					break;
				case DigGoal.EDigPieceType.eDigPiece_Goal:
					if (tileData.mStrength <= 3)
					{
						theHeader = GlobalMembers._ID("GOLD", 186);
						theBody = GlobalMembers._ID("Dig up gold to score points.", 187);
						result = true;
					}
					else
					{
						theHeader = GlobalMembers._ID("DIAMONDS", 188);
						theBody = GlobalMembers._ID("Dig up diamonds to score points.", 189);
						result = true;
					}
					break;
				}
			}
			return result;
		}

		public void AddDustEffect(FPoint pt)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_SANDSTORM_DIG);
			particleEffect.mX = pt.mX;
			particleEffect.mY = pt.mY;
			particleEffect.mDoDrawTransform = true;
			this.mDustFXManager.AddEffect(particleEffect);
		}

		public new virtual bool IsGameIdle()
		{
			return base.IsGameIdle() && !this.mCvScrollY.IsDoingCurve();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DigGoal()
		{
			int[,] array = new int[2, 2];
			array[0, 0] = 1;
			array[1, 0] = -1;
			DigGoal.DrawGameElements_DrawGrassLR_neighbors = array;
			DigGoal.DrawGameElements_DrawGrassLR_images = new int[] { 1229, 1228 };
			DigGoal.DrawGameElements_neighbors = new int[,]
			{
				{ 1, 0 },
				{ -1, 0 },
				{ 0, 1 },
				{ 0, -1 }
			};
			DigGoal.DrawGameElements_images = new int[] { 1177, 1180, 1183, 1173 };
			DigGoal.DrawGameElements_imagesHighlights = new int[] { 1178, 1181, 1184, 1174 };
			DigGoal.DrawGameElements_imagesHighlightShadows = new int[] { 1179, 1182, -1, 1175 };
			DigGoal.DrawDigBarAnim_made = false;
			DigGoal.DrawDigBarAnim_cvClearAnimX = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvClearAnimScale = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvClearAnimAlpha = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvClearAnimColorCycle = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvAllClearAnimX = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvAllClearAnimScale = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvAllClearAnimAlpha = new CurvedVal();
			DigGoal.DrawDigBarAnim_cvAllClearAnimColorCycle = new CurvedVal();
			DigGoal.Update_neighbors = new int[,]
			{
				{ 1, 0 },
				{ -1, 0 },
				{ 0, 1 },
				{ 0, -1 }
			};
			DigGoal.TriggerDigPiece_goldCollectRect = new Rect(GlobalMembers.M(10), GlobalMembers.M(10), 100 - GlobalMembers.M(20), 100 - GlobalMembers.M(20));
		}

		public const int COIN_SIZE = 70;

		public const int TRIGGER_LINE_AT = 6;

		public const int GOLD_COLLECTION_COUNT = 3;

		public const int MAX_MINE_STRENGTH = 7;

		public const int MAX_BLOCKER_STRENGTH = 5;

		public const int DARK_ROCK_STRENGTH = 4;

		public const int INFINITE_GOLD_STRENGTH = 9;

		internal int[] ARTIFACT_OVERLAY_IDS = new int[] { 1284, 1285, 1286 };

		internal int[] ARTIFACT_UNDERLAY_IDS = new int[] { 1287, 1288, 1289 };

		public List<DigGoal.ArtifactData> mArtifacts = new List<DigGoal.ArtifactData>();

		public CurvedVal mArtifactPossRange = new CurvedVal();

		public List<int> mArtifactPoss = new List<int>();

		public List<int> mCollectedArtifacts = new List<int>();

		public List<int> mPlacedArtifacts = new List<int>();

		public List<int> mTreasureRangeMin = new List<int>();

		public List<int> mTreasureRangeMax = new List<int>();

		public List<List<bool>> mVisited = new List<List<bool>>();

		public Dictionary<int, DigGoal.TileData> mIdToTileData = new Dictionary<int, DigGoal.TileData>();

		public int mInitPushAnimPixels;

		public CurvedVal mInitPushAnimCv = new CurvedVal();

		public int mBlackrockTipPieceId;

		public bool mDefaultNeverAllowCascades;

		public bool mSkipMoveCreditIdCheck;

		public CurvedVal mDigBarFlash = new CurvedVal();

		public int mGoldValue;

		public int mGemValue;

		public int mArtifactBaseValue;

		public int mDigBarFlashCount;

		public int mTargetCount;

		public int mTextFlashTicks;

		public int[] mTreasureEarnings = new int[3];

		public int mArtifactMinTiles;

		public int mArtifactMaxTiles;

		public int mNextArtifactTileCount;

		public List<int> mStartArtifactRow = new List<int>();

		public List<DigGoal.CheckPiece> mCheckPieces = new List<DigGoal.CheckPiece>();

		public List<Piece> mMovingPieces = new List<Piece>();

		public List<int> mHypercubes = new List<int>();

		public DigGoal.OldPieceData[,] mOldPieceData = new DigGoal.OldPieceData[2, 8];

		public bool mAllowDescent;

		public double mGridDepth;

		public int mRandRowIdx;

		public int mDigCountPerScroll;

		public GridData mGridData = new GridData();

		public CurvedVal mCvScrollY = new CurvedVal();

		public CurvedVal mCvShakey = new CurvedVal();

		public bool mForceScroll;

		public int mBoardOffsetXExtra;

		public int mBoardOffsetYExtra;

		public string mTutorialHeader = string.Empty;

		public string mTutorialText = string.Empty;

		public int mClearedAnimAtTick;

		public int mAllClearedAnimAtTick;

		public bool mClearedAnimPlayed;

		public bool mAllClearAnimPlayed;

		public bool mFirstChestPt;

		public bool mInMegaDig;

		public bool mDrawScoreWidget;

		public List<GoldCollectEffect> mGoldFx = new List<GoldCollectEffect>();

		public Dictionary<int, int> mIdToBlastCount = new Dictionary<int, int>();

		public CurvedVal mCvDarkRockFreq = new CurvedVal();

		public CurvedVal mCvMinBrickStr = new CurvedVal();

		public CurvedVal mCvMaxBrickStr = new CurvedVal();

		public CurvedVal mCvEdgeBrickStrPerLevel = new CurvedVal();

		public CurvedVal mCvMinMineStr = new CurvedVal();

		public CurvedVal mCvMaxMineStr = new CurvedVal();

		public SpreadCurve mBrickStrSpread = new SpreadCurve(500);

		public SpreadCurve mArtifactSpread = new SpreadCurve(500);

		public SpreadCurve mMineStrSpread = new SpreadCurve(500);

		public int mGoldRushTipPieceId;

		public int mNextBottomHypermixerWait;

		public int mArtifactSkipTileCount;

		public int mPowerGemThresholdDepth0;

		public int mPowerGemThresholdDepth20;

		public int mPowerGemThresholdDepth40;

		public CurvedVal mCvMineProb = new CurvedVal();

		public bool mWantTopFrame;

		public List<int> mArtifactVals = new List<int>();

		public EffectsManager mGoldPointsFXManager;

		public EffectsManager mDustFXManager;

		public EffectsManager mMessageFXManager;

		public bool mUsingRandomizers;

		public float mGameTicksF;

		public bool mLoadingGame;

		public bool mDigInProgress;

		private int[,] IsSpecialPieceUnlocked_neighbors = new int[,]
		{
			{ 1, 0 },
			{ -1, 0 },
			{ 0, 1 },
			{ 0, -1 }
		};

		private static DigGoal.MyPiece[] DP_pHypercubes = new DigGoal.MyPiece[32];

		private static DigGoal.MyPiece[] DP_pSpecial = new DigGoal.MyPiece[32];

		private static DigGoal.MyPiece[] DP_pGoals = new DigGoal.MyPiece[32];

		private static DigGoal.MyPiece[] DP_pBlocks = new DigGoal.MyPiece[32];

		private static DigGoal.MyPiece[] DP_pArtifacts = new DigGoal.MyPiece[32];

		private static int[,] DrawGameElements_DrawGrassLR_neighbors;

		private static int[] DrawGameElements_DrawGrassLR_images;

		private static int[,] DrawGameElements_neighbors;

		private static int[] DrawGameElements_images;

		private static int[] DrawGameElements_imagesHighlights;

		private static int[] DrawGameElements_imagesHighlightShadows;

		private static bool DrawDigBarAnim_made;

		private static CurvedVal DrawDigBarAnim_cvClearAnimX;

		private static CurvedVal DrawDigBarAnim_cvClearAnimScale;

		private static CurvedVal DrawDigBarAnim_cvClearAnimAlpha;

		private static CurvedVal DrawDigBarAnim_cvClearAnimColorCycle;

		private static CurvedVal DrawDigBarAnim_cvAllClearAnimX;

		private static CurvedVal DrawDigBarAnim_cvAllClearAnimScale;

		private static CurvedVal DrawDigBarAnim_cvAllClearAnimAlpha;

		private static CurvedVal DrawDigBarAnim_cvAllClearAnimColorCycle;

		private static int[,] Update_neighbors;

		private static Rect TriggerDigPiece_goldCollectRect;

		public enum EDigPieceType
		{
			eDigPiece_Artifact,
			eDigPiece_Block,
			eDigPiece_Goal,
			eDigPiece_COUNT
		}

		public enum DrawPiecesPass
		{
			DrawNormal,
			DrawAdditive,
			COUNT
		}

		public enum DrawPiecePrePass
		{
			DrawNormal,
			DrawAdditive,
			DrawShadow,
			COUNT
		}

		public enum DrawGameElementsPass
		{
			DrawCenter,
			DrawGrassTop,
			DrawEdgeShadows,
			DrawEdges,
			DrawGrassLR,
			COUNT
		}

		public class TileData
		{
			public TileData()
			{
				this.mIsDeleting = false;
				this.mStartStrength = 1;
				this.mPieceType = DigGoal.EDigPieceType.eDigPiece_Goal;
				this.mRandCfg = 0U;
				this.mBlingPIFx = null;
				this.mGoldInnerFx = null;
				this.mIsTopTile = false;
				this.mDiamondFx = null;
				this.mStrength = new Ref<int>(1);
				this.mArtifactId = this.mStrength;
				this.mGoldOrDiamondValue = this.mStrength;
			}

			public TileData(DigGoal.TileData rhs)
			{
				this.mIsDeleting = rhs.mIsDeleting;
				this.mStartStrength = rhs.mStartStrength;
				this.mPieceType = rhs.mPieceType;
				this.mRandCfg = rhs.mRandCfg;
				this.mBlingPIFx = rhs.mBlingPIFx;
				this.mGoldInnerFx = rhs.mGoldInnerFx;
				this.mIsTopTile = rhs.mIsTopTile;
				this.mDiamondFx = rhs.mDiamondFx;
				this.mStrength = new Ref<int>(rhs.mStrength.value);
				this.mArtifactId = this.mStrength;
				this.mGoldOrDiamondValue = this.mStrength;
			}

			public void SetAs(DigGoal.EDigPieceType theType, int theStr, Piece thePiece, DigGoal theGoal)
			{
				if (this.mDiamondFx != null)
				{
					this.mDiamondFx.Dispose();
				}
				this.mDiamondFx = null;
				if (this.mBlingPIFx != null)
				{
					this.mBlingPIFx.mDeleteMe = true;
					if (this.mBlingPIFx.mRefCount > 0)
					{
						this.mBlingPIFx.mRefCount--;
					}
					this.mBlingPIFx = null;
				}
				if (this.mGoldInnerFx != null)
				{
					this.mGoldInnerFx.Dispose();
					this.mGoldInnerFx = null;
				}
				this.mPieceType = theType;
				this.mStrength.value = theStr;
				this.mStartStrength = this.mStrength;
				this.mRandCfg = (uint)BejeweledLivePlus.Misc.Common.Rand();
				if (this.mPieceType == DigGoal.EDigPieceType.eDigPiece_Goal)
				{
					bool flag = this.IsDiamond();
					if (flag)
					{
						this.mDiamondFx = new DiamondEffect(Math.Min(4, this.mStrength.value - 3));
					}
					this.mBlingPIFx = BlingParticleEffect.fromPIEffectAndPieceId(flag ? GlobalMembersResourcesWP.PIEFFECT_DIAMOND_SPARKLES : GlobalMembersResourcesWP.PIEFFECT_GOLD_BLING, thePiece.mId);
					this.mBlingPIFx.mDoDrawTransform = true;
					this.mBlingPIFx.mX = thePiece.CX();
					this.mBlingPIFx.mY = thePiece.CY();
					this.mBlingPIFx.mPieceId = thePiece.mId;
					this.mBlingPIFx.mRefCount++;
					theGoal.mQuestBoard.mPostFXManager.AddEffect(this.mBlingPIFx);
					return;
				}
				if (this.mPieceType == DigGoal.EDigPieceType.eDigPiece_Block)
				{
					if (this.mStrength == 4 && theGoal.mBlackrockTipPieceId == -1)
					{
						theGoal.mBlackrockTipPieceId = thePiece.mId;
						return;
					}
				}
				else if (this.mPieceType == DigGoal.EDigPieceType.eDigPiece_Artifact)
				{
					if (theGoal.mArtifacts.Count > 0)
					{
						this.mStrength.value = Math.Max(0, Math.Min(theGoal.mArtifacts.Count - 1, this.mStrength));
					}
					this.mStartStrength = this.mStrength;
				}
			}

			public bool IsDiamond()
			{
				return this.mGoldOrDiamondValue > 3;
			}

			public Ref<int> mStrength;

			public Ref<int> mArtifactId;

			public Ref<int> mGoldOrDiamondValue;

			public int mStartStrength;

			public DigGoal.EDigPieceType mPieceType;

			public uint mRandCfg;

			public BlingParticleEffect mBlingPIFx;

			public PIEffect mGoldInnerFx;

			public bool mIsTopTile;

			public bool mIsDeleting;

			public DiamondEffect mDiamondFx;
		}

		public class ArtifactData
		{
			public string mId;

			public string mName = string.Empty;

			public int mMinDepth;

			public int mValue;

			public int mImageId;

			public int mUnderlayImgId;

			public int mOverlayImgId;
		}

		public class OldPieceData
		{
			public int mFlags;

			public int mColor;
		}

		public class CheckPiece
		{
			public CheckPiece(int col, int row, bool isHyperCube, int moveCreditId)
			{
				this.mCol = col;
				this.mRow = row;
				this.mIsHyperCube = isHyperCube;
				this.mMoveCreditId = moveCreditId;
			}

			public int mCol;

			public int mRow;

			public bool mIsHyperCube;

			public int mMoveCreditId;
		}

		private struct MyPiece
		{
			public Piece piece;

			public DigGoal.TileData tile;
		}
	}
}
