using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class EndNodeActivity : Activity
    {
        public override bool AllowUserToEditArgumentRowList => true;

        public override bool AllowUserToEditArgumentColumn1 => true;

        public override bool AllowUserToEditArgumentColumn2 => true;

        public override bool AllowUserToEditArgumentColumn3 => true;

        [DataMember]
        private List<InArgument> InArguments { get; set; }

        protected override List<InArgument> GetInArguments() => this.InArguments;

        public EndNodeActivity() => this.InArguments = new List<InArgument>();

        public void Execute(ActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
