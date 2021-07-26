using System;

namespace Lawn
{
	public interface LawnMessageBoxListener
	{
		void LawnMessageBoxDone(int theResult);
	}
}
