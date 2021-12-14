using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class ZombieGalleryWidget : Widget
	{
		public ZombieGalleryWidget(AlmanacDialog theDialog)
		{
			mDialog = theDialog;
			mWidth = Constants.ZombieGallerySize.X;
			mHeight = Constants.ZombieGallerySize.Y;
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			ZombieType zombieType = ZombieHitTest(x, y);
			if (zombieType != ZombieType.ZOMBIE_INVALID)
			{
				mDialog.ZombieSelected(zombieType);
			}
		}

		public ZombieType GetZombieType(int theIndex)
		{
			if (theIndex >= 33)
			{
				return ZombieType.ZOMBIE_INVALID;
			}
			return (ZombieType)theIndex;
		}

		public ZombieType ZombieHitTest(int x, int y)
		{
			for (int i = 0; i < GameConstants.NUM_ALMANAC_ZOMBIES; i++)
			{
				if (i < 33)
				{
					ZombieType zombieType = GetZombieType(i);
					if (zombieType != ZombieType.ZOMBIE_INVALID && ZombieIsShown(zombieType))
					{
						int num = 0;
						int num2 = 0;
						GetZombiePosition(zombieType, ref num, ref num2);
						if (x >= num && y >= num2 && (float)x < (float)num + Constants.InvertAndScale(76f) && (float)y < (float)num2 + Constants.InvertAndScale(76f))
						{
							return zombieType;
						}
					}
				}
			}
			return ZombieType.ZOMBIE_INVALID;
		}

		public void GetZombiePosition(ZombieType theZombieType, ref int x, ref int y)
		{
			if (theZombieType == ZombieType.ZOMBIE_BOSS)
			{
				x = Constants.Almanac_BossPosition.X;
				y = Constants.Almanac_BossPosition.Y;
				return;
			}
			if (theZombieType == ZombieType.ZOMBIE_IMP)
			{
				x = Constants.Almanac_ImpPosition.X;
				y = Constants.Almanac_ImpPosition.Y;
				return;
			}
			x = (int)((int)theZombieType % 3 * Constants.Almanac_ZombieSpace.X);
			y = (int)(5 + (int)theZombieType / 3 * Constants.Almanac_ZombieSpace.Y);
		}

		public bool ZombieIsShown(ZombieType theZombieType)
		{
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			int level = mDialog.mApp.mPlayerInfo.GetLevel();
			if (mDialog.mApp.IsTrialStageLocked() && theZombieType > ZombieType.ZOMBIE_SNORKEL)
			{
				return false;
			}
			if (theZombieType == ZombieType.ZOMBIE_YETI)
			{
				return mDialog.mApp.CanSpawnYetis() || mDialog.ZombieHasSilhouette(ZombieType.ZOMBIE_YETI);
			}
			return theZombieType <= ZombieType.ZOMBIE_BOSS && (mDialog.mApp.HasFinishedAdventure() || (zombieDefinition.mStartingLevel <= level && (zombieDefinition.mStartingLevel != level || (theZombieType != ZombieType.ZOMBIE_IMP && theZombieType != ZombieType.ZOMBIE_BOBSLED && theZombieType != ZombieType.ZOMBIE_BACKUP_DANCER) || AlmanacDialog.gZombieDefeated[(int)theZombieType])));
		}

		public override void Draw(Graphics g)
		{
			for (int i = 0; i < GameConstants.NUM_ALMANAC_ZOMBIES; i++)
			{
				ZombieType zombieType = GetZombieType(i);
				int num = 0;
				int num2 = 0;
				GetZombiePosition(zombieType, ref num, ref num2);
				if (zombieType != ZombieType.ZOMBIE_INVALID)
				{
					if (!ZombieIsShown(zombieType))
					{
						g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIEBLANK, num, num2);
					}
					else
					{
						g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIEWINDOW, num + (int)Constants.InvertAndScale(5f), num2 + (int)Constants.InvertAndScale(6f));
						ZombieType zombieType2 = zombieType;
						Graphics @new = Graphics.GetNew(g);
						@new.ClipRect(num + Constants.ZombieGalleryWidget_Window_Clip.X, num2 + Constants.ZombieGalleryWidget_Window_Clip.Y, Constants.ZombieGalleryWidget_Window_Clip.Width, Constants.ZombieGalleryWidget_Window_Clip.Height);
						if (mDialog.ZombieHasSilhouette(zombieType))
						{
							@new.SetColor(new SexyColor(0, 0, 0, 64));
							@new.SetColorizeImages(true);
						}
						mDialog.mApp.mReanimatorCache.DrawCachedZombie(@new, (float)(num + (int)Constants.InvertAndScale((float)AlmanacDialog.ZombieOffsets[(int)zombieType2].mX)), (float)(num2 + (int)Constants.InvertAndScale((float)AlmanacDialog.ZombieOffsets[(int)zombieType2].mY)), zombieType2);
						@new.SetColorizeImages(false);
						g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIEWINDOW2, num + Constants.ZombieGalleryWidget_Window_Offset.X, num2 + Constants.ZombieGalleryWidget_Window_Offset.Y);
						@new.PrepareForReuse();
					}
				}
			}
		}

		public AlmanacDialog mDialog;
	}
}
