using System;

namespace Roro.Workflow
{
    public sealed class ProcessNode : Node
    {
        public Guid Next { get; set; }

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
