using System;
using System.Collections.Generic;
using BejeweledLivePlus;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

public class BorderEffect
{
	public BorderEffect(ref float theMarkerPos, ref float theMarkerPosOuter, int theLength)
	{
		this.mId = 0;
		this.mX = 0;
		this.mY = 0;
		this.mFxOffsetX = 0;
		this.mFxOffsetY = 0;
		this.mFx = null;
		this.mUpdateCnt = 0;
		this.mBorderGlow = false;
		if (theLength > 0)
		{
			List<float> list = this.mMarkerPos;
			this.mMarkerPos.Clear();
			foreach (float num in list)
			{
				this.mMarkerPos.Add(num);
			}
			list = this.mMarkerPosOuter;
			this.mMarkerPosOuter.Clear();
			foreach (float num2 in list)
			{
				this.mMarkerPos.Add(num2);
			}
		}
		this.mMarkerLen = theLength;
		this.mTotalDist = 0.0;
		for (int i = 0; i < this.mMarkerLen * 2; i++)
		{
			this.mDists.Add(0f);
		}
		this.mImg = null;
		this.mPhase.SetConstant(0.0);
		this.mMultMagnitudeOuter.SetConstant(1.0);
		this.mMultMagnitudeInner.SetConstant(1.0);
		this.mMultMagnitudeOuterTextureSpan.SetConstant(1.0);
		this.mMultMagnitudeOuterTextureSpanMag.SetConstant(1.0);
		this.mAlpha.SetConstant(1.0);
		this.mLastMultOuterMag = 1.0;
		this.mLastMultInnerMag = 1.0;
		this.mDelayCnt = 0;
		this.mMaxParticles = 0;
		this.mSortOrder = 0;
		this.mParticlesInForeground = false;
		for (int j = 0; j < this.mDists.size<float>(); j++)
		{
			float num3 = this.GetMarkerX(j) - this.GetMarkerX((j + this.mDists.size<float>() - 1) % this.mDists.size<float>());
			float num4 = this.GetMarkerY(j) - this.GetMarkerY((j + this.mDists.size<float>() - 1) % this.mDists.size<float>());
			this.mDists[j] = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			this.mTotalDist += (double)this.mDists[j];
		}
	}

	public void Dispose()
	{
	}

