using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class ZenGarden : StoreListener
	{
		public ZenGarden()
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = null;
			this.mGardenType = GardenType.GARDEN_MAIN;
			this.mIsTutorial = false;
		}

		static ZenGarden()
		{
			for (int i = 0; i < ZenGarden.gGreenhouseGridPlacement.Length; i++)
			{
				ZenGarden.gGreenhouseGridPlacement[i].mPixelX = (int)((float)ZenGarden.gGreenhouseGridPlacement[i].mPixelX * Constants.ZenGardenGreenhouseMultiplierX) + Constants.ZenGardenGreenhouseOffset.X;
				ZenGarden.gGreenhouseGridPlacement[i].mPixelY = (int)((float)ZenGarden.gGreenhouseGridPlacement[i].mPixelY * Constants.ZenGardenGreenhouseMultiplierY) + Constants.ZenGardenGreenhouseOffset.Y;
			}
			for (int j = 0; j < Constants.gMushroomGridPlacement.Length; j++)
			{
				Constants.gMushroomGridPlacement[j].mPixelX = Constants.gMushroomGridPlacement[j].mPixelX + Constants.ZenGardenMushroomGardenOffset.X;
				Constants.gMushroomGridPlacement[j].mPixelY = Constants.gMushroomGridPlacement[j].mPixelY + Constants.ZenGardenMushroomGardenOffset.Y;
			}
		}

		public void Dispose()
		{
			this.UnloadBackdrop();
		}

		public void UnloadBackdrop()
		{
			this.mApp.DelayLoadZenGardenBackground(string.Empty);
		}

		public void ZenGardenInitLevel(bool theJustSwitchingGardens)
		{
			this.mBoard = this.mApp.mBoard;
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant pottedPlant = this.PottedPlantFromIndex(i);
				if (pottedPlant.mWhichZenGarden == this.mGardenType)
				{
					this.PlacePottedPlant(i);
				}
			}
			Challenge mChallenge = this.mBoard.mChallenge;
			mChallenge.mChallengeStateCounter = 3000;
			this.AddStinky();
			this.mApp.mMusic.StartGameMusic();
		}

		public float GetPottedPlantXOffset(SeedType theType, bool isFlipped)
		{
			return 0f;
		}

		public float GetPottedPlantYOffset(SeedType theType, bool isFlipped)
		{
			return 0f;
		}

		public void DrawPottedPlantIcon(Graphics g, float x, float y, PottedPlant thePottedPlant)
		{
			this.DrawPottedPlant(g, x, y, thePottedPlant, 0.7f, true);
		}

		public void DrawPottedPlant(Graphics g, float x, float y, PottedPlant thePottedPlant, float theScale, bool theDrawPot)
		{
			Graphics @new = Graphics.GetNew(g);
			@new.mScaleX = theScale;
			@new.mScaleY = theScale;
			DrawVariation theDrawVariation = DrawVariation.VARIATION_NORMAL;
			SeedType seedType = thePottedPlant.mSeedType;
			if (thePottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SPROUT)
			{
				seedType = SeedType.SEED_SPROUT;
				if (thePottedPlant.mSeedType != SeedType.SEED_MARIGOLD)
				{
					theDrawVariation = DrawVariation.VARIATION_SPROUT_NO_FLOWER;
				}
			}
			else if (seedType == SeedType.SEED_TANGLEKELP && thePottedPlant.mWhichZenGarden == GardenType.GARDEN_AQUARIUM)
			{
				theDrawVariation = DrawVariation.VARIATION_AQUARIUM;
			}
			else if (seedType == SeedType.SEED_SEASHROOM && thePottedPlant.mWhichZenGarden == GardenType.GARDEN_AQUARIUM)
			{
				theDrawVariation = DrawVariation.VARIATION_AQUARIUM;
			}
			else if (seedType == SeedType.SEED_SUNSHROOM)
			{
				theDrawVariation = DrawVariation.VARIATION_BIGIDLE;
			}
			else
			{
				theDrawVariation = thePottedPlant.mDrawVariation;
			}
			PottedPlant.FacingDirection mFacing = thePottedPlant.mFacing;
			float num = 0f;
			float num2 = 0f;
			if (theDrawPot)
			{
				DrawVariation theDrawVariation2 = DrawVariation.VARIATION_ZEN_GARDEN;
				if (Plant.IsAquatic(seedType))
				{
					theDrawVariation2 = DrawVariation.VARIATION_ZEN_GARDEN_WATER;
				}
				Plant.DrawSeedType(@new, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE, theDrawVariation2, x, y);
			}
			if (thePottedPlant.mFacing == PottedPlant.FacingDirection.FACING_LEFT)
			{
				@new.mScaleX = -theScale;
			}
			if (theDrawPot)
			{
				num2 += Constants.InvertAndScale((float)ZenGarden.POTTED_PLANT_DRAW_OFFSETS[(int)seedType].yCachedOffset) * @new.mScaleY;
				num += Constants.InvertAndScale((float)ZenGarden.POTTED_PLANT_DRAW_OFFSETS[(int)seedType].xCachedOffset) * @new.mScaleX;
			}
			Plant.DrawSeedType(@new, seedType, SeedType.SEED_NONE, theDrawVariation, x + num, y + Constants.S * num2);
			@new.PrepareForReuse();
		}

		public bool IsZenGardenFull(bool theIncludeDroppedPresents)
		{
			int num = 0;
			if (this.mBoard != null && theIncludeDroppedPresents)
			{
				num += this.mBoard.CountCoinByType(CoinType.COIN_AWARD_PRESENT);
				num += this.mBoard.CountCoinByType(CoinType.COIN_PRESENT_PLANT);
			}
			int num2 = 0;
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant pottedPlant = this.PottedPlantFromIndex(i);
				if (pottedPlant.mWhichZenGarden == GardenType.GARDEN_MAIN)
				{
					num2++;
				}
			}
			return num2 + num >= 32;
		}

		public void FindOpenZenGardenSpot(ref int theSpotX, ref int theSpotY)
		{
			TodWeightedGridArray[] array = new TodWeightedGridArray[32];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
			}
			int num = 0;
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 4; k++)
				{
					if (this.mApp.mCrazyDaveMessageIndex == -1 || (j >= 2 && k >= 1))
					{
						bool flag = false;
						for (int l = 0; l < this.mApp.mPlayerInfo.mNumPottedPlants; l++)
						{
							PottedPlant pottedPlant = this.PottedPlantFromIndex(l);
							if (pottedPlant.mWhichZenGarden == GardenType.GARDEN_MAIN && pottedPlant.mX == j && pottedPlant.mY == k)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							array[num].mX = j;
							array[num].mY = k;
							array[num].mWeight = 1;
							num++;
						}
					}
				}
			}
			TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array, num);
			theSpotX = todWeightedGridArray.mX;
			theSpotY = todWeightedGridArray.mY;
		}

		public void AddPottedPlant(PottedPlant thePottedPlant)
		{
			Debug.ASSERT(this.mApp.mPlayerInfo.mNumPottedPlants < 200);
			int mNumPottedPlants = this.mApp.mPlayerInfo.mNumPottedPlants;
			PottedPlant pottedPlant = this.mApp.mPlayerInfo.mPottedPlant[mNumPottedPlants];
			pottedPlant.mDrawVariation = thePottedPlant.mDrawVariation;
			pottedPlant.mFacing = thePottedPlant.mFacing;
			pottedPlant.mFeedingsPerGrow = thePottedPlant.mFeedingsPerGrow;
			pottedPlant.mFutureAttribute = thePottedPlant.mFutureAttribute;
			pottedPlant.mLastChocolateTime = thePottedPlant.mLastChocolateTime;
			pottedPlant.mLastFertilizedTime = thePottedPlant.mLastFertilizedTime;
			pottedPlant.mLastNeedFulfilledTime = thePottedPlant.mLastNeedFulfilledTime;
			pottedPlant.mPlantAge = thePottedPlant.mPlantAge;
			pottedPlant.mPlantNeed = thePottedPlant.mPlantNeed;
			pottedPlant.mSeedType = thePottedPlant.mSeedType;
			pottedPlant.mTimesFed = thePottedPlant.mTimesFed;
			pottedPlant.mX = thePottedPlant.mX;
			pottedPlant.mY = thePottedPlant.mY;
			pottedPlant.mWhichZenGarden = GardenType.GARDEN_MAIN;
			pottedPlant.mLastWateredTime = default(DateTime);
			this.FindOpenZenGardenSpot(ref pottedPlant.mX, ref pottedPlant.mY);
			this.mApp.mPlayerInfo.mNumPottedPlants++;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mBoard != null && pottedPlant.mWhichZenGarden == this.mGardenType)
			{
				Plant thePlant = this.PlacePottedPlant(mNumPottedPlants);
				if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) == null)
				{
					this.mBoard.DoPlantingEffects(pottedPlant.mX, pottedPlant.mY, thePlant, this.mGardenType == GardenType.GARDEN_AQUARIUM);
				}
			}
		}

		public void MouseDownWithTool(int x, int y, CursorType theCursorType)
		{
			if (theCursorType == CursorType.CURSOR_TYPE_WHEEELBARROW && this.GetPottedPlantInWheelbarrow() != null)
			{
				this.MouseDownWithFullWheelBarrow(x, y);
				this.mBoard.ClearCursor();
				return;
			}
			if (theCursorType == CursorType.CURSOR_TYPE_WATERING_CAN || theCursorType == CursorType.CURSOR_TYPE_FERTILIZER || theCursorType == CursorType.CURSOR_TYPE_BUG_SPRAY || theCursorType == CursorType.CURSOR_TYPE_PHONOGRAPH || theCursorType == CursorType.CURSOR_TYPE_CHOCOLATE)
			{
				this.MouseDownWithFeedingTool(x, y, theCursorType);
				return;
			}
			HitResult hitResult = this.mBoard.ToolHitTest(x, y, true);
			Plant plant = null;
			if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
			{
				plant = (Plant)hitResult.mObject;
			}
			if (plant == null || plant.mPottedPlantIndex == -1)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				this.mBoard.ClearCursor();
				return;
			}
			if (theCursorType == CursorType.CURSOR_TYPE_MONEY_SIGN)
			{
				this.MouseDownWithMoneySign(plant);
				return;
			}
			if (theCursorType == CursorType.CURSOR_TYPE_WHEEELBARROW)
			{
				this.MouseDownWithEmptyWheelBarrow(plant);
				this.mBoard.ClearCursor();
				return;
			}
			if (theCursorType == CursorType.CURSOR_TYPE_GLOVE)
			{
				this.mBoard.mCursorObject.mType = plant.mSeedType;
				this.mBoard.mCursorObject.mImitaterType = plant.mImitaterType;
				this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE;
				this.mBoard.mCursorObject.mGlovePlantID = this.mBoard.mPlants[this.mBoard.mPlants.IndexOf(plant)];
				plant.mGloveGrabbed = true;
				this.mBoard.mIgnoreMouseUp = true;
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
		}

		public void MovePlant(Plant thePlant, int theGridX, int theGridY)
		{
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				return;
			}
			int num = this.mBoard.GridToPixelX(theGridX, theGridY);
			int num2 = this.mBoard.GridToPixelY(theGridX, theGridY);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				num2 -= Constants.ZenGardenGreenhouseOffset.Y;
			}
			Debug.ASSERT(this.mBoard.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_ANY) == null);
			bool mIsAsleep = thePlant.mIsAsleep;
			thePlant.SetSleeping(false);
			Plant topPlantAt = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ONLY_UNDER_PLANT);
			if (topPlantAt != null)
			{
				topPlantAt.mX = num;
				topPlantAt.mY = num2;
				topPlantAt.mPlantCol = theGridX;
				topPlantAt.mRow = theGridY;
				topPlantAt.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, 0, topPlantAt.mY);
			}
			float num3 = (float)(num - thePlant.mX);
			float num4 = (float)(num2 - thePlant.mY);
			thePlant.mX = num;
			thePlant.mY = num2;
			thePlant.mPlantCol = theGridX;
			thePlant.mRow = theGridY;
			thePlant.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, 0, thePlant.mY + 1);
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(thePlant.mParticleID);
			if (todParticleSystem != null && todParticleSystem.mEmitterList.Count != 0)
			{
				TodParticleEmitter todParticleEmitter = todParticleSystem.mParticleHolder.mEmitters[0];
				todParticleSystem.SystemMove(todParticleEmitter.mSystemCenter.x + num3, todParticleEmitter.mSystemCenter.y + num4);
			}
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			pottedPlant.mX = theGridX;
			pottedPlant.mY = theGridY;
			if (thePlant.mState == PlantState.STATE_ZEN_GARDEN_HAPPY)
			{
				this.RemoveHappyEffect(thePlant);
				this.AddHappyEffect(thePlant);
			}
			if (topPlantAt != null)
			{
				this.mBoard.DoPlantingEffects(theGridX, theGridY, topPlantAt, this.mGardenType == GardenType.GARDEN_AQUARIUM);
				return;
			}
			this.mBoard.DoPlantingEffects(theGridX, theGridY, thePlant, this.mGardenType == GardenType.GARDEN_AQUARIUM);
		}

		public void MouseDownWithMoneySign(Plant thePlant)
		{
			this.mBoard.ClearCursor();
			string theDialogHeader = TodStringFile.TodStringTranslate("[ZEN_SELL_HEADER]");
			string theDialogLines = TodStringFile.TodStringTranslate("[ZEN_SELL_LINES]");
			int plantSellPrice = this.GetPlantSellPrice(thePlant);
			if (this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF)
			{
				this.mApp.CrazyDaveEnter();
			}
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			string text = this.mApp.GetCrazyDaveText(1700);
			text = TodCommon.TodReplaceString(text, "{SELL_PRICE}", Common.CommaSeperate(plantSellPrice * 10));
			string theStringToSubstitute = string.Empty;
			if (thePlant.mSeedType == SeedType.SEED_SPROUT && pottedPlant.mSeedType == SeedType.SEED_MARIGOLD)
			{
				theStringToSubstitute = TodStringFile.TodStringTranslate("[MARIGOLD_SPROUT]");
			}
			else
			{
				theStringToSubstitute = Plant.GetNameString(thePlant.mSeedType, thePlant.mImitaterType);
			}
			text = TodCommon.TodReplaceString(text, "{PLANT_TYPE}", theStringToSubstitute);
			this.mApp.CrazyDaveTalkMessage(text);
			Reanimation reanimation = this.mApp.ReanimationGet(this.mApp.mCrazyDaveReanimID);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_blahblah, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			LawnDialog lawnDialog = this.mApp.DoDialog(48, true, theDialogHeader, theDialogLines, "", 1);
			lawnDialog.mX += Constants.ZenGarden_SellDialog_Offset.X;
			lawnDialog.mY += Constants.ZenGarden_SellDialog_Offset.Y;
			this.mBoard.ShowCoinBank();
			this.mPlantForSale = thePlant;
		}

		public Plant PlacePottedPlant(int thePottedPlantIndex)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePottedPlantIndex);
			SeedType seedType = pottedPlant.mSeedType;
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SPROUT)
			{
				seedType = SeedType.SEED_SPROUT;
			}
			bool flag = true;
			if (this.mGardenType == GardenType.GARDEN_MUSHROOM && !Plant.IsAquatic(seedType))
			{
				flag = false;
			}
			else if (this.mGardenType == GardenType.GARDEN_AQUARIUM)
			{
				flag = false;
			}
			if (flag)
			{
				Plant plant = this.mBoard.NewPlant(pottedPlant.mX, pottedPlant.mY, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
				plant.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, 0, plant.mY);
				plant.mStateCountdown = 0;
				Reanimation reanimation = this.mApp.ReanimationGet(plant.mBodyReanimID);
				if (Plant.IsAquatic(seedType))
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_waterplants);
				}
				else
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_zengarden);
				}
			}
			int num = (int)seedType;
			if (num < 0 || num >= 53)
			{
				pottedPlant.mSeedType = SeedType.SEED_KERNELPULT;
				seedType = SeedType.SEED_KERNELPULT;
			}
			Plant plant2 = this.mBoard.NewPlant(pottedPlant.mX, pottedPlant.mY, seedType, SeedType.SEED_NONE);
			plant2.mPottedPlantIndex = thePottedPlantIndex;
			plant2.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, 0, plant2.mY + 1);
			plant2.mStateCountdown = 0;
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(plant2.mBodyReanimID);
			if (reanimation2 != null)
			{
				if (seedType == SeedType.SEED_SPROUT)
				{
					if (pottedPlant.mSeedType != SeedType.SEED_MARIGOLD)
					{
						reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_noflower);
					}
				}
				else if (seedType == SeedType.SEED_TANGLEKELP && this.mGardenType == GardenType.GARDEN_AQUARIUM)
				{
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_aquarium);
				}
				else if (seedType == SeedType.SEED_SEASHROOM && this.mGardenType == GardenType.GARDEN_AQUARIUM)
				{
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_aquarium);
				}
				plant2.UpdateReanim();
				reanimation2.Update();
			}
			this.PlantSetLaunchCounter(plant2);
			this.UpdatePlantEffectState(plant2);
			return plant2;
		}

		public float PlantPottedDrawHeightOffset(SeedType theSeedType, float theScale, bool bInWheelBarrow)
		{
			return this.PlantPottedDrawHeightOffset(theSeedType, theScale, bInWheelBarrow, DrawVariation.VARIATION_NORMAL);
		}

		public float PlantPottedDrawHeightOffset(SeedType theSeedType, float theScale, bool bInWheelBarrow, DrawVariation theDrawVariation)
		{
			float num = 0f;
			float num2 = 0f;
			if (theSeedType == SeedType.SEED_GRAVEBUSTER)
			{
				num2 += 50f;
				num += 15f;
			}
			else if (theSeedType == SeedType.SEED_PUFFSHROOM)
			{
				num2 += 10f;
				num += 24f;
			}
			else if (theSeedType == SeedType.SEED_SUNSHROOM)
			{
				num2 += 10f;
				num += 17f;
			}
			else if (theSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				num2 += 5f;
				num += 5f;
			}
			else if (theSeedType == SeedType.SEED_TANGLEKELP)
			{
				num2 += -18f;
				num += 20f;
			}
			else if (theSeedType == SeedType.SEED_SEASHROOM)
			{
				num2 += -20f;
				num += 15f;
			}
			else if (theSeedType == SeedType.SEED_LILYPAD)
			{
				num2 += -10f;
				num += 30f;
			}
			else if (theSeedType == SeedType.SEED_CHOMPER)
			{
				num += 0f;
			}
			else if (theSeedType == SeedType.SEED_HYPNOSHROOM)
			{
				num += 10f;
			}
			else if (theSeedType == SeedType.SEED_MAGNETSHROOM)
			{
				num += 10f;
			}
			else if (theSeedType == SeedType.SEED_PEASHOOTER || theSeedType == SeedType.SEED_REPEATER || theSeedType == SeedType.SEED_LEFTPEATER || theSeedType == SeedType.SEED_SNOWPEA || theSeedType == SeedType.SEED_THREEPEATER || theSeedType == SeedType.SEED_SUNFLOWER || theSeedType == SeedType.SEED_MARIGOLD)
			{
				num += 10f;
			}
			else if (theSeedType == SeedType.SEED_STARFRUIT)
			{
				num2 += 10f;
				num += 24f;
			}
			else if (theSeedType == SeedType.SEED_CABBAGEPULT || theSeedType == SeedType.SEED_MELONPULT)
			{
				num += 10f;
				num2 += 3f;
			}
			else if (theSeedType == SeedType.SEED_POTATOMINE)
			{
				num += 5f;
			}
			else if (theSeedType == SeedType.SEED_TORCHWOOD)
			{
				num += 3f;
			}
			else if (theSeedType == SeedType.SEED_SPIKEWEED)
			{
				num += 10f;
				num2 -= 13f;
			}
			else if (theSeedType == SeedType.SEED_BLOVER)
			{
				num += 10f;
			}
			else if (theSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				num += 20f;
			}
			else if (theSeedType == SeedType.SEED_PLANTERN)
			{
				num += -1f;
			}
			if (bInWheelBarrow && theSeedType != SeedType.SEED_FLOWERPOT)
			{
				float num3 = (float)ZenGarden.POTTED_PLANT_DRAW_OFFSETS[(int)theSeedType].yWheelBarrowScale;
				num2 += num3 + num3 * (theScale - 0.5f) / 2f;
			}
			num = Constants.InvertAndScale(num);
			return num2 + (-num + num * theScale);
		}

		public int GetPlantSellPrice(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			if (pottedPlant.mSeedType == SeedType.SEED_MARIGOLD)
			{
				if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SPROUT)
				{
					return 150;
				}
				if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SMALL)
				{
					return 200;
				}
				if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_MEDIUM)
				{
					return 250;
				}
				if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_FULL)
				{
					return 300;
				}
				Debug.ASSERT(false);
			}
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SPROUT)
			{
				return 150;
			}
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SMALL)
			{
				return 300;
			}
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_MEDIUM)
			{
				return 500;
			}
			if (pottedPlant.mPlantAge != PottedPlantAge.PLANTAGE_FULL)
			{
				Debug.ASSERT(false);
				return -666;
			}
			if (Plant.IsNocturnal(pottedPlant.mSeedType) || Plant.IsAquatic(pottedPlant.mSeedType))
			{
				return 1000;
			}
			return 800;
		}

		public void ZenGardenUpdate(int updateCount)
		{
			if (this.mApp.GetDialog(4) != null)
			{
				return;
			}
			Challenge mChallenge = this.mBoard.mChallenge;
			if (this.mBoard.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_NORMAL)
			{
				mChallenge.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				mChallenge.mChallengeStateCounter = 3000;
			}
			else if (this.mApp.mBoard.mTutorialState == TutorialState.TUTORIAL_OFF)
			{
				if (mChallenge.mChallengeStateCounter > 0)
				{
					mChallenge.mChallengeStateCounter--;
				}
				if (mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL && mChallenge.mChallengeStateCounter == 0)
				{
					mChallenge.mChallengeState = ChallengeState.STATECHALLENGE_ZEN_FADING;
					mChallenge.mChallengeStateCounter = 50;
				}
			}
			this.UpdatePlantNeeds();
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mPottedPlantIndex != -1)
				{
					this.PottedPlantUpdate(plant);
				}
			}
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_ZEN_TOOL)
				{
					this.ZenToolUpdate(gridItem);
				}
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_STINKY)
				{
					this.StinkyUpdate(gridItem);
				}
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_KEEP_WATERING && this.CountPlantsNeedingFertilizer() > 0)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_VISIT_STORE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_LONG, AdviceType.ADVICE_NONE);
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZEN_GARDEN_VISIT_STORE;
				this.mApp.mPlayerInfo.mZenTutorialMessage = 25;
				this.mBoard.mStoreButton.mDisabled = false;
				this.mBoard.mStoreButton.mBtnNoDraw = false;
			}
		}

		public void MouseDownWithFullWheelBarrow(int x, int y)
		{
			PottedPlant pottedPlantInWheelbarrow = this.GetPottedPlantInWheelbarrow();
			Debug.ASSERT(pottedPlantInWheelbarrow != null);
			if (this.mApp.mZenGarden.mGardenType == GardenType.GARDEN_AQUARIUM && !Plant.IsAquatic(pottedPlantInWheelbarrow.mSeedType))
			{
				this.mBoard.DisplayAdvice("[ZEN_ONLY_AQUATIC_PLANTS]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NONE);
				return;
			}
			int num = this.mBoard.PixelToGridX(x, y);
			int num2 = this.mBoard.PixelToGridY(x, y);
			if (num == -1 || num2 == -1)
			{
				return;
			}
			PlantingReason plantingReason = this.mBoard.CanPlantAt(num, num2, pottedPlantInWheelbarrow.mSeedType);
			if (plantingReason != PlantingReason.PLANTING_OK)
			{
				return;
			}
			pottedPlantInWheelbarrow.mWhichZenGarden = this.mGardenType;
			pottedPlantInWheelbarrow.mX = num;
			pottedPlantInWheelbarrow.mY = num2;
			int thePottedPlantIndex = -1;
			for (int i = 0; i < this.mApp.mPlayerInfo.mPottedPlant.Length; i++)
			{
				if (pottedPlantInWheelbarrow == this.mApp.mPlayerInfo.mPottedPlant[i])
				{
					thePottedPlantIndex = i;
					break;
				}
			}
			Plant thePlant = this.PlacePottedPlant(thePottedPlantIndex);
			this.mBoard.DoPlantingEffects(pottedPlantInWheelbarrow.mX, pottedPlantInWheelbarrow.mY, thePlant, this.mGardenType == GardenType.GARDEN_AQUARIUM);
		}

		public void MouseDownWithEmptyWheelBarrow(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			this.RemovePottedPlant(thePlant);
			pottedPlant.mWhichZenGarden = GardenType.GARDEN_WHEELBARROW;
			pottedPlant.mX = 0;
			pottedPlant.mY = 0;
			this.mApp.PlayFoley(FoleyType.FOLEY_PLANT);
		}

		public void GotoNextGarden()
		{
			this.LeaveGarden();
			this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			this.mBoard.mPlants.Clear();
			this.mBoard.mCoins.Clear();
			this.mApp.mEffectSystem.EffectSystemFreeAll();
			bool flag = false;
			if (this.mGardenType == GardenType.GARDEN_MAIN)
			{
				if (this.mApp.mPlayerInfo.mPurchases[18] != 0)
				{
					this.mGardenType = GardenType.GARDEN_MUSHROOM;
					this.mBoard.mBackground = BackgroundType.BACKGROUND_MUSHROOM_GARDEN;
				}
				else if (this.mApp.mPlayerInfo.mPurchases[25] != 0)
				{
					this.mGardenType = GardenType.GARDEN_AQUARIUM;
					this.mBoard.mBackground = BackgroundType.BACKGROUND_ZOMBIQUARIUM;
				}
				else if (this.mApp.mPlayerInfo.mPurchases[27] != 0)
				{
					flag = true;
				}
			}
			else if (this.mGardenType == GardenType.GARDEN_MUSHROOM)
			{
				if (this.mApp.mPlayerInfo.mPurchases[25] != 0)
				{
					this.mGardenType = GardenType.GARDEN_AQUARIUM;
					this.mBoard.mBackground = BackgroundType.BACKGROUND_ZOMBIQUARIUM;
				}
				else if (this.mApp.mPlayerInfo.mPurchases[27] != 0)
				{
					flag = true;
				}
				else
				{
					this.mGardenType = GardenType.GARDEN_MAIN;
					this.mBoard.mBackground = BackgroundType.BACKGROUND_GREENHOUSE;
				}
			}
			else if (this.mGardenType == GardenType.GARDEN_AQUARIUM)
			{
				this.mGardenType = GardenType.GARDEN_MAIN;
				this.mBoard.mBackground = BackgroundType.BACKGROUND_GREENHOUSE;
			}
			if (flag)
			{
				this.mApp.KillBoard();
				this.mApp.PreNewGame(GameMode.GAMEMODE_TREE_OF_WISDOM, false);
				return;
			}
			if (this.mBoard.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN)
			{
				this.mApp.DelayLoadZenGardenBackground("DelayLoad_MushroomGarden");
			}
			else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_GREENHOUSE)
			{
				this.mApp.DelayLoadZenGardenBackground("DelayLoad_GreenHouseGarden");
			}
			else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM)
			{
				this.mApp.DelayLoadZenGardenBackground("DelayLoad_Zombiquarium");
			}
			else
			{
				Debug.ASSERT(false);
			}
			if ((this.mBoard.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN || this.mBoard.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM) && this.mApp.mPlayerInfo.mPurchases[19] == 0)
			{
				this.mBoard.DisplayAdvice("[ADVICE_NEED_WHEELBARROW]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NEED_WHEELBARROW);
			}
			this.ZenGardenInitLevel(true);
		}

		public PottedPlant GetPottedPlantInWheelbarrow()
		{
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant pottedPlant = this.PottedPlantFromIndex(i);
				if (pottedPlant.mWhichZenGarden == GardenType.GARDEN_WHEELBARROW)
				{
					return pottedPlant;
				}
			}
			return null;
		}

		public void RemovePottedPlant(Plant thePlant)
		{
			thePlant.Die();
			Plant topPlantAt = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ONLY_UNDER_PLANT);
			if (topPlantAt != null)
			{
				topPlantAt.Die();
			}
		}

		public SpecialGridPlacement[] GetSpecialGridPlacements(ref int theCount)
		{
			if (this.mBoard.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN)
			{
				theCount = Constants.gMushroomGridPlacement.Length;
				return Constants.gMushroomGridPlacement;
			}
			if (this.mBoard.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM)
			{
				theCount = ZenGarden.gAquariumGridPlacement.Length;
				return ZenGarden.gAquariumGridPlacement;
			}
			if (this.mBoard.mBackground == BackgroundType.BACKGROUND_GREENHOUSE)
			{
				theCount = ZenGarden.gGreenhouseGridPlacement.Length;
				return ZenGarden.gGreenhouseGridPlacement;
			}
			Debug.ASSERT(false);
			return null;
		}

		public int PixelToGridX(int theX, int theY)
		{
			int num = 0;
			SpecialGridPlacement[] specialGridPlacements = this.GetSpecialGridPlacements(ref num);
			for (int i = 0; i < num; i++)
			{
				SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
				if (theX >= specialGridPlacement.mPixelX && theX <= specialGridPlacement.mPixelX + 80 && theY >= specialGridPlacement.mPixelY && theY <= specialGridPlacement.mPixelY + 85)
				{
					return specialGridPlacement.mGridX;
				}
			}
			return -1;
		}

		public int PixelToGridY(int theX, int theY)
		{
			int num = 0;
			SpecialGridPlacement[] specialGridPlacements = this.GetSpecialGridPlacements(ref num);
			for (int i = 0; i < num; i++)
			{
				SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
				if (theX >= specialGridPlacement.mPixelX && theX <= specialGridPlacement.mPixelX + 80 && theY >= specialGridPlacement.mPixelY && theY <= specialGridPlacement.mPixelY + 85)
				{
					return specialGridPlacement.mGridY;
				}
			}
			return -1;
		}

		public int GridToPixelX(int theGridX, int theGridY)
		{
			int num = 0;
			SpecialGridPlacement[] specialGridPlacements = this.GetSpecialGridPlacements(ref num);
			for (int i = 0; i < num; i++)
			{
				SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
				if (theGridX == specialGridPlacement.mGridX && theGridY == specialGridPlacement.mGridY)
				{
					return specialGridPlacement.mPixelX;
				}
			}
			return -1;
		}

		public int GridToPixelY(int theGridX, int theGridY)
		{
			int num = 0;
			SpecialGridPlacement[] specialGridPlacements = this.GetSpecialGridPlacements(ref num);
			for (int i = 0; i < num; i++)
			{
				SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
				if (theGridX == specialGridPlacement.mGridX && theGridY == specialGridPlacement.mGridY)
				{
					return specialGridPlacement.mPixelY;
				}
			}
			return -1;
		}

		public void DrawBackdrop(Graphics g)
		{
			if (this.mGardenType == GardenType.GARDEN_AQUARIUM && (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_WHEEL_BARROW || this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WHEEELBARROW || this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE))
			{
				int num = 0;
				SpecialGridPlacement[] specialGridPlacements = this.GetSpecialGridPlacements(ref num);
				for (int i = 0; i < num; i++)
				{
					SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
					if (this.mBoard.GetTopPlantAt(specialGridPlacement.mGridX, specialGridPlacement.mGridY, PlantPriority.TOPPLANT_ZEN_TOOL_ORDER) == null)
					{
						TodCommon.TodDrawImageCelScaled(g, AtlasResources.IMAGE_PLANTSHADOW, (int)(Constants.S * (float)(specialGridPlacement.mPixelX - Constants.ZenGarden_Aquarium_ShadowOffset.X)), (int)(Constants.S * (float)(specialGridPlacement.mPixelY + Constants.ZenGarden_Aquarium_ShadowOffset.Y)), 0, 0, 1.7f, 1.7f);
					}
				}
			}
		}

		public bool MouseDownZenGarden(int x, int y, int theClickCount, HitResult theHitResult)
		{
			Challenge mChallenge = this.mBoard.mChallenge;
			if (mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
			{
				mChallenge.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			}
			mChallenge.mChallengeStateCounter = 3000;
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_STINKY && this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL)
			{
				this.WakeStinky();
			}
			else if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_GLOVE)
			{
				if (this.mBoard.CanUseGameObject(GameObjectType.OBJECT_TYPE_WHEELBARROW))
				{
					TRect zenButtonRect = this.mBoard.GetZenButtonRect(GameObjectType.OBJECT_TYPE_WHEELBARROW);
					PottedPlant pottedPlantInWheelbarrow = this.GetPottedPlantInWheelbarrow();
					if (zenButtonRect.Contains(x, y) && pottedPlantInWheelbarrow != null)
					{
						this.mBoard.ClearCursor();
						this.mBoard.mCursorObject.mType = pottedPlantInWheelbarrow.mSeedType;
						this.mBoard.mCursorObject.mImitaterType = SeedType.SEED_NONE;
						this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_PLANT_FROM_WHEEL_BARROW;
						return true;
					}
				}
			}
			else if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE)
			{
				if (this.mBoard.CanUseGameObject(GameObjectType.OBJECT_TYPE_WHEELBARROW))
				{
					TRect zenButtonRect2 = this.mBoard.GetZenButtonRect(GameObjectType.OBJECT_TYPE_WHEELBARROW);
					Plant plant = this.mBoard.mPlants[this.mBoard.mPlants.IndexOf(this.mBoard.mCursorObject.mGlovePlantID)];
					if (plant != null && zenButtonRect2.Contains(x, y) && this.GetPottedPlantInWheelbarrow() == null)
					{
						plant.mGloveGrabbed = false;
						this.MouseDownWithEmptyWheelBarrow(plant);
						this.mBoard.ClearCursor();
						return true;
					}
				}
			}
			else if (theHitResult.mObjectType != GameObjectType.OBJECT_TYPE_NONE || this.mBoard.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_NORMAL || this.mGardenType == GardenType.GARDEN_AQUARIUM)
			{
			}
			if (this.mApp.mCrazyDaveMessageIndex != -1)
			{
				this.AdvanceCrazyDaveDialog();
				return true;
			}
			return false;
		}

		public void PlantFulfillNeed(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			pottedPlant.mLastNeedFulfilledTime = this.aNow;
			pottedPlant.mPlantNeed = PottedPlantNeed.PLANTNEED_NONE;
			pottedPlant.mTimesFed = 0;
			this.mApp.PlayFoley(FoleyType.FOLEY_PRIZE);
			this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
			this.mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
			if (Plant.IsNocturnal(thePlant.mSeedType) || Plant.IsAquatic(thePlant.mSeedType))
			{
				this.mBoard.AddCoin(thePlant.mX + 10, thePlant.mY, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
				this.mBoard.AddCoin(thePlant.mX + 70, thePlant.mY, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
			}
		}

		public void PlantWatered(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			pottedPlant.mTimesFed++;
			int num = TodCommon.RandRangeInt(0, 8);
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_WATER_PLANT || this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_KEEP_WATERING)
			{
				num = 9;
			}
			pottedPlant.mLastWateredTime = this.aNow;
			pottedPlant.mLastWateredTime = pottedPlant.mLastWateredTime.Subtract(TimeSpan.FromSeconds((double)num));
			this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
			this.mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_FULL && pottedPlant.mPlantNeed == PottedPlantNeed.PLANTNEED_NONE)
			{
				pottedPlant.mPlantNeed = (PottedPlantNeed)TodCommon.RandRangeInt(3, 4);
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_WATER_PLANT)
			{
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZEN_GARDEN_KEEP_WATERING;
				this.mApp.mPlayerInfo.mZenTutorialMessage = 24;
				this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_KEEP_WATERING]", MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG, AdviceType.ADVICE_NONE);
			}
		}

		public PottedPlantNeed GetPlantsNeed(PottedPlant thePottedPlant)
		{
			bool flag = false;
			if (thePottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SPROUT)
			{
				flag = false;
			}
			else if (Plant.IsNocturnal(thePottedPlant.mSeedType) && thePottedPlant.mWhichZenGarden == GardenType.GARDEN_MAIN)
			{
				flag = true;
			}
			if (flag)
			{
				return PottedPlantNeed.PLANTNEED_NONE;
			}
			if (thePottedPlant.mWhichZenGarden == GardenType.GARDEN_WHEELBARROW)
			{
				return PottedPlantNeed.PLANTNEED_NONE;
			}
			TimeSpan timeSpan = this.aNow - thePottedPlant.mLastWateredTime;
			bool flag2 = timeSpan.TotalSeconds > 15.0;
			bool flag3 = timeSpan.TotalSeconds < 3.0;
			if (this.WasPlantFertilizedInLastHour(thePottedPlant))
			{
				return PottedPlantNeed.PLANTNEED_NONE;
			}
			if (this.WasPlantNeedFulfilledToday(thePottedPlant))
			{
				return PottedPlantNeed.PLANTNEED_NONE;
			}
			if (Plant.IsAquatic(thePottedPlant.mSeedType) && thePottedPlant.mPlantAge != PottedPlantAge.PLANTAGE_SPROUT)
			{
				if (thePottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_FULL)
				{
					if (this.PlantShouldRefreshNeed(thePottedPlant))
					{
						return PottedPlantNeed.PLANTNEED_NONE;
					}
					return thePottedPlant.mPlantNeed;
				}
				else
				{
					if (thePottedPlant.mWhichZenGarden != GardenType.GARDEN_AQUARIUM)
					{
						return PottedPlantNeed.PLANTNEED_NONE;
					}
					return PottedPlantNeed.PLANTNEED_FERTILIZER;
				}
			}
			else
			{
				if (!flag2)
				{
					return PottedPlantNeed.PLANTNEED_NONE;
				}
				if (thePottedPlant.mTimesFed < thePottedPlant.mFeedingsPerGrow)
				{
					return PottedPlantNeed.PLANTNEED_WATER;
				}
				if (flag3)
				{
					return PottedPlantNeed.PLANTNEED_NONE;
				}
				if (thePottedPlant.mPlantAge != PottedPlantAge.PLANTAGE_FULL)
				{
					return PottedPlantNeed.PLANTNEED_FERTILIZER;
				}
				if (this.PlantShouldRefreshNeed(thePottedPlant))
				{
					return PottedPlantNeed.PLANTNEED_NONE;
				}
				if (thePottedPlant.mPlantNeed != PottedPlantNeed.PLANTNEED_NONE)
				{
					return thePottedPlant.mPlantNeed;
				}
				return PottedPlantNeed.PLANTNEED_WATER;
			}
		}

		public void MouseDownWithFeedingTool(int x, int y, CursorType theCursorType)
		{
			HitResult hitResult = this.mApp.mBoard.ToolHitTest(x, y, true);
			Plant plant = null;
			if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
			{
				plant = (Plant)hitResult.mObject;
			}
			bool flag = theCursorType == CursorType.CURSOR_TYPE_WATERING_CAN && this.mApp.mPlayerInfo.mPurchases[13] > 0;
			if ((plant == null || plant.mPottedPlantIndex == -1) && !flag && theCursorType != CursorType.CURSOR_TYPE_CHOCOLATE)
			{
				this.mBoard.ClearCursor();
				return;
			}
			if (theCursorType == CursorType.CURSOR_TYPE_CHOCOLATE)
			{
				Debug.ASSERT(this.mApp.mPlayerInfo.mPurchases[26] > 1000);
				GridItem stinky = this.GetStinky();
				if (!this.IsStinkyHighOnChocolate() && stinky != null)
				{
					this.WakeStinky();
					this.mApp.AddTodParticle(stinky.mPosX + 40f, stinky.mPosY + 40f, stinky.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
					this.mApp.mPlayerInfo.mLastStinkyChocolateTime = this.aNow;
					this.mApp.mPlayerInfo.mPurchases[26]--;
					this.mApp.PlayFoley(FoleyType.FOLEY_WAKEUP);
					this.mApp.PlaySample(Resources.SOUND_MINDCONTROLLED);
				}
				if (plant != null)
				{
					this.mApp.mPlayerInfo.mPurchases[26]--;
					this.FeedChocolateToPlant(plant);
					this.mApp.PlayFoley(FoleyType.FOLEY_WAKEUP);
				}
			}
			if (plant != null || flag)
			{
				GridItem newGridItem = GridItem.GetNewGridItem();
				newGridItem.mGridItemType = GridItemType.GRIDITEM_ZEN_TOOL;
				this.mBoard.mGridItems.Add(newGridItem);
				if (plant != null)
				{
					newGridItem.mGridX = plant.mPlantCol;
					newGridItem.mGridY = plant.mRow;
					newGridItem.mPosX = (float)(plant.mX + 40);
					newGridItem.mPosY = (float)(plant.mY + 40);
				}
				newGridItem.mRenderOrder = 800000;
				if (flag)
				{
					newGridItem.mPosX = (float)x;
					newGridItem.mPosY = (float)y;
					Reanimation reanimation = this.mApp.AddReanimation((float)(x + Constants.ZenGarden_GoldenWater_Pos.X), (float)(y + Constants.ZenGarden_GoldenWater_Pos.Y), 0, ReanimationType.REANIM_ZENGARDEN_WATERINGCAN, true);
					reanimation.PlayReanim("anim_water_area", ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 8f);
					newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation);
					newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_ZEN_TOOL_GOLD_WATERING_CAN;
					this.mApp.PlayFoley(FoleyType.FOLEY_WATERING);
				}
				else if (theCursorType == CursorType.CURSOR_TYPE_WATERING_CAN && this.mApp.mPlayerInfo.mPurchases[13] != 0)
				{
					newGridItem.mPosX = (float)x;
					newGridItem.mPosY = (float)y;
					Reanimation reanimation2 = this.mApp.AddReanimation((float)x, (float)y, 0, ReanimationType.REANIM_ZENGARDEN_WATERINGCAN);
					reanimation2.PlayReanim("anim_water_area", ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 8f);
					newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation2);
					newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_ZEN_TOOL_GOLD_WATERING_CAN;
					this.mApp.PlayFoley(FoleyType.FOLEY_WATERING);
				}
				else if (theCursorType == CursorType.CURSOR_TYPE_WATERING_CAN)
				{
					Reanimation reanimation3 = this.mApp.AddReanimation((float)(plant.mX + 32), (float)plant.mY, 0, ReanimationType.REANIM_ZENGARDEN_WATERINGCAN);
					reanimation3.PlayReanim("anim_water", ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 0f);
					newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation3);
					newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_ZEN_TOOL_WATERING_CAN;
					this.mApp.PlayFoley(FoleyType.FOLEY_WATERING);
				}
				else if (theCursorType == CursorType.CURSOR_TYPE_FERTILIZER)
				{
					Reanimation reanimation4 = this.mApp.AddReanimation((float)plant.mX, (float)plant.mY, 0, ReanimationType.REANIM_ZENGARDEN_FERTILIZER);
					reanimation4.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation4);
					newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_ZEN_TOOL_FERTILIZER;
					this.mApp.PlayFoley(FoleyType.FOLEY_FERTILIZER);
					Debug.ASSERT(this.mApp.mPlayerInfo.mPurchases[14] > 1000);
					this.mApp.mPlayerInfo.mPurchases[14]--;
				}
				else if (theCursorType == CursorType.CURSOR_TYPE_BUG_SPRAY)
				{
					Reanimation reanimation5 = this.mApp.AddReanimation((float)(plant.mX + 54), (float)plant.mY, 0, ReanimationType.REANIM_ZENGARDEN_BUGSPRAY);
					reanimation5.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation5);
					newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_ZEN_TOOL_BUG_SPRAY;
					this.mApp.PlayFoley(FoleyType.FOLEY_BUGSPRAY);
					Debug.ASSERT(this.mApp.mPlayerInfo.mPurchases[15] > 1000);
					this.mApp.mPlayerInfo.mPurchases[15]--;
				}
				else if (theCursorType == CursorType.CURSOR_TYPE_PHONOGRAPH)
				{
					Reanimation reanimation6 = this.mApp.AddReanimation((float)(plant.mX + 20), (float)(plant.mY + 34), 0, ReanimationType.REANIM_ZENGARDEN_PHONOGRAPH);
					reanimation6.mAnimRate = 20f;
					reanimation6.mLoopType = ReanimLoopType.REANIM_LOOP;
					newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation6);
					newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_ZEN_TOOL_PHONOGRAPH;
					this.mApp.PlayFoley(FoleyType.FOLEY_PHONOGRAPH);
				}
			}
			this.mBoard.ClearCursor();
		}

		public void DrawPlantOverlay(Graphics g, Plant thePlant)
		{
			if (thePlant.mPottedPlantIndex == -1)
			{
				return;
			}
			PottedPlant thePottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			PottedPlantNeed plantsNeed = this.mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
			if (plantsNeed == PottedPlantNeed.PLANTNEED_NONE)
			{
				return;
			}
			g.DrawImage(AtlasResources.IMAGE_PLANTSPEECHBUBBLE, Constants.ZenGarden_PlantSpeechBubble_Pos.X, Constants.ZenGarden_PlantSpeechBubble_Pos.Y);
			switch (plantsNeed)
			{
			case PottedPlantNeed.PLANTNEED_WATER:
				g.DrawImage(AtlasResources.IMAGE_WATERDROP, Constants.ZenGarden_WaterDrop_Pos.X, Constants.ZenGarden_WaterDrop_Pos.Y);
				return;
			case PottedPlantNeed.PLANTNEED_FERTILIZER:
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG3, (float)Constants.ZenGarden_Fertiliser_Pos.X, (float)Constants.ZenGarden_Fertiliser_Pos.Y, 0.5f, 0.5f);
				return;
			case PottedPlantNeed.PLANTNEED_BUGSPRAY:
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE, (float)Constants.ZenGarden_BugSpray_Pos.X, (float)Constants.ZenGarden_BugSpray_Pos.Y, 0.5f, 0.5f);
				return;
			case PottedPlantNeed.PLANTNEED_PHONOGRAPH:
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_PHONOGRAPH, (float)Constants.ZenGarden_Phonograph_Pos.X, (float)Constants.ZenGarden_Phonograph_Pos.Y, 0.5f, 0.5f);
				return;
			default:
				return;
			}
		}

		public PottedPlant PottedPlantFromIndex(int thePottedPlantIndex)
		{
			Debug.ASSERT(thePottedPlantIndex >= 0 && thePottedPlantIndex < this.mApp.mPlayerInfo.mNumPottedPlants);
			return this.mApp.mPlayerInfo.mPottedPlant[thePottedPlantIndex];
		}

		public bool WasPlantNeedFulfilledToday(PottedPlant thePottedPlant)
		{
			TimeSpan timeSpan = this.aNow - thePottedPlant.mLastNeedFulfilledTime;
			double totalSeconds = timeSpan.TotalSeconds;
			return timeSpan.TotalDays < 1.0;
		}

		public void PottedPlantUpdate(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			if (pottedPlant.mLastWateredTime > this.aNow || pottedPlant.mLastNeedFulfilledTime > this.aNow || pottedPlant.mLastFertilizedTime > this.aNow || pottedPlant.mLastChocolateTime > this.aNow)
			{
				this.ResetPlantTimers(pottedPlant);
			}
			if (thePlant.mIsAsleep)
			{
				return;
			}
			if (thePlant.mStateCountdown > 0)
			{
				thePlant.mStateCountdown--;
			}
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_FULL && this.WasPlantNeedFulfilledToday(pottedPlant))
			{
				this.PlantUpdateProduction(thePlant);
			}
			this.UpdatePlantEffectState(thePlant);
		}

		public void AddHappyEffect(Plant thePlant)
		{
			Plant topPlantAt = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ONLY_UNDER_PLANT);
			if (topPlantAt == null)
			{
				thePlant.AddAttachedParticle(thePlant.mX + 40, thePlant.mY + 60, thePlant.mRenderOrder - 1, ParticleEffect.PARTICLE_POTTED_ZEN_GLOW);
				return;
			}
			if (Plant.IsAquatic(thePlant.mSeedType))
			{
				topPlantAt.AddAttachedParticle(topPlantAt.mX + 40, topPlantAt.mY + 61, topPlantAt.mRenderOrder - 1, ParticleEffect.PARTICLE_POTTED_WATER_PLANT_GLOW);
				return;
			}
			topPlantAt.AddAttachedParticle(topPlantAt.mX + 40, topPlantAt.mY + 63, topPlantAt.mRenderOrder - 1, ParticleEffect.PARTICLE_POTTED_PLANT_GLOW);
		}

		public void RemoveHappyEffect(Plant thePlant)
		{
			Plant topPlantAt = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ONLY_UNDER_PLANT);
			TodParticleSystem todParticleSystem;
			if (topPlantAt != null)
			{
				todParticleSystem = this.mApp.ParticleTryToGet(topPlantAt.mParticleID);
			}
			else
			{
				todParticleSystem = this.mApp.ParticleTryToGet(thePlant.mParticleID);
			}
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
			}
		}

		public void PlantUpdateProduction(Plant thePlant)
		{
			thePlant.mLaunchCounter--;
			this.SetPlantAnimSpeed(thePlant);
			PottedPlant thePottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			if (this.PlantHighOnChocolate(thePottedPlant))
			{
				thePlant.mLaunchCounter--;
			}
			if (thePlant.mLaunchCounter <= 0)
			{
				this.PlantSetLaunchCounter(thePlant);
				this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
				int num = RandomNumbers.NextNumber(1000);
				int theTimeAge = this.PlantGetMinutesSinceHappy(thePlant);
				num += TodCommon.TodAnimateCurve(5, 30, theTimeAge, 0, 80, TodCurves.CURVE_LINEAR);
				CoinType theCoinType = CoinType.COIN_SILVER;
				if (num < 100)
				{
					theCoinType = CoinType.COIN_GOLD;
				}
				this.mBoard.AddCoin(thePlant.mX, thePlant.mY, theCoinType, CoinMotion.COIN_MOTION_COIN);
			}
		}

		public bool CanDropPottedPlantLoot()
		{
			return this.mApp.HasFinishedAdventure() && !this.mApp.mZenGarden.IsZenGardenFull(true);
		}

		public void ShowTutorialArrowOnWateringCan()
		{
			TRect zenButtonRect = this.mBoard.GetZenButtonRect(GameObjectType.OBJECT_TYPE_WATERING_CAN);
			this.mBoard.TutorialArrowShow(zenButtonRect.mX + Constants.ZenGarden_TutorialArrow_Offset, (int)((float)zenButtonRect.mY + Constants.S * 10f));
			this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_PICK_UP_WATER]", MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG, AdviceType.ADVICE_NONE);
			this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZEN_GARDEN_PICKUP_WATER;
			this.mApp.mPlayerInfo.mIsInZenTutorial = true;
			this.mApp.mPlayerInfo.mZenTutorialMessage = 22;
		}

		public bool PlantsNeedWater()
		{
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant thePottedPlant = this.PottedPlantFromIndex(i);
				PottedPlantNeed plantsNeed = this.mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
				if (plantsNeed == PottedPlantNeed.PLANTNEED_WATER)
				{
					return true;
				}
			}
			return false;
		}

		public void ZenGardenStart()
		{
		}

		public void UpdatePlantEffectState(Plant thePlant)
		{
			PottedPlant thePottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			PlantState mState = thePlant.mState;
			PottedPlantNeed plantsNeed = this.GetPlantsNeed(thePottedPlant);
			if (plantsNeed == PottedPlantNeed.PLANTNEED_WATER)
			{
				thePlant.mState = PlantState.STATE_NOTREADY;
			}
			else if (plantsNeed == PottedPlantNeed.PLANTNEED_NONE)
			{
				if (this.WasPlantNeedFulfilledToday(thePottedPlant))
				{
					thePlant.mState = PlantState.STATE_ZEN_GARDEN_HAPPY;
				}
				else if (thePlant.mIsAsleep)
				{
					thePlant.mState = PlantState.STATE_NOTREADY;
				}
				else
				{
					thePlant.mState = PlantState.STATE_ZEN_GARDEN_WATERED;
				}
			}
			else
			{
				thePlant.mState = PlantState.STATE_ZEN_GARDEN_NEEDY;
			}
			if (mState == thePlant.mState)
			{
				return;
			}
			Plant topPlantAt = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ONLY_UNDER_PLANT);
			if (topPlantAt != null && !Plant.IsAquatic(thePlant.mSeedType))
			{
				Reanimation reanimation = this.mApp.ReanimationGet(topPlantAt.mBodyReanimID);
				if (thePlant.mState == PlantState.STATE_ZEN_GARDEN_WATERED || thePlant.mState == PlantState.STATE_ZEN_GARDEN_NEEDY || thePlant.mState == PlantState.STATE_ZEN_GARDEN_HAPPY)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_pot_top, AtlasResources.IMAGE_REANIM_POT_TOP_DARK);
				}
				else
				{
					Image theImage = null;
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_pot_top, theImage);
				}
			}
			if (mState == PlantState.STATE_ZEN_GARDEN_HAPPY)
			{
				this.RemoveHappyEffect(thePlant);
			}
			if (thePlant.mState == PlantState.STATE_ZEN_GARDEN_HAPPY)
			{
				thePlant.SetSleeping(false);
				this.AddHappyEffect(thePlant);
				return;
			}
			if (Plant.IsNocturnal(thePlant.mSeedType) && !this.mBoard.StageIsNight())
			{
				thePlant.SetSleeping(true);
			}
		}

		public void ZenToolUpdate(GridItem theZenTool)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(theZenTool.mGridItemReanimID);
			if (reanimation == null)
			{
				return;
			}
			int num = 1;
			if (theZenTool.mGridItemState == GridItemState.GRIDITEM_STATE_ZEN_TOOL_PHONOGRAPH)
			{
				num = 2;
			}
			if (reanimation.mLoopCount >= num)
			{
				this.DoFeedingTool((int)theZenTool.mPosX, (int)theZenTool.mPosY, theZenTool.mGridItemState);
				theZenTool.GridItemDie();
			}
		}

		public void DoFeedingTool(int x, int y, GridItemState theToolType)
		{
			if (theToolType == GridItemState.GRIDITEM_STATE_ZEN_TOOL_GOLD_WATERING_CAN)
			{
				int count = this.mBoard.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = this.mBoard.mPlants[i];
					if (!plant.mDead && this.mBoard.IsPlantInGoldWateringCanRange(x, y, plant))
					{
						PottedPlant thePottedPlant = this.PottedPlantFromIndex(plant.mPottedPlantIndex);
						PottedPlantNeed plantsNeed = this.GetPlantsNeed(thePottedPlant);
						if (plantsNeed == PottedPlantNeed.PLANTNEED_WATER)
						{
							this.PlantWatered(plant);
						}
					}
				}
				return;
			}
			int theGridX = this.PixelToGridX(x, y);
			int theGridY = this.PixelToGridY(x, y);
			Plant topPlantAt = this.mBoard.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_ZEN_TOOL_ORDER);
			if (topPlantAt != null)
			{
				PottedPlant thePottedPlant2 = this.PottedPlantFromIndex(topPlantAt.mPottedPlantIndex);
				PottedPlantNeed plantsNeed2 = this.GetPlantsNeed(thePottedPlant2);
				if (plantsNeed2 == PottedPlantNeed.PLANTNEED_WATER && theToolType == GridItemState.GRIDITEM_STATE_ZEN_TOOL_WATERING_CAN)
				{
					this.PlantWatered(topPlantAt);
				}
				else if (plantsNeed2 == PottedPlantNeed.PLANTNEED_FERTILIZER && theToolType == GridItemState.GRIDITEM_STATE_ZEN_TOOL_FERTILIZER)
				{
					this.PlantFertilized(topPlantAt);
				}
				else if (plantsNeed2 == PottedPlantNeed.PLANTNEED_BUGSPRAY && theToolType == GridItemState.GRIDITEM_STATE_ZEN_TOOL_BUG_SPRAY)
				{
					this.PlantFulfillNeed(topPlantAt);
				}
				else if (plantsNeed2 == PottedPlantNeed.PLANTNEED_PHONOGRAPH && theToolType == GridItemState.GRIDITEM_STATE_ZEN_TOOL_PHONOGRAPH)
				{
					this.PlantFulfillNeed(topPlantAt);
				}
				if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_FERTILIZE_PLANTS && theToolType == GridItemState.GRIDITEM_STATE_ZEN_TOOL_FERTILIZER)
				{
					if (this.AllPlantsHaveBeenFertilized())
					{
						this.mApp.mBoard.mTutorialState = TutorialState.TUTORIAL_ZEN_GARDEN_COMPLETED;
						this.mApp.mPlayerInfo.mZenTutorialMessage = 27;
						this.mApp.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_CONTINUE_ADVENTURE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NONE);
						this.mBoard.mMenuButton.mDisabled = false;
						this.mBoard.mMenuButton.mBtnNoDraw = false;
						return;
					}
					if (this.mApp.mPlayerInfo.mPurchases[14] == 1000)
					{
						this.mApp.mPlayerInfo.mPurchases[14] = 1005;
						this.mApp.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_NEED_MORE_FERTILIZER]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NONE);
					}
				}
			}
		}

		public void AddStinky()
		{
			if (!this.HasPurchasedStinky())
			{
				return;
			}
			if (this.mGardenType != GardenType.GARDEN_MAIN)
			{
				return;
			}
			if (!this.mApp.mPlayerInfo.mHasSeenStinky)
			{
				this.mApp.mPlayerInfo.mHasSeenStinky = true;
				this.mApp.mPlayerInfo.mPurchases[20] = this.GetStinkyTime();
			}
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_STINKY;
			newGridItem.mPosX = (float)this.mApp.mPlayerInfo.mStinkyPosX;
			newGridItem.mPosY = (float)this.mApp.mPlayerInfo.mStinkyPosY;
			newGridItem.mGoalX = newGridItem.mPosX;
			newGridItem.mGoalY = newGridItem.mPosY;
			this.mBoard.mGridItems.Add(newGridItem);
			Reanimation reanimation = this.mApp.AddReanimation(newGridItem.mPosX * Constants.S, newGridItem.mPosY * Constants.S, 0, ReanimationType.REANIM_STINKY);
			reanimation.OverrideScale(0.8f, 0.8f);
			newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation);
			if (this.mApp.mPlayerInfo.mStinkyPosX == 0)
			{
				this.StinkyPickGoal(newGridItem);
				newGridItem.mPosX = newGridItem.mGoalX;
				newGridItem.mPosY = newGridItem.mGoalY;
			}
			if (this.ShouldStinkyBeAwake())
			{
				reanimation.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.REANIM_LOOP, 0, 6f);
				newGridItem.mGridItemState = GridItemState.GRIDITEM_STINKY_WALKING_LEFT;
			}
			else
			{
				newGridItem.mPosY = (float)Constants.STINKY_SLEEP_POS_Y;
				this.StinkyFinishFallingAsleep(newGridItem, 0);
			}
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, 0, (int)newGridItem.mPosY - 30);
			reanimation.SetPosition(newGridItem.mPosX * Constants.S, newGridItem.mPosY * Constants.S);
		}

		private int GetStinkyTime()
		{
			return (int)(TimeSpan.FromTicks(this.aNow.Ticks).TotalSeconds - this.STINKY_BASE_TIME);
		}

		public void StinkyUpdate(GridItem theStinky)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
			if (this.mApp.mPlayerInfo.mLastStinkyChocolateTime > this.aNow || this.mApp.mPlayerInfo.mPurchases[20] > this.GetStinkyTime())
			{
				this.ResetStinkyTimers();
			}
			bool flag = this.IsStinkyHighOnChocolate();
			this.UpdateStinkyMotionTrail(theStinky, flag);
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_FALLING_ASLEEP)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.StinkyFinishFallingAsleep(theStinky, 20);
				}
				return;
			}
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_SLEEPING)
			{
				ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName("shell");
				Reanimation reanimation2 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
				Debug.ASSERT(reanimation2 != null);
				if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE)
				{
					reanimation2.AssignRenderGroupToPrefix("z", -1);
				}
				else
				{
					reanimation2.AssignRenderGroupToPrefix("z", 0);
				}
				if (this.ShouldStinkyBeAwake())
				{
					this.StinkyWakeUp(theStinky);
				}
				return;
			}
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WAKING_UP)
			{
				if (reanimation.mLoopCount > 0)
				{
					theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_WALKING_LEFT;
					reanimation.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.REANIM_LOOP, 10, 6f);
					this.StinkyPickGoal(theStinky);
				}
				return;
			}
			if (!this.ShouldStinkyBeAwake())
			{
				if (theStinky.mPosY < (float)Constants.STINKY_SLEEP_POS_Y)
				{
					if (theStinky.mGoalY != (float)Constants.STINKY_SLEEP_POS_Y)
					{
						theStinky.mGoalY = (float)Constants.STINKY_SLEEP_POS_Y + 10f;
					}
				}
				else
				{
					if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT)
					{
						this.StinkyStartFallingAsleep(theStinky);
						return;
					}
					if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
					{
						Reanimation reanimation3 = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
						theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_TURNING_LEFT;
						reanimation3.PlayReanim("turn", ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 6f);
						theStinky.mMotionTrailCount = 0;
						theStinky.mGoalX = theStinky.mPosX;
						theStinky.mGoalY = theStinky.mPosY;
						return;
					}
				}
			}
			if (theStinky.mGridItemCounter > 0)
			{
				theStinky.mGridItemCounter--;
			}
			SexyVector2 lhs = new SexyVector2(theStinky.mPosX, theStinky.mPosY);
			SexyVector2 rhs = default(SexyVector2);
			Coin coin = null;
			while (this.mBoard.IterateCoins(ref coin))
			{
				if (!coin.mIsBeingCollected)
				{
					rhs.x = coin.mPosX;
					rhs.y = coin.mPosY + 30f;
					float num = (lhs - rhs).Magnitude();
					if (num < 20f)
					{
						coin.PlayCollectSound();
						coin.Collect();
					}
				}
			}
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
			{
				if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE && !this.IsStinkyHighOnChocolate())
				{
					if (!reanimation.IsAnimPlaying("anim_idle"))
					{
						reanimation.PlayReanim("anim_idle", ReanimLoopType.REANIM_LOOP, 10, 6f);
					}
				}
				else if (!reanimation.IsAnimPlaying(Reanimation.ReanimTrackId_anim_crawl))
				{
					reanimation.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.REANIM_LOOP, 10, 6f);
				}
			}
			float num2 = theStinky.mPosX - theStinky.mGoalX;
			float num3 = theStinky.mPosY - theStinky.mGoalY;
			float num4 = 0.5f;
			float num5 = reanimation.GetTrackVelocity(Reanimation.ReanimTrackId__ground) * 5f;
			if (flag)
			{
				num4 = 1f;
				num5 = Math.Max(num5, 0.5f);
			}
			else if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE)
			{
				num4 = 0f;
				num5 = 0f;
			}
			num4 *= TodCommon.TodAnimateCurveFloatTime(20f, 5f, Math.Abs(num3), 1f, 0.2f, TodCurves.CURVE_LINEAR);
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT)
			{
				theStinky.mPosX -= num5;
				if (theStinky.mPosX < theStinky.mGoalX)
				{
					theStinky.mPosX = theStinky.mGoalX;
				}
			}
			else if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
			{
				theStinky.mPosX += num5;
				if (theStinky.mPosX > theStinky.mGoalX)
				{
					theStinky.mPosX = theStinky.mGoalX;
				}
			}
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
			{
				if (Math.Abs(num3) < num4)
				{
					theStinky.mPosY = theStinky.mGoalY;
				}
				else if (num3 > 0f)
				{
					theStinky.mPosY -= num4;
				}
				else
				{
					theStinky.mPosY += num4;
				}
				if (Math.Abs(num2) < 5f && Math.Abs(num3) < 5f)
				{
					this.StinkyPickGoal(theStinky);
				}
				else if (theStinky.mGridItemCounter == 0)
				{
					this.StinkyPickGoal(theStinky);
				}
			}
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_LEFT)
			{
				if (reanimation.mLoopCount > 0)
				{
					theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_WALKING_LEFT;
					reanimation.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.REANIM_LOOP, 10, 6f);
				}
			}
			else if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_RIGHT && reanimation.mLoopCount > 0)
			{
				theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_WALKING_RIGHT;
				reanimation.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.REANIM_LOOP, 10, 6f);
			}
			this.StinkyAnimRateUpdate(theStinky);
			if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_LEFT)
			{
				reanimation.OverrideScale(-0.8f, 0.8f);
				reanimation.SetPosition((theStinky.mPosX + 69f) * Constants.S, theStinky.mPosY * Constants.S);
			}
			else
			{
				reanimation.OverrideScale(0.8f, 0.8f);
				reanimation.SetPosition(theStinky.mPosX * Constants.S, theStinky.mPosY * Constants.S);
			}
			theStinky.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, 0, (int)theStinky.mPosY - 30);
		}

		public void OpenStore()
		{
			this.LeaveGarden();
			StoreScreen storeScreen = this.mApp.ShowStoreScreen(this);
			storeScreen.SetupBackButtonForZenGarden();
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_VISIT_STORE)
			{
				storeScreen.SetupForIntro(2600);
				this.mApp.mPlayerInfo.mPurchases[14] = 1005;
			}
			storeScreen.mBackButton.SetLabel("[STORE_BACK_TO_GAME]");
			storeScreen.mPage = StorePage.STORE_PAGE_ZEN1;
		}

		public GridItem GetStinky()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_STINKY)
				{
					return gridItem;
				}
			}
			return null;
		}

		public void StinkyPickGoal(GridItem theStinky)
		{
			float num = TodCommon.Distance2D(theStinky.mGoalX, theStinky.mGoalY, theStinky.mPosX, theStinky.mPosY);
			Coin coin = null;
			float num2 = 0f;
			Coin coin2 = null;
			while (this.mBoard.IterateCoins(ref coin2))
			{
				if (!coin2.mIsBeingCollected && coin2.mPosY == (float)coin2.mGroundY)
				{
					float num3 = TodCommon.Distance2D(coin2.mPosX, coin2.mPosY + 30f, theStinky.mPosX, theStinky.mPosY);
					float num4 = num3;
					if (coin2.mType == CoinType.COIN_GOLD)
					{
						num4 -= 40f;
					}
					else if (coin2.mType == CoinType.COIN_DIAMOND)
					{
						num4 -= 80f;
					}
					float num5 = TodCommon.Distance2D(coin2.mPosX, coin2.mPosY + 30f, theStinky.mGoalX, theStinky.mGoalY);
					if (num5 < 5f)
					{
						num4 -= 20f;
					}
					if (num5 < 5f)
					{
						num4 += (float)TodCommon.TodAnimateCurve(3000, 6000, coin2.mDisappearCounter, 0, -40, TodCurves.CURVE_LINEAR);
					}
					if (coin == null || num4 < num2)
					{
						coin = coin2;
						num2 = num4;
					}
				}
			}
			if (coin != null)
			{
				theStinky.mGoalX = coin.mPosX;
				theStinky.mGoalY = coin.mPosY + 30f;
			}
			else
			{
				if (num > 10f)
				{
					return;
				}
				TodWeightedGridArray[] array = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
				}
				int num6 = 0;
				int num7 = 0;
				SpecialGridPlacement[] specialGridPlacements = this.GetSpecialGridPlacements(ref num7);
				Debug.ASSERT(num7 < Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY);
				for (int j = 0; j < num7; j++)
				{
					SpecialGridPlacement specialGridPlacement = specialGridPlacements[j];
					Plant topPlantAt = this.mBoard.GetTopPlantAt(specialGridPlacement.mGridX, specialGridPlacement.mGridY, PlantPriority.TOPPLANT_ANY);
					array[num6].mX = specialGridPlacement.mPixelX + 15;
					array[num6].mY = specialGridPlacement.mPixelY + 80;
					if (topPlantAt != null)
					{
						array[num6].mWeight = 2000;
						array[num6].mWeight -= (int)Math.Abs((float)array[num6].mY - theStinky.mPosY);
					}
					else
					{
						array[num6].mWeight = 1;
					}
					num6++;
				}
				TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array, num6);
				theStinky.mGoalX = (float)todWeightedGridArray.mX;
				theStinky.mGoalY = (float)todWeightedGridArray.mY;
			}
			theStinky.mGridItemCounter = 100;
			if (theStinky.mGoalX < theStinky.mPosX && theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
				theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_TURNING_LEFT;
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_turn, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 6f);
				theStinky.mMotionTrailCount = 0;
				return;
			}
			if (theStinky.mGoalX > theStinky.mPosX && theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
				theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_TURNING_RIGHT;
				reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_turn, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 6f);
				theStinky.mMotionTrailCount = 0;
			}
		}

		public bool PlantShouldRefreshNeed(PottedPlant thePottedPlant)
		{
			TimeSpan timeSpan = this.aNow - thePottedPlant.mLastWateredTime;
			return timeSpan.TotalSeconds >= 3600.0 && timeSpan.TotalDays >= 1.0;
		}

		public void PlantFertilized(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			pottedPlant.mLastFertilizedTime = this.aNow;
			pottedPlant.mPlantNeed = PottedPlantNeed.PLANTNEED_NONE;
			pottedPlant.mTimesFed = 0;
			pottedPlant.mPlantAge++;
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SMALL)
			{
				this.RemovePottedPlant(thePlant);
				this.PlacePottedPlant(thePlant.mPottedPlantIndex);
				this.mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
			}
			else
			{
				thePlant.mStateCountdown = 100;
				this.mApp.PlayFoley(FoleyType.FOLEY_PLANTGROW);
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SMALL)
			{
				this.mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_MEDIUM)
			{
				this.mBoard.AddCoin(thePlant.mX + 30, thePlant.mY, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
				this.mBoard.AddCoin(thePlant.mX + 50, thePlant.mY, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_FULL)
			{
				if (pottedPlant.mSeedType == SeedType.SEED_MARIGOLD)
				{
					this.mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
					return;
				}
				this.mBoard.AddCoin(thePlant.mX + 10, thePlant.mY, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				this.mBoard.AddCoin(thePlant.mX + 70, thePlant.mY, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
			}
		}

		public bool WasPlantFertilizedInLastHour(PottedPlant thePottedPlant)
		{
			return (this.aNow - thePottedPlant.mLastFertilizedTime).TotalSeconds < 3600.0;
		}

		public void SetupForZenTutorial()
		{
			this.mBoard.mMenuButton.SetLabel("[CONTINUE_BUTTON]");
			this.mBoard.mStoreButton.mDisabled = true;
			this.mBoard.mStoreButton.mBtnNoDraw = true;
			this.mBoard.mMenuButton.mDisabled = true;
			this.mBoard.mMenuButton.mBtnNoDraw = true;
			this.mIsTutorial = true;
			if (this.mApp.mPlayerInfo.mIsDaveTalkingZenTutorial)
			{
				this.mApp.CrazyDaveEnter();
				this.mApp.CrazyDaveTalkIndex(this.mApp.mPlayerInfo.mZenTutorialMessage);
				return;
			}
			if (!this.mApp.mPlayerInfo.mIsInZenTutorial)
			{
				this.mApp.mPlayerInfo.mIsDaveTalkingZenTutorial = true;
				this.mApp.mPlayerInfo.mZenTutorialMessage = 2050;
				this.mApp.CrazyDaveEnter();
				this.mApp.CrazyDaveTalkIndex(2050);
				return;
			}
			this.mBoard.mTutorialState = (TutorialState)this.mApp.mPlayerInfo.mZenTutorialMessage;
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_PICKUP_WATER)
			{
				this.ShowTutorialArrowOnWateringCan();
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_KEEP_WATERING)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_KEEP_WATERING]", MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG, AdviceType.ADVICE_NONE);
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_VISIT_STORE)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_VISIT_STORE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_LONG, AdviceType.ADVICE_NONE);
				this.mBoard.mStoreButton.mDisabled = false;
				this.mBoard.mStoreButton.mBtnNoDraw = false;
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_FERTILIZE_PLANTS)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_FERTILIZE]", MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG, AdviceType.ADVICE_NONE);
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_COMPLETED)
			{
				this.mIsTutorial = false;
				this.mApp.mPlayerInfo.mZenGardenTutorialComplete = true;
				this.mApp.mPlayerInfo.mIsInZenTutorial = false;
			}
		}

		public bool HasPurchasedStinky()
		{
			return this.mApp.mPlayerInfo.mPurchases[20] != 0;
		}

		public int CountPlantsNeedingFertilizer()
		{
			int num = 0;
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant thePottedPlant = this.PottedPlantFromIndex(i);
				PottedPlantNeed plantsNeed = this.mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
				if (plantsNeed == PottedPlantNeed.PLANTNEED_FERTILIZER)
				{
					num++;
				}
			}
			return num;
		}

		public bool AllPlantsHaveBeenFertilized()
		{
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant pottedPlant = this.PottedPlantFromIndex(i);
				if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SPROUT)
				{
					return false;
				}
			}
			return true;
		}

		public void WakeStinky()
		{
			this.mApp.mPlayerInfo.mPurchases[20] = this.GetStinkyTime();
			this.mApp.PlaySample(Resources.SOUND_TAP);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_STINKY_SLEEPING);
			GlobalStaticVars.gLawnApp.mPlayerInfo.mHasWokenStinky = true;
		}

		public bool ShouldStinkyBeAwake()
		{
			if (this.IsStinkyHighOnChocolate())
			{
				return true;
			}
			int num = (int)(TimeSpan.FromTicks(this.aNow.Ticks).TotalSeconds - this.STINKY_BASE_TIME - (double)this.mApp.mPlayerInfo.mPurchases[20]);
			int num2 = 180;
			return num <= num2;
		}

		public bool IsStinkySleeping()
		{
			GridItem stinky = this.GetStinky();
			return stinky != null && stinky.mGridItemState == GridItemState.GRIDITEM_STINKY_SLEEPING;
		}

		public SeedType PickRandomSeedType()
		{
			SeedType[] array = new SeedType[40];
			int num = 0;
			for (int i = 0; i < 40; i++)
			{
				SeedType seedType = (SeedType)i;
				if (seedType != SeedType.SEED_MARIGOLD && seedType != SeedType.SEED_FLOWERPOT)
				{
					array[num] = seedType;
					num++;
				}
			}
			int num2 = RandomNumbers.NextNumber(num);
			SeedType seedType2 = array[num2];
			return array[num2];
		}

		public void StinkyWakeUp(GridItem theStinky)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_out, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 6f);
			theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_WAKING_UP;
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_shell);
			Reanimation reanimation2 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
			reanimation2.ReanimationDie();
			GlobalStaticVars.gLawnApp.mPlayerInfo.mHasWokenStinky = true;
		}

		public void StinkyStartFallingAsleep(GridItem theStinky)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_in, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 6f);
			theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_FALLING_ASLEEP;
		}

		public void StinkyFinishFallingAsleep(GridItem theStinky, byte theBlendTime)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_out, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, theBlendTime, 0f);
			reanimation.mAnimRate = 0f;
			Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_SLEEPING);
			reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation2.mAnimRate = 3f;
			int num = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_shell);
			ReanimatorTrackInstance reanimatorTrackInstance = reanimation.mTrackInstances[num];
			GlobalMembersAttachment.AttachReanim(ref reanimatorTrackInstance.mAttachmentID, reanimation2, 34f * Constants.S, 39f * Constants.S);
			theStinky.mGridItemState = GridItemState.GRIDITEM_STINKY_SLEEPING;
			if (!GlobalStaticVars.gLawnApp.mPlayerInfo.mHasWokenStinky)
			{
				this.mApp.mBoard.DisplayAdvice("[ADVICE_STINKY_SLEEPING]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_STINKY_SLEEPING);
			}
		}

		public void AdvanceCrazyDaveDialog()
		{
			if (this.mApp.mCrazyDaveMessageIndex == -1 || this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null || this.mApp.GetDialog(Dialogs.DIALOG_ZEN_SELL) != null)
			{
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 2053 || this.mApp.mCrazyDaveMessageIndex == 2063)
			{
				this.mApp.mPlayerInfo.mIsDaveTalkingZenTutorial = false;
				this.ShowTutorialArrowOnWateringCan();
			}
			if (!this.mApp.AdvanceCrazyDaveText())
			{
				this.mApp.CrazyDaveLeave();
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex != 2054)
			{
				this.mApp.mPlayerInfo.mZenTutorialMessage = this.mApp.mCrazyDaveMessageIndex;
			}
			if ((this.mApp.mCrazyDaveMessageIndex == 2052 || this.mApp.mCrazyDaveMessageIndex == 2062) && this.mApp.mPlayerInfo.mNumPottedPlants == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					PottedPlant pottedPlant = new PottedPlant();
					pottedPlant.InitializePottedPlant(SeedType.SEED_MARIGOLD);
					pottedPlant.mDrawVariation = (DrawVariation)TodCommon.RandRangeInt(2, 12);
					pottedPlant.mFeedingsPerGrow = 3;
					this.mApp.mZenGarden.AddPottedPlant(pottedPlant);
				}
			}
		}

		public void LeaveGarden()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_ZEN_TOOL)
				{
					this.DoFeedingTool((int)gridItem.mPosX, (int)gridItem.mPosY, gridItem.mGridItemState);
					gridItem.GridItemDie();
				}
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_STINKY)
				{
					this.mApp.mPlayerInfo.mStinkyPosX = (int)gridItem.mPosX;
					this.mApp.mPlayerInfo.mStinkyPosY = (int)gridItem.mPosY;
					gridItem.GridItemDie();
				}
			}
			Coin coin = null;
			while (this.mBoard.IterateCoins(ref coin))
			{
				if (coin.mIsBeingCollected)
				{
					coin.ScoreCoin();
				}
				else
				{
					coin.Die();
				}
			}
		}

		public bool CanDropChocolate()
		{
			return this.HasPurchasedStinky() && this.mApp.mPlayerInfo.mPurchases[26] - 1000 < 10;
		}

		public void FeedChocolateToPlant(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			pottedPlant.mLastChocolateTime = this.aNow;
			thePlant.mLaunchCounter = 200;
			this.mApp.AddTodParticle((float)thePlant.mX + 40f, (float)thePlant.mY + 40f, thePlant.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
		}

		public bool PlantHighOnChocolate(PottedPlant thePottedPlant)
		{
			return (this.aNow - thePottedPlant.mLastChocolateTime).TotalSeconds < 300.0;
		}

		public bool PlantCanHaveChocolate(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			return pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_FULL && this.WasPlantNeedFulfilledToday(pottedPlant) && !this.PlantHighOnChocolate(pottedPlant);
		}

		public void SetPlantAnimSpeed(Plant thePlant)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(thePlant.mBodyReanimID);
			PottedPlant thePottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			bool flag = this.PlantHighOnChocolate(thePottedPlant);
			bool flag2 = reanimation.mAnimRate >= 25f;
			if (flag2 == flag)
			{
				return;
			}
			float num;
			if (thePlant.mSeedType == SeedType.SEED_PEASHOOTER || thePlant.mSeedType == SeedType.SEED_SNOWPEA || thePlant.mSeedType == SeedType.SEED_REPEATER || thePlant.mSeedType == SeedType.SEED_LEFTPEATER || thePlant.mSeedType == SeedType.SEED_GATLINGPEA || thePlant.mSeedType == SeedType.SEED_SPLITPEA || thePlant.mSeedType == SeedType.SEED_THREEPEATER || thePlant.mSeedType == SeedType.SEED_MARIGOLD)
			{
				num = TodCommon.RandRangeFloat(15f, 20f);
			}
			else if (thePlant.mSeedType == SeedType.SEED_POTATOMINE)
			{
				num = 12f;
			}
			else
			{
				num = TodCommon.RandRangeFloat(10f, 15f);
			}
			if (flag)
			{
				num *= 2f;
				num = Math.Max(25f, num);
			}
			reanimation.mAnimRate = num;
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID);
			Reanimation reanimation3 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID2);
			Reanimation reanimation4 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID3);
			if (reanimation2 != null)
			{
				reanimation2.mAnimRate = reanimation.mAnimRate;
				reanimation2.mAnimTime = reanimation.mAnimTime;
			}
			if (reanimation3 != null)
			{
				reanimation3.mAnimRate = reanimation.mAnimRate;
				reanimation3.mAnimTime = reanimation.mAnimTime;
			}
			if (reanimation4 != null)
			{
				reanimation4.mAnimRate = reanimation.mAnimRate;
				reanimation4.mAnimTime = reanimation.mAnimTime;
			}
		}

		public void UpdateStinkyMotionTrail(GridItem theStinky, bool theStinkyHighOnChocolate)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
			if (!theStinkyHighOnChocolate)
			{
				theStinky.mMotionTrailCount = 0;
				return;
			}
			if (theStinky.mGridItemState != GridItemState.GRIDITEM_STINKY_WALKING_RIGHT && theStinky.mGridItemState != GridItemState.GRIDITEM_STINKY_WALKING_LEFT)
			{
				theStinky.mMotionTrailCount = 0;
				return;
			}
			if (theStinky.mMotionTrailCount == 12)
			{
				theStinky.mMotionTrailCount--;
			}
			if (theStinky.mMotionTrailCount > 0)
			{
				Array.Copy(theStinky.mMotionTrailFrames, 1, theStinky.mMotionTrailFrames, 0, theStinky.mMotionTrailCount);
			}
			theStinky.mMotionTrailFrames[0].mPosX = theStinky.mPosX;
			theStinky.mMotionTrailFrames[0].mPosY = theStinky.mPosY;
			theStinky.mMotionTrailFrames[0].mAnimTime = reanimation.mAnimTime;
			theStinky.mMotionTrailCount++;
		}

		public void ResetPlantTimers(PottedPlant thePottedPlant)
		{
			thePottedPlant.mLastWateredTime = default(DateTime);
			thePottedPlant.mLastNeedFulfilledTime = default(DateTime);
			thePottedPlant.mLastFertilizedTime = default(DateTime);
			thePottedPlant.mLastChocolateTime = default(DateTime);
		}

		public void ResetStinkyTimers()
		{
			this.mApp.mPlayerInfo.mPurchases[20] = 2;
			this.mApp.mPlayerInfo.mLastStinkyChocolateTime = DateTime.MinValue;
		}

		public void UpdatePlantNeeds()
		{
			this.aNow = DateTime.UtcNow;
			if (this.mApp.mPlayerInfo == null)
			{
				return;
			}
			for (int i = 0; i < this.mApp.mPlayerInfo.mNumPottedPlants; i++)
			{
				PottedPlant thePottedPlant = this.PottedPlantFromIndex(i);
				this.RefreshPlantNeeds(thePottedPlant);
			}
		}

		public void RefreshPlantNeeds(PottedPlant thePottedPlant)
		{
			if (thePottedPlant.mPlantAge != PottedPlantAge.PLANTAGE_FULL)
			{
				return;
			}
			if (!this.PlantShouldRefreshNeed(thePottedPlant))
			{
				return;
			}
			if (Plant.IsAquatic(thePottedPlant.mSeedType))
			{
				thePottedPlant.mLastWateredTime = this.aNow;
				thePottedPlant.mPlantNeed = (PottedPlantNeed)TodCommon.RandRangeInt(3, 4);
				return;
			}
			thePottedPlant.mTimesFed = 0;
			thePottedPlant.mPlantNeed = PottedPlantNeed.PLANTNEED_NONE;
		}

		public void PlantSetLaunchCounter(Plant thePlant)
		{
			int theTimeAge = this.PlantGetMinutesSinceHappy(thePlant);
			int theMax = TodCommon.TodAnimateCurve(5, 30, theTimeAge, 3000, 15000, TodCurves.CURVE_LINEAR);
			thePlant.mLaunchCounter = TodCommon.RandRangeInt(1800, theMax);
		}

		public int PlantGetMinutesSinceHappy(Plant thePlant)
		{
			PottedPlant pottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			int result = (int)((this.aNow - pottedPlant.mLastNeedFulfilledTime).TotalSeconds / 60.0);
			if (this.PlantHighOnChocolate(pottedPlant))
			{
				result = 0;
			}
			return result;
		}

		public bool IsStinkyHighOnChocolate()
		{
			return (this.aNow - this.mApp.mPlayerInfo.mLastStinkyChocolateTime).TotalSeconds < 3600.0;
		}

		public void StinkyAnimRateUpdate(GridItem theStinky)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(theStinky.mGridItemReanimID);
			if (this.IsStinkyHighOnChocolate())
			{
				if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
				{
					reanimation.mAnimRate = 12f;
					return;
				}
				if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_RIGHT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_LEFT)
				{
					reanimation.mAnimRate = 12f;
					return;
				}
			}
			else
			{
				if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_LEFT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_WALKING_RIGHT)
				{
					reanimation.mAnimRate = 6f;
					return;
				}
				if (theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_RIGHT || theStinky.mGridItemState == GridItemState.GRIDITEM_STINKY_TURNING_LEFT)
				{
					reanimation.mAnimRate = 6f;
				}
			}
		}

		public bool PlantCanBeWatered(Plant thePlant)
		{
			if (thePlant.mPottedPlantIndex == -1)
			{
				return false;
			}
			PottedPlant thePottedPlant = this.PottedPlantFromIndex(thePlant.mPottedPlantIndex);
			PottedPlantNeed plantsNeed = this.mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
			return plantsNeed == PottedPlantNeed.PLANTNEED_WATER;
		}

		public void MakeStinkySleeping()
		{
			if (!this.HasPurchasedStinky())
			{
				return;
			}
			if (!this.mApp.mPlayerInfo.mHasSeenStinky)
			{
				return;
			}
			this.ResetStinkyTimers();
		}

		public float ZenPlantOffsetX(PottedPlant thePottedPlant)
		{
			int num = 0;
			if (thePottedPlant.mFacing == PottedPlant.FacingDirection.FACING_LEFT && thePottedPlant.mSeedType == SeedType.SEED_POTATOMINE)
			{
				num -= 6;
			}
			return (float)num;
		}

		public void DoPlantSale(bool wasSold)
		{
			this.mApp.CrazyDaveLeave();
			if (wasSold)
			{
				int plantSellPrice = this.GetPlantSellPrice(this.mPlantForSale);
				this.PottedPlantFromIndex(this.mPlantForSale.mPottedPlantIndex);
				this.mApp.mPlayerInfo.AddCoins(plantSellPrice);
				this.mBoard.mCoinsCollected += plantSellPrice;
				int num = this.mApp.mPlayerInfo.mNumPottedPlants - this.mPlantForSale.mPottedPlantIndex - 1;
				if (num > 0)
				{
					for (int i = this.mPlantForSale.mPottedPlantIndex; i < this.mApp.mPlayerInfo.mPottedPlant.Length; i++)
					{
						if (i != this.mApp.mPlayerInfo.mPottedPlant.Length - 1)
						{
							this.mApp.mPlayerInfo.mPottedPlant[i] = this.mApp.mPlayerInfo.mPottedPlant[i + 1];
						}
					}
					int count = this.mBoard.mPlants.Count;
					for (int j = 0; j < count; j++)
					{
						Plant plant = this.mBoard.mPlants[j];
						if (!plant.mDead && plant.mPottedPlantIndex > this.mPlantForSale.mPottedPlantIndex)
						{
							plant.mPottedPlantIndex--;
						}
					}
				}
				this.mApp.mPlayerInfo.mNumPottedPlants--;
				this.mApp.PlayFoley(FoleyType.FOLEY_USE_SHOVEL);
				this.RemovePottedPlant(this.mPlantForSale);
			}
		}

		public void BackFromStore()
		{
			StoreScreen storeScreen = (StoreScreen)this.mApp.GetDialog(4);
			bool mGoToTreeNow = storeScreen.mGoToTreeNow;
			this.mApp.KillDialog(Dialogs.DIALOG_STORE);
			if (mGoToTreeNow)
			{
				this.mApp.KillBoard();
				this.mApp.PreNewGame(GameMode.GAMEMODE_TREE_OF_WISDOM, false);
				return;
			}
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_ZEN_GARDEN);
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_VISIT_STORE)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_FERTILIZE]", MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG, AdviceType.ADVICE_NONE);
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZEN_GARDEN_FERTILIZE_PLANTS;
				this.mApp.mPlayerInfo.mZenTutorialMessage = 26;
			}
			this.AddStinky();
		}

		private static ZenGarden.PottedPlantOffset[] POTTED_PLANT_DRAW_OFFSETS = new ZenGarden.PottedPlantOffset[]
		{
			new ZenGarden.PottedPlantOffset(30, 0, -35, -7, -16, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -9, -14, 0),
			new ZenGarden.PottedPlantOffset(36, 0, -30, -25, -17, 0),
			new ZenGarden.PottedPlantOffset(34, -2, -35, -27, -17, 0),
			new ZenGarden.PottedPlantOffset(25, -5, -35, -13, -17, 0),
			new ZenGarden.PottedPlantOffset(30, -3, -40, -7, -17, -1),
			new ZenGarden.PottedPlantOffset(28, 4, -50, -16, -19, 3),
			new ZenGarden.PottedPlantOffset(30, -2, -37, -7, -17, 0),
			new ZenGarden.PottedPlantOffset(32, -4, -37, -25, -17, 0),
			new ZenGarden.PottedPlantOffset(35, 0, -37, -25, -17, 0),
			new ZenGarden.PottedPlantOffset(28, 0, -37, -20, -17, 1),
			new ZenGarden.PottedPlantOffset(70, 0, -37, 0, -14, 0),
			new ZenGarden.PottedPlantOffset(28, 0, -37, -25, -18, 0),
			new ZenGarden.PottedPlantOffset(34, 8, -43, -21, -20, -3),
			new ZenGarden.PottedPlantOffset(28, 0, -40, -21, -14, 0),
			new ZenGarden.PottedPlantOffset(28, 0, -40, -21, -18, 0),
			new ZenGarden.PottedPlantOffset(12, 0, -40, -36, -17, 0),
			new ZenGarden.PottedPlantOffset(28, 0, -40, -19, -17, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -8, -17, 0),
			new ZenGarden.PottedPlantOffset(10, 0, -40, -28, -17, 0),
			new ZenGarden.PottedPlantOffset(28, 0, -40, -22, -17, 0),
			new ZenGarden.PottedPlantOffset(22, 0, -40, -30, -17, 0),
			new ZenGarden.PottedPlantOffset(27, 0, -40, -19, -17, 0),
			new ZenGarden.PottedPlantOffset(28, 0, -40, -22, -17, 0),
			new ZenGarden.PottedPlantOffset(5, 0, -40, -23, -11, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -28, -17, 0),
			new ZenGarden.PottedPlantOffset(33, 5, -45, -28, -17, -1),
			new ZenGarden.PottedPlantOffset(35, 0, -40, -32, -22, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -20, -17, -5),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -30, -14, 0),
			new ZenGarden.PottedPlantOffset(15, 0, -40, -18, -14, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -26, -14, -3),
			new ZenGarden.PottedPlantOffset(33, 0, -40, -27, -18, -8),
			new ZenGarden.PottedPlantOffset(0, 0, 0, 0, -14, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -37, -23, -14, -5),
			new ZenGarden.PottedPlantOffset(30, 10, -30, -10, -14, 0),
			new ZenGarden.PottedPlantOffset(32, 0, -40, -25, -14, 0),
			new ZenGarden.PottedPlantOffset(35, 0, -40, -30, -17, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -35, -8, -17, -2),
			new ZenGarden.PottedPlantOffset(33, 0, -40, -28, -17, -8),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -22, -14, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -38, -22, -14, 0),
			new ZenGarden.PottedPlantOffset(35, 0, -40, -26, -14, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -26, -14, 0),
			new ZenGarden.PottedPlantOffset(34, 0, -40, -26, -14, -5),
			new ZenGarden.PottedPlantOffset(30, 0, -40, -25, -14, -2),
			new ZenGarden.PottedPlantOffset(25, 0, -40, -35, -14, 0),
			new ZenGarden.PottedPlantOffset(0, 0, -40, 0, -14, 0),
			new ZenGarden.PottedPlantOffset(33, 0, -40, 0, -14, 0),
			new ZenGarden.PottedPlantOffset(20, 0, 0, 0, -14, 0),
			new ZenGarden.PottedPlantOffset(0, 0, 0, 0, -14, 0),
			new ZenGarden.PottedPlantOffset(30, 0, -38, -35, -14, 0),
			new ZenGarden.PottedPlantOffset(30, 0, 37, 0, -14, -5)
		};

		public static SpecialGridPlacement[] gGreenhouseGridPlacement = new SpecialGridPlacement[]
		{
			new SpecialGridPlacement(73, 73, 0, 0),
			new SpecialGridPlacement(155, 71, 1, 0),
			new SpecialGridPlacement(239, 68, 2, 0),
			new SpecialGridPlacement(321, 73, 3, 0),
			new SpecialGridPlacement(406, 71, 4, 0),
			new SpecialGridPlacement(484, 67, 5, 0),
			new SpecialGridPlacement(566, 70, 6, 0),
			new SpecialGridPlacement(648, 72, 7, 0),
			new SpecialGridPlacement(67, 168, 0, 1),
			new SpecialGridPlacement(150, 165, 1, 1),
			new SpecialGridPlacement(232, 170, 2, 1),
			new SpecialGridPlacement(314, 175, 3, 1),
			new SpecialGridPlacement(416, 173, 4, 1),
			new SpecialGridPlacement(497, 170, 5, 1),
			new SpecialGridPlacement(578, 164, 6, 1),
			new SpecialGridPlacement(660, 168, 7, 1),
			new SpecialGridPlacement(41, 268, 0, 2),
			new SpecialGridPlacement(130, 266, 1, 2),
			new SpecialGridPlacement(219, 260, 2, 2),
			new SpecialGridPlacement(310, 266, 3, 2),
			new SpecialGridPlacement(416, 267, 4, 2),
			new SpecialGridPlacement(504, 261, 5, 2),
			new SpecialGridPlacement(594, 265, 6, 2),
			new SpecialGridPlacement(684, 269, 7, 2),
			new SpecialGridPlacement(37, 371, 0, 3),
			new SpecialGridPlacement(124, 369, 1, 3),
			new SpecialGridPlacement(211, 368, 2, 3),
			new SpecialGridPlacement(302, 369, 3, 3),
			new SpecialGridPlacement(425, 375, 4, 3),
			new SpecialGridPlacement(512, 368, 5, 3),
			new SpecialGridPlacement(602, 365, 6, 3),
			new SpecialGridPlacement(691, 368, 7, 3)
		};

		public static SpecialGridPlacement[] gAquariumGridPlacement = new SpecialGridPlacement[]
		{
			new SpecialGridPlacement(113, 185, 0, 0),
			new SpecialGridPlacement(306, 120, 1, 0),
			new SpecialGridPlacement(356, 270, 2, 0),
			new SpecialGridPlacement(622, 120, 3, 0),
			new SpecialGridPlacement(669, 270, 4, 0),
			new SpecialGridPlacement(122, 355, 5, 0),
			new SpecialGridPlacement(365, 458, 6, 0),
			new SpecialGridPlacement(504, 417, 7, 0)
		};

		public LawnApp mApp;

		public Board mBoard;

		public GardenType mGardenType;

		public bool mIsTutorial;

		public readonly ulong STINKY_BASE_TIME = (ulong)TimeSpan.FromTicks(new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks).TotalSeconds;

		private DateTime aNow;

		public Plant mPlantForSale;

		private struct PottedPlantOffset
		{
			public PottedPlantOffset(int y, int xRight, int xLeft, int yWheelBarrowScale, int yCachedOffset, int xCachedOffset)
			{
				this.y = y;
				this.xRight = xRight;
				this.xLeft = xLeft;
				this.yWheelBarrowScale = yWheelBarrowScale;
				this.yCachedOffset = yCachedOffset;
				this.xCachedOffset = xCachedOffset;
			}

			public int y;

			public int xRight;

			public int xLeft;

			public int yWheelBarrowScale;

			public int yCachedOffset;

			public int xCachedOffset;
		}
	}
}
