using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class Fog : Effect
	{
		private static float GetAlphaTimeRange()
		{
			return (float)Common.IntRange(Common._M(150), Common._M1(500));
		}

		private static float GetSizeTimeRange()
		{
			return (float)Common.IntRange(Common._M(200), Common._M1(500));
		}

		private static float GetSizeRange()
		{
			return Common.FloatRange(Common._M(0.75f), Common._M1(1.25f));
		}

		protected override void Init()
		{
			this.mFogElements.Clear();
			Rect[] array = new Rect[]
			{
				new Rect(0, 0, Common._S(Common._M(50)), GameApp.gApp.mHeight),
				new Rect(0, 0, GameApp.gApp.mWidth, Common._S(Common._M1(50))),
				new Rect(GameApp.gApp.mWidth - Common._S(Common._M(50)), 0, Common._S(Common._M1(50)), GameApp.gApp.mHeight),
				new Rect(0, GameApp.gApp.mHeight - Common._S(Common._M2(50)), GameApp.gApp.mWidth, Common._S(Common._M3(50)))
			};
			for (int i = 0; i < 4; i++)
			{
				this.SetupSide(array[i]);
			}
			this.mFogElements.Sort(new ImageSort());
		}

		protected void SetupSide(Rect r)
		{
		}

		protected void DoDraw(Graphics g, bool under)
		{
		}

		public Fog()
		{
			this.mResGroup = "Boss6_StoneHead";
		}

		public override string GetName()
		{
			return "Fog";
		}

		public override void DrawUnderBackground(Graphics g)
		{
			if (!g.Is3D() || this.mForceAllDrawOverBalls)
			{
				return;
			}
			this.DoDraw(g, true);
		}

		public override void DrawAboveBalls(Graphics g)
		{
			if (!g.Is3D() || this.mForceAllDrawOverBalls)
			{
				return;
			}
			this.DoDraw(g, true);
		}

		public override void Update()
		{
		}

		public override void SetParams(string key, string value)
		{
		}

		private static int MAX_ALPHA = 220;

		protected List<FogElement> mFogElements = new List<FogElement>();

		protected bool mForceAllDrawOverBalls;
	}
}
