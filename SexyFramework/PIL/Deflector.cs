using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class Deflector
	{
		public Deflector()
		{
			this.mX1 = 0f;
			this.mY1 = 0f;
			this.mX2 = 0f;
			this.mY2 = 0f;
			this.mBaseAngle = 0f;
			this.mRotX1 = 0f;
			this.mRotX2 = 0f;
			this.mRotY1 = 0f;
			this.mRotY2 = 0f;
			this.mLastFrame = 0;
			this.mLastSettings = null;
			this.mX1Off = 0f;
			this.mY1Off = 0f;
			this.mX2Off = 0f;
			this.mY2Off = 0f;
			this.mCenterX = 0f;
			this.mCenterY = 0f;
			this.mSerialIndex = -1;
			this.mSystem = null;
			this.mLastSettings = new DeflectorSettings();
			this.mTimeLine.mCurrentSettings = this.mLastSettings;
			this.mWaypointManager = new WaypointManager();
		}

		public virtual void Dispose()
		{
			this.mWaypointManager.Dispose();
			this.mWaypointManager = null;
		}

		public void ResetForReuse()
		{
			this.mLastSettings = new DeflectorSettings();
			this.mTimeLine.mCurrentSettings = this.mLastSettings;
		}

		public void Update(int frame)
		{
			if (frame == 0)
			{
				if (this.mWaypointManager.GetNumPoints() == 0)
				{
					this.mCenterX = this.mX1 + (this.mX2 - this.mX1) * 0.5f;
					this.mCenterY = this.mY1 + (this.mY2 - this.mY1) * 0.5f;
				}
				this.mX1Off = this.mX1 - this.mCenterX;
				this.mY1Off = this.mY1 - this.mCenterY;
				this.mX2Off = this.mX2 - this.mCenterX;
				this.mY2Off = this.mY2 - this.mCenterY;
				this.mBaseAngle = Common.AngleBetweenPoints(this.mX1, this.mY1, this.mX2, this.mY2);
			}
			this.mLastFrame = frame;
			this.mTimeLine.Update(frame);
			if (this.mLastSettings.mThickness < 2)
			{
				this.mLastSettings.mThickness = 2;
			}
			if (this.mWaypointManager.GetNumPoints() > 0)
			{
				this.mWaypointManager.Update(frame);
				this.mCenterX = this.mWaypointManager.GetLastPoint().X;
				this.mCenterY = this.mWaypointManager.GetLastPoint().Y;
			}
			float num = this.mCenterX + this.mX1Off;
			float num2 = this.mCenterY + this.mY1Off;
			Common.RotatePoint(-this.mBaseAngle, ref num, ref num2, this.mCenterX, this.mCenterY);
			float num3 = this.mCenterX + this.mX2Off;
			float num4 = this.mCenterY + this.mY2Off;
			Common.RotatePoint(-this.mBaseAngle, ref num3, ref num4, this.mCenterX, this.mCenterY);
			this.mUnrotatedRect.SetValue((int)num, (int)(num2 - (float)(this.mLastSettings.mThickness / 2)), (int)(num3 - num), this.mLastSettings.mThickness);
			this.mRotX1 = this.mCenterX + this.mX1Off;
			this.mRotX2 = this.mCenterX + this.mX2Off;
			this.mRotY1 = this.mCenterY + this.mY1Off;
			this.mRotY2 = this.mCenterY + this.mY2Off;
			Common.RotatePoint(this.mLastSettings.mAngle, ref this.mRotX1, ref this.mRotY1, this.mCenterX, this.mCenterY);
			Common.RotatePoint(this.mLastSettings.mAngle, ref this.mRotX2, ref this.mRotY2, this.mCenterX, this.mCenterY);
			float num5 = (float)this.mLastSettings.mThickness / 2f * (float)Math.Sin((double)(this.mLastSettings.mAngle + this.mBaseAngle));
			float num6 = (float)this.mLastSettings.mThickness / 2f * (float)Math.Cos((double)(this.mLastSettings.mAngle + this.mBaseAngle));
			this.mRotatedPoints[0].mX = (int)(this.mRotX1 - num5);
			this.mRotatedPoints[0].mY = (int)(this.mRotY1 - num6);
			this.mRotatedPoints[1].mX = (int)(this.mRotX2 - num5);
			this.mRotatedPoints[1].mY = (int)(this.mRotY2 - num6);
			this.mRotatedPoints[2].mX = (int)(this.mRotX2 + num5);
			this.mRotatedPoints[2].mY = (int)(this.mRotY2 + num6);
			this.mRotatedPoints[3].mX = (int)(this.mRotX1 + num5);
			this.mRotatedPoints[3].mY = (int)(this.mRotY1 + num6);
		}

		public void DebugDraw(Graphics g)
		{
		}

		public void AddKeyFrame(int frame, DeflectorSettings p)
		{
			this.mTimeLine.AddKeyFrame(frame, p);
		}

		public void Apply(MovableObject p)
		{
			if (!p.CanInteract())
			{
				return;
			}
			DeflectorCollInfo deflectorCollInfo = new DeflectorCollInfo();
			p.mDeflectorCollMap[this] = deflectorCollInfo;
			if (deflectorCollInfo.mIgnoresDeflector)
			{
				return;
			}
			Rect r = new Rect((int)p.GetX() - 1, (int)p.GetY() - 1, 2, 2);
			if (Common.RotatedRectsIntersect(r, -p.mAngle, this.mUnrotatedRect, this.mBaseAngle + this.mLastSettings.mAngle))
			{
				if (Common.Rand() % 100 >= (int)(100f * this.mLastSettings.mHitChance))
				{
					deflectorCollInfo.mIgnoresDeflector = true;
					return;
				}
				float[] array = new float[4];
				float num = 3.4E+38f;
				int num2 = 5;
				float num3 = 0f;
				for (int i = 0; i < 4; i++)
				{
					array[i] = Common.DistFromPointToLine(this.mRotatedPoints[i], (i < 3) ? this.mRotatedPoints[i + 1] : this.mRotatedPoints[0], new SexyPoint((int)p.GetX(), (int)p.GetY()), ref num3);
					if (array[i] < num)
					{
						num = array[i];
						num2 = i;
					}
				}
				SexyPoint point = ((num2 == 3) ? this.mRotatedPoints[0] : this.mRotatedPoints[num2 + 1]);
				SexyVector2 sexyVector = new SexyVector2((float)(point.mX - this.mRotatedPoints[num2].mX), (float)(point.mY - this.mRotatedPoints[num2].mY));
				SexyVector2 sexyVector2 = -sexyVector.Normalize().Perp();
				float t = this.mLastSettings.mBounceMult * p.mBounce / 100f;
				SexyVector2 impliedObject = new SexyVector2(p.mVX, p.mVY);
				SexyVector2 sexyVector3 = (impliedObject - sexyVector2 * 2f * impliedObject.Dot(sexyVector2)) * t;
				p.SetX(p.GetX() + sexyVector2.x * this.mLastSettings.mCollisionMult);
				p.SetY(p.GetY() + sexyVector2.y * this.mLastSettings.mCollisionMult);
				p.mVX = sexyVector3.x;
				p.mVY = sexyVector3.y;
			}
		}

		public virtual void Serialize(SexyBuffer b)
		{
			this.mTimeLine.Serialize(b);
			this.mLastSettings.Serialize(b);
			b.WriteLong((long)this.mUnrotatedRect.mX);
			b.WriteLong((long)this.mUnrotatedRect.mY);
			b.WriteLong((long)this.mUnrotatedRect.mWidth);
			b.WriteLong((long)this.mUnrotatedRect.mHeight);
			for (int i = 0; i < 4; i++)
			{
				b.WriteLong((long)this.mRotatedPoints[i].mX);
				b.WriteLong((long)this.mRotatedPoints[i].mY);
			}
			b.WriteFloat(this.mRotX1);
			b.WriteFloat(this.mRotX2);
			b.WriteFloat(this.mRotY1);
			b.WriteFloat(this.mRotY2);
			b.WriteFloat(this.mBaseAngle);
			b.WriteLong((long)this.mLastFrame);
			b.WriteFloat(this.mX1Off);
			b.WriteFloat(this.mY1Off);
			b.WriteFloat(this.mX2Off);
			b.WriteFloat(this.mY2Off);
			b.WriteLong((long)this.mSerialIndex);
			b.WriteFloat(this.mX1);
			b.WriteFloat(this.mY1);
			b.WriteFloat(this.mX2);
			b.WriteFloat(this.mY2);
			b.WriteFloat(this.mCenterX);
			b.WriteFloat(this.mCenterY);
			this.mWaypointManager.Serialize(b);
		}

		public virtual void Deserialize(SexyBuffer b)
		{
			this.mTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(DeflectorSettings.Instantiate));
			this.mLastSettings.Deserialize(b);
			this.mUnrotatedRect.mX = (int)b.ReadLong();
			this.mUnrotatedRect.mY = (int)b.ReadLong();
			this.mUnrotatedRect.mWidth = (int)b.ReadLong();
			this.mUnrotatedRect.mHeight = (int)b.ReadLong();
			for (int i = 0; i < 4; i++)
			{
				this.mRotatedPoints[i].mX = (int)b.ReadLong();
				this.mRotatedPoints[i].mY = (int)b.ReadLong();
			}
			this.mRotX1 = b.ReadFloat();
			this.mRotX2 = b.ReadFloat();
			this.mRotY1 = b.ReadFloat();
			this.mRotY2 = b.ReadFloat();
			this.mBaseAngle = b.ReadFloat();
			this.mLastFrame = (int)b.ReadLong();
			this.mX1Off = b.ReadFloat();
			this.mY1Off = b.ReadFloat();
			this.mX2Off = b.ReadFloat();
			this.mY2Off = b.ReadFloat();
			this.mSerialIndex = (int)b.ReadLong();
			this.mX1 = b.ReadFloat();
			this.mY1 = b.ReadFloat();
			this.mX2 = b.ReadFloat();
			this.mY2 = b.ReadFloat();
			this.mCenterX = b.ReadFloat();
			this.mCenterY = b.ReadFloat();
			this.mWaypointManager.Deserialize(b);
		}

		public void LoopTimeLine(bool l)
		{
			this.mTimeLine.mLoop = l;
		}

		protected TimeLine mTimeLine = new TimeLine();

		protected DeflectorSettings mLastSettings;

		protected Rect mUnrotatedRect = default(Rect);

		protected SexyPoint[] mRotatedPoints = new SexyPoint[]
		{
			new SexyPoint(),
			new SexyPoint(),
			new SexyPoint(),
			new SexyPoint()
		};

		protected float mRotX1;

		protected float mRotX2;

		protected float mRotY1;

		protected float mRotY2;

		protected float mBaseAngle;

		protected int mLastFrame;

		protected float mX1Off;

		protected float mY1Off;

		protected float mX2Off;

		protected float mY2Off;

		public int mSerialIndex;

		public float mX1;

		public float mY1;

		public float mX2;

		public float mY2;

		public float mCenterX;

		public float mCenterY;

		public WaypointManager mWaypointManager;

		public System mSystem;
	}
}
