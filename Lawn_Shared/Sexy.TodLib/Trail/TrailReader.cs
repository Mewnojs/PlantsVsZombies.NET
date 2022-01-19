using System;
using Microsoft.Xna.Framework.Content;

namespace Sexy.TodLib
{
    internal class TrailReader : ContentTypeReader<TrailDefinition>
    {
        protected override TrailDefinition Read(ContentReader input, TrailDefinition value)
        {
            TrailDefinition trailDefinition = new TrailDefinition();
            trailDefinition.mImageName = input.ReadString();
            trailDefinition.mMaxPoints = input.ReadInt32();
            trailDefinition.mMinPointDistance = (float)input.ReadDouble();
            trailDefinition.mTrailFlags = input.ReadInt32();
            ReadFloatParameterTrack(ref input, out trailDefinition.mTrailDuration);
            ReadFloatParameterTrack(ref input, out trailDefinition.mWidthOverLength);
            ReadFloatParameterTrack(ref input, out trailDefinition.mWidthOverTime);
            ReadFloatParameterTrack(ref input, out trailDefinition.mAlphaOverLength);
            ReadFloatParameterTrack(ref input, out trailDefinition.mAlphaOverTime);
            return trailDefinition;
        }

        private void ReadFloatParameterTrack(ref ContentReader input, out FloatParameterTrack value)
        {
            value = new FloatParameterTrack();
            value.mCountNodes = input.ReadInt32();
            value.mNodes = new FloatParameterTrackNode[value.mCountNodes];
            for (int i = 0; i < value.mCountNodes; i++)
            {
                FloatParameterTrackNode floatParameterTrackNode = new FloatParameterTrackNode();
                floatParameterTrackNode.mCurveType = (TodCurves)input.ReadInt32();
                floatParameterTrackNode.mDistribution = (TodCurves)input.ReadInt32();
                floatParameterTrackNode.mHighValue = (float)input.ReadDouble();
                floatParameterTrackNode.mLowValue = (float)input.ReadDouble();
                floatParameterTrackNode.mTime = (float)input.ReadDouble();
                value.mNodes[i] = floatParameterTrackNode;
            }
        }
    }
}
