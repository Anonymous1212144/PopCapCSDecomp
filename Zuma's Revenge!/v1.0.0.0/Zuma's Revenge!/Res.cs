using System;
using System.Collections.Generic;
using System.Reflection;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public static class Res
	{
		public static void InitResources(GameApp app)
		{
			Res.mApp = app;
			Res.mResMgr = Res.mApp.mResourceManager;
		}

		public static Image GetImageByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as Image;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] == null)
			{
				List<string> allEnum = Res.GetAllEnum(id);
				for (int i = 0; i < allEnum.Count; i++)
				{
					string theId = allEnum[i];
					Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(theId);
					if (Res.mGlobalRes[(int)id] != null)
					{
						break;
					}
				}
			}
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadImage(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as Image;
		}

		public static List<string> GetAllEnum(ResID id)
		{
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in typeof(ResID).GetFields())
			{
				if (fieldInfo.IsLiteral && typeof(ResID).GetType() == typeof(ResID).GetType() && (int)fieldInfo.GetRawConstantValue() == (int)typeof(ResID).GetField(id.ToString()).GetRawConstantValue())
				{
					list.Add(fieldInfo.Name);
				}
			}
			return list;
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

		public static Font GetFontByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as Font;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadFont(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as Font;
		}

		public static int GetSoundByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return (int)Res.mGlobalRes[(int)id].mResObject;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadSound(text);
			}
			return (int)Res.mGlobalRes[(int)id].mResObject;
		}

		public static PIEffect GetPIEffectByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as PIEffect;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadPIEffect(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as PIEffect;
		}

		public static Effect GetEffectByID(ResID id)
		{
			throw new NotImplementedException();
		}

		public static PopAnim GetPopAnimByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as PopAnim;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadPopAnim(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as PopAnim;
		}

		public static int GetOffsetXByID(ResID id)
		{
			if (Res.mGlobalResOffset[(int)id] != null)
			{
				return Res.mGlobalResOffset[(int)id].mX;
			}
			string theId = id.ToString();
			Point offsetOfImage = Res.mResMgr.GetOffsetOfImage(theId);
			if (offsetOfImage != null)
			{
				Res.mGlobalResOffset[(int)id] = new Point(offsetOfImage);
				return Res.mGlobalResOffset[(int)id].mX;
			}
			return 0;
		}

		public static int GetOffsetYByID(ResID id)
		{
			if (Res.mGlobalResOffset[(int)id] != null)
			{
				return Res.mGlobalResOffset[(int)id].mY;
			}
			string theId = id.ToString();
			Point offsetOfImage = Res.mResMgr.GetOffsetOfImage(theId);
			if (offsetOfImage != null)
			{
				Res.mGlobalResOffset[(int)id] = new Point(offsetOfImage);
				return Res.mGlobalResOffset[(int)id].mY;
			}
			return 0;
		}

		private static ResGlobalPtr[] mGlobalRes = new ResGlobalPtr[1850];

		private static Point[] mGlobalResOffset = new Point[1850];

		private static GameApp mApp = null;

		private static ResourceManager mResMgr = null;
	}
}
