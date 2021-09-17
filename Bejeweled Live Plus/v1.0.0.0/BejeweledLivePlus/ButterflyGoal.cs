using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class ButterflyGoal : QuestGoal
	{
		public ButterflyGoal(QuestBoard theQuestBoard)
			: base(theQuestBoard)
		{
			this.mButterflyCollectedDisplay = 0;
			this.mButterflyCurrentScore = 0;
			this.mCountBeforeUpdate = false;
			this.mSavedCounter = 0;
			this.mScoreScale = 1f;
			this.mScoreIncSize = false;
			this.mScoreDecSize = false;
		}

		public override Rect GetCountdownBarRect()
		{
			return new Rect(0, 0, 0, 0);
		}

		public override void DrawPieces(Graphics g, Piece[] pPieces, int numPieces, bool thePostFX)
		{
		}

		public override void Update()
		{
			if (this.mCountBeforeUpdate && this.mQuestBoard.mUpdateCnt - this.mSavedCounter > 0)
			{
				this.mCountBeforeUpdate = false;
				this.mSavedCounter = 0;
				this.mButterflyCollectedDisplay = this.mButterflyCurrentScore;
				this.mScoreIncSize = true;
			}
			if (this.mQuestBoard.mGameStats[28] > this.mButterflyCurrentScore)
			{
				this.mCountBeforeUpdate = true;
				this.mSavedCounter = this.mQuestBoard.mUpdateCnt;
				this.mButterflyCurrentScore = this.mQuestBoard.mGameStats[28];
			}
			if (this.mScoreIncSize)
			{
				this.mScoreScale += 0.08f;
				if (this.mScoreScale >= 3f)
				{
					this.mScoreIncSize = false;
					this.mScoreDecSize = true;
					return;
				}
			}
			else if (this.mScoreDecSize)
			{
				this.mScoreScale -= 0.08f;
				if (this.mScoreScale <= 1f)
				{
					this.mScoreScale = 1f;
					this.mScoreDecSize = false;
				}
			}
		}

		public override void NewGame()
		{
			if (this.mQuestBoard.mParams.ContainsKey("Butterflies"))
			{
				this.mNumButterflies = GlobalMembers.sexyatoi(this.mQuestBoard.mParams["Butterflies"]);
			}
			this.mButterflyCollectedDisplay = (this.mButterflyCurrentScore = this.mQuestBoard.mGameStats[28]);
			this.mCountBeforeUpdate = false;
			this.mSavedCounter = 0;
			this.mScoreScale = 1f;
			this.mScoreIncSize = (this.mScoreDecSize = false);
		}

		public override void PieceTallied(Piece thePiece)
		{
			if (thePiece.IsFlagSet(128U) && (this.mQuestBoard.mGameOverCount == 0 || this.mQuestBoard.mIsPerpetual))
			{
				this.mQuestBoard.AddToStat(28, 1, thePiece.mMoveCreditId);
				if (!this.mQuestBoard.mIsPerpetual)
				{
					this.mQuestBoard.mPoints++;
					this.mQuestBoard.mLevelPointsTotal++;
				}
			}
			base.PieceTallied(thePiece);
		}

		public override int GetLevelPoints()
		{
			if (this.mQuestBoard.mIsPerpetual)
			{
				return 0;
			}
			return this.mNumButterflies;
		}

		public override bool SaveGameExtra(Serialiser theBuffer)
		{
			int[] array = new int[] { this.mNumButterflies, this.mButterflyCollectedDisplay, this.mButterflyCurrentScore };
			int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkButterflyGoal);
			for (int i = 0; i < array.Length; i++)
			{
				theBuffer.WriteInt32(array[i]);
			}
			theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			return base.SaveGameExtra(theBuffer);
		}

		public override void LoadGameExtra(Serialiser theBuffer)
		{
			int num = 0;
			GameChunkHeader header = new GameChunkHeader();
			if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkButterflyGoal, header, out num))
			{
				return;
			}
			this.mNumButterflies = theBuffer.ReadInt32();
			this.mButterflyCollectedDisplay = theBuffer.ReadInt32();
			this.mButterflyCurrentScore = theBuffer.ReadInt32();
			base.LoadGameExtra(theBuffer);
		}

		public override void DrawOverlay(Graphics g)
		{
			int bfly_SCORE_DISPLAY_X = ConstantsWP.BFLY_SCORE_DISPLAY_X;
			int bfly_SCORE_DISPLAY_Y = ConstantsWP.BFLY_SCORE_DISPLAY_Y;
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.mQuestBoard.GetAlpha())));
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, 0, new Color(255, 255, 255, 255));
			g.WriteString(SexyFramework.Common.CommaSeperate(this.mQuestBoard.mGameStats[1]), bfly_SCORE_DISPLAY_X, bfly_SCORE_DISPLAY_Y, -1, 0);
			g.SetColor(Color.White);
		}

		public override bool DrawScoreWidget(Graphics g)
		{
			return !this.mQuestBoard.mIsPerpetual;
		}

		public override bool DrawScore(Graphics g)
		{
			string theString = SexyFramework.Common.CommaSeperate(this.mButterflyCollectedDisplay);
			g.StringWidth(theString);
			int num_BUTTERFLY_DISPLAY_X = ConstantsWP.NUM_BUTTERFLY_DISPLAY_X;
			int num_BUTTERFLY_DISPLAY_Y = ConstantsWP.NUM_BUTTERFLY_DISPLAY_Y;
			g.PushState();
			g.SetScale(this.mScoreScale, this.mScoreScale, (float)num_BUTTERFLY_DISPLAY_X, (float)(num_BUTTERFLY_DISPLAY_Y - GlobalMembers.S(10)));
			g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.mQuestBoard.GetAlpha())));
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 0, Bej3Widget.COLOR_SUBHEADING_4_STROKE);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 1, Bej3Widget.COLOR_SUBHEADING_4_FILL);
			g.WriteString(SexyFramework.Common.CommaSeperate(this.mButterflyCollectedDisplay), num_BUTTERFLY_DISPLAY_X, num_BUTTERFLY_DISPLAY_Y, -1, 0);
			g.SetColor(Color.White);
			g.PopState();
			return !this.mQuestBoard.mIsPerpetual;
		}

		public override int GetSidebarTextY()
		{
			return GlobalMembers.M(80);
		}

		public int mNumButterflies;

		public int mButterflyCollectedDisplay;

		public int mButterflyCurrentScore;

		public bool mCountBeforeUpdate;

		public int mSavedCounter;

		public float mScoreScale;

		public bool mScoreIncSize;

		public bool mScoreDecSize;
	}
}
