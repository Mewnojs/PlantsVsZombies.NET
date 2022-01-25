using System;
using Lawn;
using Microsoft.Xna.Framework;

namespace Sexy
{
    internal class Constants : FrameworkConstants
    {
        public static void Load320x480()
        {
            Constants.S = 0.53333336f;
            Constants.IS = 1.875f;
            FrameworkConstants.Font_Scale = 0.6666667f;
            Constants.Board_Offset_AspectRatio_Correction = 0;
            Constants.Loaded = true;
            Constants.BackBufferSize = new Point(320, 480);
            Constants.ImageSubPath = "\\320x480\\";
            Constants.BOARD_WIDTH = 480;
            Constants.BOARD_HEIGHT = 320;
            Constants.BOARD_EDGE = 72;
            Constants.BOARD_OFFSET = 119;
            Constants.BOARD_EXTRA_ROOM = 100;
            Constants.HIGH_GROUND_HEIGHT = 30;
            Constants.GRIDSIZEX = 9;
            Constants.MAX_GRIDSIZEY = 6;
            Constants.MORE_GAMES_ORIGIN_X = 0;
            Constants.MAIN_MENU_ORIGIN_X = 354;
            Constants.ACHIEVEMENTS_ORIGIN_X = 354;
            Constants.QUICKPLAY_ORIGIN_X = 834;
            Constants.MORE_GAMES_PLANK_HEIGHT = 100;
            Constants.MORE_GAMES_PLANK_WIDTH = 320;
            Constants.MORE_GAMES_ITEM_GAP = 2;
            Constants.SMALL_SEEDPACKET_WIDTH = 54;
            Constants.SMALL_SEEDPACKET_HEIGHT = 35;
            Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET = 320;
            Constants.SCROLL_AREA_OFFSET_X = 67;
            Constants.SCROLL_AREA_OFFSET_Y = 28;
            Constants.DIALOG_HEADER_OFFSET = 24;
            Constants.WIDE_BOARD_WIDTH = 900;
            Constants.ImageWidth = 1400;
            Constants.LAWN_XMIN = 140;
            Constants.LAWN_YMIN = 80;
            Constants.TitleScreen_ReanimStart_X = 60;
            Constants.GameSelector_Width = 1311;
            Constants.GameSelector_Height = 640;
            Constants.GameSelector_AdventureButton_X = 228;
            Constants.GameSelector_MiniGameButton_X = 236;
            Constants.GameSelector_MiniGameButton_Y = (int)Constants.InvertAndScale(94f);
            Constants.GameSelector_PuzzleButton_X = Constants.GameSelector_MiniGameButton_X + 4;
            Constants.GameSelector_PuzzleButton_Y = (int)Constants.InvertAndScale(160f);
            Constants.GameSelector_OptionsButton_X = 340;
            Constants.GameSelector_OptionsButton_Y = (int)Constants.InvertAndScale(200f);
            Constants.GameSelector_ZenGardenButton_X = 260;
            Constants.GameSelector_ZenGardenButton_Y = (int)Constants.InvertAndScale(200f);
            Constants.GameSelector_AlmanacButton_X = 255;
            Constants.GameSelector_StoreButton_X = 212;
            Constants.GameSelector_StoreButton_Y = (int)Constants.InvertAndScale(168f);
            Constants.GameSelector_AchievementsButton_X = 126;
            Constants.GameSelector_AchievementsButton_Y = (int)Constants.InvertAndScale(250f);
            Constants.GameSelector_AchievementsStatue_X = 234;
            Constants.GameSelector_AchievementsStatue_Y = (int)Constants.InvertAndScale(170f);
            Constants.GameSelector_MoreWaysToPlay_MiniGames_Y = 276;
            Constants.GameSelector_MoreWaysToPlay_VaseBreaker_X = 65;
            Constants.GameSelector_MoreWaysToPlay_IZombie_X = 161;
            Constants.GameSelector_MoreWaysToPlay_Back_X = 174;
            Constants.GameSelector_PlayerName_Pos = new Point(112, -111);
            Constants.GameSelector_LevelNumber_1_Pos = new Point(306, 19);
            Constants.GameSelector_LevelNumber_2_Pos = new Point(324, 20);
            Constants.GameSelector_LevelNumber_3_Pos = new Point(333, 21);
            Constants.GameSelector_LevelNumber_Bar = new Rectangle(320, 28, 3, 1);
            Constants.GameSelector_LevelNumber_ButtonDown_Offset = 3;
            Constants.GameSelector_Update_Offset = 69;
            Constants.LeaderboardScreen_Vasebreaker_Button_X = 621;
            Constants.LeaderboardScreen_Vasebreaker_Button_Y = 335;
            Constants.LeaderboardScreen_IZombie_Button_X = 129;
            Constants.LeaderboardScreen_IZombie_Button_Y = 307;
            Constants.LeaderboardScreen_PileBase_X = 331;
            Constants.LeaderboardScreen_PileBase_Y = 53;
            Constants.Leaderboard_IZombie_Score_X = 198;
            Constants.Leaderboard_IZombie_Score_Y = 290;
            Constants.Leaderboard_Vasebreaker_Score_X = 698;
            Constants.Leaderboard_Vasebreaker_Score_Y = 322;
            Constants.Leaderboard_Pile_1_X = 297;
            Constants.LeaderboardDialog_CannotConnect_Rect = new TRect(100, 100, 300, 300);
            Constants.LeaderboardScreen_EdgeOfSpace_Overlay_Offset = 30;
            Constants.SEED_PACKET_HORIZ_GAP = 2;
            Constants.SEED_PACKET_VERT_GAP = 1;
            Constants.SeedPacket_Selector_Pos = new Point(-2, 2);
            Constants.AchievementWidget_ROW_HEIGHT = 82;
            Constants.AchievementWidget_ROW_START = 149;
            Constants.AchievementWidget_HOLE_DEPTH = 128;
            Constants.AchievementWidget_GAMERSCORE_POS = new Point(100, 100);
            Constants.AchievementWidget_BackButton_X = 52;
            Constants.AchievementWidget_BackButton_Y = 5;
            Constants.AchievementWidget_BackButton_Rect = new TRect(54, 0, 105, 80);
            Constants.AchievementWidget_Background_Offset_Y = 0;
            Constants.AchievementWidget_Description_Box = new Rectangle(180, 30, 212, 60);
            Constants.AchievementWidget_Pipe_Offset = new Point(27, 13);
            Constants.AchievementWidget_Worm_Offset = new Point(382, 18);
            Constants.AchievementWidget_ZombieWorm_Offset = new Point(29, 74);
            Constants.AchievementWidget_GemLeft_Offset = new Point(86, 43);
            Constants.AchievementWidget_GemRight_Offset = new Point(263, 43);
            Constants.AchievementWidget_Fossile_Offset = new Point(131, 49);
            Constants.AchievementWidget_Image_Pos = new Point(10, 10);
            Constants.AchievementWidget_Image_Size = (int)(FrameworkConstants.Font_Scale * 64f);
            Constants.AchievementWidget_Name_Pos = new Point(250, 10);
            Constants.AchievementWidget_Name_MaxWidth = 300;
            Constants.QuickPlayWidget_Thumb_X = 26;
            Constants.QuickPlayWidget_Thumb_Y = 22;
            Constants.QuickPlayWidget_Bungee_Y = -40;
            Constants.SeedChooserScreen_MenuButton_X = 390;
            Constants.SeedChooserScreen_Background_Top = new Point(58, 2);
            Constants.SeedChooserScreen_Background_Middle = new Point(58, 31);
            Constants.SeedChooserScreen_Background_Middle_Height = 220;
            Constants.SeedChooserScreen_Background_Bottom = new Point(58, 251);
            Constants.SeedChooserScreen_Gradient_Top = new Rectangle(0, 24, 222, 12);
            Constants.SeedChooserScreen_Gradient_Bottom = new Rectangle(-1, 246, 223, 12);
            Constants.SeedPacket_Cost = new Point(29, 18);
            Constants.SeedPacket_Cost_IZombie = new Point(31, 18);
            Constants.SeedPacket_CostText_Pos = new Point(51, 15);
            Constants.SeedPacket_CostText_IZombie_Pos = new Point(55, 15);
            Constants.ImitaterDialog_Size = new Point(0, 155);
            Constants.ImitaterDialog_ScrollWidget_Offset_X = 0;
            Constants.ImitaterDialog_ScrollWidget_Y = 70;
            Constants.ImitaterDialog_ScrollWidget_ExtraWidth = 13;
            Constants.ImitaterDialog_Height = 152;
            Constants.ImitaterDialog_BottomGradient_Y = 143;
            Constants.CutScene_ReadySetPlant_Pos = new Point(240, 173);
            Constants.CutScene_LogoEndPos = 255;
            Constants.CutScene_LogoBackRect_Height = 150;
            Constants.CutScene_LogoEnd_Particle_Pos = new Point(400, 300);
            Constants.CutScene_ExtraRoom_1_Particle_Pos = new Point(35, 348);
            Constants.CutScene_ExtraRoom_2_Particle_Pos = new Point(35, 246);
            Constants.CutScene_ExtraRoom_3_Particle_Pos = new Point(35, 459);
            Constants.CutScene_ExtraRoom_4_Particle_Pos = new Point(32, 150);
            Constants.CutScene_ExtraRoom_5_Particle_Pos = new Point(32, 551);
            Constants.CutScene_SodRoll_1_Pos = -102;
            Constants.CutScene_SodRoll_2_Pos = 111;
            Constants.CutScene_SodRoll_3_Pos = new Point(-3, -198);
            Constants.CutScene_SodRoll_4_Pos = new Point(-3, 203);
            Constants.CutScene_Upsell_TerraCotta_Arrow = new Point(592, 240);
            Constants.CutScene_Upsell_TerraCotta_Pot = new Point(565, 360);
            Constants.ConveyorBeltClipRect = new TRect(0, 5, 54, 310);
            Constants.StoreScreen_BackButton_X = 230;
            Constants.StoreScreen_BackButton_Y = 257;
            Constants.StoreScreen_Car_X = 120;
            Constants.StoreScreen_Car_Y = 50;
            Constants.StoreScreen_PrevButton_X = 158;
            Constants.StoreScreen_PrevButton_Y = 196;
            Constants.StoreScreen_NextButton_X = 380;
            Constants.StoreScreen_NextButton_Y = 196;
            Constants.StoreScreen_HatchOpen_X = 160;
            Constants.StoreScreen_HatchOpen_Y = -22;
            Constants.StoreScreen_HatchClosed_X = 150;
            Constants.StoreScreen_HatchClosed_Y = 49;
            Constants.StoreScreen_CarNight_X = 120;
            Constants.StoreScreen_CarNight_Y = 50;
            Constants.StoreScreen_StoreSign_X = 269;
            Constants.StoreScreen_StoreSign_Y_Min = -150;
            Constants.StoreScreen_StoreSign_Y_Max = -10;
            Constants.StoreScreen_Coinbank_X = 340;
            Constants.StoreScreen_Coinbank_Y = 270;
            Constants.StoreScreen_Coinbank_TextOffset = new Point(116, 5);
            Constants.StoreScreen_ItemOffset_1_X = 234;
            Constants.StoreScreen_ItemOffset_1_Y = 40;
            Constants.StoreScreen_ItemOffset_2_X = 214;
            Constants.StoreScreen_ItemOffset_2_Y = 109;
            Constants.StoreScreen_ItemSize = 54;
            Constants.StoreScreen_ItemSize_Offset = 4;
            Constants.StoreScreen_PriceTag_X = 3;
            Constants.StoreScreen_PriceTag_Y = 70;
            Constants.StoreScreen_PriceTag_Text_Offset_X = 23;
            Constants.StoreScreen_PriceTag_Text_Offset_Y = 67;
            Constants.StoreScreen_ComingSoon_X = 60;
            Constants.StoreScreen_ComingSoon_Y = 70;
            Constants.StoreScreen_SoldOut_Width = 50;
            Constants.StoreScreen_SoldOut_Y = 30;
            Constants.StoreScreen_SoldOut_Height = 60;
            Constants.StoreScreen_PacketUpgrade_X = 7;
            Constants.StoreScreen_PacketUpgrade_Y = 16;
            Constants.StoreScreen_PacketUpgrade_Text_Size = new Rectangle(0, 12, 55, 70);
            Constants.StoreScreen_RetardedDave_Offset_X = -40;
            Constants.StoreScreen_FirstAidNut_Offset_Y = 22;
            Constants.StoreScreen_PoolCleaner_Offset_X = 16;
            Constants.StoreScreen_PoolCleaner_Offset_Y = 34;
            Constants.StoreScreen_Rake_Offset_X = -10;
            Constants.StoreScreen_Rake_Offset_Y = 34;
            Constants.StoreScreen_RoofCleaner_Offset_X = 10;
            Constants.StoreScreen_RoofCleaner_Offset_Y = 42;
            Constants.StoreScreen_Imitater_Offset_X = -1;
            Constants.StoreScreen_Imitater_Offset_Y = 34;
            Constants.StoreScreen_Default_Offset_Y = 34;
            Constants.StoreScreen_MouseRegion = new Point(50, 87);
            Constants.StoreScreen_Dialog = new Rectangle(120, 32, 322, 280);
            Constants.StoreScreen_PotPlant_Offset = new Point(25, 67);
            Constants.StoreScreenMushroomGardenOffsetX = 15;
            Constants.StoreScreenAquariumGardenOffsetX = 15;
            Constants.StoreScreenGloveOffsetY = 20;
            Constants.StoreScreenWheelbarrowOffsetY = 20;
            Constants.StoreScreenBugSprayOffsetX = 17;
            Constants.StoreScreenBugSprayOffsetY = 20;
            Constants.StoreScreenPhonographOffsetY = 20;
            Constants.StoreScreenPlantFoodOffsetX = 9;
            Constants.StoreScreenPlantFoodOffsetY = 20;
            Constants.StoreScreenWateringCanOffsetX = 6;
            Constants.StoreScreenWateringCanOffsetY = 20;
            Constants.NewOptionsDialog_FXLabel_X = 240;
            Constants.NewOptionsDialog_FXLabel_Y = 92;
            Constants.NewOptionsDialog_MusicLabel_X = 240;
            Constants.NewOptionsDialog_MusicLabel_On_Y = 65;
            Constants.NewOptionsDialog_MusicLabel_Off_Y = 70;
            Constants.NewOptionsDialog_VibrationLabel_X = 240;
            Constants.NewOptionsDialog_VibrationLabel_Y = 125;
            Constants.NewOptionsDialog_VibrationLabel_MaxWidth = 200;
            Constants.NewOptionsDialog_FullScreenOffset = 25;
            Constants.NewOptionsDialog_FX_Offset = 15;
            Constants.NewOptionsDialog_Music_Offset = 5;
            Constants.NewOptionsDialog_Version_Low_Y = 185;
            Constants.NewOptionsDialog_Version_High_Y = 195;
            Constants.RetardedDave_Bubble_Tip_Offset = 4;
            Constants.RetardedDave_Bubble_Offset_Shop = new Point(-190, -78);
            Constants.RetardedDave_Bubble_Offset_Board = new Point(-150, -48);
            Constants.RetardedDave_Bubble_Size = 200;
            Constants.RetardedDave_Bubble_Rect = new TRect(8, 6, 0, 144);
            Constants.RetardedDave_Bubble_TapToContinue_Y = 142;
            Constants.PlantGallerySize = new Point(234, 492);
            Constants.ZombieGallerySize = new Point(249, 712);
            Constants.AlmanacHeaderY = 4;
            Constants.Almanac_PlantsButtonRect = new TRect(40, 100, 180, 160);
            Constants.Almanac_ZombiesButtonRect = new TRect(260, 100, 180, 160);
            Constants.Almanac_CloseButtonRect = new TRect(396, 290, 89, 26);
            Constants.Almanac_IndexButtonRect = new TRect(265, 290, 89, 26);
            Constants.Almanac_IndexPlantPos = new Point(110, 160);
            Constants.Almanac_IndexZombiePos = new Point(320, 150);
            Constants.Almanac_IndexPlantTextPos = new Point(Constants.Almanac_PlantsButtonRect.mX + Constants.Almanac_PlantsButtonRect.mWidth / 2, Constants.Almanac_PlantsButtonRect.mY + Constants.Almanac_PlantsButtonRect.mHeight - 50);
            Constants.Almanac_IndexZombieTextPos = new Point(Constants.Almanac_ZombiesButtonRect.mX + Constants.Almanac_ZombiesButtonRect.mWidth / 2, Constants.Almanac_ZombiesButtonRect.mY + Constants.Almanac_ZombiesButtonRect.mHeight - 50);
            Constants.Almanac_PlantScrollRect = new TRect(28, 50, 237, 245);
            Constants.Almanac_ZombieScrollRect = new TRect(17, 50, 250, 245);
            Constants.Almanac_DescriptionScrollRect = new TRect(282, 170, 183, 111);
            Constants.Almanac_PlantTopGradientY = 46;
            Constants.Almanac_ZombieTopGradientY = 48;
            Constants.Almanac_BottomGradientY = 288;
            Constants.Almanac_PlantGradientWidth = 222;
            Constants.Almanac_ZombieGradientWidth = 240;
            Constants.Almanac_ZombieStoneRect = new TRect(267, 1, 212, 310);
            Constants.Almanac_BackgroundPosition = new Point(320, 40);
            Constants.Almanac_ZombieClipRect = new TRect(-22, -30, 109, 109);
            Constants.Almanac_NavyRect = new TRect(306, 27, 134, 134);
            Constants.Almanac_NamePosition = new Point(373, 0);
            Constants.Almanac_ClayRect = new TRect(270, 4, 204, 310);
            Constants.Almanac_BrownRect = new TRect(306, 27, 133, 133);
            Constants.Almanac_ZombieSpace = new Point(82, 78);
            Constants.Almanac_ZombieOffset = new Point(0, 5);
            Constants.Almanac_BossPosition = new Point(123, 629);
            Constants.Almanac_ImpPosition = new Point(41, 629);
            Constants.Almanac_ImitatorPosition = new Point(Constants.PlantGallerySize.X / 2 - 28, 450);
            Constants.Almanac_SeedSpace = new Point(2, 2);
            Constants.Almanac_SeedOffset = new Point(0, 5);
            Constants.Almanac_ZombiePosition = new Point(640, 130);
            Constants.Almanac_PlantPosition = new Point(350, 70);
            Constants.Almanac_Text_Scale = 0.8f;
            Constants.Almanac_PlantsHeader_Pos = new Point(2, 2);
            Constants.Almanac_ZombieHeader_Pos = new Point(0, 2);
            Constants.Almanac_ItemName_MaxWidth = 230;
            Constants.Zombie_StartOffset = 120f;
            Constants.Zombie_StartRandom_Offset = 40f;
            Constants.AwardScreen_Note_Credits_Background = new Rectangle(-350, -180, 1400, 600);
            Constants.AwardScreen_Note_Credits_Paper = new Point((int)(75f * Constants.S) + 25, (int)(60f * Constants.S));
            Constants.AwardScreen_Note_Credits_Text = new Rectangle((int)(149f * Constants.S) + 25, (int)(103f * Constants.S), (int)(475f * Constants.S), (int)(325f * Constants.S));
            Constants.AwardScreen_Note_Help_Background = new Rectangle(-350, -150, 1400, 600);
            Constants.AwardScreen_Note_Help_Paper = new Point(80, 42);
            Constants.AwardScreen_Note_Help_Text = new Point(106, 70);
            Constants.AwardScreen_Note_1_Background = new Rectangle(-350, -150, 1400, 600);
            Constants.AwardScreen_Note_1_Paper = new Point(70, 30);
            Constants.AwardScreen_Note_1_Text = new Point(96, 58);
            Constants.AwardScreen_Note_Message = new Point(240, 0);
            Constants.AwardScreen_Note_2_Background = new Rectangle(-350, -150, 1400, 600);
            Constants.AwardScreen_Note_2_Paper = new Point(76, 30);
            Constants.AwardScreen_Note_2_Text = new Point(96, 58);
            Constants.AwardScreen_Note_3_Background = new Rectangle(-350, -150, 1400, 600);
            Constants.AwardScreen_Note_3_Paper = new Point(70, 26);
            Constants.AwardScreen_Note_3_Text = new Point(92, 46);
            Constants.AwardScreen_Note_4_Background = new Rectangle(-350, -150, 1400, 600);
            Constants.AwardScreen_Note_4_Paper = new Point(70, 30);
            Constants.AwardScreen_Note_4_Text = new Point(90, 52);
            Constants.AwardScreen_Note_Final_Background = new Rectangle(-350, -150, 1400, 600);
            Constants.AwardScreen_Note_Final_Paper = new Point(70, 30);
            Constants.AwardScreen_Note_Final_Text = new Point(96, 58);
            Constants.AwardScreen_Bacon = (Constants.AwardScreen_Taco = (Constants.AwardScreen_CarKeys = new Point(123, 140)));
            Constants.AwardScreen_WateringCan = new Point(123, 140);
            Constants.AwardScreen_Almanac = new Point(123, 120);
            Constants.AwardScreen_Trophy = new Point((int)(400f * Constants.S), (int)(137f * Constants.S));
            Constants.AwardScreen_Shovel = new Point(123, 132);
            Constants.AwardScreen_MenuButton = new Point(345, 280);
            Constants.AwardScreen_ClayTablet = new TRect(31, 54, 418, 190);
            Constants.AwardScreen_TitlePos = new Point(240, 13);
            Constants.AwardScreen_GroundDay_Pos = new Point(70, 112);
            Constants.AwardScreen_BrownRect = new TRect(56, 97, 133, 133);
            Constants.AwardScreen_BottomMessage_Pos = new Point(240, 56);
            Constants.AwardScreen_BottomText_Rect_Text = new TRect(208, 114, 218, 100);
            Constants.AwardScreen_BottomText_Rect_NoText = new TRect(131, 78, 218, 140);
            Constants.AwardScreen_CreditsButton = new TRect(50, 278, 140, 32);
            Constants.AwardScreen_CreditsButton_Offset = new Point(20, -5);
            Constants.AwardScreen_Seed_Pos = new Point(97, 145);
            Constants.AwardScreen_ContinueButton_Offset = 35;
            Constants.AwardScreen_AchievementImage_Pos = new Point(80, 4);
            Constants.AwardScreen_AchievementName_Pos = new Point(160, 25);
            Constants.AwardScreen_AchievementDescription_Rect = new Rectangle(160, 30, 212, 60);
            Constants.CreditScreen_ReplayButton = new Rectangle(10, 277, 125, 32);
            Constants.CreditScreen_ReplayButton_TextOffset = new Point(15, -5);
            Constants.CreditScreen_MainMenu = new Rectangle(330, 282, 140, 30);
            Constants.CreditScreen_TextStart = 120;
            Constants.CreditScreen_TextEnd = 200;
            Constants.CreditScreen_TextClip = 250;
            Constants.CreditScreen_LeftText_X = 0;
            Constants.CreditScreen_RightText_X = 245;
            Constants.CreditScreen_LeftRight_Text_Width = 230;
            Constants.ConfirmPurchaseDialog_Background = new TRect(26, 70, 262, 140);
            Constants.ConfirmPurchaseDialog_Item_Pos = new Point(42, 80);
            Constants.ConfirmPurchaseDialog_Text = new TRect(120, 70, 150, 140);
            Constants.LawnDialog_Insets = new Insets(40, 15, 46, 20);
            Constants.UILevelPosition = new Point(476, 297);
            Constants.UIMenuButtonPosition = new Point(390, 2);
            Constants.UIMenuButtonWidth = 90;
            Constants.UISunBankPositionX = 57;
            Constants.UISunBankTextOffset = new Point(60, 2);
            Constants.UIShovelButtonPosition = new Point(156, 2);
            Constants.UIProgressMeterPosition = new Point(222, 2);
            Constants.UIProgressMeterHeadEnd = 135;
            Constants.UIProgressMeterBarEnd = 143;
            Constants.UICoinBankPosition = new Point(66, 319);
            Constants.Board_Cutscene_ExtraScroll = 0;
            Constants.Board_SunCoinRange = 550;
            Constants.Board_SunCoin_CollectTarget = new Point(100, 0);
            Constants.Board_Cel_Y_Values_Normal = new int[]
            {
                80,
                177,
                280,
                380,
                470,
                570,
                610
            };
            Constants.Board_Cel_Y_Values_Pool = new int[]
            {
                80,
                170,
                275,
                360,
                447,
                522,
                610
            };
            Constants.Board_Cel_Y_Values_ZenGarden = new int[]
            {
                80,
                177,
                280,
                380,
                470,
                570,
                610
            };
            Constants.Board_GameOver_Interior_Overlay_1 = new Point(-13, 121);
            Constants.Board_GameOver_Interior_Overlay_2 = new Point(-14, 108);
            Constants.Board_GameOver_Interior_Overlay_3 = new Point(-37, 129);
            Constants.Board_GameOver_Interior_Overlay_4 = new Point(-37, 129);
            Constants.Board_GameOver_Exterior_Overlay_1 = new Point(-15, 109);
            Constants.Board_GameOver_Exterior_Overlay_2 = new Point(-14, 112);
            Constants.Board_GameOver_Exterior_Overlay_3 = new Point(-37, 125);
            Constants.Board_GameOver_Exterior_Overlay_4 = new Point(-38, 71);
            Constants.Board_GameOver_Exterior_Overlay_5 = new Point(-63, 44);
            Constants.Board_GameOver_Exterior_Overlay_6 = new Point(-63, 44);
            Constants.Board_Ice_Start = 800;
            Constants.Board_ProgressBarText_Pos = 1;
            Constants.MessageWidget_SlotMachine_Y = 518;
            Constants.LawnMower_Coin_Offset = new Point(40, 40);
            Constants.DescriptionWidget_ScrollBar_Padding = 7;
            Constants.Zombie_Bungee_Offset = new Point(61, 15);
            Constants.Zombie_Bungee_Target_Offset = new Point(10, 60);
            Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT = -145;
            Constants.Zombie_Dancer_Dance_Limit_X = 700;
            Constants.Zombie_Dancer_Spotlight_Scale = 2.1f;
            Constants.Zombie_Dancer_Spotlight_Offset = new Point(-32, 60);
            Constants.Zombie_Dancer_Spotlight_Pos = new Point(-25, -480);
            Constants.Zombie_ClipOffset_Default = 100;
            Constants.Zombie_ClipOffset_Snorkel = 0;
            Constants.Zombie_ClipOffset_Snorkel_intoPool_Small = 40;
            Constants.Zombie_ClipOffset_Snorkel_Dying = 76;
            Constants.Zombie_ClipOffset_Snorkel_Dying_Small = 65;
            Constants.Zombie_ClipOffset_Pail = 78;
            Constants.Zombie_ClipOffset_Normal = 108;
            Constants.Zombie_ClipOffset_Digger = 75;
            Constants.Zombie_ClipOffset_Dolphin_Into_Pool = 65;
            Constants.Zombie_ClipOffset_Snorkel_Grabbed = 75;
            Constants.Zombie_ClipOffset_PeaHead_InPool = 78;
            Constants.Zombie_ClipOffset_RisingFromGrave = 28;
            Constants.Zombie_ClipOffset_RisingFromGrave_Small = -35;
            Constants.Zombie_ClipOffset_Snorkel_Into_Pool = 60;
            Constants.Zombie_ClipOffset_Normal_In_Pool = 30;
            Constants.Zombie_ClipOffset_Flag_In_Pool = 30;
            Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL = -40;
            Constants.Zombie_ClipOffset_TrafficCone_In_Pool_SMALL = -55;
            Constants.Zombie_ClipOffset_Ducky_Dying_In_Pool = 30;
            Constants.Zombie_GameOver_ClipOffset_1 = 53;
            Constants.Zombie_GameOver_ClipOffset_2 = 56;
            Constants.Zombie_GameOver_ClipOffset_3 = 56;
            Constants.ZombieGalleryWidget_Window_Offset = new Point(0, 0);
            Constants.ZombieGalleryWidget_Window_Clip = new Rectangle(2, 2, 72, 72);
            Constants.Coin_AwardSeedpacket_Pos = new Point(460, 300);
            Constants.Coin_Glow_Offset = new Point(14, 12);
            Constants.Coin_Silver_Offset = 9f;
            Constants.Coin_MoneyBag_Offset = new Point(-45, -40);
            Constants.Coin_Shovel_Offset = new Point(3, 15);
            Constants.Coin_Silver_Award_Offset = new Point(21, 21);
            Constants.Coin_Almanac_Offset = new Point(0, -36);
            Constants.Coin_Note_Offset = new Point(0, -10);
            Constants.Coin_CarKeys_Offset = new Point(0, 10);
            Constants.Coin_Taco_Offset = new Point(0, 20);
            Constants.Coin_Bacon_Offset = new Point(0, 20);
            Constants.Plant_CobCannon_Projectile_Offset = new Point(-28, -165);
            Constants.Plant_Squished_Offset = new Point(0, 0);
            Constants.IZombieBrainPosition = 140;
            Constants.IZombie_SeedOffset = new Point(5, 6);
            Constants.IZombie_ClipOffset = new Rectangle(2, 2, 70, 34);
            Constants.ZombieOffsets = new Point[]
            {
                new Point(20, 11),
                new Point(13, 15),
                new Point(22, 14),
                new Point(3, 13),
                new Point(20, 13),
                new Point(18, 13),
                new Point(19, 13),
                new Point(4, 11),
                new Point(14, 13),
                new Point(19, 12),
                new Point(20, 13),
                new Point(16, 10),
                new Point(1, 11),
                new Point(16, 10),
                new Point(16, 15),
                new Point(14, 10),
                new Point(13, 12),
                new Point(10, 13),
                new Point(20, 12),
                new Point(11, 12),
                new Point(0, 9),
                new Point(15, 14),
                new Point(4, 12),
                new Point(18, 10),
                new Point(23, 27),
                new Point(1, 15)
            };
            Constants.LastStandButtonRect = new TRect(200, 297, 140, 28);
            Constants.VasebreakerJackInTheBoxOffset = 0;
            Constants.JackInTheBoxPlantRadius = 90;
            Constants.JackInTheBoxZombieRadius = 115;
            Constants.ZenGardenGreenhouseMultiplierX = 1.125f;
            Constants.ZenGardenGreenhouseMultiplierY = 1.182f;
            Constants.ZenGardenGreenhouseOffset = new Point(-30, 30);
            Constants.ZenGardenMushroomGardenOffset = new Point(10, 30);
            Constants.ZenGardenStoreButtonX = 650;
            Constants.ZenGardenStoreButtonY = 33;
            Constants.ZenGardenTopButtonStart = -100;
            Constants.ZenGardenButtonCounterOffset = new Point(40, 40);
            Constants.ZenGardenButton_GoldenWateringCan_Offset = new Point(-2, -6);
            Constants.ZenGardenButton_NormalWateringCan_Offset = new Point(-2, -6);
            Constants.ZenGardenButton_Fertiliser_Offset = new Point(-6, -7);
            Constants.ZenGardenButton_BugSpray_Offset = new Point(0, -1);
            Constants.ZenGardenButton_Phonograph_Offset = new Point(2, 2);
            Constants.ZenGardenButton_Chocolate_Offset = new Point(6, 4);
            Constants.ZenGardenButton_Glove_Offset = new Point(-6, -4);
            Constants.ZenGardenButton_MoneySign_Offset = new Point(-5, -4);
            Constants.ZenGardenButton_NextGarden_Offset = new Point(2, 0);
            Constants.ZenGardenButton_Wheelbarrow_Offset = new Point(-7, -3);
            Constants.ZenGardenButton_WheelbarrowPlant_Offset = new Point(23, -8);
            Constants.ZenGardenButton_Wheelbarrow_Facing_Offset = 80f;
            Constants.ZenGarden_Backdrop_X = 0;
            Constants.ZenGarden_SellDialog_Offset = new Point(120, 60);
            Constants.ZenGarden_NextGarden_Pos = new Point(564, 0);
            Constants.ZenGarden_RetardedDaveBubble_Pos = new Point(-250, -120);
            Constants.ZenGarden_WaterDrop_Pos = new Point(67, 9);
            Constants.ZenGarden_Fertiliser_Pos = new Point(61, 7);
            Constants.ZenGarden_Phonograph_Pos = new Point(60, 7);
            Constants.ZenGarden_BugSpray_Pos = new Point(61, 7);
            Constants.ZenGarden_PlantSpeechBubble_Pos = new Point(50, 0);
            Constants.ZenGarden_StinkySpeechBubble_Pos = new Point(50, -20);
            Constants.ZenGarden_GoldenWater_Pos = new Point(-14, -14);
            Constants.ZenGarden_Chocolate_Pos = new Point(63, -28);
            Constants.ZenGarden_MoneyTarget_X = 442;
            Constants.ZenGarden_TutorialArrow_Offset = 100;
            Constants.ZEN_XMIN = 10;
            Constants.ZEN_YMIN = 65;
            Constants.ZEN_XMAX = 470;
            Constants.ZEN_YMAX = 310;
            Constants.ZenGarden_GoldenWater_Size = new Rectangle(-70, -90, 80, 80);
            Constants.STINKY_SLEEP_POS_Y = 461;
            Constants.gMushroomGridPlacement = new SpecialGridPlacement[]
            {
                new SpecialGridPlacement(110, 441, 0, 0),
                new SpecialGridPlacement(237, 360, 1, 0),
                new SpecialGridPlacement(298, 458, 2, 0),
                new SpecialGridPlacement(355, 296, 3, 0),
                new SpecialGridPlacement(387, 203, 4, 0),
                new SpecialGridPlacement(460, 385, 5, 0),
                new SpecialGridPlacement(486, 478, 6, 0),
                new SpecialGridPlacement(552, 283, 7, 0)
            };
            Constants.ZenGarden_Marigold_Sprout_Offset = new Point(24, 30);
            Constants.ZenGarden_Aquarium_ShadowOffset = new Point(35, 0);
            Constants.Challenge_SeeingStars_StarfruitPreview_Offset_Y = 8;
            Constants.Challenge_SlotMachine_Pos = new Point(100, 200);
            Constants.Challenge_SlotMachineHandle_Pos = new TRect(473, 0, 55, 80);
            Constants.Challenge_SlotMachine_Gap = 9;
            Constants.Challenge_SlotMachine_Offset = 62;
            Constants.Challenge_SlotMachine_Shadow_Offset = 3;
            Constants.Challenge_SlotMachine_Y_Offset = 3;
            Constants.Challenge_SlotMachine_Y_Pos = 3;
            Constants.Challenge_SlotMachine_ClipHeight = 40;
            Constants.Challenge_BeghouldedTwist_Offset = new Point(40, 50);
            Constants.GridItem_ScaryPot_SeedPacket_Offset = new Point(23, 33);
            Constants.GridItem_ScaryPot_Zombie_Offset = new Point(6, 19);
            Constants.GridItem_ScaryPot_ZombieFootball_Offset = new Point(1, 16);
            Constants.GridItem_ScaryPot_ZombieGargantuar_Offset = new Point(9, 7);
            Constants.GridItem_ScaryPot_Sun_Offset = new Point(42, 62);
        }

