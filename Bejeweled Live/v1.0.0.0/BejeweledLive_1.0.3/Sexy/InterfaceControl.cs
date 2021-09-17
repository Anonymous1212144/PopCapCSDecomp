using System;

namespace Sexy
{
	public class InterfaceControl : Widget
	{
		public bool IsExpanded
		{
			get
			{
				return this.mIsExpanded;
			}
		}

		public InterfaceControl()
		{
			this.mTransitionDuration = 0;
			this.mTransitionTick = 0;
			this.mOpacity = 1f;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public void MoveCenterTo(int cx, int cy)
		{
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
			this.mPositionTwerp.Clear();
			this.mOpacityTwerp.Clear();
			this.mExpandTwerp.Clear();
			this.mPositionTwerp.SetKey(tick, value2, false, true);
			this.mPositionTwerp.SetKey(tick2, value, true, true);
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
			this.mOpacity = 1f;
			this.Update();
		}

		public void SlideOut(WidgetTransitionSubType direction, float startSeconds, float endSeconds)
		{
			TPoint value = new TPoint(this.mX, this.mY);
			TPoint value2 = this.OutPosForDirection(direction);
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mOpacityTwerp.Clear();
			this.mExpandTwerp.Clear();
			this.mPositionTwerp.SetKey(tick, value, false, true);
			this.mPositionTwerp.SetKey(tick2, value2, true, true);
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
			this.mExpandTwerp.Clear();
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
			this.mExpandTwerp.Clear();
			this.mOpacityTwerp.SetKey(tick, 1f, false, true);
			this.mOpacityTwerp.SetKey(tick2, 0f, false, true);
			this.mOpacity = 1f;
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
		}

		public virtual void SetOpacity(float opacity)
		{
			this.mOpacityTwerp.Clear();
			this.mOpacity = opacity;
		}

		public void ExpandDown(float startSeconds, float endSeconds, float theHeight)
		{
			this.mExpandState = 0;
			this.mIsExpanded = true;
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mExpandTwerp.Clear();
			this.mExpandTwerp.SetKey(tick, (float)this.mHeight, false, true);
			this.mExpandTwerp.SetKey(tick2, theHeight, false, true);
			this.mCollapseHeight = this.mHeight;
			this.mExpandY = this.mY;
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
		}

		public void ExpandUp(float startSeconds, float endSeconds, float theHeight)
		{
			this.mIsExpanded = true;
			this.mExpandState = 1;
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mExpandTwerp.Clear();
			this.mExpandTwerp.SetKey(tick, (float)this.mHeight, false, true);
			this.mExpandTwerp.SetKey(tick2, theHeight, false, true);
			this.mCollapseHeight = this.mHeight;
			this.mExpandY = this.mY;
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
		}

		public void CollapseUp(float startSeconds, float endSeconds)
		{
			this.mIsExpanded = false;
			this.mExpandState = 3;
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mExpandTwerp.Clear();
			this.mExpandTwerp.SetKey(tick, (float)this.mHeight, false, true);
			this.mExpandTwerp.SetKey(tick2, (float)this.mCollapseHeight, false, true);
			this.mExpandY = this.mY;
			this.mTransitionTick = 0;
			this.mTransitionDuration = tick2;
		}

		public void CollapseDown(float startSeconds, float endSeconds)
		{
			this.mIsExpanded = false;
			this.mExpandState = 2;
			int tick = SexyAppFrameworkConstants.ticksForSeconds(startSeconds);
			int tick2 = SexyAppFrameworkConstants.ticksForSeconds(endSeconds);
			this.mPositionTwerp.Clear();
			this.mExpandTwerp.Clear();
			this.mExpandTwerp.SetKey(tick, (float)this.mHeight, false, true);
			this.mExpandTwerp.SetKey(tick2, (float)this.mCollapseHeight, false, true);
			this.mExpandY = this.mY;
			this.mTempHeight = this.mHeight;
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
				if (!this.mExpandTwerp.Empty())
				{
					if (this.mExpandState == 0)
					{
						this.mHeight = (int)this.mExpandTwerp.Tick((float)this.mTransitionTick);
						return;
					}
					if (this.mExpandState == 1)
					{
						int num = (int)this.mExpandTwerp.Tick((float)this.mTransitionTick);
						this.mHeight = num;
						this.mY = this.mExpandY - (num - this.mCollapseHeight);
						return;
					}
					if (this.mExpandState == 3)
					{
						this.mHeight = (int)this.mExpandTwerp.Tick((float)this.mTransitionTick);
						return;
					}
					if (this.mExpandState == 2)
					{
						int num2 = (int)this.mExpandTwerp.Tick((float)this.mTransitionTick);
						this.mHeight = num2;
						this.mY = this.mExpandY + (this.mTempHeight - this.mCollapseHeight - (num2 - this.mCollapseHeight));
					}
				}
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

		protected KeyInterpolatorFloat mExpandTwerp = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		protected float mOpacity;

		protected TPoint mOrigionalPos;

		protected int mCollapseY;

		protected int mCollapseHeight;

		protected int mExpandState;

		protected int mExpandY;

		protected int mTempHeight;

		protected bool mIsExpanded;

		public enum ExpandState
		{
			STATE_EXPANDING_DOWN,
			STATE_EXPANDING_UP,
			STATE_COLLAPSING_DOWN,
			STATE_COLLAPSING_UP
		}
	}
}
