using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Sexy;

namespace Bejeweled3
{
	public class LightningStorm
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 10; i++)
			{
				new LightningStorm().PrepareForReuse();
			}
		}

		private LightningStorm()
		{
		}

		public static LightningStorm GetNewLightningStorm(BoardBejLive theBoard, PieceBejLive thePiece, LightningStorm.StormType theType)
		{
			if (LightningStorm.unusedObjects.Count > 0)
			{
				LightningStorm lightningStorm = LightningStorm.unusedObjects.Pop();
				lightningStorm.Reset(theBoard, thePiece, theType);
				return lightningStorm;
			}
			return new LightningStorm(theBoard, thePiece, theType);
		}

		public void PrepareForReuse()
		{
			for (int i = 0; i < this.mZaps.Count; i++)
			{
				this.mZaps[i].PrepareForReuse();
			}
			this.mZaps.Clear();
			this.mLightningVector.Clear();
			this.mPieceIds.Clear();
			LightningStorm.unusedObjects.Push(this);
		}

		public void Reset(BoardBejLive theBoard, PieceBejLive thePiece, LightningStorm.StormType theType)
		{
			this.mBoard = theBoard;
			this.mUpdateCnt = 0;
			this.mLightningCount = 1;
			this.mGemAlpha = 1f;
			this.mDoneDelay = 0;
			this.mMatchType = thePiece.mColor;
			this.mPieceIds.Add(thePiece.mId);
			this.miCol = thePiece.mCol;
			this.miRow = thePiece.mRow;
			this.mLandscaped = theBoard.mApp.IsLandscape();
			this.mMoveCreditId = thePiece.mMoveCreditId;
			this.mMatchId = thePiece.mMatchId;
			this.mExplodeTimer = 0f;
			if (thePiece.IsFlagSet(1))
			{
				this.mNovaScale.SetCurve(CurvedValDefinition.mNovaScaleCurve);
				this.mNovaAlpha.SetCurve(CurvedValDefinition.mNovaAlphaCurve);
				this.mNukeScale.SetCurve(CurvedValDefinition.mNukeScaleCurve);
				this.mNukeAlpha.SetCurve(CurvedValDefinition.mNukeAlphaCurve);
				this.mStormType = LightningStorm.StormType.STORM_FLAMING;
			}
			else
			{
				this.mStormType = theType;
			}
			this.mElectrocuterId = thePiece.mId;
			this.mCX = (int)thePiece.CX();
			this.mCY = (int)thePiece.CY();
			thePiece.mIsElectrocuting = true;
			if (this.mStormType != LightningStorm.StormType.STORM_HYPERGEM)
			{
				thePiece.mElectrocutePercent = 0.9f;
			}
			this.mColor = ((theType == LightningStorm.StormType.STORM_BOTH || theType == LightningStorm.StormType.STORM_STAR) ? thePiece.mColor : (-1));
			this.mTimer = 0f;
			this.mDist = 0;
			this.mHoldDelay = 1f;
			this.mStormLength = (int)((this.mStormType == LightningStorm.StormType.STORM_SHORT) ? Constants.mConstants.M(3f) : Constants.mConstants.M(7f));
			if (this.mStormType != LightningStorm.StormType.STORM_HYPERGEM)
			{
				for (int i = ((this.mStormType == LightningStorm.StormType.STORM_FLAMING) ? (-1) : 0); i <= ((this.mStormType == LightningStorm.StormType.STORM_FLAMING) ? 1 : 0); i++)
				{
					int rowAt = this.mBoard.GetRowAt((int)(thePiece.mY + (float)(GameConstants.GEM_HEIGHT / 2) + (float)(i * GameConstants.GEM_HEIGHT)));
					if (rowAt >= 0 && rowAt < 8 && this.mStormType != LightningStorm.StormType.STORM_VERT)
					{
						LightningZap newLightningZap = LightningZap.GetNewLightningZap(this.mBoard, Math.Max(this.mBoard.GetColX(0), this.mCX - this.mStormLength * GameConstants.GEM_WIDTH - GameConstants.GEM_WIDTH / 2), (int)(thePiece.mY + (float)(GameConstants.GEM_HEIGHT / 2) + (float)(i * GameConstants.GEM_HEIGHT)), Math.Min(this.mBoard.GetColX(8), this.mCX + this.mStormLength * GameConstants.GEM_WIDTH + GameConstants.GEM_WIDTH / 2), (int)(thePiece.mY + (float)(GameConstants.GEM_HEIGHT / 2) + (float)(i * GameConstants.GEM_HEIGHT)), LightningZap.gElectColors[this.mMatchType], Constants.mConstants.M(10f), this.mStormType == LightningStorm.StormType.STORM_FLAMING);
						this.mZaps.Add(newLightningZap);
					}
					int colAt = this.mBoard.GetColAt((int)(thePiece.mX + (float)(GameConstants.GEM_WIDTH / 2) + (float)(i * GameConstants.GEM_WIDTH)));
					if (colAt >= 0 && colAt < 8 && this.mStormType != LightningStorm.StormType.STORM_HORZ)
					{
						LightningZap newLightningZap2 = LightningZap.GetNewLightningZap(this.mBoard, (int)(thePiece.mX + (float)(GameConstants.GEM_WIDTH / 2) + (float)(i * GameConstants.GEM_WIDTH)), Math.Max(this.mBoard.GetRowY(0), this.mCY - this.mStormLength * GameConstants.GEM_HEIGHT - GameConstants.GEM_HEIGHT / 2), (int)(thePiece.mX + (float)(GameConstants.GEM_WIDTH / 2) + (float)(i * GameConstants.GEM_WIDTH)), Math.Min(this.mBoard.GetRowY(8), this.mCY + this.mStormLength * GameConstants.GEM_HEIGHT + GameConstants.GEM_HEIGHT / 2), LightningZap.gElectColors[this.mMatchType], Constants.mConstants.M(10f), this.mStormType == LightningStorm.StormType.STORM_FLAMING);
						this.mZaps.Add(newLightningZap2);
					}
				}
				this.mLightingAlpha.SetCurve(CurvedValDefinition.lightningZapAlphaCurve);
			}
		}

		private LightningStorm(BoardBejLive theBoard, PieceBejLive thePiece)
			: this(theBoard, thePiece, LightningStorm.StormType.STORM_BOTH)
		{
		}

		private LightningStorm(BoardBejLive theBoard, PieceBejLive thePiece, LightningStorm.StormType theType)
		{
			this.Reset(theBoard, thePiece, theType);
		}

		public void AddLightning(int theStartX, int theStartY, int theEndX, int theEndY)
		{
			Lightning newLightning = Lightning.GetNewLightning();
			newLightning.mPercentDone = 0f;
			float num = (float)(theEndY - theStartY);
			float num2 = (float)(theEndX - theStartX);
			float num3 = (float)Math.Atan2((double)num, (double)num2);
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num * num));
			newLightning.mPullX = (float)Math.Cos((double)(num3 - 1.570795f)) * num4 * 0.4f;
			newLightning.mPullY = (float)Math.Sin((double)(num3 - 1.570795f)) * num4 * 0.4f;
			for (int i = 0; i < 8; i++)
			{
				float num5 = (float)i / 7f;
				float mX = (float)theStartX * (1f - num5) + (float)theEndX * num5;
				float mY = (float)theStartY * (1f - num5) + (float)theEndY * num5;
				newLightning.mPoints[i, 0].mX = mX;
				newLightning.mPoints[i, 0].mY = mY;
				newLightning.mPoints[i, 1].mX = mX;
				newLightning.mPoints[i, 1].mY = mY;
			}
			this.mLightningVector.Add(newLightning);
		}

		public void UpdateHyperGemLightning()
		{
			if (this.mDoneDelay > 0)
			{
				return;
			}
			if (this.mLandscaped != this.mBoard.mApp.IsLandscape())
			{
				for (int i = 0; i < this.mLightningVector.Count; i++)
				{
					this.mLightningVector[i].PrepareForReuse();
				}
				this.mLightningVector.Clear();
				this.mLightningCount = 0;
				this.mLandscaped = this.mBoard.mApp.IsLandscape();
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < this.anElectrocutedPieces.Length; j++)
			{
				this.anElectrocutedPieces[j] = null;
			}
			for (int k = 0; k < this.anElectrocuterPieces.Length; k++)
			{
				this.anElectrocuterPieces[k] = null;
			}
			for (int l = 0; l < this.aMatchingPieces.Length; l++)
			{
				this.aMatchingPieces[l] = null;
			}
			bool flag = false;
			bool flag2 = false;
			for (int m = 0; m < 8; m++)
			{
				for (int n = 0; n < 8; n++)
				{
					PieceBejLive pieceBejLive = this.mBoard.mBoard[m, n];
					if (pieceBejLive != null)
					{
						if (pieceBejLive.mExplodeDelay > 0)
						{
							flag2 = true;
						}
						if (pieceBejLive.mIsElectrocuting)
						{
							if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
							{
								pieceBejLive.mElectrocutePercent += Constants.mConstants.M(0.01f);
							}
							else
							{
								pieceBejLive.mElectrocutePercent += 0.015f;
							}
							if (pieceBejLive.mElectrocutePercent > 1f)
							{
								this.mBoard.SetMoveCredit(pieceBejLive, this.mMoveCreditId);
								if (!this.mBoard.TriggerSpecial(pieceBejLive, this.mBoard.GetPieceById(this.mElectrocuterId), this.mColor))
								{
									pieceBejLive.mExplodeDelay = 1;
								}
							}
							else
							{
								if ((double)pieceBejLive.mElectrocutePercent < 0.04)
								{
									this.anElectrocuterPieces[num2++] = pieceBejLive;
								}
								this.anElectrocutedPieces[num++] = pieceBejLive;
							}
						}
						else if ((pieceBejLive.mColor == this.mColor || this.mColor == -1) && pieceBejLive.mY > (float)(-(float)GameConstants.GEM_HEIGHT))
						{
							this.aMatchingPieces[num3++] = pieceBejLive;
						}
					}
				}
			}
			if (this.mColor != -1)
			{
				ReportAchievement.ReportHyperBlast(num3);
			}
			int num4 = 20 / (num + 1) + 5;
			if (this.mColor == -1)
			{
				num4 = (int)((float)num4 / Constants.mConstants.M(2f));
			}
			if (num3 > 0 && (this.mLightningVector.Count == 0 || this.mBoard.mRand.Next() % num4 == 0))
			{
				PieceBejLive pieceBejLive2 = null;
				PieceBejLive pieceBejLive3 = null;
				if (num2 > 0)
				{
					pieceBejLive3 = this.anElectrocuterPieces[this.mBoard.mRand.Next() % num2];
				}
				else if (num > 0)
				{
					pieceBejLive3 = this.anElectrocutedPieces[this.mBoard.mRand.Next() % num];
				}
				if (pieceBejLive3 != null)
				{
					int maxValue = int.MaxValue;
					for (int num5 = 0; num5 < num3; num5++)
					{
						PieceBejLive pieceBejLive4 = this.aMatchingPieces[num5];
						int num6 = Math.Min(Math.Abs(pieceBejLive4.mCol - pieceBejLive3.mCol), Math.Abs(pieceBejLive4.mRow - pieceBejLive3.mRow));
						if (num6 < maxValue)
						{
							pieceBejLive2 = pieceBejLive4;
						}
					}
					this.AddLightning((int)pieceBejLive2.mX + GameConstants.GEM_WIDTH / 2, (int)pieceBejLive2.mY + GameConstants.GEM_HEIGHT / 2, (int)pieceBejLive3.mX + GameConstants.GEM_WIDTH / 2, (int)pieceBejLive3.mY + GameConstants.GEM_HEIGHT / 2);
					this.mBoard.mApp.PlaySample(Resources.SOUND_ELECTRO_PATH);
				}
				else
				{
					pieceBejLive2 = this.aMatchingPieces[this.mBoard.mRand.Next() % num3];
				}
				pieceBejLive2.mIsElectrocuting = true;
			}
			for (int num7 = 0; num7 < this.mLightningVector.Count; num7++)
			{
				Lightning lightning = this.mLightningVector[num7];
				lightning.mPercentDone += 0.012f;
				if (lightning.mPercentDone > 1f)
				{
					this.mLightningVector[num7].PrepareForReuse();
					this.mLightningVector.RemoveAt(num7);
					num7--;
				}
				else
				{
					float num8 = Math.Max(0f, 1f - (1f - lightning.mPercentDone) * 3f);
					if (this.mUpdateCnt % 4 == 0)
					{
						float mX = lightning.mPoints[0, 0].mX;
						float mY = lightning.mPoints[0, 0].mY;
						float mX2 = lightning.mPoints[7, 0].mX;
						float mY2 = lightning.mPoints[7, 0].mY;
						for (int num9 = 0; num9 < 8; num9++)
						{
							float num10 = (float)num9 / 7f;
							float num11 = 1f - Math.Abs(1f - num10 * 2f);
							float num12 = mX * (1f - num10) + mX2 * num10 + num11 * (Board.GetRandFloat() * 40f + num8 * lightning.mPullX);
							float num13 = mY * (1f - num10) + mY2 * num10 + num11 * (Board.GetRandFloat() * 40f + num8 * lightning.mPullY);
							if (num9 == 0 || num9 == 7)
							{
								lightning.mPoints[num9, 0].mX = num12;
								lightning.mPoints[num9, 0].mY = num13;
								lightning.mPoints[num9, 1].mX = num12;
								lightning.mPoints[num9, 1].mY = num13;
							}
							else
							{
								float num14 = Constants.mConstants.S(100f);
								lightning.mPoints[num9, 0].mX = num12 + Board.GetRandFloat() * num14;
								lightning.mPoints[num9, 0].mY = num13 + Board.GetRandFloat() * num14;
								lightning.mPoints[num9, 1].mX = num12 + Board.GetRandFloat() * num14;
								lightning.mPoints[num9, 1].mY = num13 + Board.GetRandFloat() * num14;
							}
						}
					}
				}
			}
			if (!flag2 && num == 0 && num3 == 0 && this.mLightningVector.Count == 0 && !flag)
			{
				for (int num15 = 0; num15 < 8; num15++)
				{
					for (int num16 = 0; num16 < 8; num16++)
					{
						PieceBejLive pieceBejLive5 = this.mBoard.mBoard[num15, num16];
						if (pieceBejLive5 != null)
						{
							pieceBejLive5.mFallVelocity = 0f;
						}
					}
				}
				this.mDoneDelay = 50;
			}
		}

		public void UpdateLaserGemLightning()
		{
			if (this.mDoneDelay > 0)
			{
				return;
			}
			if (this.mLandscaped != this.mBoard.mApp.IsLandscape())
			{
				for (int i = 0; i < this.mLightningVector.Count; i++)
				{
					this.mLightningVector[i].PrepareForReuse();
				}
				this.mLightningVector.Clear();
				this.mLightningCount = 0;
				this.mLandscaped = this.mBoard.mApp.IsLandscape();
				this.mCX = this.mBoard.GetColX(this.miCol) + (GameConstants.GEM_WIDTH >> 1);
				this.mCY = this.mBoard.GetRowY(this.miRow) + (GameConstants.GEM_WIDTH >> 1);
			}
			int lightningZap_HorizontalZap_Start_X = Constants.mConstants.LightningZap_HorizontalZap_Start_X;
			int num = this.mCY - (GameConstants.GEM_WIDTH >> 1);
			int lightningZap_HorizontalZap_End_X = Constants.mConstants.LightningZap_HorizontalZap_End_X;
			int num2 = this.mCY - (GameConstants.GEM_WIDTH >> 1);
			if (this.mLightningCount <= 8)
			{
				this.AddLightning((int)((float)lightningZap_HorizontalZap_Start_X - Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_2), (int)((float)(num + GameConstants.GEM_WIDTH / 2) + Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_1), (int)((float)(lightningZap_HorizontalZap_End_X + GameConstants.GEM_WIDTH) + Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_2), (int)((float)(num2 + GameConstants.GEM_WIDTH / 2) - Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_1));
				this.mLightningCount++;
			}
			int num3 = this.mCX - (GameConstants.GEM_WIDTH >> 1);
			int lightningZap_VerticalZap_Start_Y = Constants.mConstants.LightningZap_VerticalZap_Start_Y;
			int num4 = this.mCX - (GameConstants.GEM_WIDTH >> 1);
			int lightningZap_VerticalZap_End_Y = Constants.mConstants.LightningZap_VerticalZap_End_Y;
			if (this.mLightningCount <= 8)
			{
				this.AddLightning((int)((float)(num3 + GameConstants.GEM_WIDTH / 2) + Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_1), (int)((float)lightningZap_VerticalZap_Start_Y - Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_2), (int)((float)(num4 + GameConstants.GEM_WIDTH / 2) - Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_1), (int)((float)(lightningZap_VerticalZap_End_Y + GameConstants.GEM_WIDTH) + Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_2));
				this.mLightningCount++;
			}
			for (int j = 0; j < this.mLightningVector.Count; j++)
			{
				Lightning lightning = this.mLightningVector[j];
				lightning.mPercentDone += 0.012f;
				if (lightning.mPercentDone > 1f)
				{
					this.mLightningVector[j].PrepareForReuse();
					this.mLightningVector.RemoveAt(j);
					j--;
				}
				else if (this.mUpdateCnt % 4 == 0)
				{
					float mX = lightning.mPoints[0, 0].mX;
					float mY = lightning.mPoints[0, 0].mY;
					float mX2 = lightning.mPoints[7, 0].mX;
					float mY2 = lightning.mPoints[7, 0].mY;
					for (int k = 0; k < 8; k++)
					{
						float num5 = (float)k / 7f;
						float num6 = 1f - Math.Abs(1f - num5 * 2f);
						float num7 = mX * (1f - num5) + mX2 * num5 + num6 * (Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_1);
						float num8 = mY * (1f - num5) + mY2 * num5 + num6 * (Board.GetRandFloat() * Constants.mConstants.BoardBej3_LaserGem_MaxOffset_1);
						if (k == 0 || k == 7)
						{
							lightning.mPoints[k, 0].mX = num7;
							lightning.mPoints[k, 0].mY = num8;
							lightning.mPoints[k, 1].mX = num7;
							lightning.mPoints[k, 1].mY = num8;
						}
						else
						{
							float num9 = Constants.mConstants.S(10f);
							lightning.mPoints[k, 0].mX = num7 + Board.GetRandFloat() * num9;
							lightning.mPoints[k, 0].mY = num8 + Board.GetRandFloat() * num9;
							lightning.mPoints[k, 1].mX = num7 + Board.GetRandFloat() * num9;
							lightning.mPoints[k, 1].mY = num8 + Board.GetRandFloat() * num9;
						}
					}
				}
			}
		}

		public void DrawLightning(Graphics g)
		{
			SexyColor aColor = new SexyColor(0, 0, 0, 0);
			if (this.mLightningVector.Count > 0)
			{
				g.BeginPolyFill();
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			}
			SexyColor sexyColor;
			if (this.mColor == -1)
			{
				sexyColor = new SexyColor(LightningZap.gElectColors[Board.Rand(LightningZap.gElectColors.Length - 1)].mRed, LightningZap.gElectColors[Board.Rand(LightningZap.gElectColors.Length - 1)].mGreen, LightningZap.gElectColors[Board.Rand(LightningZap.gElectColors.Length - 1)].mBlue);
			}
			else
			{
				sexyColor = LightningZap.gElectColors[this.mColor];
			}
			for (int i = 0; i < this.mLightningVector.Count; i++)
			{
				Lightning lightning = this.mLightningVector[i];
				float num = Math.Min((1f - lightning.mPercentDone) * 8f, 1f);
				int num2 = (int)((double)num * 255.0);
				SexyColor aColor2 = new SexyColor(num2, num2, num2);
				SexyColor aColor3;
				if (this.mColor == -1)
				{
					aColor3 = new SexyColor((int)((float)sexyColor.mRed * num), (int)((float)sexyColor.mGreen * num), (int)((float)sexyColor.mBlue * num));
				}
				else
				{
					aColor3 = new SexyColor((int)((float)sexyColor.mRed * num), (int)((float)sexyColor.mGreen * num), (int)((float)sexyColor.mBlue * num));
				}
				for (int j = 0; j < 7; j++)
				{
					TPointFloat tpointFloat = lightning.mPoints[j, 0];
					TPointFloat tpointFloat2 = lightning.mPoints[j, 1];
					TPointFloat tpointFloat3 = lightning.mPoints[j + 1, 0];
					TPointFloat tpointFloat4 = lightning.mPoints[j + 1, 1];
					float num3 = tpointFloat.mX * 0.6f + tpointFloat2.mX * 0.399999976f;
					float num4 = tpointFloat.mY * 0.6f + tpointFloat2.mY * 0.399999976f;
					float num5 = tpointFloat2.mX * 0.6f + tpointFloat.mX * 0.399999976f;
					float num6 = tpointFloat2.mY * 0.6f + tpointFloat.mY * 0.399999976f;
					float num7 = tpointFloat3.mX * 0.6f + tpointFloat4.mX * 0.399999976f;
					float num8 = tpointFloat3.mY * 0.6f + tpointFloat4.mY * 0.399999976f;
					float num9 = tpointFloat4.mX * 0.6f + tpointFloat3.mX * 0.399999976f;
					float num10 = tpointFloat4.mY * 0.6f + tpointFloat3.mY * 0.399999976f;
					float num11 = (num3 + num5) / 2f;
					float num12 = (num7 + num9) / 2f;
					float num13 = (num4 + num6) / 2f;
					float num14 = (num8 + num10) / 2f;
					g.SetColor(aColor);
					this.pointBuffer[0].mX = (int)tpointFloat.mX;
					this.pointBuffer[0].mY = (int)tpointFloat.mY;
					this.pointBuffer[1].mX = (int)tpointFloat3.mX;
					this.pointBuffer[1].mY = (int)tpointFloat3.mY;
					g.PolyFill(this.pointBuffer, 2);
					g.SetColor(aColor3);
					this.pointBuffer[0].mX = (int)num3;
					this.pointBuffer[0].mY = (int)num4;
					this.pointBuffer[1].mX = (int)num3;
					this.pointBuffer[1].mY = (int)num4;
					this.pointBuffer[2].mX = (int)num7;
					this.pointBuffer[2].mY = (int)num8;
					g.PolyFill(this.pointBuffer, 3);
					g.SetColor(aColor);
					this.pointBuffer[0].mX = (int)tpointFloat3.mX;
					this.pointBuffer[0].mY = (int)tpointFloat3.mY;
					g.PolyFill(this.pointBuffer, 1);
					g.SetColor(aColor3);
					this.pointBuffer[0].mX = (int)num3;
					this.pointBuffer[0].mY = (int)num4;
					this.pointBuffer[1].mX = (int)num7;
					this.pointBuffer[1].mY = (int)num8;
					g.PolyFill(this.pointBuffer, 2);
					g.SetColor(aColor2);
					this.pointBuffer[0].mX = (int)num11;
					this.pointBuffer[0].mY = (int)num13;
					this.pointBuffer[1].mX = (int)num11;
					this.pointBuffer[1].mY = (int)num13;
					this.pointBuffer[2].mX = (int)num12;
					this.pointBuffer[2].mY = (int)num14;
					g.PolyFill(this.pointBuffer, 3);
					g.SetColor(aColor3);
					this.pointBuffer[0].mX = (int)num7;
					this.pointBuffer[0].mY = (int)num8;
					g.PolyFill(this.pointBuffer, 1);
					g.SetColor(aColor2);
					this.pointBuffer[0].mX = (int)num11;
					this.pointBuffer[0].mY = (int)num13;
					this.pointBuffer[1].mX = (int)num12;
					this.pointBuffer[1].mY = (int)num14;
					g.PolyFill(this.pointBuffer, 2);
					g.SetColor(aColor3);
					this.pointBuffer[0].mX = (int)num5;
					this.pointBuffer[0].mY = (int)num6;
					this.pointBuffer[1].mX = (int)num5;
					this.pointBuffer[1].mY = (int)num6;
					this.pointBuffer[2].mX = (int)num9;
					this.pointBuffer[2].mY = (int)num10;
					g.PolyFill(this.pointBuffer, 3);
					g.SetColor(aColor2);
					this.pointBuffer[0].mX = (int)num12;
					this.pointBuffer[0].mY = (int)num14;
					g.PolyFill(this.pointBuffer, 1);
					g.SetColor(aColor3);
					this.pointBuffer[0].mX = (int)num5;
					this.pointBuffer[0].mY = (int)num6;
					this.pointBuffer[1].mX = (int)num9;
					this.pointBuffer[1].mY = (int)num10;
					g.PolyFill(this.pointBuffer, 2);
					g.SetColor(aColor);
					this.pointBuffer[0].mX = (int)tpointFloat2.mX;
					this.pointBuffer[0].mY = (int)tpointFloat2.mY;
					this.pointBuffer[1].mX = (int)tpointFloat2.mX;
					this.pointBuffer[1].mY = (int)tpointFloat2.mY;
					this.pointBuffer[2].mX = (int)tpointFloat4.mX;
					this.pointBuffer[2].mY = (int)tpointFloat4.mY;
					g.PolyFill(this.pointBuffer, 3);
					g.SetColor(aColor3);
					this.pointBuffer[0].mX = (int)num9;
					this.pointBuffer[0].mY = (int)num10;
					g.PolyFill(this.pointBuffer, 1);
				}
			}
			if (this.mLightningVector.Count > 0)
			{
				g.EndPolyFill();
			}
		}

		public void Update()
		{
			this.mUpdateCnt++;
			this.mNovaScale.IncInVal();
			this.mNukeScale.IncInVal();
			if (this.mStormType == LightningStorm.StormType.STORM_FLAMING && this.mNukeScale.CheckInThreshold((double)Constants.mConstants.M(1.6f)))
			{
				this.mBoard.mApp.PlaySample(Resources.SOUND_BOMB_EXPLODE, 0);
			}
			for (int i = 0; i < this.mZaps.Count; i++)
			{
				LightningZap lightningZap = this.mZaps[i];
				lightningZap.Update();
				if (lightningZap.mDeleteMe)
				{
					lightningZap.PrepareForReuse();
					this.mZaps.RemoveAt(i);
					i--;
				}
			}
			this.mGemAlpha = Math.Max(0f, this.mGemAlpha - Constants.mConstants.M(0.01f));
			if (this.mStormType == LightningStorm.StormType.STORM_HYPERGEM)
			{
				this.UpdateHyperGemLightning();
			}
		}

		public void Draw(Graphics g)
		{
			for (int i = 0; i < this.mZaps.Count; i++)
			{
				this.mZaps[i].Draw(g);
			}
			if (this.mStormType == LightningStorm.StormType.STORM_HYPERGEM)
			{
				this.DrawLightning(g);
			}
		}

		private const int HYPERCUBE_LIGHTNING_WIDTH = 100;

		private const float HYPERCUBE_LIGHTNING_WHITE_PERCENT = 0.6f;

		public BoardBejLive mBoard;

		public int mCX;

		public int mCY;

		public int miCol;

		public int miRow;

		public int mUpdateCnt;

		public int mColor;

		public int mStormLength;

		public int mMatchType;

		public int mLightningCount;

		public LightningStorm.StormType mStormType;

		public float mExplodeTimer;

		public float mHoldDelay;

		public int mElectrocuterId;

		public int mMoveCreditId;

		public int mMatchId;

		public int mDist;

		public float mTimer;

		public List<LightningZap> mZaps = new List<LightningZap>();

		public List<Lightning> mLightningVector = new List<Lightning>();

		public List<int> mPieceIds = new List<int>();

		public CurvedVal mNovaScale = CurvedVal.GetNewCurvedVal();

		public CurvedVal mNovaAlpha = CurvedVal.GetNewCurvedVal();

		public CurvedVal mNukeScale = CurvedVal.GetNewCurvedVal();

		public CurvedVal mNukeAlpha = CurvedVal.GetNewCurvedVal();

		public CurvedVal mLightingAlpha = CurvedVal.GetNewCurvedVal();

		public float mGemAlpha;

		public int mDoneDelay;

		public bool mLandscaped;

		private static Stack<LightningStorm> unusedObjects = new Stack<LightningStorm>();

		private PieceBejLive[] anElectrocutedPieces = new PieceBejLive[64];

		private PieceBejLive[] anElectrocuterPieces = new PieceBejLive[64];

		private PieceBejLive[] aMatchingPieces = new PieceBejLive[64];

		private TPoint[] pointBuffer = new TPoint[3];

		public enum StormType
		{
			STORM_HORZ,
			STORM_VERT,
			STORM_BOTH,
			STORM_SHORT,
			STORM_STAR,
			STORM_SCREEN,
			STORM_FLAMING,
			STORM_HYPERGEM
		}
	}
}