	public void Draw(Graphics g)
	{
		if (this.mLastMultOuterMag != this.mMultMagnitudeOuter || this.mMultMagnitudeOuterTextureSpan.IsDoingCurve())
		{
			this.MultMagnitudeOuter(this.mMultMagnitudeOuter);
		}
		if (this.mLastMultInnerMag != this.mMultMagnitudeInner)
		{
			this.MultMagnitudeInner(this.mMultMagnitudeInner);
		}
		if (this.mAlpha != 1.0)
		{
			if (this.mAlpha == 0.0)
			{
				return;
			}
			g.SetColorizeImages(true);
			g.SetColor(Color.FAlpha((float)this.mAlpha));
		}
		g.PushState();
		g.Translate(BejeweledLivePlus.GlobalMembers.S(this.mX), BejeweledLivePlus.GlobalMembers.S(this.mY));
		int num = this.mMarkerLen;
		if (!this.mParticlesInForeground)
		{
			this.DrawParticles(g);
		}
		if (this.mImg != null)
		{
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			this.CalcTextureProps(ref num2, ref num3, ref num4, ref num5);
			int num6 = 0;
			double num7 = num5;
			double num8 = 0.0;
			double num9 = 0.0;
			while ((double)this.mDists[num6] <= num7)
			{
				num7 -= (double)this.mDists[num6];
				num6 = (num6 + 1) % this.mDists.size<float>();
			}
			double num10 = num7;
			double num11 = num7;
			int num12 = -1;
			int num13 = 0;
			for (;;)
			{
				double num14 = num4 - num8;
				double num15 = (double)this.mDists[num6] - num7;
				double num16 = Math.Min(num14, num15);
				if (num6 == num12 && num10 - num7 >= 0.0)
				{
					num16 = Math.Min(num16, num10 - num7);
				}
				num9 += num16;
				num7 += num16;
				double inAlpha = num11 / (double)this.mDists[num6];
				double inAlpha2 = num7 / (double)this.mDists[num6];
				int num17 = (num6 + 1) % num;
				double theNum = SexyMath.Lerp((double)this.GetMarkerX(num6), (double)this.GetMarkerX(num17), inAlpha);
				double theNum2 = SexyMath.Lerp((double)this.GetMarkerY(num6), (double)this.GetMarkerY(num17), inAlpha);
				double theNum3 = SexyMath.Lerp((double)this.GetMarkerX(num6), (double)this.GetMarkerX(num17), inAlpha2);
				double theNum4 = SexyMath.Lerp((double)this.GetMarkerY(num6), (double)this.GetMarkerY(num17), inAlpha2);
				double theNum5 = SexyMath.Lerp((double)this.GetMarkerOuterX(num6), (double)this.GetMarkerOuterX(num17), inAlpha);
				double theNum6 = SexyMath.Lerp((double)this.GetMarkerOuterY(num6), (double)this.GetMarkerOuterY(num17), inAlpha);
				double theNum7 = SexyMath.Lerp((double)this.GetMarkerOuterX(num6), (double)this.GetMarkerOuterX(num17), inAlpha2);
				double theNum8 = SexyMath.Lerp((double)this.GetMarkerOuterY(num6), (double)this.GetMarkerOuterY(num17), inAlpha2);
				SexyVertex2D[,] array = new SexyVertex2D[2, 3];
				double num18 = num8 / num4;
				double num19 = num9 / num4;
				array[0, 0].x = (float)BejeweledLivePlus.GlobalMembers.S(theNum);
				array[0, 0].y = (float)BejeweledLivePlus.GlobalMembers.S(theNum2);
				array[0, 0].u = (float)num18;
				array[0, 0].v = 1f;
				array[0, 1].x = (float)BejeweledLivePlus.GlobalMembers.S(theNum7);
				array[0, 1].y = (float)BejeweledLivePlus.GlobalMembers.S(theNum8);
				array[0, 1].u = (float)num19;
				array[0, 1].v = 0f;
				array[0, 2].x = (float)BejeweledLivePlus.GlobalMembers.S(theNum5);
				array[0, 2].y = (float)BejeweledLivePlus.GlobalMembers.S(theNum6);
				array[0, 2].u = (float)num18;
				array[0, 2].v = 0f;
				array[1, 0].x = (float)BejeweledLivePlus.GlobalMembers.S(theNum7);
				array[1, 0].y = (float)BejeweledLivePlus.GlobalMembers.S(theNum8);
				array[1, 0].u = (float)num19;
				array[1, 0].v = 0f;
				array[1, 1].x = (float)BejeweledLivePlus.GlobalMembers.S(theNum);
				array[1, 1].y = (float)BejeweledLivePlus.GlobalMembers.S(theNum2);
				array[1, 1].u = (float)num18;
				array[1, 1].v = 1f;
				array[1, 2].x = (float)BejeweledLivePlus.GlobalMembers.S(theNum3);
				array[1, 2].y = (float)BejeweledLivePlus.GlobalMembers.S(theNum4);
				array[1, 2].u = (float)num19;
				array[1, 2].v = 1f;
				g.DrawTrianglesTex(this.mImg, array, 2);
				num13 += 2;
				if (num6 == num12 && num10 - num7 <= 0.0)
				{
					break;
				}
				if (num7 >= (double)this.mDists[num6])
				{
					if (num12 < 0)
					{
						num12 = num6;
					}
					num7 = 0.0;
					num6 = (num6 + 1) % this.mDists.size<float>();
				}
				if (num9 >= num4)
				{
					num9 = 0.0;
				}
				num11 = num7;
				num8 = num9;
			}
		}
		if (this.mParticlesInForeground)
		{
			this.DrawParticles(g);
		}
		g.PopState();
		g.SetColor(new Color(-1));
		g.SetColorizeImages(false);
	}

