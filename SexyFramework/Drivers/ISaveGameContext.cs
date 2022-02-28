using System;
using Sexy.Drivers.Profile;
using Sexy.Misc;

namespace Sexy.Drivers
{
	public abstract class ISaveGameContext
	{
		public virtual void Dispose()
		{
		}

		public abstract UserProfile GetPlayer();

		public abstract string GetSaveName();

		public abstract string GetSegmentName();

		public abstract bool IsLoading();

		public abstract bool IsSaving();

		public abstract bool IsDeleting();

		public abstract void Update();

		public abstract bool HasError();

		public abstract bool IsDone();

		public abstract SexyBuffer GetBuffer();

		public abstract void SetDisplayName(string name);

		public abstract string GetDisplayName();

		public abstract void SetDisplayDetails(string name);

		public abstract string GetDisplayDetails();

		public abstract void SetIconFilename(string iconFile);

		public abstract string GetIconFilename();

		public abstract void Destroy();
	}
}
