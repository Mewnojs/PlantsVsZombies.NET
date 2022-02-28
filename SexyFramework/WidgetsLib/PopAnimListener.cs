using System;
using Sexy.GraphicsLib;

namespace Sexy.WidgetsLib
{
	public interface PopAnimListener
	{
		void PopAnimPlaySample(string theSampleName, int thePan, double theVolume, double theNumSteps);

		PIEffect PopAnimLoadParticleEffect(string theEffectName);

		bool PopAnimObjectPredraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, SexyColor theColor);

		bool PopAnimObjectPostdraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, SexyColor theColor);

		ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, Graphics g, int theDrawCount);

		void PopAnimStopped(int theId);

		void PopAnimCommand(int theId, string theCommand, string theParam);

		bool PopAnimCommand(int theId, PASpriteInst theSpriteInst, string theCommand, string theParam);
	}
}
