using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class FakeCredits : IDisposable
	{
		public FakeCredits()
		{
			this.mUpdateCount = 0;
			this.mFrogEffect = null;
			for (int i = 0; i < FakeCredits.MAX_CREDITS; i++)
			{
				this.mCredits[i] = new FakeCreditsText();
			}
			for (int j = 0; j < FakeCredits.MAX_HEADER_LETTERS; j++)
			{
				this.mHeaderLetters[j] = new HeaderLetter();
			}
		}

		public virtual void Dispose()
		{
			if (this.mFrogEffect != null)
			{
				this.mFrogEffect.Dispose();
				this.mFrogEffect = null;
			}
			for (int i = 0; i < this.mTikis.Count; i++)
			{
				if (this.mTikis[i].mSmoke != null)
				{
					this.mTikis[i].mSmoke.Dispose();
					this.mTikis[i].mSmoke = null;
				}
			}
			this.mTikis.Clear();
		}

		public void Init(Gun frog)
		{
			this.mTauntText.Clear();
			string[] array = new string[]
			{
				TextManager.getInstance().getString(459),
				TextManager.getInstance().getString(460),
				TextManager.getInstance().getString(461)
			};
			if (GameApp.gApp.GetBoard().IsHardAdventureMode())
			{
				array[0] = TextManager.getInstance().getString(462);
				array[1] = TextManager.getInstance().getString(463);
				array[2] = TextManager.getInstance().getString(464);
			}
			for (int i = 0; i < array.Length; i++)
			{
				SimpleFadeText simpleFadeText = new SimpleFadeText();
				this.mTauntText.Add(simpleFadeText);
				simpleFadeText.mString = array[i];
				simpleFadeText.mAlpha = 0f;
				simpleFadeText.mFadeIn = true;
			}
			this.mBossX = (GameApp.gApp.mWidth - Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_STONE_BOSS).GetCelWidth()) / 2 - GameApp.gApp.mBoardOffsetX;
			if (this.mFrogEffect != null)
			{
				this.mFrogEffect.Dispose();
				this.mFrogEffect = null;
			}
			this.mFrogEffect = new FrogFlyOff();
			this.mFrogEffect.JumpOut(frog);
			this.mTopWoodY = (float)(-(float)Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_WOOD_TOP).mHeight);
			this.mBottomWoodY = (float)GameApp.gApp.mHeight;
			this.mTopWoodShakeY = 0f;
			this.mState = 0;
			this.mBottomWoodBounceDir = 0;
			this.mBottomWoodBounceCount = 0;
			this.mBGAlpha = 0f;
			this.mTimer = 0;
			this.mScreenShakeTimer = 0;
			this.mCanDoNextLevel = (this.mDone = false);
			this.mFadeOutAlpha = 0f;
			this.mBossY = (float)(-(float)Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_STONE_BOSS).mHeight * 3);
			for (int j = 0; j < FakeCredits.MAX_CREDITS; j++)
			{
				this.mCredits[j].mImage = Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_NAME1 + j);
				this.mCredits[j].mX = (float)((j == 0) ? ZumasRevenge.Common._M(206) : ZumasRevenge.Common._M1(136));
				this.mCredits[j].mY = (float)((j == 0) ? ZumasRevenge.Common._M(286) : (ZumasRevenge.Common._M1(362) + (j - 1) * ZumasRevenge.Common._M2(50)));
				this.mCredits[j].mAngle = 0f;
				this.mCredits[j].mFalling = false;
			}
			this.mRockBits.Clear();
			Point[] array2 = new Point[]
			{
				new Point(ZumasRevenge.Common._M(170), ZumasRevenge.Common._M1(45)),
				new Point(ZumasRevenge.Common._M2(225), ZumasRevenge.Common._M3(35)),
				new Point(ZumasRevenge.Common._M4(327), ZumasRevenge.Common._M5(31)),
				new Point(ZumasRevenge.Common._M6(384), ZumasRevenge.Common._M7(20)),
				new Point(ZumasRevenge.Common._M8(474), ZumasRevenge.Common._M9(12)),
				new Point(ZumasRevenge.Common._M(274), ZumasRevenge.Common._M1(112)),
				new Point(ZumasRevenge.Common._M2(231), ZumasRevenge.Common._M3(108)),
				new Point(ZumasRevenge.Common._M4(455), ZumasRevenge.Common._M5(126))
			};
			for (int k = 0; k < FakeCredits.MAX_HEADER_LETTERS; k++)
			{
				this.mHeaderLetters[k] = new HeaderLetter(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_TEXT_G + k));
				this.mHeaderLetters[k].mAngleInc = SexyFramework.Common.DegreesToRadians(SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(10f), ZumasRevenge.Common._M1(15f))) * (float)((SexyFramework.Common.Rand(100) < 50) ? 1 : (-1));
				this.mHeaderLetters[k].mX = (float)array2[k].mX;
				this.mHeaderLetters[k].mY = (float)array2[k].mY;
				if (k == FakeCredits.MAX_HEADER_LETTERS - 1)
				{
					this.mHeaderLetters[k].mHinge = true;
					this.mHeaderLetters[k].mAngleInc = ZumasRevenge.Common._M(-0.1f);
					this.mHeaderLetters[k].mAngleAccel = ZumasRevenge.Common._M(0.01f);
				}
				float num = SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(8f), ZumasRevenge.Common._M1(12f));
				float num2 = 3.14159274f * SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(0f), ZumasRevenge.Common._M1(2f));
				this.mHeaderLetters[k].mVX = num * (float)Math.Cos((double)num2);
				this.mHeaderLetters[k].mVY = -num * (float)Math.Sin((double)num2);
			}
			this.mNextLevelLoaded = false;
			this.mCreditsGameY = (float)(-(float)Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_TEXT_GAME).mHeight);
			this.mCreditsWinY = (float)(-(float)Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_TEXT_YOU).mHeight);
			Point[] array3 = new Point[]
			{
				new Point(1852, 296),
				new Point(2046, 1264),
				new Point(106, 708),
				new Point(1070, 712),
				new Point(1378, 70),
				new Point(502, 832),
				new Point(842, 724),
				new Point(1806, 782),
				new Point(112, 1274),
				new Point(420, 1372),
				new Point(960, 194),
				new Point(1518, 1240),
				new Point(486, 284),
				new Point(6, 98),
				new Point(956, 1260),
				new Point(1396, 648)
			};
			float[] array4 = new float[]
			{
				0.08f, 0.08f, 0.09f, 0.07f, 0.07f, 0.12f, 0.08f, 0.08f, 0.09f, 0.09f,
				0.09f, 0.09f, 0.1f, 0.07f, 0.08f, 0.1f
			};
			int[] array5 = new int[]
			{
				1, 1, 2, 3, 3, 4, 5, 5, 5, 6,
				6, 7, 7, 8, 8, 8
			};
			Point[] array6 = new Point[]
			{
				new Point(1854, 326),
				new Point(int.MaxValue, int.MaxValue),
				new Point(112, 788),
				new Point(int.MaxValue, int.MaxValue),
				new Point(int.MaxValue, int.MaxValue),
				new Point(498, 852),
				new Point(846, 814),
				new Point(1814, 875),
				new Point(int.MaxValue, int.MaxValue),
				new Point(int.MaxValue, int.MaxValue),
				new Point(968, 326),
				new Point(int.MaxValue, int.MaxValue),
				new Point(492, 374),
				new Point(16, 170),
				new Point(int.MaxValue, int.MaxValue),
				new Point(1400, 709)
			};
			FPoint[] array7 = new FPoint[]
			{
				new FPoint(1f, 1f),
				new FPoint(1f, 1f),
				new FPoint(1f, 1f),
				new FPoint(0.56f, 0.88f),
				new FPoint(1f, 1f),
				new FPoint(0.8f, 0.8f),
				new FPoint(0.65f, 0.65f),
				new FPoint(1f, 0.59f),
				new FPoint(1f, 1f),
				new FPoint(0.59f, 1f),
				new FPoint(1f, 1f),
				new FPoint(1f, 1f),
				new FPoint(1f, 1f),
				new FPoint(1f, 1f),
				new FPoint(1f, 1f),
				new FPoint(1f, 1f)
			};
			for (int l = 0; l < array4.Length; l++)
			{
				TikiComponent tikiComponent = new TikiComponent();
				this.mTikis.Add(tikiComponent);
				if (array6[l].mX != 2147483647)
				{
					float num3 = ZumasRevenge.Common._M(2f) * array7[l].mX;
					tikiComponent.mSmoke = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_SMOKEYBREATH").Duplicate();
					float num4 = GameApp.DownScaleNum(1f);
					tikiComponent.mSmoke.mDrawTransform.Scale(num4, num4);
					tikiComponent.mSmoke.mDrawTransform.Scale(num3, num3);
					int num5 = ZumasRevenge.Common._DS(array6[l].mX - 160);
					int num6 = ZumasRevenge.Common._DS(array6[l].mY);
					tikiComponent.mSmoke.mDrawTransform.Translate((float)num5, (float)num6);
				}
				tikiComponent.mBreathTimeline.mHoldLastFrame = true;
				tikiComponent.mBreathTimeline.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_TIKIFACES_1 + (array5[l] - 1));
				tikiComponent.mBreathTimeline.AddPosX(new Component((float)ZumasRevenge.Common._DS(array3[l].mX - 160)));
				tikiComponent.mBreathTimeline.AddPosY(new Component((float)ZumasRevenge.Common._DS(array3[l].mY)));
				tikiComponent.mBreathTimeline.AddOpacity(new Component(array4[l]));
				float num7 = ZumasRevenge.Common._M(1.1f);
				tikiComponent.mBreathTimeline.mOverallXScale = array7[l].mX * num7;
				tikiComponent.mBreathTimeline.mOverallYScale = array7[l].mY * num7;
				int num8 = ZumasRevenge.Common._M(16);
				int num9 = 36 * num8;
				tikiComponent.mBreathTimeline.AddScaleX(new Component(0.8f, 0.81f, 0, 11 * num8));
				tikiComponent.mBreathTimeline.AddScaleX(new Component(0.81f, 0.83f, 11 * num8, 16 * num8));
				tikiComponent.mBreathTimeline.AddScaleX(new Component(0.83f, 0.84f, 16 * num8, 23 * num8));
				tikiComponent.mBreathTimeline.AddScaleX(new Component(0.84f, 0.8f, 23 * num8, num9));
				tikiComponent.mBreathTimeline.AddScaleY(new Component(0.8f, 0.83f, 0, 11 * num8));
				tikiComponent.mBreathTimeline.AddScaleY(new Component(0.83f, 0.84f, 11 * num8, 16 * num8));
				tikiComponent.mBreathTimeline.AddScaleY(new Component(0.84f, 0.85f, 16 * num8, 23 * num8));
				tikiComponent.mBreathTimeline.AddScaleY(new Component(0.85f, 0.8f, 23 * num8, num9));
				tikiComponent.mBreathTimeline.mEndFrame = num9;
			}
		}

		public void Update()
		{
			if (this.mDone)
			{
				return;
			}
			this.mUpdateCount++;
			if (this.mScreenShakeTimer > 0)
			{
				if (--this.mScreenShakeTimer == 0)
				{
					this.mTopWoodShakeY = 0f;
				}
				else
				{
					this.mTopWoodShakeY = (float)SexyFramework.Common.Rand(ZumasRevenge.Common._M(8));
				}
			}
			if (this.mState > 1 && this.mBGAlpha < 255f)
			{
				this.mBGAlpha += ZumasRevenge.Common._M(4f);
				if (this.mBGAlpha > 255f)
				{
					this.mBGAlpha = 255f;
				}
			}
			if (this.mState == 0)
			{
				this.mFrogEffect.Update();
				if (this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime)
				{
					this.mState++;
				}
			}
			else if (this.mState == 1)
			{
				int num = GameApp.gApp.mHeight - Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_WOOD_BOTTOM).mHeight;
				if (this.mBottomWoodBounceDir == 0)
				{
					this.mBottomWoodY -= ZumasRevenge.Common._M(15f);
					this.mTopWoodY += ZumasRevenge.Common._M(30f);
					if (this.mBottomWoodY <= (float)num)
					{
						this.mBottomWoodY = (float)num;
					}
					if (this.mTopWoodY >= (float)ZumasRevenge.Common._M(0))
					{
						this.mTopWoodY = (float)ZumasRevenge.Common._M(0);
						this.mBottomWoodBounceDir = 1;
					}
				}
				else
				{
					this.mBottomWoodBounceCount++;
					if (this.mBottomWoodBounceDir == 1 && this.mBottomWoodBounceCount == FakeCredits.MAX_BOTTOM_WOOD_COUNT)
					{
						this.mBottomWoodBounceDir = -1;
					}
					else if (this.mBottomWoodBounceDir == -1 && this.mBottomWoodBounceCount == FakeCredits.MAX_BOTTOM_WOOD_COUNT * 2)
					{
						GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_MOUTH_CLOSE));
						this.mState++;
						this.mFrogEffect.mFrog.SetAngle(3.14159274f * ZumasRevenge.Common._M(1f));
						this.mFrogEffect.JumpIn(this.mFrogEffect.mFrog, ZumasRevenge.Common._SS(GameApp.gApp.mWidth) / 2 - GameApp.gApp.mBoardOffsetX, ZumasRevenge.Common._M(532));
					}
				}
			}
			else if (this.mState == 2 && ++this.mTimer >= ZumasRevenge.Common._M(50))
			{
				this.mFrogEffect.Update();
				if (this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime)
				{
					this.mState++;
				}
			}
			else if (this.mState == 3)
			{
				this.mTopWoodY -= ZumasRevenge.Common._M(10f);
				if (this.mTopWoodY <= (float)ZumasRevenge.Common._S(ZumasRevenge.Common._M(-210)))
				{
					this.mTopWoodY = (float)ZumasRevenge.Common._S(ZumasRevenge.Common._M(-210));
					this.mState++;
					this.mTimer = 0;
					GameApp.gApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_MUSIC));
				}
			}
			else if (this.mState == 4 && ++this.mTimer >= ZumasRevenge.Common._M(20))
			{
				this.TransitionHeaders();
			}
			else if (this.mState == 5)
			{
				for (int i = 0; i < FakeCredits.MAX_CREDITS; i++)
				{
					this.mCredits[i].mY -= ZumasRevenge.Common._M(0.6f);
				}
				if (this.mCredits[0].mY <= (float)ZumasRevenge.Common._M(-100))
				{
					this.mTimer = 0;
					this.mState++;
				}
			}
			else if (this.mState == 6)
			{
				this.mBossY += ZumasRevenge.Common._M(10f);
				if (this.mBossY >= (float)ZumasRevenge.Common._M(7))
				{
					GameApp.gApp.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_MUSIC));
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_IDOL));
					this.mBossY = (float)ZumasRevenge.Common._M(7);
					this.mScreenShakeTimer = ZumasRevenge.Common._M(100);
					this.mState++;
					this.mCredits[0].mFalling = true;
					this.mCredits[0].mAngleInc = SexyFramework.Common.DegreesToRadians(SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(-0.2f), ZumasRevenge.Common._M1(0.2f)));
					for (int j = 0; j < ZumasRevenge.Common._M(30); j++)
					{
						RockBits rockBits = new RockBits();
						this.mRockBits.Add(rockBits);
						rockBits.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_ROCK1 + SexyFramework.Common.Rand(11));
						rockBits.mX = (float)ZumasRevenge.Common._M(431);
						rockBits.mY = (float)ZumasRevenge.Common._M(166);
						float num2 = SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(6f), ZumasRevenge.Common._M1(8f));
						float num3 = SexyFramework.Common.FloatRange(0f, 6.28318548f);
						rockBits.mVX = num2 * (float)Math.Cos((double)num3);
						rockBits.mVY = num2 * -(float)Math.Sin((double)num3);
						rockBits.mAlpha = 255f;
						rockBits.mGravity = SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(0.2f), ZumasRevenge.Common._M1(0.4f));
					}
				}
			}
			else if (this.mState == 7)
			{
				float num4 = ZumasRevenge.Common._M(4f);
				for (int k = 0; k < FakeCredits.MAX_CREDITS; k++)
				{
					if (!this.mCredits[k].mFalling)
					{
						break;
					}
					this.mCredits[k].mY += num4;
					this.mCredits[k].mAngle += this.mCredits[k].mAngleInc;
					if (k < FakeCredits.MAX_CREDITS - 1 && !this.mCredits[k + 1].mFalling && this.mCredits[k].mY + (float)this.mCredits[k].mImage.mHeight >= this.mCredits[k + 1].mY)
					{
						this.mCredits[k + 1].mFalling = true;
						this.mCredits[k + 1].mAngleInc = SexyFramework.Common.DegreesToRadians(SexyFramework.Common.FloatRange(ZumasRevenge.Common._M(-0.2f), ZumasRevenge.Common._M1(0.2f)));
					}
				}
			}
			else if (this.mState == 8)
			{
				this.mFadeOutAlpha += ZumasRevenge.Common._M(2.5f);
				if (this.mFadeOutAlpha >= 255f)
				{
					this.mFadeOutAlpha = 255f;
					this.mState++;
					this.mTimer = 0;
					SoundAttribs soundAttribs = new SoundAttribs();
					soundAttribs.fadeout = 0.01f;
					GameApp.gApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_IDOL_TALKING), soundAttribs);
				}
			}
			else if (this.mState == 9)
			{
				for (int l = 0; l < this.mTauntText.Count; l++)
				{
					SimpleFadeText simpleFadeText = this.mTauntText[l];
					if (simpleFadeText.mFadeIn)
					{
						simpleFadeText.mAlpha += ZumasRevenge.Common._M(1.5f);
						if (simpleFadeText.mAlpha > 255f)
						{
							simpleFadeText.mAlpha = 255f;
						}
						if (simpleFadeText.mAlpha < (float)ZumasRevenge.Common._M(128))
						{
							break;
						}
					}
					else
					{
						simpleFadeText.mAlpha -= ZumasRevenge.Common._M(2f);
						if (simpleFadeText.mAlpha <= 0f)
						{
							this.mCanDoNextLevel = true;
							this.mState = 10;
							this.mTimer = 0;
						}
					}
				}
				if (Enumerable.Last<SimpleFadeText>(this.mTauntText).mFadeIn && Enumerable.Last<SimpleFadeText>(this.mTauntText).mAlpha >= 255f && ++this.mTimer >= ZumasRevenge.Common._M(300))
				{
					for (int m = 0; m < this.mTauntText.Count; m++)
					{
						this.mTauntText[m].mFadeIn = false;
					}
				}
			}
			else if (this.mState == 10)
			{
				this.mFadeOutAlpha -= ZumasRevenge.Common._M(2.5f);
				if (this.mFadeOutAlpha <= 0f)
				{
					this.mFadeOutAlpha = 0f;
					this.mDone = true;
					GameApp.gApp.GetBoard().mContinueNextLevelOnLoadProfile = false;
					GameApp.gApp.mSoundPlayer.Fade(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_IDOL_TALKING));
				}
			}
			for (int n = 0; n < this.mRockBits.Count; n++)
			{
				RockBits rockBits2 = this.mRockBits[n];
				rockBits2.mVY += rockBits2.mGravity;
				rockBits2.mX += rockBits2.mVX;
				rockBits2.mY += rockBits2.mVY;
				rockBits2.mAlpha -= ZumasRevenge.Common._M(2f);
				if (rockBits2.mAlpha <= 0f)
				{
					this.mRockBits.RemoveAt(n);
					n--;
				}
			}
			if (this.mState >= 7)
			{
				for (int num5 = 0; num5 < FakeCredits.MAX_HEADER_LETTERS; num5++)
				{
					this.mHeaderLetters[num5].mAngle += this.mHeaderLetters[num5].mAngleInc;
					this.mHeaderLetters[num5].mUpdateCount++;
					int num6 = ZumasRevenge.Common._M(20);
					if (num5 == FakeCredits.MAX_HEADER_LETTERS - 1 && this.mHeaderLetters[num5].mAngleInc == 0f)
					{
						this.mHeaderLetters[num5].mY += ZumasRevenge.Common._M(10f);
					}
					else if (num5 == FakeCredits.MAX_HEADER_LETTERS - 1 && this.mHeaderLetters[num5].mUpdateCount == num6)
					{
						this.mHeaderLetters[num5].mSwingCount++;
						this.mHeaderLetters[num5].mUpdateCount = 0;
						this.mHeaderLetters[num5].mAngleInc = ((this.mHeaderLetters[num5].mAngleInc < 0f) ? (-this.mHeaderLetters[num5].mAngleInc / ZumasRevenge.Common._M(2f)) : (-this.mHeaderLetters[num5].mAngleInc));
						if (SexyFramework.Common._eq(this.mHeaderLetters[num5].mAngleInc, 0f, ZumasRevenge.Common._M(0.001f)))
						{
							this.mHeaderLetters[num5].mAngleInc = 0f;
						}
					}
					else if (num5 < FakeCredits.MAX_HEADER_LETTERS - 1)
					{
						this.mHeaderLetters[num5].mX += this.mHeaderLetters[num5].mVX;
						this.mHeaderLetters[num5].mY += this.mHeaderLetters[num5].mVY;
					}
				}
			}
			if (this.mHeaderLetters[FakeCredits.MAX_HEADER_LETTERS - 1].mY > (float)GameApp.gApp.mHeight && this.mState < 8)
			{
				this.mState = 8;
				this.mFadeOutAlpha = 0f;
			}
			if (this.mState >= 9)
			{
				List<int> list = new List<int>();
				for (int num7 = 0; num7 < this.mTikis.Count; num7++)
				{
					TikiComponent tikiComponent = this.mTikis[num7];
					if (tikiComponent.mCanUpdate)
					{
						if (tikiComponent.mBreathTimeline.Done() || tikiComponent.mBreathTimeline.GetUpdateCount() >= ZumasRevenge.Common._M(138))
						{
							tikiComponent.mSmoke.Update();
						}
						tikiComponent.mBreathTimeline.Update();
						if (tikiComponent.mSmoke.mFrameNum >= (float)tikiComponent.mSmoke.mLastFrameNum && tikiComponent.mBreathTimeline.Done())
						{
							tikiComponent.mCanUpdate = false;
						}
					}
					else if (tikiComponent.mSmoke != null)
					{
						list.Add(num7);
					}
				}
				if (list.Count > 0 && SexyFramework.Common.Rand(ZumasRevenge.Common._M(10)) == 0)
				{
					TikiComponent tikiComponent2 = this.mTikis[list[SexyFramework.Common.Rand(list.Count)]];
					tikiComponent2.mCanUpdate = true;
					tikiComponent2.mSmoke.ResetAnim();
					tikiComponent2.mBreathTimeline.Reset();
				}
			}
		}

		public void TransitionHeaders()
		{
			int num = 8;
			int num2 = 108;
			float num3 = 5f;
			float num4 = 7.5f;
			if (this.mCreditsGameY < (float)num)
			{
				this.mCreditsGameY += num3;
				if (this.mCreditsGameY >= (float)num)
				{
					int aDelay = (int)((float)(num2 - Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_TEXT_YOU).mHeight) / num4);
					GameApp.gApp.mSoundPlayer.PlayChained(Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_GAMEOVER), Res.GetSoundByID(ResID.SOUND_NEW_FAKE_CREDITS_YOUWIN), aDelay);
					this.mCreditsGameY = (float)num;
					this.mScreenShakeTimer = ZumasRevenge.Common._M(20);
				}
				return;
			}
			if (this.mCreditsWinY < (float)num2)
			{
				this.mCreditsWinY += num4;
				if (this.mCreditsWinY >= (float)num2)
				{
					this.mCreditsWinY = (float)num2;
					this.mScreenShakeTimer = ZumasRevenge.Common._M(20);
					this.mState++;
				}
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mDone)
			{
				return;
			}
			if (!this.mNextLevelLoaded)
			{
				if (this.mState < 9)
				{
					if (this.mState > 1 || this.mBottomWoodBounceDir != 0)
					{
						g.SetColorizeImages(true);
						g.SetColor(255, 255, 255, (int)this.mBGAlpha);
						g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_BACKGROUND), ZumasRevenge.Common._S(-80), 0);
						for (int i = 0; i < FakeCredits.MAX_CREDITS; i++)
						{
							if (this.mCredits[i].mY > 200f && this.mCredits[i].mY < 460f)
							{
								g.DrawImageRotated(this.mCredits[i].mImage, (int)ZumasRevenge.Common._S(this.mCredits[i].mX), (int)ZumasRevenge.Common._S(this.mCredits[i].mY), (double)this.mCredits[i].mAngle);
							}
						}
						g.SetColorizeImages(false);
					}
					int num = ((this.mBottomWoodBounceCount > FakeCredits.MAX_BOTTOM_WOOD_COUNT) ? (FakeCredits.MAX_BOTTOM_WOOD_COUNT * 2 - this.mBottomWoodBounceCount) : this.mBottomWoodBounceCount);
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_WOOD_TOP), ZumasRevenge.Common._S(-80), (int)(this.mTopWoodY + this.mTopWoodShakeY));
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_WOOD_BOTTOM), ZumasRevenge.Common._S(-80), (int)(this.mBottomWoodY + (float)num * ZumasRevenge.Common._M(5f)));
				}
				if (this.mState >= 4 && this.mState < 7)
				{
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_TEXT_GAME), ZumasRevenge.Common._S(ZumasRevenge.Common._M(170)), (int)ZumasRevenge.Common._S(this.mCreditsGameY));
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_TEXT_YOU), ZumasRevenge.Common._S(ZumasRevenge.Common._M(231)), (int)ZumasRevenge.Common._S(this.mCreditsWinY));
				}
				else if (this.mState >= 7)
				{
					for (int j = 0; j < FakeCredits.MAX_HEADER_LETTERS; j++)
					{
						if (j < FakeCredits.MAX_HEADER_LETTERS - 1)
						{
							g.DrawImageRotated(this.mHeaderLetters[j].mImage, (int)ZumasRevenge.Common._S(this.mHeaderLetters[j].mX), (int)ZumasRevenge.Common._S(this.mHeaderLetters[j].mY), (double)this.mHeaderLetters[j].mAngle);
						}
						else
						{
							Transform transform = new Transform();
							transform.Translate((float)(this.mHeaderLetters[j].mImage.mWidth / 2), (float)(-(float)this.mHeaderLetters[j].mImage.mHeight / 2));
							transform.RotateRad(this.mHeaderLetters[j].mAngle);
							transform.Translate((float)(-(float)this.mHeaderLetters[j].mImage.mWidth / 2), (float)(this.mHeaderLetters[j].mImage.mHeight / 2));
							g.DrawImageTransform(this.mHeaderLetters[j].mImage, transform, ZumasRevenge.Common._S(this.mHeaderLetters[j].mX) + (float)(this.mHeaderLetters[j].mImage.mWidth / 2), ZumasRevenge.Common._S(this.mHeaderLetters[j].mY) + (float)(this.mHeaderLetters[j].mImage.mHeight / 2));
						}
					}
				}
				for (int k = 0; k < this.mRockBits.Count; k++)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)this.mRockBits[k].mAlpha);
					g.DrawImage(this.mRockBits[k].mImage, (int)ZumasRevenge.Common._S(this.mRockBits[k].mX), (int)ZumasRevenge.Common._S(this.mRockBits[k].mY));
					g.SetColorizeImages(false);
				}
			}
			if (this.mFadeOutAlpha != 0f)
			{
				g.SetColor(0, 0, 0, (int)this.mFadeOutAlpha);
				g.FillRect(ZumasRevenge.Common._S(-80), 0, GameApp.gApp.mWidth + ZumasRevenge.Common._S(160), GameApp.gApp.mHeight);
			}
			if (this.mState >= 8)
			{
				for (int l = 0; l < this.mTikis.Count; l++)
				{
					this.mTikis[l].mBreathTimeline.mOverallAlphaPct = this.mFadeOutAlpha / 255f;
					this.mTikis[l].mBreathTimeline.Draw(g);
				}
				if (this.mState >= 9)
				{
					if (g.Is3D())
					{
						for (int m = 0; m < this.mTikis.Count; m++)
						{
							TikiComponent tikiComponent = this.mTikis[m];
							if (tikiComponent.mCanUpdate)
							{
								g.PushState();
								tikiComponent.mSmoke.Draw(g);
								g.PopState();
							}
						}
					}
					Font fontByID = Res.GetFontByID(ResID.FONT_BOSS_TAUNT);
					for (int n = 0; n < this.mTauntText.Count; n++)
					{
						if (this.mTauntText[n].mAlpha > 0f)
						{
							g.SetFont(fontByID);
							g.SetColor(255, 255, 255, (int)this.mTauntText[n].mAlpha);
							g.DrawString(this.mTauntText[n].mString, (GameApp.gApp.mWidth - fontByID.StringWidth(this.mTauntText[n].mString)) / 2 - GameApp.gApp.mBoardOffsetX, ZumasRevenge.Common._S(ZumasRevenge.Common._M(300)) + n * fontByID.mHeight);
						}
					}
				}
			}
			if (!this.mNextLevelLoaded)
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_STONE_BOSS);
				g.DrawImage(imageByID, this.mBossX, (int)ZumasRevenge.Common._S(this.mBossY), imageByID.GetCelRect(0));
				int num2 = ZumasRevenge.Common._M(-1);
				int num3 = ZumasRevenge.Common._M(0);
				g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_CREDITS_STONE_BOSS_EYES), (int)((float)this.mBossX + ZumasRevenge.Common._DSA((float)ZumasRevenge.Common._M(50), (float)num2)), (int)(ZumasRevenge.Common._S(this.mBossY) + ZumasRevenge.Common._DSA((float)ZumasRevenge.Common._M1(58), (float)num3)), 0);
			}
			else
			{
				GameApp.gApp.GetBoard().mLevel.mBoss.Draw(g);
			}
			if (this.mState == 0 || this.mState >= 2)
			{
				this.mFrogEffect.Draw(g);
			}
		}

		public bool Done()
		{
			return this.mDone;
		}

		public bool CanStartNextLevel()
		{
			bool flag = this.mCanDoNextLevel;
			if (flag)
			{
				this.mNextLevelLoaded = true;
			}
			this.mCanDoNextLevel = false;
			return flag;
		}

		public bool IsFullyOpaque()
		{
			return (this.mState > 2 || this.mBGAlpha >= 255f) && this.mState < 10;
		}

		public bool HasClosedScene()
		{
			return this.mState >= 2;
		}

		private static int MAX_CREDITS = 10;

		private static int MAX_HEADER_LETTERS = 8;

		private static int MAX_BOTTOM_WOOD_COUNT = 5;

		protected List<TikiComponent> mTikis = new List<TikiComponent>();

		protected List<SimpleFadeText> mTauntText = new List<SimpleFadeText>();

		protected List<RockBits> mRockBits = new List<RockBits>();

		protected FrogFlyOff mFrogEffect;

		protected FakeCreditsText[] mCredits = new FakeCreditsText[FakeCredits.MAX_CREDITS];

		protected HeaderLetter[] mHeaderLetters = new HeaderLetter[FakeCredits.MAX_HEADER_LETTERS];

		protected float mTopWoodY;

		protected float mBottomWoodY;

		protected float mTopWoodShakeY;

		protected float mBGAlpha;

		protected int mBottomWoodBounceDir;

		protected int mBottomWoodBounceCount;

		protected int mState;

		protected int mTimer;

		protected int mUpdateCount;

		protected int mScreenShakeTimer;

		protected float mCreditsGameY;

		protected float mCreditsWinY;

		protected float mBossY;

		protected float mFadeOutAlpha;

		protected bool mDone;

		protected bool mCanDoNextLevel;

		protected bool mNextLevelLoaded;

		public int mBossX;

		public enum State
		{
			State_FrogFlyOut,
			State_WoodClose,
			State_FrogFlyIn,
			State_WoodOpen,
			State_TextDropIn,
			State_ScrollCredits,
			State_DropIn,
			State_TextSmashed,
			State_FadeOut,
			State_TauntText,
			State_NextLevel
		}
	}
}
