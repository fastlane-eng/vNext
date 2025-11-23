using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FastLane.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.DashboardPage());
        }
    }
}
