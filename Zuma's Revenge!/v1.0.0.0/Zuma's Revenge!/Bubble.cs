using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class Bubble
	{
		public void Init(float vx, float vy, float jiggle_speed, int jiggle_timer)
		{
			this.mVX = vx;
			this.mVY = vy;
			this.mJiggleSpeed = jiggle_speed;
			this.mJiggleTimer = jiggle_timer;
			this.mDefJiggleTimer = jiggle_timer;
		}

		public void Update()
		{
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			this.mX += this.mVX;
			this.mY += this.mVY;
			if (this.mJiggleLeft)
			{
				this.mX -= this.mJiggleSpeed;
			}
			else
			{
				this.mX += this.mJiggleSpeed;
			}
			if (--this.mJiggleTimer <= 0)
			{
				this.mJiggleLeft = !this.mJiggleLeft;
				this.mJiggleTimer = this.mDefJiggleTimer;
			}
			this.mAlpha -= this.mAlphaDec;
		}

		public void Draw(Graphics g)
		{
		}

		public void SetX(float x)
		{
			this.mX = x;
		}

		public void SetY(float y)
		{
			this.mY = y;
		}

		public void SetAlphaFade(float f)
		{
			this.mAlphaDec = f;
		}

		public void SetDelay(int d)
		{
			this.mDelay = d;
		}

		public float GetAlpha()
		{
			return this.mAlpha;
		}

		protected float mX;

		protected float mY;

		protected float mVX;

		protected float mVY;

		protected float mJiggleSpeed;

		protected bool mJiggleLeft;

		protected int mJiggleTimer;

		protected int mDefJiggleTimer;

		protected int mDelay;

		protected float mAlpha;

		protected float mAlphaDec;
	}
}
