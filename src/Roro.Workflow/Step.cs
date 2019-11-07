using Roro.Workflow.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Roro.Workflow
{
    public abstract class Step : ObservableObject
    {
        public Guid Id { get; }

        public string Name { get; set; }

        public ObservableCollection<StepInput> Inputs { get; }

        public ObservableCollection<StepOutput> Outputs { get; }

        public StepExecutionState State { get; set; }

        public abstract StepExecutionResult Execute(StepExecutionContext context);

        #region Navigation

        public Step Parent { get; }

        public ObservableCollection<Step> Children { get; }

        public Step FirstChild => Children.FirstOrDefault();

        public Step LastChild => Children.LastOrDefault();

        public Step PreviousSibling => Parent?.Children.ElementAtOrDefault(ChildIndex - 1);

        public Step NextSibling => Parent?.Children.ElementAtOrDefault(ChildIndex + 1);

        public int ChildIndex => Parent?.Children.IndexOf(this) ?? -1;

        public bool IsFirstChild => Parent?.FirstChild == this;

        public bool IsLastChild => Parent?.LastChild == this;

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
        }
    }
}