        public static Constants.LanguageIndex Language
        {
            get
            {
                return Constants.mLanguage;
            }
            set
            {
                Constants.mLanguage = value;
                Constants.LanguageSubDir = Constants.mLanguage.ToString();
            }
        }

        public static float InvertLowResValue(float x)
        {
            return 1.875f * x;
        }

        public static float InvertAndScale(float x)
        {
            return Constants.InvertLowResValue(x) * Constants.S;
        }

        public static float ScaleFrom480(float x)
        {
            return FrameworkConstants.Font_Scale * x;
        }

        public static void Load480x800()
        {
            Constants.S = 0.8f;
            Constants.IS = 1.25f;
            FrameworkConstants.Font_Scale = 1f;
            Constants.Board_Offset_AspectRatio_Correction = 80;
            if (Constants.Language == Constants.LanguageIndex.it)
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(50f);
            }
            else if (Constants.Language == Constants.LanguageIndex.de)
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(85f);
            }
            else if (Constants.Language == Constants.LanguageIndex.fr)
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(85f);
            }
            else if (Constants.Language == Constants.LanguageIndex.es)
            {
                Constants.ReanimTextCenterOffsetX = -(int)Constants.InvertAndScale(10f);
            }
            else
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(85f);
            }
            Constants.Loaded = true;
            Constants.BackBufferSize = new Point(480, 800);
            Constants.ImageSubPath = "\\480x800\\";
            Constants.BOARD_WIDTH = 800;
            Constants.BOARD_HEIGHT = 480;
            Constants.BOARD_EDGE = 50;
            Constants.BOARD_OFFSET = 119;
            Constants.BOARD_EXTRA_ROOM = 100;
            Constants.HIGH_GROUND_HEIGHT = 30;
            Constants.GRIDSIZEX = 9;
            Constants.MAX_GRIDSIZEY = 6;
            Constants.MORE_GAMES_ORIGIN_X = 100;
            Constants.MAIN_MENU_ORIGIN_X = 640;
            Constants.ACHIEVEMENTS_ORIGIN_X = 640;
            Constants.QUICKPLAY_ORIGIN_X = 1440;
            Constants.MORE_GAMES_PLANK_HEIGHT = 100;
            Constants.MORE_GAMES_PLANK_WIDTH = 320;
            Constants.MORE_GAMES_ITEM_GAP = 2;
            Constants.SMALL_SEEDPACKET_WIDTH = 81;
            Constants.SMALL_SEEDPACKET_HEIGHT = 52;
            Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET = 480;
            Constants.SCROLL_AREA_OFFSET_X = 102;
            Constants.SCROLL_AREA_OFFSET_Y = (int)Constants.InvertAndScale(28f);
            Constants.DIALOG_HEADER_OFFSET = 36;
            Constants.WIDE_BOARD_WIDTH = 900;
            Constants.ImageWidth = (int)(Constants.S * 1400f);
            Constants.LAWN_XMIN = 140;
            Constants.LAWN_YMIN = 80;
            Constants.SEED_PACKET_HORIZ_GAP = (int)Constants.InvertAndScale(2f);
            Constants.SEED_PACKET_VERT_GAP = (int)Constants.InvertAndScale(1f);
            Constants.SeedPacket_Selector_Pos = new Point(-5, 2);
            Constants.TitleScreen_ReanimStart_X = 130;
            Constants.GameSelector_Width = 2348;
            Constants.GameSelector_Height = (int)Constants.InvertAndScale(640f);
            Constants.GameSelector_AdventureButton_X = 430;
            Constants.GameSelector_MiniGameButton_X = Constants.GameSelector_AdventureButton_X + 21;
            Constants.GameSelector_MiniGameButton_Y = (int)Constants.InvertAndScale(100f);
            Constants.GameSelector_OptionsButton_X = 662;
            Constants.GameSelector_OptionsButton_Y = 276;
            Constants.GameSelector_ZenGardenButton_X = 418;
            Constants.GameSelector_ZenGardenButton_Y = 349;
            Constants.GameSelector_AlmanacButton_X = 536;
            Constants.GameSelector_AlmanacButton_Y = 334;
            Constants.GameSelector_StoreButton_X = 375;
            Constants.GameSelector_StoreButton_Y = 273;
            Constants.GameSelector_LeaderboardButton_X = 0;
            Constants.GameSelector_LeaderboardButton_Y = (int)Constants.InvertAndScale(200f);
            Constants.GameSelector_AchievementsButton_X = 211;
            Constants.GameSelector_AchievementsButton_Y = 429;
            Constants.GameSelector_AchievementsStatue_X = 212;
            Constants.GameSelector_AchievementsStatue_Y = 252;
            Constants.GameSelector_MoreWaysToPlay_MiniGames_X = 236;
            Constants.GameSelector_MoreWaysToPlay_MiniGames_Y = (int)Constants.InvertAndScale(160f);
            Constants.GameSelector_MoreWaysToPlay_VaseBreaker_X = 525;
            Constants.GameSelector_MoreWaysToPlay_VaseBreaker_Y = (int)Constants.InvertAndScale(175f);
            Constants.GameSelector_MoreWaysToPlay_IZombie_X = 350;
            Constants.GameSelector_MoreWaysToPlay_IZombie_Y = (int)Constants.InvertAndScale(195f);
            Constants.GameSelector_MoreWaysToPlay_Back_X = 330;
            Constants.GameSelector_MoreWaysToPlay_Back_Y = (int)Constants.InvertAndScale(270f);
            Constants.GameSelector_PlayerName_Pos = new Point(167, -190);
            Constants.GameSelector_LevelNumber_1_Pos = new Point(553, 32);
            Constants.GameSelector_LevelNumber_2_Pos = new Point(580, 33);
            Constants.GameSelector_LevelNumber_3_Pos = new Point(592, 34);
            Constants.GameSelector_LevelNumber_Bar = new Rectangle(573, 44, 5, 2);
            Constants.GameSelector_LevelNumber_ButtonDown_Offset = 6;
            Constants.GameSelector_Update_Offset = 110;
            Constants.LeaderboardScreen_Vasebreaker_Button_X = 633;
            Constants.LeaderboardScreen_Vasebreaker_Button_Y = 339;
            Constants.LeaderboardScreen_IZombie_Button_X = 125;
            Constants.LeaderboardScreen_IZombie_Button_Y = 306;
            Constants.LeaderboardScreen_Killed_Button_X = 405;
            Constants.LeaderboardScreen_Killed_Button_Y = 306;
            Constants.LeaderboardScreen_PileBase_X = 331;
            Constants.LeaderboardScreen_PileBase_Y = 53;
            Constants.Leaderboard_IZombie_Score_X = 198;
            Constants.Leaderboard_IZombie_Score_Y = 290;
            Constants.Leaderboard_Vasebreaker_Score_X = 698;
            Constants.Leaderboard_Vasebreaker_Score_Y = 322;
            Constants.Leaderboard_Pile_1_X = 297;
            Constants.LeaderboardDialog_CannotConnect_Rect = new TRect(30, 70, 570, 300);
            Constants.LeaderboardScreen_EdgeOfSpace_Overlay_Offset = 30;
            Constants.AchievementWidget_ROW_HEIGHT = 135;
            Constants.AchievementWidget_ROW_START = 275;
            Constants.AchievementWidget_HOLE_DEPTH = 128;
            Constants.AchievementWidget_GAMERSCORE_POS = new Point(217, 345);
            Constants.AchievementWidget_BackButton_X = 152;
            Constants.AchievementWidget_BackButton_Y = 6;
            Constants.AchievementWidget_BackButton_Rect = new TRect(152, 0, 170, 120);
            Constants.AchievementWidget_Background_Offset_Y = 412;
            Constants.AchievementWidget_Description_Box = new Rectangle(270, 30, 400, 70);
            Constants.AchievementWidget_Pipe_Offset = new Point(0, 140);
            Constants.AchievementWidget_Worm_Offset = new Point(659, -104);
            Constants.AchievementWidget_ZombieWorm_Offset = new Point(76, -9);
            Constants.AchievementWidget_GemLeft_Offset = new Point(180, 0);
            Constants.AchievementWidget_GemRight_Offset = new Point(450, -69);
            Constants.AchievementWidget_Fossile_Offset = new Point(239, -55);
            Constants.AchievementWidget_Image_Pos = new Point(200, 10);
            Constants.AchievementWidget_Image_Size = 64;
            Constants.AchievementWidget_Name_Pos = new Point(270, 0);
            Constants.AchievementWidget_Name_MaxWidth = 400;
            Constants.QuickPlayWidget_Thumb_X = 20;
            Constants.QuickPlayWidget_Thumb_Y = 15;
            Constants.QuickPlayWidget_Bungee_Y = -52;
            Constants.SeedChooserScreen_MenuButton_X = 665;
            Constants.SeedChooserScreen_Background_Top = new Point(88, 4);
            Constants.SeedChooserScreen_Background_Middle = new Point(Constants.SeedChooserScreen_Background_Top.X, Constants.SeedChooserScreen_Background_Top.Y + 44);
            Constants.SeedChooserScreen_Background_Middle_Height = 325;
            Constants.SeedChooserScreen_Background_Bottom = new Point(Constants.SeedChooserScreen_Background_Top.X, Constants.SeedChooserScreen_Background_Middle.Y + Constants.SeedChooserScreen_Background_Middle_Height);
            Constants.SeedChooserScreen_Gradient_Top = new Rectangle(0, 40, 336, 22);
            Constants.SeedChooserScreen_Gradient_Bottom = new Rectangle(0, 362, 336, 22);
            Constants.SeedPacket_Cost = new Point(42, 26);
            Constants.SeedPacket_Cost_IZombie = new Point(41, 27);
            Constants.SeedPacket_CostText_Pos = new Point(74, 22);
            Constants.SeedPacket_CostText_IZombie_Pos = new Point(73, 23);
            Constants.ImitaterDialog_Size = new Point(0, 240);
            Constants.ImitaterDialog_ScrollWidget_Offset_X = 5;
            Constants.ImitaterDialog_ScrollWidget_Y = 120;
            Constants.ImitaterDialog_ScrollWidget_ExtraWidth = 20;
            Constants.ImitaterDialog_Height = 250;
            Constants.ImitaterDialog_BottomGradient_Y = 235;
            Constants.CutScene_ReadySetPlant_Pos = new Point(370, 260);
            Constants.CutScene_LogoEndPos = 350;
            Constants.CutScene_LogoBackRect_Height = 150;
            Constants.CutScene_LogoEnd_Particle_Pos = new Point(600, 400);
            Constants.CutScene_ExtraRoom_1_Particle_Pos = new Point(35, 348);
            Constants.CutScene_ExtraRoom_2_Particle_Pos = new Point(Constants.CutScene_ExtraRoom_1_Particle_Pos.X, 246);
            Constants.CutScene_ExtraRoom_3_Particle_Pos = new Point(Constants.CutScene_ExtraRoom_1_Particle_Pos.X, 459);
            Constants.CutScene_ExtraRoom_4_Particle_Pos = new Point(32, 150);
            Constants.CutScene_ExtraRoom_5_Particle_Pos = new Point(32, 551);
            Constants.CutScene_SodRoll_1_Pos = -105;
            Constants.CutScene_SodRoll_2_Pos = 107;
            Constants.CutScene_SodRoll_3_Pos = new Point(-6, -205);
            Constants.CutScene_SodRoll_4_Pos = new Point(-6, 205);
            Constants.CutScene_Upsell_TerraCotta_Arrow = new Point(733, 360);
            Constants.CutScene_Upsell_TerraCotta_Pot = new Point(565, 360);
            Constants.ConveyorBeltClipRect = new TRect(0, 8, 83, 465);
            Constants.StoreScreen_BackButton_X = 430;
            Constants.StoreScreen_BackButton_Y = 390;
            Constants.StoreScreen_Car_X = 260;
            Constants.StoreScreen_Car_Y = 80;
            Constants.StoreScreen_PrevButton_X = 317;
            Constants.StoreScreen_PrevButton_Y = 296;
            Constants.StoreScreen_NextButton_X = 645;
            Constants.StoreScreen_NextButton_Y = 296;
            Constants.StoreScreen_HatchOpen_X = 316;
            Constants.StoreScreen_HatchOpen_Y = -35;
            Constants.StoreScreen_HatchClosed_X = 309;
            Constants.StoreScreen_HatchClosed_Y = 81;
            Constants.StoreScreen_CarNight_X = 260;
            Constants.StoreScreen_CarNight_Y = 80;
            Constants.StoreScreen_StoreSign_X = 450;
            Constants.StoreScreen_StoreSign_Y_Min = -150;
            Constants.StoreScreen_StoreSign_Y_Max = -10;
            Constants.StoreScreen_Coinbank_X = 590;
            Constants.StoreScreen_Coinbank_Y = 405;
            Constants.StoreScreen_Coinbank_TextOffset = new Point(180, 8);
            Constants.StoreScreen_ItemOffset_1_X = 430;
            Constants.StoreScreen_ItemOffset_1_Y = 102;
            Constants.StoreScreen_ItemOffset_2_X = 390;
            Constants.StoreScreen_ItemOffset_2_Y = 190;
            Constants.StoreScreen_ItemSize = 85;
            Constants.StoreScreen_ItemSize_Offset = 4;
            Constants.StoreScreen_PriceTag_X = 3;
            Constants.StoreScreen_PriceTag_Y = 70;
            Constants.StoreScreen_PriceTag_Text_Offset_X = 35;
            Constants.StoreScreen_PriceTag_Text_Offset_Y = 67;
            Constants.StoreScreen_ComingSoon_X = 100;
            Constants.StoreScreen_ComingSoon_Y = 70;
            Constants.StoreScreen_SoldOut_Width = 80;
            Constants.StoreScreen_SoldOut_Y = 0;
            Constants.StoreScreen_SoldOut_Height = 100;
            Constants.StoreScreen_PacketUpgrade_X = 10;
            Constants.StoreScreen_PacketUpgrade_Y = 5;
            Constants.StoreScreen_PacketUpgrade_Text_Size = new Rectangle(0, 0, 80, 60);
            Constants.StoreScreen_RetardedDave_Offset_X = -10;
            Constants.StoreScreen_FirstAidNut_Offset_Y = 10;
            Constants.StoreScreen_PoolCleaner_Offset_X = 23;
            Constants.StoreScreen_PoolCleaner_Offset_Y = 24;
            Constants.StoreScreen_Rake_Offset_X = -10;
            Constants.StoreScreen_Rake_Offset_Y = 24;
            Constants.StoreScreen_RoofCleaner_Offset_X = 15;
            Constants.StoreScreen_RoofCleaner_Offset_Y = 38;
            Constants.StoreScreen_Imitater_Offset_X = -1;
            Constants.StoreScreen_Imitater_Offset_Y = 24;
            Constants.StoreScreen_Default_Offset_Y = 20;
            Constants.StoreScreen_MouseRegion = new Point(75, 100);
            Constants.StoreScreen_Dialog = new Rectangle(200, 50, 400, 450);
            Constants.StoreScreen_PotPlant_Offset = new Point(40, 70);
            Constants.StoreScreenMushroomGardenOffsetX = 15;
            Constants.StoreScreenAquariumGardenOffsetX = 15;
            Constants.StoreScreenGloveOffsetY = 15;
            Constants.StoreScreenWheelbarrowOffsetY = 15;
            Constants.StoreScreenBugSprayOffsetX = 17;
            Constants.StoreScreenBugSprayOffsetY = 15;
            Constants.StoreScreenPhonographOffsetY = 15;
            Constants.StoreScreenPlantFoodOffsetX = 13;
            Constants.StoreScreenPlantFoodOffsetY = 15;
            Constants.StoreScreenWateringCanOffsetX = 6;
            Constants.StoreScreenWateringCanOffsetY = 15;
            Constants.NewOptionsDialog_FXLabel_X = 340;
            Constants.NewOptionsDialog_FXLabel_Y = 143;
            Constants.NewOptionsDialog_MusicLabel_X = 340;
            Constants.NewOptionsDialog_MusicLabel_On_Y = 97;
            Constants.NewOptionsDialog_MusicLabel_Off_Y = 103;
            Constants.NewOptionsDialog_VibrationLabel_X = 340;
            Constants.NewOptionsDialog_VibrationLabel_Y = 193;
            Constants.NewOptionsDialog_LockedLabel_Y = 265;
            Constants.NewOptionsDialog_VibrationLabel_MaxWidth = 300;
            Constants.NewOptionsDialog_FullScreenOffset = 25;
            Constants.NewOptionsDialog_FX_Offset = 15;
            Constants.NewOptionsDialog_Music_Offset = 5;
            Constants.NewOptionsDialog_Version_Low_Y = 340;
            Constants.NewOptionsDialog_Version_High_Y = 345;
            Constants.RetardedDave_Bubble_Tip_Offset = 5;
            Constants.RetardedDave_Bubble_Offset_Shop = new Point(-280, -117);
            Constants.RetardedDave_Bubble_Offset_Board = new Point(-200, -60);
            Constants.RetardedDave_Bubble_Size = 400;
            Constants.RetardedDave_Bubble_Rect = new TRect(15, 6, 0, 250);
            Constants.RetardedDave_Bubble_TapToContinue_Y = 230;
            Constants.PlantGallerySize = new Point(390, 750);
            Constants.ZombieGallerySize = new Point(415, 1068);
            Constants.AlmanacHeaderY = 8;
            Constants.Almanac_PlantsButtonRect = new TRect(67, 150, 300, 240);
            Constants.Almanac_ZombiesButtonRect = new TRect(450, 150, 300, 240);
            Constants.Almanac_CloseButtonRect = new TRect(660, 435, 148, 39);
            Constants.Almanac_IndexButtonRect = new TRect(442, 435, 148, 39);
            Constants.Almanac_IndexPlantPos = new Point(183, 240);
            Constants.Almanac_IndexZombiePos = new Point(553, 225);
            Constants.Almanac_IndexPlantTextPos = new Point(Constants.Almanac_PlantsButtonRect.mX + Constants.Almanac_PlantsButtonRect.mWidth / 2, Constants.Almanac_PlantsButtonRect.mY + Constants.Almanac_PlantsButtonRect.mHeight - 65);
            Constants.Almanac_IndexZombieTextPos = new Point(Constants.Almanac_ZombiesButtonRect.mX + Constants.Almanac_ZombiesButtonRect.mWidth / 2, Constants.Almanac_ZombiesButtonRect.mY + Constants.Almanac_ZombiesButtonRect.mHeight - 65);
            Constants.Almanac_PlantScrollRect = new TRect(47, 75, 380, 368);
            Constants.Almanac_ZombieScrollRect = new TRect(28, 75, 410, 368);
            Constants.Almanac_DescriptionScrollRect = new TRect(470, 255, 305, 167);
            Constants.Almanac_PlantTopGradientY = 74;
            Constants.Almanac_ZombieTopGradientY = 72;
            Constants.Almanac_BottomGradientY = 432;
            Constants.Almanac_PlantGradientWidth = 360;
            Constants.Almanac_ZombieGradientWidth = 390;
            Constants.Almanac_ZombieStoneRect = new TRect(445, 2, 353, 465);
            Constants.Almanac_BackgroundPosition = new Point(533, 60);
            Constants.Almanac_ZombieClipRect = new TRect(-40, -45, 175, 175);
            Constants.Almanac_NavyRect = new TRect(515, 40, 196, 198);
            Constants.Almanac_NamePosition = new Point(622, 0);
            Constants.Almanac_ClayRect = new TRect(450, 6, 340, 465);
            Constants.Almanac_BrownRect = new TRect(513, 41, 198, 198);
            Constants.Almanac_ZombieSpace = new Point(137, 117);
            Constants.Almanac_ZombieOffset = new Point(0, 5);
            Constants.Almanac_BossPosition = new Point(205, 944);
            Constants.Almanac_ImpPosition = new Point(68, 944);
            Constants.Almanac_ImitatorPosition = new Point(Constants.PlantGallerySize.X / 2 - 52, 688);
            Constants.Almanac_SeedSpace = new Point(4, 4);
            Constants.Almanac_SeedOffset = new Point(16, 8);
            Constants.Almanac_ZombiePosition = new Point(700, 120);
            Constants.Almanac_PlantPosition = new Point(583, 105);
            Constants.Almanac_Text_Scale = 0.8f;
            Constants.Almanac_PlantsHeader_Pos = new Point(32, 2);
            Constants.Almanac_ZombieHeader_Pos = new Point(25, 2);
            Constants.Almanac_ItemName_MaxWidth = 330;
            Constants.Zombie_StartOffset = 110f;
            Constants.Zombie_StartRandom_Offset = 40f;
            Constants.AwardScreen_Note_Credits_Background = new Rectangle(-500, -170, 1600, 800);
            Constants.AwardScreen_Note_Credits_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_Credits_Text = new Rectangle(180, 80, 429, 287);
            Constants.AwardScreen_Note_Help_Background = new Rectangle(-350, -150, 1700, 700);
            Constants.AwardScreen_Note_Help_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_Help_Text = new Point(180, 95);
            Constants.AwardScreen_Note_1_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_1_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_1_Text = new Point(180, 85);
            Constants.AwardScreen_Note_Message = new Point(400, 0);
            Constants.AwardScreen_Note_2_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_2_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_2_Text = new Point(175, 85);
            Constants.AwardScreen_Note_3_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_3_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_3_Text = new Point(165, 80);
            Constants.AwardScreen_Note_4_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_4_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_4_Text = new Point(160, 80);
            Constants.AwardScreen_Note_Final_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_Final_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_Final_Text = new Point(170, 95);
            Constants.AwardScreen_Bacon = (Constants.AwardScreen_Taco = (Constants.AwardScreen_CarKeys = new Point(191, 210)));
            Constants.AwardScreen_WateringCan = new Point(180, 210);
            Constants.AwardScreen_Almanac = new Point(195, 185);
            Constants.AwardScreen_Trophy = new Point((int)(400f * Constants.S), (int)(137f * Constants.S));
            Constants.AwardScreen_Shovel = new Point(195, 195);
            Constants.AwardScreen_MenuButton = new Point(610, 420);
            Constants.AwardScreen_ClayTablet = new TRect(50, 70, 700, 300);
            Constants.AwardScreen_TitlePos = new Point(400, 10);
            Constants.AwardScreen_GroundDay_Pos = new Point(111, 167);
            Constants.AwardScreen_BrownRect = new TRect(90, 145, 200, 200);
            Constants.AwardScreen_BottomMessage_Pos = new Point(400, 70);
            Constants.AwardScreen_BottomText_Rect_Text = new TRect(317, 160, 400, 170);
            Constants.AwardScreen_BottomText_Rect_NoText = new TRect(150, 120, 500, 200);
            Constants.AwardScreen_CreditsButton = new TRect(180, 413, 140, 48);
            Constants.AwardScreen_CreditsButton_Offset = new Point(50, -7);
            if (Constants.Language == Constants.LanguageIndex.es)
            {
                Constants.AwardScreen_CreditsButton_Offset.X = 70;
            }
            else if (Constants.Language == Constants.LanguageIndex.it)
            {
                Constants.AwardScreen_CreditsButton_Offset.X = 70;
            }
            else if (Constants.Language == Constants.LanguageIndex.de)
            {
                Constants.AwardScreen_CreditsButton_Offset.X = 60;
            }
            Constants.AwardScreen_Seed_Pos = new Point(151, 220);
            Constants.AwardScreen_ContinueButton_Offset = 35;
            Constants.AwardScreen_AchievementImage_Pos = new Point(80, 4);
            Constants.AwardScreen_AchievementName_Pos = new Point(200, 5);
            Constants.AwardScreen_AchievementDescription_Rect = new Rectangle(200, 30, 400, 60);
            Constants.CreditScreen_ReplayButton = new Rectangle(100, 400, 200, 48);
            Constants.CreditScreen_ReplayButton_TextOffset = new Point(-5, -7);
            Constants.CreditScreen_MainMenu = new Rectangle(500, 400, 200, 50);
            Constants.CreditScreen_TextStart = 200;
            Constants.CreditScreen_TextEnd = 400;
            Constants.CreditScreen_TextClip = 370;
            Constants.CreditScreen_LeftText_X = 0;
            Constants.CreditScreen_RightText_X = 390;
            Constants.CreditScreen_LeftRight_Text_Width = 370;
            Constants.ConfirmPurchaseDialog_Background = new TRect(38, 100, 312, 250);
            Constants.ConfirmPurchaseDialog_Item_Pos = new Point(60, 155);
            Constants.ConfirmPurchaseDialog_Text = new TRect(145, 95, 190, 265);
            Constants.LawnDialog_Insets = new Insets(60, 20, 70, 35);
            Constants.UILevelPosition = new Point(793, 445);
            Constants.UIMenuButtonPosition = new Point(650 - Constants.Board_Offset_AspectRatio_Correction, 3);
            Constants.UIMenuButtonWidth = 150;
            Constants.UISunBankPositionX = 95;
            Constants.UISunBankTextOffset = new Point(92, 3);
            Constants.UIShovelButtonPosition = new Point(260, 3);
            Constants.UIProgressMeterPosition = new Point(370, 3);
            Constants.UIProgressMeterHeadEnd = 220;
            Constants.UIProgressMeterBarEnd = 225;
            Constants.UICoinBankPosition = new Point(110 - Constants.Board_Offset_AspectRatio_Correction, 479);
            Constants.Board_Cutscene_ExtraScroll = 170;
            Constants.Board_SunCoinRange = 550;
            Constants.Board_SunCoin_CollectTarget = new Point(115 - (int)(Board_Offset_AspectRatio_Correction * Constants.IS), 5);
            Constants.Board_Cel_Y_Values_Normal = new int[]
            {
                80,
                177,
                280,
                380,
                470,
                570,
                610
            };
            Constants.Board_Cel_Y_Values_Pool = new int[]
            {
                80,
                170,
                275,
                360,
                447,
                522,
                610
            };
            Constants.Board_Cel_Y_Values_ZenGarden = new int[]
            {
                125,
                225,
                335,
                455,
                560,
                750,
                750
            };
            Constants.Board_GameOver_Interior_Overlay_1 = new Point(-15, 182);
            Constants.Board_GameOver_Interior_Overlay_2 = new Point(-17, 160);
            Constants.Board_GameOver_Interior_Overlay_3 = new Point(-60, 178);
            Constants.Board_GameOver_Interior_Overlay_4 = new Point(-56, 202);
            Constants.Board_GameOver_Exterior_Overlay_1 = new Point(-22, 142);
            Constants.Board_GameOver_Exterior_Overlay_2 = new Point(-21, 148);
            Constants.Board_GameOver_Exterior_Overlay_3 = new Point(-57, 166);
            Constants.Board_GameOver_Exterior_Overlay_4 = new Point(-58, 105);
            Constants.Board_GameOver_Exterior_Overlay_5 = new Point(5, 179);
            Constants.Board_GameOver_Exterior_Overlay_6 = new Point(5, 179);
            Constants.Board_Ice_Start = 1000;
            Constants.Board_ProgressBarText_Pos = 1;
            Constants.MessageWidget_SlotMachine_Y = 518;
            Constants.LawnMower_Coin_Offset = new Point(20, 5);
            Constants.DescriptionWidget_ScrollBar_Padding = 10;
            Constants.Zombie_Bungee_Offset = new Point(61, 15);
            Constants.Zombie_Bungee_Target_Offset = new Point(5, 40);
            Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT = -145;
            Constants.Zombie_Dancer_Dance_Limit_X = 1000;
            Constants.Zombie_Dancer_Spotlight_Scale = 2.7f;
            Constants.Zombie_Dancer_Spotlight_Offset = new Point(-45, 50);
            Constants.Zombie_Dancer_Spotlight_Pos = new Point(2, -560);
            Constants.Zombie_ClipOffset_Default = 100;
            Constants.Zombie_ClipOffset_Snorkel = 0;
            Constants.Zombie_ClipOffset_Snorkel_intoPool_Small = 40;
            Constants.Zombie_ClipOffset_Snorkel_Dying = 76;
            Constants.Zombie_ClipOffset_Snorkel_Dying_Small = 65;
            Constants.Zombie_ClipOffset_Pail = 78;
            Constants.Zombie_ClipOffset_Normal = 108;
            Constants.Zombie_ClipOffset_Digger = 75;
            Constants.Zombie_ClipOffset_Dolphin_Into_Pool = 65;
            Constants.Zombie_ClipOffset_Snorkel_Grabbed = 75;
            Constants.Zombie_ClipOffset_PeaHead_InPool = 78;
            Constants.Zombie_ClipOffset_RisingFromGrave = 20;
            Constants.Zombie_ClipOffset_RisingFromGrave_Small = -35;
            Constants.Zombie_ClipOffset_Snorkel_Into_Pool = 80;
            Constants.Zombie_ClipOffset_Normal_In_Pool = 30;
            Constants.Zombie_ClipOffset_Flag_In_Pool = 30;
            Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL = -40;
            Constants.Zombie_ClipOffset_TrafficCone_In_Pool_SMALL = -55;
            Constants.Zombie_ClipOffset_Ducky_Dying_In_Pool = 27;
            Constants.Zombie_GameOver_ClipOffset_1 = 77;
            Constants.Zombie_GameOver_ClipOffset_2 = 80;
            Constants.Zombie_GameOver_ClipOffset_3 = 85;
            Constants.ZombieGalleryWidget_Window_Offset = new Point(3, 7);
            Constants.ZombieGalleryWidget_Window_Clip = new Rectangle(6, 4, 100, 100);
            Constants.Coin_AwardSeedpacket_Pos = new Point(500 - Constants.Board_Offset_AspectRatio_Correction, 300);
            Constants.Coin_Glow_Offset = new Point(8, 6);
            Constants.Coin_Silver_Offset = 6f;
            Constants.Coin_MoneyBag_Offset = new Point(-50, -10);
            Constants.Coin_Shovel_Offset = new Point(5, 30);
            Constants.Coin_Silver_Award_Offset = new Point(21, 40);
            Constants.Coin_Almanac_Offset = new Point(0, 0);
            Constants.Coin_Note_Offset = new Point(0, 10);
            Constants.Coin_CarKeys_Offset = new Point(10, 20);
            Constants.Coin_Taco_Offset = new Point(0, 20);
            Constants.Coin_Bacon_Offset = new Point(0, 20);
            Constants.Plant_CobCannon_Projectile_Offset = new Point(-30, -160);
            Constants.Plant_Squished_Offset = new Point(0, 0);
            Constants.IZombieBrainPosition = 140;
            Constants.IZombie_SeedOffset = new Point(4, 2);
            Constants.IZombie_ClipOffset = new Rectangle(0, 0, 80, 50);
            Constants.ZombieOffsets = new Point[]
            {
                new Point(20, 11),
                new Point(13, 15),
                new Point(22, -30),
                new Point(-30, 0),
                new Point(20, -30),
                new Point(18, 13),
                new Point(19, 13),
                new Point(0, -10),
                new Point(14, -10),
                new Point(19, 12),
                new Point(20, 13),
                new Point(16, 10),
                new Point(1, 11),
                new Point(16, 10),
                new Point(16, 15),
                new Point(14, 10),
                new Point(13, 12),
                new Point(10, -10),
                new Point(20, 12),
                new Point(11, 12),
                new Point(-20, -10),
                new Point(15, -10),
                new Point(4, 12),
                new Point(18, 10),
                new Point(23, 23),
                new Point(1, 15)
            };
            Constants.LastStandButtonRect = new TRect(240, 439, 210, 46);
            Constants.VasebreakerJackInTheBoxOffset = -1;
            Constants.JackInTheBoxPlantRadius = 100;
            Constants.JackInTheBoxZombieRadius = 192;
            Constants.ZenGardenGreenhouseMultiplierX = 1.125f;
            Constants.ZenGardenGreenhouseMultiplierY = 1.182f;
            Constants.ZenGardenGreenhouseOffset = new Point(-30, 30);
            Constants.ZenGardenMushroomGardenOffset = new Point(10, 30);
            Constants.ZenGardenStoreButtonX = 628;
            Constants.ZenGardenStoreButtonY = 45;
            Constants.ZenGardenTopButtonStart = -77;
            Constants.ZenGardenButtonCounterOffset = new Point(60, 45);
            Constants.ZenGardenButton_GoldenWateringCan_Offset = new Point(3, 5);
            Constants.ZenGardenButton_NormalWateringCan_Offset = new Point(3, 5);
            Constants.ZenGardenButton_Fertiliser_Offset = new Point(9, 4);
            Constants.ZenGardenButton_BugSpray_Offset = new Point(17, 6);
            Constants.ZenGardenButton_Phonograph_Offset = new Point(2, 5);
            Constants.ZenGardenButton_Chocolate_Offset = new Point(12, 10);
            Constants.ZenGardenButton_Glove_Offset = new Point(5, 5);
            Constants.ZenGardenButton_MoneySign_Offset = new Point(5, 5);
            Constants.ZenGardenButton_NextGarden_Offset = new Point(2, 0);
            Constants.ZenGardenButton_Wheelbarrow_Offset = new Point(2, 2);
            Constants.ZenGardenButton_WheelbarrowPlant_Offset = new Point(40, 32);
            Constants.ZenGardenButton_Wheelbarrow_Facing_Offset = 43f;
            Constants.ZenGarden_Backdrop_X = 95;
            Constants.ZenGarden_SellDialog_Offset = new Point(214, 90);
            Constants.ZenGarden_NextGarden_Pos = new Point(500, 0);
            Constants.ZenGarden_RetardedDaveBubble_Pos = new Point(-250, -120);
            Constants.ZenGarden_WaterDrop_Pos = new Point(65, 6);
            Constants.ZenGarden_Fertiliser_Pos = new Point(60, 7);
            Constants.ZenGarden_Phonograph_Pos = new Point(60, 7);
            Constants.ZenGarden_BugSpray_Pos = new Point(67, 9);
            Constants.ZenGarden_PlantSpeechBubble_Pos = new Point(50, 0);
            Constants.ZenGarden_StinkySpeechBubble_Pos = new Point(50, -20);
            Constants.ZenGarden_GoldenWater_Pos = new Point(-10, -10);
            Constants.ZenGarden_Chocolate_Pos = new Point(65, -8);
            Constants.ZenGarden_MoneyTarget_X = 477;
            Constants.ZenGarden_TutorialArrow_Offset = 98;
            Constants.ZEN_XMIN = -50;
            Constants.ZEN_YMIN = 75;
            Constants.ZEN_XMAX = 700;
            Constants.ZEN_YMAX = 500;
            Constants.ZenGarden_GoldenWater_Size = new Rectangle(-100, -100, 100, 100);
            Constants.STINKY_SLEEP_POS_Y = 540;
            Constants.gMushroomGridPlacement = new SpecialGridPlacement[]
            {
                new SpecialGridPlacement(80, 435, 0, 0),
                new SpecialGridPlacement(220, 360, 1, 0),
                new SpecialGridPlacement(290, 458, 2, 0),
                new SpecialGridPlacement(355, 296, 3, 0),
                new SpecialGridPlacement(387, 203, 4, 0),
                new SpecialGridPlacement(470, 380, 5, 0),
                new SpecialGridPlacement(500, 472, 6, 0),
                new SpecialGridPlacement(580, 283, 7, 0)
            };
            Constants.ZenGarden_Marigold_Sprout_Offset = new Point(24, 30);
            Constants.ZenGarden_Aquarium_ShadowOffset = new Point(35, 0);
            Constants.Challenge_SeeingStars_StarfruitPreview_Offset_Y = 14;
            Constants.Challenge_SlotMachine_Pos = new Point(210, 425);
            Constants.Challenge_SlotMachineHandle_Pos = new TRect(672, 425, 100, 80);
            Constants.Challenge_SlotMachine_Gap = 13;
            Constants.Challenge_SlotMachine_Offset = 92;
            Constants.Challenge_SlotMachine_Shadow_Offset = 3;
            Constants.Challenge_SlotMachine_Y_Offset = 7;
            Constants.Challenge_SlotMachine_Y_Pos = 3;
            Constants.Challenge_SlotMachine_ClipHeight = 40;
            Constants.Challenge_BeghouldedTwist_Offset = new Point(50, 50);
            Constants.GridItem_ScaryPot_SeedPacket_Offset = new Point(-2, 35);
            Constants.GridItem_ScaryPot_Zombie_Offset = new Point(23, 30);
            Constants.GridItem_ScaryPot_ZombieFootball_Offset = new Point(15, 25);
            Constants.GridItem_ScaryPot_ZombieGargantuar_Offset = new Point(-7, -5);
            Constants.GridItem_ScaryPot_Sun_Offset = new Point(42, 62);
            New.Load();
        }

        public static void Load600x1000()
        {
            Constants.S = 1.0f;
            Constants.IS = 1.0f;
            FrameworkConstants.Font_Scale = 0.8f;
            Constants.Board_Offset_AspectRatio_Correction = 80;
            if (Constants.Language == Constants.LanguageIndex.it)
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(50f);
            }
            else if (Constants.Language == Constants.LanguageIndex.de)
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(85f);
            }
            else if (Constants.Language == Constants.LanguageIndex.fr)
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(85f);
            }
            else if (Constants.Language == Constants.LanguageIndex.es)
            {
                Constants.ReanimTextCenterOffsetX = -(int)Constants.InvertAndScale(10f);
            }
            else
            {
                Constants.ReanimTextCenterOffsetX = (int)Constants.InvertAndScale(85f);
            }
            Constants.Loaded = true;
            Constants.BackBufferSize = new Point(600, 1000);
            Constants.ImageSubPath = "\\480x800\\";
            Constants.BOARD_WIDTH = 1000;
            Constants.BOARD_HEIGHT = 600;
            Constants.BOARD_EDGE = 50;
            Constants.BOARD_OFFSET = 119;
            Constants.BOARD_EXTRA_ROOM = 100;
            Constants.HIGH_GROUND_HEIGHT = 30;
            Constants.GRIDSIZEX = 9;
            Constants.MAX_GRIDSIZEY = 6;
            Constants.MORE_GAMES_ORIGIN_X = 100;
            Constants.MAIN_MENU_ORIGIN_X = 640;
            Constants.ACHIEVEMENTS_ORIGIN_X = 640;
            Constants.QUICKPLAY_ORIGIN_X = 1440;
            Constants.MORE_GAMES_PLANK_HEIGHT = 100;
            Constants.MORE_GAMES_PLANK_WIDTH = 320;
            Constants.MORE_GAMES_ITEM_GAP = 2;
            Constants.SMALL_SEEDPACKET_WIDTH = 81;
            Constants.SMALL_SEEDPACKET_HEIGHT = 52;
            Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET = 480;
            Constants.SCROLL_AREA_OFFSET_X = 102;
            Constants.SCROLL_AREA_OFFSET_Y = (int)Constants.InvertAndScale(28f);
            Constants.DIALOG_HEADER_OFFSET = 36;
            Constants.WIDE_BOARD_WIDTH = 900;
            Constants.ImageWidth = (int)(Constants.S * 1400f);
            Constants.LAWN_XMIN = 140;
            Constants.LAWN_YMIN = 80;
            Constants.SEED_PACKET_HORIZ_GAP = (int)Constants.InvertAndScale(2f);
            Constants.SEED_PACKET_VERT_GAP = (int)Constants.InvertAndScale(1f);
            Constants.SeedPacket_Selector_Pos = new Point(-5, 2);
            Constants.TitleScreen_ReanimStart_X = 130;
            Constants.GameSelector_Width = 2348;
            Constants.GameSelector_Height = (int)Constants.InvertAndScale(640f);
            Constants.GameSelector_AdventureButton_X = 430;
            Constants.GameSelector_MiniGameButton_X = Constants.GameSelector_AdventureButton_X + 21;
            Constants.GameSelector_MiniGameButton_Y = (int)Constants.InvertAndScale(100f);
            Constants.GameSelector_OptionsButton_X = 662;
            Constants.GameSelector_OptionsButton_Y = 276;
            Constants.GameSelector_ZenGardenButton_X = 418;
            Constants.GameSelector_ZenGardenButton_Y = 349;
            Constants.GameSelector_AlmanacButton_X = 536;
            Constants.GameSelector_AlmanacButton_Y = 334;
            Constants.GameSelector_StoreButton_X = 375;
            Constants.GameSelector_StoreButton_Y = 273;
            Constants.GameSelector_LeaderboardButton_X = 0;
            Constants.GameSelector_LeaderboardButton_Y = (int)Constants.InvertAndScale(200f);
            Constants.GameSelector_AchievementsButton_X = 211;
            Constants.GameSelector_AchievementsButton_Y = 429;
            Constants.GameSelector_AchievementsStatue_X = 212;
            Constants.GameSelector_AchievementsStatue_Y = 252;
            Constants.GameSelector_MoreWaysToPlay_MiniGames_X = 236;
            Constants.GameSelector_MoreWaysToPlay_MiniGames_Y = (int)Constants.InvertAndScale(160f);
            Constants.GameSelector_MoreWaysToPlay_VaseBreaker_X = 525;
            Constants.GameSelector_MoreWaysToPlay_VaseBreaker_Y = (int)Constants.InvertAndScale(175f);
            Constants.GameSelector_MoreWaysToPlay_IZombie_X = 350;
            Constants.GameSelector_MoreWaysToPlay_IZombie_Y = (int)Constants.InvertAndScale(195f);
            Constants.GameSelector_MoreWaysToPlay_Back_X = 330;
            Constants.GameSelector_MoreWaysToPlay_Back_Y = (int)Constants.InvertAndScale(270f);
            Constants.GameSelector_PlayerName_Pos = new Point(167, -190);
            Constants.GameSelector_LevelNumber_1_Pos = new Point(553, 32);
            Constants.GameSelector_LevelNumber_2_Pos = new Point(580, 33);
            Constants.GameSelector_LevelNumber_3_Pos = new Point(592, 34);
            Constants.GameSelector_LevelNumber_Bar = new Rectangle(573, 44, 5, 2);
            Constants.GameSelector_LevelNumber_ButtonDown_Offset = 6;
            Constants.GameSelector_Update_Offset = 110;
            Constants.LeaderboardScreen_Vasebreaker_Button_X = 633;
            Constants.LeaderboardScreen_Vasebreaker_Button_Y = 339;
            Constants.LeaderboardScreen_IZombie_Button_X = 125;
            Constants.LeaderboardScreen_IZombie_Button_Y = 306;
            Constants.LeaderboardScreen_Killed_Button_X = 405;
            Constants.LeaderboardScreen_Killed_Button_Y = 306;
            Constants.LeaderboardScreen_PileBase_X = 331;
            Constants.LeaderboardScreen_PileBase_Y = 53;
            Constants.Leaderboard_IZombie_Score_X = 198;
            Constants.Leaderboard_IZombie_Score_Y = 290;
            Constants.Leaderboard_Vasebreaker_Score_X = 698;
            Constants.Leaderboard_Vasebreaker_Score_Y = 322;
            Constants.Leaderboard_Pile_1_X = 297;
            Constants.LeaderboardDialog_CannotConnect_Rect = new TRect(30, 70, 570, 300);
            Constants.LeaderboardScreen_EdgeOfSpace_Overlay_Offset = 30;
            Constants.AchievementWidget_ROW_HEIGHT = 135;
            Constants.AchievementWidget_ROW_START = 275;
            Constants.AchievementWidget_HOLE_DEPTH = 128;
            Constants.AchievementWidget_GAMERSCORE_POS = new Point(217, 345);
            Constants.AchievementWidget_BackButton_X = 152;
            Constants.AchievementWidget_BackButton_Y = 6;
            Constants.AchievementWidget_BackButton_Rect = new TRect(152, 0, 170, 120);
            Constants.AchievementWidget_Background_Offset_Y = 412;
            Constants.AchievementWidget_Description_Box = new Rectangle(270, 30, 400, 70);
            Constants.AchievementWidget_Pipe_Offset = new Point(0, 140);
            Constants.AchievementWidget_Worm_Offset = new Point(659, -104);
            Constants.AchievementWidget_ZombieWorm_Offset = new Point(76, -9);
            Constants.AchievementWidget_GemLeft_Offset = new Point(180, 0);
            Constants.AchievementWidget_GemRight_Offset = new Point(450, -69);
            Constants.AchievementWidget_Fossile_Offset = new Point(239, -55);
            Constants.AchievementWidget_Image_Pos = new Point(200, 10);
            Constants.AchievementWidget_Image_Size = 64;
            Constants.AchievementWidget_Name_Pos = new Point(270, 0);
            Constants.AchievementWidget_Name_MaxWidth = 400;
            Constants.QuickPlayWidget_Thumb_X = 20;
            Constants.QuickPlayWidget_Thumb_Y = 15;
            Constants.QuickPlayWidget_Bungee_Y = -52;
            Constants.SeedChooserScreen_MenuButton_X = 665;
            Constants.SeedChooserScreen_Background_Top = new Point(88, 4);
            Constants.SeedChooserScreen_Background_Middle = new Point(Constants.SeedChooserScreen_Background_Top.X, Constants.SeedChooserScreen_Background_Top.Y + 44);
            Constants.SeedChooserScreen_Background_Middle_Height = 325;
            Constants.SeedChooserScreen_Background_Bottom = new Point(Constants.SeedChooserScreen_Background_Top.X, Constants.SeedChooserScreen_Background_Middle.Y + Constants.SeedChooserScreen_Background_Middle_Height);
            Constants.SeedChooserScreen_Gradient_Top = new Rectangle(0, 40, 336, 22);
            Constants.SeedChooserScreen_Gradient_Bottom = new Rectangle(0, 362, 336, 22);
            Constants.SeedPacket_Cost = new Point(42, 26);
            Constants.SeedPacket_Cost_IZombie = new Point(41, 27);
            Constants.SeedPacket_CostText_Pos = new Point(74, 22);
            Constants.SeedPacket_CostText_IZombie_Pos = new Point(73, 23);
            Constants.ImitaterDialog_Size = new Point(0, 240);
            Constants.ImitaterDialog_ScrollWidget_Offset_X = 5;
            Constants.ImitaterDialog_ScrollWidget_Y = 120;
            Constants.ImitaterDialog_ScrollWidget_ExtraWidth = 20;
            Constants.ImitaterDialog_Height = 250;
            Constants.ImitaterDialog_BottomGradient_Y = 235;
            Constants.CutScene_ReadySetPlant_Pos = new Point(370, 260);
            Constants.CutScene_LogoEndPos = 350;
            Constants.CutScene_LogoBackRect_Height = 150;
            Constants.CutScene_LogoEnd_Particle_Pos = new Point(600, 400);
            Constants.CutScene_ExtraRoom_1_Particle_Pos = new Point(35, 348);
            Constants.CutScene_ExtraRoom_2_Particle_Pos = new Point(Constants.CutScene_ExtraRoom_1_Particle_Pos.X, 246);
            Constants.CutScene_ExtraRoom_3_Particle_Pos = new Point(Constants.CutScene_ExtraRoom_1_Particle_Pos.X, 459);
            Constants.CutScene_ExtraRoom_4_Particle_Pos = new Point(32, 150);
            Constants.CutScene_ExtraRoom_5_Particle_Pos = new Point(32, 551);
            Constants.CutScene_SodRoll_1_Pos = -105;
            Constants.CutScene_SodRoll_2_Pos = 107;
            Constants.CutScene_SodRoll_3_Pos = new Point(-6, -205);
            Constants.CutScene_SodRoll_4_Pos = new Point(-6, 205);
            Constants.CutScene_Upsell_TerraCotta_Arrow = new Point(733, 360);
            Constants.CutScene_Upsell_TerraCotta_Pot = new Point(565, 360);
            Constants.ConveyorBeltClipRect = new TRect(0, 8, 83, 465);
            Constants.StoreScreen_BackButton_X = 430;
            Constants.StoreScreen_BackButton_Y = 390;
            Constants.StoreScreen_Car_X = 260;
            Constants.StoreScreen_Car_Y = 80;
            Constants.StoreScreen_PrevButton_X = 317;
            Constants.StoreScreen_PrevButton_Y = 296;
            Constants.StoreScreen_NextButton_X = 645;
            Constants.StoreScreen_NextButton_Y = 296;
            Constants.StoreScreen_HatchOpen_X = 316;
            Constants.StoreScreen_HatchOpen_Y = -35;
            Constants.StoreScreen_HatchClosed_X = 309;
            Constants.StoreScreen_HatchClosed_Y = 81;
            Constants.StoreScreen_CarNight_X = 260;
            Constants.StoreScreen_CarNight_Y = 80;
            Constants.StoreScreen_StoreSign_X = 450;
            Constants.StoreScreen_StoreSign_Y_Min = -150;
            Constants.StoreScreen_StoreSign_Y_Max = -10;
            Constants.StoreScreen_Coinbank_X = 590;
            Constants.StoreScreen_Coinbank_Y = 405;
            Constants.StoreScreen_Coinbank_TextOffset = new Point(180, 8);
            Constants.StoreScreen_ItemOffset_1_X = 430;
            Constants.StoreScreen_ItemOffset_1_Y = 102;
            Constants.StoreScreen_ItemOffset_2_X = 390;
            Constants.StoreScreen_ItemOffset_2_Y = 190;
            Constants.StoreScreen_ItemSize = 85;
            Constants.StoreScreen_ItemSize_Offset = 4;
            Constants.StoreScreen_PriceTag_X = 3;
            Constants.StoreScreen_PriceTag_Y = 70;
            Constants.StoreScreen_PriceTag_Text_Offset_X = 35;
            Constants.StoreScreen_PriceTag_Text_Offset_Y = 67;
            Constants.StoreScreen_ComingSoon_X = 100;
            Constants.StoreScreen_ComingSoon_Y = 70;
            Constants.StoreScreen_SoldOut_Width = 80;
            Constants.StoreScreen_SoldOut_Y = 0;
            Constants.StoreScreen_SoldOut_Height = 100;
            Constants.StoreScreen_PacketUpgrade_X = 10;
            Constants.StoreScreen_PacketUpgrade_Y = 5;
            Constants.StoreScreen_PacketUpgrade_Text_Size = new Rectangle(0, 0, 80, 60);
            Constants.StoreScreen_RetardedDave_Offset_X = -10;
            Constants.StoreScreen_FirstAidNut_Offset_Y = 10;
            Constants.StoreScreen_PoolCleaner_Offset_X = 23;
            Constants.StoreScreen_PoolCleaner_Offset_Y = 24;
            Constants.StoreScreen_Rake_Offset_X = -10;
            Constants.StoreScreen_Rake_Offset_Y = 24;
            Constants.StoreScreen_RoofCleaner_Offset_X = 15;
            Constants.StoreScreen_RoofCleaner_Offset_Y = 38;
            Constants.StoreScreen_Imitater_Offset_X = -1;
            Constants.StoreScreen_Imitater_Offset_Y = 24;
            Constants.StoreScreen_Default_Offset_Y = 20;
            Constants.StoreScreen_MouseRegion = new Point(75, 100);
            Constants.StoreScreen_Dialog = new Rectangle(200, 50, 400, 450);
            Constants.StoreScreen_PotPlant_Offset = new Point(40, 70);
            Constants.StoreScreenMushroomGardenOffsetX = 15;
            Constants.StoreScreenAquariumGardenOffsetX = 15;
            Constants.StoreScreenGloveOffsetY = 15;
            Constants.StoreScreenWheelbarrowOffsetY = 15;
            Constants.StoreScreenBugSprayOffsetX = 17;
            Constants.StoreScreenBugSprayOffsetY = 15;
            Constants.StoreScreenPhonographOffsetY = 15;
            Constants.StoreScreenPlantFoodOffsetX = 13;
            Constants.StoreScreenPlantFoodOffsetY = 15;
            Constants.StoreScreenWateringCanOffsetX = 6;
            Constants.StoreScreenWateringCanOffsetY = 15;
            Constants.NewOptionsDialog_FXLabel_X = 340;
            Constants.NewOptionsDialog_FXLabel_Y = 143;
            Constants.NewOptionsDialog_MusicLabel_X = 340;
            Constants.NewOptionsDialog_MusicLabel_On_Y = 97;
            Constants.NewOptionsDialog_MusicLabel_Off_Y = 103;
            Constants.NewOptionsDialog_VibrationLabel_X = 340;
            Constants.NewOptionsDialog_VibrationLabel_Y = 193;
            Constants.NewOptionsDialog_LockedLabel_Y = 265;
            Constants.NewOptionsDialog_VibrationLabel_MaxWidth = 300;
            Constants.NewOptionsDialog_FullScreenOffset = 25;
            Constants.NewOptionsDialog_FX_Offset = 15;
            Constants.NewOptionsDialog_Music_Offset = 5;
            Constants.NewOptionsDialog_Version_Low_Y = 340;
            Constants.NewOptionsDialog_Version_High_Y = 345;
            Constants.RetardedDave_Bubble_Tip_Offset = 5;
            Constants.RetardedDave_Bubble_Offset_Shop = new Point(-280, -117);
            Constants.RetardedDave_Bubble_Offset_Board = new Point(-200, -60);
            Constants.RetardedDave_Bubble_Size = 400;
            Constants.RetardedDave_Bubble_Rect = new TRect(15, 6, 0, 250);
            Constants.RetardedDave_Bubble_TapToContinue_Y = 230;
            Constants.PlantGallerySize = new Point(390, 750);
            Constants.ZombieGallerySize = new Point(415, 1068);
            Constants.AlmanacHeaderY = 8;
            Constants.Almanac_PlantsButtonRect = new TRect(67, 150, 300, 240);
            Constants.Almanac_ZombiesButtonRect = new TRect(450, 150, 300, 240);
            Constants.Almanac_CloseButtonRect = new TRect(660, 435, 148, 39);
            Constants.Almanac_IndexButtonRect = new TRect(442, 435, 148, 39);
            Constants.Almanac_IndexPlantPos = new Point(183, 240);
            Constants.Almanac_IndexZombiePos = new Point(553, 225);
            Constants.Almanac_IndexPlantTextPos = new Point(Constants.Almanac_PlantsButtonRect.mX + Constants.Almanac_PlantsButtonRect.mWidth / 2, Constants.Almanac_PlantsButtonRect.mY + Constants.Almanac_PlantsButtonRect.mHeight - 65);
            Constants.Almanac_IndexZombieTextPos = new Point(Constants.Almanac_ZombiesButtonRect.mX + Constants.Almanac_ZombiesButtonRect.mWidth / 2, Constants.Almanac_ZombiesButtonRect.mY + Constants.Almanac_ZombiesButtonRect.mHeight - 65);
            Constants.Almanac_PlantScrollRect = new TRect(47, 75, 380, 368);
            Constants.Almanac_ZombieScrollRect = new TRect(28, 75, 410, 368);
            Constants.Almanac_DescriptionScrollRect = new TRect(470, 255, 305, 167);
            Constants.Almanac_PlantTopGradientY = 74;
            Constants.Almanac_ZombieTopGradientY = 72;
            Constants.Almanac_BottomGradientY = 432;
            Constants.Almanac_PlantGradientWidth = 360;
            Constants.Almanac_ZombieGradientWidth = 390;
            Constants.Almanac_ZombieStoneRect = new TRect(445, 2, 353, 465);
            Constants.Almanac_BackgroundPosition = new Point(533, 60);
            Constants.Almanac_ZombieClipRect = new TRect(-40, -45, 175, 175);
            Constants.Almanac_NavyRect = new TRect(515, 40, 196, 198);
            Constants.Almanac_NamePosition = new Point(622, 0);
            Constants.Almanac_ClayRect = new TRect(450, 6, 340, 465);
            Constants.Almanac_BrownRect = new TRect(513, 41, 198, 198);
            Constants.Almanac_ZombieSpace = new Point(137, 117);
            Constants.Almanac_ZombieOffset = new Point(0, 5);
            Constants.Almanac_BossPosition = new Point(205, 944);
            Constants.Almanac_ImpPosition = new Point(68, 944);
            Constants.Almanac_ImitatorPosition = new Point(Constants.PlantGallerySize.X / 2 - 52, 688);
            Constants.Almanac_SeedSpace = new Point(4, 4);
            Constants.Almanac_SeedOffset = new Point(16, 8);
            Constants.Almanac_ZombiePosition = new Point(700, 120);
            Constants.Almanac_PlantPosition = new Point(583, 105);
            Constants.Almanac_Text_Scale = 0.8f;
            Constants.Almanac_PlantsHeader_Pos = new Point(32, 2);
            Constants.Almanac_ZombieHeader_Pos = new Point(25, 2);
            Constants.Almanac_ItemName_MaxWidth = 330;
            Constants.Zombie_StartOffset = 110f;
            Constants.Zombie_StartRandom_Offset = 40f;
            Constants.AwardScreen_Note_Credits_Background = new Rectangle(-500, -170, 1600, 800);
            Constants.AwardScreen_Note_Credits_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_Credits_Text = new Rectangle(180, 80, 429, 287);
            Constants.AwardScreen_Note_Help_Background = new Rectangle(-350, -150, 1700, 700);
            Constants.AwardScreen_Note_Help_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_Help_Text = new Point(180, 95);
            Constants.AwardScreen_Note_1_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_1_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_1_Text = new Point(180, 85);
            Constants.AwardScreen_Note_Message = new Point(400, 0);
            Constants.AwardScreen_Note_2_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_2_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_2_Text = new Point(175, 85);
            Constants.AwardScreen_Note_3_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_3_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_3_Text = new Point(165, 80);
            Constants.AwardScreen_Note_4_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_4_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_4_Text = new Point(160, 80);
            Constants.AwardScreen_Note_Final_Background = new Rectangle(-300, -150, 1600, 700);
            Constants.AwardScreen_Note_Final_Paper = new Point(138, 50);
            Constants.AwardScreen_Note_Final_Text = new Point(170, 95);
            Constants.AwardScreen_Bacon = (Constants.AwardScreen_Taco = (Constants.AwardScreen_CarKeys = new Point(191, 210)));
            Constants.AwardScreen_WateringCan = new Point(180, 210);
            Constants.AwardScreen_Almanac = new Point(195, 185);
            Constants.AwardScreen_Trophy = new Point((int)(400f * Constants.S), (int)(137f * Constants.S));
            Constants.AwardScreen_Shovel = new Point(195, 195);
            Constants.AwardScreen_MenuButton = new Point(610, 420);
            Constants.AwardScreen_ClayTablet = new TRect(50, 70, 700, 300);
            Constants.AwardScreen_TitlePos = new Point(400, 10);
            Constants.AwardScreen_GroundDay_Pos = new Point(111, 167);
            Constants.AwardScreen_BrownRect = new TRect(90, 145, 200, 200);
            Constants.AwardScreen_BottomMessage_Pos = new Point(400, 70);
            Constants.AwardScreen_BottomText_Rect_Text = new TRect(317, 160, 400, 170);
            Constants.AwardScreen_BottomText_Rect_NoText = new TRect(150, 120, 500, 200);
            Constants.AwardScreen_CreditsButton = new TRect(180, 413, 140, 48);
            Constants.AwardScreen_CreditsButton_Offset = new Point(50, -7);
            if (Constants.Language == Constants.LanguageIndex.es)
            {
                Constants.AwardScreen_CreditsButton_Offset.X = 70;
            }
            else if (Constants.Language == Constants.LanguageIndex.it)
            {
                Constants.AwardScreen_CreditsButton_Offset.X = 70;
            }
            else if (Constants.Language == Constants.LanguageIndex.de)
            {
                Constants.AwardScreen_CreditsButton_Offset.X = 60;
            }
            Constants.AwardScreen_Seed_Pos = new Point(151, 220);
            Constants.AwardScreen_ContinueButton_Offset = 35;
            Constants.AwardScreen_AchievementImage_Pos = new Point(80, 4);
            Constants.AwardScreen_AchievementName_Pos = new Point(200, 5);
            Constants.AwardScreen_AchievementDescription_Rect = new Rectangle(200, 30, 400, 60);
            Constants.CreditScreen_ReplayButton = new Rectangle(100, 400, 200, 48);
            Constants.CreditScreen_ReplayButton_TextOffset = new Point(-5, -7);
            Constants.CreditScreen_MainMenu = new Rectangle(500, 400, 200, 50);
            Constants.CreditScreen_TextStart = 200;
            Constants.CreditScreen_TextEnd = 400;
            Constants.CreditScreen_TextClip = 370;
            Constants.CreditScreen_LeftText_X = 0;
            Constants.CreditScreen_RightText_X = 390;
            Constants.CreditScreen_LeftRight_Text_Width = 370;
            Constants.ConfirmPurchaseDialog_Background = new TRect(38, 100, 312, 250);
            Constants.ConfirmPurchaseDialog_Item_Pos = new Point(60, 155);
            Constants.ConfirmPurchaseDialog_Text = new TRect(145, 95, 190, 265);
            Constants.LawnDialog_Insets = new Insets(60, 20, 70, 35);
            Constants.UILevelPosition = new Point(793, 445);
            Constants.UIMenuButtonPosition = new Point(650 - Constants.Board_Offset_AspectRatio_Correction, 3);
            Constants.UIMenuButtonWidth = 150;
            Constants.UISunBankPositionX = 95;
            Constants.UISunBankTextOffset = new Point(92, 3);
            Constants.UIShovelButtonPosition = new Point(260, 3);
            Constants.UIProgressMeterPosition = new Point(370, 3);
            Constants.UIProgressMeterHeadEnd = 220;
            Constants.UIProgressMeterBarEnd = 225;
            Constants.UICoinBankPosition = new Point(110 - Constants.Board_Offset_AspectRatio_Correction, 479);
            Constants.Board_Cutscene_ExtraScroll = 170;
            Constants.Board_SunCoinRange = 550;
            Constants.Board_SunCoin_CollectTarget = new Point(115 - (int)(Board_Offset_AspectRatio_Correction * Constants.IS), 5);
            Constants.Board_Cel_Y_Values_Normal = new int[]
            {
                80,
                177,
                280,
                380,
                470,
                570,
                610
            };
            Constants.Board_Cel_Y_Values_Pool = new int[]
            {
                80,
                170,
                275,
                360,
                447,
                522,
                610
            };
            Constants.Board_Cel_Y_Values_ZenGarden = new int[]
            {
                125,
                225,
                335,
                455,
                560,
                750,
                750
            };
            Constants.Board_GameOver_Interior_Overlay_1 = new Point(-15, 182);
            Constants.Board_GameOver_Interior_Overlay_2 = new Point(-17, 160);
            Constants.Board_GameOver_Interior_Overlay_3 = new Point(-60, 178);
            Constants.Board_GameOver_Interior_Overlay_4 = new Point(-56, 202);
            Constants.Board_GameOver_Exterior_Overlay_1 = new Point(-22, 142);
            Constants.Board_GameOver_Exterior_Overlay_2 = new Point(-21, 148);
            Constants.Board_GameOver_Exterior_Overlay_3 = new Point(-57, 166);
            Constants.Board_GameOver_Exterior_Overlay_4 = new Point(-58, 105);
            Constants.Board_GameOver_Exterior_Overlay_5 = new Point(5, 179);
            Constants.Board_GameOver_Exterior_Overlay_6 = new Point(5, 179);
            Constants.Board_Ice_Start = 1000;
            Constants.Board_ProgressBarText_Pos = 1;
            Constants.MessageWidget_SlotMachine_Y = 518;
            Constants.LawnMower_Coin_Offset = new Point(20, 5);
            Constants.DescriptionWidget_ScrollBar_Padding = 10;
            Constants.Zombie_Bungee_Offset = new Point(61, 15);
            Constants.Zombie_Bungee_Target_Offset = new Point(5, 40);
            Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT = -145;
            Constants.Zombie_Dancer_Dance_Limit_X = 1000;
            Constants.Zombie_Dancer_Spotlight_Scale = 2.7f;
            Constants.Zombie_Dancer_Spotlight_Offset = new Point(-45, 50);
            Constants.Zombie_Dancer_Spotlight_Pos = new Point(2, -560);
            Constants.Zombie_ClipOffset_Default = 100;
            Constants.Zombie_ClipOffset_Snorkel = 0;
            Constants.Zombie_ClipOffset_Snorkel_intoPool_Small = 40;
            Constants.Zombie_ClipOffset_Snorkel_Dying = 76;
            Constants.Zombie_ClipOffset_Snorkel_Dying_Small = 65;
            Constants.Zombie_ClipOffset_Pail = 78;
            Constants.Zombie_ClipOffset_Normal = 108;
            Constants.Zombie_ClipOffset_Digger = 75;
            Constants.Zombie_ClipOffset_Dolphin_Into_Pool = 65;
            Constants.Zombie_ClipOffset_Snorkel_Grabbed = 75;
            Constants.Zombie_ClipOffset_PeaHead_InPool = 78;
            Constants.Zombie_ClipOffset_RisingFromGrave = 20;
            Constants.Zombie_ClipOffset_RisingFromGrave_Small = -35;
            Constants.Zombie_ClipOffset_Snorkel_Into_Pool = 80;
            Constants.Zombie_ClipOffset_Normal_In_Pool = 30;
            Constants.Zombie_ClipOffset_Flag_In_Pool = 30;
            Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL = -40;
            Constants.Zombie_ClipOffset_TrafficCone_In_Pool_SMALL = -55;
            Constants.Zombie_ClipOffset_Ducky_Dying_In_Pool = 27;
            Constants.Zombie_GameOver_ClipOffset_1 = 77;
            Constants.Zombie_GameOver_ClipOffset_2 = 80;
            Constants.Zombie_GameOver_ClipOffset_3 = 85;
            Constants.ZombieGalleryWidget_Window_Offset = new Point(3, 7);
            Constants.ZombieGalleryWidget_Window_Clip = new Rectangle(6, 4, 100, 100);
            Constants.Coin_AwardSeedpacket_Pos = new Point(500 - Constants.Board_Offset_AspectRatio_Correction, 300);
            Constants.Coin_Glow_Offset = new Point(8, 6);
            Constants.Coin_Silver_Offset = 6f;
            Constants.Coin_MoneyBag_Offset = new Point(-50, -10);
            Constants.Coin_Shovel_Offset = new Point(5, 30);
            Constants.Coin_Silver_Award_Offset = new Point(21, 40);
            Constants.Coin_Almanac_Offset = new Point(0, 0);
            Constants.Coin_Note_Offset = new Point(0, 10);
            Constants.Coin_CarKeys_Offset = new Point(10, 20);
            Constants.Coin_Taco_Offset = new Point(0, 20);
            Constants.Coin_Bacon_Offset = new Point(0, 20);
            Constants.Plant_CobCannon_Projectile_Offset = new Point(-30, -160);
            Constants.Plant_Squished_Offset = new Point(5, 20);
            Constants.IZombieBrainPosition = 140;
            Constants.IZombie_SeedOffset = new Point(4, 2);
            Constants.IZombie_ClipOffset = new Rectangle(0, 0, 80, 50);
            Constants.ZombieOffsets = new Point[]
            {
                new Point(20, 11),
                new Point(13, 15),
                new Point(22, -30),
                new Point(-30, 0),
                new Point(20, -30),
                new Point(18, 13),
                new Point(19, 13),
                new Point(0, -10),
                new Point(14, -10),
                new Point(19, 12),
                new Point(20, 13),
                new Point(16, 10),
                new Point(1, 11),
                new Point(16, 10),
                new Point(16, 15),
                new Point(14, 10),
                new Point(13, 12),
                new Point(10, -10),
                new Point(20, 12),
                new Point(11, 12),
                new Point(-20, -10),
                new Point(15, -10),
                new Point(4, 12),
                new Point(18, 10),
                new Point(23, 23),
                new Point(1, 15)
            };
            Constants.LastStandButtonRect = new TRect(240, 439, 210, 46);
            Constants.VasebreakerJackInTheBoxOffset = -1;
            Constants.JackInTheBoxPlantRadius = 100;
            Constants.JackInTheBoxZombieRadius = 192;
            Constants.ZenGardenGreenhouseMultiplierX = 1.125f;
            Constants.ZenGardenGreenhouseMultiplierY = 1.182f;
            Constants.ZenGardenGreenhouseOffset = new Point(-30, 30);
            Constants.ZenGardenMushroomGardenOffset = new Point(10, 30);
            Constants.ZenGardenStoreButtonX = 628;
            Constants.ZenGardenStoreButtonY = 45;
            Constants.ZenGardenTopButtonStart = -77;
            Constants.ZenGardenButtonCounterOffset = new Point(60, 45);
            Constants.ZenGardenButton_GoldenWateringCan_Offset = new Point(3, 5);
            Constants.ZenGardenButton_NormalWateringCan_Offset = new Point(3, 5);
            Constants.ZenGardenButton_Fertiliser_Offset = new Point(9, 4);
            Constants.ZenGardenButton_BugSpray_Offset = new Point(17, 6);
            Constants.ZenGardenButton_Phonograph_Offset = new Point(2, 5);
            Constants.ZenGardenButton_Chocolate_Offset = new Point(12, 10);
            Constants.ZenGardenButton_Glove_Offset = new Point(5, 5);
            Constants.ZenGardenButton_MoneySign_Offset = new Point(5, 5);
            Constants.ZenGardenButton_NextGarden_Offset = new Point(2, 0);
            Constants.ZenGardenButton_Wheelbarrow_Offset = new Point(2, 2);
            Constants.ZenGardenButton_WheelbarrowPlant_Offset = new Point(40, 32);
            Constants.ZenGardenButton_Wheelbarrow_Facing_Offset = 43f;
            Constants.ZenGarden_Backdrop_X = 95;
            Constants.ZenGarden_SellDialog_Offset = new Point(214, 90);
            Constants.ZenGarden_NextGarden_Pos = new Point(500, 0);
            Constants.ZenGarden_RetardedDaveBubble_Pos = new Point(-250, -120);
            Constants.ZenGarden_WaterDrop_Pos = new Point(65, 6);
            Constants.ZenGarden_Fertiliser_Pos = new Point(60, 7);
            Constants.ZenGarden_Phonograph_Pos = new Point(60, 7);
            Constants.ZenGarden_BugSpray_Pos = new Point(67, 9);
            Constants.ZenGarden_PlantSpeechBubble_Pos = new Point(50, 0);
            Constants.ZenGarden_StinkySpeechBubble_Pos = new Point(50, -20);
            Constants.ZenGarden_GoldenWater_Pos = new Point(-10, -10);
            Constants.ZenGarden_Chocolate_Pos = new Point(65, -8);
            Constants.ZenGarden_MoneyTarget_X = 477;
            Constants.ZenGarden_TutorialArrow_Offset = 98;
            Constants.ZEN_XMIN = -50;
            Constants.ZEN_YMIN = 75;
            Constants.ZEN_XMAX = 700;
            Constants.ZEN_YMAX = 500;
            Constants.ZenGarden_GoldenWater_Size = new Rectangle(-100, -100, 100, 100);
            Constants.STINKY_SLEEP_POS_Y = 540;
            Constants.gMushroomGridPlacement = new SpecialGridPlacement[]
            {
                new SpecialGridPlacement(80, 435, 0, 0),
                new SpecialGridPlacement(220, 360, 1, 0),
                new SpecialGridPlacement(290, 458, 2, 0),
                new SpecialGridPlacement(355, 296, 3, 0),
                new SpecialGridPlacement(387, 203, 4, 0),
                new SpecialGridPlacement(470, 380, 5, 0),
                new SpecialGridPlacement(500, 472, 6, 0),
                new SpecialGridPlacement(580, 283, 7, 0)
            };
            Constants.ZenGarden_Marigold_Sprout_Offset = new Point(24, 30);
            Constants.ZenGarden_Aquarium_ShadowOffset = new Point(35, 0);
            Constants.Challenge_SeeingStars_StarfruitPreview_Offset_Y = 14;
            Constants.Challenge_SlotMachine_Pos = new Point(210, 425);
            Constants.Challenge_SlotMachineHandle_Pos = new TRect(672, 425, 100, 80);
            Constants.Challenge_SlotMachine_Gap = 13;
            Constants.Challenge_SlotMachine_Offset = 92;
            Constants.Challenge_SlotMachine_Shadow_Offset = 3;
            Constants.Challenge_SlotMachine_Y_Offset = 7;
            Constants.Challenge_SlotMachine_Y_Pos = 3;
            Constants.Challenge_SlotMachine_ClipHeight = 40;
            Constants.Challenge_BeghouldedTwist_Offset = new Point(50, 50);
            Constants.GridItem_ScaryPot_SeedPacket_Offset = new Point(-2, 35);
            Constants.GridItem_ScaryPot_Zombie_Offset = new Point(23, 30);
            Constants.GridItem_ScaryPot_ZombieFootball_Offset = new Point(15, 25);
            Constants.GridItem_ScaryPot_ZombieGargantuar_Offset = new Point(-7, -5);
            Constants.GridItem_ScaryPot_Sun_Offset = new Point(42, 62);
            New.Load();
        }

        public const int PC_BOARD_WIDTH = 800;

        public const int PC_BOARD_HEIGHT = 600;

        public static bool Loaded;

        public static string ImageSubPath;

        public static DisplayOrientation SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

        private static Constants.LanguageIndex mLanguage;

        public static string LanguageSubDir;

        public static SexyColor YellowFontColour = new SexyColor(227, 173, 57);

        public static SexyColor GreenFontColour = new SexyColor(0, 203, 0);

        public static SexyColor Almanac_Paper_Colour = new SexyColor(242, 182, 123);

        public static Point BackBufferSize;

        public static int BOARD_WIDTH;

        public static int BOARD_HEIGHT;

        public static int BOARD_EDGE;

        public static int BOARD_OFFSET;

        public static int BOARD_EXTRA_ROOM;

        public static int HIGH_GROUND_HEIGHT;

        public static int GRIDSIZEX;

        public static int MAX_GRIDSIZEY;

        public static int MORE_GAMES_ORIGIN_X;

        public static int MAIN_MENU_ORIGIN_X;

        public static int ACHIEVEMENTS_ORIGIN_X;

        public static int QUICKPLAY_ORIGIN_X;

        public static int MORE_GAMES_PLANK_HEIGHT;

        public static int MORE_GAMES_PLANK_WIDTH;

        public static int MORE_GAMES_ITEM_GAP;

        public static int SEED_CHOOSER_OFFSETSCREEN_OFFSET;

        public static int SMALL_SEEDPACKET_WIDTH;

        public static int SMALL_SEEDPACKET_HEIGHT;

        public static int DIALOG_HEADER_OFFSET;

        public static int WIDE_BOARD_WIDTH;

        public static int ImageWidth;

        public static int LAWN_XMIN;

        public static int LAWN_YMIN;

        public static int SCROLL_AREA_OFFSET_X = 71;

        public static int SCROLL_AREA_OFFSET_Y = 28;

        public static int SEED_PACKET_HORIZ_GAP = 2;

        public static int SEED_PACKET_VERT_GAP = 1;

        public static Point SeedPacket_Selector_Pos;

        public static float S;

        public static float IS;

        public static int ReanimTextCenterOffsetX;

        public static int TitleScreen_ReanimStart_X;

        public static int GameSelector_Width;

        public static int GameSelector_Height;

        public static int GameSelector_AdventureButton_X;

        public static int GameSelector_MiniGameButton_X;

        public static int GameSelector_MiniGameButton_Y;

        public static int GameSelector_PuzzleButton_X;

        public static int GameSelector_PuzzleButton_Y;

        public static int GameSelector_OptionsButton_X;

        public static int GameSelector_OptionsButton_Y;

        public static int GameSelector_ZenGardenButton_X;

        public static int GameSelector_ZenGardenButton_Y;

        public static int GameSelector_AlmanacButton_X;

        public static int GameSelector_AlmanacButton_Y;

        public static int GameSelector_StoreButton_X;

        public static int GameSelector_StoreButton_Y;

        public static int GameSelector_AchievementsButton_X;

        public static int GameSelector_AchievementsButton_Y;

        public static int GameSelector_AchievementsStatue_X;

        public static int GameSelector_AchievementsStatue_Y;

        public static int GameSelector_MoreWaysToPlay_MiniGames_X;

        public static int GameSelector_MoreWaysToPlay_MiniGames_Y;

        public static int GameSelector_MoreWaysToPlay_VaseBreaker_X;

        public static int GameSelector_MoreWaysToPlay_VaseBreaker_Y;

        public static int GameSelector_MoreWaysToPlay_IZombie_X;

        public static int GameSelector_MoreWaysToPlay_IZombie_Y;

        public static int GameSelector_MoreWaysToPlay_Back_X;

        public static int GameSelector_MoreWaysToPlay_Back_Y;

        public static int GameSelector_LeaderboardButton_X;

        public static int GameSelector_LeaderboardButton_Y;

        public static Point GameSelector_PlayerName_Pos;

        public static Point GameSelector_LevelNumber_1_Pos;

        public static Point GameSelector_LevelNumber_2_Pos;

        public static Point GameSelector_LevelNumber_3_Pos;

        public static Rectangle GameSelector_LevelNumber_Bar;

        public static int GameSelector_LevelNumber_ButtonDown_Offset;

        public static int GameSelector_Update_Offset;

        public static int LeaderboardScreen_Vasebreaker_Button_X;

        public static int LeaderboardScreen_Vasebreaker_Button_Y;

        public static int LeaderboardScreen_IZombie_Button_X;

        public static int LeaderboardScreen_IZombie_Button_Y;

        public static int LeaderboardScreen_Killed_Button_X;

        public static int LeaderboardScreen_Killed_Button_Y;

        public static int LeaderboardScreen_PileBase_X;

        public static int LeaderboardScreen_PileBase_Y;

        public static int Leaderboard_IZombie_Score_X;

        public static int Leaderboard_IZombie_Score_Y;

        public static int Leaderboard_Vasebreaker_Score_X;

        public static int Leaderboard_Vasebreaker_Score_Y;

        public static int Leaderboard_Pile_1_X;

        public static TRect LeaderboardDialog_CannotConnect_Rect;

        public static int LeaderboardScreen_EdgeOfSpace_Overlay_Offset;

        public static int AchievementWidget_ROW_HEIGHT;

        public static int AchievementWidget_ROW_START;

        public static Point AchievementWidget_GAMERSCORE_POS;

        public static int AchievementWidget_HOLE_DEPTH;

        public static int AchievementWidget_BackButton_X;

        public static int AchievementWidget_BackButton_Y;

        public static TRect AchievementWidget_BackButton_Rect;

        public static int AchievementWidget_Background_Offset_Y;

        public static Rectangle AchievementWidget_Description_Box;

        public static Point AchievementWidget_Pipe_Offset;

        public static Point AchievementWidget_Worm_Offset;

        public static Point AchievementWidget_ZombieWorm_Offset;

        public static Point AchievementWidget_GemLeft_Offset;

        public static Point AchievementWidget_GemRight_Offset;

        public static Point AchievementWidget_Fossile_Offset;

        public static Point AchievementWidget_Image_Pos;

        public static int AchievementWidget_Image_Size;

        public static Point AchievementWidget_Name_Pos;

        public static int AchievementWidget_Name_MaxWidth;

        public static int QuickPlayWidget_Thumb_X;

        public static int QuickPlayWidget_Thumb_Y;

        public static int QuickPlayWidget_Bungee_Y;

        public static int SeedChooserScreen_MenuButton_X;

        public static Point SeedChooserScreen_Background_Top;

        public static Point SeedChooserScreen_Background_Middle;

        public static int SeedChooserScreen_Background_Middle_Height;

        public static Point SeedChooserScreen_Background_Bottom;

        public static Rectangle SeedChooserScreen_Gradient_Top;

        public static Rectangle SeedChooserScreen_Gradient_Bottom;

        public static Color SeedChooserScreen_BackColour = new Color(66, 32, 0);

        public static Point SeedPacket_Cost;

        public static Point SeedPacket_Cost_IZombie;

        public static Point SeedPacket_CostText_Pos;

        public static Point SeedPacket_CostText_IZombie_Pos;

        public static Point ImitaterDialog_Size;

        public static int ImitaterDialog_ScrollWidget_Offset_X;

        public static int ImitaterDialog_ScrollWidget_Y;

        public static int ImitaterDialog_ScrollWidget_ExtraWidth;

        public static int ImitaterDialog_Height;

        public static int ImitaterDialog_BottomGradient_Y;

        public static TRect ConveyorBeltClipRect;

        public static Point CutScene_ReadySetPlant_Pos;

        public static int CutScene_LogoEndPos;

        public static int CutScene_LogoBackRect_Height;

        public static Point CutScene_LogoEnd_Particle_Pos;

        public static Point CutScene_ExtraRoom_1_Particle_Pos;

        public static Point CutScene_ExtraRoom_2_Particle_Pos;

        public static Point CutScene_ExtraRoom_3_Particle_Pos;

        public static Point CutScene_ExtraRoom_4_Particle_Pos;

        public static Point CutScene_ExtraRoom_5_Particle_Pos;

        public static int CutScene_SodRoll_1_Pos;

        public static int CutScene_SodRoll_2_Pos;

        public static Point CutScene_SodRoll_3_Pos;

        public static Point CutScene_SodRoll_4_Pos;

        public static Point CutScene_Upsell_TerraCotta_Arrow;

        public static Point CutScene_Upsell_TerraCotta_Pot;

        public static int StoreScreen_BackButton_X;

        public static int StoreScreen_BackButton_Y;

        public static int StoreScreen_Car_X;

        public static int StoreScreen_Car_Y;

        public static int StoreScreen_PrevButton_X;

        public static int StoreScreen_PrevButton_Y;

        public static int StoreScreen_NextButton_X;

        public static int StoreScreen_NextButton_Y;

        public static int StoreScreen_HatchOpen_X;

        public static int StoreScreen_HatchOpen_Y;

        public static int StoreScreen_HatchClosed_X;

        public static int StoreScreen_HatchClosed_Y;

        public static int StoreScreen_CarNight_X;

        public static int StoreScreen_CarNight_Y;

        public static int StoreScreen_StoreSign_X;

        public static int StoreScreen_StoreSign_Y_Min;

        public static int StoreScreen_StoreSign_Y_Max;

        public static int StoreScreen_Coinbank_X;

        public static int StoreScreen_Coinbank_Y;

        public static Point StoreScreen_Coinbank_TextOffset;

        public static int StoreScreen_ItemOffset_1_X;

        public static int StoreScreen_ItemOffset_1_Y;

        public static int StoreScreen_ItemOffset_2_X;

        public static int StoreScreen_ItemOffset_2_Y;

        public static int StoreScreen_ItemSize;

        public static int StoreScreen_ItemSize_Offset;

        public static int StoreScreen_PriceTag_X;

        public static int StoreScreen_PriceTag_Y;

        public static int StoreScreen_PriceTag_Text_Offset_X;

        public static int StoreScreen_PriceTag_Text_Offset_Y;

        public static int StoreScreen_ComingSoon_X;

        public static int StoreScreen_ComingSoon_Y;

        public static int StoreScreen_SoldOut_Width;

        public static int StoreScreen_SoldOut_Y;

        public static int StoreScreen_SoldOut_Height;

        public static int StoreScreen_PacketUpgrade_X;

        public static int StoreScreen_PacketUpgrade_Y;

        public static Rectangle StoreScreen_PacketUpgrade_Text_Size;

        public static int StoreScreen_RetardedDave_Offset_X;

        public static int StoreScreen_FirstAidNut_Offset_Y;

        public static int StoreScreen_PoolCleaner_Offset_X;

        public static int StoreScreen_PoolCleaner_Offset_Y;

        public static int StoreScreen_Rake_Offset_X;

        public static int StoreScreen_Rake_Offset_Y;

        public static int StoreScreen_RoofCleaner_Offset_X;

        public static int StoreScreen_RoofCleaner_Offset_Y;

        public static int StoreScreen_Imitater_Offset_X;

        public static int StoreScreen_Imitater_Offset_Y;

        public static int StoreScreen_Default_Offset_Y;

        public static Point StoreScreen_MouseRegion;

        public static Rectangle StoreScreen_Dialog;

        public static Point StoreScreen_PotPlant_Offset;

        public static int StoreScreenMushroomGardenOffsetX;

        public static int StoreScreenAquariumGardenOffsetX;

        public static int StoreScreenGloveOffsetY;

        public static int StoreScreenWheelbarrowOffsetY;

        public static int StoreScreenBugSprayOffsetX;

        public static int StoreScreenBugSprayOffsetY;

        public static int StoreScreenPhonographOffsetY;

        public static int StoreScreenPlantFoodOffsetX;

        public static int StoreScreenPlantFoodOffsetY;

        public static int StoreScreenWateringCanOffsetX;

        public static int StoreScreenWateringCanOffsetY;

        public static int NewOptionsDialog_FXLabel_X;

        public static int NewOptionsDialog_FXLabel_Y;

        public static int NewOptionsDialog_MusicLabel_X;

        public static int NewOptionsDialog_MusicLabel_On_Y;

        public static int NewOptionsDialog_MusicLabel_Off_Y;

        public static int NewOptionsDialog_VibrationLabel_X;

        public static int NewOptionsDialog_VibrationLabel_Y;

        public static int NewOptionsDialog_LockedLabel_Y;

        public static int NewOptionsDialog_VibrationLabel_MaxWidth;

        public static int NewOptionsDialog_FullScreenOffset;

        public static int NewOptionsDialog_FX_Offset;

        public static int NewOptionsDialog_Music_Offset;

        public static int NewOptionsDialog_Version_Low_Y;

        public static int NewOptionsDialog_Version_High_Y;

        public static int RetardedDave_Bubble_Tip_Offset;

        public static Point RetardedDave_Bubble_Offset_Shop;

        public static Point RetardedDave_Bubble_Offset_Board;

        public static int RetardedDave_Bubble_Size;

        public static TRect RetardedDave_Bubble_Rect;

        public static int RetardedDave_Bubble_TapToContinue_Y;

        public static int AlmanacHeaderY;

        public static TRect Almanac_PlantsButtonRect;

        public static TRect Almanac_ZombiesButtonRect;

        public static TRect Almanac_CloseButtonRect;

        public static TRect Almanac_IndexButtonRect;

        public static Point Almanac_IndexPlantPos;

        public static Point Almanac_IndexZombiePos;

        public static Point Almanac_IndexPlantTextPos;

        public static Point Almanac_IndexZombieTextPos;

        public static TRect Almanac_PlantScrollRect;

        public static TRect Almanac_ZombieScrollRect;

        public static TRect Almanac_DescriptionScrollRect;

        public static int Almanac_PlantTopGradientY;

        public static int Almanac_ZombieTopGradientY;

        public static int Almanac_BottomGradientY;

        public static int Almanac_PlantGradientWidth;

        public static int Almanac_ZombieGradientWidth;

        public static TRect Almanac_ZombieStoneRect;

        public static Point Almanac_BackgroundPosition;

        public static TRect Almanac_ZombieClipRect;

        public static TRect Almanac_NavyRect;

        public static Point Almanac_NamePosition;

        public static TRect Almanac_ClayRect;

        public static TRect Almanac_BrownRect;

        public static Point Almanac_ZombieSpace;

        public static Point Almanac_ZombieOffset;

        public static Point Almanac_BossPosition;

        public static Point Almanac_ImpPosition;

        public static Point Almanac_ImitatorPosition;

        public static Point Almanac_SeedSpace;

        public static Point Almanac_SeedOffset;

        public static Point Almanac_ZombiePosition;

        public static Point Almanac_PlantPosition;

        public static float Almanac_Text_Scale;

        public static Point Almanac_PlantsHeader_Pos;

        public static Point Almanac_ZombieHeader_Pos;

        public static int Almanac_ItemName_MaxWidth;

        public static Point PlantGallerySize;

        public static Point ZombieGallerySize;

        public static Point ZombieGalleryWidget_Window_Offset;

        public static Rectangle ZombieGalleryWidget_Window_Clip;

        public static float Zombie_StartOffset;

        public static float Zombie_StartRandom_Offset;

        public static Point Zombie_Bungee_Offset;

        public static Point Zombie_Bungee_Target_Offset;

        public static int Zombie_Dancer_Dance_Limit_X;

        public static float Zombie_Dancer_Spotlight_Scale;

        public static Point Zombie_Dancer_Spotlight_Offset;

        public static int ZOMBIE_BACKUP_DANCER_RISE_HEIGHT;

        public static Point Zombie_Dancer_Spotlight_Pos;

        public static int Zombie_ClipOffset_Default;

        public static int Zombie_ClipOffset_Snorkel;

        public static int Zombie_ClipOffset_Snorkel_intoPool_Small;

        public static int Zombie_ClipOffset_Snorkel_Dying;

        public static int Zombie_ClipOffset_Snorkel_Dying_Small;

        public static int Zombie_ClipOffset_Pail;

        public static int Zombie_ClipOffset_Normal;

        public static int Zombie_ClipOffset_Digger;

        public static int Zombie_ClipOffset_Dolphin_Into_Pool;

        public static int Zombie_ClipOffset_Snorkel_Grabbed;

        public static int Zombie_ClipOffset_PeaHead_InPool;

        public static int Zombie_ClipOffset_RisingFromGrave;

        public static int Zombie_ClipOffset_RisingFromGrave_Small;

        public static int Zombie_ClipOffset_Snorkel_Into_Pool;

        public static int Zombie_ClipOffset_Normal_In_Pool;

        public static int Zombie_ClipOffset_Flag_In_Pool;

        public static int Zombie_ClipOffset_TrafficCone_In_Pool_SMALL;

        public static int Zombie_ClipOffset_Normal_In_Pool_SMALL;

        public static int Zombie_ClipOffset_Ducky_Dying_In_Pool;

        public static int Zombie_GameOver_ClipOffset_1;

        public static int Zombie_GameOver_ClipOffset_2;

        public static int Zombie_GameOver_ClipOffset_3;

        public static Rectangle AwardScreen_Note_Credits_Background;

        public static Point AwardScreen_Note_Credits_Paper;

        public static Rectangle AwardScreen_Note_Credits_Text;

        public static Rectangle AwardScreen_Note_Help_Background;

        public static Point AwardScreen_Note_Help_Paper;

        public static Point AwardScreen_Note_Help_Text;

        public static Rectangle AwardScreen_Note_1_Background;

        public static Point AwardScreen_Note_1_Paper;

        public static Point AwardScreen_Note_1_Text;

        public static Point AwardScreen_Note_Message;

        public static Rectangle AwardScreen_Note_2_Background;

        public static Point AwardScreen_Note_2_Paper;

        public static Point AwardScreen_Note_2_Text;

        public static Rectangle AwardScreen_Note_3_Background;

        public static Point AwardScreen_Note_3_Paper;

        public static Point AwardScreen_Note_3_Text;

        public static Rectangle AwardScreen_Note_4_Background;

        public static Point AwardScreen_Note_4_Paper;

        public static Point AwardScreen_Note_4_Text;

        public static Rectangle AwardScreen_Note_Final_Background;

        public static Point AwardScreen_Note_Final_Paper;

        public static Point AwardScreen_Note_Final_Text;

        public static Point AwardScreen_Bacon;

        public static Point AwardScreen_WateringCan;

        public static Point AwardScreen_Taco;

        public static Point AwardScreen_CarKeys;

        public static Point AwardScreen_Almanac;

        public static Point AwardScreen_Trophy;

        public static Point AwardScreen_Shovel;

        public static Point AwardScreen_MenuButton;

        public static TRect AwardScreen_ClayTablet;

        public static Point AwardScreen_TitlePos;

        public static Point AwardScreen_GroundDay_Pos;

        public static TRect AwardScreen_BrownRect;

        public static Point AwardScreen_BottomMessage_Pos;

        public static TRect AwardScreen_BottomText_Rect_Text;

        public static TRect AwardScreen_BottomText_Rect_NoText;

        public static TRect AwardScreen_CreditsButton;

        public static Point AwardScreen_CreditsButton_Offset;

        public static Point AwardScreen_Seed_Pos;

        public static int AwardScreen_ContinueButton_Offset;

        public static Point AwardScreen_AchievementImage_Pos;

        public static Point AwardScreen_AchievementName_Pos;

        public static Rectangle AwardScreen_AchievementDescription_Rect;

        public static Rectangle CreditScreen_ReplayButton;

        public static Point CreditScreen_ReplayButton_TextOffset;

        public static Rectangle CreditScreen_MainMenu;

        public static int CreditScreen_TextStart;

        public static int CreditScreen_TextEnd;

        public static int CreditScreen_TextClip;

        public static int CreditScreen_LeftText_X;

        public static int CreditScreen_RightText_X;

        public static int CreditScreen_LeftRight_Text_Width;

        public static TRect ConfirmPurchaseDialog_Background;

        public static Point ConfirmPurchaseDialog_Item_Pos;

        public static TRect ConfirmPurchaseDialog_Text;

        public static Insets LawnDialog_Insets;

        public static Point UILevelPosition;

        public static Point UIMenuButtonPosition;

        public static int UIMenuButtonWidth;

        public static int UISunBankPositionX;

        public static Point UISunBankTextOffset;

        public static Point UIShovelButtonPosition;

        public static Point UIProgressMeterPosition;

        public static int UIProgressMeterHeadEnd;

        public static int UIProgressMeterBarEnd;

        public static Point UICoinBankPosition;

        public static int Board_Cutscene_ExtraScroll;

        public static int Board_SunCoinRange;

        public static Point Board_SunCoin_CollectTarget;

        public static int[] Board_Cel_Y_Values_Normal;

        public static int[] Board_Cel_Y_Values_Pool;

        public static int[] Board_Cel_Y_Values_ZenGarden;

        public static Point Board_GameOver_Interior_Overlay_1;

        public static Point Board_GameOver_Interior_Overlay_2;

        public static Point Board_GameOver_Interior_Overlay_3;

        public static Point Board_GameOver_Interior_Overlay_4;

        public static Point Board_GameOver_Exterior_Overlay_1;

        public static Point Board_GameOver_Exterior_Overlay_2;

        public static Point Board_GameOver_Exterior_Overlay_3;

        public static Point Board_GameOver_Exterior_Overlay_4;

        public static Point Board_GameOver_Exterior_Overlay_5;

        public static Point Board_GameOver_Exterior_Overlay_6;

        public static int Board_Offset_AspectRatio_Correction;

        public static int Board_Ice_Start;

        public static int Board_ProgressBarText_Pos;

        public static int MessageWidget_SlotMachine_Y;

        public static Point LawnMower_Coin_Offset;

        public static int DescriptionWidget_ScrollBar_Padding;

        public static Point Coin_AwardSeedpacket_Pos;

        public static Point Coin_Glow_Offset;

        public static float Coin_Silver_Offset;

        public static Point Coin_MoneyBag_Offset;

        public static Point Coin_Shovel_Offset;

        public static Point Coin_Silver_Award_Offset;

        public static Point Coin_Almanac_Offset;

        public static Point Coin_Note_Offset;

        public static Point Coin_CarKeys_Offset;

        public static Point Coin_Taco_Offset;

        public static Point Coin_Bacon_Offset;

        public static Point Plant_CobCannon_Projectile_Offset;

        public static Point Plant_Squished_Offset;

        public static int IZombieBrainPosition;

        public static Point IZombie_SeedOffset;

        public static Rectangle IZombie_ClipOffset;

        public static Point[] ZombieOffsets;

        public static TRect LastStandButtonRect;

        public static int VasebreakerJackInTheBoxOffset;

        public static int JackInTheBoxPlantRadius;

        public static int JackInTheBoxZombieRadius;

        public static float ZenGardenGreenhouseMultiplierX;

        public static float ZenGardenGreenhouseMultiplierY;

        public static Point ZenGardenGreenhouseOffset;

        public static Point ZenGardenMushroomGardenOffset;

        public static int ZenGardenStoreButtonX;

        public static int ZenGardenStoreButtonY;

        public static int ZenGardenTopButtonStart;

        public static Point ZenGardenButtonCounterOffset;

        public static Point ZenGardenButton_GoldenWateringCan_Offset;

        public static Point ZenGardenButton_NormalWateringCan_Offset;

        public static Point ZenGardenButton_Fertiliser_Offset;

        public static Point ZenGardenButton_BugSpray_Offset;

        public static Point ZenGardenButton_Phonograph_Offset;

        public static Point ZenGardenButton_Chocolate_Offset;

        public static Point ZenGardenButton_Glove_Offset;

        public static Point ZenGardenButton_MoneySign_Offset;

        public static Point ZenGardenButton_NextGarden_Offset;

        public static Point ZenGardenButton_Wheelbarrow_Offset;

        public static Point ZenGardenButton_WheelbarrowPlant_Offset;

        public static float ZenGardenButton_Wheelbarrow_Facing_Offset;

        public static int ZenGarden_Backdrop_X;

        public static Point ZenGarden_SellDialog_Offset;

        public static Point ZenGarden_NextGarden_Pos;

        public static Point ZenGarden_RetardedDaveBubble_Pos;

        public static Point ZenGarden_PlantSpeechBubble_Pos;

        public static Point ZenGarden_StinkySpeechBubble_Pos;

        public static Point ZenGarden_WaterDrop_Pos;

        public static Point ZenGarden_Fertiliser_Pos;

        public static Point ZenGarden_Phonograph_Pos;

        public static Point ZenGarden_BugSpray_Pos;

        public static Point ZenGarden_GoldenWater_Pos;

        public static Point ZenGarden_Chocolate_Pos;

        public static int ZenGarden_MoneyTarget_X;

        public static int ZenGarden_TutorialArrow_Offset;

        public static int ZEN_XMIN;

        public static int ZEN_YMIN;

        public static int ZEN_XMAX;

        public static int ZEN_YMAX;

        public static Rectangle ZenGarden_GoldenWater_Size;

        public static int STINKY_SLEEP_POS_Y;

        public static SpecialGridPlacement[] gMushroomGridPlacement;

        public static Point ZenGarden_Marigold_Sprout_Offset;

        public static Point ZenGarden_Aquarium_ShadowOffset;

        public static int Challenge_SeeingStars_StarfruitPreview_Offset_Y;

        public static Point Challenge_SlotMachine_Pos;

        public static TRect Challenge_SlotMachineHandle_Pos;

        public static int Challenge_SlotMachine_Gap;

        public static int Challenge_SlotMachine_Offset;

        public static int Challenge_SlotMachine_Shadow_Offset;

        public static int Challenge_SlotMachine_Y_Offset;

        public static int Challenge_SlotMachine_Y_Pos;

        public static int Challenge_SlotMachine_ClipHeight;

        public static Point Challenge_BeghouldedTwist_Offset;

        public static Point GridItem_ScaryPot_SeedPacket_Offset;

        public static Point GridItem_ScaryPot_Zombie_Offset;

        public static Point GridItem_ScaryPot_ZombieFootball_Offset;

        public static Point GridItem_ScaryPot_ZombieGargantuar_Offset;

        public static Point GridItem_ScaryPot_Sun_Offset;

        public enum LanguageIndex
        {
            en = 1,
            fr,
            de,
            es,
            it
        }
        public static class New 
        {
            public static void Load() 
            {
                Board_GridCellSizeX = 80;
                Board_GridCellSizeY_6Rows = 85;
                Board_GridCellSizeY_5Rows = 100;
                SeedBank_Width = (int)Constants.InvertAndScale(62f);
            }
            public static int Board_GridCellSizeX;
            public static int Board_GridCellSizeY_5Rows;
            public static int Board_GridCellSizeY_6Rows;
            public static int SeedBank_Width;
        }
    }
}
