﻿using System;

namespace Sexy.TodLib
{
	internal class ReanimationParams
	{
		public ReanimationParams(ReanimationType aReanimationType, string aReanimFilename)
			: this(aReanimationType, aReanimFilename, 0)
		{
		}

		public ReanimationParams(ReanimationType aReanimationType, string aReanimFilename, int aReanimparamFlags)
		{
			this.mReanimationType = aReanimationType;
			this.mReanimFileName = aReanimFilename;
			this.mReanimParamFlags = aReanimparamFlags;
		}

		public ReanimationType mReanimationType;

		public string mReanimFileName;

		public int mReanimParamFlags;
	}
}
