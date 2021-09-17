using System;
using System.Globalization;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class ZenOptionsMenu : Bej3Widget
	{
		private void Init()
		{
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE, GlobalMembers._ID("ZEN OPTIONS", 3631), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE);
			this.mHeadingLabel.Resize(GlobalMembers.gApp.mWidth / 2, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			this.mZenOptionsContainer = new ZenOptionsContainer();
			this.mZenOptionsContainer.Resize(0, ConstantsWP.ZENOPTIONS_CONTAINER_Y, ConstantsWP.ZENOPTIONS_CENTER_X * 2, GlobalMembers.gApp.mHeight - ConstantsWP.ZENOPTIONS_CONTAINER_Y);
			this.AddWidget(this.mZenOptionsContainer);
			int num = ConstantsWP.DIALOG_HEADING_LABEL_Y + GlobalMembersResources.FONT_HUGE.GetHeight();
			this.mDescLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("Customise your Zen Experience!", 3632), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE);
			this.mDescLabel.Resize(GlobalMembers.gApp.mWidth / 2, num, 0, 0);
			this.AddWidget(this.mDescLabel);
			num += GlobalMembersResources.FONT_SUBHEADER.GetHeight() * 3 / 2;
			this.mAmbientSoundBtn = new Bej3Button(18, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mAmbientSoundBtn.SetLabel(GlobalMembers._ID("AMBIENT SOUNDS", 3633));
			this.mAmbientSoundBtn.Resize(ConstantsWP.ZENOPTIONS_CENTER_X - ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH / 2, num, ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH, 0);
			this.AddWidget(this.mAmbientSoundBtn);
			num += ConstantsWP.ZENOPTIONS_BUTTON_STEP_Y;
			this.mMantrasBtn = new Bej3Button(19, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mMantrasBtn.SetLabel(GlobalMembers._ID("MANTRAS", 3634));
			this.mMantrasBtn.Resize(ConstantsWP.ZENOPTIONS_CENTER_X - ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH / 2, num, ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH, 0);
			this.AddWidget(this.mMantrasBtn);
			num += ConstantsWP.ZENOPTIONS_BUTTON_STEP_Y;
			this.mBreathModBtn = new Bej3Button(20, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mBreathModBtn.SetLabel(GlobalMembers._ID("BREATHING MODULATION", 3635));
			this.mBreathModBtn.Resize(ConstantsWP.ZENOPTIONS_CENTER_X - ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH / 2, num, ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH, 0);
			this.AddWidget(this.mBreathModBtn);
			this.mBackButton = new Bej3Button(21, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3636));
			this.mBackButton.Resize(ConstantsWP.ZENOPTIONS_CENTER_X - ConstantsWP.BEJ3BUTTON_AUTOSCALE_DEFAULT_WIDTH / 2, ConstantsWP.ZENOPTIONS_BACK_BTN_Y, ConstantsWP.BEJ3BUTTON_AUTOSCALE_DEFAULT_WIDTH, 0);
			this.AddWidget(this.mBackButton);
			num += ConstantsWP.ZENOPTIONS_BUTTON_STEP_Y;
			this.mResumeButton = new Bej3Button(22, this, Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			this.mResumeButton.SetLabel(GlobalMembers._ID("RESUME", 3637));
			this.mResumeButton.Resize(ConstantsWP.ZENOPTIONS_CENTER_X - ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH / 2, num, ConstantsWP.BEJ3BUTTON_LONG_DEFAULT_WIDTH, 0);
			this.AddWidget(this.mResumeButton);
			num += ConstantsWP.ZENOPTIONS_BUTTON_STEP_Y;
			this.SetMode(ZenOptionsMode.MODE_MENU_SELECT);
		}

		protected ZenOptionsMenu(Menu_Type type, bool hasBackButton, Bej3ButtonType topButtonType)
			: base(type, hasBackButton, topButtonType)
		{
			this.Init();
			this.LinkUpAssets();
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		public ZenOptionsMenu()
			: base(Menu_Type.MENU_ZENOPTIONSMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Init();
			this.LinkUpAssets();
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back)
			{
				args.processed = true;
				int theId = 10001;
				if (this.mMode == ZenOptionsMode.MODE_AMBIENT_SOUNDS || this.mMode == ZenOptionsMode.MODE_BREATH_MOD || this.mMode == ZenOptionsMode.MODE_MANTRAS)
				{
					theId = 21;
				}
				this.ButtonDepress(theId);
			}
		}

		public override void Draw(Graphics g)
		{
			GlobalMembers.gApp.mWidgetManager.FlushDeferredOverlayWidgets(1);
			base.DrawFadedBack(g);
			g.SetColor(Color.White);
			Bej3Widget.DrawDialogBox(g, this.mWidth, 0f, true, true);
			if (this.mMode != ZenOptionsMode.MODE_MENU_SELECT)
			{
				Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.MENU_DIVIDER_Y);
			}
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			base.DrawOverlay(g, thePriority);
		}

		public override void Update()
		{
			if (this.mTransitionToMode != this.mMode && base.IsInOutPosition())
			{
				this.SetMode(this.mTransitionToMode);
				if (this.mTransitionToMode == ZenOptionsMode.MODE_MENU_SELECT)
				{
					this.mTargetPos = ConstantsWP.ZENOPTIONS_MENU_SELECT_Y;
					this.mBackButton.mY = ConstantsWP.ZENOPTIONS_BACK_BTN_Y;
				}
				else if (this.mTransitionToMode == ZenOptionsMode.MODE_BREATH_MOD)
				{
					this.mTargetPos = ConstantsWP.ZENOPTIONS_MENU_BREATH_MOD_Y;
					this.mBackButton.mY = ConstantsWP.ZENOPTIONS_MENU_BREATH_MOD_BACK_BTN_Y;
				}
				else
				{
					this.mTargetPos = 0;
					this.mBackButton.mY = ConstantsWP.ZENOPTIONS_BACK_BTN_Y;
				}
			}
			base.Update();
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			base.AllowSlideIn(allow, previousTopButton);
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 18:
				this.mTransitionToMode = ZenOptionsMode.MODE_AMBIENT_SOUNDS;
				this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
				return;
			case 19:
				this.mTransitionToMode = ZenOptionsMode.MODE_MANTRAS;
				this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
				return;
			case 20:
				this.mTransitionToMode = ZenOptionsMode.MODE_BREATH_MOD;
				this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
				return;
			case 21:
				this.mTransitionToMode = ZenOptionsMode.MODE_MENU_SELECT;
				this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
				return;
			case 22:
				break;
			default:
				if (theId != 10001)
				{
					return;
				}
				break;
			}
			this.Collapse();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.LinkUpAssets();
		}

		public override void HideCompleted()
		{
			BejeweledLivePlusApp.UnloadContent("ZenOptions");
			base.HideCompleted();
		}

		public override void LinkUpAssets()
		{
			if (this.mZenOptionsContainer != null)
			{
				this.mZenOptionsContainer.LinkUpAssets();
			}
		}

		public override void Show()
		{
			BejeweledLivePlusApp.LoadContent("ZenOptions");
			this.LinkUpAssets();
			this.mZenOptionsContainer.Show();
			base.Show();
		}

		public override void ShowCompleted()
		{
			if (this.mTopButton != null)
			{
				this.mTopButton.SetDisabled(false);
			}
			base.ShowCompleted();
		}

		public override void Hide()
		{
			Bej3WidgetState mState = this.mState;
			base.Hide();
			this.mZenOptionsContainer.Hide();
		}

		public void Expand()
		{
			Bej3Widget.DisableWidget(this.mZenOptionsContainer, false);
			GlobalMembers.gApp.DoZenOptionsMenu();
			this.SetMode(ZenOptionsMode.MODE_MENU_SELECT);
			this.mTargetPos = ConstantsWP.ZENOPTIONS_MENU_SELECT_Y;
			base.ResetFadedBack(true);
			GlobalMembers.gApp.DisableAllExceptThis(this, true);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			Bej3Widget.mCurrentSlidingMenu = this;
		}

		public void Collapse()
		{
			this.Collapse(false, false);
		}

		public void Collapse(bool fadeInstantly)
		{
			this.Collapse(fadeInstantly, false);
		}

		public void Collapse(bool fadeInstantly, bool fromRestart)
		{
			if (this.mZenOptionsContainer.mAmbientSoundId >= 0)
			{
				bool flag = this.mZenOptionsContainer.AmbientLoadSound(this.mZenOptionsContainer.mAmbientSoundId);
				this.mZenOptionsContainer.mAmbientSoundId = -1;
				this.mZenOptionsContainer.mAmbientSoundStartDelay = 0;
				if (flag && GlobalMembers.gApp.mProfile.mNoiseOn && !GlobalMembers.gApp.IsMuted() && GlobalMembers.gApp.mBoard != null)
				{
					ZenBoard zenBoard = (ZenBoard)GlobalMembers.gApp.mBoard;
					zenBoard.PlayZenNoise();
				}
			}
			GlobalMembers.gApp.GoBackToGame();
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			GlobalMembers.gApp.DisableAllExceptThis(this, false);
			if (this.mState == Bej3WidgetState.STATE_FADING_IN)
			{
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
			}
			base.Transition_SlideOut();
		}

		public void SetMode(ZenOptionsMode mode)
		{
			this.mTransitionToMode = mode;
			this.mMode = mode;
			this.mZenOptionsContainer.SetMode(mode);
			Bej3Widget.DisableWidget(this.mAmbientSoundBtn, mode != ZenOptionsMode.MODE_MENU_SELECT);
			Bej3Widget.DisableWidget(this.mMantrasBtn, mode != ZenOptionsMode.MODE_MENU_SELECT);
			Bej3Widget.DisableWidget(this.mBreathModBtn, mode != ZenOptionsMode.MODE_MENU_SELECT);
			Bej3Widget.DisableWidget(this.mBackButton, mode == ZenOptionsMode.MODE_MENU_SELECT);
			Bej3Widget.DisableWidget(this.mResumeButton, mode != ZenOptionsMode.MODE_MENU_SELECT);
			Bej3Widget.DisableWidget(this.mDescLabel, mode != ZenOptionsMode.MODE_MENU_SELECT);
			string name = CultureInfo.CurrentCulture.Name;
			switch (mode)
			{
			case ZenOptionsMode.MODE_MENU_SELECT:
				this.mHeadingLabel.Resize(GlobalMembers.gApp.mWidth / 2, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
				this.mHeadingLabel.SetText(GlobalMembers._ID("Zen Options", 3483));
				return;
			case ZenOptionsMode.MODE_AMBIENT_SOUNDS:
				if (name == "de-DE" || name == "es-ES" || name == "it-IT")
				{
					this.mHeadingLabel.Resize(GlobalMembers.gApp.mWidth / 2, ConstantsWP.DIALOG_HEADING_LABEL_Y + 45, 0, 0);
				}
				else
				{
					this.mHeadingLabel.Resize(GlobalMembers.gApp.mWidth / 2, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
				}
				this.mHeadingLabel.SetText(GlobalMembers._ID("Ambient Sounds", 3484));
				return;
			case ZenOptionsMode.MODE_MANTRAS:
				this.mHeadingLabel.Resize(GlobalMembers.gApp.mWidth / 2, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
				this.mHeadingLabel.SetText(GlobalMembers._ID("Mantras", 3485));
				return;
			case ZenOptionsMode.MODE_BREATH_MOD:
				this.mHeadingLabel.Resize(GlobalMembers.gApp.mWidth / 2, ConstantsWP.DIALOG_HEADING_LABEL_Y + 45, 0, 0);
				this.mHeadingLabel.SetText(GlobalMembers._ID("Breathing Modulation", 3486));
				return;
			default:
				return;
			}
		}

		public override void PlayMenuMusic()
		{
		}

		private ZenOptionsMode mMode;

		private ZenOptionsMode mTransitionToMode;

		private Label mHeadingLabel;

		private Bej3ScrollWidget mScrollWidget;

		private ZenOptionsContainer mZenOptionsContainer;

		private Label mDescLabel;

		private Bej3Button mAmbientSoundBtn;

		private Bej3Button mMantrasBtn;

		private Bej3Button mBreathModBtn;

		private Bej3Button mBackButton;

		private Bej3Button mResumeButton;
	}
}
