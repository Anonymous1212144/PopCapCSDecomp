using System;
using Microsoft.Xna.Framework;

namespace Bejeweled3
{
	internal struct Line
	{
		public Line(Vector2 start, Vector2 end)
		{
			this.Start = start;
			this.End = end;
		}

		public Vector2 Start;

		public Vector2 End;
	}
}
