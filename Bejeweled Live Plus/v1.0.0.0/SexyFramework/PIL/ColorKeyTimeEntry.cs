﻿using System;

namespace SexyFramework.PIL
{
	public class ColorKeyTimeEntry
	{
		public ColorKeyTimeEntry(float pt, ColorKey key)
		{
			this.first = pt;
			this.second = key;
		}

		public float first;

		public ColorKey second;
	}
}
