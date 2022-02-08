using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Sexy;

namespace BejeweledLIVE
{
	public static class LeaderBoardComm
	{
		public static LeaderBoardComm.ConnectionState State { get; private set; } = LeaderBoardComm.ConnectionState.Connecting;

		public static event LeaderBoardComm.LoadingCompletedHandler LoadingCompleted;

		public static Image UnknownPlayerImage
		{
			get
			{
				return AtlasResources.IMAGE_GAMERTAG_UNKNOWN;
			}
		}

		static LeaderBoardComm()
		{
			LeaderBoardComm.leaderboardLoaderClassic.LoadingCompleted += LeaderBoardComm.GetResultsCallBack;
			LeaderBoardComm.leaderboardLoaderAction.LoadingCompleted += LeaderBoardComm.GetResultsCallBack;
		}

		public static void RecordResult(GameMode gameMode, int score)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return;
			}
			if (SexyAppBase.IsInTrialMode)
			{
				return;
			}
			if (gameMode != GameMode.MODE_CLASSIC && gameMode != GameMode.MODE_TIMED)
			{
				return;
			}
			if (Gamer.SignedInGamers.Count == 0)
			{
				return;
			}
			SignedInGamer gamer = Main.GetGamer();
			lock (LeaderBoardComm.LeaderboardLock)
			{
				try
				{
					int gameMode2;
					if (gameMode == GameMode.MODE_CLASSIC)
					{
						gameMode2 = LeaderBoardComm.leaderboardLoaderClassic.GameMode;
					}
					else
					{
						gameMode2 = LeaderBoardComm.leaderboardLoaderAction.GameMode;
					}
					LeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(0, gameMode2);
					LeaderboardWriter leaderboardWriter = gamer.LeaderboardWriter;
					LeaderboardEntry leaderboard = leaderboardWriter.GetLeaderboard(leaderboardIdentity);
					leaderboard.Rating = (long)score;
					LeaderBoardComm.leaderboardLoaderAction.ResetCache();
					LeaderBoardComm.leaderboardLoaderClassic.ResetCache();
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
			}
		}

