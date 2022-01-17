using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal static class GlobalMembersStoreScreen
	{
		internal static void DrawStoreItemIcon(Graphics g, int aPosX, int aPosY, StoreItem theItemType, int aQuantity)
		{
			if (theItemType == StoreItem.STORE_ITEM_PACKET_UPGRADE)
			{
				g.DrawImage(AtlasResources.IMAGE_STORE_PACKETUPGRADE, aPosX - Constants.StoreScreen_PacketUpgrade_X, aPosY + Constants.StoreScreen_PacketUpgrade_Y);
				if (aQuantity > 9)
				{
					aQuantity = 9;
				}
				string text;
				if (!GlobalMembersStoreScreen.seedNumberUpgradeCache.TryGetValue(aQuantity, out text))
				{
					text = TodCommon.TodReplaceNumberString("[STORE_UPGRADE_SLOTS]", "{SLOTS}", aQuantity);
					GlobalMembersStoreScreen.seedNumberUpgradeCache.Add(aQuantity, text);
				}
				TRect theRect = new TRect(aPosX, aPosY + Constants.StoreScreen_PacketUpgrade_Text_Size.Y, Constants.StoreScreen_PacketUpgrade_Text_Size.Width, Constants.StoreScreen_PacketUpgrade_Text_Size.Height);
				TodStringFile.TodDrawStringWrapped(g, text, theRect, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.CenterVerticalMiddle);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_BONUS_LAWN_MOWER)
			{
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_POOL_CLEANER)
			{
				g.DrawImage(AtlasResources.IMAGE_ICON_POOLCLEANER, aPosX + Constants.StoreScreen_PoolCleaner_Offset_X, aPosY + Constants.StoreScreen_PoolCleaner_Offset_Y);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_RAKE)
			{
				g.DrawImage(AtlasResources.IMAGE_ICON_RAKE, aPosX - Constants.StoreScreen_Rake_Offset_X, aPosY + Constants.StoreScreen_Rake_Offset_Y);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_ROOF_CLEANER)
			{
				g.DrawImage(AtlasResources.IMAGE_ICON_ROOFCLEANER, aPosX + Constants.StoreScreen_RoofCleaner_Offset_X, aPosY + Constants.StoreScreen_RoofCleaner_Offset_Y);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_PLANT_IMITATER)
			{
				SeedPacket.DrawSmallSeedPacket(g, aPosX + Constants.StoreScreen_Imitater_Offset_X, aPosY + Constants.StoreScreen_Imitater_Offset_Y, SeedType.Imitater, SeedType.None, 0f, 255, false, false, true, false);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_FIRSTAID)
			{
				g.DrawImage(AtlasResources.IMAGE_STORE_FIRSTAIDWALLNUTICON, aPosX, aPosY + Constants.StoreScreen_FirstAidNut_Offset_Y);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_MUSHROOM_GARDEN)
			{
				g.DrawImage(AtlasResources.IMAGE_STORE_MUSHROOMGARDENICON, aPosX + Constants.StoreScreenMushroomGardenOffsetX, aPosY + 28, 50, 40);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_AQUARIUM_GARDEN)
			{
				g.DrawImage(AtlasResources.IMAGE_STORE_AQUARIUMGARDENICON, aPosX + Constants.StoreScreenAquariumGardenOffsetX, aPosY + 28, 50, 40);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_STINKY_THE_SNAIL)
			{
				g.DrawImage(AtlasResources.IMAGE_REANIM_STINKY_TURN3, aPosX, aPosY + 26);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_GOLD_WATERINGCAN)
			{
				g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1_GOLD, aPosX + Constants.StoreScreenWateringCanOffsetX, aPosY + Constants.StoreScreenWateringCanOffsetY);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_FERTILIZER)
			{
				g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG1, aPosX + Constants.StoreScreenPlantFoodOffsetX, aPosY + Constants.StoreScreenPlantFoodOffsetY);
				TodCommon.TodDrawString(g, "x5", aPosX + 50, aPosY + 65, Resources.FONT_HOUSEOFTERROR16, Color.White, DrawStringJustification.Right);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_PHONOGRAPH)
			{
				g.DrawImage(AtlasResources.IMAGE_PHONOGRAPH, aPosX + 3, aPosY + Constants.StoreScreenPhonographOffsetY);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_BUG_SPRAY)
			{
				g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE, aPosX + Constants.StoreScreenBugSprayOffsetX, aPosY + Constants.StoreScreenBugSprayOffsetY);
				TodCommon.TodDrawString(g, "x5", aPosX + 47, aPosY + 65, Resources.FONT_HOUSEOFTERROR16, Color.White, DrawStringJustification.Right);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_GARDENING_GLOVE)
			{
				g.DrawImage(AtlasResources.IMAGE_ZEN_GARDENGLOVE, aPosX + 5, aPosY + Constants.StoreScreenGloveOffsetY);
				return;
			}
			if (theItemType == StoreItem.STORE_ITEM_WHEEL_BARROW)
			{
				g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, aPosX + 5, aPosY + Constants.StoreScreenWheelbarrowOffsetY);
				return;
			}
			if (StoreScreen.IsPottedPlant(theItemType))
			{
				LawnApp gLawnApp = GlobalStaticVars.gLawnApp;
				GlobalMembersStoreScreen.tempPottedPlant.InitializePottedPlant(SeedType.Marigold);
				GlobalMembersStoreScreen.tempPottedPlant.mFacing = PottedPlant.FacingDirection.Right;
				gLawnApp.mZenGarden.DrawPottedPlantIcon(g, aPosX + Constants.StoreScreen_PotPlant_Offset.X, aPosY + Constants.StoreScreen_PotPlant_Offset.Y, GlobalMembersStoreScreen.tempPottedPlant);
				return;
			}
			SeedType theSeedType = (SeedType)(theItemType + 40);
			SeedPacket.DrawSmallSeedPacket(g, aPosX + -3, aPosY + Constants.StoreScreen_Default_Offset_Y, theSeedType, SeedType.None, 0f, 255, false, false, true, false);
		}

		internal static void DrawStoreItem(Graphics g, int aPosX, int aPosY, StoreItem theItemType, bool isComingSoon, bool isSoldOut, int aQuantity, int aCost)
		{
			GlobalMembersStoreScreen.DrawStoreItemIcon(g, aPosX, aPosY, theItemType, aQuantity);
			g.DrawImage(AtlasResources.IMAGE_STORE_PRICETAG, aPosX - Constants.StoreScreen_PriceTag_X, aPosY + Constants.StoreScreen_PriceTag_Y);
			string moneyString = LawnApp.GetMoneyString(aCost);
			TodCommon.TodDrawString(g, moneyString, aPosX + Constants.StoreScreen_PriceTag_Text_Offset_X, aPosY + Constants.StoreScreen_PriceTag_Text_Offset_Y, Resources.FONT_BRIANNETOD12, new SexyColor(0, 0, 0), DrawStringJustification.Center);
			if (isComingSoon)
			{
				TRect theRect = new TRect(aPosX - (int)(Constants.S * 12f), aPosY, Constants.StoreScreen_ComingSoon_X, Constants.StoreScreen_ComingSoon_Y);
				if (theItemType == StoreItem.STORE_ITEM_PLANT_TWINSUNFLOWER || theItemType == StoreItem.STORE_ITEM_PACKET_UPGRADE)
				{
					theRect.mX -= (int)(Constants.S * 4f);
				}
				TodStringFile.TodDrawStringWrapped(g, "[COMING_SOON]", theRect, Resources.FONT_HOUSEOFTERROR16, new SexyColor(255, 0, 0), DrawStringJustification.CenterVerticalMiddle);
				return;
			}
			if (isSoldOut)
			{
				TRect theRect2 = new TRect(aPosX, aPosY + Constants.StoreScreen_SoldOut_Y, Constants.StoreScreen_SoldOut_Width, Constants.StoreScreen_SoldOut_Height);
				TodStringFile.TodDrawStringWrapped(g, "[SOLD_OUT]", theRect2, Resources.FONT_HOUSEOFTERROR16, new SexyColor(255, 0, 0), DrawStringJustification.CenterVerticalMiddle);
			}
		}

		public const int PURCHASE_COUNT_OFFSET = 1000;

		private static Dictionary<int, string> seedNumberUpgradeCache = new Dictionary<int, string>(3);

		private static PottedPlant tempPottedPlant = new PottedPlant();
	}
}
