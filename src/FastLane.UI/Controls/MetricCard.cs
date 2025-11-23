namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a metric card for comparing current and previous values.
    /// </summary>
    public class MetricCard
    {
        public string Name { get; set; }
        public double CurrentValue { get; set; }
        public double PreviousValue { get; set; }

        public MetricCard(string name, double currentValue, double previousValue)
        {
            Name = name;
            CurrentValue = currentValue;
            PreviousValue = previousValue;
        }

        public double Delta => CurrentValue - PreviousValue;
    }
}
