using System;
using Microsoft.Xna.Framework.Input;
using Sexy.Drivers;

namespace Sexy
{
	internal class XNAGamepad : IGamepad
	{
		public override bool IsConnected()
		{
			return false;
		}

		public override int GetGamepadIndex()
		{
			return 0;
		}

		public override bool IsButtonDown(GamepadButton button)
		{
			return XNAGamepad.mGamepadData.mButton[(int)button];
		}

		public override float GetButtonPressure(GamepadButton button)
		{
			return 0f;
		}

		public override float GetAxisXPosition()
		{
			return 0f;
		}

		public override float GetAxisYPosition()
		{
			return 0f;
		}

		public override float GetRightAxisXPosition()
		{
			return 0f;
		}

		public override float GetRightAxisYPosition()
		{
			return 0f;
		}

		public override void Update()
		{
			if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
			{
				XNAGamepad.mGamepadData.mButton[4] = true;
			}
			if (GamePad.GetState(0).Buttons.Back == ButtonState.Released)
			{
				XNAGamepad.mGamepadData.mButton[4] = false;
			}
		}

		public override void AddRumbleEffect(float theLeft, float theRight, float theFadeTime)
		{
		}

		public static GamepadData mGamepadData = new GamepadData();
	}
}
