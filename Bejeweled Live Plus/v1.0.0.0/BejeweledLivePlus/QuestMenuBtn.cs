using System;
using BejeweledLivePlus;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

public class QuestMenuBtn : Bej3Button
{
	public QuestMenuBtn(int theId, Bej3ButtonListener theListener)
		: base(theId, theListener)
	{
		this.mIconImg = null;
	}

	public override void Draw(Graphics g)
	{
		if (this.HaveButtonImage(this.mButtonImage, this.mBaseImgRect))
		{
			this.DrawButtonImage(g, this.mButtonImage, this.mBaseImgRect, 0, 0);
		}
		base.Draw(g);
		if (this.mIconImg != null)
		{
			g.DrawImage(this.mIconImg, GlobalMembers.S(this.mIconOffX), GlobalMembers.S(this.mIconOffY));
		}
	}

	public Rect mBaseImgRect;

	public Image mIconImg;

	public int mIconOffX;

	public int mIconOffY;
}
