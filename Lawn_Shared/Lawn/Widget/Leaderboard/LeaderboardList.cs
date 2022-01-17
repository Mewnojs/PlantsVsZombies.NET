using System;
using Microsoft.Xna.Framework.GamerServices;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class LeaderboardList : Widget
	{
		public LeaderboardList(LawnApp theApp, LeaderBoardType aType)
		{
			mApp = theApp;
			switch (aType)
			{
			case LeaderBoardType.Izombie:
				mLeaderboardState = LeaderboardState.IZombie;
				mGameMode = LeaderboardGameMode.IZombie;
				break;
			case LeaderBoardType.Vasebreaker:
				mLeaderboardState = LeaderboardState.Vasebreaker;
				mGameMode = LeaderboardGameMode.Vasebreaker;
				break;
			case LeaderBoardType.Killed:
				mLeaderboardState = LeaderboardState.Adventure;
				mGameMode = LeaderboardGameMode.Adventure;
				break;
			}
			mWidth = AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth;
			int num = LeaderBoardComm.LoadResults(mGameMode);
			mHeight = num * 20 + num * AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mHeight;
			LeaderBoardComm.SetCache(mGameMode);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			g.HardwareClip();
			if (LeaderBoardComm.LoadResults(mGameMode) >= 0)
			{
				int num = LeaderBoardComm.LoadResults(mGameMode);
				for (int i = 0; i < num; i++)
				{
					int num2 = i * 20 + i * AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mHeight;
					g.DrawImage(AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER, 0, num2);
					long leaderboardScore = LeaderBoardComm.GetLeaderboardScore(i, mLeaderboardState);
					Gamer leaderboardGamer = LeaderBoardComm.GetLeaderboardGamer(i, mLeaderboardState);
					TodCommon.TodDrawString(g, LawnApp.ToString(i + 1), 30, num2 + 25, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, 150, DrawStringJustification.Left);
					TodCommon.TodDrawString(g, LawnApp.ToString((int)leaderboardScore), AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_BANNER.mWidth - 20, num2 + 25, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Right);
					if (leaderboardGamer != null)
					{
						TodCommon.TodDrawString(g, leaderboardGamer.Gamertag, 150, num2 + 25, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, 150, DrawStringJustification.Left);
						Image gamerImage = LeaderBoardComm.GetGamerImage(leaderboardGamer);
						if (gamerImage != null)
						{
							g.DrawImage(gamerImage, 69, num2 + 12);
						}
					}
					else
					{
						TodCommon.TodDrawString(g, "GAMERTAG", 150, num2 + 25, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, 150, DrawStringJustification.Left);
					}
				}
			}
			g.EndHardwareClip();
		}

		public LawnApp mApp;

		public LeaderboardState mLeaderboardState;

		public LeaderboardGameMode mGameMode;
	}
}
