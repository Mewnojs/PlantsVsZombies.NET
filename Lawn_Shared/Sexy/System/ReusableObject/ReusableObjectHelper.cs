using System;
using System.Collections.Generic;

namespace Sexy
{
    internal class ReusableObjectHelper<T> where T : IReusable, new()
    {
        public T GetNew()
        {
            if (unusedInstances.Count != 0)
            {
                return unusedInstances.Pop();
            }
            if (default(T) != null)
            {
                return default(T);
            }
            return Activator.CreateInstance<T>();
        }

        public void PushOnToReuseStack(T obj)
        {
            obj.Reset();
            unusedInstances.Push(obj);
        }

        private Stack<T> unusedInstances = new Stack<T>();
    }
}
