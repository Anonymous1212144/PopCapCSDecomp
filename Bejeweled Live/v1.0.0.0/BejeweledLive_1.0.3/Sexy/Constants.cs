using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public abstract class Constants : FrameworkConstants
	{
		public static int StartWidth
		{
			get
			{
				return 480;
			}
		}

		public static int StartHeight
		{
			get
			{
				return 800;
			}
		}

		public static SexyColor FRAME_BACK_COLOUR(float alpha)
		{
			return new SexyColor(80, 16, 32, (int)(255f * alpha));
		}

		public virtual int getCoordinateY(int id)
		{
			return 0;
		}

		public virtual string getString(int id)
		{
			return "EMPTY";
		}

		public static float SpeedBonusTextScale
		{
			get
			{
				switch (Constants.mLanguage)
				{
				case Constants.LanguageIndex.es:
					return 0.8f;
				case Constants.LanguageIndex.it:
					return 0.85f;
				default:
					return 1f;
				}
			}
		}

		public virtual int Menu1_X
		{
			get
			{
				return 0;
			}
		}

		public virtual Insets ActionSheet_ACTION_SHEET_FRAME_INSETS
		{
			get
			{
				return new Insets(20, 20, 20, 20);
			}
		}

		public virtual int ActionSheet_ACTION_SHEET_BUTTON_GAP
		{
			get
			{
				return 8;
			}
		}

		public virtual float ActionSheet_mCancelButton_WidthFactor
		{
			get
			{
				return 0.4f;
			}
		}

		public virtual int ActionSheet_Header_Offset_Y
		{
			get
			{
				return 34;
			}
		}

		public virtual Insets Alert_ALERT_INSETS
		{
			get
			{
				return new Insets(20, 18, 20, 26);
			}
		}

		public virtual int Alert_ALERT_WIDGET_WIDTH
		{
			get
			{
				return 300;
			}
		}

		public virtual int Alert_HeadingToText_Distance
		{
			get
			{
				return 4;
			}
		}

		public virtual int Board_Board_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 0;
				}
				return 134;
			}
		}

		public virtual int Board_Board_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 10;
				}
				return 0;
			}
		}

		public virtual int OutValueX
		{
			get
			{
				return 360;
			}
		}

		public virtual int Board_ButtonDistanceFromBottom
		{
			get
			{
				return 120;
			}
		}

		public virtual int GEM_WIDTH
		{
			get
			{
				return 40;
			}
		}

		public virtual int GEM_HEIGHT
		{
			get
			{
				return 40;
			}
		}

		public virtual int HelpScreen_TITLE_OFFSET_Y
		{
			get
			{
				return 50;
			}
		}

		public virtual int HelpScreen_BACKBUTTON_OFFSET_Y
		{
			get
			{
				return 20;
			}
		}

		public virtual int HelpScreen_FlameGem_Offset_Y
		{
			get
			{
				return 2;
			}
		}

		public virtual int HelpScreen_Score_Y
		{
			get
			{
				return 100;
			}
		}

		public virtual int OptionsDialog_IMAGE_SLIDER_FILL_X
		{
			get
			{
				return 16;
			}
		}

		public virtual int OptionsDialog_aTrackY_Offset
		{
			get
			{
				return 3;
			}
		}

		public virtual int OptionsDialog_IMAGE_SLIDER_RING_Offset
		{
			get
			{
				return 12;
			}
		}

		public virtual int OptionsDialog_IMAGE_SLIDER_RING_Offset2
		{
			get
			{
				return 24;
			}
		}

		public virtual int OptionsDialog_IMAGE_SLIDER_BALL_OffsetX
		{
			get
			{
				return 11;
			}
		}

		public virtual int OptionsDialog_IMAGE_SLIDER_BALL_OffsetY
		{
			get
			{
				return 11;
			}
		}

		public virtual int BoardBej2_mMenuButton_YPosition_Portrait
		{
			get
			{
				return 400;
			}
		}

		public virtual int BoardBej2_mMenuButton_XPosition_Portrait
		{
			get
			{
				return 2;
			}
		}

		public virtual int BoardBej2_mHintButton_XPosition_Portrait
		{
			get
			{
				return 318;
			}
		}

		public virtual int BoardBej2_mMenuButton_XPosition_Landscape
		{
			get
			{
				return 5;
			}
		}

		public virtual int BoardBej2_mMenuButton_YPosition_Landscape
		{
			get
			{
				return 252;
			}
		}

		public virtual int BoardBej2_mHintButton_XPosition_Landscape
		{
			get
			{
				return 80;
			}
		}

		public virtual int BoardBej2_bombifiedPieceRotatingStarOffset
		{
			get
			{
				return 9;
			}
		}

		public virtual int BoardBej2_infinitySymbolX_Landscape
		{
			get
			{
				return 47;
			}
		}

		public virtual int BoardBej2_infinitySymbolY_Landscape
		{
			get
			{
				return 59;
			}
		}

		public virtual int BoardBej2_infinitySymbolX_Portrait
		{
			get
			{
				return 160;
			}
		}

		public virtual int BoardBej2_infinitySymbolY_Portrait
		{
			get
			{
				return 425;
			}
		}

		public virtual int BoardBej2_TrialTime_Offset_Y_Portrait
		{
			get
			{
				return 5;
			}
		}

		public virtual int BoardBej2_selectPiece_dimensions
		{
			get
			{
				return 39;
			}
		}

		public virtual int BoardBej2_Shard_X_ForFrame(int frame)
		{
			return (int)((double)frame * this.BoardBej2_Shard_X_SIZE * 480.0 / 1024.0);
		}

		public virtual int BoardBej2_LevelText_X_Landscape
		{
			get
			{
				return 66;
			}
		}

		public virtual int BoardBej2_LevelText_X_Portrait
		{
			get
			{
				return 315;
			}
		}

		public virtual int BoardBej2_LevelText_Y_Landscape
		{
			get
			{
				return 55;
			}
		}

		public virtual int BoardBej2_LevelText_Y_Portrait
		{
			get
			{
				return 30;
			}
		}

		public virtual int BoardBej2_ScoreText_X_Portrait
		{
			get
			{
				return 63;
			}
		}

		public virtual int BoardBej2_ScoreText_Y_Portrait
		{
			get
			{
				return 150;
			}
		}

		public virtual int BoardBej2_ScoreText_X_Landscape
		{
			get
			{
				return 63;
			}
		}

		public virtual int BoardBej2_ScoreText_Y_Landscape
		{
			get
			{
				return 6;
			}
		}

		public virtual int BoardBej2_ScoreText_WarpOffset_Y
		{
			get
			{
				return 20;
			}
		}

		public virtual int SparklyLogo_StarPosX_Landscape
		{
			get
			{
				return 129;
			}
		}

		public virtual int SparklyLogo_StarPosY_Landscape
		{
			get
			{
				return 10;
			}
		}

		public virtual int SparklyLogo_StarPosX_Portrait
		{
			get
			{
				return 110;
			}
		}

		public virtual int SparklyLogo_StarPosY_Portrait
		{
			get
			{
				return 1;
			}
		}

		public virtual int BUTTON_BAR_HEIGHT
		{
			get
			{
				return 40;
			}
		}

		public virtual int BUTTON_BAR_OFFSET_Y
		{
			get
			{
				return 3;
			}
		}

		public virtual int BUTTON_BAR_OFFSET_X
		{
			get
			{
				return 15;
			}
		}

		public virtual Insets GameApp_DIALOGBOX_EXTERIOR_INSETS
		{
			get
			{
				return new Insets(10, 9, 10, 10);
			}
		}

		public virtual Insets GameApp_DIALOGBOX_INTERIOR_INSETS
		{
			get
			{
				return new Insets(16, 15, 16, 16);
			}
		}

		public virtual Insets GameApp_DIALOGBOX_CONTENT_INSETS
		{
			get
			{
				return new Insets(28, 21, 28, 22);
			}
		}

		public virtual int ScoresWidget_aCol1_Rank_Landscape
		{
			get
			{
				return 30;
			}
		}

		public virtual int ScoresWidget_aCol2_Rank_Landscape
		{
			get
			{
				return 240;
			}
		}

		public virtual int ScoresWidget_aCol1_Scores_Landscape
		{
			get
			{
				return 60;
			}
		}

		public virtual int ScoresWidget_aCol2_Scores_Landscape
		{
			get
			{
				return 80;
			}
		}

		public virtual int ScoresWidget_aCol3_Scores_Landscape
		{
			get
			{
				return 300;
			}
		}

		public virtual int ScoresWidget_aCol4_Scores_Landscape
		{
			get
			{
				return 350;
			}
		}

		public virtual int ScoresWidget_aCol1_Rank_Portrait
		{
			get
			{
				return 30;
			}
		}

		public virtual int ScoresWidget_aCol2_Rank_Portrait
		{
			get
			{
				return 30;
			}
		}

		public virtual int ScoresWidget_aCol1_Scores_Portrait
		{
			get
			{
				return 60;
			}
		}

		public virtual int ScoresWidget_aCol2_Scores_Portrait
		{
			get
			{
				return 80;
			}
		}

		public virtual int ScoresWidget_aCol3_Scores_Portrait
		{
			get
			{
				return 300;
			}
		}

		public virtual int ScoresWidget_aCol4_Scores_Portrait
		{
			get
			{
				return 350;
			}
		}

		public virtual int ScoresWidget_Y_Scores_Portrait
		{
			get
			{
				return 50;
			}
		}

		public virtual int ScoresWidget_Y_Scores_Landscape
		{
			get
			{
				return 50;
			}
		}

		public virtual int ScoresWidget_NameClipWidth_Scores_Portrait
		{
			get
			{
				return 120;
			}
		}

		public virtual int ScoresWidget_NameClipWidth_Scores_Landscape
		{
			get
			{
				return 120;
			}
		}

		public virtual double BoardBej2_Shard_X_SIZE
		{
			get
			{
				return 30.0;
			}
		}

		public virtual int BoardBej2_Shard_Size
		{
			get
			{
				return (int)((this.BoardBej2_Shard_X_SIZE * 480.0 + 479.0) / 1024.0);
			}
		}

		public virtual double BoardBej2_Explosion_HalfSize
		{
			get
			{
				return 18.75;
			}
		}

		public virtual int BoardBej2_Explosion_Size(int frame)
		{
			return (int)((double)(frame * 80) * 480.0 / 1024.0);
		}

		public virtual int BoardBej2_Explosion_SizeFull
		{
			get
			{
				return 37;
			}
		}

		public virtual int BoardBej2_ProgressBarLength
		{
			get
			{
				return 310;
			}
		}

		public virtual int BoardBej2_DisplayBar_X_add
		{
			get
			{
				return 9;
			}
		}

		public virtual int BoardBej2_BlackHoleDimension
		{
			get
			{
				return 128;
			}
		}

		public virtual int BoardBej2_HintArrowOffset
		{
			get
			{
				return -3;
			}
		}

		public virtual int BoardBej2_HintArrowOverlayOffset_X
		{
			get
			{
				return 11;
			}
		}

		public virtual int BoardBej2_HintArrowOverlayOffset_Y
		{
			get
			{
				return 10;
			}
		}

		public virtual int BoardBej2_HintArrowOverlaySize
		{
			get
			{
				return 19;
			}
		}

		public virtual int BoardBej2_Sparkle_X_LongLife
		{
			get
			{
				return 6;
			}
		}

		public virtual int BoardBej2_Sparkle_Y_LongLife
		{
			get
			{
				return 3;
			}
		}

		public virtual int BoardBej2_Sparkle_Frame(int frame)
		{
			return (int)((double)(frame * 40) * 480.0 / 1024.0);
		}

		public virtual int BoardBej2_Sparkle_Dimension
		{
			get
			{
				return 18;
			}
		}

		public virtual int BoardBej2_Sparkle_X_MediumLife
		{
			get
			{
				return 4;
			}
		}

		public virtual int BoardBej2_Sparkle_Y_MediumLife
		{
			get
			{
				return 1;
			}
		}

		public virtual int BoardBej2_Sparkle_X_ShortLife
		{
			get
			{
				return 2;
			}
		}

		public virtual int BoardBej2_Sparkle_Y_ShortLife
		{
			get
			{
				return 8;
			}
		}

		public virtual int BoardBej2_Sparkle_HalfSize
		{
			get
			{
				return 9;
			}
		}

		public virtual int BoardBej2_PauseButton_X_Offset_Landscape
		{
			get
			{
				return 28;
			}
		}

		public virtual int BoardBej2_PauseButton_X_Offset_Portrait
		{
			get
			{
				return 158;
			}
		}

		public virtual int BoardBej2_PauseButton_Y_Offset_Landscape
		{
			get
			{
				return 0;
			}
		}

		public virtual int HyperSpace_FireRing_Size
		{
			get
			{
				return 100;
			}
		}

		public virtual float HyperSpace_HyperPoint_Scale
		{
			get
			{
				return 0.46875f;
			}
		}

		public virtual float HyperSpace_TunnelEnd_Scale
		{
			get
			{
				return 0.46875f;
			}
		}

		public virtual int HyperSpace_FireRing_Width(int tunnelEndSize)
		{
			return (tunnelEndSize * 480 + 479) / 1024;
		}

		public virtual TRect HyperSpace_TunnelEnd_Source
		{
			get
			{
				return new TRect(0, 0, 120, 120);
			}
		}

		public virtual double HyperSpace_PortalSize_Scale
		{
			get
			{
				return 0.95;
			}
		}

		public virtual double HyperSpace_PortalSize_ScaleOffset
		{
			get
			{
				return 0.2;
			}
		}

		public virtual int HyperSpace_WarpOffset
		{
			get
			{
				return -20;
			}
		}

		public virtual int EffectOverlay_PortalSize_landscape
		{
			get
			{
				return 480;
			}
		}

		public virtual int EffectOverlay_PortalSize_portait
		{
			get
			{
				return 320;
			}
		}

		public virtual int BIGTEXT_WIDTH
		{
			get
			{
				return 375;
			}
		}

		public virtual int BIGTEXT_HEIGHT
		{
			get
			{
				return 100;
			}
		}

		public virtual int MainMenu_CancelButtonWidth
		{
			get
			{
				return 100;
			}
		}

		public virtual int MainMenu_SparklyLogo_Y_Landscape
		{
			get
			{
				return 13;
			}
		}

		public virtual int MainMenu_SparklyLogo_Y_Portrait
		{
			get
			{
				return 13;
			}
		}

		public virtual int MainMenu_ButtonStartPos_Y_TrialMode_Portrait
		{
			get
			{
				return 160;
			}
		}

		public virtual int MainMenu_ButtonStartPos_Y_NotTrialMode_Portrait
		{
			get
			{
				return 130;
			}
		}

		public virtual int MainMenu_ButtonStartPos_Y_TrialMode_Landscape
		{
			get
			{
				return 110;
			}
		}

		public virtual int MainMenu_ButtonStartPos_Y_NotTrialMode_Landscape
		{
			get
			{
				return 130;
			}
		}

		public virtual int MainMenu_ButtonDistance_Y_Portrait
		{
			get
			{
				return 80;
			}
		}

		public virtual int MainMenu_ButtonDistance_TrialMode
		{
			get
			{
				return 10;
			}
		}

		public virtual int MainMenu_ButtonDistance_NotTrialMode
		{
			get
			{
				return 20;
			}
		}

		public virtual int BoardBej2_TimerBar_Offset_Y_Portrait
		{
			get
			{
				return 6;
			}
		}

		public virtual int BoardBej2_FrameChipOffset
		{
			get
			{
				return 3;
			}
		}

		public virtual int BoardBej2_FrameChip_Warn_Offset
		{
			get
			{
				return 12;
			}
		}

		public virtual int BoardBej2_FrameChip_Warn_Offset2
		{
			get
			{
				return 3;
			}
		}

		public virtual int BoardBej2_IMAGE_GEM_ADD_WHITE_Width
		{
			get
			{
				return 400;
			}
		}

		public virtual int PodButtonFancy_Top_Offset
		{
			get
			{
				return 8;
			}
		}

		public virtual int PodButtonFancy_Bottom_Offset
		{
			get
			{
				return 7;
			}
		}

		public virtual double Scale_Long
		{
			get
			{
				return 0.46875;
			}
		}

		public virtual int BackButton_Width
		{
			get
			{
				return 160;
			}
		}

		public virtual int MoreGamesDialog_Scroller_Size_LandScape
		{
			get
			{
				return 320;
			}
		}

		public virtual int MoreGamesDialog_BackButton_X_Landscape
		{
			get
			{
				return 80;
			}
		}

		public virtual int MoreGamesDialog_BackButton_Y_Landscape
		{
			get
			{
				return 240;
			}
		}

		public virtual int MoreGamesDialog_Scroller_Width_Portrait
		{
			get
			{
				return 320;
			}
		}

		public virtual int MoreGamesDialog_Scroller_Height_Portrait
		{
			get
			{
				return 338;
			}
		}

		public virtual int SplashScreen_Logo_Width
		{
			get
			{
				return 220;
			}
		}

		public virtual int SplashScreen_Logo_Height
		{
			get
			{
				return 220;
			}
		}

		public virtual int BoardBej3_MultiplierGem_Size
		{
			get
			{
				return 44;
			}
		}

		public virtual int BoardBej3_MultiplierGem_Text_Size
		{
			get
			{
				return 25;
			}
		}

		public virtual int BoardBej3_MultiplierGem_Text_Offset
		{
			get
			{
				return 8;
			}
		}

		public virtual int BoardBej3_MultiplierGem_Text_Offset_UI
		{
			get
			{
				return 10;
			}
		}

		public virtual int BoardBej3_Bar_Length
		{
			get
			{
				return 320;
			}
		}

		public virtual int BoardBej3_Time_Position_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 145;
				}
				return 44;
			}
		}

		public virtual int BoardBej3_Time_Position_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 391;
				}
				return 226;
			}
		}

		public virtual int BoardBej3_LastHurrah_Position_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 98;
				}
				return 3;
			}
		}

		public virtual int BoardBej3_LastHurrah_Position_Offset_X
		{
			get
			{
				return 4;
			}
		}

		public virtual int BoardBej3_Blazing_Position_Y_Landscape
		{
			get
			{
				return 216;
			}
		}

		public virtual int BoardBej3_Blazing2_Position_Y_Landscape
		{
			get
			{
				return 236;
			}
		}

		public virtual float BoardBej3_LaserGem_MaxOffset_1
		{
			get
			{
				return 10f;
			}
		}

		public virtual float BoardBej3_LaserGem_MaxOffset_2
		{
			get
			{
				return 5f;
			}
		}

		public virtual int Board_Multiplier_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 280;
				}
				return 43;
			}
		}

		public virtual int BoardBej3_SpeedBonus_Y
		{
			get
			{
				return 40;
			}
		}

		public virtual int PointsBej3_Min_Y
		{
			get
			{
				return 75;
			}
		}

		public virtual int LASER_GLOW_LENGTH
		{
			get
			{
				return 80;
			}
		}

		public virtual int LASER_GLOW_OFFSET
		{
			get
			{
				return 20;
			}
		}

		public virtual int LASER_GEM_LENGTH
		{
			get
			{
				return 60;
			}
		}

		public virtual int LightningZap_HorizontalZap_Start_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 0;
				}
				return 128;
			}
		}

		public virtual int LightningZap_HorizontalZap_End_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 280;
				}
				return 420;
			}
		}

		public virtual int LightningZap_VerticalZap_Start_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 40;
				}
				return 0;
			}
		}

		public virtual int LightningZap_VerticalZap_End_Y
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 333;
				}
				return 280;
			}
		}

		public virtual int BoardBej3_SpeedBonus_Text_Offset_X
		{
			get
			{
				return 68;
			}
		}

		public virtual int BoardBej3_SpeedBonus_Text_Offset_Y
		{
			get
			{
				return 22;
			}
		}

		public virtual int BoardBej3_Warp_Text_Target_X
		{
			get
			{
				return 300;
			}
		}

		public virtual int BoardBej3_Warp_Text_Target_Y
		{
			get
			{
				return 400;
			}
		}

		public virtual int BoardBej3_Frame_Top_Offset_Landscape
		{
			get
			{
				return -25;
			}
		}

		public virtual int BoardBej3_Frame_Bottom_Offset_Landscape
		{
			get
			{
				return 4;
			}
		}

		public virtual int BoardBej3_Frame_Top_Offset_Portrait
		{
			get
			{
				return 10;
			}
		}

		public virtual int BoardBej3_Frame_Bottom_Offset_Portrait
		{
			get
			{
				return 4;
			}
		}

		public virtual int BoardBej3_MenuHintButton_X_Portrait
		{
			get
			{
				return 250;
			}
		}

		public virtual int BoardBej3_MenuHintButton_X_Landscape
		{
			get
			{
				return 63;
			}
		}

		public virtual int BoardBej3_MenuHintButton_Offset_Y
		{
			get
			{
				return 0;
			}
		}

		public virtual int BoardBej3_ScoreText_Offset_Y
		{
			get
			{
				return 15;
			}
		}

		public virtual int MenuHintButton_Menu_Y
		{
			get
			{
				return 50;
			}
		}

		public virtual int MenuHintButton_MenuButton_X
		{
			get
			{
				return 29;
			}
		}

		public virtual int MenuHintButton_MenuButton_Y
		{
			get
			{
				return 88;
			}
		}

		public virtual int MenuHintButton_Double_MenuButton_X
		{
			get
			{
				return 65;
			}
		}

		public virtual int MenuHintButton_Double_MenuButton_Y
		{
			get
			{
				return 75;
			}
		}

		public virtual float MenuHintButton_Menu_Font_Scale
		{
			get
			{
				return 0.8f;
			}
		}

		public virtual int MenuHintButton_HintButton_X
		{
			get
			{
				return 17;
			}
		}

		public virtual int MenuHintButton_HintButton_Y
		{
			get
			{
				return 20;
			}
		}

		public virtual int MenuHintButton_Double_HintButton_X
		{
			get
			{
				return 27;
			}
		}

		public virtual int MenuHintButton_Double_HintButton_Y
		{
			get
			{
				return 15;
			}
		}

		public virtual int MenuHintButton_Double_LeftButton_X
		{
			get
			{
				return 11;
			}
		}

		public virtual int MenuHintButton_Double_LeftButton_Y
		{
			get
			{
				return 75;
			}
		}

		public virtual int MenuHintButton_Double_Endless_X
		{
			get
			{
				return 5;
			}
		}

		public virtual int MenuHintButton_Double_Endless_Y
		{
			get
			{
				return 5;
			}
		}

		public virtual int MenuHintButton_Double_Trial_X
		{
			get
			{
				return 5;
			}
		}

		public virtual int MenuHintButton_Double_Trial_Y
		{
			get
			{
				return 17;
			}
		}

		public virtual int ScoreBoardWidget_Score_Y
		{
			get
			{
				return 50;
			}
		}

		public virtual int Piece_FlameGem_Size
		{
			get
			{
				return 48;
			}
		}

		public virtual int Piece_FlameGem_Offset
		{
			get
			{
				return 4;
			}
		}

		public virtual int Piece_HyperCube_Size
		{
			get
			{
				return 48;
			}
		}

		public virtual int Piece_HyperCube_Offset
		{
			get
			{
				return 4;
			}
		}

		public virtual int Piece_LASER_GEM_OFFSET
		{
			get
			{
				return 10;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_X_Landscape
		{
			get
			{
				return 5;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_X2_Landscape
		{
			get
			{
				return 15;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_Y_Landscape
		{
			get
			{
				return 26;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_Y2_Landscape
		{
			get
			{
				return 25;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_X_Portrait
		{
			get
			{
				return 25;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_X2_Portrait
		{
			get
			{
				return 26;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_Y_Portrait
		{
			get
			{
				return 1;
			}
		}

		public virtual int BoardBej3_ProgressBar_Clip_Y2_Portrait
		{
			get
			{
				return 13;
			}
		}

		public virtual TRect AchievementScreen_Bounds
		{
			get
			{
				return new TRect(15, 30, (GlobalStaticVars.gSexyAppBase.IsLandscape() ? 480 : 320) - 30, (GlobalStaticVars.gSexyAppBase.IsLandscape() ? 320 : 480) - 160);
			}
		}

		public virtual float AchievementScreen_Picture_Scale
		{
			get
			{
				return 0.6666667f;
			}
		}

		public virtual TRect Leaderboard_Bounds
		{
			get
			{
				return new TRect(20, 30, -30, -50);
			}
		}

		public virtual TRect Leaderboard_Frame_Bounds
		{
			get
			{
				return new TRect(20, 30, -30, -50);
			}
		}

		public virtual int LeaderBoard_Button_Width
		{
			get
			{
				return 230;
			}
		}

		public virtual int LiveRank_Frame_Offset_Y
		{
			get
			{
				return 15;
			}
		}

		public virtual int LiveRank_Frame_Offset_Top
		{
			get
			{
				return 5;
			}
		}

		public virtual int LiveRank_Frame_Offset_Bottom
		{
			get
			{
				return 60;
			}
		}

		public virtual int LiveRank_Heading_Y
		{
			get
			{
				return 20;
			}
		}

		public virtual int LiveRank_Row_Distance_Portrait
		{
			get
			{
				return 10;
			}
		}

		public virtual int LiveRank_Row_Distance_Landscape
		{
			get
			{
				return 8;
			}
		}

		public virtual int LiveRank_Text_Y_Portrait
		{
			get
			{
				return 100;
			}
		}

		public virtual int LiveRank_Text_Y_Landscape
		{
			get
			{
				return 90;
			}
		}

		public virtual int FrameWidget_Text_Y
		{
			get
			{
				return -1;
			}
		}

		public virtual int FrameWidget_Image_Offset_X
		{
			get
			{
				return 10;
			}
		}

		public virtual int FrameWidget_Image_Offset_Y
		{
			get
			{
				return 5;
			}
		}

		public virtual int LeaderboardListWidget_Rank_X
		{
			get
			{
				return 30;
			}
		}

		public virtual int LeaderboardListWidget_Name_X
		{
			get
			{
				return 70;
			}
		}

		public virtual int LeaderboardListWidget_Picture_X
		{
			get
			{
				return 100;
			}
		}

		public virtual int LeaderboardListWidget_Name_Y
		{
			get
			{
				return 15;
			}
		}

		public virtual int LeaderboardListWidget_Score_X
		{
			get
			{
				return 71;
			}
		}

		public virtual int LeaderboardListWidget_Score_Y
		{
			get
			{
				return 40;
			}
		}

		public virtual int LeaderboardListWidget_Row_Height
		{
			get
			{
				return 70;
			}
		}

		public virtual int LiveLeaderboard_Box_Offset_Y
		{
			get
			{
				return 15;
			}
		}

		public virtual int LiveLeaderboard_Frame_Offset
		{
			get
			{
				return 20;
			}
		}

		public virtual int LiveLeaderboard_Row_Distance_Portrait
		{
			get
			{
				return 20;
			}
		}

		public virtual int LiveLeaderboard_Row_Distance_Landscape
		{
			get
			{
				return 20;
			}
		}

		public virtual int LeaderboardListWidget_Heading_Text_Offset
		{
			get
			{
				return 20;
			}
		}

		public virtual int AchievementScreen_Image_X
		{
			get
			{
				return 20;
			}
		}

		public virtual int AchievementScreen_Name_Y
		{
			get
			{
				return 10;
			}
		}

		public virtual int AchievementScreen_Score_X
		{
			get
			{
				if (!GlobalStaticVars.gSexyAppBase.IsLandscape())
				{
					return 200;
				}
				return 350;
			}
		}

		public virtual TRect AchievementScreen_Description_Box
		{
			get
			{
				return new TRect(80, 35, GlobalStaticVars.gSexyAppBase.IsLandscape() ? 350 : 190, 100);
			}
		}

		public virtual int AchievementScreen_Row_Height
		{
			get
			{
				return 100;
			}
		}

		public virtual int AlertInsetDistance_X
		{
			get
			{
				return 20;
			}
		}

		public virtual int AlertInsetDistance_Y
		{
			get
			{
				return 25;
			}
		}

		public virtual int SubHeader_Stretch_Offset
		{
			get
			{
				return 53;
			}
		}

		public virtual int GameOverDialog_Heading_EdgeOffset_X_Landscape
		{
			get
			{
				return 300;
			}
		}

		public virtual int GameOverDialog_Box_EdgeOffset_X_Landscape
		{
			get
			{
				return 200;
			}
		}

		public virtual int GameOverDialog_Box_Offset_X_Landscape
		{
			get
			{
				return 200;
			}
		}

		public virtual int GameOverDialog_Box_Offset_Y_Landscape
		{
			get
			{
				return 10;
			}
		}

		public virtual int GameOverDialog_Heading_Y
		{
			get
			{
				return 60;
			}
		}

		public virtual int GameOverDialog_Text_Y
		{
			get
			{
				return 100;
			}
		}

		public virtual float BackgroundWidget_Image_Scale
		{
			get
			{
				return 0.6666667f;
			}
		}

		public virtual float S(float num)
		{
			return (float)((double)num * 480.0 / 1024.0);
		}

		public virtual float M(float num)
		{
			return num;
		}

		public virtual float MS(float num)
		{
			return this.S(this.M(num));
		}

		public virtual string MP(string m)
		{
			return m;
		}

		public virtual float Gravity
		{
			get
			{
				return 0.12f;
			}
		}

		public virtual float SPEED_TIME1
		{
			get
			{
				return this.M(175f);
			}
		}

		public virtual float SPEED_TIME2
		{
			get
			{
				return this.M(275f);
			}
		}

		public virtual int SPEED_START_THRESHOLD
		{
			get
			{
				return (int)this.M(6f);
			}
		}

		public virtual float SPEED_TIME_LEFT
		{
			get
			{
				return (float)((int)this.M(180f));
			}
		}

		public virtual float SPEED_TIME_RIGHT
		{
			get
			{
				return (float)((int)this.M(100f));
			}
		}

		public virtual Point BackBufferSize
		{
			get
			{
				return new Point(320, 480);
			}
		}

		public static Constants mConstants;

		public static DisplayOrientation SupportedOrientations = 7;

		public static Color SCROLLER_BACK_COLOR = Constants.FRAME_BACK_COLOUR(255f);

		public static readonly float BUTTON_WIDTH_FACTOR_LANDSCAPE = 0.7f;

		public static Constants.LanguageIndex mLanguage = (Constants.LanguageIndex)0;

		public enum LanguageIndex
		{
			en = 1,
			fr,
			de,
			es,
			it
		}

		public enum StringId
		{
			MAINMENU_TITLE_CLASSIC,
			MAINMENU_TITLE_ACTION,
			MAINMENU_TITLE_ENDLESS
		}
	}
}
