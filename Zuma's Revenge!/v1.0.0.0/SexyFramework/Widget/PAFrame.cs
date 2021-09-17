using System;
using System.Collections.Generic;

namespace SexyFramework.Widget
{
	public class PAFrame
	{
		public PAFrame()
		{
			this.mHasStop = false;
		}

		public List<PAObjectPos> mFrameObjectPosVector = new List<PAObjectPos>();

		public bool mHasStop;

		public List<PACommand> mCommandVector = new List<PACommand>();
	}
}
