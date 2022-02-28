using System;

namespace Sexy.Drivers
{
	public class IFileSearch
	{
		public virtual void Dispose()
		{
		}

		public IFileSearch.SearchType GetSearchType()
		{
			return this.mSearchType;
		}

		protected IFileSearch()
		{
			this.mSearchType = IFileSearch.SearchType.UNKNOWN;
		}

		protected IFileSearch.SearchType mSearchType;

		public enum SearchType
		{
			UNKNOWN,
			PAK_FILE_INTERNAL,
			DRIVER_INTERNAL
		}
	}
}
