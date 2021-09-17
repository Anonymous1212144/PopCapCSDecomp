using System;
using System.Linq;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3ScrollWidget : ScrollWidget
	{
		public Bej3ScrollWidget(Bej3ScrollWidgetListener listener, bool useIndicators)
			: base(listener)
		{
			this.mLocked = false;
			this.mAllowScrolling = true;
			this.mTouched = false;
			this.mScrollDownOffset = 0;
			if (useIndicators)
			{
				base.EnableIndicators(GlobalMembersResourcesWP.IMAGE_DIALOG_SCROLLBAR);
				base.SetIndicatorsInsets(new Insets(ConstantsWP.SCROLLWIDGET_INSET_X, ConstantsWP.SCROLLWIDGET_INSET_Y, ConstantsWP.SCROLLWIDGET_INSET_X, ConstantsWP.SCROLLWIDGET_INSET_Y));
			}
		}

		public Bej3ScrollWidget(Bej3ScrollWidgetListener listener)
			: base(listener)
		{
			bool flag = true;
			this.mLocked = false;
			this.mAllowScrolling = true;
			this.mTouched = false;
			this.mScrollDownOffset = 0;
			if (flag)
			{
				base.EnableIndicators(GlobalMembersResourcesWP.IMAGE_DIALOG_SCROLLBAR);
				base.SetIndicatorsInsets(new Insets(ConstantsWP.SCROLLWIDGET_INSET_X, ConstantsWP.SCROLLWIDGET_INSET_Y, ConstantsWP.SCROLLWIDGET_INSET_X, ConstantsWP.SCROLLWIDGET_INSET_Y));
			}
		}

		public void AllowScrolling(bool allow)
		{
			this.mAllowScrolling = allow;
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
			if (!this.mTouched)
			{
				if (this.mClient != null)
				{
					bool flag = true;
					if (this.mSeekScrollTarget)
					{
						if (this.mListener != null)
						{
							this.mListener.ScrollTargetInterrupted(this);
						}
						if (this.mPagingEnabled)
						{
							flag = false;
						}
					}
					this.mScrollTouchReference = new FPoint((float)touch.location.mX, (float)touch.location.mY);
					this.mScrollOffsetReference = new FPoint((float)this.mClient.mX, (float)this.mClient.mY);
					this.mScrollOffset = new FPoint(this.mScrollOffsetReference);
					this.mScrollLastTimestamp = touch.timestamp;
					this.mScrollTracking = false;
					if (flag)
					{
						this.mSeekScrollTarget = false;
					}
					this.mClientLastDown = base.GetClientWidgetAt(touch);
					this.mClientLastDown.mIsDown = true;
					this.mClientLastDown.mIsOver = true;
					this.mClientLastDown.TouchBegan(touch);
				}
				this.MarkDirty();
			}
			this.mTouched = true;
		}

		public override void TouchMoved(SexyAppBase.Touch touch)
		{
			if (this.mAllowScrolling)
			{
				bool flag = false;
				Bej3ScrollWidget bej3ScrollWidget = null;
				if (this.mClientLastDown != null)
				{
					bej3ScrollWidget = this.mClientLastDown as Bej3ScrollWidget;
				}
				if (bej3ScrollWidget != null)
				{
					flag = bej3ScrollWidget.mLocked;
				}
				FPoint fpoint = new FPoint((float)touch.location.mX, (float)touch.location.mY) - this.mScrollTouchReference;
				fpoint.mY += (float)this.mScrollDownOffset;
				if (flag)
				{
					fpoint.mX = (fpoint.mY = 0f);
				}
				if (this.mClient != null)
				{
					if (!this.mScrollTracking && (this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(fpoint.mX) > (float)ConstantsWP.BEJ3SCROLLWIDGET_TOLERANCE)
					{
						this.mScrollTouchReference.mX = (float)touch.location.mX;
						this.mScrollTracking = true;
						this.mLocked = true;
					}
					if (!this.mScrollTracking && (this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(fpoint.mY) > (float)ConstantsWP.BEJ3SCROLLWIDGET_TOLERANCE)
					{
						this.mScrollTouchReference.mY = (float)touch.location.mY;
						this.mScrollTracking = true;
						this.mLocked = true;
					}
					if (this.mScrollTracking && this.mClientLastDown != null)
					{
						this.mClientLastDown.TouchesCanceled();
						this.mClientLastDown.mIsDown = false;
						this.mClientLastDown = null;
					}
				}
				if (this.mScrollTracking)
				{
					base.TouchMotion(touch);
				}
				else if (this.mClientLastDown != null)
				{
					Point p = this.GetAbsPos() - this.mClientLastDown.GetAbsPos();
					Point impliedObject = new Point(touch.location.mX, touch.location.mY);
					Point point = impliedObject + p;
					Point thePoint = new Point(point.mX + this.mClientLastDown.mX, point.mY + this.mClientLastDown.mY);
					bool flag2 = this.mClientLastDown.GetInsetRect().Contains(thePoint);
					if (flag2 && !this.mClientLastDown.mIsOver)
					{
						this.mClientLastDown.mIsOver = true;
						this.mClientLastDown.MouseEnter();
					}
					else if (!flag2 && this.mClientLastDown.mIsOver)
					{
						this.mClientLastDown.MouseLeave();
						this.mClientLastDown.mIsOver = false;
					}
					touch.location.mX = touch.location.mX + p.mX;
					touch.location.mY = touch.location.mY + p.mY;
					touch.previousLocation.mX = touch.previousLocation.mX + p.mX;
					touch.location.mY = touch.location.mY + p.mY;
					this.mClientLastDown.TouchMoved(touch);
				}
				this.MarkDirty();
			}
		}

		public override void TouchEnded(SexyAppBase.Touch touch)
		{
			this.mLocked = false;
			this.mTouched = false;
			if (this.mScrollTracking)
			{
				base.TouchMotion(touch);
				this.mScrollTracking = false;
				if (this.mPagingEnabled)
				{
					this.SnapToCurrentPage();
				}
			}
			else if (this.mClientLastDown != null)
			{
				Point p = this.GetAbsPos() - this.mClientLastDown.GetAbsPos();
				Point impliedObject = new Point(touch.location.mX, touch.location.mY);
				impliedObject + p;
				touch.location.mX = touch.location.mX + p.mX;
				touch.location.mY = touch.location.mY + p.mY;
				touch.previousLocation.mX = touch.previousLocation.mX + p.mX;
				touch.location.mY = touch.location.mY + p.mY;
				this.mClientLastDown.TouchEnded(touch);
				this.mClientLastDown.mIsDown = false;
				this.mClientLastDown = null;
			}
			this.MarkDirty();
		}

		public override void TouchesCanceled()
		{
			this.mTouched = false;
			this.mIsDown = false;
			base.TouchesCanceled();
			this.SnapToCurrentPage();
		}

		public void SnapToCurrentPage()
		{
			base.SnapToPage();
			((Bej3ScrollWidgetListener)this.mListener).PageChanged(this, base.GetPageHorizontal(), base.GetPageVertical());
		}

		public override void Update()
		{
			base.SetPage(base.GetPageHorizontal(), base.GetPageVertical(), true);
			base.Update();
		}

		public int GetPageCount()
		{
			return this.mPageCountHorizontal;
		}

		public int GetCurrentPage()
		{
			return this.mCurrentPageVertical;
		}

		public void SetPageHorizontalAfterInterrupt(int page)
		{
			this.mTouched = true;
			base.SetPageHorizontal(page, true);
		}

		public Widget GetDirectChildAt(SexyAppBase.Touch touch)
		{
			Widget widget = base.GetClientWidgetAt(touch);
			Widget widget2 = Enumerable.First<Widget>(this.mWidgets);
			while (widget.mParent != null && widget.mParent != widget2)
			{
				widget = (Widget)widget.mParent;
			}
			return widget;
		}

		public bool IsScrolling()
		{
			return this.mSeekScrollTarget;
		}

		public bool HasReachedScrollTarget()
		{
			return this.mScrollOffset == this.mScrollTarget;
		}

		public FPoint GetScrollVelocity()
		{
			return new FPoint(this.mScrollVelocity);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		private bool mTouched;

		public bool mLocked;

		public bool mAllowScrolling;

		public int mScrollDownOffset;
	}
}
