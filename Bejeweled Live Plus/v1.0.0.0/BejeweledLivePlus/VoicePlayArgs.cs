using System;

namespace BejeweledLivePlus
{
	public class VoicePlayArgs
	{
		public int SoundID { get; set; }

		public int Pan { get; set; }

		public double Volume { get; set; }

		public int InterruptID { get; set; }

		public SoundPlayCondition Condition { get; set; }

		public VoicePlayArgs()
		{
			this.SoundID = -1;
			this.Pan = 0;
			this.Volume = 1.0;
			this.InterruptID = -1;
			this.Condition = null;
		}

		public VoicePlayArgs(int id, int pan, double volume, int interruptId, SoundPlayCondition condition)
		{
			this.SoundID = id;
			this.Pan = pan;
			this.Volume = volume;
			this.InterruptID = interruptId;
			this.Condition = condition;
		}
	}
}
