using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript.Events
{
    public abstract class ActorEvent<T, A> : GameEvent<T> where T : ActorEvent<T, A> where A : class
    {
        public A? Actor { get; private set; }

        protected static string EventName { get => typeof(T).Name; }

        public static ActorEvent<T, A> GetNew(A actor)
        {
            return GetNew(EventName + "@" + typeof(A).Name, true, actor);
        }

        public override void Initialize(string name, bool cancellable, params object[] args)
        {
            base.Initialize(name, cancellable, args);
            Actor = (A?)args.GetValue(0); // actor
        }

        public override void Reset()
        {
            base.Reset();
            Actor = null;
        }
    }

    public abstract class NonCancellableActorEvent<T, A> : GameEvent<T> where T : NonCancellableActorEvent<T, A> where A : class
    {
        public A? Actor { get; private set; }

        protected static string EventName { get => typeof(T).Name; }

        public static NonCancellableActorEvent<T, A> GetNew(A actor)
        {
            return GetNew(EventName + "@" + typeof(A).Name, false, actor);
        }

        public override void Initialize(string name, bool cancellable, params object[] args)
        {
            base.Initialize(name, cancellable, args);
            Actor = (A?)args.GetValue(0); // actor
        }

        public override void Reset()
        {
            base.Reset();
            Actor = null;
        }
    }
}
