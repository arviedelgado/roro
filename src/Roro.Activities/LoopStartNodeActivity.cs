using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class LoopStartNodeActivity : Activity
    {
        public override bool AllowUserToEditArgumentColumn3 => true;

        [DataMember]
        private List<InArgument> InArguments { get; set; }

        [DataMember]
        private List<OutArgument> OutArguments { get; set; }

        protected override List<InArgument> GetInArguments() => this.InArguments;

        protected override List<OutArgument> GetOutArguments() => this.OutArguments;

        public LoopStartNodeActivity()
        {
            this.InArguments = new List<InArgument>()
            {
                new InArgument<Collection>("Collection")
            };
            this.OutArguments = new List<OutArgument>();
        }

        public void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
