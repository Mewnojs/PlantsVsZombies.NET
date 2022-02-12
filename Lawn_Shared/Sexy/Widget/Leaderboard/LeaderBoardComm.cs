using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
    internal static class LeaderBoardComm
    {
        public static LeaderBoardComm.ConnectionState State { get; private set; } = LeaderBoardComm.ConnectionState.Connecting;

        public static event LeaderBoardComm.LoadingCompletedHandler LoadingCompleted;

        public static Image UnknownPlayerImage
        {
            get
            {
                return null;
            }
        }

        static LeaderBoardComm()
        {
            for (int i = 0; i < 3; i++)
            {
                if (LeaderBoardHelper.IsModeSupported((LeaderboardGameMode)i))
                {
                    LeaderBoardComm.leaderboardLoaders[i] = new LeaderBoardLoader((LeaderboardGameMode)i);
                    LeaderBoardComm.leaderboardLoaders[i].LoadingCompleted += LeaderBoardComm.GetResultsCallBack;
                }
            }
        }

        public static void RecordResult(LeaderboardGameMode gameMode, int score)
        {
            if (!SexyAppBase.UseLiveServers)
            {
                return;
            }
            if (SexyAppBase.IsInTrialMode)
            {
                return;
            }
            if (!LeaderBoardHelper.IsModeSupported(gameMode))
            {
                return;
            }
            if (Gamer.SignedInGamers.Count == 0)
            {
                return;
            }
            SignedInGamer gamer = Main.GetGamer();
            lock (LeaderBoardComm.LeaderboardLock)
            {
                try
                {
                    int leaderboardNumber = LeaderBoardHelper.GetLeaderboardNumber(gameMode);
                    LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, leaderboardNumber);
                    LeaderboardWriter leaderboardWriter = gamer.LeaderboardWriter;
                    LeaderboardEntry leaderboard = leaderboardWriter.GetLeaderboard(leaderboardId);
                    leaderboard.Rating = score;
                    foreach (LeaderBoardLoader leaderBoardLoader in LeaderBoardComm.leaderboardLoaders)
                    {
                        leaderBoardLoader.ResetCache();
                    }
                }
                catch (GameUpdateRequiredException)
                {
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool IsPlayer(Gamer signedInGamer, int index, LeaderboardState state)
        {
            if (!SexyAppBase.UseLiveServers)
            {
                return false;
            }
            lock (LeaderBoardComm.LeaderboardLock)
            {
                try
                {
                    if (signedInGamer == null)
                    {
                        return false;
                    }
                    LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
                    if (entry != null)
                    {
                        return signedInGamer.Gamertag == entry.Gamer.Gamertag;
                    }
                }
                catch (GameUpdateRequiredException)
                {
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public static void LoadInitialLeaderboard()
        {
            for (int i = 0; i < 3; i++)
            {
                LeaderBoardComm.LoadResults((LeaderboardGameMode)i);
            }
        }

        public static int GetMaxEntries(LeaderboardState state)
        {
            LeaderBoardLoader loader = LeaderBoardComm.GetLoader(state);
            return loader.LeaderboardEntryCount;
        }

        private static void GetGamerCallBack(IAsyncResult result)
        {
            lock (LeaderBoardComm.LeaderboardLock)
            {
                try
                {
                    Gamer gamer = result.AsyncState as Gamer;
                    if (gamer != null)
                    {
                        if (LeaderBoardComm.gamerImages.ContainsKey(gamer.Gamertag))
                        {
                            LeaderBoardComm.gamerImages.Remove(gamer.Gamertag);
                        }
                        GamerProfile gamerProfile = gamer.EndGetProfile(result);
                        //Texture2D theTexture = Texture2D.FromStream(GlobalStaticVars.g.GraphicsDevice, gamerProfile.GetGamerPicture());
                        //Image image = new Image(theTexture);
                        //LeaderBoardComm.gamerImages.Add(gamer.Gamertag, image);
                    }
                }
                catch (GameUpdateRequiredException)
                {
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                }
                catch (Exception)
                {
                }
            }
        }

        public static Image GetGamerImage(Gamer gamer)
        {
            Image unknownPlayerImage = LeaderBoardComm.UnknownPlayerImage;
            if (!SexyAppBase.UseLiveServers || gamer == null)
            {
                return LeaderBoardComm.UnknownPlayerImage;
            }
            lock (LeaderBoardComm.LeaderboardLock)
            {
                if (!LeaderBoardComm.gamerImages.TryGetValue(gamer.Gamertag, out unknownPlayerImage))
                {
                    unknownPlayerImage = LeaderBoardComm.UnknownPlayerImage;
                    LeaderBoardComm.gamerImages.Add(gamer.Gamertag, unknownPlayerImage);
                    try
                    {
                        gamer.BeginGetProfile(new AsyncCallback(LeaderBoardComm.GetGamerCallBack), gamer);
                    }
                    catch (GameUpdateRequiredException)
                    {
                        GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                    }
                    catch (Exception ex)
                    {
                        LeaderBoardComm.e = ex.Message;
                    }
                }
            }
            return unknownPlayerImage;
        }

        public static Image GetLeaderboardGamerImage(int index, LeaderboardState state)
        {
            if (!SexyAppBase.UseLiveServers)
            {
                return LeaderBoardComm.UnknownPlayerImage;
            }
            Image gamerImage;
            lock (LeaderBoardComm.LeaderboardLock)
            {
                Gamer gamer = null;
                try
                {
                    LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
                    if (entry != null)
                    {
                        gamer = entry.Gamer;
                    }
                }
                catch (GameUpdateRequiredException)
                {
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                }
                catch (Exception)
                {
                }
                gamerImage = LeaderBoardComm.GetGamerImage(gamer);
            }
            return gamerImage;
        }

        public static Gamer GetLeaderboardGamer(int index, LeaderboardState state)
        {
            if (!SexyAppBase.UseLiveServers)
            {
                return null;
            }
            Gamer result;
            lock (LeaderBoardComm.LeaderboardLock)
            {
                Gamer gamer = null;
                try
                {
                    LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
                    if (entry != null)
                    {
                        gamer = entry.Gamer;
                    }
                }
                catch (GameUpdateRequiredException)
                {
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                }
                catch (Exception)
                {
                }
                result = gamer;
            }
            return result;
        }

        public static int GetSignedInGamerIndex(LeaderboardState state)
        {
            LeaderBoardLoader loader = LeaderBoardComm.GetLoader(state);
            return loader.SignedInGamerIndex;
        }

        private static LeaderBoardLoader GetLoader(LeaderboardState state)
        {
            return LeaderBoardComm.leaderboardLoaders[LeaderBoardHelper.GetLeaderboardNumber(state)];
        }

        private static LeaderBoardLoader GetLoader(LeaderboardGameMode mode)
        {
            return LeaderBoardComm.leaderboardLoaders[LeaderBoardHelper.GetLeaderboardNumber(mode)];
        }

        private static LeaderboardEntry GetEntry(int index, LeaderboardState state)
        {
            LeaderBoardLoader leaderBoardLoader = LeaderBoardComm.leaderboardLoaders[LeaderBoardHelper.GetLeaderboardNumber(state)];
            LeaderboardEntry result;
            if (!leaderBoardLoader.LeaderboardEntries.TryGetValue(index, out result))
            {
                leaderBoardLoader.LoadEntry(index);
            }
            return result;
        }

        public static long GetLeaderboardScore(int index, LeaderboardState state)
        {
            if (!SexyAppBase.UseLiveServers)
            {
                return 0L;
            }
            lock (LeaderBoardComm.LeaderboardLock)
            {
                try
                {
                    LeaderboardEntry entry = LeaderBoardComm.GetEntry(index, state);
                    if (entry != null)
                    {
                        return entry.Rating;
                    }
                }
                catch (GameUpdateRequiredException)
                {
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                }
                catch (Exception)
                {
                }
            }
            return 0L;
        }

        public static void SetCache(LeaderboardGameMode gameMode)
        {
            LeaderBoardLoader loader = LeaderBoardComm.GetLoader(gameMode);
            loader.CACHE_DURATION = 10;
        }

        public static int LoadResults(LeaderboardGameMode gameMode)
        {
            /*if (!SexyAppBase.UseLiveServers || LeaderBoardComm.State == LeaderBoardComm.ConnectionState.CannotConnect)
            {
                if (LeaderBoardComm.State == LeaderBoardComm.ConnectionState.CannotConnect && (DateTime.UtcNow - LeaderBoardComm.cannotConnectSince).TotalSeconds > 2147483647.0)
                {
                    LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connecting;
                }
                return -2;
            }*/
            lock (LeaderBoardComm.LeaderboardLock)
            {
                //if (Gamer.SignedInGamers.Count == 0)
                //{
                //    return -2;
                //}
                LeaderBoardLoader loader = LeaderBoardComm.GetLoader(gameMode);
                loader.SendRequest();
                LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connecting;
                if (loader.LeaderboardConnectionState == LeaderBoardLoader.LoaderState.Loaded || (loader.LeaderboardConnectionState == LeaderBoardLoader.LoaderState.Loading && loader.LeaderboardEntryCount > 0))
                {
                    loader.CACHE_DURATION = int.MaxValue;
                    return loader.LeaderboardEntryCount;
                }
                loader.CACHE_DURATION = int.MaxValue;
            }
            return -1;
        }

        private static void GetResultsCallBack(LeaderBoardLoader loader)
        {
            lock (LeaderBoardComm.LeaderboardLock)
            {
                switch (loader.ErrorState)
                {
                case LeaderBoardLoader.ErrorStates.None:
                    LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connected;
                    break;
                case LeaderBoardLoader.ErrorStates.GameUpdateRequired:
                    GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
                    break;
                case LeaderBoardLoader.ErrorStates.Error:
                    LeaderBoardComm.State = LeaderBoardComm.ConnectionState.CannotConnect;
                    LeaderBoardComm.cannotConnectSince = DateTime.UtcNow;
                    break;
                }
                if (LeaderBoardComm.LoadingCompleted != null)
                {
                    LeaderBoardComm.LoadingCompleted();
                }
            }
        }

        private const int cannotConnectDelay = 2147483647;

        public static object LeaderboardLock = new object();

        private static DateTime cannotConnectSince;

        private static string[] columnIndexStrings = new string[]
        {
            "SCORE"
        };

        private static LeaderBoardLoader[] leaderboardLoaders = new LeaderBoardLoader[3];

        private static Dictionary<string, Image> gamerImages = new Dictionary<string, Image>();

        private static string e;

        private static List<Gamer> loadingGamers = new List<Gamer>();

        public enum ConnectionState
        {
            Connected,
            Connecting,
            CannotConnect
        }

        private enum ColumnIndices
        {
            Score
        }

        public delegate void LoadingCompletedHandler();
    }

    public enum LeaderboardGameMode
    {
        Adventure,
        IZombie,
        Vasebreaker,
        Max
    }

    internal enum LeaderboardState
    {
        Adventure,
        IZombie,
        Vasebreaker
    }
}
