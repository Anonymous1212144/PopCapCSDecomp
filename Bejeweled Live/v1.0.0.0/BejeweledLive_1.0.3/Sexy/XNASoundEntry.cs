﻿using System;
using Microsoft.Xna.Framework.Audio;

namespace Sexy
{
	public class XNASoundEntry
	{
		public void Dispose()
		{
			this.mSound.Dispose();
		}

		public float mBaseVolume = 1f;

		public float mBasePan;

		public SoundEffect mSound;
	}
}
