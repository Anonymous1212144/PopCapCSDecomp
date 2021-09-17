using System;
using System.Collections.Generic;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class Menu : Widget, Bej3ButtonListener, ButtonListener
	{
		public Menu()
		{
			this.InitWidgets();
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, false);
			this.mQuestButton.Clear();
			base.Dispose();
		}

		public void InitWidgets()
		{
			this.mClassicButton = new ButtonWidget(1000, this);
			this.mClassicButton.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mClassicButton.mLabel = GlobalMembers._ID("Play Classic", 293);
			this.AddWidget(this.mClassicButton);
			this.mBlitz1Button = new ButtonWidget(1001, this);
			this.mBlitz1Button.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mBlitz1Button.mLabel = GlobalMembers._ID("Time Bonus", 294);
			this.AddWidget(this.mBlitz1Button);
			this.mBlitz2Button = new ButtonWidget(1002, this);
			this.mBlitz2Button.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mBlitz2Button.mLabel = GlobalMembers._ID("Tug of War", 295);
			this.AddWidget(this.mBlitz2Button);
			this.mZenButton = new ButtonWidget(1005, this);
			this.mZenButton.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mZenButton.mLabel = GlobalMembers._ID("Play Zen", 296);
			this.AddWidget(this.mZenButton);
			this.mQuestModeButton = new ButtonWidget(1003, this);
			this.mQuestModeButton.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mQuestModeButton.mLabel = GlobalMembers._ID("Play Quests", 297);
			this.AddWidget(this.mQuestModeButton);
			this.mSecretModeButton = new ButtonWidget(1020, this);
			this.mSecretModeButton.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mSecretModeButton.mLabel = GlobalMembers._ID("Secret Modes", 298);
			this.AddWidget(this.mSecretModeButton);
			this.mQuitButton = new ButtonWidget(1004, this);
			this.mQuitButton.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mQuitButton.mLabel = GlobalMembers._ID("Quit", 299);
			this.AddWidget(this.mQuitButton);
			this.mMainMenuButton = new ButtonWidget(1006, this);
			this.mMainMenuButton.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mMainMenuButton.mLabel = GlobalMembers._ID("Main Menu", 300);
			this.AddWidget(this.mMainMenuButton);
		}

		public override void Resize(Rect theRect)
		{
			base.Resize(theRect);
			this.mClassicButton.Resize(theRect.mWidth / 2 - GlobalMembers.S(650), GlobalMembers.S(40), GlobalMembers.S(250), GlobalMembers.S(100));
			this.mBlitz1Button.Resize(theRect.mWidth / 2 - GlobalMembers.S(375), GlobalMembers.S(40), GlobalMembers.S(250), GlobalMembers.S(50));
			this.mBlitz2Button.Resize(theRect.mWidth / 2 - GlobalMembers.S(375), GlobalMembers.S(90), GlobalMembers.S(250), GlobalMembers.S(50));
			this.mZenButton.Resize(theRect.mWidth / 2 - GlobalMembers.S(100), GlobalMembers.S(40), GlobalMembers.S(200), GlobalMembers.S(100));
			this.mQuestModeButton.Resize(theRect.mWidth / 2 + GlobalMembers.S(125), GlobalMembers.S(40), GlobalMembers.S(250), GlobalMembers.S(100));
			this.mSecretModeButton.Resize(theRect.mWidth / 2 + GlobalMembers.S(400), GlobalMembers.S(40), GlobalMembers.S(250), GlobalMembers.S(100));
			this.mMainMenuButton.Resize(theRect.mWidth / 2 - GlobalMembers.S(250), this.mHeight - GlobalMembers.S(80), GlobalMembers.S(250), GlobalMembers.S(60));
			this.mQuitButton.Resize(theRect.mWidth / 2 - GlobalMembers.S(-50), this.mHeight - GlobalMembers.S(80), GlobalMembers.S(250), GlobalMembers.S(60));
			for (int i = 0; i < this.mQuestButton.Count; i++)
			{
				int theX = theRect.mWidth / 2 - GlobalMembers.S(620) + i % 3 * GlobalMembers.S(420);
				int theY = GlobalMembers.MS(190) + i / 3 * GlobalMembers.S(55);
				this.mQuestButton[i].Resize(theX, theY, GlobalMembers.S(400), GlobalMembers.S(45));
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(new Color(64, 64, 64));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			g.SetColor(new Color(32, 32, 32));
			g.FillRect(this.mWidth / 2 - GlobalMembers.MS(660), GlobalMembers.MS(160), GlobalMembers.MS(1320), GlobalMembers.MS(930));
		}

		public override void KeyChar(char theChar)
		{
			if (theChar != 'r')
			{
				return;
			}
			GlobalMembers.gApp.LoadConfigs();
			base.RemoveAllWidgets(true, false);
			this.mQuestButton.Clear();
			this.InitWidgets();
			this.Resize(GlobalMembers.gApp.mScreenBounds);
		}

		public bool ButtonsEnabled()
		{
			return false;
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDepress(int theId)
		{
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

		public ButtonWidget mClassicButton;

		public ButtonWidget mBlitz1Button;

		public ButtonWidget mBlitz2Button;

		public ButtonWidget mZenButton;

		public ButtonWidget mQuitButton;

		public ButtonWidget mMainMenuButton;

		public ButtonWidget mQuestModeButton;

		public ButtonWidget mSecretModeButton;

		public List<ButtonWidget> mQuestButton = new List<ButtonWidget>();
	}
}
