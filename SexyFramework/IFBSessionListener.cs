using System;

namespace Sexy
{
	public class IFBSessionListener
	{
		public virtual void FacebookDidLogin(IFacebookDriver facebook)
		{
		}

		public virtual void FacebookDidNotLogin(IFacebookDriver facebook, int canceled)
		{
		}

		public virtual void FacebookDidLogout(IFacebookDriver facebook)
		{
		}
	}
}
