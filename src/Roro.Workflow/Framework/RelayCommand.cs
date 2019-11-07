using System;
using System.Windows.Input;

namespace Roro.Workflow.Framework
{
    public sealed class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecute;

        private readonly Action _execute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => CanExecute();

        void ICommand.Execute(object parameter) => Execute();

        public RelayCommand(Func<bool> canExecute, Action execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute()
        {
            return _canExecute.Invoke();
        }

        public void Execute()
        {
            if (CanExecute())
                _execute.Invoke();
        }

    }
}
