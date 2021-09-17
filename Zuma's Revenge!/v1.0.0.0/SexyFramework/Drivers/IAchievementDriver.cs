using System;
using SexyFramework.Drivers.Profile;

namespace SexyFramework.Drivers
{
	public abstract class IAchievementDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract int Init();

		public abstract void Update();

		public abstract IAchievementContext StartReadUnlockedAchievements(UserProfile p);

		public abstract IAchievementContext StartUnlockAchievement(UserProfile p, uint id);
	}
}
