using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public class Lightning
	{
		public static Lightning GetNewLightning()
		{
			if (Lightning.unusedObjects.Count > 0)
			{
				return Lightning.unusedObjects.Pop();
			}
			return new Lightning();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			Lightning.unusedObjects.Push(this);
		}

		private void Reset()
		{
		}

		private Lightning()
		{
		}

		public TPointFloat[,] mPoints = new TPointFloat[8, 2];

		public float mPercentDone;

		public float mPullX;

		public float mPullY;

		private static Stack<Lightning> unusedObjects = new Stack<Lightning>();
	}
}
