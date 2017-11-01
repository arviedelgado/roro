using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roro.Workflow
{
    [DataContract]
    public class Document
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Page> Pages { get; private set; }

        public Document()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Pages = new List<Page>();
            this.Pages.Add(new Page());
        }
    }
}