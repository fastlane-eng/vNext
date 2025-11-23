using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FastLane.App.Services
{
    public class NavigationService
    {
        public async Task NavigateToAsync(Page page)
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                await navPage.Navigation.PushAsync(page);
            }
        }

        public async Task NavigateBackAsync()
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                await navPage.Navigation.PopAsync();
            }
        }
    }
}
