using System;
using Sexy;

namespace BejeweledLIVE
{
	public class AchievementFrame : InterfaceControl, DialogListener
	{
		public AchievementFrame(GameApp aApp)
		{
			this.mApp = aApp;
			this.mAchievements = new Widget();
			this.AddWidget(this.mAchievements);
			this.mAchievementList = new AchievementListWidget(aApp, 0, this);
			this.mScroller = new ScrollWidget();
			this.mScroller.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mScroller.AddWidget(this.mAchievementList);
			this.mAchievements.AddWidget(this.mScroller);
		}

		public void Layout()
		{
			TRect achievementScreen_Bounds = Constants.mConstants.AchievementScreen_Bounds;
			this.mAchievements.Resize(achievementScreen_Bounds);
			achievementScreen_Bounds.mX = 0;
			achievementScreen_Bounds.mY = 0;
			this.mAchievementList.RefreshList();
			this.mAchievementList.mWidth = achievementScreen_Bounds.mWidth;
			this.mScroller.Resize(achievementScreen_Bounds);
			this.mScroller.SetIndicatorsInsets(new Insets(2, 2, 4, 2));
			this.mScroller.FlashIndicators();
			this.mScroller.SetVisible(true);
			this.mScroller.SetColor(0, Constants.SCROLLER_BACK_COLOR);
			this.mScroller.EnableBackgroundFill(true);
			this.mScroller.AddOverlayImage(AtlasResources.IMAGE_LEADERBOARD_LIST_TOP_OV, new CGPoint(0f, 0f));
			this.mScroller.AddOverlayImage(AtlasResources.IMAGE_LEADERBOARD_LIST_BOTTOM_OV, new CGPoint(0f, (float)(this.mScroller.mHeight - AtlasResources.IMAGE_LEADERBOARD_LIST_BOTTOM_OV.GetHeight())));
			int rowIndex = 0;
			TRect rowRect = this.mAchievementList.GetRowRect(rowIndex);
			this.mScroller.ScrollToPoint(new CGPoint((float)rowRect.mX, (float)rowRect.mY), false);
		}

		public override void FadeOut(float startSeconds, float endSeconds)
		{
			base.FadeOut(startSeconds, endSeconds);
			this.mScroller.FadeOut(startSeconds, endSeconds);
			this.mAchievementList.FadeOut(startSeconds, endSeconds);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void SetOpacity(float opacity)
		{
			base.SetOpacity(opacity);
			this.mScroller.SetOpacity(opacity);
			this.mAchievementList.SetOpacity(opacity);
		}

		public override void FadeIn(float startSeconds, float endSeconds)
		{
			base.FadeIn(startSeconds, endSeconds);
			this.mScroller.FadeIn(startSeconds, endSeconds);
			this.mAchievementList.FadeIn(startSeconds, endSeconds);
		}

		public void DialogButtonPress(int dialogId, int buttonId)
		{
		}

		public void DialogButtonDepress(int dialogId, int buttonId)
		{
		}

		public GameApp mApp;

		public Widget mAchievements;

		public ScrollWidget mScroller;

		public AchievementListWidget mAchievementList;

		public FrameWidget mFrame;

		public enum ButtonID
		{
			ACHIEVEMENT_LIST_ID
		}
	}
}
