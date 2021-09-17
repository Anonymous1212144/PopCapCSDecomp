using System;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class UnderDialogWidget : Bej3Widget
	{
		public UnderDialogWidget()
			: base(Menu_Type.MENU_FADE_UNDERLAY, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mCanAllowSlide = false;
			this.mMouseVisible = false;
			this.mHasAlpha = true;
			this.mShrunkScreen1 = null;
			this.mShrunkScreen2 = null;
			this.mClip = false;
			this.mUpdateWhenNotVisible = true;
		}

		public override void Dispose()
		{
			if (this.mShrunkScreen1 != null)
			{
				this.mShrunkScreen1.Dispose();
			}
			if (this.mShrunkScreen2 != null)
			{
				this.mShrunkScreen2.Dispose();
			}
			base.Dispose();
		}

		public void CreateImages()
		{
		}

		public void DrawPaused(Graphics g)
		{
		}

		public override void Update()
		{
			base.Update();
			this.mY = (this.mFinalY = 0);
			if (GlobalMembers.gApp.mDialogObscurePct > 0f && GlobalMembers.gSexyAppBase.mHasFocus)
			{
				this.MarkDirty();
			}
		}

		public override void Draw(Graphics g)
		{
			this.mY = (this.mX = 0);
			g.mTransX = (g.mTransY = 0f);
			g.SetColor(UnderDialogWidget.GetFadeColor());
			g.mPushedColorVector.RemoveAt(0);
			GlobalMembers.gApp.mWidgetManager.FlushDeferredOverlayWidgets(int.MaxValue);
			g.SetColorizeImages(true);
			g.FillRect(GlobalMembers.gSexyAppBase.mScreenBounds);
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			base.DrawAll(theFlags, g);
		}

		public override void PlayMenuMusic()
		{
		}

		public override void Show()
		{
			base.SetVisible(true);
		}

		public override void Hide()
		{
			base.SetVisible(false);
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			base.SetVisible(true);
		}

		public static Color GetFadeColor()
		{
			return new Color(0, 0, 0, (int)(GlobalMembers.gApp.mDialogObscurePct * 160f));
		}

		public DeviceImage mShrunkScreen1;

		public DeviceImage mShrunkScreen2;

		public enum WIDGET_FADE
		{
			UNDERDIALOG_WIDGET_FADE = 160
		}
	}
}
