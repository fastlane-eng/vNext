using Microsoft.Maui;
using Microsoft.Maui.Controls;
using FastLane.App.Services;

namespace FastLane.App
{
    public partial class App : Application
    {
        // Static navigation service for managing page navigation across the app
        public static NavigationService NavigationService { get; private set; }

        public App()
        {
            InitializeComponent();

            // Initialize the navigation service
            NavigationService = new NavigationService();

            // Set the main page within a navigation page so we can push/pop pages
            MainPage = new NavigationPage(new Views.DashboardPage());
        }
    }
}
