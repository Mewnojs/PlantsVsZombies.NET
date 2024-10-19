using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawn;

namespace LawnScript.Events
{
    public class BoardEvents
    {
        public sealed class InitLevelEvent : ActorEvent<InitLevelEvent, Board>
        {
        }

        public sealed class AfterInitLevelEvent : NonCancellableActorEvent<AfterInitLevelEvent, Board>
        {
        }

        public sealed class InitLevelNumberStrEvent : ActorEvent<InitLevelNumberStrEvent, Board>
        {
        }

        public sealed class AfterInitLevelNumberStrEvent : NonCancellableActorEvent<AfterInitLevelNumberStrEvent, Board>
        {
        }

        public sealed class InitLevelSunMoneyEvent : ActorEvent<InitLevelSunMoneyEvent, Board>
        {
        }

        public sealed class AfterInitLevelSunMoneyEvent : NonCancellableActorEvent<AfterInitLevelSunMoneyEvent, Board>
        {
        }

        public sealed class InitLevelSeedBankEvent : ActorEvent<InitLevelSeedBankEvent, Board>
        {
        }

        public sealed class AfterInitLevelSeedBankEvent : NonCancellableActorEvent<AfterInitLevelSeedBankEvent, Board>
        {
        }

        public sealed class PickBackgroundTypeEvent : ActorEvent<PickBackgroundTypeEvent, Board>
        {
        }

        public sealed class AfterPickBackgroundTypeEvent : ActorEvent<AfterPickBackgroundTypeEvent, Board>
        {
        }

        public sealed class PickPlantRowEvent : ActorEvent<PickPlantRowEvent, Board>
        {
        }

        public sealed class AfterPickPlantRowEvent : ActorEvent<AfterPickPlantRowEvent, Board>
        {
        }

        public sealed class PickGridSquareEvent : ActorEvent<PickGridSquareEvent, Board>
        {
        }

        public sealed class AfterPickGridSquareEvent : ActorEvent<AfterPickGridSquareEvent, Board>
        {
        }

        public sealed class PickGraveStoneEvent : ActorEvent<PickGraveStoneEvent, Board>
        {
        }

        public sealed class AfterPickGraveStoneEvent : ActorEvent<AfterPickGraveStoneEvent, Board>
        {
        }

        public sealed class InitZombieWavesEvent : ActorEvent<InitZombieWavesEvent, Board>
        {
        }

        public sealed class AfterInitZombieWavesEvent : NonCancellableActorEvent<AfterInitZombieWavesEvent, Board>
        {
        }
    }
}