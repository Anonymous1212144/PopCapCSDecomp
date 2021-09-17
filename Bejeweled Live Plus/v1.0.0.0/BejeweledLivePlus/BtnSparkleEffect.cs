using System;
using BejeweledLivePlus;
using BejeweledLivePlus.Bej3Graphics;
using SexyFramework;
using SexyFramework.Graphics;

public class BtnSparkleEffect : Effect
{
	public void init()
	{
		this.mAnchorBtn = null;
		this.mDAlpha = 0f;
		this.mCurvedRotate.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.8,0.01,2,####     CS,E#    ^~SYE"));
		this.mCurvedScale.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.5,0.01,2,#6JCjB6)8    )~###    X><uQV'<.a"));
		this.mCurvedAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.75,0.01,2,####    5~### t~###   v'###"));
	}

	public override void Draw(Graphics g)
	{
	}

	public override void Update()
	{
		if (this.mCurvedAlpha.HasBeenTriggered() || (double)this.mAnchorBtn.mAlpha == 0.0 || !this.mAnchorBtn.mVisible)
		{
			this.mDeleteMe = true;
		}
	}

	public CurvedVal mCurvedRotate;

	public double mBaseRot;

	public double mRotMult;

	public QuestMenuBtn mAnchorBtn;
}
