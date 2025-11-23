using Microsoft.Maui;
using Microsoft.Maui.Controls;
using FastLane.App.Services;
using FastLane.Services;
using FastLane.Data.Repositories;
using FastLane.Core.Interfaces;

namespace FastLane.App
{
    public partial class App : Application
    {
        // Static navigation service for managing page navigation across the app
        public static NavigationService NavigationService { get; private set; }

        // Static inventory service for accessing inventory data
        public static IInventoryService InventoryService { get; private set; }

        public App()
        {
            InitializeComponent();

            // Initialize services
            NavigationService = new NavigationService();
            InventoryService = new InventoryService(new InventoryRepository());

            // Set the main page within a navigation page so we can push/pop pages
            MainPage = new NavigationPage(new Views.DashboardPage());
        }
    }
}
