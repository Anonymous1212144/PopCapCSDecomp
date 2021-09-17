using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class BossLame : BossShoot
	{
		protected override void DrawBossSpecificArt(Graphics g)
		{
			int num = (int)(this.mX - (float)(Common._M(183) / 2));
			int num2 = (int)(this.mY - (float)(Common._M(213) / 2));
			bool flag = this.mDeathState == 0 || this.mPoof.mFrameNum < 81f;
			g.PushState();
			if (!MathUtils._eq(this.mAlphaOverride, 255f))
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
			}
			if (this.mDeathState <= 1 && this.mDeathState >= 0)
			{
				Composition composition = this.mDeathComp.GetComposition(flag ? "CRY" : "FLAG WAVE");
				CumulativeTransform cumulativeTransform = new CumulativeTransform();
				cumulativeTransform.mTrans.Translate((float)(Common._S(num) + Common._DS(Common._M(-442))), (float)(Common._S(num2) + Common._DS(Common._M1(-354))));
				int frame = -1;
				if (flag && this.mDeathState != 0)
				{
					frame = 0;
				}
				composition.Draw(g, cumulativeTransform, frame, Common._DS(1f));
			}
			else if (this.mHitTimer > 0)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_TIKI_HIT), Common._S(num), Common._S(num2));
			}
			else if (this.mIsFiring)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_TIKI_THROW), Common._S(num), Common._S(num2));
			}
			else
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_TIKI_IDLE), Common._S(num), Common._S(num2));
				if (this.mBlink)
				{
					g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_EYES), Common._S(num + Common._M(38)), Common._S(num2 + Common._M1(60)), this.mEyeFrame);
				}
			}
			int num3 = (this.mIsFiring ? Common._M(10) : 0);
			if (this.mDeathState == -1 || flag)
			{
				g.DrawImageRotated(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CHICKEN_LEFT), Common._S(num + Common._M(-20)), Common._S(num2 + Common._M1(68) + num3), (double)this.mAngleOff);
				g.DrawImageRotated(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CHICKEN_RIGHT), Common._S(num + Common._M(100)), Common._S(num2 + Common._M1(67) + num3), (double)(-(double)this.mAngleOff));
			}
			if (this.mDeathState == 1 && this.mPoof.mFrameNum < (float)this.mPoof.mLastFrameNum)
			{
				g.PushState();
				this.mPoof.Draw(g);
				g.PopState();
			}
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				g.ClearClipRect();
			}
			if (!this.mLevel.mBoard.IsPaused())
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					if (bossBullet.mState != 0)
					{
						Egg egg = (Egg)bossBullet.mData;
						this.mGlobalTranform.Reset();
						this.mGlobalTranform.RotateRad(egg.mAngle);
						this.mGlobalTranform.Scale(egg.mSize, egg.mSize);
						g.DrawImageTransform(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_EGG), this.mGlobalTranform, Common._S(bossBullet.mX), Common._S(bossBullet.mY));
					}
				}
			}
			for (int j = 0; j < this.mEggFragments.Count; j++)
			{
				EggFragment eggFragment = this.mEggFragments[j];
				if (eggFragment.mAlpha != 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)Math.Min(eggFragment.mAlpha, this.mAlphaOverride));
				}
				g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_LAME_ROCKS), (int)Common._S(eggFragment.mX), (int)Common._S(eggFragment.mY), eggFragment.mCol);
				g.SetColorizeImages(false);
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			for (int k = 0; k < this.mFeathers.Count; k++)
			{
				Feather feather = this.mFeathers[k];
				float num4 = Math.Min(feather.mAlpha, this.mAlphaOverride);
				if (num4 != 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)num4);
				}
				g.DrawImageRotated(feather.mImage, (int)Common._S(feather.mX), (int)Common._S(feather.mY), (double)feather.mAngleOsc.GetVal());
				g.SetColorizeImages(false);
			}
			g.PopState();
		}

		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			this.mHitTimer = Common._M(100);
			this.AddFeathers();
			bool flag = base.DoHit(b, from_prox_bomb);
			if (flag)
			{
				this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CHICKEN_SURRENDER));
				if (Common._S(this.mX) < (float)(GameApp.gApp.mWidth / 2))
				{
					this.mRunRight = true;
				}
				else
				{
					this.mRunRight = false;
				}
				this.mPauseMovement = true;
				this.mDeathTextTimer = Common._M(600);
				this.mApp.GetBoard().mPreventBallAdvancement = true;
				this.mDeathState = 0;
				this.mApp.GetBoard().mContinueNextLevelOnLoadProfile = true;
				this.mCryLoopCount = 0;
			}
			return flag;
		}

		protected override void DidFire()
		{
			base.DidFire();
			this.mIsFiring = true;
		}

		protected override bool CanFire()
		{
			return Common._eq(this.mHP, this.mMaxHP);
		}

		protected override bool CanRetaliate()
		{
			return false;
		}

		protected override bool PreBulletUpdate(BossBullet b, int index)
		{
			Egg egg = (Egg)b.mData;
			if (b.mState <= 2)
			{
				b.mX = this.mX - (float)(Common._M(183) / 2) + (float)Common._M1(72);
				b.mY = this.mY - (float)(Common._M(213) / 2) + (float)Common._M1(110);
			}
			if (b.mState == 0)
			{
				return true;
			}
			if (b.mState == 1)
			{
				egg.mSize += Common._M(0.04f);
				if (egg.mSize >= Common._M(0.75f))
				{
					b.mState++;
				}
				return true;
			}
			if (b.mState == 2)
			{
				egg.mSize -= Common._M(0.03f);
				if (egg.mSize <= Common._M(0.5f))
				{
					egg.mSize = Common._M(0.5f);
					b.mState++;
					if (b.mShotType == 1 || b.mShotType == 3)
					{
						base.FireBulletAtPlayer(b, Common.FloatRange(base.mMinBulletSpeed, base.mMaxBulletSpeed), b.mX, b.mY);
						b.mTargetVX = b.mVX;
						b.mTargetVY = b.mVY;
					}
				}
				return true;
			}
			if (egg.mSize < 1f)
			{
				egg.mSize += Common._M(0.02f);
				if (egg.mSize > 1f)
				{
					egg.mSize = 1f;
				}
			}
			egg.mAngle += Common._M(0.05f);
			return false;
		}

		protected override BossBullet CreateBossBullet()
		{
			BossBullet bossBullet = new BossBullet();
			Egg mData = new Egg();
			bossBullet.mData = mData;
			return bossBullet;
		}

		protected override void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
			if (b.mData != null)
			{
				b.mData = null;
			}
		}

		protected override void BulletHitPlayer(BossBullet b)
		{
			base.BulletHitPlayer(b);
			int num = Common._M(40);
			float num2 = 6.28318f / (float)num;
			for (int i = 0; i < num; i++)
			{
				EggFragment eggFragment = new EggFragment();
				this.mEggFragments.Add(eggFragment);
				eggFragment.mCol = Common.Rand() % Res.GetImageByID(ResID.IMAGE_BOSS_LAME_ROCKS).mNumCols;
				eggFragment.mX = b.mX;
				eggFragment.mY = b.mY;
				eggFragment.mAlpha = 255f;
				float value = (float)i * num2;
				float num3 = Common.FloatRange(Common._M(4f), Common._M1(6f));
				eggFragment.mVX = SexyMath.CosF(value) * num3;
				eggFragment.mVY = -SexyMath.SinF(value) * num3;
				eggFragment.mDecVX = eggFragment.mVX / Common._M(40f);
				eggFragment.mDecVY = eggFragment.mVY / Common._M(40f);
			}
			if (!GameApp.gApp.GetLevelMgr().mBossesCanAttackFuckedFrog)
			{
				for (int j = 0; j < this.mBullets.Count; j++)
				{
					BossBullet bossBullet = this.mBullets[j];
					if (bossBullet.mState == 0)
					{
						bossBullet.mDeleteInstantly = true;
					}
				}
			}
		}

		protected override void DrawHearts(Graphics g)
		{
			if (this.mHP <= 0f || this.mDoDeathExplosions || this.mNumHeartsVisible <= 0 || this.mLevel.mBoard.DoingBossIntro())
			{
				return;
			}
			g.PushState();
			if (!Common._eq(this.mAlphaOverride, 255f))
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
			}
			int num = Boss.NUM_HEARTS - this.mNumHeartsVisible;
			int num2 = Boss.NUM_HEARTS * 2 - num - 1;
			for (int i = 0; i < Boss.NUM_HEARTS * 2; i++)
			{
				if (i >= num && i <= num2)
				{
					int theCel = ((i < Boss.NUM_HEARTS) ? this.mHeartCels[i] : 0);
					if (this.mHP < this.mMaxHP - 1f)
					{
						theCel = 4;
					}
					g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS), (int)(Common._S(this.mX + (float)this.mHeartXOff) + (float)(i * Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS).GetCelWidth())), (int)Common._S(this.mY + (float)this.mHeartYOff), theCel);
				}
			}
			g.PopState();
		}

		protected virtual void AddFeathers()
		{
			this.AddFeathers(false);
		}

		protected virtual void AddFeathers(bool do_more)
		{
			float num = Common.DegreesToRadians((float)Common._M(45));
			float num2 = Common.DegreesToRadians((float)Common._M(165));
			int num3 = Common._M(7);
			if (do_more)
			{
				num3 += Common._M(15);
			}
			float num4 = (num2 - num) / (float)num3;
			float num5 = Common._M(10f);
			int num6 = (int)(this.mX - (float)(Common._M(183) / 2));
			int num7 = (int)(this.mY - (float)(Common._M(213) / 2));
			for (int i = 0; i < num3; i++)
			{
				Feather feather = new Feather();
				this.mFeathers.Add(feather);
				feather.mImgNum = 1 + Common.Rand() % 4;
				feather.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_FEATHER1 + (feather.mImgNum - 1));
				feather.mX = (float)(num6 + ((this.mX < this.mDestX) ? Common._M(90) : Common._M1(30)));
				feather.mY = (float)(num7 + Common._M(100));
				feather.mVX = SexyMath.CosF(num + (float)i * num4) * num5;
				feather.mVY = -SexyMath.SinF(num + (float)i * num4) * num5;
				feather.mDecVX = feather.mVX / Common._M(25f);
				feather.mDecVY = feather.mVY / Common._M(25f);
				feather.mAlpha = 255f;
				feather.mAngleOsc.Init(Common.DegreesToRadians((float)Common._M(-45)), Common.DegreesToRadians((float)Common._M1(45)), Common.Rand() % 2 == 0, Common.FloatRange(Common._M2(0.0001f), Common._M3(0.0003f)));
			}
		}

		protected override bool BulletIntersectsBoss(Bullet b)
		{
			return MathUtils.CirclesIntersect(b.GetX(), b.GetY(), this.mX - (float)Common._DS(Common._M(30)), this.mY + (float)this.mBossRadiusYOff, (float)(this.mBossRadius + b.GetRadius() + Common._DS(Common._M1(0))));
		}

		public BossLame()
			: base(null)
		{
			this.Initialize();
		}

		public BossLame(Level l)
			: base(l)
		{
			this.Initialize();
		}

		private void Initialize()
		{
			this.mAngleOff = 0f;
			this.mAngleDir = Common._M(0.2f);
			this.mIsFiring = false;
			this.mBlink = false;
			this.mBlinkClosed = true;
			this.mEyeFrame = 0;
			this.mHitTimer = 0;
			this.mResGroup = "Boss6_Lame";
			this.mDeathTextTimer = 0;
			this.mBossRadius = Common._M(70);
			this.mNumHeartsVisible = Boss.NUM_HEARTS;
			this.mBulletRadius = Common._M(25);
			this.mDeathStateTimer = 0;
			this.mDeathComp = null;
			this.mPoof = null;
			this.mCryLoopCount = 0;
			this.mDeathState = -1;
			this.mDrawDeathBGTikis = false;
			this.mDrawHeartsBelowBoss = true;
		}

		public override void DrawTopLevel(Graphics g)
		{
			base.DrawTopLevel(g);
			g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
			int num = this.mDeathTextTimer;
			if (num > 255)
			{
				num = 255;
			}
			else if (num <= 0)
			{
				return;
			}
			g.SetColor(0, 0, 0, num);
			string[] array = new string[]
			{
				TextManager.getInstance().getString(384),
				TextManager.getInstance().getString(385),
				TextManager.getInstance().getString(386)
			};
			bool flag = this.mLevel.mBoard.IsHardAdventureMode();
			if (flag)
			{
				array[0] = TextManager.getInstance().getString(387);
				array[1] = TextManager.getInstance().getString(388);
				array[2] = "";
			}
			for (int i = 0; i < array.Length; i++)
			{
				g.WriteString(array[i], -GameApp.gApp.mBoardOffsetX, Common._DS(Common._M(650)) + i * g.GetFont().GetHeight(), 1024);
			}
		}

		public override bool AllowFrogToFire()
		{
			return base.AllowFrogToFire() && this.mDeathTextTimer <= 0;
		}

		public override void Update()
		{
			this.Update(1f);
		}

		public override void Update(float f)
		{
			base.Update(f);
			if (!this.mLevel.AllCurvesAtRolloutPoint() && this.mLevel.mCanDrawBoss && this.mUpdateCount % Common._M(40) == 0 && this.mNumHeartsVisible < Boss.NUM_HEARTS)
			{
				this.mNumHeartsVisible++;
			}
			if (this.mFireDelay <= Common._M(100))
			{
				this.mIsFiring = true;
				this.mAngleOff += this.mAngleDir;
				if ((this.mAngleDir > 0f && this.mAngleOff >= Common._M(0.26f)) || (this.mAngleDir < 0f && this.mAngleOff <= 0f))
				{
					this.mAngleDir *= -1f;
				}
			}
			else
			{
				this.mAngleOff = 0f;
			}
			if (this.mDeathTextTimer > 0)
			{
				this.mDeathTextTimer--;
			}
			if (this.mDeathTextTimer <= 0 && this.mApp.GetBoard().GetGameState() != GameState.GameState_Boss6FakeCredits && this.mDeathState == 1 && ((this.mRunRight && Common._S(this.mX) > (float)(GameApp.gApp.mWidth + Common._DS(Common._M(200)))) || (!this.mRunRight && Common._S(this.mX) < (float)Common._DS(Common._M1(-200)))))
			{
				this.mHP = 0f;
				this.mApp.GetBoard().BossDied();
				return;
			}
			if (this.mDeathState == 0)
			{
				Composition composition = this.mDeathComp.GetComposition("CRY");
				composition.Update();
				if (composition.Done())
				{
					this.mCryLoopCount++;
					composition.mUpdateCount = 1;
				}
				if (this.mCryLoopCount == Common._M(2))
				{
					this.mPoof.ResetAnim();
					this.mDeathState = 1;
				}
			}
			else if (this.mDeathState == 1)
			{
				this.mPoof.mDrawTransform.LoadIdentity();
				float num = GameApp.DownScaleNum(1f);
				this.mPoof.mDrawTransform.Scale(num, num);
				int value = (int)(this.mX - (float)(Common._M(183) / 2));
				int value2 = (int)(this.mY - (float)(Common._M(213) / 2));
				this.mPoof.mDrawTransform.Translate((float)(Common._S(value) + Common._DS(Common._M(0))), (float)(Common._S(value2) + Common._DS(Common._M1(200))));
				this.mPoof.Update();
				Composition composition2 = this.mDeathComp.GetComposition("FLAG WAVE");
				composition2.mLoop = true;
				composition2.mMaxFrame = Common._M(32);
				if (this.mPoof.mFrameNum >= (float)this.mPoof.mLastFrameNum)
				{
					composition2.Update();
				}
				if (this.mDeathStateTimer == Common._M(299))
				{
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CHICKEN_FLEE));
					this.AddFeathers(true);
				}
				if (++this.mDeathStateTimer >= Common._M(300))
				{
					this.mX += (float)(this.mRunRight ? Common._M(16) : Common._M1(-16));
				}
			}
			int num2 = -1;
			bool flag = true;
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				BossBullet bossBullet = this.mBullets[i];
				if (bossBullet.mState == 1 || bossBullet.mState == 2)
				{
					flag = false;
				}
				else if (bossBullet.mState == 0)
				{
					num2 = i;
				}
			}
			if (flag && num2 != -1)
			{
				BossBullet bossBullet2 = this.mBullets[num2];
				bossBullet2.mState++;
			}
			else if (flag && num2 == -1)
			{
				this.mIsFiring = false;
			}
			if (this.mHitTimer > 0)
			{
				this.mHitTimer--;
			}
			if (!this.mIsFiring && !this.mBlink && this.mHitTimer == 0 && Common.Rand() % Common._M(100) == 0)
			{
				this.mBlink = (this.mBlinkClosed = true);
				this.mEyeFrame = 0;
			}
			if (this.mBlink && this.mUpdateCount % Common._M(4) == 0)
			{
				if (this.mBlinkClosed && ++this.mEyeFrame >= Res.GetImageByID(ResID.IMAGE_BOSS_LAME_EYES).mNumCols)
				{
					this.mBlinkClosed = false;
					this.mEyeFrame = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_EYES).mNumCols - 1;
				}
				else if (!this.mBlinkClosed && --this.mEyeFrame < 0)
				{
					this.mBlink = false;
				}
			}
			for (int j = 0; j < this.mFeathers.Count; j++)
			{
				Feather feather = this.mFeathers[j];
				feather.mX += feather.mVX;
				feather.mY += feather.mVY;
				if (feather.mVX != 0f)
				{
					int num3 = (int)Common.Sign(feather.mVX);
					feather.mVX -= feather.mDecVX;
					int num4 = (int)Common.Sign(feather.mVX);
					if (num4 != num3)
					{
						feather.mVX = 0f;
					}
				}
				if (feather.mVY != 0f)
				{
					int num5 = (int)Common.Sign(feather.mVY);
					feather.mVY -= feather.mDecVY;
					int num6 = (int)Common.Sign(feather.mVY);
					if (num6 != num5)
					{
						feather.mVY = 0f;
					}
				}
				feather.mY += Common._M(0.5f);
				if (feather.mVX == feather.mVY && feather.mVX == 0f)
				{
					feather.mAngleOsc.Update();
					feather.mAlpha -= Common._M(2.5f);
					if (feather.mAlpha <= 0f)
					{
						this.mFeathers.RemoveAt(j);
						j--;
					}
				}
			}
			for (int k = 0; k < this.mEggFragments.Count; k++)
			{
				EggFragment eggFragment = this.mEggFragments[k];
				eggFragment.mX += eggFragment.mVX;
				eggFragment.mY += eggFragment.mVY + Common._M(0f);
				eggFragment.mVX -= eggFragment.mDecVX;
				eggFragment.mVY -= eggFragment.mDecVY;
				eggFragment.mAlpha -= Common._M(8f);
				if (eggFragment.mAlpha <= 0f)
				{
					this.mEggFragments.RemoveAt(k);
					k--;
				}
			}
		}

		public override void Init(Level l)
		{
			this.mWidth = Common._M(151);
			this.mHeight = Common._M(201);
			base.Init(l);
			this.mNumHeartsVisible = Common._M(-1);
			this.mX = (float)(Common._SS(GameApp.gApp.mWidth) / 2 + Common._DS(Common._M(32)) - GameApp.gApp.mBoardOffsetX);
			if (GameApp.gApp.mUserProfile.mBoss6Part2DialogSeen == 0)
			{
				GameApp.gApp.mUserProfile.mBoss6Part2DialogSeen++;
				TauntText tauntText = new TauntText();
				this.mTauntQueue.Add(tauntText);
				tauntText.mText = TextManager.getInstance().getString(389);
				tauntText.mTextId = 389;
				tauntText.mDelay = Common._M(500);
			}
			this.mDeathComp = this.mApp.LoadComposition("pax\\Lametiki_flagwave1", "_BOSS_LAME");
			this.mPoof = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_GENERICBOSSPOOF").Duplicate();
			Common.SetFXNumScale(this.mPoof, this.mApp.Is3DAccelerated() ? 1f : Common._M(0.15f));
			for (int i = 0; i < Boss.NUM_HEARTS; i++)
			{
				this.mHeartCels[i] = 0;
			}
		}

		public override Boss Instantiate()
		{
			BossLame bossLame = new BossLame(this.mLevel);
			bossLame.CopyFrom(this);
			return bossLame;
		}

		public void CopyFrom(BossLame rhs)
		{
			base.CopyFrom(rhs);
			this.mAngleOff = rhs.mAngleOff;
			this.mAngleDir = rhs.mAngleDir;
			this.mIsFiring = rhs.mIsFiring;
			this.mBlink = rhs.mBlink;
			this.mBlinkClosed = rhs.mBlinkClosed;
			this.mEyeFrame = rhs.mEyeFrame;
			this.mHitTimer = rhs.mHitTimer;
			this.mResGroup = rhs.mResGroup;
			this.mDeathTextTimer = rhs.mDeathTextTimer;
			this.mBossRadius = rhs.mBossRadius;
			this.mNumHeartsVisible = rhs.mNumHeartsVisible;
			this.mBulletRadius = rhs.mBulletRadius;
			this.mDeathStateTimer = rhs.mDeathStateTimer;
			this.mDeathComp = rhs.mDeathComp;
			this.mPoof = rhs.mPoof;
			this.mCryLoopCount = rhs.mCryLoopCount;
			this.mDeathState = rhs.mDeathState;
			this.mDrawDeathBGTikis = rhs.mDrawDeathBGTikis;
			this.mDrawHeartsBelowBoss = rhs.mDrawHeartsBelowBoss;
		}

		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncLong(ref this.mNumHeartsVisible);
			sync.SyncLong(ref this.mDeathTextTimer);
			sync.SyncFloat(ref this.mAngleOff);
			sync.SyncFloat(ref this.mAngleDir);
			sync.SyncBoolean(ref this.mIsFiring);
			sync.SyncLong(ref this.mHitTimer);
			sync.SyncBoolean(ref this.mBlink);
			sync.SyncBoolean(ref this.mBlinkClosed);
			sync.SyncLong(ref this.mEyeFrame);
			this.SyncListFeathers(sync, this.mFeathers, true);
			this.SyncListEggFragments(sync, this.mEggFragments, true);
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				if (sync.isWrite())
				{
					Egg egg = (Egg)this.mBullets[i].mData;
					egg.SyncState(sync);
				}
				else
				{
					Egg egg2 = new Egg();
					egg2.SyncState(sync);
					this.mBullets[i].mData = egg2;
				}
			}
		}

		private void SyncListFeathers(DataSync sync, List<Feather> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Feather feather = new Feather();
					feather.SyncState(sync);
					theList.Add(feather);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Feather feather2 in theList)
			{
				feather2.SyncState(sync);
			}
		}

		private void SyncListEggFragments(DataSync sync, List<EggFragment> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					EggFragment eggFragment = new EggFragment();
					eggFragment.SyncState(sync);
					theList.Add(eggFragment);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (EggFragment eggFragment2 in theList)
			{
				eggFragment2.SyncState(sync);
			}
		}

		protected float mAngleOff;

		protected float mAngleDir;

		protected int mNumHeartsVisible;

		protected bool mIsFiring;

		protected int mHitTimer;

		protected int mDeathTextTimer;

		protected bool mBlink;

		protected bool mBlinkClosed;

		protected bool mRunRight;

		protected int mEyeFrame;

		protected int mCryLoopCount;

		protected int mDeathState;

		protected int mDeathStateTimer;

		protected List<Feather> mFeathers = new List<Feather>();

		protected List<EggFragment> mEggFragments = new List<EggFragment>();

		protected CompositionMgr mDeathComp;

		protected PIEffect mPoof;

		private enum EState
		{
			State_Crying,
			State_FlagWave
		}
	}
}
