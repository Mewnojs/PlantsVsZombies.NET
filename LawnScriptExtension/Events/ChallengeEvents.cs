using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawn;

namespace LawnScript.Events
{
    public class ChallengeEvents
    {
        public sealed class InitLevelEvent : ActorEvent<InitLevelEvent, Challenge>
        {
        }

        public sealed class AfterInitLevelEvent : NonCancellableActorEvent<AfterInitLevelEvent, Challenge>
        {
        }

        public sealed class InitZombieWavesEvent : ActorEvent<InitZombieWavesEvent, Challenge>
        {
        }

        public sealed class AfterInitZombieWavesEvent : NonCancellableActorEvent<AfterInitZombieWavesEvent, Challenge>
        {
        }
    }
}