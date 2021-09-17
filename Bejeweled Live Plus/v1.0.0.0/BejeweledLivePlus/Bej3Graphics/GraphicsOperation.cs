using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public struct GraphicsOperation
	{
		public GraphicsOperation(Graphics g, GraphicsOperation.IMAGE_TYPE theType, int Timestamp)
		{
			this.mType = theType;
			this.mDrawMode = g.mDrawMode;
			this.mColor = (g.mColorizeImages ? g.mColor : Color.White);
			this.mTimestamp = Timestamp;
			this.mImage = null;
			this.mDestRect = default(FRect);
			this.mSrcRect = default(Rect);
			this.mFRect = default(FRect);
			this.mFloat = 0f;
		}

		public void Execute(Graphics g)
		{
			g.SetDrawMode(this.mDrawMode);
			g.SetColor(this.mColor);
			Rect theDestRect = new Rect((int)this.mDestRect.mX, (int)this.mDestRect.mY, (int)this.mDestRect.mWidth, (int)this.mDestRect.mHeight);
			switch (this.mType)
			{
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XY:
				g.DrawImageF(this.mImage, this.mDestRect.mX, this.mDestRect.mY);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XYWH:
				g.DrawImage(this.mImage, (int)this.mDestRect.mX, (int)this.mDestRect.mY, this.mSrcRect.mWidth, this.mSrcRect.mHeight);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XYR:
				g.DrawImage(this.mImage, (int)this.mDestRect.mX, (int)this.mDestRect.mY, this.mSrcRect);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_RR:
				g.DrawImage(this.mImage, theDestRect, this.mSrcRect);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGECEL_XYC:
				g.DrawImageCel(this.mImage, (int)this.mDestRect.mX, (int)this.mDestRect.mY, this.mSrcRect.mX);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGECEL_RC:
				g.DrawImageCel(this.mImage, theDestRect, this.mSrcRect.mX);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGEROTATED_XYAR:
				g.DrawImageRotated(this.mImage, (int)this.mDestRect.mX, (int)this.mDestRect.mY, (double)this.mFloat, this.mSrcRect);
				return;
			case GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGEROTATED_XYAXYR:
				g.DrawImageRotated(this.mImage, (int)this.mDestRect.mX, (int)this.mDestRect.mY, (double)this.mFloat, (int)this.mDestRect.mWidth, (int)this.mDestRect.mHeight, this.mSrcRect);
				return;
			default:
				return;
			}
		}

		public int mTimestamp;

		public GraphicsOperation.IMAGE_TYPE mType;

		public Image mImage;

		public FRect mDestRect;

		public Rect mSrcRect;

		public Color mColor;

		public int mDrawMode;

		public float mFloat;

		public FRect mFRect;

		public enum IMAGE_TYPE
		{
			TYPE_DRAWIMAGE_XY,
			TYPE_DRAWIMAGE_XYWH,
			TYPE_DRAWIMAGE_XYR,
			TYPE_DRAWIMAGE_RR,
			TYPE_DRAWIMAGECEL_XYC,
			TYPE_DRAWIMAGECEL_RC,
			TYPE_DRAWIMAGEROTATED_XYAR,
			TYPE_DRAWIMAGEROTATED_XYAXYR,
			TYPE_SETSCALE
		}
	}
}
