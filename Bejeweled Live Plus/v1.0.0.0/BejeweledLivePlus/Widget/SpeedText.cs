using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class SpeedText : Widget
	{
		public SpeedText(int theCY, int theIndex)
		{
			this.mFading = false;
			this.mClip = false;
			this.mTextX = GlobalMembers.gApp.mWidth;
			this.mTextY = GlobalMembers.S(5);
			this.mState = 0;
			this.mMouseVisible = false;
		}

		public override void Update()
		{
			base.WidgetUpdate();
			if (this.mOldX.Count > 20)
			{
				this.mOldX.RemoveAt(0);
			}
			if (!this.mFading)
			{
				this.mOldX.Add(this.mTextX);
			}
			switch (this.mState)
			{
			case 0:
			{
				int num = GlobalMembers.gApp.mWidth / 2 - this.mImage.mWidth / 2;
				this.mTextX -= GlobalMembers.M(30);
				if (this.mTextX < num)
				{
					this.mTextX = num;
					this.mTimer = GlobalMembers.M(1.5f);
					this.mState++;
				}
				break;
			}
			case 1:
				this.mTimer -= 0.01f;
				if (this.mTimer <= 0f)
				{
					this.mState++;
				}
				break;
			case 2:
			{
				int num2 = -this.mImage.mWidth;
				this.mTextX -= GlobalMembers.S(50);
				if (this.mTextX < num2)
				{
					this.mTextX = num2;
					this.mFading = true;
					if (this.mOldX.Count > 0)
					{
						this.mOldX.RemoveAt(0);
					}
					else
					{
						this.mState++;
						this.mParent.RemoveWidget(this);
						GlobalMembers.gApp.SafeDeleteWidget(this);
					}
				}
				break;
			}
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(new Color(255, 255, 255));
			int[] array = new int[]
			{
				this.mTextY,
				GlobalMembers.gApp.mHeight - this.mTextY - this.mImage.mHeight
			};
			for (int i = 0; i < 2; i++)
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.DrawImage(this.mImage, this.mTextX, array[i]);
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.DrawImage(this.mOutlineImage, this.mTextX, array[i]);
			}
			int num = GlobalMembers.M(200);
			for (int j = 0; j < this.mOldX.Count; j++)
			{
				g.SetColor(new Color(0, 255, 255, num));
				num -= GlobalMembers.M(9);
				if (num <= 0)
				{
					break;
				}
				if (this.mOldX[j] != this.mTextX)
				{
					g.DrawImage(this.mImage, this.mOldX[j], this.mTextY);
					g.DrawImage(this.mImage, this.mOldX[j], GlobalMembers.gApp.mHeight - this.mTextY - this.mImage.mHeight);
				}
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
		}

		public List<int> mOldX;

		private Image mImage;

		private Image mOutlineImage;

		private float mAlpha;

		private float mTimer;

		private int mTextX;

		private int mTextY;

		private int mState;

		private bool mFading;

		public enum STATE
		{
			STATE_ZOOMON,
			STATE_HOLD,
			STATE_ZOOMOFF
		}
	}
}
