﻿using System;
using Microsoft.Xna.Framework;
using SexyFramework.Drivers;
using SexyFramework.Graphics;
using SexyFramework.PIL;

namespace SexyFramework
{
	public static class GlobalMembers
	{
		public static SexyAppBase gSexyAppBase = null;

		public static SexyApp gSexyApp = null;

		public static bool gIs3D = false;

		public static IFileDriver gFileDriver = null;

		public static int gTotalGraphicsMemory = 0;

		public static float SEXYMATH_PI = 3.14159274f;

		public static float SEXYMATH_2PI = 6.28318548f;

		public static float SEXYMATH_E = 2.71828f;

		public static float SEXYMATH_EPSILON = 0.001f;

		public static float SEXYMATH_EPSILONSQ = 1E-06f;

		public static bool IsBackButtonPressed = false;

		public static Vector2 NO_TOUCH_MOUSE_POS = new Vector2(-1f, -1f);

		public static int[,] gButtonWidgetColors = new int[,]
		{
			{ 0, 0, 0 },
			{ 0, 0, 0 },
			{ 0, 0, 0 },
			{ 255, 255, 255 },
			{ 132, 132, 132 },
			{ 212, 212, 212 }
		};

		public static int[,] gDialogButtonColors = new int[,]
		{
			{ 255, 255, 255 },
			{ 255, 255, 255 },
			{ 0, 0, 0 },
			{ 255, 255, 255 },
			{ 132, 132, 132 },
			{ 212, 212, 212 }
		};

		public static int[,] gDialogColors = new int[,]
		{
			{ 255, 255, 255 },
			{ 255, 255, 0 },
			{ 255, 255, 255 },
			{ 255, 255, 255 },
			{ 255, 255, 255 },
			{ 80, 80, 80 },
			{ 255, 255, 255 }
		};

		public static string DIALOG_YES_STRING = "YES";

		public static string DIALOG_NO_STRING = "NO";

		public static string DIALOG_OK_STRING = "OK";

		public static string DIALOG_CANCEL_STRING = "CANCEL";

		public delegate int GetIdByImageFunc(Image img);

		public delegate Image GetImageByIdFunc(int id);

		public delegate KeyFrameData KFDInstantiateFunc();
	}
}
