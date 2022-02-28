using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public abstract class RenderStateManager
	{
		protected virtual RenderStateManager.State.FCommitFunc GetCommitFunc(RenderStateManager.State inState)
		{
			return null;
		}

		public RenderStateManager()
		{
			this.mDirtyDummyHead = new RenderStateManager.State();
			this.mContextDefDummyHead = new RenderStateManager.State();
			this.mWouldCommitStateDirty = false;
			this.mWouldCommitStateResult = false;
			this.mCurrentContext = this.mDefaultContext;
		}

		public virtual void Dispose()
		{
		}

		public abstract void Init();

		public abstract void Reset();

		public virtual void Cleanup()
		{
			this.mCurrentContext.Unacquire();
		}

		public bool IsDirty()
		{
			return this.mDirtyDummyHead.mDirtyNext != this.mDirtyDummyHead;
		}

		public void ApplyContextDefaults()
		{
			for (RenderStateManager.State mContextDefNext = this.mContextDefDummyHead.mContextDefNext; mContextDefNext != this.mContextDefDummyHead; mContextDefNext = mContextDefNext.mContextDefNext)
			{
				mContextDefNext.mValue = mContextDefNext.mContextDefaultValue;
				mContextDefNext.SetDirty();
			}
		}

		public bool WouldCommitState()
		{
			if (!this.mWouldCommitStateDirty)
			{
				return this.mWouldCommitStateResult;
			}
			this.mWouldCommitStateDirty = false;
			for (RenderStateManager.State mDirtyNext = this.mDirtyDummyHead.mDirtyNext; mDirtyNext != this.mDirtyDummyHead; mDirtyNext = mDirtyNext.mDirtyNext)
			{
				if (!(mDirtyNext.mValue == mDirtyNext.mLastCommittedValue) && mDirtyNext.mCommitFunc != null)
				{
					this.mWouldCommitStateResult = true;
					return this.mWouldCommitStateResult;
				}
			}
			this.mWouldCommitStateResult = false;
			return this.mWouldCommitStateResult;
		}

		public bool CommitState()
		{
			bool flag = true;
			while (this.mDirtyDummyHead.mDirtyNext != this.mDirtyDummyHead)
			{
				RenderStateManager.State mDirtyNext = this.mDirtyDummyHead.mDirtyNext;
				if (mDirtyNext.mValue == mDirtyNext.mLastCommittedValue)
				{
					mDirtyNext.ClearDirty();
				}
				else
				{
					if (mDirtyNext.mCommitFunc != null)
					{
						flag &= mDirtyNext.mCommitFunc(mDirtyNext);
					}
					else
					{
						mDirtyNext.ClearDirty();
					}
					mDirtyNext.mLastCommittedValue = mDirtyNext.mValue;
				}
			}
			return flag;
		}

		public virtual void Flush()
		{
		}

		public RenderStateManager.Context GetContext()
		{
			return this.mCurrentContext;
		}

		public void SetContext(RenderStateManager.Context inContext)
		{
			if (inContext == null)
			{
				inContext = this.mDefaultContext;
			}
			if (inContext == this.mCurrentContext)
			{
				return;
			}
			if (this.mCurrentContext.mParentContext == inContext)
			{
				this.mCurrentContext.Unacquire(true);
				this.mCurrentContext = inContext;
				return;
			}
			if (inContext.mParentContext == this.mCurrentContext)
			{
				this.mCurrentContext = inContext;
				this.mCurrentContext.Reacquire(true);
				return;
			}
			this.mCurrentContext.Unacquire();
			this.mCurrentContext = inContext;
			this.mCurrentContext.Reacquire();
		}

		public void RevertState()
		{
			this.mCurrentContext.RevertState();
		}

		public virtual void PushState()
		{
			this.mCurrentContext.PushState();
		}

		public virtual void PopState()
		{
			this.mCurrentContext.PopState();
		}

		protected RenderStateManager.State mDirtyDummyHead;

		protected RenderStateManager.State mContextDefDummyHead;

		protected RenderStateManager.Context mCurrentContext;

		protected RenderStateManager.Context mDefaultContext = new RenderStateManager.Context();

		protected bool mWouldCommitStateDirty;

		protected bool mWouldCommitStateResult;

		public class StateValue
		{
			public StateValue()
			{
			}

			public StateValue(uint inDword)
			{
				this.mType = RenderStateManager.StateValue.EStateValueType.SV_Dword;
				this.mDword = inDword;
			}

			public StateValue(float inFloat)
			{
				this.mType = RenderStateManager.StateValue.EStateValueType.SV_Float;
				this.mFloat = inFloat;
			}

			public StateValue(object inPtr)
			{
				this.mType = RenderStateManager.StateValue.EStateValueType.SV_Ptr;
				this.mPtr = inPtr;
			}

			public StateValue(float inX, float inY, float inZ, float inW)
			{
				this.mType = RenderStateManager.StateValue.EStateValueType.SV_Vector;
				this.mX = inX;
				this.mY = inY;
				this.mZ = inZ;
				this.mW = inW;
			}

			public StateValue(RenderStateManager.StateValue inValue)
			{
				this.mType = inValue.mType;
				switch (this.mType)
				{
				case RenderStateManager.StateValue.EStateValueType.SV_Dword:
					this.mDword = inValue.mDword;
					return;
				case RenderStateManager.StateValue.EStateValueType.SV_Float:
					this.mFloat = inValue.mFloat;
					return;
				case RenderStateManager.StateValue.EStateValueType.SV_Ptr:
					this.mPtr = inValue.mPtr;
					return;
				case RenderStateManager.StateValue.EStateValueType.SV_Vector:
					this.mX = inValue.mX;
					this.mY = inValue.mY;
					this.mZ = inValue.mZ;
					this.mW = inValue.mW;
					return;
				default:
					return;
				}
			}

			public override bool Equals(object obj)
			{
				if (obj == null || !(obj is RenderStateManager.StateValue))
				{
					return false;
				}
				RenderStateManager.StateValue stateValue = (RenderStateManager.StateValue)obj;
				switch (this.mType)
				{
				case RenderStateManager.StateValue.EStateValueType.SV_Dword:
					return this.mDword == stateValue.mDword;
				case RenderStateManager.StateValue.EStateValueType.SV_Float:
					return this.mFloat == stateValue.mFloat;
				case RenderStateManager.StateValue.EStateValueType.SV_Ptr:
					return this.mPtr == stateValue.mPtr;
				case RenderStateManager.StateValue.EStateValueType.SV_Vector:
					return this.mX == stateValue.mX && this.mY == stateValue.mY && this.mZ == stateValue.mZ && this.mW == stateValue.mW;
				default:
					return false;
				}
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public uint GetDword()
			{
				return this.mDword;
			}

			public float GetFloat()
			{
				return this.mFloat;
			}

			public object GetPtr()
			{
				return this.mPtr;
			}

			public void GetVector(ref float outX, ref float outY, ref float outZ, ref float outW)
			{
				outX = this.mX;
				outY = this.mY;
				outZ = this.mZ;
				outW = this.mW;
			}

			public static bool operator ==(RenderStateManager.StateValue ImpliedObject, RenderStateManager.StateValue inValue)
			{
				if (ImpliedObject == null)
				{
					return inValue == null;
				}
				return ImpliedObject.Equals(inValue);
			}

			public static bool operator !=(RenderStateManager.StateValue ImpliedObject, RenderStateManager.StateValue inValue)
			{
				return !(ImpliedObject == inValue);
			}

			public RenderStateManager.StateValue.EStateValueType mType;

			public uint mDword;

			public float mFloat;

			public object mPtr;

			public float mX;

			public float mY;

			public float mZ;

			public float mW;

			public enum EStateValueType
			{
				SV_Dword,
				SV_Float,
				SV_Ptr,
				SV_Vector
			}
		}

		public class State
		{
			public State(RenderStateManager inManager, uint inContext0, uint inContext1, uint inContext2)
				: this(inManager, inContext0, inContext1, inContext2, 0U)
			{
			}

			public State(RenderStateManager inManager, uint inContext0, uint inContext1)
				: this(inManager, inContext0, inContext1, 0U, 0U)
			{
			}

			public State(RenderStateManager inManager, uint inContext0)
				: this(inManager, inContext0, 0U, 0U, 0U)
			{
			}

			public State(RenderStateManager inManager)
				: this(inManager, 0U, 0U, 0U, 0U)
			{
			}

			public State()
				: this(null, 0U, 0U, 0U, 0U)
			{
			}

			public State(RenderStateManager inManager, uint inContext0, uint inContext1, uint inContext2, uint inContext3)
			{
				this.mContext = new uint[4];
				this.mValue = new RenderStateManager.StateValue();
				this.mHardwareDefaultValue = new RenderStateManager.StateValue();
				this.mContextDefaultValue = new RenderStateManager.StateValue();
				this.mLastCommittedValue = new RenderStateManager.StateValue();
				this.mManager = inManager;
				this.mCommitFunc = null;
				this.mDirtyNext = this;
				this.mDirtyPrev = this;
				this.mContextDefNext = this;
				this.mContextDefPrev = this;
				this.mContext[0] = inContext0;
				this.mContext[1] = inContext1;
				this.mContext[2] = inContext2;
				this.mContext[3] = inContext3;
			}

			public State(RenderStateManager.State inState)
			{
				this.mContext = new uint[4];
				this.mValue = new RenderStateManager.StateValue();
				this.mHardwareDefaultValue = new RenderStateManager.StateValue();
				this.mContextDefaultValue = new RenderStateManager.StateValue();
				this.mLastCommittedValue = new RenderStateManager.StateValue();
				this.mManager = inState.mManager;
				this.mValue = new RenderStateManager.StateValue(inState.mValue);
				this.mHardwareDefaultValue = new RenderStateManager.StateValue(inState.mHardwareDefaultValue);
				this.mContextDefaultValue = new RenderStateManager.StateValue(inState.mContextDefaultValue);
				this.mLastCommittedValue = new RenderStateManager.StateValue(inState.mLastCommittedValue);
				this.mCommitFunc = inState.mCommitFunc;
				this.mDirtyNext = this;
				this.mDirtyPrev = this;
				this.mContextDefNext = this;
				this.mContextDefPrev = this;
				for (int i = 0; i < 4; i++)
				{
					this.mContext[i] = inState.mContext[i];
				}
			}

			public void Init(RenderStateManager.StateValue inDefaultValue, string inName)
			{
				this.Init(inDefaultValue, inName, null);
			}

			public void Init(ulong inDefaultValue, string inName)
			{
				this.Init(new RenderStateManager.StateValue(inDefaultValue), inName);
			}

			public void Init(RenderStateManager.StateValue inDefaultValue, string inName, string inValueEnumName)
			{
				this.mLastCommittedValue = inDefaultValue;
				this.mContextDefaultValue = inDefaultValue;
				this.mHardwareDefaultValue = inDefaultValue;
				this.mValue = inDefaultValue;
				this.mCommitFunc = this.mManager.GetCommitFunc(this);
				this.mName = inName;
			}

			public void Init(ulong inDefaultValue, string inName, string inValueEnumName)
			{
				this.Init(new RenderStateManager.StateValue(inDefaultValue), inName, inValueEnumName);
			}

			public void Init(RenderStateManager.StateValue inHardwareDefaultValue, RenderStateManager.StateValue inContextDefaultValue, string inName)
			{
				this.Init(inHardwareDefaultValue, inContextDefaultValue, inName, null);
			}

			public void Init(ulong inHardwareDefaultValue, ulong inContextDefaultValue, string inName)
			{
				this.Init(new RenderStateManager.StateValue(inHardwareDefaultValue), new RenderStateManager.StateValue(inContextDefaultValue), inName);
			}

			public void Init(RenderStateManager.StateValue inHardwareDefaultValue, RenderStateManager.StateValue inContextDefaultValue, string inName, string inValueEnumName)
			{
				this.mLastCommittedValue = inHardwareDefaultValue;
				this.mHardwareDefaultValue = inHardwareDefaultValue;
				this.mValue = inHardwareDefaultValue;
				this.mContextDefaultValue = inContextDefaultValue;
				this.mCommitFunc = this.mManager.GetCommitFunc(this);
				this.mName = inName;
				this.mContextDefPrev = this.mManager.mContextDefDummyHead;
				this.mContextDefNext = this.mManager.mContextDefDummyHead.mContextDefNext;
				RenderStateManager.State state = this.mContextDefPrev;
				this.mContextDefNext.mContextDefPrev = this;
				state.mContextDefNext = this;
			}

			public void Init(ulong inHardwareDefaultValue, ulong inContextDefaultValue, string inName, string inValueEnumName)
			{
				this.Init(new RenderStateManager.StateValue(inHardwareDefaultValue), new RenderStateManager.StateValue(inContextDefaultValue), inName, inValueEnumName);
			}

			public void Reset()
			{
				this.mLastCommittedValue = this.mHardwareDefaultValue;
			}

			public bool HasContextDefault()
			{
				return this.mContextDefPrev != this;
			}

			public bool IsDirty()
			{
				return this.mDirtyPrev != this;
			}

			public void SetDirty()
			{
				if (this.IsDirty())
				{
					return;
				}
				this.mDirtyPrev = this.mManager.mDirtyDummyHead;
				this.mDirtyNext = this.mManager.mDirtyDummyHead.mDirtyNext;
				RenderStateManager.State state = this.mDirtyPrev;
				this.mDirtyNext.mDirtyPrev = this;
				state.mDirtyNext = this;
				this.mManager.mWouldCommitStateDirty = true;
			}

			public void ClearDirty()
			{
				this.ClearDirty(false);
			}

			public void ClearDirty(bool inActAsCommit)
			{
				if (!this.IsDirty())
				{
					return;
				}
				if (inActAsCommit)
				{
					this.mLastCommittedValue = this.mValue;
				}
				this.mDirtyPrev.mDirtyNext = this.mDirtyNext;
				this.mDirtyNext.mDirtyPrev = this.mDirtyPrev;
				this.mDirtyNext = this;
				this.mDirtyPrev = this;
				this.mManager.mWouldCommitStateDirty = true;
			}

			public void SetValue(RenderStateManager.StateValue inValue)
			{
				if (inValue == this.mValue)
				{
					return;
				}
				this.mManager.Flush();
				if (this.mManager.mCurrentContext != null)
				{
					this.mManager.mCurrentContext.SplitChildren();
					this.mManager.mCurrentContext.mJournal.Add(new RenderStateManager.Context.JournalEntry(this, this.mValue, inValue));
				}
				this.mValue = inValue;
				this.SetDirty();
			}

			public void SetValue(uint inDword)
			{
				this.SetValue(new RenderStateManager.StateValue(inDword));
			}

			public void SetValue(float inFloat)
			{
				this.SetValue(new RenderStateManager.StateValue(inFloat));
			}

			public void SetValue(IntPtr inPtr)
			{
				this.SetValue(new RenderStateManager.StateValue(inPtr));
			}

			public void SetValue(float inX, float inY, float inZ, float inW)
			{
				this.SetValue(new RenderStateManager.StateValue(inX, inY, inZ, inW));
			}

			public uint GetDword()
			{
				return this.mValue.GetDword();
			}

			public float GetFloat()
			{
				return this.mValue.GetFloat();
			}

			public object GetPtr()
			{
				return this.mValue.GetPtr();
			}

			public void GetVector(ref float outX, ref float outY, ref float outZ, ref float outW)
			{
				this.mValue.GetVector(ref outX, ref outY, ref outZ, ref outW);
			}

			public RenderStateManager mManager;

			public uint[] mContext;

			public RenderStateManager.State mDirtyPrev;

			public RenderStateManager.State mDirtyNext;

			public RenderStateManager.StateValue mValue;

			public RenderStateManager.StateValue mHardwareDefaultValue;

			public RenderStateManager.StateValue mContextDefaultValue;

			public RenderStateManager.StateValue mLastCommittedValue;

			public RenderStateManager.State mContextDefPrev;

			public RenderStateManager.State mContextDefNext;

			public RenderStateManager.State.FCommitFunc mCommitFunc;

			public string mName;

			public delegate bool FCommitFunc(RenderStateManager.State inState);
		}

		public class Context
		{
			public Context()
			{
				this.mParentContext = null;
				this.mJournalFloor = 0U;
				this.mParentContext = null;
			}

			public virtual void Dispose()
			{
				this.SplitChildren();
				if (this.mParentContext != null)
				{
					int count = this.mParentContext.mChildContexts.Count;
					for (int i = 0; i < count; i++)
					{
						if (this.mParentContext.mChildContexts[i] == this)
						{
							this.mParentContext.mChildContexts.RemoveAt(i);
							return;
						}
					}
				}
			}

			public void RevertState()
			{
			}

			public void PushState()
			{
			}

			public void PopState()
			{
			}

			public void Unacquire()
			{
				this.Unacquire(false);
			}

			public void Unacquire(bool inIgnoreParent)
			{
			}

			public void Reacquire()
			{
				this.Reacquire(false);
			}

			public void Reacquire(bool inIgnoreParent)
			{
			}

			public void SplitChildren()
			{
			}

			public List<RenderStateManager.Context.JournalEntry> mJournal = new List<RenderStateManager.Context.JournalEntry>();

			public List<RenderStateManager.Context> mChildContexts = new List<RenderStateManager.Context>();

			public List<uint> mFloorStack = new List<uint>();

			public uint mJournalFloor;

			public RenderStateManager.Context mParentContext;

			public class JournalEntry
			{
				public JournalEntry()
				{
					this.mState = null;
				}

				public JournalEntry(RenderStateManager.State inState, RenderStateManager.StateValue inOldValue, RenderStateManager.StateValue inNewValue)
				{
					this.mState = inState;
					this.mOldValue = inOldValue;
					this.mNewValue = inNewValue;
				}

				public RenderStateManager.State mState;

				public RenderStateManager.StateValue mOldValue = new RenderStateManager.StateValue();

				public RenderStateManager.StateValue mNewValue = new RenderStateManager.StateValue();
			}
		}
	}
}
