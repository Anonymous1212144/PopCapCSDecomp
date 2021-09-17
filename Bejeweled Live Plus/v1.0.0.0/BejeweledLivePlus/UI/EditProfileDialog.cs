using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class EditProfileDialog : ProfileMenuBase, ScrollWidgetListener, Bej3EditListener, EditListener
	{
		public virtual void EditWidgetText(int id, string text)
		{
		}

		private void HighlightSelectedButton()
		{
			int profilePictureId;
			if (this.mSelectedProfilePicture >= 0)
			{
				profilePictureId = this.mSelectedProfilePicture;
			}
			else
			{
				profilePictureId = GlobalMembers.gApp.mProfile.GetProfilePictureId();
			}
			if (profilePictureId >= 0 && profilePictureId < 30)
			{
				this.mContainer.mSelection = new Point(this.mContainer.mImageLibrary[profilePictureId].mX + this.mContainer.mImageLibrary[profilePictureId].mWidth / 2, this.mContainer.mImageLibrary[profilePictureId].mY + this.mContainer.mImageLibrary[profilePictureId].mHeight / 2);
				this.mContainer.mSelectedImg = GlobalMembersResourcesWP.GetImageById(742 + profilePictureId);
			}
			if (this.mState == Bej3WidgetState.STATE_IN || this.mState == Bej3WidgetState.STATE_FADING_IN)
			{
				GlobalMembers.gApp.mProfile.SetProfilePictureId(profilePictureId);
				GlobalMembers.gApp.mProfile.WriteProfile();
				GlobalMembers.gApp.mProfile.WriteProfileList();
			}
		}

		public EditProfileDialog()
			: base(Menu_Type.MENU_EDITPROFILEMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mFirstTime = false;
			this.mSelectedProfilePicture = -1;
			this.mDrawnSinceChange = false;
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, ConstantsWP.EDITPROFILEMENU_WIDTH, GlobalMembers.gApp.mHeight);
			this.mFinalY = 0;
			this.mUpdateWhenNotVisible = true;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.EDITPROFILEMENU_HEADING_X, ConstantsWP.EDITPROFILEMENU_HEADING_Y, 0, 0);
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			this.mGetPicLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mGetPicLabel.Resize(ConstantsWP.EDITPROFILEMENU_GET_PIC_X, ConstantsWP.EDITPROFILEMENU_GET_PIC_Y, 0, 0);
			this.mGetPicLabel.SetText(GlobalMembers._ID("Choose a profile picture:", 3284));
			this.AddWidget(this.mGetPicLabel);
			this.mPlayerNameLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mPlayerNameLabel.SetText(GlobalMembers.gApp.mProfile.mProfileName);
			this.mPlayerNameLabel.Resize(ConstantsWP.EDITPROFILEMENU_HEADING_X, ConstantsWP.EDITPROFILEMENU_HEADING_Y, 0, 0);
			this.mPlayerNameLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH - 50);
			this.AddWidget(this.mPlayerNameLabel);
			this.mEditNameButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG, true);
			this.mEditNameButton.SetLabel(GlobalMembers._ID("EDIT NAME", 3285));
			Bej3Widget.CenterWidgetAt(ConstantsWP.EDITPROFILEMENU_EDIT_NAME_X, ConstantsWP.EDITPROFILEMENU_EDIT_NAME_Y, this.mEditNameButton, true, false);
			int num = 100;
			this.mSaveButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			Bej3Widget.CenterWidgetAt(ConstantsWP.EDITPROFILEMENU_SAVE_X, ConstantsWP.EDITPROFILEMENU_SAVE_Y + num, this.mSaveButton, true, false);
			this.AddWidget(this.mSaveButton);
			this.mPlayerImage = new ImageWidget(712, true);
			this.mPlayerImage.Resize(ConstantsWP.EDITPROFILEMENU_PLAYER_IMAGE_X, ConstantsWP.EDITPROFILEMENU_PLAYER_IMAGE_Y, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE);
			this.AddWidget(this.mPlayerImage);
			this.mImageLibraryScrollWidget = new ScrollWidget(this);
			this.mContainer = new EditProfileDialogImageContainer(this);
			this.mImageLibraryScrollWidget.Resize(ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_X, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_Y, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_WIDTH, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_HEIGHT);
			this.mImageLibraryScrollWidget.AddWidget(this.mContainer);
			this.mImageLibraryScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_DISABLED);
			this.AddWidget(this.mImageLibraryScrollWidget);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(0);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			int num = (int)this.mAlphaCurve;
			g.SetColor(new Color(255, 255, 255, 255 * num * num * num));
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.EDITPROFILEMENU_DIVIDER_1_Y);
			this.mDrawnSinceChange = true;
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			this.mDrawnSinceChange = false;
			this.mWidgetManager.SetFocus(null);
			if (theId >= 100000)
			{
				this.mSelectedProfilePicture = theId - 100000;
				this.SetUpPlayerImage(this.mSelectedProfilePicture);
				this.HighlightSelectedButton();
				return;
			}
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.mProfile.SetProfilePictureId(this.mPlayerImage.GetImageId() - 712);
				GlobalMembers.gApp.mProfile.RenameProfile(GlobalMembers.gApp.mProfile.mProfileName, this.mDisplayName);
				GlobalMembers.gApp.mProfile.WriteProfile();
				GlobalMembers.gApp.mProfile.WriteProfileList();
				this.mSelectedProfilePicture = -1;
				GlobalMembers.gApp.DoMainMenu();
				((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).Expand();
				base.Transition_SlideOut();
				return;
			case 1:
				GlobalMembers.gApp.DoRenameUserDialog();
				return;
			default:
				if (theId != 10001)
				{
					return;
				}
				GlobalMembers.gApp.DoMainMenu();
				((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).Expand();
				base.Transition_SlideOut();
				return;
			}
		}

		public override void LinkUpAssets()
		{
			this.mEditNameButton.LinkUpAssets();
			this.mSaveButton.LinkUpAssets();
			this.SetUpPlayerImage(this.mSelectedProfilePicture);
			this.mContainer.LinkUpAssets();
			base.LinkUpAssets();
			this.HighlightSelectedButton();
		}

		public override void Show()
		{
			this.SetDisplayedName(GlobalMembers.gApp.mProfile.mProfileName);
			this.mSelectedProfilePicture = -1;
			this.mTopButton.SetType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			this.mContainer.Show();
			base.Show();
			this.SetVisible(false);
		}

		public override void ShowCompleted()
		{
			this.mTopButton.SetType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			this.mTopButton.SetDisabled(false);
		}

		public override void Hide()
		{
			this.mSelectedProfilePicture = -1;
			base.Hide();
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
		}

		public override bool ButtonsEnabled()
		{
			bool result = base.ButtonsEnabled();
			if (this.mLoading)
			{
				result = false;
			}
			if (!this.mDrawnSinceChange)
			{
				result = false;
			}
			return result;
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public virtual void SetupForFirstShow(bool firstTime)
		{
			base.ShowBackButton(!firstTime);
			this.mFirstTime = firstTime;
			this.mEditNameButton.mVisible = (this.mEditNameButton.mMouseVisible = !firstTime);
			this.mEditNameButton.mDisabled = firstTime;
			if (firstTime)
			{
				this.mPlayerImage.Resize(ConstantsWP.EDITPROFILEMENU_PLAYER_IMAGE_X_FIRSTTIME, ConstantsWP.EDITPROFILEMENU_PLAYER_IMAGE_Y_FIRSTTIME, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE);
				this.mPlayerNameLabel.SetFont(GlobalMembersResources.FONT_HUGE);
				this.mPlayerNameLabel.Resize(ConstantsWP.EDITPROFILEMENU_NAME_LABEL_X_FIRST_TIME, ConstantsWP.EDITPROFILEMENU_NAME_LABEL_Y_FIRST_TIME, 0, 0);
				this.mHeadingLabel.SetText("");
				this.mSaveButton.SetLabel(GlobalMembers._ID("CONTINUE", 3573));
				this.mSaveButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
				return;
			}
			this.mPlayerImage.Resize(ConstantsWP.EDITPROFILEMENU_PLAYER_IMAGE_X, ConstantsWP.EDITPROFILEMENU_PLAYER_IMAGE_Y, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE, ConstantsWP.LARGE_PROFILE_PICTURE_SIZE);
			this.mPlayerNameLabel.SetFont(GlobalMembersResources.FONT_HUGE);
			this.mPlayerNameLabel.Resize(ConstantsWP.EDITPROFILEMENU_NAME_LABEL_X, ConstantsWP.EDITPROFILEMENU_NAME_LABEL_Y + 30, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("EDIT PROFILE", 3287));
			this.mSaveButton.SetLabel(GlobalMembers._ID("BACK", 3288));
			this.mSaveButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
		}

		public void SetDisplayedName(string name)
		{
			this.mDisplayName = name;
		}

		public void ResetDisplayedPicture()
		{
			this.mSelectedProfilePicture = -1;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public virtual bool AllowKey(int theId, KeyCode theKey)
		{
			return true;
		}

		public virtual bool AllowChar(int theId, char theChar)
		{
			return true;
		}

		public virtual bool AllowText(int theId, string theText)
		{
			return true;
		}

		public override void GotFocus()
		{
			base.GotFocus();
		}

		public virtual void EditWidgetGotFocus(int theId)
		{
		}

		public virtual void EditWidgetLostFocus(int theId)
		{
		}

		public bool IsEditingName()
		{
			return GlobalMembers.gApp.IsKeyboardShowing();
		}

		public override void SetUpPlayerImage(int overridePresetId)
		{
			if (this.mState == Bej3WidgetState.STATE_OUT)
			{
				return;
			}
			if (this.mPlayerImage == null)
			{
				return;
			}
			bool mLoading = this.mLoading;
			this.mLoading = true;
			if (overridePresetId >= 0 || GlobalMembers.gApp.mProfile.UsesPresetProfilePicture())
			{
				int num;
				if (overridePresetId >= 0)
				{
					num = overridePresetId;
				}
				else
				{
					num = GlobalMembers.gApp.mProfile.GetProfilePictureId();
				}
				this.mLoadedProfilePictureId = num;
				for (int i = 0; i < 30; i++)
				{
					BejeweledLivePlusApp.LoadContent(string.Format("ProfilePic_{0}", i), false);
				}
				this.mPlayerImage.SetImage(num + 712);
			}
			if (!mLoading)
			{
				this.LinkUpAssets();
			}
			this.mLoading = false;
		}

		private bool mDrawnSinceChange;

		private Label mGetPicLabel;

		private Bej3Button mSaveButton;

		private ScrollWidget mImageLibraryScrollWidget;

		private EditProfileDialogImageContainer mContainer;

		private int mSelectedProfilePicture;

		private Label mHeadingLabel;

		private Bej3Button mEditNameButton;

		private Label mPlayerNameLabel;

		public string mDisplayName;

		public bool mFirstTime;

		private enum EditProfileDialog_IDS
		{
			BTN_SAVE_ID,
			BTN_EDIT_NAME_ID
		}
	}
}
