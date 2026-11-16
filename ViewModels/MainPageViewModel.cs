using Counter.Models;
using Counter.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Counter.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<CounterViewModel> Counters { get; } = new();

    private string _newCounterName = string.Empty;
    public string NewCounterName {
        get => _newCounterName;
        set {
            _newCounterName = value;
            OnPropertyChanged();
        }
    }
    public ICommand AddCounterCommand { get; }

    public MainPageViewModel()
    {
        AddCounterCommand = new Command(AddCounter);

        List<CounterModel> LoadedCounters = CounterSave.Load();
        LoadedCounters.ForEach((counter) =>
        {
            AddCounterToList(counter);
        });
    }

    public void AddCounter()
    {
        CounterModel cm = new CounterModel
        {
            Count = 0,
            Name = _newCounterName
        };

        Counters.Add(new CounterViewModel(cm));
    }

    private void AddCounterToList(CounterModel cm)
    {
        CounterViewModel viewModel = new CounterViewModel(cm);
        Counters.Add(viewModel);
    }

    public void SaveCounters()
    {
        var models = Counters.Select((counter) =>
        {
            return new CounterModel
            {
                Name = counter.Name,
                Count = counter.Count
            };
        }).ToList();

        CounterSave.Save(models);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}