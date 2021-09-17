using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class BambooColumn
	{
		public BambooColumn()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.IMAGE_BAMBOO_PIECE_A = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_A);
			this.IMAGE_BAMBOO_PIECE_B = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_B);
			this.IMAGE_BAMBOO_PIECE_C = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_C);
			this.IMAGE_BAMBOO_PIECE_D = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_D);
			this.mState = BambooColumn.BambooState.Init;
			float num = (float)GameApp.gApp.GetScreenRect().mHeight / 2f;
			float num2 = (float)(Common.Rand() % Common._DS(400) - Common._DS(200));
			this.mTopEnd.mFinalY = num + num2;
			this.mTopEnd.mY = (float)(-(float)this.IMAGE_BAMBOO_PIECE_C.GetHeight());
			this.mTopEnd.mVelocityY = (this.mTopEnd.mFinalY - this.mTopEnd.mY) / 20f;
			this.mBotEnd.mFinalY = this.mTopEnd.mFinalY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight();
			this.mBotEnd.mY = (float)(GameApp.gApp.GetScreenRect().mHeight + this.IMAGE_BAMBOO_PIECE_D.GetHeight());
			this.mBotEnd.mVelocityY = (this.mBotEnd.mFinalY - this.mBotEnd.mY) / 20f;
			this.mGravity = 0.1f;
			this.mSmoke.Clear();
		}

		public void Draw(Graphics g)
		{
			this.mDrawed = true;
			g.DrawImage(this.IMAGE_BAMBOO_PIECE_C, (int)(this.mX + (float)Common._DS(4)), (int)this.mTopEnd.mY);
			float num = this.mTopEnd.mY;
			bool flag = false;
			while (num >= 0f)
			{
				Image image;
				if (flag)
				{
					image = this.IMAGE_BAMBOO_PIECE_B;
					num -= (float)image.GetHeight();
				}
				else
				{
					image = this.IMAGE_BAMBOO_PIECE_A;
					num -= (float)image.GetHeight();
				}
				g.DrawImage(image, (int)this.mX, (int)num);
				flag = !flag;
			}
			g.DrawImage(this.IMAGE_BAMBOO_PIECE_D, (int)this.mX, (int)this.mBotEnd.mY);
			float num2 = this.mBotEnd.mY;
			flag = false;
			while (num2 <= (float)GameApp.gApp.GetScreenRect().mHeight)
			{
				Image image2;
				if (flag)
				{
					image2 = this.IMAGE_BAMBOO_PIECE_B;
					num2 += (float)image2.GetHeight();
				}
				else
				{
					image2 = this.IMAGE_BAMBOO_PIECE_A;
					num2 += (float)image2.GetHeight();
				}
				g.DrawImage(image2, (int)this.mX, (int)num2);
				flag = !flag;
			}
		}

		public void DrawSmoke(Graphics g)
		{
			if (Enumerable.Count<LTSmokeParticle>(this.mSmoke) > 0)
			{
				for (int i = 0; i < Enumerable.Count<LTSmokeParticle>(this.mSmoke); i++)
				{
					BambooTransition.DrawSmokeParticle(g, this.mSmoke[i]);
				}
			}
		}

		public void Update(bool sound)
		{
			switch (this.mState)
			{
			case BambooColumn.BambooState.Falling:
				this.mTopEnd.mY = this.mTopEnd.mY + this.mTopEnd.mVelocityY;
				this.mBotEnd.mY = this.mBotEnd.mY + this.mBotEnd.mVelocityY;
				if (this.mTopEnd.mY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() >= this.mBotEnd.mY)
				{
					this.mTopEnd.mY = this.mBotEnd.mY - (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() - 1f;
					this.mState = BambooColumn.BambooState.Bouncing;
					if (sound)
					{
						this.PlayBambooSound(0.2f);
					}
				}
				break;
			case BambooColumn.BambooState.Bouncing:
			{
				float num = -(this.mTopEnd.mVelocityY / Common._M(10f) - this.mGravity);
				float num2 = -(this.mBotEnd.mVelocityY / Common._M(10f) + this.mGravity);
				this.mTopEnd.mY = this.mTopEnd.mY + num;
				this.mBotEnd.mY = this.mBotEnd.mY + num2;
				this.mGravity += 0.1f;
				if (this.mTopEnd.mY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() >= this.mBotEnd.mY)
				{
					this.mTopEnd.mY = this.mBotEnd.mY - (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() + (float)Common._DS(7);
					this.mState = BambooColumn.BambooState.Closed;
					if (sound)
					{
						this.PlayBambooSound(0.1f);
					}
				}
				break;
			}
			case BambooColumn.BambooState.Opening:
			{
				this.mTopEnd.mY = this.mTopEnd.mY - this.mTopEnd.mVelocityY;
				this.mBotEnd.mY = this.mBotEnd.mY - this.mBotEnd.mVelocityY;
				bool flag = this.mTopEnd.mY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() < -20f;
				bool flag2 = this.mBotEnd.mY >= (float)(GameApp.gApp.GetScreenRect().mHeight + 20);
				if (flag && flag2 && this.mDrawed)
				{
					this.mState = BambooColumn.BambooState.Open;
				}
				break;
			}
			}
			this.mDrawed = false;
		}

		public void UpdateSmokeParticle()
		{
			if (this.mState != BambooColumn.BambooState.Init && this.mState != BambooColumn.BambooState.Falling)
			{
				for (int i = 0; i < Enumerable.Count<LTSmokeParticle>(this.mSmoke); i++)
				{
					LTSmokeParticle s = this.mSmoke[i];
					if (BambooTransition.UpdateSmokeParticle(s))
					{
						this.mSmoke.RemoveAt(i);
						i--;
					}
				}
			}
		}

		public void SetColumnX(float theX)
		{
			this.mX = theX;
		}

		public void Close()
		{
			if (this.mState == BambooColumn.BambooState.Open)
			{
				this.Reset();
			}
			if (this.mState == BambooColumn.BambooState.Init)
			{
				this.mState = BambooColumn.BambooState.Falling;
			}
		}

		public void Open()
		{
			if (this.mState == BambooColumn.BambooState.Closed)
			{
				this.mState = BambooColumn.BambooState.Opening;
			}
		}

		public bool IsClosed()
		{
			return this.mState == BambooColumn.BambooState.Closed;
		}

		public bool IsOpened()
		{
			return this.mState == BambooColumn.BambooState.Open;
		}

		public float GetColumnX()
		{
			return this.mX;
		}

		public float GetCollisionY()
		{
			return this.mTopEnd.mFinalY;
		}

		public void AddSmokeParticle(LTSmokeParticle s)
		{
			this.mSmoke.Add(s);
		}

		private void PlayBambooSound(float inVolume)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.volume = inVolume;
			GameApp.gApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_BAMBOO_CLOSE), soundAttribs);
		}

		public const int BAMBOO_TRANSITION_FADE_TIME = 100;

		public const int BAMBOO_TRANSITION_PAUSE_TIME = 100;

		public const float BAMBOO_TRANSITION_FALL_TIME = 20f;

		public const float BAMBOO_BOUNCE_GRAVITY = 0.1f;

		public const int BAMBOO_CLOSE_UPDATE_WAIT_COUNT = 10;

		public const float BAMBOO_V_DIV = 10f;

		private BambooColumn.BambooEnd mTopEnd = default(BambooColumn.BambooEnd);

		private BambooColumn.BambooEnd mBotEnd = default(BambooColumn.BambooEnd);

		private BambooColumn.BambooState mState;

		private float mX;

		private float mGravity;

		private List<LTSmokeParticle> mSmoke = new List<LTSmokeParticle>();

		private Image IMAGE_BAMBOO_PIECE_A;

		private Image IMAGE_BAMBOO_PIECE_B;

		private Image IMAGE_BAMBOO_PIECE_C;

		private Image IMAGE_BAMBOO_PIECE_D;

		private bool mDrawed;

		private enum BambooState
		{
			Init,
			Falling,
			Bouncing,
			Closed,
			Opening,
			Open
		}

		private struct BambooEnd
		{
			public float mY;

			public float mFinalY;

			public float mVelocityY;
		}
	}
}
