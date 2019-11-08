using Roro.Workflow.Framework;
using System;
using System.Linq;

namespace Roro.Workflow
{
    public sealed class Flow : ObservableObject
    {
        public Guid Id { get; }

        public string Name
        {
            get => _name;
            set => NotifyPropertyChanged(ref _name, value);
        }
        private string _name;

        public bool IsReadOnly { get; }

        public StepCollection Steps { get; }

        public Flow()
        {
            Id = Guid.NewGuid();
            Name = GetType().Name + '_' + Id;
            Steps = new StepCollection(this);
            Steps.AddCommand.Execute();
            Steps.First().Children.AddCommand.Execute();
        }
    }
}
