using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BejeweledLIVE;

namespace Sexy
{
	public static class GlobalStaticVars
	{
		public static void initialize(Main main)
		{
			GlobalStaticVars.g = main.graphics;
			GlobalStaticVars.mGlobalContent = new GlobalContentManager(main);
			GlobalStaticVars.gSexyAppBase = new GameApp(main);
			GlobalStaticVars.g.Init();
			GlobalStaticVars.mGlobalContent.initialize();
			GlobalStaticVars.gSexyAppBase.Init();
		}

		public static int timeGetTime()
		{
			return DateTime.Now.Millisecond;
		}

		internal static string GetResourceDir()
		{
			return "";
		}

		internal static string CommaSeperate_(int mDispPoints)
		{
			if (mDispPoints == 0)
			{
				return "0";
			}
			string text;
			if (!GlobalStaticVars.commaSeparatedCache.TryGetValue(mDispPoints, ref text))
			{
				text = string.Format("{0:#,#}", mDispPoints);
				GlobalStaticVars.commaSeparatedCache.Add(mDispPoints, text);
			}
			return text;
		}

		internal static void CommaSeperate_(int mDispPoints, StringBuilder sb)
		{
			sb.Remove(0, sb.Length);
			int i = 1;
			while (mDispPoints / i > 9)
			{
				i *= 10;
			}
			bool flag = true;
			while (i > 0)
			{
				if (!flag && (i == 100 || i == 100000))
				{
					sb.Append(GlobalStaticVars.commaSeparator);
				}
				sb.Append(GlobalStaticVars.DigitStrings[mDispPoints / i]);
				mDispPoints %= i;
				i /= 10;
				flag = false;
			}
		}

		internal static string GetDocumentsDir()
		{
			return "docs";
		}

		private const string commaSeparatorFormat = "{0:#,#}";

		private const string zero = "0";

		public static GlobalContentManager mGlobalContent;

		public static GameApp gSexyAppBase;

		public static Graphics g;

		public static readonly string[] DigitStrings = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		private static Dictionary<int, string> commaSeparatedCache = new Dictionary<int, string>(1000);

		private static readonly string commaSeparator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator;

		public struct CGPoint
		{
			public float x;

			public float y;
		}

		public enum Phase
		{
			TOUCH_BEGAN,
			TOUCH_MOVED,
			TOUCH_STATIONARY,
			TOUCH_ENDED,
			TOUCH_CANCELLED
		}

		public struct Touch
		{
			public GlobalStaticVars.CGPoint location;
		}
	}
}
