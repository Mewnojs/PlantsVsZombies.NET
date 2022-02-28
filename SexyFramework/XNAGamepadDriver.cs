using System;
using Sexy.Drivers;

namespace Sexy
{
	internal class XNAGamepadDriver : IGamepadDriver
	{
		public static IGamepadDriver CreateGamepadDriver()
		{
			XNAGamepadDriver.mXNAGamepad = new XNAGamepad();
			return new XNAGamepadDriver();
		}

		public override void Dispose()
		{
		}

		public override int InitGamepadDriver(SexyAppBase app)
		{
			this.gApp = app;
			return 1;
		}

		public override IGamepad GetGamepad(int theIndex)
		{
			return XNAGamepadDriver.mXNAGamepad;
		}

		public override void Update()
		{
			XNAGamepadDriver.mXNAGamepad.Update();
		}

		private static XNAGamepad mXNAGamepad;

		private SexyAppBase gApp;
	}
}
