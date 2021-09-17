using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public class ActionSheet : Dialogoid
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 2; i++)
			{
				new ActionSheet(false, 0, null, null).PrepareForReuse();
			}
		}

		public static ActionSheet GetNewActionSheet(bool useHeader, int id, DialogListener listener, GameApp app)
		{
			if (ActionSheet.unusedObjects.Count > 0)
			{
				ActionSheet actionSheet = ActionSheet.unusedObjects.Pop();
				actionSheet.Reset(useHeader, id, listener, app);
				return actionSheet;
			}
			return new ActionSheet(useHeader, id, listener, app);
		}

		public static ActionSheet GetNewActionSheet(bool useHeader, int id, DialogListener listener, GameApp app, string heading)
		{
			if (ActionSheet.unusedObjects.Count > 0)
			{
				ActionSheet actionSheet = ActionSheet.unusedObjects.Pop();
				actionSheet.Reset(useHeader, id, listener, app, heading);
				return actionSheet;
			}
			return new ActionSheet(useHeader, id, listener, app, heading);
		}

		protected virtual void PrepareForReuse()
		{
			ActionSheet.unusedObjects.Push(this);
		}

		protected virtual void Reset(bool useHeader, int id, DialogListener listener, GameApp app)
		{
			this.Reset(useHeader, id, listener, app, "");
		}

		protected virtual void Reset(bool useHeader, int id, DialogListener listener, GameApp app, string heading)
		{
			this.mCoverRect = default(TRect);
			this.mImage = null;
			this.mDangerButton = null;
			this.mCancelButton = null;
			this.mOtherButtons.Clear();
			this.mFancyText = null;
			this.mBasicText = string.Empty;
			this.mReservedSpace = default(TRect);
			this.mSlideInPresent = false;
			this.mSlideInDuration = 0;
			this.mSlideInTick = 0;
			this.mSlideInPosition.Clear();
			this.mDimmerAlpha.Clear();
			this.mDimmerColor = 0;
			this.disabledButtons.Clear();
			this.cancelButtonId = 0;
			this.didGarbageCollection = false;
			base.Reset(id, listener, app);
			this.useHeader = useHeader;
			this.SetText(heading);
			this.mImage = AtlasResources.IMAGE_ACTION_SHEET_BG;
			this.mDimmerColor = 0;
			this.mSlideInTick = 0;
			this.mReservedSpace = new TRect(0, 0, 0, 0);
			this.mHasTransparencies = true;
			this.mHasAlpha = true;
			this.mPriority = 1;
		}

		private ActionSheet(bool useHeader, int id, DialogListener listener, GameApp app)
			: this(useHeader, id, listener, app, "")
		{
		}

		private ActionSheet(bool useHeader, int id, DialogListener listener, GameApp app, string heading)
			: base(id, listener, app)
		{
			this.Reset(useHeader, id, listener, app, heading);
		}

		public override void Dispose()
		{
			base.Dispose();
			this.PrepareForReuse();
		}

		public void AddButton(int id, string label)
		{
			this.AddButton(id, label, SmallButtonColors.SMALL_BUTTON_BLUE);
		}

		public void AddButton(int id, string label, SmallButtonColors colour)
		{
			this.AddButton(id, label, 0, 0);
		}

		public void AddButton(int id, string label, int numButtons, int buttonNum)
		{
			this.AddButton(id, label, numButtons, buttonNum, true, false);
		}

		public void AddButton(int id, string label, SmallButtonColors colour, bool enableButton, bool fadeWhenDisabled)
		{
			this.AddButton(id, label, 0, 0, enableButton, fadeWhenDisabled);
		}

		public void AddButton(int id, string label, int aNumButtons, int aButtonNum, bool enableButton, bool fadeWhenDisabled)
		{
			FancySmallButton newFancySmallButton = FancySmallButton.GetNewFancySmallButton(id, aNumButtons, aButtonNum, label, this);
			newFancySmallButton.SetDisabled(!enableButton);
			newFancySmallButton.FadeWhenDisabled = fadeWhenDisabled;
			this.mOtherButtons.Add(newFancySmallButton);
			this.AddWidget(newFancySmallButton);
			if (!enableButton)
			{
				this.disabledButtons.Add(newFancySmallButton);
			}
		}

		public void SetDangerButton(int id, string label)
		{
			if (this.mDangerButton == null)
			{
				this.mDangerButton = FancySmallButton.GetNewFancySmallButton(id, 1, 1, label, this);
				this.AddWidget(this.mDangerButton);
				return;
			}
			this.mDangerButton.mLabel = label;
		}

		public void SetCancelButton(int id, string label)
		{
			if (this.mCancelButton == null)
			{
				this.cancelButtonId = id;
				this.mCancelButton = FancySmallButton.GetNewFancySmallButton(id, 0, 0, label, this);
				this.AddWidget(this.mCancelButton);
				return;
			}
			this.mCancelButton.mLabel = label;
		}

		public void SetText(string text)
		{
			if (this.mFancyText == null)
			{
				this.mFancyText = new FancyTextWidget();
				this.AddWidget(this.mFancyText);
			}
			this.mBasicText = text;
		}

		public override void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			this.Layout();
		}

		public override void Present()
		{
			this.mSlideInPresent = true;
			if (this.mSlideInTick == 0)
			{
				this.Layout();
				this.mSlideInTick = 0;
				this.mSlideInDuration = SexyAppFrameworkConstants.ticksForSeconds(0.25f);
				TPoint tpoint = this.SetupSliding();
				this.Move(tpoint.mX, tpoint.mY);
				this.mDimmerAlpha.SetKey(0, 0, false, true);
				this.mDimmerAlpha.SetKey(this.mSlideInDuration, 192, false, true);
				this.SetDisabled(true);
				base.Present();
				this.mResult = -1;
			}
		}

		private TPoint SetupSliding()
		{
			TPoint tpoint = new TPoint(0, 0);
			TPoint value = new TPoint(this.mX, this.mY);
			this.mSlideInPosition.SetKey(0, tpoint, false, true);
			this.mSlideInPosition.SetKey(this.mSlideInDuration, value, true, true);
			return tpoint;
		}

		public override void Dismiss()
		{
			this.mSlideInPresent = false;
		}

		public override void Update()
		{
			if (this.mParent != null)
			{
				if (this.mSlideInPresent && this.mSlideInTick < this.mSlideInDuration)
				{
					this.mSlideInTick++;
					TPoint tpoint = this.mSlideInPosition.Tick((float)this.mSlideInTick);
					this.Move(tpoint.mX, tpoint.mY);
					this.mDimmerColor = this.mDimmerAlpha.Tick((float)this.mSlideInTick);
					if (this.mSlideInTick == this.mSlideInDuration)
					{
						this.SetDisabled(false);
						return;
					}
				}
				else if (!this.mSlideInPresent && this.mSlideInTick > 0)
				{
					this.mSlideInTick--;
					TPoint tpoint2 = this.mSlideInPosition.Tick((float)this.mSlideInTick);
					this.Move(tpoint2.mX, tpoint2.mY);
					this.mDimmerColor = this.mDimmerAlpha.Tick((float)this.mSlideInTick);
					if (this.mSlideInTick == 0)
					{
						this.mListener.DialogButtonDepress(this.mId, this.mResult);
						base.Dismiss();
						return;
					}
				}
				else if (this.didEndAnimation)
				{
					if (!this.didGarbageCollection)
					{
						GC.Collect();
						this.didGarbageCollection = true;
						return;
					}
				}
				else
				{
					this.didEndAnimation = true;
				}
			}
		}

		public override bool BackButtonPress()
		{
			base.ButtonDepress(this.cancelButtonId);
			return true;
		}

		public override void SetDisabled(bool disabled)
		{
			base.SetDisabled(disabled);
			foreach (PodButton podButton in this.disabledButtons)
			{
				podButton.SetDisabled(true);
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(new SexyColor(0, 0, 0, this.mDimmerColor));
			g.SetColorizeImages(true);
			g.FillRect(this.mCoverRect.mX - this.mX, this.mCoverRect.mY - this.mY, this.mCoverRect.mWidth, this.mCoverRect.mHeight);
			g.SetColorizeImages(false);
			TRect theDest = new TRect(0, this.mCoverRect.mHeight, this.mWidth, this.mHeight - this.mCoverRect.mHeight);
			g.DrawImageBox(theDest, this.mImage);
			if (this.useHeader && !string.IsNullOrEmpty(this.mBasicText))
			{
				g.SetFont(Resources.FONT_HEADING);
				this.DrawFrameHeadingAndText(g, this.mBasicText, this.mWidth / 2, this.mCoverRect.mHeight - Constants.mConstants.ActionSheet_Header_Offset_Y, null);
			}
		}

		protected virtual void Layout()
		{
			Insets actionSheet_ACTION_SHEET_FRAME_INSETS = Constants.mConstants.ActionSheet_ACTION_SHEET_FRAME_INSETS;
			this.mCoverRect = new TRect(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			int mWidth = this.mCoverRect.mWidth;
			int num = this.mCoverRect.mHeight + actionSheet_ACTION_SHEET_FRAME_INSETS.mTop;
			int num2 = mWidth - actionSheet_ACTION_SHEET_FRAME_INSETS.mLeft - actionSheet_ACTION_SHEET_FRAME_INSETS.mRight;
			if (!this.useHeader && this.mFancyText != null)
			{
				this.mFancyText.Resize(actionSheet_ACTION_SHEET_FRAME_INSETS.mLeft, num, num2, 0);
				this.mFancyText.Clear();
				this.mFancyText.SetComposeColor(SexyColor.White);
				this.mFancyText.SetComposeFont(Resources.FONT_TEXT);
				this.mFancyText.SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
				this.mFancyText.AddWrappedText(this.mBasicText);
				this.mFancyText.NewLine(0);
				this.mFancyText.ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
				num += this.mFancyText.mHeight + Constants.mConstants.ActionSheet_ACTION_SHEET_BUTTON_GAP;
			}
			this.mFancyText.SetVisible(!this.useHeader);
			int mHeight = this.mReservedSpace.mHeight;
			this.mReservedSpace = new TRect(actionSheet_ACTION_SHEET_FRAME_INSETS.mLeft, num, num2, mHeight);
			num += mHeight + Constants.mConstants.ActionSheet_ACTION_SHEET_BUTTON_GAP;
			if (this.mOtherButtons.Count > 3 && this.mApp.IsLandscape())
			{
				int buttonNumber = 0;
				int num3 = this.mOtherButtons.Count;
				if (this.mDangerButton != null)
				{
					num3++;
				}
				num3 /= 2;
				Insets insets = actionSheet_ACTION_SHEET_FRAME_INSETS;
				insets.mLeft /= 2;
				num2 /= 2;
				int num4 = num;
				int num5 = num;
				int num6 = 0;
				if (this.mDangerButton != null)
				{
					mHeight = this.mDangerButton.mHeight;
					this.mDangerButton.Resize(insets.mLeft, num, num2, mHeight);
					this.mDangerButton.ButtonNumber = buttonNumber;
					this.mDangerButton.ButtonCount = num3;
					num += mHeight + Constants.mConstants.ActionSheet_ACTION_SHEET_BUTTON_GAP;
				}
				for (int i = 0; i < this.mOtherButtons.Count; i++)
				{
					FancyPodButton fancyPodButton = this.mOtherButtons[i];
					mHeight = fancyPodButton.mHeight;
					if (i == this.mOtherButtons.Count - 1 && i % 2 == 0)
					{
						num6 = mWidth / 2 - num2 / 2 - insets.mLeft;
					}
					fancyPodButton.Resize(insets.mLeft + num6, num, num2, mHeight);
					num += mHeight + Constants.mConstants.ActionSheet_ACTION_SHEET_BUTTON_GAP;
					fancyPodButton.ButtonNumber = buttonNumber++;
					fancyPodButton.ButtonCount = num3;
					if (i == this.mOtherButtons.Count / 2 - 1)
					{
						num5 = num;
						buttonNumber = 0;
						num = num4;
						num6 = mWidth / 2;
					}
				}
				if (num < num5)
				{
					num = num5;
				}
			}
			else
			{
				int num7 = 0;
				int num8 = this.mOtherButtons.Count;
				if (this.mDangerButton != null)
				{
					num8++;
				}
				if (this.mApp.IsLandscape())
				{
					num2 = (int)((float)num2 * Constants.BUTTON_WIDTH_FACTOR_LANDSCAPE);
					actionSheet_ACTION_SHEET_FRAME_INSETS.mLeft = mWidth / 2 - num2 / 2;
				}
				if (this.mDangerButton != null)
				{
					mHeight = this.mDangerButton.mHeight;
					this.mDangerButton.Resize(actionSheet_ACTION_SHEET_FRAME_INSETS.mLeft, num, num2, mHeight);
					this.mDangerButton.ButtonNumber = num7++;
					this.mDangerButton.ButtonCount = num8;
					num += mHeight + Constants.mConstants.ActionSheet_ACTION_SHEET_BUTTON_GAP;
				}
				foreach (FancyPodButton fancyPodButton2 in this.mOtherButtons)
				{
					mHeight = fancyPodButton2.mHeight;
					fancyPodButton2.Resize(actionSheet_ACTION_SHEET_FRAME_INSETS.mLeft, num, num2, mHeight);
					fancyPodButton2.ButtonNumber = num7++;
					fancyPodButton2.ButtonCount = num8;
					num += mHeight + Constants.mConstants.ActionSheet_ACTION_SHEET_BUTTON_GAP;
				}
			}
			if (this.mCancelButton != null)
			{
				mHeight = this.mCancelButton.mHeight;
				this.mCancelButton.Resize(mWidth / 2 - Constants.mConstants.MainMenu_CancelButtonWidth / 2, num, Constants.mConstants.MainMenu_CancelButtonWidth, mHeight);
				num += mHeight + actionSheet_ACTION_SHEET_FRAME_INSETS.mBottom;
			}
			int theX = 0;
			int theY = this.mApp.mHeight - num;
			this.Resize(theX, theY, mWidth, num);
			this.SetupSliding();
		}

		public const int ACTION_SHEET_DIMMER_ALPHA = 192;

		public const float ACTION_SHEET_SLIDE_DURATION_SECONDS = 0.25f;

		protected TRect mCoverRect = default(TRect);

		protected Image mImage;

		protected FancyPodButton mDangerButton;

		protected FancyPodButton mCancelButton;

		protected List<FancyPodButton> mOtherButtons = new List<FancyPodButton>();

		protected FancyTextWidget mFancyText;

		protected string mBasicText = string.Empty;

		protected TRect mReservedSpace = default(TRect);

		protected bool mSlideInPresent;

		protected int mSlideInDuration;

		protected int mSlideInTick;

		protected KeyInterpolatorTPoint mSlideInPosition = KeyInterpolatorTPoint.GetNewKeyInterpolatorTPoint();

		protected KeyInterpolatorInt mDimmerAlpha = KeyInterpolatorInt.GetNewKeyInterpolatorInt();

		protected int mDimmerColor;

		private List<FancyPodButton> disabledButtons = new List<FancyPodButton>();

		private int cancelButtonId;

		private bool useHeader;

		private static Stack<ActionSheet> unusedObjects = new Stack<ActionSheet>();

		private bool didGarbageCollection;

		private bool didEndAnimation;
	}
}
