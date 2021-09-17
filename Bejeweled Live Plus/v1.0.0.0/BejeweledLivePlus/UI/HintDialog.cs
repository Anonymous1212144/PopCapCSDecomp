using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class HintDialog : Bej3Dialog, CheckboxListener
	{
		public HintDialog(string theHeader, string theText, bool allowReplay, bool disableBox, Piece tutorialPiece, Board theBoard)
			: base(18, true, theHeader, string.Empty, string.Empty, allowReplay ? 2 : 3, Bej3ButtonType.BUTTON_TYPE_CUSTOM, Bej3ButtonType.BUTTON_TYPE_CUSTOM, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mTextContainer = null;
			this.mYesButton.mBtnNoDraw = true;
			this.mYesButton.Resize(0, 0, 0, 0);
			if (this.mTopButton != null)
			{
				this.mTopButton.mId = this.mYesButton.mId;
			}
			this.mHeadingLabel.SetVisible(false);
			this.Resize(0, 0, 0, 0);
			this.mWantDisableBox = disableBox;
			if (this.mWantDisableBox)
			{
				this.mNoHintsCheckbox = new Bej3Checkbox(0, this);
				this.mNoHintsCheckbox.mChecked = false;
			}
			else
			{
				this.mNoHintsCheckbox = null;
			}
			int num;
			if (allowReplay)
			{
				num = ConstantsWP.HINTDIALOG_TEXT_WIDTH_REPLAY;
				this.mNoButton.mLabel = GlobalMembers._ID("REPLAY", 266);
				this.mNoButton.SetVisible(true);
				((Bej3Button)this.mNoButton).SetType(Bej3ButtonType.BUTTON_TYPE_HINT_CAMERA);
				((Bej3Button)this.mNoButton).Resize(0, 0, 0, 0);
				Bej3Widget.CenterWidgetAt(ConstantsWP.HINTDIALOG_TEXT_X + num + (ConstantsWP.HINTDIALOG_WIDTH - (ConstantsWP.HINTDIALOG_TEXT_X + num)) / 2, ConstantsWP.HINTDIALOG_BUTTON_Y, this.mNoButton);
			}
			else
			{
				num = ConstantsWP.HINTDIALOG_TEXT_WIDTH_NO_REPLAY;
			}
			this.mTextContainer = new TextContainer(theText, num);
			if (this.mTextContainer.mText.GetTextBlock().mHeight <= ConstantsWP.HINTDIALOG_TEXT_NO_SCROLL_HEIGHT)
			{
				this.mScrollWidget = null;
				this.mTextContainer.mX = ConstantsWP.HINTDIALOG_TEXT_X;
				this.mTextContainer.mY = ConstantsWP.HINTDIALOG_TEXTCONTAINER_Y - this.mTextContainer.mHeight / 2;
				this.AddWidget(this.mTextContainer);
			}
			else
			{
				this.mScrollWidget = new ScrollWidget(this.mTextContainer);
				this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_VERTICAL);
				this.mScrollWidget.EnableBounce(true);
				this.mScrollWidget.Resize(ConstantsWP.HINTDIALOG_TEXT_X, ConstantsWP.HINTDIALOG_TEXT_Y, num, ConstantsWP.HINTDIALOG_TEXT_HEIGHT);
				this.mScrollWidget.AddWidget(this.mTextContainer);
				this.AddWidget(this.mScrollWidget);
			}
			this.Resize(ConstantsWP.HINTDIALOG_X, ConstantsWP.HINTDIALOG_Y, ConstantsWP.HINTDIALOG_WIDTH, ConstantsWP.HINTDIALOG_HEIGHT);
			this.BringToFront(this.mNoButton);
			this.mDialogListener = GlobalMembers.gApp.mBoard;
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		public override void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back)
			{
				args.processed = true;
				this.ButtonDepress(10001);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			if (this.mNoHintsCheckbox != null)
			{
				this.mNoHintsCheckbox.Dispose();
				this.mNoHintsCheckbox = null;
			}
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth, 0f, false);
			if (this.mScrollWidget != null)
			{
				Rect rect = this.mScrollWidget.GetRect();
				Bej3Widget.DrawSwipeInlay(g, rect.mY, rect.mHeight, this.mWidth, false);
			}
		}

		public override void Update()
		{
			GlobalMembers.gApp.mDoFadeBackForDialogs = false;
			base.Update();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			this.mTargetPos = theY;
			theY = GlobalMembers.gApp.mHeight;
			base.superSubResize(theX, theY, theWidth, theHeight);
			this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			base.AllowSlideIn(allow, previousTopButton);
			bool mDoFadeBackForDialogs = false;
			if (Bej3Widget.mCurrentSlidingMenu != null && Bej3Widget.mCurrentSlidingMenu.mShouldFadeBehind)
			{
				mDoFadeBackForDialogs = true;
			}
			GlobalMembers.gApp.mDoFadeBackForDialogs = mDoFadeBackForDialogs;
		}

		public void CheckboxChecked(int theId, bool isChecked)
		{
			throw new NotImplementedException();
		}

		public Bej3Checkbox mNoHintsCheckbox;

		public bool mWantDisableBox;

		public TextContainer mTextContainer;

		public ScrollWidget mScrollWidget;

		private enum HINTDIALOG_IDS
		{
			CHK_DISABLE_ID
		}
	}
}
