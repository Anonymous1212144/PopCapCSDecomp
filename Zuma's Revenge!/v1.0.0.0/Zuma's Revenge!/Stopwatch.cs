using System;

namespace ZumasRevenge
{
	public class Stopwatch
	{
		public Stopwatch(string msg)
		{
			this.text = msg;
			this.start = DateTime.Now.Millisecond;
		}

		~Stopwatch()
		{
		}

		private string text;

		private int start;
	}
}
