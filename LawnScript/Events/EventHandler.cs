using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript.Events
{
    public enum EventPriority : sbyte
    {
        OverloadLowest = -4,
        OverloadLow = -3,
        Overload = -2,
        OverloadHigh = -1,
        Game = 0,
        Lowest = 1,
        Low = 2,
        Normal = 3,
        High = 4,
        Highest = 5,
        Monitor = 0x7F,
    }

    public class EventHandler<T> where T : GameEvent<T>
    {
        public readonly EventPriority priority = (EventPriority)(-0x80);

        public bool ignoreCancelled;

        private readonly Func<GameEvent<T>, bool> process;

        public readonly Content plugin;

        public EventHandler(Func<GameEvent<T>, bool> process, Content plugin, bool ignoreCancelled = true, EventPriority priority = EventPriority.Normal)
        {
            if (priority == EventPriority.Game)
                throw new ArgumentException($"The handler of priority \"{priority}\" cannot be initialized.");
            this.priority = priority;
            this.process = process;
            this.ignoreCancelled = ignoreCancelled;
        }

        public bool Invoke(GameEvent<T> gameEvent)
        {
            if (ignoreCancelled && gameEvent.IsCancelled)
                return false;
            return process.Invoke(gameEvent);
        }
    }
}
