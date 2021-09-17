using System;

namespace ZumasRevenge
{
	public class SoundAttribs
	{
		public SoundAttribs()
		{
			this.pan = 0;
			this.pitch = 0f;
			this.fadein = 1f;
			this.fadeout = 1f;
			this.delay = 0;
			this.stagger = 0;
			this.volume = 1f;
		}

		public int pan { get; set; }

		public int delay { get; set; }

		public int stagger { get; set; }

		public float fadein { get; set; }

		public float fadeout { get; set; }

		public float pitch { get; set; }

		public float volume { get; set; }
	}
}
