using System;
using Sexy;

namespace BejeweledLIVE
{
	public abstract class Dialogoid : Widget, ButtonListener
	{
		public Dialogoid(int id, DialogListener listener, GameApp app)
		{
			this.Reset(id, listener, app);
		}

		protected void Reset(int id, DialogListener listener, GameApp app)
		{
			this.mId = id;
			this.mApp = GlobalStaticVars.gSexyAppBase;
			this.mListener = listener;
			this.mAutoDelete = true;
			this.mResult = -1;
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public int GetId()
		{
			return this.mId;
		}

		public virtual void Present()
		{
			this.mZOrder = 100;
			this.mApp.mWidgetManager.AddWidget(this);
			this.mApp.mWidgetManager.AddBaseModal(this);
			this.mApp.ModalOpen();
		}

		public virtual void Dismiss()
		{
			this.mApp.mWidgetManager.RemoveBaseModal(this);
			this.mApp.mWidgetManager.RemoveWidget(this);
			this.mApp.ModalClose();
			if (this.mAutoDelete)
			{
				this.mApp.SafeDeleteWidget(this);
			}
		}

		public void ButtonPress(int buttonId)
		{
			this.mListener.DialogButtonPress(this.mId, buttonId);
		}

		public void ButtonDepress(int buttonId)
		{
			this.mResult = buttonId;
			this.Dismiss();
		}

		protected PodButton CreateButton(int id, int color, string label)
		{
			return FancySmallButton.GetNewFancySmallButton(id, color, label, this);
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		protected int mId;

		protected SexyAppBase mApp;

		protected DialogListener mListener;

		protected bool mAutoDelete;

		protected int mResult;

		public enum ButtonID
		{
			YES_BUTTON_ID = 1000,
			NO_BUTTON_ID,
			OK_BUTTON_ID,
			CANCEL_BUTTON_ID,
			FIRST_APPLICATION_BUTTON_ID
		}
	}
}
