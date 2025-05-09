using System;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Hotel_Client.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Func<Task>? _asyncExecute;
        private readonly Action? _syncExecute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Func<Task> asyncExecute, Func<bool>? canExecute = null)
        {
            _asyncExecute = asyncExecute ?? throw new ArgumentNullException(nameof(asyncExecute));
            _canExecute = canExecute;
        }

        public RelayCommand(Action syncExecute, Func<bool>? canExecute = null)
        {
            _syncExecute = syncExecute ?? throw new ArgumentNullException(nameof(syncExecute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public async void Execute(object? parameter)
        {
            if (_asyncExecute != null)
                await _asyncExecute();
            else
                _syncExecute?.Invoke();
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T, Task>? _asyncExecute;
        private readonly Action<T>? _syncExecute;
        private readonly Predicate<T>? _canExecute;

        public RelayCommand(Func<T, Task> asyncExecute, Predicate<T>? canExecute = null)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<T> syncExecute, Predicate<T>? canExecute = null)
        {
            _syncExecute = syncExecute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (parameter == null && typeof(T).IsValueType)
                return _canExecute == null;

            return _canExecute == null || _canExecute((T)parameter!);
        }

        public async void Execute(object? parameter)
        {
            if (_asyncExecute != null)
                await _asyncExecute((T)parameter!);
            else
                _syncExecute?.Invoke((T)parameter!);
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}