using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public class EffectOverlay : InterfaceWidget
	{
		public EffectOverlay(GameApp app)
			: base(app)
		{
			if (this.mApp.mTextEffectsImage == null)
			{
				this.mApp.mTextEffectsImage = new MemoryImage();
				this.mApp.mTextEffectsImage.Create(GameConstants.BIGTEXT_WIDTH, GameConstants.BIGTEXT_HEIGHT, PixelFormat.kPixelFormat_RGBA4444);
			}
			this.mTextImage = this.mApp.mTextEffectsImage;
			this.mTextWidth = 0;
			this.mMouseVisible = false;
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mPortalOpening = false;
			this.mPortalPercent = 0f;
			this.mFaceFlashPct = 0f;
			this.mTextState = 0;
			this.mFaceState = 0;
			this.mFaceLinePct = 0f;
			this.mFaceAnimUpdateCnt = 0;
			this.mFaceLastClickTick = 0;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void DoUpdate()
		{
			if (this.mApp.FocusPaused())
			{
				return;
			}
			base.Update();
			if (this.mTextState != 0 && (this.mApp.mBoard == null || this.mApp.mBoard.mPauseCount <= 0) && this.mApp.mDialogMap.empty<int, Dialog>())
			{
				if (this.mRigidness < 1f)
				{
					this.mRigidness += 0.04f;
				}
				float num = -this.mStrechiness;
				this.mStrechinessAdd += num * (0.002f + this.mRigidness * 0.01f);
				this.mStrechinessAdd *= 0.975f - this.mRigidness * 0.12f;
				this.mStrechiness += this.mStrechinessAdd;
				if (Math.Abs(this.mStrechiness) < 0.001f && Math.Abs(this.mStrechinessAdd) < 0.0001f)
				{
					this.mStrechiness = 0f;
					this.mStrechinessAdd = 0f;
				}
				if (this.mOutDelay > 0)
				{
					this.mOutDelay--;
				}
				else
				{
					this.mShrinkDelay--;
					this.mStayDelay--;
					if (this.mStayDelay > 0)
					{
						num = 1.2f - this.mScale;
						this.mScaleAdd += num * 0.0015f;
					}
					else if (this.mShrinkDelay > 0)
					{
						num = 1.75f - this.mScale;
						this.mScaleAdd += num * 0.0015f;
					}
					else
					{
						num = 0f - this.mScale;
						this.mScaleAdd += num * 0.002f;
					}
					this.mScale += this.mScaleAdd;
				}
				if (this.mFadeDelay > 0)
				{
					this.mFadeDelay--;
					this.mTextFade += 0.02f;
					if (this.mTextFade > 1f)
					{
						this.mTextFade = 1f;
					}
				}
				else
				{
					this.mTextFade -= 0.03f;
				}
				if (this.mTextFade < 0f || this.mScale <= 0f)
				{
					this.mTextState = 0;
				}
				this.MarkDirty();
			}
			if (this.mPortalOpening)
			{
				if (this.mPortalDelay > 0)
				{
					this.mPortalDelay--;
					if (this.mPortalDelay == 0)
					{
						this.mApp.PlaySample(Resources.SOUND_MAIN_GAME_START);
					}
				}
				else
				{
					if (this.mApp.mBoard.mUpdateCnt == 0)
					{
						this.mApp.mBoard.Update();
					}
					this.mPortalPercent += 0.02f;
					if (this.mPortalPercent > 1f)
					{
						this.mPortalOpening = false;
						this.mApp.InterfaceTransactionDown(this.mApp.mBoard);
						this.mApp.mBoard.Show();
					}
				}
				this.MarkDirty();
			}
			if (this.mFaceStateDelay > 0)
			{
				this.mFaceStateDelay--;
			}
			if (this.mFaceState != 0)
			{
				this.mFaceUpdateCnt++;
				this.MarkDirty();
			}
			if (this.mFaceState == 1 && this.mFaceStateDelay == 0)
			{
				if (this.mUpdateCnt % 4 == 0)
				{
					this.mFaceBarSize++;
				}
				if (this.mFaceBarSize == 56)
				{
					this.mFaceState = 2;
					this.mFaceStateDelay = 0;
				}
			}
			if (this.mFaceState == 2)
			{
				if (this.mFaceStateDelay == 0)
				{
					for (int i = 0; i < this.mFaceTinyGemVector.Count; i++)
					{
						TinyGem tinyGem = this.mFaceTinyGemVector[i];
						if (tinyGem.mY < tinyGem.mDestY)
						{
							tinyGem.mVelY += 0.02f;
							tinyGem.mY += tinyGem.mVelY;
							if (tinyGem.mY >= tinyGem.mDestY)
							{
								tinyGem.mY = tinyGem.mDestY;
								if (this.mUpdateCnt - this.mFaceLastClickTick >= 10)
								{
									this.mApp.PlaySample(Resources.SOUND_GEM_HIT);
									this.mFaceLastClickTick = this.mUpdateCnt;
								}
							}
						}
					}
					if (this.mFaceUpdateCnt == 760)
					{
						this.mFaceState = 3;
						this.mFaceStateDelay = 0;
					}
				}
			}
			else if (this.mFaceState == 3)
			{
				if (this.mFaceStateDelay == 0)
				{
					this.mFaceLinePct += 0.015f;
					if (this.mFaceLinePct > 1f)
					{
						this.mFaceLinePct = 1f;
						this.mFaceState = 4;
						this.mFaceStateDelay = 10;
					}
				}
			}
			else if (this.mFaceState == 4)
			{
				if (this.mFaceStateDelay == 0)
				{
					this.mApp.PlaySample(Resources.SOUND_HYPERGEM_CREATE);
					this.mFaceFlashPct = 1f;
					this.mFaceTinyGemVector.Clear();
					this.mFaceState = 5;
					this.mFaceStateDelay = 50;
				}
			}
			else if (this.mFaceState == 5)
			{
				if (this.mFaceStateDelay == 0)
				{
					this.mFaceStateDelay = 200;
					this.mFaceState = 6;
				}
			}
			else if (this.mFaceState == 6)
			{
				if (this.mFaceStateDelay == 0)
				{
					this.mFaceAnimUpdateCnt++;
					if (this.mFaceAnimUpdateCnt == 180)
					{
						this.mApp.PlaySample(Resources.SOUND_EXCELLENT);
					}
					int num2 = this.mFaceAnimUpdateCnt;
					int num3 = 0;
					while (num2 > 0 && this.gFaceAnimTable[num3, 3] != -1 && num2 >= this.gFaceAnimTable[num3, 3])
					{
						num2 -= this.gFaceAnimTable[num3, 3];
						num3++;
					}
					if (this.gFaceAnimTable[num3, 3] != -1)
					{
						if (num2 == 0)
						{
							this.mFacePointsFrom = this.mFaceCurPoints;
						}
						float theFactor = (float)(num2 + 1) / (float)this.gFaceAnimTable[num3, 3];
						for (int j = 0; j < this.mFaceCurPoints.Count; j++)
						{
							TPointFloat thePoint = this.InterpolatePointF(this.mFacePoints[this.gFaceAnimTable[num3 + 1, 0]][j], this.mFacePoints[this.gFaceAnimTable[num3 + 1, 1]][j], (float)this.gFaceAnimTable[num3 + 1, 2] / 100f);
							TPointFloat thePoint2 = new TPointFloat((float)this.mFacePointsFrom[j].mX, (float)this.mFacePointsFrom[j].mY);
							this.mFaceCurPoints[j] = this.InterpolatePoint(thePoint2, thePoint, theFactor);
						}
					}
				}
				if (this.mFaceFadeDelay == 0)
				{
					this.mFaceAlpha -= 0.01f;
					if (this.mFaceAlpha <= 0f)
					{
						this.mFaceAlpha = 0f;
						this.mFaceState = 8;
						this.mFaceStateDelay = 30;
					}
				}
				else
				{
					this.mFaceFadeDelay--;
				}
			}
			if (this.mFaceState == 7)
			{
				if (this.mFaceFadeDelay == 0)
				{
					this.mFaceAlpha -= 0.01f;
					if (this.mFaceAlpha <= 0f)
					{
						this.mFaceAlpha = 0f;
						this.mFaceState = 8;
						this.mFaceStateDelay = 100;
					}
				}
			}
			else if (this.mFaceState == 8 && this.mFaceStateDelay == 0)
			{
				this.mFaceHyperSwirlPct += 0.003f + this.mFaceHyperSwirlPct * 0.004f;
				TPoint tpoint = this.InterpolatePoint(new TPointFloat(513f, 192f), new TPointFloat(513f, 400f), this.mFaceHyperSwirlPct);
				float num4 = (float)Math.Pow((double)(this.mFaceHyperSwirlPct * 12f), 1.2000000476837158);
				float num5 = (float)(Math.Pow((double)(1f - Math.Abs((0.5f - this.mFaceHyperSwirlPct) * 2f)), 0.60000002384185791) * 100.0);
				this.mFaceHyperCubeX = (float)tpoint.mX + (float)Math.Cos((double)num4) * num5;
				this.mFaceHyperCubeY = (float)tpoint.mY + (float)Math.Sin((double)num4) * num5;
				if (this.mFaceHyperSwirlPct >= 1f)
				{
					this.mFaceState = 9;
				}
			}
			if (this.mFaceState != 5 && this.mFaceFlashPct > 0f)
			{
				this.mFaceFlashPct -= 0.01f;
				if (this.mFaceFlashPct < 0f)
				{
					this.mFaceFlashPct = 0f;
				}
			}
		}

		public TPointFloat InterpolatePointF(TPoint thePoint1, TPoint thePoint2, float theFactor)
		{
			return new TPointFloat((float)thePoint1.mX * (1f - theFactor) + (float)thePoint2.mX * theFactor, (float)thePoint1.mY * (1f - theFactor) + (float)thePoint2.mY * theFactor);
		}

		public TPoint InterpolatePoint(TPointFloat thePoint1, TPointFloat thePoint2, float theFactor)
		{
			return new TPoint((int)((double)(thePoint1.mX * (1f - theFactor) + thePoint2.mX * theFactor) + 0.05), (int)((double)(thePoint1.mY * (1f - theFactor) + thePoint2.mY * theFactor) + 0.05));
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
		}

		public override void Update()
		{
			this.DoUpdate();
			if (this.mApp.mBoard != null && this.mApp.mBoard.mDoubleSpeed)
			{
				this.DoUpdate();
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.mBoard != null)
			{
				if (this.mApp.mBoard.mPauseCount == this.mApp.mBoard.mVisPauseCount && this.mApp.mBoard.mVisPauseCount > 0)
				{
					return;
				}
				if (this.mApp.mBoard.mExternalDraw)
				{
					this.mApp.mBoard.DrawBoard(g, true, false);
				}
				if (this.mApp.mBoard.mHintPiece != null && this.mApp.mGameMode != GameMode.MODE_BLITZ)
				{
					float num = (float)(1.0 - Math.Cos((double)this.mApp.mBoard.mHintAngle)) * 0.5f;
					int num2 = (int)this.mApp.mBoard.mHintPiece.mY + -23 + (int)(num * 10f);
					g.DrawImage(AtlasResources.IMAGE_HELP_ARROW, (int)this.mApp.mBoard.mHintPiece.mX + Constants.mConstants.BoardBej2_HintArrowOffset, num2);
					g.SetColorizeImages(true);
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
					g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num)));
					g.DrawImage(AtlasResources.IMAGE_HELP_INDICATOR_ARROW, (int)this.mApp.mBoard.mHintPiece.mX + Constants.mConstants.BoardBej2_HintArrowOverlayOffset_X, num2 + Constants.mConstants.BoardBej2_HintArrowOverlayOffset_Y, new TRect(0, 0, Constants.mConstants.BoardBej2_HintArrowOverlaySize, Constants.mConstants.BoardBej2_HintArrowOverlaySize));
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
					g.SetColorizeImages(false);
				}
				if (this.mApp.mBoard.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWN && this.mApp.mBoard.mPuzzleStateOverlay != null && this.mApp.mBoard.mNoMoreMoves && this.mApp.mBoard.mState == Board.BoardState.STATE_PUZZLE_NO_MOVES)
				{
					bool mInTutorialMode = this.mApp.mBoard.mInTutorialMode;
				}
			}
			if (this.mTextState != 0 && this.mApp.mBoard != null && (this.mApp.mBoard.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWN || this.mApp.mBoard.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWING))
			{
				int num3;
				int num4;
				this.mApp.mBoard.GetBoardCenter(out num3, out num4);
				float num5 = this.mScale;
				float num6 = this.mStrechiness;
				if (this.mApp.IsPortrait())
				{
					num5 *= 0.67f;
				}
				else
				{
					num5 *= 0.8f;
				}
				float num7 = ((float)this.mTextWidth + num6 * (float)this.mTextWidth * 3.9f) * num5 * this.mOverallScale;
				float num8 = ((float)GameConstants.BIGTEXT_HEIGHT - num6 * (float)GameConstants.BIGTEXT_HEIGHT * 1.8f) * num5 * this.mOverallScale;
				if (num8 < 6f)
				{
					num8 = 6f;
				}
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mTextFade)));
				g.SetFastStretch(false);
				g.DrawImage(this.mTextImage, new TRect(num3 - (int)(num7 / 2f), num4 - (int)Constants.mConstants.S(50f) - (int)(num8 / 2f), (int)num7, (int)num8), new TRect(0, 0, this.mTextWidth, GameConstants.BIGTEXT_HEIGHT));
				g.SetFastStretch(false);
				g.SetColorizeImages(false);
			}
			if (this.mPortalOpening)
			{
				int num9 = (int)((float)(this.mApp.IsLandscape() ? Constants.mConstants.EffectOverlay_PortalSize_landscape : Constants.mConstants.EffectOverlay_PortalSize_portait) * (this.mPortalPercent * this.mPortalPercent * 4f + 0.01f));
				int num10 = this.mWidth / 2;
				int num11 = this.mHeight / 2;
				int num12 = (int)((float)num9 / 2.72f);
				TRect theDestRect = default(TRect);
				theDestRect.mX = num10 - num12 / 2;
				theDestRect.mY = num11 - num12 / 2;
				theDestRect.mWidth = num12;
				theDestRect.mHeight = num12;
				new TRect(0, 0, this.mWidth, this.mHeight);
				theDestRect = theDestRect.Intersection(new TRect(0, 0, this.mWidth, this.mHeight));
				float num13 = 1f / Math.Min(this.mPortalPercent + 0.25f, 1f);
				TRect theSrcRect = default(TRect);
				theSrcRect.mX = (int)((float)num10 - (float)(num10 - theDestRect.mX) * num13);
				theSrcRect.mY = (int)((float)num11 - (float)(num11 - theDestRect.mY) * num13);
				theSrcRect.mWidth = (int)((float)theDestRect.mWidth * num13);
				theSrcRect.mHeight = (int)((float)theDestRect.mHeight * num13);
				if (this.mApp.mCurBackdrop != null)
				{
					Image image = (this.mApp.IsLandscape() ? GameConstants.BGH_TEXTURE : GameConstants.BGV_TEXTURE);
					TRect theTRect = new TRect(0, 0, image.GetWidth(), image.GetHeight());
					theSrcRect = theSrcRect.Intersection(theTRect);
					g.DrawImage(image, theDestRect, theSrcRect);
				}
				TRect theDestRect2 = new TRect(num10 - num9 / 2, num11 - num9 / 2, num9, num9);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.SetFastStretch(false);
				if (this.mPortalPercent < 1f)
				{
					g.DrawImage(AtlasResources.IMAGE_FIRE_RING, theDestRect2, new TRect(this.mUpdateCnt / 2 % 10 * Constants.mConstants.HyperSpace_FireRing_Size, 0, Constants.mConstants.HyperSpace_FireRing_Size, Constants.mConstants.HyperSpace_FireRing_Size));
				}
				g.SetFastStretch(true);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			}
		}

		public void DoPortal(bool useBoardBackdrop)
		{
			if (useBoardBackdrop)
			{
				this.mPortalBackground = (this.mApp.IsPortrait() ? GameConstants.BGV_TEXTURE : GameConstants.BGH_TEXTURE);
			}
			this.mApp.InterfaceTransactionUp(this.mApp.mBoard);
			this.mPortalOpening = true;
			this.mPortalPercent = 0f;
			this.mPortalDelay = 1;
		}

		public void StopStrechyText()
		{
			this.mTextWidth = 0;
			this.mMouseVisible = false;
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mTextState = 0;
			this.mFaceState = 0;
			this.mFaceLinePct = 0f;
			this.mFaceAnimUpdateCnt = 0;
			this.mFaceLastClickTick = 0;
			MemoryImage memoryImage = this.mTextImage;
		}

		public void DoStrechyText(string theText, int theExtraDelay)
		{
			this.mOverallScale = 1f;
			this.mTextState = 1;
			this.mTextFade = 0f;
			this.mStrechiness = 1f;
			this.mStrechinessAdd = 0f;
			this.mRigidness = 0f;
			this.mOutDelay = 30;
			this.mFadeDelay = 90 + theExtraDelay;
			this.mStayDelay = 60;
			this.mShrinkDelay = 60 + theExtraDelay;
			this.mChunkSpacing = 0f;
			this.mScale = 1f;
			this.mScaleAdd = 0.025f;
			Graphics @new = Graphics.GetNew(this.mTextImage);
			@new.Clear(new SexyColor(0, 0, 0, 0));
			@new.PrepareDrawing();
			@new.SetColor(new SexyColor(255, 255, 255));
			@new.SetFont(Resources.FONT_HUGE);
			float num = (float)@new.GetFont().StringWidth(theText);
			int num2 = @new.GetFont().StringHeight(theText);
			int num3 = (int)Constants.mConstants.S(4f);
			bool flag = false;
			if (num > (float)GameConstants.BIGTEXT_WIDTH)
			{
				float num4 = (float)GameConstants.BIGTEXT_WIDTH / num;
				if (num4 < 0.6f)
				{
					num4 = 0.5f;
					@new.SetScale(num4);
					@new.WriteWordWrapped(new TRect(num3, 0, GameConstants.BIGTEXT_WIDTH - num3 * 2, GameConstants.BIGTEXT_HEIGHT), theText, 0, 0, true);
					this.mTextWidth = GameConstants.BIGTEXT_WIDTH;
					flag = true;
				}
				else
				{
					@new.SetScale(num4);
					@new.DrawString(theText, (int)Constants.mConstants.S(4f), GameConstants.BIGTEXT_HEIGHT / 2 - num2 / 2);
					this.mTextWidth = Math.Min((int)Constants.mConstants.S(16f) + Resources.FONT_HUGE.StringWidth(theText), GameConstants.BIGTEXT_WIDTH);
					flag = true;
				}
			}
			if (!flag)
			{
				@new.DrawString(theText, (int)Constants.mConstants.S(4f), GameConstants.BIGTEXT_HEIGHT / 2 - num2 / 2);
				this.mTextWidth = Math.Min((int)Constants.mConstants.S(16f) + Resources.FONT_HUGE.StringWidth(theText), GameConstants.BIGTEXT_WIDTH);
			}
			@new.SetScale(1f);
			@new.FinishedDrawing();
			@new.SetRenderTarget(null);
			@new.PrepareForReuse();
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
		}

		public int mTextState;

		public MemoryImage mTextImage;

		public int mTextWidth;

		public float mTextFade;

		public float mOverallScale;

		public float mScale;

		public float mScaleAdd;

		public float mStrechiness;

		public float mStrechinessAdd;

		public float mRigidness;

		public float mChunkSpacing;

		public int mOutDelay;

		public int mFadeDelay;

		public int mShrinkDelay;

		public int mStayDelay;

		public bool mPortalOpening;

		public float mPortalPercent;

		public int mPortalDelay;

		public Image mPortalBackground = new Image();

		public int mFaceUpdateCnt;

		public int mFaceAnimUpdateCnt;

		public List<TPoint>[] mFacePoints = new List<TPoint>[4];

		public List<TPoint> mFacePointsFrom = new List<TPoint>();

		public List<TPoint> mFaceCurPoints = new List<TPoint>();

		public List<Line> mFaceLines = new List<Line>();

		public List<Polygon> mFacePolygons = new List<Polygon>();

		public List<TinyGem> mFaceTinyGemVector = new List<TinyGem>();

		public int mFaceLastClickTick;

		public float mFaceLinePct;

		public int mFaceStateDelay;

		public float mFaceFlashPct;

		public int mFaceState;

		public float mFaceHyperCubeX;

		public float mFaceHyperCubeY;

		public float mFaceHyperSwirlPct;

		public float mFaceAlpha;

		public float mFaceModePct;

		public int mFaceFadeDelay;

		public int mFaceBarSize;

		public List<string> mCreditLines = new List<string>();

		private int[,] gFaceAnimTable = new int[,]
		{
			{ 3, 2, 0, 50 },
			{ 3, 2, 0, 50 },
			{ 0, 2, 0, 70 },
			{ 0, 2, 0, 13 },
			{ 0, 2, 100, 12 },
			{ 0, 2, 0, 10 },
			{ 0, 1, 100, 15 },
			{ 0, 1, 0, 10 },
			{ 0, 1, 45, 5 },
			{ 0, 1, 65, 8 },
			{ 0, 1, 20, 20 },
			{ 0, 1, 25, 15 },
			{ 0, 1, 0, 70 },
			{ 0, 1, 0, 100 },
			{ 0, 3, 100, -1 },
			{ 0, 3, 0, 0 },
			{ 0, 1, 0, -1 },
			{ 0, 1, 0, 0 },
			{ 0, 1, 0, 0 },
			{ 0, 0, 0, -1 }
		};

		private readonly char[] SEPARATORS = new char[] { ' ' };

		public enum FaceState
		{
			FACESTATE_NONE,
			FACESTATE_BARS,
			FACESTATE_GEMS,
			FACESTATE_LINES,
			FACESTATE_FLASH,
			FACESTATE_FLASH_WHITE,
			FACESTATE_TALK,
			FACESTATE_FADE,
			FACESTATE_HYPER_SWIRL,
			FACESTATE_DONE
		}
	}
}
