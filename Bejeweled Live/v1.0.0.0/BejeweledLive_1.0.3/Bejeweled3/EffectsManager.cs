using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Sexy;

namespace Bejeweled3
{
	public class EffectsManager : InterfaceWidget
	{
		public void CreateDistortionMap()
		{
		}

		public void ClearDistortionMap(Graphics g)
		{
		}

		public DDImage GetHeightImage()
		{
			this.CreateDistortionMap();
			this.mHeightImageUsed = true;
			return this.mHeightImage;
		}

		public EffectsManager(BoardBejLive theBoard)
			: base(theBoard.GetApp())
		{
			this.mBoard = theBoard;
			this.Reset();
		}

		public new void Reset()
		{
			base.Reset();
			this.mApplyBoardTransformToDraw = false;
			this.mDisableMask = false;
			this.mIs3d = false;
			this.mAlpha = 1f;
			this.mX = (this.mY = 0);
			this.mClip = false;
			this.mMouseVisible = false;
			this.mHeightImage = null;
			this.mHeightImageUsed = false;
			this.mHeightImageDirty = false;
			this.mHasAlpha = true;
			this.mRewindEffect = false;
			for (int i = 0; i < this.mEffectMap.Count; i++)
			{
				this.mEffectMap[i].PrepareForReuse();
			}
			this.mEffectMap.Clear();
		}

		public override void Dispose()
		{
			this.Clear();
			if (this.mHeightImage != null)
			{
				this.mHeightImage = null;
			}
			base.Dispose();
		}

		public override void Update()
		{
			base.Update();
			this.mIs3d = this.mApp.Is3DAccelerated();
			this.mWidth = this.mApp.mWidth;
			this.mHeight = this.mApp.mHeight;
			for (int i = 0; i < this.mEffectMap.Count; i++)
			{
				EffectBej3 value = this.mEffectMap[i].Value;
				if (value.mType == EffectBej3.EffectType.TYPE_CUSTOMCLASS)
				{
					value.Update();
				}
				else
				{
					int num = ((value.mUpdateDiv == 0) ? 1 : value.mUpdateDiv);
					value.mX += value.mDX / (float)num;
					value.mY += value.mDY / (float)num;
					value.mZ += value.mDZ / (float)num;
					if (this.mUpdateCnt % num == 0)
					{
						value.mDY += value.mGravity;
						value.mDX *= value.mDXScalar;
						value.mDY *= value.mDYScalar;
						value.mDZ *= value.mDZScalar;
						if ((value.mFlags & 4) != 0 && value.mDelay > 0f)
						{
							value.mDelay -= 0.01f;
							if (value.mDelay <= 0f)
							{
								value.mDelay = 0f;
							}
						}
						else
						{
							value.mAlpha += value.mDAlpha;
							value.mScale += value.mDScale;
						}
					}
					switch (value.mType)
					{
					case EffectBej3.EffectType.TYPE_EXPLOSION:
						if (value.mDelay > 0f)
						{
							value.mDelay -= 1f;
						}
						else if (this.mUpdateCnt % 2 == 0)
						{
							value.mFrame++;
							this.MarkDirty();
							if (value.mFrame == 20)
							{
								value.mDeleteMe = true;
							}
						}
						break;
					case EffectBej3.EffectType.TYPE_GEM_SHARD:
						if (this.mUpdateCnt % 2 == 0)
						{
							value.mFrame = (value.mFrame + 1) % 40;
						}
						value.mDX *= value.mDecel;
						value.mDY *= value.mDecel;
						value.mAngle += value.mDAngle;
						value.mValue[0] += value.mValue[2];
						value.mValue[1] += Constants.mConstants.M(1f) * value.mValue[3];
						break;
					case EffectBej3.EffectType.TYPE_SPARKLE_SHARD:
						value.mDX *= 0.98f;
						value.mDY *= 0.98f;
						if (this.mUpdateCnt % value.mUpdateDiv == 0)
						{
							value.mFrame = (value.mFrame + 1) % 40;
						}
						if (value.mFrame == 14)
						{
							value.mDeleteMe = true;
						}
						break;
					case EffectBej3.EffectType.TYPE_GLITTER_SPARK:
						value.mAlpha = (((float)Board.Rand() % Constants.mConstants.M(5f) == 0f) ? 1f : Constants.mConstants.M(0.25f));
						break;
					}
					if (value.mScale < value.mMinScale)
					{
						value.mDeleteMe = true;
						value.mScale = value.mMinScale;
					}
					else if (value.mScale > value.mMaxScale)
					{
						value.mScale = value.mMaxScale;
						if ((value.mFlags & 1) != 0)
						{
							value.mDScale = -value.mDScale;
						}
					}
					if (value.mAlpha > value.mMaxAlpha)
					{
						value.mAlpha = value.mMaxAlpha;
						if ((value.mFlags & 2) != 0)
						{
							value.mDAlpha = -value.mDAlpha;
						}
						else
						{
							value.mDeleteMe = true;
						}
					}
					else if (value.mAlpha <= 0f)
					{
						value.mDeleteMe = true;
					}
					if (value.mCurvedAlpha.HasBeenTriggered())
					{
						value.mDeleteMe = true;
					}
					if (value.mCurvedScale.HasBeenTriggered())
					{
						value.mDeleteMe = true;
					}
				}
				if (value.mDeleteMe)
				{
					this.FreeEffect(value);
					this.mEffectMap[i].PrepareForReuse();
					this.mEffectMap.RemoveAt(i);
				}
			}
		}

