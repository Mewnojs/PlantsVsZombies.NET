using System;

namespace Sexy.PIL
{
	public class EmitterUpdatePair
	{
		public EmitterUpdatePair(Emitter emitter, int value)
		{
			this.emitter = emitter;
			this.value = value;
		}

		public Emitter emitter;

		public int value;
	}
}
