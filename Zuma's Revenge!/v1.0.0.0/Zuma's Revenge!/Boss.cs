using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public abstract class Boss : IDisposable
	{
		public int mWallDownTime
		{
			get
			{
				return this.mDWallDownTime.value;
			}
			set
			{
				this.mDWallDownTime.value = value;
			}
		}

		public float mHPDecPerHit
		{
			get
			{
				return this.mDHPDecPerHit.value;
			}
			set
			{
				this.mDHPDecPerHit.value = value;
			}
		}

		public float mHPDecPerProxBomb
		{
			get
			{
				return this.mDHPDecPerProxBomb.value;
			}
			set
			{
				this.mDHPDecPerProxBomb.value = value;
			}
		}

		public int mTikiHealthRespawnAmt
		{
			get
			{
				return this.mDTikiHealthRespawnAmt.value;
			}
			set
			{
				this.mDTikiHealthRespawnAmt.value = value;
			}
		}

		private void InitParamPointers()
		{
			Dictionary<string, ParamData<float>>.Enumerator enumerator = this.mFParamPointerMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				DDS gDDS = GameApp.gDDS;
				KeyValuePair<string, ParamData<float>> keyValuePair = enumerator.Current;
				if (gDDS.HasBossParam(keyValuePair.Key))
				{
					ParamData<float> paramData = new ParamData<float>();
					ParamData<float> paramData2 = paramData;
					DDS gDDS2 = GameApp.gDDS;
					KeyValuePair<string, ParamData<float>> keyValuePair2 = enumerator.Current;
					paramData2.value = gDDS2.GetBossParam(keyValuePair2.Key);
					Dictionary<string, ParamData<float>> dictionary = this.mFParamPointerMap;
					KeyValuePair<string, ParamData<float>> keyValuePair3 = enumerator.Current;
					dictionary[keyValuePair3.Key] = paramData;
				}
			}
			Dictionary<string, ParamData<int>>.Enumerator enumerator2 = this.mIParamPointerMap.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				DDS gDDS3 = GameApp.gDDS;
				KeyValuePair<string, ParamData<int>> keyValuePair4 = enumerator2.Current;
				if (gDDS3.HasBossParam(keyValuePair4.Key))
				{
					ParamData<int> paramData3 = new ParamData<int>();
					ParamData<int> paramData4 = paramData3;
					DDS gDDS4 = GameApp.gDDS;
					KeyValuePair<string, ParamData<int>> keyValuePair5 = enumerator2.Current;
					paramData4.value = (int)gDDS4.GetBossParam(keyValuePair5.Key);
					Dictionary<string, ParamData<int>> dictionary2 = this.mIParamPointerMap;
					KeyValuePair<string, ParamData<int>> keyValuePair6 = enumerator2.Current;
					dictionary2[keyValuePair6.Key] = paramData3;
				}
			}
			for (int i = 0; i < this.mBerserkTiers.size<BerserkTier>(); i++)
			{
				BerserkTier berserkTier = this.mBerserkTiers[i];
				for (int j = 0; j < berserkTier.mParams.size<BerserkModifier>(); j++)
				{
					BerserkModifier berserkModifier = berserkTier.mParams[j];
					string text = berserkModifier.mParamName.ToLower();
					if (this.mFParamPointerMap.ContainsKey(text))
					{
						berserkModifier.AddPointerFloat(this.mFParamPointerMap[text]);
					}
					if (this.mIParamPointerMap.ContainsKey(text))
					{
						berserkModifier.AddPointerInt(this.mIParamPointerMap[text]);
					}
					if (this.mBParamPointerMap.ContainsKey(text))
					{
						berserkModifier.AddPointerBool(this.mBParamPointerMap[text]);
					}
				}
			}
		}

		protected virtual void DecHearts(int amount)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS);
			for (int i = 0; i < Boss.NUM_HEARTS; i++)
			{
				if (this.mHeartCels[i] < imageByID.mNumCols - 1)
				{
					int num = this.mHeartCels[i];
					this.mHeartCels[i] += amount;
					if (this.mHeartCels[i] <= imageByID.mNumCols - 1)
					{
						break;
					}
					this.mHeartCels[i] = imageByID.mNumCols - 1;
					amount -= this.mHeartCels[i] - num;
				}
			}
		}

		protected virtual void ResetWallAndTikis(int wall_index)
		{
			if (this.mHP <= 0f)
			{
				return;
			}
			if (Enumerable.Count<BossWall>(this.mWalls) == Enumerable.Count<Tiki>(this.mTikis))
			{
				this.mTikis[wall_index].mWasHit = false;
				this.mTikis[wall_index].mAlphaFadeDir = 1;
				this.mWalls[wall_index].mAlphaFadeDir = 1;
			}
		}

		protected virtual bool DoHit(Bullet b, bool from_prox_bomb)
		{
			float mPrevHP = this.mHP;
			float num = (from_prox_bomb ? this.mHPDecPerProxBomb : this.mHPDecPerHit);
			int amount = (from_prox_bomb ? this.mHeartPieceDecAmtProxBomb : this.mHeartPieceDecAmt);
			if (num <= 0f)
			{
				return false;
			}
			this.mHP -= num;
			if (this.mTikiHealthRespawnAmt > 0 && this.CanDecTikiHealthSpawnAmt())
			{
				this.mCurrTikiBossHealthRemoved += (int)num;
				if (this.mCurrTikiBossHealthRemoved >= this.mTikiHealthRespawnAmt)
				{
					this.mCurrTikiBossHealthRemoved = 0;
					for (int i = 0; i < Enumerable.Count<BossWall>(this.mWalls); i++)
					{
						this.ResetWallAndTikis(i);
					}
				}
			}
			if (this.mHP <= 0f)
			{
				this.mHP = 0f;
				this.mDeathTimer = 0;
				this.PlaySound(0);
				this.mApp.GetBoard().BossDied();
			}
			else
			{
				this.PlaySound(3);
			}
			this.mDoExplosion = true;
			if (this.mAllowCompacting)
			{
				this.mNeedsCompacting = true;
			}
			this.DecHearts(amount);
			if (this.mHP > 0f)
			{
				this.CheckIfShouldGoBerserk(mPrevHP);
			}
			else
			{
				this.mTauntQueue.Clear();
			}
			return true;
		}

		protected virtual bool CompactCurves()
		{
			for (int i = 0; i < this.mLevel.mNumCurves; i++)
			{
				if (!this.mLevel.mCurveMgr[i].CanCompact())
				{
					return false;
				}
			}
			for (int j = 0; j < this.mLevel.mNumCurves; j++)
			{
				this.mLevel.mCurveMgr[j].CompactCurve(false);
			}
			return true;
		}

		protected virtual void DrawHearts(Graphics g)
		{
			if (this.mHP <= 0f || this.mDoDeathExplosions || this.mLevel.mBoard.DoingBossIntro())
			{
				return;
			}
			g.PushState();
			if (this.mAlphaOverride <= 254f)
			{
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
				g.SetColorizeImages(true);
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS);
			for (int i = 0; i < Boss.NUM_HEARTS; i++)
			{
				g.DrawImageCel(imageByID, (int)(ZumasRevenge.Common._S(this.mX + (float)this.mHeartXOff) + (float)(i * imageByID.GetCelWidth())), (int)ZumasRevenge.Common._S(this.mY + (float)this.mHeartYOff), this.mHeartCels[i]);
			}
			g.PopState();
		}

		protected virtual void DrawMisc(Graphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.DoingBossIntro())
			{
				for (int i = 0; i < this.mTikis.size<Tiki>(); i++)
				{
					this.mTikis[i].Draw(g);
				}
				this.DrawWalls(g);
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
			if (Boss.gBerserkTextAlpha > 0f)
			{
				g.SetFont(fontByID);
				g.SetColor(ZumasRevenge.Common._M(255), ZumasRevenge.Common._M1(0), ZumasRevenge.Common._M2(0), (int)Boss.gBerserkTextAlpha);
				string @string = TextManager.getInstance().getString(150);
				int num = g.GetFont().StringWidth(@string);
				g.DrawString(@string, (this.mApp.mWidth - num) / 2, (int)Boss.gBerserkTextY);
			}
			if (Boss.gImpatientTextAlpha > 0f)
			{
				g.SetFont(fontByID);
				g.SetColor(ZumasRevenge.Common._M(0), ZumasRevenge.Common._M1(0), ZumasRevenge.Common._M2(0), (int)Boss.gImpatientTextAlpha);
				string string2 = TextManager.getInstance().getString(151);
				int num2 = g.GetFont().StringWidth(string2);
				g.DrawString(string2, (this.mApp.mWidth - num2) / 2, (int)Boss.gImpatientTextY);
			}
			if (this.mHP <= 0f && this.mBandagedImg != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, 255 - (int)this.mAlphaOverride);
				g.DrawImage(this.mBandagedImg, (int)(ZumasRevenge.Common._S(this.mX) - (float)(this.mBandagedImg.mWidth / 2) + (float)this.mShakeXOff + (float)ZumasRevenge.Common._S(this.mBandagedXOff)), (int)(ZumasRevenge.Common._S(this.mY) - (float)(this.mBandagedImg.mHeight / 2) + (float)this.mShakeYOff + (float)ZumasRevenge.Common._S(this.mBandagedYOff)));
				g.SetColorizeImages(false);
			}
			if (this.mShouldDoDeathExplosions)
			{
				for (int j = 0; j < this.mDeathExplosions.size<PIEffect>(); j++)
				{
					PIEffect pieffect = this.mDeathExplosions[j];
					pieffect.mDrawTransform.LoadIdentity();
					float num3 = GameApp.DownScaleNum(1f);
					pieffect.mDrawTransform.Scale(num3, num3);
					pieffect.mDrawTransform.Translate(ZumasRevenge.Common._S(this.mX), ZumasRevenge.Common._S(this.mY));
					pieffect.Draw(g);
				}
			}
		}

		protected virtual bool BulletIntersectsBoss(Bullet b)
		{
			return MathUtils.CirclesIntersect(b.GetX(), b.GetY(), this.mX, this.mY + (float)this.mBossRadiusYOff, (float)(this.mBossRadius + b.GetRadius()));
		}

		protected void AddParamPointer(string p, ParamData<float> v)
		{
			this.mFParamPointerMap[p.ToLower()] = v;
		}

		protected void AddParamPointer(string p, ParamData<int> v)
		{
			this.mIParamPointerMap[p.ToLower()] = v;
		}

		protected void AddParamPointer(string p, ParamData<bool> v)
		{
			this.mBParamPointerMap[p.ToLower()] = v;
		}

		protected void CheckIfShouldGoBerserk(float mPrevHP)
		{
			foreach (BerserkTier berserkTier in this.mBerserkTiers)
			{
				if (mPrevHP >= (float)berserkTier.mHealthLimit && this.mHP < (float)berserkTier.mHealthLimit)
				{
					for (int i = 0; i < Enumerable.Count<BerserkModifier>(berserkTier.mParams); i++)
					{
						berserkTier.mParams[i].ModifyVariable();
					}
					this.BerserkActivated(berserkTier.mHealthLimit);
					this.ReInit();
					break;
				}
			}
		}

		protected virtual void ReInit()
		{
			this.mHeartPieceDecAmt = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / this.mHPDecPerHit));
			this.mHeartPieceDecAmtProxBomb = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / this.mHPDecPerProxBomb));
		}

		protected virtual void BerserkActivated(int health_limit)
		{
			Boss.gBerserkTextAlpha = 255f;
			Boss.gBerserkTextY = (float)(this.mApp.mHeight / 2);
			this.mIsBerserk = true;
			this.PlaySound(1);
			foreach (HulaEntry hulaEntry in this.mHulaEntryVec)
			{
				if (hulaEntry.mBerserkAmt == health_limit)
				{
					this.mCurrentHulaEntry = hulaEntry;
					break;
				}
			}
		}

		protected virtual void BallEaten(Bullet b)
		{
		}

		protected virtual bool CanSpawnHulaDancers()
		{
			return true;
		}

		protected virtual void DrawWalls(Graphics g)
		{
		}

		protected virtual Rect GetWallRect(BossWall w)
		{
			return new Rect(w.mX, w.mY, w.mWidth, w.mHeight);
		}

		protected virtual bool CollidesWithWall(Bullet b)
		{
			float num = (float)b.GetRadius() * 0.75f;
			Rect theTRect = new Rect((int)(b.GetX() - num), (int)(b.GetY() - num), (int)(num * 2f), (int)(num * 2f));
			foreach (BossWall bossWall in this.mWalls)
			{
				if (bossWall.mAlphaFadeDir >= 0 && this.GetWallRect(bossWall).Intersects(theTRect))
				{
					return true;
				}
			}
			return false;
		}

		protected virtual bool CanDecTikiHealthSpawnAmt()
		{
			return true;
		}

		protected virtual bool CanTaunt()
		{
			return true;
		}

		protected virtual void TikiHit(int idx)
		{
		}

		public Boss()
			: this(null)
		{
		}

		public Boss(Level l)
		{
			this.mX = 0f;
			this.mY = 0f;
			this.mMaxHP = 0f;
			this.mHP = 0f;
			this.mWidth = 101;
			this.mHeight = 78;
			this.mUpdateCount = 0;
			this.mHPDecPerHit = 0f;
			this.mHPDecPerProxBomb = 0f;
			this.mLevel = l;
			this.mShakeXAmt = 0;
			this.mShakeYAmt = 0;
			this.mShouldDoDeathExplosions = true;
			this.mShakeXOff = 0;
			this.mShakeYOff = 0;
			this.mAllowLevelDDS = false;
			this.mDoExplosion = false;
			this.mNeedsCompacting = false;
			this.mAllowCompacting = false;
			this.mHeartXOff = 0;
			this.mHeartYOff = 150;
			this.mResetWallTimerOnTikiHit = false;
			this.mResetWallsOnBossHit = false;
			this.mWallDownTime = 0;
			this.mCurWallDownTime = 0;
			this.mStunTime = 0;
			this.mCurrTikiBossHealthRemoved = 0;
			this.mTikiHealthRespawnAmt = 0;
			this.mNum = 0;
			this.mIsBerserk = false;
			this.mApp = GameApp.gApp;
			this.mEatsBalls = false;
			this.mImpatientTimer = -1;
			this.mBombFreqMax = 0;
			this.mBombFreqMin = 0;
			this.mBombDuration = 0;
			this.mProxBombRadius = 80;
			this.mDrawRadius = false;
			this.mBossRadius = 70;
			this.mNeedsIntroSound = false;
			this.mBombInRange = false;
			this.mRadiusColorChangeMode = 1;
			this.mDoDeathExplosions = false;
			this.mDeathTimer = 0;
			this.mWordBubbleTimer = 300;
			this.mSepiaImage = null;
			this.mDeathTX = 0f;
			this.mDeathTY = 0f;
			this.mDeathVX = 0f;
			this.mDeathVY = 0f;
			this.mExplosionRate = 4;
			this.mBossRadiusYOff = 0;
			this.mHulaAmnesty = 0;
			this.mBandagedImg = null;
			this.mAlphaOverride = 255f;
			this.mBandagedXOff = 0;
			this.mBandagedYOff = 0;
			this.mDrawDeathBGTikis = true;
			this.mTauntTextYOff = 0;
			Boss.gBerserkTextAlpha = 0f;
			Boss.gBerserkTextY = 0f;
			Boss.gImpatientTextAlpha = 0f;
			Boss.gImpatientTextY = 0f;
			this.mResPrefix = "IMAGE_";
			this.mHitEffect = null;
		}

		public virtual void Dispose()
		{
			this.mSepiaImage = null;
			for (int i = 0; i < this.mHulaDancers.size<HulaDancer>(); i++)
			{
				this.mHulaDancers[i] = null;
			}
			for (int j = 0; j < this.mDeathExplosions.Count; j++)
			{
				if (this.mDeathExplosions[j] != null)
				{
					this.mDeathExplosions[j].Dispose();
				}
			}
			this.mDeathExplosions.Clear();
			if (this.mHitEffect != null)
			{
				this.mHitEffect.Dispose();
				this.mHitEffect = null;
			}
		}

		public void AddTiki(int x, int y, int id, int rail_w, int rail_h, int travel_time)
		{
			Tiki tiki = new Tiki();
			this.mTikis.Add(tiki);
			tiki.mId = id;
			tiki.mX = (float)x;
			tiki.mY = (float)y;
			tiki.mRailStartX = x;
			tiki.mRailStartY = y;
			tiki.mRailEndX = x + rail_w;
			tiki.mRailEndY = y + rail_h;
			tiki.mTravelTime = travel_time;
			if (travel_time != 0)
			{
				tiki.mVX = (float)(tiki.mRailEndX - tiki.mRailStartX) / (float)travel_time;
			}
		}

		public void AddTiki(int x, int y, int id)
		{
			this.AddTiki(x, y, id, 0, 0, 0);
		}

		public void AddWall(int x, int y, int w, int h, int id)
		{
			BossWall bossWall = new BossWall();
			bossWall.mX = x;
			bossWall.mY = y;
			bossWall.mWidth = w;
			bossWall.mHeight = h;
			bossWall.mId = id;
			bossWall.mAlphaFadeDir = 1;
			bossWall.mAlpha = 0;
			this.mWalls.Add(bossWall);
		}

		public List<BossWall> getWalls()
		{
			return this.mWalls;
		}

		public void ForceNextTauntText()
		{
			this.mTauntQueue.Clear();
			Boss.FNTT_last_idx = (Boss.FNTT_last_idx + 1) % Enumerable.Count<TauntText>(this.mTauntText);
			if (Boss.FNTT_last_idx > Enumerable.Count<TauntText>(this.mTauntText))
			{
				Boss.FNTT_last_idx = 0;
			}
			this.mTauntQueue.Add(this.mTauntText[Boss.FNTT_last_idx]);
		}

		public virtual void Init(Level l)
		{
			this.mMaxHP = (this.mHP = 100f);
			if (l != null)
			{
				this.mLevel = l;
				for (int i = 0; i < this.mTikis.size<Tiki>(); i++)
				{
					this.mTikis[i].Init(this);
				}
			}
			if (this.mResGroup.Length > 0 && !this.mApp.mResourceManager.IsGroupLoaded(this.mResGroup) && !this.mApp.mResourceManager.LoadResources(this.mResGroup))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			if (!this.mApp.mResourceManager.IsGroupLoaded("Bosses") && !this.mApp.mResourceManager.LoadResources("Bosses"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			if (this.mNum == 6 && !this.mApp.mResourceManager.IsGroupLoaded("Boss6Common") && !this.mApp.mResourceManager.LoadResources("Boss6Common"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			this.mHitEffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_DEATH_EXPLOSION).Duplicate();
			ZumasRevenge.Common.SetFXNumScale(this.mHitEffect, this.mApp.Is3DAccelerated() ? 1f : ZumasRevenge.Common._M(0.25f));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_HEARTS);
			this.ReInit();
			for (int j = 0; j < Boss.NUM_HEARTS; j++)
			{
				this.mHeartCels[j] = imageByID.mNumCols - 1;
			}
			this.InitParamPointers();
			if (this.mWalls.size<BossWall>() == this.mTikis.size<Tiki>())
			{
				for (int k = 0; k < this.mWalls.size<BossWall>(); k++)
				{
					BossWall bossWall = this.mWalls[k];
					bossWall.mAlphaFadeDir = 1;
					bossWall.mAlpha = 0;
				}
			}
			if (this.mTikis.size<Tiki>() == 2)
			{
				this.mTikis[0].SetIsLeft(this.mTikis[0].mX < this.mTikis[1].mX);
				this.mTikis[1].SetIsLeft(this.mTikis[1].mX < this.mTikis[0].mX);
			}
			this.mSounds[6] = -1;
			this.mSounds[7] = -1;
			this.mSounds[8] = -1;
			this.mSounds[9] = -1;
			if (this.mNum < 6)
			{
				this.mSounds[0] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_DIE");
				this.mSounds[1] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_ENRAGE");
				this.mSounds[2] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_FIRE");
				this.mSounds[3] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_HIT");
				this.mSounds[4] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_PLAYER_HIT");
				this.mSounds[5] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS" + this.mNum + "_INTRO");
				if (this.mNum == 4)
				{
					this.mSounds[6] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS4_EAT_BALL");
					this.mSounds[8] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS4_TELEPORT");
				}
				else if (this.mNum == 1)
				{
					this.mSounds[7] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS1_ROAR");
				}
				else if (this.mNum == 5)
				{
					this.mSounds[9] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS5_SHIELD_HIT");
				}
			}
			else
			{
				this.mSounds[0] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_DIE" + (1 + SexyFramework.Common.Rand() % 3));
				this.mSounds[1] = -1;
				this.mSounds[2] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_FIRE");
				this.mSounds[3] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_HIT" + (1 + SexyFramework.Common.Rand() % 4));
				this.mSounds[4] = this.mApp.mResourceManager.LoadSound("SOUND_BULLET_HIT");
				this.mSounds[5] = this.mApp.mResourceManager.LoadSound("SOUND_BOSS_INTRO" + SexyFramework.Common.Rand() % 3);
			}
			for (int m = 0; m < this.mHulaEntryVec.size<HulaEntry>(); m++)
			{
				if (this.mHulaEntryVec[m].mBerserkAmt >= 100)
				{
					this.mCurrentHulaEntry = this.mHulaEntryVec[m];
					break;
				}
			}
			int num = -1;
			for (int n = 0; n < this.mTauntText.size<TauntText>(); n++)
			{
				if (this.mTauntText[n].mMinDeaths <= this.mApp.mUserProfile.GetAdvModeVars().mNumDeathsCurLevel && this.mTauntText[n].mMinDeaths > num)
				{
					num = this.mTauntText[n].mMinDeaths;
				}
			}
			for (int num2 = 0; num2 < this.mTauntText.size<TauntText>(); num2++)
			{
				TauntText tauntText = this.mTauntText[num2];
				if (tauntText.mCondition == 0 && tauntText.mMinDeaths == num)
				{
					this.mTauntQueue.Add(tauntText);
				}
			}
		}

		public virtual void Update(float f)
		{
			if (this.mHP <= 0f || this.mLevel.mBoard.GetGameState() == GameState.GameState_Losing)
			{
				float num = ((this.mHP <= 0f) ? ZumasRevenge.Common._M(1f) : ZumasRevenge.Common._M1(3f));
				this.mAlphaOverride -= num;
				if (this.mAlphaOverride < 0f)
				{
					this.mAlphaOverride = 0f;
				}
			}
			else if (this.mLevel.DoingInitialPathHilite() && this.mLevel.mBoard.GetGameState() != GameState.GameState_BossIntro && this.mUpdateCount % ZumasRevenge.Common._M(8) == 0 && this.mCleanHeart)
			{
				for (int i = 0; i < Boss.NUM_HEARTS; i++)
				{
					if (this.mHeartCels[i] != 0)
					{
						this.mHeartCels[i]--;
						break;
					}
				}
			}
			if (this.mAlphaOverride < 255f && this.mLevel.mBoard.GetGameState() != GameState.GameState_Losing && this.mLevel.mBoard.GetGameState() != GameState.GameState_BossDead)
			{
				this.mAlphaOverride += ZumasRevenge.Common._M(3f);
				if (this.mAlphaOverride > 255f)
				{
					this.mAlphaOverride = 255f;
				}
			}
			this.mUpdateCount++;
			if (this.mDoExplosion)
			{
				this.mHitEffect.Update();
				if (!this.mHitEffect.IsActive())
				{
					this.mHitEffect.ResetAnim();
					this.mDoExplosion = false;
				}
			}
			if (this.mCurWallDownTime > 0 && --this.mCurWallDownTime == 0)
			{
				for (int j = 0; j < this.mWalls.size<BossWall>(); j++)
				{
					this.ResetWallAndTikis(j);
				}
			}
			if (this.mWordBubbleTimer > 0 && !this.mLevel.mBoard.DoingBossIntro())
			{
				this.mWordBubbleTimer--;
			}
			if (this.mDoDeathExplosions && this.mShouldDoDeathExplosions && this.mHP <= 0f && this.mUpdateCount % ZumasRevenge.Common._M(25) == 0)
			{
				PIEffect pieffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_DEATH_EXPLOSION).Duplicate();
				this.mDeathExplosions.Add(pieffect);
				ZumasRevenge.Common.SetFXNumScale(pieffect, this.mApp.Is3DAccelerated() ? 1f : ZumasRevenge.Common._M(0.25f));
				SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Translate((float)ZumasRevenge.Common._S(-this.mWidth / 3 + SexyFramework.Common.Rand() % (int)((double)this.mWidth / 1.5)), (float)ZumasRevenge.Common._S(-this.mHeight / 3 + SexyFramework.Common.Rand() % (int)((double)this.mHeight / 1.5)));
				pieffect.mEmitterTransform.CopyFrom(sexyTransform2D);
			}
			for (int k = 0; k < this.mDeathExplosions.size<PIEffect>(); k++)
			{
				PIEffect pieffect2 = this.mDeathExplosions[k];
				pieffect2.Update();
				if (!pieffect2.IsActive())
				{
					pieffect2.Dispose();
					this.mDeathExplosions.RemoveAt(k);
					k--;
				}
			}
			for (int l = 0; l < this.mTauntQueue.size<TauntText>(); l++)
			{
				TauntText tauntText = this.mTauntQueue[l];
				tauntText.mUpdateCount++;
				if (tauntText.mUpdateCount < tauntText.mDelay)
				{
					break;
				}
				this.mTauntQueue.RemoveAt(l);
				l--;
			}
			if (this.mTauntQueue.size<TauntText>() == 0 && this.mApp.GetLevelMgr().mBossTauntChance > 0 && this.CanTaunt() && SexyFramework.Common._geq(this.mAlphaOverride, 255f))
			{
				List<int> list = new List<int>();
				for (int m = 0; m < this.mTauntText.size<TauntText>(); m++)
				{
					TauntText tauntText2 = this.mTauntText[m];
					if (this.mUpdateCount > tauntText2.mMinTime && SexyFramework.Common.Rand() % this.mApp.GetLevelMgr().mBossTauntChance == 0 && (tauntText2.mCondition != 1 || (SexyFramework.Common._eq(this.mHP, this.mMaxHP) && tauntText2.mCondition != 0)) && (tauntText2.mMinDeaths < 0 || tauntText2.mMinDeaths == this.mApp.mUserProfile.GetAdvModeVars().mNumDeathsCurLevel))
					{
						list.Add(m);
					}
				}
				if (list.size<int>() > 0)
				{
					this.mTauntQueue.Add(this.mTauntText[list[SexyFramework.Common.Rand() % list.size<int>()]]);
				}
			}
			if (this.mDoExplosion || this.mDoDeathExplosions)
			{
				this.mShakeXOff = SexyFramework.Common.IntRange(0, this.mShakeXAmt);
				this.mShakeYOff = SexyFramework.Common.IntRange(0, this.mShakeYAmt);
			}
			if (Boss.gBerserkTextAlpha > 0f)
			{
				Boss.gBerserkTextAlpha -= ZumasRevenge.Common._M(1f);
				Boss.gBerserkTextY -= ZumasRevenge.Common._M(1f);
			}
			if (Boss.gImpatientTextAlpha > 0f)
			{
				Boss.gImpatientTextAlpha -= ZumasRevenge.Common._M(1f);
				Boss.gImpatientTextY -= ZumasRevenge.Common._M(1f);
			}
			if (this.mLevel.mBoard.DoingBossIntro())
			{
				return;
			}
			if (this.mHP <= 0f)
			{
				if ((!this.mLevel.mFinalLevel || !this.mLevel.mBoard.mAdventureWinScreen) && Boss.last_idx >= 4)
				{
					Boss.last_idx = 0;
				}
				if (!this.mDoDeathExplosions)
				{
					for (int n = 0; n < this.mDeathText.size<BossText>(); n++)
					{
						BossText bossText = this.mDeathText[n];
						if (bossText.mAlpha < 255f)
						{
							bool flag = n == this.mDeathText.size<BossText>() - 1 && bossText.mAlpha < 255f;
							bossText.mAlpha = Math.Min(255f, bossText.mAlpha + 3f);
							if (flag && bossText.mAlpha >= 255f)
							{
								this.mApp.SetCursor(ECURSOR.CURSOR_HAND);
							}
						}
						if (bossText.mAlpha < (float)ZumasRevenge.Common._M(200))
						{
							break;
						}
					}
				}
				this.mX += this.mDeathVX;
				this.mY += this.mDeathVY;
				if ((this.mDeathVX > 0f && this.mX >= this.mDeathTX) || (this.mDeathVX < 0f && this.mX <= this.mDeathTX))
				{
					this.mX = this.mDeathTX;
					this.mDeathVX = 0f;
				}
				if ((this.mDeathVY > 0f && this.mY >= this.mDeathTY) || (this.mDeathVY < 0f && this.mY <= this.mDeathTY))
				{
					this.mY = this.mDeathTY;
					this.mDeathVY = 0f;
				}
				return;
			}
			bool flag2 = this.mLevel.AllCurvesAtRolloutPoint();
			if (this.mNeedsIntroSound && flag2 && !this.mApp.GetBoard().DoingIntros())
			{
				this.mNeedsIntroSound = false;
				this.PlaySound(5);
			}
			if (this.IsStunned())
			{
				this.mStunTime--;
			}
			if (this.mNeedsCompacting && !this.IsStunned() && this.CompactCurves())
			{
				this.mNeedsCompacting = false;
			}
			if (this.mHulaAmnesty > 0)
			{
				this.mHulaAmnesty--;
			}
			else if (this.mCurrentHulaEntry.mSpawnRate > 0 && this.mUpdateCount % this.mCurrentHulaEntry.mSpawnRate == 0 && SexyFramework.Common._geq(this.mAlphaOverride, 255f) && this.CanSpawnHulaDancers())
			{
				HulaDancer hulaDancer = new HulaDancer();
				this.mHulaDancers.Add(hulaDancer);
				bool has_proj = SexyFramework.Common.Rand() % 100 < this.mCurrentHulaEntry.mProjChance;
				hulaDancer.Setup(has_proj, (float)this.mCurrentHulaEntry.mSpawnY, this.mCurrentHulaEntry.mProjVY);
			}
			for (int num2 = 0; num2 < this.mHulaDancers.size<HulaDancer>(); num2++)
			{
				HulaDancer hulaDancer2 = this.mHulaDancers[num2];
				if (!SexyFramework.Common._eq(this.mAlphaOverride, 255f))
				{
					hulaDancer2.mFadeOut = true;
				}
				hulaDancer2.Update(this.mCurrentHulaEntry.mVX);
				if (hulaDancer2.CanRemove())
				{
					this.mHulaDancers[num2].Dispose();
					this.mHulaDancers.RemoveAt(num2);
					num2--;
				}
				else if (hulaDancer2.ProjectileCollided(this.mLevel.mFrog.GetRect()))
				{
					if (!this.mLevel.mFrog.IsFuckedUp())
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_SLOW));
						switch (this.mCurrentHulaEntry.mAttackType)
						{
						case 1:
							this.mLevel.mFrog.Stun(this.mCurrentHulaEntry.mAttackTime);
							break;
						case 2:
							this.mLevel.mFrog.Poison(this.mCurrentHulaEntry.mAttackTime);
							break;
						case 3:
							this.mLevel.mBoard.SetHallucinateTimer(this.mCurrentHulaEntry.mAttackTime);
							break;
						case 4:
							this.mLevel.mFrog.SetSlowTimer(this.mCurrentHulaEntry.mAttackTime);
							break;
						}
					}
					hulaDancer2.DestroyBullet();
				}
				else if (!hulaDancer2.HasFired() && this.CanSpawnHulaDancers() && hulaDancer2.GetX() > (float)(this.mLevel.mFrog.GetCenterX() + this.mCurrentHulaEntry.mProjRange))
				{
					hulaDancer2.Fire();
				}
			}
			if (this.mImpatientTimer > 0 && flag2 && --this.mImpatientTimer == 0)
			{
				Boss.gImpatientTextAlpha = 255f;
				Boss.gImpatientTextY = (float)(this.mApp.mHeight / 2);
			}
			if (this.mDrawRadius && flag2)
			{
				this.mBombInRange = false;
				int num3 = this.mProxBombRadius + 56 + ZumasRevenge.Common.GetDefaultBallRadius();
				int num4 = num3 * num3;
				int num5 = 0;
				while (num5 < this.mLevel.mNumCurves && !this.mBombInRange)
				{
					for (int num6 = 0; num6 < this.mLevel.mCurveMgr[num5].mBallList.Count; num6++)
					{
						Ball ball = this.mLevel.mCurveMgr[num5].mBallList[num6];
						if (ball.GetPowerOrDestType(false) == PowerType.PowerType_ProximityBomb)
						{
							if ((this.mRadiusColorChangeMode != 2 || ball.GetY() > this.mY - (float)(this.mHeight / 2) + (float)ZumasRevenge.Common._M(0)) && SexyFramework.Common.Distance(ball.GetX(), ball.GetY(), this.mX, this.mY, false) <= (float)num4)
							{
								ball.mDoBossPulse = true;
								this.mBombInRange = true;
							}
							else
							{
								ball.mDoBossPulse = false;
							}
						}
					}
					num5++;
				}
			}
			if (this.IsImpatient())
			{
				for (int num7 = 0; num7 < this.mLevel.mNumCurves; num7++)
				{
					this.mLevel.mCurveMgr[num7].mSpeedScale += 0.000100000005f;
				}
			}
			for (int num8 = 0; num8 < this.mTikis.size<Tiki>(); num8++)
			{
				this.mTikis[num8].Update();
			}
			for (int num9 = 0; num9 < this.mWalls.size<BossWall>(); num9++)
			{
				BossWall bossWall = this.mWalls[num9];
				int mAlpha = bossWall.mAlpha;
				bossWall.mAlpha += bossWall.mAlphaFadeDir * ZumasRevenge.Common._M(8);
				if (bossWall.mAlpha < 0)
				{
					bossWall.mAlpha = 0;
				}
				else if (bossWall.mAlpha > 255)
				{
					bossWall.mAlpha = 255;
				}
				if (bossWall.mAlphaFadeDir == 1 && bossWall.mAlpha >= 255 && mAlpha < bossWall.mAlpha)
				{
					bossWall.mAlphaFadeDir = 0;
					this.ResetWallAndTikis(num9);
				}
			}
			if (this.mBombInRange)
			{
				Boss.gWackColorFade += Boss.gWackColorFadeDir;
				if (Boss.gWackColorFade >= 255 && Boss.gWackColorFadeDir > 0)
				{
					Boss.gWackColorFade = 255;
					Boss.gWackColorFadeDir *= -1;
					return;
				}
				if (Boss.gWackColorFade <= 0 && Boss.gWackColorFadeDir < 0)
				{
					Boss.gWackColorFade = 0;
					Boss.gWackColorFadeDir *= -1;
				}
			}
		}

		public virtual void Update()
		{
			this.Update(1f);
		}

		public virtual void DrawDeathBGTikis(Graphics g)
		{
			if (this.mHP <= 0f && this.mDrawDeathBGTikis)
			{
				int num = (int)((255f - this.mAlphaOverride) / (float)ZumasRevenge.Common._M(11));
				if (num > 255)
				{
					num = 255;
				}
				g.PushState();
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, num);
				for (int i = 0; i < 13; i++)
				{
					ResID id = ResID.IMAGE_BOSSES_DEATH_BG_TIKIS_1 + i;
					Image imageByID = Res.GetImageByID(id);
					int num2 = ZumasRevenge.Common._DS(Res.GetOffsetXByID(id) - 160);
					int theY = ZumasRevenge.Common._DS(Res.GetOffsetYByID(id));
					g.DrawImage(imageByID, num2, theY);
					if (i != 7 && i != 5)
					{
						g.DrawImageMirror(imageByID, num2 + imageByID.GetWidth(), theY);
					}
				}
				g.PopState();
			}
		}

		public virtual void Draw(Graphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.DoingBossIntro())
			{
				for (int i = 0; i < this.mHulaDancers.size<HulaDancer>(); i++)
				{
					this.mHulaDancers[i].Draw(g);
				}
			}
		}

		public void DrawDeathText(Graphics g, int alpha_override)
		{
			bool flag = false;
			for (int i = 0; i < this.mDeathText.size<BossText>(); i++)
			{
				BossText bossText = this.mDeathText[i];
				if (bossText.mAlpha <= 0f)
				{
					break;
				}
				if (i == this.mDeathText.size<BossText>() - 1 && bossText.mAlpha >= (float)ZumasRevenge.Common._M(200))
				{
					flag = true;
				}
				Font fontByID = Res.GetFontByID(ResID.FONT_BOSS_TAUNT);
				if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_CH)
				{
					fontByID.mAscent = 25;
				}
				g.SetFont(fontByID);
				int num = ZumasRevenge.Common._S(ZumasRevenge.Common._M(200)) + i * ZumasRevenge.Common._S(ZumasRevenge.Common._M1(30));
				g.SetColor(ZumasRevenge.Common._M(255), ZumasRevenge.Common._M1(255), ZumasRevenge.Common._M2(255), (int)((alpha_override == -1) ? bossText.mAlpha : ((float)alpha_override)));
				g.WriteWordWrapped(new Rect(0, num + Localization.GetCurrentFontOffsetY() * i, this.mApp.mWidth, this.mApp.mHeight), bossText.mText, -1, 0);
			}
			if (flag)
			{
				if (alpha_override != -1)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, alpha_override);
				}
				Image imageByID = Res.GetImageByID(ResID.IMAGE_FROG_RIBBIT);
				g.DrawImage(imageByID, (this.mApp.mWidth - imageByID.mWidth) / 2, ZumasRevenge.Common._S(ZumasRevenge.Common._M(330)));
				g.SetColorizeImages(false);
				Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
				g.SetFont(fontByID2);
				g.SetColor(ZumasRevenge.Common._M(255), ZumasRevenge.Common._M1(255), ZumasRevenge.Common._M2(255));
				g.WriteString(TextManager.getInstance().getString(433), 0, ZumasRevenge.Common._DS(ZumasRevenge.Common._M(1170)), this.mApp.mWidth, 0);
			}
		}

		public void DrawDeathText(Graphics g)
		{
			this.DrawDeathText(g, -1);
		}

		public virtual void DrawTopLevel(Graphics g)
		{
		}

		public virtual void DrawBottomLevel(Graphics g)
		{
		}

		public virtual void DrawBelowBalls(Graphics g)
		{
			if (this.mDrawRadius && this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.DoingBossIntro())
			{
				Color color = new Color(0, 0, 255, ZumasRevenge.Common._M(125));
				if (this.mRadiusColorChangeMode != 0 && this.mBombInRange)
				{
					color = new Color(255, 0, 0, ZumasRevenge.Common._M(200));
				}
				g.SetColor(color);
				CommonGraphics.DrawCircle(g, ZumasRevenge.Common._S(this.mX), ZumasRevenge.Common._S(this.mY), (float)this.mProxBombRadius, ZumasRevenge.Common._S(ZumasRevenge.Common._M(30)));
			}
		}

		public virtual void DrawWordBubble(Graphics g)
		{
			if (this.mTauntQueue.size<TauntText>() == 0)
			{
				return;
			}
			TauntText tauntText = this.mTauntQueue[0];
			int wordBubbleAlpha = this.GetWordBubbleAlpha(tauntText);
			if (wordBubbleAlpha < 0)
			{
				return;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_MAIN22);
			Image theComponentImage;
			Rect theDest;
			Rect theRect;
			this.SetWordBubbleLayout(tauntText.mText, fontByID, out theComponentImage, out theDest, out theRect);
			g.SetFont(fontByID);
			g.SetColor(255, 255, 255, wordBubbleAlpha);
			if ((wordBubbleAlpha != 255 && this.mTauntQueue.size<TauntText>() == 1) || this.mAlphaOverride <= 254f)
			{
				g.SetColorizeImages(true);
			}
			g.DrawImageBox(theDest, theComponentImage);
			g.SetColor(0, 0, 0, wordBubbleAlpha);
			g.WriteWordWrapped(theRect, tauntText.mText, -1, 0);
			g.SetColorizeImages(false);
		}

		public int GetWordBubbleAlpha(TauntText inTauntText)
		{
			int num = inTauntText.mDelay - inTauntText.mUpdateCount;
			int num2 = 255;
			if (num <= 20)
			{
				num2 -= 26 * (20 - num);
			}
			if (this.mAlphaOverride <= 254f)
			{
				num2 = (int)Math.Min((float)num2, this.mAlphaOverride);
			}
			return num2;
		}

		public void SetWordBubbleLayout(string inText, Font inFont, out Image outBubbleBkg, out Rect outBubble, out Rect outInset)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_BOSSUI);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_WORD_BUBBLE);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_WORD_BUBBLE_MIRROR);
			int num = (int)((float)this.mApp.GetScreenRect().mWidth - (float)imageByID.GetWidth() * 1.5f);
			int num2 = (int)((float)(num - ZumasRevenge.Common._S(this.mWidth)) * 0.4f);
			int num3 = ZumasRevenge.Common._S(5);
			int num4 = num2 - num3 * 2;
			int num5 = ZumasRevenge.Common._GetWordWrappedHeight(inText, inFont, num4);
			int num6 = num5 + num3 * 2;
			Image image = imageByID2;
			int num7 = (int)((float)image.GetWidth() * 0.23f);
			int num8 = (int)((float)image.GetHeight() * 0.22f);
			Rect rect = default(Rect);
			rect.mX = (int)ZumasRevenge.Common._S(this.mX + (float)this.mWidth * 0.5f);
			rect.mY = (int)(ZumasRevenge.Common._S(this.mY - (float)this.mHeight * 0.5f) + (float)this.mTauntTextYOff);
			rect.mWidth = num2 + num7 * 2;
			rect.mHeight = num6 + num8 * 2;
			int num9 = this.mApp.GetScreenRect().mX + num;
			if (rect.mX + rect.mWidth >= num9)
			{
				int num10 = rect.mWidth + ZumasRevenge.Common._S(this.mWidth);
				if (rect.mX - num10 >= 0)
				{
					image = imageByID3;
					rect.mX -= num10;
				}
			}
			outBubbleBkg = image;
			outBubble = rect;
			outInset = new Rect(rect.mX + num7 + num3, rect.mY + num8 + num3, num4, num5);
		}

		public virtual void FrogInitialized(Gun g)
		{
		}

		public virtual void MouseDownDuringNoFire(int x, int y)
		{
		}

		public virtual bool AllowFrogToFire()
		{
			return this.mLevel.HasReachedCruisingSpeed();
		}

		public virtual int GetFrogReloadType()
		{
			return -1;
		}

		public virtual void MoveToDeathPosition(float x, float y)
		{
			this.mDeathTX = x;
			this.mDeathTY = y;
			this.mDeathVX = (x - this.mX) / 200f;
			this.mDeathVY = (y - this.mY) / 200f;
		}

		public void ShowAllDeathText()
		{
			this.mApp.SetCursor(ECURSOR.CURSOR_HAND);
			for (int i = 0; i < Enumerable.Count<BossText>(this.mDeathText); i++)
			{
				this.mDeathText[i].mAlpha = 255f;
			}
		}

		public void AddHulaEntry(float vx, float projvy, int spawn, int spawny, int proj_chance, int berserk_amt, int proj_range, int atype, int atime, int amnesty)
		{
			HulaEntry hulaEntry = new HulaEntry();
			hulaEntry.mBerserkAmt = berserk_amt;
			hulaEntry.mAmnesty = amnesty;
			hulaEntry.mProjVY = projvy;
			hulaEntry.mSpawnRate = spawn;
			hulaEntry.mVX = vx;
			hulaEntry.mSpawnY = spawny;
			hulaEntry.mProjChance = proj_chance;
			hulaEntry.mAttackTime = atime;
			hulaEntry.mAttackType = atype;
			hulaEntry.mProjRange = proj_range;
			this.mHulaEntryVec.Add(hulaEntry);
		}

		public List<HulaEntry> getHulaEntryList()
		{
			return this.mHulaEntryVec;
		}

		public void PlaySound(int soundid)
		{
			if (this.mApp.GetBoard().DoingIntros())
			{
				return;
			}
			if (this.mSounds[soundid] != -1)
			{
				this.mApp.PlaySample(this.mSounds[soundid]);
			}
		}

		public virtual void ProximityBombActivated(float x, float y, int radius)
		{
			this.ForceActivation(true);
		}

		public void AddBerserkValue(int health_limit, string param_name, string value, ref string minval, ref string maxval, bool _override)
		{
			BerserkModifier berserkModifier = new BerserkModifier(param_name, value, minval, maxval, _override);
			bool flag = param_name.Length == 0;
			for (int i = 0; i < Enumerable.Count<BerserkTier>(this.mBerserkTiers); i++)
			{
				if (this.mBerserkTiers[i].mHealthLimit == health_limit)
				{
					if (!flag)
					{
						this.mBerserkTiers[i].mParams.Add(berserkModifier);
					}
					return;
				}
			}
			BerserkTier berserkTier = new BerserkTier(health_limit);
			if (!flag)
			{
				berserkTier.mParams.Add(berserkModifier);
			}
			for (int j = 0; j < Enumerable.Count<BerserkTier>(this.mBerserkTiers); j++)
			{
				if (health_limit > this.mBerserkTiers[j].mHealthLimit)
				{
					this.mBerserkTiers.Insert(j, berserkTier);
					return;
				}
			}
			this.mBerserkTiers.Add(berserkTier);
		}

		public List<BerserkTier> getBerserkTiers()
		{
			return this.mBerserkTiers;
		}

		public void AddBerserkValue(int health_limit, string param_name, string value)
		{
			string text = "";
			this.AddBerserkValue(health_limit, param_name, value, ref text, ref text, false);
		}

		public virtual void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mEatsBalls);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mMaxHP);
			sync.SyncFloat(ref this.mHP);
			sync.SyncLong(ref this.mHulaAmnesty);
			sync.SyncFloat(ref this.mDHPDecPerHit.value);
			sync.SyncFloat(ref this.mDHPDecPerProxBomb.value);
			sync.SyncBoolean(ref this.mNeedsIntroSound);
			sync.SyncBoolean(ref this.mIsBerserk);
			sync.SyncLong(ref this.mWidth);
			sync.SyncLong(ref this.mHeight);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncBoolean(ref this.mBombInRange);
			sync.SyncBoolean(ref this.mDoExplosion);
			if (sync.isWrite())
			{
				ZumasRevenge.Common.SerializePIEffect(this.mHitEffect, sync);
			}
			else
			{
				ZumasRevenge.Common.DeserializePIEffect(this.mHitEffect, sync);
				ZumasRevenge.Common.SetFXNumScale(this.mHitEffect, GameApp.gApp.Is3DAccelerated() ? 1f : 0.25f);
			}
			sync.SyncBoolean(ref this.mNeedsCompacting);
			sync.SyncLong(ref this.mStunTime);
			sync.SyncLong(ref this.mCurrTikiBossHealthRemoved);
			sync.SyncLong(ref this.mDTikiHealthRespawnAmt.value);
			sync.SyncFloat(ref this.mDeathVX);
			sync.SyncFloat(ref this.mDeathVY);
			sync.SyncFloat(ref this.mDeathTX);
			sync.SyncFloat(ref this.mDeathTY);
			sync.SyncLong(ref this.mWordBubbleTimer);
			sync.SyncLong(ref this.mDeathTimer);
			sync.SyncBoolean(ref this.mDoDeathExplosions);
			sync.SyncLong(ref this.mDWallDownTime.value);
			sync.SyncLong(ref this.mCurWallDownTime);
			sync.SyncLong(ref this.mImpatientTimer);
			sync.SyncLong(ref this.mCurrentHulaEntry.mBerserkAmt);
			sync.SyncFloat(ref this.mCurrentHulaEntry.mVX);
			sync.SyncFloat(ref this.mCurrentHulaEntry.mProjVY);
			sync.SyncLong(ref this.mCurrentHulaEntry.mSpawnRate);
			sync.SyncLong(ref this.mCurrentHulaEntry.mSpawnY);
			sync.SyncLong(ref this.mCurrentHulaEntry.mProjChance);
			sync.SyncLong(ref this.mCurrentHulaEntry.mAttackType);
			sync.SyncLong(ref this.mCurrentHulaEntry.mAttackTime);
			sync.SyncLong(ref this.mCurrentHulaEntry.mProjRange);
			sync.SyncLong(ref this.mCurrentHulaEntry.mAmnesty);
			this.SyncTauntTexts(sync, true);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteLong((long)this.mHulaDancers.Count);
				for (int i = 0; i < this.mHulaDancers.Count; i++)
				{
					this.mHulaDancers[i].SyncState(sync);
				}
				buffer.WriteLong((long)this.mDeathText.Count);
				for (int j = 0; j < this.mDeathText.Count; j++)
				{
					buffer.WriteFloat(this.mDeathText[j].mAlpha);
				}
				buffer.WriteLong((long)this.mDeathExplosions.Count);
				for (int k = 0; k < this.mDeathExplosions.Count; k++)
				{
					ZumasRevenge.Common.SerializePIEffect(this.mDeathExplosions[k], sync);
				}
			}
			else
			{
				int num = (int)buffer.ReadLong();
				for (int l = 0; l < num; l++)
				{
					HulaDancer hulaDancer = new HulaDancer();
					hulaDancer.SyncState(sync);
					this.mHulaDancers.Add(hulaDancer);
				}
				int num2 = (int)buffer.ReadLong();
				for (int m = 0; m < num2; m++)
				{
					this.mDeathText[m].mAlpha = buffer.ReadFloat();
				}
				num2 = (int)buffer.ReadLong();
				for (int n = 0; n < num2; n++)
				{
					PIEffect pieffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_DEATH_EXPLOSION).Duplicate();
					this.mDeathExplosions.Add(pieffect);
					ZumasRevenge.Common.DeserializePIEffect(pieffect, sync);
					ZumasRevenge.Common.SetFXNumScale(pieffect, GameApp.gApp.Is3DAccelerated() ? 1f : ZumasRevenge.Common._M(0.25f));
				}
			}
			for (int num3 = 0; num3 < this.mWalls.Count; num3++)
			{
				sync.SyncLong(ref this.mWalls[num3].mAlpha);
				sync.SyncLong(ref this.mWalls[num3].mAlphaFadeDir);
			}
			for (int num4 = 0; num4 < this.mTikis.Count; num4++)
			{
				sync.SyncLong(ref this.mTikis[num4].mAlphaFadeDir);
				sync.SyncFloat(ref this.mTikis[num4].mX);
				sync.SyncFloat(ref this.mTikis[num4].mY);
				sync.SyncBoolean(ref this.mTikis[num4].mWasHit);
				sync.SyncLong(ref this.mTikis[num4].mAlpha);
			}
			for (int num5 = 0; num5 < Boss.NUM_HEARTS; num5++)
			{
				sync.SyncLong(ref this.mHeartCels[num5]);
			}
		}

		private void SyncTauntTexts(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mTauntQueue.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					TauntText tauntText = new TauntText();
					tauntText.SyncState(sync);
					this.mTauntQueue.Add(tauntText);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mTauntQueue.Count);
			foreach (TauntText tauntText2 in this.mTauntQueue)
			{
				tauntText2.SyncState(sync);
			}
		}

		public virtual bool Collides(Bullet b)
		{
			float num = (float)b.GetRadius() * ZumasRevenge.Common._M(0.75f);
			Rect r = new Rect((int)(b.GetX() - num), (int)(b.GetY() - num), (int)(num * 2f), (int)(num * 2f));
			bool flag = false;
			if (this.AllowFrogToFire())
			{
				flag = this.BulletIntersectsBoss(b);
				if (flag && !this.mEatsBalls)
				{
					flag = this.DoHit(b, false);
				}
				else if (flag && this.mEatsBalls)
				{
					this.BallEaten(b);
					this.PlaySound(6);
					return true;
				}
			}
			if (this.CollidesWithWall(b))
			{
				return true;
			}
			for (int i = 0; i < Enumerable.Count<HulaDancer>(this.mHulaDancers); i++)
			{
				if (this.mHulaDancers[i].Collided(r))
				{
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_HULAGIRLHIT));
					this.mHulaAmnesty = this.mCurrentHulaEntry.mAmnesty;
					this.mHulaDancers[i].Disable();
					return true;
				}
			}
			bool flag2 = false;
			if (this.AllowFrogToFire())
			{
				for (int j = 0; j < Enumerable.Count<Tiki>(this.mTikis); j++)
				{
					if (!this.mTikis[j].mWasHit && this.mTikis[j].mAlphaFadeDir >= 0)
					{
						bool flag3 = false;
						if (this.mTikis[j].Collides(b, ref flag3))
						{
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_TIKI_HIT));
							if (Enumerable.Count<Tiki>(this.mTikis) == Enumerable.Count<BossWall>(this.mWalls))
							{
								BossWall bossWall = this.mWalls[j];
								bossWall.mAlphaFadeDir = -1;
								this.TikiHit(j);
								int num2 = 0;
								for (int k = 0; k < Enumerable.Count<Tiki>(this.mTikis); k++)
								{
									if (this.mTikis[k].mWasHit)
									{
										num2++;
									}
								}
								if (num2 == Enumerable.Count<Tiki>(this.mTikis))
								{
									this.mCurWallDownTime = this.mWallDownTime;
								}
							}
							return true;
						}
					}
				}
			}
			if (flag && this.mResetWallsOnBossHit)
			{
				for (int l = 0; l < Enumerable.Count<BossWall>(this.mWalls); l++)
				{
					this.mWalls[l].mAlphaFadeDir = 1;
				}
				for (int m = 0; m < Enumerable.Count<Tiki>(this.mTikis); m++)
				{
					this.mTikis[m].mAlphaFadeDir = 1;
					this.mTikis[m].mWasHit = false;
				}
			}
			return flag || flag2;
		}

		public virtual void ForceActivation(bool from_prox_bomb)
		{
			this.DoHit(null, from_prox_bomb);
		}

		public abstract Boss Instantiate();

		public virtual void PostInstantiationHook(Boss source_boss)
		{
			this.mFParamPointerMap.Clear();
			this.mIParamPointerMap.Clear();
			this.mBParamPointerMap.Clear();
			this.AddParamPointer("WallDownTime", this.mDWallDownTime);
			this.AddParamPointer("HPDecPerHit", this.mDHPDecPerHit);
			this.AddParamPointer("HPDecPerProxBomb", this.mDHPDecPerProxBomb);
			this.AddParamPointer("TikiHealthRespawn", this.mDTikiHealthRespawnAmt);
			this.mTikis.Clear();
			foreach (Tiki tiki in source_boss.mTikis)
			{
				this.AddTiki((int)tiki.mX, (int)tiki.mY, tiki.mId, tiki.mRailEndX - tiki.mRailStartX, tiki.mRailEndY - tiki.mRailStartY, tiki.mTravelTime);
			}
		}

		public virtual bool CanAdvanceBalls()
		{
			return true;
		}

		public virtual void PlayerStartedFiring()
		{
		}

		public virtual void SetXY(float x, float y)
		{
			this.mX = x;
			this.mY = y;
		}

		public virtual void SetX(float x)
		{
			this.mX = x;
		}

		public virtual void SetY(float y)
		{
			this.mY = y;
		}

		public virtual void SetHPDecPerHit(float hp)
		{
			this.mHPDecPerHit = hp;
		}

		public virtual void SetHPDecPerHitProxBomb(float hp)
		{
			this.mHPDecPerProxBomb = hp;
		}

		public virtual void Stun(int stime)
		{
			this.mStunTime = stime;
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STUNNED));
		}

		public virtual void SetHP(float hp)
		{
			float num = this.mHP;
			this.mHP = hp;
			int num2 = (int)((num - this.mHP) / this.mHPDecPerHit);
			for (int i = 0; i < num2; i++)
			{
				this.DecHearts(this.mHeartPieceDecAmt);
			}
		}

		public bool IsStunned()
		{
			return this.mStunTime > 0;
		}

		public float GetHP()
		{
			return this.mHP;
		}

		public virtual int GetX()
		{
			return (int)this.mX;
		}

		public virtual int GetY()
		{
			return (int)this.mY;
		}

		public virtual int GetTopLeftX()
		{
			return (int)this.mX - this.mWidth / 2;
		}

		public virtual int GetTopLeftY()
		{
			return (int)this.mY - this.mHeight / 2;
		}

		public int GetWidth()
		{
			return this.mWidth;
		}

		public int GetHeight()
		{
			return this.mHeight;
		}

		public bool IsImpatient()
		{
			return this.mImpatientTimer == 0;
		}

		public bool IsHitByExplosion(float x, float y, int radius)
		{
			return MathUtils.CirclesIntersect(x, y, this.mX, this.mY, (float)(this.mProxBombRadius + 56 + ZumasRevenge.Common.GetDefaultBallRadius()));
		}

		public virtual void InitParam()
		{
		}

		public void CopyFrom(Boss rhs)
		{
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mMaxHP = rhs.mMaxHP;
			this.mHP = rhs.mHP;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mHPDecPerHit = rhs.mHPDecPerHit;
			this.mHPDecPerProxBomb = rhs.mHPDecPerProxBomb;
			this.mShakeXAmt = rhs.mShakeXAmt;
			this.mShakeYAmt = rhs.mShakeYAmt;
			this.mShouldDoDeathExplosions = rhs.mShouldDoDeathExplosions;
			this.mShakeXOff = rhs.mShakeXOff;
			this.mShakeYOff = rhs.mShakeYOff;
			this.mAllowLevelDDS = rhs.mAllowLevelDDS;
			this.mDoExplosion = rhs.mDoExplosion;
			this.mNeedsCompacting = rhs.mNeedsCompacting;
			this.mAllowCompacting = rhs.mAllowCompacting;
			this.mHeartXOff = rhs.mHeartXOff;
			this.mHeartYOff = rhs.mHeartYOff;
			this.mResetWallTimerOnTikiHit = rhs.mResetWallTimerOnTikiHit;
			this.mResetWallsOnBossHit = rhs.mResetWallsOnBossHit;
			this.mWallDownTime = rhs.mWallDownTime;
			this.mCurWallDownTime = rhs.mCurWallDownTime;
			this.mStunTime = rhs.mStunTime;
			this.mCurrTikiBossHealthRemoved = rhs.mCurrTikiBossHealthRemoved;
			this.mTikiHealthRespawnAmt = rhs.mTikiHealthRespawnAmt;
			this.mNum = rhs.mNum;
			this.mIsBerserk = rhs.mIsBerserk;
			this.mApp = GameApp.gApp;
			this.mEatsBalls = rhs.mEatsBalls;
			this.mImpatientTimer = rhs.mImpatientTimer;
			this.mBombFreqMax = rhs.mBombFreqMax;
			this.mBombFreqMin = rhs.mBombFreqMin;
			this.mBombDuration = rhs.mBombDuration;
			this.mProxBombRadius = rhs.mProxBombRadius;
			this.mDrawRadius = rhs.mDrawRadius;
			this.mBossRadius = rhs.mBossRadius;
			this.mNeedsIntroSound = rhs.mNeedsIntroSound;
			this.mBombInRange = rhs.mBombInRange;
			this.mRadiusColorChangeMode = rhs.mRadiusColorChangeMode;
			this.mDoDeathExplosions = rhs.mDoDeathExplosions;
			this.mDeathTimer = rhs.mDeathTimer;
			this.mWordBubbleTimer = rhs.mWordBubbleTimer;
			this.mSepiaImage = rhs.mSepiaImage;
			this.mDeathTX = rhs.mDeathTX;
			this.mDeathTY = rhs.mDeathTY;
			this.mDeathVX = rhs.mDeathVX;
			this.mDeathVY = rhs.mDeathVY;
			this.mExplosionRate = rhs.mExplosionRate;
			this.mBossRadiusYOff = rhs.mBossRadiusYOff;
			this.mHulaAmnesty = rhs.mHulaAmnesty;
			this.mBandagedImg = rhs.mBandagedImg;
			this.mAlphaOverride = rhs.mAlphaOverride;
			this.mBandagedXOff = rhs.mBandagedXOff;
			this.mBandagedYOff = rhs.mBandagedYOff;
			this.mDrawDeathBGTikis = rhs.mDrawDeathBGTikis;
			this.mTauntTextYOff = rhs.mTauntTextYOff;
			this.mResPrefix = rhs.mResPrefix;
			this.mHitEffect = rhs.mHitEffect;
			this.mDeathText.Clear();
			this.mDeathText.AddRange(rhs.mDeathText.ToArray());
			this.mTauntText.Clear();
			this.mTauntText.AddRange(rhs.mTauntText.ToArray());
			this.mTikis.Clear();
			for (int i = 0; i < rhs.mTikis.Count; i++)
			{
				this.mTikis.Add(new Tiki(rhs.mTikis[i]));
			}
			this.mHulaDancers.Clear();
			for (int j = 0; j < rhs.mHulaDancers.Count; j++)
			{
				this.mHulaDancers.Add(new HulaDancer(rhs.mHulaDancers[j]));
			}
			this.mHulaEntryVec.Clear();
			for (int k = 0; k < rhs.mHulaEntryVec.Count; k++)
			{
				this.mHulaEntryVec.Add(new HulaEntry(rhs.mHulaEntryVec[k]));
			}
			if (rhs.mCurrentHulaEntry != null)
			{
				this.mCurrentHulaEntry = new HulaEntry(rhs.mCurrentHulaEntry);
			}
			Dictionary<string, ParamData<float>>.Enumerator enumerator = rhs.mFParamPointerMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Dictionary<string, ParamData<float>> dictionary = this.mFParamPointerMap;
				KeyValuePair<string, ParamData<float>> keyValuePair = enumerator.Current;
				if (dictionary[keyValuePair.Key] != null)
				{
					Dictionary<string, ParamData<float>> dictionary2 = this.mFParamPointerMap;
					KeyValuePair<string, ParamData<float>> keyValuePair2 = enumerator.Current;
					ParamData<float> paramData = dictionary2[keyValuePair2.Key];
					KeyValuePair<string, ParamData<float>> keyValuePair3 = enumerator.Current;
					paramData.value = keyValuePair3.Value.value;
				}
			}
			Dictionary<string, ParamData<int>>.Enumerator enumerator2 = rhs.mIParamPointerMap.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Dictionary<string, ParamData<int>> dictionary3 = this.mIParamPointerMap;
				KeyValuePair<string, ParamData<int>> keyValuePair4 = enumerator2.Current;
				if (dictionary3[keyValuePair4.Key] != null)
				{
					Dictionary<string, ParamData<int>> dictionary4 = this.mIParamPointerMap;
					KeyValuePair<string, ParamData<int>> keyValuePair5 = enumerator2.Current;
					ParamData<int> paramData2 = dictionary4[keyValuePair5.Key];
					KeyValuePair<string, ParamData<int>> keyValuePair6 = enumerator2.Current;
					paramData2.value = keyValuePair6.Value.value;
				}
			}
			Dictionary<string, ParamData<bool>>.Enumerator enumerator3 = rhs.mBParamPointerMap.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Dictionary<string, ParamData<bool>> dictionary5 = this.mBParamPointerMap;
				KeyValuePair<string, ParamData<bool>> keyValuePair7 = enumerator3.Current;
				if (dictionary5[keyValuePair7.Key] != null)
				{
					Dictionary<string, ParamData<bool>> dictionary6 = this.mBParamPointerMap;
					KeyValuePair<string, ParamData<bool>> keyValuePair8 = enumerator3.Current;
					ParamData<bool> paramData3 = dictionary6[keyValuePair8.Key];
					KeyValuePair<string, ParamData<bool>> keyValuePair9 = enumerator3.Current;
					paramData3.value = keyValuePair9.Value.value;
				}
			}
			this.mBerserkTiers.Clear();
			for (int l = 0; l < rhs.mBerserkTiers.Count; l++)
			{
				this.mBerserkTiers.Add(new BerserkTier(rhs.mBerserkTiers[l]));
			}
			this.mWalls.Clear();
			for (int m = 0; m < rhs.mWalls.Count; m++)
			{
				this.mWalls.Add(new BossWall(rhs.mWalls[m]));
			}
			this.mDeathExplosions.Clear();
			for (int n = 0; n < rhs.mDeathExplosions.Count; n++)
			{
				this.mDeathExplosions.Add(this.mDeathExplosions[n]);
			}
			this.mTauntQueue.Clear();
			for (int num = 0; num < rhs.mTauntQueue.Count; num++)
			{
				this.mTauntQueue.Add(new TauntText(this.mTauntQueue[num]));
			}
			for (int num2 = 0; num2 < rhs.mSounds.Length; num2++)
			{
				this.mSounds[num2] = rhs.mSounds[num2];
			}
			for (int num3 = 0; num3 < rhs.mHeartCels.Length; num3++)
			{
				this.mHeartCels[num3] = rhs.mHeartCels[num3];
			}
		}

		public static float gBerserkTextAlpha;

		public static float gBerserkTextY;

		public static float gImpatientTextAlpha;

		public static float gImpatientTextY;

		protected static int gWackColorFade = 0;

		protected static int gWackColorFadeDir = 2;

		protected static int NUM_HEARTS = 5;

		protected static int FNTT_last_idx = 0;

		protected static int last_idx = 0;

		protected Dictionary<string, ParamData<float>> mFParamPointerMap = new Dictionary<string, ParamData<float>>();

		protected Dictionary<string, ParamData<int>> mIParamPointerMap = new Dictionary<string, ParamData<int>>();

		protected Dictionary<string, ParamData<bool>> mBParamPointerMap = new Dictionary<string, ParamData<bool>>();

		protected ParamData<int> mDWallDownTime = new ParamData<int>();

		protected ParamData<float> mDHPDecPerHit = new ParamData<float>();

		protected ParamData<float> mDHPDecPerProxBomb = new ParamData<float>();

		protected ParamData<int> mDTikiHealthRespawnAmt = new ParamData<int>();

		public bool mShouldDoDeathExplosions;

		public bool mDoDeathExplosions;

		public bool mNeedsIntroSound;

		public bool mEatsBalls;

		public GameApp mApp;

		public Level mLevel;

		public bool mAllowCompacting;

		public int mShakeXAmt;

		public int mShakeYAmt;

		public int mHeartXOff;

		public int mHeartYOff;

		public bool mResetWallsOnBossHit;

		public bool mResetWallTimerOnTikiHit;

		public bool mAllowLevelDDS;

		public bool mDrawRadius;

		public int mRadiusColorChangeMode;

		public int mCurWallDownTime;

		public int mCurrTikiBossHealthRemoved;

		public int mImpatientTimer;

		public int mNum;

		public string mName = "";

		public string mResPrefix = "";

		public int mBombFreqMin;

		public int mBombFreqMax;

		public int mBombDuration;

		public int mProxBombRadius;

		public int mBossRadius;

		public int mBossRadiusYOff;

		public int mVolcanoOffscreenDelay;

		public List<BossText> mDeathText = new List<BossText>();

		public string mWordBubbleText = "";

		public string mSepiaImagePath = "";

		public string mResGroup = "";

		public List<TauntText> mTauntText = new List<TauntText>();

		public DeviceImage mSepiaImage;

		public PIEffect mHitEffect;

		public float mAlphaOverride;

		public List<Tiki> mTikis = new List<Tiki>();

		protected int mTauntTextYOff;

		protected Image mBandagedImg;

		protected int mBandagedXOff;

		protected int mBandagedYOff;

		protected List<HulaDancer> mHulaDancers = new List<HulaDancer>();

		protected List<HulaEntry> mHulaEntryVec = new List<HulaEntry>();

		protected HulaEntry mCurrentHulaEntry = new HulaEntry();

		protected List<BerserkTier> mBerserkTiers = new List<BerserkTier>();

		protected List<BossWall> mWalls = new List<BossWall>();

		protected List<PIEffect> mDeathExplosions = new List<PIEffect>();

		protected List<TauntText> mTauntQueue = new List<TauntText>();

		protected int[] mSounds = new int[10];

		protected int mExplosionRate;

		protected bool mDrawDeathBGTikis;

		protected float mX;

		protected float mY;

		protected float mMaxHP;

		protected float mHP;

		protected float mDeathTX;

		protected float mDeathTY;

		protected float mDeathVX;

		protected float mDeathVY;

		protected int mHulaAmnesty;

		protected int mWidth;

		protected int mHeight;

		protected int mShakeXOff;

		protected int mShakeYOff;

		protected int mUpdateCount;

		protected int mHeartPieceDecAmt;

		protected int mHeartPieceDecAmtProxBomb;

		protected int[] mHeartCels = new int[Boss.NUM_HEARTS];

		protected int mStunTime;

		protected int mDeathTimer;

		protected bool mDoExplosion;

		protected bool mNeedsCompacting;

		protected bool mIsBerserk;

		protected bool mBombInRange;

		protected int mWordBubbleTimer;

		protected bool mCleanHeart = true;

		public enum Sound
		{
			Sound_Die,
			Sound_Enrage,
			Sound_Fire,
			Sound_BossHit,
			Sound_PlayerHit,
			Sound_Intro,
			Sound_EatBalls,
			Sound_Roar,
			Sound_Teleport,
			Sound_ShieldHit,
			Max_Sounds
		}

		public enum ColorChange
		{
			ColorChange_Never,
			ColorChange_BombInRange,
			ColorChange_NotBehind
		}
	}
}
