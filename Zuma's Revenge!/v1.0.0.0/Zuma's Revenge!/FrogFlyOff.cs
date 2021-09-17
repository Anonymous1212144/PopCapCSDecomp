using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class FrogFlyOff
	{
		public FrogFlyOff()
		{
			this.mPlayThud = false;
			this.mFrogJumpTime = Common._M(80);
		}

		public virtual void Dispose()
		{
		}

		public void JumpOut(Gun frog, int dest_x, int dest_y, int start_x, int start_y, float angle)
		{
			FrogFlyOff.FROG_START_SCALE = Common._M(0.5f);
			this.mTimer = 0;
			this.mJumpOut = true;
			this.mFrog = frog;
			this.mFrogX = (float)((start_x == int.MaxValue) ? this.mFrog.GetCenterX() : start_x);
			this.mFrogY = (float)((start_y == int.MaxValue) ? this.mFrog.GetCenterY() : start_y);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (dest_x == 2147483647)
			{
				dest_x = GlobalMembers.gSexyApp.mWidth - imageByID.mWidth / 2;
			}
			if (dest_y == 2147483647)
			{
				dest_y = -(int)this.mFrogY - imageByID.mHeight / 2;
			}
			dest_x -= (int)this.mFrogX;
			this.mFrogVX = (float)dest_x / (float)this.mFrogJumpTime;
			this.mFrogVY = (float)dest_y / (float)this.mFrogJumpTime;
			this.mScaleDelta = (Common._M(2f) - FrogFlyOff.FROG_START_SCALE) / (float)this.mFrogJumpTime;
			this.mFrogScale = FrogFlyOff.FROG_START_SCALE;
			this.mFrogAngle = (this.mDestFrogAngle = (MathUtils._eq(angle, float.MaxValue) ? this.mFrog.GetAngle() : angle));
			this.mFrogAngleDelta = Common._M(0.15f);
		}

		public void JumpOut(Gun frog, int dest_x, int dest_y, int start_x, int start_y)
		{
			this.JumpOut(frog, dest_x, dest_y, start_x, start_y, float.MaxValue);
		}

		public void JumpOut(Gun frog, int dest_x, int dest_y, int start_x)
		{
			this.JumpOut(frog, dest_x, dest_y, start_x, int.MaxValue);
		}

		public void JumpOut(Gun frog, int dest_x, int dest_y)
		{
			this.JumpOut(frog, dest_x, dest_y, int.MaxValue);
		}

		public void JumpOut(Gun frog, int dest_x)
		{
			this.JumpOut(frog, dest_x, int.MaxValue, int.MaxValue);
		}

		public void JumpOut(Gun frog)
		{
			this.JumpOut(frog, int.MaxValue);
		}

		public void JumpIn(Gun frog, int dest_x, int dest_y, bool continue_from_jump_out, int jump_to_x, int jump_to_y)
		{
			FrogFlyOff.FROG_START_SCALE = Common._M(0.5f);
			if (!continue_from_jump_out)
			{
				this.JumpOut(frog, jump_to_x, jump_to_y);
				this.mFrogX += this.mFrogVX * (float)this.mFrogJumpTime;
				this.mFrogY += this.mFrogVY * (float)this.mFrogJumpTime;
				this.mFrogAngle += this.mFrogAngleDelta * (float)this.mFrogJumpTime;
			}
			this.mTimer = 0;
			this.mFrog = frog;
			this.mJumpOut = false;
			this.mPlayThud = true;
			this.mFrogScale = Common._M(2f);
			this.mScaleDelta *= -1f;
			this.RehupFrogPosition(dest_x, dest_y);
		}

		public void JumpIn(Gun frog, int dest_x, int dest_y, bool continue_from_jump_out, int jump_to_x)
		{
			this.JumpIn(frog, dest_x, dest_y, continue_from_jump_out, jump_to_x, int.MaxValue);
		}

		public void JumpIn(Gun frog, int dest_x, int dest_y, bool continue_from_jump_out)
		{
			this.JumpIn(frog, dest_x, dest_y, continue_from_jump_out, int.MaxValue);
		}

		public void JumpIn(Gun frog, int dest_x, int dest_y)
		{
			this.JumpIn(frog, dest_x, dest_y, true);
		}

		public bool HasCompletedFlyOff()
		{
			return this.mTimer > this.mFrogJumpTime;
		}

		public void RehupFrogPosition(int dest_x, int dest_y)
		{
			this.RehupFrogPosition(dest_x, dest_y, this.mFrog.GetAngle());
		}

		public void RehupFrogPosition(int dest_x, int dest_y, float forced_dest_angle)
		{
			this.mFrogAngleDelta = -(this.mFrogAngle - forced_dest_angle) / (float)this.mFrogJumpTime;
			this.mFrogVX = -(this.mFrogX - (float)dest_x) / (float)this.mFrogJumpTime;
			this.mFrogVY = -(this.mFrogY - (float)dest_y) / (float)this.mFrogJumpTime;
		}

		public void Update()
		{
			if (this.mTimer > this.mFrogJumpTime)
			{
				return;
			}
			this.mTimer++;
			if (this.mJumpOut)
			{
				if (this.mFrogScale < 1f)
				{
					if (this.mTimer == 1)
					{
						GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_FROG_LAUNCH));
					}
					this.mFrogScale += this.mScaleDelta;
					if (this.mFrogScale > 1f)
					{
						this.mFrogScale = 1f;
					}
				}
				this.mFrogAngle += this.mFrogAngleDelta;
				this.mFrogX += this.mFrogVX;
				this.mFrogY += this.mFrogVY;
				return;
			}
			this.mFrogAngle += this.mFrogAngleDelta;
			this.mFrogX += this.mFrogVX;
			this.mFrogY += this.mFrogVY;
			if (this.mTimer >= this.mFrogJumpTime)
			{
				this.mFrogAngle = this.mDestFrogAngle;
			}
			if (this.mFrogScale > FrogFlyOff.FROG_START_SCALE)
			{
				this.mFrogScale += this.mScaleDelta;
				this.PlayFrogLandingSound();
				if (this.mFrogScale < FrogFlyOff.FROG_START_SCALE)
				{
					this.mFrogScale = FrogFlyOff.FROG_START_SCALE;
				}
			}
		}

		public void Draw(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (this.mFrogY + (float)(imageByID.mHeight / 2) >= 0f)
			{
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_FROG_SHADOW);
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.RotateRad(this.mFrogAngle);
				float num = (float)this.mTimer / (float)this.mFrogJumpTime;
				if (num > 1f)
				{
					num = 1f;
				}
				float num2 = Common._M(1f);
				float num3 = Common._M(3f);
				float num4 = Common._M(1f);
				float num5 = Common._M(0f);
				float num6 = Common._M(0f);
				float num7 = Common._M(150f);
				float num8;
				float num9;
				float num10;
				if (this.mJumpOut)
				{
					num8 = num2 + (num3 - num2) * num;
					num9 = num4 + (num5 - num4) * num;
					num10 = num6 + (num7 - num6) * num;
				}
				else
				{
					num8 = num3 - (num3 - num2) * num;
					num9 = num5 - (num5 - num4) * num;
					num10 = num7 - (num7 - num6) * num;
				}
				this.mGlobalTranform.Scale(num8, num8);
				g.SetColorizeImages(true);
				g.SetColor(0, 0, 0, (int)(num9 * 255f));
				g.DrawImageTransform(imageByID2, this.mGlobalTranform, imageByID2.GetCelRect(0), Common._S(this.mFrogX - num10), Common._S(this.mFrogY + num10));
				g.SetColorizeImages(false);
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.RotateRad(this.mFrogAngle);
				this.mGlobalTranform.Scale(this.mFrogScale, this.mFrogScale);
				g.DrawImageTransform(imageByID, this.mGlobalTranform, Common._S(this.mFrogX), Common._S(this.mFrogY));
			}
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mFrogScale);
			sync.SyncFloat(ref this.mFrogX);
			sync.SyncFloat(ref this.mFrogY);
			sync.SyncFloat(ref this.mFrogAngle);
			sync.SyncFloat(ref this.mFrogAngleDelta);
			sync.SyncFloat(ref this.mFrogVX);
			sync.SyncFloat(ref this.mFrogVY);
			sync.SyncFloat(ref this.mScaleDelta);
			sync.SyncFloat(ref this.mDestFrogAngle);
			sync.SyncLong(ref this.mFrogJumpTime);
			sync.SyncLong(ref this.mTimer);
			sync.SyncBoolean(ref this.mJumpOut);
			if (sync.isRead())
			{
				this.mFrog = GameApp.gApp.mBoard.mFrog;
			}
		}

		private void PlayFrogLandingSound()
		{
			if (!this.mPlayThud || this.mFrogScale > FrogFlyOff.FROG_START_SCALE - this.mScaleDelta * 15f)
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_FROG_FALL));
			this.mPlayThud = false;
		}

		public float mFrogScale;

		public float mFrogX;

		public float mFrogY;

		public float mFrogAngle;

		public float mFrogAngleDelta;

		public float mFrogVX;

		public float mFrogVY;

		public float mScaleDelta;

		public float mDestFrogAngle;

		public int mFrogJumpTime;

		public int mTimer;

		public bool mJumpOut;

		public bool mPlayThud;

		public Gun mFrog;

		protected Transform mGlobalTranform = new Transform();

		private static float FROG_START_SCALE = 0.26f;
	}
}
