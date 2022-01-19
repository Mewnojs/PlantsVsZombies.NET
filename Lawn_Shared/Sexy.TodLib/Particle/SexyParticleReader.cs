using System;
using Microsoft.Xna.Framework.Content;

namespace Sexy.TodLib
{
    internal class SexyParticleReader : ContentTypeReader<TodParticleDefinition>
    {
        protected override TodParticleDefinition Read(ContentReader input, TodParticleDefinition existingInstance)
        {
            TodParticleDefinition todParticleDefinition = new TodParticleDefinition();
            todParticleDefinition.mEmitterDefCount = input.ReadInt32();
            todParticleDefinition.mEmitterDefs = new TodEmitterDefinition[todParticleDefinition.mEmitterDefCount];
            for (int i = 0; i < todParticleDefinition.mEmitterDefCount; i++)
            {
                todParticleDefinition.mEmitterDefs[i] = new TodEmitterDefinition();
            }
            for (int j = 0; j < todParticleDefinition.mEmitterDefCount; j++)
            {
                TodEmitterDefinition todEmitterDefinition = todParticleDefinition.mEmitterDefs[j];
                string theStringId = input.ReadString();
                todEmitterDefinition.mImage = AtlasResources.GetImageInAtlasById(AtlasResources.GetAtlasIdByStringId(theStringId));
                todEmitterDefinition.mImageCol = input.ReadInt32();
                todEmitterDefinition.mImageRow = input.ReadInt32();
                todEmitterDefinition.mImageFrames = input.ReadInt32();
                todEmitterDefinition.mAnimated = input.ReadInt32();
                todEmitterDefinition.mParticleFlags = input.ReadInt32();
                todEmitterDefinition.mEmitterType = (EmitterType)input.ReadInt32();
                todEmitterDefinition.mName = input.ReadString();
                todEmitterDefinition.mOnDuration = input.ReadString();
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemDuration);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mCrossFadeDuration);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSpawnRate);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSpawnMinActive);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSpawnMaxActive);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSpawnMaxLaunched);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterRadius);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterOffsetX);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterOffsetY);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterBoxX);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterBoxY);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterSkewX);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterSkewY);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mEmitterPath);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleDuration);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mLaunchSpeed);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mLaunchAngle);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemRed);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemGreen);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemBlue);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemAlpha);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemBrightness);
                todEmitterDefinition.mParticleFieldCount = input.ReadInt32();
                todEmitterDefinition.mParticleFields = new ParticleField[todEmitterDefinition.mParticleFieldCount];
                for (int k = 0; k < todEmitterDefinition.mParticleFieldCount; k++)
                {
                    todEmitterDefinition.mParticleFields[k] = new ParticleField();
                }
                for (int l = 0; l < todEmitterDefinition.mParticleFieldCount; l++)
                {
                    ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleFields[l].mX);
                    ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleFields[l].mY);
                    todEmitterDefinition.mParticleFields[l].mFieldType = (ParticleFieldType)input.ReadInt32();
                }
                todEmitterDefinition.mSystemFieldCount = input.ReadInt32();
                todEmitterDefinition.mSystemFields = new ParticleField[todEmitterDefinition.mSystemFieldCount];
                for (int m = 0; m < todEmitterDefinition.mSystemFieldCount; m++)
                {
                    todEmitterDefinition.mSystemFields[m] = new ParticleField();
                }
                for (int n = 0; n < todEmitterDefinition.mSystemFieldCount; n++)
                {
                    ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemFields[n].mX);
                    ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mSystemFields[n].mY);
                    todEmitterDefinition.mSystemFields[n].mFieldType = (ParticleFieldType)input.ReadInt32();
                }
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleRed);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleGreen);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleBlue);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleAlpha);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleBrightness);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleSpinAngle);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleSpinSpeed);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleScale);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mParticleStretch);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mCollisionReflect);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mCollisionSpin);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mClipTop);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mClipBottom);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mClipLeft);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mClipRight);
                ReadFloatParameterTrack(ref input, ref todEmitterDefinition.mAnimationRate);
                todParticleDefinition.mEmitterDefs[j] = todEmitterDefinition;
            }
            return todParticleDefinition;
        }

        public void ReadFloatParameterTrack(ref ContentReader input, ref FloatParameterTrack track)
        {
            track.mCountNodes = input.ReadInt32();
            if (track.mCountNodes == 0)
            {
                track.mNodes = null;
                return;
            }
            track.mNodes = new FloatParameterTrackNode[track.mCountNodes];
            for (int i = 0; i < track.mCountNodes; i++)
            {
                track.mNodes[i] = new FloatParameterTrackNode();
            }
            for (int j = 0; j < track.mCountNodes; j++)
            {
                FloatParameterTrackNode floatParameterTrackNode = track.mNodes[j];
                floatParameterTrackNode.mTime = input.ReadSingle();
                floatParameterTrackNode.mLowValue = input.ReadSingle();
                floatParameterTrackNode.mHighValue = input.ReadSingle();
                floatParameterTrackNode.mCurveType = (TodCurves)input.ReadInt32();
                floatParameterTrackNode.mDistribution = (TodCurves)input.ReadInt32();
            }
        }
    }
}
