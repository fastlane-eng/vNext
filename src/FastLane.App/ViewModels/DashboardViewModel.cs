using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
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

    // Commands for navigating to different sections
    public ICommand NavigateToInventoryCommand { get; }
    public ICommand NavigateToPurchaseOrdersCommand { get; }
    public ICommand NavigateToForecastCommand { get; }
    public ICommand NavigateToAnalyticsCommand { get; }

    public DashboardViewModel()
    {
        // Initialize with sample KPI data
        Kpis.Add(new Kpi { Name = "Total SKUs", Value = 0m, Unit = "count" });
        Kpis.Add(new Kpi { Name = "Inventory Value", Value = 0m, Unit = "USD" });

        // Initialize navigation commands
        NavigateToInventoryCommand = new Command(async () =>
        {
            await App.NavigationService.NavigateToAsync(new Views.InventoryPage());
        });

        NavigateToPurchaseOrdersCommand = new Command(async () =>
        {
            await App.NavigationService.NavigateToAsync(new Views.PurchaseOrdersPage());
        });

        NavigateToForecastCommand = new Command(async () =>
        {
            await App.NavigationService.NavigateToAsync(new Views.ForecastPage());
        });

        NavigateToAnalyticsCommand = new Command(async () =>
        {
            await App.NavigationService.NavigateToAsync(new Views.AnalyticsPage());
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
