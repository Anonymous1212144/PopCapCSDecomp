using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3Slider : Slider
	{
		public Bej3Slider(int theId, SliderListener theListener)
			: base(GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL, GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HANDLE, theId, theListener)
		{
			this.mGrayedOut = true;
			this.mGrayOutWhenDisabled = false;
			this.mClippingEnabled = true;
			this.mLocked = false;
			this.mThreshold = new Point(0, 0);
			this.mDownPos = new Point(-1, -1);
			this.mGrayOutWhenZero = true;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			theHeight = ConstantsWP.BEJ3SLIDER_HEIGHT;
			theY -= theHeight / 2;
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public void LinkUpAssets()
		{
			this.mThumbImage = GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HANDLE;
			if (this.mGrayedOut || (this.mVal == 0.0 && this.mGrayOutWhenZero))
			{
				this.mTrackImage = GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE;
				return;
			}
			this.mTrackImage = GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL;
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			if (!this.mClippingEnabled)
			{
				g.ClearClipRect();
			}
			int num = (this.mHorizontal ? (this.mTrackImage.GetWidth() / 3) : this.mTrackImage.GetWidth());
			int num2 = (this.mHorizontal ? this.mTrackImage.GetHeight() : (this.mTrackImage.GetHeight() / 3));
			if (this.mHorizontal)
			{
				int theY = this.mHeight / 2 - this.mTrackImage.GetCelHeight() / 2;
				g.DrawImageBox(new Rect(ConstantsWP.BEJ3SLIDER_X_OFFSET, theY, this.mWidth - ConstantsWP.BEJ3SLIDER_WIDTH_OFFSET - ConstantsWP.BEJ3SLIDER_X_OFFSET, this.mTrackImage.GetCelHeight()), this.mTrackImage);
				Bej3Slider.xOfsFill = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1369) - GlobalMembersResourcesWP.ImgXOfs(1368));
				int num3 = (int)(this.mVal * (double)(this.mWidth - this.mThumbImage.GetCelWidth())) + this.mThumbImage.GetCelWidth() / 2;
				int theY2 = this.mHeight / 2 - GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL.GetCelHeight() / 2;
				Rect mClipRect = g.mClipRect;
				int num4 = num3;
				g.ClipRect(Bej3Slider.xOfsFill + ConstantsWP.BEJ3SLIDER_X_OFFSET, theY2, num4 - ConstantsWP.BEJ3SLIDER_X_OFFSET, GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL.GetCelHeight());
				Image theComponentImage = (this.mGrayedOut ? GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE : GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL);
				g.DrawImageBox(new Rect(Bej3Slider.xOfsFill + ConstantsWP.BEJ3SLIDER_X_OFFSET, theY2, this.mWidth - ConstantsWP.BEJ3SLIDER_WIDTH_OFFSET - Bej3Slider.xOfsFill * 2 - ConstantsWP.BEJ3SLIDER_X_OFFSET, GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL.GetCelHeight()), theComponentImage);
				g.mClipRect = mClipRect;
			}
			else
			{
				int theX = (this.mWidth - num) / 2;
				g.DrawImage(this.mTrackImage, theX, 0, new Rect(0, 0, num, num2));
				for (int i = 0; i < (this.mHeight - num2 * 2 + num2 - 1) / num2; i++)
				{
					g.DrawImage(this.mTrackImage, theX, num2 + i * num2, new Rect(0, num2, num, num2));
				}
				g.DrawImage(this.mTrackImage, theX, this.mHeight - num2, new Rect(0, num2 * 2, num, num2));
			}
			if (this.mHorizontal && this.mThumbImage != null)
			{
				Rect theSrcRect = default(Rect);
				if (this.mGrayedOut)
				{
					theSrcRect = this.mThumbImage.GetCelRect(2);
				}
				else
				{
					theSrcRect = this.mThumbImage.GetCelRect(1);
				}
				g.DrawImage(this.mThumbImage, (int)(this.mVal * (double)(this.mWidth - this.mThumbImage.GetCelWidth())), (this.mHeight - this.mThumbImage.GetCelHeight()) / 2, theSrcRect);
				if (this.mDragging)
				{
					theSrcRect = this.mThumbImage.GetCelRect(0);
					g.DrawImage(this.mThumbImage, (int)(this.mVal * (double)(this.mWidth - this.mThumbImage.GetCelWidth())), (this.mHeight - this.mThumbImage.GetCelHeight()) / 2, theSrcRect);
					return;
				}
			}
			else if (!this.mHorizontal && this.mThumbImage != null)
			{
				g.DrawImage(this.mThumbImage, (this.mWidth - this.mThumbImage.GetCelWidth()) / 2, (int)(this.mVal * (double)(this.mHeight - this.mThumbImage.GetCelHeight())));
			}
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			if (this.mGrayOutWhenDisabled)
			{
				this.mGrayedOut = this.mDisabled;
			}
			this.LinkUpAssets();
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
			this.mDragging = true;
			int num = ((this.mThumbImage == null) ? this.mKnobSize : this.mThumbImage.GetCelWidth());
			int num2 = (int)(this.mVal * (double)(this.mWidth - num));
			this.mRelX = x - num2;
		}

		public override void MouseDrag(int x, int y)
		{
			base.MouseDrag(x, y);
		}

		public override void MouseUp(int x, int y)
		{
			this.mLocked = false;
			base.MouseUp(x, y);
		}

		public void SetGrayedOut(bool grayedOut)
		{
		}

		public override void TouchesCanceled()
		{
			base.MouseUp(-1, -1);
			base.TouchesCanceled();
		}

		public void SetThreshold(int x, int y)
		{
			this.mThreshold.mX = x;
			this.mThreshold.mY = y;
		}

		public bool mGrayedOut;

		public bool mGrayOutWhenDisabled;

		public bool mClippingEnabled;

		public bool mLocked;

		public Point mThreshold = default(Point);

		public Point mDownPos = default(Point);

		public bool mGrayOutWhenZero;

		private static int xOfsFill;
	}
}
