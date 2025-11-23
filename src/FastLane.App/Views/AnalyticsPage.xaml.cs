using FastLane.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FastLane.App.Views
{
    public partial class AnalyticsPage : ContentPage
    {
        public AnalyticsPage()
        {
            InitializeComponent();
            BindingContext = new AnalyticsViewModel();
        }
    }
}
