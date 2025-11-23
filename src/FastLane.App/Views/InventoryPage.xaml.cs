using FastLane.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FastLane.App.Views;

public partial class InventoryPage : ContentPage
{
    public InventoryPage()
    {
        InitializeComponent();
        BindingContext = new InventoryViewModel();
    }
}
