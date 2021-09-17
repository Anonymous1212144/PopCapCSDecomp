using System;
using Sexy;

public static class Resources
{
	public static bool ExtractResourcesByName(ResourceManager theManager, string theName)
	{
		if (theName == "3dImages")
		{
			return Resources.Extract3dImagesResources(theManager);
		}
		if (theName == "Atlases")
		{
			return Resources.ExtractAtlasesResources(theManager);
		}
		if (theName == "Levels")
		{
			return Resources.ExtractLevelsResources(theManager);
		}
		return theName == "LoadingThread" && Resources.ExtractLoadingThreadResources(theManager);
	}

	public static void ExtractResources(ResourceManager theManager, AtlasResources theRes)
	{
		Resources.Extract3dImagesResources(theManager);
		Resources.ExtractAtlasesResources(theManager);
		Resources.ExtractLevelsResources(theManager);
		Resources.ExtractLoadingThreadResources(theManager);
		theRes.ExtractResources();
	}

	public static bool Extract3dImagesResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_HYPERSPACE = theManager.GetImageThrow("IMAGE_HYPERSPACE");
			Resources.IMAGE_HYPERSPACE_INITIAL = theManager.GetImageThrow("IMAGE_HYPERSPACE_INITIAL");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractAtlasesResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_EXTRAS = theManager.GetImageThrow("IMAGE_EXTRAS");
			Resources.IMAGE_ADDITIVES = theManager.GetImageThrow("IMAGE_ADDITIVES");
			Resources.IMAGE_BOARD = theManager.GetImageThrow("IMAGE_BOARD");
			Resources.IMAGE_UI = theManager.GetImageThrow("IMAGE_UI");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractLevelsResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		return true;
	}

	public static bool ExtractLoadingThreadResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_MAINMENU_BKG = theManager.GetImageThrow("IMAGE_MAINMENU_BKG");
			Resources.IMAGE_MAINMENU_BKG_LAYER1 = theManager.GetImageThrow("IMAGE_MAINMENU_BKG_LAYER1");
			Resources.IMAGE_MAINMENU_BKG_LAYER2 = theManager.GetImageThrow("IMAGE_MAINMENU_BKG_LAYER2");
			Resources.IMAGE_MAINMENU_BKG_LAYER3 = theManager.GetImageThrow("IMAGE_MAINMENU_BKG_LAYER3");
			Resources.SOUND_BAD = theManager.GetSoundThrow("SOUND_BAD");
			Resources.SOUND_GEM_HIT = theManager.GetSoundThrow("SOUND_GEM_HIT");
			Resources.SOUND_CLICK = theManager.GetSoundThrow("SOUND_CLICK");
			Resources.SOUND_SELECT = theManager.GetSoundThrow("SOUND_SELECT");
			Resources.SOUND_MULTISHOT = theManager.GetSoundThrow("SOUND_MULTISHOT");
			Resources.SOUND_MAIN_GAME_START = theManager.GetSoundThrow("SOUND_MAIN_GAME_START");
			Resources.SOUND_MAIN_MOUSEOVER = theManager.GetSoundThrow("SOUND_MAIN_MOUSEOVER");
			Resources.SOUND_BOMB_EXPLODE = theManager.GetSoundThrow("SOUND_BOMB_EXPLODE");
			Resources.SOUND_TRANSFER_BIG = theManager.GetSoundThrow("SOUND_TRANSFER_BIG");
			Resources.SOUND_TRANSFER = theManager.GetSoundThrow("SOUND_TRANSFER");
			Resources.SOUND_COMBO_1 = theManager.GetSoundThrow("SOUND_COMBO_1");
			Resources.SOUND_COMBO_2 = theManager.GetSoundThrow("SOUND_COMBO_2");
			Resources.SOUND_COMBO_3 = theManager.GetSoundThrow("SOUND_COMBO_3");
			Resources.SOUND_COMBO_4 = theManager.GetSoundThrow("SOUND_COMBO_4");
			Resources.SOUND_COMBO_5 = theManager.GetSoundThrow("SOUND_COMBO_5");
			Resources.SOUND_COMBO_6 = theManager.GetSoundThrow("SOUND_COMBO_6");
			Resources.SOUND_SPEED_1 = theManager.GetSoundThrow("SOUND_SPEED_1");
			Resources.SOUND_SPEED_2 = theManager.GetSoundThrow("SOUND_SPEED_2");
			Resources.SOUND_SPEED_3 = theManager.GetSoundThrow("SOUND_SPEED_3");
			Resources.SOUND_SPEED_4 = theManager.GetSoundThrow("SOUND_SPEED_4");
			Resources.SOUND_SPEED_5 = theManager.GetSoundThrow("SOUND_SPEED_5");
			Resources.SOUND_SPEED_6 = theManager.GetSoundThrow("SOUND_SPEED_6");
			Resources.SOUND_SPEED_7 = theManager.GetSoundThrow("SOUND_SPEED_7");
			Resources.SOUND_SPEED_8 = theManager.GetSoundThrow("SOUND_SPEED_8");
			Resources.SOUND_SPEED_9 = theManager.GetSoundThrow("SOUND_SPEED_9");
			Resources.SOUND_HYPERGEM_CREATE = theManager.GetSoundThrow("SOUND_HYPERGEM_CREATE");
			Resources.SOUND_HYPERGEM_DESTROYED = theManager.GetSoundThrow("SOUND_HYPERGEM_DESTROYED");
			Resources.SOUND_EXPLODE = theManager.GetSoundThrow("SOUND_EXPLODE");
			Resources.SOUND_WARNING = theManager.GetSoundThrow("SOUND_WARNING");
			Resources.SOUND_WHIRLPOOL_START = theManager.GetSoundThrow("SOUND_WHIRLPOOL_START");
			Resources.SOUND_WHIRLPOOL_END = theManager.GetSoundThrow("SOUND_WHIRLPOOL_END");
			Resources.SOUND_GOOD = theManager.GetSoundThrow("SOUND_GOOD");
			Resources.SOUND_EXCELLENT = theManager.GetSoundThrow("SOUND_EXCELLENT");
			Resources.SOUND_AWESOME = theManager.GetSoundThrow("SOUND_AWESOME");
			Resources.SOUND_SPECTACULAR = theManager.GetSoundThrow("SOUND_SPECTACULAR");
			Resources.SOUND_EXTRAORDINARY = theManager.GetSoundThrow("SOUND_EXTRAORDINARY");
			Resources.SOUND_UNBELIEVABLE = theManager.GetSoundThrow("SOUND_UNBELIEVABLE");
			Resources.SOUND_GAME_OVER = theManager.GetSoundThrow("SOUND_GAME_OVER");
			Resources.SOUND_GO = theManager.GetSoundThrow("SOUND_GO");
			Resources.SOUND_LEVEL_COMPLETE = theManager.GetSoundThrow("SOUND_LEVEL_COMPLETE");
			Resources.SOUND_NO_MORE_MOVES = theManager.GetSoundThrow("SOUND_NO_MORE_MOVES");
			Resources.SOUND_TIME_UP = theManager.GetSoundThrow("SOUND_TIME_UP");
			Resources.SOUND_BLAZING_SPEED = theManager.GetSoundThrow("SOUND_BLAZING_SPEED");
			Resources.SOUND_ELECTRO_START = theManager.GetSoundThrow("SOUND_ELECTRO_START");
			Resources.SOUND_ELECTRO_PATH = theManager.GetSoundThrow("SOUND_ELECTRO_PATH");
			Resources.SOUND_ELECTRO_EXPLODE = theManager.GetSoundThrow("SOUND_ELECTRO_EXPLODE");
			Resources.SOUND_BURNING_SPEED = theManager.GetSoundThrow("SOUND_BURNING_SPEED");
			Resources.SOUND_LIGHTNING_MAKE = theManager.GetSoundThrow("SOUND_LIGHTNING_MAKE");
			Resources.SOUND_MULTIPLIER_MAKE = theManager.GetSoundThrow("SOUND_MULTIPLIER_MAKE");
			Resources.SOUND_MULTIPLIER_TRIGGER = theManager.GetSoundThrow("SOUND_MULTIPLIER_TRIGGER");
			Resources.FONT_LOCALIZED_TEST = theManager.GetFontThrow("FONT_LOCALIZED_TEST");
			Resources.FONT_BUTTON = theManager.GetFontThrow("FONT_BUTTON");
			Resources.FONT_HUGE = theManager.GetFontThrow("FONT_HUGE");
			Resources.FONT_TINY_TEXT = theManager.GetFontThrow("FONT_TINY_TEXT");
			Resources.FONT_TEXT = theManager.GetFontThrow("FONT_TEXT");
			Resources.FONT_BIG_TEXT = theManager.GetFontThrow("FONT_BIG_TEXT");
			Resources.FONT_LEADERBOARD = theManager.GetFontThrow("FONT_LEADERBOARD");
			Resources.FONT_MULTIPLIER_GEMS = theManager.GetFontThrow("FONT_MULTIPLIER_GEMS");
			Resources.FONT_FLOAT_POINTS = theManager.GetFontThrow("FONT_FLOAT_POINTS");
			Resources.FONT_SCORE = theManager.GetFontThrow("FONT_SCORE");
			Resources.FONT_TIMER = theManager.GetFontThrow("FONT_TIMER");
			Resources.FONT_BOMB = theManager.GetFontThrow("FONT_BOMB");
			Resources.FONT_ACHIEVEMENT_NAME = theManager.GetFontThrow("FONT_ACHIEVEMENT_NAME");
			Resources.FONT_HEADING = theManager.GetFontThrow("FONT_HEADING");
			Resources.FONT_SPEED_BONUS = theManager.GetFontThrow("FONT_SPEED_BONUS");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static Image GetImageById(int theId)
	{
		return (Image)Resources.gResources[theId];
	}

	public static Font GetFontById(int theId)
	{
		return (Font)Resources.gResources[theId];
	}

	public static int GetSoundById(int theId)
	{
		return (int)Resources.gResources[theId];
	}

	public static Image GetImageRefById(int theId)
	{
		return (Image)Resources.gResources[theId];
	}

	public static Font GetFontRefById(int theId)
	{
		return (Font)Resources.gResources[theId];
	}

	public static int GetSoundRefById(int theId)
	{
		return (int)Resources.gResources[theId];
	}

	public static Resources.ResourceId GetIdByImage(Image theImage)
	{
		return Resources.GetIdByVariable(theImage);
	}

	public static Resources.ResourceId GetIdByFont(Font theFont)
	{
		return Resources.GetIdByVariable(theFont);
	}

	public static Resources.ResourceId GetIdBySound(int theSound)
	{
		return Resources.GetIdByVariable((IntPtr)theSound);
	}

	public static Resources.ResourceId GetIdByStringId(string theStringId)
	{
		return Resources.ResourceId.RESOURCE_ID_MAX;
	}

	public static Resources.ResourceId GetIdByVariable(object theVariable)
	{
		return Resources.ResourceId.RESOURCE_ID_MAX;
	}

	public static void LinkUpResArray()
	{
		object[] array = new object[76];
		array[0] = Resources.IMAGE_HYPERSPACE;
		array[1] = Resources.IMAGE_HYPERSPACE_INITIAL;
		array[2] = Resources.IMAGE_EXTRAS;
		array[3] = Resources.IMAGE_ADDITIVES;
		array[4] = Resources.IMAGE_BOARD;
		array[5] = Resources.IMAGE_UI;
		array[6] = Resources.IMAGE_MAINMENU_BKG;
		array[7] = Resources.IMAGE_MAINMENU_BKG_LAYER1;
		array[8] = Resources.IMAGE_MAINMENU_BKG_LAYER2;
		array[9] = Resources.IMAGE_MAINMENU_BKG_LAYER3;
		array[10] = Resources.FONT_LOCALIZED_TEST;
		array[11] = Resources.FONT_BUTTON;
		array[12] = Resources.FONT_HUGE;
		array[13] = Resources.FONT_TINY_TEXT;
		array[14] = Resources.FONT_TEXT;
		array[15] = Resources.FONT_BIG_TEXT;
		array[16] = Resources.FONT_LEADERBOARD;
		array[17] = Resources.FONT_MULTIPLIER_GEMS;
		array[18] = Resources.FONT_FLOAT_POINTS;
		array[19] = Resources.FONT_SCORE;
		array[20] = Resources.FONT_TIMER;
		array[21] = Resources.FONT_BOMB;
		array[22] = Resources.FONT_ACHIEVEMENT_NAME;
		array[23] = Resources.FONT_HEADING;
		array[24] = Resources.FONT_SPEED_BONUS;
		array[25] = Resources.SOUND_BAD;
		array[26] = Resources.SOUND_GEM_HIT;
		array[27] = Resources.SOUND_CLICK;
		array[28] = Resources.SOUND_SELECT;
		array[29] = Resources.SOUND_MULTISHOT;
		array[30] = Resources.SOUND_MAIN_GAME_START;
		array[31] = Resources.SOUND_MAIN_MOUSEOVER;
		array[32] = Resources.SOUND_BOMB_EXPLODE;
		array[33] = Resources.SOUND_TRANSFER_BIG;
		array[34] = Resources.SOUND_TRANSFER;
		array[35] = Resources.SOUND_COMBO_1;
		array[36] = Resources.SOUND_COMBO_2;
		array[37] = Resources.SOUND_COMBO_3;
		array[38] = Resources.SOUND_COMBO_4;
		array[39] = Resources.SOUND_COMBO_5;
		array[40] = Resources.SOUND_COMBO_6;
		array[41] = Resources.SOUND_SPEED_1;
		array[42] = Resources.SOUND_SPEED_2;
		array[43] = Resources.SOUND_SPEED_3;
		array[44] = Resources.SOUND_SPEED_4;
		array[45] = Resources.SOUND_SPEED_5;
		array[46] = Resources.SOUND_SPEED_6;
		array[47] = Resources.SOUND_SPEED_7;
		array[48] = Resources.SOUND_SPEED_8;
		array[49] = Resources.SOUND_SPEED_9;
		array[50] = Resources.SOUND_HYPERGEM_CREATE;
		array[51] = Resources.SOUND_HYPERGEM_DESTROYED;
		array[52] = Resources.SOUND_EXPLODE;
		array[53] = Resources.SOUND_WARNING;
		array[54] = Resources.SOUND_WHIRLPOOL_START;
		array[55] = Resources.SOUND_WHIRLPOOL_END;
		array[56] = Resources.SOUND_GOOD;
		array[57] = Resources.SOUND_EXCELLENT;
		array[58] = Resources.SOUND_AWESOME;
		array[59] = Resources.SOUND_SPECTACULAR;
		array[60] = Resources.SOUND_EXTRAORDINARY;
		array[61] = Resources.SOUND_UNBELIEVABLE;
		array[62] = Resources.SOUND_GAME_OVER;
		array[63] = Resources.SOUND_GO;
		array[64] = Resources.SOUND_LEVEL_COMPLETE;
		array[65] = Resources.SOUND_NO_MORE_MOVES;
		array[66] = Resources.SOUND_TIME_UP;
		array[67] = Resources.SOUND_BLAZING_SPEED;
		array[68] = Resources.SOUND_ELECTRO_START;
		array[69] = Resources.SOUND_ELECTRO_PATH;
		array[70] = Resources.SOUND_ELECTRO_EXPLODE;
		array[71] = Resources.SOUND_BURNING_SPEED;
		array[72] = Resources.SOUND_LIGHTNING_MAKE;
		array[73] = Resources.SOUND_MULTIPLIER_MAKE;
		array[74] = Resources.SOUND_MULTIPLIER_TRIGGER;
		Resources.gResources = array;
	}

	public static string GetStringIdById(int theId)
	{
		switch (theId)
		{
		case 0:
			return "IMAGE_HYPERSPACE";
		case 1:
			return "IMAGE_HYPERSPACE_INITIAL";
		case 2:
			return "IMAGE_EXTRAS";
		case 3:
			return "IMAGE_ADDITIVES";
		case 4:
			return "IMAGE_BOARD";
		case 5:
			return "IMAGE_UI";
		case 6:
			return "IMAGE_MAINMENU_BKG";
		case 7:
			return "IMAGE_MAINMENU_BKG_LAYER1";
		case 8:
			return "IMAGE_MAINMENU_BKG_LAYER2";
		case 9:
			return "IMAGE_MAINMENU_BKG_LAYER3";
		case 10:
			return "SOUND_BAD";
		case 11:
			return "SOUND_GEM_HIT";
		case 12:
			return "SOUND_CLICK";
		case 13:
			return "SOUND_SELECT";
		case 14:
			return "SOUND_MULTISHOT";
		case 15:
			return "SOUND_MAIN_GAME_START";
		case 16:
			return "SOUND_MAIN_MOUSEOVER";
		case 17:
			return "SOUND_BOMB_EXPLODE";
		case 18:
			return "SOUND_TRANSFER_BIG";
		case 19:
			return "SOUND_TRANSFER";
		case 20:
			return "SOUND_COMBO_1";
		case 21:
			return "SOUND_COMBO_2";
		case 22:
			return "SOUND_COMBO_3";
		case 23:
			return "SOUND_COMBO_4";
		case 24:
			return "SOUND_COMBO_5";
		case 25:
			return "SOUND_COMBO_6";
		case 26:
			return "SOUND_SPEED_1";
		case 27:
			return "SOUND_SPEED_2";
		case 28:
			return "SOUND_SPEED_3";
		case 29:
			return "SOUND_SPEED_4";
		case 30:
			return "SOUND_SPEED_5";
		case 31:
			return "SOUND_SPEED_6";
		case 32:
			return "SOUND_SPEED_7";
		case 33:
			return "SOUND_SPEED_8";
		case 34:
			return "SOUND_SPEED_9";
		case 35:
			return "SOUND_HYPERGEM_CREATE";
		case 36:
			return "SOUND_HYPERGEM_DESTROYED";
		case 37:
			return "SOUND_EXPLODE";
		case 38:
			return "SOUND_WARNING";
		case 39:
			return "SOUND_WHIRLPOOL_START";
		case 40:
			return "SOUND_WHIRLPOOL_END";
		case 41:
			return "SOUND_GOOD";
		case 42:
			return "SOUND_EXCELLENT";
		case 43:
			return "SOUND_AWESOME";
		case 44:
			return "SOUND_SPECTACULAR";
		case 45:
			return "SOUND_EXTRAORDINARY";
		case 46:
			return "SOUND_UNBELIEVABLE";
		case 47:
			return "SOUND_GAME_OVER";
		case 48:
			return "SOUND_GO";
		case 49:
			return "SOUND_LEVEL_COMPLETE";
		case 50:
			return "SOUND_NO_MORE_MOVES";
		case 51:
			return "SOUND_TIME_UP";
		case 52:
			return "SOUND_BLAZING_SPEED";
		case 53:
			return "SOUND_ELECTRO_START";
		case 54:
			return "SOUND_ELECTRO_PATH";
		case 55:
			return "SOUND_ELECTRO_EXPLODE";
		case 56:
			return "SOUND_BURNING_SPEED";
		case 57:
			return "SOUND_LIGHTNING_MAKE";
		case 58:
			return "SOUND_MULTIPLIER_MAKE";
		case 59:
			return "SOUND_MULTIPLIER_TRIGGER";
		case 60:
			return "FONT_LOCALIZED_TEST";
		case 61:
			return "FONT_BUTTON";
		case 62:
			return "FONT_HUGE";
		case 63:
			return "FONT_TINY_TEXT";
		case 64:
			return "FONT_TEXT";
		case 65:
			return "FONT_BIG_TEXT";
		case 66:
			return "FONT_LEADERBOARD";
		case 67:
			return "FONT_MULTIPLIER_GEMS";
		case 68:
			return "FONT_FLOAT_POINTS";
		case 69:
			return "FONT_SCORE";
		case 70:
			return "FONT_TIMER";
		case 71:
			return "FONT_BOMB";
		case 72:
			return "FONT_ACHIEVEMENT_NAME";
		case 73:
			return "FONT_HEADING";
		case 74:
			return "FONT_SPEED_BONUS";
		default:
			return "";
		}
	}

	// Note: this type is marked as 'beforefieldinit'.
	static Resources()
	{
		object[] array = new object[76];
		array[0] = Resources.IMAGE_HYPERSPACE;
		array[1] = Resources.IMAGE_HYPERSPACE_INITIAL;
		array[2] = Resources.IMAGE_EXTRAS;
		array[3] = Resources.IMAGE_ADDITIVES;
		array[4] = Resources.IMAGE_BOARD;
		array[5] = Resources.IMAGE_UI;
		array[6] = Resources.IMAGE_MAINMENU_BKG;
		array[7] = Resources.IMAGE_MAINMENU_BKG_LAYER1;
		array[8] = Resources.IMAGE_MAINMENU_BKG_LAYER2;
		array[9] = Resources.IMAGE_MAINMENU_BKG_LAYER3;
		array[10] = Resources.FONT_LOCALIZED_TEST;
		array[11] = Resources.FONT_BUTTON;
		array[12] = Resources.FONT_HUGE;
		array[13] = Resources.FONT_TINY_TEXT;
		array[14] = Resources.FONT_TEXT;
		array[15] = Resources.FONT_BIG_TEXT;
		array[16] = Resources.FONT_LEADERBOARD;
		array[17] = Resources.FONT_MULTIPLIER_GEMS;
		array[18] = Resources.FONT_FLOAT_POINTS;
		array[19] = Resources.FONT_SCORE;
		array[20] = Resources.FONT_TIMER;
		array[21] = Resources.FONT_BOMB;
		array[22] = Resources.FONT_ACHIEVEMENT_NAME;
		array[23] = Resources.FONT_HEADING;
		array[24] = Resources.FONT_SPEED_BONUS;
		array[25] = Resources.SOUND_BAD;
		array[26] = Resources.SOUND_GEM_HIT;
		array[27] = Resources.SOUND_CLICK;
		array[28] = Resources.SOUND_SELECT;
		array[29] = Resources.SOUND_MULTISHOT;
		array[30] = Resources.SOUND_MAIN_GAME_START;
		array[31] = Resources.SOUND_MAIN_MOUSEOVER;
		array[32] = Resources.SOUND_BOMB_EXPLODE;
		array[33] = Resources.SOUND_TRANSFER_BIG;
		array[34] = Resources.SOUND_TRANSFER;
		array[35] = Resources.SOUND_COMBO_1;
		array[36] = Resources.SOUND_COMBO_2;
		array[37] = Resources.SOUND_COMBO_3;
		array[38] = Resources.SOUND_COMBO_4;
		array[39] = Resources.SOUND_COMBO_5;
		array[40] = Resources.SOUND_COMBO_6;
		array[41] = Resources.SOUND_SPEED_1;
		array[42] = Resources.SOUND_SPEED_2;
		array[43] = Resources.SOUND_SPEED_3;
		array[44] = Resources.SOUND_SPEED_4;
		array[45] = Resources.SOUND_SPEED_5;
		array[46] = Resources.SOUND_SPEED_6;
		array[47] = Resources.SOUND_SPEED_7;
		array[48] = Resources.SOUND_SPEED_8;
		array[49] = Resources.SOUND_SPEED_9;
		array[50] = Resources.SOUND_HYPERGEM_CREATE;
		array[51] = Resources.SOUND_HYPERGEM_DESTROYED;
		array[52] = Resources.SOUND_EXPLODE;
		array[53] = Resources.SOUND_WARNING;
		array[54] = Resources.SOUND_WHIRLPOOL_START;
		array[55] = Resources.SOUND_WHIRLPOOL_END;
		array[56] = Resources.SOUND_GOOD;
		array[57] = Resources.SOUND_EXCELLENT;
		array[58] = Resources.SOUND_AWESOME;
		array[59] = Resources.SOUND_SPECTACULAR;
		array[60] = Resources.SOUND_EXTRAORDINARY;
		array[61] = Resources.SOUND_UNBELIEVABLE;
		array[62] = Resources.SOUND_GAME_OVER;
		array[63] = Resources.SOUND_GO;
		array[64] = Resources.SOUND_LEVEL_COMPLETE;
		array[65] = Resources.SOUND_NO_MORE_MOVES;
		array[66] = Resources.SOUND_TIME_UP;
		array[67] = Resources.SOUND_BLAZING_SPEED;
		array[68] = Resources.SOUND_ELECTRO_START;
		array[69] = Resources.SOUND_ELECTRO_PATH;
		array[70] = Resources.SOUND_ELECTRO_EXPLODE;
		array[71] = Resources.SOUND_BURNING_SPEED;
		array[72] = Resources.SOUND_LIGHTNING_MAKE;
		array[73] = Resources.SOUND_MULTIPLIER_MAKE;
		array[74] = Resources.SOUND_MULTIPLIER_TRIGGER;
		Resources.gResources = array;
	}

	public static readonly Image LOAD_LOGO_IMAGE_DATA;

	public static Image IMAGE_HYPERSPACE;

	public static Image IMAGE_HYPERSPACE_INITIAL;

	public static Image IMAGE_EXTRAS;

	public static Image IMAGE_ADDITIVES;

	public static Image IMAGE_BOARD;

	public static Image IMAGE_UI;

	public static Image IMAGE_MAINMENU_BKG;

	public static Image IMAGE_MAINMENU_BKG_LAYER1;

	public static Image IMAGE_MAINMENU_BKG_LAYER2;

	public static Image IMAGE_MAINMENU_BKG_LAYER3;

	public static int SOUND_BAD;

	public static int SOUND_GEM_HIT;

	public static int SOUND_CLICK;

	public static int SOUND_SELECT;

	public static int SOUND_MULTISHOT;

	public static int SOUND_MAIN_GAME_START;

	public static int SOUND_MAIN_MOUSEOVER;

	public static int SOUND_BOMB_EXPLODE;

	public static int SOUND_TRANSFER_BIG;

	public static int SOUND_TRANSFER;

	public static int SOUND_COMBO_1;

	public static int SOUND_COMBO_2;

	public static int SOUND_COMBO_3;

	public static int SOUND_COMBO_4;

	public static int SOUND_COMBO_5;

	public static int SOUND_COMBO_6;

	public static int SOUND_SPEED_1;

	public static int SOUND_SPEED_2;

	public static int SOUND_SPEED_3;

	public static int SOUND_SPEED_4;

	public static int SOUND_SPEED_5;

	public static int SOUND_SPEED_6;

	public static int SOUND_SPEED_7;

	public static int SOUND_SPEED_8;

	public static int SOUND_SPEED_9;

	public static int SOUND_HYPERGEM_CREATE;

	public static int SOUND_HYPERGEM_DESTROYED;

	public static int SOUND_EXPLODE;

	public static int SOUND_WARNING;

	public static int SOUND_WHIRLPOOL_START;

	public static int SOUND_WHIRLPOOL_END;

	public static int SOUND_GOOD;

	public static int SOUND_EXCELLENT;

	public static int SOUND_AWESOME;

	public static int SOUND_SPECTACULAR;

	public static int SOUND_EXTRAORDINARY;

	public static int SOUND_UNBELIEVABLE;

	public static int SOUND_GAME_OVER;

	public static int SOUND_GO;

	public static int SOUND_LEVEL_COMPLETE;

	public static int SOUND_NO_MORE_MOVES;

	public static int SOUND_TIME_UP;

	public static int SOUND_BLAZING_SPEED;

	public static int SOUND_ELECTRO_START;

	public static int SOUND_ELECTRO_PATH;

	public static int SOUND_ELECTRO_EXPLODE;

	public static int SOUND_BURNING_SPEED;

	public static int SOUND_LIGHTNING_MAKE;

	public static int SOUND_MULTIPLIER_MAKE;

	public static int SOUND_MULTIPLIER_TRIGGER;

	public static Font FONT_LOCALIZED_TEST;

	public static Font FONT_BUTTON;

	public static Font FONT_HUGE;

	public static Font FONT_TINY_TEXT;

	public static Font FONT_TEXT;

	public static Font FONT_BIG_TEXT;

	public static Font FONT_LEADERBOARD;

	public static Font FONT_MULTIPLIER_GEMS;

	public static Font FONT_FLOAT_POINTS;

	public static Font FONT_SCORE;

	public static Font FONT_TIMER;

	public static Font FONT_BOMB;

	public static Font FONT_ACHIEVEMENT_NAME;

	public static Font FONT_HEADING;

	public static Font FONT_SPEED_BONUS;

	public static bool gNeedRecalcVariableToIdMap = false;

	public static object[] gResources;

	public enum ResourceId
	{
		IMAGE_HYPERSPACE_ID,
		IMAGE_HYPERSPACE_INITIAL_ID,
		IMAGE_EXTRAS_ID,
		IMAGE_ADDITIVES_ID,
		IMAGE_BOARD_ID,
		IMAGE_UI_ID,
		IMAGE_MAINMENU_BKG_ID,
		IMAGE_MAINMENU_BKG_LAYER1_ID,
		IMAGE_MAINMENU_BKG_LAYER2_ID,
		IMAGE_MAINMENU_BKG_LAYER3_ID,
		SOUND_BAD_ID,
		SOUND_GEM_HIT_ID,
		SOUND_CLICK_ID,
		SOUND_SELECT_ID,
		SOUND_MULTISHOT_ID,
		SOUND_MAIN_GAME_START_ID,
		SOUND_MAIN_MOUSEOVER_ID,
		SOUND_BOMB_EXPLODE_ID,
		SOUND_TRANSFER_BIG_ID,
		SOUND_TRANSFER_ID,
		SOUND_COMBO_1_ID,
		SOUND_COMBO_2_ID,
		SOUND_COMBO_3_ID,
		SOUND_COMBO_4_ID,
		SOUND_COMBO_5_ID,
		SOUND_COMBO_6_ID,
		SOUND_SPEED_1_ID,
		SOUND_SPEED_2_ID,
		SOUND_SPEED_3_ID,
		SOUND_SPEED_4_ID,
		SOUND_SPEED_5_ID,
		SOUND_SPEED_6_ID,
		SOUND_SPEED_7_ID,
		SOUND_SPEED_8_ID,
		SOUND_SPEED_9_ID,
		SOUND_HYPERGEM_CREATE_ID,
		SOUND_HYPERGEM_DESTROYED_ID,
		SOUND_EXPLODE_ID,
		SOUND_WARNING_ID,
		SOUND_WHIRLPOOL_START_ID,
		SOUND_WHIRLPOOL_END_ID,
		SOUND_GOOD_ID,
		SOUND_EXCELLENT_ID,
		SOUND_AWESOME_ID,
		SOUND_SPECTACULAR_ID,
		SOUND_EXTRAORDINARY_ID,
		SOUND_UNBELIEVABLE_ID,
		SOUND_GAME_OVER_ID,
		SOUND_GO_ID,
		SOUND_LEVEL_COMPLETE_ID,
		SOUND_NO_MORE_MOVES_ID,
		SOUND_TIME_UP_ID,
		SOUND_BLAZING_SPEED_ID,
		SOUND_ELECTRO_START_ID,
		SOUND_ELECTRO_PATH_ID,
		SOUND_ELECTRO_EXPLODE_ID,
		SOUND_BURNING_SPEED_ID,
		SOUND_LIGHTNING_MAKE_ID,
		SOUND_MULTIPLIER_MAKE_ID,
		SOUND_MULTIPLIER_TRIGGER_ID,
		FONT_LOCALIZED_TEST_ID,
		FONT_BUTTON_ID,
		FONT_HUGE_ID,
		FONT_TINY_TEXT_ID,
		FONT_TEXT_ID,
		FONT_BIG_TEXT_ID,
		FONT_LEADERBOARD_ID,
		FONT_MULTIPLIER_GEMS_ID,
		FONT_FLOAT_POINTS_ID,
		FONT_SCORE_ID,
		FONT_TIMER_ID,
		FONT_BOMB_ID,
		FONT_ACHIEVEMENT_NAME_ID,
		FONT_HEADING_ID,
		FONT_SPEED_BONUS_ID,
		RESOURCE_ID_MAX
	}
}
