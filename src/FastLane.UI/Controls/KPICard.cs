namespace FastLane.UI.Controls
{
    using System;
    using System.Windows.Input;
    using Microsoft.Maui.Controls;

    /// <summary>
    /// Represents a KPI card control to display a key performance indicator.
    /// This control shows a title, a formatted value and unit, and supports a tap command.
    /// </summary>
    public class KPICard : ContentView
    {
        /// <summary>
        /// Bindable property for the title text displayed on the card.
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(KPICard), string.Empty);

        /// <summary>
        /// Bindable property for the numeric value displayed on the card.
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(KPICard), 0.0);

        /// <summary>
        /// Bindable property for the unit associated with the value.
        /// </summary>
        public static readonly BindableProperty UnitProperty =
            BindableProperty.Create(nameof(Unit), typeof(string), typeof(KPICard), string.Empty);

        /// <summary>
        /// Bindable property for an optional command executed when the card is tapped.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(KPICard), default(ICommand));

        /// <summary>
        /// Gets or sets the title text displayed on the KPI card.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Gets or sets the numeric value displayed on the KPI card.
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the unit label displayed after the value.
        /// </summary>
        public string Unit
        {
            get => (string)GetValue(UnitProperty);
            set => SetValue(UnitProperty, value);
        }

        /// <summary>
        /// Gets or sets the command invoked when the user taps the card.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KPICard"/> class.
        /// Constructs the visual tree and sets up bindings.
        /// </summary>
        public KPICard()
        {
            // Create the outer frame to style the card with padding and rounded corners.
            var frame = new Frame
            {
                Padding = new Thickness(16),
                CornerRadius = 8,
                HasShadow = true,
                BackgroundColor = (Color)Application.Current.Resources["SecondaryColor"]
            };

            // Create labels and bind them to the control's properties.
            var titleLabel = new Label
            {
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["PrimaryColor"]
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));

            var valueLabel = new Label
            {
                FontSize = 32,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["AccentColor"]
            };
            // Format the numeric value using the current culture and thousands separators.
            valueLabel.SetBinding(Label.TextProperty, new Binding(nameof(Value), source: this, stringFormat: "{0:N0}"));

            var unitLabel = new Label
            {
                FontSize = 14,
                TextColor = (Color)Application.Current.Resources["PrimaryColor"]
            };
            unitLabel.SetBinding(Label.TextProperty, new Binding(nameof(Unit), source: this));

            // Assemble the layout.
            var stack = new StackLayout
            {
                Spacing = 4
            };
            stack.Children.Add(titleLabel);
            stack.Children.Add(valueLabel);
            stack.Children.Add(unitLabel);

            frame.Content = stack;
            Content = frame;

            // Tap gesture recognizer executes the bound command if present.
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (Command != null && Command.CanExecute(null))
                    {
                        Command.Execute(null);
                    }
                })
            });
        }
    }
}
