using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class Alert : Dialogoid
	{
		public Alert(Dialogs id, DialogListener listener, GameApp app)
			: base((int)id, listener, app)
		{
			this.mTimeline = new Timeline<Alert>(this);
			this.mImage = AtlasResources.IMAGE_SUB_BORDER;
			this.mHasTransparencies = true;
			this.mHasAlpha = true;
			this.mPriority = 1;
			this.mFancyText = new FancyTextWidget();
			this.AddWidget(this.mFancyText);
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public virtual void SetHeadingText(string text)
		{
			this.mHeadingText = text;
		}

		public void SetBodyText(string text)
		{
			this.mBodyText.Clear();
			this.mBodyText.Add(text);
		}

		public void ClearBodyText()
		{
			this.mBodyText.Clear();
		}

		public void AddBodyText(string text)
		{
			this.mBodyText.Add(text);
		}

		public override void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			base.InterfaceOrientationChanged(toOrientation);
			this.Layout();
		}

		public override bool BackButtonPress()
		{
			base.ButtonDepress(this.DefaultButton);
			return true;
		}

		public void AddButton(int id, int color, string label)
		{
			this.AddButton(id, color, label, 1f);
		}

		public void AddButton(Dialogoid.ButtonID id, SmallButtonColors color, string label)
		{
			this.AddButton((int)id, (int)color, label);
		}

		public void AddButton(int id, int color, string label, float layoutWidthFactor)
		{
			PodButton podButton = base.CreateButton(id, color, label);
			podButton.SetLayoutWidthFactor(layoutWidthFactor);
			this.mButtons.Add(podButton);
			this.AddWidget(podButton);
			if (this.DefaultButton == -1)
			{
				this.DefaultButton = id;
			}
		}

		public override void Present()
		{
			if (this.mParent == null && !this.mTimeline.IsRunning())
			{
				this.Layout();
				this.SetDisabled(true);
				base.Present();
				this.SetVisible(true);
				this.mResult = -1;
				float seconds = 0.175f;
				int tick = SexyAppFrameworkConstants.ticksForSeconds(0.25f);
				int tick2 = SexyAppFrameworkConstants.ticksForSeconds(seconds);
				this.mTimeline.Clear();
				int channelId = this.mTimeline.AddChannel<float>(delegate(Alert a, float dimmerOpacity)
				{
					a.mDimmerOpacity = (int)dimmerOpacity;
				}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
				this.mTimeline.SetKey<float>(channelId, 0, 0f, false, true);
				this.mTimeline.SetKey<float>(channelId, tick, 192f, false, true);
				channelId = this.mTimeline.AddChannel<float>(delegate(Alert a, float widgetOpacity)
				{
					a.mWidgetOpacity = (int)widgetOpacity;
				}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
				this.mTimeline.SetKey<float>(channelId, 0, 0f, false, true);
				this.mTimeline.SetKey<float>(channelId, tick2, 255f, false, true);
				channelId = this.mTimeline.AddChannel<float>(delegate(Alert a, float widgetScale)
				{
					a.mWidgetScale = widgetScale;
				}, new KeyInterpolatorGeneric<float>.InterpolatorMethod(KeyInterpolatorFloat.InterpolationMethodFloat));
				this.mTimeline.SetKey<float>(channelId, 0, 0.9f, true, true);
				this.mTimeline.SetKey<float>(channelId, tick2, 1.04f, true, true);
				this.mTimeline.SetKey<float>(channelId, tick, 1f, true, true);
				this.mTimeline.SetEvent(tick, new Timeline<Alert>.EventFunc(this.PresentComplete));
				this.mTimeline.Start();
				foreach (PodButton podButton in this.mButtons)
				{
					podButton.FadeIn(0.125f, 0.25f);
				}
				this.mFancyText.FadeIn(0.125f, 0.25f);
			}
		}

		public override void Dismiss()
		{
			if (this.mParent != null && !this.mTimeline.IsRunning())
			{
				int tick = SexyAppFrameworkConstants.ticksForSeconds(0.25f);
				this.mTimeline.Clear();
				int channelId = this.mTimeline.AddChannel<int>(delegate(Alert a, int dimmerOpacity)
				{
					a.mDimmerOpacity = dimmerOpacity;
				}, new KeyInterpolatorGeneric<int>.InterpolatorMethod(KeyInterpolatorInt.InterpolationMethodInt));
				this.mTimeline.SetKey<int>(channelId, 0, 192, false, true);
				this.mTimeline.SetKey<int>(channelId, tick, 0, false, true);
				channelId = this.mTimeline.AddChannel<int>(delegate(Alert a, int widgetOpacity)
				{
					a.mWidgetOpacity = widgetOpacity;
				}, new KeyInterpolatorGeneric<int>.InterpolatorMethod(KeyInterpolatorInt.InterpolationMethodInt));
				this.mTimeline.SetKey<int>(channelId, 0, 255, false, true);
				this.mTimeline.SetKey<int>(channelId, tick, 0, false, true);
				this.mWidgetScale = 1f;
				this.mTimeline.SetEvent(tick, new Timeline<Alert>.EventFunc(this.DismissComplete));
				this.mTimeline.Start();
				foreach (PodButton podButton in this.mButtons)
				{
					podButton.FadeOut(0f, 0.25f);
				}
				this.mFancyText.FadeOut(0f, 0.25f);
			}
		}

		public override void Update()
		{
			if (this.mVisible && this.mTimeline.IsRunning())
			{
				this.mTimeline.UpdateF(1f);
				this.mTimeline.Update();
			}
			if (!this.didGarbageCollection && this.mVisible && !this.mTimeline.IsRunning())
			{
				if (this.didEndAnimation)
				{
					GC.Collect();
					this.didGarbageCollection = true;
					return;
				}
				this.didEndAnimation = true;
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			TRect theRect = new TRect(0, 0, this.mWidth, this.mHeight);
			g.SetColor(new Color(0, 0, 0, this.mDimmerOpacity));
			g.FillRect(theRect);
			TRect trect = this.mBoxFrame;
			int theX = (int)(((float)trect.mWidth * this.mWidgetScale - (float)trect.mWidth) / 2f);
			int theY = (int)(((float)trect.mHeight * this.mWidgetScale - (float)trect.mHeight) / 2f);
			trect.Inflate(theX, theY);
			TRect theRect2 = trect;
			theRect2.mX += Constants.mConstants.AlertInsetDistance_X;
			theRect2.mWidth -= Constants.mConstants.AlertInsetDistance_X * 2;
			theRect2.mY += Constants.mConstants.AlertInsetDistance_Y;
			theRect2.mHeight -= Constants.mConstants.AlertInsetDistance_Y * 2;
			g.SetColor(new SexyColor(0, 0, 0, this.mWidgetOpacity));
			g.SetColorizeImages(true);
			g.FillRect(theRect2);
			g.SetColor(new Color(255, 255, 255, this.mWidgetOpacity));
			g.SetColorizeImages(true);
			g.DrawImageBox(trect, this.mImage);
		}

		protected virtual void Layout()
		{
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			Insets alert_INSETS = this.ALERT_INSETS;
			int alert_WIDGET_WIDTH = this.ALERT_WIDGET_WIDTH;
			int num = alert_WIDGET_WIDTH - alert_INSETS.mRight;
			int num2 = num - alert_INSETS.mLeft;
			this.mFancyText.Resize(alert_INSETS.mLeft, alert_INSETS.mTop, num2, 0);
			this.mFancyText.Clear();
			this.mFancyText.SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
			this.mFancyText.SetComposeFont(Resources.FONT_TEXT);
			if (!string.IsNullOrEmpty(this.mHeadingText))
			{
				this.mFancyText.SetComposeColor(Color.White);
				this.mFancyText.AddWrappedText(this.mHeadingText);
				this.mFancyText.NewLine(Constants.mConstants.Alert_HeadingToText_Distance);
				this.mFancyText.SetComposeFont(Resources.FONT_TINY_TEXT);
			}
			this.mFancyText.SetComposeColor(Color.White);
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
			int num4 = alert_INSETS.mLeft;
			num3 = num3;
			foreach (PodButton podButton in this.mButtons)
			{
				int mHeight = podButton.mHeight;
				int layoutWidth = podButton.GetLayoutWidth(num2);
				podButton.Resize(num4, num3, layoutWidth, mHeight);
				num4 += layoutWidth;
				if (num4 >= num)
				{
					num4 = alert_INSETS.mLeft;
					num3 += mHeight;
				}
			}
			num3 += alert_INSETS.mBottom;
			this.mBoxFrame.mX = (this.mApp.mWidth - alert_WIDGET_WIDTH) / 2;
			this.mBoxFrame.mY = (this.mApp.mHeight - num3) / 2;
			this.mBoxFrame.mWidth = alert_WIDGET_WIDTH;
			this.mBoxFrame.mHeight = num3;
			foreach (Widget widget in this.mWidgets)
			{
				widget.Move(widget.mX + this.mBoxFrame.mX, widget.mY + this.mBoxFrame.mY);
			}
		}

		protected void PresentComplete()
		{
			this.SetDisabled(false);
		}

		protected void DismissComplete()
		{
			this.SetVisible(false);
			base.Dismiss();
			this.mListener.DialogButtonDepress(this.mId, this.mResult);
		}

		public const int ALERT_BUTTON_GAP = 0;

		public const int ALERT_DIMMER_OPACITY = 192;

		public const float ALERT_PRESENTATION_SECONDS = 0.25f;

		protected Image mImage;

		protected string mHeadingText = string.Empty;

		protected List<string> mBodyText = new List<string>();

		protected FancyTextWidget mFancyText;

		protected List<PodButton> mButtons = new List<PodButton>();

		protected TRect mBoxFrame = default(TRect);

		protected Timeline<Alert> mTimeline;

		protected Color mDimmerColor = default(Color);

		protected int mDimmerOpacity;

		protected int mWidgetOpacity;

		protected float mWidgetScale;

		protected bool mPresent;

		public readonly Insets ALERT_INSETS = Constants.mConstants.Alert_ALERT_INSETS;

		public readonly int ALERT_WIDGET_WIDTH = Constants.mConstants.Alert_ALERT_WIDGET_WIDTH;

		public int DefaultButton = -1;

		private bool didGarbageCollection;

		private bool didEndAnimation;
	}
}
