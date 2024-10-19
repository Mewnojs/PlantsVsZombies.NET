using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript.Events
{
    public class GameEvent<T> : IDisposable where T : GameEvent<T>
    {
        protected GameEvent()
        {
        }

        public bool Cancellable { get; private set; } = false;

        private static EventHandlerListImpl<T> eventHandlers = new();

        private string name = string.Empty;

        private bool isCancelled = false;

        public bool IsCancelled { get => isCancelled; set { if (Cancellable) isCancelled = value; } }

        public static EventHandlerList<T> Handlers { get => eventHandlers; }

        public string Name { get => name; }

        public bool Fire(Action<GameEvent<T>>? gamePredicate = null)
        {
            return eventHandlers.Fire(this, gamePredicate);
        }

        public virtual void Initialize(string name, bool cancellable, params object[] args)
        {
            Cancellable = cancellable;
            this.name = name;
        }

        public virtual void Reset()
        {
            IsCancelled = false;
        }

        protected static T GetNew(string name, bool cancellable, params object[] args)
        {
            if (!unused.TryPop(out var result))
                result = Activator.CreateInstance<T>();
            result.Initialize(name, cancellable, args);
            return result;
        }

        public void Dispose()
        {
            PrepareForReuse();
        }

        public void PrepareForReuse()
        {
            Reset();
            unused.Push((T)this);
        }

        //private static ObjectPool<T> poolo = new DefaultObjectPool<T>(new DefaultPooledObjectPolicy<T>());

        private static ConcurrentStack<T> unused = new ConcurrentStack<T>();
    }
}