		public override void Draw(Graphics g)
		{
			this.DoDraw(g, false);
			if (this.mWidgetManager != null)
			{
				base.DeferOverlay(1);
			}
		}

		public override void DrawOverlay(Graphics g)
		{
			this.DoDraw(g, true);
		}

		public void DoDraw(Graphics g, bool isOverlay)
		{
			for (int i = 0; i < 2; i++)
			{
				Graphics.DrawMode drawMode = ((i == 0) ? Graphics.DrawMode.DRAWMODE_NORMAL : Graphics.DrawMode.DRAWMODE_ADDITIVE);
				foreach (EffectTypePair effectTypePair in this.mEffectMap)
				{
					EffectBej3 value = effectTypePair.Value;
					if (value.mOverlay == isOverlay)
					{
						if (value.mType == EffectBej3.EffectType.TYPE_CUSTOMCLASS)
						{
							value.Draw(g);
						}
						else
						{
							if (this.mBoard != null)
							{
								this.mAlpha *= 1f - this.mBoard.mBoardHidePct;
							}
							switch (value.mType)
							{
							case EffectBej3.EffectType.TYPE_EXPLOSION:
								if (value.mFrame > 0 && drawMode == Graphics.DrawMode.DRAWMODE_ADDITIVE)
								{
									g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
									g.DrawImage(AtlasResources.IMAGE_EXPLOSION, (int)((double)value.mX - Constants.mConstants.BoardBej2_Explosion_HalfSize), (int)((double)value.mY - Constants.mConstants.BoardBej2_Explosion_HalfSize), new TRect(Constants.mConstants.BoardBej2_Explosion_Size(value.mFrame), 0, Constants.mConstants.BoardBej2_Explosion_SizeFull, Constants.mConstants.BoardBej2_Explosion_SizeFull));
								}
								break;
							case EffectBej3.EffectType.TYPE_GEM_SHARD:
								if (drawMode == Graphics.DrawMode.DRAWMODE_NORMAL)
								{
									g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
									g.SetColorizeImages(true);
									g.SetColor(value.GetAlphaColor());
									g.DrawImage(AtlasResources.IMAGE_SHARD, (int)value.mX, (int)value.mY, new TRect(Constants.mConstants.BoardBej2_Shard_X_ForFrame(value.mFrame), 0, Constants.mConstants.BoardBej2_Shard_Size, Constants.mConstants.BoardBej2_Shard_Size));
									g.SetColorizeImages(false);
								}
								break;
							case EffectBej3.EffectType.TYPE_SPARKLE_SHARD:
								if (drawMode == Graphics.DrawMode.DRAWMODE_ADDITIVE)
								{
									g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
									g.SetColorizeImages(true);
									g.SetColor(value.mColor);
									g.DrawImage(AtlasResources.IMAGE_SPARKLE, (int)value.mX - Constants.mConstants.BoardBej2_Sparkle_HalfSize, (int)value.mY - Constants.mConstants.BoardBej2_Sparkle_HalfSize, new TRect(Constants.mConstants.BoardBej2_Sparkle_Frame(value.mFrame), 0, Constants.mConstants.BoardBej2_Sparkle_Dimension, Constants.mConstants.BoardBej2_Sparkle_Dimension));
									g.SetColorizeImages(false);
								}
								break;
							}
						}
					}
				}
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		public override void AddedToManager(WidgetManager theMgr)
		{
			base.AddedToManager(theMgr);
		}

		public bool IsEmpty()
		{
			return this.mEffectMap.size<EffectTypePair>() == 0;
		}

		public EffectBej3 AllocEffect(EffectBej3.EffectType theType)
		{
			return EffectBej3.GetNewEffectBej3(theType);
		}

		public void FreeEffect(EffectBej3 theEffect)
		{
			theEffect.PrepareForReuse();
		}

		public void AddEffect(EffectBej3 theEffect)
		{
			this.mEffectMap.Add(EffectTypePair.GetNewEffectTypePair(theEffect.mType, theEffect));
			theEffect.mFXManager = this;
		}

		public void AddSteamExplosion(float theX, float theY, Color theColor)
		{
		}

		public void Clear()
		{
			foreach (EffectTypePair effectTypePair in this.mEffectMap)
			{
				effectTypePair.Value.PrepareForReuse();
			}
			this.mEffectMap.Clear();
		}

		public void BltDouble(Graphics g, Image theImage, ref TRectDouble theDestRect, Color theColor)
		{
			this.BltDouble(g, theImage, ref theDestRect, theColor, 1f);
		}

		public void BltDouble(Graphics g, Image theImage, ref TRectDouble theDestRect, Color theColor, float theDestScale)
		{
		}

		public void BltDoubleFromSrcRect(Graphics g, Image theImage, TRectDouble theDestRect, TRectDouble theSrcRect, Color theColor)
		{
			this.BltDoubleFromSrcRect(g, theImage, theDestRect, theSrcRect, theColor, 1f);
		}

		public void BltDoubleFromSrcRect(Graphics g, Image theImage, TRectDouble theDestRect, TRectDouble theSrcRect, Color theColor, float theDestScale)
		{
		}

		public void RenderDistortEffects(Graphics g)
		{
		}

		public BoardBejLive mBoard;

		public List<EffectTypePair> mEffectMap = new List<EffectTypePair>(100);

		public bool mApplyBoardTransformToDraw;

		public bool mDisableMask;

		public bool mDoDistorts;

		public bool mIs3d;

		public DDImage mHeightImage;

		public bool mHeightImageUsed;

		public bool mHeightImageDirty;

		public bool mRewindEffect;

		public float mAlpha;
	}
}
