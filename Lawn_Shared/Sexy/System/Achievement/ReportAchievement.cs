using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;

namespace Sexy
{
	internal static class ReportAchievement
	{
		public static event ReportAchievement.AchievementHandler AchievementsChanged;

		public static int EarnedGamerScore { get; private set; }

		public static int MaxGamerScore { get; private set; }

		public static void Initialise()
		{
			SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(ReportAchievement.GamerSignedInCallback);
		}

		private static void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			SignedInGamer gamer = args.Gamer;
			if (gamer != null && ReportAchievement.gamestate == ReportAchievement.GameState.WaitingForSignIn)
			{
				ReportAchievement.gamestate = ReportAchievement.GameState.WaitingForAchivements;
				ReportAchievement.StartGetAchievements();
			}
		}

		private static void GetAchievementsCallback(IAsyncResult result)
		{
			SignedInGamer gamer = Main.GetGamer();
			if (gamer == null)
			{
				return;
			}
			lock (ReportAchievement.achievementLock)
			{
				Achievements.ClearAchievements();
				ReportAchievement.MaxGamerScore = 0;
				ReportAchievement.EarnedGamerScore = 0;
				try
				{
					ReportAchievement.achievements = gamer.EndGetAchievements(result);
					for (int i = 0; i < ReportAchievement.achievements.Count; i++)
					{
						Achievement achievement = ReportAchievement.achievements[i];
						ReportAchievement.MaxGamerScore += achievement.GamerScore;
						if (achievement.IsEarned)
						{
							ReportAchievement.EarnedGamerScore += achievement.GamerScore;
						}
						AchievementItem item = new AchievementItem(achievement);
						Achievements.AddAchievement(item);
					}
				}
				/*catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}*/
				catch (Exception ex)
				{
					string message = ex.Message;
				}
				ReportAchievement.gamestate = ReportAchievement.GameState.Ready;
			}
			if (ReportAchievement.AchievementsChanged != null)
			{
				ReportAchievement.AchievementsChanged();
			}
		}

		public static bool GiveAchievement(AchievementId achievement)
		{
			return ReportAchievement.GiveAchievement(achievement, false);
		}

		public static bool GiveAchievement(AchievementId achievement, bool forceGive)
		{
			if (!forceGive && ReportAchievement.pendingAchievements.Contains(achievement))
			{
				return false;
			}
			if (Gamer.SignedInGamers.Count == 0)
			{
				return false;
			}
			SignedInGamer gamer = Main.GetGamer();
			string achievementKey = Achievements.GetAchievementKey(achievement);
			if (achievementKey == null)
			{
				return false;
			}
			lock (ReportAchievement.achievementLock)
			{
				if (ReportAchievement.achievements == null)
				{
					return false;
				}
				foreach (Achievement achievement2 in ReportAchievement.achievements)
				{
					if (achievement2.Key == achievementKey && achievement2.IsEarned)
					{
						return false;
					}
				}
				if (!SexyAppBase.IsInTrialMode)
				{
					gamer.BeginAwardAchievement(achievementKey, new AsyncCallback(ReportAchievement.AwardingAchievementCallback), null);
				}
				if (!ReportAchievement.pendingAchievements.Contains(achievement))
				{
					ReportAchievement.pendingAchievements.Add(achievement);
				}
				if (SexyAppBase.IsInTrialMode)
				{
					ReportAchievement.pendingAchievementAlerts.Enqueue(new TrialAchievementAlert(achievement));
				}
			}
			if (ReportAchievement.AchievementsChanged != null)
			{
				ReportAchievement.AchievementsChanged();
			}
			return true;
		}

		public static void GivePendingAchievements()
		{
			if (ReportAchievement.pendingAchievementAlerts.Count > 0)
			{
				GlobalStaticVars.gSexyAppBase.ShowAchievementMessage(ReportAchievement.pendingAchievementAlerts.Dequeue());
			}
		}

		private static void AwardingAchievementCallback(IAsyncResult result)
		{
			SignedInGamer gamer = Main.GetGamer();
			if (gamer != null)
			{
				gamer.EndAwardAchievement(result);
				ReportAchievement.StartGetAchievements();
			}
		}

		public static void StartGetAchievements()
		{
			if (Main.GetGamer() == null)
			{
				return;
			}
			SignedInGamer gamer = Main.GetGamer();
			gamer.BeginGetAchievements(new AsyncCallback(ReportAchievement.GetAchievementsCallback), gamer);
		}

		private static Queue<TrialAchievementAlert> pendingAchievementAlerts = new Queue<TrialAchievementAlert>(10);

		public static object achievementLock = new object();

		private static List<AchievementId> pendingAchievements = new List<AchievementId>();

		private static AchievementCollection achievements;

		private static ReportAchievement.GameState gamestate = ReportAchievement.GameState.WaitingForSignIn;

		public delegate void AchievementHandler();

		private enum GameState
		{
			Error,
			WaitingForSignIn,
			WaitingForAchivements,
			Ready
		}
	}
}
