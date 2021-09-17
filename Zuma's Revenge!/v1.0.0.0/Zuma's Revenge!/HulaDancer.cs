using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.PIL;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class HulaDancer
	{
		public HulaDancer()
		{
			this.mHasProjectile = false;
			this.mX = 0f;
			this.mY = 0f;
			this.mProjX = 0f;
			this.mProjY = 0f;
			this.mProjVY = 0f;
			this.mFireProjectile = false;
			this.mProjectileDestroyed = false;
			this.mCel = 0;
			this.mUpdateCount = 0;
			this.mSystem = null;
			this.mFadeOut = false;
			this.mFadeAlpha = 255f;
			this.mDeathEffect = null;
		}

		public HulaDancer(HulaDancer rhs)
		{
			this.mHasProjectile = rhs.mHasProjectile;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mProjX = rhs.mProjX;
			this.mProjY = rhs.mProjY;
			this.mProjVY = rhs.mProjVY;
			this.mFireProjectile = rhs.mFireProjectile;
			this.mProjectileDestroyed = rhs.mProjectileDestroyed;
			this.mCel = rhs.mCel;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mSystem = rhs.mSystem;
			this.mFadeOut = rhs.mFadeOut;
			this.mFadeAlpha = rhs.mFadeAlpha;
			this.mDeathEffect = rhs.mDeathEffect;
			this.mRect = new Rect(rhs.mRect);
			this.mImage = rhs.mImage;
		}

		public virtual void Dispose()
		{
			if (this.mSystem != null)
			{
				this.mSystem.Dispose();
				this.mSystem = null;
			}
		}

		public void Setup(bool has_proj, float y, float proj_vy)
		{
			this.mHasProjectile = has_proj;
			if (this.mHasProjectile)
			{
				this.mCel = 3;
			}
			this.mY = y;
			this.mProjVY = proj_vy;
			this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_HULA_GIRL);
			this.mRect = new Rect((int)this.mX, (int)this.mY, Common._SS(this.mImage.GetCelWidth()), Common._SS(this.mImage.GetCelHeight()));
			this.mRect.Inflate(Common._M(-5), Common._M1(-5));
			this.mX = (float)(-(float)this.mImage.GetCelWidth());
			this.mSystem = new System(100, 50);
			this.mSystem.mScale = Common._S(1f);
			this.mSystem.WaitForEmitters(true);
			Emitter emitter = new Emitter();
			emitter.mPreloadFrames = Common._M(200);
			emitter.mCullingRect = new Rect(-100, -100, Common._SS(GameApp.gApp.mWidth) + 200, Common._SS(GameApp.gApp.mHeight) + 200);
			emitter.mEmissionCoordsAreOffsets = true;
			emitter.SetEmitterType(2);
			emitter.AddScaleKeyFrame(0, new EmitterScale
			{
				mLifeScale = Common._M(1f),
				mNumberScale = Common._M(1f),
				mSizeXScale = Common._M(1.27f),
				mSizeYScale = Common._M(0.9f),
				mVelocityScale = Common._M(1f),
				mWeightScale = Common._M(0.48f),
				mZoom = Common._M(1.66f)
			});
			emitter.AddSettingsKeyFrame(0, new EmitterSettings
			{
				mVisibility = Common._M(0.51f),
				mXRadius = (float)Common._M(10),
				mYRadius = (float)Common._M(12)
			});
			ParticleType particleType = new ParticleType();
			particleType.mLockSizeAspect = false;
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_FUZZY_CIRCLE);
			particleType.mColorKeyManager.AddColorKey(0f, new Color(16, 255, 0));
			particleType.mColorKeyManager.AddColorKey(0.5f, new Color(255, 233, 0));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(0, 255, 42));
			particleType.mAdditive = true;
			particleType.mEmitterAttachPct = Common._M(1f);
			ParticleSettings particleSettings = new ParticleSettings();
			particleSettings.mLife = Common._M(10);
			particleSettings.mNumber = Common._M(40);
			particleSettings.mXSize = (particleSettings.mYSize = Common._M(13));
			particleSettings.mVelocity = Common._M(5);
			particleSettings.mWeight = (float)Common._M(0);
			particleSettings.mGlobalVisibility = Common._M(1f);
			particleType.AddSettingsKeyFrame(0, particleSettings);
			ParticleVariance particleVariance = new ParticleVariance();
			particleVariance.mLifeVar = Common._M(5);
			particleVariance.mNumberVar = Common._M(2);
			particleVariance.mSizeXVar = (particleVariance.mSizeYVar = Common._M(3));
			particleVariance.mVelocityVar = Common._M(5);
			particleType.AddVarianceKeyFrame(0, particleVariance);
			LifetimeSettings lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = (lifetimeSettings.mSizeYMult = Common._M(0.2f));
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = (lifetimeSettings.mSizeYMult = Common._M(1.9f));
			particleType.AddSettingAtLifePct(0.75f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = (lifetimeSettings.mSizeYMult = 0f);
			particleType.AddSettingAtLifePct(1f, lifetimeSettings);
			emitter.AddParticleType(particleType);
			this.mSystem.AddEmitter(emitter);
		}

		public void Setup(bool has_proj, float y)
		{
			this.Setup(has_proj, y, 0f);
		}

		public bool CanRemove()
		{
			if (this.mDeathEffect != null)
			{
				return false;
			}
			if (this.mFadeAlpha <= 0f)
			{
				return true;
			}
			if ((!this.mHasProjectile && !this.mFireProjectile) || this.mProjectileDestroyed)
			{
				return this.mX > (float)(Common._SS(GameApp.gApp.mWidth) + 80);
			}
			return this.mX > (float)(Common._SS(GameApp.gApp.mWidth) + 80) && this.mProjY > (float)(Common._SS(GameApp.gApp.mHeight) + Common._M(100));
		}

		public void Update(float vx)
		{
			if (this.mFadeOut)
			{
				this.mFadeAlpha -= Common._M(2f);
			}
			this.mUpdateCount++;
			if (this.mFireProjectile && !this.mProjectileDestroyed && !this.mFadeOut)
			{
				this.mProjY += this.mProjVY;
			}
			if (this.mSystem != null)
			{
				this.mSystem.Update();
				if (this.mFireProjectile)
				{
					this.mSystem.SetPos(this.mProjX + (float)Common._M(10), this.mProjY + (float)Common._M1(10));
				}
				else
				{
					this.mSystem.SetPos(this.mX + (float)Common._M(50), this.mY + (float)Common._M1(10));
				}
			}
			if (this.mUpdateCount % Common._M(5) == 0)
			{
				if (!this.mHasProjectile)
				{
					this.mCel = (this.mCel + 1) % 3;
				}
				else if (++this.mCel >= this.mImage.mNumCols)
				{
					this.mCel = 3;
				}
			}
			if (this.mDeathEffect != null)
			{
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.Translate(Common._S(this.mX + (float)Common._M(0)), Common._S(this.mY + (float)Common._M1(0)));
				this.mDeathEffect.SetTransform(this.mGlobalTranform.GetMatrix());
				this.mDeathEffect.Update();
				if (!this.mDeathEffect.IsActive())
				{
					this.mDeathEffect = null;
					this.mX = 9999999f;
				}
			}
			this.mX += vx;
			this.mRect.mX = (int)this.mX;
		}

		public void Draw(Graphics g)
		{
			if (this.mFadeAlpha <= 0f)
			{
				return;
			}
			if (this.mFadeAlpha != 255f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mFadeAlpha);
			}
			if (g.Is3D())
			{
				if (this.mDeathEffect == null)
				{
					g.DrawImageCel(this.mImage, (int)Common._S(this.mX), (int)Common._S(this.mY), this.mCel);
				}
				else
				{
					this.mDeathEffect.Draw(g);
				}
				if (this.mFireProjectile && !this.mProjectileDestroyed && !GameApp.gApp.GetBoard().IsPaused())
				{
					g.DrawImageF(Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_COCONUT), (float)((int)Common._S(this.mProjX)), (float)((int)Common._S(this.mProjY)));
				}
			}
			else
			{
				if (this.mDeathEffect == null)
				{
					g.DrawImageCel(this.mImage, (int)Common._S(this.mX), (int)Common._S(this.mY), this.mCel);
				}
				else
				{
					this.mDeathEffect.Draw(g);
				}
				if (this.mFireProjectile && !this.mProjectileDestroyed && !GameApp.gApp.GetBoard().IsPaused())
				{
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_COCONUT), (int)Common._S(this.mProjX), (int)Common._S(this.mProjY));
				}
			}
			if (this.mSystem != null && !GameApp.gApp.GetBoard().IsPaused())
			{
				this.mSystem.mAlphaPct = this.mFadeAlpha / 255f;
				this.mSystem.Draw(g);
			}
			g.SetColorizeImages(false);
		}

		public bool Collided(Rect r)
		{
			return r.Intersects(this.mRect) && !this.mFadeOut && this.mDeathEffect == null;
		}

		public bool ProjectileCollided(Rect gun_rect)
		{
			if (!this.mFireProjectile || this.mProjectileDestroyed)
			{
				return false;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_COCONUT);
			bool flag = gun_rect.Intersects(new Rect((int)this.mProjX, (int)this.mProjY, Common._SS(imageByID.mWidth), Common._SS(imageByID.mHeight)));
			if (flag && this.mSystem != null)
			{
				this.mSystem.ForceStopEmitting(true);
			}
			return flag;
		}

		public bool HasFired()
		{
			return this.mFireProjectile;
		}

		public void Fire()
		{
			if (this.mFireProjectile || !this.mHasProjectile || this.mProjectileDestroyed || this.mDeathEffect != null)
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_COCONUT_DROP));
			this.mHasProjectile = false;
			this.mCel -= 3;
			this.mFireProjectile = true;
			this.mProjX = this.mX + (float)Common._M(40);
			this.mProjY = this.mY + (float)Common._M(0);
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mFadeOut);
			sync.SyncFloat(ref this.mFadeAlpha);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mProjX);
			sync.SyncFloat(ref this.mProjY);
			sync.SyncFloat(ref this.mProjVY);
			sync.SyncBoolean(ref this.mFireProjectile);
			sync.SyncBoolean(ref this.mHasProjectile);
			sync.SyncBoolean(ref this.mProjectileDestroyed);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mUpdateCount);
			if (sync.isRead())
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_HULA_GIRL);
				this.mRect = new Rect((int)this.mX, (int)this.mY, this.mImage.GetCelWidth(), this.mImage.GetCelWidth());
				this.mRect.Inflate(Common._M(-5), Common._M1(-5));
			}
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				Common.SerializeParticleSystem(this.mSystem, sync);
				buffer.WriteBoolean(this.mDeathEffect != null);
				if (this.mDeathEffect != null)
				{
					buffer.WriteLong((long)((int)this.mDeathEffect.mMainSpriteInst.mFrameNum));
					return;
				}
			}
			else
			{
				this.mSystem = Common.DeserializeParticleSystem(sync);
				if (buffer.ReadBoolean())
				{
					this.mDeathEffect = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_HULAGIRLDEATH);
					this.mDeathEffect.ResetAnim();
					this.mDeathEffect.Play((int)buffer.ReadLong());
				}
			}
		}

		public void Disable()
		{
			if (this.mDeathEffect == null)
			{
				this.mDeathEffect = Res.GetPopAnimByID(ResID.POPANIM_NONRESIZE_HULAGIRLDEATH);
				this.mDeathEffect.ResetAnim();
				this.mDeathEffect.Play("Main");
				if (!this.mFireProjectile)
				{
					this.mFadeOut = true;
				}
			}
		}

		public float GetX()
		{
			return this.mX;
		}

		public void DestroyBullet()
		{
			this.mProjectileDestroyed = true;
		}

		protected bool mHasProjectile;

		protected bool mFireProjectile;

		protected bool mProjectileDestroyed;

		protected float mX;

		protected float mY;

		protected float mProjX;

		protected float mProjY;

		protected float mProjVY;

		protected float mFadeAlpha;

		protected int mCel;

		protected int mUpdateCount;

		protected Rect mRect = default(Rect);

		protected Image mImage;

		protected System mSystem;

		protected PopAnim mDeathEffect;

		protected Transform mGlobalTranform = new Transform();

		public bool mFadeOut;
	}
}
