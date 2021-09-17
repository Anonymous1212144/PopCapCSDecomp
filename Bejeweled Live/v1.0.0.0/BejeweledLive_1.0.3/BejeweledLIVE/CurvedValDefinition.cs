using System;
using System.Collections.Generic;
using System.Globalization;

namespace BejeweledLIVE
{
	public class CurvedValDefinition
	{
		static CurvedValDefinition()
		{
			CurvedValDefinition.allDefinitions = new List<CurvedValDefinition>();
			CurvedValDefinition.mRainbowSizeCurve = new CurvedValDefinition("b;0,1,0.006667,1,#6P4 dG==] 9e###OH###Qm###bD###Mb###V?###LS###O8###|?<i^  G####");
			CurvedValDefinition.mRainbowAlphaCurve = new CurvedValDefinition("b;0,1,0.006667,1,####    *(###`:###O'###    i####");
			CurvedValDefinition.mSpeedBonusDispCurve = new CurvedValDefinition("b+0,1,0.05,1,####         ~~###");
			CurvedValDefinition.mGameSpeedCurve = new CurvedValDefinition("b;0,1,0.003333,1,~###    I~### qy|Em   e}###");
			CurvedValDefinition.mTimeUpAlphaCurve = new CurvedValDefinition("b;0,1,0.005556,1,#### q~###     w~###  7####");
			CurvedValDefinition.mTimeUpScaleCurve = new CurvedValDefinition("b;0,1,0.005556,1,#### B~###      a~### |####");
			CurvedValDefinition.mSlotsAlphaCurve = new CurvedValDefinition("b+0,1,0.004,1,#### x#### B~###      e~###");
			CurvedValDefinition.mSlotsSpinPctCurve = new CurvedValDefinition("b+0,1,0,1,####   V#### T~###    t~###");
			CurvedValDefinition.mNovaScaleCurve = new CurvedValDefinition("b+0,1.5,0.013333,1,#4I(         ~~P##");
			CurvedValDefinition.mNovaAlphaCurve = new CurvedValDefinition("b+0,1,0,1,####    e~###     <####");
			CurvedValDefinition.mNukeScaleCurve = new CurvedValDefinition("b+0,2,0.006667,1,####   ^X### bX### S~###   /####");
			CurvedValDefinition.mNukeAlphaCurve = new CurvedValDefinition("b+0,1,0,1,#### o~###     Y}###  V####");
			CurvedValDefinition.mSpeedBonusDispCurve3 = new CurvedValDefinition("b+0,1,0.05,1,####         ~~###");
			CurvedValDefinition.mSpeedBonusPointsGlowCurve = new CurvedValDefinition("b+0,1,0.033333,1,#### ;I-7l        f####");
			CurvedValDefinition.mSpeedBonusPointsScaleCurve2 = new CurvedValDefinition("b+1,2,0.033333,1,####  >4###       c####");
			CurvedValDefinition.mComboFlashPctCurve = new CurvedValDefinition("b-0,1,0.006667,1,#### 3{### oO### jk### TI### ]W###  &####");
			CurvedValDefinition.theSelectedmSelectorAlphaCurve = new CurvedValDefinition("b+0,1,0.066667,1,~###         ~#@yd");
			CurvedValDefinition.aPiecemDestPctCurve = new CurvedValDefinition("b;0,1,0.033333,1,#.ht         ~~W[d");
			CurvedValDefinition.aPiecemAlphaCurve = new CurvedValDefinition("b;0,1,0.033333,1,~r)6         H;?D,X#>3Z");
			CurvedValDefinition.mGoCurve = new CurvedValDefinition("b;0,1,0.005556,1,#### q~###     w~###  7####");
			CurvedValDefinition.mSpeedBonusDispCurve2 = new CurvedValDefinition("b+0,1,0.003333,1,~###       +~###  @####X####");
			CurvedValDefinition.mSpeedBonusPointsScaleCurve = new CurvedValDefinition("b+0,2,0.003333,1,P###  RF###  t`###    X####");
			CurvedValDefinition.mSwapPctCurve = new CurvedValDefinition("b+-1,1,0.033333,1,#$)h B####    cObb4   z~LPQ");
			CurvedValDefinition.mGemScaleCurve = new CurvedValDefinition("b+-0.075,0.075,0.022222,1,P#FJ FP###    C~###    :PL=H");
			CurvedValDefinition.aSwapDatamSwapPctCurve = new CurvedValDefinition("b+-1,1,0.033333,1,~x:*    }Ppt+     $#LPR");
			CurvedValDefinition.aSwapDatamScaleCurve = new CurvedValDefinition("b+-0.075,0.075,0.033333,1,P&X%     '~###    zPL=I");
			CurvedValDefinition.pieceShrinkScaleCurve = new CurvedValDefinition("b+0,1.2,0.05,1,~###         ~#Blc");
			CurvedValDefinition.pieceMatchGrowScaleCurve = new CurvedValDefinition("b+1,1.2,0.333333,1,#+Ky         ~~###");
			CurvedValDefinition.pieceCascadeGrowScaleCurve = new CurvedValDefinition("b+1,1.22,0.033333,1,#+Kx      uw*7u   ,~###");
			CurvedValDefinition.lightningZapAlphaCurve = new CurvedValDefinition("b;0,1,0.00885,1,####oCh;uZV###X^8.tQ<###Uqh*Kzk###QG###R~###hI###u~### $#### 2y### *####");
			CurvedValDefinition.complementAlphaCurve = new CurvedValDefinition("b+0,1,0.005714,1,#### 0~r2d        q#G_h");
			CurvedValDefinition.complementScaleCurve = new CurvedValDefinition("b+0,1,0,1,#7+F  td,P9       -~P##");
		}

