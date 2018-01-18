using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class StartNodeActivity : Activity
    {
        //public override bool AllowUserToEditArgumentRowList => true;

        //public override bool AllowUserToEditArgumentColumn1 => true;

        //public override bool AllowUserToEditArgumentColumn2 => true;

        //public override bool AllowUserToEditArgumentColumn3 => true;

        //[DataMember]
        //private List<OutArgument> OutArguments { get; set; }

        //protected override List<OutArgument> GetOutArguments() => this.OutArguments;

        //public StartNodeActivity() => this.OutArguments = new List<OutArgument>();

        public void Execute(ActivityContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
