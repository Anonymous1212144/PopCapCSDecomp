using System;
using Sexy;

namespace BejeweledLIVE
{
	public class ImageWidget : InterfaceControl
	{
		public ImageWidget()
		{
			this.mImage = null;
			this.mGeometry = ImageWidget.Geometry.DRAW_CENTERED;
		}

		public ImageWidget(Image image, ImageWidget.Geometry geometry)
		{
			this.mImage = image;
			this.mGeometry = geometry;
			this.mWidth = this.mImage.GetWidth();
			this.mHeight = this.mImage.GetHeight();
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void SetImage(Image image, ImageWidget.Geometry geometry)
		{
			this.mImage = image;
			this.mGeometry = geometry;
			this.Resize(this.mX, this.mY, this.mImage.GetWidth(), this.mImage.GetHeight());
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * this.mOpacity)));
			g.SetColorizeImages(true);
			if (this.mGeometry == ImageWidget.Geometry.DRAW_BOX)
			{
				TRect theDest = new TRect(0, 0, this.mWidth, this.mHeight);
				g.DrawImageBox(theDest, this.mImage);
				return;
			}
			if (ImageWidget.Geometry.DRAW_CENTERED == this.mGeometry)
			{
				int theX = (this.mWidth - this.mImage.GetWidth()) / 2;
				int theY = (this.mHeight - this.mImage.GetHeight()) / 2;
				g.DrawImage(this.mImage, theX, theY);
			}
		}

		protected Image mImage;

		protected ImageWidget.Geometry mGeometry;

		public enum Geometry
		{
			DRAW_BOX,
			DRAW_CENTERED
		}
	}
}
