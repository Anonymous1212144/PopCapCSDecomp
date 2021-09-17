﻿using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Sexy
{
	public static class Common
	{
		public static double _wtof(string str)
		{
			return Convert.ToDouble(str);
		}

		public static double _wtof(char str)
		{
			return Convert.ToDouble(str);
		}

		public static int stricmp(string s1, string s2)
		{
			return string.Compare(s1, s2);
		}

		public static void SetCommaSeparator(char theSeparator)
		{
			Common.CommaChar = theSeparator;
		}

		public static string CommaSeperate(int theValue)
		{
			char[] array = new char[20];
			if (theValue < 0)
			{
				array[0] = '\0';
				return Convert.ToString(array);
			}
			int num = 19;
			int num2 = 0;
			array[num--] = '\0';
			do
			{
				if (num2 == 3)
				{
					array[num--] = Common.CommaChar;
					num2 = 0;
				}
				array[num--] = (char)(theValue % 10 + 48);
				theValue /= 10;
				num2++;
			}
			while (theValue >= 0);
			return Convert.ToString(array);
		}

		public static void inlineUpper(ref string theData)
		{
			theData = theData.ToUpper();
		}

		public static void inlineLower(ref string theData)
		{
			theData = theData.ToLower();
		}

		public static string URLEncode(string theString)
		{
			char[] array = new char[]
			{
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'A', 'B', 'C', 'D', 'E', 'F'
			};
			string text = theString;
			int i = 0;
			while (i < theString.Length)
			{
				char c = theString.get_Chars(i);
				char c2 = c;
				if (c2 <= ' ')
				{
					switch (c2)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_8D;
					case '\v':
					case '\f':
						goto IL_D3;
					default:
						if (c2 != ' ')
						{
							goto IL_D3;
						}
						text.Insert(theString.Length - 1, "+");
						break;
					}
				}
				else
				{
					switch (c2)
					{
					case '%':
					case '&':
						goto IL_8D;
					default:
						if (c2 != '+' && c2 != '?')
						{
							goto IL_D3;
						}
						goto IL_8D;
					}
				}
				IL_E6:
				i++;
				continue;
				IL_8D:
				text = text.Insert(theString.Length, "%");
				text = text.Insert(theString.Length, Convert.ToString(array[(int)((c >> 4) & '\u000f')]));
				text = text.Insert(theString.Length, Convert.ToString(array[(int)(c & '\u000f')]));
				goto IL_E6;
				IL_D3:
				text = text.Insert(theString.Length, Convert.ToString(c));
				goto IL_E6;
			}
			return text;
		}

		public static string StringToUpper(ref string theString)
		{
			return theString.ToUpper();
		}

		public static string StringToLower(ref string theString)
		{
			return theString.ToLower();
		}

		public static string Upper(string theString)
		{
			return theString.ToUpper();
		}

		public static string Lower(string theString)
		{
			return theString.ToLower();
		}

		public static string Trim(string theString)
		{
			int num = 0;
			while (num < theString.Length && char.IsWhiteSpace(theString.get_Chars(num)))
			{
				num++;
			}
			int num2 = theString.Length - 1;
			while (num2 >= 0 && char.IsWhiteSpace(theString.get_Chars(num2)))
			{
				num2--;
			}
			return theString.Substring(num, num2 - num + 1);
		}

		public static string ToString(string theString)
		{
			return theString;
		}

		public static char CharAtStringIndex(string theString, int theIndex)
		{
			Debug.ASSERT(theIndex <= theString.Length);
			int num = 0;
			for (int i = 0; i < theString.Length; i++)
			{
				char result = theString.get_Chars(i);
				if (num == theIndex)
				{
					return result;
				}
				num++;
			}
			return '\0';
		}

		public static bool StringToInt(string theString, ref int theIntVal)
		{
			theIntVal = Convert.ToInt32(theString, 10);
			return true;
		}

		public static bool StringToDouble(string theString, ref double theDoubleVal)
		{
			theDoubleVal = Convert.ToDouble(theString);
			return true;
		}

		public static string XMLDecodeString(string theString)
		{
			string text = "";
			for (int i = 0; i < theString.Length; i++)
			{
				sbyte b = (sbyte)Common.CharAtStringIndex(theString, i);
				if (b == 38)
				{
					int num = theString.IndexOf(';', i);
					if (num != -1)
					{
						string text2 = theString.Substring(i + 1, num - i - 1);
						i = num;
						if (text2 == "lt")
						{
							b = 60;
						}
						else if (text2 == "amp")
						{
							b = 38;
						}
						else if (text2 == "gt")
						{
							b = 62;
						}
						else if (text2 == "quot")
						{
							b = 34;
						}
						else if (text2 == "apos")
						{
							b = 39;
						}
						else if (text2 == "nbsp")
						{
							b = 32;
						}
						else if (text2 == "cr")
						{
							b = 10;
						}
					}
				}
				text.Insert(text.Length, Convert.ToString(b));
			}
			return text;
		}

		public static string XMLEncodeString(string theString)
		{
			string text = "";
			bool flag = false;
			int i = 0;
			while (i < theString.Length)
			{
				sbyte b = (sbyte)Common.CharAtStringIndex(theString, i);
				if (b != 32)
				{
					flag = false;
					goto IL_37;
				}
				if (!flag)
				{
					flag = true;
					goto IL_37;
				}
				text += "&nbsp;";
				IL_D9:
				i++;
				continue;
				IL_37:
				sbyte b2 = b;
				if (b2 <= 34)
				{
					if (b2 == 10)
					{
						text += "&cr;";
						goto IL_D9;
					}
					if (b2 == 34)
					{
						text += "&quot;";
						goto IL_D9;
					}
				}
				else
				{
					switch (b2)
					{
					case 38:
						text += "&amp;";
						goto IL_D9;
					case 39:
						text += "&apos;";
						goto IL_D9;
					default:
						switch (b2)
						{
						case 60:
							text += "&lt;";
							goto IL_D9;
						case 62:
							text += "&gt;";
							goto IL_D9;
						}
						break;
					}
				}
				text += b;
				goto IL_D9;
			}
			return text;
		}

		public static string GetFileName(string thePath)
		{
			return Common.GetFileName(thePath, false);
		}

		public static string GetFileName(string thePath, bool noExtension)
		{
			if (noExtension)
			{
				return Path.GetFileNameWithoutExtension(thePath);
			}
			return Path.GetFileName(thePath);
		}

		public static string GetFileDir(string thePath)
		{
			return Common.GetFileDir(thePath, false);
		}

		public static string GetFileDir(string thePath, bool withSlash)
		{
			int num = thePath.LastIndexOf('/');
			string text = thePath.Substring(0, num);
			if (withSlash)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		public static string GetPathFrom(string theRelPath, string theDir)
		{
			return Path.GetFullPath(theRelPath);
		}

		public static string GetFullPath(string theRelPath)
		{
			return Common.GetPathFrom(theRelPath, Common.GetCurDir());
		}

		public static string GetCurDir()
		{
			return Directory.GetCurrentDirectory();
		}

		public static string RemoveTrailingSlash(string theDirectory)
		{
			int length = theDirectory.Length;
			if (length > 0 && theDirectory.get_Chars(length - 1) == '/')
			{
				return theDirectory.Substring(0, length - 1);
			}
			return theDirectory;
		}

		public static string AddTrailingSlash(string theDirectory)
		{
			if (string.IsNullOrEmpty(theDirectory))
			{
				return "";
			}
			sbyte b = (sbyte)theDirectory.get_Chars(theDirectory.Length - 1);
			if (b != 47)
			{
				return theDirectory + '/';
			}
			return theDirectory;
		}

		public static bool FileExists(string theFileName)
		{
			return File.Exists(theFileName);
		}

		public static long GetFileDate(string theFileName)
		{
			Debug.ASSERT(Common.FileExists(theFileName));
			return File.GetLastWriteTime(theFileName).ToFileTime();
		}

		public static void Sleep(uint inTime)
		{
		}

		public static void MkDir(string theDir)
		{
			int num = 0;
			for (;;)
			{
				int num2 = theDir.IndexOf('/', num);
				if (num2 == -1)
				{
					break;
				}
				num = num2 + 1;
				string text = theDir.Substring(0, num2);
				Directory.CreateDirectory(text);
			}
			Directory.CreateDirectory(theDir);
		}

		public static bool Deltree(string thePath)
		{
			if (Directory.Exists(thePath))
			{
				Directory.Delete(thePath, true);
				return true;
			}
			return false;
		}

		public static bool DeleteFile(string lpFileName)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			if (userStoreForApplication.FileExists(lpFileName))
			{
				userStoreForApplication.DeleteFile(lpFileName);
				return true;
			}
			return false;
		}

		public static string GetAppDataFolder()
		{
			return Common.gAppDataFolder;
		}

		public static void SetAppDataFolder(string thePath)
		{
			Common.gAppDataFolder = Common.AddTrailingSlash(thePath);
		}

		public static string StrFormat_(string fmt, params object[] LegacyParamArray)
		{
			return string.Format(fmt, LegacyParamArray);
		}

		public const uint SEXY_RAND_MAX = 2147483647U;

		private static string gAppDataFolder = "boogers";

		internal static char CommaChar = ',';

		public class StringLessNoCase
		{
			public bool Equals(string s1, string s2)
			{
				return Common.stricmp(s1, s2) < 0;
			}
		}

		public class StringEqualNoCase
		{
			public bool Equals(string s1, string s2)
			{
				return Common.stricmp(s1, s2) == 0;
			}
		}

		public class StringGreaterNoCase
		{
			public bool Equals(string s1, string s2)
			{
				return Common.stricmp(s1, s2) > 0;
			}
		}

		public class CharToCharFunc
		{
			public static string Str(string theStr)
			{
				return theStr;
			}

			public static sbyte Char(sbyte theChar)
			{
				return theChar;
			}
		}
	}
}
