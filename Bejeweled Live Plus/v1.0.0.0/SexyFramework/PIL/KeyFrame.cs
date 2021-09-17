using System;

namespace SexyFramework.PIL
{
	public class KeyFrame
	{
		public KeyFrame()
		{
		}

		public KeyFrame(int k, KeyFrameData data)
		{
			this.first = k;
			this.second = data;
		}

		public int first;

		public KeyFrameData second;
	}
}
