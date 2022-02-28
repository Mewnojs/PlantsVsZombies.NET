using System;

namespace Sexy
{
	public class SongChangedEventArgs : EventArgs
	{
		public int songID;

		public bool loop;
	}
}
