using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class EffectsManager : Widget, IDisposable
	{
		public EffectsManager(Board theBoard, bool deferOverlay)
		{
			this.mBoard = theBoard;
			this.mDeferOverlay = deferOverlay;
			this.mApplyBoardTransformToDraw = false;
			this.mDisableMask = false;
			this.mIs3d = false;
			this.mAlpha = 1f;
			this.mUpdateCnt = 0;
			this.mX = (this.mY = 0);
			this.mClip = false;
			this.mMouseVisible = false;
			this.mHeightImageDirty = false;
			this.mHasAlpha = true;
			this.mRewindEffect = false;
			for (int i = 0; i < 24; i++)
			{
				this.mEffectList[i] = new List<Effect>();
			}
		}

		public EffectsManager(Board theBoard)
			: this(theBoard, false)
		{
		}

		public override void Dispose()
		{
			this.Clear();
			base.Dispose();
		}

		public void CreateDistortionMap()
		{
			DeviceImage deviceImage = this.mHeightImageSharedRT.GetCurrentLockImage();
			if (deviceImage != null && (deviceImage.mWidth != GlobalMembers.gApp.mScreenBounds.mWidth / 4 || deviceImage.mHeight != GlobalMembers.gApp.mScreenBounds.mHeight / 4))
			{
				this.mHeightImageSharedRT.Unlock();
				deviceImage = null;
			}
			if (deviceImage != null || !GlobalMembers.gIs3D)
			{
				return;
			}
			this.mHeightImageSharedRT.Lock(GlobalMembers.gApp.mScreenBounds.mWidth / 4, GlobalMembers.gApp.mScreenBounds.mHeight / 4, 8U, "HeightImage");
			this.ClearDistortionMap(null);
		}

		public void ClearDistortionMap(Graphics g)
		{
		}

		public DeviceImage GetHeightImage()
		{
			this.CreateDistortionMap();
			return this.mHeightImageSharedRT.GetCurrentLockImage();
		}

		public virtual void UpdateTypeEmber(int type)
		{
			for (int i = 0; i < this.mEffectList[type].Count; i++)
			{
				Effect effect = this.mEffectList[type][i];
				effect.mX += effect.mDX;
				effect.mY += effect.mDY;
				effect.mZ += effect.mDZ;
				effect.mDY += effect.mGravity;
				effect.mDX *= effect.mDXScalar;
				effect.mDY *= effect.mDYScalar;
				effect.mDZ *= effect.mDZScalar;
				if ((effect.mFlags & 4) != 0 && effect.mDelay > 0f)
				{
					effect.mDelay -= 0.01f;
					if (effect.mDelay <= 0f)
					{
						effect.mDelay = 0f;
					}
				}
				else
				{
					effect.mAlpha += effect.mDAlpha;
					effect.mScale += effect.mDScale;
				}
				effect.mScale += effect.mDScale;
				effect.mAngle += effect.mDAngle;
				if (effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT || effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
				{
					effect.mAlpha += effect.mDAlpha;
					if (effect.mAlpha >= 1f)
					{
						effect.mDeleteMe = true;
					}
					effect.mFrame = (int)(12f * effect.mAlpha);
				}
				else if (effect.mColor.mRed + effect.mColor.mGreen == 0)
				{
					effect.mScale += 0.02f;
					effect.mAlpha -= 0.01f;
					if (effect.mAlpha <= 0f)
					{
						effect.mDeleteMe = true;
					}
				}
				if (effect.mScale < effect.mMinScale)
				{
					effect.mDeleteMe = true;
					effect.mScale = effect.mMinScale;
				}
				else if (effect.mScale > effect.mMaxScale)
				{
					effect.mScale = effect.mMaxScale;
					if ((effect.mFlags & 1) != 0)
					{
						effect.mDScale = -effect.mDScale;
					}
				}
				if (effect.mAlpha > effect.mMaxAlpha)
				{
					effect.mAlpha = effect.mMaxAlpha;
					if ((effect.mFlags & 2) != 0)
					{
						effect.mDAlpha = -effect.mDAlpha;
					}
					else
					{
						effect.mDeleteMe = true;
					}
				}
				else if (effect.mAlpha <= 0f)
				{
					effect.mDeleteMe = true;
				}
			}
		}

		public virtual void DrawTypeEmber(Effect.Type type, Graphics g, bool isOverlay)
		{
			float num = this.mAlpha;
			if (this.mBoard != null)
			{
				num *= GlobalMembers.gApp.mBoard.GetAlpha();
			}
			int num2 = 1;
			if (type == Effect.Type.TYPE_EMBER_BOTTOM || type == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
			{
				num2 = 0;
			}
			g.SetDrawMode((num2 != 0) ? 1 : 0);
			for (int i = 0; i < this.mEffectList[(int)type].Count; i++)
			{
				Effect effect = this.mEffectList[(int)type][i];
				if (effect.mOverlay == isOverlay && !effect.mDeleteMe)
				{
					Piece mPieceRel = effect.mPieceRel;
					if (mPieceRel == null || mPieceRel.mAlpha > 0.0)
					{
						int num3 = 0;
						int num4 = 0;
						if (mPieceRel != null)
						{
							num3 = (int)mPieceRel.GetScreenX();
							num4 = (int)mPieceRel.GetScreenY();
							effect.mX += (float)num3;
							effect.mY += (float)num4;
						}
						float num5 = Math.Min(1f, effect.mAlpha) * num;
						g.SetColorizeImages(true);
						Color mColor = effect.mColor;
						mColor.mAlpha = (int)(200f * num5);
						g.SetColor(mColor);
						if (type == Effect.Type.TYPE_EMBER_FADEINOUT || type == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
						{
							float num6 = Math.Min(63f, 64f * effect.mAlpha);
							Color mColor2 = effect.mColor;
							mColor2.mAlpha = (int)((float)mColor2.mAlpha * this.mAlpha);
							Transform drawTypeEmberTrans = this.DrawTypeEmberTrans;
							drawTypeEmberTrans.Reset();
							Rect theSrcRect = default(Rect);
							drawTypeEmberTrans.Scale(effect.mScale, effect.mScale);
							if (effect.mAngle != 0f)
							{
								drawTypeEmberTrans.RotateRad(effect.mAngle);
							}
							g.SetColor(mColor2);
							theSrcRect = effect.mImage.GetCelRect((int)num6);
							GlobalMembers.gGR.DrawImageTransformF(g, effect.mImage, drawTypeEmberTrans, theSrcRect, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
						}
						else
						{
							Transform drawTypeEmberTrans2 = this.DrawTypeEmberTrans;
							drawTypeEmberTrans2.Reset();
							drawTypeEmberTrans2.Scale(effect.mScale, effect.mScale);
							drawTypeEmberTrans2.RotateRad(effect.mAngle);
							GlobalMembers.gGR.DrawImageTransformF(g, effect.mImage, drawTypeEmberTrans2, new Rect(0, 0, effect.mImage.mWidth, effect.mImage.mHeight), GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
						}
						if (mPieceRel != null)
						{
							effect.mX -= (float)num3;
							effect.mY -= (float)num4;
						}
					}
				}
			}
		}

		public virtual void GarbageCollectEffects(int type)
		{
			int count = this.mEffectList[type].Count;
			for (int i = count - 1; i >= 0; i--)
			{
				Effect effect = this.mEffectList[type][i];
				if (effect.mDeleteMe)
				{
					this.FreeEffect(effect);
					this.mEffectList[type].RemoveAt(i);
				}
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mBoard != null && (this.mBoard.mBoardHidePct == 1f || this.mBoard.mSuspendingGame))
			{
				return;
			}
			this.mUpdateCnt++;
			this.mIs3d = GlobalMembers.gApp.Is3DAccelerated();
			this.mWidth = GlobalMembers.gApp.mWidth;
			this.mHeight = GlobalMembers.gApp.mHeight;
			if (this.mDistortEffectList.Count != 0)
			{
				int count = this.mDistortEffectList.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					DistortEffect distortEffect = this.mDistortEffectList[i];
					bool flag = true;
					flag &= !distortEffect.mTexturePos.IncInVal();
					flag &= !distortEffect.mRadius.IncInVal();
					flag &= !distortEffect.mIntensity.IncInVal();
					flag &= !distortEffect.mMovePct.IncInVal();
					if (flag)
					{
						this.mDistortEffectList.RemoveAt(i);
					}
				}
			}
			int num = 0;
			this.UpdateTypeEmber(3);
			this.UpdateTypeEmber(4);
			this.UpdateTypeEmber(5);
			this.UpdateTypeEmber(6);
			for (int j = 0; j < 24; j++)
			{
				int num2 = 0;
				if (this.mUpdateCnt % 24 == j)
				{
					this.GarbageCollectEffects(j);
				}
				if (j < 3 || j > 6)
				{
					foreach (Effect effect in this.mEffectList[j])
					{
						effect.Update();
						num++;
						num2++;
						if (effect.mDoubleSpeed)
						{
							effect.Update();
						}
						int num3 = ((effect.mUpdateDiv == 0) ? 1 : effect.mUpdateDiv);
						effect.mX += effect.mDX / (float)num3;
						effect.mY += effect.mDY / (float)num3;
						effect.mZ += effect.mDZ / (float)num3;
						if (this.mUpdateCnt % num3 == 0)
						{
							effect.mDY += effect.mGravity;
							effect.mDX *= effect.mDXScalar;
							effect.mDY *= effect.mDYScalar;
							effect.mDZ *= effect.mDZScalar;
							if ((effect.mFlags & 4) != 0 && effect.mDelay > 0f)
							{
								effect.mDelay -= 0.01f;
								if (effect.mDelay <= 0f)
								{
									effect.mDelay = 0f;
								}
							}
							else
							{
								effect.mAlpha += effect.mDAlpha;
								effect.mScale += effect.mDScale;
							}
						}
						switch (effect.mType)
						{
						case Effect.Type.TYPE_NONE:
							if (effect.mUpdateDiv == 0 || this.mUpdateCnt % effect.mUpdateDiv == 0)
							{
								if (effect.mIsCyclingColor)
								{
									effect.mCurHue = (effect.mCurHue + 8) % 256;
									effect.mColor = new Color((int)GlobalMembers.gApp.HSLToRGB(effect.mCurHue, 255, 128));
								}
								effect.mAngle += effect.mDAngle;
							}
							break;
						case Effect.Type.TYPE_BLAST_RING:
							effect.mScale += 0.8f;
							effect.mAlpha -= 0.02f;
							break;
						case Effect.Type.TYPE_EMBER_BOTTOM:
						case Effect.Type.TYPE_EMBER:
						case Effect.Type.TYPE_EMBER_FADEINOUT:
							effect.mScale += effect.mDScale;
							effect.mAngle += effect.mDAngle;
							if (effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT || effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
							{
								effect.mAlpha += effect.mDAlpha;
								if (effect.mAlpha >= 1f)
								{
									effect.mDeleteMe = true;
								}
								effect.mFrame = (int)(12f * effect.mAlpha);
							}
							else if (effect.mColor.mRed + effect.mColor.mGreen == 0)
							{
								effect.mScale += 0.02f;
								effect.mAlpha -= 0.01f;
								if (effect.mAlpha <= 0f)
								{
									effect.mDeleteMe = true;
								}
							}
							break;
						case Effect.Type.TYPE_SMOKE_PUFF:
							effect.mDX *= 0.95f;
							break;
						case Effect.Type.TYPE_DROPLET:
							if (effect.mScale < 0f)
							{
								effect.mAlpha = 0f;
							}
							effect.mAngle = GlobalMembers.M(0f) + (float)Math.Atan2((double)effect.mDX, (double)effect.mDY);
							break;
						case Effect.Type.TYPE_STEAM_COMET:
							if (effect.mScale < GlobalMembers.M(0.1f))
							{
								effect.mAlpha = 0f;
							}
							else
							{
								if (this.mUpdateCnt % GlobalMembers.M(10) == GlobalMembers.M(9))
								{
									effect.mDX += GlobalMembers.M(1.3f) * GlobalMembersUtils.GetRandFloat();
									effect.mDY += GlobalMembers.M(1.3f) * GlobalMembersUtils.GetRandFloat();
								}
								if (this.mUpdateCnt % 2 != 0)
								{
									Effect effect2 = this.AllocEffect(Effect.Type.TYPE_STEAM);
									effect2.mDX = 0f;
									effect2.mDY = 0f;
									effect2.mX = effect.mX;
									effect2.mY = effect.mY;
									effect2.mIsAdditive = false;
									effect2.mScale = effect.mScale;
									effect2.mDScale = GlobalMembers.M(0f);
									this.AddEffect(effect2);
								}
							}
							break;
						case Effect.Type.TYPE_FRUIT_SPARK:
							effect.mDY += effect.mGravity;
							effect.mAngle += (this.mIs3d ? (effect.mDY * 0.02f) : 0f);
							effect.mScale += 0.05f;
							if (effect.mScale > 0.5f)
							{
								effect.mScale = 0.5f;
							}
							break;
						case Effect.Type.TYPE_GEM_SHARD:
							if (this.mUpdateCnt % 2 == 0)
							{
								effect.mFrame = (effect.mFrame + 1) % 40;
							}
							effect.mDX *= effect.mDecel;
							effect.mDY *= effect.mDecel;
							effect.mAngle += effect.mDAngle;
							effect.mValue[0] += effect.mValue[2];
							effect.mValue[1] += GlobalMembers.M(1f) * effect.mValue[3];
							break;
						case Effect.Type.TYPE_COUNTDOWN_SHARD:
						{
							effect.mAngle += effect.mDAngle;
							Effect effect3 = this.AllocEffect(Effect.Type.TYPE_SMOKE_PUFF);
							effect3.mScale *= effect.mScale * (GlobalMembers.M(0f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.45f));
							effect3.mAlpha *= GlobalMembers.M(0.3f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.05f);
							effect3.mAlpha *= effect.mAlpha;
							effect3.mAngle = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
							effect3.mDAngle = GlobalMembersUtils.GetRandFloat() * 3.14159274f * GlobalMembers.M(0.1f);
							effect3.mIsAdditive = GlobalMembers.M(0) > 0;
							effect3.mGravity = 0f;
							effect3.mDY = GlobalMembers.MS(-1f);
							effect3.mX = effect.mX;
							effect3.mY = effect.mY;
							this.AddEffect(effect3);
							if (effect.mScale < GlobalMembers.M(0.02f))
							{
								effect.mDeleteMe = true;
							}
							break;
						}
						case Effect.Type.TYPE_SPARKLE_SHARD:
							effect.mDX *= 0.98f;
							effect.mDY *= 0.98f;
							if (this.mUpdateCnt % effect.mUpdateDiv == 0)
							{
								effect.mFrame = (effect.mFrame + 1) % 40;
							}
							if (effect.mFrame == 14)
							{
								effect.mDeleteMe = true;
							}
							break;
						case Effect.Type.TYPE_STEAM:
						{
							float num4 = effect.mDX * effect.mDX + effect.mDY * effect.mDY;
							effect.mDX *= effect.mValue[2];
							if (num4 > effect.mValue[0] * effect.mValue[0])
							{
								effect.mDY *= effect.mValue[2];
							}
							else
							{
								effect.mDAlpha = effect.mValue[1];
							}
							effect.mAngle += effect.mDAngle;
							effect.mScale += effect.mDScale;
							break;
						}
						case Effect.Type.TYPE_GLITTER_SPARK:
							effect.mAlpha = ((BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(5) == 0) ? 1f : GlobalMembers.M(0.25f));
							break;
						case Effect.Type.TYPE_LIGHT:
							effect.mLightSize = effect.mScale;
							effect.mLightIntensity = effect.mAlpha;
							break;
						case Effect.Type.TYPE_WALL_ROCK:
							effect.mDX *= effect.mDecel;
							if (effect.mY > (float)GlobalMembers.RS(GlobalMembers.gApp.mHeight))
							{
								effect.mDeleteMe = true;
							}
							break;
						case Effect.Type.TYPE_QUAKE_DUST:
							if (effect.mAlpha >= 1f)
							{
								effect.mAlpha = 1f;
								effect.mDAlpha = -0.01f;
							}
							break;
						case Effect.Type.TYPE_HYPERCUBE_ENERGIZE:
							effect.mAngle += effect.mDAngle;
							break;
						}
						if (effect.mScale < effect.mMinScale)
						{
							effect.mDeleteMe = true;
							effect.mScale = effect.mMinScale;
						}
						else if (effect.mScale > effect.mMaxScale)
						{
							effect.mScale = effect.mMaxScale;
							if ((effect.mFlags & 1) != 0)
							{
								effect.mDScale = -effect.mDScale;
							}
						}
						if (effect.mAlpha > effect.mMaxAlpha)
						{
							effect.mAlpha = effect.mMaxAlpha;
							if ((effect.mFlags & 2) != 0)
							{
								effect.mDAlpha = -effect.mDAlpha;
							}
							else
							{
								effect.mDeleteMe = true;
							}
						}
						else if (effect.mAlpha <= 0f)
						{
							effect.mDeleteMe = true;
						}
						if (effect.mCurvedAlpha.HasBeenTriggered())
						{
							effect.mDeleteMe = true;
						}
						if (effect.mCurvedScale.HasBeenTriggered())
						{
							effect.mDeleteMe = true;
						}
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			this.DoDraw(g, false);
			if (this.mWidgetManager != null)
			{
				base.DeferOverlay(1);
				return;
			}
			if (this.mDeferOverlay)
			{
				this.DrawOverlay(g);
			}
		}

		public override void DrawOverlay(Graphics g)
		{
			if (this.mBoard != null && this.mAlpha > 0f && !this.mBoard.mKilling && !this.mBoard.mIsWholeGameReplay && !this.mBoard.mSuspendingGame && (this.mDistortEffectList.Count > 0 || (this.mHeightImageDirty && this.mWidgetManager.mImage == g.mDestImage)))
			{
				Graphics3D graphics3D = g.Get3D();
				if (graphics3D != null && graphics3D.SupportsPixelShaders())
				{
					this.RenderDistortEffects(g);
				}
				this.mHeightImageDirty = false;
			}
			this.DoDraw(g, true);
		}

		public void DoDraw(Graphics g, bool isOverlay)
		{
			if (this.mAlpha == 0f)
			{
				return;
			}
			if (this.mBoard != null && ((this.mBoard.mGameOverCount > 0 && this.mBoard.mSlideUIPct >= 1.0) || this.mBoard.mSuspendingGame))
			{
				return;
			}
			Graphics3D graphics3D = g.Get3D();
			if (this.mDisableMask && graphics3D != null)
			{
				graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_NONE);
			}
			this.DrawTypeEmber(Effect.Type.TYPE_EMBER_BOTTOM, g, isOverlay);
			this.DrawTypeEmber(Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM, g, isOverlay);
			this.DrawTypeEmber(Effect.Type.TYPE_EMBER, g, isOverlay);
			this.DrawTypeEmber(Effect.Type.TYPE_EMBER_FADEINOUT, g, isOverlay);
			for (int i = 0; i < 24; i++)
			{
				if (i < 3 || i > 6)
				{
					if (i == 21)
					{
						TimeBonusEffect.batchBegin();
					}
					foreach (Effect effect in this.mEffectList[i])
					{
						if (effect.mOverlay == isOverlay && !effect.mDeleteMe)
						{
							Piece mPieceRel = effect.mPieceRel;
							if (mPieceRel == null || mPieceRel.mAlpha > 0.0)
							{
								if (mPieceRel != null && effect.mType != Effect.Type.TYPE_PI && effect.mType != Effect.Type.TYPE_POPANIM && effect.mType != Effect.Type.TYPE_TIME_BONUS && effect.mType != Effect.Type.TYPE_CUSTOMCLASS)
								{
									effect.mX += mPieceRel.GetScreenX();
									effect.mY += mPieceRel.GetScreenY();
									if (this.mBoard != null && this.mBoard.mPostFXManager == this)
									{
										effect.mX += (float)this.mBoard.mSideXOff * this.mBoard.mSlideXScale;
									}
								}
								if (effect.mType != Effect.Type.TYPE_CUSTOMCLASS)
								{
									float num = Math.Min(1f, effect.mAlpha) * this.mAlpha;
									if (this.mBoard != null)
									{
										this.mAlpha *= GlobalMembers.gApp.mBoard.GetAlpha();
									}
									switch (effect.mType)
									{
									case Effect.Type.TYPE_NONE:
									{
										g.SetColorizeImages(true);
										Color mColor = effect.mColor;
										mColor.mAlpha = (int)(255f * num);
										g.SetColor(mColor);
										g.SetDrawMode(effect.mIsAdditive ? 1 : 0);
										if (effect.mImage != null)
										{
											Rect celRect = effect.mImage.GetCelRect(effect.mFrame);
											Utils.MyDrawImageRotated(g, effect.mImage, celRect, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, effect.mScale, effect.mScale);
											g.SetColorizeImages(false);
										}
										break;
									}
									case Effect.Type.TYPE_CUSTOMCLASS:
									case Effect.Type.TYPE_BLAST_RING:
									case Effect.Type.TYPE_CURSOR_RING:
										goto IL_DB7;
									case Effect.Type.TYPE_EMBER_BOTTOM:
									case Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM:
									case Effect.Type.TYPE_EMBER:
									case Effect.Type.TYPE_EMBER_FADEINOUT:
									{
										g.SetColorizeImages(true);
										Color mColor2 = effect.mColor;
										int num2 = ((GlobalMembers.M(1) != 0) ? GlobalMembers.M(1) : (mColor2.mRed + mColor2.mGreen + mColor2.mBlue));
										if (effect.mType == Effect.Type.TYPE_EMBER_BOTTOM || effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
										{
											num2 = 0;
										}
										g.SetDrawMode((num2 != 0) ? 1 : 0);
										mColor2.mAlpha = (int)(200f * num);
										g.SetColor(mColor2);
										if (effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT || effect.mType == Effect.Type.TYPE_EMBER_FADEINOUT_BOTTOM)
										{
											float num3 = (float)Math.Min(63, (int)(64f * effect.mAlpha));
											Color mColor3 = effect.mColor;
											mColor3.mAlpha = (int)((float)mColor3.mAlpha * this.mAlpha);
											Transform transform = new Transform();
											Rect theSrcRect = default(Rect);
											if (graphics3D != null)
											{
												transform.Scale(effect.mScale, effect.mScale);
												if (effect.mAngle != 0f)
												{
													transform.RotateRad(effect.mAngle);
												}
											}
											g.SetColor(mColor3);
											theSrcRect = effect.mImage.GetCelRect((int)num3);
											if (graphics3D != null)
											{
												GlobalMembers.gGR.DrawImageTransformF(g, effect.mImage, transform, theSrcRect, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
											}
											else
											{
												GlobalMembers.gGR.DrawImageTransform(g, effect.mImage, transform, theSrcRect, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
											}
										}
										else
										{
											Utils.MyDrawImageRotated(g, effect.mImage, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, effect.mScale, effect.mScale);
										}
										break;
									}
									case Effect.Type.TYPE_SMOKE_PUFF:
									case Effect.Type.TYPE_STEAM_COMET:
									{
										g.SetDrawMode(0);
										g.SetColorizeImages(true);
										Color mColor4 = effect.mColor;
										mColor4.mAlpha = (int)(255f * num);
										g.SetColor(mColor4);
										int mFrame = effect.mFrame;
										Utils.MyDrawImageRotated(g, effect.mImage, effect.mImage.GetCelRect(mFrame), GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, effect.mScale, effect.mScale);
										break;
									}
									case Effect.Type.TYPE_DROPLET:
									{
										g.SetDrawMode(0);
										g.SetColorizeImages(true);
										Color mColor5 = effect.mColor;
										mColor5.mAlpha = (int)(255f * num);
										g.SetColor(mColor5);
										float num4 = (float)Math.Sqrt((double)effect.mScale);
										int theCel = 0;
										Utils.MyDrawImageRotated(g, effect.mImage, effect.mImage.GetCelRect(theCel), GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, num4, num4);
										break;
									}
									case Effect.Type.TYPE_FRUIT_SPARK:
									{
										g.SetDrawMode(1);
										g.SetColorizeImages(true);
										Color color = new Color(255, 255, 255, (int)(255f * num));
										g.SetColor(color);
										Image image_GEM_FRUIT_SPARK = GlobalMembersResourcesWP.IMAGE_GEM_FRUIT_SPARK;
										float num5 = effect.mScale;
										num5 *= num;
										Utils.MyDrawImageRotated(g, image_GEM_FRUIT_SPARK, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, num5, num5);
										break;
									}
									case Effect.Type.TYPE_GEM_SHARD:
									{
										Color mColor6 = effect.mColor;
										float num6 = this.mAlpha * effect.mAlpha;
										if (num6 > 0f)
										{
											num6 = (float)Math.Sqrt((double)num6);
										}
										mColor6.mAlpha = (int)(num6 * GlobalMembers.M(64f));
										float num7 = 0.2f;
										float num8 = 0.3f;
										float num9 = num7 % num8;
										g.SetColorizeImages(true);
										g.SetColor(mColor6);
										float mScale = effect.mScale;
										EffectsManager.DD_aTrans.Reset();
										EffectsManager.DD_aTrans.RotateRad(effect.mAngle);
										float num10 = GlobalMembers.M(0.25f);
										float num11 = (float)Math.Sin((double)effect.mValue[0]);
										if (num11 > 0f && num11 < num10)
										{
											num11 = num10;
										}
										if (num11 < 0f && num11 > -num10)
										{
											num11 = -num10;
										}
										float num12 = (float)Math.Sin((double)effect.mValue[1]);
										if (num12 > 0f && num12 < num10)
										{
											num12 = num10;
										}
										if (num12 < 0f && num12 > -num10)
										{
											num12 = -num10;
										}
										EffectsManager.DD_aTrans.Scale(GlobalMembers.M(1.4f) * num11 * mScale, GlobalMembers.M(1.4f) * num12 * mScale);
										g.SetColorizeImages(true);
										g.SetDrawMode(0);
										EffectsManager.DD_aTrans.Scale(GlobalMembers.M(1.15f) * effect.mScale, GlobalMembers.M(1.15f) * effect.mScale);
										mColor6.mAlpha = (int)(num6 * GlobalMembers.M(255f));
										g.SetColor(mColor6);
										int num13 = effect.GetHashCode() * this.mUpdateCnt % 8;
										GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_SM_SHARDS, EffectsManager.DD_aTrans, new Rect(GlobalMembersResourcesWP.IMAGE_SM_SHARDS.GetCelWidth() * num13, 0, GlobalMembersResourcesWP.IMAGE_SM_SHARDS.GetCelWidth(), GlobalMembersResourcesWP.IMAGE_SM_SHARDS.GetCelHeight()), (float)((int)GlobalMembers.S(effect.mX)), (float)((int)GlobalMembers.S(effect.mY)));
										break;
									}
									case Effect.Type.TYPE_COUNTDOWN_SHARD:
									{
										g.SetColorizeImages(true);
										Color mColor7 = effect.mColor;
										mColor7.mAlpha = (int)(255f * num);
										g.SetColor(mColor7);
										g.SetDrawMode(0);
										if (effect.mImage != null)
										{
											Rect celRect2 = effect.mImage.GetCelRect(effect.mFrame, 0);
											Utils.MyDrawImageRotated(g, effect.mImage, celRect2, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, effect.mScale, effect.mScale);
											g.SetColorizeImages(false);
										}
										break;
									}
									case Effect.Type.TYPE_SPARKLE_SHARD:
									{
										Image image_SPARKLE = GlobalMembersResourcesWP.IMAGE_SPARKLE;
										g.SetDrawMode(1);
										g.SetColorizeImages(true);
										effect.mColor.mAlpha = (int)(255f * num);
										g.SetColor(effect.mColor);
										int num14 = (int)(effect.mScale * (float)image_SPARKLE.GetCelWidth());
										int num15 = (int)(effect.mScale * (float)image_SPARKLE.GetCelHeight());
										GlobalMembers.gGR.DrawImageCel(g, image_SPARKLE, new Rect((int)GlobalMembers.S(effect.mX) - num14 / 2, (int)GlobalMembers.S(effect.mY) - num15 / 2, num14, num15), effect.mFrame);
										g.SetColorizeImages(false);
										g.SetDrawMode(0);
										break;
									}
									case Effect.Type.TYPE_STEAM:
									{
										g.SetDrawMode(0);
										g.SetColorizeImages(true);
										Color mColor8 = effect.mColor;
										mColor8.mAlpha = (int)(255f * num);
										g.SetColor(mColor8);
										int mFrame2 = effect.mFrame;
										Utils.MyDrawImageRotated(g, effect.mImage, effect.mImage.GetCelRect(mFrame2), GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, effect.mScale, effect.mScale);
										break;
									}
									case Effect.Type.TYPE_GLITTER_SPARK:
									{
										g.SetDrawMode(1);
										g.SetColorizeImages(true);
										Color color2 = new Color(255, 255, 128, (int)(255f * num));
										g.SetColor(color2);
										Image image_GEM_FRUIT_SPARK2 = GlobalMembersResourcesWP.IMAGE_GEM_FRUIT_SPARK;
										float num16 = effect.mScale;
										num16 *= num;
										Utils.MyDrawImageRotated(g, image_GEM_FRUIT_SPARK2, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY), (double)effect.mAngle, num16, num16);
										break;
									}
									case Effect.Type.TYPE_LIGHT:
										break;
									case Effect.Type.TYPE_WALL_ROCK:
										g.SetColor(new Color(-1));
										g.SetDrawMode(0);
										g.DrawImageCel(effect.mImage, (int)(GlobalMembers.S(effect.mX) - (float)(effect.mImage.GetCelWidth() / 2)), (int)(GlobalMembers.S(effect.mY) - (float)(effect.mImage.GetCelHeight() / 2)), effect.mFrame);
										if (effect.mColor.ToInt() != -1)
										{
											g.SetColorizeImages(true);
											g.SetDrawMode(1);
											g.SetColor(effect.mColor);
											g.DrawImageCel(effect.mImage, (int)(GlobalMembers.S(effect.mX) - (float)(effect.mImage.GetCelWidth() / 2)), (int)(GlobalMembers.S(effect.mY) - (float)(effect.mImage.GetCelHeight() / 2)), effect.mFrame);
											g.SetDrawMode(0);
										}
										break;
									case Effect.Type.TYPE_QUAKE_DUST:
										g.SetColor(Color.White);
										g.SetDrawMode(0);
										g.SetColorizeImages(true);
										g.mColor.mAlpha = (int)(effect.mAlpha * 255f);
										g.DrawImage(effect.mImage, (int)GlobalMembers.S(effect.mX), (int)GlobalMembers.S(effect.mY));
										break;
									case Effect.Type.TYPE_HYPERCUBE_ENERGIZE:
									{
										g.SetDrawMode(1);
										g.SetColorizeImages(true);
										Transform transform2 = new Transform();
										transform2.Reset();
										int num17 = (int)(effect.mCurvedAlpha * (double)this.mAlpha);
										g.SetColor(new Color(num17, num17, num17));
										float num18 = (float)Math.Pow(effect.mCurvedScale, GlobalMembers.M(2.5));
										transform2.Scale((float)(effect.mCurvedAlpha / 160.0), (float)(effect.mCurvedAlpha / 320.0));
										transform2.RotateDeg(effect.mAngle * 0.1f * num18 + 15f);
										GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_HYPERFLARELINE, transform2, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
										transform2.Reset();
										transform2.Scale((float)(effect.mCurvedAlpha / 250.0), (float)(effect.mCurvedAlpha / 500.0));
										transform2.RotateDeg(-effect.mAngle * 0.2f * num18 - 45f);
										GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_HYPERFLARELINE, transform2, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
										transform2.Reset();
										transform2.Scale((float)(effect.mCurvedAlpha / 300.0), (float)(effect.mCurvedAlpha / 600.0));
										transform2.RotateDeg(effect.mAngle * 0.05f * num18 + 60f);
										GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_HYPERFLARELINE, transform2, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
										transform2.Reset();
										transform2.Scale((float)(effect.mCurvedScale * (double)GlobalMembers.M(0.6f)), (float)(effect.mCurvedScale * (double)GlobalMembers.M(0.6f)));
										GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_HYPERFLARERING, transform2, GlobalMembers.S(effect.mX), GlobalMembers.S(effect.mY));
										break;
									}
									default:
										goto IL_DB7;
									}
									IL_DBE:
									if (mPieceRel == null || effect.mType == Effect.Type.TYPE_PI || effect.mType == Effect.Type.TYPE_POPANIM || effect.mType == Effect.Type.TYPE_TIME_BONUS)
									{
										continue;
									}
									effect.mX -= mPieceRel.GetScreenX();
									effect.mY -= mPieceRel.GetScreenY();
									if (this.mBoard != null && this.mBoard.mPostFXManager == this)
									{
										effect.mX -= (float)(this.mBoard.mSideXOff * (double)this.mBoard.mSlideXScale);
										continue;
									}
									continue;
									IL_DB7:
									effect.Draw(g);
									goto IL_DBE;
								}
								effect.Draw(g);
							}
						}
					}
					if (i == 21)
					{
						TimeBonusEffect.batchEnd(g);
					}
				}
			}
		}

		public override void AddedToManager(WidgetManager theMgr)
		{
			base.AddedToManager(theMgr);
		}

		public bool IsEmpty()
		{
			for (int i = 0; i < 24; i++)
			{
				if (this.mEffectList[i].Count != 0)
				{
					return false;
				}
			}
			return true;
		}

		public Effect AllocEffect()
		{
			return Effect.alloc();
		}

		public Effect AllocEffect(Effect.Type theType)
		{
			return Effect.alloc(theType);
		}

		public void FreeEffect(Effect theEffect)
		{
			if (theEffect != null)
			{
				theEffect.release();
			}
		}

		public void AddEffect(Effect theEffect)
		{
			this.mEffectList[(int)theEffect.mType].Add(theEffect);
			theEffect.mFXManager = this;
		}

		public void FreePieceEffect(int thePieceId)
		{
			for (int i = 0; i < 24; i++)
			{
				List<Effect> list = this.mEffectList[i];
				for (int j = list.Count - 1; j >= 0; j--)
				{
					Effect effect = list[j];
					if (effect.mPieceRel != null && effect.mPieceRel.mId == thePieceId)
					{
						effect.Dispose();
						list.RemoveAt(j);
					}
				}
			}
		}

		public void FreePieceEffect(Piece piece)
		{
			for (int i = 0; i < 24; i++)
			{
				List<Effect> list = this.mEffectList[i];
				for (int j = list.Count - 1; j >= 0; j--)
				{
					Effect effect = list[j];
					if (effect.mPieceRel == piece)
					{
						effect.Dispose();
						list.RemoveAt(j);
					}
				}
			}
		}

		public void AddSteamExplosion(float theX, float theY, Color theColor)
		{
			for (int i = 0; i < GlobalMembers.M(12); i++)
			{
				Effect effect = this.AllocEffect(Effect.Type.TYPE_STEAM);
				float num = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
				float num2 = GlobalMembers.MS(0f) + GlobalMembers.MS(4f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
				effect.mDX = num2 * (float)Math.Cos((double)num);
				effect.mDY = num2 * (float)Math.Sin((double)num);
				effect.mX = theX + (float)Math.Cos((double)num) * GlobalMembers.M(25f);
				effect.mY = theY + (float)Math.Sin((double)num) * GlobalMembers.M(25f);
				effect.mIsAdditive = false;
				effect.mScale = GlobalMembers.M(0.1f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
				effect.mDScale = GlobalMembers.M(0.02f);
				this.AddEffect(effect);
			}
			for (int j = 0; j < GlobalMembers.M(12); j++)
			{
				Effect effect2 = this.AllocEffect(Effect.Type.TYPE_DROPLET);
				float num3 = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
				float num4 = GlobalMembers.MS(1f) + GlobalMembers.MS(5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
				effect2.mDX = num4 * (float)Math.Cos((double)num3);
				effect2.mDY = num4 * (float)Math.Sin((double)num3) + GlobalMembers.M(-1.5f);
				effect2.mX = theX + (float)Math.Cos((double)num3) * GlobalMembers.M(25f);
				effect2.mY = theY + (float)Math.Sin((double)num3) * GlobalMembers.M(25f);
				effect2.mIsAdditive = false;
				effect2.mScale = GlobalMembers.M(0.6f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.2f);
				effect2.mDScale = GlobalMembers.M(-0.01f);
				effect2.mAlpha = GlobalMembers.M(0.6f);
				effect2.mColor = new Color(11184844);
				this.AddEffect(effect2);
			}
			for (int k = 0; k < GlobalMembers.M(3); k++)
			{
				Effect effect3 = this.AllocEffect(Effect.Type.TYPE_STEAM_COMET);
				float num5 = (float)k * 3.14159274f / 3f + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.2f);
				float num6 = GlobalMembers.MS(6f) + GlobalMembers.MS(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
				effect3.mValue[0] = theX;
				effect3.mValue[1] = theY;
				effect3.mDX = num6 * (float)Math.Cos((double)num5);
				effect3.mDY = num6 * (float)Math.Sin((double)num5);
				effect3.mX = theX + (float)Math.Cos((double)num5) * GlobalMembers.M(25f);
				effect3.mY = theY + (float)Math.Sin((double)num5) * GlobalMembers.M(25f);
				effect3.mIsAdditive = false;
				effect3.mScale = GlobalMembers.M(2.5f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.1f);
				effect3.mDScale = GlobalMembers.M(-0.08f);
				this.AddEffect(effect3);
			}
			for (int l = 0; l < GlobalMembers.M(12); l++)
			{
				Effect effect4 = this.AllocEffect(Effect.Type.TYPE_GEM_SHARD);
				effect4.mColor = theColor;
				double num7 = (double)(GlobalMembersUtils.GetRandFloat() * 3.14159f);
				int num8 = (int)(GlobalMembersUtils.GetRandFloat() * (float)GlobalMembers.S(GlobalMembers.M(48)));
				effect4.mX = theX + (float)((int)(GlobalMembers.M(1f) * (float)num8 * (float)Math.Cos(num7)));
				effect4.mY = theY + (float)((int)(GlobalMembers.M(1f) * (float)num8 * (float)Math.Sin(num7)));
				num7 = (double)((float)Math.Atan2((double)(effect4.mY - theY), (double)(effect4.mX - theX)) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.3f));
				float num9 = GlobalMembers.MS(4.5f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.MS(1.5f);
				effect4.mDX = (float)Math.Cos(num7) * num9;
				effect4.mDY = (float)Math.Sin(num7) * num9 + GlobalMembers.MS(-2f);
				effect4.mDecel = GlobalMembers.M(0.99f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.005f);
				effect4.mAngle = (float)num7;
				effect4.mDAngle = GlobalMembers.M(0.05f) * GlobalMembersUtils.GetRandFloat();
				effect4.mGravity = GlobalMembers.M(0.06f);
				effect4.mValue[0] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
				effect4.mValue[1] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
				effect4.mValue[2] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
				effect4.mValue[3] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
				effect4.mDAlpha = GlobalMembers.M(-0.0025f) * (GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(4f));
				this.AddEffect(effect4);
			}
		}

		public DistortEffect AddHeatwave(float x, float y, float theScale)
		{
			DistortEffect distortEffect = new DistortEffect();
			distortEffect.mCenter = new FPoint(x, y);
			distortEffect.mRadius.mOutMax *= (double)theScale;
			this.mDistortEffectList.Add(distortEffect);
			return distortEffect;
		}

		public void Clear()
		{
			for (int i = 0; i < 24; i++)
			{
				this.mEffectList[i].GetEnumerator();
				this.mEffectList[i].Clear();
			}
		}

		public void RemovePieceFromEffects(Piece thePiece)
		{
			for (int i = 0; i < 24; i++)
			{
				foreach (Effect effect in this.mEffectList[i])
				{
					if (effect.mPieceRel == thePiece)
					{
						effect.mPieceRel = null;
						effect.mDeleteMe = true;
					}
				}
			}
		}

		public void BltDouble(Graphics g, Image theImage, FRect theDestRect, Color theColor, float theDestScale)
		{
			FRect frect = new FRect(theDestRect);
			float pu = frect.mX / (float)GlobalMembers.gApp.mScreenBounds.mWidth * theDestScale;
			float pu2 = (frect.mX + frect.mWidth) / (float)GlobalMembers.gApp.mScreenBounds.mWidth * theDestScale;
			float pv = frect.mY / (float)GlobalMembers.gApp.mScreenBounds.mHeight * theDestScale;
			float pv2 = (frect.mY + frect.mHeight) / (float)GlobalMembers.gApp.mScreenBounds.mHeight * theDestScale;
			SexyVertex2D[] theVertices = new SexyVertex2D[]
			{
				new SexyVertex2D(frect.mX - 0.5f, frect.mY - 0.5f, 0f, 0f, pu, pv),
				new SexyVertex2D(frect.mX + frect.mWidth - 0.5f, frect.mY - 0.5f, 1f, 0f, pu2, pv),
				new SexyVertex2D(frect.mX - 0.5f, frect.mY + frect.mHeight - 0.5f, 0f, 1f, pu, pv2),
				new SexyVertex2D(frect.mX + frect.mWidth - 0.5f, frect.mY + frect.mHeight - 0.5f, 1f, 1f, pu2, pv2)
			};
			g.Get3D().SetTexture(0, theImage);
			g.Get3D().DrawPrimitive(708U, Graphics3D.EPrimitiveType.PT_TriangleStrip, theVertices, 2, theColor, 0, 0f, 0f, true, 0U);
		}

		public void BltDoubleFromSrcRect(Graphics g, Image theImage, FRect theDestRect, FRect theSrcRect, Color theColor)
		{
			this.BltDoubleFromSrcRect(g, theImage, theDestRect, theSrcRect, theColor, 1f);
		}

		public void BltDoubleFromSrcRect(Graphics g, Image theImage, FRect theDestRect, FRect theSrcRect, Color theColor, float theDestScale)
		{
			float pu = theDestRect.mX / (float)this.mWidth * theDestScale;
			float pu2 = (theDestRect.mX + theDestRect.mWidth) / (float)this.mWidth * theDestScale;
			float pv = theDestRect.mY / (float)this.mHeight * theDestScale;
			float pv2 = (theDestRect.mY + theDestRect.mHeight) / (float)this.mHeight * theDestScale;
			float pu3 = (theSrcRect.mX + g.mTransX) / (float)GlobalMembers.gApp.mScreenBounds.mWidth;
			float pu4 = (theSrcRect.mX + g.mTransX + theSrcRect.mWidth) / (float)GlobalMembers.gApp.mScreenBounds.mWidth;
			float pv3 = theSrcRect.mY / (float)GlobalMembers.gApp.mScreenBounds.mHeight;
			float pv4 = (theSrcRect.mY + theSrcRect.mHeight) / (float)GlobalMembers.gApp.mScreenBounds.mHeight;
			SexyVertex2D[] theVertices = new SexyVertex2D[]
			{
				new SexyVertex2D(theDestRect.mX + g.mTransX - 0.5f, theDestRect.mY - 0.5f, pu3, pv3, pu, pv),
				new SexyVertex2D(theDestRect.mX + g.mTransX + theDestRect.mWidth - 0.5f, theDestRect.mY - 0.5f, pu4, pv3, pu2, pv),
				new SexyVertex2D(theDestRect.mX + g.mTransX - 0.5f, theDestRect.mY + theDestRect.mHeight - 0.5f, pu3, pv4, pu, pv2),
				new SexyVertex2D(theDestRect.mX + g.mTransX + theDestRect.mWidth - 0.5f, theDestRect.mY + theDestRect.mHeight - 0.5f, pu4, pv4, pu2, pv2)
			};
			g.Get3D().SetTexture(0, theImage);
			g.Get3D().DrawPrimitive(708U, Graphics3D.EPrimitiveType.PT_TriangleStrip, theVertices, 2, theColor, 0, 0f, 0f, true, 0U);
		}

		public void RenderDistortEffects(Graphics g)
		{
			DeviceImage heightImage = this.GetHeightImage();
			if (heightImage == null)
			{
				return;
			}
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D == null || !graphics3D.SupportsPixelShaders())
			{
				return;
			}
			Graphics graphics = new Graphics(heightImage);
			Graphics3D graphics3D2 = graphics.Get3D();
			graphics.PushState();
			graphics.mTransX = 0f;
			graphics.mTransY = 0f;
			graphics3D2.SetTextureLinearFilter(0, true);
			graphics3D2.SetTextureLinearFilter(1, true);
			graphics3D2.SetTextureWrap(0, true);
			graphics3D2.SetTextureWrap(1, true);
			graphics.SetColor(new Color(128, 128, 128, 255));
			graphics.FillRect(0, 0, heightImage.mWidth, heightImage.mHeight);
			graphics.SetColorizeImages(true);
			graphics.SetColor(Color.White);
			RenderEffect effect = graphics3D2.GetEffect(GlobalMembersResourcesWP.EFFECT_WAVE);
			using (RenderEffectAutoState renderEffectAutoState = new RenderEffectAutoState(graphics, effect))
			{
				while (!renderEffectAutoState.IsDone())
				{
					foreach (DistortEffect distortEffect in this.mDistortEffectList)
					{
						float num = (float)(distortEffect.mIntensity * (double)this.mAlpha);
						float num2 = (float)distortEffect.mTexturePos;
						float[] array = new float[]
						{
							Math.Max(1f - Math.Abs(num2) * 3f, 0f) * num,
							Math.Max(1f - Math.Abs(num2 - 0.333f) * 3f, 0f) * num,
							Math.Max(1f - Math.Abs(num2 - 0.667f) * 3f, 0f) * num,
							Math.Max(1f - Math.Abs(num2 - 1f) * 3f, 0f) * num
						};
						float num3 = (float)distortEffect.mRadius;
						new Rect((int)(GlobalMembers.S((double)distortEffect.mCenter.mX + (double)distortEffect.mMoveDelta.mX * distortEffect.mMovePct - (double)num3) / (double)GlobalMembers.M(4f)), (int)(GlobalMembers.S((double)distortEffect.mCenter.mY + (double)distortEffect.mMoveDelta.mY * distortEffect.mMovePct - (double)num3) / (double)GlobalMembers.M(4f)), (int)(GlobalMembers.S(num3) / 2f), (int)(GlobalMembers.S(num3) / 2f));
						Color color = new Color((int)(array[0] * 255f), (int)(array[1] * 255f), (int)(array[2] * 255f), (int)(array[3] * 255f));
						graphics.SetColor(color);
					}
					renderEffectAutoState.NextPass();
				}
			}
			graphics.PopState();
			if (this.mDistortEffectList.Count > 0 || GlobalMembers.M(0) != 0)
			{
				SharedRenderTarget sharedRenderTarget = new SharedRenderTarget();
				Image image = sharedRenderTarget.LockScreenImage("DistortFull");
				g.PushState();
				g.mTransX = 0f;
				g.mTransY = 0f;
				g.SetColor(Color.White);
				graphics3D.SetBlend(Graphics3D.EBlendMode.BLEND_ONE, Graphics3D.EBlendMode.BLEND_ZERO);
				g.DrawImage(image, 0, 0);
				graphics3D.SetTexture(1, heightImage);
				graphics3D.SetTextureLinearFilter(0, true);
				graphics3D.SetTextureLinearFilter(1, true);
				effect = graphics3D.GetEffect(GlobalMembersResourcesWP.EFFECT_SCREEN_DISTORT);
				float[] array2 = new float[4];
				array2[0] = (array2[1] = GlobalMembers.M(0.02f));
				effect.SetVector4("Params", array2);
				Rect rect = new Rect(0, 0, image.mWidth, image.mHeight);
				using (RenderEffectAutoState renderEffectAutoState2 = new RenderEffectAutoState(g, effect))
				{
					while (!renderEffectAutoState2.IsDone())
					{
						foreach (DistortEffect distortEffect2 in this.mDistortEffectList)
						{
							distortEffect2.mTexturePos;
							float num4 = (float)distortEffect2.mRadius;
							Rect rect2 = new Rect((int)GlobalMembers.S((double)distortEffect2.mCenter.mX + (double)distortEffect2.mMoveDelta.mX * distortEffect2.mMovePct - (double)num4), (int)GlobalMembers.S((double)distortEffect2.mCenter.mY + (double)distortEffect2.mMoveDelta.mY * distortEffect2.mMovePct - (double)num4), (int)(GlobalMembers.S(num4) * 2f), (int)(GlobalMembers.S(num4) * 2f));
							rect2 = rect.Intersection(rect2);
							g.DrawImage(image, rect2, rect2);
						}
						renderEffectAutoState2.NextPass();
					}
				}
				graphics3D.SetTexture(1, null);
				sharedRenderTarget.Unlock();
				g.PopState();
				return;
			}
			SharedRenderTarget sharedRenderTarget2 = new SharedRenderTarget();
			Image theImage = sharedRenderTarget2.LockScreenImage("DistortQuadA");
			graphics3D.SetTextureLinearFilter(0, true);
			graphics3D.SetTextureLinearFilter(1, true);
			effect = graphics3D.GetEffect(GlobalMembersResourcesWP.EFFECT_SCREEN_DISTORT);
			float[] array3 = new float[4];
			array3[0] = (array3[1] = GlobalMembers.M(0.02f));
			effect.SetVector4("Params", array3);
			graphics3D.SetTexture(1, heightImage);
			int num5 = ((this.mBoard != null) ? this.mBoard.mDistortionQuads.Count : 0);
			using (RenderEffectAutoState renderEffectAutoState3 = new RenderEffectAutoState(g, effect))
			{
				while (!renderEffectAutoState3.IsDone())
				{
					for (int i = 0; i < num5; i++)
					{
						Board.DistortionQuad distortionQuad = this.mBoard.mDistortionQuads[i];
						FRect frect = new FRect(distortionQuad.x1, distortionQuad.y1, distortionQuad.x2 - distortionQuad.x1, distortionQuad.y2 - distortionQuad.y1);
						this.BltDoubleFromSrcRect(g, theImage, frect, frect, new Color(255, 255, 255, 64));
					}
					renderEffectAutoState3.NextPass();
				}
			}
			sharedRenderTarget2.Unlock();
			graphics3D.SetTexture(1, null);
			theImage = sharedRenderTarget2.LockScreenImage("DistortQuadB");
			for (int j = 0; j < num5; j++)
			{
				Board.DistortionQuad distortionQuad2 = this.mBoard.mDistortionQuads[j];
				FRect frect2 = new FRect(distortionQuad2.x1, distortionQuad2.y1, distortionQuad2.x2 - distortionQuad2.x1, distortionQuad2.y2 - distortionQuad2.y1);
				this.BltDoubleFromSrcRect(g, theImage, frect2, frect2, new Color(255, 255, 255, 255));
			}
			sharedRenderTarget2.Unlock();
		}

		public Board mBoard;

		public bool mDeferOverlay;

		public List<Effect>[] mEffectList = new List<Effect>[24];

		public List<DistortEffect> mDistortEffectList = new List<DistortEffect>();

		public bool mApplyBoardTransformToDraw;

		public bool mDisableMask;

		public bool mDoDistorts;

		public bool mIs3d;

		public SharedRenderTarget mHeightImageSharedRT = new SharedRenderTarget();

		public bool mHeightImageDirty;

		public bool mRewindEffect;

		public float mAlpha;

		public new int mUpdateCnt;

		private Transform DrawTypeEmberTrans = new Transform();

		private static Transform DD_aTrans = new Transform();
	}
}
