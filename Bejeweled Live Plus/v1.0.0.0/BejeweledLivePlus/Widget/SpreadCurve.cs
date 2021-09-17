using System;
using System.Collections.Generic;
using SexyFramework;

namespace BejeweledLivePlus.Widget
{
	public class SpreadCurve
	{
		public SpreadCurve(int theSize)
		{
			this.mVals = new List<float>(theSize);
			this.mSize = theSize;
			for (int i = 0; i < this.mSize; i++)
			{
				this.mVals.Add((float)i / (float)theSize);
			}
		}

		public SpreadCurve()
		{
			int num = 256;
			this.mVals = new List<float>(num);
			this.mSize = num;
			for (int i = 0; i < this.mSize; i++)
			{
				this.mVals.Add((float)i / (float)num);
			}
		}

		public SpreadCurve(CurvedVal theCurve, int theSize)
		{
			this.mVals = new List<float>(theSize);
			this.mSize = theSize;
			for (int i = 0; i < this.mSize; i++)
			{
				this.mVals.Add((float)i / (float)theSize);
			}
			this.SetToCurve(theCurve);
		}

		public SpreadCurve(CurvedVal theCurve)
		{
			int num = 256;
			this.mVals = new List<float>(num);
			this.mSize = num;
			for (int i = 0; i < this.mSize; i++)
			{
				this.mVals.Add((float)i / (float)num);
			}
			this.SetToCurve(theCurve);
		}

		public void SetToCurve(CurvedVal theCurve)
		{
			List<double> list = new List<double>(this.mSize);
			for (int i = 0; i < this.mSize; i++)
			{
				list.Add(0.0);
			}
			GlobalMembers.DBG_ASSERT(theCurve.mOutMax <= 1.0);
			GlobalMembers.DBG_ASSERT(theCurve.mInMax >= 0.0);
			double num = 0.0;
			for (int j = 0; j < this.mSize; j++)
			{
				double outVal = theCurve.GetOutVal((double)((float)j / (float)this.mSize));
				List<double> list2;
				int num2;
				(list2 = list)[num2 = j] = list2[num2] + outVal;
				num += outVal;
			}
			int num3 = 0;
			double num4 = 0.0;
			double num5 = (double)this.mSize;
			for (int k = 0; k < this.mSize; k++)
			{
				this.mVals[k] = 1f;
			}
			for (int l = 0; l < this.mSize; l++)
			{
				num4 += list[l] / num * (num5 - 1.0);
				double num6 = (double)l / (num5 - 1.0);
				while ((double)num3 <= num4)
				{
					if ((double)num3 < num5)
					{
						GlobalMembers.DBG_ASSERT(num6 <= 1.0 && num6 >= 0.0);
						this.mVals[num3] = (float)num6;
					}
					num3++;
				}
			}
		}

		public float GetOutVal(float theVal)
		{
			int num = GlobalMembers.MAX(0, (int)GlobalMembers.MIN((float)(this.mSize - 1), theVal * (float)(this.mSize - 1)));
			return this.mVals[num];
		}

		public List<float> mVals;

		public int mSize;
	}
}
