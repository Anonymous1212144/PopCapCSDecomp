using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3Checkbox : Checkbox
	{
		public Bej3Checkbox(int theId, CheckboxListener theCheckboxListener, int theUncheckedImage, int theCheckedImage)
			: base(GlobalMembersResourcesWP.GetImageById(theUncheckedImage), GlobalMembersResourcesWP.GetImageById(theCheckedImage), theId, theCheckboxListener)
		{
			this.mGrayOutWhenDisabled = true;
			this.mUncheckImageId = theUncheckedImage;
			this.mCheckedImageId = theCheckedImage;
			this.mGrayedOut = false;
			this.mClippingEnabled = false;
		}

		public Bej3Checkbox(int theId, CheckboxListener theCheckboxListener, int theUncheckedImage)
			: this(theId, theCheckboxListener, theUncheckedImage, 1334)
		{
		}

		public Bej3Checkbox(int theId, CheckboxListener theCheckboxListener)
			: this(theId, theCheckboxListener, 1333, 1334)
		{
		}

		public override void Draw(Graphics g)
		{
			if (!this.mClippingEnabled)
			{
				g.ClearClipRect();
			}
			Bej3Checkbox.checkOffset = new Point((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1334) - GlobalMembersResourcesWP.ImgXOfs(1333)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1334) - GlobalMembersResourcesWP.ImgYOfs(1333)));
			Bej3Checkbox.x = this.mWidth / 2 - this.mUncheckedImage.GetCelWidth() / 2;
			Bej3Checkbox.y = this.mHeight / 2 - this.mUncheckedImage.GetCelHeight() / 2;
			Image theImage;
			if (this.mGrayedOut)
			{
				theImage = GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX_UNSELECTED;
			}
			else
			{
				theImage = this.mUncheckedImage;
			}
			g.DrawImage(theImage, Bej3Checkbox.x, Bej3Checkbox.y);
			if (this.mChecked)
			{
				if (this.mGrayedOut)
				{
					g.SetColorizeImages(true);
					g.SetColor(Bej3WidgetBase.GreyedOutColor);
				}
				g.DrawImage(this.mCheckedImage, Bej3Checkbox.x + Bej3Checkbox.checkOffset.mX, Bej3Checkbox.y + Bej3Checkbox.checkOffset.mY);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			theWidth = ConstantsWP.BEJ3CHECKBOX_SIZE;
			theHeight = ConstantsWP.BEJ3CHECKBOX_SIZE;
			theX -= theWidth / 2;
			theY -= theHeight / 2;
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public void LinkUpAssets()
		{
			this.mUncheckedImage = GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX;
			this.mCheckedImage = GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX_CHECKED;
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			if (this.mGrayOutWhenDisabled)
			{
				this.mGrayedOut = this.mDisabled;
			}
		}

		public void SetClippingEnabled(bool enable)
		{
		}

		public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
			this.mChecked = !this.mChecked;
			if (this.mListener != null)
			{
				this.mListener.CheckboxChecked(this.mId, this.mChecked);
			}
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TICK);
			this.MarkDirty();
		}

		private int mUncheckImageId;

		private int mCheckedImageId;

		public bool mGrayedOut;

		public bool mGrayOutWhenDisabled;

		public bool mClippingEnabled;

		private static int x = 0;

		private static int y = 0;

		private static Point checkOffset = default(Point);
	}
}
