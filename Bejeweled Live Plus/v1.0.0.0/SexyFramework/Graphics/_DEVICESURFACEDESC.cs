﻿using System;

namespace SexyFramework.Graphics
{
	public class _DEVICESURFACEDESC
	{
		public uint dwFlags;

		public uint dwHeight;

		public uint dwWidth;

		public uint lPitch;

		public IntPtr lpSurface;

		public _DEVICEPIXELFORMAT ddpfPixelFormat = new _DEVICEPIXELFORMAT();
	}
}
