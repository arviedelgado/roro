using Roro.Workflow.Framework;
using Roro.Workflow.Statements;
using System;
using System.Collections.ObjectModel;
using Action = Roro.Workflow.Statements.Action;

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

        public Step RootStep { get; }

        public Flow()
        {
            Id = Guid.NewGuid();
            Name = GetType().Name + '_' + Id;
            RootStep = new Action();
            RootStep.AddChildCommand.Execute();
            RootStep.FirstChild.AddChildCommand.Execute();
        }
    }
}
