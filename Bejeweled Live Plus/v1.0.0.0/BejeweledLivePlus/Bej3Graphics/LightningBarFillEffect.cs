using System;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class LightningBarFillEffect : Effect
	{
		public LightningBarFillEffect()
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
		}

		public void init()
		{
			base.init(Effect.Type.TYPE_CUSTOMCLASS);
			this.mPercentDone = 0f;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					this.mPoints[i, j] = default(FPoint);
				}
			}
		}

		public override void Update()
		{
			bool flag = this.mPercentDone == 0f;
			this.mPercentDone += GlobalMembers.M(0.012f);
			if (this.mPercentDone > 1f)
			{
				this.mDeleteMe = true;
				return;
			}
			if (this.mFXManager.mBoard.mUpdateCnt % GlobalMembers.M(3) == 0 || flag)
			{
				float num = (float)ConstantsWP.SPEEDBOARD_TIME_LIGHTNING_START_X;
				float num2 = (float)ConstantsWP.SPEEDBOARD_TIME_LIGHTNING_START_Y;
				float num3 = (float)ConstantsWP.SPEEDBOARD_TIME_LIGHTNING_END_X + this.mFXManager.mBoard.mCountdownBarPct * (float)ConstantsWP.SPEEDBOARD_TIME_LIGHTNING_END_X_FULL;
				float num4 = (float)ConstantsWP.SPEEDBOARD_TIME_LIGHTNING_END_Y;
				for (int i = 0; i < 8; i++)
				{
					float num5 = (float)i / 7f;
					float num6 = 1f - Math.Abs(1f - num5 * 2f);
					float num7 = num * (1f - num5) + num3 * num5 + num6 * (GlobalMembersUtils.GetRandFloat() * 60f);
					float num8 = num2 * (1f - num5) + num4 * num5 + num6 * (GlobalMembersUtils.GetRandFloat() * 60f);
					FPoint fpoint = this.mPoints[i, 0];
					FPoint fpoint2 = this.mPoints[i, 1];
					if (i != 0 && i != 7)
					{
						float num9 = (float)ConstantsWP.SPEEDBOARD_TIME_LIGHTNING_WIDTH;
						GlobalMembersUtils.GetRandFloat();
						GlobalMembersUtils.GetRandFloat();
						GlobalMembersUtils.GetRandFloat();
						GlobalMembersUtils.GetRandFloat();
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			g.PushState();
			float num = GlobalMembers.MIN((1f - this.mPercentDone) * 8f, 1f) * this.mFXManager.mBoard.GetAlpha();
			int num2 = (int)((double)num * 255.0);
			if (graphics3D != null)
			{
				SexyVertex2D[,] array = new SexyVertex2D[14, 3];
				int num3 = 0;
				for (int i = 0; i < 7; i++)
				{
					FPoint fpoint = this.mPoints[i, 0];
					FPoint fpoint2 = this.mPoints[i, 1];
					FPoint fpoint3 = this.mPoints[i + 1, 0];
					FPoint fpoint4 = this.mPoints[i + 1, 1];
					float num4 = (float)i / 7f;
					float num5 = (float)(i + 1) / 7f;
					if (i == 0)
					{
						SexyVertex2D sexyVertex2D = array[num3++, 0];
						GlobalMembers.S(fpoint.mX);
						GlobalMembers.S(fpoint.mY);
						SexyVertex2D sexyVertex2D2 = array[num3 - 1, 1];
						GlobalMembers.S(fpoint4.mX);
						GlobalMembers.S(fpoint4.mY);
						SexyVertex2D sexyVertex2D3 = array[num3 - 1, 2];
						GlobalMembers.S(fpoint3.mX);
						GlobalMembers.S(fpoint3.mY);
					}
					else if (i == 6)
					{
						SexyVertex2D sexyVertex2D4 = array[num3++, 0];
						GlobalMembers.S(fpoint.mX);
						GlobalMembers.S(fpoint.mY);
						SexyVertex2D sexyVertex2D5 = array[num3 - 1, 1];
						GlobalMembers.S(fpoint2.mX);
						GlobalMembers.S(fpoint2.mY);
						SexyVertex2D sexyVertex2D6 = array[num3 - 1, 2];
						GlobalMembers.S(fpoint3.mX);
						GlobalMembers.S(fpoint3.mY);
					}
					else
					{
						SexyVertex2D sexyVertex2D7 = array[num3++, 0];
						GlobalMembers.S(fpoint.mX);
						GlobalMembers.S(fpoint.mY);
						SexyVertex2D sexyVertex2D8 = array[num3 - 1, 1];
						GlobalMembers.S(fpoint4.mX);
						GlobalMembers.S(fpoint4.mY);
						SexyVertex2D sexyVertex2D9 = array[num3 - 1, 2];
						GlobalMembers.S(fpoint3.mX);
						GlobalMembers.S(fpoint3.mY);
						SexyVertex2D sexyVertex2D10 = array[num3++, 0];
						GlobalMembers.S(fpoint.mX);
						GlobalMembers.S(fpoint.mY);
						SexyVertex2D sexyVertex2D11 = array[num3 - 1, 1];
						GlobalMembers.S(fpoint2.mX);
						GlobalMembers.S(fpoint2.mY);
						SexyVertex2D sexyVertex2D12 = array[num3 - 1, 2];
						GlobalMembers.S(fpoint4.mX);
						GlobalMembers.S(fpoint4.mY);
					}
				}
				Color theColor = new Color(GlobalMembers.M(255), GlobalMembers.M(200), GlobalMembers.M(100));
				g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_LIGHTNING_TEX, array, num3, theColor, 1, g.mTransX, g.mTransY, true, default(Rect));
				g.DrawTrianglesTex(GlobalMembersResourcesWP.IMAGE_LIGHTNING_CENTER, array, num3, new Color(num2, num2, num2), 1, g.mTransX, g.mTransY, true, default(Rect));
			}
			else
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				Color color = new Color(GlobalMembers.M(255), GlobalMembers.M(200), GlobalMembers.M(100));
				for (int j = 0; j < 7; j++)
				{
					FPoint fpoint5 = this.mPoints[j, 0];
					FPoint fpoint6 = this.mPoints[j, 1];
					FPoint fpoint7 = this.mPoints[j + 1, 0];
					FPoint fpoint8 = this.mPoints[j + 1, 1];
					float num6 = 0.3f;
					float theNum = fpoint5.mX * num6 + fpoint6.mX * (1f - num6);
					float theNum2 = fpoint5.mY * num6 + fpoint6.mY * (1f - num6);
					float theNum3 = fpoint6.mX * num6 + fpoint5.mX * (1f - num6);
					float theNum4 = fpoint6.mY * num6 + fpoint5.mY * (1f - num6);
					float theNum5 = fpoint7.mX * num6 + fpoint8.mX * (1f - num6);
					float theNum6 = fpoint7.mY * num6 + fpoint8.mY * (1f - num6);
					float theNum7 = fpoint8.mX * num6 + fpoint7.mX * (1f - num6);
					float theNum8 = fpoint8.mY * num6 + fpoint7.mY * (1f - num6);
					Point[] array2 = new Point[3];
					g.SetColor(color);
					array2[0].mX = (int)GlobalMembers.S(fpoint5.mX);
					array2[0].mY = (int)GlobalMembers.S(fpoint5.mY);
					array2[1].mX = (int)GlobalMembers.S(fpoint8.mX);
					array2[1].mY = (int)GlobalMembers.S(fpoint8.mY);
					array2[2].mX = (int)GlobalMembers.S(fpoint7.mX);
					array2[2].mY = (int)GlobalMembers.S(fpoint7.mY);
					g.PolyFill(array2, 3, false);
					array2[0].mX = (int)GlobalMembers.S(fpoint5.mX);
					array2[0].mY = (int)GlobalMembers.S(fpoint5.mY);
					array2[1].mX = (int)GlobalMembers.S(fpoint6.mX);
					array2[1].mY = (int)GlobalMembers.S(fpoint6.mY);
					array2[2].mX = (int)GlobalMembers.S(fpoint8.mX);
					array2[2].mY = (int)GlobalMembers.S(fpoint8.mY);
					g.PolyFill(array2, 3, false);
					g.SetColor(new Color(num2, num2, num2));
					array2[0].mX = (int)GlobalMembers.S(theNum);
					array2[0].mY = (int)GlobalMembers.S(theNum2);
					array2[1].mX = (int)GlobalMembers.S(theNum7);
					array2[1].mY = (int)GlobalMembers.S(theNum8);
					array2[2].mX = (int)GlobalMembers.S(theNum5);
					array2[2].mY = (int)GlobalMembers.S(theNum6);
					g.PolyFill(array2, 3, false);
					array2[0].mX = (int)GlobalMembers.S(theNum);
					array2[0].mY = (int)GlobalMembers.S(theNum2);
					array2[1].mX = (int)GlobalMembers.S(theNum3);
					array2[1].mY = (int)GlobalMembers.S(theNum4);
					array2[2].mX = (int)GlobalMembers.S(theNum7);
					array2[2].mY = (int)GlobalMembers.S(theNum8);
					g.PolyFill(array2, 3, false);
				}
				g.SetDrawMode(Graphics.DrawMode.Normal);
			}
			g.PopState();
		}

		public new static void initPool()
		{
			LightningBarFillEffect.thePool_ = new SimpleObjectPool(512, typeof(LightningBarFillEffect));
		}

		public new static LightningBarFillEffect alloc()
		{
			LightningBarFillEffect lightningBarFillEffect = (LightningBarFillEffect)LightningBarFillEffect.thePool_.alloc();
			lightningBarFillEffect.init();
			return lightningBarFillEffect;
		}

		public override void release()
		{
			this.Dispose();
			LightningBarFillEffect.thePool_.release(this);
		}

		private const int NUM_BARFILL_LIGTNING_POINTS = 8;

		public FPoint[,] mPoints = new FPoint[8, 2];

		public float mPercentDone;

		private static SimpleObjectPool thePool_;
	}
}
