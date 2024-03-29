﻿using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class BambooTransition : Widget
	{
		public BambooTransition()
		{
			this.Reset();
			this.mZOrder = int.MaxValue;
			this.mUpdateNum = 0;
		}

		public void Reset()
		{
			this.IMAGE_BAMBOO_PIECE_A = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_A);
			this.IMAGE_BAMBOO_PIECE_B = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_B);
			this.IMAGE_BAMBOO_PIECE_C = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_C);
			this.IMAGE_BAMBOO_PIECE_D = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_D);
			this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT;
			this.mBambooCloseWaitCount = 0;
			this.mLoadStartTime = ulong.MaxValue;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) == 0)
			{
				float num = (float)GameApp.gApp.GetScreenRect().mX - 10f;
				for (float num2 = num; num2 <= (float)GameApp.gApp.GetScreenRect().mWidth; num2 += (float)(this.IMAGE_BAMBOO_PIECE_A.GetWidth() - ZumasRevenge.Common._DS(19)))
				{
					this.mBambooColumns.Add(new BambooColumn());
					Enumerable.Last<BambooColumn>(this.mBambooColumns).SetColumnX(num2);
				}
			}
			else
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Reset();
				}
			}
			this.SetupBambooSmoke();
		}

		public override void Draw(Graphics g)
		{
			if (this.mState != BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT)
			{
				base.DeferOverlay(10);
			}
		}

		public override void DrawOverlay(Graphics g)
		{
			int alpha = (int)(255f * (this.mFadeCount / 40f));
			g.SetColor(0, 0, 0, alpha);
			g.FillRect(GameApp.gApp.GetScreenRect().mX, GameApp.gApp.GetScreenRect().mY, GameApp.gApp.GetScreenRect().mWidth, GameApp.gApp.GetScreenRect().mHeight);
			if (this.mBambooColumns.Count > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Draw(g);
				}
				for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
				{
					this.mBambooColumns[j].DrawSmoke(g);
				}
			}
			this.mUpdateNum = 0;
		}

		public override void Update()
		{
			this.mUpdateNum++;
			for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
			{
				this.mBambooColumns[i].UpdateSmokeParticle();
			}
			if (this.mUpdateNum > 2)
			{
				return;
			}
			for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
			{
				bool sound = false;
				if (j == Enumerable.Count<BambooColumn>(this.mBambooColumns) - 1)
				{
					sound = true;
				}
				this.mBambooColumns[j].Update(sound);
			}
			switch (this.mState)
			{
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSING:
			{
				if (this.mFadeCount < 40f)
				{
					this.mFadeCount += 1UL;
				}
				bool flag = true;
				if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
				{
					for (int k = 0; k < Enumerable.Count<BambooColumn>(this.mBambooColumns); k++)
					{
						flag &= this.mBambooColumns[k].IsClosed();
					}
				}
				if (flag)
				{
					this.mBambooCloseWaitCount++;
					if (this.mBambooCloseWaitCount >= 10)
					{
						this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSED;
						return;
					}
				}
				break;
			}
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSED:
				if (this.mTransitionDelegate != null)
				{
					this.mTransitionDelegate();
				}
				this.mFadeCount = 40UL;
				this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_PAUSE;
				this.mLoadStartTime = (ulong)SexyFramework.Common.SexyTime();
				return;
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_PAUSE:
			{
				ulong num = (ulong)SexyFramework.Common.SexyTime() - this.mLoadStartTime;
				if (num >= 100UL)
				{
					this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPENING;
					if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
					{
						for (int l = 0; l < Enumerable.Count<BambooColumn>(this.mBambooColumns); l++)
						{
							this.mBambooColumns[l].Open();
						}
						return;
					}
				}
				break;
			}
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPENING:
			{
				if (this.mFadeCount > 0UL)
				{
					this.mFadeCount -= 1UL;
				}
				bool flag2 = true;
				if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
				{
					for (int m = 0; m < Enumerable.Count<BambooColumn>(this.mBambooColumns); m++)
					{
						flag2 &= this.mBambooColumns[m].IsOpened();
					}
				}
				if (flag2)
				{
					this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPEN;
					return;
				}
				break;
			}
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPEN:
				this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT;
				GameApp.gApp.BambooTransitionOpened();
				break;
			default:
				return;
			}
		}

		public void StartTransition()
		{
			if (this.mState != BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT)
			{
				Console.WriteLine("\n >>>>> WARNING: Attempting to start bamboo transition while a transition is occurring\n ");
				return;
			}
			this.mFadeCount = 0UL;
			this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSING;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Close();
				}
			}
		}

		public bool IsInProgress()
		{
			return this.mState != BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT;
		}

		private void SetupBambooSmoke()
		{
			int i = ZumasRevenge.Common._M(4);
			List<int> list = new List<int>();
			for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
			{
				list.Add(j);
			}
			while (i > 0)
			{
				int num = SexyFramework.Common.Rand() % Enumerable.Count<int>(list);
				for (int k = 0; k < ZumasRevenge.Common._M(20); k++)
				{
					BambooColumn bambooColumn = this.mBambooColumns[list[num]];
					bambooColumn.AddSmokeParticle(BambooTransition.SpawnSmokeParticle(bambooColumn.GetColumnX(), bambooColumn.GetCollisionY(), false, false));
				}
				list.RemoveAt(num);
				i--;
			}
		}

		public static LTSmokeParticle SpawnSmokeParticle(float x, float y)
		{
			return BambooTransition.SpawnSmokeParticle(x, y, false);
		}

		public static LTSmokeParticle SpawnSmokeParticle(float x, float y, bool fast)
		{
			return BambooTransition.SpawnSmokeParticle(x, y, fast, false);
		}

		public static LTSmokeParticle SpawnSmokeParticle(float x, float y, bool fast, bool slow_fade)
		{
			LTSmokeParticle ltsmokeParticle = new LTSmokeParticle();
			ltsmokeParticle.mX = x;
			ltsmokeParticle.mY = y;
			ltsmokeParticle.mFadingIn = true;
			ltsmokeParticle.mSize = MathUtils.FloatRange(ZumasRevenge.Common._M(0.22f), ZumasRevenge.Common._M1(0.45f));
			float num = (fast ? MathUtils.FloatRange(ZumasRevenge.Common._M(1.5f), ZumasRevenge.Common._M1(2.5f)) : MathUtils.FloatRange(ZumasRevenge.Common._M2(0.75f), ZumasRevenge.Common._M3(1.5f)));
			float num2 = MathUtils.FloatRange(0f, 6.28318548f);
			ltsmokeParticle.mVX = num * (float)Math.Cos((double)num2);
			ltsmokeParticle.mVY = -num * (float)Math.Sin((double)num2);
			ltsmokeParticle.mAlpha.mColor = new FColor(0f, 0f, 0f, 0f);
			ltsmokeParticle.mAlpha.mFadeRate = (float)MathUtils.IntRange(ZumasRevenge.Common._M(10), ZumasRevenge.Common._M1(20));
			ltsmokeParticle.mAlphaFadeOutTime = (slow_fade ? MathUtils.IntRange(ZumasRevenge.Common._M(50), ZumasRevenge.Common._M1(75)) : MathUtils.IntRange(ZumasRevenge.Common._M2(10), ZumasRevenge.Common._M3(20)));
			if (SexyFramework.Common.Rand() % 100 == 0)
			{
				ltsmokeParticle.mColorFader.mColor = (ltsmokeParticle.mColorFader.mMinColor = new FColor(249f, 255f, 249f));
				ltsmokeParticle.mColorFader.mMaxColor = new FColor(205f, 208f, 148f);
			}
			else
			{
				ltsmokeParticle.mColorFader.mColor = (ltsmokeParticle.mColorFader.mMinColor = new FColor(212f, 217f, 212f));
				ltsmokeParticle.mColorFader.mMaxColor = new FColor(153f, 148f, 99f);
			}
			ltsmokeParticle.mColorFader.FadeOverTime((int)((float)ltsmokeParticle.mAlphaFadeOutTime + 255f / ltsmokeParticle.mAlpha.mFadeRate));
			return ltsmokeParticle;
		}

		public static void DrawSmokeParticle(Graphics g, LTSmokeParticle s)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_PARTICLE_FUZZ);
			g.SetColorizeImages(true);
			Color color = s.mColorFader.mColor.ToColor();
			color.mAlpha = (int)s.mAlpha.mColor.mAlpha;
			g.SetColor(color);
			g.DrawImage(imageByID, (int)ZumasRevenge.Common._S(s.mX), (int)ZumasRevenge.Common._S(s.mY), (int)((float)imageByID.mWidth * s.mSize), (int)((float)imageByID.mHeight * s.mSize));
			g.SetColorizeImages(false);
		}

		public static bool UpdateSmokeParticle(LTSmokeParticle s)
		{
			s.mAlpha.Update();
			s.mColorFader.Update();
			s.mX += s.mVX;
			s.mY += s.mVY;
			if (!s.mFadingIn && s.mAlpha.mColor.mAlpha <= 0f)
			{
				return true;
			}
			if (s.mAlpha.mColor.mAlpha == (float)s.mAlpha.mMax && s.mFadingIn)
			{
				s.mFadingIn = false;
				s.mAlpha.mMin = 0;
				s.mAlpha.mFadeRate = -255f / (float)s.mAlphaFadeOutTime;
			}
			return false;
		}

		private BambooTransition.BambooTransitionState mState;

		private List<BambooColumn> mBambooColumns = new List<BambooColumn>();

		private ulong mLoadStartTime;

		private ulong mFadeCount;

		private int mBambooCloseWaitCount;

		public BambooTransition.BambooTransitionDelegate mTransitionDelegate;

		private Image IMAGE_BAMBOO_PIECE_A;

		private Image IMAGE_BAMBOO_PIECE_B;

		private Image IMAGE_BAMBOO_PIECE_C;

		private Image IMAGE_BAMBOO_PIECE_D;

		private int mUpdateNum;

		private enum BambooTransitionState
		{
			BAMBOO_TRANSITION_INIT,
			BAMBOO_TRANSITION_CLOSING,
			BAMBOO_TRANSITION_CLOSED,
			BAMBOO_TRANSITION_PAUSE,
			BAMBOO_TRANSITION_OPENING,
			BAMBOO_TRANSITION_OPEN,
			NUM_BAMBOO_TRANSITION_STATES
		}

		public delegate void BambooTransitionDelegate();
	}
}
