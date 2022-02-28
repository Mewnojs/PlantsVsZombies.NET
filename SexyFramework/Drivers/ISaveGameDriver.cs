using System;
using Sexy.Drivers.Profile;
using Sexy.Misc;

namespace Sexy.Drivers
{
	public abstract class ISaveGameDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract bool Init();

		public abstract void Update();

		public abstract ISaveGameContext CreateSaveGameContext(UserProfile player, string saveName, ulong requiredBytes);

		public abstract bool BeginLoad(ISaveGameContext context, string segment, bool checkOnly);

		public abstract bool BeginSave(ISaveGameContext context, string segment, SexyBuffer data);

		public abstract bool BeginDelete(ISaveGameContext context, string segment);

		public abstract bool BeginSaveGameDelete(ISaveGameContext context);
	}
}
