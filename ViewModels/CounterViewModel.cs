using Counter.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Counter.ViewModels;

public class CounterViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private CounterModel _cm;

    public string Name => _cm.Name;
    public int Count
    {
        get => _cm.Count;
        set
        {
            _cm.Count = value;
            OnPropertyChanged();
        }
    }
    public ICommand IncrementCountCommand { get; }
    public ICommand DecrementCountCommand { get; }

    public CounterViewModel(CounterModel cm)
    {
        _cm = cm;
        IncrementCountCommand = new Command(IncrementCount);
        DecrementCountCommand = new Command(DecrementCount);
    }

    private void IncrementCount()
    {
        Count++;
    }

    private void DecrementCount()
    {
        Count--;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}