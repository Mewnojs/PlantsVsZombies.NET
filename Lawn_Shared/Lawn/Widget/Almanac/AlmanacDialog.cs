using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class AlmanacDialog : LawnDialog
	{
		public static void AlmanacInitForPlayer()
		{
		}

		public static void AlmanacPlayerDefeatedZombie(ZombieType theZombieType)
		{
			AlmanacDialog.gZombieDefeated[(int)theZombieType] = true;
		}

		public AlmanacDialog(LawnApp theApp, AlmanacListener theListener) : base(theApp, null, 3, true, "[ALMANAC_HEADER]", "", "", 0)
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mListener = theListener;
			this.mOpenPage = AlmanacPage.ALMANAC_PAGE_INDEX;
			this.mSelectedSeed = SeedType.SEED_PEASHOOTER;
			this.mSelectedZombie = ZombieType.ZOMBIE_NORMAL;
			this.mZombie = null;
			this.mPlant = null;
			this.mDrawStandardBack = false;
			this.mViewPlantsRect = Constants.Almanac_PlantsButtonRect;
			this.mViewZombiesRect = Constants.Almanac_ZombiesButtonRect;
			this.mPlantGalleryWidget = new PlantGalleryWidget(this);
			this.mPlantsScrollWidget = new ScrollWidget();
			this.mPlantsScrollWidget.Resize(Constants.Almanac_PlantScrollRect.mX, Constants.Almanac_PlantScrollRect.mY, Constants.Almanac_PlantScrollRect.mWidth, Constants.Almanac_PlantScrollRect.mHeight);
			this.mPlantsScrollWidget.AddWidget(this.mPlantGalleryWidget);
			this.mPlantGalleryWidget.Move(0, 0);
			this.AddWidget(this.mPlantsScrollWidget);
			this.mPlantsScrollWidget.SetVisible(false);
			this.mPlantsScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mZombieGalleryWidget = new ZombieGalleryWidget(this);
			this.mZombiesScrollWidget = new ScrollWidget();
			this.mZombiesScrollWidget.Resize(Constants.Almanac_ZombieScrollRect.mX, Constants.Almanac_ZombieScrollRect.mY, Constants.Almanac_ZombieScrollRect.mWidth, Constants.Almanac_ZombieScrollRect.mHeight);
			this.mZombiesScrollWidget.AddWidget(this.mZombieGalleryWidget);
			this.mZombieGalleryWidget.Move(0, 0);
			this.AddWidget(this.mZombiesScrollWidget);
			this.mZombiesScrollWidget.SetVisible(false);
			this.mZombiesScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mDescriptionWidget = new DescriptionWidget();
			this.mDescriptionScrollWidget = new ScrollWidget();
			this.mDescriptionScrollWidget.Resize(Constants.Almanac_DescriptionScrollRect.mX, Constants.Almanac_DescriptionScrollRect.mY, Constants.Almanac_DescriptionScrollRect.mWidth, Constants.Almanac_DescriptionScrollRect.mHeight);
			this.mDescriptionWidget.mWidth = this.mDescriptionScrollWidget.mWidth;
			this.mDescriptionScrollWidget.AddWidget(this.mDescriptionWidget);
			this.mDescriptionWidget.Move(0, 0);
			this.mDescriptionScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.AddWidget(this.mDescriptionScrollWidget);
			this.Resize(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			this.mCloseButton = new GameButton(0, this);
			this.mCloseButton.mButtonImage = AtlasResources.IMAGE_ALMANAC_CLOSEBUTTON;
			this.mCloseButton.mOverImage = null;
			this.mCloseButton.mDownImage = null;
			this.mCloseButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mCloseButton.mColors[0] = new SexyColor(42, 42, 90);
			this.mCloseButton.mColors[1] = new SexyColor(42, 42, 90);
			this.mCloseButton.Resize(Constants.Almanac_CloseButtonRect.mX, Constants.Almanac_CloseButtonRect.mY, Constants.Almanac_CloseButtonRect.mWidth, Constants.Almanac_CloseButtonRect.mHeight);
			this.mCloseButton.mParentWidget = this;
			this.mCloseButton.mTextOffsetX = -8;
			this.mCloseButton.mTextOffsetY = 1;
			this.mIndexButton = new GameButton(3, this);
			this.mIndexButton.mButtonImage = AtlasResources.IMAGE_ALMANAC_BACKBUTTON;
			this.mIndexButton.mOverImage = null;
			this.mIndexButton.mDownImage = null;
			this.mIndexButton.SetFont(Resources.FONT_BRIANNETOD12);
			this.mIndexButton.mColors[0] = new SexyColor(42, 42, 90);
			this.mIndexButton.mColors[1] = new SexyColor(42, 42, 90);
			this.mIndexButton.Resize(Constants.Almanac_IndexButtonRect.mX, Constants.Almanac_IndexButtonRect.mY, Constants.Almanac_IndexButtonRect.mWidth, Constants.Almanac_IndexButtonRect.mHeight);
			this.mIndexButton.mParentWidget = this;
			this.mIndexButton.mTextOffsetX = 8;
			this.mIndexButton.mTextOffsetY = 1;
			this.SetPage(AlmanacPage.ALMANAC_PAGE_INDEX);
			if (this.mApp.mBoard == null || !this.mApp.mBoard.mPaused)
			{
				this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
			}
		}

		public override void Dispose()
		{
			this.mCloseButton.Dispose();
			this.mIndexButton.Dispose();
			this.ClearPlantsAndZombies();
			this.RemoveAllWidgets(true);
			base.Dispose();
		}

		public override void Update()
		{
			this.mCloseButton.Update();
			this.mIndexButton.Update();
			if (this.mPlant != null)
			{
				this.mPlant.Update();
			}
			if (this.mZombie != null)
			{
				this.mZombie.Update();
			}
			this.MarkDirty();
		}

		public override bool BackButtonPress()
		{
			switch (this.mOpenPage)
			{
			case AlmanacPage.ALMANAC_PAGE_INDEX:
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
				this.mApp.KillAlmanacDialog();
				if (this.mListener != null)
				{
					this.mListener.BackFromAlmanac();
				}
				break;
			case AlmanacPage.ALMANAC_PAGE_PLANTS:
			case AlmanacPage.ALMANAC_PAGE_ZOMBIES:
				this.mApp.PlaySample(Resources.SOUND_TAP);
				this.SetPage(AlmanacPage.ALMANAC_PAGE_INDEX);
				break;
			}
			return true;
		}

		public override void Draw(Graphics g)
		{
			g.SetLinearBlend(true);
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX)
			{
				this.DrawIndex(g);
			}
			else if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_PLANTS)
			{
				this.DrawPlants(g);
			}
			else if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_ZOMBIES)
			{
				this.DrawZombies(g);
			}
			this.mCloseButton.Draw(g);
			this.mIndexButton.Draw(g);
			if (this.mOpenPage != AlmanacPage.ALMANAC_PAGE_INDEX)
			{
				base.DeferOverlay();
			}
		}

		public override void DrawOverlay(Graphics g)
		{
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_PLANTS)
			{
				g.SetColorizeImages(true);
				g.SetColor(Constants.Almanac_Paper_Colour);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, this.mPlantsScrollWidget.mX, Constants.Almanac_PlantTopGradientY, Constants.Almanac_PlantGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT.mHeight);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, this.mPlantsScrollWidget.mX, Constants.Almanac_BottomGradientY, Constants.Almanac_PlantGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT.mHeight);
				g.SetColorizeImages(false);
				return;
			}
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_ZOMBIES)
			{
				g.SetColorizeImages(true);
				g.SetColor(Constants.Almanac_Paper_Colour);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, this.mZombiesScrollWidget.mX, Constants.Almanac_ZombieTopGradientY, Constants.Almanac_ZombieGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT.mHeight);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, this.mZombiesScrollWidget.mX, Constants.Almanac_BottomGradientY, Constants.Almanac_ZombieGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT.mHeight);
				g.SetColorizeImages(false);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && this.mViewPlantsRect.Contains(new TPoint(x, y)))
			{
				this.SetPage(AlmanacPage.ALMANAC_PAGE_PLANTS);
				return;
			}
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && this.mViewZombiesRect.Contains(new TPoint(x, y)))
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
				this.SetPage(AlmanacPage.ALMANAC_PAGE_ZOMBIES);
				return;
			}
			if (this.mCloseButton.IsMouseOver())
			{
				this.mApp.KillAlmanacDialog();
				if (this.mListener != null)
				{
					this.mListener.BackFromAlmanac();
				}
				return;
			}
			if (this.mIndexButton.IsMouseOver())
			{
				this.SetPage(AlmanacPage.ALMANAC_PAGE_INDEX);
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			this.mCloseButton.Update();
			this.mIndexButton.Update();
			if ((this.mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && this.mViewPlantsRect.Contains(new TPoint(x, y))) || this.mCloseButton.IsMouseOver() || this.mIndexButton.IsMouseOver())
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && this.mViewZombiesRect.Contains(new TPoint(x, y)))
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
		}

		public virtual void KeyChar(char theChar)
		{
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.ClearPlantsAndZombies();
		}

		public void DrawIndex(Graphics g)
		{
			g.SetClipRect(0, 0, Constants.BackBufferSize.Y, Constants.BackBufferSize.X);
			LawnCommon.DrawImageBox(g, new TRect(0, 0, this.mWidth, this.mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_INDEX_HEADER, this.mWidth / 2 - AtlasResources.IMAGE_ALMANAC_INDEX_HEADER.mWidth / 2, Constants.AlmanacHeaderY);
			float almanac_Text_Scale = Constants.Almanac_Text_Scale;
			if (this.mPlant != null)
			{
				Graphics @new = Graphics.GetNew(g);
				LawnCommon.DrawImageBox(g, this.mViewPlantsRect, AtlasResources.IMAGE_ALMANAC_CLAY_BORDER);
				@new.mTransX += this.mPlant.mX;
				@new.mTransY += this.mPlant.mY;
				this.mPlant.Draw(@new);
				TodCommon.TodDrawString(g, "[VIEW_PLANTS]", Constants.Almanac_IndexPlantTextPos.X, Constants.Almanac_IndexPlantTextPos.Y, Resources.FONT_DWARVENTODCRAFT15, Constants.YellowFontColour, DrawStringJustification.DS_ALIGN_CENTER, almanac_Text_Scale);
				@new.PrepareForReuse();
			}
			if (this.mZombie != null)
			{
				Graphics new2 = Graphics.GetNew(g);
				LawnCommon.DrawImageBox(g, this.mViewZombiesRect, AtlasResources.IMAGE_ALMANAC_STONE_BORDER);
				new2.mTransX += this.mZombie.mX;
				new2.mTransY += this.mZombie.mY;
				this.mZombie.Draw(new2);
				TodCommon.TodDrawString(g, "[VIEW_ZOMBIES]", Constants.Almanac_IndexZombieTextPos.X, Constants.Almanac_IndexZombieTextPos.Y, Resources.FONT_DWARVENTODCRAFT15, Constants.GreenFontColour, DrawStringJustification.DS_ALIGN_CENTER, almanac_Text_Scale);
				new2.PrepareForReuse();
			}
		}

		public void SetPage(AlmanacPage thePage)
		{
			this.mOpenPage = thePage;
			this.ClearPlantsAndZombies();
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX)
			{
				this.mPlantsScrollWidget.SetVisible(false);
				this.mZombiesScrollWidget.SetVisible(false);
				this.mDescriptionScrollWidget.SetVisible(false);
				this.mPlant = Plant.GetNewPlant();
				this.mPlant.mBoard = null;
				this.mPlant.mIsOnBoard = false;
				this.mPlant.PlantInitialize(0, 0, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
				this.mPlant.mX = Constants.Almanac_IndexPlantPos.X;
				this.mPlant.mY = Constants.Almanac_IndexPlantPos.Y;
				if (this.mZombie != null)
				{
					this.mZombie.PrepareForReuse();
				}
				this.mZombie = Zombie.GetNewZombie();
				this.mZombie.mBoard = null;
				this.mZombie.ZombieInitialize(0, ZombieType.ZOMBIE_NORMAL, false, null, GameConstants.ZOMBIE_WAVE_UI);
				this.mZombie.mPosX = (float)Constants.Almanac_IndexZombiePos.X;
				this.mZombie.mPosY = (float)Constants.Almanac_IndexZombiePos.Y;
				this.mIndexButton.mBtnNoDraw = true;
				return;
			}
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_PLANTS)
			{
				this.SetupPlant();
				this.mPlantsScrollWidget.SetVisible(true);
				this.mZombiesScrollWidget.SetVisible(false);
				this.mPlantsScrollWidget.ScrollToMin(false);
				this.mDescriptionScrollWidget.SetVisible(true);
				this.mDescriptionScrollWidget.ScrollToMin(false);
				this.mIndexButton.mBtnNoDraw = false;
				return;
			}
			if (this.mOpenPage == AlmanacPage.ALMANAC_PAGE_ZOMBIES)
			{
				this.SetupZombie();
				this.mPlantsScrollWidget.SetVisible(false);
				this.mZombiesScrollWidget.SetVisible(true);
				this.mZombiesScrollWidget.ScrollToMin(false);
				this.mDescriptionScrollWidget.SetVisible(true);
				this.mDescriptionScrollWidget.ScrollToMin(false);
				this.mIndexButton.mBtnNoDraw = false;
				return;
			}
			Debug.ASSERT(false);
		}

		public void DrawZombies(Graphics g)
		{
			LawnCommon.DrawImageBox(g, new TRect(0, 0, this.mWidth, this.mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIES_HEADER, Constants.Almanac_ZombieHeader_Pos.X, Constants.Almanac_ZombieHeader_Pos.Y);
			LawnCommon.DrawImageBox(g, Constants.Almanac_ZombieStoneRect, AtlasResources.IMAGE_ALMANAC_STONE_TABLET);
			int x = Constants.Almanac_BackgroundPosition.X;
			int y = Constants.Almanac_BackgroundPosition.Y;
			if (this.mZombie.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombie.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDICE, x, y);
			}
			else
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDDAY, x, y);
			}
			if (this.mZombie != null && !this.ZombieHasSilhouette(this.mZombie.mZombieType))
			{
				Graphics @new = Graphics.GetNew(g);
				@new.mTransX += (int)((float)this.mZombie.mX * Constants.S);
				@new.mTransY += (int)((float)this.mZombie.mY * Constants.S);
				@new.SetClipRect(ref Constants.Almanac_ZombieClipRect);
				if (this.mZombie.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
				{
					@new.mTransX += (int)(-30f * Constants.S);
					@new.mTransY += (int)(5f * Constants.S);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_GARGANTUAR)
				{
					@new.mTransY += (int)(30f * Constants.S);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
				{
					@new.mTransX += (int)(-10f * Constants.S);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					@new.mTransY += (int)(-20f * Constants.S);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_BUNGEE)
				{
					@new.mTransX += (int)Constants.InvertAndScale(12f);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_CATAPULT)
				{
					@new.mTransX += (int)(-10f * Constants.S);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_BOSS)
				{
					@new.mTransX += (int)(-540f * Constants.S);
					@new.mTransY += (int)(-175f * Constants.S);
				}
				else if (this.mZombie.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
				{
					@new.mTransX += (int)Constants.InvertAndScale(17f);
				}
				if (this.mZombie.mZombieType != ZombieType.ZOMBIE_BUNGEE && this.mZombie.mZombieType != ZombieType.ZOMBIE_BOSS && this.mZombie.mZombieType != ZombieType.ZOMBIE_ZAMBONI && this.mZombie.mZombieType != ZombieType.ZOMBIE_CATAPULT)
				{
					this.mZombie.DrawShadow(@new);
				}
				@new.HardwareClip();
				this.mZombie.Draw(@new);
				@new.EndHardwareClip();
				@new.PrepareForReuse();
			}
			LawnCommon.DrawImageBox(g, Constants.Almanac_NavyRect, AtlasResources.IMAGE_ALMANAC_NAVY_RECT, false);
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(this.mSelectedZombie);
			string theText;
			if (this.ZombieHasSilhouette(this.mSelectedZombie))
			{
				theText = TodStringFile.TodStringTranslate("[NOT_ENCOUNTERED_YET_NAME]");
			}
			else
			{
				theText = Common.StrFormat_("[{0}]", zombieDefinition.mZombieName);
			}
			TodCommon.TodDrawString(g, theText, Constants.Almanac_NamePosition.X, Constants.Almanac_NamePosition.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(0, 196, 0), Constants.Almanac_ItemName_MaxWidth, DrawStringJustification.DS_ALIGN_CENTER);
			TRect rect = this.mDescriptionScrollWidget.GetRect();
			rect.Inflate((int)Constants.InvertAndScale(6f), (int)Constants.InvertAndScale(6f));
			this.DrawPaper(g, rect, new SexyColor(171, 159, 207));
		}

		public void DrawPlants(Graphics g)
		{
			LawnCommon.DrawImageBox(g, new TRect(0, 0, this.mWidth, this.mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_HEADER, Constants.Almanac_PlantsHeader_Pos.X, Constants.Almanac_PlantsHeader_Pos.Y);
			LawnCommon.DrawImageBox(g, Constants.Almanac_ClayRect, AtlasResources.IMAGE_ALMANAC_CLAY_TABLET);
			int x = Constants.Almanac_BackgroundPosition.X;
			int y = Constants.Almanac_BackgroundPosition.Y;
			if (this.mSelectedSeed == SeedType.SEED_LILYPAD || this.mSelectedSeed == SeedType.SEED_TANGLEKELP || this.mSelectedSeed == SeedType.SEED_CATTAIL)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDPOOL, x, y);
			}
			else if (this.mSelectedSeed == SeedType.SEED_SEASHROOM)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDNIGHTPOOL, x, y);
			}
			else if (Plant.IsNocturnal(this.mSelectedSeed) || this.mSelectedSeed == SeedType.SEED_GRAVEBUSTER || this.mSelectedSeed == SeedType.SEED_PLANTERN)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDNIGHT, x, y);
			}
			else if (this.mSelectedSeed == SeedType.SEED_FLOWERPOT)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDROOF, x, y);
			}
			else
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDDAY, x, y);
			}
			if (this.mPlant != null)
			{
				Graphics @new = Graphics.GetNew(g);
				@new.mTransX += this.mPlant.mX;
				@new.mTransY += this.mPlant.mY;
				this.mPlant.Draw(@new);
				@new.PrepareForReuse();
			}
			LawnCommon.DrawImageBox(g, Constants.Almanac_BrownRect, AtlasResources.IMAGE_ALMANAC_BROWN_RECT, false);
			string nameString = Plant.GetNameString(this.mSelectedSeed, SeedType.SEED_NONE);
			TodCommon.TodDrawString(g, nameString, Constants.Almanac_NamePosition.X, Constants.Almanac_NamePosition.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(213, 159, 43), Constants.Almanac_ItemName_MaxWidth, DrawStringJustification.DS_ALIGN_CENTER);
			TRect rect = this.mDescriptionScrollWidget.GetRect();
			rect.Inflate((int)Constants.InvertAndScale(6f), (int)Constants.InvertAndScale(6f));
			this.DrawPaper(g, rect, new SexyColor(253, 186, 117));
		}

		public void SetupPlant()
		{
			this.ClearPlantsAndZombies();
			float num = (float)Constants.Almanac_PlantPosition.X;
			float num2 = (float)Constants.Almanac_PlantPosition.Y;
			if (this.mSelectedSeed == SeedType.SEED_TALLNUT)
			{
				num2 += (float)((int)Constants.InvertAndScale(18f));
			}
			if (this.mSelectedSeed == SeedType.SEED_COBCANNON)
			{
				num += (float)((int)Constants.InvertAndScale(-20f));
			}
			if (this.mSelectedSeed == SeedType.SEED_FLOWERPOT)
			{
				num2 += (float)((int)Constants.InvertAndScale(-20f));
			}
			if (this.mSelectedSeed == SeedType.SEED_INSTANT_COFFEE)
			{
				num2 += (float)((int)Constants.InvertAndScale(20f));
			}
			if (this.mSelectedSeed == SeedType.SEED_GRAVEBUSTER)
			{
				num2 += (float)((int)Constants.InvertAndScale(35f));
			}
			this.mPlant = Plant.GetNewPlant();
			this.mPlant.mBoard = null;
			this.mPlant.mIsOnBoard = false;
			this.mPlant.PlantInitialize(0, 0, this.mSelectedSeed, SeedType.SEED_NONE);
			this.mPlant.mX = (int)num;
			this.mPlant.mY = (int)num2;
			PlantDefinition plantDefinition = Plant.GetPlantDefinition(this.mSelectedSeed);
			string text = "";
			if (TodStringFile.TodStringListExists("[" + plantDefinition.mPlantName + "_DESCRIPTION_HEADER]"))
			{
				text = TodStringFile.TodStringTranslate("[" + plantDefinition.mPlantName + "_DESCRIPTION_HEADER]");
			}
			string text2 = TodStringFile.TodStringTranslate("[" + plantDefinition.mPlantName + "_DESCRIPTION]");
			if (text != string.Empty)
			{
				text2 = string.Concat(new string[]
				{
					"{NORMAL}",
					text,
					"\n",
					text2,
					"\n"
				});
			}
			string text3 = "{KEYWORD}{COST}:{STAT} " + plantDefinition.mSeedCost.ToString();
			text3 = TodCommon.TodReplaceString(text3, "{COST}", "[COST]");
			string text4;
			if (plantDefinition.mRefreshTime == 750)
			{
				text4 = "{KEYWORD}{WAIT_TIME}:{STAT} {WAIT_TIME_LENGTH}";
				text4 = TodCommon.TodReplaceString(text4, "{WAIT_TIME_LENGTH}", "[WAIT_TIME_SHORT]");
			}
			else if (plantDefinition.mRefreshTime == 3000)
			{
				text4 = "{KEYWORD}{WAIT_TIME}:{STAT} {WAIT_TIME_LENGTH}";
				text4 = TodCommon.TodReplaceString(text4, "{WAIT_TIME_LENGTH}", "[WAIT_TIME_LONG]");
			}
			else
			{
				text4 = "{KEYWORD}{WAIT_TIME}:{STAT} {WAIT_TIME_LENGTH}";
				text4 = TodCommon.TodReplaceString(text4, "{WAIT_TIME_LENGTH}", "[WAIT_TIME_VERY_LONG]");
			}
			text4 = TodCommon.TodReplaceString(text4, "{WAIT_TIME}", "[WAIT_TIME]");
			text2 += "{NORMAL}";
			if (this.mSelectedSeed != SeedType.SEED_IMITATER)
			{
				string text5 = text2;
				text2 = string.Concat(new string[]
				{
					text5,
					"\n",
					text4,
					"\n",
					text3
				});
			}
			this.mDescriptionWidget.SetText(ref text2);
			this.mDescriptionScrollWidget.ClientSizeChanged();
			this.mDescriptionScrollWidget.ScrollToMin(false);
		}

		public void ClearPlantsAndZombies()
		{
			if (this.mPlant != null)
			{
				this.mPlant.Die();
				this.mPlant.PrepareForReuse();
				this.mPlant = null;
			}
			if (this.mZombie != null)
			{
				this.mZombie.DieNoLoot(false);
				this.mZombie.Dispose();
				this.mZombie = null;
			}
		}

		public void SetupZombie()
		{
			this.ClearPlantsAndZombies();
			if (this.mZombie != null)
			{
				this.mZombie.PrepareForReuse();
			}
			this.mZombie = Zombie.GetNewZombie();
			this.mZombie.mBoard = null;
			this.mZombie.ZombieInitialize(0, this.mSelectedZombie, false, null, GameConstants.ZOMBIE_WAVE_UI);
			this.mZombie.mScaleZombie = 1f;
			this.mZombie.mPosX = (float)Constants.Almanac_ZombiePosition.X;
			this.mZombie.mPosY = (float)Constants.Almanac_ZombiePosition.Y;
			string text = "";
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(this.mSelectedZombie);
			string text2;
			if (this.ZombieHasDescription(this.mSelectedZombie))
			{
				string theString = Common.StrFormat_("[{0}_DESCRIPTION]", zombieDefinition.mZombieName);
				text2 = TodStringFile.TodStringTranslate(theString);
				if (TodStringFile.TodStringListExists(Common.StrFormat_("[{0}_DESCRIPTION_HEADER]", zombieDefinition.mZombieName)))
				{
					text = TodStringFile.TodStringTranslate(Common.StrFormat_("[{0}_DESCRIPTION_HEADER]", zombieDefinition.mZombieName));
				}
			}
			else
			{
				text2 = "[NOT_ENCOUNTERED_YET]";
			}
			if (text != string.Empty)
			{
				text2 = string.Concat(new string[]
				{
					"{NORMAL}",
					text,
					"\n",
					text2,
					"\n"
				});
			}
			for (int i = 0; i < GameConstants.gLawnStringFormatCount; i++)
			{
				TodStringListFormat todStringListFormat = GameConstants.gLawnStringFormats[i];
				if (TodCommon.TestBit(todStringListFormat.mFormatFlags, 1))
				{
					if (!this.mApp.HasSeedType(SeedType.SEED_MAGNETSHROOM))
					{
						todStringListFormat.mNewColor = todStringListFormat.mBaseColor;
						todStringListFormat.mNewColor.mAlpha = 0;
						todStringListFormat.mLineSpacingOffset = -17;
					}
					else
					{
						todStringListFormat.mNewColor = todStringListFormat.mBaseColor;
						todStringListFormat.mNewColor.mAlpha = 255;
						todStringListFormat.mLineSpacingOffset = 0;
					}
				}
			}
			this.mDescriptionWidget.SetText(ref text2);
			this.mDescriptionScrollWidget.ClientSizeChanged();
			this.mDescriptionScrollWidget.ScrollToMin(false);
		}

		public void ShowZombie(ZombieType theZombieType)
		{
			this.mSelectedZombie = theZombieType;
			this.SetPage(AlmanacPage.ALMANAC_PAGE_ZOMBIES);
		}

		public void ShowPlant(SeedType theSeedType)
		{
			this.mSelectedSeed = theSeedType;
			this.SetPage(AlmanacPage.ALMANAC_PAGE_PLANTS);
		}

		public bool ZombieHasDescription(ZombieType theZombieType)
		{
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			int level = this.mApp.mPlayerInfo.GetLevel();
			if (theZombieType == ZombieType.ZOMBIE_YETI)
			{
				if (!this.mApp.CanSpawnYetis())
				{
					return false;
				}
				if (this.mApp.mPlayerInfo.mFinishedAdventure >= 2)
				{
					return true;
				}
			}
			else if (this.mApp.HasFinishedAdventure())
			{
				return true;
			}
			return zombieDefinition.mStartingLevel <= level && (zombieDefinition.mStartingLevel != level || AlmanacDialog.gZombieDefeated[(int)theZombieType]);
		}

		public bool ZombieHasSilhouette(ZombieType theZombieType)
		{
			if (theZombieType != ZombieType.ZOMBIE_YETI)
			{
				return false;
			}
			if (this.mApp.CanSpawnYetis())
			{
				return false;
			}
			if (this.mApp.HasFinishedAdventure())
			{
				return true;
			}
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			int level = this.mApp.mPlayerInfo.GetLevel();
			return level > zombieDefinition.mStartingLevel;
		}

		public void DrawPaper(Graphics g, TRect theRect, SexyColor theColor)
		{
			g.SetColorizeImages(true);
			g.SetColor(theColor);
			LawnCommon.DrawImageBox(g, theRect, AtlasResources.IMAGE_ALMANAC_PAPER);
			g.SetColorizeImages(false);
		}

		public void PlantSelected(SeedType theSeedType)
		{
			if (this.mSelectedSeed != theSeedType)
			{
				this.mSelectedSeed = theSeedType;
				this.SetupPlant();
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
		}

		public void ZombieSelected(ZombieType theZombieType)
		{
			if (this.mSelectedZombie != theZombieType)
			{
				this.mSelectedZombie = theZombieType;
				this.SetupZombie();
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
		}

		public static TPoint[] ZombieOffsets = new TPoint[]
		{
			new TPoint(20, 11),
			new TPoint(13, 15),
			new TPoint(22, 14),
			new TPoint(3, 13),
			new TPoint(20, 13),
			new TPoint(18, 13),
			new TPoint(19, 13),
			new TPoint(4, 11),
			new TPoint(14, 13),
			new TPoint(19, 12),
			new TPoint(20, 13),
			new TPoint(16, 10),
			new TPoint(1, 11),
			new TPoint(16, 10),
			new TPoint(16, 15),
			new TPoint(14, 10),
			new TPoint(13, 12),
			new TPoint(10, 13),
			new TPoint(20, 12),
			new TPoint(11, 12),
			new TPoint(0, 9),
			new TPoint(15, 14),
			new TPoint(4, 12),
			new TPoint(18, 10),
			new TPoint(23, 27),
			new TPoint(1, 15)
		};

		public static bool[] gZombieDefeated = new bool[33];

		public new LawnApp mApp;

		public TRect mViewPlantsRect = default(TRect);

		public TRect mViewZombiesRect = default(TRect);

		public ScrollWidget mPlantsScrollWidget;

		public ScrollWidget mZombiesScrollWidget;

		public ScrollWidget mDescriptionScrollWidget;

		public DescriptionWidget mDescriptionWidget;

		public PlantGalleryWidget mPlantGalleryWidget;

		public ZombieGalleryWidget mZombieGalleryWidget;

		public GameButton mCloseButton;

		public GameButton mIndexButton;

		public AlmanacPage mOpenPage;

		public Reanimation[] mReanim = new Reanimation[GameConstants.NUM_ALMANAC_REANIMS];

		public SeedType mSelectedSeed;

		public ZombieType mSelectedZombie;

		public Plant mPlant;

		public Zombie mZombie;

		public AlmanacListener mListener;
	}
}
