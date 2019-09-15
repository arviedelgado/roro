using System;
using System.Windows.Input;

namespace Roro.Workspace.Framework
{
    public sealed class Command : ICommand
    {
        public bool CanExecute()
        {
            return _canExecute.Invoke();
        }

        public void Execute()
        {
            if (CanExecute())
            {
                _execute.Invoke();
            }
        }

        public Command(Func<bool> canExecute, Action execute)
        {
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        // non-public

        private readonly Func<bool> _canExecute;

        private readonly Action _execute;

        #region ICommand

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => CanExecute();

        void ICommand.Execute(object parameter) => Execute();

        #endregion
    }
}
