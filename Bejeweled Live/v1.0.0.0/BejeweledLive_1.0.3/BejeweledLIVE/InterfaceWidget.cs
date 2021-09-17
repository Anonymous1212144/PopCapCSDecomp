using System;
using Sexy;

namespace BejeweledLIVE
{
	public class InterfaceWidget : InterfaceControl
	{
		public static void LayoutWidgetBelow(Widget widget, ref TRect cursor)
		{
			cursor.mHeight = widget.mHeight;
			widget.Resize(cursor);
			cursor.mY += cursor.mHeight;
			cursor.mHeight = 0;
		}

		public static void LayoutWidgetAbove(Widget widget, ref TRect cursor)
		{
			cursor.mY -= widget.mHeight;
			cursor.mHeight = widget.mHeight;
			widget.Resize(cursor);
			cursor.mHeight = 0;
		}

		public static void LayoutWidgetsBelow(Widget leftWidget, Widget rightWidget, ref TRect cursor)
		{
			cursor.mHeight = Math.Max(leftWidget.Height(), rightWidget.Height());
			TRect theRect = new TRect(cursor);
			theRect.mWidth /= 2;
			leftWidget.Resize(theRect);
			theRect.mX += theRect.mWidth;
			rightWidget.Resize(theRect);
			cursor.mY += cursor.mHeight;
			cursor.mHeight = 0;
		}

		public static void LayoutWidgetsAbove(Widget leftWidget, Widget rightWidget, ref TRect cursor)
		{
			cursor.mHeight = Math.Max(leftWidget.Height(), rightWidget.Height());
			cursor.mY -= cursor.mHeight;
			TRect theRect = new TRect(cursor);
			theRect.mWidth /= 2;
			leftWidget.Resize(theRect);
			theRect.mX += theRect.mWidth;
			rightWidget.Resize(theRect);
			cursor.mHeight = 0;
		}

		public static void LayoutWidgetsBelow(Widget labelWidget, int labelWidth, Widget widget, ref TRect cursor)
		{
			cursor.mHeight = Math.Max(labelWidget.Height(), widget.Height());
			TRect theRect = default(TRect);
			theRect.mHeight = labelWidget.mHeight;
			theRect.mY = cursor.mY + (cursor.mHeight - theRect.mHeight) / 2;
			theRect.mWidth = labelWidth;
			theRect.mX = cursor.mX;
			labelWidget.Resize(theRect);
			theRect.mX = cursor.mX + labelWidth;
			theRect.mHeight = widget.mHeight;
			theRect.mY = cursor.mY + (cursor.mHeight - theRect.mHeight) / 2;
			theRect.mWidth = cursor.mWidth - labelWidth;
			widget.Resize(theRect);
			cursor.mY += cursor.mHeight;
			cursor.mHeight = 0;
		}

		public InterfaceWidget(GameApp app)
		{
			this.mApp = app;
			this.mInterfaceState = 0;
			this.mInterfaceStateParam = 0;
			this.mInterfaceStateLayout = 0;
			this.mTransactionEndTick = 0;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public override void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			base.InterfaceOrientationChanged(toOrientation);
			InterfaceLayouts uiStateLayout = (this.mApp.OrientationIsLandscape(toOrientation) ? InterfaceLayouts.UI_LAYOUT_LANDSCAPE : InterfaceLayouts.UI_LAYOUT_PORTRAIT);
			this.mInterfaceStateLayout = (int)uiStateLayout;
			if (this.mInterfaceStateParam != 0)
			{
				this.SetupForState(this.mInterfaceState, this.mInterfaceStateParam, (int)uiStateLayout);
			}
		}

		public virtual void InitInterfaceState(int uiState, int uiStateParam, int uiStateLayout)
		{
			this.mInterfaceState = uiState;
			this.mInterfaceStateParam = uiStateParam;
			this.mInterfaceStateLayout = uiStateLayout;
		}

		public void InitInterfaceState(InterfaceStates uiState, int uiStateParam, int uiStateLayout)
		{
			this.InitInterfaceState((int)uiState, uiStateParam, uiStateLayout);
		}

		public virtual void InterfaceTransactionBegin(int uiState, int uiStateParam, int uiStateLayout)
		{
			this.SetDisabled(true);
			if (0 == this.mInterfaceStateParam != (0 == uiStateParam))
			{
				if (uiStateParam == 0)
				{
					this.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
				}
				else
				{
					if (this.mParent == null)
					{
						this.mApp.mWidgetManager.AddWidget(this);
					}
					this.SetupForState(uiState, uiStateParam, uiStateLayout);
					this.TransitionInToState(uiState, uiStateParam, uiStateLayout);
				}
			}
			this.mInterfaceState = uiState;
			this.mInterfaceStateParam = uiStateParam;
			this.mInterfaceStateLayout = uiStateLayout;
		}

		public virtual void InterfaceTransactionEnd(int uiState)
		{
			if (this.mInterfaceStateParam == 0)
			{
				if (this.mParent != null)
				{
					this.mParent.RemoveWidget(this);
					return;
				}
			}
			else if (-1 != this.mInterfaceStateParam)
			{
				this.SetDisabled(false);
			}
		}

		public void InterfaceTransactionEnd(InterfaceStates uiState)
		{
			this.InterfaceTransactionEnd((int)uiState);
		}

		public virtual void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			this.SetVisible(true);
		}

		public virtual void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
		}

		public virtual void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
		}

		public virtual void ApplicationWillResignActive()
		{
		}

		public virtual void ApplicationDidBecomeActive()
		{
		}

		public override void SetDisabled(bool disabled)
		{
			base.SetDisabled(disabled);
			foreach (Widget widget in this.mWidgets)
			{
				widget.SetDisabled(disabled);
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mTransactionEndTick == this.mUpdateCnt)
			{
				this.mApp.InterfaceTransactionDown(this);
			}
		}

		public virtual void TouchCheatGesture()
		{
		}

		protected void TransactionTimeSeconds(float seconds)
		{
			this.mTransactionStartTick = this.mUpdateCnt;
			this.mTransactionEndTick = this.mTransactionStartTick + SexyAppFrameworkConstants.ticksForSeconds(seconds);
			this.mApp.InterfaceTransactionUp(this);
		}

		protected int TransactionTick()
		{
			return Math.Max(0, Math.Min(this.mUpdateCnt, this.mTransactionEndTick) - this.mTransactionStartTick);
		}

		protected bool TransactionInProgress()
		{
			return this.mTransactionStartTick <= this.mUpdateCnt && this.mUpdateCnt < this.mTransactionEndTick;
		}

		protected float TransactionProgress()
		{
			int num = this.mTransactionEndTick - this.mTransactionStartTick;
			if (num == 0)
			{
				return 0f;
			}
			return (float)this.TransactionTick() / (float)num;
		}

		protected int TransactionDurationTicks()
		{
			return this.mTransactionEndTick - this.mTransactionStartTick;
		}

		public GameApp mApp;

		protected int mInterfaceState;

		protected int mInterfaceStateParam;

		protected int mInterfaceStateLayout;

		protected int mTransactionStartTick;

		protected int mTransactionEndTick;
	}
}
