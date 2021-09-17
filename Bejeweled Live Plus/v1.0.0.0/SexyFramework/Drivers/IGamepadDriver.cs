using System;

namespace SexyFramework.Drivers
{
	public abstract class IGamepadDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract int InitGamepadDriver(SexyAppBase NamelessParameter);

		public abstract IGamepad GetGamepad(int theIndex);

		public abstract void Update();
	}
}
