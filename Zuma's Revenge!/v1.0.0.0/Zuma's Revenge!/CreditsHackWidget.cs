using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class CreditsHackWidget : Widget, ButtonListener
	{
		public CreditsHackWidget()
		{
			Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT);
			Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);
			this.mPriority = 2147483646;
			this.mZOrder = 2147483646;
			this.mHasAlpha = (this.mHasTransparencies = true);
			this.mClip = false;
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public virtual void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public virtual void ButtonPress(int theId)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
			}
		}

		public virtual void ButtonDepress(int theId)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.ReturnFromCredits();
			}
		}

		public override void Update()
		{
			if (GameApp.gApp.mCredits != null && GameApp.gApp.mHasFocus)
			{
				this.MarkDirty();
				GameApp.gApp.mCredits.Update();
			}
		}

		public override void Draw(Graphics g)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.mCredits.Draw(g);
			}
		}

		public override void MouseUp(int x, int y)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.mCredits.mSpeedUp = false;
				if (GameApp.gApp.mCredits.AtEnd() && GameApp.gApp.mCredits.mTapDown)
				{
					GameApp.gApp.mCredits.mTapDown = false;
					OptionsDialog optionsDialog = GameApp.gApp.GetDialog(2) as OptionsDialog;
					if (optionsDialog != null)
					{
						optionsDialog.OnCreditsHided();
					}
					GameApp.gApp.ReturnFromCredits();
				}
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.mCredits.mSpeedUp = true;
				if (GameApp.gApp.mCredits.AtEnd())
				{
					GameApp.gApp.mCredits.mTapDown = true;
				}
			}
		}

		public virtual void ButtonDownTick(int x)
		{
		}

		public virtual void ButtonMouseEnter(int x)
		{
		}

		public virtual void ButtonMouseLeave(int x)
		{
		}

		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		public ButtonWidget mContinueBtn;
	}
}
