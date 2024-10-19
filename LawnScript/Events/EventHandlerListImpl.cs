using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript.Events
{
    internal class EventHandlerListImpl<T> : EventHandlerList<T> where T : GameEvent<T>
    {
        public bool Subscribe(EventHandler<T> handler)
        {
            LinkedListNode<EventHandler<T>>? curr = handlers.First;
            while (curr != null)
            {
                if (curr.Value.priority > handler.priority) // Behavior: the last subscribed handler has the highest priority.
                    break;
                curr = curr.Next;
            }
            if (curr == null)
                handlers.AddLast(handler);
            else
                handlers.AddBefore(curr, handler);
            return true;
        }

        private readonly LinkedList<EventHandler<T>> handlers = new();

        public bool Unsubscribe(EventHandler<T> handler)
        {
            return handlers.Remove(handler);
        }

        /// <summary>
        /// Fire an event.
        /// </summary>
        /// <param name="gamePredicate">Original game logic adapted into a closure.</param>
        /// <param name="gameEvent">The event body.</param>
        /// <returns>If the event was NOT cancelled.</returns>
        public bool Fire(GameEvent<T> gameEvent, Action<GameEvent<T>>? gamePredicate = null)
        {
            FireBeforeGame(gameEvent, out var nextNode);
            if (!gameEvent.IsCancelled)
                gamePredicate?.Invoke(gameEvent);
            FireAfterGame(gameEvent, in nextNode);
            return !gameEvent.IsCancelled;
        }

        private void FireBeforeGame(GameEvent<T> gameEvent, out LinkedListNode<EventHandler<T>>? nextNode)
        {
            LinkedListNode<EventHandler<T>>? curr = handlers.First;
            while (curr != null)
            {
                if (curr.Value.priority > EventPriority.Game)
                    break;
                curr.Value.Invoke(gameEvent);
                curr = curr.Next;
            }
            nextNode = curr;
        }

        private void FireAfterGame(GameEvent<T> gameEvent, in LinkedListNode<EventHandler<T>>? initialNode)
        {
            if (initialNode == null)
                return;
            LinkedListNode<EventHandler<T>>? curr = initialNode;
            while (curr != null)
            {
                if (curr.Value.priority <= EventPriority.Game)
                    throw new ArgumentOutOfRangeException("The handler list has been corrupted!");
                curr.Value.Invoke(gameEvent);
                curr = curr.Next;
            }
            return;
        }
    }
}
