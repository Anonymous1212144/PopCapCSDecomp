using System;
using Sexy;

namespace BejeweledLIVE
{
	public static class GameConstants
	{
		public static int GEM_WIDTH
		{
			get
			{
				return Constants.mConstants.GEM_WIDTH;
			}
		}

		public static int GEM_HEIGHT
		{
			get
			{
				return Constants.mConstants.GEM_HEIGHT;
			}
		}

		public static int BIGTEXT_WIDTH
		{
			get
			{
				return Constants.mConstants.BIGTEXT_WIDTH;
			}
		}

		public static int BIGTEXT_HEIGHT
		{
			get
			{
				return Constants.mConstants.BIGTEXT_HEIGHT;
			}
		}

		public static bool LoadLevelBackdrops(int levelNumber, int nextLevelNumber)
		{
			return GlobalStaticVars.gSexyAppBase.mResourceManager.LoadLevelBackgrounds(levelNumber, nextLevelNumber, out GameConstants.BGV_TEXTURE, out GameConstants.BGH_TEXTURE);
		}

		public const int FRAMEWORK_UPDATE_RATE = 100;

		public const int NSTIMER_UPDATE_RATE = 30;

		public const int ASSOCIATED_ALPHA = 1;

		public const int CHEATS_ENABLED = 0;

		public const int NUM_PLANET_MOONS = 5;

		public const int TARGET_WIDTH = 480;

		public const int TARGET_HEIGHT = 320;

		public const int NUM_BOMBIFIED_FRAMES = 10;

		public const int MTRAND_N = 624;

		public const int CV_NUM_SPLINE_POINTS = 8192;

		public const int MOVE_ASSIST = 1;

		public const int NUM_LIGTNING_POINTS = 8;

		public const int HYPERWARP_ROWS = 7;

		public const int HYPERWARP_COLS = 7;

		public const int SCORE_X = 0;

		public const int SCORE_Y = 24;

		public const int ROBOT = 0;

		public const int MAX_SOURCE_SOUNDS = 256;

		public const int MAX_CHANNELS = 32;

		public const int PUZZLESTATE_X = 4;

		public const int PUZZLESTATE_Y = 2;

		public const int NUM_PUZZLE_PODS = 7;

		public const int NUM_HYPER_RINGS = 18;

		public const int NUM_RING_POINTS = 20;

		public const int NUM_FLOATING_THINGS = 2;

		public const int HIGH_SCORE_TABLE_SIZE = 5;

		public const int THREAD_PRINT = 1;

		public const int NUM_POINT_SETS = 4;

		public const int MAX_Z = 1024;

		public const int UI_Y = 125;

		public const int NUM_GEM_TYPES = 7;

		public const int NUM_SHRUNKEN_GEMS = 6;

		public const int FLAME_GEM_WIDTH = 40;

		public const int FLAME_GEM_HEIGHT = 50;

		public const int FLAME_GEM_H_OFFSET = 7;

		public const int FLAME_GEM_FRAME_COUNT = 20;

		public const int HYPER_CUBE_LENGTH = 40;

		public const int HYPER_CUBE_FRAME_COUNT = 20;

		public const int HYPER_CUBE_PER_ROW = 20;

		public const int HYPER_CUBE_OFFSET = 0;

		public const int LASER_GEM_FRAME_COUNT = 20;

		public const int LASER_GLOW_LENGTH = 80;

		public const int LASER_GLOW_FRAME_COUNT = 11;

		public const int LASER_GLOW_PER_ROW = 11;

		public const int LASER_GLOW_OFFSET = 20;

		public const int MAX_BACKDROPS = 50;

		public const int MULTIPLIER_FONT_WIDTH = 25;

		public const int MULTIPLIER_FONT_HEIGHT = 30;

		public const int MULTIPLIER_FONT_OFFSET = 7;

		public const int TRACK_CASCADE = 0;

		public const int AUTO_HINT_ADJUST = 0;

		public const int DEFAULT_COMBO_LEN = 3;

		public const int MAX_COMBO_LEN = 5;

		public const int MAX_COMBO_POWERUP_LEVEL = 3;

		public const int NUM_GEMCOLORS = 7;

		public const int NUM_TIME_SEGMENTS = 13;

		public const int NUM_TIMER_BARS = 25;

		public const int MAX_MULTIPLIER = 8;

		public const int MAX_SHARD = 5;

		public const int HINT_COOLDOWN = 400;

		public const int MAXIMUM_LIGHTNING = 8;

