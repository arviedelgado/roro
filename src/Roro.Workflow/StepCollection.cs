using Roro.Workflow.Framework;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Roro.Workflow
{
    public class StepCollection : ObservableCollection<Step>
    {
        public Flow Flow { get; }

        public Step Parent { get; }

        public StepCollection(Step parent) : this(parent.Flow, parent)
        {
 
        }

        public StepCollection(Flow flow) : this(flow, null)
        {
 
        }

        private StepCollection(Flow flow, Step parent)
        {
            Flow = flow;
            Parent = parent;
            AddCommand = new RelayCommand(() => true, () => Add(new Statements.Action()));
        }

        protected override void ClearItems()
        {
            base.ClearItems();

            Items.ToList().ForEach(x =>
            {
                x.InternalSet_Flow(null);
                x.InternalSet_Parent(null);
            });
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            e.OldItems?.Cast<Step>().ToList().ForEach(x =>
            {
                x.InternalSet_Flow(null);
                x.InternalSet_Parent(null);
            });

            e.NewItems?.Cast<Step>().ToList().ForEach(x =>
            {
                x.InternalSet_Flow(Flow);
                x.InternalSet_Parent(Parent);
            });

            Parent?.NotifyPropertyChanged(null);

            Items.ToList().ForEach(x => x.NotifyPropertyChanged(null));
        }

        public RelayCommand AddCommand { get; }
    }
}
