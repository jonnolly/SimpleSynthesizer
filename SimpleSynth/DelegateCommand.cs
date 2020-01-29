using System;
using System.Windows.Input;

namespace SimpleSynth
{
    public class DelegateCommand : ICommand
    {
        // executes
        private readonly Action<object> _executeActionParam;
        private readonly Action _executeAction;

        // canExecutes
        private readonly Func<object, bool> _canExecuteActionParam;
        private readonly Func<bool> _canExecuteAction;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteAction)
        {
            _executeActionParam = executeAction;
            _canExecuteActionParam = canExecuteAction;
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public void Execute(object parameter) => _executeActionParam(parameter);
        public void Execute() => _executeAction();

        public bool CanExecute(object parameter) => _canExecuteActionParam?.Invoke(parameter) ?? true;
        public bool CanExecute() => _canExecuteAction?.Invoke() ?? true;

        public event EventHandler CanExecuteChanged;

        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
