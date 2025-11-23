using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastLane.Core.Models;

namespace FastLane.App.ViewModels;

public class DashboardViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Kpi> _kpis = new();
    public ObservableCollection<Kpi> Kpis
    {
        get => _kpis;
        set
        {
            _kpis = value;
            OnPropertyChanged();
        }
    }

    public DashboardViewModel()
    {
        // Initialize with sample KPI data
        Kpis.Add(new Kpi { Name = "Total SKUs", Value = 0m, Unit = "count" });
        Kpis.Add(new Kpi { Name = "Inventory Value", Value = 0m, Unit = "USD" });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
