using System;
using Sexy;

namespace BejeweledLIVE
{
	public class TrialAchievementAlert : Alert
	{
		public TrialAchievementAlert(Dialogs id, DialogListener listener, GameApp app, Achievements.AchievementId achievementId)
			: base(id, listener, app)
		{
			this.mAchievement = Achievements.GetAchievementItem(achievementId);
			this.SetHeadingText(Strings.Achievement_Unlocked);
			base.SetBodyText(Common.StrFormat_(Strings.TrialAchievementAlert_Achievement_Unlocked, new object[] { this.mAchievement.Name }));
		}

		public override void SetHeadingText(string theText)
		{
			this.mHeadingText = theText;
		}

		protected override void Layout()
		{
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			Insets insets = new Insets(this.ALERT_INSETS);
			int mWidth = this.mApp.mWidth;
			int num = mWidth - insets.mRight;
			int num2 = num - insets.mLeft;
			int mTop = insets.mTop;
			this.mFancyText.Resize(insets.mLeft, mTop, num2, 0);
			this.mFancyText.Clear();
			this.mFancyText.SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
			this.mFancyText.SetComposeFont(Resources.FONT_TEXT);
			if (!string.IsNullOrEmpty(this.mHeadingText))
			{
				this.mFancyText.SetComposeColor(SexyColor.White);
				this.mFancyText.AddWrappedText(this.mHeadingText);
				this.mFancyText.NewLine(10);
				this.mFancyText.ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
			}
			this.mFancyText.SetComposeColor(SexyColor.White);
			this.mFancyText.AddImage(this.mAchievement.AchievementImage);
			this.mFancyText.NewLine(1);
			this.mFancyText.SetComposeFont(Resources.FONT_TINY_TEXT);
			for (int i = 0; i < this.mBodyText.Count; i++)
			{
				this.mFancyText.AddWrappedText(this.mBodyText[i]);
				if (i < this.mBodyText.Count - 2)
				{
					this.mFancyText.NewLine(4);
				}
			}
			this.mFancyText.NewLine(4);
			this.mFancyText.ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
			int num3 = this.mFancyText.Bottom();
			int num4 = insets.mLeft;
			num3 = num3;
			foreach (PodButton podButton in this.mButtons)
			{
				int mHeight = podButton.mHeight;
				int layoutWidth = podButton.GetLayoutWidth(num2);
				podButton.Resize(num4, num3, layoutWidth, mHeight);
				num4 += layoutWidth;
				if (num4 >= num)
				{
					num4 = insets.mLeft;
					num3 += mHeight;
				}
				podButton.SetFont(Resources.FONT_BUTTON);
			}
			num3 += insets.mBottom;
			this.mBoxFrame.mX = (this.mApp.mWidth - mWidth) / 2;
			this.mBoxFrame.mY = (this.mApp.mHeight - num3) / 2;
			this.mBoxFrame.mWidth = mWidth;
			this.mBoxFrame.mHeight = num3;
			foreach (Widget widget in this.mWidgets)
			{
				widget.Move(widget.mX + this.mBoxFrame.mX, widget.mY + this.mBoxFrame.mY);
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			TRect theRect = new TRect(0, 0, this.mWidth, this.mHeight);
			g.SetColor(new SexyColor(0, 0, 0, this.mDimmerOpacity));
			g.FillRect(theRect);
			TRect mBoxFrame = this.mBoxFrame;
			int theX = (int)(((float)mBoxFrame.mWidth * this.mWidgetScale - (float)mBoxFrame.mWidth) / 2f);
			int theY = (int)(((float)mBoxFrame.mHeight * this.mWidgetScale - (float)mBoxFrame.mHeight) / 2f);
			mBoxFrame.Inflate(theX, theY);
			TRect theRect2 = mBoxFrame;
			theRect2.mX += Constants.mConstants.AlertInsetDistance_X;
			theRect2.mWidth -= Constants.mConstants.AlertInsetDistance_X * 2;
			theRect2.mY += Constants.mConstants.AlertInsetDistance_Y;
			theRect2.mHeight -= Constants.mConstants.AlertInsetDistance_Y * 2;
			g.SetColor(new SexyColor(0, 0, 0, this.mWidgetOpacity));
			g.SetColorizeImages(true);
			g.FillRect(theRect2);
			g.SetColor(new SexyColor(255, 255, 255, this.mWidgetOpacity));
			g.SetColorizeImages(true);
			g.DrawImageBox(mBoxFrame, this.mImage);
		}

		public AchievementItem mAchievement;
	}
}
