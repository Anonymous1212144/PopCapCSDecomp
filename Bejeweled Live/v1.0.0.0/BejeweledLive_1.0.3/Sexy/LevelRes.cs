using System;

namespace Sexy
{
	public class LevelRes : BaseRes
	{
		public LevelRes()
		{
			this.mLevelNumber = -1;
		}

		public override void DeleteResource()
		{
			base.DeleteResource();
		}

		public int mLevelNumber;
	}
}
