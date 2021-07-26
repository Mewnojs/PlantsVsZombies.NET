using System;
using Sexy;

namespace Lawn
{
	internal class SeedPacketsWidget : Widget
	{
		public SeedPacketsWidget(LawnApp theApp, int theNumberOfRows, bool theIsImitaters, SeedPacketsWidgetListener theListener)
		{
			this.mApp = theApp;
			this.mListener = theListener;
			this.mImitaters = theIsImitaters;
			this.mRows = theNumberOfRows;
			this.mWidth = 4 * Constants.SMALL_SEEDPACKET_WIDTH + 3 * Constants.SEED_PACKET_HORIZ_GAP;
			this.mHeight = Constants.SMALL_SEEDPACKET_HEIGHT * this.mRows + (this.mRows - 1) * Constants.SEED_PACKET_VERT_GAP;
		}

		public void GetSeedPosition(SeedType theSeedType, ref int theX, ref int theY)
		{
			theX = (int)(theSeedType % SeedType.SEED_POTATOMINE * (SeedType)(Constants.SMALL_SEEDPACKET_WIDTH + Constants.SEED_PACKET_HORIZ_GAP));
			theY = (int)(theSeedType / SeedType.SEED_POTATOMINE * (SeedType)(Constants.SMALL_SEEDPACKET_HEIGHT + Constants.SEED_PACKET_VERT_GAP));
		}

		public override void Dispose()
		{
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (y < 0)
			{
				y = 0;
			}
			int num = x / (Constants.SMALL_SEEDPACKET_WIDTH + Constants.SEED_PACKET_HORIZ_GAP);
			if (num > 3)
			{
				num = 3;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = y / (Constants.SMALL_SEEDPACKET_HEIGHT + Constants.SEED_PACKET_VERT_GAP);
			SeedType unnamedParameter = (SeedType)(num2 * 4 + num);
			this.mListener.SeedSelected(unnamedParameter);
		}

		public void DrawPackets(Graphics g, bool theDrawCost, bool theDrawBackground)
		{
			int num = 44;
			if (this.mRows == 12)
			{
				num = 48;
			}
			for (int i = 0; i < num; i++)
			{
				int num2 = 0;
				int num3 = 0;
				SeedType seedType = (SeedType)i;
				this.GetSeedPosition(seedType, ref num2, ref num3);
				if (seedType != SeedType.SEED_IMITATER)
				{
					if (this.mApp.HasSeedType(seedType))
					{
						ChosenSeed chosenSeed = this.mApp.mSeedChooserScreen.mChosenSeeds[i];
						if (chosenSeed.mSeedState != ChosenSeedState.SEED_IN_CHOOSER)
						{
							if (this.mImitaters)
							{
								SeedPacket.DrawSmallSeedPacket(g, (float)num2, (float)num3, SeedType.SEED_IMITATER, seedType, 0f, 255, theDrawCost, false, theDrawBackground, theDrawBackground);
							}
							else
							{
								SeedPacket.DrawSmallSeedPacket(g, (float)num2, (float)num3, seedType, SeedType.SEED_NONE, 0f, 55, theDrawCost, false, theDrawBackground, theDrawBackground);
							}
						}
					}
					else
					{
						g.DrawImage(AtlasResources.IMAGE_SEEDPACKETSILHOUETTE, num2, num3);
					}
				}
			}
			for (int j = 0; j < 49; j++)
			{
				SeedType theSeedType = (SeedType)j;
				if (this.mApp.HasSeedType(theSeedType))
				{
					ChosenSeed chosenSeed2 = this.mApp.mSeedChooserScreen.mChosenSeeds[j];
					if (chosenSeed2.mSeedState != ChosenSeedState.SEED_FLYING_TO_CHOOSER && chosenSeed2.mSeedState != ChosenSeedState.SEED_FLYING_TO_BANK && chosenSeed2.mSeedState != ChosenSeedState.SEED_PACKET_HIDDEN && chosenSeed2.mSeedState == ChosenSeedState.SEED_IN_CHOOSER)
					{
						bool flag = false;
						uint num4 = this.mApp.mSeedChooserScreen.SeedNotRecommendedToPick(chosenSeed2.mSeedType);
						if (num4 != 0U)
						{
							flag = true;
						}
						else if (this.mApp.mSeedChooserScreen.SeedNotAllowedToPick(chosenSeed2.mSeedType))
						{
							flag = true;
						}
						else if (this.mApp.mSeedChooserScreen.SeedNotAllowedDuringTrial(chosenSeed2.mSeedType))
						{
							flag = true;
						}
						int num5 = 0;
						int num6 = 0;
						this.GetSeedPosition(chosenSeed2.mSeedType, ref num5, ref num6);
						float num7 = (float)(flag ? 115 : 255);
						if (this.mImitaters)
						{
							SeedPacket.DrawSmallSeedPacket(g, (float)num5, (float)num6, SeedType.SEED_IMITATER, chosenSeed2.mSeedType, 0f, (int)num7, theDrawCost, false, theDrawBackground, theDrawBackground);
						}
						else
						{
							SeedPacket.DrawSmallSeedPacket(g, (float)num5, (float)num6, chosenSeed2.mSeedType, chosenSeed2.mImitaterType, 0f, (int)num7, theDrawCost, false, theDrawBackground, theDrawBackground);
						}
					}
				}
			}
		}

		public override void Draw(Graphics g)
		{
			g.HardwareClip();
			this.DrawPackets(g, false, true);
			this.DrawPackets(g, true, false);
			g.EndHardwareClip();
		}

		public LawnApp mApp;

		public int mRows;

		public bool mImitaters;

		public SeedPacketsWidgetListener mListener;
	}
}
