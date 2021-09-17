using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class Skeleton : IDisposable
	{
		public Skeleton()
		{
			this.mAlpha = 0f;
			this.mIncAlpha = true;
			this.mActivated = false;
			this.mEffectDone = false;
			this.mFadeOut = false;
			this.mFadeAlpha = 255f;
			this.mOrbSize = 1f;
			this.mOrbSizeDec = 0f;
			this.mRibCel = 0;
			this.mHeadYOff = 0f;
			this.mHeadVY = 0f;
			this.mHeadBounceCount = 0;
			this.mUpdateCount = 0;
			this.mExplosionCel = 0;
			this.mRings[0] = (this.mRings[1] = null);
		}

		public virtual void Dispose()
		{
			if (this.mRings[0] != null)
			{
				this.mRings[0].Dispose();
			}
			if (this.mRings[1] != null)
			{
				this.mRings[1].Dispose();
			}
		}

		public void Update()
		{
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			this.mUpdateCount++;
			this.mX += this.mVX;
			this.mY += this.mVY;
			if (this.mHasPowerup)
			{
				int num = (int)Common._M(14f);
				if (this.mIncAlpha)
				{
					this.mAlpha += (float)num;
					if (this.mAlpha >= 255f)
					{
						this.mAlpha = 255f;
						this.mIncAlpha = false;
					}
				}
				else
				{
					this.mAlpha -= (float)num;
					if (this.mAlpha <= 0f)
					{
						this.mAlpha = 0f;
						this.mIncAlpha = true;
					}
				}
				if (this.mActivated)
				{
					if (this.mUpdateCount % Common._M(4) == 0)
					{
						this.mRibCel++;
					}
					if (this.mHeadBounceCount < Common._M(5))
					{
						float num2 = (float)Common._M(25);
						float num3 = num2 - (float)Common._M(10);
						if (this.mHeadBounceCount % 2 == 0)
						{
							this.mHeadYOff += this.mHeadVY;
							if (this.mHeadYOff >= num2)
							{
								this.mHeadYOff = num2;
								this.mHeadBounceCount++;
							}
						}
						else
						{
							this.mHeadYOff -= this.mHeadVY;
							if (this.mHeadYOff <= num3)
							{
								this.mHeadYOff = num3;
								this.mHeadBounceCount++;
							}
						}
					}
				}
			}
			else if (this.mActivated)
			{
				if (this.mUpdateCount % Common._M(2) == 0)
				{
					this.mExplosionCel++;
				}
				if (this.mExplosionCel == Common._M(3))
				{
					this.mFadeOut = true;
				}
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_EXPLODE);
				if (this.mExplosionCel >= imageByID.mNumCols * imageByID.mNumRows)
				{
					this.mEffectDone = true;
				}
			}
			if (this.mRings[0] != null)
			{
				if (this.mOrbSize > 0f)
				{
					this.mOrbSize -= this.mOrbSizeDec;
					if (this.mOrbSize < 0f)
					{
						this.mOrbSize = 0f;
					}
				}
				this.mRings[0].Update();
				this.mRings[1].Update();
				if (this.mRings[0].IsDone() && this.mRings[1].IsDone())
				{
					this.mEffectDone = true;
				}
				else if (!this.mRings[0].IsExpanding())
				{
					this.mFadeOut = true;
				}
			}
			if (this.mFadeOut)
			{
				this.mFadeAlpha -= 255f / Common._M(15f);
				if (this.mFadeAlpha < 0f)
				{
					this.mFadeAlpha = 0f;
				}
			}
		}

		public void DoHit()
		{
			float max_radius = Common._M(30f);
			float alpha_fade = 255f / Common._M(20f);
			float size_fade = 1f / Common._M(50f);
			float angle_inc = Common._M(0.2f);
			this.mRings[0] = new OrbPowerRing(0f, max_radius, alpha_fade, size_fade, angle_inc);
			this.mRings[1] = new OrbPowerRing(3.14159f, max_radius, alpha_fade, size_fade, angle_inc);
			this.mOrbSizeDec = 1f / Common._M(50f);
			this.mHeadVY = Common._M(2f);
		}

		public void SetupFade(Graphics g)
		{
			if (this.mFadeOut)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mFadeAlpha);
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mDelay > 0 || (this.mFadeOut && this.mFadeAlpha <= 0f))
			{
				return;
			}
			this.SetupFade(g);
			if (!this.mHasPowerup)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON), (int)Common._S(this.mX), (int)Common._S(this.mY));
			}
			g.SetColorizeImages(false);
			if (this.mHasPowerup)
			{
				this.SetupFade(g);
				if (this.mRibCel < Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_RIBS).mNumCols)
				{
					g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_RIBS), (int)Common._S(this.mX + (float)Common._M(1)), (int)Common._S(this.mY + (float)Common._M1(34)), this.mRibCel);
				}
				Image theImage = (this.mActivated ? Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD_CLOSED) : Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD));
				g.DrawImage(theImage, (int)Common._S(this.mX + (float)Common._M(0)), (int)Common._S(this.mY + (float)Common._M1(0) + this.mHeadYOff));
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_JAW), (int)Common._S(this.mX + (float)Common._M(34)), (int)Common._S(this.mY + (float)Common._M1(82)));
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_GLOWBALL);
				float num = (float)imageByID.GetCelWidth() * this.mOrbSize;
				float num2 = (float)imageByID.GetCelHeight() * this.mOrbSize;
				g.DrawImage(imageByID, (int)(Common._S(this.mX + (float)Common._M(28)) + (float)(imageByID.GetCelWidth() / 2) - num / 2f), (int)(Common._S(this.mY + (float)Common._M1(40)) + (float)(imageByID.GetCelHeight() / 2) - num2 / 2f), (int)num, (int)num2);
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				g.SetColor(255, 255, 255, (int)((this.mFadeAlpha < this.mAlpha) ? this.mFadeAlpha : this.mAlpha));
				g.DrawImage(imageByID, (int)(Common._S(this.mX + (float)Common._M(28)) + (float)(imageByID.GetCelWidth() / 2) - num / 2f), (int)(Common._S(this.mY + (float)Common._M1(40)) + (float)(imageByID.GetCelHeight() / 2) - num2 / 2f), (int)num, (int)num2);
				g.SetColorizeImages(false);
				g.SetDrawMode(0);
				this.SetupFade(g);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_RIBS_SHADOW);
				if (this.mRibCel < imageByID2.mNumCols)
				{
					g.DrawImageCel(imageByID2, (int)Common._S(this.mX + (float)Common._M(1)), (int)Common._S(this.mY + (float)Common._M1(34)), this.mRibCel);
				}
				theImage = (this.mActivated ? Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD_CLOSED) : Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD_SHADOW));
				g.DrawImage(theImage, (int)Common._S(this.mX + (float)Common._M(0)), (int)Common._S(this.mY + (float)Common._M1(0) + this.mHeadYOff));
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_JAW_SHADOW), (int)Common._S(this.mX + (float)Common._M(34)), (int)Common._S(this.mY + (float)Common._M1(82)));
				g.SetColorizeImages(false);
			}
			this.SetupFade(g);
			if (!this.mHasPowerup)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON_NOSHADOW), (int)Common._S(this.mX), (int)Common._S(this.mY));
			}
			g.SetColorizeImages(false);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_EXPLODE);
			if (this.mActivated && !this.mHasPowerup && this.mExplosionCel < imageByID3.mNumCols * imageByID3.mNumRows)
			{
				Rect celRect = imageByID3.GetCelRect(this.mExplosionCel);
				int theWidth = celRect.mWidth * 4;
				int theHeight = celRect.mHeight * 4;
				g.DrawImage(imageByID3, new Rect((int)Common._S(this.mX + (float)Common._M(-50)), (int)Common._S(this.mY + (float)Common._M1(-50)), theWidth, theHeight), celRect);
			}
			if (this.mRings[0] != null)
			{
				Image imageByID4 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON);
				for (int i = 0; i < 2; i++)
				{
					this.mRings[i].Draw(g, Common._S(this.mX) + (float)(imageByID4.GetCelWidth() / 2), Common._S(this.mY) + (float)(imageByID4.GetCelHeight() / 2));
				}
			}
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mHasPowerup);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncLong(ref this.mDelay);
			sync.SyncFloat(ref this.mOrbSize);
			sync.SyncFloat(ref this.mOrbSizeDec);
			sync.SyncFloat(ref this.mFadeAlpha);
			sync.SyncBoolean(ref this.mFadeOut);
			sync.SyncLong(ref this.mRibCel);
			sync.SyncFloat(ref this.mHeadYOff);
			sync.SyncFloat(ref this.mHeadVY);
			sync.SyncLong(ref this.mHeadBounceCount);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mExplosionCel);
			sync.SyncBoolean(ref this.mActivated);
			Buffer buffer = sync.GetBuffer();
			if (sync.isRead())
			{
				if (buffer.ReadBoolean())
				{
					this.mRings[0] = new OrbPowerRing(0f, 0f, 0f, 0f, 0f);
					this.mRings[1] = new OrbPowerRing(0f, 0f, 0f, 0f, 0f);
					for (int i = 0; i < 2; i++)
					{
						this.mRings[i].SyncState(sync);
					}
					return;
				}
			}
			else
			{
				if (this.mRings[0] == null)
				{
					buffer.WriteBoolean(false);
					return;
				}
				buffer.WriteBoolean(true);
				for (int j = 0; j < 2; j++)
				{
					this.mRings[j].SyncState(sync);
				}
			}
		}

		public bool mHasPowerup;

		public float mVX;

		public float mVY;

		public float mX;

		public float mY;

		public int mDelay;

		public float mOrbSize;

		public float mOrbSizeDec;

		public float mAlpha;

		public float mFadeAlpha;

		public bool mIncAlpha;

		public bool mActivated;

		public bool mEffectDone;

		public bool mFadeOut;

		public int mRibCel;

		public float mHeadYOff;

		public float mHeadVY;

		public int mHeadBounceCount;

		public int mUpdateCount;

		public int mExplosionCel;

		public OrbPowerRing[] mRings = new OrbPowerRing[2];
	}
}
