using System;

namespace BejeweledLivePlus
{
	internal class Pair<KEY, VALUE>
	{
		public Pair(KEY k, VALUE v)
		{
			this.first = k;
			this.second = v;
		}

		public KEY first;

		public VALUE second;
	}
}
