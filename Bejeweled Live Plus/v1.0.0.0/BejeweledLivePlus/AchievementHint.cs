using System;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;

namespace BejeweledLivePlus
{
	public class AchievementHint
	{
		private event AchievementHintFinishedHandler OnFinished;

		public AchievementHint(string achievementName, AchievementHintFinishedHandler handler)
		{
			this.mStage = AchievementHint.HintStage.FadeIn;
			this.mText = GlobalMembers._ID("You have earned the", 3488) + " " + achievementName;
			this.mColor = new Color(255, 255, 255, 0);
			this.OnFinished += handler;
			this.mElapsed = 0;
		}

		public void Update()
		{
			switch (this.mStage)
			{
			case AchievementHint.HintStage.FadeIn:
				if (this.mElapsed >= 1000)
				{
					this.mStage = AchievementHint.HintStage.Display;
					this.mElapsed = 0;
					return;
				}
				this.mElapsed += GlobalMembers.gApp.ElapsedTime;
				this.mColor.mAlpha = (int)(255f * ((float)this.mElapsed / 1000f));
				if (this.mColor.mAlpha > 255)
				{
					this.mColor.mAlpha = 255;
					return;
				}
				break;
			case AchievementHint.HintStage.Display:
				this.mColor.mAlpha = 255;
				if (this.mElapsed > 7000)
				{
					this.mElapsed = 0;
					this.mStage = AchievementHint.HintStage.FadeOut;
					return;
				}
				this.mElapsed += GlobalMembers.gApp.ElapsedTime;
				return;
			case AchievementHint.HintStage.FadeOut:
				if (this.mElapsed >= 1000)
				{
					this.mStage = AchievementHint.HintStage.HoldToRemove;
					this.mElapsed = 0;
					return;
				}
				this.mElapsed += GlobalMembers.gApp.ElapsedTime;
				this.mColor.mAlpha = (int)(255f * ((float)(1000 - this.mElapsed) / 1000f));
				if (this.mColor.mAlpha < 0)
				{
					this.mColor.mAlpha = 0;
					return;
				}
				break;
			case AchievementHint.HintStage.HoldToRemove:
				if (this.mElapsed > 3000)
				{
					this.OnFinished(this);
					return;
				}
				this.mElapsed += GlobalMembers.gApp.ElapsedTime;
				break;
			default:
				return;
			}
		}

		public void Draw(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			int num = (int)ConstantsWP.DEVICE_WIDTH_F / 2;
			int num2 = ConstantsWP.BOARD_UI_HINT_BTN_Y + ConstantsWP.DIALOGBOX_BUTTON_MEASURE_HEIGHT + ConstantsWP.DIALOGBOX_EXTRA_HEIGHT - g.GetFont().GetAscent() / 2;
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, this.mColor);
			float mScaleX = g.mScaleX;
			float mScaleY = g.mScaleY;
			g.SetScale(1f, 1f, (float)num, (float)num2);
			g.WriteString(this.mText, num, num2);
			g.mScaleX = mScaleX;
			g.mScaleY = mScaleY;
		}

		private AchievementHint.HintStage mStage;

		private Color mColor;

		private string mText;

		private int mElapsed;

		private enum HintStage
		{
			FadeIn,
			Display,
			FadeOut,
			HoldToRemove
		}
	}
}
