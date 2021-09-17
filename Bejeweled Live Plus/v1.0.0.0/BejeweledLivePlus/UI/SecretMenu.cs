using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class SecretMenu : Widget, ButtonListener
	{
		public SecretMenu()
		{
			GlobalMembers.gApp.LoadConfigs();
			this.InitWidgets();
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, false);
			base.Dispose();
		}

		public void InitWidgets()
		{
			ButtonWidget buttonWidget = new ButtonWidget(1000, this);
			buttonWidget.mLabel = GlobalMembers._ID("Back", 476);
			buttonWidget.SetFont(GlobalMembersResources.FONT_DIALOG);
			buttonWidget.Resize(GlobalMembers.gApp.mScreenBounds.mWidth / 2 - GlobalMembers.S(100), GlobalMembers.S(1100), GlobalMembers.S(200), GlobalMembers.S(45));
			this.AddWidget(buttonWidget);
			for (int i = 0; i < GlobalMembers.gApp.mSecretModeDataParser.mQuestDataVector.Count; i++)
			{
				int num = GlobalMembers.MS(400);
				int theX = GlobalMembers.gApp.mScreenBounds.mWidth / 2 - GlobalMembers.MS(200) + (i % 3 - 1) * num;
				int theY = GlobalMembers.S(400) + i / 3 * GlobalMembers.MS(105);
				buttonWidget = new ButtonWidget(i, this);
				buttonWidget.SetFont(GlobalMembersResources.FONT_DIALOG);
				buttonWidget.mLabel = GlobalMembers.gApp.mSecretModeDataParser.mQuestDataVector[i].mQuestName;
				buttonWidget.Resize(theX, theY, num, GlobalMembers.S(45));
				this.AddWidget(buttonWidget);
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(new Color(64, 64, 64));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			g.SetColor(new Color(32, 32, 32));
			g.FillRect(this.mWidth / 2 - GlobalMembers.MS(660), GlobalMembers.MS(250), GlobalMembers.MS(1320), GlobalMembers.MS(780));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			g.SetColor(Color.White);
			g.WriteString(GlobalMembers._ID("SECRET GAMES", 477), this.mWidth / 2, GlobalMembers.S(350));
		}

		public void ButtonDepress(int theId)
		{
		}

		public override void KeyChar(char theChar)
		{
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
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
	}
}
