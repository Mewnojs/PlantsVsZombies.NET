using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawn;

namespace LawnScript.Events
{
    public class ProjectileEvents : GameObjectEvents
    {
        public sealed class InitializeTypeEvent : ActorEvent<InitializeTypeEvent, Projectile>
        {
        }

        public sealed class AfterInitializeTypeEvent : NonCancellableActorEvent<AfterInitializeTypeEvent, Projectile>
        {
        }
    }
}
