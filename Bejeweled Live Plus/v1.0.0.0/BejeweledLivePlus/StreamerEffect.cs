using System;
using SexyFramework;
using SexyFramework.Graphics;

public class StreamerEffect
{
	public void Update()
	{
	}

	public void Draw(Graphics g)
	{
	}

	public bool IsDone()
	{
		return this.mAlpha.HasBeenTriggered() && this.mAlpha == 0.0;
	}

	public void BuildTris(SexyVertex2D[,] theTris, double theStartMag)
	{
	}

	public void DrawDebug(Graphics g)
	{
	}

	public int mX;

	public int mY;

	public CurvedVal mRotSpeed;

	public CurvedVal mMag;

	public CurvedVal mAlpha;

	public CurvedVal mRotExtra;

	public double mRot;

	public double mMagMult;

	public double mStartMag;

	public double mBaseSize;

	public double mEdgeSize;

	public bool mNegRot;

	public int mColor;

	public int mColor2;

	public double mColorLerp;

	public Image mImg;
}
