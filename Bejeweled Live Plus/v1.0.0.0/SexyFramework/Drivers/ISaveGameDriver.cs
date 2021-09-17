using System;
using SexyFramework.Drivers.Profile;
using SexyFramework.Misc;

namespace SexyFramework.Drivers
{
	public abstract class ISaveGameDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract bool Init();

		public abstract void Update();

		public abstract ISaveGameContext CreateSaveGameContext(UserProfile player, string saveName, ulong requiredBytes);

		public abstract bool BeginLoad(ISaveGameContext context, string segment, bool checkOnly);

		public abstract bool BeginSave(ISaveGameContext context, string segment, Buffer data);

		public abstract bool BeginDelete(ISaveGameContext context, string segment);

		public abstract bool BeginSaveGameDelete(ISaveGameContext context);
	}
}
