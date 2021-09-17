using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class CreditsMenuContainer : Bej3Widget, ScrollWidgetListener
	{
		private void SplitUpString(string input, List<string> roles, List<int> nameCount1)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			do
			{
				num3 = input.IndexOf(",", num2);
				if (num3 != -1)
				{
					string text = input.Substring(num2, num3 - num2);
					roles.Add(text);
					if (text.Length > 0)
					{
						num++;
					}
					num2 = num3 + 1;
				}
				else
				{
					roles.Add(input.Substring(num2, input.Length - num2));
					num++;
				}
			}
			while (num3 != -1);
			nameCount1.Add(num);
		}

		private Color GetColor(Color baseColour, int baseY)
		{
			return baseColour;
		}

		private bool IsInVisibleRange(int absY, Graphics g)
		{
			return absY > 0 && absY < GlobalMembers.gApp.mHeight;
		}

		public CreditsMenuContainer()
			: base(Menu_Type.MENU_CREDITSMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			this.Resize(0, 0, GlobalMembers.gApp.mWidth - ConstantsWP.CREDITSMENU_PADDING_X * 2, ConstantsWP.CREDITSMENU_HEIGHT);
			this.mRoles.Add(GlobalMembers._ID("iOS Team", 3245));
			this.SplitUpString("", this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Production", 3246));
			this.SplitUpString(GlobalMembers._ID("Viktorya Hollings,Jim McDonagh,JP Vaughan", 3265), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Game Design", 3247));
			this.SplitUpString(GlobalMembers._ID("David Bishop,Antubel Moreda", 3266), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Programming", 3248));
			this.SplitUpString(GlobalMembers._ID("Yang Han,Alanna Kelly,Stuart Johnson,Robert Lester,Paolo Maninetti,Paul O'Donnell,PJ O'Halloran,Christian Schinkoethe,Narinder Singh Basran", 3267), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Art", 3249));
			this.SplitUpString(GlobalMembers._ID("Lee Davies,Riana McKeith", 3268), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Writing", 3250));
			this.SplitUpString(GlobalMembers._ID("David Bishop,Borja Guillan,Antubel Moreda", 3269), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Quality Assurance", 3251));
			this.SplitUpString(GlobalMembers._ID("Adam Beck,Didier Canovas,David Cleaveley,Aaron Collum,Colm Gallagher,Stephen Geddes,Kar Hay Ho,Brian Lelas,Carlos Losada,Philip Plunkett,Ildefonso Ranchal,Carl Sidebotham,Viacheslav Zakhariev", 3270), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Localization", 3252));
			this.SplitUpString(GlobalMembers._ID("Antonio Asensio Pérez,Karl Byrne,Mark Coffey,Jean De Merey,Shinobu Koiwa,Anthony Mackey,Silvie McCullough,John Paul Newman,Lorenzo Penati,Jessica Schuster,Jonathon Young", 3271), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Special Thanks", 3253));
			this.SplitUpString(GlobalMembers._ID("Ed Allard,Sharon Archbold,Sameer Baroova,Aoife Brennan,Paul Breslin,Bob Chamberlain,John Coleman,Valeria Colnaghi,Giordano Contestabile,Meriem Djazouli,Meredith Dorrance,Eddie Dowse,Plamen Dragozov,Brian Fiete,Kieran Gleeson,John Halloran,Gillian Hayes,Nicole LeMaster,Linda McGee,Cormac Mulhall,Kate O'Brien,Cathy Orr,Stiw Puljic,Guillaume Richard,David Roberts,Jennifer Staunton,Andrew Stein,John Vechey,David Ward,Ember Wardrop", 3272), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("PC/Mac Team", 3254));
			this.SplitUpString("", this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Production", 3255));
			this.SplitUpString(GlobalMembers._ID("Michael Guillory,Jason Kapalka,Sukhbir Sidhu", 3273), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Game Design", 3256));
			this.SplitUpString(GlobalMembers._ID("Brian Fiete,Jason Kapalka,Josh Langley", 3274), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Programming", 3257));
			this.SplitUpString(GlobalMembers._ID("Jeremy Bilas,Brian Fiete,Chris Hargrove,Josh Langley,Joe Mobley,Matt Scott,Jacob VanWingen", 3275), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Art", 3258));
			this.SplitUpString(GlobalMembers._ID("Jim Abraham,Misael Armendariz,Gene Blakefield,Marcia Broderick,Tysen Henderson,Matt Holmberg,Jordan Kotzebue,Josh Langley,Dereck McCaughan,Bill Olmstead,Rick Schmitz,Rich Werner", 3276), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Audio", 3259));
			this.SplitUpString(GlobalMembers._ID("Alexander Brandon (Funky Rustic),Peter Hajba,Gregory Hinde,Jason Kapalka,Zachary Throne", 3277), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Writing", 3260));
			this.SplitUpString(GlobalMembers._ID("Stephen Notley", 3278), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Quality Assurance", 3261));
			this.SplitUpString(GlobalMembers._ID("Sharon Bruhn,David Chan,Bob Church,David Cole,Bill Dennes,Jon Fleming,Michael Guillory,Abigail Houghton,Ed Miller,Mike Racioppi,DJ Stiner,Chad Zoellner", 3279), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Localization", 3262));
			this.SplitUpString(GlobalMembers._ID("Karl Byrne,Jean De Merey,Anthony Mackey,John Paul Newman,Lorenzo Penati,Antonio Pérez,Jessica Schuster,Jonathon Young", 3280), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Release Management", 3263));
			this.SplitUpString(GlobalMembers._ID("Irene Cheung,Daniel Landeck,Eric Olson,Nick Tomilson", 3281), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Special Thanks", 3264));
			this.SplitUpString(GlobalMembers._ID("Ed Allard,Yvette Camacho,Garth Chouteau,Leigh Daughtridge,Glenn Drover,Cristina Estrada-Eligio,Liz Harris,Amy Hevron,Curtis Kuhn,Nicole LeMaster,Kong Lu,Cathy Orr,Kelley Poston,Ron Powers,David Roberts,Ben Rotholtz,John Vechey,Eve Warmflash,Paula Wong,PopCap Beta Testers", 3282), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("WP7 Team", 4000));
			this.SplitUpString("", this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Executive Producer", 4001));
			this.SplitUpString(GlobalMembers._ID("D. Cicurel", 4002), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Production", 3255));
			this.SplitUpString(GlobalMembers._ID("D. Chen", 4004), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Programming", 3248));
			this.SplitUpString(GlobalMembers._ID("Y. Liu,X. Yin,L. Ran,C. Wang,C. Liu", 4006), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Art", 3258));
			this.SplitUpString(GlobalMembers._ID("M. Xu", 4008), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Quality Assurance", 3261));
			this.SplitUpString(GlobalMembers._ID("Y. Feng", 4010), this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("Development Team", 6000));
			this.SplitUpString("", this.mNames, this.nameCount);
			this.mRoles.Add(GlobalMembers._ID("", 9998));
			this.SplitUpString(GlobalMembers._ID("Thomas Valmorin,Kenneth Holm,George Applegate,EA QA India,EA LT", 6001), this.mNames, this.nameCount);
			this.mExtraMessages.Add(GlobalMembers._ID("Thank you for playing!", 3283));
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			g.SetColor(Color.White);
			int num = ConstantsWP.CREDITSMENU_LOGO_Y + ConstantsWP.CREDITSMENU_SCROLL_START;
			if (-this.mY < GlobalMembers.gApp.mHeight / 2)
			{
				num += ConstantsWP.CREDITSMENU_LOGO_FADE_OFFSET_BOTTOM;
			}
			else
			{
				num += ConstantsWP.CREDITSMENU_LOGO_FADE_OFFSET_TOP;
			}
			if (this.IsInVisibleRange(num + (int)g.mTransY, g))
			{
				Image image_MAIN_MENU_LOGO = GlobalMembersResourcesWP.IMAGE_MAIN_MENU_LOGO;
				int theX = this.mWidth / 2 - (int)((float)image_MAIN_MENU_LOGO.mWidth * 1.12f / 2f);
				int theY = ConstantsWP.CREDITSMENU_LOGO_Y + ConstantsWP.CREDITSMENU_SCROLL_START;
				g.DrawImage(image_MAIN_MENU_LOGO, theX, theY, (int)((float)image_MAIN_MENU_LOGO.mWidth * 1.12f), (int)((float)image_MAIN_MENU_LOGO.mHeight * 1.12f));
			}
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HEADER, 0, Bej3Widget.COLOR_HEADING_GLOW_1);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 1, Bej3Widget.COLOR_SUBHEADING_1_FILL);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 0, Bej3Widget.COLOR_SUBHEADING_1_STROKE);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, 0, Bej3Widget.COLOR_DIALOG_WHITE);
			g.SetFont(GlobalMembersResources.FONT_HEADER);
			for (int i = 0; i < this.mSubheadings.Count; i++)
			{
				int num2 = this.mSubheadingPositions[i].mY + (int)g.mTransY;
				if (num2 > GlobalMembers.gApp.mHeight)
				{
					break;
				}
				if (num2 >= 0)
				{
					g.DrawString(this.mSubheadings[i], this.mSubheadingPositions[i].mX, this.mSubheadingPositions[i].mY);
				}
			}
			g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			for (int j = 0; j < this.mRoles.Count; j++)
			{
				int num3 = this.mRolePositions[j].mY + (int)g.mTransY;
				if (num3 > GlobalMembers.gApp.mHeight)
				{
					break;
				}
				if (num3 >= 0)
				{
					g.DrawString(this.mRoles[j], this.mRolePositions[j].mX, this.mRolePositions[j].mY);
				}
			}
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			for (int k = 0; k < this.mNames.Count; k++)
			{
				int num4 = this.mNamePositions[k].mY + (int)g.mTransY;
				if (num4 > GlobalMembers.gApp.mHeight)
				{
					break;
				}
				if (num4 >= 0)
				{
					g.DrawString(this.mNames[k], this.mNamePositions[k].mX, this.mNamePositions[k].mY);
				}
			}
			g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			for (int l = 0; l < this.mExtraMessages.Count; l++)
			{
				if (this.IsInVisibleRange(this.mExtraMessagePositions[l].mY + (int)g.mTransY, g))
				{
					g.DrawString(this.mExtraMessages[l], this.mExtraMessagePositions[l].mX, this.mExtraMessagePositions[l].mY);
				}
			}
		}

		public override void Show()
		{
			base.Show();
			if (this.done)
			{
				return;
			}
			Graphics graphics = new Graphics();
			graphics.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			int num = ConstantsWP.CREDITSMENU_START + ConstantsWP.CREDITSMENU_SCROLL_START;
			for (int i = 0; i < this.mRoles.Count; i++)
			{
				Point point = new Point(this.mWidth / 2 - graphics.StringWidth(this.mRoles[i]) / 2, num);
				this.mRolePositions.Add(point);
				int num2 = 0;
				if (this.nameCount[i] > 0)
				{
					num2 = ConstantsWP.CREDITSMENU_ROLE_DIST;
				}
				num2 += this.nameCount[i] * ConstantsWP.CREDITSMENU_NAME_HEIGHT;
				num += num2;
			}
			graphics.SetFont(GlobalMembersResources.FONT_DIALOG);
			int num3 = 0;
			int num4 = 0;
			num = this.mRolePositions[num3].mY + ConstantsWP.CREDITSMENU_NAME_HEIGHT;
			for (int j = 0; j < this.mNames.Count; j++)
			{
				int num5 = this.mWidth / 2;
				Point point2 = new Point(num5 - graphics.StringWidth(this.mNames[j]) / 2, num);
				this.mNamePositions.Add(point2);
				num += ConstantsWP.CREDITSMENU_NAME_HEIGHT;
				num4++;
				if (num4 == this.nameCount[num3])
				{
					num4 = 0;
					num3++;
					if (num3 < this.mRolePositions.Count)
					{
						num = this.mRolePositions[num3].mY + ConstantsWP.CREDITSMENU_NAME_HEIGHT;
					}
				}
			}
			graphics.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			num = this.mHeight - this.mExtraMessages.Count * ConstantsWP.CREDITSMENU_EXTRA_MESSAGE_HEIGHT - ConstantsWP.CREDITSMENU_EXTRA_MESSAGE_OFFSET;
			for (int k = 0; k < this.mExtraMessages.Count; k++)
			{
				Point point3 = new Point(this.mWidth / 2 - graphics.StringWidth(this.mExtraMessages[k]) / 2, num + 450);
				this.mExtraMessagePositions.Add(point3);
				num += ConstantsWP.CREDITSMENU_EXTRA_MESSAGE_HEIGHT;
			}
			this.mRolePositions[this.SUBHEADING1] = new Point(this.mRolePositions[this.SUBHEADING1].mX, this.mRolePositions[this.SUBHEADING1].mY + ConstantsWP.CREDITSMENU_SUB_HEADING_DIST);
			this.mRolePositions[this.SUBHEADING2] = new Point(this.mRolePositions[this.SUBHEADING2].mX, this.mRolePositions[this.SUBHEADING2].mY + ConstantsWP.CREDITSMENU_SUB_HEADING_DIST);
			this.mRolePositions[this.SUBHEADING3] = new Point(this.mRolePositions[this.SUBHEADING3].mX, this.mRolePositions[this.SUBHEADING3].mY + ConstantsWP.CREDITSMENU_SUB_HEADING_DIST);
			this.mRolePositions[this.SUBHEADING4] = new Point(this.mRolePositions[this.SUBHEADING4].mX, this.mRolePositions[this.SUBHEADING4].mY + ConstantsWP.CREDITSMENU_SUB_HEADING_DIST);
			this.mSubheadingPositions.Add(this.mRolePositions[this.SUBHEADING1]);
			this.mSubheadingPositions.Add(this.mRolePositions[this.SUBHEADING2]);
			this.mSubheadingPositions.Add(this.mRolePositions[this.SUBHEADING3]);
			this.mSubheadingPositions.Add(this.mRolePositions[this.SUBHEADING4]);
			this.mRolePositions.RemoveAt(this.SUBHEADING1);
			this.mRolePositions.RemoveAt(this.SUBHEADING2 - 1);
			this.mRolePositions.RemoveAt(this.SUBHEADING3 - 2);
			this.mRolePositions.RemoveAt(this.SUBHEADING4 - 3);
			this.mSubheadings.Add(this.mRoles[this.SUBHEADING1]);
			this.mSubheadings.Add(this.mRoles[this.SUBHEADING2]);
			this.mSubheadings.Add(this.mRoles[this.SUBHEADING3]);
			this.mSubheadings.Add(this.mRoles[this.SUBHEADING4]);
			this.mRoles.RemoveAt(this.SUBHEADING1);
			this.mRoles.RemoveAt(this.SUBHEADING2 - 1);
			this.mRoles.RemoveAt(this.SUBHEADING3 - 2);
			this.mRoles.RemoveAt(this.SUBHEADING4 - 3);
			graphics.SetFont(GlobalMembersResources.FONT_HEADER);
			for (int l = 0; l < this.mSubheadingPositions.Count; l++)
			{
				int theX = this.mWidth / 2 - graphics.StringWidth(this.mSubheadings[l]) / 2;
				this.mSubheadingPositions[l] = new Point(theX, this.mSubheadingPositions[l].mY);
			}
			this.done = true;
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public override void PlayMenuMusic()
		{
		}

		public override void ButtonDepress(int theId)
		{
		}

		private List<string> mRoles = new List<string>();

		private List<string> mNames = new List<string>();

		private List<string> mSubheadings = new List<string>();

		private List<string> mExtraMessages = new List<string>();

		private List<Point> mRolePositions = new List<Point>();

		private List<Point> mNamePositions = new List<Point>();

		private List<Point> mSubheadingPositions = new List<Point>();

		private List<int> nameCount = new List<int>();

		private List<Point> mExtraMessagePositions = new List<Point>();

		private readonly int SUBHEADING1;

		private readonly int SUBHEADING2 = 9;

		private readonly int SUBHEADING3 = 20;

		private readonly int SUBHEADING4 = 26;

		private bool done;
	}
}
