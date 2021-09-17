using System;
using SexyFramework;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class CreditsMenuScrollWidget : ScrollWidget
	{
		public CreditsMenuScrollWidget(ScrollWidgetListener listener)
			: base(listener)
		{
			this.mTouched = false;
		}

		public new FPoint GetScrollOffset()
		{
			return this.mScrollOffset;
		}

		public float GetVelocity()
		{
			return this.mScrollVelocity.mY;
		}

		public override void Update()
		{
			if (this.mScrollVelocity.mY > ConstantsWP.CREDITSMENU_SPEED && this.mScrollVelocity.mY <= ConstantsWP.CREDITSMENU_SPEED_CHANGE)
			{
				this.mScrollVelocity.mY = this.mScrollVelocity.mY - ConstantsWP.CREDITSMENU_SPEED_CHANGE;
			}
			if (float.IsNaN(this.mScrollVelocity.mY))
			{
				this.mScrollVelocity.mY = ConstantsWP.CREDITSMENU_SPEED;
			}
			if (this.mAnimate)
			{
				base.Update();
				if (this.mScrollOffset.mY < this.mScrollMin.mY)
				{
					this.Restart(true);
				}
			}
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
			if (!this.mTouched)
			{
				base.TouchBegan(touch);
			}
			this.mTouched = true;
		}

		public override void TouchEnded(SexyAppBase.Touch touch)
		{
			this.mTouched = false;
			base.TouchEnded(touch);
		}

		public override void TouchesCanceled()
		{
			this.mTouched = false;
			base.TouchesCanceled();
		}

		public void Restart(bool wasFinished)
		{
			base.ScrollToMin(false);
		}

		public void Restart()
		{
			this.Restart(false);
		}

		private bool mTouched;

		public bool mAnimate;
	}
}
