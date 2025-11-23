namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a KPI card for displaying a key performance indicator.
    /// </summary>
    public class KPICard
    {
        public string Title { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }

        public KPICard(string title, double value, string unit)
        {
            Title = title;
            Value = value;
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Title}: {Value} {Unit}";
        }
    }
}
