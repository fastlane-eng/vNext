using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastLane.Core.Models;

namespace FastLane.App.ViewModels
{
    public class ForecastViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Forecast> Forecasts { get; } = new ObservableCollection<Forecast>();

        public ForecastViewModel()
        {
            // Sample forecasts for demonstration
            Forecasts.Add(new Forecast { SkuId = 1, ForecastDate = System.DateTime.Today.AddDays(7), Quantity = 120 });
            Forecasts.Add(new Forecast { SkuId = 1, ForecastDate = System.DateTime.Today.AddDays(30), Quantity = 450 });
            Forecasts.Add(new Forecast { SkuId = 2, ForecastDate = System.DateTime.Today.AddDays(7), Quantity = 200 });
            Forecasts.Add(new Forecast { SkuId = 2, ForecastDate = System.DateTime.Today.AddDays(30), Quantity = 600 });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
