using System;

public static class GlobalMembersCurvedVal
{
	internal static float CVCharToFloat(sbyte theChar)
	{
		if (theChar >= 92)
		{
			theChar -= 1;
		}
		return (float)(theChar - 35) / 90f;
	}

	internal static int CVCharToInt(sbyte theChar)
	{
		if (theChar >= 92)
		{
			theChar -= 1;
		}
		return (int)(theChar - 35);
	}

	internal static float CVStrToAngle(string theStr)
	{
		int num = 0;
		num += GlobalMembersCurvedVal.CVCharToInt((sbyte)theStr.get_Chars(0));
		num *= 90;
		num += GlobalMembersCurvedVal.CVCharToInt((sbyte)theStr.get_Chars(1));
		num *= 90;
		num += GlobalMembersCurvedVal.CVCharToInt((sbyte)theStr.get_Chars(2));
		return (float)num * 360f / 729000f;
	}
}
