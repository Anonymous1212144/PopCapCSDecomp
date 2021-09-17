using System;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class LegalInfo : DialogEx, SliderListener
	{
		public LegalInfo()
			: base(null, null, 11, true, "", "", "", 0)
		{
			this.FONT_SHAGLOUNGE28_GREEN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN);
			this.FONT_SHAGEXOTICA68_BASE = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			this.FONT_SHAGLOUNGE28_BROWN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_BROWN);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			this.mEndUserLicenseAgreement = null;
			this.mPrivacyPolicy = null;
			this.mTermsOfService = null;
			this.mAboutBtn = null;
			this.mHelpBtn = null;
			this.mOKBtn = null;
			this.mExternalLinkDialog = null;
			int num = Common._DS(Common._M(304));
			int num2 = Common._DS(Common._M(162));
			int num3 = Common._DS(85);
			int num4 = Common._DS(25);
			int num5 = Common._DS(75);
			string @string = TextManager.getInstance().getString(1);
			string string2 = TextManager.getInstance().getString(862);
			string string3 = TextManager.getInstance().getString(480);
			int num6 = this.FONT_SHAGLOUNGE28_GREEN.StringWidth(string3);
			string string4 = TextManager.getInstance().getString(481);
			int num7 = this.FONT_SHAGLOUNGE28_GREEN.StringWidth(string4);
			string string5 = TextManager.getInstance().getString(482);
			int num8 = this.FONT_SHAGLOUNGE28_GREEN.StringWidth(string5);
			int num9 = Math.Max(num6, Math.Max(num7, num8));
			this.mAboutBtn = Common.MakeButton(4, this, string2);
			this.mAboutBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mAboutBtn.Resize(num5, Common._DS(Common._M(30)) + num4, num9 + num3, num2);
			this.AddWidget(this.mAboutBtn);
			this.mEndUserLicenseAgreement = Common.MakeButton(0, this, string3);
			this.mEndUserLicenseAgreement.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mEndUserLicenseAgreement.Resize(num5, this.mAboutBtn.mY + this.mAboutBtn.mHeight + num4, num9 + num3, num2);
			int num10 = this.mEndUserLicenseAgreement.mWidth;
			this.AddWidget(this.mEndUserLicenseAgreement);
			this.mPrivacyPolicy = Common.MakeButton(1, this, string4);
			this.mPrivacyPolicy.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mPrivacyPolicy.Resize(num5, this.mEndUserLicenseAgreement.mY + this.mEndUserLicenseAgreement.mHeight + num4, num9 + num3, num2);
			num10 = ((num10 < this.mPrivacyPolicy.mWidth) ? this.mPrivacyPolicy.mWidth : num10);
			this.AddWidget(this.mPrivacyPolicy);
			this.mTermsOfService = Common.MakeButton(2, this, string5);
			this.mTermsOfService.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mTermsOfService.Resize(num5, this.mPrivacyPolicy.mY + this.mPrivacyPolicy.mHeight + num4, num9 + num3, num2);
			num10 = ((num10 < this.mTermsOfService.mWidth) ? this.mTermsOfService.mWidth : num10);
			this.AddWidget(this.mTermsOfService);
			this.mHelpBtn = Common.MakeButton(5, this, @string);
			this.mHelpBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mHelpBtn.Resize(num5, this.mTermsOfService.mY + this.mTermsOfService.mHeight + num4, num9 + num3, num2);
			this.AddWidget(this.mHelpBtn);
			int num11 = num10 + num5 * 2;
			int num12 = (int)((float)(this.mTermsOfService.mY + num4) + (float)num2 * 3f) + 20;
			this.Resize((GameApp.gApp.mWidth - num11) / 2, (GameApp.gApp.GetScreenRect().mHeight - num12) / 2, num11, num12);
			this.mVersionTextY = this.mHelpBtn.mY + this.mHelpBtn.mHeight + num4 + 29;
			this.mOKBtn = Common.MakeButton(3, this, TextManager.getInstance().getString(483));
			this.mOKBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			int num13 = 10;
			this.mOKBtn.Resize((this.mWidth - num) / 2, this.mHeight - num2 - num13, num, num2);
			this.AddWidget(this.mOKBtn);
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
			this.mEndUserLicenseAgreement = null;
			this.mPrivacyPolicy = null;
			this.mTermsOfService = null;
			this.mOKBtn = null;
			this.mAboutBtn = null;
			this.mHelpBtn = null;
			this.mExternalLinkDialog = null;
		}

		public override void Draw(Graphics g)
		{
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight);
		}

		public override void ButtonPress(int inButtonID)
		{
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			base.ButtonPress(inButtonID);
		}

		public void ProcessHardwareBackButton()
		{
			if (this.mExternalLinkDialog != null)
			{
				this.mExternalLinkDialog.ButtonDepress(1001);
			}
			else if (GameApp.gApp.mGenericHelp != null)
			{
				GameApp.gApp.mGenericHelp.ButtonDepress(1000);
			}
			else
			{
				this.ButtonDepress(3);
			}
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		public override void ButtonDepress(int inButtonID)
		{
			if (this.mExternalLinkDialog != null)
			{
				return;
			}
			string text = "";
			switch (this.mCurrentLanguage)
			{
			default:
				text += "en";
				break;
			case Localization.LanguageType.Language_FR:
				text += "fr";
				break;
			case Localization.LanguageType.Language_IT:
				text += "it";
				break;
			case Localization.LanguageType.Language_GR:
				text += "de";
				break;
			case Localization.LanguageType.Language_SP:
				text += "es";
				break;
			case Localization.LanguageType.Language_CH:
				text += "sc";
				break;
			case Localization.LanguageType.Language_RU:
				text += "ru";
				break;
			case Localization.LanguageType.Language_PL:
				text += "pl";
				break;
			case Localization.LanguageType.Language_PG:
				text += "pt";
				break;
			case Localization.LanguageType.Language_SPC:
				text += "es";
				break;
			case Localization.LanguageType.Language_CHT:
				text += "tc";
				break;
			case Localization.LanguageType.Language_PGB:
				text += "br";
				break;
			}
			if (this.mOKBtn != null && inButtonID == this.mOKBtn.mId)
			{
				this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
				this.mWidgetFlagsMod.mRemoveFlags |= 16;
				GameApp.gApp.HideLegal();
				return;
			}
			if (this.mEndUserLicenseAgreement != null && inButtonID == this.mEndUserLicenseAgreement.mId)
			{
				this.ShowExternalLinkInfo("http://tos.ea.com/legalapp/mobileeula/US/" + text + "/GM");
				return;
			}
			if (this.mTermsOfService != null && inButtonID == this.mTermsOfService.mId)
			{
				this.ShowExternalLinkInfo("http://tos.ea.com/legalapp/WEBTERMS/US/" + text + "/PC");
				return;
			}
			if (this.mPrivacyPolicy != null && inButtonID == this.mPrivacyPolicy.mId)
			{
				this.ShowExternalLinkInfo("http://tos.ea.com/legalapp/WEBPRIVACY/US/" + text + "/PC/");
				return;
			}
			if (this.mAboutBtn != null && inButtonID == this.mAboutBtn.mId)
			{
				GameApp.gApp.ShowAbout();
				return;
			}
			if (this.mHelpBtn != null && inButtonID == this.mHelpBtn.mId)
			{
				GameApp.gApp.mGenericHelp = new GenericHelp();
				GameApp.gApp.AddDialog(GameApp.gApp.mGenericHelp);
			}
		}

		public override void MouseDrag(int x, int y)
		{
		}

		private void ShowExternalLinkInfo(string theURL)
		{
			if (this.mExternalLinkDialog == null)
			{
				this.mExternalLinkDialog = new LegalInfo.ExternalLinkDialog(this, theURL);
				Common.SetupDialog(this.mExternalLinkDialog);
				GameApp.gApp.AddDialog(this.mExternalLinkDialog);
			}
		}

		public void HideExternalLinkInfo()
		{
			this.mExternalLinkDialog.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mExternalLinkDialog.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mExternalLinkDialog = null;
		}

		public void SliderVal(int theId, double theVal)
		{
		}

		public void SliderReleased(int theId, double theVal)
		{
		}

		private DialogButton mEndUserLicenseAgreement;

		private DialogButton mPrivacyPolicy;

		private DialogButton mTermsOfService;

		private DialogButton mAboutBtn;

		private DialogButton mHelpBtn;

		private DialogButton mOKBtn;

		private int mVersionTextY;

		private LegalInfo.ExternalLinkDialog mExternalLinkDialog;

		private Font FONT_SHAGLOUNGE28_GREEN;

		private Font FONT_SHAGEXOTICA68_BASE;

		private Font FONT_SHAGLOUNGE28_BROWN;

		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX;

		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK;

		private Localization.LanguageType mCurrentLanguage;

		private enum LegalButtonIDs
		{
			Legal_EndUserLicenseAgreementID,
			Legal_PrivacyPolicyID,
			Legal_TermsOfServiceID,
			Legal_OKID,
			Legal_AboutID,
			Legal_HelpID,
			Legal_MetricsSharingID
		}

		private class ExternalLinkDialog : ZumaDialog
		{
			public ExternalLinkDialog(LegalInfo theLegalInfo, string theURL)
				: base(13, true, TextManager.getInstance().getString(486), TextManager.getInstance().getString(487), "", 2)
			{
				this.mLegalInfo = theLegalInfo;
				this.mURL = theURL;
			}

			~ExternalLinkDialog()
			{
			}

			public override void Resize(int x, int y, int w, int h)
			{
				base.Resize(x, y, w, h);
				ButtonWidget[] inButtons = new ButtonWidget[] { this.mYesButton, this.mNoButton };
				Common.SizeButtonsToLabel(inButtons, 2, Common._S(20));
			}

			public override void ButtonDepress(int id)
			{
				if (id == 2000 + this.mId || id == 1000)
				{
					GameApp.gApp.OpenURL(this.mURL);
					this.mLegalInfo.HideExternalLinkInfo();
					return;
				}
				if (id == 3000 + this.mId || id == 1001)
				{
					this.mLegalInfo.HideExternalLinkInfo();
				}
			}

			private string mURL;

			private LegalInfo mLegalInfo;
		}
	}
}
