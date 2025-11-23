using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastLane.Core.Models;

namespace FastLane.App.ViewModels;

public class InventoryViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Sku> _skus = new();
    public ObservableCollection<Sku> Skus
    {
        get => _skus;
        set
        {
            _skus = value;
            OnPropertyChanged();
        }
    }

    public InventoryViewModel()
    {
        // Sample data
        Skus.Add(new Sku { Id = 1, SkuCode = "SKU001", Description = "Sample SKU 1", QuantityOnHand = 10, ReorderPoint = 5, UnitPrice = 1.99m });
        Skus.Add(new Sku { Id = 2, SkuCode = "SKU002", Description = "Sample SKU 2", QuantityOnHand = 20, ReorderPoint = 10, UnitPrice = 2.99m });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
