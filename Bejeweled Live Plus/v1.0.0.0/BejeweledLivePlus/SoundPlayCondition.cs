using System;

namespace BejeweledLivePlus
{
	public abstract class SoundPlayCondition
	{
		public abstract void update();

		public abstract bool shouldActivate();
	}
}