		public static bool IsPlayer(Gamer signedInGamer, int index, LeaderboardState state)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return false;
			}
			lock (LeaderBoardComm.LeaderboardLock)
			{
				try
				{
					if (signedInGamer == null)
					{
						return false;
					}
					LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
					if (entry != null)
					{
						if (state == LeaderboardState.Classic)
						{
							return signedInGamer.Gamertag == entry.Gamer.Gamertag;
						}
						if (state == LeaderboardState.Action)
						{
							return signedInGamer.Gamertag == entry.Gamer.Gamertag;
						}
					}
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
			}
			return false;
		}

		public static void LoadInitialLeaderboard()
		{
			LeaderBoardComm.LoadResults(GameMode.MODE_CLASSIC);
			LeaderBoardComm.LoadResults(GameMode.MODE_TIMED);
		}

		public static int GetMaxEntries(LeaderboardState state)
		{
			LeaderBoardLoader leaderBoardLoader;
			if (state == LeaderboardState.Classic)
			{
				leaderBoardLoader = LeaderBoardComm.leaderboardLoaderClassic;
			}
			else
			{
				leaderBoardLoader = LeaderBoardComm.leaderboardLoaderAction;
			}
			return leaderBoardLoader.LeaderboardEntryCount;
		}

		public static string GetLeaderboardName(int index, LeaderboardState state)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return string.Empty;
			}
			lock (LeaderBoardComm.LeaderboardLock)
			{
				try
				{
					Gamer gamer = null;
					LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
					if (entry != null)
					{
						if (state == LeaderboardState.Classic)
						{
							gamer = entry.Gamer;
						}
						if (state == LeaderboardState.Action)
						{
							gamer = entry.Gamer;
						}
					}
					if (!string.IsNullOrEmpty(gamer.DisplayName))
					{
						return gamer.DisplayName;
					}
					return gamer.Gamertag;
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
			}
			return string.Empty;
		}

		private static void GetGamerCallBack(IAsyncResult result)
		{
			lock (LeaderBoardComm.LeaderboardLock)
			{
				try
				{
					Gamer gamer = result.AsyncState as Gamer;
					if (gamer != null)
					{
						if (LeaderBoardComm.gamerImages.ContainsKey(gamer.Gamertag))
						{
							LeaderBoardComm.gamerImages.Remove(gamer.Gamertag);
						}
						GamerProfile gamerProfile = gamer.EndGetProfile(result);
						Texture2D theTexture = Texture2D.FromStream(GlobalStaticVars.g.GraphicsDevice, gamerProfile.GetGamerPicture());
						Image image = new Image(theTexture);
						LeaderBoardComm.gamerImages.Add(gamer.Gamertag, image);
					}
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
			}
		}

		public static Image GetGamerImage(Gamer gamer)
		{
			Image unknownPlayerImage = LeaderBoardComm.UnknownPlayerImage;
			if (!SexyAppBase.UseLiveServers || gamer == null)
			{
				return LeaderBoardComm.UnknownPlayerImage;
			}
			lock (LeaderBoardComm.LeaderboardLock)
			{
				if (!LeaderBoardComm.gamerImages.TryGetValue(gamer.Gamertag, ref unknownPlayerImage))
				{
					unknownPlayerImage = LeaderBoardComm.UnknownPlayerImage;
					LeaderBoardComm.gamerImages.Add(gamer.Gamertag, unknownPlayerImage);
					try
					{
						gamer.BeginGetProfile(new AsyncCallback(LeaderBoardComm.GetGamerCallBack), gamer);
					}
					catch (GameUpdateRequiredException)
					{
						GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
					}
					catch (Exception ex)
					{
						LeaderBoardComm.e = ex.Message;
					}
				}
			}
			return unknownPlayerImage;
		}

		public static Image GetLeaderboardGamerImage(int index, LeaderboardState state)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return LeaderBoardComm.UnknownPlayerImage;
			}
			Image gamerImage;
			lock (LeaderBoardComm.LeaderboardLock)
			{
				Gamer gamer = null;
				try
				{
					LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
					if (entry != null)
					{
						if (state == LeaderboardState.Classic)
						{
							gamer = entry.Gamer;
						}
						else
						{
							gamer = entry.Gamer;
						}
					}
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
				gamerImage = LeaderBoardComm.GetGamerImage(gamer);
			}
			return gamerImage;
		}

		public static Gamer GetLeaderboardGamer(int index, LeaderboardState state)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return null;
			}
			Gamer result;
			lock (LeaderBoardComm.LeaderboardLock)
			{
				Gamer gamer = null;
				try
				{
					LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
					if (entry != null)
					{
						if (state == LeaderboardState.Classic)
						{
							gamer = entry.Gamer;
						}
						else
						{
							gamer = entry.Gamer;
						}
					}
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
				result = gamer;
			}
			return result;
		}

		public static int GetSignedInGamerIndex(LeaderboardState state)
		{
			LeaderBoardLoader leaderBoardLoader;
			if (state == LeaderboardState.Classic)
			{
				leaderBoardLoader = LeaderBoardComm.leaderboardLoaderClassic;
			}
			else
			{
				leaderBoardLoader = LeaderBoardComm.leaderboardLoaderAction;
			}
			return leaderBoardLoader.SignedInGamerIndex;
		}

		private static LeaderboardEntry GetEntry(int index, LeaderboardState state)
		{
			LeaderBoardLoader leaderBoardLoader;
			if (state == LeaderboardState.Classic)
			{
				leaderBoardLoader = LeaderBoardComm.leaderboardLoaderClassic;
			}
			else
			{
				leaderBoardLoader = LeaderBoardComm.leaderboardLoaderAction;
			}
			LeaderboardEntry result;
			if (!leaderBoardLoader.LeaderboardEntries.TryGetValue(index, ref result))
			{
				leaderBoardLoader.LoadEntry(index);
			}
			return result;
		}

		public static long GetLeaderboardScore(int index, LeaderboardState state)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return 0L;
			}
			lock (LeaderBoardComm.LeaderboardLock)
			{
				try
				{
					LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
					if (entry != null)
					{
						if (state == LeaderboardState.Classic)
						{
							return entry.Rating;
						}
						if (state == LeaderboardState.Action)
						{
							return entry.Rating;
						}
					}
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception)
				{
				}
			}
			return 0L;
		}

		public static int LoadResults(GameMode gameMode)
		{
			if (!SexyAppBase.UseLiveServers || LeaderBoardComm.State == LeaderBoardComm.ConnectionState.CannotConnect)
			{
				if (LeaderBoardComm.State == LeaderBoardComm.ConnectionState.CannotConnect && (DateTime.Now - LeaderBoardComm.cannotConnectSince).TotalSeconds > 60.0)
				{
					LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connecting;
				}
				return -2;
			}
			lock (LeaderBoardComm.LeaderboardLock)
			{
				if (Gamer.SignedInGamers.Count == 0)
				{
					return -1;
				}
				LeaderBoardLoader leaderBoardLoader;
				if (gameMode == GameMode.MODE_CLASSIC)
				{
					leaderBoardLoader = LeaderBoardComm.leaderboardLoaderClassic;
				}
				else
				{
					leaderBoardLoader = LeaderBoardComm.leaderboardLoaderAction;
				}
				leaderBoardLoader.SendRequest();
				LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connecting;
				if (gameMode == GameMode.MODE_CLASSIC && (LeaderBoardComm.leaderboardLoaderClassic.LeaderboardConnectionState == LeaderBoardLoader.LoaderState.Loaded || (LeaderBoardComm.leaderboardLoaderClassic.LeaderboardConnectionState == LeaderBoardLoader.LoaderState.Loading && LeaderBoardComm.leaderboardLoaderClassic.LeaderboardEntryCount > 0)))
				{
					return LeaderBoardComm.leaderboardLoaderClassic.LeaderboardEntryCount;
				}
				if (gameMode == GameMode.MODE_TIMED && (LeaderBoardComm.leaderboardLoaderAction.LeaderboardConnectionState == LeaderBoardLoader.LoaderState.Loaded || (LeaderBoardComm.leaderboardLoaderAction.LeaderboardConnectionState == LeaderBoardLoader.LoaderState.Loading && LeaderBoardComm.leaderboardLoaderAction.LeaderboardEntryCount > 0)))
				{
					return LeaderBoardComm.leaderboardLoaderAction.LeaderboardEntryCount;
				}
			}
			return -1;
		}

		private static void GetResultsCallBack(LeaderBoardLoader loader)
		{
			lock (LeaderBoardComm.LeaderboardLock)
			{
				switch (loader.ErrorState)
				{
				case LeaderBoardLoader.ErrorStates.None:
					LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connected;
					break;
				case LeaderBoardLoader.ErrorStates.GameUpdateRequired:
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
					break;
				case LeaderBoardLoader.ErrorStates.Error:
					LeaderBoardComm.State = LeaderBoardComm.ConnectionState.CannotConnect;
					LeaderBoardComm.cannotConnectSince = DateTime.Now;
					break;
				}
				if (LeaderBoardComm.LoadingCompleted != null)
				{
					LeaderBoardComm.LoadingCompleted();
				}
			}
		}

		private const int cannotConnectDelay = 60;

		public static object LeaderboardLock = new object();

		private static DateTime cannotConnectSince;

		private static string[] columnIndexStrings = new string[] { "SCORE" };

		private static LeaderBoardLoader leaderboardLoaderClassic = new LeaderBoardLoader(GameMode.MODE_CLASSIC);

		private static LeaderBoardLoader leaderboardLoaderAction = new LeaderBoardLoader(GameMode.MODE_TIMED);

		private static Dictionary<string, Image> gamerImages = new Dictionary<string, Image>();

		private static string e;

		private static List<Gamer> loadingGamers = new List<Gamer>();

		public enum ConnectionState
		{
			Connected,
			Connecting,
			CannotConnect
		}

		private enum LeaderboardMode
		{
			Classic,
			Action,
			MAX
		}

		private enum ColumnIndices
		{
			Score
		}

		public delegate void LoadingCompletedHandler();
	}
}