	public void DrawDebug(Graphics g)
	{
		int num = this.mMarkerLen;
		int num2 = 0;
		while (num2 < num && num2 != BejeweledLivePlus.GlobalMembers.M(-1))
		{
			double theNum = (double)this.GetMarkerX(num2);
			double theNum2 = (double)this.GetMarkerX((num2 + 1) % num);
			double theNum3 = (double)this.GetMarkerY(num2);
			double theNum4 = (double)this.GetMarkerY((num2 + 1) % num);
			double theNum5 = (double)this.GetMarkerOuterX(num2);
			double theNum6 = (double)this.GetMarkerOuterX((num2 + 1) % num);
			double theNum7 = (double)this.GetMarkerOuterY(num2);
			double theNum8 = (double)this.GetMarkerOuterY((num2 + 1) % num);
			g.SetColor(new Color(BejeweledLivePlus.GlobalMembers.M(16777215), BejeweledLivePlus.GlobalMembers.M(100)));
			g.DrawLine((int)BejeweledLivePlus.GlobalMembers.S(theNum), (int)BejeweledLivePlus.GlobalMembers.S(theNum3), (int)BejeweledLivePlus.GlobalMembers.S(theNum2), (int)BejeweledLivePlus.GlobalMembers.S(theNum4));
			g.DrawLine((int)BejeweledLivePlus.GlobalMembers.S(theNum5), (int)BejeweledLivePlus.GlobalMembers.S(theNum7), (int)BejeweledLivePlus.GlobalMembers.S(theNum6), (int)BejeweledLivePlus.GlobalMembers.S(theNum8));
			g.DrawLine((int)BejeweledLivePlus.GlobalMembers.S(theNum2), (int)BejeweledLivePlus.GlobalMembers.S(theNum4), (int)BejeweledLivePlus.GlobalMembers.S(theNum5), (int)BejeweledLivePlus.GlobalMembers.S(theNum7));
			g.DrawLine((int)BejeweledLivePlus.GlobalMembers.S(theNum5), (int)BejeweledLivePlus.GlobalMembers.S(theNum7), (int)BejeweledLivePlus.GlobalMembers.S(theNum), (int)BejeweledLivePlus.GlobalMembers.S(theNum3));
			g.SetColor(new Color(BejeweledLivePlus.GlobalMembers.M(65280), BejeweledLivePlus.GlobalMembers.M(100)));
			g.FillRect((int)BejeweledLivePlus.GlobalMembers.S(theNum) - 1, (int)BejeweledLivePlus.GlobalMembers.S(theNum3) - 1, 2, 2);
			g.FillRect((int)BejeweledLivePlus.GlobalMembers.S(theNum2) - 1, (int)BejeweledLivePlus.GlobalMembers.S(theNum4) - 1, 2, 2);
			g.FillRect((int)BejeweledLivePlus.GlobalMembers.S(theNum5) - 1, (int)BejeweledLivePlus.GlobalMembers.S(theNum7) - 1, 2, 2);
			g.FillRect((int)BejeweledLivePlus.GlobalMembers.S(theNum6) - 1, (int)BejeweledLivePlus.GlobalMembers.S(theNum8) - 1, 2, 2);
			num2++;
		}
		if (this.mFx != null)
		{
			for (int i = 0; i < this.mFx.mLayerVector.size<PILayer>(); i++)
			{
				PILayer layer = this.mFx.GetLayer(i);
				for (int j = 0; j < layer.mEmitterInstanceVector.size<PIEmitterInstance>(); j++)
				{
					PIEmitterInstance emitter = layer.GetEmitter(j);
					int num3 = emitter.mEmitterInstanceDef.mPoints.size<PIValue2D>();
					for (int k = 0; k < num3; k++)
					{
						g.DrawLine((int)BejeweledLivePlus.GlobalMembers.S(emitter.mEmitterInstanceDef.mPoints[k].mValuePoint2DVector[0].mValue.X), (int)BejeweledLivePlus.GlobalMembers.S(emitter.mEmitterInstanceDef.mPoints[k].mValuePoint2DVector[0].mValue.Y), (int)BejeweledLivePlus.GlobalMembers.S(emitter.mEmitterInstanceDef.mPoints[(k + 1) % num3].mValuePoint2DVector[0].mValue.X), (int)BejeweledLivePlus.GlobalMembers.S(emitter.mEmitterInstanceDef.mPoints[(k + 1) % num3].mValuePoint2DVector[0].mValue.Y));
					}
				}
			}
		}
	}

