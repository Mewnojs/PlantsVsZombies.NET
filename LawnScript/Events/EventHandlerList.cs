using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript.Events
{
    public interface EventHandlerList<T> where T : GameEvent<T>
    {
        /// <summary>
        /// Try to subscribe to the event.
        /// </summary>
        /// <param name="handler">If the subscription was successful.</param>
        /// <returns></returns>
        public bool Subscribe(EventHandler<T> handler);

        /// <summary>
        /// Try to unsubscribe from the event.
        /// </summary>
        /// <param name="handler">If the operation was successful.</param>
        /// <returns></returns>
        public bool Unsubscribe(EventHandler<T> handler);
    }
}
