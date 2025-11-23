using System;

namespace FastLane.Core.Models
{
    public class Forecast
    {
        public int SkuId { get; set; }
        public DateTime ForecastDate { get; set; }
        public int Quantity { get; set; }
    }
}
