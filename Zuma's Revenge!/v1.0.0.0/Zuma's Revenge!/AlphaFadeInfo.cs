using System;
using JeffLib;

namespace ZumasRevenge
{
	public class AlphaFadeInfo
	{
		public AlphaFadeInfo()
		{
		}

		public AlphaFadeInfo(AlphaFader f, bool s)
		{
			this.first = f;
			this.second = s;
		}

		public AlphaFader first;

		public bool second;
	}
}
