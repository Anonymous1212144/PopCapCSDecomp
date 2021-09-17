using System;
using Sexy;

namespace BejeweledLIVE
{
	public class InterfaceButton : ButtonWidget
	{
		public InterfaceButton(int id, ButtonListener listener)
			: base(id, listener)
		{
			this.Reset();
		}

		protected override void Reset()
		{
			base.Reset();
			this.mLayoutWidthFactor = 0f;
			this.mPositionTwerp.Clear();
			this.mOpacityTwerp.Clear();
			this.mTransitionDuration = 0;
			this.mTransitionTick = 0;
			this.mOpacity = 1f;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void SetLayoutWidthFactor(float widthFactor)
		{
			this.mLayoutWidthFactor = widthFactor;
		}

		public int GetLayoutWidth(int columnWidth)
		{
			return (int)(this.mLayoutWidthFactor * (float)columnWidth);
		}

		public void MoveCenterTo(int cx, int cy)
		{
			this.mPositionTwerp.Clear();
			int theNewX = cx - this.mWidth / 2;
			int theNewY = cy - this.mHeight / 2;
			this.Move(theNewX, theNewY);
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

		public virtual void FadeIn(float startSeconds, float endSeconds)
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

		public virtual void FadeOut(float startSeconds, float endSeconds)
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

		public void SetOpacity(float fOpacity)
		{
			this.mOpacity = fOpacity;
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

		protected float mLayoutWidthFactor;

		protected int mTransitionDuration;

		protected int mTransitionTick;

		protected KeyInterpolatorTPoint mPositionTwerp = KeyInterpolatorTPoint.GetNewKeyInterpolatorTPoint();

		protected KeyInterpolatorFloat mOpacityTwerp = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		protected float mOpacity;
	}
}
