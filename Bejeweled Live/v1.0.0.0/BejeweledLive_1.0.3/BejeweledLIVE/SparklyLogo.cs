using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class SparklyLogo : Widget
	{
		public SparklyLogo()
		{
			this.mTransitionDuration = 0;
			this.mTransitionTick = 0;
			this.mLogoImage = AtlasResources.IMAGE_MAINMENU_LIVE_LOGO;
			this.mWidth = this.mLogoImage.GetWidth();
			this.mHeight = this.mLogoImage.GetHeight();
			this.mStarImage = AtlasResources.IMAGE_BIGSTAR;
			this.mClip = false;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void SetupForLayout(int layout)
		{
		}

		public void SlideIn(WidgetTransitionSubType direction, float startSeconds, float endSeconds)
		{
			TPoint value = new TPoint(this.mX, this.mY);
			TPoint value2 = this.OutPosForDirection(direction);
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mOpacityTwerp.Clear();
			this.mPositionTwerp.Clear();
			this.mPositionTwerp.SetKey(tick, value2, false, true);
			this.mPositionTwerp.SetKey(tick2, value, true, true);
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
			this.mOpacity = 1f;
		}

		public void FadeIn(float startSeconds, float endSeconds)
		{
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mOpacityTwerp.Clear();
			this.mOpacityTwerp.SetKey(tick, 0f, false, true);
			this.mOpacityTwerp.SetKey(tick2, 1f, false, true);
			this.mOpacity = 0f;
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
		}

		public void FadeOut(float startSeconds, float endSeconds)
		{
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mOpacityTwerp.Clear();
			this.mOpacityTwerp.SetKey(tick, 1f, false, true);
			this.mOpacityTwerp.SetKey(tick2, 0f, false, true);
			this.mOpacity = 1f;
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
		}

		public override void Update()
		{
			base.Update();
			if (this.mVisible && this.mTransitionTick < this.mTransitionDuration)
			{
				this.mTransitionTick++;
				if (!this.mPositionTwerp.Empty())
				{
					TPoint tpoint = this.mPositionTwerp.Tick((float)this.mTransitionTick);
					this.Move(tpoint.mX, tpoint.mY);
				}
				if (!this.mOpacityTwerp.Empty())
				{
					this.mOpacity = this.mOpacityTwerp.Tick((float)this.mTransitionTick);
				}
			}
		}

		public void Scale(float scale)
		{
			this.mWidth = (int)((float)this.mLogoImage.mWidth * scale);
			this.mHeight = (int)((float)this.mLogoImage.mHeight * scale);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mOpacity > 0f)
			{
				if (this.mOpacity < 1f)
				{
					g.SetColorizeImages(true);
					g.SetColor(new Color(255, 255, 255, (int)(this.mOpacity * 255f)));
				}
				g.DrawImage(this.mLogoImage, 0, 0, this.mWidth, this.mHeight);
			}
		}

		protected TPoint OutPosForDirection(WidgetTransitionSubType direction)
		{
			TPoint result = new TPoint(this.mX, this.mY);
			switch (direction)
			{
			case WidgetTransitionSubType.WIDGET_FROM_RIGHT:
				result.mX = this.mParent.mWidth;
				break;
			case WidgetTransitionSubType.WIDGET_FROM_LEFT:
				result.mX = -this.mWidth;
				break;
			case WidgetTransitionSubType.WIDGET_FROM_TOP:
				result.mY = -this.mHeight;
				break;
			case WidgetTransitionSubType.WIDGET_FROM_BOTTOM:
				result.mY = this.mParent.mHeight;
				break;
			}
			return result;
		}

		protected int mTransitionDuration;

		protected int mTransitionTick;

		protected KeyInterpolatorTPoint mPositionTwerp = KeyInterpolatorTPoint.GetNewKeyInterpolatorTPoint();

		protected KeyInterpolatorFloat mOpacityTwerp = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		protected float mOpacity;

		private Image mLogoImage;

		private Image mStarImage;
	}
}
