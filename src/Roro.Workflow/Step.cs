using Roro.Workflow.Framework;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Roro.Workflow
{
    public abstract class Step : ObservableObject
    {
        public Guid Id { get; }

        public string Name
        {
            get => _name;
            set => NotifyPropertyChanged(ref _name, value);
        }
        private string _name;

        public string Number
        {
            get => (Parent is null ? string.Empty : Parent.Number + ".") + ChildIndex;
        }

        public ObservableCollection<StepInput> Inputs { get; }

        public ObservableCollection<StepOutput> Outputs { get; }

        public StepExecutionState State
        {
            get => _state;
            set => NotifyPropertyChanged(ref _state, value);
        }
        private StepExecutionState _state;

        public RelayCommand AddChildCommand { get; }

        public abstract StepExecutionResult Execute(StepExecutionContext context);

        #region Navigation

        public Step Parent
        {
            get => _parent;
            set => NotifyPropertyChanged(ref _parent, value);
        }
        private Step _parent;

        public ObservableCollection<Step> Children { get; }

        public Step FirstChild => Children.FirstOrDefault();

        public Step LastChild => Children.LastOrDefault();

        public Step PreviousSibling => Parent?.Children.ElementAtOrDefault(ChildIndex - 1);

        public Step NextSibling => Parent?.Children.ElementAtOrDefault(ChildIndex + 1);

        public int ChildIndex => Parent?.Children.IndexOf(this) ?? -1;

        public bool IsFirstChild => Parent?.FirstChild == this;

        public bool IsLastChild => Parent?.LastChild == this;

        public bool HasChildren => Children.Count > 0;

        public Step GetAncestor(Func<Step, bool> condition)
        {
            var ancestor = Parent;
            while (ancestor != null)
            {
                if (condition.Invoke(ancestor))
                    break;

                ancestor = ancestor.Parent;
            }

            return ancestor;
        }

        public Step GetPreviousSibling(Func<Step, bool> condition)
        {
            var previousSibling = Parent;
            while (previousSibling != null)
            {
                if (condition.Invoke(previousSibling))
                    break;

                previousSibling = previousSibling.PreviousSibling;
            }

            return previousSibling;
        }

        public Step GetNextSibling(Func<Step, bool> condition)
        {
            var nextSibling = Parent;
            while (nextSibling != null)
            {
                if (condition.Invoke(nextSibling))
                    break;

                nextSibling = nextSibling.NextSibling;                    
            }

            return nextSibling;
        }

        #endregion

        protected Step()
        {
            Id = Guid.NewGuid();
            Name = GetType().Name + '_' + Id;
            Inputs = new ObservableCollection<StepInput>();
            Outputs = new ObservableCollection<StepOutput>();
            Children = new ObservableCollection<Step>();
            Children.CollectionChanged += Children_CollectionChanged;
            AddChildCommand = new RelayCommand(() => true, AddChild);
        }

        private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(
                nameof(HasChildren),
                nameof(FirstChild),
                nameof(LastChild));

            Children.ToList().ForEach(child => child.NotifyPropertyChanged(
                nameof(child.Number),
                nameof(child.PreviousSibling),
                nameof(child.NextSibling),
                nameof(child.IsFirstChild),
                nameof(child.IsLastChild)));
        }

        private void AddChild()
        {
            Children.Add(new Statements.Action() { Parent = this });
        }
    }
}
