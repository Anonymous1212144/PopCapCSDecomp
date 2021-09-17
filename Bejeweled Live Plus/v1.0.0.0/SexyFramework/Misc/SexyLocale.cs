using System;
using System.Globalization;
using System.Text;

namespace SexyFramework.Misc
{
	public static class SexyLocale
	{
		public static void SetSeperators(string theGrouping, string theSeperator)
		{
			SexyLocale.gGrouping = theGrouping;
			SexyLocale.gThousandSep = theSeperator;
		}

		public static void SetLocale(string theLocale)
		{
		}

		public static string StringToUpper(string theString)
		{
			return theString.ToUpper();
		}

		public static string StringToLower(string theString)
		{
			return theString.ToLower();
		}

		public static bool isalnum(char theChar)
		{
			return char.IsNumber(theChar);
		}

		public static string ReverseString(string str)
		{
			SexyLocale.RS_builder.Clear();
			SexyLocale.RS_builder.Append(str);
			int num = SexyLocale.RS_builder.Length / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = SexyLocale.RS_builder.Length - (i + 1);
				char c = SexyLocale.RS_builder.get_Chars(i);
				SexyLocale.RS_builder.set_Chars(i, SexyLocale.RS_builder.get_Chars(num2));
				SexyLocale.RS_builder.set_Chars(num2, c);
			}
			return SexyLocale.RS_builder.ToString();
		}

		public static string CommaSeparate(int theValue)
		{
			if (theValue < 0)
			{
				return "-" + SexyLocale.UCommaSeparate((uint)(-(uint)theValue));
			}
			return SexyLocale.UCommaSeparate((uint)theValue);
		}

		public static string UCommaSeparate(uint theValue)
		{
			string text = theValue.ToString();
			char[] array = new char[text.Length + (text.Length - 1) / 3];
			string name;
			char c;
			if ((name = CultureInfo.CurrentCulture.Name) != null)
			{
				if (name == "en-US")
				{
					c = SexyLocale.gThousandSep.get_Chars(0);
					goto IL_AF;
				}
				if (name == "de-DE" || name == "es-ES" || name == "it-IT")
				{
					c = SexyLocale.gThousandSep.get_Chars(2);
					goto IL_AF;
				}
				if (name == "fr-FR")
				{
					c = SexyLocale.gThousandSep.get_Chars(1);
					goto IL_AF;
				}
			}
			c = SexyLocale.gThousandSep.get_Chars(0);
			IL_AF:
			int num = 0;
			int num2 = array.Length - 1;
			for (int i = text.Length - 1; i >= 0; i--)
			{
				array[num2--] = text.get_Chars(i);
				num++;
				if (num % 3 == 0 && num2 >= 0)
				{
					array[num2--] = c;
				}
			}
			return new string(array);
		}

		public static string CommaSeparate64(long theValue)
		{
			if (theValue < 0L)
			{
				return "-" + SexyLocale.UCommaSeparate64((ulong)(-(ulong)theValue));
			}
			return SexyLocale.UCommaSeparate64((ulong)theValue);
		}

		public static string UCommaSeparate64(ulong theValue)
		{
			string text = theValue.ToString();
			char[] array = new char[text.Length + (text.Length - 1) / 3];
			string name;
			char c;
			if ((name = CultureInfo.CurrentCulture.Name) != null)
			{
				if (name == "en-US")
				{
					c = SexyLocale.gThousandSep.get_Chars(0);
					goto IL_AF;
				}
				if (name == "de-DE" || name == "es-ES" || name == "it-IT")
				{
					c = SexyLocale.gThousandSep.get_Chars(2);
					goto IL_AF;
				}
				if (name == "fr-FR")
				{
					c = SexyLocale.gThousandSep.get_Chars(1);
					goto IL_AF;
				}
			}
			c = SexyLocale.gThousandSep.get_Chars(0);
			IL_AF:
			int num = 0;
			int num2 = array.Length - 1;
			for (int i = text.Length - 1; i >= 0; i--)
			{
				array[num2--] = text.get_Chars(i);
				num++;
				if (num % 3 == 0 && num2 >= 0)
				{
					array[num2--] = c;
				}
			}
			return new string(array);
		}

		public static string gGrouping = "\\3";

		public static string gThousandSep = ", .";

		private static char CHAR_MAX = '\u007f';

		private static StringBuilder RS_builder = new StringBuilder();
	}
}
