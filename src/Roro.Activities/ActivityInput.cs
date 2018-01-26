using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public class Input
    {
        [DataMember]
        internal string Name { get; set; }

        [DataMember]
        internal string Type { get; set; }

        [DataMember]
        internal string Value { get; set; }

        internal Input()
        {
            this.Name = string.Empty;
            this.Type = DataType.GetDefault().Id;
            this.Value = string.Empty;
        }
    }

    [DataContract]
    public sealed class Input<T> : Input where T : DataType, new()
    {
        public Input() => this.Type = new T().Id;
    }
}