		public const int MAX_LAYERS = 5;

		public const int SAVEGAME_VERSION = 7;

		public const int SAVEGAME_VERSION_MIN = 7;

		public const int SAVEBOARD_VERSION = 1;

		public const int SAVEBOARD_VERSION_MIN = 1;

		public const int NUM_CHUNKS = 24;

		public const int EDIT_Y_PORTRAIT = 130;

		public const int EDIT_Y_LANDSCAPE = 98;

		public const int EDIT_WIDTH = 200;

		public const int EDIT_HEIGHT = 30;

		public const int LOAD_RING_IMAGE_WIDTH = 256;

		public const int LOAD_RING_IMAGE_HEIGHT = 256;

		public const int LOADING_IMAGE_WIDTH = 128;

		public const int LOADING_IMAGE_HEIGHT = 32;

		public const int LOAD_LOGO_IMAGE_WIDTH = 256;

		public const int LOAD_LOGO_IMAGE_HEIGHT = 256;

		public const int COPYRIGHT_IMAGE_WIDTH = 512;

		public const int COPYRIGHT_IMAGE_HEIGHT = 32;

		public const string DEFAULT_GALAXY = "Galaxy One";

		public const string FILENAME = "profile.dat";

		public const int TRUE = 1;

		public const int FALSE = 0;

		public const int MB_OK = 1;

		public const int NUM_PREFIXES = 10;

		public const int NUM_SUFFIXES = 16;

		public const int FILE_ATTRIBUTE_REPARSE_POINT = 1024;

		public const int FILE_ATTRIBUTE_READONLY = 1;

		public const int FILE_ATTRIBUTE_HIDDEN = 2;

		public const int FILE_ATTRIBUTE_SYSTEM = 4;

		public const int FILE_ATTRIBUTE_DIRECTORY = 16;

		public const int FILE_ATTRIBUTE_ARCHIVE = 32;

		public const int FILE_ATTRIBUTE_DEVICE = 64;

		public const int FILE_ATTRIBUTE_NORMAL = 128;

		public const int MOVEFILE_REPLACE_EXISTING = 1;

		public const int MOVEFILE_COPY_ALLOWED = 2;

		public const int MOVEFILE_DELAY_UNTIL_REBOOT = 4;

		public const int MOVEFILE_WRITE_THROUGH = 8;

		public const int VK_LBUTTON = 1;

		public const int VK_RBUTTON = 2;

		public const int VK_CANCEL = 3;

		public const int VK_MBUTTON = 4;

		public const int VK_BACK = 8;

		public const int VK_TAB = 9;

		public const int VK_CLEAR = 12;

		public const int VK_RETURN = 13;

		public const int VK_SHIFT = 16;

		public const int VK_CONTROL = 17;

		public const int VK_MENU = 18;

		public const int VK_PAUSE = 19;

		public const int VK_CAPITAL = 20;

		public const int VK_KANA = 21;

		public const int VK_HANGEUL = 21;

		public const int VK_HANGUL = 21;

		public const int VK_JUNJA = 23;

		public const int VK_FINAL = 24;

		public const int VK_HANJA = 25;

		public const int VK_KANJI = 25;

		public const int VK_ESCAPE = 27;

		public const int VK_CONVERT = 28;

		public const int VK_NONCONVERT = 29;

		public const int VK_ACCEPT = 30;

		public const int VK_MODECHANGE = 31;

		public const int VK_SPACE = 32;

		public const int VK_PRIOR = 33;

		public const int VK_NEXT = 34;

		public const int VK_END = 35;

		public const int VK_HOME = 36;

		public const int VK_LEFT = 37;

		public const int VK_UP = 38;

		public const int VK_RIGHT = 39;

		public const int VK_DOWN = 40;

		public const int VK_SELECT = 41;

		public const int VK_PRINT = 42;

		public const int VK_EXECUTE = 43;

		public const int VK_SNAPSHOT = 44;

		public const int VK_INSERT = 45;

		public const int VK_DELETE = 46;

		public const int VK_HELP = 47;

		public const int VK_LWIN = 91;

		public const int VK_RWIN = 92;

		public const int VK_APPS = 93;

		public const int VK_SLEEP = 95;

		public const int VK_NUMPAD0 = 96;

		public const int VK_NUMPAD1 = 97;

		public const int VK_NUMPAD2 = 98;

		public const int VK_NUMPAD3 = 99;

		public const int VK_NUMPAD4 = 100;

