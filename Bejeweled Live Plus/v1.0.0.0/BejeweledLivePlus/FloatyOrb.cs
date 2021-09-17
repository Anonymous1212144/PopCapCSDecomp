using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

public class FloatyOrb
{
	public FloatyOrb()
	{
		this.mLandFx = null;
	}

	public CurvedVal mAlpha;

	public CurvedVal mLerp;

	public CurvedVal mScale;

	public CurvedVal mNormalOffset;

	public FPoint mPosFrom;

	public int mIdx;

	public PIEffect mLandFx;
}
