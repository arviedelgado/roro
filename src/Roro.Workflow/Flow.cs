using Roro.Workflow.Framework;
using System;
using System.Collections.ObjectModel;

namespace Roro.Workflow
{
    public sealed class Flow : ObservableObject
    {
        public Guid Id { get; }

        public string Name { get; set; }

        public ObservableCollection<Step> Steps { get; }

        public Flow()
        {
            Id = Guid.NewGuid();
            Name = GetType().Name + '_' + Id;
        }
    }
}
