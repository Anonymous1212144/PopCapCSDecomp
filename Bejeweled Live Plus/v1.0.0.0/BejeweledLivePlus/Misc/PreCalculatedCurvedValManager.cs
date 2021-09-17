using System;
using System.Collections.Generic;
using SexyFramework;

namespace BejeweledLivePlus.Misc
{
	public class PreCalculatedCurvedValManager
	{
		private void CalculateCurvedValsFromConfigFiles()
		{
			Dictionary<string, string> mParams = GlobalMembers.gApp.mSecretModeDataParser.mQuestDataVector[3].mParams;
			Dictionary<string, string> mParams2 = GlobalMembers.gApp.mSecretModeDataParser.mQuestDataVector[2].mParams;
			Dictionary<string, string> mParams3 = GlobalMembers.gApp.mSpeedModeDataParser.mQuestDataVector[0].mParams;
			this.mCurvedValLookupTable[116].SetCurve(mParams["ArtifactPossRange"]);
			this.mCurvedValLookupTable[124].SetCurve(mParams["DarkRockFrequency"]);
			this.mCurvedValLookupTable[125].SetCurve(mParams["MinBrickStrPerLevel"]);
			this.mCurvedValLookupTable[126].SetCurve(mParams["MaxBrickStrPerLevel"]);
			this.mCurvedValLookupTable[127].SetCurve(mParams["EdgeBrickStrPerLevel"]);
			this.mCurvedValLookupTable[128].SetCurve(mParams["MinMineStrPerLevel"]);
			this.mCurvedValLookupTable[129].SetCurve(mParams["MaxMineStrPerLevel"]);
			this.mCurvedValLookupTable[130].SetCurve(mParams["MineProbPerLevel"]);
			this.mCurvedValLookupTable[158].SetCurve(mParams["ArtifactSpread"]);
			this.mCurvedValLookupTable[159].SetCurve(mParams["BrickStrSpread"]);
			this.mCurvedValLookupTable[160].SetCurve(mParams["MineStrSpread"]);
			this.mCurvedValLookupTable[230].SetCurve(mParams2["ColComboCoolDownVsCount"]);
			this.mCurvedValLookupTable[235].SetCurve(mParams2["RowFireSpeed"]);
			this.mCurvedValLookupTable[236].SetCurve(mParams2["LevelProgress"]);
			this.mCurvedValLookupTable[237].SetCurve(mParams2["ColCountOverTime"]);
			this.mCurvedValLookupTable[238].SetCurve(mParams2["ColDistribution"]);
			this.mCurvedValLookupTable[239].SetCurve(mParams2["ReprieveStrVsRow"]);
			this.mCurvedValLookupTable[346].SetCurve(mParams3["5SecChanceCurve"]);
			this.mCurvedValLookupTable[347].SetCurve(mParams3["10SecChanceCurve"]);
		}

