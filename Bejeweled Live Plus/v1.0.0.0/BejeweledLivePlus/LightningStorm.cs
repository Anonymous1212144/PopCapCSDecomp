using System;
using System.Collections.Generic;
using System.Linq;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class LightningStorm : IDisposable
	{
		public LightningStorm(Board theBoard, Piece thePiece)
		{
			this.Init(theBoard, thePiece, 2);
		}

		public LightningStorm(Board theBoard, Piece thePiece, int theType)
		{
			this.Init(theBoard, thePiece, theType);
		}

		public void Init(Board theBoard, Piece thePiece, int theType)
		{
			this.mBoard = theBoard;
			this.mUpdateCnt = 0;
			this.mLightningCount = 1;
			this.mGemAlpha = 1f;
			this.mMatchType = thePiece.mColor;
			this.mPieceIds.Add(thePiece.mId);
			this.mDoneDelay = 0;
			this.mLastElectroSound = 0;
			this.mStartPieceFlags = thePiece.mFlags;
			this.mMoveCreditId = thePiece.mMoveCreditId;
			this.mMatchId = thePiece.mMatchId;
			this.mExplodeTimer = 0f;
			if (thePiece.IsFlagSet(1U))
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eLIGHTNING_STORM_NOVA_SCALE, this.mNovaScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eLIGHTNING_STORM_NOVA_ALPHA, this.mNovaAlpha, this.mNovaScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eLIGHTNING_STORM_NUKE_SCALE, this.mNukeScale, this.mNovaScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eLIGHTNING_STORM_NUKE_ALPHA, this.mNukeAlpha, this.mNovaScale);
				this.mStormType = 6;
			}
			else
			{
				this.mStormType = theType;
			}
			this.mElectrocuterId = thePiece.mId;
			this.mCX = (int)thePiece.CX() - this.mBoard.GetBoardX();
			this.mCY = (int)thePiece.CY() - this.mBoard.GetBoardY();
			this.mOriginCol = thePiece.mCol;
			this.mOriginRow = thePiece.mRow;
			thePiece.mIsElectrocuting = true;
			if (this.mStormType != 7)
			{
				thePiece.mElectrocutePercent = 0.9f;
			}
			this.mColor = -1;
			this.mTimer = 0f;
			this.mDist = 0;
			this.mHoldDelay = 1f;
			this.mStormLength = ((this.mStormType == 3) ? GlobalMembers.M(3) : GlobalMembers.M(7));
			if (this.mStormType != 7)
			{
				for (int i = ((this.mStormType == 6) ? (-1) : 0); i <= ((this.mStormType == 6) ? 1 : 0); i++)
				{
					int rowAt = this.mBoard.GetRowAt((int)thePiece.mY + 50 + i * 100);
					if (rowAt >= 0 && rowAt < 8 && this.mStormType != 1)
					{
						if (this.mMatchType < 0)
						{
							this.mMatchType = SexyFramework.Common.Rand(Bej3Com.gElectColors.Length);
						}
						LightningZap lightningZap = new LightningZap(this.mBoard, Math.Max(0, this.mCX - this.mStormLength * 100 - 50), (int)thePiece.mY + 50 + i * 100, Math.Min(this.mBoard.GetColX(8), this.mCX + this.mStormLength * 100 + 50), (int)thePiece.mY + 50 + i * 100, Bej3Com.gElectColors[this.mMatchType], GlobalMembers.M(10f), this.mStormType == 6);
						this.mZaps.Add(lightningZap);
					}
					int colAt = this.mBoard.GetColAt((int)thePiece.mX + 50 + i * 100);
					if (colAt >= 0 && colAt < 8 && this.mStormType != 0)
					{
						LightningZap lightningZap2 = new LightningZap(this.mBoard, (int)thePiece.mX + 50 + i * 100, Math.Max(0, this.mCY - this.mStormLength * 100 - 50), (int)thePiece.mX + 50 + i * 100, Math.Min(this.mBoard.GetRowY(8), this.mCY + this.mStormLength * 100 + 50), Bej3Com.gElectColors[this.mMatchType], GlobalMembers.M(10f), this.mStormType == 6);
						this.mZaps.Add(lightningZap2);
					}
				}
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eLIGHTNING_STORM_LIGHTNING_ALPHA, this.mLightingAlpha);
				return;
			}
			if (thePiece != null && thePiece.mColor == -1)
			{
				thePiece.mDestructing = true;
			}
		}

		public void Dispose()
		{
			for (int i = 0; i < this.mZaps.size<LightningZap>(); i++)
			{
				if (this.mZaps[i] != null)
				{
					this.mZaps[i].Dispose();
				}
			}
			for (int j = 0; j < this.mLightningVector.size<Lightning>(); j++)
			{
				if (this.mLightningVector[j] != null)
				{
					this.mLightningVector[j].Dispose();
				}
			}
		}

		public void AddLightning(int theStartX, int theStartY, int theEndX, int theEndY)
		{
			Lightning lightning = new Lightning();
			lightning.mPercentDone = 0f;
			float num = (float)(theEndY - theStartY);
			float num2 = (float)(theEndX - theStartX);
			float num3 = (float)Math.Atan2((double)num, (double)num2);
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num * num));
			lightning.mPullX = (float)Math.Cos((double)num3 - 1.570795) * num4 * 0.4f;
			lightning.mPullY = (float)Math.Sin((double)(num3 - 1.570795f)) * num4 * 0.4f;
			for (int i = 0; i < Bej3Com.NUM_LIGTNING_POINTS; i++)
			{
				float num5 = (float)i / (float)(Bej3Com.NUM_LIGTNING_POINTS - 1);
				float mX = (float)theStartX * (1f - num5) + (float)theEndX * num5;
				float mY = (float)theStartY * (1f - num5) + (float)theEndY * num5;
				FPoint fpoint = lightning.mPoints[i, 0];
				FPoint fpoint2 = lightning.mPoints[i, 1];
				fpoint.mX = mX;
				fpoint.mY = mY;
				fpoint2.mX = mX;
				fpoint2.mY = mY;
				lightning.mPoints[i, 0] = fpoint;
				lightning.mPoints[i, 1] = fpoint2;
			}
			this.mLightningVector.Add(lightning);
		}

		public void UpdateLightning()
		{
			if (this.mDoneDelay > 0)
			{
				return;
			}
			bool flag = this.mBoard.WantsCalmEffects();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag2 = false;
			bool flag3 = false;
			Piece[,] array = this.mBoard.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null)
					{
						if (piece.mExplodeDelay > 0)
						{
							flag3 = true;
						}
						if (piece.mIsElectrocuting)
						{
							if (flag)
							{
								if (piece.IsFlagSet(2U))
								{
									piece.mElectrocutePercent += GlobalMembers.M(0.0075f);
								}
								else
								{
									piece.mElectrocutePercent += GlobalMembers.M(0.01f);
								}
							}
							else if (piece.IsFlagSet(2U))
							{
								piece.mElectrocutePercent += GlobalMembers.M(0.01f);
							}
							else
							{
								piece.mElectrocutePercent += 0.015f;
							}
							if (piece.mElectrocutePercent > 1f)
							{
								this.mBoard.SetMoveCredit(piece, this.mMoveCreditId);
								piece.mExplodeSourceId = this.mElectrocuterId;
								piece.mExplodeSourceFlags |= this.mStartPieceFlags;
								if (!this.mBoard.TriggerSpecial(piece, this.mBoard.GetPieceById(this.mElectrocuterId)))
								{
									piece.mExplodeDelay = 1;
									piece.mMatchId = 1;
								}
							}
							else
							{
								if ((double)piece.mElectrocutePercent < 0.04)
								{
									this.UL_anElectrocuterPieces[num2++] = piece;
								}
								this.UL_anElectrocutedPieces[num++] = piece;
							}
						}
						else if ((piece.mColor == this.mColor || this.mColor == -1) && piece.GetScreenY() > -100f && !piece.IsFlagSet(6144U))
						{
							this.UL_aMatchingPieces[num3++] = piece;
						}
					}
				}
			}
			int num4 = 20 / (num + 1) + 5;
			if (flag)
			{
				num4 = (int)((float)num4 * GlobalMembers.M(1.4f));
			}
			if (this.mColor == -1)
			{
				num4 /= GlobalMembers.M(2);
			}
			if (num3 > 0 && (this.mLightningVector.size<Lightning>() == 0 || (ulong)this.mBoard.mRand.Next() % (ulong)((long)num4) == 0UL))
			{
				Piece piece2 = null;
				Piece piece3 = null;
				checked
				{
					if (num2 > 0)
					{
						piece3 = this.UL_anElectrocuterPieces[(int)((IntPtr)(unchecked((ulong)this.mBoard.mRand.Next() % (ulong)((long)num2))))];
					}
					else if (num > 0)
					{
						piece3 = this.UL_anElectrocutedPieces[(int)((IntPtr)(unchecked((ulong)this.mBoard.mRand.Next() % (ulong)((long)num))))];
					}
					if (piece3 != null)
					{
						int maxValue = int.MaxValue;
						unchecked
						{
							for (int k = 0; k < num3; k++)
							{
								Piece piece4 = this.UL_aMatchingPieces[k];
								int num5 = Math.Min(Math.Abs(piece4.mCol - piece3.mCol), Math.Abs(piece4.mRow - piece3.mRow));
								if (num5 < maxValue)
								{
									piece2 = piece4;
								}
							}
							this.AddLightning((int)piece2.mX + 50, (int)piece2.mY + 50, (int)piece3.mX + 50, (int)piece3.mY + 50);
							Effect effect = this.mBoard.mPostFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
							effect.mFlags = 2;
							effect.mX = piece2.CX();
							effect.mY = piece2.CY();
							effect.mZ = GlobalMembers.M(0.08f);
							effect.mValue[0] = GlobalMembers.M(16.1f);
							effect.mValue[1] = GlobalMembers.M(-0.8f);
							effect.mAlpha = GlobalMembers.M(0f);
							effect.mDAlpha = GlobalMembers.M(0.1f);
							effect.mScale = GlobalMembers.M(140f);
							this.mBoard.mPostFXManager.AddEffect(effect);
							if (this.mUpdateCnt - this.mLastElectroSound >= GlobalMembers.M(20) || this.mLastElectroSound == 0)
							{
								if (flag)
								{
									GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ELECTRO_PATH2, GlobalMembers.M(0), GlobalMembers.MS(0.67), GlobalMembers.MS(-1.0));
								}
								else
								{
									GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ELECTRO_PATH2);
								}
								this.mLastElectroSound = 0;
							}
						}
					}
					else
					{
						piece2 = this.UL_aMatchingPieces[(int)((IntPtr)(unchecked((ulong)this.mBoard.mRand.Next() % (ulong)((long)num3))))];
					}
					piece2.mIsElectrocuting = true;
				}
			}
			float num6 = (GlobalMembers.gIs3D ? GlobalMembers.M(32f) : GlobalMembers.M(24f));
			for (int l = 0; l < this.mLightningVector.size<Lightning>(); l++)
			{
				Lightning lightning = this.mLightningVector[l];
				lightning.mPercentDone += 0.012f;
				if (lightning.mPercentDone > 1f)
				{
					lightning.Dispose();
					this.mLightningVector.RemoveAt(l);
					l--;
				}
				else
				{
					float num7 = Math.Max(0f, 1f - (1f - lightning.mPercentDone) * 3f);
					if ((flag && this.mUpdateCnt % GlobalMembers.M(8) == 0) || (!flag && this.mUpdateCnt % 4 == 0))
					{
						float mX = lightning.mPoints[0, 0].mX;
						float mY = lightning.mPoints[0, 0].mY;
						float mX2 = lightning.mPoints[Bej3Com.NUM_LIGTNING_POINTS - 1, 0].mX;
						float mY2 = lightning.mPoints[Bej3Com.NUM_LIGTNING_POINTS - 1, 0].mY;
						for (int m = 0; m < Bej3Com.NUM_LIGTNING_POINTS; m++)
						{
							float num8 = (float)m / (float)(Bej3Com.NUM_LIGTNING_POINTS - 1);
							float num9 = 1f - Math.Abs(1f - num8 * 2f);
							float num10 = mX * (1f - num8) + mX2 * num8 + num9 * (GlobalMembersUtils.GetRandFloat() * 40f + num7 * lightning.mPullX);
							float num11 = mY * (1f - num8) + mY2 * num8 + num9 * (GlobalMembersUtils.GetRandFloat() * 40f + num7 * lightning.mPullY);
							if (flag)
							{
								num10 = mX * (1f - num8) + mX2 * num8 + num9 * (GlobalMembersUtils.GetRandFloat() * 15f + num7 * lightning.mPullX);
								num11 = mY * (1f - num8) + mY2 * num8 + num9 * (GlobalMembersUtils.GetRandFloat() * 15f + num7 * lightning.mPullY);
							}
							FPoint fpoint = lightning.mPoints[m, 0];
							FPoint fpoint2 = lightning.mPoints[m, 1];
							if (m == 0 || m == Bej3Com.NUM_LIGTNING_POINTS - 1)
							{
								fpoint.mX = num10;
								fpoint.mY = num11;
								fpoint2.mX = num10;
								fpoint2.mY = num11;
							}
							else
							{
								fpoint.mX = num10 + GlobalMembersUtils.GetRandFloat() * num6;
								fpoint.mY = num11 + GlobalMembersUtils.GetRandFloat() * num6;
								fpoint2.mX = num10 + GlobalMembersUtils.GetRandFloat() * num6;
								fpoint2.mY = num11 + GlobalMembersUtils.GetRandFloat() * num6;
							}
							lightning.mPoints[m, 0] = fpoint;
							lightning.mPoints[m, 1] = fpoint2;
						}
					}
				}
			}
			if (!flag3 && num == 0 && num3 == 0 && this.mLightningVector.size<Lightning>() == 0 && !flag2)
			{
				for (int n = 0; n < 8; n++)
				{
					for (int num12 = 0; num12 < 8; num12++)
					{
						Piece piece5 = this.mBoard.mBoard[n, num12];
						if (piece5 != null)
						{
							piece5.mFallVelocity = 0f;
						}
					}
				}
				this.mDoneDelay = 10;
			}
		}

		public void DrawLightning(Graphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			g.PushState();
			g.Translate(GlobalMembers.S(this.mBoard.GetBoardX()), GlobalMembers.S(this.mBoard.GetBoardY()));
			for (int i = 0; i < this.mLightningVector.size<Lightning>(); i++)
			{
				Lightning lightning = this.mLightningVector[i];
				float num = Math.Min((1f - lightning.mPercentDone) * 8f, 1f) * this.mBoard.GetPieceAlpha();
				int num2 = (int)((double)num * 255.0);
				if (graphics3D != null)
				{
					int num3 = 0;
					for (int j = 0; j < Bej3Com.NUM_LIGTNING_POINTS - 1; j++)
					{
						FPoint fpoint = lightning.mPoints[j, 0];
						FPoint fpoint2 = lightning.mPoints[j, 1];
						FPoint fpoint3 = lightning.mPoints[j + 1, 0];
						FPoint fpoint4 = lightning.mPoints[j + 1, 1];
						float v = (float)j / (float)(Bej3Com.NUM_LIGTNING_POINTS - 1);
						float v2 = (float)(j + 1) / (float)(Bej3Com.NUM_LIGTNING_POINTS - 1);
						float x = GlobalMembers.S(fpoint.mX);
						float y = GlobalMembers.S(fpoint.mY);
						float x2 = GlobalMembers.S(fpoint4.mX);
						float y2 = GlobalMembers.S(fpoint4.mY);
						float x3 = GlobalMembers.S(fpoint3.mX);
						float y3 = GlobalMembers.S(fpoint3.mY);
						if (j == 0)
						{
							LightningStorm.DL_aTriVertices[num3, 0].x = x;
							LightningStorm.DL_aTriVertices[num3, 0].y = y;
							LightningStorm.DL_aTriVertices[num3, 0].u = 0.5f;
							LightningStorm.DL_aTriVertices[num3, 0].v = v;
							LightningStorm.DL_aTriVertices[num3, 1] = LightningStorm.DL_aTriVertices[num3, 1];
							LightningStorm.DL_aTriVertices[num3, 1].x = x2;
							LightningStorm.DL_aTriVertices[num3, 1].y = y2;
							LightningStorm.DL_aTriVertices[num3, 1].u = 1f;
							LightningStorm.DL_aTriVertices[num3, 1].v = v2;
							LightningStorm.DL_aTriVertices[num3, 2] = LightningStorm.DL_aTriVertices[num3, 2];
							LightningStorm.DL_aTriVertices[num3, 2].x = x3;
							LightningStorm.DL_aTriVertices[num3, 2].y = y3;
							LightningStorm.DL_aTriVertices[num3, 2].u = 0f;
							LightningStorm.DL_aTriVertices[num3, 2].v = v2;
							num3++;
						}
						else if (j == Bej3Com.NUM_LIGTNING_POINTS - 2)
						{
							LightningStorm.DL_aTriVertices[num3, 0].x = x;
							LightningStorm.DL_aTriVertices[num3, 0].y = y;
							LightningStorm.DL_aTriVertices[num3, 0].u = 0f;
							LightningStorm.DL_aTriVertices[num3, 0].v = v;
							LightningStorm.DL_aTriVertices[num3, 1].x = GlobalMembers.S(fpoint2.mX);
							LightningStorm.DL_aTriVertices[num3, 1].y = GlobalMembers.S(fpoint2.mY);
							LightningStorm.DL_aTriVertices[num3, 1].u = 1f;
							LightningStorm.DL_aTriVertices[num3, 1].v = v;
							LightningStorm.DL_aTriVertices[num3, 2].x = x3;
							LightningStorm.DL_aTriVertices[num3, 2].y = y3;
							LightningStorm.DL_aTriVertices[num3, 2].u = 0.5f;
							LightningStorm.DL_aTriVertices[num3, 2].v = v2;
							num3++;
						}
						else
						{
							LightningStorm.DL_aTriVertices[num3, 0].x = x;
							LightningStorm.DL_aTriVertices[num3, 0].y = y;
							LightningStorm.DL_aTriVertices[num3, 0].u = 0f;
							LightningStorm.DL_aTriVertices[num3, 0].v = v;
							LightningStorm.DL_aTriVertices[num3, 1].x = x2;
							LightningStorm.DL_aTriVertices[num3, 1].y = y2;
							LightningStorm.DL_aTriVertices[num3, 1].u = 1f;
							LightningStorm.DL_aTriVertices[num3, 1].v = v2;
							LightningStorm.DL_aTriVertices[num3, 2].x = x3;
							LightningStorm.DL_aTriVertices[num3, 2].y = y3;
							LightningStorm.DL_aTriVertices[num3, 2].u = 0f;
							LightningStorm.DL_aTriVertices[num3, 2].v = v2;
							num3++;
							LightningStorm.DL_aTriVertices[num3, 0].x = x;
							LightningStorm.DL_aTriVertices[num3, 0].y = y;
							LightningStorm.DL_aTriVertices[num3, 0].u = 0f;
							LightningStorm.DL_aTriVertices[num3, 0].v = v;
							LightningStorm.DL_aTriVertices[num3, 1].x = GlobalMembers.S(fpoint2.mX);
							LightningStorm.DL_aTriVertices[num3, 1].y = GlobalMembers.S(fpoint2.mY);
							LightningStorm.DL_aTriVertices[num3, 1].u = 1f;
							LightningStorm.DL_aTriVertices[num3, 1].v = v;
							LightningStorm.DL_aTriVertices[num3, 2].x = x2;
							LightningStorm.DL_aTriVertices[num3, 2].y = y2;
							LightningStorm.DL_aTriVertices[num3, 2].u = 1f;
							LightningStorm.DL_aTriVertices[num3, 2].v = v2;
							num3++;
						}
					}
					int num4 = SexyFramework.Common.Rand(5);
					Color theColor = Color.White;
					switch (num4)
					{
					case 0:
						theColor = Color.White;
						break;
					case 1:
						theColor = Color.Red;
						break;
					case 2:
						theColor = Color.Blue;
						break;
					case 3:
						theColor = Color.Green;
						break;
					case 4:
						theColor = Color.Yellow;
						break;
					}
					if (this.mColor >= 0)
					{
						theColor = new Color((int)((float)Bej3Com.gElectColors[this.mColor].mRed * num), (int)((float)Bej3Com.gElectColors[this.mColor].mGreen * num), (int)((float)Bej3Com.gElectColors[this.mColor].mBlue * num));
					}
					g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_LIGHTNING_TEX, LightningStorm.DL_aTriVertices, num3, theColor, 1, g.mTransX, g.mTransY, true, Rect.INVALIDATE_RECT);
					g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_LIGHTNING_CENTER, LightningStorm.DL_aTriVertices, num3, new Color(num2, num2, num2), 1, g.mTransX, g.mTransY, true, Rect.INVALIDATE_RECT);
				}
				else
				{
					g.SetDrawMode(1);
					Color color = new Color((int)((float)Bej3Com.gElectColors[this.mColor].mRed * num), (int)((float)Bej3Com.gElectColors[this.mColor].mGreen * num), (int)((float)Bej3Com.gElectColors[this.mColor].mBlue * num));
					Point[] array = new Point[3];
					for (int k = 0; k < Bej3Com.NUM_LIGTNING_POINTS - 1; k++)
					{
						FPoint fpoint5 = lightning.mPoints[k, 0];
						FPoint fpoint6 = lightning.mPoints[k, 1];
						FPoint fpoint7 = lightning.mPoints[k + 1, 0];
						FPoint fpoint8 = lightning.mPoints[k + 1, 1];
						float num5 = GlobalMembers.S(fpoint5.mX * 0.3f) + fpoint6.mX * 0.7f;
						float num6 = GlobalMembers.S(fpoint5.mY * 0.3f) + fpoint6.mY * 0.7f;
						float num7 = GlobalMembers.S(fpoint6.mX * 0.3f) + fpoint5.mX * 0.7f;
						float num8 = GlobalMembers.S(fpoint6.mY * 0.3f) + fpoint5.mY * 0.7f;
						float num9 = GlobalMembers.S(fpoint7.mX * 0.3f) + fpoint8.mX * 0.7f;
						float num10 = GlobalMembers.S(fpoint7.mY * 0.3f) + fpoint8.mY * 0.7f;
						float num11 = GlobalMembers.S(fpoint8.mX * 0.3f) + fpoint7.mX * 0.7f;
						float num12 = GlobalMembers.S(fpoint8.mY * 0.3f) + fpoint7.mY * 0.7f;
						float num13 = GlobalMembers.S(fpoint5.mX);
						float num14 = GlobalMembers.S(fpoint5.mY);
						float num15 = GlobalMembers.S(fpoint8.mX);
						float num16 = GlobalMembers.S(fpoint8.mY);
						float num17 = GlobalMembers.S(fpoint7.mX);
						float num18 = GlobalMembers.S(fpoint7.mY);
						g.SetColor(color);
						array[0].mX = (int)num13;
						array[0].mY = (int)num14;
						array[1].mX = (int)num15;
						array[1].mY = (int)num16;
						array[2].mX = (int)num17;
						array[2].mY = (int)num18;
						g.PolyFill(array, 3, false);
						array[0].mX = (int)num13;
						array[0].mY = (int)num14;
						array[1].mX = (int)GlobalMembers.S(fpoint6.mX);
						array[1].mY = (int)GlobalMembers.S(fpoint6.mY);
						array[2].mX = (int)num15;
						array[2].mY = (int)num16;
						g.PolyFill(array, 3, false);
						g.SetColor(new Color(num2, num2, num2));
						array[0].mX = (int)num5;
						array[0].mY = (int)num6;
						array[1].mX = (int)num11;
						array[1].mY = (int)num12;
						array[2].mX = (int)num9;
						array[2].mY = (int)num10;
						g.PolyFill(array, 3, false);
						array[0].mX = (int)num5;
						array[0].mY = (int)num6;
						array[1].mX = (int)num7;
						array[1].mY = (int)num8;
						array[2].mX = (int)num11;
						array[2].mY = (int)num12;
						g.PolyFill(array, 3, false);
					}
					g.SetDrawMode(0);
				}
			}
			g.PopState();
		}

		public void Update()
		{
			this.mUpdateCnt++;
			this.mNovaScale.IncInVal();
			this.mNukeScale.IncInVal();
			if (this.mStormType == 6 && this.mNukeScale.CheckInThreshold((double)GlobalMembers.M(1.6f)))
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE, 0);
			}
			for (int i = 0; i < this.mZaps.size<LightningZap>(); i++)
			{
				LightningZap lightningZap = this.mZaps[i];
				lightningZap.Update();
			}
			this.mGemAlpha = Math.Max(0f, this.mGemAlpha - GlobalMembers.M(0.01f));
			if (this.mStormType == 7)
			{
				this.UpdateLightning();
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mNovaAlpha != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mNovaAlpha);
				Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BOOM_NOVA, (float)GlobalMembers.S(this.mBoard.GetBoardX() + this.mCX), (float)GlobalMembers.S(this.mBoard.GetBoardY() + this.mCY), (float)this.mNovaScale, (float)this.mNovaScale);
			}
			if (this.mNukeAlpha != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mNukeAlpha);
				Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BOOM_NUKE, (float)GlobalMembers.S(this.mBoard.GetBoardX() + this.mCX), (float)GlobalMembers.S(this.mBoard.GetBoardY() + this.mCY), (float)this.mNukeScale, (float)this.mNukeScale);
			}
			for (int i = 0; i < Enumerable.Count<LightningZap>(this.mZaps); i++)
			{
				LightningZap lightningZap = this.mZaps[i];
				lightningZap.Draw(g);
			}
			if (this.mStormType == 7)
			{
				this.DrawLightning(g);
			}
		}

		public Board mBoard;

		public int mCX;

		public int mCY;

		public int mUpdateCnt;

		public int mColor;

		public int mStormLength;

		public int mLastElectroSound;

		public int mStartPieceFlags;

		public int mMatchType;

		public int mLightningCount;

		public int mStormType;

		public float mExplodeTimer;

		public float mHoldDelay;

		public int mElectrocuterId;

		public int mMoveCreditId;

		public int mMatchId;

		public int mOriginCol;

		public int mOriginRow;

		public int mDist;

		public float mTimer;

		public List<LightningZap> mZaps = new List<LightningZap>();

		public List<Lightning> mLightningVector = new List<Lightning>();

		public List<int> mPieceIds = new List<int>();

		public List<ElectrocutedCel> mElectrocutedCelVector = new List<ElectrocutedCel>();

		public CurvedVal mNovaScale = new CurvedVal();

		public CurvedVal mNovaAlpha = new CurvedVal();

		public CurvedVal mNukeScale = new CurvedVal();

		public CurvedVal mNukeAlpha = new CurvedVal();

		public CurvedVal mLightingAlpha = new CurvedVal();

		public float mGemAlpha;

		public int mDoneDelay;

		private Piece[] UL_anElectrocutedPieces = new Piece[64];

		private Piece[] UL_anElectrocuterPieces = new Piece[64];

		private Piece[] UL_aMatchingPieces = new Piece[64];

		private static SexyVertex2D[,] DL_aTriVertices = new SexyVertex2D[(Bej3Com.NUM_LIGTNING_POINTS - 1) * 2, 3];

		public enum STORM
		{
			STORM_HORZ,
			STORM_VERT,
			STORM_BOTH,
			STORM_SHORT,
			STORM_STAR,
			STORM_SCREEN,
			STORM_FLAMING,
			STORM_HYPERCUBE
		}
	}
}
