using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Roro.Workflow
{
    public class Document
    {
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public List<Page> Pages { get; }
    }
}