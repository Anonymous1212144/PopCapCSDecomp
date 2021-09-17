using System;
using System.Text;

namespace BejeweledLivePlus
{
	public static class GlobalMembersProfile
	{
		internal static string ToUserDirectoryName(string theString)
		{
			StringBuilder stringBuilder = new StringBuilder(theString);
			for (int i = 0; i < stringBuilder.Length; i++)
			{
				char c = stringBuilder.get_Chars(i);
				if (c != ' ' && c < 'A' && (c < '0' || c > '9'))
				{
					stringBuilder.set_Chars(i, '_');
				}
			}
			return stringBuilder.ToString();
		}

		public const int PROFILE_LIST_VERSION = 8;

		public const int MIN_PROFILE_LIST_VERSION = 8;

		public const int PROFILE_VERSION = 73;

		public const int MIN_PROFILE_VERSION = 71;

		public const int PROFILE_MAGIC = 958131957;
	}
}
