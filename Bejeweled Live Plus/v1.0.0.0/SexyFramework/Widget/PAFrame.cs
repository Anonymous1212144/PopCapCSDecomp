using System;

namespace SexyFramework.Widget
{
	public class PAFrame
	{
		public PAFrame()
		{
			this.mHasStop = false;
		}

		public PAObjectPos[] mFrameObjectPosVector = new PAObjectPos[0];

		public bool mHasStop;

		public PACommand[] mCommandVector = new PACommand[0];
	}
}
