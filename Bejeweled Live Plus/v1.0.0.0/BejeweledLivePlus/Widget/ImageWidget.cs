using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Widget
{
	public class ImageWidget : Bej3WidgetBase
	{
		private void DrawImage(Graphics g)
		{
			if (this.mImage == null)
			{
				return;
			}
			if (this.mIsImageBox)
			{
				g.DrawImageBoxStretch(this.mImageBox, this.mImage);
				return;
			}
			Rect theDestRect = new Rect(this.mAlignmentOffset.mX, this.mAlignmentOffset.mY, this.mImageSize.mX, this.mImageSize.mY);
			Rect celRect = this.mImage.GetCelRect(0);
			theDestRect.mWidth = (int)((float)theDestRect.mWidth * this.mScale);
			theDestRect.mHeight = (int)((float)theDestRect.mHeight * this.mScale);
			g.DrawImageMirror(this.mImage, theDestRect, celRect, this.mMirror);
			if (this.mAdditiveOverlayColor != Color.White)
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColor(this.mAdditiveOverlayColor);
				g.DrawImageMirror(this.mImage, theDestRect, celRect, this.mMirror);
			}
		}

		public ImageWidget(int theImage)
		{
			bool mClippingEnabled = false;
			this.mMirror = false;
			this.mImageId = theImage;
			this.mScale = 1f;
			this.mIsImageBox = false;
			this.mImageBox = new Rect(0, 0, 0, 0);
			this.mImage = null;
			this.mAdditiveOverlayColor = Color.White;
			this.mImageSize = default(Point);
			this.mAlignment = IMAGEWIDGET_ALIGNMENT.IMAGEWIDGET_ALIGNMENT_TOPLEFT;
			this.mAlignmentOffset = default(Point);
			this.mClippingEnabled = mClippingEnabled;
			this.SetImage(theImage);
		}

		public ImageWidget(int theImage, bool clippingEnabled)
		{
			this.mMirror = false;
			this.mImageId = theImage;
			this.mScale = 1f;
			this.mIsImageBox = false;
			this.mImageBox = new Rect(0, 0, 0, 0);
			this.mImage = null;
			this.mAdditiveOverlayColor = Color.White;
			this.mImageSize = default(Point);
			this.mAlignment = IMAGEWIDGET_ALIGNMENT.IMAGEWIDGET_ALIGNMENT_TOPLEFT;
			this.mAlignmentOffset = default(Point);
			this.mClippingEnabled = clippingEnabled;
			this.SetImage(theImage);
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			Color mColor = this.mColor;
			g.SetColor(mColor);
			if (!this.mClippingEnabled)
			{
				Rect mClipRect = g.mClipRect;
				g.ClearClipRect();
				this.DrawImage(g);
				g.SetClipRect(mClipRect);
				return;
			}
			this.DrawImage(g);
		}

		public override void LinkUpAssets()
		{
			if (this.mImageId != -1)
			{
				this.mImage = GlobalMembersResourcesWP.GetImageById(this.mImageId);
			}
			IMAGEWIDGET_ALIGNMENT imagewidget_ALIGNMENT = this.mAlignment;
			if (imagewidget_ALIGNMENT == IMAGEWIDGET_ALIGNMENT.IMAGEWIDGET_ALIGNMENT_CENTRE)
			{
				this.mAlignmentOffset = new Point(-this.mImageSize.mX / 2, -this.mImageSize.mY / 2);
				return;
			}
			this.mAlignmentOffset = new Point(0, 0);
		}

		public void SetImage(int imageId)
		{
			this.mImageId = imageId;
			this.LinkUpAssets();
			if (this.mImage != null)
			{
				this.mImageSize = new Point(this.mImage.mWidth, this.mImage.mHeight);
			}
		}

		public void SetImage(Image image)
		{
			this.mImageId = -1;
			this.mImage = image;
			this.LinkUpAssets();
			this.mImageSize = new Point(this.mImage.GetCelWidth(), this.mImage.GetCelHeight());
		}

		public Image GetImage()
		{
			return this.mImage;
		}

		public int GetImageId()
		{
			return this.mImageId;
		}

		public void SetImageBox(Rect theImageBox)
		{
			this.mImageBox = theImageBox;
			this.mIsImageBox = this.mImageBox.mWidth != 0 && this.mImageBox.mHeight != 0;
		}

		public void SetImageSize(int width, int height)
		{
			this.mImageSize = new Point(width, height);
		}

		public Point GetImageSize()
		{
			return this.mImageSize;
		}

		public void SetAlignment(IMAGEWIDGET_ALIGNMENT alignment)
		{
			this.mAlignment = alignment;
			this.LinkUpAssets();
		}

		private int mImageId;

		private Image mImage;

		private bool mIsImageBox;

		private Rect mImageBox;

		private Point mImageSize;

		private IMAGEWIDGET_ALIGNMENT mAlignment;

		private Point mAlignmentOffset;

		public bool mMirror;

		public float mScale;

		public Color mAdditiveOverlayColor;
	}
}
