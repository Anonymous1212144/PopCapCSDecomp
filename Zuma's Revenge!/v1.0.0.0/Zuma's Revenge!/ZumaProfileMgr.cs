using System;
using System.Collections.Generic;
using SexyFramework.Drivers.Profile;

namespace ZumasRevenge
{
	public class ZumaProfileMgr : ProfileManager
	{
		public ZumaProfileMgr()
			: base(GameApp.gApp)
		{
		}

		public void RenameTempProfile(string new_name)
		{
			if (this.GetProfile(".temp") != null)
			{
				bool flag = this.RenameProfile(".temp", new_name);
				if (flag)
				{
					return;
				}
			}
			this.AddProfile(new_name);
		}

		public bool HasTempProfile()
		{
			return this.GetProfile(".temp") != null;
		}

		public void GetListOfUserNames(List<string> user_vec)
		{
			if (user_vec == null)
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)this.GetNumProfiles()))
			{
				UserProfile profile = this.GetProfile(num);
				user_vec.Insert(user_vec.Count, profile.GetName());
				num++;
			}
		}
	}
}
