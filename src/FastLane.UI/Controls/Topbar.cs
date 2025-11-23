using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a top bar component providing a consistent header across pages.
    /// This control displays a title centered between optional left and right action buttons.
    /// The left and right buttons can be bound to commands and text via BindableProperties.
    /// Colors and typography are driven from the application's theme resources.
    /// </summary>
    public class Topbar : ContentView
    {
        /// <summary>
        /// Backing BindableProperty for <see cref="Title"/>.
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title), typeof(string), typeof(Topbar), string.Empty);

        /// <summary>
        /// Backing BindableProperty for <see cref="LeftCommand"/>.
        /// </summary>
        public static readonly BindableProperty LeftCommandProperty =
            BindableProperty.Create(
                nameof(LeftCommand), typeof(ICommand), typeof(Topbar));

        /// <summary>
        /// Backing BindableProperty for <see cref="RightCommand"/>.
        /// </summary>
        public static readonly BindableProperty RightCommandProperty =
            BindableProperty.Create(
                nameof(RightCommand), typeof(ICommand), typeof(Topbar));

        /// <summary>
        /// Backing BindableProperty for <see cref="LeftButtonText"/>.
        /// </summary>
        public static readonly BindableProperty LeftButtonTextProperty =
            BindableProperty.Create(
                nameof(LeftButtonText), typeof(string), typeof(Topbar), string.Empty);

        /// <summary>
        /// Backing BindableProperty for <see cref="RightButtonText"/>.
        /// </summary>
        public static readonly BindableProperty RightButtonTextProperty =
            BindableProperty.Create(
                nameof(RightButtonText), typeof(string), typeof(Topbar), string.Empty);

        /// <summary>
        /// Gets or sets the title displayed at the center of the top bar.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Gets or sets the command executed when the left action button is tapped.
        /// </summary>
        public ICommand LeftCommand
        {
            get => (ICommand)GetValue(LeftCommandProperty);
            set => SetValue(LeftCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command executed when the right action button is tapped.
        /// </summary>
        public ICommand RightCommand
        {
            get => (ICommand)GetValue(RightCommandProperty);
            set => SetValue(RightCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the text displayed on the left action button. If null or empty, the button is hidden.
        /// </summary>
        public string LeftButtonText
        {
            get => (string)GetValue(LeftButtonTextProperty);
            set => SetValue(LeftButtonTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the text displayed on the right action button. If null or empty, the button is hidden.
        /// </summary>
        public string RightButtonText
        {
            get => (string)GetValue(RightButtonTextProperty);
            set => SetValue(RightButtonTextProperty, value);
        }

        private readonly Button _leftButton;
        private readonly Button _rightButton;
        private readonly Label _titleLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Topbar"/> class.
        /// </summary>
        public Topbar()
        {
            // Create UI elements
            _leftButton = new Button
            {
                BackgroundColor = Colors.Transparent,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 0, 10, 0)
            };
            _leftButton.SetBinding(Button.CommandProperty, new Binding(nameof(LeftCommand), source: this));
            _leftButton.SetBinding(Button.TextProperty, new Binding(nameof(LeftButtonText), source: this));
            _leftButton.SetBinding(IsVisibleProperty, new Binding(nameof(LeftButtonText), source: this, converter: new NullOrEmptyToBoolConverter()));
            _leftButton.TextColor = GetAccentColor();

            _rightButton = new Button
            {
                BackgroundColor = Colors.Transparent,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 0, 0, 0)
            };
            _rightButton.SetBinding(Button.CommandProperty, new Binding(nameof(RightCommand), source: this));
            _rightButton.SetBinding(Button.TextProperty, new Binding(nameof(RightButtonText), source: this));
            _rightButton.SetBinding(IsVisibleProperty, new Binding(nameof(RightButtonText), source: this, converter: new NullOrEmptyToBoolConverter()));
            _rightButton.TextColor = GetAccentColor();

            _titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                LineBreakMode = LineBreakMode.TailTruncation,
                TextColor = GetPrimaryTextColor()
            };
            _titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));

            // Compose layout
            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Auto }
                },
                Padding = new Thickness(10, 5),
                BackgroundColor = GetPrimaryColor()
            };

            grid.Children.Add(_leftButton, 0, 0);
            grid.Children.Add(_titleLabel, 1, 0);
            grid.Children.Add(_rightButton, 2, 0);

            Content = grid;
        }

        /// <summary>
        /// Attempts to resolve the accent color from application resources. Defaults to red if not found.
        /// </summary>
        private Color GetAccentColor()
        {
            return TryGetColorFromResource("AccentColor") ?? Color.FromArgb("#DA3435");
        }

        /// <summary>
        /// Attempts to resolve the primary color from application resources. Defaults to white.
        /// </summary>
        private Color GetPrimaryColor()
        {
            return TryGetColorFromResource("PrimaryColor") ?? Colors.White;
        }

        /// <summary>
        /// Attempts to resolve the primary text color from resources. Defaults to black.
        /// </summary>
        private Color GetPrimaryTextColor()
        {
            return TryGetColorFromResource("PrimaryTextColor") ?? Colors.Black;
        }

        /// <summary>
        /// Utility method to fetch a color resource by key.
        /// </summary>
        /// <param name="key">Resource key.</param>
        /// <returns>Color if found; otherwise null.</returns>
        private Color? TryGetColorFromResource(string key)
        {
            if (Application.Current?.Resources?.TryGetValue(key, out var value) == true && value is Color color)
            {
                return color;
            }
            return null;
        }
    }

    /// <summary>
    /// A simple converter that returns true if the supplied value is not null or empty; otherwise false.
    /// Used to toggle visibility of buttons based on their text.
    /// </summary>
    public class NullOrEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(value is string str) || !string.IsNullOrEmpty(str);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
