using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.PIL;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public static class Common
	{
		public static bool IsDeprecatedPowerUp(PowerType ptype)
		{
			return ptype == PowerType.PowerType_Fireball || ptype == PowerType.PowerType_ShieldFrog || ptype == PowerType.PowerType_FreezeBoss || ptype == PowerType.PowerType_BallEater || ptype == PowerType.PowerType_BombBullet || ptype == PowerType.PowerType_Lob;
		}

		public static bool StrEquals(string str1, string str2, bool pIgnoreCase)
		{
			if (!pIgnoreCase)
			{
				return str1 == str2;
			}
			return string.Compare(str1, str2, 1) == 0;
		}

		public static bool StrEquals(string str1, string str2)
		{
			return Common.StrEquals(str1, str2, true);
		}

		public static bool StrICaseEquals(string str1, string str2)
		{
			return string.Compare(str1, str2, 1) == 0;
		}

		public static int GetDefaultBallRadius()
		{
			if (GameApp.gApp.mGraphicsDriver.Is3D())
			{
				return 18;
			}
			return 17;
		}

		public static int GetDefaultBallSize()
		{
			return Common.GetDefaultBallRadius() * 2;
		}

		public static void MirrorPoint(ref float x, ref float y, MirrorType theMirror)
		{
			switch (theMirror)
			{
			case MirrorType.MirrorType_X:
				x = (float)GameApp.gApp.mWidth - x;
				return;
			case MirrorType.MirrorType_Y:
				y = (float)GameApp.gApp.mHeight - y;
				return;
			case MirrorType.MirrorType_XY:
				x = (float)GameApp.gApp.mWidth - x;
				y = (float)GameApp.gApp.mHeight - y;
				return;
			default:
				return;
			}
		}

		public static void SetupDialog(Dialog theDialog)
		{
			theDialog.SetHeaderFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_YELLOW));
			theDialog.SetLinesFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_YELLOW));
			theDialog.SetColor(0, new Color(203, 201, 187));
			theDialog.SetColor(1, new Color(244, 148, 28));
			theDialog.mPriority = 1;
			Common.SetupDialogButton(theDialog.mYesButton);
			Common.SetupDialogButton(theDialog.mNoButton);
		}

		public static void SetupDialogButton(DialogButton theButton)
		{
			if (theButton == null)
			{
				return;
			}
			theButton.mTranslateX = -1;
			theButton.mTranslateY = 1;
			int mNumCols = theButton.mComponentImage.mNumCols;
			int num = theButton.mComponentImage.mWidth / mNumCols;
			int mHeight = theButton.mComponentImage.mHeight;
			if (mNumCols == 3)
			{
				theButton.mNormalRect = new Rect(0, 0, num, mHeight);
				theButton.mOverRect = new Rect(num, 0, num, mHeight);
				theButton.mDownRect = new Rect(num * 2, 0, num, mHeight);
			}
			theButton.SetFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN));
			theButton.SetColor(1, new Color(16777215));
			theButton.mHasAlpha = true;
			theButton.mHasTransparencies = true;
			if (theButton.mWidth == 0)
			{
				int mX = theButton.mX;
				int mY = theButton.mY;
				int theWidth = theButton.mFont.StringWidth(theButton.mLabel);
				int mHeight2 = theButton.mComponentImage.mHeight;
				theButton.Resize(mX, mY, theWidth, mHeight2);
			}
			theButton.mIsDown = false;
			theButton.mIsOver = false;
		}

		public static DialogButton MakeButton(int theId, ButtonListener theListener, string theText)
		{
			DialogButton dialogButton = new DialogButton(Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BUTTON), theId, theListener);
			dialogButton.mLabel = theText;
			Common.SetupDialogButton(dialogButton);
			return dialogButton;
		}

		public static DialogButton MakeButton(int theId, Image theButtonImage, ButtonListener theListener, string theText)
		{
			DialogButton dialogButton = new DialogButton(theButtonImage, theId, theListener);
			dialogButton.mLabel = theText;
			Common.SetupDialogButton(dialogButton);
			return dialogButton;
		}

		public static void SizeButtonsToLabel(ButtonWidget[] inButtons, int inButtonCount, int inXPadding)
		{
			int num = 0;
			for (int i = 0; i < inButtonCount; i++)
			{
				ButtonWidget buttonWidget = inButtons[i];
				if (buttonWidget.mFont == null)
				{
					return;
				}
				int num2 = buttonWidget.mFont.StringWidth(buttonWidget.mLabel);
				if (num2 > num)
				{
					num = num2;
				}
			}
			num += inXPadding * 2;
			for (int j = 0; j < inButtonCount; j++)
			{
				ButtonWidget buttonWidget2 = inButtons[j];
				buttonWidget2.Resize((int)((float)buttonWidget2.mX - (float)(num - buttonWidget2.mWidth) * 0.5f), buttonWidget2.mY, num, buttonWidget2.mHeight);
			}
		}

		public static void SetFXNumScale(PIEffect p, float scale)
		{
			if (p == null)
			{
				return;
			}
			int num = 0;
			for (;;)
			{
				PILayer layer = p.GetLayer(num);
				if (layer == null)
				{
					break;
				}
				int num2 = 0;
				for (;;)
				{
					PIEmitterInstance emitter = layer.GetEmitter(num2);
					if (emitter == null)
					{
						break;
					}
					emitter.mNumberScale = scale;
					num2++;
				}
				num++;
			}
		}

		public static void DrawCommonDialogBorder(Graphics g, int x, int y, int width, int height)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOTOPEDGE);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOTEDGE);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOT);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOSIDE);
			g.SetColorizeImages(false);
			g.ClearClipRect();
			g.DrawImage(imageByID, x, y);
			g.DrawImageMirror(imageByID, x + width - imageByID.GetWidth(), y);
			g.DrawImage(imageByID2, x, y + height - imageByID2.GetHeight());
			g.DrawImageMirror(imageByID2, x + width - imageByID2.GetWidth(), y + height - imageByID2.GetHeight());
			g.SetClipRect(x + imageByID.GetWidth(), y, width - imageByID.GetWidth() * 2, height);
			for (int i = x + imageByID.GetWidth(); i < x + width - imageByID.GetWidth(); i += imageByID3.GetWidth())
			{
				g.DrawImage(imageByID3, i, y - 1);
				g.DrawImage(imageByID3, i, y + height - imageByID3.GetHeight() + 1);
			}
			g.ClearClipRect();
			g.SetClipRect(x, y + imageByID.GetHeight(), width, height - imageByID.GetHeight() * 2);
			for (int j = y + imageByID.GetHeight(); j < y + height - imageByID.GetHeight(); j += imageByID4.GetHeight())
			{
				g.DrawImage(imageByID4, x, j);
				g.DrawImage(imageByID4, x + width - imageByID4.GetWidth(), j);
			}
			g.ClearClipRect();
		}

		public static int _GetWordWrappedHeight(string inText, Font inFont, int inWidth)
		{
			List<string> list = Common.Split(inText);
			int num = 0;
			int num2 = 1;
			int num3 = inFont.CharWidth(' ');
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == "\n")
				{
					num2++;
					num = 0;
				}
				else
				{
					int num4 = inFont.StringWidth(list[i]);
					if (num + num4 + num3 <= inWidth)
					{
						num += num4 + num3;
					}
					else if (num + num4 <= inWidth)
					{
						num += num4;
					}
					else
					{
						num2++;
						num = num4 + num3;
					}
				}
			}
			int num5 = inFont.GetHeight() - inFont.GetAscent();
			return num2 * inFont.GetHeight() - num5;
		}

		public static void DrawCommonDialogBacking(Graphics g, int x, int y, int width, int height)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_FRAME_WOOD);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOSIDE);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOT);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOTOPEDGE);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_BAMBOOBOTEDGE);
			g.ClearClipRect();
			g.ClipRect(x + imageByID2.GetWidth() - 2, y + imageByID3.GetHeight() - 3, width + 4 - imageByID2.GetWidth() * 2, height + 10 - imageByID3.GetHeight() * 2);
			int i = x;
			int j = y;
			bool flag = false;
			while (j <= y + height + imageByID.GetHeight())
			{
				while (i < x + width + imageByID.GetWidth())
				{
					if (flag)
					{
						g.DrawImageMirror(imageByID, i, j);
					}
					else
					{
						g.DrawImage(imageByID, i, j);
					}
					i += imageByID.GetWidth();
					flag = !flag;
				}
				i = x;
				j += imageByID.GetHeight();
			}
			g.ClearClipRect();
			g.DrawImage(imageByID4, x, y);
			g.DrawImageMirror(imageByID4, x + width - imageByID4.GetWidth(), y);
			g.DrawImage(imageByID5, x, y + height - imageByID5.GetHeight());
			g.DrawImageMirror(imageByID5, x + width - imageByID5.GetWidth(), y + height - imageByID5.GetHeight());
			g.SetClipRect(x + imageByID4.GetWidth(), y, width - imageByID4.GetWidth() * 2, height);
			for (int k = x + imageByID4.GetWidth(); k < x + width - imageByID4.GetWidth(); k += imageByID3.GetWidth())
			{
				g.DrawImage(imageByID3, k, y - 1);
				g.DrawImage(imageByID3, k, y + height - imageByID3.GetHeight() + 1);
			}
			g.ClearClipRect();
			g.SetClipRect(x, y + imageByID4.GetHeight(), width, height - imageByID4.GetHeight() * 2);
			for (int l = y + imageByID4.GetHeight(); l < y + height - imageByID4.GetHeight(); l += imageByID2.GetHeight())
			{
				g.DrawImage(imageByID2, x, l);
				g.DrawImage(imageByID2, x + width - imageByID2.GetWidth(), l);
			}
			g.ClearClipRect();
		}

		public static bool ExtractAdventureStatsResources(ResourceManager res)
		{
			return true;
		}

		public static int GetIdByStringId(string theStringId)
		{
			return 0;
		}

		public static int GetBoardStateCount()
		{
			Board board = ((GameApp)GlobalMembers.gSexyApp).GetBoard();
			if (board == null)
			{
				return 0;
			}
			return board.GetStateCount();
		}

		public static uint GetBoardTickCount()
		{
			return (uint)(Common.GetBoardStateCount() * 10);
		}

		public static float _S(float value)
		{
			return GameApp.ScaleNum(value);
		}

		public static int _S(int value)
		{
			return GameApp.ScaleNum(value);
		}

		public static float _SS(float value)
		{
			return GameApp.ScreenScaleNum(value);
		}

		public static int _SS(int value)
		{
			return GameApp.ScreenScaleNum(value);
		}

		public static string _MP(string value)
		{
			return value;
		}

		public static float _M(float value)
		{
			return value;
		}

		public static float _M1(float value)
		{
			return Common._M(value);
		}

		public static float _M2(float value)
		{
			return Common._M(value);
		}

		public static float _M3(float value)
		{
			return Common._M(value);
		}

		public static float _M4(float value)
		{
			return Common._M(value);
		}

		public static float _M5(float value)
		{
			return Common._M(value);
		}

		public static float _M6(float value)
		{
			return Common._M(value);
		}

		public static float _M7(float value)
		{
			return Common._M(value);
		}

		public static int _M(int value)
		{
			return value;
		}

		public static int _M1(int value)
		{
			return Common._M(value);
		}

		public static int _M2(int value)
		{
			return Common._M(value);
		}

		public static int _M3(int value)
		{
			return Common._M(value);
		}

		public static int _M4(int value)
		{
			return Common._M(value);
		}

		public static int _M5(int value)
		{
			return Common._M(value);
		}

		public static int _M6(int value)
		{
			return Common._M(value);
		}

		public static int _M7(int value)
		{
			return Common._M(value);
		}

		public static int _M8(int value)
		{
			return Common._M(value);
		}

		public static int _M9(int value)
		{
			return Common._M(value);
		}

		public static float _SA(float value, float add)
		{
			return value;
		}

		public static float _DS(float value)
		{
			return GameApp.DownScaleNum(value);
		}

		public static int _DS(int value)
		{
			return GameApp.DownScaleNum(value);
		}

		public static float _DSA(float value, float add)
		{
			return GameApp.DownScaleNum(value, add);
		}

		public static List<string> Split(string inText)
		{
			Common.mTotalWords.Clear();
			string[] array = inText.Split(new char[] { '\n' });
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { ' ' });
				for (int j = 0; j < array2.Length; j++)
				{
					Common.mTotalWords.Add(array2[j]);
				}
				if (array.Length > 1)
				{
					Common.mTotalWords.Add("\n");
				}
			}
			return Common.mTotalWords;
		}

		public static bool BossLevel(Level level)
		{
			return level != null && (level.IsFinalBossLevel() || level.mBoss != null || level.mEndSequence > 0);
		}

		public static string PowerupToStr(PowerType t, bool all_caps)
		{
			int id = 0;
			switch (t)
			{
			case PowerType.PowerType_ProximityBomb:
				id = (all_caps ? 696 : 697);
				break;
			case PowerType.PowerType_SlowDown:
				id = (all_caps ? 698 : 699);
				break;
			case PowerType.PowerType_Accuracy:
				id = (all_caps ? 700 : 701);
				break;
			case PowerType.PowerType_MoveBackwards:
				id = (all_caps ? 702 : 703);
				break;
			case PowerType.PowerType_Cannon:
				id = (all_caps ? 704 : 705);
				break;
			case PowerType.PowerType_ColorNuke:
				id = (all_caps ? 706 : 707);
				break;
			case PowerType.PowerType_Laser:
				id = (all_caps ? 708 : 709);
				break;
			case PowerType.PowerType_GauntletMultBall:
				id = (all_caps ? 710 : 711);
				break;
			}
			return TextManager.getInstance().getString(id);
		}

		public static bool LinesIntersect(FPoint a1, FPoint a2, FPoint b1, FPoint b2)
		{
			return Common.LinesIntersect(a1, a2, b1, b2, null);
		}

		public static bool LinesIntersect(FPoint a1, FPoint a2, FPoint b1, FPoint b2, FPoint intersectFPoint)
		{
			if ((a1.mX == a2.mX && a1.mY == a2.mY) || (b1.mX == b2.mX && b1.mY == b2.mY))
			{
				return false;
			}
			a2.mX -= a1.mX;
			a2.mY -= a1.mY;
			b1.mX -= a1.mX;
			b1.mY -= a1.mY;
			b2.mX -= a1.mX;
			b2.mY -= a1.mY;
			double num = Math.Sqrt((double)(a2.mX * a2.mX + a2.mY * a2.mY));
			double num2 = (double)a2.mX / num;
			double num3 = (double)a2.mY / num;
			double num4 = (double)b1.mX * num2 + (double)b1.mY * num3;
			b1.mY = (float)((double)b1.mY * num2 - (double)b1.mX * num3);
			b1.mX = (float)num4;
			num4 = (double)b2.mX * num2 + (double)b2.mY * num3;
			b2.mY = (float)((double)b2.mY * num2 - (double)b2.mX * num3);
			b2.mX = (float)num4;
			if ((b1.mY < 0f && b2.mY < 0f) || (b1.mY >= 0f && b2.mY >= 0f))
			{
				return false;
			}
			double num5 = (double)(b2.mX + (b1.mX - b2.mX) * b2.mY / (b2.mY - b1.mY));
			if (num5 < 0.0 || num5 > num)
			{
				return false;
			}
			if (intersectFPoint != null)
			{
				intersectFPoint.mX = (float)((double)a1.mX + num5 * num2);
				intersectFPoint.mY = (float)((double)a1.mY + num5 * num3);
			}
			return true;
		}

		public static float GetCanonicalAngleRad(float theRad)
		{
			if (theRad >= 0f && theRad < 6.28318548f)
			{
				return theRad;
			}
			return Common.AceModF(theRad, 6.28318548f);
		}

		private static float AceModF(float x, float y)
		{
			if (x < 0f)
			{
				return y - -x % y;
			}
			return x % y;
		}

		public static string PILGetNameByImage(Image img)
		{
			return img.mNameForRes;
		}

		public static Image PILGetImageByName(string name)
		{
			SharedImageRef sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(name);
			if (sharedImageRef != null)
			{
				return sharedImageRef.GetImage();
			}
			return null;
		}

		public static int PILGetIDByImage(Image img)
		{
			return Res.GetIDByImage(img);
		}

		public static Image PILGetImageByID(int id)
		{
			return Res.GetImageByID((ResID)id);
		}

		public static void SerializePIEffect(PIEffect s, DataSync sync)
		{
			Buffer buffer = new Buffer();
			s.SaveState(buffer);
			Buffer buffer2 = sync.GetBuffer();
			buffer2.WriteLong((long)buffer.GetDataLen());
			buffer2.WriteBytes(buffer.GetDataPtr(), buffer.GetDataLen());
		}

		public static void DeserializePIEffect(PIEffect s, DataSync sync)
		{
			Buffer buffer = sync.GetBuffer();
			int num = (int)buffer.ReadLong();
			byte[] thePtr = new byte[num];
			buffer.ReadBytes(ref thePtr, num);
			Buffer buffer2 = new Buffer();
			buffer2.SetData(thePtr, num);
			s.LoadState(buffer2);
		}

		public static void SerializeParticleSystem(System s, DataSync sync)
		{
			Buffer buffer = new Buffer();
			s.Serialize(buffer, new GlobalMembers.GetIdByImageFunc(Common.PILGetIDByImage));
			Buffer buffer2 = sync.GetBuffer();
			buffer2.WriteLong((long)buffer.GetDataLen());
			buffer2.WriteBytes(buffer.GetDataPtr(), buffer.GetDataLen());
		}

		public static System DeserializeParticleSystem(DataSync sync)
		{
			Buffer buffer = sync.GetBuffer();
			int num = (int)buffer.ReadLong();
			byte[] thePtr = new byte[num];
			buffer.ReadBytes(ref thePtr, num);
			Buffer buffer2 = new Buffer();
			buffer2.SetData(thePtr, num);
			System system = System.Deserialize(buffer2, new GlobalMembers.GetImageByIdFunc(Common.PILGetImageByID));
			system.mScale = Common._S(1f);
			return system;
		}

		public const int MIN_LEVEL_FOR_BRONZE = 5;

		public const int MIN_LEVEL_FOR_SILVER = 10;

		public const int MIN_LEVEL_FOR_GOLD = 15;

		public const int MAX_DRAW_PRIORITY = 5;

		public const float MY_PI = 3.14159f;

		public const int MAX_CURVES = 4;

		public const int MAX_GUN_POINTS = 5;

		public const int POINTS_FOR_EXTRA_LIFE = 50000;

		public const int HOLE_SIZE = 96;

		public const int PROXIMITY_BOMB_RADIUS = 56;

		public const float EPSILON = 1E-06f;

		public const float JL_PI = 3.14159274f;

		public const float M_PI = 3.14159f;

		public const float FLT_MAX = 3.40282347E+38f;

		public const int MUSIC_LOADING = 0;

		public const int MUSIC_MENU = 1;

		public const int MUSIC_TUNE1 = 12;

		public const int MUSIC_TUNE2 = 24;

		public const int MUSIC_TUNE3 = 35;

		public const int MUSIC_TUNE4 = 45;

		public const int MUSIC_TUNE5 = 58;

		public const int MUSIC_TUNE6 = 71;

		public const int MUSIC_INTRO1 = 12;

		public const int MUSIC_INTRO2 = 24;

		public const int MUSIC_INTRO3 = 35;

		public const int MUSIC_INTRO4 = 45;

		public const int MUSIC_INTRO5 = 58;

		public const int MUSIC_INTRO6 = 71;

		public const int MUSIC_HI_SCORE = 116;

		public const int MUSIC_GAME_OVER = 126;

		public const int MUSIC_WON1 = 120;

		public const int MUSIC_WON2 = 121;

		public const int MUSIC_WON3 = 122;

		public const int MUSIC_WON4 = 123;

		public const int MUSIC_WON5 = 124;

		public const int MUSIC_WON6 = 125;

		public const int MUSIC_BOSS = 127;

		public const int MUSIC_BOSS_WIN = 137;

		public const int MUSIC_BONUS = 138;

		public const int MUSIC_WON_GAME = 144;

		public const int MUSIC_MISC1 = 95;

		public const int MUSIC_MISC2 = 100;

		public const int MUSIC_MISC3 = 105;

		public const int MUSIC_MISC4 = 110;

		public const int MUSIC_DANGER1 = 32;

		public const int MUSIC_DANGER2 = 33;

		public const int MUSIC_DANGER3 = 34;

		public static List<string> mTotalWords = new List<string>();

		public static bool[] gGotPowerUp = new bool[14];

		public static bool gSuckMode = false;

		public static bool gDieAtEnd = true;

		public static bool gAddBalls = true;

		public static int[] gBallColors = new int[] { 1671423, 16776960, 16711680, 65280, 16711935, 16777215 };

		public static int[] gBrightBallColors = new int[] { 8454143, 16777024, 16755370, 8454016, 16744703, 16777215 };

		public static int[] gDarkBallColors = new int[] { 2299513, 6312202, 10489620, 2114594, 5641795, 3676962 };

		public static int[] gTextBallColors = new int[] { 2984959, 16776960, 16711680, 65280, 16711935, 16777215 };
	}
}
