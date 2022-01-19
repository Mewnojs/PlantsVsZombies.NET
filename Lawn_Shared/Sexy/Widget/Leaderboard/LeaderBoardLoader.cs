using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;

namespace Sexy
{
    internal class LeaderBoardLoader
    {
        public LeaderBoardLoader.ErrorStates ErrorState { get; private set; }

        public int SignedInGamerIndex { get; private set; }

        public int GameMode { get; private set; }

        public event LeaderBoardLoader.LoadingCompletedhandler LoadingCompleted;

        public LeaderBoardLoader.LoaderState LeaderboardConnectionState
        {
            get
            {
                if ((DateTime.UtcNow - resultsReceived).TotalSeconds < CACHE_DURATION)
                {
                    return LeaderBoardLoader.LoaderState.Loaded;
                }
                if ((DateTime.UtcNow - requestSendTime).TotalSeconds < 30.0)
                {
                    return LeaderBoardLoader.LoaderState.Loading;
                }
                return LeaderBoardLoader.LoaderState.Idle;
            }
        }

        public void ResetCache()
        {
            resultsReceived = DateTime.MinValue;
        }

        public void SendRequest()
        {
            if (Gamer.SignedInGamers.Count == 0)
            {
                return;
            }
            SignedInGamer gamer = Main.GetGamer();
            if (LeaderboardConnectionState != LeaderBoardLoader.LoaderState.Idle)
            {
                return;
            }
            try
            {
                LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, GameMode);
                LeaderboardReader.BeginRead(leaderboardId, gamer, 5, new AsyncCallback(RequestReceived), gamer);
            }
            catch (GameUpdateRequiredException)
            {
                GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetResultsCallBack. {0}", ex.Message);
            }
            requestSendTime = DateTime.UtcNow;
        }

        public void LoadEntry(int index)
        {
            if (index < reader.PageStart)
            {
                if (!pagingUp && reader.CanPageUp)
                {
                    pagingUp = true;
                    reader.BeginPageUp(new AsyncCallback(PageUpRequestReceived), Main.GetGamer());
                    return;
                }
            }
            else if (!pagingDown && reader.CanPageDown)
            {
                pagingDown = true;
                reader.BeginPageDown(new AsyncCallback(PageDownRequestReceived), Main.GetGamer());
            }
        }

        private void PageUpRequestReceived(IAsyncResult result)
        {
            pagingUp = false;
            ProcessData(result, false);
        }

        private void PageDownRequestReceived(IAsyncResult result)
        {
            pagingDown = false;
            ProcessData(result, false);
        }

        private void RequestReceived(IAsyncResult result)
        {
            ProcessData(result, true);
        }

        private void ProcessData(IAsyncResult result, bool clearList)
        {
            requestSendTime = DateTime.MinValue;
            lock (LeaderBoardComm.LeaderboardLock)
            {
                try
                {
                    reader = LeaderboardReader.EndRead(result);
                    LeaderboardEntryCount = reader.TotalLeaderboardSize;
                    if (clearList)
                    {
                        LeaderboardEntries.Clear();
                    }
                    UpdateEntriesFromReader();
                    resultsReceived = DateTime.UtcNow;
                }
                catch (GameUpdateRequiredException)
                {
                    ErrorState = LeaderBoardLoader.ErrorStates.GameUpdateRequired;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    Console.WriteLine("Error in RequestReceived in LeaderBoardLoader. {0}", ex.Message);
                    if (LeaderboardEntries.Count == 0)
                    {
                        ErrorState = LeaderBoardLoader.ErrorStates.Error;
                    }
                    else
                    {
                        ErrorState = LeaderBoardLoader.ErrorStates.None;
                    }
                }
            }
            if (LoadingCompleted != null)
            {
                LoadingCompleted(this);
            }
        }

        private void UpdateEntriesFromReader()
        {
            Gamer gamer = Main.GetGamer();
            for (int i = 0; i < reader.Entries.Count; i++)
            {
                int num = reader.PageStart + i;
                LeaderboardEntry leaderboardEntry = reader.Entries[i];
                if (LeaderboardEntries.ContainsKey(num))
                {
                    LeaderboardEntries[num] = leaderboardEntry;
                }
                else
                {
                    LeaderboardEntries.Add(num, leaderboardEntry);
                }
                if (gamer != null && leaderboardEntry.Gamer.Gamertag == gamer.Gamertag)
                {
                    SignedInGamerIndex = i;
                }
            }
        }

        public LeaderBoardLoader(LeaderboardGameMode gameMode)
        {
            ErrorState = LeaderBoardLoader.ErrorStates.None;
            GameMode = LeaderBoardHelper.GetLeaderboardNumber(gameMode);
        }

        private const int REQUEST_RESEND_DELAY = 30;

        private const int PAGE_SIZE = 5;

        public int CACHE_DURATION = int.MaxValue;

        public Dictionary<int, LeaderboardEntry> LeaderboardEntries = new Dictionary<int, LeaderboardEntry>(100);

        public int LeaderboardEntryCount;

        private DateTime requestSendTime = DateTime.MinValue;

        private DateTime resultsReceived = DateTime.MinValue;

        private bool pagingUp;

        private bool pagingDown;

        private LeaderboardReader reader;

        public enum LoaderState
        {
            Idle,
            Loading,
            Loaded
        }

        public enum ErrorStates
        {
            None,
            GameUpdateRequired,
            Error
        }

        public delegate void LoadingCompletedhandler(LeaderBoardLoader loader);
    }
}
