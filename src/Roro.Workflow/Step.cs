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

        public Flow Flow => Get_Flow();

        public Step Parent => Get_Parent();

        public StepCollection Children { get; }

        public bool HasChildren => Children.Count > 0;

        public Step FirstChild => Children.FirstOrDefault();

        public Step LastChild => Children.LastOrDefault();

        public Step PreviousSibling => (Parent is null ? Flow.Steps : Parent.Children).ElementAtOrDefault(ChildIndex - 1);

        public Step NextSibling => (Parent is null ? Flow.Steps : Parent.Children).ElementAtOrDefault(ChildIndex + 1);

        public int ChildIndex => (Parent is null ? Flow.Steps : Parent.Children).IndexOf(this);

        public bool IsFirstChild => (Parent is null ? Flow.Steps : Parent.Children).FirstOrDefault() == this;

        public bool IsLastChild => (Parent is null ? Flow.Steps : Parent.Children).LastOrDefault() == this;

        #endregion

        protected Step()
        {
            Id = Guid.NewGuid();
            Name = GetType().Name + '_' + Id;
            Inputs = new ObservableCollection<StepInput>();
            Outputs = new ObservableCollection<StepOutput>();
            Children = new StepCollection(this);
            AddSiblingCommand = new RelayCommand(() => IsLastChild, () => (Parent is null ? Flow.Steps : Parent.Children).AddCommand.Execute());
        }

        #region Flow

        private Flow _flow;

        private Flow Get_Flow()
        {
            return _flow;
        }

        internal void InternalSet_Flow(Flow value)
        {
            NotifyPropertyChanged(ref _flow, value, nameof(Flow));
        }

        #endregion

        #region Parent

        private Step _parent;

        private Step Get_Parent()
        {
            return _parent;
        }

        internal void InternalSet_Parent(Step value)
        {
            NotifyPropertyChanged(ref _parent, value, nameof(Parent));
        }

        #endregion

        public RelayCommand AddSiblingCommand { get; }

    }
}
