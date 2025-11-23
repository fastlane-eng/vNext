using FastLane.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FastLane.App.Views
{
    public partial class ForecastPage : ContentPage
    {
        public ForecastPage()
        {
            InitializeComponent();
            BindingContext = new ForecastViewModel();
        }
    }
}
