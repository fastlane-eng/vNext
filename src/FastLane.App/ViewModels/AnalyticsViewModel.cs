using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastLane.Core.Models;

namespace FastLane.App.ViewModels
{
    public class AnalyticsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Kpi> _kpis;
        public ObservableCollection<Kpi> Kpis
        {
            get => _kpis;
            set
            {
                if (_kpis != value)
                {
                    _kpis = value;
                    OnPropertyChanged();
                }
            }
        }

        public AnalyticsViewModel()
        {
            Kpis = new ObservableCollection<Kpi>
            {
                new Kpi { Name = "Total Revenue", Value = 120000m, Unit = "$" },
                new Kpi { Name = "Average Stock Turnover", Value = 30m, Unit = "days" },
                new Kpi { Name = "Gross Margin", Value = 0.35m, Unit = "%" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
