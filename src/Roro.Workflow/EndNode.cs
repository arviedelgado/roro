using System;

namespace Roro.Workflow
{
    public sealed class EndNode : Node
    {
        public EndNode()
        {
            this.Activity = new EndNodeActivity();
        }

        public override Guid Execute()
        {
            Console.WriteLine("Execute: {0} {1}", this.Id, this.GetType().Name);
            return Guid.Empty;
        }

        public override void SetNext(Guid id)
        {
            Console.WriteLine("Error: Cannot set next to {0}.", this.GetType().Name);
        }
    }

}
