using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class BlendedImage : IDisposable
	{
		public void Dispose()
		{
			this.DeleteImages();
		}

		public void DeleteImages()
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					this.mImages[i, j].Dispose();
					this.mImages[i, j] = null;
				}
			}
		}

		public BlendedImage(MemoryImage theImage, Rect theSrcRect, bool rotated)
		{
			int theWidth = theSrcRect.mWidth + 3;
			int theHeight = theSrcRect.mHeight + 3;
			MemoryImage memoryImage = new MemoryImage();
			memoryImage.Create(theWidth, theHeight);
			Graphics graphics = new Graphics(memoryImage);
			graphics.DrawImage(theImage, 1, 1, theSrcRect);
			graphics.ClearRenderContext();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					MemoryImage memoryImage2 = new MemoryImage();
					memoryImage2.Create(theWidth, theHeight);
					Graphics graphics2 = new Graphics(memoryImage2);
					if (!rotated)
					{
						graphics2.DrawImageF(memoryImage, (float)i / 4f * 0.9f + 0.1f, (float)j / 4f * 0.9f + 0.1f);
					}
					else
					{
						graphics2.DrawImageRotatedF(memoryImage, (float)i / 4f * 0.9f + 0.1f, (float)j / 4f * 0.9f + 0.1f, -1.5707000494003296);
					}
					this.mImages[i, j] = memoryImage2;
					graphics2.ClearRenderContext();
				}
			}
		}

		public void Draw(Graphics g, float x, float y)
		{
			int num = (int)(((double)x - Math.Floor((double)x)) * 4.0);
			int num2 = (int)(((double)y - Math.Floor((double)y)) * 4.0);
			g.DrawImage(this.mImages[num, num2], (int)x, (int)y);
		}

		protected const int NUM_BLENDS = 4;

		protected Image[,] mImages = new Image[4, 4];
	}
}
