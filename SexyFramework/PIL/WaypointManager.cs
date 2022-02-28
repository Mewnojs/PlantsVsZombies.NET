using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class WaypointManager : IDisposable
	{
		protected void Clean()
		{
			if (this.mCurve != null)
			{
				this.mCurve.Dispose();
			}
			this.mWaypoints.Clear();
		}

		public WaypointManager()
		{
			this.mTotalTime = 0f;
			this.mLoop = false;
			this.mTotalFrames = 0;
			this.mLastFrameWasEnd = false;
			this.mCurve = new Bezier();
		}

		public WaypointManager(WaypointManager rhs)
		{
			this.CopyFrom(rhs);
		}

		public virtual void Dispose()
		{
			this.Clean();
		}

		public void CopyFrom(WaypointManager rhs)
		{
			if (this == rhs || rhs == null)
			{
				return;
			}
			this.Clean();
			this.mTotalTime = rhs.mTotalTime;
			this.mTotalFrames = rhs.mTotalFrames;
			this.mLoop = rhs.mLoop;
			this.mLastPoint = rhs.mLastPoint;
			this.mCurve = new Bezier(rhs.mCurve);
			this.mLastFrameWasEnd = rhs.mLastFrameWasEnd;
			for (int i = 0; i < rhs.mWaypoints.size<Waypoint>(); i++)
			{
				Waypoint waypoint = new Waypoint();
				waypoint.CopyFrom(rhs.mWaypoints[i]);
				this.mWaypoints.Add(waypoint);
			}
		}

		public void Serialize(SexyBuffer b)
		{
			this.mCurve.Serialize(b);
			b.WriteFloat(this.mTotalTime);
			b.WriteLong((long)this.mTotalFrames);
			b.WriteBoolean(this.mLastFrameWasEnd);
			b.WriteFloat(this.mLastPoint.X);
			b.WriteFloat(this.mLastPoint.Y);
			b.WriteLong((long)this.mWaypoints.Count);
			for (int i = 0; i < this.mWaypoints.Count; i++)
			{
				this.mWaypoints[i].Serialize(b);
			}
		}

		public void Deserialize(SexyBuffer b)
		{
			this.mCurve.Deserialize(b);
			this.mTotalTime = b.ReadFloat();
			this.mTotalFrames = (int)b.ReadLong();
			this.mLastFrameWasEnd = b.ReadBoolean();
			this.mLastPoint.X = b.ReadFloat();
			this.mLastPoint.Y = b.ReadFloat();
			this.mWaypoints.Clear();
			int num = (int)b.ReadLong();
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint();
				waypoint.Deserialize(b);
				this.mWaypoints.Add(waypoint);
			}
		}

		public void AddPoint(int frame, Vector2 p, bool linear, Vector2 c1)
		{
			Waypoint waypoint = new Waypoint();
			this.mWaypoints.Add(waypoint);
			waypoint.mTime = (float)frame / 100f;
			waypoint.mFrame = frame;
			float num = waypoint.mTime;
			int num2 = frame;
			if (this.mWaypoints.size<Waypoint>() > 1)
			{
				num -= this.mWaypoints[this.mWaypoints.size<Waypoint>() - 2].mTime;
				num2 -= this.mWaypoints[this.mWaypoints.size<Waypoint>() - 2].mFrame;
			}
			this.mTotalTime += num;
			this.mTotalFrames += num2;
			waypoint.mLinear = linear;
			waypoint.mPoint = p;
			waypoint.mControl1 = c1;
			Vector2 vector = c1 - p;
			waypoint.mControl2 = p - vector;
		}

		public void AddPoint(int frame, Vector2 p, bool linear)
		{
			this.AddPoint(frame, p, linear, Vector2.Zero);
		}

		public void AddPoint(int frame, Vector2 p)
		{
			this.AddPoint(frame, p, false);
		}

		public void Init(bool make_curve_image)
		{
			Vector2[] array = new Vector2[this.mWaypoints.size<Waypoint>()];
			Vector2[] array2 = new Vector2[2 * (this.mWaypoints.size<Waypoint>() - 1)];
			float[] array3 = new float[this.mWaypoints.size<Waypoint>()];
			int num = 0;
			for (int i = 0; i < this.mWaypoints.size<Waypoint>(); i++)
			{
				Waypoint waypoint = this.mWaypoints[i];
				array3[i] = waypoint.mTime;
				array[i] = waypoint.mPoint;
				if (!waypoint.mLinear)
				{
					array2[num] = waypoint.mControl1;
					if (i > 0 && i < this.mWaypoints.size<Waypoint>() - 1)
					{
						array2[num + 1] = waypoint.mControl2;
					}
				}
				else if (i == 0 && this.mWaypoints.size<Waypoint>() == 1)
				{
					array2[0] = waypoint.mPoint;
				}
				else
				{
					Vector2 vector = default(Vector2);
					if (i < this.mWaypoints.size<Waypoint>() - 1)
					{
						vector = this.mWaypoints[i + 1].mPoint - this.mWaypoints[i].mPoint;
						array2[(i == 0) ? 0 : (num + 1)] = this.mWaypoints[i].mPoint + vector / 2f;
					}
					if (i > 0)
					{
						vector = this.mWaypoints[i].mPoint - this.mWaypoints[i - 1].mPoint;
						array2[num] = this.mWaypoints[i - 1].mPoint + vector / 2f;
					}
				}
				num++;
				if (i > 0)
				{
					num++;
				}
			}
			this.mCurve.Init(array, array2, array3, this.mWaypoints.size<Waypoint>());
			if (make_curve_image)
			{
				this.mCurve.GenerateCurveImage(SexyColor.White, 10000);
			}
		}

		public void Update(int frame)
		{
			if (this.mCurve.GetNumPoints() == 0)
			{
				return;
			}
			if (frame > 0 && frame % this.mTotalFrames == 0)
			{
				this.mLastFrameWasEnd = true;
			}
			if (this.mLoop)
			{
				frame %= this.mTotalFrames;
			}
			else if (frame > this.mTotalFrames)
			{
				return;
			}
			this.mLastPoint = this.mCurve.Evaluate((float)frame / (float)this.mTotalFrames * this.mTotalTime);
		}

		public void DebugDraw(Graphics g, float scale)
		{
		}

		public void DebugDraw(Graphics g)
		{
			this.DebugDraw(g, 1f);
		}

		public Vector2 GetLastPoint()
		{
			return this.mLastPoint;
		}

		public int GetNumPoints()
		{
			return this.mCurve.GetNumPoints();
		}

		public bool AtEnd()
		{
			return this.mLastFrameWasEnd;
		}

		protected Bezier mCurve;

		protected List<Waypoint> mWaypoints = new List<Waypoint>();

		protected float mTotalTime;

		protected int mTotalFrames;

		protected Vector2 mLastPoint = default(Vector2);

		protected bool mLastFrameWasEnd;

		public bool mLoop;
	}
}
