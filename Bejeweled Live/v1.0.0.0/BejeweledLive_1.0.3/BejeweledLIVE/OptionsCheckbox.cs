using System;
using Sexy;

namespace BejeweledLIVE
{
	public class OptionsCheckbox : InterfaceControl
	{
		public OptionsCheckbox(int id, CheckboxListener checkboxListener)
		{
			this.mId = id;
			this.mListener = checkboxListener;
			this.mChecked = false;
			this.mWidth = AtlasResources.IMAGE_CHECKBOX_CHECKED.GetWidth();
			this.mHeight = AtlasResources.IMAGE_CHECKBOX_CHECKED.GetHeight();
		}

		public void SetChecked(bool aChecked)
		{
			this.SetChecked(aChecked, true);
		}

		public void SetChecked(bool aChecked, bool tellListener)
		{
			this.mChecked = aChecked;
			if (tellListener && this.mListener != null)
			{
				this.mListener.CheckboxChecked(this.mId, this.mChecked);
			}
		}

		public bool IsChecked()
		{
			return this.mChecked;
		}

		public override void TouchBegan(_Touch touch)
		{
			this.mChecked = !this.mChecked;
			if (this.mListener != null)
			{
				this.mListener.CheckboxChecked(this.mId, this.mChecked);
			}
		}

		public override void TouchMoved(_Touch touch)
		{
		}

		public override void TouchEnded(_Touch touch)
		{
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mOpacity > 0f)
			{
				Image theImage = (this.IsChecked() ? AtlasResources.IMAGE_CHECKBOX_CHECKED : AtlasResources.IMAGE_CHECKBOX_UNCHECKED);
				bool colorizeImages = this.mOpacity < 1f;
				g.SetColorizeImages(colorizeImages);
				g.SetColor(new SexyColor(255, 255, 255, (int)(this.mOpacity * 255f)));
				g.DrawImage(theImage, 0, 0);
			}
		}

		public bool IsButtonDown()
		{
			return this.mIsDown && this.mIsOver && !this.mDisabled;
		}

		public override void Resize(TRect theRect)
		{
			this.mX = theRect.mX;
			this.mY = theRect.mY;
		}

		protected int mId;

		protected CheckboxListener mListener;

		protected bool mChecked;
	}
}
