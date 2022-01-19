using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class ReanimationWidget : Widget
    {
        public ReanimationWidget()
        {
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mReanim = null;
            mMouseVisible = false;
            mClip = false;
            mHasAlpha = true;
            mLawnDialog = null;
            mPosX = 0f;
            mPosY = 0f;
        }

        public override void Dispose()
        {
            if (mReanim != null)
            {
                mReanim.mActive = false;
                mApp.mEffectSystem.mReanimationHolder.mReanimations.Remove(mReanim);
                mReanim.PrepareForReuse();
                mReanim = null;
            }
        }

        public override void Draw(Graphics g)
        {
            if (mReanim == null)
            {
                return;
            }
            mReanim.Draw(g);
        }

        public override void Update()
        {
            if (mReanim == null)
            {
                return;
            }
            mReanim.Update();
            mParent.MarkDirty();
        }

        public void AddReanimation(float x, float y, ReanimationType theReanimationType)
        {
            Debug.ASSERT(mReanim == null);
            mPosX = x;
            mPosY = y;
            mReanim = mApp.mEffectSystem.mReanimationHolder.AllocReanimation(x, y, 0, theReanimationType);
            mReanim.mLoopType = ReanimLoopType.Loop;
            mReanim.mIsAttachment = true;
            if (mReanim.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
            {
                mReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
            }
            if (mReanim.TrackExists("zombie_butter"))
            {
                mReanim.AssignRenderGroupToTrack("zombie_butter", -1);
            }
            Resize((int)x, (int)y, (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(10f));
        }

        public LawnApp mApp;

        public Reanimation mReanim;

        public LawnDialog mLawnDialog;

        public float mPosX;

        public float mPosY;
    }
}
