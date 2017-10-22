using System;

namespace Roro.Workflow
{
    public sealed class StartNode : Node
    {
        public Guid Next { get; set; }

        public StartNode()
        {
            this.Activity = new StartNodeActivity();
        }

        public override Guid Execute()
        {
            Console.WriteLine("Execute: {0} {1}", this.Id, this.GetType().Name);
            return this.Next;
        }

        public override void SetNext(Guid id)
        {
            this.Next = id;
        }
    }
}
