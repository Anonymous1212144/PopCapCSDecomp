using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public static class Res
	{
		public static void InitResources(BejeweledLivePlusApp app)
		{
			Res.mApp = app;
			Res.mResMgr = Res.mApp.mResourceManager;
		}

		public static Image GetImageByID(ResourceId id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as Image;
			}
			string text = GlobalMembersResourcesWP.GetStringIdById((int)id);
			if (string.IsNullOrEmpty(text))
			{
				text = id.ToString();
				text = text.Substring(0, text.IndexOf("_ID"));
			}
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				if (Res.mGlobalRes[(int)id].mResObject != null)
				{
					return Res.mGlobalRes[(int)id].mResObject as Image;
				}
				Res.mResMgr.LoadImage(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as Image;
		}

		public static int GetIDByImage(Image img)
		{
			for (int i = 0; i < Res.mGlobalRes.Length; i++)
			{
				if (Res.mGlobalRes[i] != null && Res.mGlobalRes[i].mResObject == img)
				{
					return i;
				}
			}
			return -1;
		}

		public static Font GetFontByID(ResourceId id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as Font;
			}
			string text = id.ToString();
			int num = text.IndexOf("_ID");
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				if (Res.mGlobalRes[(int)id].mResObject != null)
				{
					return Res.mGlobalRes[(int)id].mResObject as Font;
				}
				Res.mResMgr.LoadFont(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as Font;
		}

		public static int GetSoundByID(ResourceId id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return (int)Res.mGlobalRes[(int)id].mResObject;
			}
			string text = id.ToString();
			int num = text.IndexOf("_ID");
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				if (Res.mGlobalRes[(int)id].mResObject != null)
				{
					return (int)Res.mGlobalRes[(int)id].mResObject;
				}
				Res.mResMgr.LoadSound(text);
			}
			return (int)Res.mGlobalRes[(int)id].mResObject;
		}

		public static PIEffect GetPIEffectByID(ResourceId id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as PIEffect;
			}
			string text = id.ToString();
			int num = text.IndexOf("_ID");
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				if (Res.mGlobalRes[(int)id].mResObject != null)
				{
					return Res.mGlobalRes[(int)id].mResObject as PIEffect;
				}
				Res.mResMgr.LoadPIEffect(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as PIEffect;
		}

		public static PopAnim GetPopAnimByID(ResourceId id)
		{
			if (Res.mGlobalRes[(int)id] != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as PopAnim;
			}
			string text = id.ToString();
			int num = text.IndexOf("_ID");
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				if (Res.mGlobalRes[(int)id].mResObject != null)
				{
					return Res.mGlobalRes[(int)id].mResObject as PopAnim;
				}
				Res.mResMgr.LoadPopAnim(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as PopAnim;
		}

		public static int GetOffsetXByID(ResourceId id)
		{
			Point point = Res.mGlobalResOffset[(int)id];
			return Res.mGlobalResOffset[(int)id].mX;
		}

		public static int GetOffsetYByID(ResourceId id)
		{
			Point point = Res.mGlobalResOffset[(int)id];
			return Res.mGlobalResOffset[(int)id].mY;
		}

		private static ResGlobalPtr[] mGlobalRes = new ResGlobalPtr[1810];

		private static Point[] mGlobalResOffset = new Point[1810];

		private static BejeweledLivePlusApp mApp = null;

		private static ResourceManager mResMgr = null;

		public static string STR_NEW_BEST_TIME = "New best time!";

		public static string STR_NEW_HIGH_SCORE = "New high score!";
	}
}
