using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawn;

namespace LawnScript.Events
{
    public class ZombieEvents : GameObjectEvents
    {

        public sealed class AgeEvent : ActorEvent<AgeEvent, Zombie>
        {
            public int Age { get => Actor.mZombieAge; set => Actor.mZombieAge = value; }
        }

        public sealed class UpdateEvent : ActorEvent<UpdateEvent, Zombie>
        {
        }

        public sealed class AfterUpdateEvent : NonCancellableActorEvent<AfterUpdateEvent, Zombie>
        {
        }

        public sealed class InitializeTypeEvent : ActorEvent<InitializeTypeEvent, Zombie>
        {
        }

        public sealed class AfterInitializeTypeEvent : NonCancellableActorEvent<AfterInitializeTypeEvent, Zombie>
        {
        }
    }
}
