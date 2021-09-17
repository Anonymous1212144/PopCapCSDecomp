using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class EffectItem
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mColor.mRed);
			sync.SyncLong(ref this.mColor.mGreen);
			sync.SyncLong(ref this.mColor.mBlue);
			sync.SyncLong(ref this.mColor.mAlpha);
			this.SyncListComponents(sync, this.mScale, true);
			this.SyncListComponents(sync, this.mOpacity, true);
			this.SyncListComponents(sync, this.mAngle, true);
			this.SyncListComponents(sync, this.mXOffset, true);
			this.SyncListComponents(sync, this.mYOffset, true);
		}

		private void SyncListComponents(DataSync sync, List<Component> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Component component = new Component();
					component.SyncState(sync);
					theList.Add(component);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Component component2 in theList)
			{
				component2.SyncState(sync);
			}
		}

		public Image mImage;

		public List<Component> mScale = new List<Component>();

		public List<Component> mOpacity = new List<Component>();

		public List<Component> mAngle = new List<Component>();

		public List<Component> mXOffset = new List<Component>();

		public List<Component> mYOffset = new List<Component>();

		public int mCel;

		public Color mColor = default(Color);
	}
}
