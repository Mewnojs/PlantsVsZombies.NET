using System;

namespace Sexy
{
	public class IFBDialogListener
	{
		public virtual void DialogDidComplete(string name, StructuredData results)
		{
		}

		public virtual void DialogWasCanceled(string name)
		{
		}

		public virtual void DialogDidFail(string name, StructuredData error)
		{
		}

		public virtual bool DialogShouldOpenURLInExternalBrowser(string name, string url)
		{
			return false;
		}
	}
}
