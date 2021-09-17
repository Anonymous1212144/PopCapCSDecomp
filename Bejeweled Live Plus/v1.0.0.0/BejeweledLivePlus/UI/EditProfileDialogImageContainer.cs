using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	internal class EditProfileDialogImageContainer : Bej3Widget
	{
		public EditProfileDialogImageContainer(EditProfileDialog parent)
			: base(Menu_Type.MENU_EDITPROFILEMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mSelectedImg = null;
			this.mEditProfileDialog = parent;
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			this.Resize(0, 0, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_WIDTH, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_HEIGHT);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 30; i++)
			{
				this.mImageLibrary[i] = new Bej3Button(100000 + i, parent, Bej3ButtonType.BUTTON_TYPE_PROFILE_PICTURE);
				this.mImageLibrary[i].Resize(num * ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_ITEM_SIZE_X, num2 * ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_ITEM_SIZE_Y, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_ITEM_SIZE_X, ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_ITEM_SIZE_Y);
				this.AddWidget(this.mImageLibrary[i]);
				num++;
				if (num == ConstantsWP.EDITPROFILEMENU_IMAGE_LIBRARY_COLUMNS)
				{
					num2++;
					num = 0;
				}
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			base.DeferOverlay(0);
		}

		public override void DrawOverlay(Graphics g)
		{
			Point absPos = this.GetAbsPos();
			g.Translate(absPos.mX, absPos.mY);
			g.ClearClipRect();
			Image image = this.mEditProfileDialog.mPlayerImage.GetImage();
			if (image != null)
			{
				float num = 1.4f;
				int num2 = (int)((float)this.mSelectedImg.GetCelWidth() * num);
				g.DrawImage(this.mSelectedImg, this.mSelection.mX - num2 / 2, this.mSelection.mY - num2 / 2, num2, num2);
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			for (int i = 0; i < 30; i++)
			{
				this.mImageLibrary[i].mButtonImage = GlobalMembersResourcesWP.GetImageById(742 + i);
			}
		}

		public override void Show()
		{
			base.Show();
			this.mY = 0;
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
		}

		public Bej3Button[] mImageLibrary = new Bej3Button[30];

		public Point mSelection;

		public Image mSelectedImg;

		public EditProfileDialog mEditProfileDialog;
	}
}
