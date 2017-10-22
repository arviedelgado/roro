using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Roro.Workflow
{
    [DataContract]
    public sealed class PageNode : Node
    {
        [DataMember]
        private readonly List<Node> Nodes = new List<Node>();

        private readonly VariableCollection Variables = new VariableCollection();
        
        public void Add<T>() where T : Node, new()
        {
            var node = new T();
            if (this.Nodes.LastOrDefault() is Node last)
            {
                last.SetNext(node.Id);
            }
            this.Nodes.Add(node);
        }

        public Node GetNodeById(Guid id)
        {
            return this.Nodes.FirstOrDefault(x => x.Id == id);
        }

        public override void SetNext(Guid id)
        {
            throw new Exception("Error calling SetNext of PageNode " + this.Id);
        }

        public override Guid Execute()
        {
            return this.Nodes.First().Id;
        }
    }
}