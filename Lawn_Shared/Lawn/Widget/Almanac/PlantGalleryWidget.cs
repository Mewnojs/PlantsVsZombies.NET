using System;
using Sexy;

namespace Lawn
{
	internal class PlantGalleryWidget : Widget
	{
		public PlantGalleryWidget(AlmanacDialog theDialog)
		{
			this.mDialog = theDialog;
			this.mWidth = Constants.PlantGallerySize.X;
			this.mHeight = Constants.PlantGallerySize.Y;
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			SeedType seedType = this.SeedHitTest(x, y);
			if (seedType != SeedType.SEED_NONE)
			{
				this.mDialog.PlantSelected(seedType);
			}
		}

		public SeedType SeedHitTest(int x, int y)
		{
			for (int i = 0; i < GameConstants.NUM_ALMANAC_SEEDS; i++)
			{
				SeedType seedType = (SeedType)i;
				if (this.mDialog.mApp.HasSeedType(seedType))
				{
					int num = 0;
					int num2 = 0;
					this.GetSeedPosition(seedType, ref num, ref num2);
					int small_SEEDPACKET_WIDTH = Constants.SMALL_SEEDPACKET_WIDTH;
					int small_SEEDPACKET_HEIGHT = Constants.SMALL_SEEDPACKET_HEIGHT;
					if (x >= num && y >= num2 && x < num + small_SEEDPACKET_WIDTH && y < num2 + small_SEEDPACKET_HEIGHT)
					{
						return seedType;
					}
				}
			}
			return SeedType.SEED_NONE;
		}

		public void GetSeedPosition(SeedType theSeedType, ref int x, ref int y)
		{
			if (theSeedType == SeedType.SEED_IMITATER)
			{
				x = Constants.Almanac_ImitatorPosition.X;
				y = Constants.Almanac_ImitatorPosition.Y;
				return;
			}
			x = (int)(Constants.Almanac_SeedOffset.X + (int)theSeedType % 4 * (Constants.SMALL_SEEDPACKET_WIDTH + Constants.Almanac_SeedSpace.X));
			y = (int)(Constants.Almanac_SeedOffset.X + (int)theSeedType / 4 * (Constants.SMALL_SEEDPACKET_HEIGHT + Constants.Almanac_SeedSpace.Y));
		}

		public override void Draw(Graphics g)
		{
			g.HardwareClip();
			bool flag = false;
			bool flag2 = true;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < GameConstants.NUM_ALMANAC_SEEDS; j++)
				{
					SeedType seedType = (SeedType)j;
					int num = 0;
					int num2 = 0;
					this.GetSeedPosition(seedType, ref num, ref num2);
					if (!this.mDialog.mApp.HasSeedType(seedType))
					{
						g.DrawImage(AtlasResources.IMAGE_ALMANAC_PLANTBLANK, num, num2);
					}
					else
					{
						SeedPacket.DrawSmallSeedPacket(g, (float)num, (float)num2, seedType, SeedType.SEED_NONE, 0f, 255, seedType != SeedType.SEED_IMITATER && flag, false, flag2, seedType != SeedType.SEED_IMITATER && flag2);
					}
				}
				flag = true;
				flag2 = false;
			}
			g.EndHardwareClip();
		}

		public AlmanacDialog mDialog;
	}
}
