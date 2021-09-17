using System;

namespace Sexy
{
	public class FacebookInterface
	{
		public static FacebookInterface createFacebookInterface()
		{
			return new FacebookInterface();
		}

		~FacebookInterface()
		{
		}

		public virtual void configureServer(string server, string apiKey, string appSecret)
		{
		}

		public virtual string getCurrentServer()
		{
			return "";
		}

		public virtual string getCurrentApiKey()
		{
			return "";
		}

		public virtual string getCurrentAppSecret()
		{
			return "";
		}

		public virtual void addListener(FacebookListener listener)
		{
		}

		public virtual void removeListener(FacebookListener listener)
		{
		}

		public virtual bool resume()
		{
			return true;
		}

		public virtual void login()
		{
		}

		public virtual void logout()
		{
		}

		public virtual void retry()
		{
		}

		public virtual void refresh()
		{
		}

		public virtual void refreshAll()
		{
		}

		public virtual bool isConnected()
		{
			return true;
		}

		public virtual bool networkIsReachable()
		{
			return true;
		}

		public virtual FacebookStatus getStatus()
		{
			return FacebookStatus.FB_LOGGED_OUT;
		}

		public virtual void save()
		{
		}

		public virtual void recordBlitzGame(int score, bool completed, string statsLine)
		{
		}

		public virtual void postLastMedal()
		{
		}

		public virtual DateTime getTournamentStart()
		{
			return DateTime.Now;
		}

		public virtual int getLeaderboardSize()
		{
			return 10;
		}

		public virtual int getLeaderboardRank()
		{
			return 0;
		}

		public virtual string getLeaderboardName(int index)
		{
			return string.Empty;
		}

		public virtual int getLeaderboardScore(int index)
		{
			return 0;
		}

		public virtual Image getLeaderboardPicture(int index)
		{
			return null;
		}

		public virtual void updateScrapbook(int index)
		{
		}

		public virtual bool getScrapbook(int index, BlitzScrapbook scrapbook)
		{
			return true;
		}

		public virtual Image getImageForMedal(int index)
		{
			return null;
		}

		public virtual Image getImageForMedalGold(int index)
		{
			return null;
		}

		public virtual void clearLastGame()
		{
		}

		public virtual int getLastScore()
		{
			return 0;
		}

		public virtual int getLastMedal()
		{
			return 0;
		}

		public virtual bool lastScoreWasNewHigh()
		{
			return true;
		}

		public virtual bool lastMedalWasFresh()
		{
			return true;
		}

		public virtual bool hasNews()
		{
			return false;
		}

		public virtual string getNewsText()
		{
			return string.Empty;
		}

		public static readonly string kLiveServer;

		public static readonly string kLiveApiKey;

		public static readonly string kLiveAppSecret;

		public static readonly string kAlmostServer;

		public static readonly string kAlmostApiKey;

		public static readonly string kAlmostAppSecret;

		public static readonly string kTestServer;

		public static readonly string kTestApiKey;

		public static readonly string kTestAppSecret;
	}
}
