using System;
using System.Collections.Generic;

namespace Sexy
{
    internal static class LinkedListExtensions
    {
        public static void Insert<T>(this LinkedList<T> list, LinkedListNode<T> position, T newNode) where T : class
        {
            list.AddAfter(position, newNode);
        }
    }
}
