using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FastLane.Core.Interfaces;
using FastLane.Core.Models;
using FastLane.Data.Repositories;
using FastLane.Services;
using Microsoft.Maui.Controls;

namespace FastLane.App.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private readonly IInventoryService _inventoryService;
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

        public ICommand IncreaseStockCommand { get; }
        public ICommand DecreaseStockCommand { get; }

        public InventoryViewModel()
            : this(new InventoryService(new InventoryRepository("Data Source=fastlane.db")))

        {
        }

        public InventoryViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;

            IncreaseStockCommand = new Command<Sku>(async sku =>
            {
                if (sku == null) return;
                sku.QuantityOnHand++;
                await _inventoryService.UpdateInventoryAsync(sku.Id, 1);
                OnPropertyChanged(nameof(Skus));
            });

            DecreaseStockCommand = new Command<Sku>(async sku =>
            {
                if (sku == null || sku.QuantityOnHand <= 0) return;
                sku.QuantityOnHand--;
                await _inventoryService.UpdateInventoryAsync(sku.Id, -1);
                OnPropertyChanged(nameof(Skus));
            });

            _ = LoadSkusAsync();
        }

        private async Task LoadSkusAsync()
        {
            var skus = await _inventoryService.GetAllSkusAsync();
            if (skus != null)
            {
                foreach (var sku in skus)
                {
                    _skus.Add(sku);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
