using System;
using System.Collections.Generic;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class SquidAnim
	{
		public SquidAnim()
		{
		}

		public SquidAnim(SquidAnim rhs)
		{
			this.mImage = rhs.mImage;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mCurCel = rhs.mCurCel;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			for (int i = 0; i < rhs.mCels.Count; i++)
			{
				SquidAnimCel squidAnimCel = new SquidAnimCel();
				squidAnimCel.mCelNum = rhs.mCels[i].mCelNum;
				squidAnimCel.mDelay = rhs.mCels[i].mDelay;
				this.mCels.Add(squidAnimCel);
			}
		}

		public void AddAnimInfo(int cel_num, int delay)
		{
			SquidAnimCel squidAnimCel = new SquidAnimCel();
			this.mCels.Add(squidAnimCel);
			squidAnimCel.mCelNum = cel_num;
			squidAnimCel.mDelay = delay;
		}

		public void Update()
		{
			if (++this.mUpdateCount >= this.mCels[this.mCurCel].mDelay)
			{
				this.mUpdateCount = 0;
				this.mCurCel = (this.mCurCel + 1) % this.mCels.Count;
			}
		}

		public void Draw(Graphics g, float x, float y)
		{
			g.DrawImageCel(this.mImage, (int)(x + Common._S(this.mX)), (int)(y + Common._S(this.mY)), this.mCels[this.mCurCel].mCelNum);
		}

		public Image mImage;

		public List<SquidAnimCel> mCels = new List<SquidAnimCel>();

		public int mUpdateCount;

		public int mCurCel;

		public float mX;

		public float mY;
	}
}
