using FastLane.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FastLane.App.Views
{
    public partial class PurchaseOrdersPage : ContentPage
    {
        public PurchaseOrdersPage()
        {
            InitializeComponent();
            BindingContext = new PurchaseOrdersViewModel();
        }
    }
}
