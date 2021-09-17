using System;
using Sexy;

public class AtlasResources
{
	public void ExtractResources()
	{
		this.UnpackBoardAtlasImages();
		this.UnpackUIAtlasImages();
		this.UnpackExtrasAtlasImages();
		this.UnpackAdditivesAtlasImages();
	}

	public virtual void UnpackBoardAtlasImages()
	{
	}

	public virtual void UnpackUIAtlasImages()
	{
	}

	public virtual void UnpackExtrasAtlasImages()
	{
	}

	public virtual void UnpackAdditivesAtlasImages()
	{
	}

	public static Image GetImageInAtlasById(int theId)
	{
		switch (theId)
		{
		case 10001:
			return AtlasResources.IMAGE_TUNNEL_END;
		case 10002:
			return AtlasResources.IMAGE_FIRE_RING;
		case 10003:
			return AtlasResources.IMAGE_BLACK_HOLE;
		case 10004:
			return AtlasResources.IMAGE_BLACK_HOLE_COVER;
		case 10005:
			return AtlasResources.IMAGE_MAINMENU_LIVE_LOGO;
		case 10006:
			return AtlasResources.IMAGE_PILL_CENTRE1;
		case 10007:
			return AtlasResources.IMAGE_PILL_CENTRE2;
		case 10008:
			return AtlasResources.IMAGE_PILL_ADDITIVE;
		case 10009:
			return AtlasResources.IMAGE_PILL_RING;
		case 10010:
			return AtlasResources.IMAGE_PILL_TOP;
		case 10011:
			return AtlasResources.IMAGE_PILL_BOT;
		case 10012:
			return AtlasResources.IMAGE_PILL_SMALL;
		case 10013:
			return AtlasResources.IMAGE_PILL_SMALL_CENTER1;
		case 10014:
			return AtlasResources.IMAGE_PILL_SMALL_CENTER2;
		case 10015:
			return AtlasResources.IMAGE_PILL_SMALL_ADDITIVE;
		case 10016:
			return AtlasResources.IMAGE_SUB_BORDER;
		case 10017:
			return AtlasResources.IMAGE_SUB_HEADER_RIGHT_MID;
		case 10018:
			return AtlasResources.IMAGE_SUB_HEADER_LEFT_MID;
		case 10019:
			return AtlasResources.IMAGE_SUB_HEADER_RIGHT;
		case 10020:
			return AtlasResources.IMAGE_SUB_HEADER_LEFT;
		case 10021:
			return AtlasResources.IMAGE_SUB_HEADER_MID;
		case 10022:
			return AtlasResources.IMAGE_RANK_HEADER;
		case 10023:
			return AtlasResources.IMAGE_POPCAP_BUTTON;
		case 10024:
			return AtlasResources.IMAGE_OPTIONS_BUTTON;
		case 10025:
			return AtlasResources.IMAGE_LITTLE_GREEN_PILL;
		case 10026:
			return AtlasResources.IMAGE_LITTLE_BLUE_PILL;
		case 10027:
			return AtlasResources.IMAGE_LITTLE_RED_PILL;
		case 10028:
			return AtlasResources.IMAGE_HELP_INDICATOR_ARROW;
		case 10029:
			return AtlasResources.IMAGE_HELP_ARROW;
		case 10030:
			return AtlasResources.IMAGE_GRID;
		case 10031:
			return AtlasResources.IMAGE_GEM0;
		case 10032:
			return AtlasResources.IMAGE_GEM1;
		case 10033:
			return AtlasResources.IMAGE_GEM2;
		case 10034:
			return AtlasResources.IMAGE_GEM3;
		case 10035:
			return AtlasResources.IMAGE_GEM4;
		case 10036:
			return AtlasResources.IMAGE_GEM5;
		case 10037:
			return AtlasResources.IMAGE_GEM6;
		case 10038:
			return AtlasResources.IMAGE_FLAME_GEM0;
		case 10039:
			return AtlasResources.IMAGE_FLAME_GEM1;
		case 10040:
			return AtlasResources.IMAGE_FLAME_GEM2;
		case 10041:
			return AtlasResources.IMAGE_FLAME_GEM3;
		case 10042:
			return AtlasResources.IMAGE_FLAME_GEM4;
		case 10043:
			return AtlasResources.IMAGE_FLAME_GEM5;
		case 10044:
			return AtlasResources.IMAGE_FLAME_GEM6;
		case 10045:
			return AtlasResources.IMAGE_HYPERCUBE_LOWER;
		case 10046:
			return AtlasResources.IMAGE_HYPERCUBE_ADD;
		case 10047:
			return AtlasResources.IMAGE_STAR_GEM0;
		case 10048:
			return AtlasResources.IMAGE_STAR_GEM1;
		case 10049:
			return AtlasResources.IMAGE_STAR_GEM2;
		case 10050:
			return AtlasResources.IMAGE_STAR_GEM3;
		case 10051:
			return AtlasResources.IMAGE_STAR_GEM4;
		case 10052:
			return AtlasResources.IMAGE_STAR_GEM5;
		case 10053:
			return AtlasResources.IMAGE_STAR_GEM6;
		case 10054:
			return AtlasResources.IMAGE_STAR_FRONT;
		case 10055:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_RING;
		case 10056:
			return AtlasResources.IMAGE_GEM_MULTIPLIER0;
		case 10057:
			return AtlasResources.IMAGE_GEM_MULTIPLIER1;
		case 10058:
			return AtlasResources.IMAGE_GEM_MULTIPLIER2;
		case 10059:
			return AtlasResources.IMAGE_GEM_MULTIPLIER3;
		case 10060:
			return AtlasResources.IMAGE_GEM_MULTIPLIER4;
		case 10061:
			return AtlasResources.IMAGE_GEM_MULTIPLIER5;
		case 10062:
			return AtlasResources.IMAGE_GEM_MULTIPLIER6;
		case 10063:
			return AtlasResources.IMAGE_GEM_MULTIPLIER7;
		case 10064:
			return AtlasResources.IMAGE_MULTIPLIER2X;
		case 10065:
			return AtlasResources.IMAGE_MULTIPLIER3X;
		case 10066:
			return AtlasResources.IMAGE_MULTIPLIER4X;
		case 10067:
			return AtlasResources.IMAGE_MULTIPLIER5X;
		case 10068:
			return AtlasResources.IMAGE_MULTIPLIER6X;
		case 10069:
			return AtlasResources.IMAGE_MULTIPLIER7X;
		case 10070:
			return AtlasResources.IMAGE_MULTIPLIER8X;
		case 10071:
			return AtlasResources.IMAGE_BOARD_SPEED_BONUS_BG;
		case 10072:
			return AtlasResources.IMAGE_SELECT;
		case 10073:
			return AtlasResources.IMAGE_SPARKLE;
		case 10074:
			return AtlasResources.IMAGE_EXPLOSION;
		case 10075:
			return AtlasResources.IMAGE_SHARD;
		case 10076:
			return AtlasResources.IMAGE_BIGSTAR;
		case 10077:
			return AtlasResources.IMAGE_GEM_LIGHTING;
		case 10078:
			return AtlasResources.IMAGE_POWER_GLOW;
		case 10079:
			return AtlasResources.IMAGE_ARROW_UP;
		case 10080:
			return AtlasResources.IMAGE_ARROW_RIGHT;
		case 10081:
			return AtlasResources.IMAGE_ALERT_BG;
		case 10082:
			return AtlasResources.IMAGE_ACTION_SHEET_BG;
		case 10083:
			return AtlasResources.IMAGE_FRAME_CHIP_HORIZ;
		case 10084:
			return AtlasResources.IMAGE_FRAME_CHIP_VERT;
		case 10085:
			return AtlasResources.IMAGE_FRAME_CHIP_WARN_HORIZ;
		case 10086:
			return AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT;
		case 10087:
			return AtlasResources.IMAGE_CHECKBOX_CHECKED;
		case 10088:
			return AtlasResources.IMAGE_CHECKBOX_UNCHECKED;
		case 10089:
			return AtlasResources.IMAGE_SLIDER_BALL_RING;
		case 10090:
			return AtlasResources.IMAGE_SLIDER_BALL;
		case 10091:
			return AtlasResources.IMAGE_SLIDER_BALL_OLD;
		case 10092:
			return AtlasResources.IMAGE_SLIDER_FILL;
		case 10093:
			return AtlasResources.IMAGE_SLIDER_FILL_OLD;
		case 10094:
			return AtlasResources.IMAGE_SLIDER_RING;
		case 10095:
			return AtlasResources.IMAGE_SLIDER_RING_OLD;
		case 10096:
			return AtlasResources.IMAGE_PAUSE_BUTTON_RING;
		case 10097:
			return AtlasResources.IMAGE_PAUSE_BUTTON_PLAY;
		case 10098:
			return AtlasResources.IMAGE_PAUSE_BUTTON_PAUSE;
		case 10099:
			return AtlasResources.IMAGE_INFINITY;
		case 10100:
			return AtlasResources.IMAGE_SCROLL_INDICATOR;
		case 10101:
			return AtlasResources.IMAGE_MORE_GAMES_NEW_CONTENT;
		case 10102:
			return AtlasResources.IMAGE_MORE_GAMES_LIST_TOP_OV;
		case 10103:
			return AtlasResources.IMAGE_MORE_GAMES_LIST_BOTTOM_OV;
		case 10104:
			return AtlasResources.IMAGE_LEADERBOARD_BG1;
		case 10105:
			return AtlasResources.IMAGE_LEADERBOARD_BG2;
		case 10106:
			return AtlasResources.IMAGE_LEADERBOARD_BG3;
		case 10107:
			return AtlasResources.IMAGE_LEADERBOARD_NEW_HIGH_SCORE;
		case 10108:
			return AtlasResources.IMAGE_LEADERBOARD_NEW_TOURNAMENT;
		case 10109:
			return AtlasResources.IMAGE_LEADERBOARD_NEWS;
		case 10110:
			return AtlasResources.IMAGE_LEADERBOARD_YOUR_SCORE;
		case 10111:
			return AtlasResources.IMAGE_LEADERBOARD_LIST_TOP_OV;
		case 10112:
			return AtlasResources.IMAGE_LEADERBOARD_LIST_BOTTOM_OV;
		case 10113:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_0;
		case 10114:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_1;
		case 10115:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_2;
		case 10116:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_3;
		case 10117:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_4;
		case 10118:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_5;
		case 10119:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_6;
		case 10120:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_7;
		case 10121:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_8;
		case 10122:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_9;
		case 10123:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_10;
		case 10124:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_11;
		case 10125:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_12;
		case 10126:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_13;
		case 10127:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_14;
		case 10128:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_15;
		case 10129:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_16;
		case 10130:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_17;
		case 10131:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_18;
		case 10132:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_19;
		case 10133:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_20;
		case 10134:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_0_GOLD;
		case 10135:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_1_GOLD;
		case 10136:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_2_GOLD;
		case 10137:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_3_GOLD;
		case 10138:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_4_GOLD;
		case 10139:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_5_GOLD;
		case 10140:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_6_GOLD;
		case 10141:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_7_GOLD;
		case 10142:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_8_GOLD;
		case 10143:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_9_GOLD;
		case 10144:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_10_GOLD;
		case 10145:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_11_GOLD;
		case 10146:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_12_GOLD;
		case 10147:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_13_GOLD;
		case 10148:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_14_GOLD;
		case 10149:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_15_GOLD;
		case 10150:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_16_GOLD;
		case 10151:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_17_GOLD;
		case 10152:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_18_GOLD;
		case 10153:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_19_GOLD;
		case 10154:
			return AtlasResources.IMAGE_LEADERBOARD_MEDAL_20_GOLD;
		case 10155:
			return AtlasResources.IMAGE_FRAME_BOTTOM;
		case 10156:
			return AtlasResources.IMAGE_FRAME_TOP;
		case 10157:
			return AtlasResources.IMAGE_FRAME_TOP_BACK;
		case 10158:
			return AtlasResources.IMAGE_WIDGET_BOTTOM;
		case 10159:
			return AtlasResources.IMAGE_WIDGET_BOTTOM_DOUBLE;
		case 10160:
			return AtlasResources.IMAGE_WIDGET_TOP;
		case 10161:
			return AtlasResources.IMAGE_PAUSE_BUTTON;
		case 10162:
			return AtlasResources.IMAGE_PLAY_BUTTON;
		case 10163:
			return AtlasResources.IMAGE_HINT_BUTTON;
		case 10164:
			return AtlasResources.IMAGE_EMPTY_BUTTON;
		case 10165:
			return AtlasResources.IMAGE_PROGRESSBAR_PARTICLE_BACK;
		case 10166:
			return AtlasResources.IMAGE_PLAYER_ICON;
		case 10167:
			return AtlasResources.IMAGE_PLAYERS_ICON;
		case 10168:
			return AtlasResources.IMAGE_LOCK;
		case 10169:
			return AtlasResources.IMAGE_DOUBLE_BUTTON;
		case 10170:
			return AtlasResources.IMAGE_PARTICLE_1;
		case 10171:
			return AtlasResources.IMAGE_PARTICLE_2;
		case 10172:
			return AtlasResources.IMAGE_PARTICLE_3;
		case 10173:
			return AtlasResources.IMAGE_PARTICLE_4;
		case 10174:
			return AtlasResources.IMAGE_LIGHTNING;
		case 10175:
			return AtlasResources.IMAGE_LIGHTNING_GLOW;
		case 10176:
			return AtlasResources.IMAGE_GAMERTAG_UNKNOWN;
		case 10177:
			return AtlasResources.IMAGE_LOADING_RING;
		case 10178:
			return AtlasResources.IMAGE_UPSELL_SCREEN_1;
		case 10179:
			return AtlasResources.IMAGE_UPSELL_SCREEN_2;
		case 10180:
			return AtlasResources.IMAGE_UPSELL_SCREEN_3;
		case 10181:
			return AtlasResources.IMAGE_UPSELL_SCREEN_4;
		case 10182:
			return AtlasResources.IMAGE_UPSELL_SCREEN_5;
		case 10183:
			return AtlasResources.IMAGE_ICON;
		default:
			return Resources.GetImageById(theId);
		}
	}