		private void CalculateAllCurvedVals()
		{
			this.mCurvedValLookupTable[0].SetCurve("b+0,1,0.003333,1,#### Q~###      ~~### O####");
			this.mCurvedValLookupTable[1].SetCurve("b+0,1,0.003333,1,#### Q~###       #~### O####");
			this.mCurvedValLookupTable[3].SetCurve("b+0,1,0.004,1,#### F####  (~###    +~###  M####");
			this.mCurvedValLookupTable[4].SetCurve("b+0,2,0.004,1,#### D####  ,P}}}    +P###  K####");
			this.mCurvedValLookupTable[5].SetCurve("b+1,10,0.004,1,~### D~i%C  ,####      S####");
			this.mCurvedValLookupTable[6].SetCurve("b+0,1,0.01,1.5,#### Q~###       %~### M####");
			this.mCurvedValLookupTable[7].SetCurve("b+0,1,0.003571,1,#### Q~###        5~###=####");
			this.mCurvedValLookupTable[8].SetCurve("b+0,2,0.003571,1,#### QP,l6      SD|H, z#:)I");
			this.mCurvedValLookupTable[9].SetCurve("b;0,1,0.002,1,####         ~~###");
			this.mCurvedValLookupTable[10].SetCurve("b;0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[11].SetCurve("b;0,0.5,0.02,1,####~~###  B####zh###  C####~P###  B####");
			this.mCurvedValLookupTable[12].SetCurve("b30,1,0.033333,1,#         ~~");
			this.mCurvedValLookupTable[13].SetCurve("b+0,1,0.005,1,~###         ~####");
			this.mCurvedValLookupTable[14].SetCurve("b+0,1,0.005,1,~###         ~#A?v");
			this.mCurvedValLookupTable[15].SetCurve("b+0,1,0.005,1,~###         ~#A?v");
			this.mCurvedValLookupTable[16].SetCurve("b30,1,0.002,1,#         ~~");
			this.mCurvedValLookupTable[17].SetCurve("b+0,1,0.005,1,~###         Q~###O####");
			this.mCurvedValLookupTable[18].SetCurve("b+0,1,0.005,1,#### 9~###        V~###4####");
			this.mCurvedValLookupTable[19].SetCurve("b+0,1,0.005,1,~###       P~###  $####O####");
			this.mCurvedValLookupTable[20].SetCurve("b+0,2,0.033333,1,####         ~P###");
			this.mCurvedValLookupTable[23].SetCurve("b+1,0,0.03,1,~###,####         u####");
			this.mCurvedValLookupTable[24].SetCurve("b+1,0,0.03,1,~### )~###      g####  4####");
			this.mCurvedValLookupTable[25].SetCurve("b+0,1,0.005,1,####H####         *~###Q~###");
			this.mCurvedValLookupTable[26].SetCurve("b+0,1,0.004,1,####H####         *~###Q~###");
			this.mCurvedValLookupTable[27].SetCurve("b+0,1,0.03,1,~### )~###      g####  4####");
			this.mCurvedValLookupTable[28].SetCurve("b+0,1,0.01,1,~###    p~###     1####");
			this.mCurvedValLookupTable[29].SetCurve("b+1,0,0.03,1,~###         ~#########");
			this.mCurvedValLookupTable[30].SetCurve("b+0,1,0.03,1,~###         ~#########");
			this.mCurvedValLookupTable[31].SetCurve("b+0,1,0.015,1,####H####         *~###Q~###");
			this.mCurvedValLookupTable[32].SetCurve("b+0,1,0.015,1,~### )~###      g####  4####");
			this.mCurvedValLookupTable[33].SetCurve("b+-1,1,0.033333,1,~x:*    }Ppt+     $#LPR");
			this.mCurvedValLookupTable[34].SetCurve("b+-1,1,0.022222,1,#$)h B####    cObb4   z~LPQ");
			this.mCurvedValLookupTable[35].SetCurve("b+-1,1,0.033333,1,~x:)    }Ppt+     $#LPQ");
			this.mCurvedValLookupTable[36].SetCurve("b+-1,1,0.022222,1,#$)g B####    cObb3   z~LPQ");
			this.mCurvedValLookupTable[37].SetCurve("b+-0.075,0.075,0.033333,1,P&X%     '~###    zPL=I");
			this.mCurvedValLookupTable[38].SetCurve("b+-0.075,0.075,0.022222,1,P#FJ FP###    C~###    :PL=H");
			this.mCurvedValLookupTable[39].SetCurve("b+0,1,0.01,1,####         ~~###");
			this.mCurvedValLookupTable[40].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[41].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[42].SetCurve("b+0,1,0.01,1,####         ~~###");
			this.mCurvedValLookupTable[43].SetCurve("b;0,1,0.013333,1,#### d~### hR### x}###    Y####");
			this.mCurvedValLookupTable[46].SetCurve("b;0,1,0.01,0.25,####  &O###       {####");
			this.mCurvedValLookupTable[47].SetCurve("b+0,1,0.05,1,####         ~~###");
			this.mCurvedValLookupTable[48].SetCurve("b+0,1,0.003333,1,~###       +~###  @####X####");
			this.mCurvedValLookupTable[49].SetCurve("b+0,1,0.033333,1,#### ;I-7l        f####");
			this.mCurvedValLookupTable[50].SetCurve("b+1,2,0.033333,1,####  >4###       c####");
			this.mCurvedValLookupTable[51].SetCurve("b+0,1,0.003333,1,~###  RF###  t`###    X####");
			this.mCurvedValLookupTable[52].SetCurve("b+0,2,0.003333,1,P###  RF###  t`###    X####");
			this.mCurvedValLookupTable[53].SetCurve("b-0,1,0.006667,1,#### 3{### oO### jk### TI### ]W###  &####");
			this.mCurvedValLookupTable[54].SetCurve("b;0,1,0.01,1,####     $~###    }####");
			this.mCurvedValLookupTable[55].SetCurve("b;0,1,0.02,1,####         ~~###");
			this.mCurvedValLookupTable[56].SetCurve("b;0,1,0.02,1,~###         ~####");
			this.mCurvedValLookupTable[57].SetCurve("b;0,1,0.01,1,####      C####   ^~###");
			this.mCurvedValLookupTable[58].SetCurve("b;0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[59].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[60].SetCurve("b+0,1,0.006667,1,####M&0:N uP###   OP###   Nz-xH`~P##");
			this.mCurvedValLookupTable[66].SetCurve("b+0,1,0.008333,1,~###         ~~###");
			this.mCurvedValLookupTable[65].SetCurve("b+0,1,0.02,1,####         ~~###");
			this.mCurvedValLookupTable[61].SetCurve("b+0,1.25,0,1,F### q[/8@sPXY5  d~###    U8=f}");
			this.mCurvedValLookupTable[67].SetCurve("b+0,1.2,0,1,####    {~###     &####");
			this.mCurvedValLookupTable[62].SetCurve("b+0,1,0,1,####     6~###    k~###");
			this.mCurvedValLookupTable[68].SetCurve("b+0,1,0,1,####  `~###    v~###  I####");
			this.mCurvedValLookupTable[63].SetCurve("b+0,-80,0,1,####     g####  7~|4(  &#;-0");
			this.mCurvedValLookupTable[69].SetCurve("b+0,-80,0,1,####         ~####");
			this.mCurvedValLookupTable[64].SetCurve("b+0,1,0,1,####     $~###    }####");
			this.mCurvedValLookupTable[70].SetCurve("b+0,1,0.006667,1,~###      U~###   K####");
			this.mCurvedValLookupTable[71].SetCurve("b+0,1,0.066667,1,####         ~~###");
			this.mCurvedValLookupTable[72].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[73].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[74].SetCurve("b+1,2,0.008,1,$###7h6t6qjk=] ,.n[(  c####     .####");
			this.mCurvedValLookupTable[75].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[77].SetCurve("b+1,2,0.008,1,$###7h6t6qjk=] ,.n[(  c####     .####");
			this.mCurvedValLookupTable[78].SetCurve("b+0,1,0.005714,1,#### 0~r2d        q#G_h");
			this.mCurvedValLookupTable[79].SetCurve("b+0,1,0,1,#7+F  td,P9       -~P##");
			this.mCurvedValLookupTable[80].SetCurve("b;0,1,0.028571,1,####         ~~###");
			this.mCurvedValLookupTable[81].SetCurve("b+-200,1500,0.006667,1,#0zN         ~~W7v");
			this.mCurvedValLookupTable[82].SetCurve("b+0,1,0,1,####         ~~###");
			this.mCurvedValLookupTable[83].SetCurve("b+0,1,0.0025,1,~###    *~### Y#### ]####  b~Qf9");
			this.mCurvedValLookupTable[84].SetCurve("b;0,1,0.02,1,~###         ~#EAC");
			this.mCurvedValLookupTable[85].SetCurve("b;1,1.2,0.02,1,####         ~~^bn");
			this.mCurvedValLookupTable[86].SetCurve("b+0.26,1,0.008333,1,#&Mg      /~a[J   r~P##");
			this.mCurvedValLookupTable[87].SetCurve("b+0,2,0.0025,1,P###    -P### faU7*#0###    1P###");
			this.mCurvedValLookupTable[90].SetCurve("b+0,-234,0.00625,1,####    G####     Y~Ws|");
			this.mCurvedValLookupTable[91].SetCurve("b+0,-234,0.009091,1,~},XB~P## ;olsb        F#O%V");
			this.mCurvedValLookupTable[92].SetCurve("b+0,-234,0.009091,1,~},XB~P## ;ols`        F#O%V");
			this.mCurvedValLookupTable[93].SetCurve("b+0,-450,0.0025,1,####  7~###   T~########    8#P##");
			this.mCurvedValLookupTable[96].SetCurve("b#0,50,0.0065,2,#    %#  (j   w~");
			this.mCurvedValLookupTable[97].SetCurve("b+0,1,0,2,~###        >~}ir 3eiIFJ)hD5+#Mfq");
			this.mCurvedValLookupTable[98].SetCurve("b#0,12,0,2,# _#  |`  g~  [}");
			this.mCurvedValLookupTable[99].SetCurve("b#0,1,0,1,~       1w  p#");
			this.mCurvedValLookupTable[100].SetCurve("b#1,2,0,1,# E# d7ae m6  P# w#");
			this.mCurvedValLookupTable[101].SetCurve("b#0,1,0.004,2,# V: Yf Y] T~   b~");
			this.mCurvedValLookupTable[102].SetCurve("b+0.1,7,0.005,1,~}M`  &~###       {#M5h");
			this.mCurvedValLookupTable[103].SetCurve("b+0.1,6,0.0125,1,#)EK         ~~aWC");
			this.mCurvedValLookupTable[104].SetCurve("b+0,1,0,1,~###      N~###   $####Q####");
			this.mCurvedValLookupTable[105].SetCurve("b+0,1,0,1,#### V*0wJ     W~###  q~###");
			this.mCurvedValLookupTable[106].SetCurve("b+0,1,0.005,1,####         ~~###");
			this.mCurvedValLookupTable[110].SetCurve("b;-3,3,0.003333,1,####     $~###    }####");
			this.mCurvedValLookupTable[111].SetCurve("b;-3,3,0.005,1,####     $~###    }####");
			this.mCurvedValLookupTable[114].SetCurve("b+1,1.3,0,1,####         ~~###");
			this.mCurvedValLookupTable[115].SetCurve("b+0,1,0.25,1,####H####         ;~###@~###");
			this.mCurvedValLookupTable[117].SetCurve("b+0,1,0.015,1.2,~###   R~kS8     g####f#Me5");
			this.mCurvedValLookupTable[118].SetCurve("b;0,1,0.01,1,~###  (~###       y#Me5");
			this.mCurvedValLookupTable[119].SetCurve("b+0,1,0.01,0.75,#########    Y~###     G####");
			this.mCurvedValLookupTable[120].SetCurve("b;0,1,0.01,1,####    ^~###     C####");
			this.mCurvedValLookupTable[121].SetCurve("b+0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[122].SetCurve("b+0,1,0.01,1.5,#### O####        Q~###");
			this.mCurvedValLookupTable[123].SetCurve("b+0,7,0.01,1.5,#7GX  |~###    =~P##  f#:g2");
			this.mCurvedValLookupTable[132].SetCurve("b-0,1,0.01,2,####  `D2UB       A~###");
			this.mCurvedValLookupTable[131].SetCurve("b-0,1,0.01,0.75,####         ~~####~####~###");
			this.mCurvedValLookupTable[134].SetCurve("b-0,1,0.01,2,####        'o15{ z~Xdl");
			this.mCurvedValLookupTable[135].SetCurve("b-0,255,0.01,0.5,~###         ~#=wH");
			this.mCurvedValLookupTable[136].SetCurve("b+1,1.3,0.01,2,#1eQ   }~### d####    >####");
			this.mCurvedValLookupTable[137].SetCurve("b+0,1,0.01,0.75,####     g####    :~####~####~###");
			this.mCurvedValLookupTable[138].SetCurve("b+0,1,0.01,1,~###  q####       0####");
			this.mCurvedValLookupTable[139].SetCurve("b+0,1.3,0.01,2,j###   }~### Q~###  *~### o)###X*###");
			this.mCurvedValLookupTable[140].SetCurve("b+0,1,0.01,1.5,####j#### [~###     :~### x####H####");
			this.mCurvedValLookupTable[141].SetCurve("b+0,200,0.01,1.5,#########  x~###   k~###   :####%#########");
			this.mCurvedValLookupTable[142].SetCurve("b+0.2,1,0.01,1,~###     -~###    t#><@");
			this.mCurvedValLookupTable[143].SetCurve("b+0.2,1,0.005,1,~###     -~###    t#><@");
			this.mCurvedValLookupTable[144].SetCurve("b+0.2,1,0.01,1,~###     -~###    8####_####");
			this.mCurvedValLookupTable[145].SetCurve("b+0.5,0.5,0.01,3,####   TP###   RP###  x~###");
			this.mCurvedValLookupTable[146].SetCurve("b+0.25,1.0,0.01,3,####   /~###   t~###  |####");
			this.mCurvedValLookupTable[147].SetCurve("b+0,1,0.01,3,####V#### Q~###      -~### <####S####");
			this.mCurvedValLookupTable[148].SetCurve("b+0.5,2.5,0.01,3,####         ~~###");
			this.mCurvedValLookupTable[149].SetCurve("b+0.5,0.5,0.01,3,####   TP###   RP###  x~###");
			this.mCurvedValLookupTable[150].SetCurve("b+1.0,4.5,0.01,3,~### O}o>m R####      }####");
			this.mCurvedValLookupTable[151].SetCurve("b+0,0.75,0.01,3,#### V#### X~###X~### ;####    }####");
			this.mCurvedValLookupTable[152].SetCurve("b+0.5,2.5,0.01,3,####         ~~###");
			this.mCurvedValLookupTable[153].SetCurve("b+-0.5,1.5,0.01,6,####  &P###     }P### |~###");
			this.mCurvedValLookupTable[154].SetCurve("b+0.45,1.25,0.01,6,####7#### j~cWR      %~hsT ]####B####");
			this.mCurvedValLookupTable[155].SetCurve("b+0,1,0.01,6,####D#### `~###     k~### t####=####");
			this.mCurvedValLookupTable[156].SetCurve("b+10,30,0.01,0.6,####    k~###     6#P##");
			this.mCurvedValLookupTable[157].SetCurve("b+0,1,0.01,1,####      6#'uU=#### ^~fe+ q~R3E");
			this.mCurvedValLookupTable[161].SetCurve("b+0,1,0.013333,1,#.<_         ~~Vf2");
			this.mCurvedValLookupTable[163].SetCurve("b+0,1,0.013333,1,####  >~###       c####");
			this.mCurvedValLookupTable[164].SetCurve("b+0,175,0.013333,1,#59{         ~~###");
			this.mCurvedValLookupTable[165].SetCurve("b+0,255,0.006667,1,#6=B     $~###    }####");
			this.mCurvedValLookupTable[166].SetCurve("b;0,1,0.01,1,#### *~###      L~###  M####");
			this.mCurvedValLookupTable[167].SetCurve("b+0,1,0.01,1,~###        Q~### O####");
			this.mCurvedValLookupTable[168].SetCurve("b+0,1,0.0025,1,~###        j~### 7####");
			this.mCurvedValLookupTable[169].SetCurve("b+0,4,0.006667,1,#6tu kP###    :M%fw   z~###");
			this.mCurvedValLookupTable[170].SetCurve("b;0,1,0,1,~###q~###        =~###q####");
			this.mCurvedValLookupTable[171].SetCurve("b+0,1,0.01,1,P### ~~###      QPoi: O:###");
			this.mCurvedValLookupTable[172].SetCurve("b;0,1,0.013333,1,~###         ~#P##");
			this.mCurvedValLookupTable[173].SetCurve("b+0,1,0.01,1,#### 0####    C~###    P~###");
			this.mCurvedValLookupTable[174].SetCurve("b+1,-1,0.01,1,P### %m###  kP###~?###     4####");
			this.mCurvedValLookupTable[175].SetCurve("b+1,-1,0.01,1,P###         ~####");
			this.mCurvedValLookupTable[176].SetCurve("b+345,700,0.003333,1,g###    @~### b<k=] X$###  G$P##");
			this.mCurvedValLookupTable[177].SetCurve("b+1390,50,0.0055,1,P###   C6###  9Y4xY  Xx### mD###");
			this.mCurvedValLookupTable[178].SetCurve("b+0,1,0.04,1,####         ~~###");
			this.mCurvedValLookupTable[179].SetCurve("b+0.65,1,0.02,1,~n%T         ~#?j,");
			this.mCurvedValLookupTable[180].SetCurve("b;0,1,0.013333,1,~###         ~#P##");
			this.mCurvedValLookupTable[181].SetCurve("b+0,200,0,1,#5j+     $~###    }#==]");
			this.mCurvedValLookupTable[182].SetCurve("b3-100,0,0.01,1,~         ~#");
			this.mCurvedValLookupTable[183].SetCurve("b+0,2,0,1,P6h{dg###     iC###  *C### KL###");
			this.mCurvedValLookupTable[184].SetCurve("b+0,1,0,1,#### ]~###v####l~###q####m~###s####o~###k####f~### h####");
			this.mCurvedValLookupTable[185].SetCurve("b+0,1,0.05,1,####         ~~###");
			this.mCurvedValLookupTable[186].SetCurve("b+0,1,0.05,1,~###    [~###     E####");
			this.mCurvedValLookupTable[187].SetCurve("b+0,1,0.1,1,~###         ~####");
			this.mCurvedValLookupTable[188].SetCurve("b+0,1,0.01,1,####      j####   7~###");
			this.mCurvedValLookupTable[190].SetCurve("b;0,0.75,0.02,1,~###  f~####~###       ;#@U3");
			this.mCurvedValLookupTable[191].SetCurve("b+0,1,0.04,1,####         ~~###");
			this.mCurvedValLookupTable[192].SetCurve("b;0,1,0.01,0.5,####         ~~b1d");
			this.mCurvedValLookupTable[193].SetCurve("b;0,1,0.01,0.5,~###         ~#>Rf");
			this.mCurvedValLookupTable[194].SetCurve("b;1,4,0.01,0.5,####      S~###   MY###");
			this.mCurvedValLookupTable[195].SetCurve("b;0,2.8,0.01,1,~###M~###   s####   ^####u+### -##############");
			this.mCurvedValLookupTable[196].SetCurve("b;0,1,0.01,0.5,~###  v~###    8O>M_  q####");
			this.mCurvedValLookupTable[197].SetCurve("b;0,1,0.01,1,~### v~###  oFm&F  I####  o####");
			this.mCurvedValLookupTable[198].SetCurve("b;0,1,0.01,0.5,~### h~###       s####D####");
			this.mCurvedValLookupTable[199].SetCurve("b;-1.2,1,0.01,1,~### B~###      'T###  XT###%T###");
			this.mCurvedValLookupTable[200].SetCurve("b;0,50,0.01,0.5,#### v####    X~###   P####");
			this.mCurvedValLookupTable[201].SetCurve("b;0,1,0.01,1,#&(1         ~~_T6");
			this.mCurvedValLookupTable[202].SetCurve("b+1,8,0.004762,1,####     l#Pr]'#*NA    1}dR)");
			this.mCurvedValLookupTable[203].SetCurve("b+0,-234,0.009091,1,####         ~~auJ");
			this.mCurvedValLookupTable[204].SetCurve("b+0,1,0.004444,1,####Q####         O}P8x");
			this.mCurvedValLookupTable[205].SetCurve("b+0.8,2.4,0.008333,1,#.ov         ~~###");
			this.mCurvedValLookupTable[206].SetCurve("b+0,1,0.004348,1,####      W+(q>   I~cu?");
			this.mCurvedValLookupTable[207].SetCurve("b+0,255,0.004255,1,##xa  @L6zN d~}Q&    I~P## T#<G{");
			this.mCurvedValLookupTable[209].SetCurve("b-0.02,1,0.2,1,~rgP         ~#DgP");
			this.mCurvedValLookupTable[210].SetCurve("b+0,1,0.0025,1,~###  .~###   4####    b####");
			this.mCurvedValLookupTable[211].SetCurve("b+0,1,0.003333,1,####   g####     K~###m~###");
			this.mCurvedValLookupTable[213].SetCurve("b;-25,25,0.01,0.25,O###   @j###   iF###  vP###");
			this.mCurvedValLookupTable[214].SetCurve("b=0,1,0.01,0.5,####         ~~###");
			this.mCurvedValLookupTable[215].SetCurve("b;0,45,0.01,0.5,####         ~}aql");
			this.mCurvedValLookupTable[216].SetCurve("b;0,1,0.01,0.5,~###         ~#>&U");
			this.mCurvedValLookupTable[217].SetCurve("b;0,1,0.01,1,####         ~~###");
			this.mCurvedValLookupTable[218].SetCurve("b;0,1,0.01,.5,~###         ~####");
			this.mCurvedValLookupTable[220].SetCurve("b+0.2,1,0.01,1,####         UtaHEK~P##");
			this.mCurvedValLookupTable[221].SetCurve("b;0,1,0.01,0.5,####         ~~###");
			this.mCurvedValLookupTable[222].SetCurve("b;0,1,0.01,0.5,~###         ~####");
			this.mCurvedValLookupTable[223].SetCurve("b+0,1,0.01,1,#.B?      Sb^#n   M~###");
			this.mCurvedValLookupTable[224].SetCurve("b#0,1,0.01,0.5,#         ~~");
			this.mCurvedValLookupTable[225].SetCurve("b;0,1,0.01,2,#77v Z~###     N~###  v#<A6");
			this.mCurvedValLookupTable[226].SetCurve("b;0.25,1.25,0.01,2,#7V. s~###     L~###  `#<R+#####");
			this.mCurvedValLookupTable[227].SetCurve("b;-700,0,0.01,0.5,####o#2}4         2~####~Q2.");
			this.mCurvedValLookupTable[228].SetCurve("b;55,230,0.01,1,~###V~i4O    P#OV7    x##J`##P##");
			this.mCurvedValLookupTable[229].SetCurve("b;0,1,0.01,1,#### ]~### b#### `~### `#### X|### h####");
			this.mCurvedValLookupTable[231].SetCurve("b;0,1,0.01,1,~###   i~###      8####");
			this.mCurvedValLookupTable[232].SetCurve("b;0,1,0.01,0.25,####         ~~###");
			this.mCurvedValLookupTable[233].SetCurve("b;1,1.5,0.008,1,$###7h6t6qjk=] ,.n[(  c####     .####");
			this.mCurvedValLookupTable[234].SetCurve("b+0,100,0.01,1,#### 3####        n~bRG");
			this.mCurvedValLookupTable[240].SetCurve("b;0,8,0.01,0.5,#1QB    t~&KQ     -#EAC");
			this.mCurvedValLookupTable[241].SetCurve("b;0,15,0.01,0.5,#1QB    t~&KQ     -#EAC");
			this.mCurvedValLookupTable[242].SetCurve("b;0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[243].SetCurve("b;0,1,0.01,13,~###        c~### >####");
			this.mCurvedValLookupTable[247].SetCurve("b+0,1,0.01,1,####         ~~b2M");
			this.mCurvedValLookupTable[248].SetCurve("b+0,0.5,0.01,0.1,#5S8         ~~T`9");
			this.mCurvedValLookupTable[249].SetCurve("b+-0.5,0.5,0.01,0.2,P/P3  (o###   /K###  ST###  <PP##");
			this.mCurvedValLookupTable[250].SetCurve("b+0,1,0,1,####  &####       {~###");
			this.mCurvedValLookupTable[251].SetCurve("b;0,1,0.01,10,~###       H~###  X####");
			this.mCurvedValLookupTable[252].SetCurve("b+1,1.5,0.01,1,####         F####'~U=zV~?z4");
			this.mCurvedValLookupTable[253].SetCurve("b+0,5,0.01,1,####         ~~###");
			this.mCurvedValLookupTable[254].SetCurve("b+0,1,0,1,####    $####aa3pl     ?~###");
			this.mCurvedValLookupTable[255].SetCurve("b+0,1,0,1,####  &####       {~###");
			this.mCurvedValLookupTable[256].SetCurve("b+0,1,0.01,1,9###    EH###Ba###   <~### ~~###");
			this.mCurvedValLookupTable[257].SetCurve("b+0,1,0,1,####~####        ~~####~###");
			this.mCurvedValLookupTable[258].SetCurve("b+1.5,3.0,0.013333,1,#4I(         ~~P##");
			this.mCurvedValLookupTable[259].SetCurve("b+0,1,0,1,####    e~###     <####");
			this.mCurvedValLookupTable[260].SetCurve("b+2,4,0.006667,1,####   ^X### bX### S~###   /####");
			this.mCurvedValLookupTable[261].SetCurve("b+0,1,0,1,#### o~###     Y}###  V####");
			this.mCurvedValLookupTable[262].SetCurve("b;0,1,0.00885,1,####oCh;uZV###X^8.tQ<###Uqh*Kzk###QG###R~###hI###u~### $#### 2y### *####");
			this.mCurvedValLookupTable[263].SetCurve("b+0,1,0.015,1,~### )~###     S####   G####");
			this.mCurvedValLookupTable[264].SetCurve("b+0,1,0.015,1,####s####     S~###   X~###");
			this.mCurvedValLookupTable[266].SetCurve("b;0,1,0.00303,1,~###         ~####");
			this.mCurvedValLookupTable[267].SetCurve("b;0,1,0.00125,1,~### Q~###        O####");
			this.mCurvedValLookupTable[269].SetCurve("b;0,1,0.005,1,####         ~~###");
			this.mCurvedValLookupTable[270].SetCurve("b;0,1,0.006667,1,~###         ~####");
			this.mCurvedValLookupTable[271].SetCurve("b;0,1,0.005,1,####         ~~###");
			this.mCurvedValLookupTable[272].SetCurve("b;0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[273].SetCurve("b;0,1,0.002,1,#### O####    R~###   }~###");
			this.mCurvedValLookupTable[274].SetCurve("b;0,1,0.005,1,~###   R~###      N####");
			this.mCurvedValLookupTable[275].SetCurve("b+0,1,0.01,1,####H####         *~###Q~###");
			this.mCurvedValLookupTable[276].SetCurve("b+1,1.2,0.333333,1,#+Ky         ~~###");
			this.mCurvedValLookupTable[277].SetCurve("b+1,1.22,0.033333,1,#+Kx      uw*7u   ,~###");
			this.mCurvedValLookupTable[278].SetCurve("b+1,1.22,0.066667,1,#+Kx      uw*7t   ,~###");
			this.mCurvedValLookupTable[279].SetCurve("b+0,2,0.006667,1,PzL;  >l### ~H### P`### qK### fV###ZP###");
			this.mCurvedValLookupTable[280].SetCurve("b+0,1,0.033333,1,~zL>         %]Bt(|#:M@");
			this.mCurvedValLookupTable[281].SetCurve("b+0,1.2,0.05,1,~###         ~#Blc");
			this.mCurvedValLookupTable[282].SetCurve("b+1,1,0.00125,1,#-{$      E~###   [P###");
			this.mCurvedValLookupTable[283].SetCurve("b+0.25,1,0.05,1,####    :####     g~###");
			this.mCurvedValLookupTable[284].SetCurve("b+0,1,0.033333,1,~r)6         H;?D,X#>3Z");
			this.mCurvedValLookupTable[285].SetCurve("b+0,1,0.066667,1,~###         ~#@yd");
			this.mCurvedValLookupTable[286].SetCurve("b+0,1,0.02,1,~###         ~####");
			this.mCurvedValLookupTable[287].SetCurve("b+0,1,0.033333,1,#.ht         ~~W[d");
			this.mCurvedValLookupTable[288].SetCurve("b;0,1,0.02,1,~###         ~####");
			this.mCurvedValLookupTable[289].SetCurve("b+0,0.55,0.02,1,####         ~~^?L");
			this.mCurvedValLookupTable[290].SetCurve("b+0,1,0.006667,1,#### ;~###       O~### 9####");
			this.mCurvedValLookupTable[291].SetCurve("b+80,64,0.006667,1,####  &}### |####  #~### z####  &~###");
			this.mCurvedValLookupTable[292].SetCurve("b#0,1,0.002857,1,#rA #w +x|<   |#  +#");
			this.mCurvedValLookupTable[294].SetCurve("b390,-90,0.005,1,~         ~#");
			this.mCurvedValLookupTable[295].SetCurve("b;0,0.5,0.030303,1,####  R}###  O####  M~###  R####");
			this.mCurvedValLookupTable[296].SetCurve("b;0,0.5,0.005,1,#### x~###   XY###|s###   P####");
			this.mCurvedValLookupTable[297].SetCurve("b;0,0.5,0.005,1,####     /~###  17###f@### |####");
			this.mCurvedValLookupTable[298].SetCurve("b;0,0.5,0.005,1,####   T~### B5### (@### ')### &@### |####");
			this.mCurvedValLookupTable[299].SetCurve("b+0,1.57,0.01,1,~###   3####      n####");
			this.mCurvedValLookupTable[300].SetCurve("b+-12,8,0.01,0.4,####   M####   W~###  z]###");
			this.mCurvedValLookupTable[301].SetCurve("b;0,2,0.013333,1,####         ~~###");
			this.mCurvedValLookupTable[302].SetCurve("b+0,1,0.015152,1,####    #####     #~###~~###");
			this.mCurvedValLookupTable[303].SetCurve("b+0,1,0.015152,1,####   #####     #~### ~~###");
			this.mCurvedValLookupTable[304].SetCurve("b+0,1,0.015152,1,#### ~####     #~###  ~~###");
			this.mCurvedValLookupTable[305].SetCurve("b+0,1,0.015152,1,####~####     #~###   ~~###");
			this.mCurvedValLookupTable[306].SetCurve("b+0,1,0.015152,1,####     #~###     #~###");
			this.mCurvedValLookupTable[307].SetCurve("b+0,1,0.002222,1,####         ~~###");
			this.mCurvedValLookupTable[308].SetCurve("b+0,1,0.002857,1,####         ~~###");
			this.mCurvedValLookupTable[309].SetCurve("b+0,1,0.05,1,#5T8     $~###    }#=Pd");
			this.mCurvedValLookupTable[310].SetCurve("b+0,1,0.013333,1,~zc*         ~#@T=");
			this.mCurvedValLookupTable[311].SetCurve("b30,1,0.013333,1,#         ~~");
			this.mCurvedValLookupTable[312].SetCurve("b;0,1,0.02,1,~###         ~####");
			this.mCurvedValLookupTable[313].SetCurve("b;0,1,0.02,1,####         ~~###");
			this.mCurvedValLookupTable[314].SetCurve("b#0,1,0.006667,1,#         ~~");
			this.mCurvedValLookupTable[315].SetCurve("b#0,1,0.004,1,#         ~~");
			this.mCurvedValLookupTable[316].SetCurve("b+0,1,0,1,#### #~###       #~### ~####");
			this.mCurvedValLookupTable[317].SetCurve("b+0,1,0,1,####  &~###       {~###");
			this.mCurvedValLookupTable[318].SetCurve("b+0,300,0,1,####~~###~####       ~####");
			this.mCurvedValLookupTable[319].SetCurve("b+0,1,0,1,####     P####  P~### ~~###");
			this.mCurvedValLookupTable[320].SetCurve("b+0,1,0,1,#### ~####}h###     $~### ~~###");
			this.mCurvedValLookupTable[321].SetCurve("b+0,1,0,1,#5f=     W~###    I3==[");
			this.mCurvedValLookupTable[322].SetCurve("b+0,1,0,1,~###       P~###  P#;bE");
			this.mCurvedValLookupTable[323].SetCurve("b+0,1,0,1,#### ~~###       ~~###");
			this.mCurvedValLookupTable[324].SetCurve("b+0,1,0,1,~###       ~~### ~####");
			this.mCurvedValLookupTable[325].SetCurve("b+0,20,0,1,####     $~###    }####");
			this.mCurvedValLookupTable[326].SetCurve("b+0,1,0,1,~###   $####    $####  |~###");
			this.mCurvedValLookupTable[327].SetCurve("b+0,1,0,1,####         ~~###");
			this.mCurvedValLookupTable[328].SetCurve("b+1,2,0,1,#5h@    }~###     $#=9(");
			this.mCurvedValLookupTable[329].SetCurve("b+0,255,0,1,#### #~###       ~~###~####");
			this.mCurvedValLookupTable[330].SetCurve("b;0,1,0.01,1,#### ;~### 7P### >~### 7P### :~### 9P### 8~### :####");
			this.mCurvedValLookupTable[331].SetCurve("b+0,1,0,1,####     $~###    }P###");
			this.mCurvedValLookupTable[332].SetCurve("b+0,1,0,1,####  #~###     ~~### ~####");
			this.mCurvedValLookupTable[333].SetCurve("b+0,1,0,1,P### #h1}G    &~###   {ho%l~####");
			this.mCurvedValLookupTable[334].SetCurve("b+0,1,0,1,#### #}###       ~~###~####");
			this.mCurvedValLookupTable[335].SetCurve("b+0,1,0,1,#### #}###       ~~###~####");
			this.mCurvedValLookupTable[336].SetCurve("b+0,1,0,1,#### #}###       ~~###~####");
			this.mCurvedValLookupTable[338].SetCurve("b+0,1.3,0,0.2,#6g<     8~###    ii###");
			this.mCurvedValLookupTable[339].SetCurve("b+0,1,0,0.2,~###         ~#>Hu");
			this.mCurvedValLookupTable[340].SetCurve("b+0,1,0.004167,1,#### 9~### (#### 5g### .#### 3W### ;#### BJ### X####");
			this.mCurvedValLookupTable[341].SetCurve("b+0,0.5,0.1,1,####    }~###     $####");
			this.mCurvedValLookupTable[342].SetCurve("b30,1,0.028571,1,#         ~~");
			this.mCurvedValLookupTable[343].SetCurve("b+1,1.5,0,1,####   P~###      P####");
			this.mCurvedValLookupTable[344].SetCurve("b#1,2,0,1,#         ~~");
			this.mCurvedValLookupTable[345].SetCurve("b+0,1,0,1,####     $~###    }####");
			this.mCurvedValLookupTable[349].SetCurve("b;0,1,0.01,1,~###         ~####");
			this.mCurvedValLookupTable[350].SetCurve("b;0,1,0.02,1,#.^$         ~~###");
			this.mCurvedValLookupTable[351].SetCurve("b;0,1,0.05,1,~###         ~####");
			this.mCurvedValLookupTable[352].SetCurve("b+0,1,0.008333,1,####    v####     +~###");
			this.mCurvedValLookupTable[353].SetCurve("b+0,1,0.01,1,~###     N~###    R####");
			this.mCurvedValLookupTable[354].SetCurve("b-0,1,0.016667,2,####   R0+vy      N~TJe");
			this.mCurvedValLookupTable[355].SetCurve("b-0,1,0.01,2,####  `D2UB       A~###");
			this.mCurvedValLookupTable[356].SetCurve("b-0,1,0,1,~###      H~###   X####");
			this.mCurvedValLookupTable[357].SetCurve("b;1,3,0.01,1,~###  q####       0####");
			this.mCurvedValLookupTable[358].SetCurve("b+0,1,0.01,1,####         ~~###");
			this.mCurvedValLookupTable[360].SetCurve("b+0,1,0.003333,1,####  @####       a~###");
			this.mCurvedValLookupTable[361].SetCurve("b+0,1,0.005,1,~###         ~####");
			this.mCurvedValLookupTable[362].SetCurve("b+0,1,0.005,1,####         ~~###");
			this.mCurvedValLookupTable[363].SetCurve("b+0,1,0.000667,1,#6P- {p4`;t~P## ,~###   R####  R####");
			this.mCurvedValLookupTable[364].SetCurve("b;0,1,0.025,1,~###         ~~###");
			this.mCurvedValLookupTable[365].SetCurve("b+0,3,0,1,)0rG         ~~P##");
			this.mCurvedValLookupTable[366].SetCurve("b+0,1,0,1,####     8fjdU    i#D2e");
			this.mCurvedValLookupTable[367].SetCurve("b+0.5,1,0.02,1,~iev         ~~###");
			this.mCurvedValLookupTable[368].SetCurve("b+0,-50,0.003333,1,####   ^#1T6      C~[MZ");
			this.mCurvedValLookupTable[369].SetCurve("b+0,1,0.003333,1,~###     U~}+`    K#;t/");
			this.mCurvedValLookupTable[370].SetCurve("b+0,1,0.003333,1,####     1####  t~### z~###");
			this.mCurvedValLookupTable[371].SetCurve("b+0,1,0.003333,1,~###       A~###  `####");
			this.mCurvedValLookupTable[372].SetCurve("b+0,1,0.003333,1,####      r#### `~### M~###");
			this.mCurvedValLookupTable[373].SetCurve("b+0,2,0.003333,1,####       4#### `o### 0P###");
			this.mCurvedValLookupTable[374].SetCurve("b+0,2,0.003333,1,P###       4P### Id### FP###");
		}

		public PreCalculatedCurvedValManager()
		{
			for (int i = 0; i < 375; i++)
			{
				this.mCurvedValLookupTable[i] = new CurvedVal();
			}
			this.CalculateAllCurvedVals();
		}

		public bool GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID index, CurvedVal cv)
		{
			return this.GetCurvedVal(index, cv, null);
		}

		public bool GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID index, CurvedVal cv, CurvedVal linkPtr)
		{
			if (index < PreCalculatedCurvedValManager.CURVED_VAL_ID.eNUMBER_CURVED_VALS)
			{
				cv.CopyFrom(this.mCurvedValLookupTable[(int)index]);
				if (cv.mAppUpdateCountSrc != null)
				{
					cv.mInitAppUpdateCount = cv.mAppUpdateCountSrc;
				}
				if (linkPtr != null)
				{
					cv.mLinkedVal = linkPtr;
				}
				return true;
			}
			return false;
		}

		public bool GetCurvedValMult(PreCalculatedCurvedValManager.CURVED_VAL_ID index, CurvedVal cv)
		{
			return this.GetCurvedValMult(index, cv, null);
		}

		public bool GetCurvedValMult(PreCalculatedCurvedValManager.CURVED_VAL_ID index, CurvedVal cv, CurvedVal linkPtr)
		{
			float num = (float)cv.GetOutVal();
			if (this.GetCurvedVal(index, cv, linkPtr))
			{
				cv.mOutMax *= (double)num;
				return true;
			}
			return false;
		}

		private CurvedVal[] mCurvedValLookupTable = new CurvedVal[375];

		public enum CURVED_VAL_ID
		{
			eANNOUNCEMENT_ALPHA,
			eANNOUNCEMENT_SCALE,
			eANNOUNCEMENT_HORZ_SCALE_MULT,
			eANNOUNCEMENT_ALPHA_BOARD,
			eANNOUNCEMENT_SCALE_BOARD,
			eANNOUNCEMENT_HORZ_SCALE_MULT_BOARD,
			eANNOUNCEMENT_ALPHA_QUEST_1,
			eANNOUNCEMENT_ALPHA_QUEST_2,
			eANNOUNCEMENT_SCALE_QUEST,
			eBACKGROUND_UPDATE_SPEED,
			eBACKGROUND_IMAGE_OVERLAY_ALPHA,
			eBACKGROUND_FLASH,
			eBADGE_MENU_ANIM_PCT_1,
			eBADGE_MENU_SCALE_1,
			eBADGE_MENU_ALPHA_1,
			eBADGE_MENU_AWARD_SHADOW_ALPHA_1,
			eBADGE_MENU_ANIM_PCT_2,
			eBADGE_MENU_SCALE_2,
			eBADGE_MENU_ALPHA_2,
			eBADGE_MENU_AWARD_SHADOW_ALPHA_2,
			eBADGE_MENU_WIDGET_SCALE,
			eBALANCE_BOARD_RED_GLOW,
			eBALANCE_BOARD_BLUE_GLOW,
			eBEJ3_WIDGET_SHOW_CURVE,
			eBEJ3_WIDGET_SHOW_CURVE_BOARD,
			eBEJ3_WIDGET_SHOW_CURVE_MAIN_MENU,
			eBEJ3_WIDGET_SHOW_CURVE_MENU_BACKGROUND,
			eBEJ3_WIDGET_HIDE_CURVE,
			eMENUBACKGROUND_HIDE_CURVE,
			eBEJ3_WIDGET_TRANSITION_FADE_IN,
			eBEJ3_WIDGET_TRANSITION_FADE_OUT,
			eBEJ3_DIALOG_SCALE_START,
			eBEJ3_DIALOG_SCALE_KILL,
			eBOARD_SWAP_PCT_1,
			eBOARD_SWAP_PCT_2,
			eBOARD_SWAP_PCT_3,
			eBOARD_SWAP_PCT_4,
			eBOARD_GEM_SCALE_1,
			eBOARD_GEM_SCALE_2,
			eBOARD_SLIDING_HUD_CURVE_OVER,
			eBOARD_SLIDING_HUD_CURVE_BACK,
			eBOARD_TRANSITION_BOARD_CURVE_OPEN,
			eBOARD_TRANSITION_BOARD_CURVE_CLOSE,
			eBOARD_LEVEL_BAR_BONUS_ALPHA,
			eBOARD_SPEED_MODE_FACTOR,
			eBOARD_SPEEDOMETER_POPUP,
			eBOARD_SPEEDOMETER_GLOW,
			eBOARD_SPEED_BONUS_DISP_ON,
			eBOARD_SPEED_BONUS_DISP_OFF,
			eBOARD_SPEED_BONUS_POINTS_GLOW,
			eBOARD_SPEED_BONUS_POINTS_SCALE_ON,
			eBOARD_SPEED_BONUS_POINTS_SCALE_OFF_NORMAL,
			eBOARD_SPEED_BONUS_POINTS_SCALE_OFF_UREPLAY,
			eBOARD_COMBO_FLASH_PCT,
			eBOARD_REPLAY_PULSE_PCT,
			eBOARD_REPLAY_WIDGET_SHOW_PCT,
			eBOARD_REPLAY_WIDGET_HIDE_PCT,
			eBOARD_REPLAY_FADEOUT_TO_WHITE,
			eBOARD_REPLAY_FADEOUT_TO_CLEAR,
			eBOARD_PREV_POINT_MULT_ALPHA,
			eBOARD_POINT_MULT_POS_PCT_1,
			eBOARD_POINT_MULT_SCALE_1,
			eBOARD_POINT_MULT_ALPHA_1,
			eBOARD_POINT_MULT_Y_ADD_1,
			eBOARD_POINT_MULT_DARKEN_PCT,
			eBOARD_POINT_MULT_TEXT_MORPH,
			eBOARD_POINT_MULT_POS_PCT_2,
			eBOARD_POINT_MULT_SCALE_2,
			eBOARD_POINT_MULT_ALPHA_2,
			eBOARD_POINT_MULT_Y_ADD_2,
			eBOARD_TIMER_INFLATE,
			eBOARD_TIMER_ALPHA,
			eBOARD_GEM_COUNT_ALPHA,
			eBOARD_GEM_SCALAR_ALPHA,
			eBOARD_GEM_COUNT_CURVE,
			eBOARD_CASCADE_COUNT_ALPHA,
			eBOARD_CASCADE_SCALAR_ALPHA,
			eBOARD_CASCADE_COUNT_CURVE,
			eBOARD_COMPLEMENT_ALPHA,
			eBOARD_COMPLEMENT_SCALE,
			eBOARD_TUTORIAL_PIECE_IRIS_PCT,
			eBOARD_SUN_POSITION,
			eBOARD_COIN_CATCHER_PCT,
			eBOARD_ALPHA_LEVEL_UP,
			eBOARD_ALPHA_BACK_TO_MENU,
			eBOARD_SCALE_BACK_TO_MENU,
			eBOARD_SCALE_HYPERSPACE_ZOOM,
			eBOARD_SCALE_LEVEL_UP,
			eBOARD_SLIDE_UI_PCT,
			eBOARD_SIDE_ALPHA,
			eBOARD_SIDE_X_OFFSET_HYPER_START,
			eBOARD_SIDE_X_OFFSET_HYPER_SLIDE,
			eBOARD_SIDE_X_OFFSET_HYPER_END,
			eBOARD_SIDE_X_OFFSET_LEVEL_UP,
			eBOARD_BOOST_SHOW_PCT,
			eBOARD_RESTART_PCT,
			eBOARD_NUKE_RADIUS,
			eBOARD_NUKE_ALPHA,
			eBOARD_NOVA_RADIUS,
			eBOARD_NOVA_ALPHA,
			eBOARD_GAME_OVER_PIECE_SCALE,
			eBOARD_GAME_OVER_PIECE_GLOW,
			eBOARD_QUEST_PORTAL_PCT_OPEN,
			eBOARD_QUEST_PORTAL_PCT_CLOSE,
			eBOARD_QUEST_PORTAL_CENTER_PCT_OPEN,
			eBOARD_QUEST_PORTAL_CENTER_PCT_CLOSE,
			eBUTTERFLY_SPIDER_WALK_PCT,
			eCRYSTAL_BALL_ALPHA,
			eCRYSTAL_BALL_FULL_PCT,
			eCRYSTAL_BALL_SCALE,
			eCRYSTAL_BALL_X_BOB,
			eCRYSTAL_BALL_Y_BOB,
			eCRYSTAL_BALL_LEFT_ARROW_PCT,
			eCRYSTAL_BALL_RIGHT_ARROW_PCT,
			eCRYSTAL_BALL_DRAW_SCALE_CURVE,
			eDIG_BOARD_CV_CRACK,
			eDIG_GOAL_ARTIFACT_POSS_RANGE,
			eDIG_GOAL_INIT_PUSH_ANIM_CV,
			eDIG_GOAL_INIT_PUSH_ANIM_CV_CHEAT,
			eDIG_GOAL_DIG_BAR_FLASH,
			eDIG_GOAL_DIG_BAR_FLASH_CHEAT,
			eDIG_GOAL_DIG_BAR_FLASH_OFF,
			eDIG_GOAL_CV_SCROLL_Y,
			eDIG_GOAL_CV_SHAKEY,
			eDIG_GOAL_CV_DARK_ROCK_FREQ,
			eDIG_GOAL_CV_MIN_BRICK_STR,
			eDIG_GOAL_CV_MAX_BRICK_STR,
			eDIG_GOAL_CV_EDGE_BRICK_STR_PER_LEVEL,
			eDIG_GOAL_CV_MIN_MINE_STR,
			eDIG_GOAL_CV_MAX_MINE_STR,
			eDIG_GOAL_CV_MINE_PROB,
			eDIG_GOAL_SPLINE_INTERP_ARTIFACT,
			eDIG_GOAL_SPLINE_INTERP,
			eDIG_GOAL_SPLINE_INTERP_UPDATE,
			eDIG_GOAL_SPLINE_INTERP_DIG_COIN,
			eDIG_GOAL_ALPHA_OUT,
			eDIG_GOAL_SCALE_CV,
			eDIG_GOAL_SCALE_CV_ARTIFACT,
			eDIG_GOAL_SCALE_CV_UPDATE,
			eDIG_GOAL_SCALE_CV_DIG_COIN,
			eDIG_GOAL_POINT_BLINK_CV,
			eDIG_GOAL_STREAMER_MAG,
			eDIG_GOAL_PARTICLE_EMIT_OVER_TIME,
			eDIG_GOAL_PARTICLE_EMIT_OVER_TIME_ARTIFACT,
			eDIG_GOAL_PARTICLE_EMIT_OVER_TIME_DIG_COIN,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_X,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_SCALE,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_ALPHA,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_COLOR_CYCLE,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_TOP_X,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_TOP_SCALE,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_TOP_ALPHA,
			eDIG_GOAL_DRAW_DIG_BAR_CLEAR_ANIM_TOP_COLOR_CYCLE,
			eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_X,
			eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_SCALE,
			eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_ALPHA,
			eDIG_GOAL_DRAW_DIG_BAR_ALL_CLEAR_ANIM_COLOR_CYCLE,
			eDIG_GOAL_GET_NEXT_CHEST_POINT_BIAS_VS_PCT,
			eDIG_GOAL_NEW_GAME_ARTIFACT_SPREAD_CURVE,
			eDIG_GOAL_NEW_GAME_BRICK_STR_SPREAD_CURVE,
			eDIG_GOAL_NEW_GAME_MINE_STR_SPREAD_CURVE,
			eEFFECTS_TEXTURE_POS,
			eEFFECTS_MOVE_PCT,
			eEFFECTS_INTENSITY,
			eEFFECTS_RADIUS,
			eEFFECTS_CURVED_ALPHA_HYPERCUBE,
			eEFFECTS_CURVED_ALPHA_DIG_GOAL_POINTS,
			eEFFECTS_CURVED_ALPHA_COIN_FLY,
			eEFFECTS_CURVED_ALPHA_BUTTERFLY,
			eEFFECTS_CURVED_SCALE_HYPERCUBE,
			eEFFECTS_CURVED_SCALE_DIG_GOAL_POINTS,
			eEFFECTS_CURVED_SCALE_COIN_FLY,
			eEFFECTS_CURVED_SCALE_CARD_GEM,
			eEFFECTS_TRANS_PCT_COIN_FLY,
			eEFFECTS_SINK_PCT_COIN_FLY,
			eEFFECTS_SINK_PCT_COIN_FLY_NULL,
			eEFFECTS_TARGET_X_BUTTERFLY,
			eEFFECTS_TARGET_Y_BUTTERFLY,
			eEFFECTS_CIRCLE_PCT_SPEED_BOARD_TIME_BONUS,
			eEFFECTS_RADIUS_SCALE_SPEED_BOARD_TIME_BONUS,
			eEFFECTS_TRAVEL_PCT_GEM_COLLECT,
			eEFFECTS_ARC_GEM_COLLECT,
			eEFFECTS_CV_Y_DIG_GOAL_POINTS,
			eEFFECTS_GEM_SCALE_CARD_GEM,
			eEFFECTS_GLOW_CARD_GEM,
			eFILLER_BOARD_SLOT_PCT,
			eFILLER_BOARD_MYSTERY_ALPHA,
			eFILLER_BOARD_MYSTERY_CIRCLE_ALPHA_EXAMINE,
			eFILLER_BOARD_MYSTERY_CIRCLE_ALPHA_FILL_IN,
			eFILLER_BOARD_ASSIST_CURVE,
			eGOLDIFY_EFFECT_CV,
			eHELP_DIALOG_ALPHA,
			eHIDDEN_OBJECT_GOAL_REVEAL,
			eHIDDEN_OBJECT_GOAL_UNREVEAL,
			eHIDDEN_OBJECT_GOAL_SCALE,
			eHIDDEN_OBJECT_GOAL_SCALE_REVEAL,
			eHIDDEN_OBJECT_GOAL_MOVEMENT_X,
			eHIDDEN_OBJECT_GOAL_MOVEMENT_X_REVEAL,
			eHIDDEN_OBJECT_GOAL_MOVEMENT_Y,
			eHIDDEN_OBJECT_GOAL_MOVEMENT_Y_REVEAL,
			eHIDDEN_OBJECT_GOAL_MOVEMENT_ADD_Y,
			eHIDDEN_OBJECT_GOAL_REVEAL_PCT,
			eHYPERSPACE_BG_SCALE,
			eHYPERSPACE_X_OFFSET_ANIM,
			eHYPERSPACE_MIN_ALPHA,
			eHYPERSPACE_SHATTER_SCALE,
			eHYPERSPACE_WARP_TUBE_TEXTURE_FADE,
			eHYPERSPACE_RING_FADE_TUNNEL_IN,
			eHYPERSPACE_RING_FADE_TUNNEL_OUT,
			eHYPERSPACE_PIECE_ALPHA,
			eHYPERSPACE_FROM_CENTER_PCT,
			eHYPERSPACE_TO_CENTER_PCT,
			eHYPERSPACE_UI_WARP_PERCENT_ADD,
			eINFERNO_BOARD_DANGER_Y_ASCEND,
			eINFERNO_BOARD_DANGER_ALPHA_ASCEND,
			eINFERNO_BOARD_DANGER_Y_DESCEND,
			eINFERNO_BOARD_DANGER_ALPHA_DESCEND,
			eINFERNO_BOARD_BOTTOM_FROST_PCT_ACTIVATE,
			eINFERNO_BOARD_BOTTOM_FROST_PCT_DEACTIVATE,
			eINFERNO_BOARD_BUMP_Y,
			eINFERNO_BOARD_REPRIEVE_RAMP_UP,
			eINFERNO_BOARD_WARNING_ALPHA_1,
			eINFERNO_BOARD_WARNING_ALPHA_2,
			eINFERNO_BOARD_LOSE_FRAME_PCT,
			eINFERNO_BOARD_DEATH_ANIM_PCT,
			eINFERNO_BOARD_MULTIPLIER_TEXT_ALPHA,
			eINFERNO_BOARD_MULTIPLIER_TEXT_SCALE,
			eINFERNO_BOARD_MULTIPLIER_TEXT_X,
			eINFERNO_BOARD_MULTIPLIER_TEXT_Y,
			eINFERNO_BOARD_MULTIPLIER_FLASH,
			eINFERNO_BOARD_COL_COMBO_COOL_DOWN_VS_COUNT,
			eINFERNO_BOARD_COL_COMBO_ALPHA,
			eINFERNO_BOARD_COL_COMBO_ALPHA_CLEARING,
			eINFERNO_BOARD_COL_COMBO_SCALE,
			eINFERNO_BOARD_COL_COMBO_Y,
			eINFERNO_BOARD_CV_ROW_FIRE_SPEED,
			eINFERNO_BOARD_CV_LEVEL_PROGRESS,
			eINFERNO_BOARD_COL_COUNT_OVER_TIME,
			eINFERNO_BOARD_COL_DISTRIB,
			eINFERNO_BOARD_REPRIEVE_STR,
			eINFERNO_BOARD_CV_SHAKEY,
			eINFERNO_BOARD_CV_SHAKEY_INTENSE,
			eINFERNO_BOARD_BACK_DIM,
			eINFERNO_BOARD_INTRO_SNOW,
			eINFERNO_BOARD_INTRO_SPEED_MOD,
			eINFERNO_BOARD_DARKEN_BOARD,
			eINFERNO_BOARD_PROGRESS,
			eINFERNO_BOARD_UPDATE_Y_FADE,
			eINFERNO_BOARD_UPDATE_SCALE_IN,
			eINFERNO_BOARD_UPDATE_WOBBLE_IN,
			eINFERNO_BOARD_DO_UPDATE_STORMY_SNOW,
			eINFERNO_BOARD_DO_UPDATE_STORMY_SNOW_SOUND_FADE,
			eINFERNO_BOARD_UPDATE_LAVA_PANIC_SCALE,
			eINFERNO_BOARD_UPDATE_LAVA_CV_SHAKEY,
			eINFERNO_BOARD_UPDATE_LAVA_CV_TOP_SNOW,
			eINFERNO_BOARD_UPDATE_LAVA_STORMY_SNOW,
			eINFERNO_BOARD_CONFIGURE_COL_COMBO_POINTS_LERP_PCT,
			eINFERNO_BOARD_DRAW_ICE_ALPHA,
			eLIGHTNING_STORM_NOVA_SCALE,
			eLIGHTNING_STORM_NOVA_ALPHA,
			eLIGHTNING_STORM_NUKE_SCALE,
			eLIGHTNING_STORM_NUKE_ALPHA,
			eLIGHTNING_STORM_LIGHTNING_ALPHA,
			eMAIN_MENU_BEJ3_LOGO_ALPHA,
			eMAIN_MENU_SHOW_CURVE,
			eMAIN_MENU_ROTATION,
			eMAIN_MENU_BUTTON_ROTATION_ADD,
			eMAIN_MENU_FORE_BLACK_ALPHA,
			eMAIN_MENU_BKG_BLACK_ALPHA,
			eMAIN_MENU_LOGO_ALPHA_FADE_IN,
			eMAIN_MENU_LOGO_ALPHA_FADE_OUT,
			eMAIN_MENU_LOADER_ALPHA_FADE_IN,
			eMAIN_MENU_LOADER_ALPHA_FADE_OUT,
			eMAIN_MENU_TIP_TEXT_ALPHA_FADE_IN,
			eMAIN_MENU_TIP_TEXT_ALPHA_FADE_OUT,
			eMAIN_MENU_SCROLL_CONTAINER_UPDATE_ALPHA,
			ePIECE_SCALE_A,
			ePIECE_SCALE_B,
			ePIECE_SCALE_C,
			ePIECE_SCALE_D,
			ePIECE_SCALE_E,
			ePIECE_SCALE_F,
			ePIECE_SCALE_G,
			ePIECE_SCALE_H,
			ePIECE_ALPHA,
			ePIECE_SELECTOR_ALPHA,
			ePIECE_DEST_PCT_A,
			ePIECE_DEST_PCT_B,
			ePIECE_DEST_PCT_C,
			ePIECE_DEST_PCT_D,
			ePIECE_HINT_ALPHA,
			ePIECE_HINT_ARROW_POS,
			ePIECE_OVERLAY_CURVE,
			ePIECE_OVERLAY_BULGE,
			ePIECE_ANIM_CURVE_A,
			ePIECE_ANIM_CURVE_B,
			ePIECE_ANIM_CURVE_C,
			ePIECE_ANIM_CURVE_D,
			ePIECE_ANIM_CURVE_E,
			ePOINTS_MANAGER_VERT_SHIFTER_ROTATION,
			ePOINTS_MANAGER_VERT_SHIFTER_DY,
			ePOKER_BOARD_CARD_FLIP_PCT,
			ePOKER_BOARD_CARD_DEAL_PCT_1,
			ePOKER_BOARD_CARD_DEAL_PCT_2,
			ePOKER_BOARD_CARD_DEAL_PCT_3,
			ePOKER_BOARD_CARD_DEAL_PCT_4,
			ePOKER_BOARD_CARD_DEAL_PCT_5,
			ePOKER_BOARD_SCORE_HAND_PCT_A,
			ePOKER_BOARD_SCORE_HAND_PCT_B,
			ePOKER_BOARD_CARD_BULGE_PCT,
			ePOKER_BOARD_SKULL_SCALE,
			ePOKER_BOARD_SKULL_CRUSHER_ANIM_PCT,
			ePOKER_BOARD_SKULL_BAR_LID_PCT_A,
			ePOKER_BOARD_SKULL_BAR_LID_PCT_B,
			ePOKER_BOARD_COIN_FLIP_PCT,
			ePOKER_BOARD_COIN_WON_PCT,
			ePOKER_BOARD_DRAW_OVERLAY_TEXT_ALPHA,
			ePOKER_BOARD_DRAW_OVERLAY_TEXT_POS_PCT,
			ePOKER_BOARD_DRAW_OVERLAY_TEXT_Y_BUMP,
			ePOKER_BOARD_DRAW_OVERLAY_SKULL_POS_PCT,
			ePOKER_BOARD_DRAW_OVERLAY_SKULL_SCALE,
			ePOKER_BOARD_DRAW_OVERLAY_COIN_Y_PCT,
			ePOKER_BOARD_DRAW_OVERLAY_COIN_ALPHA,
			ePOKER_BOARD_DRAW_CARDS_POS_PCT,
			ePOKER_BOARD_DRAW_CARDS_ALPHA,
			ePOKER_BOARD_DRAW_CARDS_Y_POPUP,
			ePOKER_BOARD_DRAW_CARDS_SHADOW_ALPHA,
			ePOKER_BOARD_DRAW_SKULL_BAR_SKULL_X,
			ePOKER_BOARD_DRAW_SKULL_BAR_SCALE,
			ePOKER_BOARD_DRAW_SKULL_BAR_SHADOW_ALPHA,
			ePOKER_BOARD_DRAW_SKULL_BAR_GLOW,
			ePOKER_BOARD_UPDATE_COIN_VOL_SCORE_HAND,
			ePOKER_BOARD_UPDATE_DARKEN_PCT,
			ePOKER_BOARD_UPDATE_COIN_VOL_COIN_FLIP,
			ePOKER_BOARD_DRAW_BULB_FANFARE_ALPHA,
			ePOKER_BOARD_DRAW_DISCO_FANFARE_ALPHA,
			ePOKER_BOARD_DRAW_STAR_FANFARE_ALPHA,
			eQUEST_BOARD_MENU_BUTTON_FLASH,
			eQUEST_BOARD_TEXT_NOTIFY_DRAW_SCALE_IN,
			eQUEST_BOARD_TEXT_NOTIFY_DRAW_SCALE_OUT,
			eRANK_BAR_WIDGET_RANKUP_GLOW,
			eRANK_BAR_WIDGET_RANKUP_GLOW_ADD,
			eRANK_UP_DIALOG_ANIM_PCT,
			eRANK_UP_DIALOG_DRAW_TEXT_SCALE,
			eRANK_UP_DIALOG_DRAW_GLOW_SCALE,
			eRANK_UP_DIALOG_DRAW_GLOW_ALPHA,
			eSPEED_BOARD_CHANCE_5,
			eSPEED_BOARD_CHANCE_10,
			eSPEED_BOARD_CHANCE_15,
			eSPEED_BOARD_COLLECTOR_EXTEND_PCT_A,
			eSPEED_BOARD_COLLECTOR_EXTEND_PCT_B,
			eSPEED_BOARD_COLLECTED_TIME_ALPHA,
			eSPEED_BOARD_LAST_HURRAH_ALPHA_A,
			eSPEED_BOARD_LAST_HURRAH_ALPHA_B,
			eSPEED_BOARD_SPLINE_INTERP_A,
			eSPEED_BOARD_SPLINE_INTERP_B,
			eSPEED_BOARD_ALPHA_OUT,
			eSPEED_BOARD_SCALE_CV,
			eTOOLTIP_ALPHA,
			eTOOLTIP_ARROW_OFFSET,
			eZEN_BOARD_NOISE_VOLUME_INIT,
			eZEN_BOARD_NOISE_VOLUME_FADE_OUT,
			eZEN_BOARD_NOISE_VOLUME_FADE_IN,
			eZEN_BOARD_BREATH_PCT,
			eZEN_BOARD_AFFIRMATION_CENTER_PCT,
			eZEN_BOARD_AFFIRMATION_ZOOM,
			eZEN_BOARD_AFFIRMATION_ALPHA,
			eZEN_BOARD_DYNAMIC_SPEED,
			eZEN_BOARD_NECKLACE_PREV_LEVEL_Y_OFS,
			eZEN_BOARD_NECKLACE_PREV_LEVEL_ALPHA,
			eZEN_BOARD_NECKLACE_LEVEL_ALPHA,
			eZEN_BOARD_NECKLACE_PREV_GEM_ALPHA,
			eZEN_BOARD_NECKLACE_CHANGED_GEM_ALPHA,
			eZEN_BOARD_NECKLACE_NEW_GEM_SCALE,
			eZEN_BOARD_CHANGED_GEM_SCALE,
			eNUMBER_CURVED_VALS
		}
	}
}
