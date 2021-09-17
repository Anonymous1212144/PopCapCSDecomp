using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class RollerScore
	{
		private int GetCel(int num)
		{
			if (num < 0)
			{
				return 0;
			}
			if (num % 10 > 0)
			{
				return num;
			}
			return 10;
		}

		private int GetSpeed()
		{
			return 0;
		}

		private void CalculateOffsets()
		{
			int celWidth = this.mRollerImg.GetCelWidth();
			int celHeight = this.mRollerImg.GetCelHeight();
			int celWidth2 = this.mNumbersImg.GetCelWidth();
			int celHeight2 = this.mNumbersImg.GetCelHeight();
			int mX = (int)((float)(celWidth - celWidth2) * 0.5f);
			int mY = (int)((float)(celHeight - celHeight2) * 0.5f);
			if (this.mGauntletMode)
			{
				this.mRollerPos.mX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_SLOTS)) - Common._S(4);
				this.mRollerPos.mY = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_SLOTS));
			}
			else
			{
				this.mRollerPos.mX = (int)((float)Common._S(55) + (float)Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD).mWidth * 0.05f);
				this.mRollerPos.mY = Common._S(10);
			}
			this.mRollerPos.mX = GameApp.gApp.GetWideScreenAdjusted(this.mRollerPos.mX);
			this.mNumberPos.mX = mX;
			this.mNumberPos.mY = mY;
		}

		public RollerScore(bool gauntlet_mode)
		{
			this.Reset(gauntlet_mode);
		}

		public virtual void Dispose()
		{
		}

		public void Reset(bool gauntlet_mode)
		{
			this.mGauntletMode = gauntlet_mode;
			this.mRollerImg = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_SLOTS);
			this.mNumbersImg = Res.GetImageByID(ResID.IMAGE_GAUNTLET_ROLLER_NUMBERS);
			this.CalculateOffsets();
			int num = (this.mGauntletMode ? (-this.mRollerImg.GetCelWidth()) : 0);
			for (int i = 0; i < this.mDigits.Length; i++)
			{
				this.mDigits[i] = new RollerDigit();
				this.mTarget[i] = new RollerDigit();
			}
			for (int j = 6; j >= 0; j--)
			{
				this.mDigits[j].mX = (float)(this.mRollerPos.mX + this.mRollerImg.GetCelWidth() * (6 - j) + this.mNumberPos.mX + num);
				this.mDigits[j].mY = 0f;
				this.mDigits[j].mVY = 0f;
				this.mDigits[j].mNum = -1;
				this.mDigits[j].mDelay = 0;
				this.mDigits[j].mBounceState = 0;
			}
			this.mDigits[0].mNum = (this.mTarget[0].mNum = 0);
			this.mTargetNum = (this.mCurrNum = 0);
			this.mAtTarget = true;
		}

		public void SetTargetScore(int num)
		{
			if (num > 9999999)
			{
				num = 9999999;
			}
			if (num == this.mCurrNum)
			{
				return;
			}
			int i = num - this.mCurrNum;
			int num2 = 1;
			while (i > 0)
			{
				i /= 10;
				if (i > 0)
				{
					num2++;
				}
			}
			this.mTargetNum = num;
			int num3 = 0;
			for (;;)
			{
				int num4 = (int)Math.Pow(10.0, (double)num3);
				int num5 = (int)Math.Pow(10.0, (double)(num3 + 1));
				int num6 = num % num5 / num4;
				this.mTarget[num3].mNum = num6;
				if (this.mDigits[num3].mNum != num6)
				{
					this.mDigits[num3].mDelay = MathUtils.SafeRand() % 25;
					if (this.mGauntletMode)
					{
						this.mDigits[num3].mVY = (float)(num2 + MathUtils.SafeRand() % 2);
						float num7 = 6f;
						if (this.mDigits[num3].mVY > num7)
						{
							this.mDigits[num3].mVY = num7;
						}
					}
					else
					{
						this.mDigits[num3].mVY = (float)(1 + MathUtils.SafeRand() % 2);
					}
					this.mDigits[num3].mBounceState = 0;
				}
				if (num / num5 == 0)
				{
					break;
				}
				num3++;
			}
			for (int j = num3 + 1; j < 7; j++)
			{
				this.mTarget[j].mNum = -1;
			}
			this.mAtTarget = false;
		}

		public void Update()
		{
			if (this.mAtTarget)
			{
				return;
			}
			int num = this.mRollerImg.GetCelHeight() + this.mNumberPos.mY;
			Board board = GameApp.gApp.GetBoard();
			if (board.GauntletMode() && board.mEndGauntletTimer <= 0 && board.mGauntletModeOver && board.mGauntletMultBarAlpha <= 0f)
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CHALLENGE_TALLY_BONUS), Common._M(5));
			}
			bool flag = true;
			for (int i = 6; i >= 0; i--)
			{
				RollerDigit rollerDigit = this.mDigits[i];
				RollerDigit rollerDigit2 = this.mTarget[i];
				if (--rollerDigit.mDelay > 0)
				{
					flag = false;
				}
				else
				{
					rollerDigit.mDelay = 0;
					if (rollerDigit.mVY == 0f)
					{
						if (rollerDigit.mNum != rollerDigit2.mNum)
						{
							flag = false;
						}
					}
					else
					{
						rollerDigit.mY += rollerDigit.mVY;
						if (rollerDigit.mY >= (float)num && rollerDigit.mBounceState == 0)
						{
							rollerDigit.mNum = ((rollerDigit.mNum == -1) ? 1 : ((rollerDigit.mNum + 1) % 10));
							flag = false;
							if (rollerDigit.mNum == rollerDigit2.mNum)
							{
								rollerDigit.mY = (float)this.mNumberPos.mY;
								rollerDigit.mBounceState = 1;
							}
							else
							{
								rollerDigit.mY = (float)num - rollerDigit.mY;
							}
						}
						else if (rollerDigit.mBounceState == 1 && rollerDigit.mY >= (float)Common._S(4))
						{
							flag = false;
							rollerDigit.mBounceState++;
							rollerDigit.mVY *= -1f;
						}
						else if (rollerDigit.mBounceState == 2 && rollerDigit.mY <= (float)Common._S(-3))
						{
							flag = false;
							rollerDigit.mBounceState++;
							rollerDigit.mVY *= -1f;
							rollerDigit.mRestingY = this.mNumberPos.mY;
						}
						else if (rollerDigit.mBounceState == 3 && rollerDigit.mY >= (float)rollerDigit.mRestingY)
						{
							rollerDigit.mY = (float)this.mNumberPos.mY;
							rollerDigit.mVY = 0f;
							rollerDigit.mBounceState = 0;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			this.mAtTarget = flag;
		}

		public void Draw(Graphics g)
		{
			g.PushState();
			int num = this.mRollerImg.GetCelHeight() + this.mNumberPos.mY;
			g.ClipRect(this.mRollerPos.mX, this.mRollerPos.mY, this.mRollerImg.GetWidth(), this.mRollerImg.GetHeight());
			g.DrawImage(this.mRollerImg, this.mRollerPos.mX, this.mRollerPos.mY);
			for (int i = 0; i < 7; i++)
			{
				RollerDigit rollerDigit = this.mDigits[i];
				g.DrawImageCel(this.mNumbersImg, (int)rollerDigit.mX, this.mRollerPos.mY + (int)rollerDigit.mY, this.GetCel(rollerDigit.mNum));
				if (rollerDigit.mY != (float)this.mNumberPos.mY)
				{
					g.DrawImageCel(this.mNumbersImg, (int)rollerDigit.mX, this.mRollerPos.mY + (int)rollerDigit.mY - num, (rollerDigit.mNum == -1) ? this.GetCel(1) : this.GetCel(rollerDigit.mNum + 1));
				}
			}
			g.PopState();
		}

		public void ForceScore(int score)
		{
			if (score > 9999999)
			{
				score = 9999999;
			}
			for (int i = 0; i < 7; i++)
			{
				this.mDigits[i].mNum = -1;
				this.mDigits[i].mVY = (this.mDigits[i].mY = 0f);
				this.mDigits[i].mDelay = (this.mDigits[i].mBounceState = 0);
			}
			for (int j = 0; j < 7; j++)
			{
				int num = (int)Math.Pow(10.0, (double)j);
				int num2 = (int)Math.Pow(10.0, (double)(j + 1));
				this.mDigits[j].mNum = score % num2 / num;
				if (score / num2 == 0)
				{
					break;
				}
			}
			for (int k = 0; k < 7; k++)
			{
				this.mTarget[k] = this.mDigits[k];
			}
			this.mTargetNum = (this.mCurrNum = score);
			this.mAtTarget = true;
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mTargetNum);
			sync.SyncLong(ref this.mCurrNum);
			sync.SyncBoolean(ref this.mAtTarget);
			sync.SyncBoolean(ref this.mGauntletMode);
			for (int i = 0; i < 7; i++)
			{
				this.mDigits[i].SyncState(sync);
				this.mTarget[i].SyncState(sync);
			}
			if (sync.isRead())
			{
				int score = this.mTargetNum;
				this.Reset(this.mGauntletMode);
				this.ForceScore(score);
			}
		}

		public int GetTargetScore()
		{
			return this.mTargetNum;
		}

		public int GetCurrentScore()
		{
			if (this.mCurrNum == this.mTargetNum)
			{
				return this.mCurrNum;
			}
			this.mCurrNum = 0;
			for (int i = 0; i < 7; i++)
			{
				RollerDigit rollerDigit = this.mDigits[i];
				if (rollerDigit.mNum == -1)
				{
					break;
				}
				this.mCurrNum += (int)(Math.Pow(10.0, (double)i) * (double)this.mDigits[i].mNum);
			}
			return this.mCurrNum;
		}

		public bool mAtTarget;

		private RollerDigit[] mDigits = new RollerDigit[7];

		private RollerDigit[] mTarget = new RollerDigit[7];

		private int mTargetNum;

		private int mCurrNum;

		private bool mGauntletMode;

		private Image mNumbersImg;

		private Image mRollerImg;

		private Point mRollerPos = new Point();

		private Point mNumberPos = new Point();
	}
}
