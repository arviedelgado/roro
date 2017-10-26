using Roro.Activities;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Roro.Workflow
{
    [DataContract]
    public abstract class Node
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Rectangle Bounds { get; set; }

        [DataMember]
        public Activity Activity { get; set; }

        public abstract void SetNext(Guid id);

        public abstract Guid Execute();

        public Node()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("{0}-{1}", this.GetType().Name, DateTime.Now.Ticks);
            if (this.GetType() == typeof(StartNode))
            {
                this.Activity = new StartNodeActivity();
            }
            else if (this.GetType() == typeof(EndNode))
            {
                this.Activity = new EndNodeActivity();
            }
        }
    }
}
