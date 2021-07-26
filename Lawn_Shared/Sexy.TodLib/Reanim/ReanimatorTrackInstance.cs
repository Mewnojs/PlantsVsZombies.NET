using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
	internal class ReanimatorTrackInstance
	{
		public static void PreallocateMemory()
		{
			for (int i = 0; i < 10000; i++)
			{
				new ReanimatorTrackInstance().PrepareForReuse();
			}
		}

		public static ReanimatorTrackInstance GetNewReanimatorTrackInstance()
		{
			if (ReanimatorTrackInstance.unusedObjects.Count > 0)
			{
				return ReanimatorTrackInstance.unusedObjects.Pop();
			}
			return new ReanimatorTrackInstance();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			ReanimatorTrackInstance.unusedObjects.Push(this);
		}

		public override string ToString()
		{
			return string.Format("Group: {0}", this.mRenderGroup);
		}

		private ReanimatorTrackInstance()
		{
			this.Reset();
		}

		private void Reset()
		{
			if (this.mBlendTransform != null)
			{
				this.mBlendTransform.PrepareForReuse();
			}
			this.mBlendTransform = null;
			this.mBlendCounter = 0;
			this.mBlendTime = 0;
			this.mShakeOverride = 0f;
			this.mShakeX = 0f;
			this.mShakeY = 0f;
			this.mAttachmentID = null;
			this.mRenderGroup = 0;
			this.mIgnoreClipRect = false;
			this.mTruncateDisappearingFrames = true;
			this.mImageOverride = null;
			this.mTrackColor = new SexyColor(Color.White);
			this.mIgnoreColorOverride = false;
			this.mIgnoreExtraAdditiveColor = false;
		}

		public byte mBlendCounter;

		public byte mBlendTime;

		public ReanimatorTransform mBlendTransform;

		public float mShakeOverride;

		public float mShakeX;

		public float mShakeY;

		public Attachment mAttachmentID;

		public Image mImageOverride;

		public int mRenderGroup;

		public SexyColor mTrackColor;

		public bool mIgnoreClipRect;

		public bool mTruncateDisappearingFrames;

		public bool mIgnoreColorOverride;

		public bool mIgnoreExtraAdditiveColor;

		private static Stack<ReanimatorTrackInstance> unusedObjects = new Stack<ReanimatorTrackInstance>();
	}
}
