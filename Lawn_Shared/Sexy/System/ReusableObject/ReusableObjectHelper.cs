using System;
using System.Collections.Generic;

namespace Sexy
{
    internal class ReusableObjectHelperCommon<T> where T : IReusable 
    {
        protected ReusableObjectHelperCommon() { }

        public static T GetNewNullable()
        {
            if (unusedInstances.Count != 0)
            {
                T obj = unusedInstances.Pop();
                singleton.ResetOnGet(obj); // @1: Moved obj.Reset() from PushOnToReuseStack()
                return obj;
            }
            if (default(T) != null)
            {
                return default(T);
            }
            else
                return default(T); //Activator.CreateInstance<T>(); Use "null" as hinting for ctor calling
        }

        public static void PushOnToReuseStack(T obj)
        {
            singleton.ResetOnPush(obj);
            unusedInstances.Push(obj);
        }

        public virtual void ResetOnGet(T obj) { }

        public virtual void ResetOnPush(T obj) { }

        public static void Initialize(int count)
        {
            unusedInstances = new Stack<T>(count);
            singleton = new ReusableObjectHelperCommon<T>();
        }

        public static void Initialize()
        {
            unusedInstances = new Stack<T>();
            singleton = new ReusableObjectHelperCommon<T>();
        }

        public static void Dispose()
        {
            unusedInstances = null;
        }

        protected static Stack<T> unusedInstances;

        protected static ReusableObjectHelperCommon<T> singleton;
    }

    internal class ReusableObjectHelper<T>: ReusableObjectHelperCommon<T> where T : IReusable 
    {
        public override void ResetOnPush(T obj) 
        {
            obj.Reset();
        }
    }

    internal class LazyReusableObjectHelper<T>: ReusableObjectHelperCommon<T> where T : IReusable//, new()
    {
        public override void ResetOnGet(T obj)
        {
            obj.Reset();
        }
    }
}
