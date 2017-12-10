using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class PreparationNodeActivity : Activity
    {
        public override bool AllowUserToEditArgumentRowList => true;

        public override bool AllowUserToEditArgumentColumn1 => true;

        public override bool AllowUserToEditArgumentColumn2 => true;

        public override bool AllowUserToEditArgumentColumn3 => true;

        [DataMember]
        private List<InOutArgument> InOutArguments { get; set; }

        protected override List<InOutArgument> GetInOutArguments() => this.InOutArguments;
        
        public PreparationNodeActivity() => this.InOutArguments = new List<InOutArgument>();

        public void Execute(ActivityContext context)
        {
            foreach (var inputOutput in this.InOutArguments)
            {
                throw new NotImplementedException();
            }
        }
    }
}
