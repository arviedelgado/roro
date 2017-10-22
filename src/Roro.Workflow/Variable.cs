using System;

namespace Roro.Workflow
{
    public class Variable
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Expression { get; set; }

        public object Value { get; set; }
    }
}