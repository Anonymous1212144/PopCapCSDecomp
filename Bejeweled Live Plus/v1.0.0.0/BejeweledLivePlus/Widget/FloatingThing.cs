using System;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Widget
{
	internal class FloatingThing
	{
		public FloatingThing()
		{
			this.mUsed = false;
		}

		public bool mUsed;

		public float mScale;

		public float mScaleAdd;

		public float mRot;

		public float mRotAdd;

		public float mX;

		public float mY;

		public float mAddX;

		public float mAddY;

		public Image mImage;
	}
}
