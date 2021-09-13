using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class MiniGamesWidget : Widget
	{
		public MiniGamesWidget(LawnApp theApp, MiniGamesWidgetListener theListener)
		{
			this.mApp = theApp;
			this.mListener = theListener;
			this.mWidth = 0;
			this.mHeight = AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight + 20;
			this.mMode = MiniGameMode.MINI_GAME_MODE_GAMES;
		}

		public void SwitchMode(MiniGameMode mode)
		{
			this.mMode = mode;
			this.SizeToFit();
		}

		public int GetModeLevelCount()
		{
			if (this.mApp.mPlayerInfo == null)
			{
				return 0;
			}
			switch (this.mMode)
			{
			case MiniGameMode.MINI_GAME_MODE_GAMES:
				if (this.mApp.mPlayerInfo.mMiniGamesUnlocked < 19)
				{
					return this.mApp.mPlayerInfo.mMiniGamesUnlocked;
				}
				return 19;
			case MiniGameMode.MINI_GAME_MODE_I_ZOMBIE:
				if (this.mApp.mPlayerInfo.mIZombieUnlocked < 10)
				{
					return this.mApp.mPlayerInfo.mIZombieUnlocked;
				}
				return 10;
			case MiniGameMode.MINI_GAME_MODE_VASEBREAKER:
				if (this.mApp.mPlayerInfo.mVasebreakerUnlocked < 10)
				{
					return this.mApp.mPlayerInfo.mVasebreakerUnlocked;
				}
				return 10;
			default:
				return -1;
			}
		}

		public bool GetDrawPadlock()
		{
			if (this.mApp.mPlayerInfo == null)
			{
				return false;
			}
			switch (this.mMode)
			{
			case MiniGameMode.MINI_GAME_MODE_GAMES:
				if (this.mApp.mPlayerInfo.mMiniGamesUnlocked != 19)
				{
					return true;
				}
				break;
			case MiniGameMode.MINI_GAME_MODE_I_ZOMBIE:
				if (this.mApp.mPlayerInfo.mIZombieUnlocked != 10)
				{
					return true;
				}
				break;
			case MiniGameMode.MINI_GAME_MODE_VASEBREAKER:
				if (this.mApp.mPlayerInfo.mVasebreakerUnlocked != 10)
				{
					return true;
				}
				break;
			}
			return false;
		}

		public bool HasBeenBeaten(int index)
		{
			return this.mApp.HasBeatenChallenge((GameMode)this.GetGameMode(index + 1));
		}

		public override void Draw(Graphics g)
		{
			int num = 10;
			int modeLevelCount = this.GetModeLevelCount();
			bool drawPadlock = this.GetDrawPadlock();
			for (int i = 0; i < modeLevelCount; i++)
			{
				this.DrawBackgroundThumbnailForLevel(g, num + Constants.QuickPlayWidget_Thumb_X, Constants.QuickPlayWidget_Thumb_Y, i + 1);
				num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
			}
			num = 10;
			for (int j = 0; j < modeLevelCount; j++)
			{
				g.DrawImage(AtlasResources.IMAGE_MINI_GAME_FRAME, num, 0);
				if (this.HasBeenBeaten(j))
				{
					g.DrawImage(AtlasResources.IMAGE_MINIGAME_TROPHY, num + 3, 6);
				}
				string levelName = this.GetLevelName(j + 1);
				g.SetFont(Resources.FONT_DWARVENTODCRAFT12);
				g.SetColor(Color.White);
				g.WriteWordWrapped(new TRect(num + 15, AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight, AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth - 30, 100), levelName, 5, 0);
				num += AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10;
			}
			if (drawPadlock)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(100, 100, 100));
				this.DrawBackgroundThumbnailForLevel(g, num + Constants.QuickPlayWidget_Thumb_X, Constants.QuickPlayWidget_Thumb_Y, modeLevelCount + 1);
				g.SetColorizeImages(false);
				g.DrawImage(AtlasResources.IMAGE_LOCK_BIG, num + 95 - AtlasResources.IMAGE_LOCK_BIG.mWidth / 2, 50);
				g.DrawImage(AtlasResources.IMAGE_MINI_GAME_FRAME, num, 0);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (y > AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight)
			{
				return;
			}
			int num = x / (AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth + 10);
			if (num == this.GetModeLevelCount())
			{
				this.DisplayLockedMessage();
				return;
			}
			if (num > this.GetModeLevelCount())
			{
				return;
			}
			int gameMode = this.GetGameMode(num + 1);
			this.mListener.MiniGamesStageSelected(gameMode - 1);
		}

		public void DisplayLockedMessage()
		{
			switch (this.mMode)
			{
			case MiniGameMode.MINI_GAME_MODE_GAMES:
				if (!this.mApp.HasFinishedAdventure())
				{
					this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[FINISH_ADVENTURE_TOOLTIP]", "[DIALOG_BUTTON_OK]", "", 3, null);
					return;
				}
				this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[ONE_MORE_CHALLENGE_TOOLTIP]", "[DIALOG_BUTTON_OK]", "", 3, null);
				return;
			case MiniGameMode.MINI_GAME_MODE_I_ZOMBIE:
				if (!this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mIZombieUnlocked == 3)
				{
					this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[FINISH_ADVENTURE_TOOLTIP]", "[DIALOG_BUTTON_OK]", "", 3, null);
					return;
				}
				this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[ONE_MORE_IZOMBIE_TOOLTIP]", "[DIALOG_BUTTON_OK]", "", 3, null);
				return;
			case MiniGameMode.MINI_GAME_MODE_VASEBREAKER:
				if (!this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mVasebreakerUnlocked == 3)
				{
					this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[FINISH_ADVENTURE_TOOLTIP]", "[DIALOG_BUTTON_OK]", "", 3, null);
					return;
				}
				this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[ONE_MORE_SCARY_POTTER_TOOLTIP]", "[DIALOG_BUTTON_OK]", "", 3, null);
				return;
			default:
				return;
			}
		}

		public void SizeToFit()
		{
			if (this.GetDrawPadlock())
			{
				this.mWidth = (10 + AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth) * (this.GetModeLevelCount() + 1);
			}
			else
			{
				this.mWidth = (10 + AtlasResources.IMAGE_MINI_GAME_FRAME.mWidth) * this.GetModeLevelCount();
			}
			this.mHeight = AtlasResources.IMAGE_MINI_GAME_FRAME.mHeight + 10;
		}

		public int GetGameMode(int index)
		{
			switch (this.mMode)
			{
			case MiniGameMode.MINI_GAME_MODE_GAMES:
				return this.GetGameModeMiniGames(index);
			case MiniGameMode.MINI_GAME_MODE_I_ZOMBIE:
				return this.GetGameModeIZombie(index);
			case MiniGameMode.MINI_GAME_MODE_VASEBREAKER:
				return this.GetGameModeVasebreaker(index);
			default:
				return -1;
			}
		}

		public string GetLevelName(int index)
		{
			switch (this.mMode)
			{
			case MiniGameMode.MINI_GAME_MODE_GAMES:
				return TodStringFile.TodStringTranslate(ChallengeScreen.gChallengeDefs[index + 14].mChallengeName);
			case MiniGameMode.MINI_GAME_MODE_I_ZOMBIE:
				if (index == 10)
				{
					return TodStringFile.TodStringTranslate("[I_ZOMBIE_ENDLESS]");
				}
				return TodStringFile.TodStringTranslate("[I_ZOMBIE_" + index.ToString() + "]");
			case MiniGameMode.MINI_GAME_MODE_VASEBREAKER:
				if (index == 10)
				{
					return TodStringFile.TodStringTranslate("[SCARY_POTTER_ENDLESS]");
				}
				return TodStringFile.TodStringTranslate("[SCARY_POTTER_" + index.ToString() + "]");
			default:
				return string.Empty;
			}
		}

		public Image GetImageForMode(int index)
		{
			switch (this.mMode)
			{
			case MiniGameMode.MINI_GAME_MODE_GAMES:
				return this.GetImageForGames(index);
			case MiniGameMode.MINI_GAME_MODE_I_ZOMBIE:
				return this.GetImageForIZombie(index);
			case MiniGameMode.MINI_GAME_MODE_VASEBREAKER:
				return this.GetImageForVasebreaker(index);
			default:
				return null;
			}
		}

		public int GetGameModeVasebreaker(int index)
		{
			return index - 1 + 50;
		}

		public int GetGameModeIZombie(int index)
		{
			return index - 1 + 60;
		}

		public int GetGameModeMiniGames(int index)
		{
			switch (index)
			{
			case 1:
				return 16;
			case 2:
				return 17;
			case 3:
				return 18;
			case 4:
				return 19;
			case 5:
				return 20;
			case 6:
				return 21;
			case 7:
				return 22;
			case 8:
				return 23;
			case 9:
				return 24;
			case 10:
				return 25;
			case 11:
				return 26;
			case 12:
				return 27;
			case 13:
				return 28;
			case 14:
				return 29;
			case 15:
				return 30;
			case 16:
				return 31;
			case 17:
				return 32;
			case 18:
				return 33;
			case 19:
				return 34;
			default:
				return -1;
			}
		}

		public void DrawBackgroundThumbnailForLevel(Graphics g, int theX, int theY, int theLevel)
		{
			Image imageForMode = this.GetImageForMode(theLevel);
			if (imageForMode != null)
			{
				g.DrawImage(imageForMode, theX, theY + 2);
			}
		}

		public Image GetImageForGames(int index)
		{
			Image result = null;
			switch (index)
			{
			case 1:
				result = AtlasResources.IMAGE_MINIGAMES_ZOMBOTANY;
				break;
			case 2:
				result = AtlasResources.IMAGE_MINIGAMES_WALLNUT_BOWLING;
				break;
			case 3:
				result = AtlasResources.IMAGE_MINIGAMES_SLOT_MACHINE;
				break;
			case 4:
				result = AtlasResources.IMAGE_MINIGAMES_RAINING_SEEDS;
				break;
			case 5:
				result = AtlasResources.IMAGE_MINIGAMES_BEGHOULED;
				break;
			case 6:
				result = AtlasResources.IMAGE_MINIGAMES_INVISIBLE;
				break;
			case 7:
				result = AtlasResources.IMAGE_MINIGAMES_SEEING_STARS;
				break;
			case 8:
				result = AtlasResources.IMAGE_MINIGAMES_BEGHOULED_TWIST;
				break;
			case 9:
				result = AtlasResources.IMAGE_MINIGAMES_LITTLE_ZOMBIE;
				break;
			case 10:
				result = AtlasResources.IMAGE_MINIGAMES_PORTAL;
				break;
			case 11:
				result = AtlasResources.IMAGE_MINIGAMES_COLUMN;
				break;
			case 12:
				result = AtlasResources.IMAGE_MINIGAMES_BOBSLED_BONANZA;
				break;
			case 13:
				result = AtlasResources.IMAGE_MINIGAMES_ZOMBIE_NIMBLE;
				break;
			case 14:
				result = AtlasResources.IMAGE_MINIGAMES_WHACK_A_ZOMBIE;
				break;
			case 15:
				result = AtlasResources.IMAGE_MINIGAMES_LAST_STAND;
				break;
			case 16:
				result = AtlasResources.IMAGE_MINIGAMES_ZOMBOTANY2;
				break;
			case 17:
				result = AtlasResources.IMAGE_MINIGAMES_WALLNUT_BOWLING2;
				break;
			case 18:
				result = AtlasResources.IMAGE_MINIGAMES_POGO_PARTY;
				break;
			case 19:
				result = AtlasResources.IMAGE_MINIGAMES_ZOMBOSS;
				break;
			}
			return result;
		}

		public Image GetImageForIZombie(int index)
		{
			return AtlasResources.IMAGE_MINIGAMES_IZOMBIE;
		}

		public Image GetImageForVasebreaker(int index)
		{
			return AtlasResources.IMAGE_MINIGAMES_VASEBREAKER;
		}

		public LawnApp mApp;

		public MiniGamesWidgetListener mListener;

		public MiniGameMode mMode;
	}
}