	public void DrawParticles(Graphics g)
	{
		if (this.mFx != null)
		{
			if (this.mAlpha != 1.0)
			{
				g.PushColorMult();
			}
			g.Translate(this.mFxOffsetX, this.mFxOffsetY);
			this.mFx.Draw(g);
			g.Translate(-this.mFxOffsetX, -this.mFxOffsetY);
			if (this.mAlpha != 1.0)
			{
				g.PopColorMult();
			}
		}
	}

	public void Update()
	{
		if (this.mDelayCnt > 0)
		{
			this.mDelayCnt--;
			return;
		}
		this.mUpdateCnt++;
		this.mPhase.IncInVal();
		this.mMultMagnitudeOuter.IncInVal();
		this.mMultMagnitudeInner.IncInVal();
		this.mAlpha.IncInVal();
		this.mMultMagnitudeOuterTextureSpanMag.IncInVal();
		this.mMultMagnitudeOuterTextureSpan.mOutMax = this.mMultMagnitudeOuterTextureSpanMag;
		if (this.mFx != null)
		{
			this.mFx.Update();
		}
	}

	public bool IsDone()
	{
		return this.mAlpha.HasBeenTriggered() && this.mAlpha == 0.0;
	}

	public float GetMarkerX(int x)
	{
		return this.mMarkerPos[x * 2];
	}

	public float GetMarkerY(int y)
	{
		return this.mMarkerPos[y * 2 + 1];
	}

	public float GetMarkerOrigX(int x)
	{
		return this.mMarkerPosOrig[x * 2];
	}

	public float GetMarkerOrigY(int y)
	{
		return this.mMarkerPosOrig[y * 2 + 1];
	}

	public float GetMarkerOuterX(int x)
	{
		return this.mMarkerPosOuter[x * 2];
	}

	public float GetMarkerOuterY(int y)
	{
		return this.mMarkerPosOuter[y * 2 + 1];
	}

	public float GetMarkerOuterOrigX(int x)
	{
		return this.mMarkerPosOuterOrig[x * 2];
	}

	public float GetMarkerOuterOrigY(int y)
	{
		return this.mMarkerPosOuterOrig[y * 2 + 1];
	}

	public void MultMagnitudeInner(double theMult)
	{
	}

	public void MultMagnitudeOuter(double theMult)
	{
	}

	public void SetEmitter(PIEffect theFx)
	{
	}

	public void RefreshEmitters()
	{
	}

	public void Finish()
	{
		this.mAlpha.SetConstant(0.0);
	}

	public void CalcTextureProps(ref double theTextureCount, ref double theTgtTextureCount, ref double theTextureW, ref double thePhase)
	{
	}

	public List<float> mDists = new List<float>();

	public double mTotalDist;

	public int mX;

	public int mY;

	public int mFxOffsetX;

	public int mFxOffsetY;

	public PIEffect mFx;

	public Image mImg;

	public int mSortOrder;

	public bool mParticlesInForeground;

	public int mMaxParticles;

	public int mId;

	public CurvedVal mPhase;

	public CurvedVal mMultMagnitudeOuterTextureSpan;

	public CurvedVal mMultMagnitudeOuterTextureSpanMag;

	public CurvedVal mMultMagnitudeOuter;

	public CurvedVal mMultMagnitudeInner;

	public CurvedVal mAlpha;

	public List<float> mMarkerPos = new List<float>();

	public List<float> mMarkerPosOrig = new List<float>();

	public List<float> mMarkerPosOuter = new List<float>();

	public List<float> mMarkerPosOuterOrig = new List<float>();

	public int mMarkerLen;

	public int mUpdateCnt;

	public int mDelayCnt;

	public bool mBorderGlow;

	protected double mLastMultOuterMag;

	protected double mLastMultInnerMag;
}
