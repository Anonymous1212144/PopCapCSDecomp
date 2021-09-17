using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;

namespace BejeweledLivePlus
{
	public class HighScoreTable
	{
		public int mMode { get; set; }

		public HighScoreTable(string modeKey)
		{
			this.mHighScoresLive = new List<HighScoreEntryLive>();
			this.mModeKey = modeKey;
			this.mMode = GlobalMembers.gApp.ModeHeadingToGameMode(this.mModeKey);
		}

		public bool Submit(string theName, int theValue, int thePicture)
		{
			this.SubmitHighScoreToXBLA(theValue);
			return false;
		}

		public void SubmitHighScoreToXBLA(int theScore)
		{
			try
			{
				SignedInGamer signedInGamer = Gamer.SignedInGamers[0];
				LeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(1, this.mMode);
				LeaderboardEntry leaderboard = signedInGamer.LeaderboardWriter.GetLeaderboard(leaderboardIdentity);
				leaderboard.Rating = (long)theScore;
				leaderboard.Columns.SetValue("TimeStamp", DateTime.Now);
				leaderboard.Columns.SetValue("BestScore", theScore);
			}
			catch (Exception)
			{
			}
		}

		public void ReadLeaderboard(HighScoreTable.HighScoreTableTime t)
		{
			try
			{
				SignedInGamer signedInGamer = Gamer.SignedInGamers[0];
				LeaderboardKey leaderboardKey = ((t == HighScoreTable.HighScoreTableTime.TIME_RECENT) ? 1 : 0);
				LeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(leaderboardKey, this.mMode);
				LeaderboardReader.BeginRead(leaderboardIdentity, signedInGamer, 10, new AsyncCallback(this.LeaderboardReadCallback), signedInGamer);
				this.mLRState = HighScoreTable.LRState.LR_Loading;
				GlobalMembers.isLeaderboardLoading = true;
			}
			catch (Exception)
			{
				this.mLRState = HighScoreTable.LRState.LR_Error;
				GlobalMembers.isLeaderboardLoading = false;
			}
		}

		protected void LeaderboardReadCallback(IAsyncResult result)
		{
			lock (this.mLeaderBoardReadLock)
			{
				SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
				if (signedInGamer != null)
				{
					try
					{
						this.leaderboardReader = LeaderboardReader.EndRead(result);
						this.CanPageUp = this.leaderboardReader.CanPageUp;
						this.CanPageDown = this.leaderboardReader.CanPageDown;
						this.CreateRankList();
						this.mLRState = HighScoreTable.LRState.LR_Ready;
						goto IL_6E;
					}
					catch (Exception)
					{
						this.mLRState = HighScoreTable.LRState.LR_Error;
						goto IL_6E;
					}
				}
				this.mLRState = HighScoreTable.LRState.LR_Error;
				IL_6E:
				GlobalMembers.isLeaderboardLoading = false;
			}
		}

		protected void CreateRankList()
		{
			this.mHighScoresLive.Clear();
			LeaderboardReader leaderboardReader = this.leaderboardReader;
			int count = leaderboardReader.Entries.Count;
			for (int i = 0; i < count; i++)
			{
				LeaderboardEntry liveEntry = leaderboardReader.Entries[i];
				HighScoreEntryLive highScoreEntryLive = new HighScoreEntryLive();
				highScoreEntryLive.Init(liveEntry);
				this.mHighScoresLive.Add(highScoreEntryLive);
			}
		}

		public List<HighScoreEntryLive> mHighScoresLive = new List<HighScoreEntryLive>();

		private string mModeKey = string.Empty;

		public HighScoreMgr mManager;

		public bool CanPageUp;

		public bool CanPageDown;

		private object mLeaderBoardReadLock = new object();

		public HighScoreTable.LRState mLRState;

		protected LeaderboardReader leaderboardReader;

		public enum HighScoreTableTime
		{
			TIME_RECENT,
			TIME_ALLTIME
		}

		public enum LRState
		{
			LR_Idle,
			LR_Loading,
			LR_Ready,
			LR_Error
		}
	}
}
