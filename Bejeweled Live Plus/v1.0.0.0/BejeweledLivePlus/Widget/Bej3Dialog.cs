using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3Dialog : Dialog, Bej3ButtonListener, ButtonListener
	{
		protected bool FinishedSlide()
		{
			return Bej3Widget.FloatEquals((float)this.mY, (float)this.mTargetPos, ConstantsWP.DASHBOARD_SLIDER_SPEED);
		}

		protected virtual void SlideInFinished()
		{
		}

		protected virtual void KilledFinished()
		{
			if (this.mOpenURLWhenDone)
			{
				GlobalMembers.gApp.OpenLastConfirmedWebsite();
			}
		}

		public Bej3Dialog(int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode, Bej3ButtonType buttonType1, Bej3ButtonType buttonType2, Bej3ButtonType topButtonType)
			: base(null, null, theId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode)
		{
			this.mHeadingLabel = null;
			this.mMessageLabel = null;
			this.mButtons = new List<DialogButton>();
			this.mMouseInvisibleChildren = new LinkedList<Widget>();
			this.mCanSlideInMenus = true;
			this.mAnimationFraction = 0f;
			this.mOpenURLWhenDone = false;
			this.mAlphaCurve.SetConstant(1.0);
			this.mZOrder = 3;
			this.mFlushPriority = -1;
			this.mClip = false;
			this.mIsKilling = false;
			this.mCanEscape = false;
			this.mAllowDrag = true;
			this.mScaleCenterX = 0;
			this.mScaleCenterY = 0;
			this.SetColor(0, new Color(255, 255, 255));
			this.SetColor(1, new Color(0, 0, 0));
			this.mSpaceAfterHeader = GlobalMembers.MS(45);
			this.mContentInsets = new Insets(GlobalMembers.MS(90), GlobalMembers.MS(22), GlobalMembers.MS(90), GlobalMembers.MS(45));
			this.mButtonHorzSpacing = GlobalMembers.MS(10);
			this.mButtonSidePadding = GlobalMembers.MS(25);
			this.mLineSpacingOffset = GlobalMembers.MS(-6);
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.SetText(theDialogHeader);
			this.AddWidget(this.mHeadingLabel);
			this.mMessageLabel = new Label(GlobalMembersResources.FONT_DIALOG);
			this.mMessageLabel.SetText(theDialogLines);
			this.mMessageLabel.SetTextBlockEnabled(true);
			this.AddWidget(this.mMessageLabel);
			if (this.mYesButton != null)
			{
				int mId = this.mYesButton.mId;
				string mLabel = this.mYesButton.mLabel;
				Bej3Button bej3Button = new Bej3Button(mId, this, buttonType1);
				bej3Button.SetLabel(mLabel);
				GlobalMembers.KILL_WIDGET(this.mYesButton);
				this.mYesButton = bej3Button;
				this.mButtons.Add(this.mYesButton);
				this.AddWidget(this.mYesButton);
			}
			if (this.mNoButton != null)
			{
				int mId2 = this.mNoButton.mId;
				string mLabel = this.mNoButton.mLabel;
				Bej3Button bej3Button2 = new Bej3Button(mId2, this, buttonType2);
				bej3Button2.SetLabel(mLabel);
				GlobalMembers.KILL_WIDGET(this.mNoButton);
				this.mNoButton = bej3Button2;
				this.mButtons.Add(this.mNoButton);
				this.AddWidget(this.mNoButton);
			}
			int theId2 = 1002;
			if (this.mYesButton != null)
			{
				theId2 = this.mYesButton.mId;
			}
			if (this.mNoButton != null)
			{
				theId2 = this.mNoButton.mId;
			}
			if (topButtonType != Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
			{
				this.mTopButton = new Bej3Button(theId2, this, topButtonType);
				int theX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(711));
				int theY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(711));
				int celWidth = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP.GetCelWidth();
				int celHeight = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP.GetCelHeight();
				this.mTopButton.Resize(theX, theY, celWidth, celHeight);
				this.AddWidget(this.mTopButton);
			}
			else
			{
				this.mTopButton = null;
			}
			this.SetTopButtonType(topButtonType);
			this.LinkUpAssets();
			bool flag = false;
			if (this.mCanSlideInMenus)
			{
				Bej3ButtonType previousButtonType = Bej3ButtonType.TOP_BUTTON_TYPE_NONE;
				if (this.mTopButton != null)
				{
					previousButtonType = this.mTopButton.GetType();
				}
				flag = Bej3Widget.SlideCurrent(true, this, previousButtonType);
			}
			Bej3Button previousTopButton = null;
			if (Bej3Widget.mCurrentSlidingMenu != null)
			{
				previousTopButton = Bej3Widget.mCurrentSlidingMenu.mTopButton;
			}
			this.AllowSlideIn(!flag, previousTopButton);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
			GlobalMembers.gApp.ClearUpdateBacklog(false);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			Bej3Widget.ClearSlide(this);
			base.Dispose();
		}

		public virtual void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back)
			{
				args.processed = true;
				this.ButtonDepress(1002);
			}
		}

		public override void SetButtonFont(Font theFont)
		{
			base.SetHeaderFont(theFont);
			this.mHeadingLabel.SetFont(theFont);
		}

		public override void SetHeaderFont(Font theFont)
		{
			base.SetHeaderFont(theFont);
			this.mHeadingLabel.SetFont(theFont);
		}

		public override void SetLinesFont(Font theFont)
		{
			base.SetLinesFont(theFont);
			this.mMessageLabel.SetFont(theFont);
		}

		public override void Draw(Graphics g)
		{
			new Rect(this.mBackgroundInsets.mLeft, this.mBackgroundInsets.mTop, this.mWidth - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight, this.mHeight - this.mBackgroundInsets.mTop - this.mBackgroundInsets.mBottom);
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.MENU_DIVIDER_DIALOG_Y, false);
		}

		public void PreDraw(Graphics g)
		{
			this.mWidgetManager.FlushDeferredOverlayWidgets(this.mFlushPriority);
			g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mAlphaCurve)));
			g.PushColorMult();
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			this.mWidgetManager.FlushDeferredOverlayWidgets(10);
			this.PreDraw(g);
			base.DrawAll(theFlags, g);
			this.PostDraw(g);
		}

		public void PostDraw(Graphics g)
		{
			g.PopColorMult();
		}

		public override void Update()
		{
			base.Update();
			this.mAlphaCurve.IncInVal();
			bool flag = Bej3Widget.FloatEquals((float)this.mY + this.mAnimationFraction, (float)this.mTargetPos, ConstantsWP.DASHBOARD_SLIDER_SPEED);
			if (!flag && this.mAllowSlide)
			{
				int num = ((this.mY < this.mTargetPos) ? 1 : (-1));
				this.mAnimationFraction += (float)this.mY + ((float)(this.mTargetPos - this.mY) - this.mAnimationFraction) * ConstantsWP.DASHBOARD_SLIDER_SPEED_SCALAR + ConstantsWP.DASHBOARD_SLIDER_SPEED * (float)num;
				this.mY = (int)this.mAnimationFraction;
				this.mAnimationFraction -= (float)this.mY;
			}
			if (this.mAllowSlide && Bej3Widget.FloatEquals((float)this.mY + this.mAnimationFraction, (float)this.mTargetPos, ConstantsWP.DASHBOARD_SLIDER_SPEED))
			{
				this.mY = this.mTargetPos;
				this.mAnimationFraction = 0f;
			}
			if (!this.mAlphaCurve.HasBeenTriggered())
			{
				flag = false;
			}
			if (!this.mFinishedTransition)
			{
				Bej3ButtonType previousButtonType = Bej3ButtonType.TOP_BUTTON_TYPE_NONE;
				if (this.mTopButton != null)
				{
					previousButtonType = this.mTopButton.GetType();
				}
				if (flag)
				{
					if (this.mIsKilling)
					{
						Bej3Widget.NotifyCurrentDialogFinished(this, previousButtonType);
						if (this.mCanSlideInMenus)
						{
							Bej3Widget.SlideCurrent(false, this, previousButtonType);
						}
						if (GlobalMembers.gApp.mBoard != null)
						{
							GlobalMembers.gApp.mBoard.DialogClosed(this.mId);
						}
						this.KilledFinished();
						GlobalMembers.gApp.KillDialog(this);
					}
					if (!this.mIsKilling)
					{
						this.SlideInFinished();
					}
					Bej3Widget.NotifyCurrentDialogFinishedSlidingIn(this, previousButtonType);
					this.mFinishedTransition = true;
				}
			}
		}

		public bool find(LinkedList<Widget> list, Widget emu)
		{
			LinkedList<Widget>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == emu)
				{
					return false;
				}
			}
			return true;
		}

		public void SetChildrenMouseVisible(bool isVisible)
		{
			for (;;)
			{
				Widget widget = this;
				bool flag;
				do
				{
					LinkedList<Widget>.Enumerator enumerator = widget.mWidgets.GetEnumerator();
					flag = false;
					while (enumerator.MoveNext())
					{
						Widget widget2 = enumerator.Current;
						if (widget2.mMouseVisible != isVisible && this.find(this.mMouseInvisibleChildren, widget2))
						{
							widget = widget2;
							flag = true;
							break;
						}
					}
				}
				while (flag);
				if (widget == this)
				{
					break;
				}
				widget.mMouseVisible = isVisible;
			}
		}

		public override int GetPreferredHeight(int theWidth)
		{
			theWidth = Math.Max(ConstantsWP.DIALOGBOX_MIN_WIDTH, theWidth);
			this.LinkUpAssets();
			int num = this.mMessageLabel.GetVisibleHeight(theWidth - ConstantsWP.DIALOGBOX_MESSAGE_PADDING_X * 2);
			num += this.mMessageLabel.mY;
			num += ConstantsWP.DIALOGBOX_EXTRA_HEIGHT;
			int num2 = this.mButtons.size<DialogButton>();
			num += num2 * ConstantsWP.DIALOGBOX_BUTTON_MEASURE_HEIGHT;
			return Math.Max(num, ConstantsWP.DIALOGBOX_MIN_HEIGHT);
		}

		public virtual void Kill()
		{
			if (this.mIsKilling)
			{
				return;
			}
			this.mIsKilling = true;
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_HIDE_CURVE, this.mAlphaCurve);
			this.mFinishedTransition = false;
			bool mDoFadeBackForDialogs = false;
			if (Bej3Widget.mCurrentSlidingMenu != null && Bej3Widget.mCurrentSlidingMenu.mShouldFadeBehind)
			{
				mDoFadeBackForDialogs = true;
			}
			GlobalMembers.gApp.mDoFadeBackForDialogs = mDoFadeBackForDialogs;
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
			this.mDragging = false;
		}

		public override void ButtonPress(int theId)
		{
			if (!this.mIsKilling)
			{
				base.ButtonPress(theId);
			}
		}

		public override void ButtonDepress(int theId)
		{
			if (this.mResult != 2147483647)
			{
				return;
			}
			if (!this.mIsKilling)
			{
				base.ButtonDepress(theId);
			}
			if (theId == 1002)
			{
				this.mResult = theId;
				this.mDialogListener.DialogButtonDepress(this.mId, theId);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			theWidth = Math.Max(ConstantsWP.DIALOGBOX_MIN_WIDTH, theWidth);
			theHeight = Math.Max(ConstantsWP.DIALOGBOX_MIN_HEIGHT, theHeight);
			theHeight = Math.Min(ConstantsWP.DIALOGBOX_MAX_HEIGHT, theHeight);
			theWidth = GlobalMembers.gApp.mWidth;
			theX = GlobalMembers.gApp.mWidth / 2 - theWidth / 2;
			this.mTargetPos = GlobalMembers.gApp.mHeight - theHeight;
			theY = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.mFinishedTransition = false;
			base.superSubResize(theX, theY, theWidth, theHeight);
			this.mHeadingLabel.SetMaximumWidth(0);
			this.mHeadingLabel.Resize(this.mWidth / 2, ConstantsWP.DIALOGBOX_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetMaximumWidth(this.mWidth - ConstantsWP.DIALOGBOX_HEADING_LABEL_MAX_WIDTH_OFFSET, ConstantsWP.DIALOGBOX_HEADING_LABEL_SPLIT_Y);
			this.LinkUpAssets();
		}

		public virtual void LinkUpAssets()
		{
			this.mComponentImage = null;
			switch (this.mButtons.size<DialogButton>())
			{
			case 1:
				this.mButtons[0].Resize(this.mWidth / 2 - ConstantsWP.DIALOGBOX_BUTTON_WIDTH_1_BUTTON / 2, this.mHeight - this.mButtons[0].mHeight - ConstantsWP.DIALOGBOX_BUTTON_1_Y_1_BUTTON, ConstantsWP.DIALOGBOX_BUTTON_WIDTH_1_BUTTON, 0);
				break;
			case 2:
				this.mButtons[1].Resize(this.mWidth / 2 - ConstantsWP.DIALOGBOX_BUTTON_WIDTH_2_BUTTONS / 2, this.mHeight - this.mButtons[0].mHeight - ConstantsWP.DIALOGBOX_BUTTON_1_Y_2_BUTTONS, ConstantsWP.DIALOGBOX_BUTTON_WIDTH_2_BUTTONS, 0);
				this.mButtons[0].Resize(this.mWidth / 2 - ConstantsWP.DIALOGBOX_BUTTON_WIDTH_2_BUTTONS / 2, this.mHeight - this.mButtons[1].mHeight - ConstantsWP.DIALOGBOX_BUTTON_2_Y_2_BUTTONS, ConstantsWP.DIALOGBOX_BUTTON_WIDTH_2_BUTTONS, 0);
				break;
			case 3:
				this.mButtons[2].Resize(this.mWidth / 2 - ConstantsWP.DIALOGBOX_BUTTON_WIDTH_3_BUTTONS / 2, this.mHeight - this.mButtons[0].mHeight - ConstantsWP.DIALOGBOX_BUTTON_1_Y_3_BUTTONS, ConstantsWP.DIALOGBOX_BUTTON_WIDTH_3_BUTTONS, 0);
				this.mButtons[1].Resize(this.mWidth / 2 - ConstantsWP.DIALOGBOX_BUTTON_WIDTH_3_BUTTONS / 2, this.mHeight - this.mButtons[1].mHeight - ConstantsWP.DIALOGBOX_BUTTON_2_Y_3_BUTTONS, ConstantsWP.DIALOGBOX_BUTTON_WIDTH_3_BUTTONS, 0);
				this.mButtons[0].Resize(this.mWidth / 2 - this.mButtons[2].mWidth / 2, this.mHeight - this.mButtons[2].mHeight - ConstantsWP.DIALOGBOX_BUTTON_3_Y_3_BUTTONS, ConstantsWP.DIALOGBOX_BUTTON_WIDTH_3_BUTTONS, 0);
				break;
			}
			if (this.mMessageLabel != null)
			{
				this.mMessageLabel.SetTextBlock(new Rect(ConstantsWP.DIALOGBOX_MESSAGE_PADDING_X, ConstantsWP.DIALOGBOX_MESSAGE_PADDING_TOP, this.mWidth - ConstantsWP.DIALOGBOX_MESSAGE_PADDING_X * 2, this.mHeight - ConstantsWP.DIALOGBOX_MESSAGE_PADDING_TOP), false);
			}
			for (int i = 0; i < this.mButtons.size<DialogButton>(); i++)
			{
				Bej3Button bej3Button = (Bej3Button)this.mButtons[i];
				bej3Button.mClippingEnabled = false;
			}
		}

		public void AddButton(DialogButton theButton)
		{
			this.mButtons.Add(theButton);
			this.AddWidget(theButton);
			this.LinkUpAssets();
		}

		public void SetButtonPosition(DialogButton theButton, int thePosition)
		{
			for (int i = 0; i < this.mButtons.size<DialogButton>(); i++)
			{
				this.RemoveWidget(this.mButtons[i]);
			}
			for (int j = 0; j < this.mButtons.size<DialogButton>(); j++)
			{
				Bej3Button bej3Button = (Bej3Button)this.mButtons[j];
				if (theButton == bej3Button)
				{
					this.mButtons.RemoveAt(j);
					this.mButtons.Insert(thePosition, bej3Button);
					break;
				}
			}
			for (int k = 0; k < this.mButtons.size<DialogButton>(); k++)
			{
				this.AddWidget(this.mButtons[k]);
			}
			this.LinkUpAssets();
		}

		public void SizeToContent()
		{
			int num = this.GetPreferredHeight(this.mWidth);
			if (num > GlobalMembers.gApp.mHeight)
			{
				num = GlobalMembers.gApp.mHeight;
				this.mY = 0;
			}
			else
			{
				this.mY += (this.mHeight - num) / 2;
			}
			this.Resize(this.mX, this.mY, this.mWidth, num);
		}

		public override void GotFocus()
		{
			base.GotFocus();
		}

		public virtual void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			bool flag = this.mAllowSlide;
			this.mAllowSlide = allow;
			if (!flag)
			{
			}
			this.SetVisible(allow);
			if (this.mAllowSlide)
			{
				if (!flag)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_SHOW_CURVE, this.mAlphaCurve);
				}
			}
			else
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_HIDE_CURVE, this.mAlphaCurve);
			}
			bool mDoFadeBackForDialogs = allow;
			if (Bej3Widget.mCurrentSlidingMenu != null && Bej3Widget.mCurrentSlidingMenu.mShouldFadeBehind)
			{
				mDoFadeBackForDialogs = true;
			}
			GlobalMembers.gApp.mDoFadeBackForDialogs = mDoFadeBackForDialogs;
		}

		public void SetTopButtonType(Bej3ButtonType type)
		{
			if (this.mTopButton != null)
			{
				this.mTopButton.SetType(type);
				this.mTopButton.SetDisabled(false);
			}
		}

		public int GetTargetPosition()
		{
			return this.mTargetPos;
		}

		public Label GetMessageLabel()
		{
			return this.mMessageLabel;
		}

		public virtual bool ButtonsEnabled()
		{
			return Bej3Widget.FloatEquals((float)this.mY, (float)this.mTargetPos);
		}

		public void ForceSetToTarget()
		{
			this.mY = this.mTargetPos;
		}

		public void SetMessageFont(Font theFont)
		{
			this.mMessageLabel.SetFont(theFont);
			this.SizeToContent();
		}

		protected List<DialogButton> mButtons;

		protected Label mHeadingLabel;

		protected Label mMessageLabel;

		protected int mTargetPos;

		protected float mAnimationFraction;

		protected bool mFinishedTransition;

		protected bool mAllowSlide;

		protected CurvedVal mAlphaCurve = new CurvedVal();

		public bool mCanSlideInMenus;

		public Bej3Button mTopButton;

		public int mFlushPriority;

		public bool mIsKilling;

		public bool mCanEscape;

		public bool mAllowDrag;

		public int mScaleCenterX;

		public int mScaleCenterY;

		public LinkedList<Widget> mMouseInvisibleChildren;

		public bool mOpenURLWhenDone;

		public new enum ButtonID
		{
			ID_CANCEL = 1002
		}
	}
}
