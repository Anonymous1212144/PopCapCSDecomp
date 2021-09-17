using System;
using SexyFramework.Drivers;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public class Checkbox : Widget
	{
		public virtual void SetChecked(bool isChecked)
		{
			this.SetChecked(isChecked, true);
		}

		public virtual void SetChecked(bool @checked, bool tellListener)
		{
			this.mChecked = @checked;
			if (tellListener && this.mListener != null)
			{
				this.mListener.CheckboxChecked(this.mId, this.mChecked);
			}
			this.MarkDirty();
		}

		public virtual bool IsChecked()
		{
			return this.mChecked;
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
			base.MouseDown(x, y, theBtnNum, theClickCount);
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			base.MouseUp(x, y, theClickCount);
		}

		public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
			base.MouseUp(x, y, theBtnNum, theClickCount);
			this.mChecked = !this.mChecked;
			if (this.mListener != null)
			{
				this.mListener.CheckboxChecked(this.mId, this.mChecked);
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mCheckedRect.mWidth == 0 && this.mCheckedImage != null && this.mUncheckedImage != null)
			{
				if (this.mChecked)
				{
					g.DrawImage(this.mCheckedImage, 0, 0);
					return;
				}
				g.DrawImage(this.mUncheckedImage, 0, 0);
				return;
			}
			else
			{
				if (this.mCheckedRect.mWidth == 0 || this.mUncheckedImage == null)
				{
					if (this.mUncheckedImage == null && this.mCheckedImage == null)
					{
						g.SetColor(this.mOutlineColor);
						g.FillRect(0, 0, this.mWidth, this.mHeight);
						g.SetColor(this.mBkgColor);
						g.FillRect(1, 1, this.mWidth - 2, this.mHeight - 2);
						if (this.mChecked)
						{
							g.SetColor(this.mCheckColor);
							g.DrawLine(1, 1, this.mWidth - 2, this.mHeight - 2);
							g.DrawLine(this.mWidth - 1, 1, 1, this.mHeight - 2);
						}
					}
					return;
				}
				if (this.mChecked)
				{
					g.DrawImage(this.mUncheckedImage, 0, 0, this.mCheckedRect);
					return;
				}
				g.DrawImage(this.mUncheckedImage, 0, 0, this.mUncheckedRect);
				return;
			}
		}

		public override void GamepadButtonDown(GamepadButton theButton, int player, uint flags)
		{
			if (theButton != GamepadButton.GAMEPAD_BUTTON_A)
			{
				base.GamepadButtonDown(theButton, player, flags);
				return;
			}
			if ((flags & 1U) != 0U)
			{
				return;
			}
			this.OnPressed();
			this.mIsDown = true;
			this.mChecked = !this.mChecked;
			if (this.mListener != null)
			{
				this.mListener.CheckboxChecked(this.mId, this.mChecked);
			}
			this.MarkDirty();
		}

		public override void GamepadButtonUp(GamepadButton theButton, int player, uint flags)
		{
			if (theButton == GamepadButton.GAMEPAD_BUTTON_A)
			{
				return;
			}
			base.GamepadButtonUp(theButton, player, flags);
		}

		public virtual void OnPressed()
		{
		}

		public Checkbox(Image theUncheckedImage, Image theCheckedImage, int theId, CheckboxListener theCheckboxListener)
		{
			this.mUncheckedImage = theUncheckedImage;
			this.mCheckedImage = theCheckedImage;
			this.mId = theId;
			this.mListener = theCheckboxListener;
			this.mChecked = false;
			this.mOutlineColor = new Color(Color.White);
			this.mBkgColor = new Color(new Color(80, 80, 80));
			this.mCheckColor = new Color(new Color(255, 255, 0));
			this.mDoFinger = true;
		}

		protected CheckboxListener mListener;

		public int mId;

		public bool mChecked;

		public Image mUncheckedImage;

		public Image mCheckedImage;

		public Rect mCheckedRect = default(Rect);

		public Rect mUncheckedRect = default(Rect);

		public Color mOutlineColor = default(Color);

		public Color mBkgColor = default(Color);

		public Color mCheckColor = default(Color);
	}
}
