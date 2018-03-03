namespace Roro.Activities
{
    public class Argument
    {
        public string Name { get; set; }

        internal string Type { get; set; }

        public string Value { get; set; }

        public Argument()
        {
            this.Name = string.Empty;
            this.Type = DataType.GetDefault().GetType().FullName;
            this.Value = string.Empty;
        }
    }

    public class Input : Argument
    {
       
    }

    public class Input<T> : Input where T : DataType, new()
    {
        public Input()
        {
            this.Type = typeof(T).FullName;
        }
    }

    public class Output : Argument
    {

    }

    public class Output<T> : Output where T : DataType, new()
    {
        public Output()
        {
            this.Type = typeof(T).FullName;
        }
    }
}