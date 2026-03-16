using System.Windows.Input;

namespace MiniSimulator.UI;

internal class DefaultCommand(Action action) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        action();
    }
}