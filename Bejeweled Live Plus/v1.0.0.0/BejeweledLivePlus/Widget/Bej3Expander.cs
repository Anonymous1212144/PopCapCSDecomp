using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3Expander : Bej3WidgetBase, Bej3ButtonListener, ButtonListener, CheckboxListener
	{
		private void DisableChildren(bool disable)
		{
			if (!this.mEnabledCheckbox.IsChecked() || this.mTargetHeight == this.mMinHeight)
			{
				disable = true;
			}
			for (int i = 0; i < this.mContainedWidgets.Count; i++)
			{
				Widget widget = this.mContainedWidgets[i];
				widget.SetDisabled(disable);
				widget.mMouseVisible = !disable;
				Bej3WidgetBase bej3WidgetBase = (Bej3WidgetBase)widget;
				if (bej3WidgetBase != null)
				{
					bej3WidgetBase.mGrayedOut = disable;
				}
			}
		}

		public Bej3Expander(int theId, Bej3ExpanderListener theListener, CheckboxListener theCheckListener, string theHeading, bool hasDivider, string theInfoHeader, string theInfoMessage)
		{
			this.mListener = theListener;
			this.mCheckListener = theCheckListener;
			this.mTargetY = 0;
			this.mHasDivider = hasDivider;
			this.mId = theId;
			this.mChildrenAlpha = 1f;
			this.mTargetHeight = 1;
			this.mHeadingWidgets = new List<Widget>();
			this.mContainedWidgets = new List<Widget>();
			this.mInfoHeader = theInfoHeader;
			this.mInfoMessage = theInfoMessage;
			this.mDividerOffset = ConstantsWP.EXPANDER_DIVIDER_DRAW_OFFSET;
			this.mWidgetsAdded = false;
			this.mWidgetFlagsMod.mRemoveFlags |= 8;
			if (this.mHasDivider)
			{
				this.mExpandedHeight = ConstantsWP.EXPANDER_MIN_HEIGHT + ConstantsWP.EXPANDER_DIVIDER_OFFSET;
				this.mTargetHeight = (this.mMinHeight = ConstantsWP.EXPANDER_MIN_HEIGHT + ConstantsWP.EXPANDER_DIVIDER_OFFSET);
			}
			else
			{
				this.mExpandedHeight += ConstantsWP.EXPANDER_MIN_HEIGHT;
				this.mTargetHeight = (this.mMinHeight = ConstantsWP.EXPANDER_MIN_HEIGHT);
			}
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_DIALOG, theHeading, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE, Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_CENTRE);
			this.mHeadingWidgets.Add(this.mHeadingLabel);
			this.AddWidget(this.mHeadingLabel);
			this.mInfoButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_INFO);
			this.mInfoButton.mClippingEnabled = false;
			this.mHeadingWidgets.Add(this.mInfoButton);
			this.AddWidget(this.mInfoButton);
			this.mExpandButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_DROPDOWN_RIGHT);
			this.mHeadingWidgets.Add(this.mExpandButton);
			this.mExpandButton.mClippingEnabled = false;
			this.AddWidget(this.mExpandButton);
			this.mEnabledCheckbox = new Bej3Checkbox(2, this);
			this.mHeadingWidgets.Add(this.mEnabledCheckbox);
			this.AddWidget(this.mEnabledCheckbox);
		}

		public override void Update()
		{
			if (this.mHeight != this.mTargetHeight)
			{
				this.mCurrentHeight += ((float)this.mTargetHeight - this.mCurrentHeight) * ConstantsWP.EXPANDER_SPEED;
				this.mHeight = (int)this.mCurrentHeight;
				if (Math.Abs(this.mHeight - this.mTargetHeight) < 1)
				{
					this.mHeight = this.mTargetHeight;
				}
			}
			if (this.mY != this.mTargetY)
			{
				this.mCurrentY += ((float)this.mTargetY - this.mCurrentY) * ConstantsWP.EXPANDER_SPEED;
				this.mY = (int)this.mCurrentY;
				if (Math.Abs(this.mY - this.mTargetY) < 1)
				{
					this.mY = this.mTargetY;
				}
			}
			if (this.mChildrenAlpha != this.mTargetChildrenAlpha)
			{
				this.mChildrenAlpha += (this.mTargetChildrenAlpha - this.mChildrenAlpha) * ConstantsWP.EXPANDER_SPEED;
				if (Math.Abs(this.mChildrenAlpha - this.mTargetChildrenAlpha) < 0.01f)
				{
					this.mChildrenAlpha = this.mTargetChildrenAlpha;
				}
			}
			base.Update();
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			if (this.mWidgetManager != null && this.mPriority > this.mWidgetManager.mMinDeferredOverlayPriority)
			{
				this.mWidgetManager.FlushDeferredOverlayWidgets(this.mPriority);
			}
			if (this.mClip && (theFlags.GetFlags() & 8) != 0)
			{
				g.ClipRect(0, 0, this.mWidth, this.mHeight);
			}
			if ((theFlags.GetFlags() & 4) != 0)
			{
				g.PushState();
				this.Draw(g);
				g.PopState();
			}
			for (int i = 0; i < this.mHeadingWidgets.Count; i++)
			{
				Widget widget = this.mHeadingWidgets[i];
				if (widget.mVisible)
				{
					if (this.mWidgetManager != null && widget == this.mWidgetManager.mBaseModalWidget)
					{
						theFlags.mIsOver = true;
					}
					g.PushState();
					g.Translate(widget.mX, widget.mY);
					widget.DrawAll(theFlags, g);
					widget.mDirty = false;
					g.PopState();
				}
			}
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.mChildrenAlpha)));
			g.PushColorMult();
			g.SetScale(1f, this.mChildrenAlpha, 0f, (float)this.mMinHeight);
			if (this.mChildrenAlpha > 0f)
			{
				for (int j = 0; j < this.mContainedWidgets.Count; j++)
				{
					Widget widget2 = this.mContainedWidgets[j];
					if (widget2.mVisible)
					{
						if (this.mWidgetManager != null && widget2 == this.mWidgetManager.mBaseModalWidget)
						{
							theFlags.mIsOver = true;
						}
						g.PushState();
						g.Translate(widget2.mX, widget2.mY);
						widget2.DrawAll(theFlags, g);
						widget2.mDirty = false;
						g.PopState();
					}
				}
			}
			g.PopColorMult();
			g.SetScale(1f, 1f, 0f, 0f);
		}

		public override void Draw(Graphics g)
		{
			if (this.mHasDivider)
			{
				Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, this.mDividerOffset, true);
			}
		}

		public override void AddWidget(Widget theWidget)
		{
			base.AddWidget(theWidget);
			if (!this.mHeadingWidgets.Contains(theWidget))
			{
				this.mContainedWidgets.Add(theWidget);
			}
			this.mWidgetsAdded = true;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			if (theHeight < this.mMinHeight)
			{
				theHeight = this.mMinHeight;
			}
			this.mTargetY = theY;
			base.Resize(theX, theY, theWidth, theHeight);
			this.mCurrentY = (float)this.mY;
			this.mCurrentHeight = (float)this.mHeight;
			int num = 0;
			int num2 = ConstantsWP.EXPANDER_MIN_HEIGHT / 2;
			if (this.mHasDivider)
			{
				num2 += ConstantsWP.EXPANDER_DIVIDER_OFFSET;
			}
			this.mEnabledCheckbox.Resize(this.mWidth - ConstantsWP.EXPANDER_CHECKBOX_X - num, num2 + ConstantsWP.EXPANDER_CHECKBOX_Y, 0, 0);
			int mHeight = this.mExpandButton.mHeight;
			this.mExpandButton.Resize(ConstantsWP.EXPANDER_BUTTON_X, num2 - mHeight / 2 + ConstantsWP.EXPANDER_BUTTON_Y_OFFSET, 0, 0);
			num = this.mInfoButton.mWidth;
			mHeight = this.mInfoButton.mHeight;
			this.mInfoButton.Resize(this.mWidth - ConstantsWP.EXPANDER_INFO_BUTTON_X - num, num2 - mHeight / 2 + ConstantsWP.EXPANDER_BUTTON_Y_OFFSET, 0, 0);
			if (this.mHasDivider)
			{
				num2 = ConstantsWP.EXPANDER_DIVIDER_OFFSET;
			}
			else
			{
				num2 = 0;
			}
			this.mHeadingLabel.Resize(this.mWidth / 2, num2 + ConstantsWP.EXPANDER_LABEL_Y, 0, 0);
		}

		public virtual void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				if (this.mTargetHeight == this.mExpandedHeight)
				{
					this.Collapse(true);
					return;
				}
				this.Expand();
				return;
			case 1:
				GlobalMembers.gApp.DoDialog(49, true, this.mInfoHeader, this.mInfoMessage, GlobalMembers._ID("OK", 3219), 3, 3, 3);
				return;
			default:
				return;
			}
		}

		public virtual void CheckboxChecked(int theId, bool checked1)
		{
			this.mCheckListener.CheckboxChecked(this.mId, checked1);
			this.LinkUpAssets();
		}

		public override void LinkUpAssets()
		{
			this.mExpandButton.LinkUpAssets();
			this.mEnabledCheckbox.LinkUpAssets();
			this.mInfoButton.LinkUpAssets();
			this.DisableChildren(false);
			this.Resize(this.mX, this.mY, this.mWidth, this.mHeight);
		}

		public void Expand()
		{
			if (!this.mWidgetsAdded && this.mTargetHeight == this.mExpandedHeight)
			{
				return;
			}
			this.mTargetHeight = this.mExpandedHeight;
			this.mExpandButton.SetType(Bej3ButtonType.BUTTON_TYPE_DROPDOWN_DOWN);
			this.mTargetChildrenAlpha = 1f;
			this.DisableChildren(false);
			this.mListener.ExpanderChanged(this.mId, true);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ZEN_MENUEXPAND);
			this.mWidgetsAdded = false;
		}

		public void Collapse(bool notifyListener)
		{
			if (!this.mWidgetsAdded && this.mTargetHeight == this.mMinHeight)
			{
				return;
			}
			this.mTargetHeight = this.mMinHeight;
			this.mExpandButton.SetType(Bej3ButtonType.BUTTON_TYPE_DROPDOWN_RIGHT);
			this.mTargetChildrenAlpha = 0f;
			this.DisableChildren(true);
			if (notifyListener)
			{
				this.mListener.ExpanderChanged(this.mId, false);
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ZEN_MENUEXPAND);
			}
			this.mWidgetsAdded = false;
		}

		public void SetExpandedHeight(int theExpandedHeight)
		{
			this.mExpandedHeight = theExpandedHeight;
		}

		public int GetAbsTargetPosition()
		{
			return this.mTargetY + this.mTargetHeight;
		}

		public void MoveToY(int y)
		{
			this.mTargetY = y;
		}

		public void ForceSetToTarget()
		{
			this.mCurrentHeight = (float)(this.mHeight = this.mTargetHeight);
			this.mCurrentY = (float)(this.mY = this.mTargetY);
			this.mChildrenAlpha = this.mTargetChildrenAlpha;
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public bool ButtonsEnabled()
		{
			return false;
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		private CheckboxListener mCheckListener;

		private Checkbox mCheckbox;

		private Bej3Button mBej3Button;

		private int mExpandedHeight;

		private int mTargetHeight;

		private int mTargetY;

		private float mCurrentY;

		private float mCurrentHeight;

		private bool mHasDivider;

		private float mTargetChildrenAlpha;

		private Bej3ExpanderListener mListener;

		private Bej3Button mExpandButton;

		private Bej3Button mInfoButton;

		private Label mHeadingLabel;

		private List<Widget> mHeadingWidgets;

		private List<Widget> mContainedWidgets;

		private string mInfoHeader;

		private string mInfoMessage;

		private bool mWidgetsAdded;

		public int mDividerOffset;

		public int mId;

		public int mMinHeight;

		public Bej3Checkbox mEnabledCheckbox;

		public float mChildrenAlpha;

		private enum BEJ3EXPANDER_IDS
		{
			BTN_EXPAND_ID,
			BTN_INFO_ID,
			CHK_ENABLE_ID
		}
	}
}
