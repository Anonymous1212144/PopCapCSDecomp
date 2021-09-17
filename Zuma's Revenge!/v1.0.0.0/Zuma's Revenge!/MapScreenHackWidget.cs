using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class MapScreenHackWidget : Widget
	{
		public MapScreenHackWidget()
		{
			this.mClip = false;
			this.mApp = GameApp.gApp;
			this.mDelay = 0;
			this.mToggledAdventureMode = false;
		}

		public override void Update()
		{
			if (this.mApp.mMapScreen != null && this.mApp.mMapScreen.mDirty)
			{
				this.MarkDirty();
			}
			if (this.mDelay == 0)
			{
				this.mApp.mMapScreen.Update();
				if (this.mApp.mMapScreen != null && this.mApp.mMapScreen.mRemove)
				{
					if (this.mApp.mMapScreen.mSelectedZone == -1)
					{
						this.mDelay = Common._M(10);
						return;
					}
					this.mDelay = Common._M(40);
					return;
				}
			}
			else if (this.mApp.mMapScreen != null)
			{
				this.mDelay--;
				if (this.mDelay == 0 && !this.mToggledAdventureMode)
				{
					this.mToggledAdventureMode = true;
					this.mApp.mMapScreen.CleanButtons();
					this.mApp.mForceZoneRestart = this.mApp.mMapScreen.mSelectedZone;
					this.mApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(this.mApp.StartAdventureMode);
					this.mApp.ToggleBambooTransition();
				}
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.mMapScreen == null)
			{
				return;
			}
			this.mApp.mMapScreen.Draw(g);
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			if (g != null)
			{
				g.Get3D();
			}
			base.DrawAll(theFlags, g);
		}

		public override void MouseMove(int x, int y)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseMove(x, y);
		}

		public override void MouseDrag(int x, int y)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseMove(x, y);
		}

		public override void MouseDown(int x, int y, int cc)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseDown(x, y);
		}

		public override void MouseUp(int x, int y)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseUp(x, y);
		}

		public override void MouseLeave()
		{
			this.mApp.mMapScreen.MouseLeave();
		}

		public override void KeyChar(char theChar)
		{
		}

		public override void GotFocus()
		{
			base.GotFocus();
			if (this.mWidgetManager != null && this.mApp.mMapScreen != null && this.mApp.mMapScreen.mContinueBtn != null)
			{
				this.mWidgetManager.SetGamepadSelection(this.mApp.mMapScreen.mContinueBtn, WidgetLinkDir.LINK_DIR_NONE);
			}
		}

		public GameApp mApp;

		public int mDelay;

		public bool mToggledAdventureMode;
	}
}
