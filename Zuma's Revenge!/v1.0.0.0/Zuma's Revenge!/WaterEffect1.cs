using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class WaterEffect1 : Effect
	{
		public WaterEffect1()
		{
			this.mResGroup = "";
			this.Reset("");
		}

		protected void SetupShoreWaves(int x, int y, bool mirror, float vx, float vy)
		{
		}

		public override string GetName()
		{
			return "WaterEffect1";
		}

		public override void Update()
		{
		}

		public override void Reset(string level_id)
		{
			this.mUpdateCount++;
		}

		public override void DrawPriority(Graphics g, int priority)
		{
		}
	}
}
