﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Sexy
{
	public class InputController
	{
		public static void TestTouchCaps()
		{
			TouchPanelCapabilities capabilities = TouchPanel.GetCapabilities();
			if (capabilities.IsConnected)
			{
				int maximumTouchCount = capabilities.MaximumTouchCount;
				Debug.OutputDebug<string, int>("maxPoints", maximumTouchCount);
			}
		}

		public static void HandleTouchInput()
		{
		}

		public static Vector2 touchPos = Vector2.Zero;
	}
}
