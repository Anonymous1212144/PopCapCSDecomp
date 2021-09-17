using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class GraphicsRecorder : IDisposable
	{
		protected GraphicsOperationRef AllocOperation(Graphics g, GraphicsOperation.IMAGE_TYPE theType)
		{
			return this.mGraphicsOperations.alloc(g, theType, this.mTimestamp);
		}

		public virtual void Dispose()
		{
		}

		public GraphicsRecorder()
		{
			this.mIgnoreDraws = false;
			this.mRecordDraws = false;
			this.mTimestamp = 0;
		}

		public void SetTimestamp(int theTimestamp)
		{
			this.mTimestamp = theTimestamp;
		}

		public void DrawImage(Graphics g, Image theImage, int theX, int theY)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImageF(theImage, (float)theX, (float)theY);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XY);
				graphicsOperationRef.mImage = theImage;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = (float)theX + g.mTransX;
				mDestRect.mY = (float)theY + g.mTransY;
				graphicsOperationRef.mDestRect = mDestRect;
			}
		}

		public void DrawImageF(Graphics g, Image theImage, float theX, float theY)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImage(theImage, (int)theX, (int)theY);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XY);
				graphicsOperationRef.mImage = theImage;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = theX + (float)((int)g.mTransX);
				mDestRect.mY = theY + (float)((int)g.mTransY);
				graphicsOperationRef.mDestRect = mDestRect;
			}
		}

		public void DrawImage(Graphics g, Image theImage, int theX, int theY, int theStretchedWidth, int theStretchedHeight)
		{
			if (!this.mIgnoreDraws || string.IsNullOrEmpty(theImage.mFilePath))
			{
				g.DrawImage(theImage, theX, theY, theStretchedWidth, theStretchedHeight);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XYWH);
				graphicsOperationRef.mImage = theImage;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = (float)(theX + (int)g.mTransX);
				mDestRect.mY = (float)(theY + (int)g.mTransY);
				graphicsOperationRef.mDestRect = mDestRect;
				Rect mSrcRect = graphicsOperationRef.mSrcRect;
				mSrcRect.mWidth = theStretchedWidth;
				mSrcRect.mHeight = theStretchedHeight;
				graphicsOperationRef.mSrcRect = mSrcRect;
			}
		}

		public void DrawImage(Graphics g, Image theImage, int theX, int theY, Rect theSrcRect)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImage(theImage, theX, theY, theSrcRect);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_XYR);
				graphicsOperationRef.mImage = theImage;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = (float)(theX + (int)g.mTransX);
				mDestRect.mY = (float)(theY + (int)g.mTransY);
				graphicsOperationRef.mDestRect = mDestRect;
				graphicsOperationRef.mSrcRect = theSrcRect;
			}
		}

		public void DrawImage(Graphics g, Image theImage, Rect theDestRect, Rect theSrcRect)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImage(theImage, theDestRect, theSrcRect);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGE_RR);
				graphicsOperationRef.mImage = theImage;
				graphicsOperationRef.mDestRect = new FRect((float)(theDestRect.mX + (int)g.mTransX), (float)(theDestRect.mY + (int)g.mTransY), (float)theDestRect.mWidth, (float)theDestRect.mHeight);
				graphicsOperationRef.mSrcRect = theSrcRect;
			}
		}

		public void DrawImageCel(Graphics g, Image theImageStrip, int theX, int theY, int theCel)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImageCel(theImageStrip, theX, theY, theCel);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImageStrip.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGECEL_XYC);
				graphicsOperationRef.mImage = theImageStrip;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = (float)(theX + (int)g.mTransX);
				mDestRect.mY = (float)(theY + (int)g.mTransY);
				graphicsOperationRef.mDestRect = mDestRect;
				Rect mSrcRect = graphicsOperationRef.mSrcRect;
				mSrcRect.mX = theCel;
				graphicsOperationRef.mSrcRect = mSrcRect;
			}
		}

		public void DrawImageCel(Graphics g, Image theImageStrip, Rect theDestRect, int theCel)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImageCel(theImageStrip, theDestRect, theCel);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImageStrip.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGECEL_RC);
				graphicsOperationRef.mImage = theImageStrip;
				graphicsOperationRef.mDestRect = new FRect((float)(theDestRect.mX + (int)g.mTransX), (float)(theDestRect.mY + (int)g.mTransY), (float)theDestRect.mWidth, (float)theDestRect.mHeight);
				Rect mSrcRect = graphicsOperationRef.mSrcRect;
				mSrcRect.mX = theCel;
				graphicsOperationRef.mSrcRect = mSrcRect;
			}
		}

		public void DrawImageRotated(Graphics g, Image theImage, int theX, int theY, double theRot)
		{
			this.DrawImageRotated(g, theImage, theX, theY, theRot, Rect.INVALIDATE_RECT);
		}

		public void DrawImageRotated(Graphics g, Image theImage, int theX, int theY, double theRot, Rect theSrcRect)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImageRotated(theImage, theX, theY, theRot, theSrcRect);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGEROTATED_XYAR);
				graphicsOperationRef.mImage = theImage;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = (float)(theX + (int)g.mTransX);
				mDestRect.mY = (float)(theY + (int)g.mTransY);
				graphicsOperationRef.mDestRect = mDestRect;
				graphicsOperationRef.mFloat = (float)theRot;
				if (theSrcRect != Rect.INVALIDATE_RECT)
				{
					graphicsOperationRef.mSrcRect = theSrcRect;
					return;
				}
				graphicsOperationRef.mSrcRect = new Rect(0, 0, theImage.mWidth, theImage.mHeight);
			}
		}

		public void DrawImageRotated(Graphics g, Image theImage, int theX, int theY, double theRot, int theRotCenterX, int theRotCenterY)
		{
			this.DrawImageRotated(g, theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, Rect.INVALIDATE_RECT);
		}

		public void DrawImageRotated(Graphics g, Image theImage, int theX, int theY, double theRot, int theRotCenterX, int theRotCenterY, Rect theSrcRect)
		{
			if (!this.mIgnoreDraws)
			{
				g.DrawImageRotated(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, theSrcRect);
			}
			if (this.mRecordDraws && !string.IsNullOrEmpty(theImage.mFilePath))
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_DRAWIMAGEROTATED_XYAXYR);
				graphicsOperationRef.mImage = theImage;
				FRect mDestRect = graphicsOperationRef.mDestRect;
				mDestRect.mX = (float)(theX + (int)g.mTransX);
				mDestRect.mY = (float)(theY + (int)g.mTransY);
				mDestRect.mWidth = (float)theRotCenterX;
				mDestRect.mHeight = (float)theRotCenterY;
				graphicsOperationRef.mDestRect = mDestRect;
				graphicsOperationRef.mFloat = (float)theRot;
				if (theSrcRect != Rect.INVALIDATE_RECT)
				{
					graphicsOperationRef.mSrcRect = theSrcRect;
					return;
				}
				graphicsOperationRef.mSrcRect = new Rect(0, 0, theImage.mWidth, theImage.mHeight);
			}
		}

		public void DrawImageTransformHelper(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect, float x, float y, bool useFloat)
		{
			if (theTransform.mComplex)
			{
				if (!this.mIgnoreDraws)
				{
					g.DrawImageMatrix(theImage, theTransform.GetMatrix(), theSrcRect, x, y);
				}
				return;
			}
			float num = (float)theSrcRect.mWidth / 2f;
			float num2 = (float)theSrcRect.mHeight / 2f;
			if (theTransform.mHaveRot)
			{
				float num3 = num - theTransform.mTransX1;
				float num4 = num2 - theTransform.mTransY1;
				x = x + theTransform.mTransX2 - num3 + 0.5f;
				y = y + theTransform.mTransY2 - num4 + 0.5f;
				if (useFloat)
				{
					g.DrawImageRotatedF(theImage, x, y, (double)theTransform.mRot, num3, num4, theSrcRect);
					return;
				}
				this.DrawImageRotated(g, theImage, (int)x, (int)y, (double)theTransform.mRot, (int)num3, (int)num4, theSrcRect);
				return;
			}
			else if (theTransform.mHaveScale)
			{
				bool flag = false;
				if (theTransform.mScaleX == -1f)
				{
					if (theTransform.mScaleY == 1f)
					{
						x = x + theTransform.mTransX1 + theTransform.mTransX2 - num + 0.5f;
						y = y + theTransform.mTransY1 + theTransform.mTransY2 - num2 + 0.5f;
						g.DrawImageMirror(theImage, (int)x, (int)y, theSrcRect);
						return;
					}
					flag = true;
				}
				float num5 = num * theTransform.mScaleX;
				float num6 = num2 * theTransform.mScaleY;
				x = x + theTransform.mTransX2 - num5;
				y = y + theTransform.mTransY2 - num6;
				Rect theDestRect = new Rect((int)x, (int)y, (int)(num5 * 2f), (int)(num6 * 2f));
				if (!flag)
				{
					this.DrawImage(g, theImage, theDestRect, theSrcRect);
					return;
				}
				g.DrawImageMirror(theImage, theDestRect, theSrcRect, true);
				return;
			}
			else
			{
				x = x + theTransform.mTransX1 + theTransform.mTransX2 - num + 0.5f;
				y = y + theTransform.mTransY1 + theTransform.mTransY2 - num2 + 0.5f;
				if (useFloat)
				{
					g.DrawImageF(theImage, x, y, theSrcRect);
					return;
				}
				this.DrawImage(g, theImage, (int)x, (int)y, theSrcRect);
				return;
			}
		}

		public void DrawImageTransform(Graphics g, Image theImage, Transform theTransform)
		{
			this.DrawImageTransform(g, theImage, theTransform, 0f, 0f);
		}

		public void DrawImageTransform(Graphics g, Image theImage, Transform theTransform, float x)
		{
			this.DrawImageTransform(g, theImage, theTransform, x, 0f);
		}

		public void DrawImageTransform(Graphics g, Image theImage, Transform theTransform, float x, float y)
		{
			this.DrawImageTransformHelper(g, theImage, theTransform, new Rect(0, 0, theImage.mWidth, theImage.mHeight), x, y, false);
		}

		public void DrawImageTransform(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect)
		{
			this.DrawImageTransform(g, theImage, theTransform, theSrcRect, 0f, 0f);
		}

		public void DrawImageTransform(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect, float x)
		{
			this.DrawImageTransform(g, theImage, theTransform, theSrcRect, x, 0f);
		}

		public void DrawImageTransform(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect, float x, float y)
		{
			this.DrawImageTransformHelper(g, theImage, theTransform, theSrcRect, x, y, false);
		}

		public void DrawImageTransformF(Graphics g, Image theImage, Transform theTransform)
		{
			this.DrawImageTransformF(g, theImage, theTransform, 0f, 0f);
		}

		public void DrawImageTransformF(Graphics g, Image theImage, Transform theTransform, float x)
		{
			this.DrawImageTransformF(g, theImage, theTransform, x, 0f);
		}

		public void DrawImageTransformF(Graphics g, Image theImage, Transform theTransform, float x, float y)
		{
			this.DrawImageTransformHelper(g, theImage, theTransform, new Rect(0, 0, theImage.mWidth, theImage.mHeight), x, y, true);
		}

		public void DrawImageTransformF(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect)
		{
			this.DrawImageTransformF(g, theImage, theTransform, theSrcRect, 0f, 0f);
		}

		public void DrawImageTransformF(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect, float x)
		{
			this.DrawImageTransformF(g, theImage, theTransform, theSrcRect, x, 0f);
		}

		public void DrawImageTransformF(Graphics g, Image theImage, Transform theTransform, Rect theSrcRect, float x, float y)
		{
			this.DrawImageTransformHelper(g, theImage, theTransform, theSrcRect, x, y, true);
		}

		public void SetScale(Graphics g, float theScaleX, float theScaleY, float theOrigX, float theOrigY)
		{
			if (!this.mIgnoreDraws)
			{
				g.SetScale(theScaleX, theScaleY, theOrigX, theOrigY);
			}
			if (this.mRecordDraws)
			{
				GraphicsOperationRef graphicsOperationRef = this.AllocOperation(g, GraphicsOperation.IMAGE_TYPE.TYPE_SETSCALE);
				graphicsOperationRef.mFRect = new FRect(theScaleX, theScaleY, theOrigX, theOrigY);
			}
		}

		public void ExecuteOperations(Graphics g, int theTimestamp)
		{
			if (this.mGraphicsOperations.count() <= 0)
			{
				return;
			}
			g.PushState();
			g.SetColorizeImages(true);
			this.mGraphicsOperations.executeFrom(theTimestamp, g);
			g.PopState();
		}

		public void ClearOperationsFrom(int theTimestampFrom)
		{
			this.mGraphicsOperations.clearFrom(theTimestampFrom);
		}

		public void ClearOperationsTo(int theTimestampTo)
		{
			this.mGraphicsOperations.clearTo(theTimestampTo);
		}

		public int GetFirstOperationTimestamp()
		{
			return this.mGraphicsOperations.firstTimestamp();
		}

		public int GetLastOperationTimestamp()
		{
			return this.mGraphicsOperations.lastTimestamp();
		}

		public int mTimestamp;

		public bool mIgnoreDraws;

		public bool mRecordDraws;

		private GraphicsOperationChain mGraphicsOperations = new GraphicsOperationChain();
	}
}
