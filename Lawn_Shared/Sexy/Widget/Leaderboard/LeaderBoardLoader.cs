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
				if ((DateTime.UtcNow - this.resultsReceived).TotalSeconds < (double)this.CACHE_DURATION)
				{
					return LeaderBoardLoader.LoaderState.Loaded;
				}
				if ((DateTime.UtcNow - this.requestSendTime).TotalSeconds < 30.0)
				{
					return LeaderBoardLoader.LoaderState.Loading;
				}
				return LeaderBoardLoader.LoaderState.Idle;
			}
		}

		public void ResetCache()
		{
			this.resultsReceived = DateTime.MinValue;
		}

		public void SendRequest()
		{
			if (Gamer.SignedInGamers.Count == 0)
			{
				return;
			}
			SignedInGamer gamer = Main.GetGamer();
			if (this.LeaderboardConnectionState != LeaderBoardLoader.LoaderState.Idle)
			{
				return;
			}
			try
			{
				LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, this.GameMode);
				LeaderboardReader.BeginRead(leaderboardId, gamer, 5, new AsyncCallback(this.RequestReceived), gamer);
			}
			catch (GameUpdateRequiredException)
			{
				GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error in GetResultsCallBack. {0}", ex.Message);
			}
			this.requestSendTime = DateTime.UtcNow;
		}

		public void LoadEntry(int index)
		{
			if (index < this.reader.PageStart)
			{
				if (!this.pagingUp && this.reader.CanPageUp)
				{
					this.pagingUp = true;
					this.reader.BeginPageUp(new AsyncCallback(this.PageUpRequestReceived), Main.GetGamer());
					return;
				}
			}
			else if (!this.pagingDown && this.reader.CanPageDown)
			{
				this.pagingDown = true;
				this.reader.BeginPageDown(new AsyncCallback(this.PageDownRequestReceived), Main.GetGamer());
			}
		}

		private void PageUpRequestReceived(IAsyncResult result)
		{
			this.pagingUp = false;
			this.ProcessData(result, false);
		}

		private void PageDownRequestReceived(IAsyncResult result)
		{
			this.pagingDown = false;
			this.ProcessData(result, false);
		}

		private void RequestReceived(IAsyncResult result)
		{
			this.ProcessData(result, true);
		}

		private void ProcessData(IAsyncResult result, bool clearList)
		{
			this.requestSendTime = DateTime.MinValue;
			lock (LeaderBoardComm.LeaderboardLock)
			{
				try
				{
					this.reader = LeaderboardReader.EndRead(result);
					this.LeaderboardEntryCount = this.reader.TotalLeaderboardSize;
					if (clearList)
					{
						this.LeaderboardEntries.Clear();
					}
					this.UpdateEntriesFromReader();
					this.resultsReceived = DateTime.UtcNow;
				}
				catch (GameUpdateRequiredException)
				{
					this.ErrorState = LeaderBoardLoader.ErrorStates.GameUpdateRequired;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
					Console.WriteLine("Error in RequestReceived in LeaderBoardLoader. {0}", ex.Message);
					if (this.LeaderboardEntries.Count == 0)
					{
						this.ErrorState = LeaderBoardLoader.ErrorStates.Error;
					}
					else
					{
						this.ErrorState = LeaderBoardLoader.ErrorStates.None;
					}
				}
			}
			if (this.LoadingCompleted != null)
			{
				this.LoadingCompleted(this);
			}
		}

		private void UpdateEntriesFromReader()
		{
			Gamer gamer = Main.GetGamer();
			for (int i = 0; i < this.reader.Entries.Count; i++)
			{
				int num = this.reader.PageStart + i;
				LeaderboardEntry leaderboardEntry = this.reader.Entries[i];
				if (this.LeaderboardEntries.ContainsKey(num))
				{
					this.LeaderboardEntries[num] = leaderboardEntry;
				}
				else
				{
					this.LeaderboardEntries.Add(num, leaderboardEntry);
				}
				if (gamer != null && leaderboardEntry.Gamer.Gamertag == gamer.Gamertag)
				{
					this.SignedInGamerIndex = i;
				}
			}
		}

		public LeaderBoardLoader(LeaderboardGameMode gameMode)
		{
			this.ErrorState = LeaderBoardLoader.ErrorStates.None;
			this.GameMode = LeaderBoardHelper.GetLeaderboardNumber(gameMode);
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
