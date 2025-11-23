namespace FastLane.UI.Controls
{
    using System;
    using System.Windows.Input;
    using Microsoft.Maui.Controls;

    /// <summary>
    /// A card control for displaying a metric with current and previous values.
    /// Shows the metric name, current value, and the delta between current and previous.
    /// </summary>
    public class MetricCard : ContentView
    {
        /// <summary>
        /// Bindable property for the metric name displayed on the card.
        /// </summary>
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(MetricCard), string.Empty);

        /// <summary>
        /// Bindable property for the current value displayed on the card.
        /// </summary>
        public static readonly BindableProperty CurrentValueProperty =
            BindableProperty.Create(nameof(CurrentValue), typeof(double), typeof(MetricCard), 0.0);

        /// <summary>
        /// Bindable property for the previous value used to compute the delta.
        /// </summary>
        public static readonly BindableProperty PreviousValueProperty =
            BindableProperty.Create(nameof(PreviousValue), typeof(double), typeof(MetricCard), 0.0);

        /// <summary>
        /// Gets or sets the metric name displayed on the card.
        /// </summary>
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        /// <summary>
        /// Gets or sets the current value for the metric.
        /// </summary>
        public double CurrentValue
        {
            get => (double)GetValue(CurrentValueProperty);
            set => SetValue(CurrentValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the previous value used for comparison.
        /// </summary>
        public double PreviousValue
        {
            get => (double)GetValue(PreviousValueProperty);
            set => SetValue(PreviousValueProperty, value);
        }

        /// <summary>
        /// Gets the difference between the current and previous values.
        /// </summary>
        public double Delta => CurrentValue - PreviousValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricCard"/> class.
        /// Builds the visual structure and binds labels to properties.
        /// </summary>
        public MetricCard()
        {
            // Frame to provide padding, rounded corners and background.
            var frame = new Frame
            {
                Padding = new Thickness(16),
                CornerRadius = 8,
                HasShadow = true,
                BackgroundColor = (Color)Application.Current.Resources["SecondaryColor"]
            };

            // Metric name label.
            var nameLabel = new Label
            {
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["PrimaryColor"]
            };
            nameLabel.SetBinding(Label.TextProperty, new Binding(nameof(Name), source: this));

            // Current value label formatted with thousands separators.
            var currentLabel = new Label
            {
                FontSize = 28,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["AccentColor"]
            };
            currentLabel.SetBinding(Label.TextProperty, new Binding(nameof(CurrentValue), source: this, stringFormat: "{0:N0}"));

            // Delta label showing positive or negative difference.
            var deltaLabel = new Label
            {
                FontSize = 12,
                FontAttributes = FontAttributes.Italic,
                TextColor = (Color)Application.Current.Resources["PrimaryColor"]
            };
            deltaLabel.SetBinding(Label.TextProperty, new Binding(nameof(Delta), source: this, stringFormat: "{0:+0.##;-0.##;0}"));

            // Stack layout to organise text elements vertically.
            var stack = new StackLayout
            {
                Spacing = 2
            };

            stack.Children.Add(nameLabel);
            stack.Children.Add(currentLabel);
            stack.Children.Add(deltaLabel);

            frame.Content = stack;
            Content = frame;
        }
    }
}
