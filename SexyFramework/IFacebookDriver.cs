using System;

namespace Sexy
{
	public abstract class IFacebookDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract void InitWithAppId(string app_id);

		public abstract void SetUserDataFields(string userDataFields);

		public abstract void Resume(IFBSessionListener listener);

		public abstract void Authorize(string permissions, IFBSessionListener listener);

		public abstract void Logout(IFBSessionListener listener);

		public abstract void Update();

		public abstract int IsAuthorized();

		public abstract int IsAuthorizing();

		public abstract string GetUserId();

		public abstract StructuredData GetUserData();

		public abstract string GetAccessToken();

		public abstract int GetExpirationDate();

		public abstract NetworkServiceProfile ServiceProfile();

		public abstract void Dialog(string name, IFBDialogListener listener);

		public abstract void Dialog(string name, StructuredData @params, IFBDialogListener listener);
	}
}
