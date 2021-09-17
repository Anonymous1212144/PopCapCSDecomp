using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class ProfileMenuButton : Bej3WidgetBase
	{
		protected void MakeChildrenTouchInvisible()
		{
		}

		public ProfileMenuButton(int theId, Bej3ButtonListener theListener, string heading)
		{
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, heading);
			this.AddWidget(this.mHeadingLabel);
		}

		public override void Draw(Graphics g)
		{
			g.DrawImageBox(new Rect(-GlobalMembers.gApp.mWidth, 0, GlobalMembers.gApp.mWidth * 3, GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.mHeadingLabel.Resize(this.mWidth / 2, ConstantsWP.PROFILEMENU_BUTTON_HEADING_Y, 0, 0);
		}

		private Label mHeadingLabel;
	}
}
