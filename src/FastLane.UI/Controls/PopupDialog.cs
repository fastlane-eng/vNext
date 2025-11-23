using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a reusable popup dialog that can display a title, message and optional accept/cancel buttons.
    /// The dialog can be toggled via the <see cref="IsOpen"/> property and exposes commands for the primary and secondary actions.
    /// </summary>
    public class PopupDialog : ContentView
    {
        /// <summary>
        /// Bindable property to control whether the dialog is visible.
        /// </summary>
        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(
                nameof(IsOpen),
                typeof(bool),
                typeof(PopupDialog),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var control = (PopupDialog)bindable;
                    control._overlay.IsVisible = (bool)newValue;
                });

        /// <summary>
        /// Gets or sets a value indicating whether the dialog is currently displayed.
        /// </summary>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        /// Bindable property for the dialog title.
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(PopupDialog), default(string));

        /// <summary>
        /// Gets or sets the title of the dialog.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Bindable property for the dialog message.
        /// </summary>
        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(PopupDialog), default(string));

        /// <summary>
        /// Gets or sets the message displayed in the dialog.
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// Bindable property for the primary button text.
        /// </summary>
        public static readonly BindableProperty PrimaryButtonTextProperty =
            BindableProperty.Create(nameof(PrimaryButtonText), typeof(string), typeof(PopupDialog), "OK");

        /// <summary>
        /// Gets or sets the text for the primary action button.
        /// </summary>
        public string PrimaryButtonText
        {
            get => (string)GetValue(PrimaryButtonTextProperty);
            set => SetValue(PrimaryButtonTextProperty, value);
        }

        /// <summary>
        /// Bindable property for the secondary button text.
        /// </summary>
        public static readonly BindableProperty SecondaryButtonTextProperty =
            BindableProperty.Create(nameof(SecondaryButtonText), typeof(string), typeof(PopupDialog), "Cancel");

        /// <summary>
        /// Gets or sets the text for the secondary action button.
        /// </summary>
        public string SecondaryButtonText
        {
            get => (string)GetValue(SecondaryButtonTextProperty);
            set => SetValue(SecondaryButtonTextProperty, value);
        }

        /// <summary>
        /// Bindable property for the command executed when the primary button is pressed.
        /// </summary>
        public static readonly BindableProperty PrimaryCommandProperty =
            BindableProperty.Create(nameof(PrimaryCommand), typeof(ICommand), typeof(PopupDialog), default(ICommand));

        /// <summary>
        /// Gets or sets the command executed when the primary button is pressed.
        /// </summary>
        public ICommand PrimaryCommand
        {
            get => (ICommand)GetValue(PrimaryCommandProperty);
            set => SetValue(PrimaryCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for the command executed when the secondary button is pressed.
        /// </summary>
        public static readonly BindableProperty SecondaryCommandProperty =
            BindableProperty.Create(nameof(SecondaryCommand), typeof(ICommand), typeof(PopupDialog), default(ICommand));

        /// <summary>
        /// Gets or sets the command executed when the secondary button is pressed.
        /// </summary>
        public ICommand SecondaryCommand
        {
            get => (ICommand)GetValue(SecondaryCommandProperty);
            set => SetValue(SecondaryCommandProperty, value);
        }

        // Internal overlay grid used to toggle visibility.
        private readonly Grid _overlay;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupDialog"/> class.
        /// </summary>
        public PopupDialog()
        {
            // Create semi-transparent overlay
            _overlay = new Grid
            {
                IsVisible = false,
                BackgroundColor = new Color(0, 0, 0, 0.4),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Create the dialog frame
            var dialogFrame = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["SecondaryColor"],
                BorderColor = (Color)Application.Current.Resources["AccentColor"],
                CornerRadius = 8,
                Padding = new Thickness(24),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Title label
            var titleLabel = new Label
            {
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["AccentColor"]
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));

            // Message label
            var messageLabel = new Label
            {
                FontSize = 16,
                TextColor = (Color)Application.Current.Resources["PrimaryColor"],
                Margin = new Thickness(0, 8, 0, 16)
            };
            messageLabel.SetBinding(Label.TextProperty, new Binding(nameof(Message), source: this));

            // Primary button
            var primaryButton = new Button
            {
                BackgroundColor = (Color)Application.Current.Resources["AccentColor"],
                TextColor = (Color)Application.Current.Resources["SecondaryColor"],
                CornerRadius = 4,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            primaryButton.SetBinding(Button.TextProperty, new Binding(nameof(PrimaryButtonText), source: this));
            primaryButton.Clicked += (s, e) =>
            {
                PrimaryCommand?.Execute(null);
                IsOpen = false;
            };

            // Secondary button
            var secondaryButton = new Button
            {
                BackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                TextColor = (Color)Application.Current.Resources["AccentColor"],
                CornerRadius = 4,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            secondaryButton.SetBinding(Button.TextProperty, new Binding(nameof(SecondaryButtonText), source: this));
            secondaryButton.Clicked += (s, e) =>
            {
                SecondaryCommand?.Execute(null);
                IsOpen = false;
            };

            // Buttons layout
            var buttonsGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star }
                },
                ColumnSpacing = 12
            };
            buttonsGrid.Children.Add(primaryButton);
            Grid.SetColumn(primaryButton, 0);
            buttonsGrid.Children.Add(secondaryButton);
            Grid.SetColumn(secondaryButton, 1);

            // Stack all elements in a vertical stack
            var contentStack = new StackLayout
            {
                Spacing = 0
            };
            contentStack.Children.Add(titleLabel);
            contentStack.Children.Add(messageLabel);
            contentStack.Children.Add(buttonsGrid);

            dialogFrame.Content = contentStack;

            // Add the dialog frame to the overlay
            _overlay.Children.Add(dialogFrame);

            Content = _overlay;
        }
    }
}
