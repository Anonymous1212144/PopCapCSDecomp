using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class HelpDialogContainer : Bej3Widget
	{
		public HelpDialogContainer()
			: base(Menu_Type.MENU_HELPMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mDoesSlideInFromBottom = false;
			this.mCanAllowSlide = false;
			Rect windowRect = HelpDialog.GetWindowRect(4);
			this.Resize(0, 0, windowRect.mX + windowRect.mWidth, ConstantsWP.HELPDIALOG_CONTAINER_HEIGHT);
			for (int i = 0; i < 4; i++)
			{
				Rect windowRect2 = HelpDialog.GetWindowRect(i);
				this.mWindowRect[i] = windowRect2;
			}
			this.mWindowCount = 0;
		}

		public void SetNumberOfWindows(int windowCount, bool useLargePadding)
		{
			this.mWindowCount = windowCount;
			Rect windowRect = HelpDialog.GetWindowRect(this.mWindowCount - 1, useLargePadding);
			this.Resize(0, 0, windowRect.mX + windowRect.mWidth + ConstantsWP.HELPDIALOG_CONTAINER_TAB_PADDING, ConstantsWP.HELPDIALOG_CONTAINER_HEIGHT);
		}

		public override void Draw(Graphics g)
		{
			for (int i = 0; i < this.mWindowCount; i++)
			{
				g.DrawImageBox(this.mWindowRect[i], GlobalMembersResourcesWP.IMAGE_DIALOG_BLACK_BOX);
			}
		}

		public override void Show()
		{
			base.Show();
		}

		public override void Hide()
		{
			base.Hide();
		}

		public override void PlayMenuMusic()
		{
		}

		private Rect[] mWindowRect = new Rect[4];

		public int mWindowCount;
	}
}
