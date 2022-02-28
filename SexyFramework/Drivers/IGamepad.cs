using System;

namespace Sexy.Drivers
{
	public abstract class IGamepad
	{
		public virtual void Dispose()
		{
		}

		public abstract bool IsConnected();

		public abstract int GetGamepadIndex();

		public abstract bool IsButtonDown(GamepadButton button);

		public abstract float GetButtonPressure(GamepadButton button);

		public abstract float GetAxisXPosition();

		public abstract float GetAxisYPosition();

		public abstract float GetRightAxisXPosition();

		public abstract float GetRightAxisYPosition();

		public abstract void Update();

		public abstract void AddRumbleEffect(float theLeft, float theRight, float theFadeTime);
	}
}
