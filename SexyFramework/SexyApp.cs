using System;

namespace Sexy
{
	public class SexyApp : SexyAppBase
	{
		public SexyApp()
		{
			GlobalMembers.gSexyApp = this;
		}

		public override void Dispose()
		{
			GlobalMembers.gSexyApp = null;
		}

		public void WriteRegistrationInfo(string theRegUser, string theRegCode, int theTimesPlayed, int theTimesExecuted)
		{
		}

		public void ReadRegistrationInfo(ref string theUser, ref string theKey, ref int theTimesPlayed, ref int theTimesExecuted)
		{
		}

		public bool Validate(string theUserName, string theRegCode)
		{
			return true;
		}

		public override void UpdateFrames()
		{
			base.UpdateFrames();
		}

		public virtual bool ShouldCheckForUpdate()
		{
			return false;
		}

		public virtual void UpdateCheckQueried()
		{
		}

		public virtual bool OpenRegisterPage(DefinesMap theDefinesMap)
		{
			return false;
		}

		public virtual bool OpenRegisterPage()
		{
			return false;
		}

		public override void Init()
		{
			base.Init();
		}

		public virtual bool OpenHTMLTemplate(string theTemplateFile, DefinesMap theDefinesMap)
		{
			return false;
		}

		public virtual void OpenUpdateURL()
		{
		}

		public virtual void HandleNotifyGameMessageCommandLine(string theCommandLine)
		{
		}

		public virtual void GetSEHWebParams(ref DefinesMap theDefinesMap)
		{
		}
	}
}
