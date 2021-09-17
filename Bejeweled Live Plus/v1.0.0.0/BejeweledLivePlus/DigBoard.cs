using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public class DigBoard : TimeLimitBoard
	{
		public void TriggerDrillSmokeEffects()
		{
			int num = this.GetBoardY() + 800;
			Effect effect = Effect.alloc(Effect.Type.TYPE_STEAM);
			effect.mX = (float)(this.GetBoardX() - GlobalMembers.S(75));
			effect.mY = (float)(num - GlobalMembers.S(50));
			effect.mMinScale = 1f;
			effect.mMaxScale = 10f;
			effect.mScale = 5f;
			effect.mValue[0] = GlobalMembers.M(0.3f);
			effect.mValue[1] = GlobalMembers.M(-0.008f);
			effect.mValue[2] = GlobalMembers.M(0.95f);
			this.mPostFXManager.AddEffect(effect);
			int i = this.GetBoardX();
			int num2 = 40;
			int num3 = i + 800;
			while (i < num3)
			{
				effect = Effect.alloc(Effect.Type.TYPE_STEAM);
				effect.mX = (float)i;
				effect.mY = (float)(num + GlobalMembers.S(15));
				effect.mMinScale = 1f;
				effect.mMaxScale = 10f;
				effect.mScale = 2f;
				effect.mValue[0] = GlobalMembers.M(0.3f);
				effect.mValue[1] = GlobalMembers.M(-0.005f);
				effect.mValue[2] = GlobalMembers.M(0.95f);
				this.mPostFXManager.AddEffect(effect);
				i += num2;
			}
			effect = Effect.alloc(Effect.Type.TYPE_STEAM);
			effect.mX = (float)(this.GetBoardX() + 800 + GlobalMembers.S(75));
			effect.mY = (float)(num - GlobalMembers.S(50));
			effect.mValue[0] = GlobalMembers.M(0.3f);
			effect.mValue[1] = GlobalMembers.M(-0.008f);
			effect.mValue[2] = GlobalMembers.M(0.95f);
			effect.mMinScale = 1f;
			effect.mMaxScale = 10f;
			effect.mScale = 5f;
			this.mPostFXManager.AddEffect(effect);
		}

		public void TriggerDrillDeathSmokeEffects()
		{
			int num = (this.GetBoardY() + 800) / 2;
			Effect effect = Effect.alloc(Effect.Type.TYPE_STEAM);
			effect.mX = (float)(this.GetBoardX() - GlobalMembers.S(75));
			effect.mY = (float)(num + GlobalMembers.S(175));
			effect.mMinScale = 1f;
			effect.mMaxScale = 10f;
			effect.mScale = 5f;
			effect.mValue[0] = GlobalMembers.M(0.3f);
			effect.mValue[1] = GlobalMembers.M(-0.002f);
			effect.mValue[2] = GlobalMembers.M(0.95f);
			this.mPostFXManager.AddEffect(effect);
			effect = Effect.alloc(Effect.Type.TYPE_STEAM);
			effect.mX = (float)(this.GetBoardX() - GlobalMembers.S(60));
			effect.mY = (float)(num - GlobalMembers.S(450));
			effect.mMinScale = 1f;
			effect.mMaxScale = 10f;
			effect.mScale = 3f;
			effect.mValue[0] = GlobalMembers.M(0.7f);
			effect.mValue[1] = GlobalMembers.M(-0.002f);
			effect.mValue[2] = GlobalMembers.M(0.95f);
			this.mPostFXManager.AddEffect(effect);
			effect = Effect.alloc(Effect.Type.TYPE_STEAM);
			effect.mX = (float)(this.GetBoardX() + 800 + GlobalMembers.S(75));
			effect.mY = (float)(num + GlobalMembers.S(175));
			effect.mValue[0] = GlobalMembers.M(0.3f);
			effect.mValue[1] = GlobalMembers.M(-0.002f);
			effect.mValue[2] = GlobalMembers.M(0.95f);
			effect.mMinScale = 1f;
			effect.mMaxScale = 10f;
			effect.mScale = 5f;
			this.mPostFXManager.AddEffect(effect);
			effect = Effect.alloc(Effect.Type.TYPE_STEAM);
			effect.mX = (float)(this.GetBoardX() + 800 - GlobalMembers.S(60));
			effect.mY = (float)(num - GlobalMembers.S(450));
			effect.mMinScale = 1f;
			effect.mMaxScale = 10f;
			effect.mScale = 3f;
			effect.mValue[0] = GlobalMembers.M(0.7f);
			effect.mValue[1] = GlobalMembers.M(-0.002f);
			effect.mValue[2] = GlobalMembers.M(0.95f);
			this.mPostFXManager.AddEffect(effect);
		}

		public DigBoard()
		{
			this.mMusicName = "BuriedTreasure";
			this.mMaxTicksLeft = 0;
			this.mRotatingCounter = new RotatingCounter(0, new Rect(ConstantsWP.DIG_BOARD_DEPTH_METER_CLIP_X, ConstantsWP.DIG_BOARD_DEPTH_METER_CLIP_Y, ConstantsWP.DIG_BOARD_DEPTH_METER_CLIP_W, ConstantsWP.DIG_BOARD_DEPTH_METER_CLIP_H), GlobalMembersResources.FONT_DIALOG);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_BOARD_CV_CRACK, this.mCvCrack);
			this.mCogsAnim = null;
		}

		public override void Dispose()
		{
			if (this.mCogsAnim != null)
			{
				this.mCogsAnim.Dispose();
				this.mCogsAnim = null;
			}
			if (this.mRotatingCounter != null)
			{
				this.mRotatingCounter.Dispose();
				this.mRotatingCounter = null;
			}
			base.Dispose();
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			BejeweledLivePlusApp.UnloadContent("GamePlayQuest_Dig");
			BejeweledLivePlusApp.UnloadContent("GamePlay_UI_Dig");
		}

		public override void LoadContent(bool threaded)
		{
			if (threaded)
			{
				BejeweledLivePlusApp.LoadContentInBackground("GamePlay_UI_Dig");
				BejeweledLivePlusApp.LoadContentInBackground("GamePlayQuest_Dig");
			}
			else
			{
				BejeweledLivePlusApp.LoadContent("GamePlay_UI_Dig");
				BejeweledLivePlusApp.LoadContent("GamePlayQuest_Dig");
				if (this.mCogsAnim == null)
				{
					this.mCogsAnim = GlobalMembersResourcesWP.POPANIM_QUEST_DIG_COGS.Duplicate();
					this.mCogsAnim.mClip = true;
					this.mCogsAnim.Play("IDLE");
				}
			}
			base.LoadContent(threaded);
		}

		public override string GetExtraGameOverLogParams()
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			if (this.mIsPerpetual)
			{
				return string.Format("distGold={0} distGems={1} distArtifacts={2} depth={3}", new object[]
				{
					digGoal.mTreasureEarnings[0],
					digGoal.mTreasureEarnings[1],
					digGoal.mTreasureEarnings[2],
					digGoal.GetGridDepth()
				});
			}
			return string.Empty;
		}

		public override float GetRankPointMultiplier()
		{
			return 2.66666675f;
		}

		public override int GetTicksLeft()
		{
			int ticksLeft = base.GetTicksLeft();
			if (this.mMaxTicksLeft == 0)
			{
				return ticksLeft;
			}
			return Math.Min(this.mMaxTicksLeft, ticksLeft);
		}

		public override void GameOver(bool visible)
		{
			this.mPoints = this.mLevelPointsTotal;
			bool flag = this.mGameOverCount > 0;
			base.GameOver(visible);
			if (this.mIsPerpetual && !flag && this.mGameOverCount > 0)
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DEATH, 0, GlobalMembers.M(1.0));
				GlobalMembers.gApp.mMusic.PlaySongNoDelay(14, false);
			}
		}

		public override void DrawCountdownBar(Graphics g)
		{
			if (this.mOffsetY != 0)
			{
				g.Translate(0, this.mOffsetY);
			}
			if (this.mIsPerpetual)
			{
				g.SetColorizeImages(true);
				float num = (float)Math.Pow((double)this.GetBoardAlpha(), 4.0);
				g.SetColor(new Color(255, 255, 255, (int)(this.GetBoardAlpha() * (float)GlobalMembers.M(255))));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK_ID)));
				g.SetColor(new Color(GlobalMembers.M(64), GlobalMembers.M(32), GlobalMembers.M(8), (int)(num * (float)GlobalMembers.M(255))));
				if (this.WantWarningGlow())
				{
					Color warningGlowColor = this.GetWarningGlowColor();
					if (warningGlowColor.mAlpha > 0)
					{
						g.PushState();
						g.SetDrawMode(Graphics.DrawMode.Additive);
						g.SetColor(warningGlowColor);
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME_ID)));
						g.PopState();
					}
				}
				Rect countdownBarRect = this.GetCountdownBarRect();
				countdownBarRect.mX -= GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER.mWidth / 2;
				countdownBarRect.mWidth = (int)(this.mCountdownBarPct * (float)countdownBarRect.mWidth + (float)this.mLevelBarSizeBias);
				g.FillRect(countdownBarRect);
				if (this.mLevelBarBonusAlpha > 0.0)
				{
					Rect countdownBarRect2 = this.GetCountdownBarRect();
					countdownBarRect2.mX -= GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER.mWidth / 2;
					countdownBarRect2.mWidth = (int)((float)countdownBarRect2.mWidth * this.GetLevelPct());
					g.SetColor(new Color(GlobalMembers.M(240), GlobalMembers.M(255), 200, (int)(this.mLevelBarBonusAlpha * (double)GlobalMembers.M(255))));
					g.FillRect(countdownBarRect2);
				}
				Graphics3D graphics3D = g.Get3D();
				SexyTransform2D mDrawTransform = this.mCountdownBarPIEffect.mDrawTransform;
				Rect mClipRect = g.mClipRect;
				if (graphics3D != null)
				{
					countdownBarRect.Scale((double)((float)this.mScale), (double)((float)this.mScale), (int)GlobalMembers.S(ConstantsWP.DEVICE_HEIGHT_F), (int)GlobalMembers.S(ConstantsWP.DEVICE_WIDTH_F));
					this.mCountdownBarPIEffect.mDrawTransform.Translate(GlobalMembers.S(-ConstantsWP.DEVICE_HEIGHT_F), GlobalMembers.S(-ConstantsWP.DEVICE_WIDTH_F));
					this.mCountdownBarPIEffect.mDrawTransform.Scale((float)this.mScale, (float)this.mScale);
					this.mCountdownBarPIEffect.mDrawTransform.Translate(GlobalMembers.S(ConstantsWP.DEVICE_HEIGHT_F), GlobalMembers.S(ConstantsWP.DEVICE_WIDTH_F));
				}
				int num2 = (int)((double)this.GetAlpha() * this.mAlphaCurve * (double)GlobalMembers.M(255));
				if (num2 == 255)
				{
					g.SetClipRect(countdownBarRect);
					g.SetColor(new Color(255, 255, 255, (int)((double)this.GetAlpha() * this.mAlphaCurve * (double)GlobalMembers.M(255))));
					g.PushColorMult();
					this.mCountdownBarPIEffect.mColor = new Color(255, 255, 255, (int)((double)this.GetAlpha() * this.mAlphaCurve * (double)GlobalMembers.M(255)));
					this.mCountdownBarPIEffect.Draw(g);
					this.mCountdownBarPIEffect.mDrawTransform = mDrawTransform;
					g.PopColorMult();
					g.SetColor(Color.White);
					g.SetClipRect(mClipRect);
				}
			}
			else
			{
				g.Translate(0, GlobalMembers.S(-this.mBoardUIOffsetY));
				base.DrawCountdownBar(g);
				g.Translate(0, GlobalMembers.S(this.mBoardUIOffsetY));
			}
			if (this.mOffsetY != 0)
			{
				g.Translate(0, -this.mOffsetY);
			}
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			if (this.mIsPerpetual)
			{
				if (digGoal.mAllClearAnimPlayed)
				{
					digGoal.DrawDigBarAnim(g, true);
				}
				else if (digGoal.mClearedAnimPlayed)
				{
					digGoal.DrawDigBarAnim(g, false);
				}
			}
			base.DrawOverlay(g, thePriority);
		}

		public override void RefreshUI()
		{
			if (this.mUiConfig == Board.EUIConfig.eUIConfig_Standard || this.mUiConfig == Board.EUIConfig.eUIConfig_StandardNoReplay)
			{
				this.mHintButton.SetOverlayType(Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_DIAMOND_MINE);
				this.mHintButton.Resize(ConstantsWP.BOARD_UI_HINT_BTN_X, ConstantsWP.DIGBOARD_UI_HINT_BTN_Y, ConstantsWP.BOARD_UI_HINT_BTN_WIDTH, 0);
				this.mHintButton.mHasAlpha = true;
				this.mHintButton.mDoFinger = true;
				this.mHintButton.mOverAlphaSpeed = 0.1;
				this.mHintButton.mOverAlphaFadeInSpeed = 0.2;
				this.mHintButton.mWidgetFlagsMod.mRemoveFlags |= 4;
				this.mHintButton.mDisabled = false;
				return;
			}
			if (this.mUiConfig == Board.EUIConfig.eUIConfig_WithReset || this.mUiConfig == Board.EUIConfig.eUIConfig_WithResetAndReplay)
			{
				this.mHintButton.Resize(ConstantsWP.BOARD_UI_HINT_BTN_X, ConstantsWP.BOARD_UI_HINT_BTN_Y, ConstantsWP.BOARD_UI_HINT_BTN_WIDTH, 0);
				this.mHintButton.mHasAlpha = true;
				this.mHintButton.mDoFinger = true;
				this.mHintButton.mOverAlphaSpeed = 0.1;
				this.mHintButton.mOverAlphaFadeInSpeed = 0.2;
				this.mHintButton.mWidgetFlagsMod.mRemoveFlags |= 4;
				this.mHintButton.mDisabled = false;
			}
		}

		public override void DrawUI(Graphics g)
		{
		}

		public override void DrawWarningHUD(Graphics g)
		{
			g.PushState();
			Color color = g.GetColor();
			g.SetDrawMode(Graphics.DrawMode.Additive);
			Color warningGlowColor = this.GetWarningGlowColor();
			g.SetColor(warningGlowColor);
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(this.mOffsetX, this.mOffsetY);
			}
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER_ID)));
			if (this.WantTopFrame())
			{
				if (this.WantTopLevelBar() || this.GetTimeLimit() > 0)
				{
					if (this.mOffsetY != 0)
					{
						g.Translate(-this.mOffsetX, this.mOffsetY);
					}
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME_ID)));
					Rect countdownBarRect = this.GetCountdownBarRect();
					if (warningGlowColor.mAlpha > 0)
					{
						Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER, (float)this.GetTimeDrawX(), (float)(countdownBarRect.mY + countdownBarRect.mHeight / 2));
					}
					if (this.mOffsetY != 0)
					{
						g.Translate(this.mOffsetX, -this.mOffsetY);
					}
				}
				else
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)));
				}
			}
			if (this.WantBottomFrame())
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)));
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColor(color);
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(-this.mOffsetX, -this.mOffsetY);
			}
			g.PopState();
		}

		public override void DrawHUDText(Graphics g)
		{
			g.PushState();
			Color color = g.GetColor();
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(this.mOffsetX, this.mOffsetY);
			}
			this.mRotatingCounter.Draw(g, 0f);
			float num = GlobalMembers.S(-1f);
			g.SetColor(new Color(0));
			g.SetFont(this.mRotatingCounter.mFont);
			g.WriteString("m", (int)Utils.Round((float)ConstantsWP.DIG_BOARD_DEPTH_METER_SYMBOL_X + num), ConstantsWP.DIG_BOARD_DEPTH_METER_SYMBOL_Y);
			if (this.mIsPerpetual)
			{
				DigGoal digGoal = (DigGoal)this.mQuestGoal;
				g.SetFont(GlobalMembersResources.FONT_SCORE);
				g.SetColor(new Color(GlobalMembers.M(16776960)));
				g.WriteString(string.Format("{0}", SexyFramework.Common.CommaSeperate(this.mLevelPointsTotal)), ConstantsWP.DIG_BOARD_SCORE_CENTER_X, ConstantsWP.DIG_BOARD_SCORE_BTM_Y);
				if (digGoal.mTextFlashTicks > 0 && digGoal.mTextFlashTicks / GlobalMembers.M(20) % GlobalMembers.M(2) == 1)
				{
					g.SetColor(new Color(GlobalMembers.M(16755200)));
					g.WriteString(string.Format("{0}", SexyFramework.Common.CommaSeperate(this.mLevelPointsTotal)), ConstantsWP.DIG_BOARD_SCORE_CENTER_X, ConstantsWP.DIG_BOARD_SCORE_BTM_Y);
				}
			}
			((ImageFont)this.mRotatingCounter.mFont).PopLayerColor(0);
			if (this.WantDrawTimer())
			{
				this.DrawTimer(g);
			}
			g.SetColor(color);
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(-this.mOffsetX, -this.mOffsetY);
			}
			g.PopState();
		}

		public override void DrawBottomFrame(Graphics g)
		{
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(this.mOffsetX, this.mOffsetY);
			}
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL_ID)));
			if (this.WantBottomFrame())
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)));
			}
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(-this.mOffsetX, -this.mOffsetY);
			}
		}

		public override void DrawTopFrame(Graphics g)
		{
			if (this.mOffsetY != 0)
			{
				g.Translate(0, this.mOffsetY);
			}
			if (this.WantTopFrame())
			{
				if (this.WantCountdownBar() || this.WantTopLevelBar())
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME_ID)));
					Rect countdownBarRect = this.GetCountdownBarRect();
					Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER, (float)this.GetTimeDrawX(), (float)(countdownBarRect.mY + countdownBarRect.mHeight / 2));
				}
				else
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME_ID)));
				}
			}
			if (this.mOffsetY != 0)
			{
				g.Translate(0, -this.mOffsetY);
			}
		}

		public override void DrawHUD(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.GetAlpha())));
			this.DrawBackgroundElements(g);
			this.DrawDepthMeter(g);
			this.DrawFrame(g);
		}

		public void DrawBackgroundElements(Graphics g)
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			Rect mClipRect = g.mClipRect;
			int num = 100;
			g.SetClipRect(GlobalMembers.S((int)GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND_ID)), GlobalMembers.S((int)GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND_ID)), GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND.mWidth, GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND.mHeight + num);
			int num2 = (int)GlobalMembers.S(digGoal.GetGridDepthAsDouble() * 100.0);
			int mHeight = GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND.mHeight;
			int num3 = 0;
			if (num2 >= mHeight)
			{
				num3 = num2 / mHeight;
			}
			if (num2 % mHeight > 0)
			{
				num3++;
			}
			g.SetScale(1f, 1.2f, 0f, 0f);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND_ID)) + 0f), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND_ID)) - (float)num2));
			for (int i = 1; i <= num3; i++)
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND_ID)) + 0f), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND_ID)) + (float)(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND.mHeight * i - num2)));
			}
			g.SetClipRect(mClipRect);
			g.SetScale(1f, 1f, 0f, 0f);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW_ID)));
			if (this.mOffsetY != 0)
			{
				g.Translate(0, this.mOffsetY);
			}
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK_ID)));
			if (this.mOffsetY != 0)
			{
				g.Translate(0, -this.mOffsetY);
			}
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(this.mOffsetX, this.mOffsetY);
			}
			this.DrawCogs(g);
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(-this.mOffsetX, -this.mOffsetY);
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mCurrentHint != null)
			{
				this.mCurrentHint.Draw(g);
			}
		}

		public void DrawDepthMeter(Graphics g)
		{
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(this.mOffsetX, this.mOffsetY);
			}
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER_ID)));
			if (this.mOffsetX != 0 || this.mOffsetY != 0)
			{
				g.Translate(-this.mOffsetX, -this.mOffsetY);
			}
		}

		public void DrawCogs(Graphics g)
		{
			this.mCogsAnim.Draw(g);
			g.SetColorizeImages(true);
		}

		public void SetCogsAnim(bool on)
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			if (on && string.Compare(this.mCogsAnim.mLastPlayedFrameLabel, "DRILL", 5) != 0)
			{
				this.mCogsAnim.Play("DRILL");
				return;
			}
			if (this.GetTicksLeft() == 0 && this.GetTimeLimit() > 0 && string.Compare(this.mCogsAnim.mLastPlayedFrameLabel, "DEATH", 5) != 0 && digGoal != null && !digGoal.mDigInProgress && base.IsBoardStill())
			{
				this.mCogsAnim.Play("DEATH");
				return;
			}
			if (!on && (this.GetTicksLeft() != 0 || this.GetTimeLimit() <= 0) && string.Compare(this.mCogsAnim.mLastPlayedFrameLabel, "IDLE", 5) != 0)
			{
				this.mCogsAnim.Play("IDLE");
			}
		}

		public void UpdateCogsAnim()
		{
			this.mCogsAnim.Update();
		}

		public void DoTimeBonus(bool theIsMega)
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			TextNotifyEffect textNotifyEffect = TextNotifyEffect.alloc();
			textNotifyEffect.mDuration = GlobalMembers.M(200);
			int mTimeBonus;
			if (theIsMega)
			{
				mTimeBonus = this.mMegaTimeBonus;
				textNotifyEffect.mText = string.Format(GlobalMembers._ID("+{0} SECOND MEGA BONUS", 170), mTimeBonus);
			}
			else
			{
				mTimeBonus = this.mTimeBonus;
				textNotifyEffect.mText = string.Format(GlobalMembers._ID("+{0} SECOND BONUS", 171), mTimeBonus);
			}
			textNotifyEffect.mFont = GlobalMembersResources.FONT_HEADER;
			int num = textNotifyEffect.mFont.StringWidth(textNotifyEffect.mText);
			if (num > GlobalMembers.gApp.mWidth)
			{
				float num2 = (float)(num - GlobalMembers.gApp.mWidth);
				textNotifyEffect.mScale = Math.Max(1f - num2 / (float)GlobalMembers.gApp.mWidth, 0.1f);
			}
			textNotifyEffect.mX = (float)GlobalMembers.RS(this.mWidth / 2);
			textNotifyEffect.mY = (float)GlobalMembers.RS(ConstantsWP.DIG_BOARD_TIME_BONUS_TEXT_Y - textNotifyEffect.mFont.GetHeight() / 2);
			digGoal.mMessageFXManager.AddEffect(textNotifyEffect);
			this.mTimeLimit += mTimeBonus;
			int ticksLeft = base.GetTicksLeft();
			if (ticksLeft <= 9000)
			{
				this.mMaxTicksLeft = ticksLeft;
				this.mTimeLimit = 90;
				this.mGameTicks = 9000 - ticksLeft + GlobalMembers.M(250);
				return;
			}
			this.mMaxTicksLeft = ticksLeft;
			this.mTimeLimit = (ticksLeft + 99) / 100;
			this.mGameTicks = 250;
		}

		public override void BlanksFilled(bool specialDropped)
		{
			base.BlanksFilled(specialDropped);
			if (this.mIsPerpetual && this.mColorCount == 4)
			{
				this.SetColorCount(5);
			}
		}

		public override void UpdateFalling()
		{
			base.UpdateFalling();
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			if (digGoal.mInitPushAnimCv.IsDoingCurve())
			{
				foreach (int theId in digGoal.mIdToTileData.Keys)
				{
					Piece pieceById = base.GetPieceById(theId);
					if (pieceById != null)
					{
						pieceById.mFallVelocity = 0f;
					}
				}
			}
		}

		public override bool WantDrawBackground()
		{
			return !this.mIsPerpetual;
		}

		public override bool WantHypermixerBottomCheck()
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			return digGoal.mNextBottomHypermixerWait == 0;
		}

		public override bool TryingDroppedPieces(List<Piece> thePieceVector, int theTryCount)
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			int[] array = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };
			for (int i = 0; i < thePieceVector.size<Piece>(); i++)
			{
				Piece piece = thePieceVector[i];
				int num = array[piece.mCol]--;
				if (num >= 0)
				{
					DigGoal.OldPieceData oldPieceData = digGoal.mOldPieceData[num, piece.mCol];
					if (oldPieceData.mFlags != -1 && piece.mFlags == 0 && (theTryCount <= i || (oldPieceData.mFlags != 0 && theTryCount < 100)))
					{
						piece.mFlags = (int)((long)oldPieceData.mFlags & 7L);
						if (oldPieceData.mColor != -1 || ((long)oldPieceData.mFlags & 2L) != 0L)
						{
							piece.mColor = oldPieceData.mColor;
						}
					}
				}
			}
			return true;
		}

		public override bool PiecesDropped(List<Piece> thePieceVector)
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			for (int i = 0; i < thePieceVector.size<Piece>(); i++)
			{
				Piece piece = thePieceVector[i];
				digGoal.mOldPieceData[1, piece.mCol] = digGoal.mOldPieceData[0, piece.mCol];
				digGoal.mOldPieceData[0, piece.mCol].mColor = -1;
				digGoal.mOldPieceData[0, piece.mCol].mFlags = -1;
			}
			return true;
		}

		public override void SwapSucceeded(SwapData theSwapData)
		{
			base.SwapSucceeded(theSwapData);
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			if (digGoal.mNextBottomHypermixerWait > 0)
			{
				digGoal.mNextBottomHypermixerWait--;
			}
		}

		public override void RemoveFromPieceMap(int theId)
		{
			base.RemoveFromPieceMap(theId);
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			for (int i = 0; i < digGoal.mMovingPieces.size<Piece>(); i++)
			{
				if (digGoal.mMovingPieces[i] != null && digGoal.mMovingPieces[i].mId == theId)
				{
					digGoal.mMovingPieces[i] = null;
				}
			}
		}

		public override void HypermixerDropped()
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			digGoal.mNextBottomHypermixerWait = GlobalMembers.M(10);
		}

		public override void KeyChar(char theChar)
		{
			if (!GlobalMembers.gApp.mDebugKeysEnabled)
			{
				return;
			}
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			base.KeyChar(theChar);
		}

		public override string GetMusicName()
		{
			return this.mMusicName;
		}

		public override void Update()
		{
			base.Update();
			if (this.mQuestGoal != null)
			{
				DigGoal digGoal = (DigGoal)this.mQuestGoal;
				digGoal.ResyncInitPushAnim();
			}
			this.mRotatingCounter.Update();
		}

		public override bool WantFreezeAutoplay()
		{
			DigGoal digGoal = (DigGoal)this.mQuestGoal;
			return base.WantFreezeAutoplay() || digGoal.CheckNeedScroll(true);
		}

		public override bool WantsCalmEffects()
		{
			return false;
		}

		public override void Init()
		{
			if (this.mIsPerpetual)
			{
				this.mUiConfig = Board.EUIConfig.eUIConfig_StandardNoReplay;
				this.mLevelBarSizeBias = GlobalMembers.MS(24);
			}
			base.Init();
			if (this.mIsPerpetual)
			{
				this.SetColorCount(4);
			}
			this.mMaxTicksLeft = 0;
		}

		public override void NewGame(bool restartingGame)
		{
			base.NewGame(restartingGame);
			if (this.mParams.ContainsKey("MegaTimeBonus"))
			{
				this.mMegaTimeBonus = GlobalMembers.sexyatoi(this.mParams["MegaTimeBonus"]);
			}
			if (this.mIsPerpetual)
			{
				DigGoal digGoal = (DigGoal)this.mQuestGoal;
				this.mPowerGemThreshold = digGoal.mPowerGemThresholdDepth0;
			}
			Bej3Widget.SetOverlayType(Bej3Widget.OVERLAY_TYPE.OVERLAY_RUSTY);
		}

		public override void PlayMenuMusic()
		{
			if (this.mGameOverCount == 0)
			{
				GlobalMembers.gApp.mMusic.PlaySongNoDelay(13, true);
			}
		}

		public override float GetGravityFactor()
		{
			return GlobalMembers.M(1.1f);
		}

		public override bool IsGameIdle()
		{
			return this.mQuestGoal.IsGameIdle();
		}

		public override int GetTimerYOffset()
		{
			return ConstantsWP.DIG_BOARD_TIMER_OFFSET_Y;
		}

		public string mMusicName = string.Empty;

		public int mMaxTicksLeft;

		public int mMegaTimeBonus;

		public CurvedVal mCvCrack = new CurvedVal();

		public RotatingCounter mRotatingCounter;

		public PopAnim mCogsAnim;
	}
}
