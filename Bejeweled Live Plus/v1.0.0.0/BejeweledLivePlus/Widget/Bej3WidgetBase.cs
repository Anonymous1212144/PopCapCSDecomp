using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3WidgetBase : Widget
	{
		public Bej3WidgetBase()
		{
			this.mAlphaStep = 0f;
			this.mAlpha = 1f;
			this.mColor = Color.White;
			this.mClippingEnabled = true;
			this.mGrayedOut = false;
		}

		public void Bej3WidgetBaseDrawAll(ModalFlags theFlags, Graphics g)
		{
			base.DrawAll(theFlags, g);
		}

		public override void Update()
		{
			if (this.mAlphaStep != 0f)
			{
				this.mAlpha += this.mAlphaStep;
				if (this.mAlpha < 0f)
				{
					this.mAlpha = 0f;
					this.mAlphaStep = 0f;
				}
				if (this.mAlpha > 1f)
				{
					this.mAlpha = 1f;
					this.mAlphaStep = 0f;
				}
				this.mColor.mAlpha = (int)(255f * this.mAlpha);
			}
			base.Update();
		}

		public virtual void Fade(float step)
		{
			this.mAlphaStep = step;
		}

		public virtual void FadeIn(float step)
		{
			this.mAlphaStep = step;
		}

		public virtual void FadeOut(float step)
		{
			if (step > 0f)
			{
				step = -step;
			}
			this.mAlphaStep = step;
		}

		public virtual void FadeIn()
		{
			this.FadeIn(0.01f);
		}

		public virtual void FadeOut()
		{
			this.FadeOut(0.01f);
		}

		public virtual void LinkUpAssets()
		{
		}

		private float mAlphaStep;

		public Color mColor = default(Color);

		public float mAlpha;

		public bool mClippingEnabled;

		public bool mGrayedOut;

		public static readonly Color GreyedOutColor = new Color(150, 150, 150, 255);
	}
}