		public const int VK_NUMPAD5 = 101;

		public const int VK_NUMPAD6 = 102;

		public const int VK_NUMPAD7 = 103;

		public const int VK_NUMPAD8 = 104;

		public const int VK_NUMPAD9 = 105;

		public const int VK_MULTIPLY = 106;

		public const int VK_ADD = 107;

		public const int VK_SEPARATOR = 108;

		public const int VK_SUBTRACT = 109;

		public const int VK_DECIMAL = 110;

		public const int VK_DIVIDE = 111;

		public const int VK_F1 = 112;

		public const int VK_F2 = 113;

		public const int VK_F3 = 114;

		public const int VK_F4 = 115;

		public const int VK_F5 = 116;

		public const int VK_F6 = 117;

		public const int VK_F7 = 118;

		public const int VK_F8 = 119;

		public const int VK_F9 = 120;

		public const int VK_F10 = 121;

		public const int VK_F11 = 122;

		public const int VK_F12 = 123;

		public const int VK_F13 = 124;

		public const int VK_F14 = 125;

		public const int VK_F15 = 126;

		public const int VK_F16 = 127;

		public const int VK_F17 = 128;

		public const int VK_F18 = 129;

		public const int VK_F19 = 130;

		public const int VK_F20 = 131;

		public const int VK_F21 = 132;

		public const int VK_F22 = 133;

		public const int VK_F23 = 134;

		public const int VK_F24 = 135;

		public const int VK_NUMLOCK = 144;

		public const int VK_SCROLL = 145;

		public const int VK_LSHIFT = 160;

		public const int VK_RSHIFT = 161;

		public const int VK_LCONTROL = 162;

		public const int VK_RCONTROL = 163;

		public const int VK_LMENU = 164;

		public const int VK_RMENU = 165;

		public const double DBL_CMP = 1E-07;

		public const long POLYNOMIAL = 79764919L;

		public const int BUFSIZE = 4096;

		public const int MAX_VERTICES = 4096;

		public const int TRACK_FLUSHES = 1;

		public const long FONT_MAGIC = 4278190267L;

		public const int INCLUDE_WINDOWS_1252_EXTENSIONS = 1;

		public const int MTRAND_M = 397;

		public const ulong MATRIX_A = 2567483615UL;

		public const ulong UPPER_MASK = 2147483648UL;

		public const ulong LOWER_MASK = 2147483647UL;

		public const long TEMPERING_MASK_B = 2636928640L;

		public const long TEMPERING_MASK_C = 4022730752L;

		public const int VERBOSE_LOADING = 0;

		public const int SERIALIZE_FONTS = 0;

		public static string[] PREFIX_NAMES = new string[]
		{
			Strings.PREFIX_NAMES_New,
			Strings.PREFIX_NAMES_Junior,
			Strings.PREFIX_NAMES_Apprentice,
			Strings.PREFIX_NAMES_Journeyman,
			Strings.PREFIX_NAMES_Senior,
			Strings.PREFIX_NAMES_Expert,
			Strings.PREFIX_NAMES_Pro,
			Strings.PREFIX_NAMES_Master,
			Strings.PREFIX_NAMES_Superior,
			Strings.PREFIX_NAMES_Legendary
		};

		public static int[] PREFIX_CUTOFFS = new int[] { 0, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000 };

		public static string[] SUFFIX_NAMES = new string[]
		{
			Strings.SUFFIX_NAMES_Gemwasher,
			Strings.SUFFIX_NAMES_Appraiser,
			Strings.SUFFIX_NAMES_Digger,
			Strings.SUFFIX_NAMES_Searcher,
			Strings.SUFFIX_NAMES_Collector,
			Strings.SUFFIX_NAMES_Geologist,
			Strings.SUFFIX_NAMES_Gemologist,
			Strings.SUFFIX_NAMES_Jeweler,
			Strings.SUFFIX_NAMES_Polisher,
			Strings.SUFFIX_NAMES_Bezelier,
			Strings.SUFFIX_NAMES_Etcher,
			Strings.SUFFIX_NAMES_Engraver,
			Strings.SUFFIX_NAMES_Gemcarver,
			Strings.SUFFIX_NAMES_Lapidary,
			Strings.SUFFIX_NAMES_Glyptographer,
			Strings.SUFFIX_NAMES_Jewelcrafter
		};

		public static Image BGV_TEXTURE = null;

		public static Image BGH_TEXTURE = null;
	}
}
