using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class LoopStartNodeActivity : Activity
    {
        [DataMember]
        private List<InArgument> InArguments { get; set; }

        [DataMember]
        private List<OutArgument> OutArguments { get; set; }

        public LoopStartNodeActivity()
        {
            //this.InArguments = new List<InArgument>()
            //{
            //    new Input<Collection>(){ Name = "Collection" }
            //};
            this.OutArguments = new List<OutArgument>();
        }

        public void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
