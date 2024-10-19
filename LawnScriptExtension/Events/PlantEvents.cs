using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawn;

namespace LawnScript.Events
{
    public class PlantEvents : GameObjectEvents
    {

        public sealed class AnimateEvent : ActorEvent<AnimateEvent, Plant>
        {
        }
        public sealed class AfterAnimateEvent : NonCancellableActorEvent<AfterAnimateEvent, Plant>
        {
        }

        public sealed class UpdateEvent : ActorEvent<UpdateEvent, Plant>
        {
        }

        public sealed class AfterUpdateEvent : NonCancellableActorEvent<AfterUpdateEvent, Plant>
        {
        }

        public sealed class UpdateShootingEvent : ActorEvent<UpdateShootingEvent, Plant>
        {
        }

        public sealed class ShootingEvent : ActorEvent<ShootingEvent, Plant>
        {
        }

        public sealed class AfterShootingEvent : NonCancellableActorEvent<AfterShootingEvent, Plant>
        {
        }

        public sealed class FireEvent : ActorEvent<FireEvent, Plant>
        {
        }

        public sealed class AfterFireEvent : NonCancellableActorEvent<AfterFireEvent, Plant>
        {
        }

        public sealed class UpgradableEvent : NonCancellableActorEvent<UpgradableEvent, Plant>
        {
        }

        public sealed class InitializeTypeEvent : ActorEvent<InitializeTypeEvent, Plant>
        {
        }

        public sealed class AfterInitializeTypeEvent : NonCancellableActorEvent<AfterInitializeTypeEvent, Plant>
        {
        }
    }
}