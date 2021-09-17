using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class ProfileMenu : ProfileMenuBase
	{
		public ProfileMenu()
			: base(Menu_Type.MENU_PROFILEMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, ConstantsWP.PROFILEMENU_WIDTH, GlobalMembers.gApp.mHeight);
			this.mFinalY = 0;
			this.mContainer = new ProfileMenuContainer(this);
			this.mScrollWidget = new Bej3ScrollWidget(this.mContainer, false);
			this.mScrollWidget.Resize(ConstantsWP.PROFILEMENU_PADDING_X, ConstantsWP.PROFILEMENU_PADDING_TOP, this.mWidth - ConstantsWP.PROFILEMENU_PADDING_X * 2, this.mHeight - ConstantsWP.PROFILEMENU_PADDING_BOTTOM - ConstantsWP.PROFILEMENU_PADDING_TOP);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_DISABLED);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.AddWidget(this.mScrollWidget);
			this.mPlayerImage = new ImageWidget(712, true);
			this.AddWidget(this.mPlayerImage);
			this.mPlayerNameLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mPlayerNameLabel.Resize(ConstantsWP.PROFILEMENU_NAME_LABEL_X, ConstantsWP.PROFILEMENU_NAME_LABEL_Y, 0, 0);
			this.AddWidget(this.mPlayerNameLabel);
			this.mPlayerRankLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mPlayerRankLabel.Resize(ConstantsWP.PROFILEMENU_RANK_LABEL_X, ConstantsWP.PROFILEMENU_RANK_LABEL_Y, 0, 0);
			this.AddWidget(this.mPlayerRankLabel);
			this.mEditButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mEditButton.SetLabel(GlobalMembers._ID("EDIT PROFILE", 3427));
			this.mEditButton.Resize(0, 0, ConstantsWP.PROFILEMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.PROFILEMENU_EDIT_X, ConstantsWP.PROFILEMENU_BOTTOM_BUTTON_Y, this.mEditButton, true, false);
			this.AddWidget(this.mEditButton);
			this.mBackButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3428));
			this.mBackButton.Resize(0, 0, ConstantsWP.PROFILEMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.PROFILEMENU_BACK_X, ConstantsWP.PROFILEMENU_BOTTOM_BUTTON_Y, this.mBackButton, true, false);
			this.AddWidget(this.mBackButton);
			this.mRankBarWidget = new RankBarWidget(ConstantsWP.PROFILEMENU_RANKBAR_WIDTH);
			this.mRankBarWidget.mDrawRankName = true;
			this.mRankBarWidget.mDrawCrown = false;
			this.mRankBarWidget.Resize(ConstantsWP.PROFILEMENU_RANKBAR_X, ConstantsWP.PROFILEMENU_RANKBAR_Y, ConstantsWP.PROFILEMENU_RANKBAR_WIDTH, 0);
			this.AddWidget(this.mRankBarWidget);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(10001);
			}
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.PROFILEMENU_DIVIDER_NAME);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.PROFILEMENU_DIVIDER_RANK);
		}

		public override void LinkUpAssets()
		{
			this.mContainer.LinkUpAssets();
			base.LinkUpAssets();
			Graphics graphics = new Graphics();
			graphics.SetFont(this.mPlayerNameLabel.GetFont());
			this.mPlayerNameLabel.SetText(Utils.GetEllipsisString(graphics, GlobalMembers.gApp.mProfile.mProfileName, ConstantsWP.PROFILEMENU_NAME_LABEL_WIDTH));
			if (GlobalMembers.gApp.mProfile.UsesPresetProfilePicture())
			{
				this.mPlayerImage.SetImage(712 + GlobalMembers.gApp.mProfile.GetProfilePictureId());
			}
			this.mPlayerImage.Resize(ConstantsWP.PROFILEMENU_PLAYER_IMAGE_X, ConstantsWP.PROFILEMENU_PLAYER_IMAGE_Y, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE);
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.DoEditProfileMenu();
				base.Transition_SlideOut();
				return;
			case 1:
				GlobalMembers.gApp.DoMainMenu(true);
				base.Transition_SlideOut();
				return;
			case 2:
				GlobalMembers.gApp.DoStatsMenu();
				base.Transition_SlideOut();
				return;
			case 3:
				GlobalMembers.gApp.DoHighScoresMenu();
				base.Transition_SlideOut();
				return;
			case 4:
				if (GlobalMembers.gApp.mProfile.mDeferredBadgeVector.Count > 0)
				{
					GlobalMembers.gApp.DoBadgeMenu(2, GlobalMembers.gApp.mProfile.mDeferredBadgeVector);
					base.Transition_SlideOut();
					return;
				}
				GlobalMembers.gApp.DoBadgeMenu(0, null);
				base.Transition_SlideOut();
				return;
			default:
				if (theId != 10001)
				{
					return;
				}
				GlobalMembers.gApp.DoMainMenu();
				base.Transition_SlideOut();
				return;
			}
		}

		public override void InterfaceStateChanged(InterfaceState newState)
		{
			base.InterfaceStateChanged(newState);
			this.mContainer.InterfaceStateChanged(newState);
		}

		public override void Show()
		{
			Bej3WidgetState mState = this.mState;
			this.mContainer.Show();
			base.Show();
			this.mPlayerRankLabel.SetText(this.mRankBarWidget.GetRankName(GlobalMembers.gApp.mProfile.mOfflineRank, false));
			this.SetVisible(false);
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
		}

		public override void Hide()
		{
			this.mContainer.Hide();
			base.Hide();
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			base.WidgetDrawOverlay(g, thePriority);
		}

		public new void KeyChar(char theChar)
		{
		}

		private Bej3Button mEditButton;

		private Bej3Button mBackButton;

		private ProfileMenuContainer mContainer;

		private Bej3ScrollWidget mScrollWidget;

		private Label mPlayerNameLabel;

		private RankBarWidget mRankBarWidget;

		private Label mPlayerRankLabel;
	}
}
