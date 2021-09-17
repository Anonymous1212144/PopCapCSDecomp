using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Drivers;

namespace ZumasRevenge
{
	public class AutoMonkey
	{
		public AutoMonkey(GameApp app)
		{
			this.mApp = app;
			this.mStateList.Add(MonkeyState.IntroScreen);
			this.mAllModesMode = MonkeyMode.PlayThroughGame;
			this.mAutoMonkeyMode = MonkeyMode.PlayThroughGame;
			this.mLastButtonPress = 0;
			this.mStateCount = 0;
			this.mRandomButtonPress = 0;
			this.mMoveDir = 2;
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_UP);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_DOWN);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_LEFT);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_RIGHT);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_UP);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DOWN);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_LEFT);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_RIGHT);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_BACK);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_START);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_A);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_B);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_X);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_Y);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_LB);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_RB);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_LTRIGGER);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_RTRIGGER);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_LSTICK);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_RSTICK);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_UP);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_DOWN);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_RIGHT);
			this.mAllowedButtons.Add(GamepadButton.GAMEPAD_BUTTON_DPAD_LEFT);
			this.mDirectionButtons.Add(GamepadButton.GAMEPAD_BUTTON_UP);
			this.mDirectionButtons.Add(GamepadButton.GAMEPAD_BUTTON_DOWN);
			this.mDirectionButtons.Add(GamepadButton.GAMEPAD_BUTTON_RIGHT);
			this.mDirectionButtons.Add(GamepadButton.GAMEPAD_BUTTON_LEFT);
			this.mAutoMonkeyDelay = 0.3f;
			this.mEnableAutoMonkey = false;
		}

		~AutoMonkey()
		{
		}

		public void Update()
		{
			this.mLastButtonPress++;
			this.mStateCount++;
			this.mRandomButtonPress++;
			switch (Enumerable.Last<MonkeyState>(this.mStateList))
			{
			case MonkeyState.IntroScreen:
				this.UpdateIntroScreen();
				return;
			case MonkeyState.MainMenu:
				this.UpdateMainMenu();
				return;
			case MonkeyState.ModalOkDialog:
				this.UpdateModalDialog();
				return;
			case MonkeyState.ModalYesNoDialog:
				this.UpdateYesNoDialog();
				return;
			case MonkeyState.PauseDialog:
				break;
			case MonkeyState.Playing:
				this.UpdatePlaying();
				break;
			default:
				return;
			}
		}

		public void SetState(MonkeyState state)
		{
			this.mStateList.Add(state);
			this.mStateCount = 0;
			this.mLastButtonPress = 0;
		}

		public void RemoveLastInstanceOfState(MonkeyState state)
		{
			bool flag = false;
			int num = this.mStateList.Count - 1;
			while (num >= 0 && !flag)
			{
				if (this.mStateList[num] == state)
				{
					this.mStateList.RemoveAt(num);
					flag = true;
				}
				num--;
			}
			if (!flag)
			{
				Console.WriteLine("Unable to find state '{0}' to remove from AutoMonkey!!", this.GetStateString(state));
			}
			this.mStateCount = 0;
			this.mLastButtonPress = 0;
		}

		public MonkeyMode GetMode()
		{
			return this.mAutoMonkeyMode;
		}

		public bool IsEnabled()
		{
			return this.GetMode() != MonkeyMode.Disabled;
		}

		protected void UpdateIntroScreen()
		{
			if (this.mAutoMonkeyDelay <= (float)this.mLastButtonPress / 100f)
			{
				this.PressButtonDown(GamepadButton.GAMEPAD_BUTTON_A, true);
			}
		}

		protected void UpdateMainMenu()
		{
		}

		protected void UpdateModalDialog()
		{
			if (3f <= (float)this.mStateCount / 100f)
			{
				this.PressButton(GamepadButton.GAMEPAD_BUTTON_A, true);
			}
		}

		protected void UpdateYesNoDialog()
		{
			if (3f <= (float)this.mStateCount / 100f)
			{
				if (Common.Rand() % 2 == 0)
				{
					this.PressButton(GamepadButton.GAMEPAD_BUTTON_RIGHT, true);
				}
				else
				{
					this.PressButton(GamepadButton.GAMEPAD_BUTTON_LEFT, true);
				}
				this.PressButton(GamepadButton.GAMEPAD_BUTTON_A, true);
			}
		}

		protected void UpdatePlaying()
		{
			if (this.mApp.mBoard == null)
			{
				return;
			}
			if (this.mApp.mMapScreen != null)
			{
				this.PressButton(GamepadButton.GAMEPAD_BUTTON_A, true);
			}
			bool flag = !this.mApp.mBoard.mDoingFirstTimeIntro && !this.mApp.mBoard.mDoingIronFrogWin && Enumerable.Count<ZumaTip>(this.mApp.mBoard.mZumaTips) == 0 && this.mApp.mBoard.mLevelTransition == null && !this.mApp.mBoard.mShowMapScreen;
			if (this.mAutoMonkeyDelay <= (float)this.mLastButtonPress / 100f)
			{
				if (this.mApp.mBoard.mDoingFirstTimeIntro || this.mApp.mBoard.mDoingIronFrogWin || this.mApp.mBoard.mLevelTransition != null || this.mApp.mBoard.mShowMapScreen)
				{
					this.mApp.mBoard.MouseDown(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
					this.mApp.mBoard.MouseUp(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
					this.PressButton(GamepadButton.GAMEPAD_BUTTON_A, true);
				}
				else if (this.mApp.mBoard.mZumaTips.Count != 0)
				{
					if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.FIRST_SHOT_HINT)
					{
						this.mApp.mBoard.mFrog.SetDestAngle(4.64f);
						this.mApp.mBoard.MouseUp((int)Common._S(this.mApp.mBoard.mFrog.mCurX - 150f), (int)Common._S(this.mApp.mBoard.mFrog.mCurY), 1);
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.ZUMA_BAR_HINT || this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.SKULL_PIT_HINT)
					{
						this.mApp.mBoard.MouseDown(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
						this.mApp.mBoard.MouseUp(GameApp.gApp.GetScreenRect().mWidth / 2, GameApp.gApp.GetScreenRect().mHeight / 2, 1);
						this.PressButton(GamepadButton.GAMEPAD_BUTTON_A, true);
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.LILLY_PAD_HINT)
					{
						if (this.mApp.mBoard.mLevel != null)
						{
							int gunPointFromPos = this.mApp.mBoard.mLevel.GetGunPointFromPos((int)this.mApp.mBoard.mFrog.mCurX, (int)this.mApp.mBoard.mFrog.mCurY);
							int num;
							do
							{
								num = Common.Rand() % this.mApp.mBoard.mLevel.mNumFrogPoints;
							}
							while (num == gunPointFromPos);
							if (num >= 0 && num != this.mApp.mBoard.mLevel.mCurFrogPoint)
							{
								this.mApp.mBoard.mLevel.mCurFrogPoint = num;
								this.mApp.mBoard.mFrog.SetDestPos(this.mApp.mBoard.mLevel.mFrogX[num], this.mApp.mBoard.mLevel.mFrogY[num], this.mApp.mBoard.mLevel.mMoveSpeed, true);
								this.mApp.mBoard.mLevel.ChangedPad(num);
								this.mApp.mUserProfile.MarkHintAsSeen(ZumaProfile.LILLY_PAD_HINT);
								this.mApp.mBoard.mZumaTips.RemoveAt(0);
								if (Enumerable.Count<ZumaTip>(this.mApp.mBoard.mZumaTips) == 0)
								{
									this.mApp.mBoard.mPreventBallAdvancement = false;
								}
							}
						}
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.FRUIT_HINT)
					{
						this.mApp.mBoard.mFrog.SetDestAngle(4.2f);
						this.PressButton(GamepadButton.GAMEPAD_BUTTON_A, true);
						this.mApp.mBoard.MouseUp((int)Common._S(this.mApp.mBoard.mFrog.mCurX - 150f), (int)Common._S(this.mApp.mBoard.mFrog.mCurY - 100f), 1);
					}
					else if (this.mApp.mBoard.mZumaTips[0].mId == ZumaProfile.SWAP_BALL_HINT)
					{
						this.mApp.mBoard.MouseDown((int)Common._S(this.mApp.mBoard.mFrog.mCurX), (int)Common._S(this.mApp.mBoard.mFrog.mCurY), 1);
						this.mApp.mBoard.MouseUp((int)Common._S(this.mApp.mBoard.mFrog.mCurX), (int)Common._S(this.mApp.mBoard.mFrog.mCurY), 1);
					}
				}
				else if (this.mApp.mCredits != null)
				{
					if (this.mApp.mCredits.mInitialDelay >= Common._M(300))
					{
						this.mApp.ReturnFromCredits();
					}
				}
				else if (this.mApp.mBoard.mLevel != null && this.mApp.mBoard.mLevel.mFinalLevel && this.mApp.mBoard.mAdventureWinScreen && this.mApp.mBoard.mAdventureWinAlpha > 0f)
				{
					if (this.mApp.mBoard.mAdvWinBtn != null)
					{
						this.mApp.mBoard.ButtonDepress(this.mApp.mBoard.mAdvWinBtn.mId);
					}
				}
				else
				{
					bool flag2 = true;
					for (int i = 0; i < this.mApp.mBoard.mLevel.mNumCurves; i++)
					{
						if (Enumerable.Count<Bullet>(this.mApp.mBoard.mLevel.mCurveMgr[i].mBulletList) != 0)
						{
							flag2 = false;
							break;
						}
					}
					flag2 = flag2 && Enumerable.Count<Bullet>(this.mApp.mBoard.mBulletList) == 0;
					if (flag2)
					{
						this.mApp.mBoard.mFrog.UpdateAutoMonkeyShotCorrection();
						if (this.mApp.mBoard.mFrog.mShotCorrectionTarget.x != 0f && this.mApp.mBoard.mFrog.mShotCorrectionTarget.y != 0f)
						{
							this.mApp.mBoard.mFrog.SetDestAngle(this.mApp.mBoard.mFrog.mShotCorrectionRad + 1.570795f);
							this.mApp.mBoard.MouseUp((int)(Common._S(this.mApp.mBoard.mFrog.mCurX) + this.mApp.mBoard.mFrog.mShotCorrectionTarget.x), (int)(Common._S(this.mApp.mBoard.mFrog.mCurY) + this.mApp.mBoard.mFrog.mShotCorrectionTarget.y), 1);
						}
						else if (this.mApp.mBoard.mLevel.mNumFrogPoints > 1)
						{
							this.PressButton(GamepadButton.GAMEPAD_BUTTON_Y, true);
						}
						else
						{
							this.PressButton(GamepadButton.GAMEPAD_BUTTON_B, true);
							this.mApp.mBoard.SwapFrogBalls();
						}
					}
				}
			}
			if (flag && this.mApp.mBoard != null && this.mApp.mBoard.mLevel.mMoveType == 1 && this.mApp.mBoard.mLevel.mBoss != null)
			{
				int num2 = this.mApp.mBoard.mLevel.mFrogX[0];
				int num3 = num2 + this.mApp.mBoard.mLevel.mBarWidth;
				int curX = this.mApp.mBoard.mFrog.GetCurX();
				if (curX <= num2)
				{
					this.mMoveDir = 2;
					this.mApp.mBoard.mFrog.SetDestPos(num2 + this.mMoveDir, this.mApp.mBoard.mFrog.GetCurY(), this.mApp.mBoard.mLevel.mMoveSpeed, true);
				}
				else if (curX >= num3)
				{
					this.mMoveDir = -2;
					this.mApp.mBoard.mFrog.SetDestPos(num3 + this.mMoveDir, this.mApp.mBoard.mFrog.GetCurY(), this.mApp.mBoard.mLevel.mMoveSpeed, true);
				}
				this.mApp.mBoard.mFrog.SetDestPos(curX + this.mMoveDir, this.mApp.mBoard.mFrog.GetCurY(), this.mApp.mBoard.mLevel.mMoveSpeed, true);
			}
			if (flag && this.mApp.mBoard != null && this.mApp.mBoard.mCheckpointEffect != null)
			{
				this.mApp.mBoard.mCheckpointEffect.ButtonDepress(0);
			}
			if (!flag && this.mApp.mBoard != null && this.mApp.mBoard.mStatsContinueBtn != null)
			{
				this.mApp.mBoard.ButtonDepress(2);
			}
		}

		protected void PressButtonDown(GamepadButton button, bool bResetTimer)
		{
			this.mApp.GamepadButtonDown(button, 0, 0U);
			if (bResetTimer)
			{
				this.mLastButtonPress = 0;
			}
		}

		protected void PressButtonUp(GamepadButton button, bool bResetTimer)
		{
			this.mApp.GamepadButtonUp(button, 0, 0U);
			if (bResetTimer)
			{
				this.mLastButtonPress = 0;
			}
		}

		protected void PressButton(GamepadButton button, bool bResetTimer)
		{
			this.PressButtonDown(button, bResetTimer);
			this.PressButtonUp(button, bResetTimer);
		}

		public string GetStateString(MonkeyState state)
		{
			switch (state)
			{
			case MonkeyState.IntroScreen:
				return "IntroScreen";
			case MonkeyState.MainMenu:
				return "MainMenu";
			case MonkeyState.ModalOkDialog:
				return "ModalOkDialog";
			case MonkeyState.ModalYesNoDialog:
				return "ModalYesNoDialog";
			case MonkeyState.PauseDialog:
				return "PauseDialog";
			case MonkeyState.Playing:
				return "Playing";
			case MonkeyState.None:
				return "None";
			default:
				return "";
			}
		}

		public string GetButtonString(GamepadButton button)
		{
			switch (button)
			{
			case GamepadButton.GAMEPAD_BUTTON_UP:
				return "GAMEPAD_BUTTON_UP";
			case GamepadButton.GAMEPAD_BUTTON_DOWN:
				return "GAMEPAD_BUTTON_DOWN";
			case GamepadButton.GAMEPAD_BUTTON_LEFT:
				return "GAMEPAD_BUTTON_LEFT";
			case GamepadButton.GAMEPAD_BUTTON_RIGHT:
				return "GAMEPAD_BUTTON_RIGHT";
			case GamepadButton.GAMEPAD_BUTTON_BACK:
				return "GAMEPAD_BUTTON_BACK";
			case GamepadButton.GAMEPAD_BUTTON_START:
				return "GAMEPAD_BUTTON_START";
			case GamepadButton.GAMEPAD_BUTTON_A:
				return "GAMEPAD_BUTTON_A";
			case GamepadButton.GAMEPAD_BUTTON_B:
				return "GAMEPAD_BUTTON_B";
			case GamepadButton.GAMEPAD_BUTTON_X:
				return "GAMEPAD_BUTTON_X";
			case GamepadButton.GAMEPAD_BUTTON_Y:
				return "GAMEPAD_BUTTON_Y";
			case GamepadButton.GAMEPAD_BUTTON_LB:
				return "GAMEPAD_BUTTON_LB";
			case GamepadButton.GAMEPAD_BUTTON_RB:
				return "GAMEPAD_BUTTON_RB";
			case GamepadButton.GAMEPAD_BUTTON_LTRIGGER:
				return "GAMEPAD_BUTTON_LTRIGGER";
			case GamepadButton.GAMEPAD_BUTTON_RTRIGGER:
				return "GAMEPAD_BUTTON_RTRIGGER";
			case GamepadButton.GAMEPAD_BUTTON_LSTICK:
				return "GAMEPAD_BUTTON_LSTICK";
			case GamepadButton.GAMEPAD_BUTTON_RSTICK:
				return "GAMEPAD_BUTTON_RSTICK";
			case GamepadButton.GAMEPAD_BUTTON_DPAD_UP:
				return "GAMEPAD_BUTTON_DPAD_UP";
			case GamepadButton.GAMEPAD_BUTTON_DPAD_DOWN:
				return "GAMEPAD_BUTTON_DPAD_DOWN";
			case GamepadButton.GAMEPAD_BUTTON_DPAD_LEFT:
				return "GAMEPAD_BUTTON_DPAD_LEFT";
			case GamepadButton.GAMEPAD_BUTTON_DPAD_RIGHT:
				return "GAMEPAD_BUTTON_DPAD_RIGHT";
			default:
				return "NONE";
			}
		}

		public MonkeyMode mAutoMonkeyMode;

		public float mAutoMonkeyDelay;

		public bool mEnableAutoMonkey;

		protected GameApp mApp;

		protected List<MonkeyState> mStateList = new List<MonkeyState>();

		protected int mStateCount;

		protected List<GamepadButton> mAllowedButtons = new List<GamepadButton>();

		protected List<GamepadButton> mDirectionButtons = new List<GamepadButton>();

		protected int mLastButtonPress;

		protected int mMoveDir;

		protected int mRandomButtonPress;

		protected MonkeyMode mAllModesMode;
	}
}
