using System;
using Microsoft.Maui.Controls;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// Provides a simple panel with a header and body area. The panel uses theme colours for
    /// background, accent border and text. The body can be any UI element and is bound
    /// via the <see cref="Body"/> property.
    /// </summary>
    public class Panel : ContentView
    {
        /// <summary>
        /// Identifies the <see cref="Header"/> bindable property. This defines the text displayed
        /// in the panel header.
        /// </summary>
        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create(nameof(Header), typeof(string), typeof(Panel), string.Empty);

        /// <summary>
        /// Identifies the <see cref="Body"/> bindable property. This defines the content displayed
        /// in the panel body.
        /// </summary>
        public static readonly BindableProperty BodyProperty =
            BindableProperty.Create(nameof(Body), typeof(View), typeof(Panel), null);

        /// <summary>
        /// Gets or sets the header text for the panel.
        /// </summary>
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the body content for the panel. This can be any view element.
        /// </summary>
        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        private readonly Label _headerLabel;
        private readonly ContentView _bodyContainer;
        private readonly Frame _frame;

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class and constructs the layout
        /// with a header label and body container inside a frame. Colours are pulled from
        /// theme resources when available.
        /// </summary>
        public Panel()
        {
            // Header label bound to the Header property.
            _headerLabel = new Label
            {
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = GetPrimaryTextColor(),
                Margin = new Thickness(0, 0, 0, 4)
            };
            _headerLabel.SetBinding(Label.TextProperty, new Binding(nameof(Header), source: this));

            // Container for body content bound to Body property.
            _bodyContainer = new ContentView();
            _bodyContainer.SetBinding(ContentView.ContentProperty, new Binding(nameof(Body), source: this));

            // Stack panel organizes header and body vertically.
            var stack = new StackLayout
            {
                Spacing = 0
            };
            stack.Children.Add(_headerLabel);
            stack.Children.Add(_bodyContainer);

            // Frame provides border, background and shadow for the panel.
            _frame = new Frame
            {
                CornerRadius = 6,
                Padding = new Thickness(12),
                BackgroundColor = GetSecondaryColor(),
                BorderColor = GetAccentColor(),
                HasShadow = true,
                Content = stack
            };

            Content = _frame;
        }

        /// <summary>
        /// Resolves the accent colour from theme resources or defaults to #DA3435.
        /// </summary>
        private ColorLegacy GetAccentColor() => TryGetColorFromResource("AccentColor") ?? Color.FromArgb("#DA3435");

        /// <summary>
        /// Resolves the secondary colour from theme resources or defaults to #F5F5F5.
        /// </summary>
        private ColLegacyor GetSecondaryColor() => TryGetColorFromResource("SecondaryColor") ?? Color.FromArgb("#F5F5F5");

        /// <summary>
        /// Resolves the primary text colour from theme resources or defaults to black.
        /// </summary>
        pr ColLegacyor GetPrimaryTextColor() => TryGetColorFromResource("PrimaryTextColor") ?? Colors.Black;

        /// <summary>
        /// Attempts to retrieve a colour resource by key.
        /// </summary>
        /// <param name="key">The key of the resource.</param>
        /// <returns>The colour if found; otherwise null.</returns>
        private Color? TryGetColorFromResource(string key)
        {
            if (Application.Current?.Resources?.TryGetValue(key, out var value) == true && value is Color color)
            {
                return color;
            }
            return null;
        }
    }
}
