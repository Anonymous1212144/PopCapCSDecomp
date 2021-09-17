using System;
using Sexy;

namespace BejeweledLIVE
{
	public class OptionsSlider : InterfaceControl
	{
		public OptionsSlider(int theId, SliderListener theListener)
		{
			this.mId = theId;
			this.mListener = theListener;
			this.mVal = 0.0;
			this.mDragging = false;
			this.mRelX = (this.mRelY = 0);
			this.mHeight = AtlasResources.IMAGE_SLIDER_BALL_RING.mHeight;
		}

		public virtual void SetValue(double theValue)
		{
			this.mVal = theValue;
			if (this.mVal < 0.0)
			{
				this.mVal = 0.0;
				return;
			}
			if (this.mVal > 1.0)
			{
				this.mVal = 1.0;
			}
		}

		public virtual bool HasTransparencies()
		{
			return true;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mOpacity > 0f)
			{
				int num = (AtlasResources.IMAGE_SLIDER_BALL_RING.mHeight - AtlasResources.IMAGE_SLIDER_RING.mHeight) / 2;
				int num2 = (int)(this.mVal * (double)(this.mWidth - AtlasResources.IMAGE_SLIDER_BALL.GetWidth()));
				int num3 = (int)Constants.mConstants.S(-4f);
				if (this.mIsDown && this.mIsOver)
				{
					bool mDisabled = this.mDisabled;
				}
				bool colorizeImages = this.mOpacity < 1f;
				g.SetColorizeImages(colorizeImages);
				g.SetColor(new SexyColor(255, 255, 255, (int)(this.mOpacity * 255f)));
				g.DrawImageBox(new TRect(0, num, this.mWidth, AtlasResources.IMAGE_SLIDER_RING.mHeight), AtlasResources.IMAGE_SLIDER_RING);
				g.SetColorizeImages(colorizeImages);
				g.DrawImageBox(new TRect((int)Constants.mConstants.S(11f), (int)Constants.mConstants.S(7f) + num, num2 + AtlasResources.IMAGE_SLIDER_BALL.mWidth / 2, AtlasResources.IMAGE_SLIDER_FILL.mHeight), AtlasResources.IMAGE_SLIDER_FILL);
				g.SetColorizeImages(colorizeImages);
				g.SetColor(new SexyColor(255, 255, 255, (int)(this.mOpacity * 255f)));
				g.DrawImage(AtlasResources.IMAGE_SLIDER_BALL, num2, num3 + num);
			}
		}

		public override void TouchBegan(_Touch touch)
		{
			int num = (int)touch.location.x;
			int num2 = (int)(this.mVal * (double)(this.mWidth - AtlasResources.IMAGE_SLIDER_BALL_RING.GetWidth()));
			if (num >= num2 && num < num2 + AtlasResources.IMAGE_SLIDER_BALL_RING.GetWidth())
			{
				this.mDragging = true;
				this.mRelX = num - num2;
				return;
			}
			this.mDragging = true;
			double num3 = (double)num / (double)this.mWidth;
			if (this.mVal != num3)
			{
				this.SetValue(num3);
				this.mListener.SliderVal(this.mId, this.mVal);
			}
		}

		public override void TouchMoved(_Touch touch)
		{
			int num = (int)touch.location.x;
			if (this.mDragging)
			{
				double num2 = this.mVal;
				this.mVal = (double)(num - this.mRelX) / (double)(this.mWidth - AtlasResources.IMAGE_SLIDER_BALL_RING.GetWidth());
				if (this.mVal < 0.0)
				{
					this.mVal = 0.0;
				}
				if (this.mVal > 1.0)
				{
					this.mVal = 1.0;
				}
				if (this.mVal != num2)
				{
					this.mListener.SliderVal(this.mId, this.mVal);
				}
			}
		}

		public override void TouchEnded(_Touch touch)
		{
			this.mDragging = false;
			this.mListener.SliderVal(this.mId, this.mVal);
		}

		public SliderListener mListener;

		public double mVal;

		public int mId;

		public bool mDragging;

		public int mRelX;

		public int mRelY;

		public string mLabel = string.Empty;
	}
}