	public static int GetAtlasIdByStringId(string theStringId)
	{
		AtlasResources.AtlasStringTable[] array = new AtlasResources.AtlasStringTable[]
		{
			new AtlasResources.AtlasStringTable("IMAGE_GRID", 10030),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM2", 10049),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM6", 10053),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM5", 10052),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM4", 10051),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM3", 10050),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM1", 10048),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_GEM0", 10047),
			new AtlasResources.AtlasStringTable("IMAGE_HYPERCUBE_LOWER", 10045),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM2", 10040),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM6", 10044),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM5", 10043),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM4", 10042),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM3", 10041),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM1", 10039),
			new AtlasResources.AtlasStringTable("IMAGE_FLAME_GEM0", 10038),
			new AtlasResources.AtlasStringTable("IMAGE_GEM0", 10031),
			new AtlasResources.AtlasStringTable("IMAGE_GEM1", 10032),
			new AtlasResources.AtlasStringTable("IMAGE_GEM2", 10033),
			new AtlasResources.AtlasStringTable("IMAGE_GEM3", 10034),
			new AtlasResources.AtlasStringTable("IMAGE_GEM4", 10035),
			new AtlasResources.AtlasStringTable("IMAGE_GEM5", 10036),
			new AtlasResources.AtlasStringTable("IMAGE_GEM6", 10037),
			new AtlasResources.AtlasStringTable("IMAGE_LIGHTNING", 10174),
			new AtlasResources.AtlasStringTable("IMAGE_WIDGET_TOP", 10160),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_TOP", 10156),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_TOP_BACK", 10157),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_BOTTOM", 10155),
			new AtlasResources.AtlasStringTable("IMAGE_BOARD_SPEED_BONUS_BG", 10071),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER2", 10058),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER6", 10062),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER5", 10061),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER4", 10060),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER7", 10063),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER3", 10059),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER1", 10057),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_MULTIPLIER0", 10056),
			new AtlasResources.AtlasStringTable("IMAGE_LIGHTNING_GLOW", 10175),
			new AtlasResources.AtlasStringTable("IMAGE_PARTICLE_1", 10170),
			new AtlasResources.AtlasStringTable("IMAGE_PARTICLE_2", 10171),
			new AtlasResources.AtlasStringTable("IMAGE_PARTICLE_3", 10172),
			new AtlasResources.AtlasStringTable("IMAGE_PARTICLE_4", 10173),
			new AtlasResources.AtlasStringTable("IMAGE_HELP_ARROW", 10029),
			new AtlasResources.AtlasStringTable("IMAGE_SELECT", 10072),
			new AtlasResources.AtlasStringTable("IMAGE_INFINITY", 10099),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER2X", 10064),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER3X", 10065),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER4X", 10066),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER5X", 10067),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER6X", 10068),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER7X", 10069),
			new AtlasResources.AtlasStringTable("IMAGE_MULTIPLIER8X", 10070),
			new AtlasResources.AtlasStringTable("IMAGE_PROGRESSBAR_PARTICLE_BACK", 10165),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_CHIP_WARN_VERT", 10086),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_CHIP_WARN_HORIZ", 10085),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_CHIP_HORIZ", 10083),
			new AtlasResources.AtlasStringTable("IMAGE_FRAME_CHIP_VERT", 10084),
			new AtlasResources.AtlasStringTable("IMAGE_FIRE_RING", 10002),
			new AtlasResources.AtlasStringTable("IMAGE_UPSELL_SCREEN_1", 10178),
			new AtlasResources.AtlasStringTable("IMAGE_UPSELL_SCREEN_2", 10179),
			new AtlasResources.AtlasStringTable("IMAGE_UPSELL_SCREEN_3", 10180),
			new AtlasResources.AtlasStringTable("IMAGE_UPSELL_SCREEN_4", 10181),
			new AtlasResources.AtlasStringTable("IMAGE_UPSELL_SCREEN_5", 10182),
			new AtlasResources.AtlasStringTable("IMAGE_MAINMENU_LIVE_LOGO", 10005),
			new AtlasResources.AtlasStringTable("IMAGE_SUB_BORDER", 10016),
			new AtlasResources.AtlasStringTable("IMAGE_ACTION_SHEET_BG", 10082),
			new AtlasResources.AtlasStringTable("IMAGE_WIDGET_BOTTOM_DOUBLE", 10159),
			new AtlasResources.AtlasStringTable("IMAGE_WIDGET_BOTTOM", 10158),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_RING", 10009),
			new AtlasResources.AtlasStringTable("IMAGE_BLACK_HOLE_COVER", 10004),
			new AtlasResources.AtlasStringTable("IMAGE_TUNNEL_END", 10001),
			new AtlasResources.AtlasStringTable("IMAGE_HINT_BUTTON", 10163),
			new AtlasResources.AtlasStringTable("IMAGE_DOUBLE_BUTTON", 10169),
			new AtlasResources.AtlasStringTable("IMAGE_ALERT_BG", 10081),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_ADDITIVE", 10008),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_CENTRE1", 10006),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_CENTRE2", 10007),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_SMALL", 10012),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_RING", 10055),
			new AtlasResources.AtlasStringTable("IMAGE_RANK_HEADER", 10022),
			new AtlasResources.AtlasStringTable("IMAGE_SUB_HEADER_MID", 10021),
			new AtlasResources.AtlasStringTable("IMAGE_LITTLE_BLUE_PILL", 10026),
			new AtlasResources.AtlasStringTable("IMAGE_LITTLE_GREEN_PILL", 10025),
			new AtlasResources.AtlasStringTable("IMAGE_LITTLE_RED_PILL", 10027),
			new AtlasResources.AtlasStringTable("IMAGE_PAUSE_BUTTON_RING", 10096),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_LIST_TOP_OV", 10111),
			new AtlasResources.AtlasStringTable("IMAGE_LOADING_RING", 10177),
			new AtlasResources.AtlasStringTable("IMAGE_EMPTY_BUTTON", 10164),
			new AtlasResources.AtlasStringTable("IMAGE_PAUSE_BUTTON", 10161),
			new AtlasResources.AtlasStringTable("IMAGE_PLAY_BUTTON", 10162),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_NEW_TOURNAMENT", 10108),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_YOUR_SCORE", 10110),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_TOP", 10010),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_NEWS", 10109),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_LIST_BOTTOM_OV", 10112),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_NEW_HIGH_SCORE", 10107),
			new AtlasResources.AtlasStringTable("IMAGE_PAUSE_BUTTON_PAUSE", 10098),
			new AtlasResources.AtlasStringTable("IMAGE_PAUSE_BUTTON_PLAY", 10097),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_BALL_RING", 10089),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_SMALL_ADDITIVE", 10015),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_SMALL_CENTER1", 10013),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_SMALL_CENTER2", 10014),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_0", 10113),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_0_GOLD", 10134),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_1", 10114),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_10", 10123),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_10_GOLD", 10144),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_11", 10124),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_11_GOLD", 10145),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_12", 10125),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_12_GOLD", 10146),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_13", 10126),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_13_GOLD", 10147),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_14", 10127),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_14_GOLD", 10148),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_15", 10128),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_15_GOLD", 10149),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_16", 10129),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_16_GOLD", 10150),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_17", 10130),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_17_GOLD", 10151),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_18", 10131),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_18_GOLD", 10152),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_19", 10132),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_19_GOLD", 10153),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_1_GOLD", 10135),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_2", 10115),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_20", 10133),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_20_GOLD", 10154),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_2_GOLD", 10136),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_3", 10116),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_3_GOLD", 10137),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_4", 10117),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_4_GOLD", 10138),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_5", 10118),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_5_GOLD", 10139),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_6", 10119),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_6_GOLD", 10140),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_7", 10120),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_7_GOLD", 10141),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_8", 10121),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_8_GOLD", 10142),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_9", 10122),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_MEDAL_9_GOLD", 10143),
			new AtlasResources.AtlasStringTable("IMAGE_PILL_BOT", 10011),
			new AtlasResources.AtlasStringTable("IMAGE_OPTIONS_BUTTON", 10024),
			new AtlasResources.AtlasStringTable("IMAGE_POPCAP_BUTTON", 10023),
			new AtlasResources.AtlasStringTable("IMAGE_GAMERTAG_UNKNOWN", 10176),
			new AtlasResources.AtlasStringTable("IMAGE_ICON", 10183),
			new AtlasResources.AtlasStringTable("IMAGE_SUB_HEADER_RIGHT", 10019),
			new AtlasResources.AtlasStringTable("IMAGE_SUB_HEADER_LEFT", 10020),
			new AtlasResources.AtlasStringTable("IMAGE_LOCK", 10168),
			new AtlasResources.AtlasStringTable("IMAGE_CHECKBOX_UNCHECKED", 10088),
			new AtlasResources.AtlasStringTable("IMAGE_CHECKBOX_CHECKED", 10087),
			new AtlasResources.AtlasStringTable("IMAGE_MORE_GAMES_NEW_CONTENT", 10101),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_RING", 10094),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_BALL_OLD", 10091),
			new AtlasResources.AtlasStringTable("IMAGE_PLAYERS_ICON", 10167),
			new AtlasResources.AtlasStringTable("IMAGE_ARROW_UP", 10079),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_BALL", 10090),
			new AtlasResources.AtlasStringTable("IMAGE_ARROW_RIGHT", 10080),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_BG1", 10104),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_BG2", 10105),
			new AtlasResources.AtlasStringTable("IMAGE_LEADERBOARD_BG3", 10106),
			new AtlasResources.AtlasStringTable("IMAGE_PLAYER_ICON", 10166),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_FILL", 10092),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_RING_OLD", 10095),
			new AtlasResources.AtlasStringTable("IMAGE_HELP_INDICATOR_ARROW", 10028),
			new AtlasResources.AtlasStringTable("IMAGE_MORE_GAMES_LIST_BOTTOM_OV", 10103),
			new AtlasResources.AtlasStringTable("IMAGE_MORE_GAMES_LIST_TOP_OV", 10102),
			new AtlasResources.AtlasStringTable("IMAGE_SCROLL_INDICATOR", 10100),
			new AtlasResources.AtlasStringTable("IMAGE_SUB_HEADER_LEFT_MID", 10018),
			new AtlasResources.AtlasStringTable("IMAGE_SUB_HEADER_RIGHT_MID", 10017),
			new AtlasResources.AtlasStringTable("IMAGE_SLIDER_FILL_OLD", 10093),
			new AtlasResources.AtlasStringTable("IMAGE_BLACK_HOLE", 10003),
			new AtlasResources.AtlasStringTable("IMAGE_POWER_GLOW", 10078),
			new AtlasResources.AtlasStringTable("IMAGE_GEM_LIGHTING", 10077),
			new AtlasResources.AtlasStringTable("IMAGE_STAR_FRONT", 10054),
			new AtlasResources.AtlasStringTable("IMAGE_HYPERCUBE_ADD", 10046),
			new AtlasResources.AtlasStringTable("IMAGE_EXPLOSION", 10074),
			new AtlasResources.AtlasStringTable("IMAGE_SHARD", 10075),
			new AtlasResources.AtlasStringTable("IMAGE_SPARKLE", 10073),
			new AtlasResources.AtlasStringTable("IMAGE_BIGSTAR", 10076)
		};
		int num = array.Length - 1;
		int i = 0;
		while (i <= num)
		{
			int num2 = (i + num) / 2;
			int num3 = string.Compare(theStringId, array[num2].mStringId);
			if (num3 == 0)
			{
				return array[num2].mImageId;
			}
			if (num3 < 0)
			{
				num = num2 - 1;
			}
			else
			{
				i = num2 + 1;
			}
		}
		return (int)Resources.GetIdByStringId(theStringId);
	}

	public static int GetIdByImageInAtlas(Image theImage)
	{
		if (theImage == AtlasResources.IMAGE_GRID)
		{
			return 10030;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM2)
		{
			return 10049;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM6)
		{
			return 10053;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM5)
		{
			return 10052;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM4)
		{
			return 10051;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM3)
		{
			return 10050;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM1)
		{
			return 10048;
		}
		if (theImage == AtlasResources.IMAGE_STAR_GEM0)
		{
			return 10047;
		}
		if (theImage == AtlasResources.IMAGE_HYPERCUBE_LOWER)
		{
			return 10045;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM2)
		{
			return 10040;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM6)
		{
			return 10044;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM5)
		{
			return 10043;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM4)
		{
			return 10042;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM3)
		{
			return 10041;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM1)
		{
			return 10039;
		}
		if (theImage == AtlasResources.IMAGE_FLAME_GEM0)
		{
			return 10038;
		}
		if (theImage == AtlasResources.IMAGE_GEM0)
		{
			return 10031;
		}
		if (theImage == AtlasResources.IMAGE_GEM1)
		{
			return 10032;
		}
		if (theImage == AtlasResources.IMAGE_GEM2)
		{
			return 10033;
		}
		if (theImage == AtlasResources.IMAGE_GEM3)
		{
			return 10034;
		}
		if (theImage == AtlasResources.IMAGE_GEM4)
		{
			return 10035;
		}
		if (theImage == AtlasResources.IMAGE_GEM5)
		{
			return 10036;
		}
		if (theImage == AtlasResources.IMAGE_GEM6)
		{
			return 10037;
		}
		if (theImage == AtlasResources.IMAGE_LIGHTNING)
		{
			return 10174;
		}
		if (theImage == AtlasResources.IMAGE_WIDGET_TOP)
		{
			return 10160;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_TOP)
		{
			return 10156;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_TOP_BACK)
		{
			return 10157;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_BOTTOM)
		{
			return 10155;
		}
		if (theImage == AtlasResources.IMAGE_BOARD_SPEED_BONUS_BG)
		{
			return 10071;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER2)
		{
			return 10058;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER6)
		{
			return 10062;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER5)
		{
			return 10061;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER4)
		{
			return 10060;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER7)
		{
			return 10063;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER3)
		{
			return 10059;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER1)
		{
			return 10057;
		}
		if (theImage == AtlasResources.IMAGE_GEM_MULTIPLIER0)
		{
			return 10056;
		}
		if (theImage == AtlasResources.IMAGE_LIGHTNING_GLOW)
		{
			return 10175;
		}
		if (theImage == AtlasResources.IMAGE_PARTICLE_1)
		{
			return 10170;
		}
		if (theImage == AtlasResources.IMAGE_PARTICLE_2)
		{
			return 10171;
		}
		if (theImage == AtlasResources.IMAGE_PARTICLE_3)
		{
			return 10172;
		}
		if (theImage == AtlasResources.IMAGE_PARTICLE_4)
		{
			return 10173;
		}
		if (theImage == AtlasResources.IMAGE_HELP_ARROW)
		{
			return 10029;
		}
		if (theImage == AtlasResources.IMAGE_SELECT)
		{
			return 10072;
		}
		if (theImage == AtlasResources.IMAGE_INFINITY)
		{
			return 10099;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER2X)
		{
			return 10064;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER3X)
		{
			return 10065;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER4X)
		{
			return 10066;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER5X)
		{
			return 10067;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER6X)
		{
			return 10068;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER7X)
		{
			return 10069;
		}
		if (theImage == AtlasResources.IMAGE_MULTIPLIER8X)
		{
			return 10070;
		}
		if (theImage == AtlasResources.IMAGE_PROGRESSBAR_PARTICLE_BACK)
		{
			return 10165;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT)
		{
			return 10086;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_CHIP_WARN_HORIZ)
		{
			return 10085;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_CHIP_HORIZ)
		{
			return 10083;
		}
		if (theImage == AtlasResources.IMAGE_FRAME_CHIP_VERT)
		{
			return 10084;
		}
		if (theImage == AtlasResources.IMAGE_FIRE_RING)
		{
			return 10002;
		}
		if (theImage == AtlasResources.IMAGE_UPSELL_SCREEN_1)
		{
			return 10178;
		}
		if (theImage == AtlasResources.IMAGE_UPSELL_SCREEN_2)
		{
			return 10179;
		}
		if (theImage == AtlasResources.IMAGE_UPSELL_SCREEN_3)
		{
			return 10180;
		}
		if (theImage == AtlasResources.IMAGE_UPSELL_SCREEN_4)
		{
			return 10181;
		}
		if (theImage == AtlasResources.IMAGE_UPSELL_SCREEN_5)
		{
			return 10182;
		}
		if (theImage == AtlasResources.IMAGE_MAINMENU_LIVE_LOGO)
		{
			return 10005;
		}
		if (theImage == AtlasResources.IMAGE_SUB_BORDER)
		{
			return 10016;
		}
		if (theImage == AtlasResources.IMAGE_ACTION_SHEET_BG)
		{
			return 10082;
		}
		if (theImage == AtlasResources.IMAGE_WIDGET_BOTTOM_DOUBLE)
		{
			return 10159;
		}
		if (theImage == AtlasResources.IMAGE_WIDGET_BOTTOM)
		{
			return 10158;
		}
		if (theImage == AtlasResources.IMAGE_PILL_RING)
		{
			return 10009;
		}
		if (theImage == AtlasResources.IMAGE_BLACK_HOLE_COVER)
		{
			return 10004;
		}
		if (theImage == AtlasResources.IMAGE_TUNNEL_END)
		{
			return 10001;
		}
		if (theImage == AtlasResources.IMAGE_HINT_BUTTON)
		{
			return 10163;
		}
		if (theImage == AtlasResources.IMAGE_DOUBLE_BUTTON)
		{
			return 10169;
		}
		if (theImage == AtlasResources.IMAGE_ALERT_BG)
		{
			return 10081;
		}
		if (theImage == AtlasResources.IMAGE_PILL_ADDITIVE)
		{
			return 10008;
		}
		if (theImage == AtlasResources.IMAGE_PILL_CENTRE1)
		{
			return 10006;
		}
		if (theImage == AtlasResources.IMAGE_PILL_CENTRE2)
		{
			return 10007;
		}
		if (theImage == AtlasResources.IMAGE_PILL_SMALL)
		{
			return 10012;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_RING)
		{
			return 10055;
		}
		if (theImage == AtlasResources.IMAGE_RANK_HEADER)
		{
			return 10022;
		}
		if (theImage == AtlasResources.IMAGE_SUB_HEADER_MID)
		{
			return 10021;
		}
		if (theImage == AtlasResources.IMAGE_LITTLE_BLUE_PILL)
		{
			return 10026;
		}
		if (theImage == AtlasResources.IMAGE_LITTLE_GREEN_PILL)
		{
			return 10025;
		}
		if (theImage == AtlasResources.IMAGE_LITTLE_RED_PILL)
		{
			return 10027;
		}
		if (theImage == AtlasResources.IMAGE_PAUSE_BUTTON_RING)
		{
			return 10096;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_LIST_TOP_OV)
		{
			return 10111;
		}
		if (theImage == AtlasResources.IMAGE_LOADING_RING)
		{
			return 10177;
		}
		if (theImage == AtlasResources.IMAGE_EMPTY_BUTTON)
		{
			return 10164;
		}
		if (theImage == AtlasResources.IMAGE_PAUSE_BUTTON)
		{
			return 10161;
		}
		if (theImage == AtlasResources.IMAGE_PLAY_BUTTON)
		{
			return 10162;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_NEW_TOURNAMENT)
		{
			return 10108;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_YOUR_SCORE)
		{
			return 10110;
		}
		if (theImage == AtlasResources.IMAGE_PILL_TOP)
		{
			return 10010;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_NEWS)
		{
			return 10109;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_LIST_BOTTOM_OV)
		{
			return 10112;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_NEW_HIGH_SCORE)
		{
			return 10107;
		}
		if (theImage == AtlasResources.IMAGE_PAUSE_BUTTON_PAUSE)
		{
			return 10098;
		}
		if (theImage == AtlasResources.IMAGE_PAUSE_BUTTON_PLAY)
		{
			return 10097;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_BALL_RING)
		{
			return 10089;
		}
		if (theImage == AtlasResources.IMAGE_PILL_SMALL_ADDITIVE)
		{
			return 10015;
		}
		if (theImage == AtlasResources.IMAGE_PILL_SMALL_CENTER1)
		{
			return 10013;
		}
		if (theImage == AtlasResources.IMAGE_PILL_SMALL_CENTER2)
		{
			return 10014;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_0)
		{
			return 10113;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_0_GOLD)
		{
			return 10134;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_1)
		{
			return 10114;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_10)
		{
			return 10123;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_10_GOLD)
		{
			return 10144;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_11)
		{
			return 10124;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_11_GOLD)
		{
			return 10145;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_12)
		{
			return 10125;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_12_GOLD)
		{
			return 10146;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_13)
		{
			return 10126;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_13_GOLD)
		{
			return 10147;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_14)
		{
			return 10127;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_14_GOLD)
		{
			return 10148;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_15)
		{
			return 10128;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_15_GOLD)
		{
			return 10149;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_16)
		{
			return 10129;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_16_GOLD)
		{
			return 10150;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_17)
		{
			return 10130;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_17_GOLD)
		{
			return 10151;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_18)
		{
			return 10131;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_18_GOLD)
		{
			return 10152;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_19)
		{
			return 10132;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_19_GOLD)
		{
			return 10153;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_1_GOLD)
		{
			return 10135;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_2)
		{
			return 10115;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_20)
		{
			return 10133;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_20_GOLD)
		{
			return 10154;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_2_GOLD)
		{
			return 10136;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_3)
		{
			return 10116;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_3_GOLD)
		{
			return 10137;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_4)
		{
			return 10117;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_4_GOLD)
		{
			return 10138;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_5)
		{
			return 10118;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_5_GOLD)
		{
			return 10139;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_6)
		{
			return 10119;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_6_GOLD)
		{
			return 10140;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_7)
		{
			return 10120;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_7_GOLD)
		{
			return 10141;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_8)
		{
			return 10121;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_8_GOLD)
		{
			return 10142;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_9)
		{
			return 10122;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_MEDAL_9_GOLD)
		{
			return 10143;
		}
		if (theImage == AtlasResources.IMAGE_PILL_BOT)
		{
			return 10011;
		}
		if (theImage == AtlasResources.IMAGE_OPTIONS_BUTTON)
		{
			return 10024;
		}
		if (theImage == AtlasResources.IMAGE_POPCAP_BUTTON)
		{
			return 10023;
		}
		if (theImage == AtlasResources.IMAGE_GAMERTAG_UNKNOWN)
		{
			return 10176;
		}
		if (theImage == AtlasResources.IMAGE_ICON)
		{
			return 10183;
		}
		if (theImage == AtlasResources.IMAGE_SUB_HEADER_RIGHT)
		{
			return 10019;
		}
		if (theImage == AtlasResources.IMAGE_SUB_HEADER_LEFT)
		{
			return 10020;
		}
		if (theImage == AtlasResources.IMAGE_LOCK)
		{
			return 10168;
		}
		if (theImage == AtlasResources.IMAGE_CHECKBOX_UNCHECKED)
		{
			return 10088;
		}
		if (theImage == AtlasResources.IMAGE_CHECKBOX_CHECKED)
		{
			return 10087;
		}
		if (theImage == AtlasResources.IMAGE_MORE_GAMES_NEW_CONTENT)
		{
			return 10101;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_RING)
		{
			return 10094;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_BALL_OLD)
		{
			return 10091;
		}
		if (theImage == AtlasResources.IMAGE_PLAYERS_ICON)
		{
			return 10167;
		}
		if (theImage == AtlasResources.IMAGE_ARROW_UP)
		{
			return 10079;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_BALL)
		{
			return 10090;
		}
		if (theImage == AtlasResources.IMAGE_ARROW_RIGHT)
		{
			return 10080;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_BG1)
		{
			return 10104;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_BG2)
		{
			return 10105;
		}
		if (theImage == AtlasResources.IMAGE_LEADERBOARD_BG3)
		{
			return 10106;
		}
		if (theImage == AtlasResources.IMAGE_PLAYER_ICON)
		{
			return 10166;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_FILL)
		{
			return 10092;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_RING_OLD)
		{
			return 10095;
		}
		if (theImage == AtlasResources.IMAGE_HELP_INDICATOR_ARROW)
		{
			return 10028;
		}
		if (theImage == AtlasResources.IMAGE_MORE_GAMES_LIST_BOTTOM_OV)
		{
			return 10103;
		}
		if (theImage == AtlasResources.IMAGE_MORE_GAMES_LIST_TOP_OV)
		{
			return 10102;
		}
		if (theImage == AtlasResources.IMAGE_SCROLL_INDICATOR)
		{
			return 10100;
		}
		if (theImage == AtlasResources.IMAGE_SUB_HEADER_LEFT_MID)
		{
			return 10018;
		}
		if (theImage == AtlasResources.IMAGE_SUB_HEADER_RIGHT_MID)
		{
			return 10017;
		}
		if (theImage == AtlasResources.IMAGE_SLIDER_FILL_OLD)
		{
			return 10093;
		}
		if (theImage == AtlasResources.IMAGE_BLACK_HOLE)
		{
			return 10003;
		}
		if (theImage == AtlasResources.IMAGE_POWER_GLOW)
		{
			return 10078;
		}
		if (theImage == AtlasResources.IMAGE_GEM_LIGHTING)
		{
			return 10077;
		}
		if (theImage == AtlasResources.IMAGE_STAR_FRONT)
		{
			return 10054;
		}
		if (theImage == AtlasResources.IMAGE_HYPERCUBE_ADD)
		{
			return 10046;
		}
		if (theImage == AtlasResources.IMAGE_EXPLOSION)
		{
			return 10074;
		}
		if (theImage == AtlasResources.IMAGE_SHARD)
		{
			return 10075;
		}
		if (theImage == AtlasResources.IMAGE_SPARKLE)
		{
			return 10073;
		}
		if (theImage == AtlasResources.IMAGE_BIGSTAR)
		{
			return 10076;
		}
		int idByImage = (int)Resources.GetIdByImage(theImage);
		if (idByImage == 75)
		{
			return -1;
		}
		return idByImage;
	}

	public static AtlasResources mAtlasResources;

	public static Image IMAGE_GRID;

	public static Image IMAGE_STAR_GEM2;

	public static Image IMAGE_STAR_GEM6;

	public static Image IMAGE_STAR_GEM5;

	public static Image IMAGE_STAR_GEM4;

	public static Image IMAGE_STAR_GEM3;

	public static Image IMAGE_STAR_GEM1;

	public static Image IMAGE_STAR_GEM0;

	public static Image IMAGE_HYPERCUBE_LOWER;

	public static Image IMAGE_FLAME_GEM2;

	public static Image IMAGE_FLAME_GEM6;

	public static Image IMAGE_FLAME_GEM5;

	public static Image IMAGE_FLAME_GEM4;

	public static Image IMAGE_FLAME_GEM3;

	public static Image IMAGE_FLAME_GEM1;

	public static Image IMAGE_FLAME_GEM0;

	public static Image IMAGE_GEM0;

	public static Image IMAGE_GEM1;

	public static Image IMAGE_GEM2;

	public static Image IMAGE_GEM3;

	public static Image IMAGE_GEM4;

	public static Image IMAGE_GEM5;

	public static Image IMAGE_GEM6;

	public static Image IMAGE_LIGHTNING;

	public static Image IMAGE_WIDGET_TOP;

	public static Image IMAGE_FRAME_TOP;

	public static Image IMAGE_FRAME_TOP_BACK;

	public static Image IMAGE_FRAME_BOTTOM;

	public static Image IMAGE_BOARD_SPEED_BONUS_BG;

	public static Image IMAGE_GEM_MULTIPLIER2;

	public static Image IMAGE_GEM_MULTIPLIER6;

	public static Image IMAGE_GEM_MULTIPLIER5;

	public static Image IMAGE_GEM_MULTIPLIER4;

	public static Image IMAGE_GEM_MULTIPLIER7;

	public static Image IMAGE_GEM_MULTIPLIER3;

	public static Image IMAGE_GEM_MULTIPLIER1;

	public static Image IMAGE_GEM_MULTIPLIER0;

	public static Image IMAGE_LIGHTNING_GLOW;

	public static Image IMAGE_PARTICLE_1;

	public static Image IMAGE_PARTICLE_2;

	public static Image IMAGE_PARTICLE_3;

	public static Image IMAGE_PARTICLE_4;

	public static Image IMAGE_HELP_ARROW;

	public static Image IMAGE_SELECT;

	public static Image IMAGE_INFINITY;

	public static Image IMAGE_MULTIPLIER2X;

	public static Image IMAGE_MULTIPLIER3X;

	public static Image IMAGE_MULTIPLIER4X;

	public static Image IMAGE_MULTIPLIER5X;

	public static Image IMAGE_MULTIPLIER6X;

	public static Image IMAGE_MULTIPLIER7X;

	public static Image IMAGE_MULTIPLIER8X;

	public static Image IMAGE_PROGRESSBAR_PARTICLE_BACK;

	public static Image IMAGE_FRAME_CHIP_WARN_VERT;

	public static Image IMAGE_FRAME_CHIP_WARN_HORIZ;

	public static Image IMAGE_FRAME_CHIP_HORIZ;

	public static Image IMAGE_FRAME_CHIP_VERT;

	public static Image IMAGE_FIRE_RING;

	public static Image IMAGE_UPSELL_SCREEN_1;

	public static Image IMAGE_UPSELL_SCREEN_2;

	public static Image IMAGE_UPSELL_SCREEN_3;

	public static Image IMAGE_UPSELL_SCREEN_4;

	public static Image IMAGE_UPSELL_SCREEN_5;

	public static Image IMAGE_MAINMENU_LIVE_LOGO;

	public static Image IMAGE_SUB_BORDER;

	public static Image IMAGE_ACTION_SHEET_BG;

	public static Image IMAGE_WIDGET_BOTTOM_DOUBLE;

	public static Image IMAGE_WIDGET_BOTTOM;

	public static Image IMAGE_PILL_RING;

	public static Image IMAGE_BLACK_HOLE_COVER;

	public static Image IMAGE_TUNNEL_END;

	public static Image IMAGE_HINT_BUTTON;

	public static Image IMAGE_DOUBLE_BUTTON;

	public static Image IMAGE_ALERT_BG;

	public static Image IMAGE_PILL_ADDITIVE;

	public static Image IMAGE_PILL_CENTRE1;

	public static Image IMAGE_PILL_CENTRE2;

	public static Image IMAGE_PILL_SMALL;

	public static Image IMAGE_LEADERBOARD_MEDAL_RING;

	public static Image IMAGE_RANK_HEADER;

	public static Image IMAGE_SUB_HEADER_MID;

	public static Image IMAGE_LITTLE_BLUE_PILL;

	public static Image IMAGE_LITTLE_GREEN_PILL;

	public static Image IMAGE_LITTLE_RED_PILL;

	public static Image IMAGE_PAUSE_BUTTON_RING;

	public static Image IMAGE_LEADERBOARD_LIST_TOP_OV;

	public static Image IMAGE_LOADING_RING;

	public static Image IMAGE_EMPTY_BUTTON;

	public static Image IMAGE_PAUSE_BUTTON;

	public static Image IMAGE_PLAY_BUTTON;

	public static Image IMAGE_LEADERBOARD_NEW_TOURNAMENT;

	public static Image IMAGE_LEADERBOARD_YOUR_SCORE;

	public static Image IMAGE_PILL_TOP;

	public static Image IMAGE_LEADERBOARD_NEWS;

	public static Image IMAGE_LEADERBOARD_LIST_BOTTOM_OV;

	public static Image IMAGE_LEADERBOARD_NEW_HIGH_SCORE;

	public static Image IMAGE_PAUSE_BUTTON_PAUSE;

	public static Image IMAGE_PAUSE_BUTTON_PLAY;

	public static Image IMAGE_SLIDER_BALL_RING;

	public static Image IMAGE_PILL_SMALL_ADDITIVE;

	public static Image IMAGE_PILL_SMALL_CENTER1;

	public static Image IMAGE_PILL_SMALL_CENTER2;

	public static Image IMAGE_LEADERBOARD_MEDAL_0;

	public static Image IMAGE_LEADERBOARD_MEDAL_0_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_1;

	public static Image IMAGE_LEADERBOARD_MEDAL_10;

	public static Image IMAGE_LEADERBOARD_MEDAL_10_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_11;

	public static Image IMAGE_LEADERBOARD_MEDAL_11_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_12;

	public static Image IMAGE_LEADERBOARD_MEDAL_12_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_13;

	public static Image IMAGE_LEADERBOARD_MEDAL_13_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_14;

	public static Image IMAGE_LEADERBOARD_MEDAL_14_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_15;

	public static Image IMAGE_LEADERBOARD_MEDAL_15_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_16;

	public static Image IMAGE_LEADERBOARD_MEDAL_16_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_17;

	public static Image IMAGE_LEADERBOARD_MEDAL_17_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_18;

	public static Image IMAGE_LEADERBOARD_MEDAL_18_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_19;

	public static Image IMAGE_LEADERBOARD_MEDAL_19_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_1_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_2;

	public static Image IMAGE_LEADERBOARD_MEDAL_20;

	public static Image IMAGE_LEADERBOARD_MEDAL_20_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_2_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_3;

	public static Image IMAGE_LEADERBOARD_MEDAL_3_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_4;

	public static Image IMAGE_LEADERBOARD_MEDAL_4_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_5;

	public static Image IMAGE_LEADERBOARD_MEDAL_5_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_6;

	public static Image IMAGE_LEADERBOARD_MEDAL_6_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_7;

	public static Image IMAGE_LEADERBOARD_MEDAL_7_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_8;

	public static Image IMAGE_LEADERBOARD_MEDAL_8_GOLD;

	public static Image IMAGE_LEADERBOARD_MEDAL_9;

	public static Image IMAGE_LEADERBOARD_MEDAL_9_GOLD;

	public static Image IMAGE_PILL_BOT;

	public static Image IMAGE_OPTIONS_BUTTON;

	public static Image IMAGE_POPCAP_BUTTON;

	public static Image IMAGE_GAMERTAG_UNKNOWN;

	public static Image IMAGE_ICON;

	public static Image IMAGE_SUB_HEADER_RIGHT;

	public static Image IMAGE_SUB_HEADER_LEFT;

	public static Image IMAGE_LOCK;

	public static Image IMAGE_CHECKBOX_UNCHECKED;

	public static Image IMAGE_CHECKBOX_CHECKED;

	public static Image IMAGE_MORE_GAMES_NEW_CONTENT;

	public static Image IMAGE_SLIDER_RING;

	public static Image IMAGE_SLIDER_BALL_OLD;

	public static Image IMAGE_PLAYERS_ICON;

	public static Image IMAGE_ARROW_UP;

	public static Image IMAGE_SLIDER_BALL;

	public static Image IMAGE_ARROW_RIGHT;

	public static Image IMAGE_LEADERBOARD_BG1;

	public static Image IMAGE_LEADERBOARD_BG2;

	public static Image IMAGE_LEADERBOARD_BG3;

	public static Image IMAGE_PLAYER_ICON;

	public static Image IMAGE_SLIDER_FILL;

	public static Image IMAGE_SLIDER_RING_OLD;

	public static Image IMAGE_HELP_INDICATOR_ARROW;

	public static Image IMAGE_MORE_GAMES_LIST_BOTTOM_OV;

	public static Image IMAGE_MORE_GAMES_LIST_TOP_OV;

	public static Image IMAGE_SCROLL_INDICATOR;

	public static Image IMAGE_SUB_HEADER_LEFT_MID;

	public static Image IMAGE_SUB_HEADER_RIGHT_MID;

	public static Image IMAGE_SLIDER_FILL_OLD;

	public static Image IMAGE_BLACK_HOLE;

	public static Image IMAGE_POWER_GLOW;

	public static Image IMAGE_GEM_LIGHTING;

	public static Image IMAGE_STAR_FRONT;

	public static Image IMAGE_HYPERCUBE_ADD;

	public static Image IMAGE_EXPLOSION;

	public static Image IMAGE_SHARD;

	public static Image IMAGE_SPARKLE;

	public static Image IMAGE_BIGSTAR;

	public enum AtlasImageId
	{
		__ATLAS_BASE_ID = 10000,
		IMAGE_TUNNEL_END_ID,
		IMAGE_FIRE_RING_ID,
		IMAGE_BLACK_HOLE_ID,
		IMAGE_BLACK_HOLE_COVER_ID,
		IMAGE_MAINMENU_LIVE_LOGO_ID,
		IMAGE_PILL_CENTRE1_ID,
		IMAGE_PILL_CENTRE2_ID,
		IMAGE_PILL_ADDITIVE_ID,
		IMAGE_PILL_RING_ID,
		IMAGE_PILL_TOP_ID,
		IMAGE_PILL_BOT_ID,
		IMAGE_PILL_SMALL_ID,
		IMAGE_PILL_SMALL_CENTER1_ID,
		IMAGE_PILL_SMALL_CENTER2_ID,
		IMAGE_PILL_SMALL_ADDITIVE_ID,
		IMAGE_SUB_BORDER_ID,
		IMAGE_SUB_HEADER_RIGHT_MID_ID,
		IMAGE_SUB_HEADER_LEFT_MID_ID,
		IMAGE_SUB_HEADER_RIGHT_ID,
		IMAGE_SUB_HEADER_LEFT_ID,
		IMAGE_SUB_HEADER_MID_ID,
		IMAGE_RANK_HEADER_ID,
		IMAGE_POPCAP_BUTTON_ID,
		IMAGE_OPTIONS_BUTTON_ID,
		IMAGE_LITTLE_GREEN_PILL_ID,
		IMAGE_LITTLE_BLUE_PILL_ID,
		IMAGE_LITTLE_RED_PILL_ID,
		IMAGE_HELP_INDICATOR_ARROW_ID,
		IMAGE_HELP_ARROW_ID,
		IMAGE_GRID_ID,
		IMAGE_GEM0_ID,
		IMAGE_GEM1_ID,
		IMAGE_GEM2_ID,
		IMAGE_GEM3_ID,
		IMAGE_GEM4_ID,
		IMAGE_GEM5_ID,
		IMAGE_GEM6_ID,
		IMAGE_FLAME_GEM0_ID,
		IMAGE_FLAME_GEM1_ID,
		IMAGE_FLAME_GEM2_ID,
		IMAGE_FLAME_GEM3_ID,
		IMAGE_FLAME_GEM4_ID,
		IMAGE_FLAME_GEM5_ID,
		IMAGE_FLAME_GEM6_ID,
		IMAGE_HYPERCUBE_LOWER_ID,
		IMAGE_HYPERCUBE_ADD_ID,
		IMAGE_STAR_GEM0_ID,
		IMAGE_STAR_GEM1_ID,
		IMAGE_STAR_GEM2_ID,
		IMAGE_STAR_GEM3_ID,
		IMAGE_STAR_GEM4_ID,
		IMAGE_STAR_GEM5_ID,
		IMAGE_STAR_GEM6_ID,
		IMAGE_STAR_FRONT_ID,
		IMAGE_LEADERBOARD_MEDAL_RING_ID,
		IMAGE_GEM_MULTIPLIER0_ID,
		IMAGE_GEM_MULTIPLIER1_ID,
		IMAGE_GEM_MULTIPLIER2_ID,
		IMAGE_GEM_MULTIPLIER3_ID,
		IMAGE_GEM_MULTIPLIER4_ID,
		IMAGE_GEM_MULTIPLIER5_ID,
		IMAGE_GEM_MULTIPLIER6_ID,
		IMAGE_GEM_MULTIPLIER7_ID,
		IMAGE_MULTIPLIER2X_ID,
		IMAGE_MULTIPLIER3X_ID,
		IMAGE_MULTIPLIER4X_ID,
		IMAGE_MULTIPLIER5X_ID,
		IMAGE_MULTIPLIER6X_ID,
		IMAGE_MULTIPLIER7X_ID,
		IMAGE_MULTIPLIER8X_ID,
		IMAGE_BOARD_SPEED_BONUS_BG_ID,
		IMAGE_SELECT_ID,
		IMAGE_SPARKLE_ID,
		IMAGE_EXPLOSION_ID,
		IMAGE_SHARD_ID,
		IMAGE_BIGSTAR_ID,
		IMAGE_GEM_LIGHTING_ID,
		IMAGE_POWER_GLOW_ID,
		IMAGE_ARROW_UP_ID,
		IMAGE_ARROW_RIGHT_ID,
		IMAGE_ALERT_BG_ID,
		IMAGE_ACTION_SHEET_BG_ID,
		IMAGE_FRAME_CHIP_HORIZ_ID,
		IMAGE_FRAME_CHIP_VERT_ID,
		IMAGE_FRAME_CHIP_WARN_HORIZ_ID,
		IMAGE_FRAME_CHIP_WARN_VERT_ID,
		IMAGE_CHECKBOX_CHECKED_ID,
		IMAGE_CHECKBOX_UNCHECKED_ID,
		IMAGE_SLIDER_BALL_RING_ID,
		IMAGE_SLIDER_BALL_ID,
		IMAGE_SLIDER_BALL_OLD_ID,
		IMAGE_SLIDER_FILL_ID,
		IMAGE_SLIDER_FILL_OLD_ID,
		IMAGE_SLIDER_RING_ID,
		IMAGE_SLIDER_RING_OLD_ID,
		IMAGE_PAUSE_BUTTON_RING_ID,
		IMAGE_PAUSE_BUTTON_PLAY_ID,
		IMAGE_PAUSE_BUTTON_PAUSE_ID,
		IMAGE_INFINITY_ID,
		IMAGE_SCROLL_INDICATOR_ID,
		IMAGE_MORE_GAMES_NEW_CONTENT_ID,
		IMAGE_MORE_GAMES_LIST_TOP_OV_ID,
		IMAGE_MORE_GAMES_LIST_BOTTOM_OV_ID,
		IMAGE_LEADERBOARD_BG1_ID,
		IMAGE_LEADERBOARD_BG2_ID,
		IMAGE_LEADERBOARD_BG3_ID,
		IMAGE_LEADERBOARD_NEW_HIGH_SCORE_ID,
		IMAGE_LEADERBOARD_NEW_TOURNAMENT_ID,
		IMAGE_LEADERBOARD_NEWS_ID,
		IMAGE_LEADERBOARD_YOUR_SCORE_ID,
		IMAGE_LEADERBOARD_LIST_TOP_OV_ID,
		IMAGE_LEADERBOARD_LIST_BOTTOM_OV_ID,
		IMAGE_LEADERBOARD_MEDAL_0_ID,
		IMAGE_LEADERBOARD_MEDAL_1_ID,
		IMAGE_LEADERBOARD_MEDAL_2_ID,
		IMAGE_LEADERBOARD_MEDAL_3_ID,
		IMAGE_LEADERBOARD_MEDAL_4_ID,
		IMAGE_LEADERBOARD_MEDAL_5_ID,
		IMAGE_LEADERBOARD_MEDAL_6_ID,
		IMAGE_LEADERBOARD_MEDAL_7_ID,
		IMAGE_LEADERBOARD_MEDAL_8_ID,
		IMAGE_LEADERBOARD_MEDAL_9_ID,
		IMAGE_LEADERBOARD_MEDAL_10_ID,
		IMAGE_LEADERBOARD_MEDAL_11_ID,
		IMAGE_LEADERBOARD_MEDAL_12_ID,
		IMAGE_LEADERBOARD_MEDAL_13_ID,
		IMAGE_LEADERBOARD_MEDAL_14_ID,
		IMAGE_LEADERBOARD_MEDAL_15_ID,
		IMAGE_LEADERBOARD_MEDAL_16_ID,
		IMAGE_LEADERBOARD_MEDAL_17_ID,
		IMAGE_LEADERBOARD_MEDAL_18_ID,
		IMAGE_LEADERBOARD_MEDAL_19_ID,
		IMAGE_LEADERBOARD_MEDAL_20_ID,
		IMAGE_LEADERBOARD_MEDAL_0_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_1_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_2_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_3_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_4_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_5_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_6_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_7_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_8_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_9_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_10_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_11_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_12_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_13_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_14_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_15_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_16_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_17_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_18_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_19_GOLD_ID,
		IMAGE_LEADERBOARD_MEDAL_20_GOLD_ID,
		IMAGE_FRAME_BOTTOM_ID,
		IMAGE_FRAME_TOP_ID,
		IMAGE_FRAME_TOP_BACK_ID,
		IMAGE_WIDGET_BOTTOM_ID,
		IMAGE_WIDGET_BOTTOM_DOUBLE_ID,
		IMAGE_WIDGET_TOP_ID,
		IMAGE_PAUSE_BUTTON_ID,
		IMAGE_PLAY_BUTTON_ID,
		IMAGE_HINT_BUTTON_ID,
		IMAGE_EMPTY_BUTTON_ID,
		IMAGE_PROGRESSBAR_PARTICLE_BACK_ID,
		IMAGE_PLAYER_ICON_ID,
		IMAGE_PLAYERS_ICON_ID,
		IMAGE_LOCK_ID,
		IMAGE_DOUBLE_BUTTON_ID,
		IMAGE_PARTICLE_1_ID,
		IMAGE_PARTICLE_2_ID,
		IMAGE_PARTICLE_3_ID,
		IMAGE_PARTICLE_4_ID,
		IMAGE_LIGHTNING_ID,
		IMAGE_LIGHTNING_GLOW_ID,
		IMAGE_GAMERTAG_UNKNOWN_ID,
		IMAGE_LOADING_RING_ID,
		IMAGE_UPSELL_SCREEN_1_ID,
		IMAGE_UPSELL_SCREEN_2_ID,
		IMAGE_UPSELL_SCREEN_3_ID,
		IMAGE_UPSELL_SCREEN_4_ID,
		IMAGE_UPSELL_SCREEN_5_ID,
		IMAGE_ICON_ID
	}

	public class AtlasStringTable
	{
		public AtlasStringTable(string strId, int imgId)
		{
			this.mStringId = strId;
			this.mImageId = imgId;
		}

		public string mStringId;

		public int mImageId;
	}
}
