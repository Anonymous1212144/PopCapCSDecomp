using System;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class AboutInfo : DialogEx, SliderListener
	{
		public AboutInfo()
			: base(null, null, 12, true, "", "", "", 0)
		{
			this.FONT_SHAGLOUNGE28_GREEN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN);
			this.FONT_SHAGEXOTICA68_BASE = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			this.FONT_SHAGLOUNGE28_BROWN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_BROWN);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			this.mOKBtn = null;
			int num = Common._DS(Common._M(304));
			int num2 = Common._DS(Common._M(162));
			Common._DS(85);
			Common._DS(25);
			Common._DS(75);
			int num3 = 840;
			int num4 = 640;
			this.Resize((GameApp.gApp.mWidth - num3) / 2, (GameApp.gApp.GetScreenRect().mHeight - num4) / 2, num3, num4);
			this.mVersionTextY = 530;
			this.mOKBtn = Common.MakeButton(0, this, TextManager.getInstance().getString(483));
			this.mOKBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			int num5 = 10;
			this.mOKBtn.Resize((this.mWidth - num) / 2, this.mHeight - num2 - num5, num, num2);
			this.AddWidget(this.mOKBtn);
			this.mMetricsSharingText = TextManager.getInstance().getString(861);
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mClip = false;
			this.mDrawScale.SetCurve(Common._MP("b+0,2,0.033333,1,####        cY### >P###"));
			this.mCurrentLanguage = Localization.GetCurrentLanguage();
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(false, true);
		}

		public override void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			base.RemoveAllWidgets(doDelete, recursive);
			this.mOKBtn = null;
		}

		public override void Draw(Graphics g)
		{
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight);
			g.SetFont(this.FONT_SHAGEXOTICA68_BASE);
			g.SetColor(new Color(205, 151, 57));
			g.WriteString(TextManager.getInstance().getString(862), 0, 60, this.mWidth, 0);
			g.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			string text = "";
			string theString = TextManager.getInstance().getString(485) + " " + GameApp.gApp.mProductVersion + text;
			g.WriteString(theString, 0, this.mVersionTextY, this.mWidth, 0);
			int num = Common._DS(30);
			int num2 = 0;
			int num3 = 0;
			g.GetWordWrappedHeight(num * 2, this.mMetricsSharingText, -1, ref num2, ref num3);
			Common._DS(50);
			Rect theDest = new Rect(15, 70, 810, 430);
			g.DrawImageBox(theDest, this.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			Rect theRect = new Rect(theDest.mX + num, theDest.mY + num, theDest.mWidth - num * 2, theDest.mHeight - num * 2);
			g.SetColor(Color.White);
			g.WriteWordWrapped(theRect, this.mMetricsSharingText);
			g.SetFont(this.FONT_SHAGLOUNGE28_BROWN);
			g.SetColor(Color.White);
		}

		public override void ButtonPress(int inButtonID)
		{
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			base.ButtonPress(inButtonID);
		}

		public void ProcessHardwareBackButton()
		{
			this.ButtonDepress(3);
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		public override void ButtonDepress(int inButtonID)
		{
			this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mWidgetFlagsMod.mRemoveFlags |= 16;
			GameApp.gApp.HideAbout();
		}

		public override void MouseDrag(int x, int y)
		{
		}

		public void SliderVal(int theId, double theVal)
		{
		}

		public void SliderReleased(int theId, double theVal)
		{
		}

		private DialogButton mOKBtn;

		private int mVersionTextY;

		private string mMetricsSharingText;

		private Font FONT_SHAGLOUNGE28_GREEN;

		private Font FONT_SHAGEXOTICA68_BASE;

		private Font FONT_SHAGLOUNGE28_BROWN;

		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX;

		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK;

		private Localization.LanguageType mCurrentLanguage;
	}
}
