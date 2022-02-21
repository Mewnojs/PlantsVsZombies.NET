using System;
using Sexy;

namespace Lawn
{
    public/*internal*/ class SeedPacketsWidget : Widget
    {
        public SeedPacketsWidget(LawnApp theApp, int theNumberOfRows, bool theIsImitaters, SeedPacketsWidgetListener theListener)
        {
            mApp = theApp;
            mListener = theListener;
            mImitaters = theIsImitaters;
            mRows = theNumberOfRows;
            mWidth = 4 * Constants.SMALL_SEEDPACKET_WIDTH + 3 * Constants.SEED_PACKET_HORIZ_GAP;
            mHeight = Constants.SMALL_SEEDPACKET_HEIGHT * mRows + (mRows - 1) * Constants.SEED_PACKET_VERT_GAP;
        }

        public void GetSeedPosition(SeedType theSeedType, ref int theX, ref int theY)
        {
            theX = (int)theSeedType % 4 * (Constants.SMALL_SEEDPACKET_WIDTH + Constants.SEED_PACKET_HORIZ_GAP);
            theY = (int)theSeedType / 4 * (Constants.SMALL_SEEDPACKET_HEIGHT + Constants.SEED_PACKET_VERT_GAP);
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
            mListener.SeedSelected(unnamedParameter);
        }

        public void DrawPackets(Graphics g, bool theDrawCost, bool theDrawBackground)
        {
            int num = 44;
            if (mRows == 12)
            {
                num = 48;
            }
            for (int i = 0; i < num; i++)
            {
                int num2 = 0;
                int num3 = 0;
                SeedType seedType = (SeedType)i;
                GetSeedPosition(seedType, ref num2, ref num3);
                if (seedType != SeedType.Imitater)
                {
                    if (mApp.HasSeedType(seedType))
                    {
                        ChosenSeed chosenSeed = mApp.mSeedChooserScreen.mChosenSeeds[i];
                        if (chosenSeed.mSeedState != ChosenSeedState.SEED_IN_CHOOSER)
                        {
                            if (mImitaters)
                            {
                                SeedPacket.DrawSmallSeedPacket(g, num2, num3, SeedType.Imitater, seedType, 0f, 255, theDrawCost, false, theDrawBackground, theDrawBackground);
                            }
                            else
                            {
                                SeedPacket.DrawSmallSeedPacket(g, num2, num3, seedType, SeedType.None, 0f, 55, theDrawCost, false, theDrawBackground, theDrawBackground);
                            }
                        }
                    }
                    else
                    {
                        g.DrawImage(AtlasResources.IMAGE_SEEDPACKETSILHOUETTE, num2, num3);
                    }
                }
            }
            for (SeedType j = 0; j < SeedType.SeedsInChooserCount; j++)
            {
                if (mApp.HasSeedType(j))
                {
                    ChosenSeed chosenSeed2 = mApp.mSeedChooserScreen.mChosenSeeds[(int)j];
                    if (chosenSeed2.mSeedState != ChosenSeedState.SEED_FLYING_TO_CHOOSER && chosenSeed2.mSeedState != ChosenSeedState.SEED_FLYING_TO_BANK && chosenSeed2.mSeedState != ChosenSeedState.SEED_PACKET_HIDDEN && chosenSeed2.mSeedState == ChosenSeedState.SEED_IN_CHOOSER)
                    {
                        bool flag = false;
                        uint num4 = mApp.mSeedChooserScreen.SeedNotRecommendedToPick(chosenSeed2.mSeedType);
                        if (num4 != 0U)
                        {
                            flag = true;
                        }
                        else if (mApp.mSeedChooserScreen.SeedNotAllowedToPick(chosenSeed2.mSeedType))
                        {
                            flag = true;
                        }
                        else if (mApp.mSeedChooserScreen.SeedNotAllowedDuringTrial(chosenSeed2.mSeedType))
                        {
                            flag = true;
                        }
                        int num5 = 0;
                        int num6 = 0;
                        GetSeedPosition(chosenSeed2.mSeedType, ref num5, ref num6);
                        float num7 = flag ? 115 : 255;
                        if (mImitaters)
                        {
                            SeedPacket.DrawSmallSeedPacket(g, num5, num6, SeedType.Imitater, chosenSeed2.mSeedType, 0f, (int)num7, theDrawCost, false, theDrawBackground, theDrawBackground);
                        }
                        else
                        {
                            SeedPacket.DrawSmallSeedPacket(g, num5, num6, chosenSeed2.mSeedType, chosenSeed2.mImitaterType, 0f, (int)num7, theDrawCost, false, theDrawBackground, theDrawBackground);
                        }
                    }
                }
            }
        }

        public override void Draw(Graphics g)
        {
            g.HardwareClip();
            DrawPackets(g, false, true);
            DrawPackets(g, true, false);
            g.EndHardwareClip();
        }

        public LawnApp mApp;

        public int mRows;

        public bool mImitaters;

        public SeedPacketsWidgetListener mListener;
    }
}
