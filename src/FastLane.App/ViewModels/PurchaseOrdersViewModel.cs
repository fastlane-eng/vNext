using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastLane.Core.Models;

namespace FastLane.App.ViewModels
{
    public class PurchaseOrdersViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PurchaseOrder> PurchaseOrders { get; } = new ObservableCollection<PurchaseOrder>();

        public PurchaseOrdersViewModel()
        {
            // Sample data
            PurchaseOrders.Add(new PurchaseOrder
            {
                Id = 1,
                CreatedDate = System.DateTime.Now.AddDays(-1),
                SupplierId = 1,
                Lines = new System.Collections.Generic.List<PurchaseOrder.PurchaseOrderLine>
                {
                    new PurchaseOrder.PurchaseOrderLine { SkuId = 1, Quantity = 50, UnitPrice = 12.5m },
                    new PurchaseOrder.PurchaseOrderLine { SkuId = 2, Quantity = 100, UnitPrice = 8.0m }
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