		public static void PreAllocateMemory()
		{
			foreach (CurvedValDefinition curve in CurvedValDefinition.allDefinitions)
			{
				CurvedVal newCurvedVal = CurvedVal.GetNewCurvedVal();
				newCurvedVal.SetCurve(curve);
				newCurvedVal.PrepareForReuse();
			}
		}

		private CurvedValDefinition(string theString)
		{
			CurvedValDefinition.allDefinitions.Add(this);
			this.originalDef = theString;
			int num = 0;
			this.aVersion = 0;
			if (theString.get_Chars(0) >= 'a' && theString.get_Chars(0) <= 'b')
			{
				this.aVersion = (int)(theString.get_Chars(0) - 'a');
			}
			num++;
			if (this.aVersion >= 1)
			{
				this.aFlags = GlobalMembersCurvedVal.CVCharToInt((sbyte)theString.get_Chars(num++));
			}
			int num2 = theString.IndexOf(',', num);
			if (num2 == -1)
			{
				this.mIsHermite = true;
				return;
			}
			double num3 = double.Parse(theString.Substring(num, num2 - num), CurvedValDefinition.culture.NumberFormat);
			this.mOutMin = (float)num3;
			num = num2 + 1;
			num2 = theString.IndexOf(',', num);
			if (num2 == -1)
			{
				return;
			}
			num3 = double.Parse(theString.Substring(num, num2 - num), CurvedValDefinition.culture.NumberFormat);
			this.mOutMax = (float)num3;
			num = num2 + 1;
			num2 = theString.IndexOf(',', num);
			if (num2 == -1)
			{
				return;
			}
			num3 = double.Parse(theString.Substring(num, num2 - num), CurvedValDefinition.culture.NumberFormat);
			this.mIncRate = (float)num3;
			num = num2 + 1;
			if (this.aVersion >= 1)
			{
				num2 = theString.IndexOf(',', num);
				if (num2 == -1)
				{
					return;
				}
				num3 = double.Parse(theString.Substring(num, num2 - num), CurvedValDefinition.culture.NumberFormat);
				this.mInMax = (float)num3;
				num = num2 + 1;
			}
			this.aCurveString = theString.Substring(num);
		}

		public override int GetHashCode()
		{
			return this.originalDef.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CurvedValDefinition curvedValDefinition = obj as CurvedValDefinition;
			return curvedValDefinition != null && curvedValDefinition.originalDef == this.originalDef;
		}

		private static List<CurvedValDefinition> allDefinitions = new List<CurvedValDefinition>();

		public static CurvedValDefinition mRainbowSizeCurve;

		public static CurvedValDefinition mRainbowAlphaCurve;

		public static CurvedValDefinition mSpeedBonusDispCurve;

		public static CurvedValDefinition mGameSpeedCurve;

		public static CurvedValDefinition mTimeUpAlphaCurve;

		public static CurvedValDefinition mTimeUpScaleCurve;

		public static CurvedValDefinition mSlotsAlphaCurve;

		public static CurvedValDefinition mSlotsSpinPctCurve;

		public static CurvedValDefinition mNovaScaleCurve;

		public static CurvedValDefinition mNovaAlphaCurve;

		public static CurvedValDefinition mNukeScaleCurve;

		public static CurvedValDefinition mNukeAlphaCurve;

		public static CurvedValDefinition mSpeedBonusDispCurve3;

		public static CurvedValDefinition mSpeedBonusPointsGlowCurve;

		public static CurvedValDefinition mSpeedBonusPointsScaleCurve2;

		public static CurvedValDefinition mComboFlashPctCurve;

		public static CurvedValDefinition theSelectedmSelectorAlphaCurve;

		public static CurvedValDefinition aPiecemDestPctCurve;

		public static CurvedValDefinition aPiecemAlphaCurve;

		public static CurvedValDefinition mGoCurve;

		public static CurvedValDefinition mSpeedBonusDispCurve2;

		public static CurvedValDefinition mSpeedBonusPointsScaleCurve;

		public static CurvedValDefinition mSwapPctCurve;

		public static CurvedValDefinition mGemScaleCurve;

		public static CurvedValDefinition aSwapDatamSwapPctCurve;

		public static CurvedValDefinition aSwapDatamScaleCurve;

		public static CurvedValDefinition pieceShrinkScaleCurve;

		public static CurvedValDefinition pieceMatchGrowScaleCurve;

		public static CurvedValDefinition pieceCascadeGrowScaleCurve;

		public static CurvedValDefinition lightningZapAlphaCurve;

		public static CurvedValDefinition complementAlphaCurve;

		public static CurvedValDefinition complementScaleCurve;

		public readonly string originalDef;

		public int aVersion;

		public int aFlags;

		public bool mIsHermite;

		public float mOutMin;

		public float mOutMax;

		public float mIncRate;

		public float mInMax;

		public string aCurveString;

		private static CultureInfo culture = CultureInfo.InvariantCulture;
	}
}
