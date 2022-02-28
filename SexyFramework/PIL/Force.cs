using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class Force
	{
		public Force()
		{
			this.mCenterX = 0f;
			this.mCenterY = 0f;
			this.mLastAX = 0f;
			this.mLastAY = 0f;
			this.mTimeLine.mCurrentSettings = new ForceSettings();
			this.mLastSettings = (ForceSettings)this.mTimeLine.mCurrentSettings;
			this.mWaypointManager = new WaypointManager();
		}

		public virtual void Dispose()
		{
			this.mWaypointManager.Dispose();
			this.mWaypointManager = null;
		}

		public void ResetForReuse()
		{
			this.mTimeLine.mCurrentSettings = new ForceSettings();
			this.mLastSettings = (ForceSettings)this.mTimeLine.mCurrentSettings;
		}

		public void Update(int frame)
		{
			this.mTimeLine.Update(frame);
			float num = ModVal.M(2000f);
			this.mLastAX = this.mLastSettings.mStrength / num * (float)Math.Cos((double)(this.mLastSettings.mAngle + this.mLastSettings.mDirection));
			this.mLastAY = -(this.mLastSettings.mStrength / num) * (float)Math.Sin((double)(this.mLastSettings.mAngle + this.mLastSettings.mDirection));
			if (this.mWaypointManager.GetNumPoints() > 0)
			{
				this.mWaypointManager.Update(frame);
				this.mCenterX = this.mWaypointManager.GetLastPoint().X;
				this.mCenterY = this.mWaypointManager.GetLastPoint().Y;
			}
		}

		public void DebugDraw(Graphics g)
		{
		}

		public void Apply(MovableObject p)
		{
			if (!p.CanInteract())
			{
				return;
			}
			Rect r = new Rect((int)(this.mCenterX - this.mLastSettings.mWidth / 2f), (int)(this.mCenterY - this.mLastSettings.mHeight / 2f), (int)this.mLastSettings.mWidth, (int)this.mLastSettings.mHeight);
			Rect r2 = new Rect((int)(p.GetX() - 1f), (int)(p.GetY() - 1f), 2, 2);
			if (Common.RotatedRectsIntersect(r, this.mLastSettings.mAngle, r2, -p.mAngle))
			{
				p.ApplyAcceleration(this.mLastAX, this.mLastAY);
			}
		}

		public void AddKeyFrame(int frame, ForceSettings p)
		{
			this.mTimeLine.AddKeyFrame(frame, p);
		}

		public void LoopTimeLine(bool l)
		{
			this.mTimeLine.mLoop = l;
		}

		public virtual void Serialize(SexyBuffer b)
		{
			this.mTimeLine.Serialize(b);
			this.mLastSettings.Serialize(b);
			b.WriteFloat(this.mLastAX);
			b.WriteFloat(this.mLastAY);
			b.WriteFloat(this.mCenterX);
			b.WriteFloat(this.mCenterY);
			this.mWaypointManager.Serialize(b);
		}

		public virtual void Deserialize(SexyBuffer b)
		{
			this.mTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(ForceSettings.Instantiate));
			this.mLastSettings.Deserialize(b);
			this.mLastAX = b.ReadFloat();
			this.mLastAY = b.ReadFloat();
			this.mCenterX = b.ReadFloat();
			this.mCenterY = b.ReadFloat();
			this.mWaypointManager.Deserialize(b);
		}

		protected TimeLine mTimeLine = new TimeLine();

		protected ForceSettings mLastSettings;

		protected float mLastAX;

		protected float mLastAY;

		public float mCenterX;

		public float mCenterY;

		public WaypointManager mWaypointManager;

		public System mSystem;
	}
}
