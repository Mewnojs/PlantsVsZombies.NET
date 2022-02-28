using System;
using Microsoft.Xna.Framework.Content;

namespace Sexy.Drivers.App
{
	public class WP7ContentManager : ContentManager
	{
		public WP7ContentManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			this.mCustom = new Action<IDisposable>(this.CustomDispose<IDisposable>);
		}

		public T LoadResDirectly<T>(string name)
		{
			return base.ReadAsset<T>(name, this.mCustom);
		}

		public void CustomDispose<IDisposable>(IDisposable obj)
		{
		}

		private Action<IDisposable> mCustom;
	}
}
