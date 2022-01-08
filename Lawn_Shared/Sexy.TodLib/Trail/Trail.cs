using System;

namespace Sexy.TodLib
{
    public/*internal*/ class Trail
    {
        public Trail()
        {
            mNumTrailPoints = 0;
            mDead = false;
            mIsAttachment = false;
            mRenderOrder = 0;
            mTrailAge = 0;
            mDefinition = null;
            mTrailDuration = 0;
            mColorOverride = SexyColor.White;
            for (int i = 0; i < 4; i++)
            {
                mTrailInterp[i] = TodCommon.RandRangeFloat(0f, 1f);
            }
        }

        public void Update()//1update no support
        {
            mTrailAge++;
            if (mTrailAge >= mTrailDuration)
            {
                if (TodCommon.TestBit((uint)mDefinition.mTrailFlags, 1))
                {
                    mTrailAge = 0;
                    return;
                }
                mDead = true;
            }
        }

        public void Draw(Graphics g)
        {
            if (mDead)
            {
                return;
            }
            if (mNumTrailPoints < 2)
            {
                return;
            }
            float theTimeValue = mTrailAge / (float)(mTrailDuration - 1);
            int theNumTriangles = (mNumTrailPoints - 1) * 2;
            TriVertex[,] array = new TriVertex[38, 3];
            bool flag = false;
            SexyVector2 sexyVector = default(SexyVector2);
            int i = 0;
            while (i < mNumTrailPoints - 1)
            {
                if (flag)
                {
                    goto IL_5F;
                }
                if (GetNormalAtPoint(i, ref sexyVector))
                {
                    flag = true;
                    goto IL_5F;
                }
                IL_669:
                i++;
                continue;
                IL_5F:
                SexyVector2 sexyVector2 = sexyVector;
                SexyVector2 sexyVector3 = default(SexyVector2);
                if (!GetNormalAtPoint(i + 1, ref sexyVector3))
                {
                    sexyVector3 = sexyVector;
                }
                else
                {
                    sexyVector = sexyVector3;
                }
                TrailPoint trailPoint = mTrailPoints[i];
                TrailPoint trailPoint2 = mTrailPoints[i + 1];
                float num = 1f - i / (float)(mNumTrailPoints - 1);
                float num2 = 1f - (i + 1) / (float)(mNumTrailPoints - 1);
                float num3 = Definition.FloatTrackEvaluate(ref mDefinition.mWidthOverLength, num, mTrailInterp[0]);
                float num4 = Definition.FloatTrackEvaluate(ref mDefinition.mWidthOverLength, num2, mTrailInterp[0]);
                float num5 = Definition.FloatTrackEvaluate(ref mDefinition.mWidthOverTime, theTimeValue, mTrailInterp[1]);
                float num6 = Definition.FloatTrackEvaluate(ref mDefinition.mWidthOverTime, theTimeValue, mTrailInterp[1]);
                float num7 = Definition.FloatTrackEvaluate(ref mDefinition.mAlphaOverLength, num, mTrailInterp[2]);
                float num8 = Definition.FloatTrackEvaluate(ref mDefinition.mAlphaOverLength, num2, mTrailInterp[2]);
                float num9 = Definition.FloatTrackEvaluate(ref mDefinition.mAlphaOverTime, theTimeValue, mTrailInterp[3]);
                float num10 = Definition.FloatTrackEvaluate(ref mDefinition.mAlphaOverTime, theTimeValue, mTrailInterp[3]);
                int colorAlpha = TodCommon.ClampInt(TodCommon.FloatRoundToInt(num7 * num9 * mColorOverride.mAlpha), 0, 255);
                int color2Alpha = TodCommon.ClampInt(TodCommon.FloatRoundToInt(num8 * num10 * mColorOverride.mAlpha), 0, 255);
                SexyColor aColor = mColorOverride;
                SexyColor aColor2 = mColorOverride;
                aColor.mAlpha = colorAlpha;
                aColor2.mAlpha = color2Alpha;
                SexyVector2[] array2 = new SexyVector2[4];
                array2[0].x = mTrailCenter.x + trailPoint.aPos.x + sexyVector2.x * num3 * num5;
                array2[0].y = mTrailCenter.y + trailPoint.aPos.y + sexyVector2.y * num3 * num5;
                array2[1].x = mTrailCenter.x + trailPoint.aPos.x + -sexyVector2.x * num3 * num5;
                array2[1].y = mTrailCenter.y + trailPoint.aPos.y + -sexyVector2.y * num3 * num5;
                array2[2].x = mTrailCenter.x + trailPoint2.aPos.x + sexyVector3.x * num4 * num6;
                array2[2].y = mTrailCenter.y + trailPoint2.aPos.y + sexyVector3.y * num4 * num6;
                array2[3].x = mTrailCenter.x + trailPoint2.aPos.x + -sexyVector3.x * num4 * num6;
                array2[3].y = mTrailCenter.y + trailPoint2.aPos.y + -sexyVector3.y * num4 * num6;
                int num11 = i * 2;
                array[num11, 0].x = array2[0].x;
                array[num11, 0].y = array2[0].y;
                array[num11, 0].u = num;
                array[num11, 0].v = 1f;
                array[num11, 0].color = aColor;
                array[num11, 1].x = array2[1].x;
                array[num11, 1].y = array2[1].y;
                array[num11, 1].u = num;
                array[num11, 1].v = 0f;
                array[num11, 1].color = aColor;
                array[num11, 2].x = array2[2].x;
                array[num11, 2].y = array2[2].y;
                array[num11, 2].u = num2;
                array[num11, 2].v = 1f;
                array[num11, 2].color = aColor2;
                array[num11 + 1, 0].x = array2[2].x;
                array[num11 + 1, 0].y = array2[2].y;
                array[num11 + 1, 0].u = num2;
                array[num11 + 1, 0].v = 1f;
                array[num11 + 1, 0].color = aColor2;
                array[num11 + 1, 1].x = array2[1].x;
                array[num11 + 1, 1].y = array2[1].y;
                array[num11 + 1, 1].u = num;
                array[num11 + 1, 1].v = 0f;
                array[num11 + 1, 1].color = aColor;
                array[num11 + 1, 2].x = array2[3].x;
                array[num11 + 1, 2].y = array2[3].y;
                array[num11 + 1, 2].u = num2;
                array[num11 + 1, 2].v = 0f;
                array[num11 + 1, 2].color = aColor2;
                goto IL_669;
            }
            g.DrawTrianglesTex(mDefinition.mImage, array, theNumTriangles);
        }

        public void AddPoint(float x, float y)
        {
            int num = TodCommon.ClampInt(mDefinition.mMaxPoints, 2, 20);
            if (mNumTrailPoints > 0)
            {
                TrailPoint trailPoint = mTrailPoints[mNumTrailPoints - 1];
                float num2 = TodCommon.Distance2D(x, y, trailPoint.aPos.x, trailPoint.aPos.y);
                if (num2 < mDefinition.mMinPointDistance)
                {
                    return;
                }
            }
            int num3 = mNumTrailPoints;
            TrailPoint trailPoint2 = mTrailPoints[mNumTrailPoints];
            trailPoint2.aPos.x = x;
            trailPoint2.aPos.y = y;
            mNumTrailPoints++;
        }

        public bool GetNormalAtPoint(int nIndex, ref SexyVector2 theNormal)
        {
            SexyVector2 sexyVector = default(SexyVector2);
            if (nIndex == 0)
            {
                TrailPoint trailPoint = mTrailPoints[nIndex];
                TrailPoint trailPoint2 = mTrailPoints[nIndex + 1];
                sexyVector = (trailPoint2.aPos - trailPoint.aPos).Perp();
            }
            else if (nIndex == mNumTrailPoints - 1)
            {
                TrailPoint trailPoint3 = mTrailPoints[nIndex];
                TrailPoint trailPoint4 = mTrailPoints[nIndex - 1];
                sexyVector = (trailPoint3.aPos - trailPoint4.aPos).Perp();
            }
            else
            {
                TrailPoint trailPoint5 = mTrailPoints[nIndex];
                TrailPoint trailPoint6 = mTrailPoints[nIndex + 1];
                TrailPoint trailPoint7 = mTrailPoints[nIndex - 1];
                SexyVector2 rhs = trailPoint6.aPos - trailPoint5.aPos;
                SexyVector2 lhs = trailPoint7.aPos - trailPoint5.aPos;
                rhs = rhs.Normalize();
                lhs = lhs.Normalize();
                sexyVector = lhs + rhs;
            }
            float num = sexyVector.Magnitude();
            if (TodCommon.FloatApproxEqual(num, 0f))
            {
                return false;
            }
            theNormal.x = sexyVector.x / num;
            theNormal.y = sexyVector.y / num;
            return true;
        }

        public TrailPoint[] mTrailPoints = new TrailPoint[20];

        public int mNumTrailPoints;

        public bool mDead;

        public int mRenderOrder;

        public int mTrailAge;

        public int mTrailDuration;

        public TrailDefinition mDefinition;

        public TrailHolder mTrailHolder;

        public float[] mTrailInterp = new float[4];

        public SexyVector2 mTrailCenter = default(SexyVector2);

        public bool mIsAttachment;

        public SexyColor mColorOverride = default(SexyColor);
    }
}
