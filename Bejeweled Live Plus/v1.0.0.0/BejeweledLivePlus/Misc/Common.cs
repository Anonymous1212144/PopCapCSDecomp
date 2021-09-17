using System;
using System.Text;
using SexyFramework;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public static class Common
	{
		public static int Rand()
		{
			return (int)Common.CommonMTRand.Next();
		}

		public static int Rand(int range)
		{
			return (int)Common.CommonMTRand.Next((uint)range);
		}

		public static float Rand(float range)
		{
			return Common.CommonMTRand.Next(range);
		}

		public static void SRand(uint theSeed)
		{
			Common.CommonMTRand.SRand(theSeed);
		}

		public static string GetAppDataFolder()
		{
			return GlobalMembers.gFileDriver.GetSaveDataPath();
		}

		public static string URLEncode(string theString)
		{
			char[] array = new char[]
			{
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'A', 'B', 'C', 'D', 'E', 'F'
			};
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < theString.Length)
			{
				char c = theString.get_Chars(i);
				if (c <= ' ')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_6C;
					case '\v':
					case '\f':
						goto IL_9F;
					default:
						if (c != ' ')
						{
							goto IL_9F;
						}
						goto IL_6C;
					}
				}
				else
				{
					switch (c)
					{
					case '%':
					case '&':
						goto IL_6C;
					default:
						if (c == '+' || c == '?')
						{
							goto IL_6C;
						}
						goto IL_9F;
					}
				}
				IL_AD:
				i++;
				continue;
				IL_6C:
				stringBuilder.Append('%');
				stringBuilder.Append(array[(int)((theString.get_Chars(i) >> 4) & '\u000f')]);
				stringBuilder.Append(array[(int)(theString.get_Chars(i) & '\u000f')]);
				goto IL_AD;
				IL_9F:
				stringBuilder.Append(theString.get_Chars(i));
				goto IL_AD;
			}
			return stringBuilder.ToString();
		}

		public static bool Deltree(string thePath)
		{
			return GlobalMembers.gFileDriver.DeleteTree(thePath);
		}

		public static bool FileExists(string theFileName)
		{
			bool flag = false;
			return Common.FileExists(theFileName, ref flag);
		}

		public static bool FileExists(string theFileName, ref bool isFolder)
		{
			return GlobalMembers.gFileDriver.FileExists(theFileName, ref isFolder);
		}

		public static string GetPathFrom(string theRelPath, string theDir)
		{
			return Common.GetPathFrom(theRelPath, theDir);
		}

		public static string GetAppFullPath(string theAppRelPath)
		{
			return Common.GetPathFrom(theAppRelPath, GlobalMembers.gFileDriver.GetLoadDataPath());
		}

		public static void MkDir(string theDir)
		{
			GlobalMembers.gFileDriver.MakeFolders(theDir);
		}

		public static string GetFileName(string thePath)
		{
			return Common.GetFileName(thePath, false);
		}

		public static string GetFileName(string thePath, bool noExtension)
		{
			return Common.GetFileName(thePath, noExtension);
		}

		public static string GetFileDir(string thePath)
		{
			return Common.GetFileDir(thePath, false);
		}

		public static string GetFileDir(string thePath, bool withSlash)
		{
			return Common.GetFileDir(thePath, withSlash);
		}

		public static long GetFileDate(string filename)
		{
			return GlobalMembers.gFileDriver.GetFileTime(filename);
		}

		public static string GetCurDir()
		{
			return GlobalMembers.gFileDriver.GetCurPath();
		}

		public static string GetFullPath(string theRelPath)
		{
			return Common.GetPathFrom(theRelPath, Common.GetCurDir());
		}

		public static MTRand CommonMTRand = new MTRand();
	}
}
