using System;
using BejeweledLivePlus.Bej3Graphics;
using SexyFramework;

namespace BejeweledLivePlus
{
	public class TimeLimitBoard : QuestBoard
	{
		public TimeLimitBoard()
		{
			this.mTimeBonus = 10;
			this.mTimePenalty = 3000;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override void LoadContent(bool threaded)
		{
			base.LoadContent(threaded);
			BejeweledLivePlusApp.LoadContent("GamePlayQuest_TimeLimit");
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			BejeweledLivePlusApp.UnloadContent("GamePlayQuest_TimeLimit");
		}

		public override void NewGame()
		{
			this.NewGame(false);
		}

		public override void NewGame(bool restartingGame)
		{
			if (this.mParams.ContainsKey("Time"))
			{
				this.mTimeLimit = GlobalMembers.sexyatoi(this.mParams["Time"]);
			}
			if (!this.mParams.ContainsKey("TimeBonus") || !int.TryParse(this.mParams["TimeBonus"], ref this.mTimeBonus))
			{
				this.mTimeBonus = 0;
			}
			if (!this.mParams.ContainsKey("TimePenalty") || !int.TryParse(this.mParams["TimePenalty"], ref this.mTimePenalty))
			{
				this.mTimePenalty = 0;
			}
			base.NewGame(restartingGame);
		}

		public override int GetTimeLimit()
		{
			return this.mTimeLimit;
		}

		public override bool WantsHideOnPause()
		{
			return true;
		}

		public override void DoQuestBonus()
		{
			this.DoQuestBonus(1f);
		}

		public override void DoQuestBonus(float iOpt_multiplier)
		{
			int num = (int)((float)this.mTimeBonus * iOpt_multiplier);
			this.mTimeLimit += num;
			TextNotifyEffect textNotifyEffect = this.ShowQuestText(string.Format(GlobalMembers._ID("+{0} SECOND BONUS", 499), num));
			textNotifyEffect.mFont = GlobalMembersResources.FONT_HEADER;
			textNotifyEffect.mY = (float)GlobalMembers.M(1050);
		}

		public override void DoQuestPenalty()
		{
			this.DoQuestPenalty(1f);
		}

		public override void DoQuestPenalty(float iOpt_multiplier)
		{
			int num = (int)((float)this.mTimePenalty * iOpt_multiplier);
			this.mTimeLimit -= num;
			this.ShowQuestText(string.Format(GlobalMembers._ID("-{0} SECONDS PENALTY", 500), num));
		}

		public override void KeyChar(char theChar)
		{
		}

		public int mTimeLimit;

		public int mTimeBonus;

		public int mTimePenalty;
	}
}
