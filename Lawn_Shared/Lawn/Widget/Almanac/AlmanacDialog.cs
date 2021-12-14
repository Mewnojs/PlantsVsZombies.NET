using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class AlmanacDialog : LawnDialog
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
			mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			mListener = theListener;
			mOpenPage = AlmanacPage.ALMANAC_PAGE_INDEX;
			mSelectedSeed = SeedType.SEED_PEASHOOTER;
			mSelectedZombie = ZombieType.ZOMBIE_NORMAL;
			mZombie = null;
			mPlant = null;
			mDrawStandardBack = false;
			mViewPlantsRect = Constants.Almanac_PlantsButtonRect;
			mViewZombiesRect = Constants.Almanac_ZombiesButtonRect;
			mPlantGalleryWidget = new PlantGalleryWidget(this);
			mPlantsScrollWidget = new ScrollWidget();
			mPlantsScrollWidget.Resize(Constants.Almanac_PlantScrollRect.mX, Constants.Almanac_PlantScrollRect.mY, Constants.Almanac_PlantScrollRect.mWidth, Constants.Almanac_PlantScrollRect.mHeight);
			mPlantsScrollWidget.AddWidget(mPlantGalleryWidget);
			mPlantGalleryWidget.Move(0, 0);
			AddWidget(mPlantsScrollWidget);
			mPlantsScrollWidget.SetVisible(false);
			mPlantsScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			mZombieGalleryWidget = new ZombieGalleryWidget(this);
			mZombiesScrollWidget = new ScrollWidget();
			mZombiesScrollWidget.Resize(Constants.Almanac_ZombieScrollRect.mX, Constants.Almanac_ZombieScrollRect.mY, Constants.Almanac_ZombieScrollRect.mWidth, Constants.Almanac_ZombieScrollRect.mHeight);
			mZombiesScrollWidget.AddWidget(mZombieGalleryWidget);
			mZombieGalleryWidget.Move(0, 0);
			AddWidget(mZombiesScrollWidget);
			mZombiesScrollWidget.SetVisible(false);
			mZombiesScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			mDescriptionWidget = new DescriptionWidget();
			mDescriptionScrollWidget = new ScrollWidget();
			mDescriptionScrollWidget.Resize(Constants.Almanac_DescriptionScrollRect.mX, Constants.Almanac_DescriptionScrollRect.mY, Constants.Almanac_DescriptionScrollRect.mWidth, Constants.Almanac_DescriptionScrollRect.mHeight);
			mDescriptionWidget.mWidth = mDescriptionScrollWidget.mWidth;
			mDescriptionScrollWidget.AddWidget(mDescriptionWidget);
			mDescriptionWidget.Move(0, 0);
			mDescriptionScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			AddWidget(mDescriptionScrollWidget);
			Resize(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			mCloseButton = new GameButton(0, this);
			mCloseButton.mButtonImage = AtlasResources.IMAGE_ALMANAC_CLOSEBUTTON;
			mCloseButton.mOverImage = null;
			mCloseButton.mDownImage = null;
			mCloseButton.SetFont(Resources.FONT_BRIANNETOD12);
			mCloseButton.mColors[0] = new SexyColor(42, 42, 90);
			mCloseButton.mColors[1] = new SexyColor(42, 42, 90);
			mCloseButton.Resize(Constants.Almanac_CloseButtonRect.mX, Constants.Almanac_CloseButtonRect.mY, Constants.Almanac_CloseButtonRect.mWidth, Constants.Almanac_CloseButtonRect.mHeight);
			mCloseButton.mParentWidget = this;
			mCloseButton.mTextOffsetX = -8;
			mCloseButton.mTextOffsetY = 1;
			mIndexButton = new GameButton(3, this);
			mIndexButton.mButtonImage = AtlasResources.IMAGE_ALMANAC_BACKBUTTON;
			mIndexButton.mOverImage = null;
			mIndexButton.mDownImage = null;
			mIndexButton.SetFont(Resources.FONT_BRIANNETOD12);
			mIndexButton.mColors[0] = new SexyColor(42, 42, 90);
			mIndexButton.mColors[1] = new SexyColor(42, 42, 90);
			mIndexButton.Resize(Constants.Almanac_IndexButtonRect.mX, Constants.Almanac_IndexButtonRect.mY, Constants.Almanac_IndexButtonRect.mWidth, Constants.Almanac_IndexButtonRect.mHeight);
			mIndexButton.mParentWidget = this;
			mIndexButton.mTextOffsetX = 8;
			mIndexButton.mTextOffsetY = 1;
			SetPage(AlmanacPage.ALMANAC_PAGE_INDEX);
			if (mApp.mBoard == null || !mApp.mBoard.mPaused)
			{
				mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
			}
		}

		public override void Dispose()
		{
			mCloseButton.Dispose();
			mIndexButton.Dispose();
			ClearPlantsAndZombies();
			RemoveAllWidgets(true);
			base.Dispose();
		}

		public override void Update()
		{
			mCloseButton.Update();
			mIndexButton.Update();
			if (mPlant != null)
			{
				mPlant.Update();
			}
			if (mZombie != null)
			{
				mZombie.Update();
			}
			MarkDirty();
		}

		public override bool BackButtonPress()
		{
			switch (mOpenPage)
			{
			case AlmanacPage.ALMANAC_PAGE_INDEX:
				mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
				mApp.KillAlmanacDialog();
				if (mListener != null)
				{
					mListener.BackFromAlmanac();
				}
				break;
			case AlmanacPage.ALMANAC_PAGE_PLANTS:
			case AlmanacPage.ALMANAC_PAGE_ZOMBIES:
				mApp.PlaySample(Resources.SOUND_TAP);
				SetPage(AlmanacPage.ALMANAC_PAGE_INDEX);
				break;
			}
			return true;
		}

		public override void Draw(Graphics g)
		{
			g.SetLinearBlend(true);
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX)
			{
				DrawIndex(g);
			}
			else if (mOpenPage == AlmanacPage.ALMANAC_PAGE_PLANTS)
			{
				DrawPlants(g);
			}
			else if (mOpenPage == AlmanacPage.ALMANAC_PAGE_ZOMBIES)
			{
				DrawZombies(g);
			}
			mCloseButton.Draw(g);
			mIndexButton.Draw(g);
			if (mOpenPage != AlmanacPage.ALMANAC_PAGE_INDEX)
			{
				base.DeferOverlay();
			}
		}

		public override void DrawOverlay(Graphics g)
		{
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_PLANTS)
			{
				g.SetColorizeImages(true);
				g.SetColor(Constants.Almanac_Paper_Colour);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, mPlantsScrollWidget.mX, Constants.Almanac_PlantTopGradientY, Constants.Almanac_PlantGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT.mHeight);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, mPlantsScrollWidget.mX, Constants.Almanac_BottomGradientY, Constants.Almanac_PlantGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT.mHeight);
				g.SetColorizeImages(false);
				return;
			}
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_ZOMBIES)
			{
				g.SetColorizeImages(true);
				g.SetColor(Constants.Almanac_Paper_Colour);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT, mZombiesScrollWidget.mX, Constants.Almanac_ZombieTopGradientY, Constants.Almanac_ZombieGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_TOPGRADIENT.mHeight);
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT, mZombiesScrollWidget.mX, Constants.Almanac_BottomGradientY, Constants.Almanac_ZombieGradientWidth, AtlasResources.IMAGE_ALMANAC_PLANTS_BOTTOMGRADIENT.mHeight);
				g.SetColorizeImages(false);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && mViewPlantsRect.Contains(new TPoint(x, y)))
			{
				SetPage(AlmanacPage.ALMANAC_PAGE_PLANTS);
				return;
			}
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && mViewZombiesRect.Contains(new TPoint(x, y)))
			{
				mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
				SetPage(AlmanacPage.ALMANAC_PAGE_ZOMBIES);
				return;
			}
			if (mCloseButton.IsMouseOver())
			{
				mApp.KillAlmanacDialog();
				if (mListener != null)
				{
					mListener.BackFromAlmanac();
				}
				return;
			}
			if (mIndexButton.IsMouseOver())
			{
				SetPage(AlmanacPage.ALMANAC_PAGE_INDEX);
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			mCloseButton.Update();
			mIndexButton.Update();
			if ((mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && mViewPlantsRect.Contains(new TPoint(x, y))) || mCloseButton.IsMouseOver() || mIndexButton.IsMouseOver())
			{
				mApp.PlaySample(Resources.SOUND_TAP);
			}
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX && mViewZombiesRect.Contains(new TPoint(x, y)))
			{
				mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
		}

		public virtual void KeyChar(char theChar)
		{
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			ClearPlantsAndZombies();
		}

		public void DrawIndex(Graphics g)
		{
			g.SetClipRect(0, 0, Constants.BackBufferSize.Y, Constants.BackBufferSize.X);
			LawnCommon.DrawImageBox(g, new TRect(0, 0, mWidth, mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_INDEX_HEADER, mWidth / 2 - AtlasResources.IMAGE_ALMANAC_INDEX_HEADER.mWidth / 2, Constants.AlmanacHeaderY);
			float almanac_Text_Scale = Constants.Almanac_Text_Scale;
			if (mPlant != null)
			{
				Graphics @new = Graphics.GetNew(g);
				LawnCommon.DrawImageBox(g, mViewPlantsRect, AtlasResources.IMAGE_ALMANAC_CLAY_BORDER);
				@new.mTransX += mPlant.mX;
				@new.mTransY += mPlant.mY;
				mPlant.Draw(@new);
				TodCommon.TodDrawString(g, "[VIEW_PLANTS]", Constants.Almanac_IndexPlantTextPos.X, Constants.Almanac_IndexPlantTextPos.Y, Resources.FONT_DWARVENTODCRAFT15, Constants.YellowFontColour, DrawStringJustification.DS_ALIGN_CENTER, almanac_Text_Scale);
				@new.PrepareForReuse();
			}
			if (mZombie != null)
			{
				Graphics new2 = Graphics.GetNew(g);
				LawnCommon.DrawImageBox(g, mViewZombiesRect, AtlasResources.IMAGE_ALMANAC_STONE_BORDER);
				new2.mTransX += mZombie.mX;
				new2.mTransY += mZombie.mY;
				mZombie.Draw(new2);
				TodCommon.TodDrawString(g, "[VIEW_ZOMBIES]", Constants.Almanac_IndexZombieTextPos.X, Constants.Almanac_IndexZombieTextPos.Y, Resources.FONT_DWARVENTODCRAFT15, Constants.GreenFontColour, DrawStringJustification.DS_ALIGN_CENTER, almanac_Text_Scale);
				new2.PrepareForReuse();
			}
		}

		public void SetPage(AlmanacPage thePage)
		{
			mOpenPage = thePage;
			ClearPlantsAndZombies();
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_INDEX)
			{
				mPlantsScrollWidget.SetVisible(false);
				mZombiesScrollWidget.SetVisible(false);
				mDescriptionScrollWidget.SetVisible(false);
				mPlant = Plant.GetNewPlant();
				mPlant.mBoard = null;
				mPlant.mIsOnBoard = false;
				mPlant.PlantInitialize(0, 0, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
				mPlant.mX = Constants.Almanac_IndexPlantPos.X;
				mPlant.mY = Constants.Almanac_IndexPlantPos.Y;
				if (mZombie != null)
				{
					mZombie.PrepareForReuse();
				}
				mZombie = Zombie.GetNewZombie();
				mZombie.mBoard = null;
				mZombie.ZombieInitialize(0, ZombieType.ZOMBIE_NORMAL, false, null, GameConstants.ZOMBIE_WAVE_UI);
				mZombie.mPosX = (float)Constants.Almanac_IndexZombiePos.X;
				mZombie.mPosY = (float)Constants.Almanac_IndexZombiePos.Y;
				mIndexButton.mBtnNoDraw = true;
				return;
			}
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_PLANTS)
			{
				SetupPlant();
				mPlantsScrollWidget.SetVisible(true);
				mZombiesScrollWidget.SetVisible(false);
				mPlantsScrollWidget.ScrollToMin(false);
				mDescriptionScrollWidget.SetVisible(true);
				mDescriptionScrollWidget.ScrollToMin(false);
				mIndexButton.mBtnNoDraw = false;
				return;
			}
			if (mOpenPage == AlmanacPage.ALMANAC_PAGE_ZOMBIES)
			{
				SetupZombie();
				mPlantsScrollWidget.SetVisible(false);
				mZombiesScrollWidget.SetVisible(true);
				mZombiesScrollWidget.ScrollToMin(false);
				mDescriptionScrollWidget.SetVisible(true);
				mDescriptionScrollWidget.ScrollToMin(false);
				mIndexButton.mBtnNoDraw = false;
				return;
			}
			Debug.ASSERT(false);
		}

		public void DrawZombies(Graphics g)
		{
			LawnCommon.DrawImageBox(g, new TRect(0, 0, mWidth, mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIES_HEADER, Constants.Almanac_ZombieHeader_Pos.X, Constants.Almanac_ZombieHeader_Pos.Y);
			LawnCommon.DrawImageBox(g, Constants.Almanac_ZombieStoneRect, AtlasResources.IMAGE_ALMANAC_STONE_TABLET);
			int x = Constants.Almanac_BackgroundPosition.X;
			int y = Constants.Almanac_BackgroundPosition.Y;
			if (mZombie.mZombieType == ZombieType.ZOMBIE_ZAMBONI || mZombie.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDICE, x, y);
			}
			else
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDDAY, x, y);
			}
			if (mZombie != null && !ZombieHasSilhouette(mZombie.mZombieType))
			{
				Graphics @new = Graphics.GetNew(g);
				@new.mTransX += (int)((float)mZombie.mX * Constants.S);
				@new.mTransY += (int)((float)mZombie.mY * Constants.S);
				@new.SetClipRect(ref Constants.Almanac_ZombieClipRect);
				if (mZombie.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
				{
					@new.mTransX += (int)(-30f * Constants.S);
					@new.mTransY += (int)(5f * Constants.S);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_GARGANTUAR)
				{
					@new.mTransY += (int)(30f * Constants.S);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
				{
					@new.mTransX += (int)(-10f * Constants.S);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					@new.mTransY += (int)(-20f * Constants.S);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_BUNGEE)
				{
					@new.mTransX += (int)Constants.InvertAndScale(12f);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_CATAPULT)
				{
					@new.mTransX += (int)(-10f * Constants.S);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_BOSS)
				{
					@new.mTransX += (int)(-540f * Constants.S);
					@new.mTransY += (int)(-175f * Constants.S);
				}
				else if (mZombie.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
				{
					@new.mTransX += (int)Constants.InvertAndScale(17f);
				}
				if (mZombie.mZombieType != ZombieType.ZOMBIE_BUNGEE && mZombie.mZombieType != ZombieType.ZOMBIE_BOSS && mZombie.mZombieType != ZombieType.ZOMBIE_ZAMBONI && mZombie.mZombieType != ZombieType.ZOMBIE_CATAPULT)
				{
					mZombie.DrawShadow(@new);
				}
				@new.HardwareClip();
				mZombie.Draw(@new);
				@new.EndHardwareClip();
				@new.PrepareForReuse();
			}
			LawnCommon.DrawImageBox(g, Constants.Almanac_NavyRect, AtlasResources.IMAGE_ALMANAC_NAVY_RECT, false);
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(mSelectedZombie);
			string theText;
			if (ZombieHasSilhouette(mSelectedZombie))
			{
				theText = TodStringFile.TodStringTranslate("[NOT_ENCOUNTERED_YET_NAME]");
			}
			else
			{
				theText = Common.StrFormat_("[{0}]", zombieDefinition.mZombieName);
			}
			TodCommon.TodDrawString(g, theText, Constants.Almanac_NamePosition.X, Constants.Almanac_NamePosition.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(0, 196, 0), Constants.Almanac_ItemName_MaxWidth, DrawStringJustification.DS_ALIGN_CENTER);
			TRect rect = mDescriptionScrollWidget.GetRect();
			rect.Inflate((int)Constants.InvertAndScale(6f), (int)Constants.InvertAndScale(6f));
			DrawPaper(g, rect, new SexyColor(171, 159, 207));
		}

		public void DrawPlants(Graphics g)
		{
			LawnCommon.DrawImageBox(g, new TRect(0, 0, mWidth, mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTS_HEADER, Constants.Almanac_PlantsHeader_Pos.X, Constants.Almanac_PlantsHeader_Pos.Y);
			LawnCommon.DrawImageBox(g, Constants.Almanac_ClayRect, AtlasResources.IMAGE_ALMANAC_CLAY_TABLET);
			int x = Constants.Almanac_BackgroundPosition.X;
			int y = Constants.Almanac_BackgroundPosition.Y;
			if (mSelectedSeed == SeedType.SEED_LILYPAD || mSelectedSeed == SeedType.SEED_TANGLEKELP || mSelectedSeed == SeedType.SEED_CATTAIL)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDPOOL, x, y);
			}
			else if (mSelectedSeed == SeedType.SEED_SEASHROOM)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDNIGHTPOOL, x, y);
			}
			else if (Plant.IsNocturnal(mSelectedSeed) || mSelectedSeed == SeedType.SEED_GRAVEBUSTER || mSelectedSeed == SeedType.SEED_PLANTERN)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDNIGHT, x, y);
			}
			else if (mSelectedSeed == SeedType.SEED_FLOWERPOT)
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDROOF, x, y);
			}
			else
			{
				g.DrawImage(AtlasResources.IMAGE_ALMANAC_GROUNDDAY, x, y);
			}
			if (mPlant != null)
			{
				Graphics @new = Graphics.GetNew(g);
				@new.mTransX += mPlant.mX;
				@new.mTransY += mPlant.mY;
				mPlant.Draw(@new);
				@new.PrepareForReuse();
			}
			LawnCommon.DrawImageBox(g, Constants.Almanac_BrownRect, AtlasResources.IMAGE_ALMANAC_BROWN_RECT, false);
			string nameString = Plant.GetNameString(mSelectedSeed, SeedType.SEED_NONE);
			TodCommon.TodDrawString(g, nameString, Constants.Almanac_NamePosition.X, Constants.Almanac_NamePosition.Y, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(213, 159, 43), Constants.Almanac_ItemName_MaxWidth, DrawStringJustification.DS_ALIGN_CENTER);
			TRect rect = mDescriptionScrollWidget.GetRect();
			rect.Inflate((int)Constants.InvertAndScale(6f), (int)Constants.InvertAndScale(6f));
			DrawPaper(g, rect, new SexyColor(253, 186, 117));
		}

		public void SetupPlant()
		{
			ClearPlantsAndZombies();
			float num = (float)Constants.Almanac_PlantPosition.X;
			float num2 = (float)Constants.Almanac_PlantPosition.Y;
			if (mSelectedSeed == SeedType.SEED_TALLNUT)
			{
				num2 += (float)((int)Constants.InvertAndScale(18f));
			}
			if (mSelectedSeed == SeedType.SEED_COBCANNON)
			{
				num += (float)((int)Constants.InvertAndScale(-20f));
			}
			if (mSelectedSeed == SeedType.SEED_FLOWERPOT)
			{
				num2 += (float)((int)Constants.InvertAndScale(-20f));
			}
			if (mSelectedSeed == SeedType.SEED_INSTANT_COFFEE)
			{
				num2 += (float)((int)Constants.InvertAndScale(20f));
			}
			if (mSelectedSeed == SeedType.SEED_GRAVEBUSTER)
			{
				num2 += (float)((int)Constants.InvertAndScale(35f));
			}
			mPlant = Plant.GetNewPlant();
			mPlant.mBoard = null;
			mPlant.mIsOnBoard = false;
			mPlant.PlantInitialize(0, 0, mSelectedSeed, SeedType.SEED_NONE);
			mPlant.mX = (int)num;
			mPlant.mY = (int)num2;
			PlantDefinition plantDefinition = Plant.GetPlantDefinition(mSelectedSeed);
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
			if (mSelectedSeed != SeedType.SEED_IMITATER)
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
			mDescriptionWidget.SetText(ref text2);
			mDescriptionScrollWidget.ClientSizeChanged();
			mDescriptionScrollWidget.ScrollToMin(false);
		}

		public void ClearPlantsAndZombies()
		{
			if (mPlant != null)
			{
				mPlant.Die();
				mPlant.PrepareForReuse();
				mPlant = null;
			}
			if (mZombie != null)
			{
				mZombie.DieNoLoot(false);
				mZombie.Dispose();
				mZombie = null;
			}
		}

		public void SetupZombie()
		{
			ClearPlantsAndZombies();
			if (mZombie != null)
			{
				mZombie.PrepareForReuse();
			}
			mZombie = Zombie.GetNewZombie();
			mZombie.mBoard = null;
			mZombie.ZombieInitialize(0, mSelectedZombie, false, null, GameConstants.ZOMBIE_WAVE_UI);
			mZombie.mScaleZombie = 1f;
			mZombie.mPosX = (float)Constants.Almanac_ZombiePosition.X;
			mZombie.mPosY = (float)Constants.Almanac_ZombiePosition.Y;
			string text = "";
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(mSelectedZombie);
			string text2;
			if (ZombieHasDescription(mSelectedZombie))
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
					if (!mApp.HasSeedType(SeedType.SEED_MAGNETSHROOM))
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
			mDescriptionWidget.SetText(ref text2);
			mDescriptionScrollWidget.ClientSizeChanged();
			mDescriptionScrollWidget.ScrollToMin(false);
		}

		public void ShowZombie(ZombieType theZombieType)
		{
			mSelectedZombie = theZombieType;
			SetPage(AlmanacPage.ALMANAC_PAGE_ZOMBIES);
		}

		public void ShowPlant(SeedType theSeedType)
		{
			mSelectedSeed = theSeedType;
			SetPage(AlmanacPage.ALMANAC_PAGE_PLANTS);
		}

		public bool ZombieHasDescription(ZombieType theZombieType)
		{
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			int level = mApp.mPlayerInfo.GetLevel();
			if (theZombieType == ZombieType.ZOMBIE_YETI)
			{
				if (!mApp.CanSpawnYetis())
				{
					return false;
				}
				if (mApp.mPlayerInfo.mFinishedAdventure >= 2)
				{
					return true;
				}
			}
			else if (mApp.HasFinishedAdventure())
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
			if (mApp.CanSpawnYetis())
			{
				return false;
			}
			if (mApp.HasFinishedAdventure())
			{
				return true;
			}
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			int level = mApp.mPlayerInfo.GetLevel();
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
			if (mSelectedSeed != theSeedType)
			{
				mSelectedSeed = theSeedType;
				SetupPlant();
				mApp.PlaySample(Resources.SOUND_TAP);
			}
		}

		public void ZombieSelected(ZombieType theZombieType)
		{
			if (mSelectedZombie != theZombieType)
			{
				mSelectedZombie = theZombieType;
				SetupZombie();
				mApp.PlaySample(Resources.SOUND_TAP);
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
