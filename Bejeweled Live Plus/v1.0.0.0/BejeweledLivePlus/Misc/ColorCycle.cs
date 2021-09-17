using System;
using System.Collections.Generic;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Misc
{
	public class ColorCycle : IDisposable
	{
		public ColorCycle()
		{
			for (int i = 0; i < ColorCycle.gNumCycleColors; i++)
			{
				this.mCycleColors.Add(GlobalMembers.gCycleColors[i]);
			}
			this.mBrightness = 0f;
			this.mLooping = true;
			this.mAlpha = 1f;
			this.Restart();
		}

		public void Dispose()
		{
		}

		public void SetSpeed(float aSpeed)
		{
			this.mSpeed = aSpeed;
		}

		public void Update()
		{
			if (this.mSpeed == 0f)
			{
				return;
			}
			if (this.mCycleColors.Count == 0)
			{
				this.mColor = new Color(0, 0, 0, 0);
				return;
			}
			if (this.mCycleColors.Count == 1)
			{
				this.mColor = this.mCycleColors[0];
				return;
			}
			this.mCyclePos += this.mSpeed * 0.01f;
			if (this.mCyclePos >= 1f && !this.mLooping)
			{
				this.mCyclePos = 1f;
				this.mColor = this.mCycleColors[this.mCycleColors.Count - 1];
				return;
			}
			while (this.mCyclePos >= 1f)
			{
				this.mCyclePos -= 1f;
			}
			float num = this.mCyclePos * (float)this.mCycleColors.Count;
			int num2 = (int)num;
			int num3 = (num2 + 1) % this.mCycleColors.Count;
			if (!this.mLooping && num3 < num2)
			{
				num3 = num2;
			}
			Color[] array = new Color[]
			{
				this.mCycleColors[num2],
				this.mCycleColors[num3]
			};
			float num4 = num - (float)num2;
			this.mColor.mRed = (int)(num4 * (float)array[1].mRed + (1f - num4) * (float)array[0].mRed);
			this.mColor.mGreen = (int)(num4 * (float)array[1].mGreen + (1f - num4) * (float)array[0].mGreen);
			this.mColor.mBlue = (int)(num4 * (float)array[1].mBlue + (1f - num4) * (float)array[0].mBlue);
			this.mColor.mAlpha = (int)(this.mAlpha * (num4 * (float)array[1].mAlpha + (1f - num4) * (float)array[0].mAlpha));
			if (this.mBrightness != 0f)
			{
				int num5 = (int)(this.mBrightness * 255f);
				if (num5 > 0)
				{
					this.mColor.mRed = Math.Min(255, this.mColor.mRed + num5);
					this.mColor.mGreen = Math.Min(255, this.mColor.mGreen + num5);
					this.mColor.mBlue = Math.Min(255, this.mColor.mBlue + num5);
					return;
				}
				this.mColor.mRed = Math.Max(0, this.mColor.mRed + num5);
				this.mColor.mGreen = Math.Max(0, this.mColor.mGreen + num5);
				this.mColor.mBlue = Math.Max(0, this.mColor.mBlue + num5);
			}
		}

		public Color GetColor()
		{
			return this.mColor;
		}

		public void SetPosition(float thePos)
		{
			while (thePos >= 1f)
			{
				thePos -= 1f;
			}
			this.mCyclePos = thePos;
			this.Update();
		}

		public void SetBrightness(float theBrightness)
		{
			this.mBrightness = theBrightness;
		}

		public void Restart()
		{
			this.mCyclePos = 0f;
			this.mSpeed = 1f;
		}

		public void ClearColors()
		{
			this.mCycleColors.Clear();
			this.mCyclePos = 0f;
		}

		public void PushColor(Color theColor)
		{
			this.mCycleColors.Add(theColor);
		}

		public static implicit operator Color(ColorCycle ImpliedObject)
		{
			return new Color(ImpliedObject.mColor);
		}

		public static int gNumCycleColors = GlobalMembers.gCycleColors.Length;

		public Color mColor = default(Color);

		public float mCyclePos;

		public float mSpeed;

		public float mBrightness;

		public float mAlpha;

		public bool mLooping = true;

		public List<Color> mCycleColors = new List<Color>();
	}
}
