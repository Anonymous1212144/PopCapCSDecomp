using System;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class ZumaSlideBox : Widget, ScrollWidgetListener
	{
		public ZumaSlideBox(DialogEx theDialog, int id, string label)
		{
			this.mLabel = label;
			this.mDialog = theDialog;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_RED_LIGHT);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			Rect theRect = default(Rect);
			theRect.mX = 0;
			theRect.mY = 0;
			theRect.mWidth = imageByID.GetWidth() * 2;
			theRect.mHeight = imageByID.GetHeight();
			this.mLabelFrame = default(Rect);
			this.mLabelFrame.mWidth = imageByID2.GetWidth() - theRect.mWidth - Common._S(9);
			this.mLabelFrame.mHeight = imageByID2.GetHeight();
			this.mLabelFrame.mX = 0;
			this.mLabelFrame.mY = (int)((float)(this.mLabelFrame.mHeight - fontByID.GetHeight()) * 0.5f);
			this.mSlideBoxButton = new ZumaSlideBoxButton(this);
			this.mSlideBoxButton.Resize(theRect);
			this.mScrollBox = new ScrollWidget(this);
			this.mScrollBox.Resize(this.mLabelFrame.mWidth, (this.mLabelFrame.mHeight - theRect.mHeight) / 2, theRect.mWidth, theRect.mHeight);
			this.mScrollBox.AddWidget(this.mSlideBoxButton);
			this.mScrollBox.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mScrollBox.EnablePaging(true);
			this.AddWidget(this.mScrollBox);
			Insets insets = new Insets();
			insets.mLeft = 0;
			insets.mRight = this.mSlideBoxButton.mWidth / 2;
			insets.mTop = 0;
			insets.mBottom = 0;
			this.mScrollBox.SetScrollInsets(insets);
			this.mScrollBox.SetPageHorizontal(0, false);
			this.mScrollBox.EnableBounce(false);
		}

		~ZumaSlideBox()
		{
		}

		public override void Draw(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			g.DrawImage(imageByID, 0, 0);
			g.SetFont(fontByID);
			g.SetColor(255, 255, 45, 255);
			g.WriteWordWrapped(this.mLabelFrame, this.mLabel, -1, 0);
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public void ScrollTargetReached(ScrollWidget scrollWidget)
		{
			this.mIsOff = scrollWidget.GetPageHorizontal() == 1;
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
		}

		public void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public void SetOnOff(bool isOn)
		{
			this.mIsOff = !isOn;
			this.mScrollBox.SetPageHorizontal(this.mIsOff ? 1 : 0, false);
		}

		public bool IsOn()
		{
			return !this.mIsOff;
		}

		public Rect mLabelFrame;

		public string mLabel;

		public bool mIsOff;

		public ScrollWidget mScrollBox;

		public ZumaSlideBoxButton mSlideBoxButton;

		public DialogEx mDialog;
	}
}
