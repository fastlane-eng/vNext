using FastLane.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FastLane.App.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel();
    }
}
