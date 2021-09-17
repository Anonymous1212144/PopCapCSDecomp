using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	public class BXMLElement
	{
		public static bool GetAttribute(BXMLElement theElem, string theName, ref string theValue)
		{
			if (theElem.mAttributes.ContainsKey(theName))
			{
				theValue = theElem.mAttributes[theName];
				return true;
			}
			return false;
		}

		public int mType;

		public string mValue;

		public Dictionary<string, string> mAttributes = new Dictionary<string, string>();
	}
}
