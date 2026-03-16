using System.ComponentModel;

namespace MiniSimulator.UI;

public class ViewModelBase : INotifyPropertyChanged
{
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}