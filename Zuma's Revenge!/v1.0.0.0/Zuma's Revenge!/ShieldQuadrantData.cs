using System;
using SexyFramework.AELib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class ShieldQuadrantData
	{
		public ShieldQuadrantData()
		{
		}

		public ShieldQuadrantData(CompositionMgr cm, PIEffect s)
		{
			this.mCompMgr = cm;
			this.mSparkles = s;
		}

		public virtual void Dispose()
		{
			if (this.mCompMgr != null)
			{
				this.mCompMgr = null;
			}
			if (this.mSparkles != null)
			{
				this.mSparkles.Dispose();
				this.mSparkles = null;
			}
		}

		public CompositionMgr mCompMgr;

		public PIEffect mSparkles;

		public bool mDoHitAnim;

		public bool mDoExplodeAnim;
	}
}
