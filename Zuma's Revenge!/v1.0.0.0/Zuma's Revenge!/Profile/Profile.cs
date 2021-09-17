using System;
using SexyFramework.Drivers.Profile;

namespace ZumasRevenge.Profile
{
	public class Profile
	{
		public void loadProfile()
		{
			this.fspd.LoadDetails();
		}

		public void saveAll()
		{
			this.fspd.SaveDetails();
		}

		private FilesystemProfileData fspd = new FilesystemProfileData(new UserProfile());

		private int aa;
	}
}
