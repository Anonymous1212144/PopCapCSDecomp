using System;
using BejeweledLivePlus.Widget;

namespace BejeweledLivePlus.UI
{
	public class ProfileMenuBase : Bej3Widget
	{
		public ProfileMenuBase(Menu_Type type, bool hasCloseButton, Bej3ButtonType topButtonType)
			: base(type, hasCloseButton, topButtonType)
		{
			this.mLoading = false;
			this.mLoadedProfilePictureId = -1;
		}

		public virtual void SetUpPlayerImage()
		{
			int num = -1;
			if (this.mState == Bej3WidgetState.STATE_OUT)
			{
				return;
			}
			if (this.mPlayerImage == null)
			{
				return;
			}
			this.mLoading = true;
			if (num != 0 || GlobalMembers.gApp.mProfile.UsesPresetProfilePicture())
			{
				int num2;
				if (num >= 0)
				{
					num2 = num;
				}
				else
				{
					num2 = GlobalMembers.gApp.mProfile.GetProfilePictureId();
				}
				this.mLoadedProfilePictureId = num2;
				if (ProfileMenuBase.loadedGroup == num2)
				{
					this.mPlayerImage.SetImage(num2 + 712);
					this.mLoading = false;
					return;
				}
				this.UnloadPlayerImages();
				BejeweledLivePlusApp.LoadContent(string.Format("ProfilePic_{0}", num2), false);
				this.mPlayerImage.SetImage(num2 + 712);
				ProfileMenuBase.loadedGroup = num2;
			}
			this.mLoading = false;
		}

		public virtual void SetUpPlayerImage(int overridePresetId)
		{
			if (this.mState == Bej3WidgetState.STATE_OUT)
			{
				return;
			}
			if (this.mPlayerImage == null)
			{
				return;
			}
			this.mLoading = true;
			if (overridePresetId != 0 || GlobalMembers.gApp.mProfile.UsesPresetProfilePicture())
			{
				int num;
				if (overridePresetId >= 0)
				{
					num = overridePresetId;
				}
				else
				{
					num = GlobalMembers.gApp.mProfile.GetProfilePictureId();
				}
				this.mLoadedProfilePictureId = num;
				if (ProfileMenuBase.loadedGroup == num)
				{
					this.mPlayerImage.SetImage(num + 712);
					this.mLoading = false;
					return;
				}
				this.UnloadPlayerImages();
				BejeweledLivePlusApp.LoadContent(string.Format("ProfilePic_{0}", num), false);
				this.mPlayerImage.SetImage(num + 712);
				ProfileMenuBase.loadedGroup = num;
			}
			this.mLoading = false;
		}

		public virtual void UnloadPlayerImages(int exceptThis)
		{
			for (int i = 0; i < 30; i++)
			{
				if (i != exceptThis && i != GlobalMembers.gApp.mProfile.GetProfilePictureId())
				{
					BejeweledLivePlusApp.UnloadContent(string.Format("ProfilePic_{0}", i));
				}
			}
			ProfileMenuBase.loadedGroup = -1;
		}

		public virtual void UnloadPlayerImages()
		{
			int num = -1;
			for (int i = 0; i < 30; i++)
			{
				if (i != num && i != GlobalMembers.gApp.mProfile.GetProfilePictureId())
				{
					BejeweledLivePlusApp.UnloadContent(string.Format("ProfilePic_{0}", i));
				}
			}
			ProfileMenuBase.loadedGroup = -1;
		}

		public override void Show()
		{
			base.Show();
			this.SetUpPlayerImage();
			base.ResetFadedBack(true);
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_PROFILEMENU && this.mInterfaceState != InterfaceState.INTERFACE_STATE_EDITPROFILEMENU && this.mInterfaceState != InterfaceState.INTERFACE_STATE_STATSMENU)
			{
				this.UnloadPlayerImages(-2);
			}
		}

		private static int loadedGroup;

		protected bool mLoading;

		public ImageWidget mPlayerImage;

		public int mLoadedProfilePictureId;
	}
}
