using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class MenuBackground : Bej3Widget
	{
		public MenuBackground()
			: base(Menu_Type.MENU_BACKGROUND, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			for (int i = 0; i < 50; i++)
			{
				this.mClouds[i] = new MenuCloud();
			}
			this.mMainMenuAlpha = 1f;
			this.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			for (int j = 0; j < 50; j++)
			{
				float theX = (float)(Common.Rand(ConstantsWP.MAIN_MENU_WIDTH + ConstantsWP.MAIN_MENU_CLOUD_EXTRA_WIDTH * 2) - ConstantsWP.MAIN_MENU_CLOUD_EXTRA_WIDTH);
				float num = (float)(ConstantsWP.MAIN_MENU_CLOUD_Y + Common.Rand(GlobalMembers.gApp.mHeight - ConstantsWP.MAIN_MENU_CLOUD_Y));
				float mScale = (float)(Common.Rand(1000) % 1000) / 1000f;
				this.mClouds[j].mPosition = new FPoint(theX, num);
				this.mClouds[j].mScale = mScale;
				int theAlpha = (int)(255f * (1f - ((float)this.mHeight - num) / (float)(this.mHeight - ConstantsWP.MAIN_MENU_CLOUD_Y)));
				this.mClouds[j].mColour = new Color(255, 255, 255, theAlpha);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Update()
		{
			base.Update();
			if (this.mState == Bej3WidgetState.STATE_IN && this.mInterfaceState != InterfaceState.INTERFACE_STATE_LOADING && GlobalMembersResourcesWP.IMAGE_MAIN_MENU_CLOUDS != null)
			{
				for (int i = 0; i < 50; i++)
				{
					MenuCloud menuCloud = this.mClouds[i];
					menuCloud.mPosition.mX = menuCloud.mPosition.mX + this.mClouds[i].mScale * ConstantsWP.MAIN_MENU_CLOUD_SPEED;
					if (this.mClouds[i].mPosition.mX > (float)(this.mWidth + ConstantsWP.MAIN_MENU_CLOUD_EXTRA_WIDTH * 2))
					{
						this.mClouds[i].mPosition.mX = (float)(-(float)ConstantsWP.MAIN_MENU_CLOUD_EXTRA_WIDTH - GlobalMembersResourcesWP.IMAGE_MAIN_MENU_CLOUDS.GetCelWidth());
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_LOADING)
			{
				g.SetColor(Color.Black);
				g.FillRect(0, 0, this.mWidth, this.mHeight);
				return;
			}
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_HELPMENU && ((HelpDialog)GlobalMembers.gApp.mMenus[8]).mHelpDialogState == HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_INGAME)
			{
				return;
			}
			if (GlobalMembers.gApp.mGameInProgress && GlobalMembers.gApp.mCurrentGameMode != GameMode.MODE_DIAMOND_MINE)
			{
				return;
			}
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_MAINMENU)
			{
				if (this.mMainMenuAlpha > 0f)
				{
					this.mMainMenuAlpha -= ConstantsWP.MENU_BACKGROUND_FADE_OUT_SPEED;
				}
			}
			else if (this.mMainMenuAlpha < 1f)
			{
				this.mMainMenuAlpha += ConstantsWP.MENU_BACKGROUND_FADE_IN_SPEED;
				if (this.mMainMenuAlpha > 1f)
				{
					this.mMainMenuAlpha = 1f;
				}
			}
			g.ClearClipRect();
			g.SetColor(new Color(255, 255, 255, 255));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_MAIN_MENU_BACKGROUND, 0, 0, this.mWidth, this.mHeight);
			if (GlobalMembersResourcesWP.IMAGE_MAIN_MENU_CLOUDS != null)
			{
				for (int i = 0; i < 50; i++)
				{
					g.SetColor(this.mClouds[i].mColour);
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_MAIN_MENU_CLOUDS, (int)this.mClouds[i].mPosition.mX, (int)this.mClouds[i].mPosition.mY);
				}
			}
			if (GlobalMembersResourcesWP.IMAGE_MAIN_MENU_PILLARL != null)
			{
				g.SetColor(Color.White);
			}
		}

		public override void Show()
		{
			base.Show();
			this.mY = 0;
		}

		public override void Hide()
		{
			base.Hide();
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_DIAMOND_MINE)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMENUBACKGROUND_HIDE_CURVE, this.mAlphaCurve);
			}
			this.mTargetPos = (this.mY = 0);
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
		}

		public void Move(int x)
		{
			this.mX += x;
		}

		public override void InterfaceStateChanged(InterfaceState newState)
		{
			this.mNeedTransition = this.mInterfaceState == InterfaceState.INTERFACE_STATE_LOADING;
			base.InterfaceStateChanged(newState);
		}

		public override bool NeedsShowTransition()
		{
			return this.mNeedTransition;
		}

		public override int GetShowCurve()
		{
			if (this.mNeedTransition && this.mInterfaceState != InterfaceState.INTERFACE_STATE_LOADING)
			{
				return 26;
			}
			return base.GetShowCurve();
		}

		public override void PlayMenuMusic()
		{
		}

		public void SetupForWelcome()
		{
			this.mAlphaCurve.SetConstant(1.0);
			GlobalMembers.gApp.mDialogObscurePct = 0.99f;
		}

		private bool mNeedTransition;

		private float mMainMenuAlpha;

		public MenuCloud[] mClouds = new MenuCloud[50];

		private enum NUM_CLOUD
		{
			NUMBER_OF_CLOUDS = 50
		}
	}
}
