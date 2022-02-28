using System;

namespace Sexy.Misc
{
	public interface SexyMatrix3
	{
		float m00 { get; set; }

		float m01 { get; set; }

		float m02 { get; set; }

		float m10 { get; set; }

		float m11 { get; set; }

		float m12 { get; set; }

		float m20 { get; set; }

		float m21 { get; set; }

		float m22 { get; set; }

		void ZeroMatrix();

		void LoadIdentity();
	}
}
