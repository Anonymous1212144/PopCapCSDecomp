using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class DeathSkull : IDisposable
	{
		public DeathSkull()
		{
			this.mUpdateCount = 0;
			this.mFrogFlyOff = null;
		}

		public virtual void Dispose()
		{
			this.mFrogFlyOff = null;
		}

		public void Init(float start_angle, float sx, float sy, float dx, float dy)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_HOLE_BASE);
			this.mFrogTX = (this.mFrogTY = -1);
			this.mLastFrogAngle = 0f;
			this.mFrogFlyOff = null;
			this.mMouthOpenDelay = Common._M(10);
			this.mOpeningRate = Common._M(0.016f);
			this.mStartingAngle = start_angle;
			this.mCurAngle = start_angle;
			this.mX = (this.mStartX = Common._S(sx) + (float)(imageByID.mWidth / 2) - (float)Common._DS(Common._M(10)));
			this.mY = (this.mStartY = Common._S(sy) + (float)(imageByID.mHeight / 2) + (float)Common._DS(Common._M(45)));
			this.mDestX = Common._S(dx) - (float)Common._S(Common._M(0));
			this.mDestY = Common._S(dy) - (float)Common._S(Common._M(250));
			this.mOpeningMouth = false;
			this.mCloseDelay = 0;
			this.mTextAlpha = 255f;
			this.mDone = false;
			this.mShowText = false;
			this.mFrogClipHeight = 0;
			this.mMoveFrames = Common._M(50);
			this.mVX = (this.mDestX - this.mX) / (float)this.mMoveFrames;
			this.mVY = (this.mDestY - this.mY) / (float)this.mMoveFrames;
			this.mDisappearing = false;
			float canonicalAngleRad = Common.GetCanonicalAngleRad(this.mCurAngle);
			if (6.28318548f - canonicalAngleRad < canonicalAngleRad)
			{
				this.mAngleInc = (6.28318548f - canonicalAngleRad) / (float)this.mMoveFrames;
				this.mAngleOutInc = canonicalAngleRad / (float)this.mMoveFrames;
				return;
			}
			this.mAngleInc = -canonicalAngleRad / (float)this.mMoveFrames;
			this.mAngleOutInc = -(6.28318548f - canonicalAngleRad) / (float)this.mMoveFrames;
		}

		public void Update()
		{
			if (this.mDone)
			{
				return;
			}
			GameApp gApp = GameApp.gApp;
			if (this.mCloseDelay > 0)
			{
				this.mCloseDelay--;
			}
			else if (++this.mUpdateCount == this.mMoveFrames && this.mDisappearing && !this.mShowText)
			{
				if (gApp.GetBoard().IronFrogMode() || gApp.GetBoard().GauntletMode() || gApp.GetBoard().mLevel.IsFinalBossLevel())
				{
					this.mDone = true;
					return;
				}
				Board board = gApp.GetBoard();
				this.mShowText = true;
				this.mTextAlpha = (float)Common._M(500);
				int num = board.GetNumLives() - 1;
				if (board.mLevel.mBoss != null || board.mLevel.IsFinalBossLevel())
				{
					this.mText = TextManager.getInstance().getString(442);
				}
				else if (board.IsCheckpointLevel() && num < 3)
				{
					this.mText = TextManager.getInstance().getString(443);
				}
				else if (num > 1)
				{
					StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(444));
					stringBuilder.Replace("$1", num.ToString());
					this.mText = stringBuilder.ToString();
				}
				else
				{
					if (num != 1)
					{
						this.mShowText = false;
						this.mDone = true;
						return;
					}
					this.mText = TextManager.getInstance().getString(445);
				}
			}
			if (this.mShowText && this.mFrogFlyOff == null && gApp.GetBoard().GetNumLives() > 0 && this.mTextAlpha < 255f && this.mTextAlpha > 0f && !this.mDone)
			{
				this.mFrogFlyOff = new FrogFlyOff();
				Gun gun = gApp.GetBoard().GetGun();
				if (this.mFrogTX == -1)
				{
					this.mFrogTX = gun.GetCenterX();
				}
				if (this.mFrogTY == -1)
				{
					this.mFrogTY = gun.GetCenterY();
				}
				this.mFrogFlyOff.JumpIn(gun, this.mFrogTX, this.mFrogTY, false);
			}
			if (this.mFrogFlyOff != null)
			{
				this.mFrogFlyOff.Update();
				if (this.mFrogFlyOff.mTimer > this.mFrogFlyOff.mFrogJumpTime)
				{
					this.mLastFrogAngle = this.mFrogFlyOff.mFrogAngle;
					this.mFrogFlyOff.Dispose();
					this.mFrogFlyOff = null;
					this.mDone = true;
				}
			}
			else if (gApp.GetBoard().GetNumLives() == 0 && this.mTextAlpha <= 0f)
			{
				this.mDone = true;
			}
			if (this.mShowText && this.mTextAlpha > 0f)
			{
				this.mTextAlpha -= Common._M(3.2f);
				if (this.mTextAlpha < 0f)
				{
					this.mTextAlpha = 0f;
				}
			}
			if (this.mCloseDelay <= 0)
			{
				this.mCurAngle += this.mAngleInc;
			}
			float num2 = (float)this.mUpdateCount / (float)this.mMoveFrames;
			if (this.mUpdateCount < this.mMoveFrames && this.mCloseDelay <= 0)
			{
				this.mX += this.mVX;
				this.mY += this.mVY;
			}
			if (this.mUpdateCount % Common._M(2) == 0 && this.mUpdateCount < this.mMoveFrames && this.mCloseDelay <= 0)
			{
				DeathSkull.SkullFrame skullFrame = new DeathSkull.SkullFrame();
				this.mFrames.Add(skullFrame);
				skullFrame.mAngle = this.mCurAngle;
				if (!this.mDisappearing)
				{
					skullFrame.mPctOpen = 0.41f;
					skullFrame.mSize = 0.31f + 1.8900001f * num2;
				}
				else
				{
					skullFrame.mPctOpen = 0.41f;
					skullFrame.mSize = 2.2f - 1.8900001f * num2;
				}
				skullFrame.mAlpha = 128f;
				skullFrame.mX = this.mX;
				skullFrame.mY = this.mY;
			}
			if (this.mUpdateCount >= this.mMoveFrames && (this.mFrames.Count == 0 || !Enumerable.Last<DeathSkull.SkullFrame>(this.mFrames).mIsFinalFrame) && !this.mDisappearing)
			{
				DeathSkull.SkullFrame skullFrame2 = new DeathSkull.SkullFrame();
				this.mFrames.Add(skullFrame2);
				skullFrame2.mIsFinalFrame = true;
				skullFrame2.mPctOpen = 0.41f;
				skullFrame2.mAngle = (this.mCurAngle = 0f);
				this.mAngleInc = 0f;
				skullFrame2.mSize = 2.2f;
				skullFrame2.mAlpha = 255f;
				skullFrame2.mX = this.mX;
				skullFrame2.mY = this.mY;
			}
			for (int i = 0; i < this.mFrames.Count; i++)
			{
				DeathSkull.SkullFrame skullFrame3 = this.mFrames[i];
				if ((!skullFrame3.mIsFinalFrame || this.mDisappearing) && this.mCloseDelay <= 0)
				{
					skullFrame3.mAlpha -= Common._M(10f);
					if (skullFrame3.mAlpha <= 0f)
					{
						this.mFrames.RemoveAt(i);
						i--;
					}
				}
				else if (this.mMouthOpenDelay > 0 && --this.mMouthOpenDelay == 0)
				{
					this.mOpeningMouth = true;
				}
				else if (this.mOpeningMouth)
				{
					this.mOpeningRate -= Common._M(7E-05f);
					skullFrame3.mPctOpen += this.mOpeningRate;
					if (skullFrame3.mPctOpen >= 1f)
					{
						skullFrame3.mPctOpen = 1f;
						this.mOpeningMouth = false;
						gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_DEATH_SKULL_CHOMP));
					}
				}
				else if (this.mMouthOpenDelay <= 0 && !this.mOpeningMouth)
				{
					float num3 = 0.41f - Common._M(0.04f);
					if (skullFrame3.mPctOpen > num3)
					{
						skullFrame3.mPctOpen -= Common._M(0.12f);
					}
					if (skullFrame3.mPctOpen < num3)
					{
						skullFrame3.mPctOpen = num3;
						gApp.GetBoard().ShakeScreen(Common._M(50), Common._M1(5), Common._M2(5));
						this.mDestX = this.mStartX;
						this.mDestY = this.mStartY;
						this.mVX = (this.mDestX - this.mX) / (float)this.mMoveFrames;
						this.mVY = (this.mDestY - this.mY) / (float)this.mMoveFrames;
						this.mUpdateCount = 0;
						this.mCloseDelay = Common._M(25);
						this.mAngleInc = this.mAngleOutInc;
						this.mDisappearing = true;
					}
					else
					{
						this.mFrogClipHeight -= (int)Common._M(40f);
					}
				}
			}
		}

		public void DrawAboveFrog(Graphics g)
		{
			if (!this.mOpeningMouth && this.mMouthOpenDelay <= 0)
			{
				int num = -1;
				for (int i = 0; i < this.mFrames.Count; i++)
				{
					if (this.mFrames[i].mIsFinalFrame)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					this.DrawFrogItem(g, this.mFrames[num], 1);
				}
			}
			if (this.mShowText && this.mTextAlpha > 0f)
			{
				g.SetColor(255, 0, 0, (this.mTextAlpha > 255f) ? 255 : ((int)this.mTextAlpha));
				g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_STROKE));
				g.WriteString(this.mText, -(int)g.mTransX, (GameApp.gApp.mHeight - g.GetFont().mHeight) / 2, GameApp.gApp.mWidth, 0);
			}
			if (this.mFrogFlyOff != null)
			{
				this.mFrogFlyOff.Draw(g);
			}
		}

		public void DrawBelowFrog(Graphics g)
		{
			int num = -1;
			int num2 = (this.mDisappearing ? (this.mFrames.Count - 1) : 0);
			int num3 = (this.mDisappearing ? (-1) : 1);
			while (this.mDisappearing ? (num2 >= 0) : (num2 < this.mFrames.Count))
			{
				DeathSkull.SkullFrame skullFrame = this.mFrames[num2];
				if (skullFrame.mIsFinalFrame)
				{
					num = num2;
				}
				else
				{
					this.DrawFrogItem(g, skullFrame, 2);
				}
				num2 += num3;
			}
			if (num != -1)
			{
				DeathSkull.SkullFrame s = this.mFrames[num];
				int frog_draw_mode = ((!this.mOpeningMouth && this.mMouthOpenDelay <= 0) ? 0 : 2);
				this.DrawFrogItem(g, s, frog_draw_mode);
			}
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mFrogTX);
			sync.SyncLong(ref this.mFrogTY);
			sync.SyncBoolean(ref this.mDone);
			sync.SyncLong(ref this.mFrogClipHeight);
			sync.SyncFloat(ref this.mLastFrogAngle);
			sync.SyncLong(ref this.mMoveFrames);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mMouthOpenDelay);
			sync.SyncLong(ref this.mCloseDelay);
			sync.SyncBoolean(ref this.mOpeningMouth);
			sync.SyncBoolean(ref this.mDisappearing);
			sync.SyncBoolean(ref this.mShowText);
			sync.SyncFloat(ref this.mTextAlpha);
			sync.SyncFloat(ref this.mOpeningRate);
			sync.SyncFloat(ref this.mStartX);
			sync.SyncFloat(ref this.mStartY);
			sync.SyncFloat(ref this.mDestX);
			sync.SyncFloat(ref this.mDestY);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mAngleInc);
			sync.SyncFloat(ref this.mAngleOutInc);
			sync.SyncFloat(ref this.mCurAngle);
			sync.SyncFloat(ref this.mStartingAngle);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteBoolean(this.mFrogFlyOff != null);
				if (this.mFrogFlyOff != null)
				{
					this.mFrogFlyOff.SyncState(sync);
				}
				buffer.WriteLong((long)this.mFrames.Count);
				for (int i = 0; i < this.mFrames.Count; i++)
				{
					this.mFrames[i].SyncState(sync);
				}
				return;
			}
			this.mFrogFlyOff = null;
			if (buffer.ReadBoolean())
			{
				this.mFrogFlyOff = new FrogFlyOff();
				this.mFrogFlyOff.SyncState(sync);
			}
			this.mFrames.Clear();
			int num = (int)buffer.ReadLong();
			for (int j = 0; j < num; j++)
			{
				DeathSkull.SkullFrame skullFrame = new DeathSkull.SkullFrame();
				skullFrame.SyncState(sync);
				this.mFrames.Add(skullFrame);
			}
			GameApp gApp = GameApp.gApp;
			int num2 = gApp.GetBoard().GetNumLives() - 1;
			if (gApp.GetBoard().mLevel.mBoss != null || gApp.GetBoard().mLevel.IsFinalBossLevel())
			{
				this.mText = TextManager.getInstance().getString(442);
				return;
			}
			if (gApp.GetBoard().IsCheckpointLevel() && num2 < 3)
			{
				this.mText = TextManager.getInstance().getString(443);
				return;
			}
			if (num2 > 1)
			{
				StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(444));
				stringBuilder.Replace("$1", num2.ToString());
				this.mText = stringBuilder.ToString();
				return;
			}
			if (num2 == 1)
			{
				this.mText = TextManager.getInstance().getString(445);
				return;
			}
			this.mText = "";
		}

		protected void DrawFrogItem(Graphics g, DeathSkull.SkullFrame s, int frog_draw_mode)
		{
			int[] array = new int[]
			{
				Common._M(0),
				Common._M1(2),
				Common._M2(0),
				Common._M3(0)
			};
			int[] array2 = new int[]
			{
				Common._M(167),
				Common._M1(200),
				Common._M2(200),
				Common._M3(0)
			};
			Image[] array3 = new Image[]
			{
				Res.GetImageByID(ResID.IMAGE_DEATHSKULL_LARGE_BOTTOM),
				Res.GetImageByID(ResID.IMAGE_DEATHSKULL_LARGE_BLACK),
				Res.GetImageByID(ResID.IMAGE_DEATHSKULL_LARGE_MIDDLE),
				Res.GetImageByID(ResID.IMAGE_DEATHSKULL_LARGE_TOP)
			};
			g.SetColorizeImages(true);
			g.SetColor(255, 255, 255, (int)s.mAlpha);
			int i;
			int num;
			switch (frog_draw_mode)
			{
			case 0:
				i = 0;
				num = 2;
				break;
			case 1:
				i = 2;
				num = 4;
				break;
			default:
				i = 0;
				num = 4;
				break;
			}
			while (i < num)
			{
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.Scale(s.mSize, s.mSize);
				this.mGlobalTranform.RotateRad(s.mAngle);
				Rect celRect = array3[i].GetCelRect(0);
				float num2 = (float)Common._DS(array[i]);
				float num3 = (float)Common._DS(array2[i]);
				if (i == 2 && (!s.mIsFinalFrame || s.mAlpha < 254f))
				{
					int num4 = Common._DS(Common._M(0));
					float num5 = 0.590000033f;
					float num6 = 1f - (s.mPctOpen - 0.41f) / num5;
					float num7 = (float)num4 * num6;
					celRect.mY = (int)num7;
					celRect.mHeight -= (int)num7;
					num3 += num7;
				}
				num2 *= s.mSize;
				num3 *= s.mSize;
				if (i == 1 || i == 2)
				{
					num3 *= s.mPctOpen;
				}
				Common.RotatePoint(s.mAngle, ref num2, ref num3, 0f, 0f);
				this.mGlobalTranform.Translate(num2 + s.mX, num3 + s.mY);
				if (g.Is3D())
				{
					g.DrawImageTransformF(array3[i], this.mGlobalTranform, celRect, 0f, 0f);
				}
				else
				{
					g.DrawImageTransform(array3[i], this.mGlobalTranform, celRect, 0f, 0f);
				}
				i++;
			}
			g.SetColorizeImages(false);
		}

		private const float MIN_MOUTH_OPEN = 0.41f;

		private const float STARTING_SIZE = 0.31f;

		private const float MAX_SIZE = 2.2f;

		protected List<DeathSkull.SkullFrame> mFrames = new List<DeathSkull.SkullFrame>();

		protected float mCurAngle;

		protected float mStartingAngle;

		protected float mAngleInc;

		protected float mAngleOutInc;

		protected float mX;

		protected float mY;

		protected float mVX;

		protected float mVY;

		protected float mDestX;

		protected float mDestY;

		protected float mStartX;

		protected float mStartY;

		protected float mOpeningRate;

		protected float mTextAlpha;

		protected bool mShowText;

		protected bool mDisappearing;

		protected bool mOpeningMouth;

		protected string mText = "";

		protected FrogFlyOff mFrogFlyOff;

		protected int mCloseDelay;

		protected int mMouthOpenDelay;

		protected int mUpdateCount;

		protected int mMoveFrames;

		protected Transform mGlobalTranform = new Transform();

		public int mFrogTX;

		public int mFrogTY;

		public bool mDone;

		public float mLastFrogAngle;

		public int mFrogClipHeight;

		public enum DrawType
		{
			Draw_BG,
			Draw_FG,
			Draw_Both
		}

		protected class SkullFrame
		{
			public SkullFrame()
			{
				this.mPctOpen = 1f;
				this.mAngle = 0f;
				this.mSize = 0f;
				this.mAlpha = 128f;
				this.mIsFinalFrame = false;
				this.mX = 0f;
				this.mY = 0f;
			}

			public void SyncState(DataSync sync)
			{
				sync.SyncFloat(ref this.mPctOpen);
				sync.SyncFloat(ref this.mAngle);
				sync.SyncFloat(ref this.mSize);
				sync.SyncFloat(ref this.mAlpha);
				sync.SyncFloat(ref this.mX);
				sync.SyncFloat(ref this.mY);
				sync.SyncBoolean(ref this.mIsFinalFrame);
			}

			public float mPctOpen;

			public float mAngle;

			public float mSize;

			public float mAlpha;

			public float mX;

			public float mY;

			public bool mIsFinalFrame;
		}
	}
}
