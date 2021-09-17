using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class BossStoneHead : BossShoot
	{
		protected override void DrawBossSpecificArt(Graphics g)
		{
			if (this.mStretchPct >= BossStoneHead.MAX_STONE_HEAD_STRETCH)
			{
				if (this.mExplodeComp.GetUpdateCount() < Common._M(150))
				{
					int num = -(this.mApp.mWidth / 2 - Common._S(this.GetX())) + Common._S(Common._M(-193)) + this.mApp.mBoardOffsetX;
					int num2 = -(this.mApp.mHeight / 2 - Common._S(this.GetY())) + Common._S(Common._M(-91));
					CumulativeTransform cumulativeTransform = new CumulativeTransform();
					cumulativeTransform.mTrans.Translate((float)num, (float)num2);
					int frame = ((this.mExplodeComp.mUpdateCount >= this.mExplodeComp.GetMaxDuration()) ? (this.mExplodeComp.GetMaxDuration() - 1) : (-1));
					this.mExplodeComp.Draw(g, cumulativeTransform, frame, Common._DS(1f));
				}
				else
				{
					this.mVolcanoBoss.Draw(g);
				}
			}
			float value = this.mX - (float)this.mWidth * this.mStretchPct / 2f + (float)this.mShakeXOff;
			float value2 = this.mY - (float)this.mHeight * this.mStretchPct / 2f + (float)this.mShakeYOff;
			for (int i = 0; i < this.mSteam.Count; i++)
			{
				Steam steam = this.mSteam[i];
				int num3 = (int)Math.Min(steam.mAlpha, this.mAlphaOverride);
				if (num3 != 255)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, num3);
				}
				if (!g.Is3D())
				{
					g.DrawImage(steam.mImage, (int)Common._S(this.mX + steam.mXOff + (float)Common._M(0)), (int)Common._S(this.mY + steam.mYOff + (float)Common._M1(0)), (int)(steam.mSize * (float)steam.mImage.mWidth), (int)(steam.mSize * (float)steam.mImage.mHeight));
				}
				else
				{
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.Scale(steam.mSize, steam.mSize);
					this.mGlobalTranform.RotateRad(steam.mAngle);
					if (g.Is3D())
					{
						g.DrawImageTransformF(steam.mImage, this.mGlobalTranform, Common._S(this.mX + steam.mXOff + (float)Common._M(0)), Common._S(this.mY + (float)Common._M1(0) + steam.mYOff));
					}
					else
					{
						g.DrawImageTransform(steam.mImage, this.mGlobalTranform, Common._S(this.mX + steam.mXOff + (float)Common._M(0)), Common._S(this.mY + (float)Common._M1(0) + steam.mYOff));
					}
				}
				g.SetColorizeImages(false);
			}
			if (this.mStretchPct < BossStoneHead.MAX_STONE_HEAD_STRETCH)
			{
				int theCel = 0;
				if (this.mHP <= 0f)
				{
					theCel = 1;
				}
				else if (this.mHitTimer > Common._M(194))
				{
					theCel = 1;
				}
				else if (this.mHitTimer > 0)
				{
					theCel = 2;
				}
				if (this.IMAGE_BOSS_STONEHEAD_FACES != null)
				{
					if (this.mHP <= 0f)
					{
						Rect theDestRect = new Rect((int)Common._S(value), (int)Common._S(value2), (int)((float)this.IMAGE_BOSS_STONEHEAD_FACES.GetCelWidth() * this.mStretchPct), (int)((float)this.IMAGE_BOSS_STONEHEAD_FACES.GetCelHeight() * this.mStretchPct));
						g.DrawImage(this.IMAGE_BOSS_STONEHEAD_FACES, theDestRect, this.IMAGE_BOSS_STONEHEAD_FACES.GetCelRect(theCel));
					}
					else
					{
						g.PushState();
						if (!Common._geq(this.mAlphaOverride, 255f))
						{
							g.SetColorizeImages(true);
							g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
						}
						g.DrawImageCel(this.IMAGE_BOSS_STONEHEAD_FACES, (int)Common._S(value), (int)Common._S(value2), theCel);
						g.PopState();
					}
				}
			}
			g.PushState();
			if (!Common._geq(this.mAlphaOverride, 255f))
			{
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
				g.SetColorizeImages(true);
			}
			if (this.mHitTimer == 0 && !this.mDoingExplodeAnim && Common._geq(this.mAlphaOverride, 255f))
			{
				int num4 = Common._M(-1);
				int num5 = Common._M(0);
				if (this.IMAGE_BOSS_STONEHEAD_EYES != null)
				{
					g.DrawImageCel(this.IMAGE_BOSS_STONEHEAD_EYES, (int)(Common._S(value) + Common._DSA(50f, (float)num4)), (int)(Common._S(value2) + Common._DSA(58f, (float)num5)), this.mEyeFrame);
				}
			}
			if (!this.mDoingExplodeAnim)
			{
				g.PushState();
				this.mLeftEye.Draw(g);
				g.PopState();
				g.PushState();
				this.mRightEye.Draw(g);
				g.PopState();
			}
			for (int j = 0; j < this.mRocks.Count; j++)
			{
				RockChunk rockChunk = this.mRocks[j];
				if (rockChunk.mAlpha != 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)rockChunk.mAlpha);
				}
				if (this.IMAGE_BOSS_STONEHEAD_ROCKS != null)
				{
					g.DrawImage(this.IMAGE_BOSS_STONEHEAD_ROCKS, new Rect((int)Common._S(rockChunk.mX), (int)Common._S(rockChunk.mY), (int)((float)this.IMAGE_BOSS_STONEHEAD_ROCKS.GetCelWidth() * Common._M(0.5f)), (int)((float)this.IMAGE_BOSS_STONEHEAD_ROCKS.GetCelHeight() * Common._M1(0.5f))), this.IMAGE_BOSS_STONEHEAD_ROCKS.GetCelRect(rockChunk.mCol));
				}
				g.SetColorizeImages(false);
			}
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				g.ClearClipRect();
			}
			if (!this.mDoingExplodeAnim && !this.mLevel.mBoard.IsPaused())
			{
				for (int k = 0; k < this.mBullets.Count; k++)
				{
					BossBullet bossBullet = this.mBullets[k];
					if (bossBullet.mDelay <= 0 && bossBullet.mState != 0)
					{
						EyeBullet eyeBullet = bossBullet.mData as EyeBullet;
						eyeBullet.Draw(g, (int)this.mAlphaOverride);
					}
				}
			}
			else if (this.mTextAlpha > 0f && this.mShowText)
			{
				g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
				g.SetColor(0, 0, 0, (int)Math.Min(this.mTextAlpha, 255f));
				float mTransX = g.mTransX;
				g.mTransX = 0f;
				if (!this.mLevel.mBoard.IsHardAdventureMode())
				{
					g.WriteString(TextManager.getInstance().getString(393), 0, Common._DS(Common._M(530)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(394), 0, Common._DS(Common._M(630)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(395), 0, Common._DS(Common._M(730)), this.mApp.mWidth, 0);
				}
				else
				{
					g.WriteString(TextManager.getInstance().getString(396), 0, Common._DS(Common._M(530)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(397), 0, Common._DS(Common._M(630)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(398), 0, Common._DS(Common._M(730)), this.mApp.mWidth, 0);
				}
				g.mTransX = mTransX;
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			g.PopState();
		}

		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			if (this.mDoingExplodeAnim)
			{
				return false;
			}
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_HIT));
			this.mHitTimer = Common._M(200);
			int num = Common._M(6);
			int num2 = (int)(this.mX - (float)(this.mWidth / 2));
			int num3 = (int)(this.mY - (float)(this.mHeight / 2));
			int num4 = num3 - Common._M(0);
			int num5 = num3 + Common._M(150);
			int num6 = (int)((float)(num5 - num4) / ((float)num / 2f));
			int num7 = num2 - Common._M(10);
			int num8 = Common._M(100);
			for (int i = 0; i < num; i++)
			{
				RockChunk rockChunk = new RockChunk();
				this.mRocks.Add(rockChunk);
				rockChunk.mCol = Common.Rand() % this.IMAGE_BOSS_STONEHEAD_ROCKS.mNumCols;
				rockChunk.mAlpha = 255f;
				rockChunk.mVX = 0f;
				rockChunk.mVY = Common.FloatRange(Common._M(3f), Common._M1(4f));
				rockChunk.mY = (float)(num4 + i / 2 * num6);
				rockChunk.mX = (float)(num7 + ((i % 2 == 0) ? num8 : 0));
			}
			bool flag = base.DoHit(b, from_prox_bomb);
			if (flag && Common._leq(this.mHP, 50f))
			{
				this.mVolcanoBoss = this.mLevel.mSecondaryBoss as BossVolcano;
				this.mVolcanoBoss.mIntro = true;
				this.mVolcanoBoss.SetXY((float)this.GetX(), (float)this.GetY());
				this.mApp.mBoard.mDrawBossUI = false;
				this.mApp.mBoard.mMenuButton.SetVisible(false);
				SoundAttribs soundAttribs = new SoundAttribs();
				soundAttribs.delay = 130;
				this.mApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_TRANSFORM), soundAttribs);
				this.mApp.GetBoard().mPreventBallAdvancement = true;
				this.mPauseMovement = true;
				this.mDoingExplodeAnim = true;
				for (int j = 0; j < this.mBullets.Count; j++)
				{
					this.mBullets[j].mDeleteInstantly = true;
				}
				this.mTextAlpha = 0f;
				for (int k = 0; k < this.mHulaDancers.Count; k++)
				{
					this.mHulaDancers[k].mFadeOut = true;
				}
			}
			return flag;
		}

		protected override void DidFire()
		{
			base.DidFire();
			this.mFiring = true;
			if (this.mLeftEye.mEyeFlame.mCurNumParticles == 0)
			{
				this.mLeftEye.mEyeFlame.ResetAnim();
			}
			if (this.mRightEye.mEyeFlame.mCurNumParticles == 0)
			{
				this.mRightEye.mEyeFlame.ResetAnim();
			}
			this.mLeftEye.mFiring = (this.mRightEye.mFiring = true);
			this.mLeftEye.mEyeFlame.mEmitAfterTimeline = (this.mRightEye.mEyeFlame.mEmitAfterTimeline = true);
		}

		protected override bool PreBulletUpdate(BossBullet b, int index)
		{
			if (b.mState == 0)
			{
				return true;
			}
			if (b.mDelay > 0)
			{
				b.mDelay--;
				return true;
			}
			if (b.mData != null && ((EyeBullet)b.mData).Update((int)b.mX, (int)b.mY, b.mBouncesLeft <= 0))
			{
				b.mDeleteInstantly = true;
			}
			return false;
		}

		protected override BossBullet CreateBossBullet()
		{
			BossBullet bossBullet = new BossBullet();
			EyeBullet eyeBullet = new EyeBullet();
			bossBullet.mData = eyeBullet;
			eyeBullet.mExplosion = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJEXPLOSION").Duplicate();
			eyeBullet.mProjectile = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJ").Duplicate();
			eyeBullet.mProjectile.mEmitAfterTimeline = true;
			eyeBullet.mSparks = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJSPARKS").Duplicate();
			return bossBullet;
		}

		protected override void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
			if (b.mData != null)
			{
				EyeBullet eyeBullet = (EyeBullet)b.mData;
				this.mApp.ReleaseGenericCachedEffect(eyeBullet.mSparks);
				this.mApp.ReleaseGenericCachedEffect(eyeBullet.mProjectile);
				this.mApp.ReleaseGenericCachedEffect(eyeBullet.mExplosion);
				b.mData = null;
			}
		}

		protected override Rect GetBulletRect(BossBullet b)
		{
			if (b.mData == null)
			{
				return new Rect(0, 0, 0, 0);
			}
			EyeBullet eyeBullet = (EyeBullet)b.mData;
			return new Rect((int)(b.mX + (float)eyeBullet.mXOff), (int)(b.mY + (float)eyeBullet.mYOff), Common._DS(Common._M(14)), Common._DS(Common._M1(20)));
		}

		protected override void BulletHitPlayer(BossBullet b)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.fadeout = 0.1f;
			this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_BURNINGFROGLOOP), soundAttribs);
			this.mApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_NEW_FIREHITFROG));
			if (!this.mApp.GetLevelMgr().mBossesCanAttackFuckedFrog)
			{
				this.mLevel.mFrog.SetSlowTimer(0);
				this.mLevel.mBoard.SetHallucinateTimer(0);
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					if (bossBullet.mDelay > 0 || bossBullet.mState == 0)
					{
						bossBullet.mDeleteInstantly = true;
					}
				}
			}
		}

		protected override void GetShotBounceOffs(BossBullet b, ref int x, ref int y)
		{
			x = (y = 0);
			if (b.mData == null)
			{
				return;
			}
			EyeBullet eyeBullet = (EyeBullet)b.mData;
			x += eyeBullet.mXOff + Common._DS(Common._M(0));
			y += eyeBullet.mYOff + Common._DS(Common._M(0));
		}

		protected override bool CanFire()
		{
			return !this.mDoingExplodeAnim;
		}

		protected override bool CanSpawnHulaDancers()
		{
			return !this.mDoingExplodeAnim;
		}

		protected override void ShotBounced(BossBullet b)
		{
			EyeBullet eyeBullet = (EyeBullet)b.mData;
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_EYE_LASER_BOUNCE));
			if (b.mBouncesLeft == 0)
			{
				eyeBullet.mExplosion.ResetAnim();
				eyeBullet.mProjectile.mEmitAfterTimeline = false;
				return;
			}
			if (eyeBullet.mSparks.mCurNumParticles == 0)
			{
				eyeBullet.mSparkFirstFrame = true;
				eyeBullet.mSparks.ResetAnim();
			}
		}

		protected override void BerserkActivated(int health_limit)
		{
			base.BerserkActivated(health_limit);
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_BERSERK));
		}

		protected override bool CanTaunt()
		{
			return !Common._leq(this.mHP, 50f) && base.CanTaunt();
		}

		public BossStoneHead(Level l)
			: base(l)
		{
			this.mShouldDoDeathExplosions = false;
			this.mBossRadius = Common._M(70);
			this.mBulletRadius = Common._M(5);
			this.mResGroup = "Boss6Common";
			this.mDrawDeathBGTikis = false;
		}

		public BossStoneHead()
			: this(null)
		{
		}

		public override void Dispose()
		{
			if (this.mExplodeComp != null)
			{
				this.mExplodeComp.Dispose();
				this.mExplodeComp = null;
			}
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				if (this.mBullets[i].mData != null)
				{
					EyeBullet eyeBullet = (EyeBullet)this.mBullets[i].mData;
					this.mApp.ReleaseGenericCachedEffect(eyeBullet.mSparks);
					this.mApp.ReleaseGenericCachedEffect(eyeBullet.mProjectile);
					this.mApp.ReleaseGenericCachedEffect(eyeBullet.mExplosion);
				}
			}
			this.mApp.ReleaseGenericCachedEffect(this.mLeftEye.mEyeFlame);
			this.mApp.ReleaseGenericCachedEffect(this.mRightEye.mEyeFlame);
		}

		public void CopyForm(BossStoneHead rhs)
		{
			base.CopyFrom(rhs);
			this.mShakeTime = rhs.mShakeTime;
			this.mEyeFlameAlpha = rhs.mEyeFlameAlpha;
			this.mBlink = rhs.mBlink;
			this.mBlinkClosed = rhs.mBlinkClosed;
			this.mFiring = rhs.mFiring;
			this.mDoingExplodeAnim = rhs.mDoingExplodeAnim;
			this.mTextAlpha = rhs.mTextAlpha;
			this.mShowText = rhs.mShowText;
			this.mEyeFrame = rhs.mEyeFrame;
			this.mHitTimer = rhs.mHitTimer;
			this.mLeftInUse = rhs.mLeftInUse;
			this.mRightInUse = rhs.mRightInUse;
			this.mExplodeComp = rhs.mExplodeComp;
			this.mVolcanoBoss = rhs.mVolcanoBoss;
			this.mLeftEye = new EyeAnim(rhs.mLeftEye);
			this.mRightEye = new EyeAnim(rhs.mRightEye);
		}

		public override void Update(float f)
		{
			base.Update(f);
			if (this.mHitTimer > 0 && Common._geq(this.mAlphaOverride, 255f))
			{
				this.mHitTimer--;
				if (this.mUpdateCount % Common._M(2) == 0)
				{
					Steam steam = new Steam();
					this.mSteam.Add(steam);
					steam.mAlphaDec = Common._M(4f);
					steam.mAngleInc = Common.FloatRange(Common._M(0.01f), Common._M1(0.1f));
					steam.mVX = Common._M(-2f);
					steam.mVY = Common._M(-0.02f);
					steam.mImgNum = Common.Rand() % 2;
					steam.mImage = ((steam.mImgNum == 0) ? Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG1) : Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG2));
					steam = new Steam();
					this.mSteam.Add(steam);
					steam.mAlphaDec = Common._M(4f);
					steam.mAngleInc = Common.FloatRange(Common._M(0.01f), Common._M1(0.1f));
					steam.mVX = Common._M(2f);
					steam.mVY = Common._M(-0.02f);
					steam.mImgNum = Common.Rand() % 2;
					steam.mImage = ((steam.mImgNum == 0) ? Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG1) : Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG2));
				}
			}
			for (int i = 0; i < this.mRocks.Count; i++)
			{
				RockChunk rockChunk = this.mRocks[i];
				rockChunk.mY += rockChunk.mVY;
				rockChunk.mX += rockChunk.mVX;
				rockChunk.mAlpha -= Common._M(4.5f);
				if (rockChunk.mAlpha <= 0f)
				{
					this.mRocks.RemoveAt(i);
					i--;
				}
			}
			for (int j = 0; j < this.mSteam.Count; j++)
			{
				Steam steam2 = this.mSteam[j];
				steam2.mXOff += steam2.mVX;
				steam2.mYOff += steam2.mVY;
				steam2.mAngle += steam2.mAngleInc;
				steam2.mSize += Common._M(0.01f);
				if (Math.Abs(steam2.mXOff) >= Common._M(-1f))
				{
					steam2.mAlpha -= steam2.mAlphaDec;
					if (steam2.mAlpha <= 0f)
					{
						this.mSteam.RemoveAt(j);
						j--;
					}
				}
			}
			if (!this.mBlink && !this.mFiring && Common.Rand() % Common._M(400) == 0)
			{
				this.mBlink = (this.mBlinkClosed = true);
			}
			else if (this.mBlink && this.mUpdateCount % Common._M(5) == 0)
			{
				if (this.mBlinkClosed && ++this.mEyeFrame >= 3)
				{
					this.mBlinkClosed = false;
					this.mEyeFrame = 2;
				}
				else if (!this.mBlinkClosed && --this.mEyeFrame < 0)
				{
					this.mBlink = false;
					this.mEyeFrame = 0;
				}
			}
			if (this.mEyeFlameAlpha >= 255f && Common._geq(this.mAlphaOverride, 255f))
			{
				float mX = this.mX - (float)this.mWidth / 2f + (float)this.mShakeXOff;
				float mY = this.mY - (float)this.mHeight / 2f + (float)this.mShakeYOff;
				for (int k = 0; k < this.mBullets.Count; k++)
				{
					BossBullet bossBullet = this.mBullets[k];
					EyeBullet eyeBullet = (EyeBullet)bossBullet.mData;
					if (bossBullet.mState == 0)
					{
						if (!this.mLeftInUse)
						{
							this.mLeftInUse = true;
							bossBullet.mState = -1;
							bossBullet.mX = mX;
							bossBullet.mY = mY;
							eyeBullet.mXOff = Common._M(49);
							eyeBullet.mYOff = Common._M(55);
						}
						else if (!this.mRightInUse)
						{
							this.mRightInUse = true;
							bossBullet.mState = 1;
							bossBullet.mX = mX;
							bossBullet.mY = mY;
							eyeBullet.mXOff = Common._M(89);
							eyeBullet.mYOff = Common._M(55);
						}
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_EYE_LASER));
						if (bossBullet.mState != 0 && (bossBullet.mShotType == 1 || bossBullet.mShotType == 3))
						{
							base.FireBulletAtPlayer(bossBullet, Common.FloatRange(base.mMinBulletSpeed, base.mMaxBulletSpeed), bossBullet.mX + (float)eyeBullet.mXOff, bossBullet.mY + (float)eyeBullet.mYOff);
							bossBullet.mTargetVX = bossBullet.mVX;
							bossBullet.mTargetVY = bossBullet.mVY;
						}
					}
					else if (bossBullet.mUpdateCount > Common._M(50))
					{
						if (bossBullet.mState < 0)
						{
							this.mLeftInUse = false;
						}
						else
						{
							this.mRightInUse = false;
						}
					}
				}
				if (!this.mLeftInUse && !this.mRightInUse)
				{
					this.mLeftEye.mEyeFlame.mEmitAfterTimeline = (this.mRightEye.mEyeFlame.mEmitAfterTimeline = false);
					this.mLeftEye.mFiring = (this.mRightEye.mFiring = false);
					this.mFiring = false;
				}
			}
			if (this.mFiring)
			{
				if (this.mEyeFlameAlpha < 255f)
				{
					this.mEyeFlameAlpha += Common._M(4f);
					if (this.mEyeFlameAlpha > 255f)
					{
						this.mEyeFlameAlpha = 255f;
					}
				}
			}
			else if (this.mEyeFlameAlpha > 0f)
			{
				this.mEyeFlameAlpha -= Common._M(5f);
				if (this.mEyeFlameAlpha < 0f)
				{
					this.mEyeFlameAlpha = 0f;
				}
			}
			this.mLeftEye.Update((int)this.mX + BossStoneHead.LEFT_EYE_XOFF, (int)this.mY + BossStoneHead.EYE_YOFF, (int)this.mAlphaOverride);
			this.mRightEye.Update((int)this.mX + BossStoneHead.RIGHT_EYE_XOFF, (int)this.mY + BossStoneHead.EYE_YOFF, (int)this.mAlphaOverride);
			if (this.mDoingExplodeAnim)
			{
				bool flag = this.UpdateDeathSequence();
				if (flag)
				{
					this.mTextAlpha -= Common._M(1f);
					this.mExplodeComp.Update();
					if (this.mExplodeComp.GetUpdateCount() == 35)
					{
						this.DoDeathRockExplosionThing();
					}
					if (this.mExplodeComp.GetUpdateCount() >= Common._M(150))
					{
						this.mVolcanoBoss.Update();
					}
					if (this.mExplodeComp.Done() && this.mTextAlpha <= 0f)
					{
						this.mLevel.SwitchToSecondaryBoss();
						this.mVolcanoBoss.mIntro = false;
						this.mApp.GetBoard().mPreventBallAdvancement = false;
						this.mApp.mBoard.mDrawBossUI = true;
						this.mApp.mBoard.mMenuButton.SetVisible(true);
					}
				}
			}
		}

		public override void Init(Level l)
		{
			this.mWidth = Common._M(120);
			this.mHeight = Common._M(182);
			base.Init(l);
			if (this.mExplodeComp != null)
			{
				this.mExplodeComp.Dispose();
				this.mExplodeComp = null;
			}
			this.mExplodeComp = new Composition();
			this.mExplodeComp.mLoadImageFunc = new AECommon.LoadCompImageFunc(GameApp.CompositionLoadFunc);
			this.mExplodeComp.mPostLoadImageFunc = new AECommon.PostLoadCompImageFunc(GameApp.CompositionPostLoadFunc);
			this.mExplodeComp.LoadFromFile("pax\\BreakEasterIsland_FINAL");
			this.mLeftEye.mEyeFlame = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSEYES").Duplicate();
			this.mRightEye.mEyeFlame = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSEYES").Duplicate();
			this.IMAGE_BOSS_STONEHEAD_FACES = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FACES);
			this.IMAGE_BOSS_STONEHEAD_ROCKS = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_ROCKS);
			this.IMAGE_BOSS_STONEHEAD_EYES = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_EYES);
		}

		public override Boss Instantiate()
		{
			BossStoneHead bossStoneHead = new BossStoneHead(this.mLevel);
			bossStoneHead.CopyFrom(this);
			bossStoneHead.mSteam.Clear();
			bossStoneHead.mRocks.Clear();
			return bossStoneHead;
		}

		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			Buffer buffer = sync.GetBuffer();
			sync.SyncBoolean(ref this.mDoingExplodeAnim);
			sync.SyncFloat(ref this.mTextAlpha);
			sync.SyncBoolean(ref this.mShowText);
			sync.SyncLong(ref this.mExplodeComp.mUpdateCount);
			sync.SyncFloat(ref this.mStretchPct);
			sync.SyncLong(ref this.mShakeTime);
			sync.SyncFloat(ref this.mEyeFlameAlpha);
			sync.SyncBoolean(ref this.mBlink);
			sync.SyncBoolean(ref this.mBlinkClosed);
			sync.SyncBoolean(ref this.mFiring);
			sync.SyncLong(ref this.mEyeFrame);
			sync.SyncLong(ref this.mHitTimer);
			sync.SyncBoolean(ref this.mLeftInUse);
			sync.SyncBoolean(ref this.mRightInUse);
			this.mLeftEye.SyncState(sync);
			this.mRightEye.SyncState(sync);
			this.SyncListRockChunks(sync, this.mRocks, true);
			this.SyncListSteams(sync, this.mSteam, true);
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				if (sync.isWrite())
				{
					EyeBullet eyeBullet = (EyeBullet)this.mBullets[i].mData;
					eyeBullet.SyncState(sync);
				}
				else
				{
					EyeBullet eyeBullet2 = new EyeBullet();
					eyeBullet2.SyncState(sync);
					this.mBullets[i].mData = eyeBullet2;
				}
			}
			if (sync.isWrite())
			{
				buffer.WriteBoolean(this.mVolcanoBoss != null);
				if (this.mVolcanoBoss != null)
				{
					buffer.WriteLong((long)this.mVolcanoBoss.GetX());
					buffer.WriteLong((long)this.mVolcanoBoss.GetY());
					return;
				}
			}
			else if (sync.isRead())
			{
				if (buffer.ReadBoolean())
				{
					this.mVolcanoBoss = (BossVolcano)this.mLevel.mSecondaryBoss;
					this.mVolcanoBoss.mIntro = true;
					int num = (int)buffer.ReadLong();
					int num2 = (int)buffer.ReadLong();
					this.mVolcanoBoss.SetXY((float)num, (float)num2);
					return;
				}
				this.mVolcanoBoss = null;
			}
		}

		private void SyncListRockChunks(DataSync sync, List<RockChunk> theList, bool clear)
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
					RockChunk rockChunk = new RockChunk();
					rockChunk.SyncState(sync);
					theList.Add(rockChunk);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (RockChunk rockChunk2 in theList)
			{
				rockChunk2.SyncState(sync);
			}
		}

		private void SyncListSteams(DataSync sync, List<Steam> theList, bool clear)
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
					Steam steam = new Steam();
					steam.SyncState(sync);
					theList.Add(steam);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Steam steam2 in theList)
			{
				steam2.SyncState(sync);
			}
		}

		public override bool AllowFrogToFire()
		{
			return base.AllowFrogToFire() && !this.mDoingExplodeAnim;
		}

		public bool UpdateDeathSequence()
		{
			if (this.mShakeTime > 0)
			{
				if (this.mShakeTime == Common._M(150))
				{
					this.mShowText = true;
				}
				else if (this.mShakeTime < Common._M(150))
				{
					this.mTextAlpha += Common._M(2.8f);
				}
				this.mShakeTime--;
				if (this.mShakeTime % Common._M(50) == 0)
				{
					this.mShakeXAmt++;
					this.mShakeYAmt++;
				}
				this.mShakeXOff = Common.IntRange(0, this.mShakeXAmt);
				this.mShakeYOff = Common.IntRange(0, this.mShakeYAmt);
			}
			else
			{
				this.mShakeXOff = (this.mShakeYOff = 0);
			}
			for (int i = 0; i < this.mRocks.Count; i++)
			{
				RockChunk rockChunk = this.mRocks[i];
				rockChunk.mY += rockChunk.mVY;
				rockChunk.mX += rockChunk.mVX;
				rockChunk.mVY += Common._M(0.2f);
				rockChunk.mAlpha -= Common._M(4.5f);
				if (rockChunk.mAlpha <= 0f)
				{
					this.mRocks.RemoveAt(i);
					i--;
				}
			}
			if (Boss.gBerserkTextAlpha > 0f)
			{
				Boss.gBerserkTextAlpha -= Common._M(1f);
				Boss.gBerserkTextY -= Common._M(1f);
			}
			for (int j = 0; j < this.mSteam.Count; j++)
			{
				Steam steam = this.mSteam[j];
				steam.mXOff += steam.mVX;
				steam.mYOff += steam.mVY;
				steam.mAngle += steam.mAngleInc;
				steam.mSize += Common._M(0.01f);
				if (Math.Abs(steam.mXOff) >= Common._M(-1f))
				{
					steam.mAlpha -= steam.mAlphaDec;
					if (steam.mAlpha <= 0f)
					{
						this.mSteam.RemoveAt(j);
						j--;
					}
				}
			}
			if (this.mShakeTime <= 0)
			{
				this.mStretchPct += (BossStoneHead.MAX_STONE_HEAD_STRETCH - 1f) / Common._M(25f);
				if (this.mStretchPct >= BossStoneHead.MAX_STONE_HEAD_STRETCH)
				{
					this.mStretchPct = BossStoneHead.MAX_STONE_HEAD_STRETCH;
					return true;
				}
			}
			return false;
		}

		public void DoDeathRockExplosionThing()
		{
			for (int i = 45; i < 135; i += Common._M(2))
			{
				RockChunk rockChunk = new RockChunk();
				this.mRocks.Add(rockChunk);
				rockChunk.mCol = Common.Rand() % this.IMAGE_BOSS_STONEHEAD_ROCKS.mNumCols;
				rockChunk.mAlpha = 255f;
				float num = Common.DegreesToRadians((float)i);
				float num2 = Common.FloatRange(Common._M(4f), Common._M1(6f));
				rockChunk.mVX = num2 * (float)Math.Cos((double)num);
				rockChunk.mVY = -num2 * (float)Math.Sin((double)num);
				rockChunk.mX = this.mX;
				rockChunk.mY = this.mY;
			}
		}

		public override int GetTopLeftX()
		{
			return (int)(this.mX - (float)this.mWidth * this.mStretchPct / 2f);
		}

		public override int GetTopLeftY()
		{
			return (int)(this.mY - (float)this.mHeight * this.mStretchPct / 2f);
		}

		protected static float MAX_STONE_HEAD_STRETCH = 1.001f;

		protected static int LEFT_EYE_XOFF = -16;

		protected static int RIGHT_EYE_XOFF = 30;

		protected static int EYE_YOFF = -35;

		protected int mShakeTime = 300;

		protected float mEyeFlameAlpha;

		protected bool mBlink;

		protected bool mBlinkClosed = true;

		protected bool mFiring;

		protected bool mDoingExplodeAnim;

		protected float mTextAlpha;

		protected bool mShowText;

		protected int mEyeFrame;

		protected int mHitTimer;

		protected bool mLeftInUse;

		protected bool mRightInUse;

		protected EyeAnim mLeftEye = new EyeAnim();

		protected EyeAnim mRightEye = new EyeAnim();

		protected List<Steam> mSteam = new List<Steam>();

		protected List<RockChunk> mRocks = new List<RockChunk>();

		protected Composition mExplodeComp;

		protected BossVolcano mVolcanoBoss;

		private Image IMAGE_BOSS_STONEHEAD_FACES;

		private Image IMAGE_BOSS_STONEHEAD_EYES;

		private Image IMAGE_BOSS_STONEHEAD_ROCKS;

		public float mStretchPct = 1f;
	}
}
