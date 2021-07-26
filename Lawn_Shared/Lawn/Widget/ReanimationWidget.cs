using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class ReanimationWidget : Widget
	{
		public ReanimationWidget()
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mReanim = null;
			this.mMouseVisible = false;
			this.mClip = false;
			this.mHasAlpha = true;
			this.mLawnDialog = null;
			this.mPosX = 0f;
			this.mPosY = 0f;
		}

		public override void Dispose()
		{
			if (this.mReanim != null)
			{
				this.mReanim.mActive = false;
				this.mApp.mEffectSystem.mReanimationHolder.mReanimations.Remove(this.mReanim);
				this.mReanim.PrepareForReuse();
				this.mReanim = null;
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mReanim == null)
			{
				return;
			}
			this.mReanim.Draw(g);
		}

		public override void Update()
		{
			if (this.mReanim == null)
			{
				return;
			}
			this.mReanim.Update();
			this.mParent.MarkDirty();
		}

		public void AddReanimation(float x, float y, ReanimationType theReanimationType)
		{
			Debug.ASSERT(this.mReanim == null);
			this.mPosX = x;
			this.mPosY = y;
			this.mReanim = this.mApp.mEffectSystem.mReanimationHolder.AllocReanimation(x, y, 0, theReanimationType);
			this.mReanim.mLoopType = ReanimLoopType.REANIM_LOOP;
			this.mReanim.mIsAttachment = true;
			if (this.mReanim.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
			{
				this.mReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
			}
			if (this.mReanim.TrackExists("zombie_butter"))
			{
				this.mReanim.AssignRenderGroupToTrack("zombie_butter", -1);
			}
			this.Resize((int)x, (int)y, (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(10f));
		}

		public LawnApp mApp;

		public Reanimation mReanim;

		public LawnDialog mLawnDialog;

		public float mPosX;

		public float mPosY;
	}
}
