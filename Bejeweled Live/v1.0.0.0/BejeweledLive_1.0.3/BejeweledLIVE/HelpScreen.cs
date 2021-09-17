using System;
using System.Collections.Generic;
using Bejeweled3;
using Sexy;

namespace BejeweledLIVE
{
	public class HelpScreen : InterfaceWidget, ButtonListener
	{
		public void PanelFinished()
		{
		}

		public HelpScreen(GameApp app)
			: base(app)
		{
			this.mTimeline = new Timeline<HelpScreen>(this);
			this.mPanelIndex = 0;
			this.mTitle = new LabelWidget("", Resources.FONT_BUTTON);
			this.mTitle.SetColor(0, SexyColor.White);
			this.AddWidget(this.mTitle);
			this.mBackButton = FancySmallButton.GetNewFancySmallButton(0, 0, 0, "", this);
			this.AddWidget(this.mBackButton);
			this.mStarFade = 0.0;
			this.mStarRot = 0.0;
			this.mStarRot2 = 0.0;
			this.flamePiece = PieceBejLive.GetNewCPieceBejLive();
			this.hypercubePiece = PieceBejLive.GetNewCPieceBejLive();
			this.SetupGrid(4, 2, 2);
			this.flamePiece.mX = (float)this.mCol[1];
			this.flamePiece.mY = (float)this.mRow[2];
			this.SetupGrid(5, 2, 2);
			this.hypercubePiece.mX = (float)this.mCol[1];
			this.hypercubePiece.mY = (float)this.mRow[2];
			this.flameEmitter = PieceParticleEmitter.CreateFlameGemEmitter(this.flamePiece);
			this.hypercubeEmitter = PieceParticleEmitter.CreateHyperCubeEmitter(this.hypercubePiece);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (7 == uiState)
			{
				this.mTitle.mText = "WELCOME TO BLITZ";
			}
			else if (8 == uiState)
			{
				this.mTitle.mText = "HOW TO PLAY BLITZ";
			}
			else
			{
				this.mTitle.mText = Strings.HelpScreen_How_To_Play;
			}
			this.mTitle.SizeToFit();
			this.mTitle.Move((this.mWidth - this.mTitle.Width()) / 2, Constants.mConstants.HelpScreen_TITLE_OFFSET_Y);
			if (7 == uiState)
			{
				this.mBackButton.mLabel = Strings.Continue;
			}
			else
			{
				this.mBackButton.mLabel = Strings.Back;
			}
			int mHeight = this.mBackButton.mHeight;
			int theY = this.mHeight - mHeight - Constants.mConstants.HelpScreen_BACKBUTTON_OFFSET_Y;
			int num = this.mWidth;
			if (this.mApp.IsLandscape())
			{
				num = (int)((float)num * Constants.BUTTON_WIDTH_FACTOR_LANDSCAPE);
			}
			int theX = this.mWidth / 2 - num / 2;
			this.mBackButton.Resize(theX, theY, num, mHeight);
			if (this.mPanelIndex < this.mPanelRotation.Count)
			{
				this.SetupGrid();
			}
			this.flameEmitter.ClearParticles();
			this.hypercubeEmitter.ClearParticles();
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.mTitle.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
			this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
			this.mTransitionFader.Clear();
			this.mTransitionFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0f), 0f, false, true);
			this.mTransitionFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0.5f), 1f, false, true);
			base.TransactionTimeSeconds(0.5f);
			this.mPanelRotation.Clear();
			if (7 == uiState)
			{
				this.mPanelRotation.Add(5);
				this.mPanelRotation.Add(4);
				this.mPanelRotation.Add(6);
				this.mPanelRotation.Add(7);
			}
			else if (8 == uiState)
			{
				this.mPanelRotation.Add(4);
				this.mPanelRotation.Add(1);
				this.mPanelRotation.Add(2);
				this.mPanelRotation.Add(3);
				this.mPanelRotation.Add(6);
				this.mPanelRotation.Add(7);
			}
			else
			{
				this.mPanelRotation.Add(0);
				this.mPanelRotation.Add(2);
				this.mPanelRotation.Add(7);
				this.mPanelRotation.Add(3);
				this.mPanelRotation.Add(8);
				this.mPanelRotation.Add(9);
			}
			this.AnimatePanel(0);
			this.mTimeline.Apply();
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mTitle.FadeOut(0f, 0.5f);
			this.mBackButton.FadeOut(0f, 0.5f);
			this.mTransitionFader.Clear();
			this.mTransitionFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0f), 1f, false, true);
			this.mTransitionFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0.5f), 0f, false, true);
			base.TransactionTimeSeconds(0.5f);
			this.mTimeline.Clear();
		}

		public override void Update()
		{
			base.Update();
			this.mStarRot += 0.015;
			if (this.mStarRot > 6.28318)
			{
				this.mStarRot -= 6.28318;
			}
			this.mStarFade += 0.006;
			if (this.mStarFade > 1.0)
			{
				this.mStarFade -= 1.0;
			}
			this.mStarRot2 -= 0.004;
			if (this.mStarRot < 0.0)
			{
				this.mStarRot += 6.28318;
			}
			if (this.mUpdateCnt <= this.mTransactionEndTick)
			{
				int num = this.mUpdateCnt - this.mTransactionStartTick;
				this.mOpacity = this.mTransitionFader.Tick((float)num);
			}
			else
			{
				this.mTimeline.Update();
				this.mTimeline.UpdateF(1f);
				if (this.mTimeline.AtEnd())
				{
					this.AnimatePanel((this.mPanelIndex + 1) % this.mPanelRotation.Count);
				}
			}
			this.MarkDirty();
			switch (this.mPanelRotation[this.mPanelIndex])
			{
			case 2:
				this.flameEmitter.Update();
				return;
			case 3:
				this.hypercubeEmitter.Update();
				return;
			default:
				this.flameEmitter.Enabled = (this.hypercubeEmitter.Enabled = false);
				return;
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(0, 0, 0, (int)(200f * this.mOpacity)));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			g.SetFont(Resources.FONT_TEXT);
			switch (this.mPanelRotation[this.mPanelIndex])
			{
			case 0:
				this.DrawGem(g, this.mCol[0], this.mRow[0], 2, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[0], 0, GameConstants.GEM_HEIGHT, 3, 1f, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 0, 1f);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[1], 0, -GameConstants.GEM_HEIGHT, 2, this.mSetScale, this.mSelector1Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[1], 4, 1f);
				this.DrawArrow(g, this.mCol[1] + GameConstants.GEM_WIDTH / 2, this.mRow[1], AtlasResources.IMAGE_ARROW_UP);
				this.DrawText(g, this.HelpScreen_Message_Swop_3);
				return;
			case 1:
				this.DrawGem(g, this.mCol[0], this.mRow[0], 2, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[0], 0, GameConstants.GEM_HEIGHT, 3, this.mSetScale, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 3, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[1], 0, -GameConstants.GEM_HEIGHT, 2, this.mSetScale, this.mSelector1Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[1], 3, this.mSetScale);
				this.DrawArrow(g, this.mCol[1] + GameConstants.GEM_WIDTH / 2, this.mRow[1], AtlasResources.IMAGE_ARROW_UP);
				this.DrawText(g, this.HelpScreen_Message_Multiple_Sets);
				return;
			case 2:
			{
				int num = this.mCol[1];
				int num2 = this.mRow[1] - (int)(Math.Sin((double)this.mAngle) * (double)GameConstants.GEM_HEIGHT);
				this.DrawGem(g, this.mCol[0], this.mRow[0], 2, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[0], 0, GameConstants.GEM_HEIGHT, 3, 1f, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[3], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 0, 1f);
				this.DrawSwapGem(g, num, num2, 0, 0, 2, (float)((this.mSetScale > 0.5f) ? 1 : 0), this.mSelector1Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[1], 4, 1f);
				this.DrawGem(g, this.mCol[3], this.mRow[1], 0, 1f);
				this.DrawFlameGem(g, num, num2, 2, this.mPanelAlpha * this.mOpacity * this.mSpecialAlpha);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				this.DrawArrow(g, this.mCol[1] + GameConstants.GEM_WIDTH / 2, this.mRow[1], AtlasResources.IMAGE_ARROW_UP);
				this.DrawText(g, this.HelpScreen_Message_Power_Gem);
				return;
			}
			case 3:
			{
				int num = this.mCol[2];
				int num2 = this.mRow[1] - (int)(Math.Sin((double)this.mAngle) * (double)GameConstants.GEM_HEIGHT);
				if (this.mSpecialAlpha * this.mPanelAlpha * this.mOpacity > 0f)
				{
					this.hypercubePiece.mX = (float)num;
					this.hypercubePiece.mY = (float)num2;
					this.hypercubePiece.SetFlag(PIECEFLAG.PIECEFLAG_HYPERGEM);
					this.hypercubeEmitter.ForceSetPosition();
					this.hypercubeEmitter.Draw(g, this.mSpecialAlpha * this.mPanelAlpha * this.mOpacity);
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				}
				this.DrawGem(g, this.mCol[0], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[1], this.mRow[0], 2, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[2], this.mRow[0], 0, GameConstants.GEM_HEIGHT, 3, 1f, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[3], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[4], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 4, 1f);
				this.DrawGem(g, this.mCol[1], this.mRow[1], 0, 1f);
				this.DrawSwapGem(g, num, num2, 0, 0, 2, 1f - this.mSpecialAlpha, this.mSelector1Alpha);
				this.DrawGem(g, this.mCol[3], this.mRow[1], 4, 1f);
				this.DrawGem(g, this.mCol[4], this.mRow[1], 0, 1f);
				int num3 = this.mUpdateCnt / 6 % 20;
				this.DrawHyperCube(g, num, num2, this.mSpecialAlpha * this.mPanelAlpha * this.mOpacity);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				this.DrawArrow(g, this.mCol[2] + GameConstants.GEM_WIDTH / 2, this.mRow[1], AtlasResources.IMAGE_ARROW_UP);
				this.DrawText(g, this.HelpScreen_Message_Hyper_Cube);
				return;
			}
			case 4:
				this.DrawGem(g, this.mCol[0], this.mRow[0], 2, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[0], 0, GameConstants.GEM_HEIGHT, 3, 1f, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 0, 1f);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[1], 0, -GameConstants.GEM_HEIGHT, 2, this.mSetScale, this.mSelector1Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[1], 4, 1f);
				this.DrawArrow(g, this.mCol[1] + GameConstants.GEM_WIDTH / 2, this.mRow[1], AtlasResources.IMAGE_ARROW_UP);
				this.DrawText(g, "BEJEWELED BLITZ IS A FAST-PACED GAME.  SCORE AS MANY POINTS AS YOU CAN IN ONE MINUTE!");
				return;
			case 5:
				break;
			case 6:
			{
				int num = this.mCol[1];
				int num2 = this.mRow[1] - (int)(Math.Sin((double)this.mAngle) * (double)GameConstants.GEM_HEIGHT);
				this.DrawGem(g, this.mCol[0], this.mRow[0], 2, this.mSetScale);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[0], 0, GameConstants.GEM_HEIGHT, 3, 1f, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 0, 1f);
				this.DrawGem(g, this.mCol[2], this.mRow[1], 4, 1f);
				Image image_GEM_MULTIPLIER = AtlasResources.IMAGE_GEM_MULTIPLIER2;
				Image image_MULTIPLIER2X = AtlasResources.IMAGE_MULTIPLIER2X;
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mPanelAlpha * this.mOpacity * this.mSetScale)));
				g.DrawImage(image_GEM_MULTIPLIER, num - 2, num2 - 2);
				g.DrawImage(image_MULTIPLIER2X, num + 8, num2 + 8);
				g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mPanelAlpha * this.mOpacity * this.mSelector1Alpha)));
				g.DrawImage(AtlasResources.IMAGE_SELECT, num, num2);
				g.SetColorizeImages(false);
				this.DrawArrow(g, this.mCol[1] + GameConstants.GEM_WIDTH / 2, this.mRow[1], AtlasResources.IMAGE_ARROW_UP);
				this.DrawText(g, "MATCH MULTIPLIER GEMS TO INCREASE YOUR SCORE MULTIPLIER.");
				return;
			}
			case 7:
			{
				int num = this.mCol[0] + (int)(Math.Sin((double)this.mAngle) * (double)GameConstants.GEM_WIDTH);
				int num2 = this.mRow[0];
				this.DrawSwapGem(g, num, num2, 0, 0, 2, 1f - this.mSpecialAlpha, this.mSelector1Alpha);
				this.DrawSwapGem(g, this.mCol[1], this.mRow[0], -GameConstants.GEM_WIDTH, 0, 3, 1f, this.mSelector2Alpha);
				this.DrawGem(g, this.mCol[2], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[3], this.mRow[0], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[0], this.mRow[1], 0, 1f);
				this.DrawGem(g, this.mCol[1], this.mRow[1], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[2], this.mRow[1], 4, 1f);
				this.DrawGem(g, this.mCol[3], this.mRow[1], 0, 1f);
				this.DrawGem(g, this.mCol[0], this.mRow[2], 4, 1f);
				this.DrawGem(g, this.mCol[1], this.mRow[2], 2, this.mSetScale);
				this.DrawGem(g, this.mCol[2], this.mRow[2], 0, 1f);
				this.DrawGem(g, this.mCol[3], this.mRow[2], 4, 1f);
				this.DrawLaserGem(g, num, num2, 2, this.mPanelAlpha * this.mOpacity * this.mSpecialAlpha);
				this.DrawArrow(g, this.mCol[1], this.mRow[0] + GameConstants.GEM_HEIGHT / 2, AtlasResources.IMAGE_ARROW_RIGHT);
				this.DrawText(g, this.HelpScreen_Message_Star_Gem);
				return;
			}
			case 8:
			{
				int num4 = this.mTextFrame.mY - Constants.mConstants.HelpScreen_Score_Y;
				BoardBejLive.DrawScoreHelper(g, Resources.FONT_BIG_TEXT, this.scoreStringSpeedBonus, this.mWidth / 2 - AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2, num4, this.mPanelAlpha * this.mOpacity);
				if (this.mSpeedPoints > 0)
				{
					BoardBejLive.DrawSpeedBonusHelper(g, this.mWidth / 2, num4 - Constants.mConstants.BoardBej3_SpeedBonus_Y, this.mSpeedPoints, 0.0, this.mPanelAlpha * this.mOpacity, true);
				}
				this.DrawText(g, this.HelpScreen_Message_Speed_Bonus);
				return;
			}
			case 9:
			{
				int num5 = this.mTextFrame.mY - Constants.mConstants.HelpScreen_Score_Y;
				BoardBejLive.DrawScoreHelper(g, Resources.FONT_BIG_TEXT, this.scoreStringBlazingSpeed, this.mWidth / 2 - AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2, num5, this.mPanelAlpha * this.mOpacity);
				BoardBejLive.DrawSpeedBonusHelper(g, this.mWidth / 2, num5 - Constants.mConstants.BoardBej3_SpeedBonus_Y, 1000, this.mSpeedBonus, this.mPanelAlpha * this.mOpacity, true);
				this.DrawText(g, this.HelpScreen_Message_Blazing_Speed);
				break;
			}
			default:
				return;
			}
		}

		public override bool BackButtonPress()
		{
			this.ButtonDepress(0);
			return true;
		}

		public void ButtonDepress(int buttonId)
		{
			if (buttonId == 0)
			{
				if (8 == this.mInterfaceState)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_HELP_SCREEN);
					this.mApp.GotoInterfaceState(4);
					return;
				}
				if (6 == this.mInterfaceState)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_HELP_SCREEN);
					this.mApp.GotoInterfaceState(4);
					return;
				}
				this.mApp.GotoInterfaceState(3);
			}
		}

		protected void AnimatePanel(int index)
		{
			this.mPanelIndex = index;
			int num = this.mPanelRotation[this.mPanelIndex];
			this.SetupGrid();
			this.mTimeline.Clear();
			for (int i = 0; i < this.t.Length; i++)
			{
				this.t[i] = HelpScreen.times[num, i];
			}
			for (int j = 0; j < HelpScreen.durations.Length; j++)
			{
				this.d[j] = HelpScreen.durations[j];
			}
			int channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float panelAlpha)
			{
				h.mPanelAlpha = panelAlpha;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[0], 0f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[0] + this.d[0], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[8], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[8] + this.d[8], 0f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float arrowAlpha)
			{
				h.mArrowAlpha = arrowAlpha;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[1], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[1] + this.d[1], 0f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float selectorAlpha1)
			{
				h.mSelector1Alpha = selectorAlpha1;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[2], 0f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[2] + this.d[2], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[5], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[5] + this.d[5], 0f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float selectorAlpha2)
			{
				h.mSelector2Alpha = selectorAlpha2;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[3], 0f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[3] + this.d[3], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[5], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[5] + this.d[5], 0f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float angle)
			{
				h.mAngle = angle;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[4], 0f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[4] + this.d[4], 1.57079637f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float scale)
			{
				h.mSetScale = scale;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[6], 1f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[6] + this.d[6], 0f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float specialAlpha)
			{
				h.mSpecialAlpha = specialAlpha;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[7], 0f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[7] + this.d[7], 1f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float score)
			{
				h.mScore = (int)score;
				h.scoreStringSpeedBonus = GlobalStaticVars.CommaSeperate_(h.mScore);
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[1], 1250f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[1] + this.d[1], 1500f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[2] + this.d[2], 1500f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[3] + this.d[3], 1750f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[4] + this.d[4], 1750f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[5] + this.d[5], 2040f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[6] + this.d[6], 2040f, false, true);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float speedPoints)
			{
				h.mSpeedPoints = (int)speedPoints;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[1], 0f, false, false);
			this.mTimeline.SetKey<float>(channelId, this.t[1] + this.d[1], 0f, false, false);
			this.mTimeline.SetKey<float>(channelId, this.t[3] + this.d[3], 40f, false, false);
			this.mTimeline.SetKey<float>(channelId, this.t[5] + this.d[5], 80f, false, false);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float blazingSpeedBuildUp)
			{
				h.mSpeedBonus = (double)blazingSpeedBuildUp;
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[1], 0.2f, false, false);
			this.mTimeline.SetKey<float>(channelId, this.t[1] + this.d[1], 0.2f, false, false);
			this.mTimeline.SetKey<float>(channelId, this.t[3] + this.d[3], 0.3f, false, false);
			this.mTimeline.SetKey<float>(channelId, this.t[5] + this.d[5], 0.4f, false, false);
			channelId = this.mTimeline.AddChannel<float>(delegate(HelpScreen h, float score)
			{
				h.scoreStringBlazingSpeed = GlobalStaticVars.CommaSeperate_((int)score);
			}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
			this.mTimeline.SetKey<float>(channelId, this.t[1], 43000f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[1] + this.d[1], 43000f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[2] + this.d[2], 43000f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[3] + this.d[3], 43250f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[4] + this.d[4], 43250f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[5] + this.d[5], 44500f, false, true);
			this.mTimeline.SetKey<float>(channelId, this.t[6] + this.d[6], 44500f, false, true);
			this.mTimeline.SetEvent(this.mTimeline.LastTick(), new Timeline<HelpScreen>.EventFunc(this.PanelFinished));
			this.mTimeline.Start();
		}

		protected void SetupGrid()
		{
			switch (this.mPanelRotation[this.mPanelIndex])
			{
			case 0:
				this.SetupGrid(3, 2, 2);
				return;
			case 1:
				this.SetupGrid(3, 2, 3);
				return;
			case 2:
				this.SetupGrid(4, 2, 2);
				return;
			case 3:
				this.SetupGrid(5, 2, 2);
				return;
			case 4:
				this.SetupGrid(3, 2, 4);
				return;
			case 5:
				this.SetupGrid(1, 2, 4);
				return;
			case 6:
				this.SetupGrid(3, 2, 2);
				return;
			case 7:
				this.SetupGrid(4, 3, 2);
				return;
			case 8:
				this.SetupGrid(1, 3, 2);
				return;
			case 9:
				this.SetupGrid(1, 3, 2);
				return;
			default:
				return;
			}
		}

		protected void SetupGrid(int cols, int rows, int lines)
		{
			if (this.mInterfaceStateLayout == 0)
			{
				this.mTextFrame.mWidth = 6 * GameConstants.GEM_WIDTH;
			}
			else
			{
				this.mTextFrame.mWidth = 12 * GameConstants.GEM_WIDTH;
			}
			this.mTextFrame.mHeight = lines * Resources.FONT_TEXT.GetHeight();
			TRect trect = default(TRect);
			trect.mWidth = GameConstants.GEM_WIDTH * cols;
			trect.mHeight = GameConstants.GEM_HEIGHT * rows + 8;
			trect.mX = (this.mWidth - trect.mWidth) / 2;
			trect.mY = (this.mHeight - trect.mHeight - this.mTextFrame.mHeight) / 2;
			this.mTextFrame.mX = (this.mWidth - this.mTextFrame.mWidth) / 2;
			this.mTextFrame.mY = trect.mY + trect.mHeight;
			for (int i = 0; i < cols; i++)
			{
				this.mCol[i] = trect.mX + i * GameConstants.GEM_WIDTH;
			}
			for (int j = 0; j < rows; j++)
			{
				this.mRow[j] = trect.mY + j * GameConstants.GEM_HEIGHT;
			}
		}

		private static PieceBejLive GetPiece(int x, int y, int colour)
		{
			PieceBejLive newCPieceBejLive = PieceBejLive.GetNewCPieceBejLive();
			newCPieceBejLive.mX = (float)x;
			newCPieceBejLive.mY = (float)y;
			newCPieceBejLive.mColor = colour;
			return newCPieceBejLive;
		}

		protected void DrawGem(Graphics g, int theX, int theY, int theGemNum, float theScale)
		{
			PieceBejLive piece = HelpScreen.GetPiece(theX, theY, theGemNum);
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mPanelAlpha * this.mOpacity)));
			int num = (int)((1.0 - (double)theScale) * 7.0) - 1;
			if (num == -1)
			{
				g.DrawImage(AtlasResources.GetImageInAtlasById(10031 + theGemNum), theX, theY, new TRect(0, 0, GameConstants.GEM_WIDTH, GameConstants.GEM_HEIGHT));
			}
			else if (num < 6)
			{
				piece.mShrinkSize = num;
				Image imageInAtlasById = AtlasResources.GetImageInAtlasById(10031 + piece.mColor);
				BoardBejLive.DrawShrinkingPiece(g, piece, 1f, imageInAtlasById);
			}
			g.SetColorizeImages(false);
			piece.PrepareForReuse();
		}

		protected void DrawHyperCube(Graphics g, int x, int y, float opacity)
		{
			if (opacity == 0f)
			{
				return;
			}
			this.hypercubeEmitter.Enabled = true;
			this.hypercubePiece.mX = (float)x;
			this.hypercubePiece.mY = (float)y;
			this.hypercubePiece.SetFlag(PIECEFLAG.PIECEFLAG_HYPERGEM);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			this.hypercubePiece.DrawHyperGem(g, this.mUpdateCnt, (double)opacity);
			this.hypercubePiece.DrawHyperGemOverlay(g, this.mUpdateCnt, (double)opacity);
		}

		protected void DrawFlameGem(Graphics g, int x, int y, int color, float opacity)
		{
			if (opacity == 0f)
			{
				return;
			}
			this.flameEmitter.Enabled = true;
			this.flamePiece.mX = (float)x;
			this.flamePiece.mY = (float)y;
			this.flamePiece.mColor = color;
			this.flamePiece.DrawFlameGem(g, this.mUpdateCnt, (double)opacity);
			this.flamePiece.DrawFlameGemOverlay(g, this.mUpdateCnt, (double)opacity);
			this.flameEmitter.ForceSetPosition();
			this.flameEmitter.Shape = (EmitterShape)this.flamePiece.mColor;
			this.flameEmitter.Draw(g, opacity);
		}

		protected void DrawLaserGem(Graphics g, int x, int y, int color, float opacity)
		{
			if (opacity == 0f)
			{
				return;
			}
			PieceBejLive piece = HelpScreen.GetPiece(x, y, color);
			piece.DrawLaserGemLowerLayer(g, this.mUpdateCnt, (double)opacity);
			piece.DrawLaserGemOverlay(g, this.mUpdateCnt, (double)opacity);
			piece.PrepareForReuse();
		}

		protected void DrawSwapGem(Graphics g, int theX, int theY, int theDeltaX, int theDeltaY, int theGemNum, float theScale, float theSelectAlpha)
		{
			theX += (int)((double)theDeltaX * Math.Sin((double)this.mAngle));
			theY += (int)((double)theDeltaY * Math.Sin((double)this.mAngle));
			this.DrawGem(g, theX, theY, theGemNum, theScale);
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mPanelAlpha * this.mOpacity * theSelectAlpha)));
			g.DrawImage(AtlasResources.IMAGE_SELECT, theX, theY);
			g.SetColorizeImages(false);
		}

		protected void DrawArrow(Graphics g, int x, int y, Image arrow)
		{
			int theAlpha = (int)(255f * this.mPanelAlpha * this.mOpacity * this.mArrowAlpha);
			g.SetColor(new SexyColor(255, 255, 255, theAlpha));
			g.SetColorizeImages(true);
			g.DrawImage(arrow, x - arrow.GetWidth() / 2, y - arrow.GetHeight() / 2);
			g.SetColorizeImages(false);
		}

		protected void DrawText(Graphics g, string text)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mPanelAlpha * this.mOpacity)));
			this.WriteWordWrapped(g, this.mTextFrame, text, -1, 0);
		}

		public void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		private int[] t = new int[10];

		private int[] d = new int[10];

		private static readonly int[,] times = new int[,]
		{
			{ 0, 175, 250, 290, 315, 330, 360, 410, 550, 625 },
			{ 0, 175, 250, 290, 315, 330, 360, 410, 500, 575 },
			{ 0, 175, 250, 290, 315, 330, 360, 360, 575, 650 },
			{ 0, 175, 250, 290, 315, 330, 360, 360, 600, 775 },
			{ 0, 175, 250, 290, 315, 330, 360, 410, 550, 625 },
			{ 0, 175, 250, 290, 315, 330, 360, 410, 550, 625 },
			{ 0, 175, 250, 290, 315, 330, 360, 410, 550, 625 },
			{ 0, 175, 250, 290, 315, 330, 360, 360, 575, 650 },
			{ 0, 100, 200, 300, 400, 500, 550, 575, 600, 650 },
			{ 0, 175, 250, 290, 315, 330, 360, 360, 575, 650 }
		};

		private string HelpScreen_Message_Hyper_Cube = Strings.HelpScreen_Message_Hyper_Cube;

		private string HelpScreen_Message_Blazing_Speed = Strings.HelpScreen_Message_Blazing_Speed;

		private string HelpScreen_Message_Multiple_Sets = Strings.HelpScreen_Message_Multiple_Sets;

		private string HelpScreen_Message_Power_Gem = Strings.HelpScreen_Message_Power_Gem;

		private string HelpScreen_Message_Speed_Bonus = Strings.HelpScreen_Message_Speed_Bonus;

		private string HelpScreen_Message_Star_Gem = Strings.HelpScreen_Message_Star_Gem;

		private string HelpScreen_Message_Swop_3 = Strings.HelpScreen_Message_Swop_3;

		private static readonly int[] durations = new int[]
		{
			SexyAppFrameworkConstants.ticksForSeconds(0.4f),
			SexyAppFrameworkConstants.ticksForSeconds(0.4f),
			SexyAppFrameworkConstants.ticksForSeconds(0.125f),
			SexyAppFrameworkConstants.ticksForSeconds(0.125f),
			SexyAppFrameworkConstants.ticksForSeconds(0.333f),
			SexyAppFrameworkConstants.ticksForSeconds(0.25f),
			SexyAppFrameworkConstants.ticksForSeconds(0.2f),
			SexyAppFrameworkConstants.ticksForSeconds(0.4f),
			SexyAppFrameworkConstants.ticksForSeconds(0.5f)
		};

		private string scoreStringSpeedBonus;

		private int mScore;

		private int mSpeedPoints;

		private string scoreStringBlazingSpeed;

		private double mSpeedBonus;

		public LabelWidget mTitle;

		public FancySmallButton mBackButton;

		public KeyInterpolatorFloat mTransitionFader = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		public Timeline<HelpScreen> mTimeline;

		protected List<int> mPanelRotation = new List<int>();

		protected int mPanelIndex;

		protected int[] mCol = new int[5];

		protected int[] mRow = new int[3];

		protected TRect mTextFrame = default(TRect);

		protected float mPanelAlpha;

		protected float mArrowAlpha;

		protected float mSelector1Alpha;

		protected float mSelector2Alpha;

		protected float mAngle;

		protected float mSetScale;

		protected float mSpecialAlpha;

		protected double mStarFade;

		protected double mStarRot;

		protected double mStarRot2;

		private PieceParticleEmitter flameEmitter;

		private PieceParticleEmitter hypercubeEmitter;

		private PieceBejLive flamePiece;

		private PieceBejLive hypercubePiece;

		private enum KeyTimes
		{
			PANEL_IN,
			ARROW_OUT,
			SEL1_IN,
			SEL2_IN,
			SWAP,
			SELS_OUT,
			SCALE,
			SPECIAL,
			PANEL_OUT,
			DONE,
			KEY_COUNT
		}

		public enum ButtonID
		{
			BACK_BUTTON_ID
		}

		protected enum Panels
		{
			MatchThree,
			MultipleSets,
			FlameGem,
			HyperCube,
			BLITZ,
			FBOOK,
			Multiplier,
			StarGem,
			SpeedBonus,
			BlazingSpeed,
			PANEL_COUNT
		}
	}
}
