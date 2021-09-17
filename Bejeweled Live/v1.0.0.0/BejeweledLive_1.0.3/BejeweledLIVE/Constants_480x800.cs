using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class Constants_480x800 : Constants
	{
		public override int Menu1_X
		{
			get
			{
				return 10;
			}
		}

		public override Insets ActionSheet_ACTION_SHEET_FRAME_INSETS
		{
			get
			{
				return new Insets(40, 50, 40, 20);
			}
		}

		public override int ActionSheet_ACTION_SHEET_BUTTON_GAP
		{
			get
			{
				return 8;
			}
		}

		public override float ActionSheet_mCancelButton_WidthFactor
		{
			get
			{
				return 0.4f;
			}
		}

		public override int ActionSheet_Header_Offset_Y
		{
			get
			{
				return 52;
			}
		}

		public override Insets Alert_ALERT_INSETS
		{
			get
			{
				return new Insets(40, 40, 40, 30);
			}
		}

		public override int Alert_ALERT_WIDGET_WIDTH
		{
			get
			{
				return 400;
			}
		}

		public override int Alert_HeadingToText_Distance
		{
			get
			{
				return 40;
			}
		}

		public override int Board_Board_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 0;
				}
				return 280;
			}
		}

		public override int Board_Board_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 30;
				}
				return 0;
			}
		}

		public override int OutValueX
		{
			get
			{
				return 820;
			}
		}

		public override int Board_ButtonDistanceFromBottom
		{
			get
			{
				return 120;
			}
		}

		public override int GEM_WIDTH
		{
			get
			{
				return 60;
			}
		}

		public override int GEM_HEIGHT
		{
			get
			{
				return 60;
			}
		}

		public override int HelpScreen_TITLE_OFFSET_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 100;
				}
				return 50;
			}
		}

		public override int HelpScreen_BACKBUTTON_OFFSET_Y
		{
			get
			{
				return 30;
			}
		}

		public override int HelpScreen_FlameGem_Offset_Y
		{
			get
			{
				return 2;
			}
		}

		public override int HelpScreen_Score_Y
		{
			get
			{
				return 150;
			}
		}

		public override int OptionsDialog_IMAGE_SLIDER_FILL_X
		{
			get
			{
				return 26;
			}
		}

		public override int OptionsDialog_aTrackY_Offset
		{
			get
			{
				return 4;
			}
		}

		public override int OptionsDialog_IMAGE_SLIDER_RING_Offset
		{
			get
			{
				return 20;
			}
		}

		public override int OptionsDialog_IMAGE_SLIDER_RING_Offset2
		{
			get
			{
				return 40;
			}
		}

		public override int OptionsDialog_IMAGE_SLIDER_BALL_OffsetX
		{
			get
			{
				return AtlasResources.IMAGE_SLIDER_BALL_RING.GetWidth() / 2 - AtlasResources.IMAGE_SLIDER_BALL.GetWidth() / 2;
			}
		}

		public override int OptionsDialog_IMAGE_SLIDER_BALL_OffsetY
		{
			get
			{
				return AtlasResources.IMAGE_SLIDER_BALL_RING.GetHeight() / 2 - AtlasResources.IMAGE_SLIDER_BALL.GetHeight() / 2;
			}
		}

		public override int BoardBej2_mMenuButton_YPosition_Portrait
		{
			get
			{
				return 680;
			}
		}

		public override int BoardBej2_mMenuButton_XPosition_Portrait
		{
			get
			{
				return 2;
			}
		}

		public override int BoardBej2_mHintButton_XPosition_Portrait
		{
			get
			{
				return 478;
			}
		}

		public override int BoardBej2_mMenuButton_XPosition_Landscape
		{
			get
			{
				return 45;
			}
		}

		public override int BoardBej2_mMenuButton_YPosition_Landscape
		{
			get
			{
				return 252;
			}
		}

		public override int BoardBej2_mHintButton_XPosition_Landscape
		{
			get
			{
				return 80;
			}
		}

		public override int BoardBej2_bombifiedPieceRotatingStarOffset
		{
			get
			{
				return 14;
			}
		}

		public override int BoardBej2_infinitySymbolX_Landscape
		{
			get
			{
				return 105;
			}
		}

		public override int BoardBej2_infinitySymbolY_Landscape
		{
			get
			{
				return 200;
			}
		}

		public override int BoardBej2_infinitySymbolX_Portrait
		{
			get
			{
				return 240;
			}
		}

		public override int BoardBej2_infinitySymbolY_Portrait
		{
			get
			{
				return 750;
			}
		}

		public override int BoardBej2_TrialTime_Offset_Y_Portrait
		{
			get
			{
				return 15;
			}
		}

		public override int BoardBej2_selectPiece_dimensions
		{
			get
			{
				return AtlasResources.IMAGE_SELECT.GetCelWidth();
			}
		}

		public override double BoardBej2_Shard_X_SIZE
		{
			get
			{
				return 21.0;
			}
		}

		public override int BoardBej2_Shard_Size
		{
			get
			{
				return AtlasResources.IMAGE_SHARD.GetWidth() / 40;
			}
		}

		public override int BoardBej2_Shard_X_ForFrame(int frame)
		{
			return frame * AtlasResources.IMAGE_SHARD.GetWidth() / 40;
		}

		public override double BoardBej2_Explosion_HalfSize
		{
			get
			{
				return (double)(AtlasResources.IMAGE_EXPLOSION.GetWidth() / 40);
			}
		}

		public override int BoardBej2_Explosion_Size(int frame)
		{
			return frame * this.BoardBej2_Explosion_SizeFull;
		}

		public override int BoardBej2_Explosion_SizeFull
		{
			get
			{
				return AtlasResources.IMAGE_EXPLOSION.GetWidth() / 20;
			}
		}

		public override int BoardBej2_ProgressBarLength
		{
			get
			{
				return 465;
			}
		}

		public override int BoardBej2_BlackHoleDimension
		{
			get
			{
				return 192;
			}
		}

		public override int BoardBej2_HintArrowOffset
		{
			get
			{
				return -5;
			}
		}

		public override int BoardBej2_HintArrowOverlayOffset_X
		{
			get
			{
				return 15;
			}
		}

		public override int BoardBej2_HintArrowOverlayOffset_Y
		{
			get
			{
				return 15;
			}
		}

		public override int BoardBej2_HintArrowOverlaySize
		{
			get
			{
				return AtlasResources.IMAGE_HELP_INDICATOR_ARROW.GetWidth();
			}
		}

		public override int BoardBej2_Sparkle_X_LongLife
		{
			get
			{
				return 10;
			}
		}

		public override int BoardBej2_Sparkle_Y_LongLife
		{
			get
			{
				return 5;
			}
		}

		public override int BoardBej2_Sparkle_Frame(int frame)
		{
			return (int)((double)frame * ((double)AtlasResources.IMAGE_SPARKLE.GetWidth() / 14.0));
		}

		public override int BoardBej2_Sparkle_Dimension
		{
			get
			{
				return AtlasResources.IMAGE_SPARKLE.GetHeight();
			}
		}

		public override int BoardBej2_Sparkle_X_MediumLife
		{
			get
			{
				return 7;
			}
		}

		public override int BoardBej2_Sparkle_Y_MediumLife
		{
			get
			{
				return 2;
			}
		}

		public override int BoardBej2_Sparkle_X_ShortLife
		{
			get
			{
				return 3;
			}
		}

		public override int BoardBej2_Sparkle_Y_ShortLife
		{
			get
			{
				return 14;
			}
		}

		public override int BoardBej2_Sparkle_HalfSize
		{
			get
			{
				return AtlasResources.IMAGE_SPARKLE.GetHeight() / 2;
			}
		}

		public override int BoardBej2_PauseButton_X_Offset_Landscape
		{
			get
			{
				return 40;
			}
		}

		public override int BoardBej2_PauseButton_X_Offset_Portrait
		{
			get
			{
				return 240;
			}
		}

		public override int BoardBej2_PauseButton_Y_Offset_Landscape
		{
			get
			{
				return -11;
			}
		}

		public override int BoardBej2_IMAGE_GEM_ADD_WHITE_Width
		{
			get
			{
				return 600;
			}
		}

		public override int BoardBej2_LevelText_X_Landscape
		{
			get
			{
				return 0;
			}
		}

		public override int BoardBej2_LevelText_X_Portrait
		{
			get
			{
				return 0;
			}
		}

		public override int BoardBej2_LevelText_Y_Landscape
		{
			get
			{
				return 82;
			}
		}

		public override int BoardBej2_LevelText_Y_Portrait
		{
			get
			{
				return 82;
			}
		}

		public override int BoardBej2_ScoreText_X_Portrait
		{
			get
			{
				return 20;
			}
		}

		public override int BoardBej2_ScoreText_Y_Portrait
		{
			get
			{
				return 600;
			}
		}

		public override int BoardBej2_ScoreText_X_Landscape
		{
			get
			{
				return 10;
			}
		}

		public override int BoardBej2_ScoreText_Y_Landscape
		{
			get
			{
				return 40;
			}
		}

		public override int BoardBej2_ScoreText_WarpOffset_Y
		{
			get
			{
				return 1;
			}
		}

		public override int Board_Multiplier_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 300;
				}
				return 60;
			}
		}

		public override int HyperSpace_FireRing_Size
		{
			get
			{
				return 150;
			}
		}

		public override float HyperSpace_HyperPoint_Scale
		{
			get
			{
				return 0.78125f;
			}
		}

		public override TRect HyperSpace_TunnelEnd_Source
		{
			get
			{
				return new TRect(0, 0, AtlasResources.IMAGE_TUNNEL_END.GetWidth(), AtlasResources.IMAGE_TUNNEL_END.GetHeight());
			}
		}

		public override float HyperSpace_TunnelEnd_Scale
		{
			get
			{
				return 0.78125f;
			}
		}

		public override int HyperSpace_FireRing_Width(int tunnelEndSize)
		{
			return (tunnelEndSize * 800 + 799) / 1024;
		}

		public override double HyperSpace_PortalSize_Scale
		{
			get
			{
				return 0.95;
			}
		}

		public override double HyperSpace_PortalSize_ScaleOffset
		{
			get
			{
				return 0.6;
			}
		}

		public override int HyperSpace_WarpOffset
		{
			get
			{
				return -60;
			}
		}

		public override int EffectOverlay_PortalSize_landscape
		{
			get
			{
				return 1100;
			}
		}

		public override int EffectOverlay_PortalSize_portait
		{
			get
			{
				return 780;
			}
		}

		public override int BIGTEXT_WIDTH
		{
			get
			{
				return 400;
			}
		}

		public override int BIGTEXT_HEIGHT
		{
			get
			{
				return 100;
			}
		}

		public override int MainMenu_CancelButtonWidth
		{
			get
			{
				return 200;
			}
		}

		public override int MainMenu_SparklyLogo_Y_Landscape
		{
			get
			{
				return 20;
			}
		}

		public override int MainMenu_SparklyLogo_Y_Portrait
		{
			get
			{
				return 50;
			}
		}

		public override int MainMenu_ButtonStartPos_Y_TrialMode_Portrait
		{
			get
			{
				return 280;
			}
		}

		public override int MainMenu_ButtonStartPos_Y_NotTrialMode_Portrait
		{
			get
			{
				return 320;
			}
		}

		public override int MainMenu_ButtonStartPos_Y_TrialMode_Landscape
		{
			get
			{
				return 170;
			}
		}

		public override int MainMenu_ButtonStartPos_Y_NotTrialMode_Landscape
		{
			get
			{
				return 200;
			}
		}

		public override int MainMenu_ButtonDistance_Y_Portrait
		{
			get
			{
				return 80;
			}
		}

		public override int MainMenu_ButtonDistance_TrialMode
		{
			get
			{
				return 0;
			}
		}

		public override int MainMenu_ButtonDistance_NotTrialMode
		{
			get
			{
				return 10;
			}
		}

		public override int PodButtonFancy_Top_Offset
		{
			get
			{
				return 12;
			}
		}

		public override int PodButtonFancy_Bottom_Offset
		{
			get
			{
				return 12;
			}
		}

		public override int BUTTON_BAR_HEIGHT
		{
			get
			{
				return 80;
			}
		}

		public override int BUTTON_BAR_OFFSET_Y
		{
			get
			{
				return 5;
			}
		}

		public override int BUTTON_BAR_OFFSET_X
		{
			get
			{
				return 70;
			}
		}

		public override Insets GameApp_DIALOGBOX_EXTERIOR_INSETS
		{
			get
			{
				return new Insets(10, 9, 10, 10);
			}
		}

		public override Insets GameApp_DIALOGBOX_INTERIOR_INSETS
		{
			get
			{
				return new Insets(16, 15, 16, 16);
			}
		}

		public override Insets GameApp_DIALOGBOX_CONTENT_INSETS
		{
			get
			{
				return new Insets(28, 21, 28, 22);
			}
		}

		public override int ScoresWidget_aCol1_Rank_Landscape
		{
			get
			{
				return 50;
			}
		}

		public override int ScoresWidget_aCol2_Rank_Landscape
		{
			get
			{
				return 50;
			}
		}

		public override int ScoresWidget_aCol1_Scores_Landscape
		{
			get
			{
				return 70;
			}
		}

		public override int ScoresWidget_aCol2_Scores_Landscape
		{
			get
			{
				return 80;
			}
		}

		public override int ScoresWidget_aCol3_Scores_Landscape
		{
			get
			{
				return 350;
			}
		}

		public override int ScoresWidget_aCol4_Scores_Landscape
		{
			get
			{
				return 420;
			}
		}

		public override int ScoresWidget_aCol1_Rank_Portrait
		{
			get
			{
				return 50;
			}
		}

		public override int ScoresWidget_aCol2_Rank_Portrait
		{
			get
			{
				return 50;
			}
		}

		public override int ScoresWidget_aCol1_Scores_Portrait
		{
			get
			{
				return 70;
			}
		}

		public override int ScoresWidget_aCol2_Scores_Portrait
		{
			get
			{
				return 80;
			}
		}

		public override int ScoresWidget_aCol3_Scores_Portrait
		{
			get
			{
				return 350;
			}
		}

		public override int ScoresWidget_aCol4_Scores_Portrait
		{
			get
			{
				return 420;
			}
		}

		public override int ScoresWidget_Y_Scores_Portrait
		{
			get
			{
				return 100;
			}
		}

		public override int ScoresWidget_Y_Scores_Landscape
		{
			get
			{
				return 100;
			}
		}

		public override int ScoresWidget_NameClipWidth_Scores_Portrait
		{
			get
			{
				return 220;
			}
		}

		public override int ScoresWidget_NameClipWidth_Scores_Landscape
		{
			get
			{
				return 220;
			}
		}

		public override int SparklyLogo_StarPosX_Landscape
		{
			get
			{
				return 165;
			}
		}

		public override int SparklyLogo_StarPosY_Landscape
		{
			get
			{
				return 4;
			}
		}

		public override int SparklyLogo_StarPosX_Portrait
		{
			get
			{
				return 164;
			}
		}

		public override int SparklyLogo_StarPosY_Portrait
		{
			get
			{
				return 5;
			}
		}

		public override int BoardBej2_TimerBar_Offset_Y_Portrait
		{
			get
			{
				return 7;
			}
		}

		public override int BoardBej2_FrameChipOffset
		{
			get
			{
				return 9;
			}
		}

		public override int BoardBej2_FrameChip_Warn_Offset
		{
			get
			{
				return 16;
			}
		}

		public override int BoardBej2_FrameChip_Warn_Offset2
		{
			get
			{
				return 7;
			}
		}

		public override TRect AchievementScreen_Bounds
		{
			get
			{
				return new TRect(29, 40, (GlobalStaticVars.gSexyAppBase.IsLandscape() ? 800 : 480) - 58, (GlobalStaticVars.gSexyAppBase.IsLandscape() ? 480 : 800) - 200);
			}
		}

		public override float AchievementScreen_Picture_Scale
		{
			get
			{
				return 1f;
			}
		}

		public override int AchievementScreen_Row_Height
		{
			get
			{
				return 100;
			}
		}

		public override TRect Leaderboard_Bounds
		{
			get
			{
				return new TRect(30, 90, -60, -130);
			}
		}

		public override TRect Leaderboard_Frame_Bounds
		{
			get
			{
				return new TRect(0, 0, 0, 0);
			}
		}

		public override int LeaderBoard_Button_Width
		{
			get
			{
				return 230;
			}
		}

		public override int LiveRank_Frame_Offset_Y
		{
			get
			{
				return 24;
			}
		}

		public override int LiveRank_Frame_Offset_Top
		{
			get
			{
				return 15;
			}
		}

		public override int LiveRank_Frame_Offset_Bottom
		{
			get
			{
				return 40;
			}
		}

		public override int LiveRank_Heading_Y
		{
			get
			{
				return 40;
			}
		}

		public override int LiveRank_Row_Distance_Portrait
		{
			get
			{
				return 15;
			}
		}

		public override int LiveRank_Row_Distance_Landscape
		{
			get
			{
				return 11;
			}
		}

		public override int FrameWidget_Text_Y
		{
			get
			{
				return -2;
			}
		}

		public override int FrameWidget_Image_Offset_X
		{
			get
			{
				return 7;
			}
		}

		public override int FrameWidget_Image_Offset_Y
		{
			get
			{
				return 7;
			}
		}

		public override int LeaderboardListWidget_Rank_X
		{
			get
			{
				return 40;
			}
		}

		public override int LeaderboardListWidget_Name_X
		{
			get
			{
				return 170;
			}
		}

		public override int LeaderboardListWidget_Name_Y
		{
			get
			{
				return 8;
			}
		}

		public override int LeaderboardListWidget_Picture_X
		{
			get
			{
				return 90;
			}
		}

		public override int LeaderboardListWidget_Score_X
		{
			get
			{
				return 170;
			}
		}

		public override int LeaderboardListWidget_Score_Y
		{
			get
			{
				return 38;
			}
		}

		public override int LeaderboardListWidget_Row_Height
		{
			get
			{
				return 100;
			}
		}

		public override int LiveRank_Text_Y_Portrait
		{
			get
			{
				return 140;
			}
		}

		public override int LiveRank_Text_Y_Landscape
		{
			get
			{
				return 100;
			}
		}

		public override int LeaderboardListWidget_Heading_Text_Offset
		{
			get
			{
				return 10;
			}
		}

		public override int LiveLeaderboard_Box_Offset_Y
		{
			get
			{
				return 15;
			}
		}

		public override int LiveLeaderboard_Frame_Offset
		{
			get
			{
				return 20;
			}
		}

		public override int LiveLeaderboard_Row_Distance_Portrait
		{
			get
			{
				return 10;
			}
		}

		public override int LiveLeaderboard_Row_Distance_Landscape
		{
			get
			{
				return 10;
			}
		}

		public override int AchievementScreen_Image_X
		{
			get
			{
				return 15;
			}
		}

		public override int AchievementScreen_Name_Y
		{
			get
			{
				return 5;
			}
		}

		public override int AchievementScreen_Score_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 380;
				}
				return 700;
			}
		}

		public override TRect AchievementScreen_Description_Box
		{
			get
			{
				return new TRect(95, 30, GlobalStaticVars.gSexyAppBase.IsLandscape() ? 600 : 250, 60);
			}
		}

		public override int AlertInsetDistance_X
		{
			get
			{
				return 20;
			}
		}

		public override int AlertInsetDistance_Y
		{
			get
			{
				return 25;
			}
		}

		public override int GameOverDialog_Heading_EdgeOffset_X_Landscape
		{
			get
			{
				return 20;
			}
		}

		public override int GameOverDialog_Box_EdgeOffset_X_Landscape
		{
			get
			{
				return 200;
			}
		}

		public override int GameOverDialog_Box_Offset_X_Landscape
		{
			get
			{
				return 500;
			}
		}

		public override int GameOverDialog_Box_Offset_Y_Landscape
		{
			get
			{
				return 10;
			}
		}

		public override int GameOverDialog_Heading_Y
		{
			get
			{
				return 60;
			}
		}

		public override int GameOverDialog_Text_Y
		{
			get
			{
				return 140;
			}
		}

		public override float BackgroundWidget_Image_Scale
		{
			get
			{
				return 1f;
			}
		}

		public override double Scale_Long
		{
			get
			{
				return 0.78125;
			}
		}

		public override int BackButton_Width
		{
			get
			{
				return 160;
			}
		}

		public override int MoreGamesDialog_Scroller_Size_LandScape
		{
			get
			{
				return 480;
			}
		}

		public override int MoreGamesDialog_BackButton_X_Landscape
		{
			get
			{
				return 80;
			}
		}

		public override int MoreGamesDialog_BackButton_Y_Landscape
		{
			get
			{
				return 440;
			}
		}

		public override int MoreGamesDialog_Scroller_Width_Portrait
		{
			get
			{
				return 480;
			}
		}

		public override int MoreGamesDialog_Scroller_Height_Portrait
		{
			get
			{
				return 338;
			}
		}

		public override int SplashScreen_Logo_Width
		{
			get
			{
				return 280;
			}
		}

		public override int SplashScreen_Logo_Height
		{
			get
			{
				return 280;
			}
		}

		public override float S(float num)
		{
			return (float)((double)num * 800.0 / 1024.0);
		}

		public override int BoardBej3_MultiplierGem_Size
		{
			get
			{
				return 66;
			}
		}

		public override int BoardBej3_MultiplierGem_Text_Size
		{
			get
			{
				return 38;
			}
		}

		public override int BoardBej3_MultiplierGem_Text_Offset
		{
			get
			{
				return 12;
			}
		}

		public override int BoardBej3_MultiplierGem_Text_Offset_UI
		{
			get
			{
				return 14;
			}
		}

		public override int BoardBej3_Bar_Length
		{
			get
			{
				return 480;
			}
		}

		public override int BoardBej3_Time_Position_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 240;
				}
				return this.BoardBej3_MenuHintButton_X_Landscape;
			}
		}

		public override int BoardBej3_Time_Position_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 540;
				}
				return 195;
			}
		}

		public override int BoardBej3_LastHurrah_Position_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 240;
				}
				return 3;
			}
		}

		public override int BoardBej3_LastHurrah_Position_Offset_X
		{
			get
			{
				return 6;
			}
		}

		public override int BoardBej3_Blazing_Position_Y_Landscape
		{
			get
			{
				return 195;
			}
		}

		public override int BoardBej3_Blazing2_Position_Y_Landscape
		{
			get
			{
				return 220;
			}
		}

		public override float BoardBej3_LaserGem_MaxOffset_1
		{
			get
			{
				return 25f;
			}
		}

		public override float BoardBej3_LaserGem_MaxOffset_2
		{
			get
			{
				return 12.5f;
			}
		}

		public override int BoardBej3_SpeedBonus_Y
		{
			get
			{
				return 50;
			}
		}

		public override int PointsBej3_Min_Y
		{
			get
			{
				return 75;
			}
		}

		public override int LASER_GEM_LENGTH
		{
			get
			{
				return 90;
			}
		}

		public override int LASER_GLOW_LENGTH
		{
			get
			{
				return 120;
			}
		}

		public override int LASER_GLOW_OFFSET
		{
			get
			{
				return 30;
			}
		}

		public override int LightningZap_HorizontalZap_Start_X
		{
			get
			{
				return this.Board_Board_X;
			}
		}

		public override int LightningZap_HorizontalZap_End_X
		{
			get
			{
				return this.Board_Board_X + AtlasResources.IMAGE_GRID.mWidth - this.GEM_WIDTH - this.GEM_WIDTH / 2;
			}
		}

		public override int LightningZap_VerticalZap_Start_Y
		{
			get
			{
				return this.Board_Board_Y + this.GEM_HEIGHT / 2;
			}
		}

		public override int LightningZap_VerticalZap_End_Y
		{
			get
			{
				return this.Board_Board_Y + AtlasResources.IMAGE_GRID.mWidth - this.GEM_HEIGHT - this.GEM_HEIGHT / 2;
			}
		}

		public override int BoardBej3_SpeedBonus_Text_Offset_X
		{
			get
			{
				return 105;
			}
		}

		public override int BoardBej3_SpeedBonus_Text_Offset_Y
		{
			get
			{
				return 7;
			}
		}

		public override int BoardBej3_Warp_Text_Target_X
		{
			get
			{
				return 300;
			}
		}

		public override int BoardBej3_Warp_Text_Target_Y
		{
			get
			{
				return 20;
			}
		}

		public override int BoardBej3_Frame_Top_Offset_Landscape
		{
			get
			{
				return -35;
			}
		}

		public override int BoardBej3_Frame_Bottom_Offset_Landscape
		{
			get
			{
				return 8;
			}
		}

		public override int BoardBej3_Frame_Top_Offset_Portrait
		{
			get
			{
				return 13;
			}
		}

		public override int BoardBej3_Frame_Bottom_Offset_Portrait
		{
			get
			{
				return 2;
			}
		}

		public override int BoardBej3_MenuHintButton_X_Portrait
		{
			get
			{
				return 370;
			}
		}

		public override int BoardBej3_MenuHintButton_X_Landscape
		{
			get
			{
				return 125;
			}
		}

		public override int BoardBej3_MenuHintButton_Offset_Y
		{
			get
			{
				return 10;
			}
		}

		public override int BoardBej3_ScoreText_Offset_Y
		{
			get
			{
				return 32;
			}
		}

		public override int Piece_FlameGem_Size
		{
			get
			{
				return 72;
			}
		}

		public override int Piece_FlameGem_Offset
		{
			get
			{
				return 6;
			}
		}

		public override int Piece_HyperCube_Size
		{
			get
			{
				return 72;
			}
		}

		public override int Piece_HyperCube_Offset
		{
			get
			{
				return 6;
			}
		}

		public override int Piece_LASER_GEM_OFFSET
		{
			get
			{
				return 15;
			}
		}

		public override int MenuHintButton_Menu_Y
		{
			get
			{
				return 150;
			}
		}

		public override int MenuHintButton_MenuButton_X
		{
			get
			{
				return 52;
			}
		}

		public override int MenuHintButton_MenuButton_Y
		{
			get
			{
				return 163;
			}
		}

		public override int MenuHintButton_Double_MenuButton_X
		{
			get
			{
				return 120;
			}
		}

		public override int MenuHintButton_Double_MenuButton_Y
		{
			get
			{
				return 137;
			}
		}

		public override int MenuHintButton_HintButton_X
		{
			get
			{
				return 31;
			}
		}

		public override int MenuHintButton_HintButton_Y
		{
			get
			{
				return 37;
			}
		}

		public override int MenuHintButton_Double_HintButton_X
		{
			get
			{
				return 47;
			}
		}

		public override int MenuHintButton_Double_HintButton_Y
		{
			get
			{
				return 27;
			}
		}

		public override int MenuHintButton_Double_LeftButton_X
		{
			get
			{
				return 20;
			}
		}

		public override int MenuHintButton_Double_LeftButton_Y
		{
			get
			{
				return 137;
			}
		}

		public override int MenuHintButton_Double_Endless_X
		{
			get
			{
				return 5;
			}
		}

		public override int MenuHintButton_Double_Endless_Y
		{
			get
			{
				return 17;
			}
		}

		public override int MenuHintButton_Double_Trial_X
		{
			get
			{
				return 30;
			}
		}

		public override int MenuHintButton_Double_Trial_Y
		{
			get
			{
				return 17;
			}
		}

		public override float MenuHintButton_Menu_Font_Scale
		{
			get
			{
				return 0.8f;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_X_Landscape
		{
			get
			{
				return 5;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_X2_Landscape
		{
			get
			{
				return 20;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_Y_Landscape
		{
			get
			{
				return 36;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_Y2_Landscape
		{
			get
			{
				return 36;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_X_Portrait
		{
			get
			{
				return 36;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_X2_Portrait
		{
			get
			{
				return 36;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_Y_Portrait
		{
			get
			{
				return 5;
			}
		}

		public override int BoardBej3_ProgressBar_Clip_Y2_Portrait
		{
			get
			{
				return 25;
			}
		}

		public override int SubHeader_Stretch_Offset
		{
			get
			{
				return 80;
			}
		}

		public override float Font_Scale
		{
			get
			{
				return 1f;
			}
		}

		public override Point BackBufferSize
		{
			get
			{
				return new Point(480, 800);
			}
		}

		public int[] coordinateXTableEN = new int[] { 10 };

		public int[] coordinateYTableEN = new int[] { 20 };
	}
}
