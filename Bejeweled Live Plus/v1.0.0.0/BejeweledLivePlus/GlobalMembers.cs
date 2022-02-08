using System;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public static class GlobalMembers
	{
		public static void OutputDebugStrF(string fmt)
		{
		}

		internal static bool IsColoredGem(int theGemColor)
		{
			return theGemColor >= 0 && theGemColor < 7;
		}

		public static bool Count_FDrawPrimFilter(IntPtr theContext, int thePrimType, uint thePrimCount, SexyVertex2D theVertices, int theVertexSize, Rect[] theClipRect)
		{
			GlobalMembers.gDrawPrimCount++;
			GlobalMembers.gDrawPrimTris += (int)thePrimCount;
			return true;
		}

		public static void KILL_WIDGET_NOW(Widget theWidget)
		{
			if (theWidget != null)
			{
				if (theWidget.mParent != null)
				{
					theWidget.mParent.RemoveWidget(theWidget);
				}
				theWidget.Dispose();
			}
		}

		public static void KILL_WIDGET(Widget theWidget)
		{
			if (theWidget != null)
			{
				if (theWidget.mParent != null)
				{
					theWidget.mParent.RemoveWidget(theWidget);
				}
				GlobalMembers.gApp.SafeDeleteWidget(theWidget);
			}
		}

		public static double RS(double theNum)
		{
			return theNum * 1200.0 / (double)GlobalMembers.gApp.mArtRes;
		}

		public static float RS(float theNum)
		{
			return theNum * 1200f / (float)GlobalMembers.gApp.mArtRes;
		}

		public static int RS(int theNum)
		{
			return (int)((float)theNum * 1200f / (float)GlobalMembers.gApp.mArtRes);
		}

		public static double S(double theNum)
		{
			return theNum * (double)GlobalMembers.gApp.mArtRes / 1200.0;
		}

		public static float S(float theNum)
		{
			return theNum * (float)GlobalMembers.gApp.mArtRes / 1200f;
		}

		public static int S(int theNum)
		{
			return (int)((float)(theNum * GlobalMembers.gApp.mArtRes) / 1200f);
		}

		public static Point S(Point theNum)
		{
			return theNum * GlobalMembers.gApp.mArtRes / 1200;
		}

		public static Rect S(Rect theRect)
		{
			return new Rect(GlobalMembers.S(theRect.mX), GlobalMembers.S(theRect.mY), GlobalMembers.S(theRect.mWidth), GlobalMembers.S(theRect.mHeight));
		}

		public static int MS(int num)
		{
			return GlobalMembers.S(num);
		}

		public static double MS(double num)
		{
			return GlobalMembers.S(num);
		}

		public static float MS(float num)
		{
			return GlobalMembers.S(num);
		}

		public static byte M(byte val)
		{
			return val;
		}

		public static char M(char val)
		{
			return val;
		}

		public static short M(short val)
		{
			return val;
		}

		public static ushort M(ushort val)
		{
			return val;
		}

		public static int M(int val)
		{
			return val;
		}

		public static uint M(uint val)
		{
			return val;
		}

		public static long M(long val)
		{
			return val;
		}

		public static ulong M(ulong val)
		{
			return val;
		}

		public static float M(float val)
		{
			return val;
		}

		public static double M(double val)
		{
			return val;
		}

		public static string MP(string val)
		{
			return val;
		}

		public static void KILL_WIDGET(DialogButton theWidget)
		{
			if (theWidget != null)
			{
				if (theWidget.mParent != null)
				{
					theWidget.mParent.RemoveWidget(theWidget);
				}
				GlobalMembers.gApp.SafeDeleteWidget(theWidget);
				theWidget = null;
			}
		}

		public static string _ID(string str, int id)
		{
			return GlobalMembers.gApp.mPopLoc.GetString(id, str);
		}

		public static string Version
		{
			get
			{
				return "0.0.0.19";
			}
		}

		public static int MIN(int a, int b)
		{
			if (a <= b)
			{
				return a;
			}
			return b;
		}

		public static float MIN(float a, float b)
		{
			if (a <= b)
			{
				return a;
			}
			return b;
		}

		public static double MIN(double a, double b)
		{
			if (a <= b)
			{
				return a;
			}
			return b;
		}

		public static int MAX(int a, int b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		public static float MAX(float a, float b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		public static double MAX(double a, double b)
		{
			if (a <= b)
			{
				return b;
			}
			return a;
		}

		public static float fmod(float x, float y)
		{
			return x % y;
		}

		public static float EulerInterpolate(float from, float to, float u)
		{
			float num = GlobalMembers.fmod(to - from, GlobalMembers.SEXYMATH_2PI);
			if (num < 0f)
			{
				num = GlobalMembers.SEXYMATH_2PI + num;
			}
			if (num > GlobalMembers.SEXYMATH_PI)
			{
				num = -1f * (GlobalMembers.SEXYMATH_2PI - num);
			}
			float num2 = GlobalMembers.fmod(from + num * u, GlobalMembers.SEXYMATH_2PI);
			if (num2 < 0f)
			{
				num2 = GlobalMembers.SEXYMATH_2PI + num2;
			}
			return num2;
		}

		public static float IMG_SXOFS(int id)
		{
			return GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(id));
		}

		public static float IMG_SYOFS(int id)
		{
			return GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(id));
		}

		public static Rect IMGRECT_S(Image img, float xofs, float yofs)
		{
			return new Rect((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs((int)GlobalMembersResourcesWP.GetIdByImage(img)) + xofs), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs((int)GlobalMembersResourcesWP.GetIdByImage(img)) + yofs), img.GetCelWidth(), img.GetCelHeight());
		}

		public static Rect IMGRECT_NS(Image img, float xOfs, float yOfs)
		{
			return new Rect((int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs((int)GlobalMembersResourcesWP.GetIdByImage(img))) + xOfs), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs((int)GlobalMembersResourcesWP.GetIdByImage(img))) + yOfs), img.GetCelWidth(), img.GetCelHeight());
		}

		public static Rect IMGSRCRECT(Image img, int frame)
		{
			return new Rect(img.GetCelWidth() * (frame % img.mNumCols), img.GetCelHeight() * (frame / img.mNumCols), img.GetCelWidth(), img.GetCelHeight());
		}

		public static bool dbg_assert(bool _Expression)
		{
			return true;
		}

		public static void DBG_ASSERT(bool exp)
		{
			GlobalMembers.gInAssert = true;
			GlobalMembers.dbg_assert(exp);
			GlobalMembers.gInAssert = false;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static GlobalMembers()
		{
			/*
An exception occurred when decompiling this method (060004D5)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void BejeweledLivePlus.GlobalMembers::.cctor()
 ---> System.ArgumentException: Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.
   at System.ThrowHelper.ThrowArgumentException(ExceptionResource resource)
   at System.Collections.Generic.List`1.GetRange(Int32 index, Int32 count)
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformByteCode(ILExpression byteCode) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 599
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformExpression(ILExpression expr) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 398
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformByteCode(ILExpression byteCode) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 479
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformExpression(ILExpression expr) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 398
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformNode(ILNode node) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 270
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformBlock(ILBlock block) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 254
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 151
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1534
*/;
		}

		internal const int COLOR_COUNT = 7;

		public const int NUM_ROWS = 8;

		public const int NUM_COLS = 8;

		public const int NUM_GEMCOLORS = 7;

		public const int GEM_WIDTH = 100;

		public const int GEM_HEIGHT = 100;

		public const string HIGHSCORES_FILENAME = "users\\hiscores.dat";

		public const int NUM_SHRUNKEN_GEMS = 15;

		public const int POKER_UNLOCK_AT_LEVEL = 5;

		public const int BUTTERFLY_UNLOCK_AT_LEVEL = 5;

		public const int ICESTORM_UNLOCK_AT_SCORE = 100000;

		public static readonly float[,,] GEM_OUTLINE_RADIUS_POINTS;

		public static Bej3Button mByTodayButton;

		public static Bej3Button mByAllTimeButton;

		public static bool isLeaderboardLoading;

		public static readonly int NUM_DIST_POINTS;

		public static readonly int NUM_RADIAL_POINTS;

		public static readonly float PI;

		public static readonly float MAGIC_SCALE;

		public static readonly int MAX_Z;

		public static float[] COS_TAB;

		public static float[] SIN_TAB;

		public static bool gTableInitialized;

		public static readonly int SCORE_X;

		public static readonly int SCORE_Y;

		public static readonly int UI_X;

		public static readonly int UI_Y;

		public static readonly int NUM_WARP_COLS;

		public static readonly int NUM_WARP_ROWS;

		public static readonly int NUM_HYPER_RINGS;

		public static readonly int NUM_RING_POINTS;

		public static readonly int NUM_FLOATING_THINGS;

		public static string[] gObjectImgs;

		public static AchievementCollection g_AchievementsXLive;

		internal static Color[] gCycleColors;

		public static string[] gBackgroundNames;

		internal static int[] aDesiredOrderList;

		public static Color[] gAllGemColors;

		public static GlobalMembers.HackGemColors gGemColors;

		public static Color[] gPointColorsArr;

		public static GlobalMembers.HackPointColors gPointColors;

		public static Color[] gComboColors;

		internal static int[] gComplements;

		public static string[] gComplementStr;

		internal static readonly int gComplementCount;

		internal static float mGravityMod;

		internal static float mGemSwapSpeedMod;

		internal static int gExplodeCount;

		internal static int[,] gExplodePoints;

		internal static int[,] gShardPoints;

		internal static int[,] gShardExplodeCenter;

		internal static int[] gShardTypes;

		internal static int gShardCount;

		public static int gFrameLightCount;

		public static int gDrawPrimCount;

		public static int gDrawPrimTris;

		public static GraphicsRecorder gGR;

		public static BejeweledLivePlusApp gApp;

		public static Game gGameMain;

		public static readonly int Max_LAYERS;

		public static readonly float M_PI;

		public static readonly float M_2PI;

		public static bool gInAssert;

		public enum PROFILEMENU_BUTTON_IDS
		{
			BTN_EDIT_ID,
			BTN_BACK_ID,
			BTN_STATS_ID,
			BTN_HIGHSCORES_ID,
			BTN_BADGES_ID,
			BTN_GAMECENTER_ID,
			BTN_PROFILE_MENU_UNUSED_ID
		}

		public struct WarpPoint
		{
			public float mX;

			public float mY;

			public float mZ;

			public float mVelX;

			public float mVelY;

			public float mU;

			public float mV;

			public float mRot;

			public float mDist;
		}

		public struct HyperPoint
		{
			public float mX;

			public float mY;

			public float mU;

			public float mV;
		}

		public struct HyperRing
		{
			public void Init()
			{
				this.mHyperPoints = new GlobalMembers.HyperPoint[GlobalMembers.NUM_RING_POINTS];
			}

			public float mFromX;

			public float mFromY;

			public float mToX;

			public float mToY;

			public float mCurX;

			public float mCurY;

			public float mCurScreenX;

			public float mCurScreenY;

			public float mCurScreenRadius;

			public float mFromRot;

			public float mToRot;

			public GlobalMembers.HyperPoint[] mHyperPoints;
		}

		public class HackGemColors
		{
			public Color this[int idx]
			{
				get
				{
					return GlobalMembers.gAllGemColors[idx + 1];
				}
			}
		}

		public class HackPointColors
		{
			public Color this[int idx]
			{
				get
				{
					return GlobalMembers.gPointColorsArr[idx + 1];
				}
			}
		}

		public enum VOLUME
		{
			VOLUME_SFX,
			VOLUME_VOICES,
			VOLUME_ZEN_AMBIENT,
			VOLUME_ZEN_BINAURAL,
			VOLUME_ZEN_BREATH
		}
	}
}
