using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class TableOfContents : Widget, ButtonListener
	{
		public TableOfContents(ChallengeMenu aChallengeMenu)
		{
			this.mChallengeMenu = aChallengeMenu;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				this.mChallengeZoneBtns[i] = null;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);
			this.Resize(0, 0, imageByID.GetWidth(), imageByID.GetHeight());
			this.mIsAwardingMedal = false;
			this.mMedalSize = 1f;
			this.mMedalAlpha = 255f;
			this.mAwardedMedal = -1;
			this.mIsAwardAce = false;
			this.mTimer = 0;
			this.mSmokeParticles = new List<LTSmokeParticle>();
		}

		public override void Dispose()
		{
			base.Dispose();
			this.RemoveAllWidgets(false, true);
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				if (this.mChallengeZoneBtns[i] != null)
				{
					this.mChallengeZoneBtns[i].Dispose();
				}
				this.mChallengeZoneBtns[i] = null;
			}
			for (int j = 0; j < this.mSmokeParticles.Count; j++)
			{
				this.mSmokeParticles[j] = null;
			}
			this.mSmokeParticles.Clear();
		}

		public void AwardMedal(int theZone, bool isAced)
		{
			this.mIsAwardingMedal = true;
			this.mMedalSize = 15f;
			this.mMedalAlpha = 0f;
			this.mAwardedMedal = theZone;
			this.mIsAwardAce = isAced;
			this.mTimer = 0;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				IndexMedal indexMedal = this.mChallengeZoneBtns[i];
				if (indexMedal != null)
				{
					indexMedal.SetVisible(false);
					indexMedal.SetDisabled(true);
				}
			}
		}

		public void Init()
		{
			int[,] array = new int[6, 2];
			array[0, 0] = Common._DS(300);
			array[0, 1] = Common._DS(250);
			array[1, 0] = Common._DS(600);
			array[1, 1] = Common._DS(250);
			array[2, 0] = Common._DS(900);
			array[2, 1] = Common._DS(250);
			array[3, 0] = Common._DS(300);
			array[3, 1] = Common._DS(600);
			array[4, 0] = Common._DS(600);
			array[4, 1] = Common._DS(600);
			array[5, 0] = Common._DS(900);
			array[5, 1] = Common._DS(600);
			int[,] array2 = array;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				this.mChallengeZoneBtns[i] = new IndexMedal(this.mChallengeMenu.HasAcedZone(i), 10101 + i, this);
				Image imageByID;
				if (this.mChallengeMenu.HasAcedZone(i))
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1 + (i + 1) * 3);
				}
				else if (this.mChallengeMenu.HasBeatZone(i))
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_STONE + (i + 1) * 3);
				}
				else
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_WOOD + (i + 1) * 3);
				}
				this.mChallengeZoneBtns[i].mButtonImage = imageByID;
				this.mChallengeZoneBtns[i].Resize(array2[i, 0] + GameApp.gApp.GetScreenRect().mX / 2, array2[i, 1], imageByID.GetWidth(), imageByID.GetHeight());
				this.mChallengeZoneBtns[i].SetVisible(true);
				this.mChallengeZoneBtns[i].SetDisabled(false);
				this.mChallengeZoneBtns[i].Init();
				this.AddWidget(this.mChallengeZoneBtns[i]);
			}
		}

		public override void Update()
		{
			if (!this.mIsAwardingMedal)
			{
				return;
			}
			if (GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			for (int i = 0; i < this.mSmokeParticles.Count; i++)
			{
				if (BambooTransition.UpdateSmokeParticle(this.mSmokeParticles[i]))
				{
					this.mSmokeParticles.RemoveAt(i);
					i--;
				}
			}
			this.MarkDirty();
			this.mTimer++;
			int num = Common._M(75) - this.mTimer;
			float num2 = 255f / (float)num;
			this.mMedalAlpha += num2;
			if (this.mMedalAlpha > 255f)
			{
				this.mMedalAlpha = 255f;
			}
			num2 = Common._M(15f) / (float)num;
			this.mMedalSize -= num2;
			if (this.mMedalSize <= 1f)
			{
				if (this.mIsAwardAce)
				{
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_MINI_CROWN_IMPACT));
				}
				else
				{
					GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_ACE_MINI_CROWN_IMPACT));
				}
				GlobalChallenge.gScreenShakeTimer = Common._M(15);
				this.mMedalSize = 1f;
				this.mMedalAlpha = 255f;
				if (GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete)
				{
					GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete = false;
				}
				else if (GameApp.gApp.mUserProfile.mDoChallengeCupComplete)
				{
					GameApp.gApp.mUserProfile.mDoChallengeCupComplete = false;
				}
				Image imageByID;
				if (this.mIsAwardAce)
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1 + (this.mAwardedMedal + 1) * 3);
					this.mChallengeZoneBtns[this.mAwardedMedal].SetAced();
				}
				else
				{
					imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_STONE + (this.mAwardedMedal + 1) * 3);
				}
				this.mChallengeZoneBtns[this.mAwardedMedal].mButtonImage = imageByID;
				this.mIsAwardingMedal = false;
				this.mIsAwardAce = false;
				this.mMedalSize = 1f;
				this.mMedalAlpha = 255f;
				for (int j = 0; j < 40; j++)
				{
					float x = (float)this.mChallengeZoneBtns[this.mAwardedMedal].mX + (float)this.mChallengeZoneBtns[this.mAwardedMedal].mWidth / 2f;
					float y = (float)this.mChallengeZoneBtns[this.mAwardedMedal].mY + (float)this.mChallengeZoneBtns[this.mAwardedMedal].mHeight / 2f;
					this.mSmokeParticles.Add(BambooTransition.SpawnSmokeParticle(x, y, false, false));
				}
				for (int k = 0; k < GlobalChallenge.NUM_CHALLENGE_ZONES; k++)
				{
					ButtonWidget buttonWidget = this.mChallengeZoneBtns[k];
					if (buttonWidget != null)
					{
						buttonWidget.SetVisible(true);
						buttonWidget.SetDisabled(false);
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			string @string = TextManager.getInstance().getString(426);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_STROKE));
			g.SetColor(Color.White);
			float num = (float)g.GetFont().StringWidth(@string);
			g.DrawString(@string, (int)((float)(GameApp.gApp.GetScreenRect().mX + this.mWidth) - num) / 2, Common._DS(150));
			float[,] array = new float[6, 2];
			array[0, 0] = (float)Common._DS(5);
			array[0, 1] = (float)Common._DS(-5);
			array[1, 0] = (float)Common._DS(11);
			array[1, 1] = (float)Common._DS(2);
			array[2, 0] = (float)Common._DS(-8);
			array[2, 1] = (float)Common._DS(5);
			array[3, 0] = (float)Common._DS(-7);
			array[3, 1] = (float)Common._DS(-1);
			array[4, 0] = (float)Common._DS(0);
			array[4, 1] = (float)Common._DS(0);
			array[5, 0] = (float)Common._DS(0);
			array[5, 1] = (float)Common._DS(0);
			float[,] array2 = array;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				if (this.mChallengeZoneBtns[i] != null)
				{
					Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_LEAVES1 + i);
					float num2 = array2[i, 0] + (float)this.mChallengeZoneBtns[i].mX - (float)((imageByID.GetWidth() - this.mChallengeZoneBtns[i].mButtonImage.GetWidth()) / 2);
					float num3 = array2[i, 1] + (float)this.mChallengeZoneBtns[i].mY - (float)((imageByID.GetHeight() - this.mChallengeZoneBtns[i].mButtonImage.GetHeight()) / 2);
					g.DrawImage(imageByID, (int)num2, (int)num3);
					if (this.mIsAwardingMedal)
					{
						g.DrawImage(this.mChallengeZoneBtns[i].mButtonImage, this.mChallengeZoneBtns[i].mX, this.mChallengeZoneBtns[i].mY);
					}
					for (int j = 0; j < this.mSmokeParticles.Count; j++)
					{
						BambooTransition.DrawSmokeParticle(g, this.mSmokeParticles[j]);
					}
				}
			}
			if (this.mIsAwardingMedal)
			{
				g.PushState();
				g.ClearClipRect();
				Image imageByID2;
				if (this.mIsAwardAce)
				{
					imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1 + (this.mAwardedMedal + 1) * 3);
				}
				else
				{
					imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_LEAVES_CUPICON_ZONE_1_STONE + (this.mAwardedMedal + 1) * 3);
				}
				g.SetColor(new Color(255, 255, 255, (int)this.mMedalAlpha));
				g.SetColorizeImages(true);
				SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Scale(this.mMedalSize, this.mMedalSize);
				sexyTransform2D.Translate((float)this.mChallengeZoneBtns[this.mAwardedMedal].mX + ((float)this.mChallengeZoneBtns[this.mAwardedMedal].mButtonImage.mWidth - (float)imageByID2.mWidth * this.mMedalSize) / 2f, (float)this.mChallengeZoneBtns[this.mAwardedMedal].mY + ((float)this.mChallengeZoneBtns[this.mAwardedMedal].mButtonImage.mHeight - (float)imageByID2.mHeight * this.mMedalSize) / 2f);
				g.DrawImageMatrix(imageByID2, sexyTransform2D, (float)imageByID2.mWidth * this.mMedalSize / 2f, (float)imageByID2.mHeight * this.mMedalSize / 2f);
				g.PopState();
			}
		}

		public virtual void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
			switch (id)
			{
			case 10101:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(1, true);
				return;
			case 10102:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(2, true);
				return;
			case 10103:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(3, true);
				return;
			case 10104:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(4, true);
				return;
			case 10105:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(5, true);
				return;
			case 10106:
				this.mChallengeMenu.mChallengeScrollWidget.SetPageHorizontal(6, true);
				return;
			default:
				return;
			}
		}

		public virtual void ButtonDownTick(int x)
		{
		}

		public virtual void ButtonMouseEnter(int x)
		{
		}

		public virtual void ButtonMouseLeave(int x)
		{
		}

		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		public virtual void ButtonPress(int id)
		{
		}

		public virtual void ButtonPress(int id, int count)
		{
		}

		private bool mIsAwardingMedal;

		private bool mIsAwardAce;

		private float mMedalSize;

		private float mMedalAlpha;

		private int mAwardedMedal;

		private int mTimer;

		private IndexMedal[] mChallengeZoneBtns = new IndexMedal[GlobalChallenge.NUM_CHALLENGE_ZONES];

		private ChallengeMenu mChallengeMenu;

		private List<LTSmokeParticle> mSmokeParticles;

		private enum ChallengeZonePages
		{
			ContentId_MettleOfTheMonkey = 10101,
			ContentId_RoosterRumble,
			ContentId_JackalJam,
			ContentId_MarshMadness,
			ContentId_UnderseaUndertaking,
			ContentId_SerpentScuffle,
			NUM_CHALLENGE_ZONE_PAGES
		}
	}
}
