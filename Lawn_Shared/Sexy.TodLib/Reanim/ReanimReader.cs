using System;
using Microsoft.Xna.Framework.Content;

namespace Sexy.TodLib
{
    internal class ReanimReader : ContentTypeReader<ReanimatorDefinition>
    {
        protected override ReanimatorDefinition Read(ContentReader input, ReanimatorDefinition existingInstance)
        {
            ReanimatorDefinition reanimatorDefinition = new ReanimatorDefinition();
            CustomContentReader customContentReader = new CustomContentReader(input);
            ReanimScaleType doScale = (ReanimScaleType)customContentReader.ReadByte();
            reanimatorDefinition.mFPS = customContentReader.ReadSingle();
            reanimatorDefinition.mTrackCount = (short)customContentReader.ReadInt32();
            reanimatorDefinition.mTracks = new ReanimatorTrack[reanimatorDefinition.mTrackCount];
            for (int i = 0; i < reanimatorDefinition.mTrackCount; i++)
            {
                ReanimatorTrack reanimatorTrack;
                ReadReanimTrack(customContentReader, doScale, out reanimatorTrack);
                reanimatorDefinition.mTracks[i] = reanimatorTrack;
            }
            return reanimatorDefinition;
        }

        private void ReadReanimTrack(CustomContentReader input, ReanimScaleType doScale, out ReanimatorTrack track)
        {
            string name = input.ReadString();
            int transformCount = input.ReadInt32();
            track = new ReanimatorTrack(name, transformCount);
            for (int i = 0; i < track.mTransformCount; i++)
            {
                ReanimatorTransform reanimatorTransform;
                ReadReanimTransform(input, doScale, out reanimatorTransform);
                track.mTransforms[i] = reanimatorTransform;
            }
        }

        private static ReanimatorTransform GetDefault()
        {
            ReanimatorTransform newReanimatorTransform = ReanimatorTransform.GetNewReanimatorTransform();
            newReanimatorTransform.mFontName = string.Empty;
            newReanimatorTransform.mImageName = string.Empty;
            newReanimatorTransform.mText = string.Empty;
            newReanimatorTransform.mAlpha = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mFrame = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mScaleX = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mScaleY = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mSkewX = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mSkewY = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mSkewXCos = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mSkewXSin = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mSkewYCos = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mSkewYSin = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mTransX = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            newReanimatorTransform.mTransY = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            return newReanimatorTransform;
        }

        private void ReadReanimTransform(CustomContentReader input, ReanimScaleType doScale, out ReanimatorTransform transform)
        {
            ReanimReader.ReanimOptimisationType reanimOptimisationType = (ReanimReader.ReanimOptimisationType)input.ReadByte();
            if (reanimOptimisationType == ReanimReader.ReanimOptimisationType.Placeholder)
            {
                transform = ReanimReader.GetDefault();
            }
            else if (reanimOptimisationType == ReanimReader.ReanimOptimisationType.CopyPrevious)
            {
                Debug.ASSERT(ReanimReader.previous != null);
                transform = ReanimReader.previous;
            }
            else
            {
                transform = ReanimatorTransform.GetReanimatorTransformForLoadingThread();
                transform.mFontName = input.ReadString();
                transform.mImageName = input.ReadString();
                transform.mText = input.ReadString();
                transform.mAlpha = input.ReadSingle();
                transform.mFrame = input.ReadSingle();
                transform.mScaleX = input.ReadSingle();
                transform.mScaleY = input.ReadSingle();
                transform.mSkewX = input.ReadSingle();
                transform.mSkewY = input.ReadSingle();
                float num = input.ReadSingle();
                float num2 = input.ReadSingle();
                if (doScale == ReanimScaleType.InvertAndScale)
                {
                    transform.mTransX = ((num == ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER) ? num : Constants.InvertAndScale(num));
                    transform.mTransY = ((num2 == ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER) ? num2 : Constants.InvertAndScale(num2));
                }
                else if (doScale == ReanimScaleType.ScaleFromPC) 
                {
                    transform.mTransX = ((num == ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER) ? num : Constants.S * num);
                    transform.mTransY = ((num2 == ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER) ? num2 : Constants.S * num2);
                }
                else
                {
                    transform.mTransX = num;
                    transform.mTransY = num2;
                }
            }
            ReanimReader.previous = transform;
        }

        private static ReanimatorTransform previous;

        public static float DEG_TO_RAD = 0.017453292f;

        internal enum ReanimOptimisationType
        {
            New,
            CopyPrevious,
            Placeholder
        }

        internal enum ReanimScaleType 
        {
            NoScale,
            InvertAndScale,
            ScaleFromPC = 0xFF
        }
    }
}
