using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public interface SexyAppBaseInterface
	{
		void Init();

		bool UpdateApp();

		void UpdateFrames();

		void DrawGame(GameTime gameTime);

		void StartLoadingThread();

		void LoadingThreadCompleted();

		void LoadingThreadProcStub();

		void InitHook();

		void DeviceOrientationChanged(UI_ORIENTATION toOrientation);

		void AccelerometerDidAccelerate(double timestamp, double ax, double ay, double az);
	}
}
