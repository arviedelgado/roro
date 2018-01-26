using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public class Output
    {
        [DataMember]
        internal string Name { get; set; }

        [DataMember]
        internal string Type { get; set; }

        [DataMember]
        internal string Value { get; set; }

        internal Output()
        {
            this.Name = string.Empty;
            this.Type = DataType.GetDefault().Id;
            this.Value = string.Empty;
        }
    }

    [DataContract]
    public sealed class Output<T> : Output where T : DataType, new()
    {
        public Output() => this.Type = new T().Id;
    }
}