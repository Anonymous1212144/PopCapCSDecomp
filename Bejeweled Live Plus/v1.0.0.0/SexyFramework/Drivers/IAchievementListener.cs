using System;

namespace SexyFramework.Drivers
{
	public abstract class IAchievementListener
	{
		public virtual void Dispose()
		{
		}

		public virtual void AchievementUnlocked(IAchievementContext context)
		{
			context.Destroy();
		}

		public virtual void AchievementRead(IAchievementContext context)
		{
			context.Destroy();
		}
	}
}
