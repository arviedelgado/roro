using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Roro.Workflow
{
    public sealed class PriorityQueue<T>
    {
        private readonly SortedList<int, Queue<T>> Priorities;

        public int Count => this.Priorities.Count;

        public PriorityQueue()
        {
            this.Priorities = new SortedList<int, Queue<T>>();
        }

        public void Enqueue(T item, int priority)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (this.Priorities.TryGetValue(priority, out Queue<T> existingQueue))
            {
                existingQueue.Enqueue(item);
            }
            else
            {
                var newQueue = new Queue<T>();
                this.Priorities.Add(priority, newQueue);
                newQueue.Enqueue(item);
            }
        }

        public T Dequeue()
        {
            var first = this.Priorities.First();
            var queue = first.Value;
            var item = queue.Dequeue();
            if (queue.Count == 0)
            {
                this.Priorities.Remove(first.Key);
            }
            return item;
        }

        public void Clear()
        {
            this.Priorities.Clear();
        }
    }
}