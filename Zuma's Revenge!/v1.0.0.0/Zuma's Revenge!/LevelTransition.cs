using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class LevelTransition : IDisposable
	{
		protected void SetupBambooSmoke()
		{
			int i = Common._M(4);
			List<int> list = new List<int>();
			for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
			{
				list.Add(j);
			}
			while (i > 0)
			{
				int num = Common.Rand() % Enumerable.Count<int>(list);
				for (int k = 0; k < Common._M(20); k++)
				{
					BambooColumn bambooColumn = this.mBambooColumns[list[num]];
					bambooColumn.AddSmokeParticle(BambooTransition.SpawnSmokeParticle(bambooColumn.GetColumnX(), bambooColumn.GetCollisionY(), false, false));
				}
				list.RemoveAt(num);
				i--;
			}
		}

		public LevelTransition(int next_level_override, bool dont_record_stats)
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("AdventureStats"))
			{
				GameApp.gApp.mResourceManager.LoadResources("AdventureStats");
			}
			this.mFrog = GameApp.gApp.GetBoard().GetGun();
			this.mFrogEffect = new FrogFlyOff();
			this.Reset(true);
		}

		public LevelTransition(int next_level_override)
			: this(next_level_override, false)
		{
		}

		public LevelTransition()
			: this(-1, false)
		{
		}

		public void Dispose()
		{
			GameApp.gApp.mResourceManager.DeleteResources("AdventureStats");
			this.mFrogEffect = null;
		}

		public bool Update()
		{
			this.mTimer++;
			if (this.mDone)
			{
				return false;
			}
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					bool sound = false;
					if (i == Enumerable.Count<BambooColumn>(this.mBambooColumns) - 1)
					{
						sound = true;
					}
					this.mBambooColumns[i].Update(sound);
				}
			}
			if (this.mState == 0)
			{
				if (this.mIntro)
				{
					this.mFrogEffect.Update();
					for (int j = 0; j < Enumerable.Count<LTSmokeParticle>(this.mFrogSmoke); j++)
					{
						LTSmokeParticle s = this.mFrogSmoke[j];
						if (BambooTransition.UpdateSmokeParticle(s))
						{
							this.mFrogSmoke.RemoveAt(j);
							j--;
						}
					}
				}
				if ((this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime / 2 && this.mIntro) || !this.mIntro)
				{
					if (this.mFrogEffect.HasCompletedFlyOff() && Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
					{
						for (int k = 0; k < Enumerable.Count<BambooColumn>(this.mBambooColumns); k++)
						{
							this.mBambooColumns[k].Close();
						}
					}
					this.mBGAlpha += 255f / (float)this.mBambooTime;
					if (this.mBGAlpha > 255f)
					{
						this.mBGAlpha = 255f;
					}
					bool flag = true;
					if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
					{
						for (int l = 0; l < Enumerable.Count<BambooColumn>(this.mBambooColumns); l++)
						{
							flag &= this.mBambooColumns[l].IsClosed();
						}
					}
					if (flag)
					{
						this.mTimer = 0;
						this.mState++;
						if (GameApp.gApp.mBoard.mLevel.mNum != 10)
						{
							int mNum = GameApp.gApp.mBoard.mLevel.mNum;
							int num = GameApp.gApp.mBoard.mLevel.mZone - 1;
							int num2 = num;
							int index = mNum + num * 10 + num2;
							string text = GameApp.gApp.GetLevelMgr().GetLevelId(index);
							text = char.ToUpper(text.get_Chars(0)) + text.Substring(1);
							string theGroup = "Levels_" + text;
							if (!GameApp.gApp.mResourceManager.IsGroupLoaded(theGroup))
							{
								GameApp.gApp.mResourceManager.PrepareLoadResources(theGroup);
							}
						}
					}
				}
			}
			else
			{
				if (this.mState == 1)
				{
					if (++this.mDelay == Common._M(20))
					{
						this.mTimer = 0;
					}
					this.mFrogEffect.Update();
					if (GameApp.gApp.mBoard.mGameState == GameState.GameState_BossIntro && GameApp.gApp.mBoard.mBossIntroBGAlpha.GetOutVal() == 1.0)
					{
						this.mDone = true;
					}
					return this.mDelay == Common._M(19);
				}
				if (this.mState == 2)
				{
					this.mFrogEffect.Update();
					if ((!this.mIntro && this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime / 2) || this.mIntro)
					{
						this.mBGAlpha -= 255f / (float)this.mBambooTime;
						if (this.mBGAlpha < 0f)
						{
							this.mBGAlpha = 0f;
						}
						bool flag2 = true;
						if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
						{
							for (int m = 0; m < Enumerable.Count<BambooColumn>(this.mBambooColumns); m++)
							{
								flag2 &= this.mBambooColumns[m].IsOpened();
							}
						}
						if (flag2 && (this.mIntro || this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime))
						{
							GameApp.gApp.mBoard.CueLevelTransition();
							this.mDone = true;
							if (!this.mIntro && this.mDrawFrogEffect)
							{
								for (int n = 0; n < Common._M(20); n++)
								{
									this.mFrog.mSmokeParticles.Add(BambooTransition.SpawnSmokeParticle((float)this.mFrog.GetCenterX(), (float)this.mFrog.GetCenterY(), false, true));
								}
							}
						}
					}
				}
			}
			return false;
		}

		public void DrawOverlay(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (this.mDone)
			{
				return;
			}
			if (this.mBGAlpha > 0f)
			{
				g.SetColor(0, 0, 0, (int)this.mBGAlpha);
				g.FillRect(GameApp.gApp.GetScreenRect());
			}
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
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
			if (((this.mState == 0 && this.mIntro) || (this.mState == 2 && !this.mIntro)) && this.mDrawFrogEffect && this.mFrogEffect.mFrogY + (float)(imageByID.mHeight / 2) + (float)Common._M(0) >= 0f)
			{
				if (this.mIntro)
				{
					for (int k = 0; k < Enumerable.Count<LTSmokeParticle>(this.mFrogSmoke); k++)
					{
						BambooTransition.DrawSmokeParticle(g, this.mFrogSmoke[k]);
					}
				}
				this.mFrogEffect.Draw(g);
			}
		}

		public void Draw(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (this.mDone)
			{
				return;
			}
			if (this.mBGAlpha > 0f)
			{
				g.SetColor(0, 0, 0, (int)this.mBGAlpha);
				g.FillRect(GameApp.gApp.GetScreenRect());
			}
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
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
			if (((this.mState == 0 && this.mIntro) || (this.mState == 2 && !this.mIntro)) && this.mDrawFrogEffect && this.mFrogEffect.mFrogY + (float)(imageByID.mHeight / 2) + (float)Common._M(0) >= 0f)
			{
				if (this.mIntro)
				{
					for (int k = 0; k < Enumerable.Count<LTSmokeParticle>(this.mFrogSmoke); k++)
					{
						BambooTransition.DrawSmokeParticle(g, this.mFrogSmoke[k]);
					}
				}
				this.mFrogEffect.Draw(g);
			}
		}

		public void Reset(bool intro)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_A);
			this.mDrawFrogEffect = true;
			this.mIntro = intro;
			this.mState = 0;
			this.mDone = false;
			this.mDelay = 0;
			this.mIntroDelay = 0;
			this.mFrogSmoke.Clear();
			this.mSilent = false;
			if (this.mIntro)
			{
				this.mFrogEffect.JumpOut(this.mFrog);
				for (int i = 0; i < Common._M(20); i++)
				{
					this.mFrogSmoke.Add(BambooTransition.SpawnSmokeParticle(this.mFrogEffect.mFrogX, this.mFrogEffect.mFrogY, true, false));
				}
			}
			this.mBGAlpha = 0f;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) == 0)
			{
				float num = (float)GameApp.gApp.GetScreenRect().mX - 10f;
				for (float num2 = num; num2 <= (float)GameApp.gApp.GetScreenRect().mWidth; num2 += (float)(imageByID.GetWidth() - Common._DS(19)))
				{
					this.mBambooColumns.Add(new BambooColumn());
					Enumerable.Last<BambooColumn>(this.mBambooColumns).SetColumnX(num2);
				}
			}
			else
			{
				for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
				{
					this.mBambooColumns[j].Reset();
				}
			}
			this.SetupBambooSmoke();
			this.mTimer = 0;
		}

		public void RehupFrogPosition()
		{
			this.mFrogEffect.RehupFrogPosition(this.mFrog.GetCenterX(), this.mFrog.GetCenterY());
		}

		public void Open()
		{
			this.mState = 2;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Open();
				}
			}
		}

		public bool IsDone()
		{
			return this.mDone;
		}

		public int GetState()
		{
			return this.mState;
		}

		public List<LTSmokeParticle> mFrogSmoke = new List<LTSmokeParticle>();

		public FrogFlyOff mFrogEffect;

		public Gun mFrog;

		public int mBambooTime;

		public int mDelay;

		public int mState;

		public int mTimer;

		public bool mDone;

		public bool mIntro;

		public float mBGAlpha;

		public List<BambooColumn> mBambooColumns = new List<BambooColumn>();

		public int mIntroDelay;

		public int mNextLevelOverride;

		public bool mDontRecordStats;

		public bool mTransitionToStats;

		public bool mDrawFrogEffect;

		public bool mSilent;

		public bool mDidFirstBounce;

		public enum State
		{
			BambooClose,
			Delay,
			BambooOpen
		}
	}
}
