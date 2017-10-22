using System;

namespace Roro.Workflow
{
    public sealed class DecisionNode : Node
    {
        public Guid True { get; set; }

        public Guid False { get; set; }

        public override Guid Execute()
        {
            Console.WriteLine("Execute: {0} {1}", this.Id, this.GetType().Name);
            return this.True;
        }

        public override void SetNext(Guid id)
        {
            this.True = id;
        }
    }
}
