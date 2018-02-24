using System.Runtime.Serialization;

namespace Roro.Activities
{
    public interface Input
    {
        string Name { get; }

        string Type { get; }

        string Value { get; }
    }

    public interface Input<T> : Input
    {

    }

    public interface Output
    {
        string Name { get; }

        string Type { get; }

        string Value { get; }
    }

    public interface Output<T> : Output
    {

    }

    [DataContract]
    public class Argument : Input
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Value { get; set; }

        internal Argument()
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