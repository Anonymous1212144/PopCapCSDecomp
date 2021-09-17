using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class TimeBonusEffect : Effect
	{
		public new static void initPool()
		{
			TimeBonusEffect.thePool_ = new SimpleObjectPool(512, typeof(TimeBonusEffect));
		}

		public static TimeBonusEffect alloc(Piece thePiece)
		{
			TimeBonusEffect timeBonusEffect = (TimeBonusEffect)TimeBonusEffect.thePool_.alloc();
			timeBonusEffect.init(thePiece);
			return timeBonusEffect;
		}

		public override void release()
		{
			this.Dispose();
			TimeBonusEffect.thePool_.release(this);
		}

		public static void batchInit()
		{
			TimeBonusEffect.batchElectro_ = new TriangleArray();
			TimeBonusEffect.batchCenter_ = new TriangleArray();
		}

		public static void batchBegin()
		{
			TimeBonusEffect.batchElectro_.clear();
			TimeBonusEffect.batchCenter_.clear();
		}

		public static void batchEnd(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.Additive);
			if (TimeBonusEffect.batchElectro_.count() > 0)
			{
				g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_ELECTROTEX, TimeBonusEffect.batchElectro_.toArray(), TimeBonusEffect.batchElectro_.count());
			}
			if (TimeBonusEffect.batchCenter_.count() > 0)
			{
				g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_ELECTROTEX_CENTER, TimeBonusEffect.batchCenter_.toArray(), TimeBonusEffect.batchCenter_.count());
			}
		}

		private static void batchAddTriangle(TimeBonusEffect.BatchType type, SexyVertex2D v1, SexyVertex2D v2, SexyVertex2D v3)
		{
			if (type == TimeBonusEffect.BatchType.Electro)
			{
				TimeBonusEffect.batchElectro_.add(v1, v2, v3);
				return;
			}
			if (type == TimeBonusEffect.BatchType.Center)
			{
				TimeBonusEffect.batchCenter_.add(v1, v2, v3);
			}
		}

		public TimeBonusEffect()
			: base(Effect.Type.TYPE_TIME_BONUS)
		{
		}

		public void init(Piece thePiece)
		{
			base.init(Effect.Type.TYPE_TIME_BONUS);
			this.mPieceRel = thePiece;
			this.mGemColor = thePiece.mColor;
			this.mTimeBonus = thePiece.mCounter;
			this.mDAlpha = 0f;
			this.mRadiusScale.SetConstant(1.0);
			this.mUpdated = false;
		}

		public override void Dispose()
		{
			this.mElectroBoltVector.Clear();
			base.Dispose();
		}

		public override void Update()
		{
			this.mUpdated = true;
			this.mCirclePct.IncInVal();
			this.mRadiusScale.IncInVal();
			Piece mPieceRel = this.mPieceRel;
			if (mPieceRel != null)
			{
				this.mOverlay = false;
				this.mX = mPieceRel.GetScreenX() + 50f;
				this.mY = mPieceRel.GetScreenY() + 50f;
				this.mTimeBonus = mPieceRel.mCounter;
				for (int i = 0; i < this.mFXManager.mBoard.mLightningStorms.Count; i++)
				{
					if (this.mFXManager.mBoard.mLightningStorms[i].mStormType == 7 && this.mFXManager.mBoard.mLightningStorms[i].mColor == mPieceRel.mColor)
					{
						this.mOverlay = true;
					}
				}
			}
			int theFrame = 0;
			if (mPieceRel != null)
			{
				theFrame = Math.Min(19, (int)(20f * mPieceRel.mRotPct));
			}
			float num = (float)Math.Min(4, this.mTimeBonus - 1) / 4f;
			bool flag = GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.12f) * num;
			if (!GlobalMembers.gIs3D)
			{
				flag = GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.001f) * num;
			}
			if (this.mElectroBoltVector.Count < Math.Min(GlobalMembers.M(3), this.mTimeBonus * 2 - 1) || flag)
			{
				ElectroBolt electroBolt = new ElectroBolt();
				electroBolt.mHitOtherGem = false;
				electroBolt.mCrossover = !electroBolt.mHitOtherGem && GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.02f);
				if (electroBolt.mHitOtherGem)
				{
					electroBolt.mAngStart = Math.Abs(GlobalMembersUtils.GetRandFloat()) * 3.14159274f * 2f;
					Piece pieceAtScreenXY = this.mFXManager.mBoard.GetPieceAtScreenXY((int)(this.mX + 50f + (float)Math.Cos((double)electroBolt.mAngStart) * 100f * GlobalMembers.M(0.6f)), (int)(this.mY + 50f + (float)Math.Sin((double)electroBolt.mAngStart) * 100f * GlobalMembers.M(0.6f)));
					if (pieceAtScreenXY != null && pieceAtScreenXY != mPieceRel)
					{
						electroBolt.mHitOtherGemId = pieceAtScreenXY.mId;
					}
					else
					{
						electroBolt.mHitOtherGem = false;
					}
				}
				if (electroBolt.mHitOtherGem)
				{
					electroBolt.mAngEnd = 3.14159274f + electroBolt.mAngStart + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.5f);
					electroBolt.mAngStartD = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.03f);
					electroBolt.mAngEndD = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.03f);
				}
				else if (electroBolt.mCrossover)
				{
					electroBolt.mAngStart = Math.Abs(GlobalMembersUtils.GetRandFloat()) * 3.14159274f * 2f;
					electroBolt.mAngEnd = electroBolt.mAngStart;
					electroBolt.mAngStartD = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.02f);
					if (electroBolt.mAngStartD < 0f)
					{
						electroBolt.mAngStartD += GlobalMembers.M(-0.02f);
					}
					else
					{
						electroBolt.mAngStartD += GlobalMembers.M(0.02f);
					}
					electroBolt.mAngEndD = -electroBolt.mAngStartD + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.02f);
				}
				else
				{
					electroBolt.mAngStart = Math.Abs(GlobalMembersUtils.GetRandFloat()) * 3.14159274f * 2f;
					electroBolt.mAngEnd = electroBolt.mAngStart + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.5f) + GlobalMembers.M(0.5f);
					electroBolt.mAngStartD = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.0075f);
					electroBolt.mAngEndD = electroBolt.mAngStartD + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.002f);
				}
				electroBolt.mNumMidPoints = 2;
				for (int j = 0; j < electroBolt.mNumMidPoints; j++)
				{
					electroBolt.mMidPointsPos[j] = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(10f);
					electroBolt.mMidPointsPosD[j] = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.2f);
				}
				this.mElectroBoltVector.Add(electroBolt);
			}
			for (int k = 0; k < this.mElectroBoltVector.Count; k++)
			{
				ElectroBolt electroBolt2 = this.mElectroBoltVector[k];
				electroBolt2.mAngStart += electroBolt2.mAngStartD;
				electroBolt2.mAngEnd += electroBolt2.mAngEndD;
				bool flag2 = false;
				for (int l = 0; l < electroBolt2.mNumMidPoints; l++)
				{
					electroBolt2.mMidPointsPos[l] += electroBolt2.mMidPointsPosD[l];
					if (electroBolt2.mHitOtherGem)
					{
						if (Math.Abs(electroBolt2.mMidPointsPos[l]) >= (float)GlobalMembers.M(25))
						{
							electroBolt2.mMidPointsPosD[l] *= -0.65f;
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.2f))
						{
							electroBolt2.mMidPointsPos[l] = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(15f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.05f))
						{
							electroBolt2.mMidPointsPosD[l] += GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(1.5f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.05f))
						{
							electroBolt2.mMidPointsPosD[l] = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(1.5f);
						}
					}
					else if (electroBolt2.mCrossover)
					{
						if (Math.Abs(electroBolt2.mMidPointsPos[l]) >= (float)GlobalMembers.M(25))
						{
							electroBolt2.mMidPointsPosD[l] *= -0.65f;
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.2f))
						{
							electroBolt2.mMidPointsPos[l] = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(15f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.1f))
						{
							electroBolt2.mMidPointsPosD[l] += GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(1.5f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.1f))
						{
							electroBolt2.mMidPointsPosD[l] = GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(1.5f);
						}
					}
					else
					{
						if (electroBolt2.mMidPointsPos[l] <= 0f)
						{
							electroBolt2.mMidPointsPos[l] = 0f;
							electroBolt2.mMidPointsPosD[l] = GlobalMembersUtils.GetRandFloatU() * GlobalMembers.M(0.1f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.05f))
						{
							float num2 = (GlobalMembers.M(4f) - electroBolt2.mMidPointsPos[l]) * GlobalMembers.M(0.1f);
							electroBolt2.mMidPointsPosD[l] = num2 + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(1f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.025f))
						{
							electroBolt2.mMidPointsPos[l] = GlobalMembersUtils.GetRandFloatU() * GlobalMembers.M(18f);
						}
						else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.04f))
						{
							electroBolt2.mMidPointsPosD[l] += GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(2.5f);
						}
						if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.1f))
						{
							float num3 = 0f;
							float num4 = 0f;
							if (l - 1 >= 0)
							{
								num3 = electroBolt2.mMidPointsPos[l - 1];
							}
							if (l + 1 < electroBolt2.mNumMidPoints)
							{
								num4 = electroBolt2.mMidPointsPos[l + 1];
							}
							electroBolt2.mMidPointsPos[l] = (electroBolt2.mMidPointsPos[l] + num3 + num4) / 3f;
						}
						if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.2f))
						{
							int num5 = l + BejeweledLivePlus.Misc.Common.Rand() % 3 - 1;
							if (num5 >= 0 && num5 < electroBolt2.mNumMidPoints)
							{
								float num6 = electroBolt2.mMidPointsPos[num5] - electroBolt2.mMidPointsPos[l];
								electroBolt2.mMidPointsPosD[l] += num6 * GlobalMembers.M(0.2f);
							}
						}
						if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.1f))
						{
							float num7 = 0f;
							float num8 = 0f;
							if (l - 1 >= 0)
							{
								num7 = electroBolt2.mMidPointsPosD[l - 1];
							}
							if (l + 1 < electroBolt2.mNumMidPoints)
							{
								num8 = electroBolt2.mMidPointsPosD[l + 1];
							}
							electroBolt2.mMidPointsPosD[l] = (num7 + num8) / 2f;
						}
					}
					if (electroBolt2.mMidPointsPos[l] > GlobalMembers.M(12f) || (num <= 0f && GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.01f)))
					{
						flag2 = true;
					}
				}
				if (electroBolt2.mHitOtherGem)
				{
					float num9 = Piece.GetAngleRadius(electroBolt2.mAngStart, this.mGemColor, theFrame) + GlobalMembers.M(0f);
					float num10 = (float)Math.Cos((double)electroBolt2.mAngStart) * num9;
					float num11 = (float)Math.Sin((double)electroBolt2.mAngStart) * num9;
					Piece pieceById = GlobalMembers.gApp.mBoard.GetPieceById(electroBolt2.mHitOtherGemId);
					if (pieceById != null)
					{
						float angleRadius = pieceById.GetAngleRadius(electroBolt2.mAngEnd);
						float num12 = (pieceById.mX - this.mX) / GlobalMembers.S(1f) + (float)Math.Cos((double)electroBolt2.mAngEnd) * angleRadius;
						float num13 = (pieceById.mY - this.mY) / GlobalMembers.S(1f) + (float)Math.Sin((double)electroBolt2.mAngEnd) * angleRadius;
						float num14 = num10 - num12;
						float num15 = num11 - num13;
						if (Math.Sqrt((double)(num14 * num14 + num15 * num15)) > (double)GlobalMembers.M(70f))
						{
							flag2 = true;
						}
						float num16 = (float)Math.Atan2((double)(pieceById.mY - this.mY), (double)(pieceById.mX - this.mX));
						float num17 = (float)Math.Cos((double)electroBolt2.mAngStart) * (float)Math.Cos((double)num16) + (float)Math.Sin((double)electroBolt2.mAngStart) * (float)Math.Sin((double)num16);
						if (num17 < GlobalMembers.M(0.8f))
						{
							flag2 = true;
						}
						float num18 = (float)Math.Cos((double)electroBolt2.mAngEnd) * (float)Math.Cos((double)(num16 + 3.14159274f)) + (float)Math.Sin((double)electroBolt2.mAngEnd) * (float)Math.Sin((double)(num16 + 3.14159274f));
						if (num18 < GlobalMembers.M(0.8f))
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
					if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.001f))
					{
						flag2 = true;
					}
				}
				else if (electroBolt2.mCrossover)
				{
					if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.001f))
					{
						flag2 = true;
					}
					if (Math.Abs(electroBolt2.mAngStart - electroBolt2.mAngEnd) >= 6.28318548f)
					{
						flag2 = true;
					}
				}
				else if (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.005f))
				{
					flag2 = true;
				}
				if (flag2)
				{
					this.mElectroBoltVector.RemoveAt(k);
					k--;
				}
			}
			if (BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(30) == 0)
			{
				Effect effect = this.mFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
				effect.mFlags = 2;
				effect.mX = this.mX + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(20f);
				effect.mY = this.mY + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(20f);
				effect.mZ = GlobalMembers.M(0.08f);
				effect.mValue[0] = GlobalMembers.M(30f);
				effect.mValue[1] = GlobalMembers.M(-0.1f);
				effect.mAlpha = GlobalMembers.M(0f);
				effect.mDAlpha = GlobalMembers.M(0.1f);
				effect.mScale = GlobalMembers.M(140f);
				effect.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255));
				if (BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(2) != 0 && this.mPieceRel != null)
				{
					effect.mPieceId = (uint)this.mPieceRel.mId;
				}
				this.mFXManager.AddEffect(effect);
			}
			if (this.mPieceRel != null && (mPieceRel == null || (!mPieceRel.IsFlagSet(131072U) && this.mElectroBoltVector.Count == 0)))
			{
				this.mDeleteMe = true;
			}
		}

		public void DrawElectroLine(Graphics g, Image theImage, float theStartX, float theStartY, float theEndX, float theEndY, float theWidth, Color theColor1, Color theColor2)
		{
			float num = theEndX - theStartX;
			float num2 = theEndY - theStartY;
			float num3 = (float)Math.Atan2((double)num2, (double)num);
			float num4 = (float)Math.Cos((double)num3);
			float num5 = (float)Math.Cos((double)(num3 + 1.57079637f));
			float num6 = (float)Math.Sin((double)(num3 + 1.57079637f));
			float num7 = theStartX + num4 * -theWidth;
			float num8 = theEndX + num4 * theWidth;
			uint theColor3 = (uint)theColor1.ToInt();
			uint theColor4 = (uint)theColor2.ToInt();
			TimeBonusEffect.BatchType type = TimeBonusEffect.BatchType.Electro;
			if (theImage == GlobalMembersResourcesWP.IMAGE_ELECTROTEX_CENTER)
			{
				type = TimeBonusEffect.BatchType.Center;
			}
			TimeBonusEffect.batchAddTriangle(type, new SexyVertex2D(num7 + num5 * theWidth, theStartY + num6 * theWidth, 0f, 0f, theColor3), new SexyVertex2D(num7 + num5 * -theWidth, theStartY + num6 * -theWidth, 0f, 1f, theColor3), new SexyVertex2D(theStartX + num5 * theWidth, theStartY + num6 * theWidth, 0.5f, 0f, theColor3));
			TimeBonusEffect.batchAddTriangle(type, new SexyVertex2D(num7 + num5 * -theWidth, theStartY + num6 * -theWidth, 0f, 1f, theColor3), new SexyVertex2D(theStartX + num5 * theWidth, theStartY + num6 * theWidth, 0.5f, 0f, theColor3), new SexyVertex2D(theStartX + num5 * -theWidth, theStartY + num6 * -theWidth, 0.5f, 1f, theColor3));
			TimeBonusEffect.batchAddTriangle(type, new SexyVertex2D(theStartX + num5 * theWidth, theStartY + num6 * theWidth, 0.5f, 0f, theColor3), new SexyVertex2D(theStartX + num5 * -theWidth, theStartY + num6 * -theWidth, 0.5f, 1f, theColor3), new SexyVertex2D(theEndX + num5 * theWidth, theEndY + num6 * theWidth, 0.5f, 0f, theColor4));
			TimeBonusEffect.batchAddTriangle(type, new SexyVertex2D(theStartX + num5 * -theWidth, theStartY + num6 * -theWidth, 0.5f, 1f, theColor3), new SexyVertex2D(theEndX + num5 * theWidth, theEndY + num6 * theWidth, 0.5f, 0f, theColor4), new SexyVertex2D(theEndX + num5 * -theWidth, theEndY + num6 * -theWidth, 0.5f, 1f, theColor4));
			TimeBonusEffect.batchAddTriangle(type, new SexyVertex2D(theEndX + num5 * theWidth, theEndY + num6 * theWidth, 0.5f, 0f, theColor4), new SexyVertex2D(theEndX + num5 * -theWidth, theEndY + num6 * -theWidth, 0.5f, 1f, theColor4), new SexyVertex2D(num8 + num5 * theWidth, theEndY + num6 * theWidth, 1f, 0f, theColor4));
			TimeBonusEffect.batchAddTriangle(type, new SexyVertex2D(theEndX + num5 * -theWidth, theEndY + num6 * -theWidth, 0.5f, 1f, theColor4), new SexyVertex2D(num8 + num5 * theWidth, theEndY + num6 * theWidth, 1f, 0f, theColor4), new SexyVertex2D(num8 + num5 * -theWidth, theEndY + num6 * -theWidth, 1f, 1f, theColor4));
		}

		public override void Draw(Graphics g)
		{
			if (GlobalMembers.gGR.mIgnoreDraws)
			{
				return;
			}
			if (!this.mUpdated)
			{
				return;
			}
			float num = this.mAlpha * this.mFXManager.mAlpha;
			if (GlobalMembers.gApp.mBoard != null)
			{
				num *= GlobalMembers.gApp.mBoard.GetAlpha();
			}
			g.SetColor(new Color(255, 255, 255, (int)(255f * num)));
			g.PushColorMult();
			int num2 = (int)(this.mX - 50f);
			int num3 = (int)(this.mY - 50f);
			Piece mPieceRel = this.mPieceRel;
			int theFrame = 0;
			if (mPieceRel != null)
			{
				float num4 = (float)mPieceRel.mScale;
				if (num4 != 1f)
				{
					Utils.PushScale(g, num4, num4, GlobalMembers.S(mPieceRel.CX()), GlobalMembers.S(mPieceRel.CY()));
				}
				theFrame = Math.Min(19, (int)(20f * mPieceRel.mRotPct));
				if (mPieceRel.mRotPct == 0f)
				{
					g.SetDrawMode(Graphics.DrawMode.Additive);
					g.SetColorizeImages(true);
					g.SetColor(new Color(255, 255, 255, (int)(Math.Min((float)this.mElectroBoltVector.Count * GlobalMembers.M(6f) + (float)GlobalMembers.M(64), 255f) * this.mAlpha)));
					Rect celRect = GlobalMembersResourcesWP.IMAGE_GEMOUTLINES.GetCelRect(0);
					celRect.mX += GlobalMembers.S(num2);
					celRect.mY += GlobalMembers.S(num3);
					GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_GEMOUTLINES, GlobalMembers.S(num2) + ConstantsWP.GEMOUTLINES_OFFSET_1, GlobalMembers.S(num3) + ConstantsWP.GEMOUTLINES_OFFSET_2, this.mGemColor);
					g.SetDrawMode(Graphics.DrawMode.Normal);
				}
			}
			g.SetDrawMode(Graphics.DrawMode.Additive);
			for (int i = 0; i < this.mElectroBoltVector.Count; i++)
			{
				ElectroBolt electroBolt = this.mElectroBoltVector[i];
				Color color = (electroBolt.mCrossover ? GlobalMembersEffects.gCrossoverColors[this.mGemColor] : GlobalMembersEffects.gArcColors[this.mGemColor]);
				color.mAlpha = (int)((float)GlobalMembers.M(255) * this.mAlpha);
				g.SetColor(color);
				float num5 = 0f;
				float num6 = Piece.GetAngleRadius(electroBolt.mAngStart, this.mGemColor, theFrame);
				num6 = (float)((this.mCirclePct * GlobalMembers.MS(48.0) + (1.0 - this.mCirclePct) * (double)num6) * this.mRadiusScale);
				float num7 = (float)Math.Cos((double)electroBolt.mAngStart) * num6;
				float num8 = (float)Math.Sin((double)electroBolt.mAngStart) * num6;
				float num9 = num7;
				float num10 = num8;
				float num11 = Piece.GetAngleRadius(electroBolt.mAngEnd, this.mGemColor, theFrame);
				num11 = (float)((this.mCirclePct * GlobalMembers.MS(48.0) + (1.0 - this.mCirclePct) * (double)num11) * this.mRadiusScale);
				float num12 = (float)Math.Cos((double)electroBolt.mAngEnd) * num11;
				float num13 = (float)Math.Sin((double)electroBolt.mAngEnd) * num11;
				if (electroBolt.mHitOtherGem)
				{
					Piece pieceById = this.mFXManager.mBoard.GetPieceById(electroBolt.mHitOtherGemId);
					if (pieceById != null)
					{
						num11 = Piece.GetAngleRadius(electroBolt.mAngEnd, this.mGemColor, theFrame);
						num11 = (float)(this.mCirclePct * GlobalMembers.MS(48.0) + (1.0 - this.mCirclePct) * (double)num11);
						num12 = (pieceById.mX - (float)num2) / GlobalMembers.S(1f) + (float)Math.Cos((double)electroBolt.mAngEnd) * num11;
						num13 = (pieceById.mY - (float)num3) / GlobalMembers.S(1f) + (float)Math.Sin((double)electroBolt.mAngEnd) * num11;
					}
				}
				for (int j = 0; j < electroBolt.mNumMidPoints + 1; j++)
				{
					float num14 = (float)(j + 1) / (float)(electroBolt.mNumMidPoints + 1);
					float num15 = (float)((double)electroBolt.mAngStart * (1.0 - (double)num14) + (double)(electroBolt.mAngEnd * num14));
					float num16 = 0f;
					if (j < electroBolt.mNumMidPoints)
					{
						num16 = electroBolt.mMidPointsPos[j];
					}
					float num17 = Piece.GetAngleRadius(num15, this.mGemColor, theFrame);
					num17 = (float)((this.mCirclePct * GlobalMembers.MS(48.0) + (1.0 - this.mCirclePct) * (double)num17 + (double)num16) * this.mRadiusScale);
					float num18 = (float)Math.Cos((double)num15) * num17;
					float num19 = (float)Math.Sin((double)num15) * num17;
					if (electroBolt.mCrossover || electroBolt.mHitOtherGem)
					{
						float num20 = (float)Math.Atan2((double)(num13 - num8), (double)(num12 - num7));
						num18 = (float)((double)num9 * (1.0 - (double)num14) + (double)(num12 * num14));
						num19 = (float)((double)num10 * (1.0 - (double)num14) + (double)(num13 * num14));
						if (j < electroBolt.mNumMidPoints)
						{
							num18 += (float)Math.Sin((double)num20) * electroBolt.mMidPointsPos[j];
							num19 += (float)Math.Cos((double)num20) * electroBolt.mMidPointsPos[j];
						}
					}
					Color theColor = new Color(color);
					Color theColor2 = new Color(color);
					if (!electroBolt.mCrossover && !electroBolt.mHitOtherGem)
					{
						theColor.mAlpha = (int)Math.Max(2f, 255f * (1f - num5 * GlobalMembers.M(0.03f)));
						theColor2.mAlpha = (int)Math.Max(2f, 255f * (1f - num16 * GlobalMembers.M(0.03f)));
					}
					theColor.mAlpha = (int)((float)theColor.mAlpha * num);
					theColor2.mAlpha = (int)((float)theColor2.mAlpha * num);
					this.DrawElectroLine(g, GlobalMembersResourcesWP.IMAGE_ELECTROTEX, (float)GlobalMembers.S(num2 + 50) + GlobalMembers.S(num7), (float)GlobalMembers.S(num3 + 50) + GlobalMembers.S(num8), (float)GlobalMembers.S(num2 + 50) + GlobalMembers.S(num18), (float)GlobalMembers.S(num3 + 50) + GlobalMembers.S(num19), electroBolt.mHitOtherGem ? GlobalMembers.MS(8f) : (electroBolt.mCrossover ? GlobalMembers.MS(9f) : GlobalMembers.MS(6f)), theColor, theColor2);
					Color theColor3 = new Color(Color.White);
					Color theColor4 = new Color(Color.White);
					if (!electroBolt.mCrossover && !electroBolt.mHitOtherGem)
					{
						theColor3.mAlpha = (int)Math.Max(2f, 255f * (GlobalMembers.M(0.85f) - num5 * GlobalMembers.M(0.04f)));
						theColor4.mAlpha = (int)Math.Max(2f, 255f * (GlobalMembers.M(0.85f) - num16 * GlobalMembers.M(0.04f)));
					}
					if (electroBolt.mCrossover)
					{
						theColor3.mAlpha = (int)((double)theColor3.mAlpha * GlobalMembers.M(0.5));
						theColor4.mAlpha = (int)((double)theColor4.mAlpha * GlobalMembers.M(0.5));
					}
					theColor3.mAlpha = (int)((float)theColor3.mAlpha * num);
					theColor4.mAlpha = (int)((float)theColor4.mAlpha * num);
					this.DrawElectroLine(g, GlobalMembersResourcesWP.IMAGE_ELECTROTEX_CENTER, (float)GlobalMembers.S(num2 + 50) + GlobalMembers.S(num7), (float)GlobalMembers.S(num3 + 50) + GlobalMembers.S(num8), (float)GlobalMembers.S(num2 + 50) + GlobalMembers.S(num18), (float)GlobalMembers.S(num3 + 50) + GlobalMembers.S(num19), electroBolt.mHitOtherGem ? GlobalMembers.MS(8f) : (electroBolt.mCrossover ? GlobalMembers.MS(8f) : GlobalMembers.MS(6f)), theColor3, theColor4);
					num7 = num18;
					num8 = num19;
					num5 = num16;
				}
			}
			if (this.mTimeBonus > 0)
			{
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.SetFont(GlobalMembersResources.FONT_HEADER);
				((ImageFont)g.mFont).PushLayerColor("GLOW", new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255), (int)((double)((float)GlobalMembers.M(255) * 0.5f) * (1.0 + (double)((float)Math.Cos((double)((float)this.mFXManager.mBoard.mUpdateCnt * GlobalMembers.M(0.15f))))))));
				g.SetColorizeImages(true);
				g.SetColor(Color.White);
				Image imageById = GlobalMembersResourcesWP.GetImageById(888 + this.mGemColor);
				GlobalMembers.gGR.DrawImageCel(g, imageById, GlobalMembers.S(num2) - ConstantsWP.SPEEDBOARD_GEMNUMS_OFFSET, GlobalMembers.S(num3) - ConstantsWP.SPEEDBOARD_GEMNUMS_OFFSET, this.mTimeBonus / 5 - 1);
				if (mPieceRel != null && this.mFXManager.mBoard.GetTicksLeft() <= GlobalMembers.M(500) && this.mFXManager.mBoard.mGameTicks / GlobalMembers.M(18) % 2 == 0)
				{
					g.SetColor(new Color(GlobalMembers.M(255), GlobalMembers.M(200), GlobalMembers.M(200), GlobalMembers.M(255)));
					GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_WHITE, GlobalMembers.S(num2) - ConstantsWP.SPEEDBOARD_GEMNUMS_OFFSET, GlobalMembers.S(num3) - ConstantsWP.SPEEDBOARD_GEMNUMS_OFFSET, this.mTimeBonus / 5 - 1);
				}
				((ImageFont)g.mFont).PopLayerColor("GLOW");
			}
			if (mPieceRel != null && mPieceRel.mScale != 1.0)
			{
				Utils.PopScale(g);
			}
			g.PopColorMult();
		}

		private static SimpleObjectPool thePool_ = null;

		private static TriangleArray batchElectro_;

		private static TriangleArray batchCenter_;

		private bool mUpdated;

		public List<ElectroBolt> mElectroBoltVector = new List<ElectroBolt>();

		public int mGemColor;

		public int mTimeBonus;

		public CurvedVal mCirclePct = new CurvedVal();

		public CurvedVal mRadiusScale = new CurvedVal();

		private enum BatchType
		{
			Electro,
			Center
		}
	}
}
